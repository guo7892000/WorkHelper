SELECT MD5 ('mypwd');
SELECT ENCODE('secret','hex'), LENGTH(ENCODE('secret','hex'));
SELECT DECODE(ENCODE('secret','hex'),'hex');



