/*指定字段指定位置为固定值的SQL*/
SELECT * FROM dbo.PA_PART_LIST WHERE Charindex('A',PARTNO)=2 OR Charindex('1',PARTNO)=2;

/*获取按指定列排序后，取出指定位置行记录*/
SELECT * FROM (
select ROW_NUMBER() OVER(ORDER BY createtime) rowNum,*
 FROM UC_T_CUST_TYPE
  WHERE CHECKSTATE = '2'
 AND ISNULL(STORE_STATUS, 0) = '0'
  AND ISENABLE = '1') a
where rowNum BETWEEN 1 AND 10

/*Union和Union all 的区别：
在数据库中，union和union all关键字都是将两个结果集合并为一个，但这两者从使用和效率上来说都有所不同。
union在进行表链接后会筛选掉重复的记录，所以在表链接后会对所产生的结果集进行排序运算，删除重复的记录再返回结果。
而union all只是简单的将两个结果合并后就返回。从效率上说，union all要比union快很多，所以，如果可以确认合并的两个结果集中不包含重复的数据的话，那么就使用union all。
使用union组合查询的结果集有两个最基本的规则：
1。所有查询中的列数和列的顺序必须相同。
2。数据类型必须兼容
*/

/*同一服务器的跨库查询*/
SELECT kcsx,stockupperlimit,kcxx,stocklowerlimit
from zyddb..j_bjml a,dms.dbo.pa_part_list b
where b.partno=a.bjbh;

/*ROW_NUMBER的使用*/
WITH TABS AS  
(  
	SELECT ROW_NUMBER() OVER(PARTITION BY ORG_ID  ORDER BY EMP_ID  ) AS ROWNO,ORG_ID,EMP_ID FROM ORG_EMPLOYEE  
 )  
SELECT MAX(ROWS) AS '员工个数',ORG_ID FROM TABS GROUP BY ORG_ID;

