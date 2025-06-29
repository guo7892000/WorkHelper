/*UUID和连接符*/
SELECT gen_random_uuid() as UUID,'A'||'B' AS "连接字符"
;

/*使用递归查询来实现层次结构数据的查询，比如组织结构、分类目录等。递归查询通常与WITH RECURSIVE子句一起使用。
可以使用多个递归。*/
WITH RECURSIVE subordinates AS (
    -- 初始化查询，选择直接下属
    SELECT id, name, manager_id
    FROM employees
    WHERE manager_id = 1  -- 从特定员工开始，例如员工ID为1
    UNION ALL
    -- 递归部分，选择所有间接下属
    SELECT e.id, e.name, e.manager_id
    FROM employees e
    INNER JOIN subordinates s ON e.manager_id = s.id
),subordinatesB AS (
    -- 初始化查询，选择直接下属
    SELECT id, name, manager_id
    FROM employees
    WHERE manager_id = 2  -- 从特定员工开始，例如员工ID为2
    UNION ALL
    -- 递归部分，选择所有间接下属
    SELECT e.id, e.name, e.manager_id
    FROM employees e
    INNER JOIN subordinatesB s ON e.manager_id = s.id
)
SELECT * FROM subordinates
;

/*字段组合*/
SELECT get_no, STRING_AGG(category_name , ',') AS names
FROM t_ebom_db_material
GROUP BY get_no;

/*时间范围查询*/
SELECT * FROM subordinates
where 1=1
and a.created_date > to_date('#created_date_beg#', 'yyyy-mm-dd')
and a.created_date < to_date('#created_date_end#', 'yyyy-mm-dd') + interval '1 day'
;

/*当字段为空时取默认值*/
select COALESCE(is_tax_machine, '0')
from t_mathine
;

/*类型转换*/
select (A.qty*A.price)::numeric(14,4) AS total_amount
from t_out_store_d A
;


