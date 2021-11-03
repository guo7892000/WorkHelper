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
CREATE TABLE IF NOT EXISTS `DBT_BD_DB_CONFIG` (
DB_CONFIG_ID varchar(36) NOT NULL  COMMENT '数据库配置ID:主键ID',/*数据库配置ID*/
DB_CONFIG_CODE varchar(50) COMMENT '数据库配置编码:',/*数据库配置编码*/
DB_CONFIG_NAME varchar(100) COMMENT '数据库配置名称:',/*数据库配置名称*/
DB_TYPE varchar(2) COMMENT '数据库类型:',/*数据库类型*/
SERVER_IP varchar(400) COMMENT '服务器IP:对Oracle类型，为TNS名称,SQLite是存文件路径',/*服务器IP*/
PORT_NO varchar(20) COMMENT '端口号:',/*端口号*/
SCHEMA_NAME varchar(30) COMMENT '架构名称:',/*架构名称*/
DB_NAME varchar(30) COMMENT '数据库名称:',/*数据库名称*/
USER_NAME varchar(50) COMMENT '用户名:',/*用户名*/
USER_PASSWORD varchar(50) COMMENT '用户密码:',/*用户密码*/
LOGIN_TYPE varchar(2) COMMENT '登录类型:针对SqlServer，有Windows登录和SQL登录两种。',/*登录类型*/
TYPE_DESC varchar(200) COMMENT '描述:',/*描述*/
SORT_ID int  COMMENT '排序ID:',/*排序ID*/
REMARK varchar(200) COMMENT '备注:',/*备注*/
CREATE_TIME datetime  NOT NULL  COMMENT '创建时间:',/*创建时间*/
CREATOR_ID varchar(36) NOT NULL  COMMENT '创建人ID:',/*创建人ID*/
CREATOR varchar(50) COMMENT '创建人:',/*创建人*/
MODIFIER_ID varchar(36) COMMENT '修改人ID:',/*修改人ID*/
MODIFIER varchar(50) COMMENT '修改人:',/*修改人*/
LAST_UPDATED_TIME datetime  NOT NULL  COMMENT '最后更新时间:',/*最后更新时间*/
IS_ENABLED varchar(2) NOT NULL  DEFAULT 1  COMMENT '是否有效:',/*是否有效*/
IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0  COMMENT '系统标志:',/*系统标志*/
ORG_ID varchar(36) NOT NULL  COMMENT '组织ID:表示数据所属的公司',/*组织ID*/
UPDATE_CONTROL_ID varchar(36) NOT NULL  COMMENT '并发控制ID:',/*并发控制ID*/
TFLAG varchar(2) NOT NULL  DEFAULT 0  COMMENT '传输标志:0不上传，1未上传，2成功上传，3上传出错',/*传输标志*/
 primary key (DB_CONFIG_ID) 
);
ALTER TABLE `DBT_BD_DB_CONFIG` COMMENT '数据库配置：';

