DROP TABLE IF EXISTS ORG_REL_EMPLOYEE_POSITION;

/*1、新增表：类型表(BAS_TYPE)*/
CREATE TABLE IF NOT EXISTS BAS_TYPE (
TYPE_ID character varying(36) NOT NULL ,/*类型ID*/
TYPE_CLASS_BIG character varying(20),/*类型大类*/
TYPE_CLASS_SMALL character varying(20),/*类型小类*/
TYPE_CODE character varying(50),/*类型编码*/
TYPE_NAME character varying(100),/*类型名称*/
IS_FROM_INTERFACE character varying(2),/*是否来源接口*/
MODIFY_MODE character varying(2),/*修改方式*/
BELONG_OBJECT_TYPE character varying(2),/*所属对象类型*/
EXTEND_VALUE1 character varying(50),/*扩展值1*/
EXTEND_VALUE2 character varying(50),/*扩展值2*/
EXTEND_VALUE3 character varying(50),/*扩展值3*/
EXTEND_VALUE4 character varying(50),/*扩展值4*/
EXTEND_VALUE5 character varying(50),/*扩展值5*/
TYPE_DESC character varying(200),/*描述*/
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
 PRIMARY KEY (TYPE_ID)
);
COMMENT ON TABLE BAS_TYPE IS '类型表：';
 COMMENT ON COLUMN BAS_TYPE.TYPE_ID IS '类型ID:主键ID';
 COMMENT ON COLUMN BAS_TYPE.TYPE_CLASS_BIG IS '类型大类:只在查询统计中使用';
 COMMENT ON COLUMN BAS_TYPE.TYPE_CLASS_SMALL IS '类型小类:只在查询统计中使用';
 COMMENT ON COLUMN BAS_TYPE.TYPE_CODE IS '类型编码:';
 COMMENT ON COLUMN BAS_TYPE.TYPE_NAME IS '类型名称:';
 COMMENT ON COLUMN BAS_TYPE.IS_FROM_INTERFACE IS '是否来源接口:0否，1是';
 COMMENT ON COLUMN BAS_TYPE.MODIFY_MODE IS '修改方式:即是否可增加子项：0不可，1可以';
 COMMENT ON COLUMN BAS_TYPE.BELONG_OBJECT_TYPE IS '所属对象类型:1系统定义，2客户定义';
 COMMENT ON COLUMN BAS_TYPE.EXTEND_VALUE1 IS '扩展值1:';
 COMMENT ON COLUMN BAS_TYPE.EXTEND_VALUE2 IS '扩展值2:';
 COMMENT ON COLUMN BAS_TYPE.EXTEND_VALUE3 IS '扩展值3:';
 COMMENT ON COLUMN BAS_TYPE.EXTEND_VALUE4 IS '扩展值4:';
 COMMENT ON COLUMN BAS_TYPE.EXTEND_VALUE5 IS '扩展值5:';
 COMMENT ON COLUMN BAS_TYPE.TYPE_DESC IS '描述:';
 COMMENT ON COLUMN BAS_TYPE.SORT_ID IS '排序ID:';
 COMMENT ON COLUMN BAS_TYPE.REMARK IS '备注:';
 COMMENT ON COLUMN BAS_TYPE.CREATE_TIME IS '创建时间:';
 COMMENT ON COLUMN BAS_TYPE.CREATOR_ID IS '创建人ID:';
 COMMENT ON COLUMN BAS_TYPE.CREATOR IS '创建人:';
 COMMENT ON COLUMN BAS_TYPE.MODIFIER_ID IS '修改人ID:';
 COMMENT ON COLUMN BAS_TYPE.MODIFIER IS '修改人:';
 COMMENT ON COLUMN BAS_TYPE.LAST_UPDATED_TIME IS '最后更新时间:';
 COMMENT ON COLUMN BAS_TYPE.IS_ENABLED IS '是否有效:';
 COMMENT ON COLUMN BAS_TYPE.IS_SYSTEM IS '系统标志:';
 COMMENT ON COLUMN BAS_TYPE.ORG_ID IS '组织ID:表示数据所属的公司';
 COMMENT ON COLUMN BAS_TYPE.UPDATE_CONTROL_ID IS '并发控制ID:';
 COMMENT ON COLUMN BAS_TYPE.TFLAG IS '传输标志:0不上传，1未上传，2成功上传，3上传出错';

/*二、修改表：增加列*/
/*增加列*/
ALTER TABLE t_test_db_supplier ADD supply_addr varchar(500) NULL;
COMMENT ON COLUMN t_test_db_supplier.supply_addr IS '供应商地址';

/*修改列长度*/
ALTER TABLE t_test_db_supplier ALTER COLUMN supplier_full_name TYPE varchar(300) USING supplier_full_name::varchar(300);
/*设置列非空*/
ALTER TABLE t_test_db_supplier ALTER COLUMN supplier_full_name SET NOT NULL;
/*修改列默认值*/
ALTER TABLE t_test_db_supplier ALTER COLUMN supplier_full_name SET DEFAULT 2;

/*删除列*/
ALTER TABLE t_test_db_supplier DROP COLUMN sort_id;
/*重命名列*/
ALTER TABLE t_test_db_supplier RENAME COLUMN supply_addr TO supply_addr1;

/*设置列默认值*/
ALTER TABLE table_name ALTER COLUMN column_name SET DEFAULT 'new_value';
/*去掉列的默认值*/
ALTER TABLE table_name ALTER COLUMN column_name DROP DEFAULT;

/*设置列非空*/
ALTER TABLE table_name ALTER COLUMN column_name SET NOT NULL;
/*去掉列的非空约束*/
ALTER TABLE your_table_name ALTER COLUMN column_name DROP NOT NULL;


