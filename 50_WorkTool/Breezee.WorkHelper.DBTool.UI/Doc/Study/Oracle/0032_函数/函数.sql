-- PL/SQL -> Help -> Support Info，可查看TNS信息
-- 查询版本
SELECT version FROM v$instance;

/*
regexp_like(search_string ,pattern[,match_option])
参数说明：
search_string：是搜索值
pattern：正则表达式元字符构成的匹配模式,长度限制在512字节内
match_option：是一个文本串，允许用户设置该函数的匹配行为。可以使用的选项有：
c ：大小写敏感，默认值
i ：大小写不敏感
n ：允许使用原点（.）匹配任何新增字符
m ：允许将源字符作为多个字符串对待
*/
AND regexp_like(B.NAME_CN,#{NAME_CN,jdbcType=VARCHAR},'i')
AND instr((','||#{itemType,jdbcType=VARCHAR}||','),(','||B.ITEM_TYPE||','))>0


/*utl_raw.cast_to_raw 是 Oracle 数据库中的一个函数，用于将字符串转换为 RAW 数据类型。
RAW 数据类型用于存储二进制数据，如图像、声音文件或其他非文本二进制数据。*/
utl_raw.cast_to_raw('string_to_convert')

/*在Oracle数据库中，MD5加密是通过DBMS_OBFUSCATION_TOOLKIT包中的MD5函数实现的。该函数将输入字符串加密为16字节的RAW类型值，
通常需要使用UTL_RAW.CAST_TO_RAW函数将其转换为十六进制字符串以便显示和比较。*/
DECLARE
 v2 VARCHAR2(32);
BEGIN
 v2 := UTL_RAW.CAST_TO_RAW(SYS.DBMS_OBFUSCATION_TOOLKIT.MD5(INPUT_STRING => '123456'));
 DBMS_OUTPUT.PUT_LINE(v2);
END;

/*Oracle 存储过程中的 =>：作为形参对应，因为位置对应方法有缺限，比如一个函数有3个参数，但第2个是可以不传（有默认值),这里就没办法位置对应方法，
oralce内部一般用此种方法作参数传递。例如：*/
select dbms_obfuscation_toolkit.MD5(input_string => '11133') from dual;