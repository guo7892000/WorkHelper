## KES Studio 或 KSQL Developer
KES Studio是一款功能强大的数据库开发和管理工具，可为数据库开发人员、DBA提供数据库开发、调试、维护等各项功能，完美支持金仓数据库。
### 创建包
使用KingbaseES数据库的官方免费连接工具来创建，步骤：  
1、连接数据库  
2、打开对应模式下的【程序包】，右击选择【新建 程序包】  
3、输入包名，以dbms_output为例  
4、录入包内容，有两种方式：  
***方法1： 可视化录入内容***
分别录入包头、包体，然后点击确定即可。    
```		
/*包头:日志输出*/
  Procedure put_line(i_remark varchar);

END				
				
/*包体日志输出*/
  Procedure put_line(i_remark varchar)
  is
  begin
          raise notice '%',i_remark;
  end;    
BEGIN
    NULL;
END
```

***方法2： 直接编辑内容***  
录入包名后，先点确定。  
右击该包，然后【生成SQL】> 【DDL】>【打开编辑器】  
直接在里边修改内容，然后点击【执行SQL脚本】即可  
```
CREATE OR REPLACE PACKAGE "fawbom"."dbms_output" AUTHID CURRENT_USER AS      /*日志输出*/
  Procedure put_line(i_remark varchar);

END;

CREATE OR REPLACE PACKAGE BODY "fawbom"."dbms_output" AS    
  /*日志输出*/
  Procedure put_line(i_remark varchar)
  is
  begin
          raise notice '%',i_remark;
  end;    
BEGIN
    NULL;
END
;;
```
### 调试
```
BEGIN 
	"public"."dbms_output"."put_line"('22');
END	
								
DECLARE n_cnt number(6):=66; 
BEGIN 
	public.dbms_output.put_line(n_cnt);
END				
```			
				
				
				

