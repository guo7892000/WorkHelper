/*
SQLite 是一个软件库，实现了自给自足的、无服务器的、零配置的、事务性的 SQL 数据库引擎。SQLite 是在世界上最广泛部署的 SQL 数据库引擎。
支持：表、视图、触发器、索引。
不支持：函数、存储过程。
*/

/*1、结构变更*/
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



