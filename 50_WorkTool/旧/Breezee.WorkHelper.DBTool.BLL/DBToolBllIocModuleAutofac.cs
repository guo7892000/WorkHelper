using Autofac;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.WorkHelper.DBTool.IDAL;


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
namespace Breezee.WorkHelper.DBTool.BLL
{
    /// <summary>
    /// 类
    /// </summary>
    public class DBToolBllIocModuleAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BDBConfigSet>().As<IDBConfigSet>();
            builder.RegisterType<BDBDefaultValue>().As<IDBDefaultValue>();
            builder.RegisterType<BDBTSystemSet>().As<IDBTSystemSet>();
        }
    }
}