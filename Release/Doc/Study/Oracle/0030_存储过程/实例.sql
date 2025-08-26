CREATE OR REPLACE PROCEDURE P_XX_NAME
(
       V_ORDER_CODE   IN VARCHAR2, --输入参数：单据编号
       V_ORDER_TYPE   IN VARCHAR2, --输入参数：单据类型
       V_RET          OUT NUMBER   --输出参数：0失败，1成功
) IS
/***********************************************************************************************
*对象名称：xxxx
*对象类型：存储过程
*创建作者：xxx
*创建日期：xxxx-xx-xx
*主要功能: 本示例仅作为示例
*修改历史：
*   xxxx-xx-xx：新增 by xxx
**********************************************************************************************/
  V_COUNT NUMBER; --整型
  MyException EXCEPTION; --自定义异常

  V_FACT_STORE NUMBER(20, 6) := 0; --小数6位
  D_NOW_NOT_OUT_QTY NUMBER(14, 4) := 0;--本次未出库的数量
  D_NOW_OUT_QTY NUMBER(14, 4) := 0;--本次出库的数量
	
  V_DLR_STORAGE_ID VARCHAR2(50); --专营店库存ID
  V_CHECK_MODEL VARCHAR2(10);

  --游标1：待处理的入出库单明细数据游标
  CURSOR CUR_DEAL_LIST IS 
	SELECT A.* 
	FROM T_TMP_COSTW A;
  REC_DEAL_LIST CUR_DEAL_LIST%ROWTYPE;  --游标记录的行类型

  --根据专营店ID和备件ID，查找备件库存批次明细游标（专营店ID，备件ID）
  CURSOR CUR_STORE_BAT(V_DLR_ID VARCHAR2,V_PART_ID VARCHAR2) IS
    SELECT A.DLR_STORAGE_D_ID,A.DLR_STORAGE_ID,
		       NVL(A.OUT_STORE_QTY,0) AS OUT_STORE_QTY,
           (A.PATCH_QTY - NVL(A.OUT_STORE_QTY,0)) AS NOT_OUT_QTY,
					 A.BATCH_NO,A.PATCH_BILL_TYPE,A.PATCH_DATE,A.PATCH_QTY,A.UP_DLR_ID
      FROM T_STORAGE_D A /*专营店代管库库存*/
     WHERE A.UP_DLR_ID = V_DLR_ID
       AND A.PART_ID = V_PART_ID
       AND A.PATCH_QTY > 0 --只针对有数量的
       ORDER BY A.PATCH_DATE; --按批次时间正序排列
  REC_STORE_BAT CUR_STORE_BAT%ROWTYPE;

