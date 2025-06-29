-- 更新语句
update t_ebom_db_material_type
set cost_attr=B."C",
cost_attr_name=B."D"
from (
SELECT  '1'  AS "ROWNO", 'TJGC'  AS "A", '通机国产'  AS "B", 'TJGC'  AS "C", '通机国产'  AS "D"
UNION ALL SELECT  '2' , 'TJJP' , '通机进口' , 'TJJP' , '通机进口' 
UNION ALL SELECT  '3' , 'JKRX' , '进口日系' , 'TJJPB' , '通机进口(保税)' 
) B
join t_ebom_db_material_type PR /**/
 on PR.material_type_no = B."A"
where t_ebom_db_material_type.level_num ='2' 
 and t_ebom_db_material_type.parent_material_type_id = PR.material_type_id
;

--方法1：使用子查询
UPDATE employees
SET salary = (SELECT AVG(salary) FROM employees) * 1.1
WHERE department = 'Sales'
;

-- 使用UPDATE ... FROM结构（PostgreSQL 11及以上版本）
UPDATE employees
SET department_name = d.department_name
FROM departments d
WHERE employees.department_id = d.department_id
;

-- 方法3：使用临时表或CTE（公用表表达式）
WITH updated_dept AS (
    SELECT e.employee_id, d.department_name
    FROM employees e
    JOIN departments d ON e.department_id = d.department_id
)
UPDATE employees
SET department_name = ud.department_name
FROM updated_dept ud
WHERE employees.employee_id = ud.employee_id
;

-- 方法4：使用PL/pgSQL函数（如果需要更复杂的逻辑）
/*
DO $$
BEGIN
    UPDATE employees
    SET department_name = d.department_name
    FROM departments d
    WHERE employees.department_id = d.department_id;
END $$;
*/

/*﻿PostgreSQL 15 提供了 MERGE 语句，它可以基于源表或者查询结果更新目标表中的数据。MERGE 可以在单个语句中实现 INSERT、UPDATE 以及 DELETE 操作。
PostgreSQL 17 进一步增强了该语句的功能，包括：
支持 RETURNING 子句，可以返回新增、更新或者删除的数据行；
支持 WHEN NOT MATCHED BY SOURCE 操作，用于操作源表中不存在但是目标表中存在的数据行。
CREATE TABLE test (
    id    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    tag   VARCHAR(10) NOT NULL UNIQUE,
    posts INT NOT NULL DEFAULT 0
);
*/

MERGE INTO test t
USING (VALUES ('sql'),('pg17')) AS s(tag)
ON t.tag = s.tag
WHEN MATCHED THEN
    UPDATE SET posts = posts + 1
WHEN NOT MATCHED THEN
    INSERT (tag, posts) VALUES (s.tag, 1)
RETURNING t.*, merge_action();

-- 源表只提供了 1 条记录，目标表存在 2 条记录，我们删除了目标表中多出的一条数据（ tag = 'sql'）
MERGE INTO test t
USING (VALUES ('pg17')) AS s(tag)
ON t.tag = s.tag
WHEN MATCHED THEN
    UPDATE SET posts = posts + 1
WHEN NOT MATCHED THEN
    INSERT (tag, posts) VALUES (s.tag, 1)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;



/*在 PostgreSQL 中，INSERT ... ON CONFLICT 语句是用来处理插入数据时遇到唯一约束（unique constraint）冲突的情况。这种语句在 SQL 标准中被称作“Upsert”（update + insert）操作，
允许你在插入数据时如果发现违反唯一性约束，可以选择更新某些字段的值而不是简单地报错。*/    
INSERT INTO employees (id, name)
VALUES (1, 'John Doe')
ON CONFLICT (id)
DO UPDATE SET name = EXCLUDED.name;




