using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：菜单类型
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
    /// 名称：菜单类型
    /// 作者：黄国辉
    /// 日期：2013-11-10
    /// 用法：后台存储的菜单类型。
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// S:系统
        /// </summary>
        System,
        /// <summary>
        /// M:模块
        /// </summary>
        Module,

        /// <summary>
        /// F:窗体
        /// </summary>
        Form,

        /// <summary>
        /// P:页面
        /// </summary>
        Page,

        /// <summary>
        /// B:按钮
        /// </summary>
        Button,

        /// <summary>
        /// L:分隔线
        /// </summary>
        Line,
        /// <summary>
        /// N:无
        /// </summary>
        None,

        /// <summary>
        /// 分类
        /// </summary>
        Class,
        /// <summary>
        /// M:模块
        /// </summary>
        Modul,
        /// <summary>
        /// 功能菜单
        /// </summary>
        Menu,
    }
}
