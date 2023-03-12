using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/***************************************
 * 对象名称：数字处理类
 * 对象类别：数字类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：数字处理类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 数字处理类
    /// </summary>
    public static class NumberHelper
    {
        #region 转换对象为decimal
        /// <summary>
        /// 转换对象为decimal，如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="o">需要转换的对象</param>
        /// <returns>返回decimal</returns>
        public static decimal ToDecimal(object o)
        {
            if (StringHelper.IsNullOrEmpty(o))
            {
                return default(decimal);
            }

            decimal result;
            if (decimal.TryParse(o.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return default(decimal);
            }
        } 
        #endregion

        #region 转换对象为double
        /// <summary>
        /// 转换对象为double，如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="o">需要转换的对象</param>
        /// <returns>返回double</returns>
        public static double ToDouble(object o)
        {
            if (StringHelper.IsNullOrEmpty(o))
            {
                return default(double);
            }

            double result;
            if (double.TryParse(o.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return default(double);
            }
        } 
        #endregion

        #region  转换对象为int
        /// <summary>
        /// 转换对象为int，如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="o">需要转换的对象</param>
        /// <returns>返回int</returns>
        public static int ToInt(object o)
        {
            return NumberHelper.ToInt(o, false);
        } 
        #endregion

        #region 转换对象为int
        /// <summary>
        /// 转换对象为int，如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="o">需要转换的对象</param>
        /// <param name="round">是否四舍五入</param>
        /// <returns>返回int</returns>
        public static int ToInt(object o, bool round)
        {
            if (StringHelper.IsNullOrEmpty(o))
            {
                return default(int);
            }

            decimal d = NumberHelper.ToDecimal(o);
            if (round)
            {
                return (int)Math.Round(d, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                return (int)d;
            }
        } 
        #endregion

        #region 将文件字节转换成kb和mb
        public static string ConvertByteToKMB(long bytes)
        {
            double filesize = double.Parse(bytes.ToString());
            string size = string.Empty;
            if (filesize > 1024 * 1024)//大于1Mb显示为Mb
            {
                filesize = filesize / 1024 / 1024;
                return string.Format("{0:F2} MB", filesize);
            }
            else//显示为kb
            {
                filesize = filesize / 1024;
                return string.Format("{0:F2} KB", filesize);
            }
        }
        #endregion
    }
}
