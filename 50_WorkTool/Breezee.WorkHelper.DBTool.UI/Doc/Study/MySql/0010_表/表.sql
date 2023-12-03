/*增加列*/
ALTER TABLE 表名 ADD 列名 数据类型;	
/*修改列类型*/
ALTER TABLE 表名 MODIFY 列名 新数据类型;
/*修改表名*/
ALTER TABLE 旧表名 RENAME TO 新表名;		
/*删除列*/
ALTER TABLE 表名 DROP COLUMN 列名;	
/*修改列名*/
ALTER TABLE 表名 CHANGE 旧列名 新列名 数据类型;
/*修改表存储引擎*/
ALTER TABLE 表名 ENGINE = 新存储引擎;	

