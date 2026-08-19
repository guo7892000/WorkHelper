### Oracle兼容
#### dual的兼容
```
CREATE OR REPLACE VIEW dual AS
SELECT NULL::"unknown" WHERE 1 = 1;

-- 授予权限（确保所有用户都能访问）
GRANT SELECT ON TABLE dual TO public;

```
#### 返回游标的OPEN次数
Oracle：可以多次 OPEN：每次 OPEN 都会让游标变量指向一个新的结果集，之前打开的查询会被自动丢弃，无需先 CLOSE。典型场景：在存储过程中，根据输入参数的不同，用同一个 OUT SYS_REFCURSOR 打开不同的查询。
GaussDB（或openGauss）：openGauss 兼容了 Oracle 的 SYS_REFCURSOR 语法，行为一致。特别说明：openGauss 底层基于 PostgreSQL，其 REFCURSOR 是可以多次 OPEN 的。但由于 openGauss 在函数内通常使用 RETURN QUERY，
	显式 OPEN SYS_REFCURSOR 多用在出参场景，机制完全兼容。如出现报错，可只在最终返回OPEN一次。

#### 表迁移
1、表结构迁移（含索引、约束等）：直接使用旧Oracle的创建表语句，去掉空间相关，然后在DBeaver中执行即可。
2、数据迁移：Oralce查询，并导出为csv文件。注：时间戳列字段需要查询时转换为字符，再导出为csv文件。	
3、Dbeaver：导入数据。在DBeaver中导入时，表映射中映射规则中，名称大小写要选择【小写】	

#### Oracle > GaussDB代码层面的Mybatis配置
* GaussDB/PostgreSQL在预处理语句中，当ILIKE与CONCAT结合使用时，可能无法正确推断参数类型，特别是当参数可能为NULL或需要隐式类型转换时。											
b.user_name ILIKE CONCAT('%', ?, '%')					修改为	b.user_name ILIKE CONCAT('%', CAST(? AS VARCHAR), '%')					
					或	b.user_name ILIKE '%' || ? || '%'					
					或	POSITION(? IN b.user_name) > 0					注意，POSITION是大小写敏感的，而ILIKE是大小写不敏感的。

#### 在Dbeaver中修改GaussDB中的包
先要切换模式的默认值，不然会报包不存在错误。或者在包名前加上模式名，这样就算不在默认模式下也能访问。
有时增加了一个数据类型或其他修改，但执行包修改保存时（部分代码还是旧的），仍然报错，可以将修改的包体复制下来到记事本内，关掉原包再重新打开并粘贴，再执行就不报错了。	
有时声明无法增加，或以试着先备份好包声明和包体。然后删除原包，再新增同名包，再将原内容复制回来，执行保存即可！！	
备份包：右击包 => 生成SQL => DDL 

