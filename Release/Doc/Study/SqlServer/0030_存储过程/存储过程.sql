/*删除存储过程*/
--1、删除CLR存储过程
IF ( OBJECT_ID('P_P_E3S_OWEQTY_RECEIPT', 'PC') IS NOT NULL ) 
	DROP PROCEDURE [dbo].[P_P_E3S_OWEQTY_RECEIPT];
--2、删除SQL或CLR存储过程
IF ( OBJECT_ID('P_PA_E3S_OUTSTOCK_RECEIPT') IS NOT NULL ) 
	DROP PROCEDURE P_PA_E3S_OUTSTOCK_RECEIPT;
--3、删除SQL或CLR存储过程
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IFR_UC_T_BASE_ASSESSMENT_ITEMProcedure]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[IFR_UC_T_BASE_ASSESSMENT_ITEMProcedure];
GO

/*创建存储过程*/
/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_COMM_GET_FORM_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE[dbo].[P_COMM_GET_FORM_CODE]
GO
*/
CREATE PROCEDURE [dbo].[P_COMM_GET_FORM_CODE]
(
       @V_ORG_ID VARCHAR(50),     --组织ID
       @V_RULE_CODE VARCHAR(50),   --规则编码
       @V_RETURN_CODE VARCHAR(50) OUTPUT
)
AS 
/*******************************************************************************************
* 存储过程名称：生成单号
* 作者：       黄国辉
* 创建日期：   2013-12-15
* 主要功能:  
*       利用单号规则表（BAS_CODE_RULE）:配置数据来生成单号，所有组织共用一套规则
*       单据使用信息表（BAS_CODE_USE）：针对每一种类型，一个组织一条数据。
* 修改历史：
*     V1.00 新增单据生成规则存储过程 HGH 2013-11-01
*	  V1.01 修改：增加配置类型的支持，实现网点编码为动态值。
*	  V1.02 修改：修正当配置类型为0时，无法产生单号问题。hgh 2016-7-4
********************************************************************************************/
BEGIN
  DECLARE @V_CODE_PRE        VARCHAR(50), --起始单号值
		@V_CODETIME_FORMAT    VARCHAR(50), --时间格式
		@V_CODE_LENGTH        VARCHAR(50), --流水号长度
		@V_RESUME_PERIOD      VARCHAR(50), --递归日期格式，当前不使用
		@V_INITVALUE          VARCHAR(50), --初始化值
		@V_STEP               VARCHAR(50), --递增值
		@V_END_CODE      VARCHAR(50), --后缀单号值
		@V_ORDER_CODE_RULE_ID VARCHAR(50), --单号规则ID
		@CONFIG_TYPE_CODE	  VARCHAR(50), --配置类型：该值决定#号的内容
		
		@V_CURRENT_VALUE      VARCHAR(50), --当前值
		@V_UPDATE_CONTROL_ID  VARCHAR(50), --当前值

		@V_CURR_FORMAT_VALUE VARCHAR(50), --上次当前
		@V_COUNT             VARCHAR(50), --计数值
		@V_NEW_FORMAT_VALUE VARCHAR(50), --当天的值

		@V_FIST_CODE VARCHAR(50),
		@V_DLR_CODE  VARCHAR(50),       /*网点编码*/
		@V_NEW_VALUE       VARCHAR(50),/*流水号字符*/
		@I_NEW_VALUE        INTEGER, /*流水号的整数值*/
		@ICOUNT             INTEGER, /*记录数*/
		@V_INT              INTEGER,
		@V_LENGTH           INTEGER,
		@VALUE_NAME         VARCHAR(100),
		
		--获取配置类型值
		@V_TABLE_NAME  VARCHAR(50),
		@V_CODE_COLUMN_NAME VARCHAR(50),
		@V_PK_COLUMN_NAME VARCHAR(50),
		@V_OTHER_CONDITIONE VARCHAR(1000),
		
		/*动态SQL相关参数*/
		@SQLSTRING NVARCHAR(4000),
		@PARMDEFINITION NVARCHAR(500),
		@FORM_CODE_SHORT_OUT VARCHAR(30),
		
		--错误信息 
		@V_NOW_DATE VARCHAR(10),
		@ERROR_MES VARCHAR(1000)	/*错误描述*/
		
  SET @V_COUNT = 0;
  SET @V_RETURN_CODE = '';
	
  /*查询该单据类型的配置记录数*/
  SET @V_COUNT = 0
  SELECT @V_COUNT=COUNT(1) 
   FROM CFG_CODE_RULE T
   WHERE T.CODE_RULE_CODE=@V_RULE_CODE
		 AND T.IS_ENABLED='1';       
  /*配置必须存在一条判断*/
  IF @V_COUNT <> 1
  BEGIN
    IF @V_COUNT = 0
		BEGIN
			SET @ERROR_MES='不存在CODE_RULE_CODE为['+@V_RULE_CODE+']的单据配置数据！'
		END
	ELSE
		BEGIN
			SET @ERROR_MES='CODE_RULE_CODE为['+@V_RULE_CODE+']的单据配置数据存在多条，请删除重复的数据！'
		END
	--抛出错误
	RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
	RETURN;
  END
  
  --获取单据配置
  SELECT  @V_ORDER_CODE_RULE_ID=T.CODE_RULE_ID,
		  @V_CODE_PRE=T.PRE_CODE,
		  @V_END_CODE = T.END_CODE,
		  @V_CODETIME_FORMAT=T.DATE_FORMAT,
		  @V_CODE_LENGTH=T.AUTO_CODE_LENGH,
		  @V_RESUME_PERIOD=T.RESUME_TYPE,
		  @V_INITVALUE=T.INIT_VALUE,
		  @V_STEP=T.STEP_VALUE,
		  @CONFIG_TYPE_CODE = T.CONFIG_TYPE_CODE
 FROM CFG_CODE_RULE T
 WHERE T.CODE_RULE_CODE=@V_RULE_CODE
		 AND T.IS_ENABLED='1';
   
  IF @CONFIG_TYPE_CODE <> '0' --0：永远使用一个当前值，跟组织等无关
  BEGIN  
		SELECT @V_COUNT=COUNT(1) 
		FROM CFG_CODE_CONFIG_TYPE T
		WHERE T.CONFIG_TYPE_CODE = @CONFIG_TYPE_CODE;       
		/*配置必须存在一条判断*/
		IF @V_COUNT = 0
		BEGIN
			SET @ERROR_MES='CFG_CODE_CONFIG_TYPE表不存在CONFIG_TYPE_CODE为['+@CONFIG_TYPE_CODE+']的配置类型数据！'
			--抛出错误	
			RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
			RETURN;
		END 
  	  --获取配置类型值
	  SELECT @V_TABLE_NAME = TABLE_NAME ,
			@V_CODE_COLUMN_NAME =CODE_COLUMN_NAME ,
			@V_PK_COLUMN_NAME =PK_COLUMN_NAME ,
			@V_OTHER_CONDITIONE =OTHER_CONDITIONE  
	  FROM CFG_CODE_CONFIG_TYPE T
	  WHERE T.CONFIG_TYPE_CODE = @CONFIG_TYPE_CODE;
	  
	  	    
	  --设置动态SQL
	  SET @SQLSTRING = N'SELECT TOP 1 @FORM_CODE_SHORT ='+@V_CODE_COLUMN_NAME+' FROM '
		+@V_TABLE_NAME+' WHERE '+
		@V_PK_COLUMN_NAME+' = @ORG_ID'+ ISNULL(@V_OTHER_CONDITIONE,'');
	  SET @PARMDEFINITION = N'@ORG_ID VARCHAR(50), @FORM_CODE_SHORT VARCHAR(50) OUTPUT';

	  EXECUTE SP_EXECUTESQL @SQLSTRING, @PARMDEFINITION, @ORG_ID = @V_ORG_ID, @FORM_CODE_SHORT=@FORM_CODE_SHORT_OUT OUTPUT;
	  
	  /*如果找不到网点记录，则抛错*/
	  IF(@FORM_CODE_SHORT_OUT IS NULL )
	  BEGIN
		 SET @ERROR_MES=@V_PK_COLUMN_NAME + '为['+@V_ORG_ID+']的'+@V_TABLE_NAME+'表记录不存在或为空，不能生成单号！';
		 RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
		 RETURN;
	  END
	  /*得到动态替换的组织编码*/
	  SET @V_DLR_CODE = @FORM_CODE_SHORT_OUT;
	  /*查找该网点是否存在该规则的使用*/
	  SELECT @ICOUNT=COUNT(1) 
	  FROM CFG_CODE_USE T 
	  WHERE T.CODE_RULE_CODE=@V_RULE_CODE 
			AND T.REL_OBJECT_ID = @V_ORG_ID;
	  /*存在多个时，抛错*/
	  IF @ICOUNT > 1
	  BEGIN
		 SET @ERROR_MES=@V_DLR_CODE+'的'+@V_RULE_CODE+'单据配置只能为一个，目前存在多个！'
		 RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
	  END
  END
  ELSE --配置类型为0，即只有一条记录
	  BEGIN
		/*查找该网点是否存在该规则的使用*/
	  SELECT @ICOUNT=COUNT(1) 
	  FROM CFG_CODE_USE T 
	  WHERE T.CODE_RULE_CODE=@V_RULE_CODE;
	  /*存在多个时，抛错*/
	  IF @ICOUNT>1
	  BEGIN
		 SET @ERROR_MES=@V_RULE_CODE+'的单据配置只能为一个，目前存在多个！'
		 RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
	  END
	  --当组织ID为空时，默认全部取1作为其ID会上。
	  SET @V_ORG_ID = '1' 
	  SET @V_DLR_CODE = ''
  END
  
  /*不存在时新增*/
  IF @ICOUNT=0 
  BEGIN    
	--新增使用记录
	INSERT INTO CFG_CODE_USE /**/
	(
	  CODE_USE_ID,	
	  CODE_RULE_CODE,	
	  CURRENT_VALUE,
	  REL_OBJECT_ID,	
	  --DATE_FORMAT_VALUE,	
	  IS_ENABLED,	
	  IS_SYSTEM,
	  CREATOR_ID,	
	  CREATOR,
	  MODIFIER_ID,	
	  MODIFIER,	
	  UPDATE_CONTROL_ID,	
	  ORG_ID	
	)
	VALUES
	(
	  NEWID(),	/**/
	  @V_RULE_CODE,	/**/
	  0,	/*CURRENT_VALUE*/
	  @V_ORG_ID,
	  --V_CODETIME_FORMAT,	/*DATE_FORMAT_VALUE*/
	  '1',	/*IS_ENABLE*/
	  '1',	/*IS_SYSTEM*/
	  '1',	/*CREATOR_ID*/
	  '1',	/*CREATOR*/
	  '1',	/*MODIFIER_ID*/
	  '1',	/*MODIFIER*/
	  NEWID(),	/*UPDATE_CONTROL_ID*/
	  @V_ORG_ID	/*DLR_CODE*/
	)
   END
   
   --锁住记录
   UPDATE CFG_CODE_USE WITH ( ROWLOCK )
   SET IS_ENABLED = IS_ENABLED
   WHERE CODE_RULE_CODE=@V_RULE_CODE 
		AND REL_OBJECT_ID = @V_ORG_ID 
   
   /*当前日期格式*/
   SELECT @V_NOW_DATE=CONVERT(VARCHAR(8), GETDATE(), 112)
   
   /*当前值*/
   SELECT   @V_CURRENT_VALUE  = R.CURRENT_VALUE,
			@V_CURR_FORMAT_VALUE   = R.DATE_FORMAT_VALUE,
			@V_UPDATE_CONTROL_ID   = R.UPDATE_CONTROL_ID
   FROM CFG_CODE_USE R
   WHERE R.CODE_RULE_CODE=@V_RULE_CODE 
		AND R.REL_OBJECT_ID = @V_ORG_ID
   
  /*根据时间格式判断当天值*/
  IF @V_CODETIME_FORMAT IS NULL
	SET @V_NEW_FORMAT_VALUE = '';
  ELSE
	  BEGIN
		--注：四位年从0开始，两位年从3开始
		SELECT @V_NEW_FORMAT_VALUE=CASE  UPPER(@V_CODETIME_FORMAT)
		 WHEN 'YYYYMMDD' THEN @V_NOW_DATE
		 WHEN 'YYYYMM' THEN SUBSTRING(@V_NOW_DATE,0,7)
		 WHEN 'YYMMDD' THEN SUBSTRING(@V_NOW_DATE,3,6)
		 WHEN 'YYMM' THEN SUBSTRING(@V_NOW_DATE,3,4)
	  END
  --PRINT @V_NEW_FORMAT_VALUE
  --如果当前的日期格式值与当前一致，则V_NEW_INITVALUE +1，否则等于初始化值
  IF @V_CURR_FORMAT_VALUE IS NULL OR @V_NEW_FORMAT_VALUE <> @V_CURR_FORMAT_VALUE
	  BEGIN
		SET @V_NEW_VALUE = CONVERT(INT,@V_INITVALUE)
	  END
  ELSE
	  BEGIN
		--当前值加上递增值
		SET @V_NEW_VALUE = CONVERT(INT,@V_CURRENT_VALUE) + CONVERT(INT,@V_STEP)
	  END
	  
  
  /*给流水号赋值*/
  SET @I_NEW_VALUE=@V_NEW_VALUE;
  --单号中流水号部分的处理
  IF CHARINDEX('@',@V_CODE_PRE)>0
     --自定义的流水号处理:调用F_MDS_GET_WORD_SEQUENCE_NO方法来获取
     SELECT @V_NEW_VALUE = DBO.F_SYS_GET_WORD_SEQUENCE_NO(@I_NEW_VALUE,@V_CODE_LENGTH)
  ELSE
	BEGIN
		/*最大值判断*/
		IF LEN(@V_NEW_VALUE) > CAST(@V_CODE_LENGTH AS INT) 
		BEGIN
		  RAISERROR('单号生成失败，单号已经超过当前规定的最大数！',1,10) WITH NOWAIT
		  RETURN
	    END
		--非自定流水的处理：不满足数据值前补“0”
		SET @V_NEW_VALUE = RIGHT(REPLICATE('0',10)+LTRIM(@V_NEW_VALUE),@V_CODE_LENGTH)
    END
  
  PRINT @V_NEW_VALUE
  
  SET @V_FIST_CODE = ''
  IF @V_CODE_PRE IS NOT NULL
  BEGIN
	--如果只是*，则取网点编码
	IF @V_CODE_PRE = '*' OR @V_CODE_PRE ='#' OR @V_CODE_PRE = '@'
		/*对*号直接取网点编码*/
		SET @V_FIST_CODE = @V_DLR_CODE	
	ELSE
		/*其他直接将#号替换为网点编码*/
		SET @V_FIST_CODE = REPLACE(@V_CODE_PRE, '#', @V_DLR_CODE)
		SET @V_FIST_CODE = REPLACE(@V_FIST_CODE, '*', @V_DLR_CODE)
		SET @V_FIST_CODE = REPLACE(@V_FIST_CODE, '@', @V_DLR_CODE)
	END
  END
  --PRINT @V_END_CODE 
  /*构造单号*/
  IF @V_END_CODE IS NOT NULL
	--如果后缀不为空
	SET @V_RETURN_CODE = @V_FIST_CODE + @V_NEW_FORMAT_VALUE  + @V_END_CODE + @V_NEW_VALUE
  ELSE
	BEGIN
	--如果后缀为空
	SET @V_RETURN_CODE = @V_FIST_CODE + @V_NEW_FORMAT_VALUE + @V_NEW_VALUE
	END;
  
  --PRINT @V_RETURN_CODE 
  /*查询并发*/
  SET @ICOUNT=0
  SELECT @ICOUNT=COUNT(1)
  FROM CFG_CODE_USE R
  WHERE R.REL_OBJECT_ID = @V_ORG_ID
	 AND R.CODE_RULE_CODE = @V_RULE_CODE
	 AND R.UPDATE_CONTROL_ID = @V_UPDATE_CONTROL_ID;
  
  IF @ICOUNT = 0
	BEGIN
	 SET @ERROR_MES='单号生成规则出现错误，该单据号数据被其它操作更新！'
	 RAISERROR(@ERROR_MES,1,10) WITH NOWAIT
	END
  ELSE
	BEGIN
		/*修改网点该类型当前值*/
		UPDATE CFG_CODE_USE
		SET CURRENT_VALUE     = @I_NEW_VALUE,
			DATE_FORMAT_VALUE = CASE WHEN CURRENT_VALUE IS
											NULL THEN NULL ELSE @V_NEW_FORMAT_VALUE END,
			   UPDATE_CONTROL_ID = NEWID()
		 WHERE REL_OBJECT_ID = @V_ORG_ID
		   AND CODE_RULE_CODE = @V_RULE_CODE
		   AND UPDATE_CONTROL_ID = @V_UPDATE_CONTROL_ID
	 END
  --返回值：用查询SQL实现
  SELECT @V_RETURN_CODE	 
END
GO

ALTER PROCEDURE [dbo].[P_COMM_GET_FORM_CODE]
(
       @V_ORG_ID VARCHAR(50),     --组织ID
       @V_RULE_CODE VARCHAR(50),   --规则编码
       @V_RETURN_CODE VARCHAR(50) OUTPUT
)
AS 
/*******************************************************************************************
* 存储过程名称：生成单号
* 作者：       黄国辉
* 创建日期：   2013-12-15
* 主要功能:  
*       利用单号规则表（BAS_CODE_RULE）:配置数据来生成单号，所有组织共用一套规则
*       单据使用信息表（BAS_CODE_USE）：针对每一种类型，一个组织一条数据。
* 修改历史：
*     V1.00 新增单据生成规则存储过程 HGH 2013-11-01
*	  V1.01 修改：增加配置类型的支持，实现网点编码为动态值。
*	  V1.02 修改：修正当配置类型为0时，无法产生单号问题。hgh 2016-7-4
********************************************************************************************/
BEGIN
	DECLARE @V_CODE_PRE        VARCHAR(50), --起始单号值
		@V_CODETIME_FORMAT    VARCHAR(50) --时间格式
    /*此处省略...*/
END
GO
