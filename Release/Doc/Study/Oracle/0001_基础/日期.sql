/*转换为毫秒字符*/
select to_char(systimestamp,'yyyy-MM-dd HH24:mi:ss.ff3') last_updated_date,TO_TIMESTAMP('2012-07' || '-01 00:00:00:000000','YYYY-MM-DD hh24:mi:ss:ff3')
from t_se_bu_tool_store;
/*截取日期*/
select sysdate 当时日期,
to_char(sysdate-1,'d') 星期几,
round(sysdate) 最近0点日期,
round(sysdate,'day') 最近星期日,
round(sysdate,'month') 最近月初,
round(sysdate,'q') 最近季初日期, 
round(sysdate,'year') 最近年初日期 from dual;

/*时间差值*/
select 
round(to_number(end_date-start_date)) "消逝的时间（以天为单位）", 
round(to_number(end_date-start_date)*24) "消逝的时间（以小时为单位）", 
round(to_number(end_date-start_date)*1440) "消逝的时间（以分钟为单位）"
from dual;
/*变动日期时间数值*/
select
trunc(sysdate)+(interval '1' second), --加1秒(1/24/60/60)
trunc(sysdate)+(interval '1' minute), --加1分钟(1/24/60)
trunc(sysdate)+(interval '1' hour), --加1小时(1/24)
trunc(sysdate)+(INTERVAL '1' DAY),  --加1天(1)
trunc(sysdate)+(INTERVAL '1' MONTH), --加1月
trunc(sysdate)+(INTERVAL '1' YEAR), --加1年
trunc(sysdate)+(interval '01:02:03' hour to second), --加指定小时到秒
trunc(sysdate)+(interval '01:02' minute to second), --加指定分钟到秒
trunc(sysdate)+(interval '01:02' hour to minute), --加指定小时到分钟
trunc(sysdate)+(interval '2 01:02' day to minute) --加指定天数到分钟
from dual;

/*取年月日*/
select to_char(sysdate,'yyyy'), to_char(sysdate, 'mm'), to_char(sysdate, 'dd') from dual; 

/*格式转换*/
TO_CHAR('#DATE#','YYYY-MM-DD')
TO_DATE('#DATE#','YYYY-MM-DD')

/*返回时间段的自增长的自定列*/
SELECT TO_CHAR(ADD_MONTHS(TO_DATE('20150525', 'YYYYMMDD'), 1 - LEVEL),
                 'YYYYMM') MONTHS
    FROM DUAL
  CONNECT BY LEVEL <= 12;


/*返回时间段（年、月、日）的自增长的自定列*/
SELECT DECODE('2',/*指定时间类型：1年，2月份，3天*/
  '1',
  ADD_MONTHS(TO_DATE('20160525', 'YYYYMMDD'), (LEVEL-1)*12),
  '2',
  ADD_MONTHS(TO_DATE('20150525', 'YYYYMMDD'), LEVEL-1),
  '3',
  TO_DATE('20150522', 'YYYYMMDD') + LEVEL-1)
FROM DUAL
CONNECT BY LEVEL <= DECODE('2',/*指定时间类型：1年，2月份，3天*/
  '1',
  TO_CHAR(TO_DATE('20160625', 'YYYYMMDD'),'YYYY')-TO_CHAR(TO_DATE('20160125', 'YYYYMMDD'),'YYYY')+1,
  '2',
  FLOOR(MONTHS_BETWEEN(TO_DATE('20150625', 'YYYYMMDD'), TO_DATE('20150501', 'YYYYMMDD')))+1,
  '3',
  TRUNC(TO_DATE('20150523', 'YYYYMMDD'))-TRUNC(TO_DATE('20150522', 'YYYYMMDD'))+1
	)