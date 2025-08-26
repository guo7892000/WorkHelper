-- 获取日期或时间
select current_date,/*获取系统当前日期*/
	current_time,/*获取系统当前时间(返回时带时区)*/
	localtime,/*获取系统当前时间(返回时不带时区)*/
	current_timestamp,/*获取系统当前日期和时间(返回时带时区)*/
	localtimestamp,/*获取系统当前日期和时间(返回时不带时区)*/
	now(),/*获取系统当前日期和时间(返回时带时区)*/
	extract(day from timestamp '2012-09-10 10:18:40') as extract_day,/*从月份中提取日期*/
	extract(month from timestamp '2012-09-10 10:18:40') as extract_month,
	extract(year from timestamp '2012-09-10 10:18:40') as extract_year,
	extract(doy from timestamp '2012-09-10 10:18:40') as extract_doy,/*查询指定日期是一年中的第几天*/
	extract(dow from timestamp '2012-09-10 10:18:40') as extract_dow,/*查询指定日期是一周中的星期几*/
	extract(quarter from timestamp '2012-09-10 10:18:40') as extract_quarter,/*查询指定日期是该年的第几季度(1-4)*/
	date '2019-09-28' + integer '10' as date_integer,/*加10天*/
	date '2012-09-28' + interval '3 hour' as date_interval_hour,/*加3小时*/
	date '2012-09-28' + time '06:00' as date_time,/*日期+时间*/
	timestamp '2012-09-28 02:00:00' + interval '10 hours' as timestamp_interval,/*时间戳+时间*/
	date '2012-11-01' - date '2012-09-10' as date_subtract_date,/*日期差*/
	date '2012-09-28' - integer '10' date_subtract_integer,/*日期-天数*/
	15 * interval '2 day' as interval_double_day,
	50 * interval '2 second' as interval_double_second,
	interval '1 hour' / integer  '2' as interval_chu_integer


-- 日期转换
select to_char(now(), 'yyyy-mm-dd hh24:mi:ss') as to_char1,
 to_char(now(), 'yyyy-mm-dd') as to_char2,
 to_timestamp('2025-04-01 14:30:55', 'yyyy-mm-dd hh24:mi:ss') as to_timestamp1,
 cast('2023-04-01 12:00:00' as timestamp) as cast_timestamp,
 to_date('2025-04-01', 'yyyy-mm-dd') as to_date1,
 cast('2023-04-01' as date) as cast_date1,
 date '2023-04-01' as date_only,
 time '12:00:00' as time_only,
 date '2023-04-01' + time '12:00:00' date_time