SELECT CHAR_LENGTH('date'), CHAR_LENGTH('egg');
SELECT LENGTH('date'), LENGTH('egg');
SELECT CONCAT('PostgreSQL', '9.15'),CONCAT('Postgre',NULL, 'SQL');
SELECT CONCAT_WS('-', '1st','2nd', '3rd'), CONCAT_WS('*', '1st', NULL, '3rd');
SELECT LEFT('football', 5),RIGHT('football', 4);
SELECT LPAD('hello',4,'??'), LPAD('hello',10,'??');
SELECT RPAD('hello',4,'?'), RPAD('hello',10,'?');
SELECT '(  book  )',CONCAT('(',LTRIM('  book  '),')');
SELECT '(  book  )',CONCAT('(', RTRIM ('  book  '),')');
SELECT '(  book  )',CONCAT('(', TRIM('  book  '),')');
SELECT TRIM('xy' FROM 'xyboxyokxyxy') ;
SELECT REPEAT('PostgreSQL', 3);
SELECT REPLACE('xxx.PostgreSQL.com', 'x', 'w');
SELECT SUBSTRING('breakfast',5) AS col1, 
	SUBSTRING('breakfast',5,3) AS col2,
	SUBSTRING('lunch', -3) AS col3;
SELECT POSITION('ball' IN 'football');
SELECT REVERSE('abc');
SELECT 'A'||'B' AS "Á¬½Ó×Ö·û";

