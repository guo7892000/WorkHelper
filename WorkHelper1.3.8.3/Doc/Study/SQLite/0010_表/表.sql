/*创建表*/
CREATE TABLE OrderTest(
	ID int not null,
	OrderCode varchar(30),
	Remark
);

/*删除表*/
DROP TABLE OrderTest;

/*更新表增加字段*/
ALTER TABLE OrderTest ADD COLUMN OrderDate Date not null;

/*更新表删除字段*/
ALTER TABLE OrderTest DROP COLUMN OrderDate;

DROP TABLE IF EXISTS ORG_POSITION_LEVEL;

/*1、新增表：类型表(BAS_TYPE)*/
CREATE TABLE IF NOT EXISTS "BAS_TYPE" (
TYPE_ID varchar(36) PRIMARY KEY  NOT NULL ,/*类型ID*/
TYPE_CLASS_BIG varchar(20),/*类型大类*/
TYPE_CLASS_SMALL varchar(20),/*类型小类*/
TYPE_CODE varchar(50),/*类型编码*/
TYPE_NAME varchar(100),/*类型名称*/
IS_FROM_INTERFACE varchar(2),/*是否来源接口*/
MODIFY_MODE varchar(2),/*修改方式*/
BELONG_OBJECT_TYPE varchar(2),/*所属对象类型*/
EXTEND_VALUE1 varchar(50),/*扩展值1*/
EXTEND_VALUE2 varchar(50),/*扩展值2*/
EXTEND_VALUE3 varchar(50),/*扩展值3*/
EXTEND_VALUE4 varchar(50),/*扩展值4*/
EXTEND_VALUE5 varchar(50),/*扩展值5*/
TYPE_DESC varchar(200),/*描述*/
SORT_ID int ,/*排序ID*/
REMARK varchar(200),/*备注*/
CREATE_TIME datetime  NOT NULL ,/*创建时间*/
CREATOR_ID varchar(36) NOT NULL ,/*创建人ID*/
CREATOR varchar(50),/*创建人*/
MODIFIER_ID varchar(36),/*修改人ID*/
MODIFIER varchar(50),/*修改人*/
LAST_UPDATED_TIME datetime  NOT NULL ,/*最后更新时间*/
IS_ENABLED varchar(2) NOT NULL  DEFAULT 1 ,/*是否有效*/
IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0 ,/*系统标志*/
ORG_ID varchar(36) NOT NULL ,/*组织ID*/
UPDATE_CONTROL_ID varchar(36) NOT NULL ,/*并发控制ID*/
TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
);

