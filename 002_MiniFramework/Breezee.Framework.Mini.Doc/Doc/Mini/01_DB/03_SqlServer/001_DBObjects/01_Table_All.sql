/***********************************************************************************
* 脚本描述: 新增修改表
* 创建作者: 
* 创建日期: 2021/11/2 
* 使用模块：
* 使用版本: 
* 说    明：
      1 新增用户表（SYS_USER）
      2 新增系统日志表（SYS_LOG）
***********************************************************************************/
/*1、新增表：用户表(SYS_USER)*/
IF OBJECT_ID('SYS_USER', 'U') IS  NULL 
 BEGIN 
CREATE TABLE SYS_USER 
(
 	USER_ID varchar(36)  CONSTRAINT [PK_SYS_USER] PRIMARY KEY(USER_ID)  NOT NULL  ,/*用户ID*/
	USER_CODE varchar(30)  NOT NULL  ,/*用户编码*/
	EMP_ID varchar(36)  NULL  ,/*职员ID*/
	USER_NAME varchar(50)  NULL  ,/*用户名*/
	USER_NAME_EN varchar(100)  NULL  ,/*用户英文名*/
	USER_PASSWORD varchar(200)  NULL  ,/*用户密码*/
	USER_TYPE varchar(10)  NOT NULL  ,/*用户类型*/
	ENCRYPT_SALT varchar(50)  NULL  ,/*加密盐*/
	PIN_YIN varchar(60)  NULL  ,/*拼音码*/
	LAST_LOGIN_TIME datetime   NULL  ,/*最后登录时间*/
	LOGIN_STATE int   NULL  ,/*登录状态*/
	TICKET_ID varchar(36)  NULL  ,/*令牌号*/
	DESCRIPTION varchar(255)  NULL  ,/*描述*/
	ACTIVE_TIME datetime   NOT NULL  ,/*激活时间*/
	DISABLE_TIME datetime   NOT NULL  ,/*失效时间*/
	SORT_ID int   NULL  ,/*排序ID*/
	REMARK varchar(200)  NULL  ,/*备注*/
	CREATE_TIME datetime   NOT NULL   CONSTRAINT DF_SYS_USER_CREATE_TIME DEFAULT(getdate()) ,/*创建时间*/
	CREATOR_ID varchar(36)  NOT NULL  ,/*创建人ID*/
	CREATOR varchar(50)  NULL  ,/*创建人*/
	MODIFIER_ID varchar(36)  NULL  ,/*修改人ID*/
	MODIFIER varchar(50)  NULL  ,/*修改人*/
	LAST_UPDATED_TIME datetime   NOT NULL   CONSTRAINT DF_SYS_USER_LAST_UPDATED_TIME DEFAULT(getdate()) ,/*最后更新时间*/
	IS_ENABLED varchar(2)  NOT NULL   CONSTRAINT DF_SYS_USER_IS_ENABLED DEFAULT('1') ,/*是否有效*/
	IS_SYSTEM varchar(2)  NOT NULL   CONSTRAINT DF_SYS_USER_IS_SYSTEM DEFAULT('0') ,/*系统标志*/
	ORG_ID varchar(36)  NOT NULL  ,/*组织ID*/
	UPDATE_CONTROL_ID varchar(36)  NOT NULL  ,/*并发控制ID*/
	TFLAG varchar(2)  NOT NULL   CONSTRAINT DF_SYS_USER_TFLAG DEFAULT('0') /*传输标志*/
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表：',
   @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_USER'
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID：主键ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户编码：即登录名',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_CODE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'职员ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'EMP_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户名',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_NAME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户英文名',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_NAME_EN'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户密码',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_PASSWORD'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'用户类型：1系统管理员，2一般用户。注：管理员具有全部菜单权限',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'USER_TYPE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'加密盐',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'ENCRYPT_SALT'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'拼音码',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'PIN_YIN'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'最后登录时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'LAST_LOGIN_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'登录状态',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'LOGIN_STATE'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'令牌号',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'TICKET_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'描述',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'DESCRIPTION'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'激活时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'ACTIVE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'失效时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'DISABLE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'排序ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'SORT_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'备注',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'REMARK'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'CREATE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'CREATOR_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'CREATOR'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'LAST_UPDATED_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'IS_ENABLED'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'系统标志',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'IS_SYSTEM'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'组织ID：表示数据所属的公司',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'ORG_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'并发控制ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'UPDATE_CONTROL_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'传输标志：0不上传，1未上传，2成功上传，3上传出错',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_USER',  
    @level2type = N'COLUMN', @level2name = N'TFLAG'  
 END
GO
/*2、新增表：系统日志表(SYS_LOG)*/
IF OBJECT_ID('SYS_LOG', 'U') IS  NULL 
 BEGIN 
CREATE TABLE SYS_LOG 
(
 	LogID varchar(36)  CONSTRAINT [PK_SYS_LOG] PRIMARY KEY(LogID)  NOT NULL  ,/*日志ID*/
	AppName varchar(100)  NULL  ,/*应用程序名称*/
	ModuleName varchar(50)  NULL  ,/*模块名称*/
	ProcName varchar(100)  NULL  ,/*处理过程名称*/
	LogLevel varchar(20)  NULL  ,/*日志级别*/
	LogTitle varchar(100)  NULL  ,/*日志标题*/
	LogMessage varchar(4000)  NULL  ,/*日志内容*/
	LogDate datetime   NULL  ,/*记录日期*/
	StackTrace varchar(4000)  NULL  ,/*跟踪信息*/
	REMARK varchar(200)  NULL  ,/*备注*/
	CREATE_TIME datetime   NOT NULL   CONSTRAINT DF_SYS_LOG_CREATE_TIME DEFAULT(getdate()) ,/*创建时间*/
	CREATOR_ID varchar(36)  NULL  ,/*创建人ID*/
	CREATOR varchar(50)  NULL  ,/*创建人*/
	MODIFIER_ID varchar(36)  NULL  ,/*修改人ID*/
	MODIFIER varchar(50)  NULL  ,/*修改人*/
	LAST_UPDATED_TIME datetime   NOT NULL   CONSTRAINT DF_SYS_LOG_LAST_UPDATED_TIME DEFAULT(getdate()) ,/*最后更新时间*/
	IS_ENABLED varchar(2)  NOT NULL   CONSTRAINT DF_SYS_LOG_IS_ENABLED DEFAULT('1') ,/*是否有效*/
	IS_SYSTEM varchar(2)  NOT NULL   CONSTRAINT DF_SYS_LOG_IS_SYSTEM DEFAULT('0') ,/*系统标志*/
	ORG_ID varchar(36)  NULL  ,/*组织ID*/
	UPDATE_CONTROL_ID varchar(36)  NOT NULL  ,/*并发控制ID*/
	TFLAG varchar(2)  NOT NULL   CONSTRAINT DF_SYS_LOG_TFLAG DEFAULT('0') /*传输标志*/
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统日志表：',
   @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_LOG'
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'日志ID：主键ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LogID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'应用程序名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'AppName'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'模块名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'ModuleName'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'处理过程名称',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'ProcName'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'日志级别',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LogLevel'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'日志标题',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LogTitle'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LogMessage'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'记录日期',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LogDate'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'跟踪信息',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'StackTrace'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'备注',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'REMARK'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'CREATE_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'CREATOR_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'CREATOR'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'修改人',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'MODIFIER'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新时间',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'LAST_UPDATED_TIME'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'IS_ENABLED'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'系统标志',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'IS_SYSTEM'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'组织ID：表示数据所属的公司',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'ORG_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'并发控制ID',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'UPDATE_CONTROL_ID'  
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'传输标志：0不上传，1未上传，2成功上传，3上传出错',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'SYS_LOG',  
    @level2type = N'COLUMN', @level2name = N'TFLAG'  
 END
GO

