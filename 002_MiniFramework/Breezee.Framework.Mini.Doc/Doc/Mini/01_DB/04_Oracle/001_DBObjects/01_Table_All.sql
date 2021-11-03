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
CREATE TABLE SYS_USER (
USER_ID varchar2(36) NOT NULL ,/*用户ID*/
USER_CODE varchar2(30) NOT NULL ,/*用户编码*/
EMP_ID varchar2(36) NULL ,/*职员ID*/
USER_NAME varchar2(50) NULL ,/*用户名*/
USER_NAME_EN varchar2(100) NULL ,/*用户英文名*/
USER_PASSWORD varchar2(200) NULL ,/*用户密码*/
USER_TYPE varchar2(10) NOT NULL ,/*用户类型*/
ENCRYPT_SALT varchar2(50) NULL ,/*加密盐*/
PIN_YIN varchar2(60) NULL ,/*拼音码*/
LAST_LOGIN_TIME date  NULL ,/*最后登录时间*/
LOGIN_STATE int  NULL ,/*登录状态*/
TICKET_ID varchar2(36) NULL ,/*令牌号*/
DESCRIPTION varchar2(255) NULL ,/*描述*/
ACTIVE_TIME date  NOT NULL ,/*激活时间*/
DISABLE_TIME date  NOT NULL ,/*失效时间*/
SORT_ID int  NULL ,/*排序ID*/
REMARK varchar2(200) NULL ,/*备注*/
CREATE_TIME date  DEFAULT sysdate  NOT NULL ,/*创建时间*/
CREATOR_ID varchar2(36) NOT NULL ,/*创建人ID*/
CREATOR varchar2(50) NULL ,/*创建人*/
MODIFIER_ID varchar2(36) NULL ,/*修改人ID*/
MODIFIER varchar2(50) NULL ,/*修改人*/
LAST_UPDATED_TIME date  DEFAULT sysdate  NOT NULL ,/*最后更新时间*/
IS_ENABLED varchar2(2) DEFAULT 1  NOT NULL ,/*是否有效*/
IS_SYSTEM varchar2(2) DEFAULT 0  NOT NULL ,/*系统标志*/
ORG_ID varchar2(36) NOT NULL ,/*组织ID*/
UPDATE_CONTROL_ID varchar2(36) NOT NULL ,/*并发控制ID*/
TFLAG varchar2(2) DEFAULT 0  NOT NULL /*传输标志*/
);
alter table SYS_USER add constraint PK_SYS_USER primary key (USER_ID);
comment on table SYS_USER is '用户表：';
comment on column SYS_USER.USER_ID is '用户ID：主键ID';
comment on column SYS_USER.USER_CODE is '用户编码：即登录名';
comment on column SYS_USER.EMP_ID is '职员ID';
comment on column SYS_USER.USER_NAME is '用户名';
comment on column SYS_USER.USER_NAME_EN is '用户英文名';
comment on column SYS_USER.USER_PASSWORD is '用户密码';
comment on column SYS_USER.USER_TYPE is '用户类型：1系统管理员，2一般用户。注：管理员具有全部菜单权限';
comment on column SYS_USER.ENCRYPT_SALT is '加密盐';
comment on column SYS_USER.PIN_YIN is '拼音码';
comment on column SYS_USER.LAST_LOGIN_TIME is '最后登录时间';
comment on column SYS_USER.LOGIN_STATE is '登录状态';
comment on column SYS_USER.TICKET_ID is '令牌号';
comment on column SYS_USER.DESCRIPTION is '描述';
comment on column SYS_USER.ACTIVE_TIME is '激活时间';
comment on column SYS_USER.DISABLE_TIME is '失效时间';
comment on column SYS_USER.SORT_ID is '排序ID';
comment on column SYS_USER.REMARK is '备注';
comment on column SYS_USER.CREATE_TIME is '创建时间';
comment on column SYS_USER.CREATOR_ID is '创建人ID';
comment on column SYS_USER.CREATOR is '创建人';
comment on column SYS_USER.MODIFIER_ID is '修改人ID';
comment on column SYS_USER.MODIFIER is '修改人';
comment on column SYS_USER.LAST_UPDATED_TIME is '最后更新时间';
comment on column SYS_USER.IS_ENABLED is '是否有效';
comment on column SYS_USER.IS_SYSTEM is '系统标志';
comment on column SYS_USER.ORG_ID is '组织ID：表示数据所属的公司';
comment on column SYS_USER.UPDATE_CONTROL_ID is '并发控制ID';
comment on column SYS_USER.TFLAG is '传输标志：0不上传，1未上传，2成功上传，3上传出错';
/
/*2、新增表：系统日志表(SYS_LOG)*/
CREATE TABLE SYS_LOG (
LogID varchar2(36) NOT NULL ,/*日志ID*/
AppName varchar2(100) NULL ,/*应用程序名称*/
ModuleName varchar2(50) NULL ,/*模块名称*/
ProcName varchar2(100) NULL ,/*处理过程名称*/
LogLevel varchar2(20) NULL ,/*日志级别*/
LogTitle varchar2(100) NULL ,/*日志标题*/
LogMessage varchar2(4000) NULL ,/*日志内容*/
LogDate date  NULL ,/*记录日期*/
StackTrace varchar2(4000) NULL ,/*跟踪信息*/
REMARK varchar2(200) NULL ,/*备注*/
CREATE_TIME date  DEFAULT sysdate  NOT NULL ,/*创建时间*/
CREATOR_ID varchar2(36) NULL ,/*创建人ID*/
CREATOR varchar2(50) NULL ,/*创建人*/
MODIFIER_ID varchar2(36) NULL ,/*修改人ID*/
MODIFIER varchar2(50) NULL ,/*修改人*/
LAST_UPDATED_TIME date  DEFAULT sysdate  NOT NULL ,/*最后更新时间*/
IS_ENABLED varchar2(2) DEFAULT 1  NOT NULL ,/*是否有效*/
IS_SYSTEM varchar2(2) DEFAULT 0  NOT NULL ,/*系统标志*/
ORG_ID varchar2(36) NULL ,/*组织ID*/
UPDATE_CONTROL_ID varchar2(36) NOT NULL ,/*并发控制ID*/
TFLAG varchar2(2) DEFAULT 0  NOT NULL /*传输标志*/
);
alter table SYS_LOG add constraint PK_SYS_LOG primary key (LogID);
comment on table SYS_LOG is '系统日志表：';
comment on column SYS_LOG.LogID is '日志ID：主键ID';
comment on column SYS_LOG.AppName is '应用程序名称';
comment on column SYS_LOG.ModuleName is '模块名称';
comment on column SYS_LOG.ProcName is '处理过程名称';
comment on column SYS_LOG.LogLevel is '日志级别';
comment on column SYS_LOG.LogTitle is '日志标题';
comment on column SYS_LOG.LogMessage is '日志内容';
comment on column SYS_LOG.LogDate is '记录日期';
comment on column SYS_LOG.StackTrace is '跟踪信息';
comment on column SYS_LOG.REMARK is '备注';
comment on column SYS_LOG.CREATE_TIME is '创建时间';
comment on column SYS_LOG.CREATOR_ID is '创建人ID';
comment on column SYS_LOG.CREATOR is '创建人';
comment on column SYS_LOG.MODIFIER_ID is '修改人ID';
comment on column SYS_LOG.MODIFIER is '修改人';
comment on column SYS_LOG.LAST_UPDATED_TIME is '最后更新时间';
comment on column SYS_LOG.IS_ENABLED is '是否有效';
comment on column SYS_LOG.IS_SYSTEM is '系统标志';
comment on column SYS_LOG.ORG_ID is '组织ID：表示数据所属的公司';
comment on column SYS_LOG.UPDATE_CONTROL_ID is '并发控制ID';
comment on column SYS_LOG.TFLAG is '传输标志：0不上传，1未上传，2成功上传，3上传出错';
/

