--获取含字母的流水号
CREATE OR REPLACE FUNCTION F_MDS_GET_WORD_SEQUENCE_NO
(
  V_SEQUENCE_NO INTEGER, ---加1后的十进制流水号，例如：10
  V_SEQUENCE_LENGTH INTEGER --流水号长度：例如4
)
/*******************************************************************************************
* 对象名称：获取含字母的流水号
* 创建作者：黄国辉
* 创建日期：2014-11-12
* 对象描述:  传入流水号及流水号长度，返回可能包含字母的流水号。
*       这样的流水号比原来只有数字的会多一些。
* 变更历史(格式：版本号\本次变更内容简述\修改人\修改日期)：
*     V1.00：新增 HGH 2014-11-12
*     V1.01：完全重写了该算法，解决之前有重复单号的问题 HUANGGH 2015-8-3
********************************************************************************************/
RETURN VARCHAR2
IS
  --自定义流水号的相关变量
  V_MY_DEFINE_VALUE  VARCHAR2(50); --自定义的流水号
  V_MY_DEFINE_WORD_LIST VARCHAR2(50):= '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ'; --自定义字符集
  N_MY_DEFINE_WORD_LIST_LEN NUMBER;  --自定义字符集长度

  I_AFTER_DIVIDE_VALUE INTEGER:=0; --除后的值
	N_MOD_VALUE           NUMBER; --余数值
	V_LOOP_NUM            NUMBER; --循环中的值
BEGIN
  --有效性判断
  IF V_SEQUENCE_NO IS NULL OR V_SEQUENCE_LENGTH IS NULL THEN
    RAISE_APPLICATION_ERROR(-20999,'传入的流水号、流水号长度都不能为空！');
  END IF;

  IF V_SEQUENCE_LENGTH <=0 THEN
    RAISE_APPLICATION_ERROR(-20999,'传入的流水号长度必须大于0！');
  END IF;

  --变量初始化
  N_MY_DEFINE_WORD_LIST_LEN := LENGTH(V_MY_DEFINE_WORD_LIST);--自定数据集长度
  V_MY_DEFINE_VALUE:=''; --返回的值
	I_AFTER_DIVIDE_VALUE:=V_SEQUENCE_NO; --值为传入的流水号

  --循环转换为流水号
	FOR V_LOOP_NUM IN 1 .. V_SEQUENCE_LENGTH LOOP
		  --得到余数值
			N_MOD_VALUE := MOD(I_AFTER_DIVIDE_VALUE,N_MY_DEFINE_WORD_LIST_LEN);
			--得到除后的值
			I_AFTER_DIVIDE_VALUE :=FLOOR(I_AFTER_DIVIDE_VALUE/N_MY_DEFINE_WORD_LIST_LEN);--这里要向下取整
			--并接
			V_MY_DEFINE_VALUE:=  SUBSTR(V_MY_DEFINE_WORD_LIST,N_MOD_VALUE + 1, 1) || V_MY_DEFINE_VALUE;
			IF I_AFTER_DIVIDE_VALUE < N_MY_DEFINE_WORD_LIST_LEN THEN --当除数小于进制数时退出
				 --得到前一位
				 V_MY_DEFINE_VALUE:=  SUBSTR(V_MY_DEFINE_WORD_LIST,I_AFTER_DIVIDE_VALUE + 1, 1) || V_MY_DEFINE_VALUE;
				 EXIT; --退出循环
			END IF;
	END LOOP;
	
	V_MY_DEFINE_VALUE :=LPAD(V_MY_DEFINE_VALUE,V_SEQUENCE_LENGTH,'0');	
    V_MY_DEFINE_VALUE := SUBSTR(V_MY_DEFINE_VALUE,LENGTH(V_MY_DEFINE_VALUE)-V_SEQUENCE_LENGTH,V_SEQUENCE_LENGTH);

  --返回值
  RETURN(V_MY_DEFINE_VALUE);
END;

