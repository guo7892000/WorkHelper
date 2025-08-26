/*空值*/
select if(ifnull('','')='','空值','有值'),if(ifnull(null,'')='','空值','有值');

-- ifnull(A.CREATOR,'')!='' 代替 A.CREATOR is not null	

