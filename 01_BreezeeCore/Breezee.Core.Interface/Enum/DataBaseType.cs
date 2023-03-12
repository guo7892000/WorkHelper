using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：数据库类型	
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5	
 * 对象说明：目前只支持五种		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        SqlServer,

        /// <summary>
        /// Oracle数据库
        /// </summary>
        Oracle,

        /// <summary>
        /// SQLite数据库
        /// </summary>
        MySql,

        /// <summary>
        /// PostgreSql数据库
        /// </summary>
        SQLite,

        /// <summary>
        /// MySql数据库
        /// </summary>
        PostgreSql,
    }
}
