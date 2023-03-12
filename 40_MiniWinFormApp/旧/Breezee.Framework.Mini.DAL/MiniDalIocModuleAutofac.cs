using Autofac;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Framework.Mini.Entity;
using Breezee.Framework.Mini.IDAL;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/26 22:15:46	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/26 22:15:46 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Framework.Mini.DAL
{
    /// <summary>
    /// 类
    /// </summary>
    public class MiniDalIocModuleAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterType<DMiniLogin>().As<IDMiniLogin>();
            //builder.RegisterType<DMiniSystem>().As<IDMiniSystem>();
            //builder.RegisterType<DMiniDBInitializer>().As<IDBInitializer>().Named<IDataAccess>(MiniGlobalValue.DBInitializerConfigKey);
        }
    }
}