BEGIN
  V_RET := 1;
	--先清除临时表
	DELETE FROM T_TMP_COSTW;
    /*一、确定备件范围游标*/
    INSERT INTO  T_TMP_COSTW  	/*专营店代管库成本临时表*/
	(
		COST_ESCROW_ID,
		BILL_BIG_TYPE,
		BILL_TYPE
	)
	SELECT SYS_GUID(),	/*COST_ESCROW_ID:代管库成本ID*/
	    A.BILL_BIG_TYPE,--BILL_BIG_TYPE,
		A.BILL_TYPE--BILL_TYPE
	FROM T_PA_UP_ESCROW_IN_STORE A
	UNION ALL
	SELECT SYS_GUID(),	/*COST_ESCROW_ID:代管库成本ID*/
	     OA.BILL_BIG_TYPE,--BILL_BIG_TYPE,
		OA.BILL_TYPE--BILL_TYPE,
	FROM T_UP_OUT OA
	;
	
	--判断临时表中是否存在入出数量小于等于0的
	SELECT COUNT(1) INTO V_COUNT
	FROM T_PA_UP_TMP_COST_ESCROW A
	WHERE A.NEED_QTY <= 0;

	IF V_COUNT>0 THEN --为空
		RAISE_APPLICATION_ERROR(-20999, '更新xxx失败。请保证入出库单中的数量大于0！');
		RETURN;
	END IF;
	
	 /*新增库存主表信息*/
	INSERT INTO  T_STORAGE  /*专营店备件库存（DLR)*/
	(
		 DLR_STORAGE_ID,
		 PART_ID,
		 PART_NO,
		 WAREHOUSE_ID,
		 PLACE_ID,
		 STORE_QTY,
		 DLR_ID,
		 CREATOR,
		 CREATED_DATE,
		 MODIFIER,
		 LAST_UPDATED_DATE,
		 IS_ENABLE,
		 UPDATE_CONTROL_ID,
		 UP_DLR_ID,
		 WELL_QTY,
		 BADNESS_QTY,
		 IS_SYSTEM
	)
	SELECT
		SYS_GUID(),  /*DLR_STORAGE_ID:DLR_STORAGE*/
		A.PART_ID,  /*PART_ID:备件ID*/
		A.PART_NO,  /*PART_NO:备件编码*/
		A.WAREHOUSE_ID,  /*WAREHOUSE_ID:仓库ID*/
		A.PLACE_ID,  /*PLACE_ID:仓位ID*/
		0,  /*STORE_QTY:库存数量*/
		A.DLR_ID,  /*DLR_ID:专营店ID*/
		A.CREATOR,  /*CREATOR:创建人*/
		SYSDATE,  /*CREATED_DATE:创建时间*/
		A.CREATOR,  /*MODIFIER:最后更新人员*/
		SYSDATE,  /*LAST_UPDATED_DATE:最后更新时间*/
		'1',  /*IS_ENABLE:是否可用 0:不可用,1:可用*/
		SYS_GUID(),  /*UPDATE_CONTROL_ID:并发控制字段*/
		A.UP_DLR_ID,
		0,
		0,
		'0'
	FROM T_PA_UP_TMP_COST_ESCROW A
	WHERE NOT EXISTS(SELECT 1 FROM T_STORAGE S
	                 WHERE S.UP_DLR_ID = A.UP_DLR_ID AND S.PART_ID = A.PART_ID 
									       AND A.WAREHOUSE_ID = S.WAREHOUSE_ID);
  
	/*锁定库存*/                       
	UPDATE T_STORAGE A
	SET A.UPDATE_CONTROL_ID = SYS_GUID()
	WHERE EXISTS(SELECT 1 FROM T_PA_UP_TMP_COST_ESCROW T
	      WHERE T.PART_ID = A.PART_ID AND T.WAREHOUSE_ID = A.WAREHOUSE_ID AND T.UP_DLR_ID = A.UP_DLR_ID);
												 
  /*二、针对每个备件，计算库存成本*/
  OPEN CUR_DEAL_LIST;
  LOOP
    FETCH CUR_DEAL_LIST
      INTO REC_DEAL_LIST;
    IF CUR_DEAL_LIST%FOUND THEN

      --得到库存ID
      SELECT DLR_STORAGE_ID INTO V_DLR_STORAGE_ID
      FROM T_STORAGE A
      WHERE A.PART_ID = REC_DEAL_LIST.PART_ID
         AND A.UP_DLR_ID = REC_DEAL_LIST.UP_DLR_ID
				 AND A.WAREHOUSE_ID = REC_DEAL_LIST.WAREHOUSE_ID;

      ---------------------入库---------------------------------------------
      IF V_ORDER_TYPE = '1' THEN
        /*更新库存明细表*/
        UPDATE T_STORAGE A/*M003_库存明细*/
           SET WAREHOUSE_ID       = REC_DEAL_LIST.WAREHOUSE_ID /*仓库名称*/,
               PLACE_ID           = REC_DEAL_LIST.PLACE_ID /*仓位编码*/,
               STORE_QTY          = NVL(STORE_QTY, 0) + REC_DEAL_LIST.NEED_QTY ,/*库存数量*/
               LAST_UPDATED_DATE  = SYSTIMESTAMP,
               MODIFIER           = REC_DEAL_LIST.CREATOR /*更新人员*/
         WHERE A.DLR_STORAGE_ID = V_DLR_STORAGE_ID;
         /*1.3新增批次库存明细*/
        INSERT INTO  T_STORAGE_D   /*专营店库存明细表*/
        (
           DLR_STORAGE_D_ID,
           DLR_STORAGE_ID,
           DLR_ID,
           PART_ID,
           PART_NO,
           BATCH_NO,
           PATCH_DATE,
           PATCH_QTY,
           OUT_STORE_QTY,
           IS_ALL_OUT,
           CREATED_DATE,
           CREATOR,
           LAST_UPDATED_DATE,
           MODIFIER,
           UPDATE_CONTROL_ID,
					 UP_DLR_ID
        )
        SELECT SYS_GUID(),  /*DLR_STORAGE_D_ID:库存明细ID*/
          V_DLR_STORAGE_ID,  /*DLR_STORAGE_ID:库存ID  T_STORAGE库存表ID*/
          A.WAREHOUSE_ID,  /*DLR_ID:专营店ID*/
          A.PART_ID,  /*PART_ID:备件ID*/
          A.PART_NO,  /*PART_NO:备件编码*/
          B.IN_STORE_CODE,  /*BATCH_NO:批次号  已入库单号为批次号*/
          B.IN_STORE_DATE,  /*PATCH_DATE:批次时间  入库时间*/
          A.IN_STORE_QTY,  /*PATCH_QTY:批次总数量  批次总数量=账面库存=良品数+不良品数量，为0时则已出完*/
          0,--OUT_STORE_QTY
          '0',  /*IS_ALL_OUT:是否已出完  0未出完1已出完(批次总数量为0时为1）*/
          SYSDATE,  /*CREATED_DATE:创建时间*/
          'COST',  /*CREATOR:创建人*/
          SYSDATE,  /*LAST_UPDATED_DATE:最后更新时间 最后修改时间*/
          'COST',  /*MODIFIER:最后更新人员 最后修改人*/
          SYS_GUID(),  /*UPDATE_CONTROL_ID:并发控制字段*/
					A.UP_DLR_ID
        FROM T_PA_UP_ESCROW_IN_STORE_D A /*备件采购入库明细表*/
        JOIN T_PA_UP_ESCROW_IN_STORE B ON A.IN_STORE_ID = B.IN_STORE_ID
        WHERE A.IN_STORE_CODE = V_ORDER_CODE
              AND A.PART_ID = REC_DEAL_LIST.PART_ID
              AND ROWNUM=1;
        
			END IF;

      --------------------入库退货------------------------------------------
      IF V_ORDER_TYPE = '2' THEN
        --查询实际库存，以明细表为准
        SELECT NVL(PS.STORE_QTY, 0)
          INTO V_FACT_STORE
          FROM T_STORAGE PS
         WHERE PS.DLR_STORAGE_ID = V_DLR_STORAGE_ID;
        /*当实际库存(含库存数量、可用良品数量、不可用良品数量)不满足时，修改返回信息为0后直接退出*/
        IF V_FACT_STORE - REC_DEAL_LIST.NEED_QTY < 0 THEN
          V_RET := 0;
          CLOSE CUR_DEAL_LIST;
          RAISE MyException; /*抛出库存不足错误*/
          EXIT;
        END IF;
        /*更新库存明细表*/
        UPDATE T_STORAGE A/*M003_库存明细*/
           SET STORE_QTY          = NVL(STORE_QTY, 0) - REC_DEAL_LIST.NEED_QTY /*库存数量*/,
               LAST_UPDATED_DATE  = SYSTIMESTAMP,
               MODIFIER           = REC_DEAL_LIST.CREATOR /*更新人员*/
         WHERE A.DLR_STORAGE_ID = V_DLR_STORAGE_ID;      
      
			END IF;

      --------------------出库----------------------------------------------
      IF V_ORDER_TYPE = '3' THEN
        --查询实际库存，以明细表为准
        SELECT NVL(PS.STORE_QTY, 0)
          INTO V_FACT_STORE
          FROM T_STORAGE PS
         WHERE PS.DLR_STORAGE_ID = V_DLR_STORAGE_ID;

        /*当实际库存(含库存数量、可用良品数量、不可用良品数量)不满足时，修改返回信息为0后直接退出*/
        IF V_FACT_STORE - REC_DEAL_LIST.NEED_QTY < 0 THEN
          V_RET := 0;
          CLOSE CUR_DEAL_LIST;
          RAISE MyException; /*抛出库存不足错误*/
          EXIT;
        END IF;
        /*更新库存明细表*/
        UPDATE T_STORAGE A/*M003_库存明细*/
           SET STORE_QTY          = NVL(STORE_QTY, 0) - REC_DEAL_LIST.NEED_QTY /*库存数量*/,
               LAST_UPDATED_DATE  = SYSTIMESTAMP,
               MODIFIER           = REC_DEAL_LIST.CREATOR /*更新人员*/
         WHERE A.DLR_STORAGE_ID = V_DLR_STORAGE_ID;
					 
      END IF;

      ---------------------出库退货----------------------------------
      IF V_ORDER_TYPE = '4' THEN
        /*更新库存明细表*/
        UPDATE T_STORAGE A/*M003_库存明细*/
           SET PLACE_ID         = REC_DEAL_LIST.PLACE_ID /*仓位编码*/,
               STORE_QTY          = NVL(STORE_QTY, 0) + REC_DEAL_LIST.NEED_QTY /*库存数量*/,
               LAST_UPDATED_DATE  = SYSTIMESTAMP,
               MODIFIER           = REC_DEAL_LIST.CREATOR /*更新人员*/
         WHERE A.DLR_STORAGE_ID = V_DLR_STORAGE_ID;
        
        --未出库退货数量：初始值为本次出库退货数量
        --注：出库退货只有良品，没有不良品
        D_NOW_NOT_OUT_QTY := REC_DEAL_LIST.NEED_QTY;
        --游标处理
        OPEN CUR_OUT_BAT(REC_DEAL_LIST.DLR_ID,REC_DEAL_LIST.PART_ID,V_ORDER_CODE);
        LOOP
          FETCH CUR_OUT_BAT
            INTO REC_OUT_BAT;
          IF CUR_OUT_BAT%FOUND THEN
              --全部退完，则退出
              IF D_NOW_NOT_OUT_QTY <= 0 THEN
                CLOSE CUR_OUT_BAT;
                EXIT;
              END IF;

              /*判断库存表中的批次是否已存在*/
              SELECT COUNT(1) INTO  V_COUNT
              FROM T_STORAGE_D A
              WHERE A.PART_NO = REC_DEAL_LIST.PART_NO
                 AND A.UP_DLR_ID = REC_DEAL_LIST.UP_DLR_ID
                 AND A.BATCH_NO = REC_OUT_BAT.BATCH_NO;
              --良品批次库存处理
              IF V_COUNT > 0 THEN --该批次存在
                --已出库数量全部满足本次退货数量
                IF REC_STORE_BAT.OUT_STORE_QTY >= D_NOW_NOT_OUT_QTY THEN
									D_NOW_OUT_QTY := D_NOW_NOT_OUT_QTY;
                  --修改已出库数量=原已出库数量-本次退货数量
                   UPDATE T_STORAGE_D T
                     SET T.OUT_STORE_QTY = T.OUT_STORE_QTY - D_NOW_NOT_OUT_QTY
                   WHERE T.DLR_STORAGE_D_ID = REC_OUT_BAT.DLR_STORAGE_D_ID;
                   --退出游标
                   CLOSE CUR_OUT_BAT;
                   EXIT;
                 ELSIF REC_STORE_BAT.OUT_STORE_QTY>0 AND  REC_STORE_BAT.OUT_STORE_QTY < D_NOW_NOT_OUT_QTY THEN
                   D_NOW_OUT_QTY := REC_STORE_BAT.OUT_STORE_QTY;
									 --修改已出库数量为0
                   UPDATE T_STORAGE_D T
                     SET T.OUT_STORE_QTY = 0
                   WHERE T.DLR_STORAGE_D_ID = REC_OUT_BAT.DLR_STORAGE_D_ID;
                 END IF;
              ELSE --批次不存在
                --查询原来的入库单来新增库存批次记录
                INSERT INTO  T_STORAGE_D   /*专营店库存明细表*/
                (
                   DLR_STORAGE_D_ID,
                   DLR_STORAGE_ID,
                   DLR_ID,
                   PART_ID,
                   PART_NO,
                   BATCH_NO,
                   PATCH_DATE,
                   PATCH_QTY,
                   IS_ALL_OUT,
                   CREATED_DATE,
                   CREATOR,
                   LAST_UPDATED_DATE,
                   MODIFIER,
                   UPDATE_CONTROL_ID,
                   OUT_STORE_QTY
                )
                SELECT SYS_GUID(),  /*DLR_STORAGE_D_ID:库存明细ID*/
                  REC_OUT_BAT.DLR_STORAGE_ID,  /*DLR_STORAGE_ID:库存ID  T_STORAGE库存表ID*/
                  REC_DEAL_LIST.WAREHOUSE_ID,  /*DLR_ID:专营店ID*/
                  REC_OUT_BAT.PART_ID,  /*PART_ID:备件ID*/
                  REC_OUT_BAT.PART_NO,  /*PART_NO:备件编码*/
                  REC_OUT_BAT.BATCH_NO,  /*BATCH_NO:批次号  已入库单号为批次号*/
                  REC_OUT_BAT.PATCH_DATE,  /*PATCH_DATE:批次时间  入库时间*/
                  REC_OUT_BAT.OUT_STORE_QTY,  /*PATCH_QTY:批次总数量  批次总数量=账面库存=良品数+不良品数量，为0时则已出完*/
                  '0',  /*IS_ALL_OUT:是否已出完  0未出完1已出完(批次总数量为0时为1）*/
                  SYSDATE,  /*CREATED_DATE:创建时间*/
                  'COST',  /*CREATOR:创建人*/
                  SYSDATE,  /*LAST_UPDATED_DATE:最后更新时间 最后修改时间*/
                  'COST',  /*MODIFIER:最后更新人员 最后修改人*/
                  SYS_GUID(),  /*UPDATE_CONTROL_ID:并发控制字段*/
                  0--OUT_STORE_QTY,
                FROM T_PA_UP_ESCROW_IN_STORE_D A
                LEFT JOIN T_PA_UP_ESCROW_IN_STORE B
                     ON A.IN_STORE_ID = B.IN_STORE_ID
                WHERE A.IN_STORE_CODE = REC_OUT_BAT.BATCH_NO
                      AND A.PART_ID = REC_OUT_BAT.PART_ID;
              END IF;
							
				--新增出库退货的批次
				INSERT INTO  T_PA_UP_ESCROW_OUT_STORE_BAT  	/*代管库出库单_批次*/
				(
					OUT_STORE_BAT_ID,
					OUT_STORE_D_DLR_ID,
					OUT_STORE_ID,
					OUT_STORE_CODE,
					PART_ID,
					PART_NO,
					UNIT,
					BATCH_NO,
					PATCH_DATE,
					OUT_STORE_QTY,
					REMARK,
					DLR_ID,
					UP_DLR_ID,
					IS_ENABLE,
					CREATED_DATE,
					CREATOR,
					LAST_UPDATED_DATE,
					MODIFIER,
					UPDATE_CONTROL_ID,
					BILL_TYPE
				)
				VALUES
				(
					SYS_GUID(),	/*OUT_STORE_BAT_ID:专营店备件出库批次明细ID*/
					REC_DEAL_LIST.ORDER_D_ID,	/*OUT_STORE_D_DLR_ID:专营店备件出库单_明细ID*/
					REC_DEAL_LIST.ORDER_ID,	/*OUT_STORE_ID:专营店备件出库单 T_PA_BU_DLR_OUT_STORE.OUT_STORE_ID*/
					REC_DEAL_LIST.ORDER_CODE,	/*OUT_STORE_CODE:出库单编号*/
					REC_DEAL_LIST.PART_ID,	/*PART_ID:备件ID*/
					REC_DEAL_LIST.PART_NO,	/*PART_NO:备件编码*/
					REC_DEAL_LIST.PART_UNIT,	/*UNIT:单位*/
					REC_OUT_BAT.BATCH_NO,	/*BATCH_NO:批次号 已入库单号为批次号*/
					REC_OUT_BAT.PATCH_DATE,	/*PATCH_DATE:批次时间 入库时间*/
					D_NOW_OUT_QTY,	/*OUT_STORE_QTY:出库数量*/
					'自动生成',	/*REMARK:备注*/
					REC_OUT_BAT.UP_DLR_ID,	/*DLR_ID:专营店ID*/
					REC_OUT_BAT.UP_DLR_ID,	/*UP_DLR_ID:统采店ID*/
					1,	/*IS_ENABLE:是否可用 0无效，1有效*/
					SYSDATE,	/*CREATED_DATE:创建时间*/
					REC_DEAL_LIST.CREATOR,	/*CREATOR:创建人*/
					SYSDATE,	/*LAST_UPDATED_DATE:最后更新时间*/
					REC_DEAL_LIST.CREATOR,	/*MODIFIER:最后更新人员*/
					SYS_GUID(),	/*UPDATE_CONTROL_ID:并发控制字段*/
					REC_OUT_BAT.BILL_TYPE	/*BILL_TYPE:单据类型*/
				);

          ELSE
            CLOSE CUR_OUT_BAT;
            EXIT;
          END IF;
        END LOOP; 
        
      END IF;

      /*入库退货与出库：利用游标实现更新批次库存明细，注批次只管总数量*/
      IF V_ORDER_TYPE = '2' OR V_ORDER_TYPE = '3' THEN
        --未出库数量：初始值为本次出库数量
        D_NOW_NOT_OUT_QTY := REC_DEAL_LIST.NEED_QTY;
        --游标处理
        OPEN CUR_STORE_BAT(REC_DEAL_LIST.DLR_ID,REC_DEAL_LIST.PART_ID);
        LOOP
          FETCH CUR_STORE_BAT
            INTO REC_STORE_BAT;
          IF CUR_STORE_BAT%FOUND THEN
              --全部出完，则退出
              IF D_NOW_NOT_OUT_QTY <= 0 THEN
                CLOSE CUR_STORE_BAT;
                EXIT;
              END IF;

              --批次库存处理
              IF REC_STORE_BAT.NOT_OUT_QTY  > 0 THEN --该批次可出库
                --全部满足
                IF REC_STORE_BAT.NOT_OUT_QTY >= D_NOW_NOT_OUT_QTY THEN
								  D_NOW_OUT_QTY := D_NOW_NOT_OUT_QTY;
                  --更新批次明细：已出库数量=当前出库数量 + 本次未出库数量
                  UPDATE T_STORAGE_D T
                   SET T.OUT_STORE_QTY = NVL(T.OUT_STORE_QTY,0) + D_NOW_NOT_OUT_QTY,
                       T.IS_ALL_OUT = CASE WHEN REC_STORE_BAT.NOT_OUT_QTY = D_NOW_NOT_OUT_QTY THEN '1' ELSE '0' END
                   WHERE T.DLR_STORAGE_D_ID = REC_STORE_BAT.DLR_STORAGE_D_ID;
                  --本次未出库数量清0
                  D_NOW_NOT_OUT_QTY:=0;
                ELSE
                  --部分满足
									D_NOW_OUT_QTY := REC_STORE_BAT.NOT_OUT_QTY;
                  --更新批次明细：已出库数量=当前出库数量 + 本批次数量
                  UPDATE T_STORAGE_D T
                   SET T.OUT_STORE_QTY = NVL(T.OUT_STORE_QTY,0) + REC_STORE_BAT.NOT_OUT_QTY,
                       T.IS_ALL_OUT = '1' --该批次全部出完库
                   WHERE T.DLR_STORAGE_D_ID = REC_STORE_BAT.DLR_STORAGE_D_ID;
                  --本次未出库数量 =  未出库数量 - 可用良品数量
                  D_NOW_NOT_OUT_QTY:=D_NOW_NOT_OUT_QTY-REC_STORE_BAT.NOT_OUT_QTY;
                END IF;
								--新增出库退货的批次
								INSERT INTO  T_PA_UP_ESCROW_OUT_STORE_BAT  	/*代管库出库单_批次*/
								(
									OUT_STORE_BAT_ID,
									OUT_STORE_D_DLR_ID,
									OUT_STORE_ID,
									OUT_STORE_CODE,
									PART_ID,
									PART_NO,
									UNIT,
									BATCH_NO,
									PATCH_DATE,
									OUT_STORE_QTY,
									REMARK,
									DLR_ID,
									UP_DLR_ID,
									IS_ENABLE,
									CREATED_DATE,
									CREATOR,
									LAST_UPDATED_DATE,
									MODIFIER,
									UPDATE_CONTROL_ID,
									BILL_TYPE
								)
								VALUES
								(
									SYS_GUID(),	/*OUT_STORE_BAT_ID:专营店备件出库批次明细ID*/
									REC_DEAL_LIST.ORDER_D_ID,	/*OUT_STORE_D_DLR_ID:专营店备件出库单_明细ID*/
									REC_DEAL_LIST.ORDER_ID,	/*OUT_STORE_ID:专营店备件出库单 T_PA_BU_DLR_OUT_STORE.OUT_STORE_ID*/
									REC_DEAL_LIST.ORDER_CODE,	/*OUT_STORE_CODE:出库单编号*/
									REC_DEAL_LIST.PART_ID,	/*PART_ID:备件ID*/
									REC_DEAL_LIST.PART_NO,	/*PART_NO:备件编码*/
									REC_DEAL_LIST.PART_UNIT,	/*UNIT:单位*/
									REC_STORE_BAT.BATCH_NO,	/*BATCH_NO:批次号 已入库单号为批次号*/
									REC_STORE_BAT.PATCH_DATE,	/*PATCH_DATE:批次时间 入库时间*/
									D_NOW_OUT_QTY,	/*OUT_STORE_QTY:出库数量*/
									'自动生成',	/*REMARK:备注*/
									REC_STORE_BAT.UP_DLR_ID,	/*DLR_ID:专营店ID*/
									REC_STORE_BAT.UP_DLR_ID,	/*UP_DLR_ID:统采店ID*/
									1,	/*IS_ENABLE:是否可用 0无效，1有效*/
									SYSDATE,	/*CREATED_DATE:创建时间*/
									REC_DEAL_LIST.CREATOR,	/*CREATOR:创建人*/
									SYSDATE,	/*LAST_UPDATED_DATE:最后更新时间*/
									REC_DEAL_LIST.CREATOR,	/*MODIFIER:最后更新人员*/
									SYS_GUID(),	/*UPDATE_CONTROL_ID:并发控制字段*/
									REC_STORE_BAT.PATCH_BILL_TYPE	/*BILL_TYPE:单据类型*/
								);
              END IF;
          ELSE
            CLOSE CUR_STORE_BAT;
            EXIT;
          END IF;
        END LOOP;
      END IF;

    ELSE
      CLOSE CUR_DEAL_LIST;
      EXIT;
    END IF;
  END LOOP;
EXCEPTION
  WHEN MyException THEN
    V_RET := 0;
    IF CUR_DEAL_LIST%ISOPEN = TRUE THEN
      CLOSE CUR_DEAL_LIST;
    END IF;
    RAISE_APPLICATION_ERROR(-20999, 'xxxxx失败！');
  WHEN OTHERS THEN
    V_RET := 0;
    IF CUR_DEAL_LIST%ISOPEN = TRUE THEN
      CLOSE CUR_DEAL_LIST;
    END IF;
    RAISE_APPLICATION_ERROR(-20999, '出现错误,更新库存失败！'||SQLERRM);
END;