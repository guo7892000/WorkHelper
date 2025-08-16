/*
SQLite 是一个软件库，实现了自给自足的、无服务器的、零配置的、事务性的 SQL 数据库引擎。SQLite 是在世界上最广泛部署的 SQL 数据库引擎。
支持：表、视图、触发器、索引。
不支持：函数、存储过程。
*/

/*查询所有表*/
SELECT * FROM sqlite_master where type='table' order by name

