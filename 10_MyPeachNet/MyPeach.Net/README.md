# MyPeach.Net Dynamic SQL Parser Tool for ASP.NET

## 概述
MyPeach.Net是在.Net生态下的一个动态SQ转换工具，它能根据SQL中配置的键（默认格式：#键名#）和 键值集合（Map<String, Object>）来生成动态的SQL。  
即某键如有值传入，那么会将该条件保留，并将其参数化或字符替换（可配置选择）；否则将该条件抛弃或修改为 AND 1=1（一些用括号括起来的多个条件分析时）。
可动态的部分包括：所有类型的条件，INSERT项，UPDATE项。

## 软件架构
.NET 6.0 + C# + VS 2022

## 特点
* 数据库无关性
* 支持的语句样式：
```
    INSER INTO ...VALUES... 
    INSERT INTO...SELECT...FROM...WHERE... 
    INSERT INTO...WITH...SELECT...FROM...WHERE... 
    WITH...INSERT INTO... SELECT...FROM...WHERE... 
    UPDATE ... SET...FROM...WHERE...  
    DELETE FROM...WHERE...  
    SEELCT...FROM...WHERE...GROUP BY...HAVING...ORDER BY...LIMIT...  
    WITH...AS (),WITH...AS () SELECT...FROM...WHERE...
    SELECT...UNION ALL SELECT... 
```
* SQL语句键可带内置的校验规则描述，让SQL更安全  
  条件使用：键字符支持'#MDLIST:N:R:LS:F#'格式，其中N或M表示非空，R表示值替换，LS表示字符列表，LI为整型列表，即IN括号里的部分字符；F表示优先使用的配置。  
* 只生成SQL，不执行。如需使用生成后的参数化SQL，需要从ParserResult中取出参数化列表，即mapQuery属性值，其为Map<string, SqlKeyValueEntity>类型。  
* 生成SQL类型可选参数化、还是字符替换；对于字符串值中的单引号，会被剔除掉，然后再在值前后分别加上单引号。
## 背景
本项目是参考本人的另一个开源项目【MyPeach Dynamic SQL Parser Tool for Java】编写的，只是本项目是运行在微软的ASP.NET环境下。

## 实现思路
具体处理思路：
* 1.传入已经键化（#键配置#）的SQL和键值集合（IDictionary<String, Object>）
* 2.预处理：去掉SQL前后空格。注：这里不要转换为大写，因为存在一些值是区分大小的，全部转换为大写会导致SQL条件错误！
* 3.剔除备注信息：以--开头或符合/**/的注释
* 4.取出SQL中的键集合和有效键集合：如果有非空键传入，但没有对应值传入，中断转换，返回不成功等信息
* 5.剔除备注信息：以#开头的注释（注：会优先将#键配置#的替换为其他字符后，才匹配） 
* 6.对于符合()正则式，循环替换为：##序号##，这样就方便从大的方面掌控SQL语句段，进行准确匹配分析
* 7.调用子类的头部处理方法：边拆边处理  
* 8.子类调用父类的FROM处理方法
  * 8.1 存在FROM：  
    * 8.1.1 处理FROM  
    * 8.1.2 处理WHERE，更新WHERE处理标志为true  
  * 8.2 如果WHERE处理标志为false，那么处理WHERE  
* 9. 最后返回ParserResult对象，转换SQL结束。其中返回的结果对象中，sql为转换为参数化后的SQL，mapObject为有效参数值集合，mapQuery为有效详细参数实体信息，mapError为转换过程的错误信息，
*    positionCondition为位置参数值，code为返回参数（0成功，1失败），message为成功或失败信息，sourceSql为转换前的SQL。
* 10. 一些关键逻辑描述：  
   对于每个SQL段，都会按AND或OR进行分拆，保证每次处理的键段SQL只包括一个参数，这样就方便整段SQL的处理：修改还是删除。但分拆出来的SQL段， 
   首先还是先做是否有##序号##，有则需要对其内部先分析：先做子查询分析，如果是子查询，则要调用一次SELECT语句的头部分析（即把它也当作一个完整的SELCT语句来转换）；
   如不是子查询，那么又要对它进行AND或OR进行分拆，这时又要调用复杂的左右括号处理方法（左括号开头和右括号结尾的处理）。最后再调用单个键的转换方法（参数化还是字符替换）。
   每一次SQL段处理时，一般会去掉前后的空格。此处的逻辑不好描述，详细请见代码中的注释！

## 使用教程
* 1. 下载源码后，打开解决方案【MyPeachNet.sln】，这时会有两个项目：
     MyPeachNet：核心类库
     MyPeachNetTest：测试工程
* 2. 右击【MyPeachNet】生成，然后到生成目录找到：
     \bin\Debug\net6.0\org.breezee.MyPeachNet.dll
* 3. 在你的项目中引入该依赖即可。  
* 4. 修改配置(\Config\MyPeachNetProperties.cs)：一般我们使用默认值即可。
* 5. 写代码：  
     条件使用：键字符支持'#MDLIST:N:LS:R#'格式，其中N或M表示非空，LS表示字符列表，LI为整型列表，即IN括号里的部分字符。
* 5.1 以下为代码示例：
```
string sPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sql\";
string sSelect = File.ReadAllText(Path.Combine(sPath, "01_Select.txt"));

MyPeachNetProperties pop = new MyPeachNetProperties();
pop.TargetSqlParamTypeEnum = TargetSqlParamTypeEnum.DIRECT_RUN;
SqlParsers sqlParsers = new SqlParsers(pop);

IDictionary<String, Object> dicQuery = new Dictionary<String, Object>();
dicQuery["PROVINCE_ID"]= "张三";
dicQuery["PROVINCE_CODE"] ="BJ";
dicQuery["PROVINCE_NAME"] ="北京";
dicQuery["DATE"] = "20222-02-10";
dicQuery["NAME"] = 1;
dicQuery["REMARK"] = "测试";
dicQuery["BF"] = "back";
dicQuery["#TFLAG#"] = "tflagValue";
//dicQuery["MDLIST"] = new string[] { "SE", "PA", "FI" };//传入一个数组
//List<String> list = new List<String>();
//list.AddRange(new string[] { "SE", "PA", "FI" });

List<int> list = new List<int>();
list.AddRange(new int[] { 2, 3, 4 });
dicQuery["MDLIST"] = list;//传入一个数组

showMsg(sqlParsers.parse(SqlTypeEnum.SELECT, sSelect, dicQuery));

static void showMsg(ParserResult result)
{
    if (result == null) return;
    Console.WriteLine(result.Code.Equals("1") ? result.Message : result.Sql);
}
````

## 问题和BUG提交
提交BUG需提供的内容：
* 1.已经键化的SQL
* 2.键值条件集合（Dictionary<String, Object>）的内容
* 3.问题简述  
  [邮件反馈建议或问题](guo7892000@126.com)
  [微信号] BreezeeHui