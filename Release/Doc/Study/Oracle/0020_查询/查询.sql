/*使用临时表查询SQL*/
WITH tempKPI AS (select TO_NUMBER(SUBSTR(KPI.YEAR_MONTH, -2)) AS MONTHS,
       KPI.PART_PZ_RATE /*备件品种满足率*/,
       KPI.NEED_VARIETY_ALL /*必备件品种满足率*/,
       KPI.LP_RELIEVE_TIME_ALL /*缺件平均解除时间*/,
       KPI.STORE_RATE /*库存度*/,
       KPI.BO_RATE /*长期BO率*/,
       KPI.ORDER_RATE /*订货波动率*/,
       KPI.REACH_RATE /*到货满足率*/,
       KPI.EO_RATE /*紧急订单率EO*/
 FROM T_PA_BU_PART_MONTH_KPI KPI
 WHERE KPI.NET_CODE= 'H2904'--'#NET_CODE#'
 AND SUBSTR(KPI.YEAR_MONTH,0,4) = '2011'--'#YYYY#%'
 ) 
select '', decode(months,1,PART_PZ_RATE,0)
from tempKPI

/*针对重复数据中获取某行数据*/
SELECT *
  FROM (SELECT *
          FROM (SELECT A.*,
                       ROW_NUMBER() OVER(PARTITION BY A.BUYUPNO ORDER BY A.BUYUPNO) RN
                  FROM T_CAR_STORE@MD_DL_EABUC A) X
         WHERE X.RN = 1) A
  LEFT JOIN (SELECT *
               FROM (SELECT B.*,
                            ROW_NUMBER() OVER(PARTITION BY B.BUYUPNO ORDER BY B.BUYUPNO) RN
                       FROM T_MEMU_CONTRACT_M@MD_DL_EABUC B
                      WHERE B.CONTRACT_TYPE IN ('0', '1')) X
              WHERE X.RN = 1) B
    ON A.BUYUPNO = B.BUYUPNO
  LEFT JOIN T_CAR_CATTYPESET@MD_DL_EABUC C
    ON A.CATTYPESETID = C.CATTYPESETID


/*将某一列数据以单列显示*/
SELECT A.PART_BRAND_CODE,
       LISTAGG(A.CAR_BRAND_CODE, ',') WITHIN GROUP(ORDER BY A.PART_BRAND_CODE) AS CAR_BRAND_CODE
  FROM T_PA_DB_PART_BRAND_DETAIL A
 WHERE A.IS_ENABLE = '1'
 GROUP BY A.PART_BRAND_CODE;

/*递归查询*/
SELECT employee_id, name, manager_id, LEVEL
FROM employees
START WITH manager_id IS NULL -- 从最高层开始
CONNECT BY PRIOR employee_id = manager_id; -- 指定父子关系

/*Oracle 11g及更高版本中可用的递归查询*/
WITH RECURSIVE employee_cte (employee_id, name, manager_id, level) AS (
   SELECT employee_id, name, manager_id, 1 AS level -- 非递归部分（基线条件）
   FROM employees
   WHERE manager_id IS NULL -- 起始点
   UNION ALL
   SELECT e.employee_id, e.name, e.manager_id, ec.level + 1 -- 递归部分
   FROM employees e
   INNER JOIN employee_cte ec ON e.manager_id = ec.employee_id -- 连接条件
)
SELECT * FROM employee_cte;
