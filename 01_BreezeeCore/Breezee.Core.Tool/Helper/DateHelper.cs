using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***************************************
 * 对象名称：日期辅助类
 * 对象类别：日期类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：日期辅助类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 日期辅助类
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 处理开始日期，当开始日期为空时，则返回DateTime.MinValue的yyyy-MM-dd格式
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <returns>返回处理后的字符串</returns>
        public static string HandleBeginDate(string beginDate)
        {
            if (string.IsNullOrEmpty(beginDate))
            {
                return DateTime.MinValue.Date.ToString("yyyy-MM-dd");;
            }
            else
            {
                return beginDate;
            }
        }

        /// <summary>
        /// 处理结束日期，当结束日期为空时，则返回9999-01-01
        /// </summary>
        /// <param name="maxDate">结束日期</param>
        /// <returns>返回处理后的字符串</returns>
        public static string HandleEndDate(string maxDate)
        {
            if (string.IsNullOrEmpty(maxDate))
            {
                return "9999-01-01";
            }
            else
            {
                return maxDate;
            }
        }

        /// <summary>
        /// 将对象转换为时间。如果对象为空或者转换失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">需要转换的对象</param>
        /// <returns>返回时间</returns>
        public static DateTime ToDateTime(object value)
        {
            if (StringHelper.IsNullOrEmpty(value))
            {
                return DateTime.MinValue;
            }

            DateTime result;
            if (DateTime.TryParse(value.ToString().Trim(), out result))
            {
                return result;
            }

            return DateTime.MinValue;
        }

        #region 计算时间毫秒差
        /// <summary>
        /// 将对象转换为时间。如果对象为空或者转换失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">需要转换的对象</param>
        /// <returns>返回时间</returns>
        public static Int32 CalRunTime(DateTime dtBegin, DateTime dtEnd)
        {
            TimeSpan ts1 = new TimeSpan(dtBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dtEnd.Ticks);
            int minNo = int.Parse(ts1.Subtract(ts2).Duration().Minutes.ToString());
            int secondNo = int.Parse(ts1.Subtract(ts2).Duration().Seconds.ToString());
            int milliSeconds = int.Parse(ts1.Subtract(ts2).Duration().Milliseconds.ToString());
            int totalSeconds = minNo * 60 * 1000 + secondNo * 1000 + milliSeconds;
            return totalSeconds;
        } 
        #endregion

    }
}
