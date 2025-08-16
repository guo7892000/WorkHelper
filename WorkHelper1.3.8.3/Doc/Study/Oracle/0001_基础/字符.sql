/*查询子字符和赋值应用*/
declare v varchar2(20);
begin
 v:='可d要地一杀敌';
 dbms_output.put_line(substr(v, 2, 3));
end;

/*字符连接*/
select concat('中国','人民') "test",'中国'||'人民' from dual;

/*INITCAP(n)函数
 将字符串n中每个单词首字母大写，其余小写(区分单词的规则是按空格或非字母字符；可以输入中文字符，但没有任何作用)。例如：*/
select initcap('中 国 人 民') "test",initcap('my word') "test1",initcap('my中国word') "test2" from dual;

/*INSTR(chr1,chr2,[n,[m]])函数
获取字符串chr2在字符串chr1中出现的位置。n和m可选,省略是默认为1；
n代表开始查找的起始位置，当n为负数从尾部开始搜索；m代表字串出现的次数。例如：*/
select instr('pplkoopijk','k',-1,1) "test",instr('pplkoopijk','k',1,2) from dual;

/*LENGTH(n)函数
 返回字符或字符串长度。(当n为null时，返回nll；返回的长度包括后面的空格)。例如：*/
select length('ppl ') "test",length(null) "test1",length(trim('ppl ')) "test" from dual;

/*LOWER(n)函数
 将n转换为小写;UPPER(n)函数的描述: 将n转换为大写。例如：*/
select lower('KKKD') "test",UPPER('hello') from dual;

/*LPAD(chr1,n,[chr2])函数
在chr1左边填充字符chr2，使得字符总长度为n。chr2可选，默认为空格；当chr1字符串长度大于n时，则从左边截取chr1的n个字符显示。
RPAD(chr1,n,chr2)函数的描述：在chr1右边填充chr2，使返回字符串长度为n..当chr1长度大于n时，返回左端n个字符。例如：*/
select lpad('kkk',5) "test",lpad('kkkkk',4) "test1",lpad('kkk',6,'lll') "test2" from dual;

/*LTRIM(chr,[n])函数
去掉字符串chr左边包含的n字符串中的任何字符，直到出现一个不包含在n中的字符为止。例如：*/
select ltrim('abcde','a') "test",ltrim('abcde','b') "test1",ltrim('abcdefg','cba') "test2",ltrim('  abcdefg') from dual;

/*REPLACE(chr,search_string,[,replacement_string])函数：
将chr中满足search_string条件的替换为replacement_string指定的字符串，当search_string为null时，返回chr；
当replacement_string为null时，返回chr中截取掉search_string部分的字符串。例如：*/
SELECT REPLACE('abcdeef','e','oo') "test",REPLACE('abcdeef','ee','oo') "test1",REPLACE('abcdeef',NULL,'oo') "test2",REPLACE('abcdeef','ee',NULL) "test3" FROM dual;

/*SQL中的SQL，把单引号换成两个单引号就好了*/
SELECT 'INSERT INTO (a, b,c,d) VALUES(''aa'', 2, '''',3)' from dual;