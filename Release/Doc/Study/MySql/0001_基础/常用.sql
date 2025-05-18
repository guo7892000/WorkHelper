
select now(),
	DATE_FORMAT(now(), '%Y-%m-%d'),
	DATE_FORMAT(now(), '%Y-%m-%d %H:%i:%s'),
	ROUND(A.NOW_RUNNING_MILE,2)
FROM T_ABLE1
where 1=1
AND  FIND_IN_SET(A.FLIT_STATUS,#{info.flitStatus})
<![CDATA[ AND INSTR(B.CUST_CODE,#{info.custCode})>0 ]]>