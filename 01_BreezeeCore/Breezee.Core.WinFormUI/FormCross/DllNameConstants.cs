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
    /// Dll文件名常量
    /// </summary>
    public class DllNameConstants
    {
        /// <summary>
        /// 主框架模块DLL
        /// </summary>
        public const string DLL_MAIN_MANAGE = "Breezee.Framework.Mini.StartUp.exe"; //Net4使用
        //public const string DLL_MAIN_MANAGE = "Breezee.Framework.Mini.StartUp.dll"; //Net6使用

        /// <summary>
        /// 数据库工具模块DLL
        /// </summary>
        public const string DLL_DB_TOOL = "Breezee.WorkHelper.DBTool.UI.dll";

        /// <summary>
        /// 系统管理模块DLL
        /// </summary>
        public const string DLL_SYSTEM_MANAGE = "Breezee.Global.SystemManager.UI.dll";
    }
}
