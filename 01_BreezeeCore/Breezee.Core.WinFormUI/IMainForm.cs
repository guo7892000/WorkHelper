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
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 主窗体接口
    /// </summary>
    public interface IMainForm
    {
        /// <summary>
        /// 添加一个MDI子窗体
        /// </summary>
        /// <param name="form">子窗体</param>
        void AddMdiChilden(Form form);
        /// <summary>
        /// 状态条消息面板
        /// </summary>
        ToolStripStatusLabel StatusBarMessagePanel { get; }

        /// <summary>
        /// 设置系统帮助索引关联
        /// </summary>
        /// <param name="ctl">带关联的控件</param>
        /// <param name="keyword">索引关键词(全局唯一)</param>
        void SetHelpKeyword(Control ctl, string keyword);

        /// <summary>
        /// 获取子窗体数量
        /// </summary>
        /// <returns></returns>
        int GetChildCount();

    }
}
