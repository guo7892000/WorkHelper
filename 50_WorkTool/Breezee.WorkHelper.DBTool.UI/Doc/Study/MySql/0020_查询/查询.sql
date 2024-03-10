/*查询前十行*/
SELECT a 
FROM t1 WHERE a=10 AND B=1 
ORDER BY a 
LIMIT 10
;

/*分组动态化：可能取一个最全最细化的所有列作为分组列，然后根据分组条件不同，看取固定值（相当于忽略该分组条件）还是具体值*/
SELECT case when t.AUTH_STATUS='3' then t.AUTH_PASS_DATE else t.AUTH_APPLY_DATE end,count(1) 
from t_au_bu_auth_apply t
#where t.AUTH_STATUS='3'
group by case when t.AUTH_STATUS='3' then t.AUTH_PASS_DATE else t.AUTH_APPLY_DATE end
;

/*使用With语法：实现数据统计明细复用。先根据条件得到数据量小的重复关联的表，再将它与其他表关联。注：WITH中的临时表数据量不能太大，会影响性能。*/
with m_tb as (SELECT * 
from t_au_bu_auth_apply t
where t.AUTH_STATUS='3'
and t.AUTH_APPLY_DATE>'2023-01-01')
select AUTH_APPLY_DATE,AUTH_STATUS from m_tb;

/*动态SQL:适用于列不确定情况，最后一个表是统计结果*/
SET @EE='';
select @EE := CONCAT(@EE,'sum(if(OLD_CUST_NAME= \'',OLD_CUST_NAME,'\',IS_BIND,0)) as `',OLD_CUST_NAME, '`,') AS aa FROM (SELECT DISTINCT OLD_CUST_NAME FROM t_au_car_bind_log where OLD_CUST_NAME is not null) A ;
SET @QQ = CONCAT('select ifnull(VIN,\'TOTAL\')as userid,',@EE,' sum(IS_BIND) as TOTAL from t_au_car_bind_log where OLD_CUST_NAME is not null group by VIN WITH ROLLUP');
#SELECT @QQ;
PREPARE stmt FROM @QQ;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;


