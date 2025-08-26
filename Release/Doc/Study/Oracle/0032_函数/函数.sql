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
