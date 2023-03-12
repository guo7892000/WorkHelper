using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System.Collections.Generic;

/*********************************************************************		
 * 对象名称：模块抽象类
 * 对象类别：抽象类		
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
    /// 模块抽象类
    /// </summary>
    public abstract class Module : IModule
    {
        public abstract IDataAccess DataAccess { get; }
        public abstract IApp BelongApp { get; set; }
        public abstract IDBInitializer DBInitializer { get; }
        public abstract IMenu Menu { get; }
        public bool IsMain => false; 
        public bool IsInner => false;
        public abstract IConfig Config { get; set; }
        public abstract DbServerInfo MainDbServer { get; }
        public abstract IDictionary<string, IModule> OutModules { get; }
    }
}
