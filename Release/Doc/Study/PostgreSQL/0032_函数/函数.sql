CREATE OR REPLACE  FUNCTION F_SYS_GET_WORD_SEQUENCE_NO
(
  V_SEQUENCE_NO INT, /**加1后的十进制流水号，例如：10**/
  V_SEQUENCE_LENGTH INT /**流水号长度：例如4**/
)
/*******************************************************************************************
* 对象名称：获取含字母的流水号
* 创建作者：黄国辉
* 创建日期：2014-11-12
* 对象描述:  传入流水号及流水号长度，返回可能包含字母的流水号。
*       这样的流水号比原来只有数字的会多一些。
* 变更历史(格式：版本号\本次变更内容简述\修改人\修改日期)：
*     V1.00：新增 HGH 2015-10-2
********************************************************************************************/
RETURNS VARCHAR(50) AS $$
  /**自定义流水号的相关变量**/
DECLARE 
  V_MY_DEFINE_VALUE VARCHAR(50); /**自定义的流水号**/
  V_MY_DEFINE_WORD_LIST VARCHAR(50); /**自定义字符集**/
  N_MY_DEFINE_WORD_LIST_LEN INT;  /**自定义字符集长度**/

  I_AFTER_DIVIDE_VALUE INT; /**除后的值**/
  N_MOD_VALUE           INT; /**余数值**/
  V_LOOP_NUM            INT; /**循环中的值**/
BEGIN  
  /**有效性判断**/
  IF V_SEQUENCE_NO IS NULL OR V_SEQUENCE_LENGTH IS NULL THEN
    /**传入的流水号、流水号长度都不能为空**/
    RETURN '';
  END IF;

  IF V_SEQUENCE_LENGTH <=0 THEN
    /**传入的流水号长度必须大于0**/
    RETURN '';
  END IF;

  /**变量初始化**/
  V_MY_DEFINE_WORD_LIST := '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
  I_AFTER_DIVIDE_VALUE := 0;
  N_MY_DEFINE_WORD_LIST_LEN := LEN(V_MY_DEFINE_WORD_LIST); /**自定数据集长度**/
  V_MY_DEFINE_VALUE := ''; /**返回的值**/
  I_AFTER_DIVIDE_VALUE := V_SEQUENCE_NO; /**值为传入的流水号**/
  
  V_LOOP_NUM := 1;
  /**循环转换为流水号**/
  WHILE V_LOOP_NUM <= V_SEQUENCE_LENGTH LOOP
	  /**得到余数值**/
		N_MOD_VALUE := I_AFTER_DIVIDE_VALUE%N_MY_DEFINE_WORD_LIST_LEN;
		/**得到除后的值**/
		I_AFTER_DIVIDE_VALUE := FLOOR(I_AFTER_DIVIDE_VALUE/N_MY_DEFINE_WORD_LIST_LEN); /**这里要向下取整**/
		/**并接**/
		V_MY_DEFINE_VALUE :=  SUBSTRING(V_MY_DEFINE_WORD_LIST,N_MOD_VALUE + 1, 1) + V_MY_DEFINE_VALUE;
		IF I_AFTER_DIVIDE_VALUE < N_MY_DEFINE_WORD_LIST_LEN THEN /**当除数小于进制数时退出**/
			 /**得到前一位**/
			 V_MY_DEFINE_VALUE :=  SUBSTRING(V_MY_DEFINE_WORD_LIST,I_AFTER_DIVIDE_VALUE + 1, 1) + V_MY_DEFINE_VALUE;
			 EXIT; /**退出循环**/
		END IF;
		V_LOOP_NUM := V_LOOP_NUM + 1;
	END LOOP;
  
  V_MY_DEFINE_VALUE := RIGHT(REPLICATE('0',10)+LTRIM(V_MY_DEFINE_VALUE),V_SEQUENCE_LENGTH);
  V_MY_DEFINE_VALUE := SUBSTRING(V_MY_DEFINE_VALUE,LEN(V_MY_DEFINE_VALUE)-V_SEQUENCE_LENGTH,V_SEQUENCE_LENGTH+1);

  /**返回值**/
  RETURN V_MY_DEFINE_VALUE;
END;
$$
language 'plpgsql';

