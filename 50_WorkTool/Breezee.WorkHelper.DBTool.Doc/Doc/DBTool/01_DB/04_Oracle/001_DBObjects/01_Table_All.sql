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
CREATE TABLE DBT_BD_DB_CONFIG (
DB_CONFIG_ID varchar2(36) NOT NULL ,/*数据库配置ID*/
DB_CONFIG_CODE varchar2(50) NULL ,/*数据库配置编码*/
DB_CONFIG_NAME varchar2(100) NULL ,/*数据库配置名称*/
DB_TYPE varchar2(2) NULL ,/*数据库类型*/
SERVER_IP varchar2(400) NULL ,/*服务器IP*/
PORT_NO varchar2(20) NULL ,/*端口号*/
SCHEMA_NAME varchar2(30) NULL ,/*架构名称*/
DB_NAME varchar2(30) NULL ,/*数据库名称*/
USER_NAME varchar2(50) NULL ,/*用户名*/
USER_PASSWORD varchar2(50) NULL ,/*用户密码*/
LOGIN_TYPE varchar2(2) NULL ,/*登录类型*/
TYPE_DESC varchar2(200) NULL ,/*描述*/
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
alter table DBT_BD_DB_CONFIG add constraint PK_DBT_BD_DB_CONFIG primary key (DB_CONFIG_ID);
comment on table DBT_BD_DB_CONFIG is '数据库配置：';
comment on column DBT_BD_DB_CONFIG.DB_CONFIG_ID is '数据库配置ID：主键ID';
comment on column DBT_BD_DB_CONFIG.DB_CONFIG_CODE is '数据库配置编码';
comment on column DBT_BD_DB_CONFIG.DB_CONFIG_NAME is '数据库配置名称';
comment on column DBT_BD_DB_CONFIG.DB_TYPE is '数据库类型';
comment on column DBT_BD_DB_CONFIG.SERVER_IP is '服务器IP：对Oracle类型，为TNS名称,SQLite是存文件路径';
comment on column DBT_BD_DB_CONFIG.PORT_NO is '端口号';
comment on column DBT_BD_DB_CONFIG.SCHEMA_NAME is '架构名称';
comment on column DBT_BD_DB_CONFIG.DB_NAME is '数据库名称';
comment on column DBT_BD_DB_CONFIG.USER_NAME is '用户名';
comment on column DBT_BD_DB_CONFIG.USER_PASSWORD is '用户密码';
comment on column DBT_BD_DB_CONFIG.LOGIN_TYPE is '登录类型：针对SqlServer，有Windows登录和SQL登录两种。';
comment on column DBT_BD_DB_CONFIG.TYPE_DESC is '描述';
comment on column DBT_BD_DB_CONFIG.SORT_ID is '排序ID';
comment on column DBT_BD_DB_CONFIG.REMARK is '备注';
comment on column DBT_BD_DB_CONFIG.CREATE_TIME is '创建时间';
comment on column DBT_BD_DB_CONFIG.CREATOR_ID is '创建人ID';
comment on column DBT_BD_DB_CONFIG.CREATOR is '创建人';
comment on column DBT_BD_DB_CONFIG.MODIFIER_ID is '修改人ID';
comment on column DBT_BD_DB_CONFIG.MODIFIER is '修改人';
comment on column DBT_BD_DB_CONFIG.LAST_UPDATED_TIME is '最后更新时间';
comment on column DBT_BD_DB_CONFIG.IS_ENABLED is '是否有效';
comment on column DBT_BD_DB_CONFIG.IS_SYSTEM is '系统标志';
comment on column DBT_BD_DB_CONFIG.ORG_ID is '组织ID：表示数据所属的公司';
comment on column DBT_BD_DB_CONFIG.UPDATE_CONTROL_ID is '并发控制ID';
comment on column DBT_BD_DB_CONFIG.TFLAG is '传输标志：0不上传，1未上传，2成功上传，3上传出错';
/
/*2、新增表：通用字段默认值配置(DBT_BD_COLUMN_DEFAULT)*/
CREATE TABLE DBT_BD_COLUMN_DEFAULT (
COL_DEFAULT_ID varchar2(36) NOT NULL ,/*列默认配置ID*/
COLUMN_NAME varchar2(50) NULL ,/*列编码*/
DEFAULT_MYSQL varchar2(100) NULL ,/*MySql默认值*/
DEFAULT_POSTGRESQL varchar2(100) NULL ,/*PostgreSql默认值*/
DEFAULT_ORACLE varchar2(100) NULL ,/*Oracle默认值*/
DEFAULT_SQLSERVER varchar2(100) NULL ,/*SqlServer默认值*/
DEFAULT_SQLITE varchar2(100) NULL ,/*SQLite默认值*/
IS_USED_ADD varchar2(2) NULL ,/*是否新增使用*/
IS_USED_UPDATE varchar2(2) NULL ,/*是否修改使用*/
IS_CONDITION_QUERY varchar2(2) NULL ,/*是否查询条件*/
IS_CONDITION_UPDATE varchar2(2) NULL ,/*是否更新条件*/
IS_CONDITION_DELETE varchar2(2) NULL ,/*是否删除条件*/
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
alter table DBT_BD_COLUMN_DEFAULT add constraint PK_DBT_BD_COLUMN_DEFAULT primary key (COL_DEFAULT_ID);
comment on table DBT_BD_COLUMN_DEFAULT is '通用字段默认值配置：';
comment on column DBT_BD_COLUMN_DEFAULT.COL_DEFAULT_ID is '列默认配置ID：主键ID';
comment on column DBT_BD_COLUMN_DEFAULT.COLUMN_NAME is '列编码';
comment on column DBT_BD_COLUMN_DEFAULT.DEFAULT_MYSQL is 'MySql默认值';
comment on column DBT_BD_COLUMN_DEFAULT.DEFAULT_POSTGRESQL is 'PostgreSql默认值';
comment on column DBT_BD_COLUMN_DEFAULT.DEFAULT_ORACLE is 'Oracle默认值';
comment on column DBT_BD_COLUMN_DEFAULT.DEFAULT_SQLSERVER is 'SqlServer默认值';
comment on column DBT_BD_COLUMN_DEFAULT.DEFAULT_SQLITE is 'SQLite默认值';
comment on column DBT_BD_COLUMN_DEFAULT.IS_USED_ADD is '是否新增使用';
comment on column DBT_BD_COLUMN_DEFAULT.IS_USED_UPDATE is '是否修改使用';
comment on column DBT_BD_COLUMN_DEFAULT.IS_CONDITION_QUERY is '是否查询条件';
comment on column DBT_BD_COLUMN_DEFAULT.IS_CONDITION_UPDATE is '是否更新条件';
comment on column DBT_BD_COLUMN_DEFAULT.IS_CONDITION_DELETE is '是否删除条件';
comment on column DBT_BD_COLUMN_DEFAULT.SORT_ID is '排序ID';
comment on column DBT_BD_COLUMN_DEFAULT.REMARK is '备注';
comment on column DBT_BD_COLUMN_DEFAULT.CREATE_TIME is '创建时间';
comment on column DBT_BD_COLUMN_DEFAULT.CREATOR_ID is '创建人ID';
comment on column DBT_BD_COLUMN_DEFAULT.CREATOR is '创建人';
comment on column DBT_BD_COLUMN_DEFAULT.MODIFIER_ID is '修改人ID';
comment on column DBT_BD_COLUMN_DEFAULT.MODIFIER is '修改人';
comment on column DBT_BD_COLUMN_DEFAULT.LAST_UPDATED_TIME is '最后更新时间';
comment on column DBT_BD_COLUMN_DEFAULT.IS_ENABLED is '是否有效';
comment on column DBT_BD_COLUMN_DEFAULT.IS_SYSTEM is '系统标志';
comment on column DBT_BD_COLUMN_DEFAULT.ORG_ID is '组织ID：表示数据所属的公司';
comment on column DBT_BD_COLUMN_DEFAULT.UPDATE_CONTROL_ID is '并发控制ID';
comment on column DBT_BD_COLUMN_DEFAULT.TFLAG is '传输标志：0不上传，1未上传，2成功上传，3上传出错';
/