#### Oracle包转换为GaussDB中的包
```
1、在Dbeaver创建同名新包：包名会自动转为小写。																
	注：包名要增加模式前缀，这样防止在其他库执行创建新的包了！！															
2、类型定义放到包声明中。暂时没有时，可以将以下内容复制到包声明中，让保存时声明有内容，不然包声明有时会莫名地为空白了！！																
	Type map_type1 Is Table Of Varchar(4000);															
3、复制旧包中的包体到新包体中。																
5、尝试全部执行一次，一般会报错，并且因为存过或函数太多，不好找是哪个出错了。																
6、如果不能按提示很好地修正错误，那么将新包体复制出来，放到一个文本中																
7、逐步复制其中一个或多个函数或存储过程放到新包中，尝试执行。																
报错则按提示修改正确；不报错则再复制一个或多个函数或存储过程到新包中，如此反复操作，直到复制完全部包体内容。																
错误包含：																
	7.1 对象缺少模式前缀导找不到：增加模式前缀															
	7.2 引用包不存在或未加模式前缀：按本说明迁移旧包。最怕的就是当前对象引用了其他对象，而其他对象又引用了一大堆相关对象，这样重写前先得把依赖的对象迁移过来先！！															
	7.3 表名未迁移或缺少模式前缀：迁移旧表通过旧表的ViewSQL，去掉tablespace部分，然后在新模式下执行即可。															
	7.4 视图未迁移或缺少模式前缀：迁移旧视图。如旧视图引用其他视图，又得先重写其他视图！！															
	7.5 打开游标的传参，直接传入参数，不要使用=>															
		Open c_CncType(i_CncArea => i_CncArea);	  修改为	Open c_CncType(i_CncArea);							
	7.6 一些关键字作为查询字段别名时要加上双引号						"text"									
	7.7 Function do not support table of index Or record nested tabel of index as in, out args.															
		Type Table_Check_Type Is Table Of Rec_Check_Type Index By varchar(32);	 去掉	Index By varchar(32);	
	7.8 package name end is not match the one begin! at or near ";"															
		消灭了包内的 DECLARE...BEGIN...END 匿名块：这是导致 package name end is not match 的罪魁祸首。OpenGauss 的包解析器在处理这种结构时极易发生标签匹配错位。														
		将辅助函数提升为包级私有函数：get_unit_char、convert_group 和 convert_int_part 都作为包内的独立函数存在，END 标签严格对应，解析器绝对不会报错。														
	7.9  递归语法（Start with …. Connect By Prior ）需要修改								WITH RECURSIVE org_hierarchy AS (							
		Select n.* From dp_sys_t_organization n							SELECT n.*,1 AS level_num FROM public.t_eap_sys_organization_real n WHERE n.org_code = i_orgcode							
		  Start With n.org_code = i_orgcode 						改为	UNION ALL SELECT n.*,oh.level_num + 1 FROM public.t_eap_sys_organization_real n							
		Connect By Prior n.org_parentid = n.org_id							JOIN org_hierarchy oh ON n.parent_org_realation_id = oh.org_realation_id)							
									SELECT *,ROW_NUMBER() OVER (ORDER BY level_num DESC) AS dd FROM org_hierarchy ORDER BY dd DESC							
	7.10 分组拼接字符（listagg）修改为STRING_AGG															
		LISTAGG → STRING_AGG：														
		Oracle: LISTAGG(column, delimiter) WITHIN GROUP (ORDER BY ...)	
		OpenGauss: STRING_AGG(column, delimiter ORDER BY ...)														
	7.11 分隔字符转换表															
		--返回的列名为小写方法名：f_getstrconvertcol														
		select * from table(pkg_com_util.f_getstrconvertcol('1,2,3',','));	
		修改为：Select unnest as column_value From unnest(string_to_array('a,b,c', ','));
	7.12 中文逗号修改为英文逗号															
	7.13 转换为boolean类型															
		sys.diutil.int_to_bool(n => var_isExists)
		转换为 var_isExists::boolean，或直接使用var_isExists>=0来判断是否存在。					
	7.14 在Merge into... using...中不支持行变量的插入，要修改为实际的新增语句：	
		var_ItemSubject   mbom_t_itemsubject%Rowtype;									
		Insert Values var_ItemSubject
		修改为：
		Insert (FACTORY_ID, VEHICLE_CODE, ITEM_ID, SUBJECT_ID, CREATER, CREATE_TIME, MODIFYER, MODIFY_TIME)
          Values (src.FACTORY_ID, src.VEHICLE_CODE, src.ITEM_ID, src.SUBJECT_ID,src.CREATER, src.CREATE_TIME, src.MODIFYER, src.MODIFY_TIME)													
																
																
																
	7.99 其他略															
8、再复制旧包声明到新包声明中。																
9、迁移完成，后续还得测试是否正常。																
注意点：																
	执行包的数据库及模式要选正确，不然包创建到其他模式下了！！															
	修改前的Oracle旧对象，和修改后的GaussDB新对象都要做好备份，包括关联对象！！															
	有时遇到包声明为空了，需要将包体内容复制下来。然后删除原包，再创建新包，再将包体内容复制过去并保存。注：一定要保证复制时的内容是最新的，不然写好的内容不见就over了！															
	部分函数或存储过程转换，可借助AI更快															
	部分表不迁移，要将原来用到的地方注释掉，并说明一下，以后改为新表。															
			com_t_std_object、t, com_t_object_property，要将每种类型转换为实际的物理表													
	创建视图不要在Dbeaver上新建，那个执行完后，注释直接丢失了，应该直接使用原始脚本来创建，方便保存原始信息！！															
```

