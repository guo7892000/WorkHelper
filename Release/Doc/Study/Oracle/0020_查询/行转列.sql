/*行转列*/
WITH tempKPI AS (select TO_NUMBER(SUBSTR(KPI.YEAR_MONTH, -2)) AS MONTHS,
       KPI.PART_PZ_RATE /*备件品种满足率*/,
       KPI.NEED_VARIETY_ALL /*必备件品种满足率*/
 FROM T_PA_BU_PART_MONTH_KPI KPI
 WHERE KPI.NET_CODE= '#NET_CODE#'
 AND SUBSTR(KPI.YEAR_MONTH,0,4) = '#YYYY#'
 ) 
select '总体满足率(%)' ""CAL_TYPE"", 
sum(decode(months,1,PART_PZ_RATE,0)) ""1"",sum(decode(months,2,PART_PZ_RATE,0)) ""2"",sum(decode(months,3,PART_PZ_RATE,0)) ""3"",
sum(decode(months,4,PART_PZ_RATE,0)) ""4"",sum(decode(months,5,PART_PZ_RATE,0)) ""5"",sum(decode(months,6,PART_PZ_RATE,0)) ""6"",
sum(decode(months,7,PART_PZ_RATE,0)) ""7"",sum(decode(months,8,PART_PZ_RATE,0)) ""8"",sum(decode(months,9,PART_PZ_RATE,0)) ""9"",
sum(decode(months,10,PART_PZ_RATE,0)) ""10"",sum(decode(months,11,PART_PZ_RATE,0)) ""11"",sum(decode(months,12,PART_PZ_RATE,0)) ""12""
from tempKPI
union 
select '必备件品种满足率(%)' ""CAL_TYPE"", 
sum(decode(months,1,NEED_VARIETY_ALL,0)) ""1"",sum(decode(months,2,NEED_VARIETY_ALL,0)) ""2"",sum(decode(months,3,NEED_VARIETY_ALL,0)) ""3"",
sum(decode(months,4,NEED_VARIETY_ALL,0)) ""4"",sum(decode(months,5,NEED_VARIETY_ALL,0)) ""5"",sum(decode(months,6,NEED_VARIETY_ALL,0)) ""6"",
sum(decode(months,7,NEED_VARIETY_ALL,0)) ""7"",sum(decode(months,8,NEED_VARIETY_ALL,0)) ""8"",sum(decode(months,9,NEED_VARIETY_ALL,0)) ""9"",
sum(decode(months,10,NEED_VARIETY_ALL,0)) ""10"",sum(decode(months,11,NEED_VARIETY_ALL,0)) ""11"",sum(decode(months,12,NEED_VARIETY_ALL,0)) ""12""
from tempKPI;


