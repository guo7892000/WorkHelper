using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// 网格列类型枚举(DataGridViewColumn)
    /// </summary>
    public enum DataGridViewColumnTypeEnum
    {
        /// <summary>
        /// T表示DataGridViewTextBoxColumn
        /// </summary>
        TextBox = 1,
        /// <summary>
        /// B表示DataGridViewButtonColumn
        /// </summary>
        Button = 2,
        /// <summary>
        /// C表示DataGridViewComboBoxColumn
        /// </summary>
        ComboBox = 4,
        /// <summary>
        /// K表示DataGridViewCheckBoxColumn
        /// </summary>
        CheckBox = 8,
        /// <summary>
        /// L表示DataGridViewLinkColumn
        /// </summary>
        Link = 16,
        /// <summary>
        /// I表示DataGridViewImageColumn
        /// </summary>
        Image = 32
    }
}
