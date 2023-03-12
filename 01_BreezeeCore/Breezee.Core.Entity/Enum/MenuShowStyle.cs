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
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 菜单的显示风格枚举
    /// </summary>
    public enum MenuShowStyle
    {
        /// <summary>
        /// 默认风格
        /// </summary>
        Default,

        /// <summary>
        /// 简约风格，不显示二级菜单，在原二级菜单位置用其对应的三级菜单替换，在原二级菜单之间插入分割线
        /// </summary>
        Contracted,

        /// <summary>
        /// 混合风格，如果第一级菜单的所有三级菜单的数目在25个内，用简约风格，否则用默认风格
        /// </summary>
        Mixed
    }
}
