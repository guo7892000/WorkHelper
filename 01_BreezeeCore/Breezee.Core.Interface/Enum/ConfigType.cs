using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：配置类型
 * 对象类别：枚举		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/13
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/13 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 配置类型
    /// </summary>
    public enum ConfigType
    {
        /// <summary>
        /// 数据配置
        /// </summary>
        Data,

        /// <summary>
        /// SQL配置
        /// </summary>
        SQL,

        /// <summary>
        /// IOC配置
        /// </summary>
        IOC,

        /// <summary>
        /// 菜单配置
        /// </summary>
        MENU,

        /// <summary>
        /// 值列表
        /// </summary>
        KeyValue,
    }
}
