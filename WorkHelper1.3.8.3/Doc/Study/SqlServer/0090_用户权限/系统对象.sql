/*SQL表对象*/
sys.objects、syscolumns、systypes、sys.default_constraints、sys.all_sql_modules
;

/*查询表信息*/
SELECT b.name OWER,a.name Table_Name,a.* 
FROM sys.objects a 
JOIN sys.schemas b ON a.schema_id=b.schema_id WHERE a.type='U' AND a.name='FI_MATAINGATHER_M'
;

/*查询列*/
Select * From sysobjects a,syscolumns b 
Where a.id = b.id and b.name = 'LOCATIONCODE' And a.type = 'U';

/*配置启用CLR语句，即可以使用VS 2005创建的存储过程*/
EXEC sp_configure 'clr enabled' , 1 
RECONFIGURE WITH override
;

/*SQL Server收缩日志及数据库文件代码*/
--收缩数据库文件： 
DBCC SHRINKDATABASE(@dbName ) 
DUMP TRANSACTION @dbName WITH NO_LOG;
--截断事务日志： 
BACKUP LOG @dbName WITH NO_LOG;

/*收缩日志文件*/ 
DBCC SHRINKFILE 
( 
{ 'file_name'  file_id } 
{ [ , EMPTYFILE ] 
 [ [ , target_size ] [ , { NOTRUNCATE  TRUNCATEONLY } ] ] 
} 
) 
[ WITH NO_INFOMSGS ] 

