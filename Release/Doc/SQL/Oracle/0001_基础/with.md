## WITH
在Oracle SQL中，WITH语句，通常称为公共表表达式（Common Table Expressions，CTE），是一种临时结果集的创建方式，可以在查询中多次引用。  
这对于简化复杂的SQL查询特别有用，因为它允许将查询分成更小的部分，使得代码更易于理解和维护。
```
WITH cte_name AS (
    SELECT column1, column2
    FROM table
    WHERE condition
)
SELECT *
FROM cte_name;
```
### 查询
```
WITH DeptMaxSalaries AS (
    SELECT department_id, MAX(salary) AS max_salary
    FROM employees
    GROUP BY department_id
),
TopEarners AS (
    SELECT e.*
    FROM employees e
    JOIN DeptMaxSalaries dms ON e.department_id = dms.department_id AND e.salary = dms.max_salary
)
SELECT *
FROM TopEarners;
```
### 新增
```
INSERT INTO TEST_TABLE(ID,CNAME)
with TMP_A AS(select #SORT_ID# as id,'#TFLAG#' as name FROM DUAL)
select * from TMP_A
```