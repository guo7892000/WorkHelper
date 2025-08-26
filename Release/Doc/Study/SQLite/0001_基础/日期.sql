SELECT date('now'), --返回当前日期
	time('now'), --返回当前时间
	datetime('now'), --返回当前日期和时间
	strftime('%Y-%m-%d %H:%M:%S', 'now'), --将日期和时间格式化为字符串
	julianday('now'), --返回从公元前4713年1月1日开始的天数，也称为儒略日。
	date('now', '+1 month'), --将当前日期添加一个月
	julianday(date1) - julianday(date2) --计算两个日期或时间之间的差异
;