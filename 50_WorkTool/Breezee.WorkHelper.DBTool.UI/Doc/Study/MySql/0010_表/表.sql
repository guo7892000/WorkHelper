/*一、创建表*/
/*创建表1*/
CREATE TABLE aprilspring.TEST1 (
	SEQ_ID BIGINT auto_increment NULL COMMENT '自增长主键',
	TEST_ID varchar(36) DEFAULT uuid() NULL COMMENT 'UUID主键',
	TEST_CODE varchar(20) NULL COMMENT '编码',
	TEST_NAME varchar(100) NULL COMMENT '名称',
	Date1 DATE DEFAULT now() NULL COMMENT '日期1',
	Char1 CHAR(5) NULL,
	DATETIME1 DATETIME DEFAULT now() NULL,
	DECIMAL1 DECIMAL(10,4) NULL,
	`INT1` INT NULL,
	`NUMERIC` NUMERIC(12,2) NULL,
	TEXT1 TEXT NULL,
	CONSTRAINT TEST1_pk PRIMARY KEY (SEQ_ID)
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8
COLLATE=utf8_general_ci;

/*创建表2:自增初始值1000*/
CREATE TABLE test.TEST1 (
	SEQ BIGINT AUTO_INCREMENT NOT NULL,
	Column1 varchar(100) NULL,
	CONSTRAINT TEST1_pk PRIMARY KEY (SEQ)
)
AUTO_INCREMENT=1000
ENGINE=InnoDB
DEFAULT CHARSET=latin1
COLLATE=latin1_swedish_ci;
/*测试插入数据*/
INSERT into TEST1(Column1) values('111');
INSERT into TEST1(Column1) values('2222');

/*二、修改表*/
/*修改表名:RENAME TABLE 【旧表名】 TO 【新表名】;*/
RENAME TABLE aprilspring.bas_type2 TO aprilspring.bas_type3;

/*增加列：ALTER TABLE 【库名.表名】 ADD 【列名】 【数据类型(长度)】 DEFAULT 【默认值】 not null COMMENT 【注释】;*/
ALTER TABLE aprilspring.bas_type2 ADD IN_QTY DECIMAL(14,2) DEFAULT 0 NOT NULL COMMENT '入库数量';

/*修改列类型:ALTER TABLE 【表名】 MODIFY COLUMN 【列名】 【数据类型(长度)】 DEFAULT 【默认值】 null COMMENT 【注释】;*/
ALTER TABLE aprilspring.bas_type2 MODIFY COLUMN EXTEND_VALUE5 varchar(60) DEFAULT '1' NULL COMMENT '扩展值5';
ALTER TABLE aprilspring.bas_type2 MODIFY COLUMN EXTEND_VALUE5 varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '1' NULL COMMENT '扩展值5';

/*删除列:ALTER TABLE 【表名】 DROP COLUMN 【列名】*/;
ALTER TABLE aprilspring.bas_type2 DROP COLUMN IN_QTY;

/*修改列名:ALTER TABLE 表名 CHANGE 旧列名 新列名 数据类型;*/
ALTER TABLE aprilspring.bas_type2 CHANGE CREATOR_ID CREATOR_ID2 varchar(36) NOT NULL COMMENT '创建人ID';
ALTER TABLE aprilspring.bas_type2 CHANGE CREATOR_ID CREATOR_ID2 varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人ID';




