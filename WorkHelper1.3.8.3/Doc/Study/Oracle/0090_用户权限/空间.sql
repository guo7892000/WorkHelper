/*oracle 10g 创建和删出表空间，用户，表*/
--第一步：创建表空间（要建两个临时表空间和数据表空间） 
--1、创建临时表空间： 
--保证目录（E:/oracle/product/10.2.0/oradata/hui）存在
create temporary tablespace hui_temp 
tempfile 'E:/oracle/product/10.2.0/oradata/hui/hui_temp01.dbf' 
size 100m 
autoextend on 
next 100m maxsize 500m 
extent management local; 

---
--2、创建数据表空间： 
CREATE TABLESPACE test 
DATAFILE 'E:/oracle/product/10.2.0/oradata/hui/hui_data01.DBF' 
SIZE 1000M AUTOEXTEND ON NEXT 100M MAXSIZE UNLIMITED LOGGING PERMANENT EXTENT 
MANAGEMENT LOCAL AUTOALLOCATE BLOCKSIZE 8K SEGMENT SPACE MANAGEMENT MANUAL FLASHBACK ON; 
---
--第二步：创建用户 
create user hui identified by hui 
default tablespace test 
temporary tablespace hui_temp; 
--其中test为用户名，testpassword为密码 
--最后：给用户授予权限 
grant connect,resource to hui; 

--建表就和其它数据库没有什么区别了，普通sql 
--如： 
create table test_user( 
first_name varchar2(15), 
last_name varchar2(20) 
); 

--删除用户：test 
drop user test cascade 
--删除表空间： 
DROP TABLESPACE hui_temp INCLUDING CONTENTS AND DATAFILES; 
--删除表数据: 
delete from users;