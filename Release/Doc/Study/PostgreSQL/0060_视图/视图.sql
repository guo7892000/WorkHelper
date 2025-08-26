
CREATE OR REPLACE VIEW V_COM_PROVICE_CITY
AS
SELECT 
/*******************************************
* 对象名称：省份城市县信息视图
* 对象类型：视图
* 创建作者：黄国辉
* 创建日期：2016-6-12
* 修改历史：
*	V1.0：hgh 创建 2016-6-12
*******************************************/
   A.PROVINCE_ID,
   A.PROVINCE_CODE,
   A.PROVINCE_NAME,
   B.CITY_ID,
   B.CITY_CODE,
   B.CITY_NAME,
   C.COUNTY_ID,
   C.COUNTY_CODE,
   C.COUNTY_NAME
FROM  BAS_PROVINCE A
JOIN BAS_CITY B
	ON A.PROVINCE_ID = B.PROVINCE_ID
JOIN BAS_COUNTY C
	ON B.CITY_ID = C.CITY_ID;


