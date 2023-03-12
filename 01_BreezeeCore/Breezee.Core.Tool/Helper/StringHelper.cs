using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Data.OleDb;

/***************************************
 * 对象名称：字符串处理类
 * 对象类别：字符类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：数字处理类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class StringHelper
    {
        #region 变量
        private static readonly string _strBlank = " "; //空格
        private static readonly string _strQuotationMark = "'"; 
        #endregion

        #region 转换为SQL查询参数
        /// <summary>
        /// 转换为SQL查询参数
        /// </summary>
        /// <returns></returns>
        public static string ConvertToSQLQueryParam(string strInParam)
        {
            return "#" + strInParam + "#";
        } 
        #endregion

        #region 生成GUID的方法(无连接符)
        /// <summary>
        /// 生成GUID的方法(无连接符)，返回32位大写字符
        /// </summary>
        /// <returns></returns>
        public static string GetGUID()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        } 
        #endregion

        #region 判断是否为双精度浮点
        /// <summary>
        /// 判断是否为双精度浮点
        /// </summary>
        /// <param name="str">需验证的字符串</param>
        /// <returns></returns>
        public static bool IsDouble(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    double i = double.Parse(str.Trim());
                    return true;
                }
            }
            catch
            {
            }
            return false;
        } 
        #endregion

        #region 字符转换成整数
        /// <summary>
        /// 字符转换成整数(小数位四舍五入)
        /// </summary>
        /// <param name="str">需验证的字符串</param>
        /// <returns></returns>
        public static int ToInt(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    str = str.Trim();
                    if (str.Contains("."))//如果包含小数点
                    {
                        int left = int.Parse(str.Split('.')[0]);
                        int right = int.Parse(str.Split('.')[1].Substring(0, 1));
                        if ((double)(right * 1.0 / 10) >= 0.5)
                            return left + 1;
                        else
                            return left;
                    }
                    else
                    {
                        int i = int.Parse(str);
                        return i;
                    }
                }
            }
            catch
            {
            }
            return 0;
        } 
        #endregion

        #region 将字符转换成日期数据
        /// <summary>
        /// 将字符转换成日期数据
        /// </summary>
        /// <param name="str">需转换的字符串</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    DateTime i = DateTime.Parse(str.Trim());
                    return i;
                }
            }
            catch
            {
            }
            return DateTime.Now.AddYears(-10); //否则返回10年前
        } 
        #endregion

        #region 将字符转换成双精度浮点小数
        /// <summary>
        /// 将字符转换成双精度浮点小数
        /// </summary>
        /// <param name="str">需转换的字符串</param>
        /// <returns></returns>
        public static double ToDouble(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    double i = double.Parse(str.Trim());
                    return i;
                }
            }
            catch
            {
            }
            return 0; //否则返回
        } 
        #endregion

        #region 转换成decimal类型
        /// <summary>
        /// 转换成decimal类型
        /// </summary>
        /// <param name="str">需转换的字符串</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(string str)
        {
            if (!string.IsNullOrEmpty(str.Trim()))
            {
                decimal value = 0;
                decimal.TryParse(str.Trim(), out value);
                return value;
            }
            return 0;
        } 
        #endregion

        #region 两个时间的比较
        /// <summary>
        /// 两个时间的比较(开始时间与截止时间比较)
        /// </summary>
        /// <param name="beginTime"> 开始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns></returns>
        public static bool CompareDateTime(string beginTime, string endTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(beginTime.Trim()) && !string.IsNullOrEmpty(endTime.Trim()))
                {
                    DateTime dtBeginTime = DateTime.Parse(beginTime.Trim());
                    DateTime dtEndTime = DateTime.Parse(endTime.Trim());
                    if (dtBeginTime < dtEndTime || dtBeginTime == dtEndTime)
                        return true;
                }
            }
            catch
            { }
            return false;
        } 
        #endregion

        #region 两个时间的比较
        /// <summary>
        /// 两个时间的比较(和当前时间进行比较，是否大于当前时间）
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="isLargerCurrent">是否大于当前时间</param>
        /// <returns></returns>
        public static bool CompareCurrentTime(string time, bool isLargerCurrent)
        {
            try
            {
                if (!string.IsNullOrEmpty(time.Trim()))
                {
                    DateTime dtBeginTime = DateTime.Parse(time.Trim());
                    if (dtBeginTime < DateTime.Now && isLargerCurrent == false)  //小于当前时间
                        return true;
                    if (dtBeginTime >= DateTime.Now && isLargerCurrent)  //大于当前时间
                        return true;
                }
            }
            catch
            { }
            return false;
        } 
        #endregion

        #region 随机产生字符串
        /// <summary>
        /// 随机产生字符串
        /// </summary>
        /// <param name="num">字符串的位数</param>
        /// <returns></returns>
        public static string GetCode(int num)
        {
            string[] source = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string code = "";

            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        } 
        #endregion

        #region 判断一个对象是否为空
        /// <summary>
        /// 判断一个对象是否为空，true表示空
        /// </summary>
        /// <param name="val">传入的对象</param>
        /// <returns>true为空</returns>
        public static bool IsNullOrEmpty(object val)
        {
            if (val == null || val == DBNull.Value)
            {
                return true;
            }

            string s = val.ToString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return true;
            }

            return false;
        } 
        #endregion

        #region 防止SQL注入
        /// <summary>
        /// 防止SQL注入
        /// </summary>
        /// <param name="strInParm"></param>
        /// <returns></returns>
        public static string PreventSQLInfuse(string strInParm)
        {
            #region 正则表达式变量
            Regex reg0 = new Regex("'", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg1 = new Regex("<", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg2 = new Regex(">", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg3 = new Regex("select", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg4 = new Regex("execute", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg5 = new Regex("where", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg6 = new Regex("insert", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg7 = new Regex("delete", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg8 = new Regex("update", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg9 = new Regex("drop", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg10 = new Regex("create", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex reg11 = new Regex("truncate", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            #endregion
            // 正则表达式替换 防注入处理
            string s = reg0.Replace(strInParm, "''"); //替换 '
            s = reg1.Replace(s, "&lt;"); // 替换 <
            s = reg2.Replace(s, "&gt;"); // 替换 >
            s = reg3.Replace(s, "sel&#101;ct"); // 替换 select
            s = reg4.Replace(s, "&#101xecute"); // 替换 execute
            s = reg5.Replace(s, "wh&#101;re"); // 替换 where
            s = reg6.Replace(s, "ins&#101;rt"); // 替换 insert
            s = reg7.Replace(s, "del&#101;te"); // 替换 delete
            s = reg8.Replace(s, "up&#100;ate"); // 替换 update
            s = reg9.Replace(s, "dro&#112;"); // 替换 drop
            s = reg10.Replace(s, "cr&#101;ate"); // 替换 create
            s = reg11.Replace(s, "trunc&#097;te"); // 替换 truncate

            return s;
        }
        #endregion

        #region 转换为整数
        public static int ToInt(string strName, string input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                throw new Exception(strName + "必须为整数!");
            }
        }
        #endregion

        #region 转换为小数
        public static Decimal ToDecimal(string strName, string input, int iNumber)
        {
            try
            {
                int iInt;
                Decimal dReturn;
                if (int.TryParse(input, out iInt))
                {
                    dReturn = Convert.ToDecimal(iInt * 1.00);
                }
                else
                {
                    dReturn = Convert.ToDecimal(input);
                }

                return Decimal.Round(dReturn, iNumber);
            }
            catch
            {
                throw new Exception(strName + "必须为小数!");
            }
        } 
        #endregion

        #region 获取字符数组(针对剪贴板内容)
        /// <summary>
        /// 获取字符数组(针对剪贴板内容)，以\n为行计算，\t为不同列
        /// </summary>
        /// <param name="pasteText"></param>
        /// <returns></returns>
        public static Object[,] GetStringArray(ref string pasteText, ref int iRow, ref int iColumn)
        {
            string strReplace = pasteText.Trim();
            int tnum = 0;
            int nnum = 0;
            //获得当前剪贴板内容的行数
            for (int i = 0; i < pasteText.Length; i++)
            {
                if (pasteText.Substring(i, 1) == "\n")
                {
                    nnum++;
                }
            }
            //获得当前剪贴板内容的列数：以第一行为准确定列数
            string strCols = strReplace.Substring(0, strReplace.IndexOf("\n"));
            for (int i = 0; i < strCols.Length - 1; i++)
            {
                if (strCols.Substring(i, 1) == "\t")
                {
                    tnum++;
                }
            }
            Object[,] data;
            //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
            if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
            {
                nnum = nnum - 1;
            }

            iRow = nnum + 1;
            iColumn = tnum + 1;
            data = new object[nnum + 1, tnum + 1];//定义一个二维数组

            String rowstr;
            rowstr = "";
            //对数组赋值
            for (int i = 0; i < iRow; i++)
            {
                for (int colIndex = 0; colIndex < iColumn; colIndex++)
                {
                    //一行中的最后一列
                    if (colIndex == tnum && pasteText.IndexOf("\r") != -1)
                    {
                        rowstr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                    }
                    //最后一行的最后一列
                    if (colIndex == tnum && pasteText.IndexOf("\r") == -1)
                    {
                        rowstr = pasteText.Substring(0);
                    }
                    //其他行列
                    if (colIndex != tnum)
                    {
                        if (pasteText.IndexOf("\t") == -1)
                        {
                            rowstr = pasteText;
                            pasteText = "";
                        }
                        else
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        }
                    }
                    data[i, colIndex] = rowstr;
                }
                //截取下一行数据
                pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);

            }
            return data;
        }
        #endregion

        #region 增加左边空格方法
        public static string AddLeftBand(string strColCode)
        {
            return _strBlank + strColCode;
        }
        #endregion

        #region 增加右边空格方法
        public static string AddRightBand(string strColCode)
        {
            return strColCode + _strBlank;
        }
        #endregion

        #region 增加左右边空格方法
        /// <summary>
        /// 增加左右边空格方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        public static string AddLeftRightBand(string strColCode)
        {
            return _strBlank + strColCode + _strBlank;
        }
        #endregion

        #region 增加左右括号方法
        public static string AddLeftRightKuoHao(string strColCode)
        {
            return "(" + strColCode + ")";
        }
        #endregion

        #region 增加左右单引号方法
        public static string ChangeIntoSqlString(string strColCode)
        {
            return _strBlank + _strQuotationMark + strColCode + _strQuotationMark + _strBlank;
        }
        #endregion
    }
}
