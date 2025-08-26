/*注：Oracle的对象命名长度：小于等于30*/

/* 变量赋值使用:=  */
/* 执行符号为/  */

/*变量声明*/
V_FACT_STORE NUMBER(20,6) := 0; --变量V_FACT_STORE：小数类型，小数位数为6位，默认值为0
V_DLR_STORAGE_ID VARCHAR2(50); --变量V_DLR_STORAGE_ID：字符类型，长度为50
V_COUNT NUMBER; --变量V_COUNT：整型
MyException EXCEPTION; --变量MyException：异常类型
V_PUR_ASSIGN_TYPE T_GROUP_D.PUR_ASSIGN_TYPE%TYPE; /*变量V_PUR_ASSIGN_TYPE：跟T_GROUP_D表的PUR_ASSIGN_TYPE列类型及长度一致*/
/*游标声明*/
CURSOR CUR_DEAL_LIST IS SELECT A.*  FROM T_TMP_COSTW A;
/*游标行变量*/
REC_DEAL_LIST CUR_DEAL_LIST%ROWTYPE;  --变量REC_DEAL_LIST：游标CUR_DEAL_LIST的行类型

/*IF判断语句*/
IF v_gender = '男' AND v_age >= 30 THEN
	dbms_output.put_line('该男性年龄大于等于30岁');
ELSIF v_gender = '男' AND v_age < 30
	dbms_output.put_line('该男性年龄小于30岁');
ELSIF v_gender = '女' AND v_age >= 30 THEN
	dbms_output.put_line('该女性年龄大于等于30岁');
ELSE
	dbms_output.put_line('该女性年龄小于30岁');
END IF;

/*DECODE：相当于if-then-else逻辑*/
DECODE(value,if1,then1,if2,then2,if3,then3,...,else);

