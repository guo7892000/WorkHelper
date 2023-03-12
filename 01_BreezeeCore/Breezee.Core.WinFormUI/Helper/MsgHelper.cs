using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 消息提示辅助类
    /// </summary>
    public class MsgHelper
    {
        #region 变量
        static string _strTitel = "温馨提示";
        static string _strTitelRemile = "温馨提醒"; 
        #endregion

        #region 提示信息
        public static void ShowInfo(string Msg)
        {
            ShowInfo(Msg, _strTitel);
        }

        public static void ShowInfo(string Msg, string title)
        {
            MsgBox.Show(Msg, title, MyButtons.OK, MyIcon.Information);
        }

        public static void ShowInfo(IDictionary<string, string> idic, string title, string longMsg)
        {
            MsgBox.Show(idic, title, longMsg);
        }

        public static void ShowInfo(string Msg, string title, string longMsg)
        {
            MsgBox.Show(Msg, title, longMsg);
        }

        public static DialogResult ShowInfo(string message, string title, MyButtons myButtons, string longMsg)
        {
            return MsgBox.Show(message, title, myButtons, longMsg);
        }

        public static DialogResult ShowInfo(string message, string title, MyButtons myButtons, MyIcon myIcon, string longMsg)
        {
            return MsgBox.Show(message, title, myButtons, myIcon, longMsg);
        }
        #endregion

        #region 提示错误
        public static void ShowErr(Exception ex)
        {
            MsgBox.ShowException(ex);
        }

        public static void ShowErr(string message, Exception ex)
        {
            MsgBox.ShowException(message, ex);
        }

        public static void ShowErr(string Msg)
        {
            ShowErr(Msg, _strTitel);
        }

        public static void ShowErr(string Msg, string title)
        {
            MsgBox.Show(Msg, title, MyButtons.OK, MyIcon.Error);
        }
        #endregion

        #region 提示OkCancel选择
        public static DialogResult ShowOkCancel(string Msg)
        {
            return ShowQuestion(Msg, _strTitelRemile, MyButtons.OKCancel);
        }
        #endregion

        #region 提示YesNo选择
        public static DialogResult ShowYesNo(string Msg)
        {
            return ShowQuestion(Msg, _strTitelRemile, MyButtons.YesNo);
        }
        #endregion

        #region 提示自定义选择
        public static DialogResult ShowQuestion(string Msg, MyButtons btn)
        {
            return ShowQuestion(Msg, _strTitelRemile, btn);
        }

        public static DialogResult ShowQuestion(string Msg, string title, MyButtons btn)
        {
            return MsgBox.Show(Msg, title, btn, MyIcon.Question); 
        }
        #endregion

    }
}
