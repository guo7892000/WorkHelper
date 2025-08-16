/*一般的查询SQL*/
SELECT A.DLR_ID,
	A.BUY_CAR_CONTRACT_ID,
	A.BUY_CONTRACT_CODE,/*合同编号*/
	C.DLR_NAME as DLR_SHORT_NAME,  /*专营店名称*/
	DATE_FORMAT(A.FIRST_REGISTER_DATE,'%Y-%m-%d') AS FIRST_REGISTER_DATE,/*首次登记日期*/
	ROUND(A.NOW_RUNNING_MILE,2) AS NOW_RUNNING_MILE,/*当前行驶里程*/
	(SELECT MAX(PI.OLD_CAR_OWNER_ID) FROM t_uc_uc_bu_car_process_info PI
	WHERE PI.UC_CAR_CODE = A.UC_CAR_CODE ) AS OLD_CAR_OWNER_ID, /*旧车主ID*/
	DATE_FORMAT(APL.APPLY_DATE,'%Y-%m-%d %H:%i:%s') APPLY_DATE,/*申请日期*/
	CONCAT(APL.DLR_AUDIT_REMARK,if(IFNULL(APL.DLR_AUDIT_REMARK,'0')='0','','->'), APL.AUDIT_REMARK) as AUDIT_TEXT, /*审核说明*/
	ROUND(datediff(now(),A.FIRST_REGISTER_DATE)/365) as CAR_AGE, /*车龄*/
	case when A.IS_SALE='1' then 'NG_SALE' 
		when A.IS_AUTH='1' then 'NG_CERT'
		when A.CONTRACT_STATUS='0' then 'NG_HAD_CANCEL'
		when A.IS_OVER_DAY='0' then 'OK' 
		ELSE '0' END AS PASS_FLAG
FROM t_uc_uc_bu_buy_contract A /*买取合同*/
LEFT JOIN t_usc_mdmn_db_base_dlr_info C /*专营店*/
ON A.DLR_ID = C.DLR_ID
LEFT JOIN t_uc_uc_bu_intent_car P
ON A.UC_CAR_CODE = P.UC_CAR_CODE
LEFT JOIN  t_uc_uc_bu_contract_cancel_apply APL /*注销或修改申请*/
ON A.BUY_CAR_CONTRACT_ID = APL.CONTRACT_ID and APL.END_FLAG ='0'
LEFT JOIN t_usc_mdmn_db_base_dlr_info FDLR /*专营店*/
ON FDLR.DLR_ID = C.PARENT_DLR_ID
WHERE 1=1
	AND A.SELLER_CUST_NAME LIKE CONCAT('%','#{info.custName}','%') /*注：MyBaits的XML中SQL配置要去掉参数左右的单引号，加上是为了SQL能直接执行测试*/
	AND INSTR(A.SELLER_CUST_NAME,'#{info.custName}') > 0 /*也可以使用INSTR实现LIKE的同样效果*/
	AND FIND_IN_SET(A.FLIT_STATUS,'#{info.flitStatus}')
	AND A.OPRATE_TIME >= DATE_FORMAT('#{info.oprateTimeBegin}','%Y-%m-%d %H:%i:%s') /*MyBaits的XML中SQL配置：大小写字符要转义【&gt;】【&lt;】*/
	AND A.OPRATE_TIME < DATE_ADD(DATE_FORMAT('#{info.oprateTimeEnd}','%Y-%m-%d %H:%i:%s'), INTERVAL +1 DAY)
ORDER BY A.OPRATE_TIME DESC
LIMIT 10 OFFSET 20
;

/*分组排序后获取行号为1的数据*/
with TMP_ALL as (
select A.BUY_NAME, A.SORT_NO, A.SORT_DATE, A.VIN, 
 row_number() over (partition by A.VIN order by A.SORT_NO asc,A.SORT_DATE desc) as LATEST_NO
 from TMP_ALL_D A
)
select A.BUY_NAME, A.SORT_NO, A.SORT_DATE, A.VIN
from TMP_ALL A
where LATEST_NO=1
;

/*查询前十行*/
SELECT A.CREATE_TIME 
FROM T_TAB A
WHERE A.ID=10 AND A.REL_ID=1 
ORDER BY A.CREATE_TIME  
LIMIT 10
;

/*分组动态化：可能取一个最全最细化的所有列作为分组列，然后根据分组条件不同，看取固定值（相当于忽略该分组条件）还是具体值*/
SELECT case when t.AUTH_STATUS='3' then t.AUTH_PASS_DATE else t.AUTH_APPLY_DATE end,count(1) 
from t_au_bu_auth_apply t
#where t.AUTH_STATUS='3'
group by case when t.AUTH_STATUS='3' then t.AUTH_PASS_DATE else t.AUTH_APPLY_DATE end
;

/*使用With语法：实现数据统计明细复用。先根据条件得到数据量小的重复关联的表，再将它与其他表关联。注：WITH中的临时表数据量不能太大，会影响性能。*/
with m_tb as (
SELECT * 
from t_au_bu_auth_apply t
where t.AUTH_STATUS='3'
and t.AUTH_APPLY_DATE>'2023-01-01'
)
select AUTH_APPLY_DATE,AUTH_STATUS 
from m_tb;

/*动态SQL:适用于列不确定情况，最后一个表是统计结果*/
SET @EE='';
select @EE := CONCAT(@EE,'sum(if(OLD_CUST_NAME= \'',OLD_CUST_NAME,'\',IS_BIND,0)) as `',OLD_CUST_NAME, '`,') AS aa FROM (SELECT DISTINCT OLD_CUST_NAME FROM t_au_car_bind_log where OLD_CUST_NAME is not null) A ;
SET @QQ = CONCAT('select ifnull(VIN,\'TOTAL\')as userid,',@EE,' sum(IS_BIND) as TOTAL from t_au_car_bind_log where OLD_CUST_NAME is not null group by VIN WITH ROLLUP');
#SELECT @QQ;
PREPARE stmt FROM @QQ;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;


