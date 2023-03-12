/***************************************
 * 对象名称：日期辅助类
 * 对象类别：日期类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：日期辅助类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
using System;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 日期辅助类
    /// </summary>
    public static class DateExtension
    {
        /// <summary>
        /// 将日期转换为yyyy-MM-dd的年月日格式，如2014-01-14。
        /// </summary>
        /// <param name="date">要转换的日期</param>
        /// <returns>返回yyyy-MM-dd的字符串</returns>
        public static string ToYYYY_MM_DD(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将日期转换为yyyy-MM的年月格式，如2014-01。
        /// </summary>
        /// <param name="date">要转换的日期</param>
        /// <returns>返回yyyy-MM的字符串</returns>
        public static string ToYYYY_MM(this DateTime date)
        {
            return date.ToString("yyyy-MM");
        }

        /// <summary>
        /// 将日期转换为yyyyMMdd的年月日格式，如20140114。
        /// </summary>
        /// <param name="date">要转换的日期</param>
        /// <returns>返回yyyyMMdd的字符串</returns>
        public static string ToYYYYMMDD(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 将日期转换为yyyyMMdd的年月日格式，如20140114。
        /// </summary>
        /// <param name="date">要转换的日期</param>
        /// <returns>返回yyyyMMdd的字符串</returns>
        public static string ToyyyyMMddHHmmss(this DateTime date)
        {
            return date.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// 将日期转换为yyyyMM的年月格式，如201401。
        /// </summary>
        /// <param name="date">要转换的日期</param>
        /// <returns>返回yyyyMM的字符串</returns>
        public static string ToYYYYMM(this DateTime date)
        {
            return date.ToString("yyyyMM");
        }  

        #region 计算时间毫秒差
        /// <summary>
        /// 将对象转换为时间。如果对象为空或者转换失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="value">需要转换的对象</param>
        /// <returns>返回时间</returns>
        public static Int32 CalRunTime(this DateTime dtBegin, DateTime dtEnd)
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
