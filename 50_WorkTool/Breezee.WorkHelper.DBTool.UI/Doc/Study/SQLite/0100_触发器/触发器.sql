--SQLite触发器的语法如下：
CREATE TRIGGER trigger_name [BEFORE/AFTER] trigger_type ON table_name
[FOR EACH ROW] [WHEN condition]
BEGIN
 --触发器操作
END;

/*其中，各项参数的含义如下：
trigger_name：触发器名称，符合标志符的命名规范，不能和现有关键字重复。
trigger_type：触发器类型，可以是INSERT，UPDATE或者DELETE。
table_name：触发器关联的表名。
FOR EACH ROW：触发器作用于每一行数据，即触发器会对每一行进行操作。
WHEN condition：触发要执行的操作的条件。
BEGIN/END：触发器内的操作用 BEGIN 和 END 来指定范围。

触发器的类型
在设定触发器时，需要选择触发器类型，常见的触发器类型包括：
INSERT：在一个INSERT语句执行前/后触发。
UPDATE：在一个UPDATE语句执行前/后触发。
DELETE：在一个DELETE语句执行前/后触发。
INSTEAD OF：类似于视图，用于定义一个视图的行为。
*/

create trigger student_log after delete
on student
begin
	--这里的old表示操作之前， 也可以为new，表示操作之后
	insert into delete_log values(old.ID, datetime('now')); -- 删除之后就往表delete_log插入一条数据
end;


DROP TRIGGER trigger_name;