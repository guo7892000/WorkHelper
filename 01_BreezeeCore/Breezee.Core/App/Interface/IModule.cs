using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：模块接口		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 所属的App
        /// </summary>
        IApp BelongApp { get; set; }

        IDBInitializer DBInitializer { get; }
        /// <summary>
        /// 模块根菜单
        /// </summary>
        IMenu Menu { get; }

        /// <summary>
        /// 应用配置
        /// </summary>
        IConfig Config { get; set; }

        /// <summary>
        /// 主数据库信息
        /// </summary>
        DbServerInfo MainDbServer { get; }

        /// <summary>
        /// 外部关联的模块
        /// </summary>
        IDictionary<string, IModule> OutModules { get; }

        /// <summary>
        /// 是否主模块
        /// </summary>
        bool IsMain { get; }

        /// <summary>
        /// 是否内部模块
        /// </summary>
        bool IsInner { get; }
    }
}
