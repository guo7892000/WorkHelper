using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：功能类型
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
    /// 名称：功能类型
    /// 作者：黄国辉
    /// 日期：2022-10-30
    /// 用法：后台存储的菜单类型。
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 页面
        /// </summary>
        Page,

        /// <summary>
        /// 窗体
        /// </summary>
        Form,

        /// <summary>
        /// 后台
        /// </summary>
        Background,

        /// <summary>
        /// 接口
        /// </summary>
        Interface
    }
}
