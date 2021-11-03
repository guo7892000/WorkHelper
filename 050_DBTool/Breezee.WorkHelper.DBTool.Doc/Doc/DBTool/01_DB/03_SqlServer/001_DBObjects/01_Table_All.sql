/***********************************************************************************
* 脚本描述: 新增修改表
* 创建作者: 
* 创建日期: 2021/11/2 
* 使用模块：
* 使用版本: 
* 说    明：
      1 新增数据库配置（DBT_BD_DB_CONFIG）
      2 新增通用字段默认值配置（DBT_BD_COLUMN_DEFAULT）
***********************************************************************************/
/*1、新增表：数据库配置(DBT_BD_DB_CONFIG)*/
IF OBJECT_ID('DBT_BD_DB_CONFIG', 'U') IS  NULL 
 BEGIN 
CREATE TABLE DBT_BD_DB_CONFIG 
(
 	DB_CONFIG_ID varchar(36)  CONSTRAINT [PK_DBT_BD_DB_CONFIG] PRIMARY KEY(DB_CONFIG_ID)  NOT NULL  ,/*数据库配置ID*/
	DB_CONFIG_CODE varchar(50)  NULL  ,/*数据库配置编码*/
	DB_CONFIG_NAME varchar(100)  NULL  ,/*数据库配置名称*/
	DB_TYPE varchar(2)  NULL  ,/*数据库类型*/
	SERVER_IP varchar(400)  NULL  ,/*服务器IP*/
	PORT_NO varchar(20)  NULL  ,/*端口号*/
	SCHEMA_NAME varchar(30)  NULL  ,/*架构名称*/
	DB_NAME varchar(30)  NULL  ,/*数据库名称*/
	USER_NAME varchar(50)  NULL  ,/*用户名*/
	USER_PASSWORD varchar(50)  NULL  ,/*用户密码*/
	LOGIN_TYPE varchar(2)  NULL  ,/*登录类型*/
	TYPE_DESC varchar(200)  NULL  ,/*描述*/
	SORT_ID int   NULL  ,/*排序ID*/
	REMARK varchar(200)  NULL  ,/*备注*/
	CREATE_TIME datetime   NOT NULL   CONSTRAINT DF_DBT_BD_DB_CONFIG_CREATE_TIME DEFAULT(getdate()) ,/*创建时间*/
	CREATOR_ID varchar(36)  NOT NULL  ,/*创建人ID*/
	CREATOR varchar(50)  NULL  ,/*创建人*/
	MODIFIER_ID varchar(36)  NULL  ,/*修改人ID*/
	MODIFIER varchar(50)  NULL  ,/*修改人*/
	LAST_UPDATED_TIME datetime   NOT NULL   CONSTRAINT DF_DBT_BD_DB_CONFIG_LAST_UPDATED_TIME DEFAULT(getdate()) ,/*最后更新时间*/
	IS_ENABLED varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_DB_CONFIG_IS_ENABLED DEFAULT('1') ,/*是否有效*/
	IS_SYSTEM varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_DB_CONFIG_IS_SYSTEM DEFAULT('0') ,/*系统标志*/
	ORG_ID varchar(36)  NOT NULL  ,/*组织ID*/
	UPDATE_CONTROL_ID varchar(36)  NOT NULL  ,/*并发控制ID*/
	TFLAG varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_DB_CONFIG_TFLAG DEFAULT('0') /*传输标志*/
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库配置：',
   @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DBT_BD_DB_CONFIG'
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据库配置ID：主键ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'DB_CONFIG_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据库配置编码',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'DB_CONFIG_CODE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据库配置名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'DB_CONFIG_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据库类型',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'DB_TYPE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'服务器IP：对Oracle类型，为TNS名称,SQLite是存文件路径',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'SERVER_IP'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'端口号',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'PORT_NO'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'架构名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'SCHEMA_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据库名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'DB_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户名',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'USER_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户密码',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'USER_PASSWORD'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'登录类型：针对SqlServer，有Windows登录和SQL登录两种。',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'LOGIN_TYPE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'描述',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'TYPE_DESC'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'排序ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'SORT_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'备注',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'REMARK'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'CREATE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'CREATOR_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'CREATOR'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'LAST_UPDATED_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'IS_ENABLED'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'系统标志',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'IS_SYSTEM'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'组织ID：表示数据所属的公司',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'ORG_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'并发控制ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'UPDATE_CONTROL_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'传输标志：0不上传，1未上传，2成功上传，3上传出错',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_DB_CONFIG',  
    @level2type = N'COLUMN', @level2name = N'TFLAG'  
 END
GO
/*2、新增表：通用字段默认值配置(DBT_BD_COLUMN_DEFAULT)*/
IF OBJECT_ID('DBT_BD_COLUMN_DEFAULT', 'U') IS  NULL 
 BEGIN 
CREATE TABLE DBT_BD_COLUMN_DEFAULT 
(
 	COL_DEFAULT_ID varchar(36)  CONSTRAINT [PK_DBT_BD_COLUMN_DEFAULT] PRIMARY KEY(COL_DEFAULT_ID)  NOT NULL  ,/*列默认配置ID*/
	COLUMN_NAME varchar(50)  NULL  ,/*列编码*/
	DEFAULT_MYSQL varchar(100)  NULL  ,/*MySql默认值*/
	DEFAULT_POSTGRESQL varchar(100)  NULL  ,/*PostgreSql默认值*/
	DEFAULT_ORACLE varchar(100)  NULL  ,/*Oracle默认值*/
	DEFAULT_SQLSERVER varchar(100)  NULL  ,/*SqlServer默认值*/
	DEFAULT_SQLITE varchar(100)  NULL  ,/*SQLite默认值*/
	IS_USED_ADD varchar(2)  NULL  ,/*是否新增使用*/
	IS_USED_UPDATE varchar(2)  NULL  ,/*是否修改使用*/
	IS_CONDITION_QUERY varchar(2)  NULL  ,/*是否查询条件*/
	IS_CONDITION_UPDATE varchar(2)  NULL  ,/*是否更新条件*/
	IS_CONDITION_DELETE varchar(2)  NULL  ,/*是否删除条件*/
	SORT_ID int   NULL  ,/*排序ID*/
	REMARK varchar(200)  NULL  ,/*备注*/
	CREATE_TIME datetime   NOT NULL   CONSTRAINT DF_DBT_BD_COLUMN_DEFAULT_CREATE_TIME DEFAULT(getdate()) ,/*创建时间*/
	CREATOR_ID varchar(36)  NOT NULL  ,/*创建人ID*/
	CREATOR varchar(50)  NULL  ,/*创建人*/
	MODIFIER_ID varchar(36)  NULL  ,/*修改人ID*/
	MODIFIER varchar(50)  NULL  ,/*修改人*/
	LAST_UPDATED_TIME datetime   NOT NULL   CONSTRAINT DF_DBT_BD_COLUMN_DEFAULT_LAST_UPDATED_TIME DEFAULT(getdate()) ,/*最后更新时间*/
	IS_ENABLED varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_COLUMN_DEFAULT_IS_ENABLED DEFAULT('1') ,/*是否有效*/
	IS_SYSTEM varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_COLUMN_DEFAULT_IS_SYSTEM DEFAULT('0') ,/*系统标志*/
	ORG_ID varchar(36)  NOT NULL  ,/*组织ID*/
	UPDATE_CONTROL_ID varchar(36)  NOT NULL  ,/*并发控制ID*/
	TFLAG varchar(2)  NOT NULL   CONSTRAINT DF_DBT_BD_COLUMN_DEFAULT_TFLAG DEFAULT('0') /*传输标志*/
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通用字段默认值配置：',
   @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DBT_BD_COLUMN_DEFAULT'
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'列默认配置ID：主键ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'COL_DEFAULT_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'列编码',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'COLUMN_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'MySql默认值',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'DEFAULT_MYSQL'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'PostgreSql默认值',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'DEFAULT_POSTGRESQL'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'Oracle默认值',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'DEFAULT_ORACLE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'SqlServer默认值',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'DEFAULT_SQLSERVER'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'SQLite默认值',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'DEFAULT_SQLITE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否新增使用',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_USED_ADD'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否修改使用',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_USED_UPDATE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否查询条件',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_CONDITION_QUERY'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否更新条件',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_CONDITION_UPDATE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否删除条件',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_CONDITION_DELETE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'排序ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'SORT_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'备注',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'REMARK'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'CREATE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'CREATOR_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'CREATOR'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'LAST_UPDATED_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_ENABLED'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'系统标志',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'IS_SYSTEM'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'组织ID：表示数据所属的公司',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'ORG_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'并发控制ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'UPDATE_CONTROL_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'传输标志：0不上传，1未上传，2成功上传，3上传出错',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'DBT_BD_COLUMN_DEFAULT',  
    @level2type = N'COLUMN', @level2name = N'TFLAG'  
 END
GO

