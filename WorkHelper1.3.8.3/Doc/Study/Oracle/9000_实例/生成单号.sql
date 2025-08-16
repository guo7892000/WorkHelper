CREATE OR REPLACE PROCEDURE P_COMM_GET_FORM_CODE
(
       V_ORG_ID      VARCHAR2,     --组织ID(可空，针对不同单据规则类型传不同的关联ID）
       V_RULE_CODE VARCHAR2,     --规则编码
       V_RETURN_CODE    OUT VARCHAR2  --返回单号
)
/*******************************************************************************************
* 存储过程名称：生成单号
* 作者：       黄国辉
* 创建日期：   2013-12-15
* 主要功能:
*       利用单号规则表（BAS_CODE_RULE）:配置数据来生成单号，所有组织共用一套规则
*       单据使用信息表（BAS_CODE_USE）：针对每一种类型，一个组织一条数据。
* 修改历史：
*   2013-11-01 新增单据生成规则存储过程 HGH 
*
********************************************************************************************/
IS
  /*单号规则表*/
  V_CODE_STRING_BEGIN  CFG_CODE_RULE.PRE_CODE%TYPE; --前缀单号值
  V_CODETIME_FORMAT    CFG_CODE_RULE.DATE_FORMAT%TYPE; --时间格式
  V_CODE_LENGTH        CFG_CODE_RULE.AUTO_CODE_LENGH%TYPE; --流水号长度
  V_RESUME_PERIOD      CFG_CODE_RULE.RESUME_TYPE%TYPE; --递归日期格式，当前不使用
  V_INIT_VALUE         CFG_CODE_RULE.INIT_VALUE%TYPE; --初始化值
  V_STEP               CFG_CODE_RULE.STEP_VALUE%TYPE; --递增值
  V_CODE_STRING_END    CFG_CODE_RULE.END_CODE%TYPE; --后缀单号值
  V_ORDER_CODE_RULE_ID CFG_CODE_RULE.CODE_RULE_ID%TYPE; --单号规则ID
  V_CONFIG_TYPE_CODE   CFG_CODE_RULE.CONFIG_TYPE_CODE%TYPE; --单据规则类型编码
 /*单据使用信息表*/
  V_CURRENT_VALUE      CFG_CODE_USE.CURRENT_VALUE%TYPE; --当前值
  V_UPDATE_CONTROL_ID  CFG_CODE_USE.UPDATE_CONTROL_ID%TYPE; --并发控制ID
  /*单据规则类型表*/
  V_CONFIG_TYPE_NAME   CFG_CODE_CONFIG_TYPE.CONFIG_TYPE_NAME%TYPE; --配置类型名称
  V_TABLE_NAME         CFG_CODE_CONFIG_TYPE.TABLE_NAME%TYPE; --表名
  V_CODE_COLUMN_NAME   CFG_CODE_CONFIG_TYPE.CODE_COLUMN_NAME%TYPE; --列名
  V_PK_COLUMN_NAME     CFG_CODE_CONFIG_TYPE.PK_COLUMN_NAME%TYPE; --主键列名
  V_OTHER_CONDITION    CFG_CODE_CONFIG_TYPE.OTHER_CONDITIONE%TYPE; --其他条件
  /*变量*/
  V_CURR_FORMAT_VALUE VARCHAR2(20); --上次当前
  V_COUNT             NUMBER; --计数值
  V_NEW_FORMAT_VALUE VARCHAR2(20); --当天的值
  V_SOURCE_ID        VARCHAR2(50); --源ID，对单据规则类型编码：0为ORDER_CODE_RULE_ID,1为DLR_ID

  V_FIST_CODE        VARCHAR2(50);
  V_DLR_CODE         VARCHAR2(50);        /*网点编码*/
  V_NEW_VALUE        VARCHAR2(50);/*流水号字符*/
  I_NEW_VALUE        INTEGER; /*流水号的整数值*/
  ICOUNT             INTEGER; /*记录数*/
  --V_INT              NUMBER;
  --V_LENGTH           NUMBER;
  --VALUE_NAME         VARCHAR2(100);
  V_TEMP_SQL         VARCHAR2(2000);

  /*单据规则使用表记录游标：注增加锁记录*/
  CURSOR CUR_IF_TAB IS
    SELECT *
      FROM CFG_CODE_USE R
     WHERE R.CODE_RULE_CODE = V_RULE_CODE
       AND R.ORG_ID = V_SOURCE_ID
     FOR UPDATE;
  /*游标变量*/
  REC_IF_TAB CUR_IF_TAB%ROWTYPE;
BEGIN
  /*1、初始化变量*/
  V_COUNT     := 0;
  V_RETURN_CODE := '';

  /*2、配置唯一性判断*/
  /*查询该单据类型的配置记录数*/
  SELECT COUNT(1) INTO V_COUNT
   FROM CFG_CODE_RULE T
   WHERE T.CODE_RULE_CODE = V_RULE_CODE
         AND T.IS_ENABLED = '1';
  /*配置必须存在一条判断*/
  IF V_COUNT <> 1 THEN
    RAISE_APPLICATION_ERROR(-20999,'不存在有效的该单据类型,或者该单据类型有多条配置数据！');
  END IF;

  /*3、取出单据中的对象编码*/
  /*获取单据配置信息*/
  SELECT  T.CODE_RULE_ID,
          T.PRE_CODE,
          NVL(T.DATE_FORMAT,'0'), /*对于没有时间格式的，给一个默认值0，这个不能修改*/
          T.AUTO_CODE_LENGH,
          T.RESUME_TYPE,
          T.INIT_VALUE,
          T.STEP_VALUE,
          T.END_CODE,
          T.CONFIG_TYPE_CODE
     INTO V_ORDER_CODE_RULE_ID,
          V_CODE_STRING_BEGIN,
          V_CODETIME_FORMAT,
          V_CODE_LENGTH,
          V_RESUME_PERIOD,
          V_INIT_VALUE,
          V_STEP,
          V_CODE_STRING_END,
          V_CONFIG_TYPE_CODE
 FROM CFG_CODE_RULE T
 WHERE T.CODE_RULE_CODE = V_RULE_CODE
         AND T.IS_ENABLED = '1';
  /*根据不同单据规则类型编码作不同处理*/
  --V_COUNT := 0;
  IF(V_CONFIG_TYPE_CODE IS NULL OR V_CONFIG_TYPE_CODE=0) THEN
    /*3.1 没有配置，则取规则ID为源ID*/
    V_SOURCE_ID:=V_ORDER_CODE_RULE_ID;
  ELSE
    /*3.2 有配置，查询规则类型编码是否存在*/
    SELECT COUNT(1) INTO V_COUNT
     FROM CFG_CODE_CONFIG_TYPE T
     WHERE T.CONFIG_TYPE_CODE = V_CONFIG_TYPE_CODE;
    /*判断记录是否存在*/
    IF V_COUNT=0 THEN
      /*不存在的类型，抛错*/
       RAISE_APPLICATION_ERROR(-20999,'目前不支持该规则类型：'||V_CONFIG_TYPE_CODE||'！');
    END IF;
    /*取出表列条件*/
    SELECT T.TABLE_NAME,
       T.CODE_COLUMN_NAME,
       T.PK_COLUMN_NAME,
       T.OTHER_CONDITIONE,
       T.CONFIG_TYPE_NAME
    INTO V_TABLE_NAME,
       V_CODE_COLUMN_NAME,
       V_PK_COLUMN_NAME,
       V_OTHER_CONDITION,
       V_CONFIG_TYPE_NAME
    FROM CFG_CODE_CONFIG_TYPE T
   WHERE T.CONFIG_TYPE_CODE = V_CONFIG_TYPE_CODE;
    /*执行动态SQL查询结果*/
    /*根据传入值查询是否有记录*/
    V_TEMP_SQL:='SELECT COUNT(1) FROM '|| V_TABLE_NAME
    ||' WHERE '|| V_PK_COLUMN_NAME ||'='''||V_ORG_ID||'''';
    IF(V_OTHER_CONDITION IS NOT NULL) THEN
      V_TEMP_SQL:=V_TEMP_SQL||' AND '||V_OTHER_CONDITION;
    END IF;
    EXECUTE IMMEDIATE V_TEMP_SQL INTO V_COUNT;
    /*源ID和编码非空判断*/
    IF (V_COUNT = 0) THEN
       RAISE_APPLICATION_ERROR(-20999,V_PK_COLUMN_NAME||'为'||V_ORG_ID||'的'||V_CONFIG_TYPE_NAME||'('||V_TABLE_NAME||')表记录不存在！');
    END IF;
    /*有记录，则查询编码*/
    V_TEMP_SQL:='SELECT ' || V_CODE_COLUMN_NAME || ','||V_PK_COLUMN_NAME||' FROM '|| V_TABLE_NAME
    ||' WHERE '|| V_PK_COLUMN_NAME ||'='''||V_ORG_ID||'''';
    IF(V_OTHER_CONDITION IS NOT NULL) THEN
      V_TEMP_SQL:=V_TEMP_SQL||' AND '||V_OTHER_CONDITION;
    END IF;
    EXECUTE IMMEDIATE V_TEMP_SQL INTO V_DLR_CODE,V_SOURCE_ID;
    /*编码非空判断*/
    IF (V_DLR_CODE IS NULL) THEN
       RAISE_APPLICATION_ERROR(-20999,V_PK_COLUMN_NAME||'为'||V_SOURCE_ID||'的'||V_CONFIG_TYPE_NAME||'('||V_TABLE_NAME||')表记录中'||V_CODE_COLUMN_NAME||'为空，请修正！');
    END IF;
  END IF;

  /*4、获取配置的当前使用值*/
  /*查找该网点是否存在该规则的使用*/
  SELECT COUNT(1) INTO ICOUNT
  FROM CFG_CODE_USE T
  WHERE T.CODE_RULE_CODE = V_RULE_CODE
        AND T.ORG_ID = V_SOURCE_ID;
  /*存在多个时，抛错*/
  IF ICOUNT>1 THEN
     RAISE_APPLICATION_ERROR(-20999,'专营店对该类型的使用配置只能为一个，目前存在多个！');
  END IF;
  /*不存在时新增*/
  IF ICOUNT=0 THEN
    INSERT INTO CFG_CODE_USE  /**/
    (
      CODE_USE_ID,
      CODE_RULE_CODE,
      CURRENT_VALUE,
      DATE_FORMAT_VALUE,
      IS_ENABLED,
      IS_SYSTEM,
      CREATOR,
      MODIFIER,
      UPDATE_CONTROL_ID,
      ORG_ID
    )
    VALUES
    (
      SYS_GUID(),  /*ORDER_CODE_USE_ID*/
      V_RULE_CODE,  /*ORDER_CODE_RULE_ID*/
      0,  /*CURRENT_VALUE*/
      V_CODETIME_FORMAT,  /*CURR_FORMAT_VALUE*/
      '1',  /*IS_ENABLE*/
      '1',  /*IS_SYSTEM*/
      '1',  /*CREATOR*/
      '1',  /*MODIFIER*/
      SYS_GUID(),  /*UPDATE_CONTROL_ID*/
      V_SOURCE_ID  /*关联对象ID*/
    );
    END IF;
  /*打开游标，取出值*/
  OPEN CUR_IF_TAB;
  LOOP
    FETCH CUR_IF_TAB
      INTO REC_IF_TAB; --提取需要处理的下发信息到游标变量
    EXIT WHEN CUR_IF_TAB%NOTFOUND;
    BEGIN
      V_CURRENT_VALUE      := REC_IF_TAB.CURRENT_VALUE;
      V_CURR_FORMAT_VALUE  := REC_IF_TAB.DATE_FORMAT_VALUE;
      V_UPDATE_CONTROL_ID  := REC_IF_TAB.UPDATE_CONTROL_ID;
    END;
  END LOOP;
  CLOSE CUR_IF_TAB;

  /*5、根据配置得到新单号*/
  /*根据时间格式判断当天值*/
  IF (NVL(V_CODETIME_FORMAT,'0')='0') THEN
    V_NEW_FORMAT_VALUE := '';
  ELSE
    V_NEW_FORMAT_VALUE := TO_CHAR(SYSDATE, V_CODETIME_FORMAT);
  END IF;

  --如果当前的日期格式值与当前一致，则V_NEW_INITVALUE +1，否则等于初始化值
  IF NVL(V_NEW_FORMAT_VALUE,'0') != V_CURR_FORMAT_VALUE THEN
    IF V_INIT_VALUE = 0 THEN
       V_INIT_VALUE :=1; --默认从1开始
    END IF;
    V_NEW_VALUE := TO_NUMBER(V_INIT_VALUE);
  ELSE
      /*如果当前值比现有的值小，则抛错。防止数据库服务器时间不对导致单号错乱*/
      IF(NVL(V_NEW_FORMAT_VALUE,'0')<V_CURR_FORMAT_VALUE) THEN
        RAISE_APPLICATION_ERROR(-20999,'本次生成的时间格式不能小于上次生成值！');
      END IF;
      --当前值加上递增值
      V_NEW_VALUE := TO_NUMBER(V_CURRENT_VALUE) + TO_NUMBER(V_STEP);
  END IF;

  /*给流水号赋值*/
  I_NEW_VALUE:=V_NEW_VALUE;
  --单号中流水号部分的处理
  SELECT INSTR(V_CODE_STRING_BEGIN,'@') INTO ICOUNT FROM　DUAL;
  IF ICOUNT>0 THEN
    --自定义的流水号处理:调用F_MDS_GET_WORD_SEQUENCE_NO方法来获取
    V_NEW_VALUE:= F_SYS_GET_WORD_SEQUENCE_NO(I_NEW_VALUE,V_CODE_LENGTH);
  ELSE
    /*最大值判断*/
    IF LENGTH(TO_CHAR(V_NEW_VALUE)) > TO_NUMBER(V_CODE_LENGTH) THEN
      RAISE_APPLICATION_ERROR(-20999,'单号生成失败，单号已经超过当前规定的最大数！');
    END IF;
    --非自定流水的处理：不满足数据值前补“0”
     V_NEW_VALUE := LPAD(V_NEW_VALUE, V_CODE_LENGTH, '0');
  END IF;

  --对替换字符的处理
  V_FIST_CODE := '';
  IF V_CODE_STRING_BEGIN IS NOT NULL THEN
    --如果只是*，则取网点编码
    CASE V_CODE_STRING_BEGIN
      WHEN '*' THEN
        /*对*号直接取网点编码*/
        V_FIST_CODE := V_DLR_CODE;
      WHEN '#' THEN
        /*#号转换为ASCII码*/
        /*V_LENGTH :=LENGTH(V_DLR_CODE);
        V_INT:=0;
        FOR V_INT   IN 1..V_LENGTH LOOP
            VALUE_NAME :=VALUE_NAME||ASCII(SUBSTR(V_DLR_CODE,V_INT,1));
        END LOOP;
        V_FIST_CODE := VALUE_NAME;*/
        V_FIST_CODE := V_DLR_CODE;
      WHEN '@' THEN
        /*@号为从系统配置取前缀，但目前系统配置表没有网点编码，以后再修改实现*/
        /*SELECT MAX(V.PARAMETER_VALUE) INTO VALUE_NAME
        FROM     T_DB_SYS_INFO V
        WHERE V. = V_DLR_CODE
              AND V.PARAMETER_CODE = 'SYS_COMM_0000';
        V_FIST_CODE := VALUE_NAME;*/
        V_FIST_CODE := V_DLR_CODE;
      ELSE
        /*其他直接将#、*、@号替换为网点编码*/
        V_FIST_CODE := REPLACE(V_CODE_STRING_BEGIN, '#', V_DLR_CODE);
        V_FIST_CODE := REPLACE(V_FIST_CODE, '*', V_DLR_CODE);
        V_FIST_CODE := REPLACE(V_FIST_CODE, '@', V_DLR_CODE);
    END CASE;
  END IF;

  /*构造单号*/
  IF V_CODE_STRING_END IS NOT NULL THEN
    --如果后缀不为空
    V_RETURN_CODE := V_FIST_CODE || V_NEW_FORMAT_VALUE || V_CODE_STRING_END || TO_CHAR(V_NEW_VALUE);
  ELSE
    --如果后缀为空
    V_RETURN_CODE := V_FIST_CODE || V_NEW_FORMAT_VALUE || TO_CHAR(V_NEW_VALUE);
  END IF;

  /*6、更新使用配置表*/
  /*查询并发*/
  ICOUNT:=0;
  SELECT COUNT(*) INTO ICOUNT
  FROM CFG_CODE_USE R
  WHERE R.ORG_ID = V_SOURCE_ID
     AND R.CODE_RULE_CODE = V_RULE_CODE
     AND R.UPDATE_CONTROL_ID = V_UPDATE_CONTROL_ID;
  IF ICOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20999,'单号生成规则出现错误，该单据号数据被其它操作更新！');
  ELSE
    /*修改网点该类型当前值*/
    UPDATE CFG_CODE_USE R
       SET R.CURRENT_VALUE     = I_NEW_VALUE,
           R.DATE_FORMAT_VALUE = DECODE(R.DATE_FORMAT_VALUE,
                                        NULL,
                                        NULL,
                                        NVL(V_NEW_FORMAT_VALUE,'0')),
           R.UPDATE_CONTROL_ID = SYS_GUID()
     WHERE R.ORG_ID = V_SOURCE_ID
       AND R.CODE_RULE_CODE = V_RULE_CODE
       AND R.UPDATE_CONTROL_ID = V_UPDATE_CONTROL_ID;

     if SQL%ROWCOUNT = 0 then
       RAISE_APPLICATION_ERROR(-20999,'发生单号并发异常！');
      end if;
  END IF;
  EXCEPTION
    /*异常处理*/
    WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20999,SQLERRM);
END;
/
