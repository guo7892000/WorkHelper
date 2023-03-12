using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：数据库维护类型		
 * 对象类别：枚举		
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
    /// 数据库维护类型
    /// </summary>
    public enum DbEditType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 物理删除
        /// </summary>
        Delete,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        Disable,
        /// <summary>
        /// 其他
        /// </summary>
        Other
    }
}
