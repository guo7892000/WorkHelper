using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/***************************************
 * 对象名称：日期辅助类
 * 对象类别：日期类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：日期辅助类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 日期辅助类
    /// </summary>
    public static class DateExtension
    {
        #region 设置时间控件为年月日时分格式
        /// <summary>
        /// 设置时间控件为年月日时分格式
        /// </summary>
        /// <param name="value">需要转换的对象</param>
        /// <returns>返回时间</returns>
        public static void SetDatePickerToYearMinute(this DateTimePicker dtp, bool IsHaveCheckBox = false, bool IsChecked = false)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.ShowCheckBox = IsHaveCheckBox;
            if (IsHaveCheckBox)
            {
                if (IsChecked)
                {
                    IsChecked = true;
                }
                else
                {
                    dtp.Checked = false;
                }
            }
            dtp.CustomFormat = "yyyy-MM-dd HH:mm";
        }
        #endregion

        #region 设置时间控件格式
        /// <summary>
        /// 设置时间控件格式
        /// </summary>
        /// <param name="value">需要转换的对象</param>
        /// <returns>返回时间</returns>
        public static void SetDatePickerFormat(this DateTimePicker dtp, string DateFormat, bool IsHaveCheckBox = false, bool IsChecked = false)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.ShowCheckBox = IsHaveCheckBox;
            if (IsHaveCheckBox)
            {
                if (IsChecked)
                {
                    dtp.Checked = true;
                }
                else
                {
                    dtp.Checked = false;
                }
            }
            dtp.CustomFormat = DateFormat;
        }
        #endregion

      
    }
}
