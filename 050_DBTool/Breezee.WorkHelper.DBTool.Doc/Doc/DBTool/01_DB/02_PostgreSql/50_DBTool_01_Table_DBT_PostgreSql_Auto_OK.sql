/***********************************************************************************
* 脚本描述: 新增修改表
* 创建作者: 
* 创建日期: 2021/8/10 
* 使用模块：
* 使用版本: 
* 说    明：
      1 新增数据库配置（DBT_BD_DB_CONFIG）
***********************************************************************************/
/*1、新增表：数据库配置(DBT_BD_DB_CONFIG)*/
CREATE TABLE IF NOT EXISTS DBT_BD_DB_CONFIG (
DB_CONFIG_ID character varying(36) NOT NULL ,/*数据库配置ID*/
DB_CONFIG_CODE character varying(50),/*数据库配置编码*/
DB_CONFIG_NAME character varying(100),/*数据库配置名称*/
DB_TYPE character varying(2),/*数据库类型*/
SERVER_IP character varying(400),/*服务器IP*/
PORT_NO character varying(20),/*端口号*/
SCHEMA_NAME character varying(30),/*架构名称*/
DB_NAME character varying(30),/*数据库名称*/
USER_NAME character varying(50),/*用户名*/
USER_PASSWORD character varying(50),/*用户密码*/
LOGIN_TYPE character varying(2),/*登录类型*/
TYPE_DESC character varying(200),/*描述*/
SORT_ID int ,/*排序ID*/
REMARK character varying(200),/*备注*/
CREATE_TIME date  NOT NULL ,/*创建时间*/
CREATOR_ID character varying(36) NOT NULL ,/*创建人ID*/
CREATOR character varying(50),/*创建人*/
MODIFIER_ID character varying(36),/*修改人ID*/
MODIFIER character varying(50),/*修改人*/
LAST_UPDATED_TIME date  NOT NULL ,/*最后更新时间*/
IS_ENABLED character varying(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM character varying(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID character varying(36) NOT NULL ,/*组织ID*/
UPDATE_CONTROL_ID character varying(36) NOT NULL ,/*并发控制ID*/
TFLAG character varying(2) NOT NULL  DEFAULT 0 ,/*传输标志*/
 PRIMARY KEY (DB_CONFIG_ID)
);
COMMENT ON TABLE DBT_BD_DB_CONFIG IS '数据库配置：';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.DB_CONFIG_ID IS '数据库配置ID:主键ID';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.DB_CONFIG_CODE IS '数据库配置编码:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.DB_CONFIG_NAME IS '数据库配置名称:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.DB_TYPE IS '数据库类型:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.SERVER_IP IS '服务器IP:对Oracle类型，为TNS名称,SQLite是存文件路径';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.PORT_NO IS '端口号:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.SCHEMA_NAME IS '架构名称:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.DB_NAME IS '数据库名称:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.USER_NAME IS '用户名:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.USER_PASSWORD IS '用户密码:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.LOGIN_TYPE IS '登录类型:针对SqlServer，有Windows登录和SQL登录两种。';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.TYPE_DESC IS '描述:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.SORT_ID IS '排序ID:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.REMARK IS '备注:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.CREATE_TIME IS '创建时间:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.CREATOR_ID IS '创建人ID:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.CREATOR IS '创建人:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.MODIFIER_ID IS '修改人ID:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.MODIFIER IS '修改人:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.LAST_UPDATED_TIME IS '最后更新时间:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.IS_ENABLED IS '是否有效:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.IS_SYSTEM IS '系统标志:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.ORG_ID IS '组织ID:表示数据所属的公司';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.UPDATE_CONTROL_ID IS '并发控制ID:';
 COMMENT ON COLUMN DBT_BD_DB_CONFIG.TFLAG IS '传输标志:0不上传，1未上传，2成功上传，3上传出错';

