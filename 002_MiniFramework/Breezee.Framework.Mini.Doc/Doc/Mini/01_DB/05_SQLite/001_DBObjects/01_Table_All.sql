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
CREATE TABLE IF NOT EXISTS "SYS_USER" (
USER_ID varchar(36) PRIMARY KEY  NOT NULL ,/*用户ID*/
USER_CODE varchar(30) NOT NULL ,/*用户编码*/
EMP_ID varchar(36),/*职员ID*/
USER_NAME varchar(50),/*用户名*/
USER_NAME_EN varchar(100),/*用户英文名*/
USER_PASSWORD varchar(200),/*用户密码*/
USER_TYPE varchar(10) NOT NULL ,/*用户类型*/
ENCRYPT_SALT varchar(50),/*加密盐*/
PIN_YIN varchar(60),/*拼音码*/
LAST_LOGIN_TIME datetime ,/*最后登录时间*/
LOGIN_STATE int ,/*登录状态*/
TICKET_ID varchar(36),/*令牌号*/
DESCRIPTION varchar(255),/*描述*/
ACTIVE_TIME datetime  NOT NULL ,/*激活时间*/
DISABLE_TIME datetime  NOT NULL ,/*失效时间*/
SORT_ID int ,/*排序ID*/
REMARK varchar(200),/*备注*/
CREATE_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')) ,/*创建时间*/
CREATOR_ID varchar(36) NOT NULL ,/*创建人ID*/
CREATOR varchar(50),/*创建人*/
MODIFIER_ID varchar(36),/*修改人ID*/
MODIFIER varchar(50),/*修改人*/
LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*最后更新时间*/
IS_ENABLED varchar(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID varchar(36) NOT NULL ,/*组织ID*/
UPDATE_CONTROL_ID varchar(36) NOT NULL ,/*并发控制ID*/
TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
);
/*2、新增表：系统日志表(SYS_LOG)*/
CREATE TABLE IF NOT EXISTS "SYS_LOG" (
LogID varchar(36) PRIMARY KEY  NOT NULL ,/*日志ID*/
AppName varchar(100),/*应用程序名称*/
ModuleName varchar(50),/*模块名称*/
ProcName varchar(100),/*处理过程名称*/
LogLevel varchar(20),/*日志级别*/
LogTitle varchar(100),/*日志标题*/
LogMessage varchar(4000),/*日志内容*/
LogDate datetime ,/*记录日期*/
StackTrace varchar(4000),/*跟踪信息*/
REMARK varchar(200),/*备注*/
CREATE_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*创建时间*/
CREATOR_ID varchar(36),/*创建人ID*/
CREATOR varchar(50),/*创建人*/
MODIFIER_ID varchar(36),/*修改人ID*/
MODIFIER varchar(50),/*修改人*/
LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*最后更新时间*/
IS_ENABLED varchar(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID varchar(36),/*组织ID*/
UPDATE_CONTROL_ID varchar(36) NOT NULL ,/*并发控制ID*/
TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
);

