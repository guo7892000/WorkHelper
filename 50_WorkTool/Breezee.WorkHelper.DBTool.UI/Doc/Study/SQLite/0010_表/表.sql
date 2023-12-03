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

