/*查询存储过程内容*/
select * from dba_source a where a.name='SP_MDM_PRO_SEND_TOUCCUST';

/*查看oracle数据文件存放路径*/
select * from v$datafile;

/*查询版本*/
select * from v$version

/*备份表语句*/
create table userinfo_bak as select * from userinfo;

/*查询变更的对象*/
SELECT A.OWNER, A.OBJECT_NAME
  FROM ALL_OBJECTS A
 WHERE A.OBJECT_TYPE IN ('PROCEDURE', 'TRIGGER', 'VIEW')
   AND A.OWNER IN ('UC')
   AND A.LAST_DDL_TIME > SYSDATE - 2
   AND A.STATUS = 'VALID'
UNION
SELECT A.OWNER, A.OBJECT_NAME
  FROM ALL_OBJECTS A
 WHERE A.OBJECT_TYPE IN ('PROCEDURE', 'TRIGGER', 'VIEW')
   AND A.OWNER IN ('IFR', 'IFS')
   AND A.LAST_DDL_TIME > SYSDATE - 2
   AND A.STATUS = 'VALID';

/*查询被锁住的表*/
SELECT /*+ rule */ lpad(' ',decode(l.xidusn ,0,3,0))||l.oracle_username User_name, 
o.owner,o.object_name,o.object_type,s.sid,s.serial# 
FROM v$locked_object l,dba_objects o,v$session s 
WHERE l.object_id=o.object_id 
AND l.session_id=s.sid 
ORDER BY o.object_id,xidusn DESC;


/*Oracle查询all_tab_columns.data_default (类型为LONG)：不能使用LONG。Oracle仍然在数据字典中使用它们，这是一个缺陷。您可以使用XML：*/
select owner
     , table_name
     , column_name
     , data_type
     , data_length
     , case
           when data_precision is null then 0
           else data_precision
       end data_precision
     , case
           when data_scale is null then 0
           else data_scale
       end data_scale
     , nullable
     , column_id
     , default_length
     , case
           when default_length is null then '0'
           else
               extractvalue
               ( dbms_xmlgen.getxmltype
                 ( 'select data_default from user_tab_columns where table_name = ''' || c.table_name || ''' and column_name = ''' || c.column_name || '''' )
               , '//text()' )
       end as data_default
from   all_tab_columns c
where  table_name like 'TABLE1';

/*从12.1开始，您可以编写自己的内联查找函数：*/
with
     function get_default(tab varchar2, col varchar2) return varchar2
     as
         dflt varchar2(4000);
     begin
         select c.data_default into dflt
         from   user_tab_columns c
         where  c.table_name = upper(tab)
         and    c.column_name = upper(col);
         
         return dflt;
     end get_default;
select owner
     , table_name
     , column_name
     , data_type
     , data_length
     , case
           when data_precision is null then 0
           else data_precision
       end data_precision
     , case
           when data_scale is null then 0
           else data_scale
       end data_scale
     , nullable
     , column_id
     , default_length
     , get_default(c.table_name, c.column_name) as data_default
from   all_tab_columns c
where  table_name like 'TABLE1%'
/
