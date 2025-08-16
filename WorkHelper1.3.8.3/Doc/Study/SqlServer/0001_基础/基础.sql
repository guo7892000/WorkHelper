/*如有启用分布式事务，则要把Distributed Transaction Coordinator服务启动*/
---操作过程：“运行”==>“net start msdtc”

/*由零件号前5码中数字与字母对照表（1－A， 2－B， 3－C， 4－D， 5－E， 6－F， 7－G， 8－H， 9－K， 0－M），
 查询以输入大于等于前五位备件号为开头的后模糊查询示例（这里只以两位为例）*/ 
 SELECT * FROM PA_PART_LIST 
 WHERE 1=1 --AND Charindex('5',PARTNO)=1
 AND (Charindex('E',PARTNO)=1 OR Charindex('5',PARTNO)=1)
 AND (Charindex('B',PARTNO)=2 OR Charindex('2',PARTNO)=2) AND PARTNO LIKE '%112A%';

