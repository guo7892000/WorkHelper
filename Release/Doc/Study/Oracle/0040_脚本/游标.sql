/*声明*/
v_Dms_NetCode eabuc.t_org_dealer.dealerno%Type;
Cursor curBalance is
    Select * from eabuc.PS_U_T_MEMU_CERTIFICATE;
  RecBalance curBalance%RowType;
begin
  open curBalance;
  loop
    Fetch curBalance
      into RecBalance;
    Exit when curBalance%NotFound;
	  begin
		/*逻辑处理，此处略*/

		  commit;
		WHEN OTHERS THEN
			/*事务回滚*/
			Rollback; 
			/*抛出系统错误*/
			RAISE_APPLICATION_ERROR(-20999, sqlerrm);
	 end;
  end loop;
  close curBalance;
end;

/*Oracle的静态游标与动态游标：
1、静态游标：显式游标和隐式游标称为静态游标，因为在使用他们之前，游标的定义已经完成，不能再更改。
定义：
	Cursor 游标名(参数1，参数2......) is  查询语句
调用时：
	for 变量行 in 游标名 loop

	end loop;

2、动态游标：游标在声明时没有设定，在打开时可以对其进行修改。
定义：
	TYPE 游标别名 IS REF CURSOR;
	游标名 游标别名;
调用时：
	open 游标名 for 动态SQL语句;
	loop
		exit when 游标名%NOTFOUND;
		fetch 游标名
		  into 变量1，变量2，变量3，变量4;
*/



