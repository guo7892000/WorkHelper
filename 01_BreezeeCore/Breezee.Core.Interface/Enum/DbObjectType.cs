using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：数据库对象类型
 * 对象类别：枚举		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5	
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 数据库对象类型
    /// </summary>
    public enum DbObjectType
    {
        /// <summary>
        /// 表
        /// </summary>
        Table,
        /// <summary>
        /// 视图
        /// </summary>
        View,
        /// <summary>
        /// 存储过程
        /// </summary>
        Produce,
        /// <summary>
        /// 自定义视图
        /// </summary>
        CustomView,
        /// <summary>
        /// 函数
        /// </summary>
        Function,
        /// <summary>
        /// 其他
        /// </summary>
        Other
    }
}
