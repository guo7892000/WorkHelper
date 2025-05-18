-- 创建一般索引
CREATE INDEX idx_t_pbom_db_material_material_name ON public.t_pbom_db_material (material_name);

-- 创建唯一索引
CREATE UNIQUE INDEX uk_t_pbom_db_material_material_code ON public.t_pbom_db_material (material_code);

--删除索引（包含一般索引和唯一索引）
DROP INDEX public.t_pbom_db_material_material_name_idx;


-- 增加主键：在创建表时直接定义主键
CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    department VARCHAR(100)
);
-- 增加主键：创建表后再使用单独的 PRIMARY KEY 约束
CREATE TABLE employees (
    id INT,
    name VARCHAR(100),
    department VARCHAR(100)
);
ALTER TABLE employees ADD PRIMARY KEY (id);
-- 增加主键：使用复合主键
CREATE TABLE employee_department (
    employee_id INT,
    department_id INT,
    PRIMARY KEY (employee_id, department_id)
);

-- 删除主键
ALTER TABLE your_table_name DROP CONSTRAINT constraint_name;
