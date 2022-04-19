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
CREATE TABLE IF NOT EXISTS SYS_USER (
USER_ID character varying(36) NOT NULL ,/*用户ID*/
USER_CODE character varying(30) NOT NULL ,/*用户编码*/
EMP_ID character varying(36),/*职员ID*/
USER_NAME character varying(50),/*用户名*/
USER_NAME_EN character varying(100),/*用户英文名*/
USER_PASSWORD character varying(200),/*用户密码*/
USER_TYPE character varying(10) NOT NULL ,/*用户类型*/
ENCRYPT_SALT character varying(50),/*加密盐*/
PIN_YIN character varying(60),/*拼音码*/
LAST_LOGIN_TIME date ,/*最后登录时间*/
LOGIN_STATE int ,/*登录状态*/
TICKET_ID character varying(36),/*令牌号*/
DESCRIPTION character varying(255),/*描述*/
ACTIVE_TIME date  NOT NULL ,/*激活时间*/
DISABLE_TIME date  NOT NULL ,/*失效时间*/
SORT_ID int ,/*排序ID*/
REMARK character varying(200),/*备注*/
CREATE_TIME date  NOT NULL  DEFAULT now() ,/*创建时间*/
CREATOR_ID character varying(36) NOT NULL ,/*创建人ID*/
CREATOR character varying(50),/*创建人*/
MODIFIER_ID character varying(36),/*修改人ID*/
MODIFIER character varying(50),/*修改人*/
LAST_UPDATED_TIME date  NOT NULL  DEFAULT now() ,/*最后更新时间*/
IS_ENABLED character varying(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM character varying(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID character varying(36) NOT NULL ,/*组织ID*/
UPDATE_CONTROL_ID character varying(36) NOT NULL ,/*并发控制ID*/
TFLAG character varying(2) NOT NULL  DEFAULT 0 ,/*传输标志*/
 PRIMARY KEY (USER_ID)
);
COMMENT ON TABLE SYS_USER IS '用户表：';
 COMMENT ON COLUMN SYS_USER.USER_ID IS '用户ID:主键ID';
 COMMENT ON COLUMN SYS_USER.USER_CODE IS '用户编码:即登录名';
 COMMENT ON COLUMN SYS_USER.EMP_ID IS '职员ID:';
 COMMENT ON COLUMN SYS_USER.USER_NAME IS '用户名:';
 COMMENT ON COLUMN SYS_USER.USER_NAME_EN IS '用户英文名:';
 COMMENT ON COLUMN SYS_USER.USER_PASSWORD IS '用户密码:';
 COMMENT ON COLUMN SYS_USER.USER_TYPE IS '用户类型:1系统管理员，2一般用户。注：管理员具有全部菜单权限';
 COMMENT ON COLUMN SYS_USER.ENCRYPT_SALT IS '加密盐:';
 COMMENT ON COLUMN SYS_USER.PIN_YIN IS '拼音码:';
 COMMENT ON COLUMN SYS_USER.LAST_LOGIN_TIME IS '最后登录时间:';
 COMMENT ON COLUMN SYS_USER.LOGIN_STATE IS '登录状态:';
 COMMENT ON COLUMN SYS_USER.TICKET_ID IS '令牌号:';
 COMMENT ON COLUMN SYS_USER.DESCRIPTION IS '描述:';
 COMMENT ON COLUMN SYS_USER.ACTIVE_TIME IS '激活时间:';
 COMMENT ON COLUMN SYS_USER.DISABLE_TIME IS '失效时间:';
 COMMENT ON COLUMN SYS_USER.SORT_ID IS '排序ID:';
 COMMENT ON COLUMN SYS_USER.REMARK IS '备注:';
 COMMENT ON COLUMN SYS_USER.CREATE_TIME IS '创建时间:';
 COMMENT ON COLUMN SYS_USER.CREATOR_ID IS '创建人ID:';
 COMMENT ON COLUMN SYS_USER.CREATOR IS '创建人:';
 COMMENT ON COLUMN SYS_USER.MODIFIER_ID IS '修改人ID:';
 COMMENT ON COLUMN SYS_USER.MODIFIER IS '修改人:';
 COMMENT ON COLUMN SYS_USER.LAST_UPDATED_TIME IS '最后更新时间:';
 COMMENT ON COLUMN SYS_USER.IS_ENABLED IS '是否有效:';
 COMMENT ON COLUMN SYS_USER.IS_SYSTEM IS '系统标志:';
 COMMENT ON COLUMN SYS_USER.ORG_ID IS '组织ID:表示数据所属的公司';
 COMMENT ON COLUMN SYS_USER.UPDATE_CONTROL_ID IS '并发控制ID:';
 COMMENT ON COLUMN SYS_USER.TFLAG IS '传输标志:0不上传，1未上传，2成功上传，3上传出错';
/*2、新增表：系统日志表(SYS_LOG)*/
CREATE TABLE IF NOT EXISTS SYS_LOG (
LogID character varying(36) NOT NULL ,/*日志ID*/
AppName character varying(100),/*应用程序名称*/
ModuleName character varying(50),/*模块名称*/
ProcName character varying(100),/*处理过程名称*/
LogLevel character varying(20),/*日志级别*/
LogTitle character varying(100),/*日志标题*/
LogMessage character varying(4000),/*日志内容*/
LogDate date ,/*记录日期*/
StackTrace character varying(4000),/*跟踪信息*/
REMARK character varying(200),/*备注*/
CREATE_TIME date  NOT NULL  DEFAULT now() ,/*创建时间*/
CREATOR_ID character varying(36),/*创建人ID*/
CREATOR character varying(50),/*创建人*/
MODIFIER_ID character varying(36),/*修改人ID*/
MODIFIER character varying(50),/*修改人*/
LAST_UPDATED_TIME date  NOT NULL  DEFAULT now() ,/*最后更新时间*/
IS_ENABLED character varying(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM character varying(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID character varying(36),/*组织ID*/
UPDATE_CONTROL_ID character varying(36) NOT NULL ,/*并发控制ID*/
TFLAG character varying(2) NOT NULL  DEFAULT 0 ,/*传输标志*/
 PRIMARY KEY (LogID)
);
COMMENT ON TABLE SYS_LOG IS '系统日志表：';
 COMMENT ON COLUMN SYS_LOG.LogID IS '日志ID:主键ID';
 COMMENT ON COLUMN SYS_LOG.AppName IS '应用程序名称:';
 COMMENT ON COLUMN SYS_LOG.ModuleName IS '模块名称:';
 COMMENT ON COLUMN SYS_LOG.ProcName IS '处理过程名称:';
 COMMENT ON COLUMN SYS_LOG.LogLevel IS '日志级别:';
 COMMENT ON COLUMN SYS_LOG.LogTitle IS '日志标题:';
 COMMENT ON COLUMN SYS_LOG.LogMessage IS '日志内容:';
 COMMENT ON COLUMN SYS_LOG.LogDate IS '记录日期:';
 COMMENT ON COLUMN SYS_LOG.StackTrace IS '跟踪信息:';
 COMMENT ON COLUMN SYS_LOG.REMARK IS '备注:';
 COMMENT ON COLUMN SYS_LOG.CREATE_TIME IS '创建时间:';
 COMMENT ON COLUMN SYS_LOG.CREATOR_ID IS '创建人ID:';
 COMMENT ON COLUMN SYS_LOG.CREATOR IS '创建人:';
 COMMENT ON COLUMN SYS_LOG.MODIFIER_ID IS '修改人ID:';
 COMMENT ON COLUMN SYS_LOG.MODIFIER IS '修改人:';
 COMMENT ON COLUMN SYS_LOG.LAST_UPDATED_TIME IS '最后更新时间:';
 COMMENT ON COLUMN SYS_LOG.IS_ENABLED IS '是否有效:';
 COMMENT ON COLUMN SYS_LOG.IS_SYSTEM IS '系统标志:';
 COMMENT ON COLUMN SYS_LOG.ORG_ID IS '组织ID:表示数据所属的公司';
 COMMENT ON COLUMN SYS_LOG.UPDATE_CONTROL_ID IS '并发控制ID:';
 COMMENT ON COLUMN SYS_LOG.TFLAG IS '传输标志:0不上传，1未上传，2成功上传，3上传出错';

