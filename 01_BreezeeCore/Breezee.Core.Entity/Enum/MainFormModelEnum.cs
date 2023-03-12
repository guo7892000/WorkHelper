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
    /// 主框架模式
    /// </summary>
    public enum MainFormModelEnum
    {
        /// <summary>
        /// 全功能模式（包括角色授权、组织管理等，一般作为企业管理系统）
        /// </summary>
        FullFunction,
        /// <summary>
        /// 迷你模式（只有简单登录，一般作为工具类框架）
        /// </summary>
        Mini
    }
}
