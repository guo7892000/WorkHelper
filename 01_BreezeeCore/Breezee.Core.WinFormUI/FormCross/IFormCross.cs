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
    /// 窗体接口的基类
    /// </summary>
    public interface IFormCross
    {
        /// <summary>
        /// 获取或者设置窗体的对话框结果
        /// </summary>
        DialogResult DialogResult { get; set; }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        void Close();

        /// <summary>
        /// 释放窗体使用的资源
        /// </summary>
        void Dispose();

        /// <summary>
        /// 向用户显示具有指定所有者的窗体
        /// </summary>
        /// <param name="owner"></param>
        void Show(IWin32Window owner);

        /// <summary>
        /// 向用户显示窗体
        /// </summary>
        void Show();

        /// <summary>
        /// 将窗体显示为模式对话框，并将当前活动窗口设置为它的所有者
        /// </summary>
        /// <returns>返回DialogResult 值之一</returns>
        DialogResult ShowDialog();

        /// <summary>
        ///  将窗体显示为具有指定所有者的模式对话框
        /// </summary>
        /// <param name="owner">表示将拥有模式对话框的顶级窗口</param>
        /// <returns>返回DialogResult 值之一</returns>
        DialogResult ShowDialog(IWin32Window owner);

        /// <summary>
        /// 获取或者设置窗体的窗口状态
        /// </summary>
        FormWindowState WindowState { get; set; }

        Object Tag { get; set; }

    }
}
