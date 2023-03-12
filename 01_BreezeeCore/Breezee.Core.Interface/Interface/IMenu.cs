using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************		
 * 对象名称：菜单接口		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/15 23:30:48		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/15 23:30:48 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 菜单接口
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// 主菜单
        /// </summary>
        MenuEntity Root { get; }
        /// <summary>
        /// 子菜单集合
        /// </summary>
        IDictionary<string, MenuEntity> Childs { get; }
        /// <summary>
        /// 菜单初始化
        /// </summary>
        void Init();

        MenuConfigType MenuConfigType { get; }
    }

    public enum MenuConfigType
    {
        Table,
        Xml
    }
}