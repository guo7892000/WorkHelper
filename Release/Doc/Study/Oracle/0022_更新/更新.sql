/*合并更新语句*/
MERGE INTO T_PA_BU_PUR_ORDER_D U
USING (SELECT SUM(NVL(B.IN_QTY, 0)) AS T_IN_QTY, B.PART_NO
             FROM T_PA_BU_IN_ORDER A
             LEFT JOIN T_PA_BU_IN_ORDER_D B
               ON A.IN_ORDER_ID = B.IN_ORDER_ID
            WHERE B.SOURCE_ORDER_CODE = V_ORDER_NO /*关联单号:外购单号*/
              and A.net_code = NetCode
            GROUP BY B.PART_NO
	  ) F
ON (U.PART_NO = F.PART_NO)
    WHEN MATCHED THEN
      UPDATE
         SET U.IN_STOCK_QTY      = F.T_IN_QTY,
             U.LAST_UPDATED_DATE = systimestamp
       WHERE POD.PUR_ORDER_ID =
             (select pur_order_id
                from T_PA_BU_PUR_ORDER
               where pur_order_code = V_ORDER_NO
                 and net_code = NetCode); /*采购单ID*/

/*合并更新语句2*/
MERGE INTO T_PA_BU_OEM_PRE_STORE UD
   USING (SELECT B.WAREHOUSE_ID,
                 C.PART_NO,
                 B.STOCK_UP_AREA,
                 B.PUR_ORDER_TYPE,
                 SUM(C.STOCK_UP_QTY) STOCK_UP_QTY
            FROM T_PA_BU_STOCK_UP B, T_PA_BU_STOCK_UP_D C
           WHERE B.STOCK_UP_ID = C.STOCK_UP_ID
             AND B.STOCK_UP_CODE = P_STOCK_UP_CODE
           GROUP BY B.WAREHOUSE_ID,
                    C.PART_NO,
                    B.STOCK_UP_AREA,
                    B.PUR_ORDER_TYPE) FR
   ON (UD.WAREHOUSE_ID = FR.WAREHOUSE_ID AND UD.PART_NO = FR.PART_NO)
   WHEN MATCHED THEN
     UPDATE
        SET UD.FROZEN_STOCK_QTY       = UD.FROZEN_STOCK_QTY +
                                        DECODE(P_STOCK_UP_AREA,
                                               '1',
                                               FR.STOCK_UP_QTY,
                                               0),
            UD.STOCK_UP_FROZEN_QTY    = UD.STOCK_UP_FROZEN_QTY +
                                        DECODE(P_STOCK_UP_AREA,
                                               '1',
                                               FR.STOCK_UP_QTY,
                                               0),
            UD.FROZEN_CROSS_STOCK_QTY = UD.FROZEN_CROSS_STOCK_QTY +
                                        DECODE(P_STOCK_UP_AREA,
                                               '2',
                                               FR.STOCK_UP_QTY,
                                               0),
            UD.FROZEN_STO_STOCK_QTY   = UD.FROZEN_STO_STOCK_QTY +
                                        DECODE(P_STOCK_UP_AREA,
                                               '3',
                                               FR.STOCK_UP_QTY,
                                               0),
            UD.FROZEN_PACKING_STORAGE = UD.FROZEN_PACKING_STORAGE +
                                        DECODE(P_STOCK_UP_AREA,
                                               'X',
                                               FR.STOCK_UP_QTY,
                                               0),
            UD.MODIFIER               = 'SYSTEM',
            UD.LAST_UPDATED_DATE      = SYSDATE,
            UD.REMARK                 = '订单审核后台更新'
   WHEN NOT MATCHED THEN --Updated by qinjh on 2015-6-17 无备件预扣库存，则增加
     INSERT
       (PRE_STORE_ID,
        WAREHOUSE_ID,
        PART_NO,
        USABLE_STOCK_QTY,
        FROZEN_STOCK_QTY,
        FLIT_FROZEN_QTY,
        STOCK_ADD_FROZEN_QTY,
        STOCK_UP_FROZEN_QTY,
        IS_ENABLE,
        CREATOR,
        CREATED_DATE,
        MODIFIER,
        FROZEN_CROSS_STOCK_QTY,
        FROZEN_STO_STOCK_QTY,
        FROZEN_BO_STOCK_QTY,
        FROZEN_PACKING_STORAGE)
     VALUES
       (SYS_GUID(),
        FR.WAREHOUSE_ID,
        FR.PART_NO,
        0,
        DECODE(P_STOCK_UP_AREA, '1', FR.STOCK_UP_QTY, 0),
        0,
        0,
        DECODE(P_STOCK_UP_AREA, '2', FR.STOCK_UP_QTY, 0),
        '1',
        'PA_SYS',
        SYSDATE,
        'PA_SYS',
        DECODE(P_STOCK_UP_AREA, '2', FR.STOCK_UP_QTY, 0),
        DECODE(P_STOCK_UP_AREA, '3', FR.STOCK_UP_QTY, 0),
        0,
        DECODE(P_STOCK_UP_AREA, 'X', FR.STOCK_UP_QTY, 0));