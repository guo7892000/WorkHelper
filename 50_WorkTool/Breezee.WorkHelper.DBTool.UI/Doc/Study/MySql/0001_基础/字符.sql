/*注：mysql中''与null是不相等的*/
if(TRIM(ifnull(B.FINANCE_TYPE,''))='','4',B.FINANCE_TYPE)
;

/*将多个查询结果合并成一行*/
SELECT GROUP_CONCAT(dlr.DLR_ID) 
FROM  t_usc_mdmn_db_base_dlr_info dlr
;


