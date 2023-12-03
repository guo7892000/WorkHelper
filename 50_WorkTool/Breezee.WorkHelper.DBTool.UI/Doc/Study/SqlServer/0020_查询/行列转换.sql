/*行列转换*/
--PIVOT 提供的语法比一系列复杂的 SELECT...CASE 语句中所指定的语法更简单、更具可读性。
--常见的可能会用到 PIVOT 的情形是，需要生成交叉表格报表以汇总数据时。
--A、行转列示例(需要筛选行数据中某列值作为具体列，其他列的值根据该值作汇总等)：
USE AdventureWorks;
GO
SELECT VendorID, [164] AS Emp1, [198] AS Emp2, [223] AS Emp3, [231] AS Emp4, [233] AS Emp5
FROM 
(SELECT PurchaseOrderID, EmployeeID, VendorID
FROM PurchaseOrderHeader) p
PIVOT
(
COUNT (PurchaseOrderID)
FOR EmployeeID IN
( [164], [198], [223], [231], [233] )
) AS pvt
ORDER BY VendorID
;

---B、列转行
--UNPIVOT 将与 PIVOT 执行几乎完全相反的操作，将列转换为行。
CREATE TABLE pvt (VendorID int, Emp1 int, Emp2 int,
Emp3 int, Emp4 int, Emp5 int)
GO
INSERT INTO pvt VALUES (1,4,3,5,4,4)
INSERT INTO pvt VALUES (2,4,1,5,5,5)
INSERT INTO pvt VALUES (3,4,3,5,4,4)
INSERT INTO pvt VALUES (4,4,2,5,5,4)
INSERT INTO pvt VALUES (5,5,1,5,5,5)
GO
--Unpivot the table.
SELECT VendorID, Employee, Orders
FROM 
   (SELECT VendorID, Emp1, Emp2, Emp3, Emp4, Emp5
   FROM pvt) p
UNPIVOT
   (Orders FOR Employee IN 
      (Emp1, Emp2, Emp3, Emp4, Emp5)
)AS unpvt
GO

/*只算维修出库的备件满足率情况*/
SELECT CAL_MONTH,CAST(SUM(REAL_PZ)/SUM(TOTAL_PZ)*100  AS DECIMAL(14,2)) PZ_RATE,
CASE WHEN SUM(PARTQTY)=0 THEN 0 else CAST((SUM(PARTQTY)-SUM(ISNULL(LP_QTY,0)))*100/SUM(PARTQTY) AS DECIMAL(14,2)) end QTY_RATE,
CASE WHEN SUM(PARTQTY*PRICE)=0 THEN 0 else CAST((SUM(PARTQTY*PRICE)-SUM(ISNULL(LP_QTY,0)*PRICE))*100/SUM(PARTQTY*PRICE) AS DECIMAL(14,2)) end MONEY_RATE
INTO #T_REPAIR_LP
FROM (
	SELECT MONTH(A.CREATETIME) CAL_MONTH,A.PARTNO,CASE WHEN ISNULL(B.QTY,0)=0 THEN 1 
		WHEN A.PARTQTY=B.QTY THEN 0 ELSE 0.5 END REAL_PZ,
		1 TOTAL_PZ,A.PARTQTY,B.QTY AS LP_QTY,A.PRICE
	FROM	(SELECT A.REPAIRPAPER_ID,A.CREATETIME,B.PARTNO,SUM(B.PARTQTY) PARTQTY,
						CAST(SUM(B.PRICE)/COUNT(*) AS DECIMAL(14,2)) PRICE
				 FROM dbo.SE_REPAIRPAPER A
				 JOIN dbo.SE_REPAIRPART B ON A.REPAIRPAPER_ID=B.REPAIRPAPER_ID
				 JOIN dbo.PA_PART_LIST C ON C.PARTNO=B.PARTNO
				 WHERE B.IS_OUT_STORAGE = '1' /*要出库的*/
					 AND A.ISENABLE=1/*有效的*/
					 AND YEAR(A.CREATETIME)='#YEAR#'/*以创建日期为准*/
					 AND A.NETCODE='#NETCODE#' /*计算的网点*/
					 AND B.REPAIR_TYPE='#REPAIR_TYPE#' 
					 #MUST_TYPE# 
				GROUP BY A.REPAIRPAPER_ID,A.CREATETIME,B.PARTNO
				)A
		LEFT JOIN (SELECT AA.REL_ID,BB.PARTNO,SUM(QTY) QTY
					FROM dbo.PA_LP_BOOK_M AA,dbo.PA_LP_BOOK_D BB 
					WHERE  AA.BOOK_M_ID=BB.BOOK_M_ID AND AA.REL_TYPE='1'/*维修出库*/
						AND	 AA.AUDIT_STATUS='2' /*审核通过*/
						AND YEAR(AA.AUDIT_DATE)='#YEAR#'/*以审核日期为准*/
						AND AA.NETCODE='#NETCODE#' /*计算的网点*/
					GROUP BY AA.REL_ID,BB.PARTNO 
			  ) B ON B.REL_ID=A.REPAIRPAPER_ID AND B.PARTNO=A.PARTNO
	 
 ) AA
GROUP BY  CAL_MONTH 
SELECT  1 AS SORTNUM,'种类(%)' CALYEAR ,SUM([1]) [1],SUM([2]) [2] ,SUM([3]) [3] ,SUM([4]) [4] ,SUM([5]) [5] ,SUM([6]) [6],
  SUM([7]) [7] ,SUM([8]) [8] ,SUM([9]) [9] ,SUM([10]) [10] ,SUM([11]) [11] ,SUM([12]) [12]
FROM #T_REPAIR_LP p PIVOT
( SUM(PZ_RATE) FOR CAL_MONTH IN ( [1], [2], [3], [4], [5], [6], [7],
                                            [8], [9], [10], [11], [12] ) ) AS pvt
UNION
SELECT  2 AS SORTNUM,'数量(%)' CALYEAR ,SUM([1]) [1],SUM([2]) [2] ,SUM([3]) [3] ,SUM([4]) [4] ,SUM([5]) [5] ,SUM([6]) [6],
  SUM([7]) [7] ,SUM([8]) [8] ,SUM([9]) [9] ,SUM([10]) [10] ,SUM([11]) [11] ,SUM([12]) [12]
FROM #T_REPAIR_LP p PIVOT
( SUM(QTY_RATE) FOR CAL_MONTH IN ( [1], [2], [3], [4], [5], [6], [7],
                                            [8], [9], [10], [11], [12] ) ) AS pvt
UNION
SELECT  3,'金额(%)' AS CALYEAR ,SUM([1]) [1],SUM([2]) [2] ,SUM([3]) [3] ,SUM([4]) [4] ,SUM([5]) [5] ,SUM([6]) [6],
  SUM([7]) [7] ,SUM([8]) [8] ,SUM([9]) [9] ,SUM([10]) [10] ,SUM([11]) [11] ,SUM([12]) [12]
FROM  #T_REPAIR_LP p PIVOT
( SUM(MONEY_RATE) FOR CAL_MONTH IN ( [1], [2], [3], [4], [5], [6], [7], [8],
                                     [9], [10], [11], [12] ) ) AS pvt
ORDER BY SORTNUM