/*Mysql的分析函数
格式：	函数名() OVER(PARTITION BY 列名1 ORDER BY 列名2 DESC) AS 列别名1	
行号：	ROW_NUMBER() OVER(PARTITION BY VIN ORDER BY INVOICE_DATE, INVOICE_NO DESC) AS ORDER_NUM	
排序	rank()	
排序	dense_rank()	
总数	count()	
最大值	max()	
最小值	min()	
求和	sum()	
平均值	avg()	
第一个值	first_value()	
最后值	last_value()	
前N行	lag()	
后N行	lead()	
*/
select instr(A.NEW_CAR_TYPE_CODE,':') >0
FROM t_car_type
;


