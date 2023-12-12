SELECT CURRENT_DATE,/*获取系统当期日期*/
	CURRENT_TIME,/*获取系统当期日期(返回时带时区)*/
	LOCALTIME,/*获取系统当期日期(返回时不带时区)*/
	CURRENT_TIMESTAMP,/*获取系统当期日期和时间*/
	LOCALTIMESTAMP,/*获取系统当期日期和时间*/
	NOW(),/*获取系统当期日期和时间*/
	EXTRACT(DAY FROM TIMESTAMP '2012-09-10 10:18:40'),/*从月份中提取日期*/
	EXTRACT(MONTH FROM TIMESTAMP '2012-09-10 10:18:40'),
	EXTRACT(YEAR FROM TIMESTAMP '2012-09-10 10:18:40'),
	EXTRACT(DOY FROM TIMESTAMP '2012-09-10 10:18:40'),/*查询指定日期是一年中的第几天*/
	EXTRACT(DOW FROM TIMESTAMP '2012-09-10 10:18:40'),/*查询指定日期是一周中的星期几*/
	EXTRACT(QUARTER FROM TIMESTAMP '2012-09-10 10:18:40'),/*查询指定日期是该年的第几季度(1-4)*/
	DATE '2019-09-28' + integer '10',
	DATE '2012-09-28' + interval '3 hour',
	DATE '2012-09-28' + time '06:00',
	TIMESTAMP '2012-09-28 02:00:00' + interval '10 hours',
	date '2012-11-01' - date '2012-09-10',
	DATE '2012-09-28' - integer '10',
	15 * interval '2 day',
	50 * interval '2 second',
	interval '1 hour' / integer  '2',



;