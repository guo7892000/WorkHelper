using Autofac;
using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
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
namespace Breezee.WorkHelper.DBTool.DAL.SQLite
{
    /// <summary>
    /// 类
    /// </summary>
    public class DBToolDalSQLiteIocModuleAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DbServerInfo serverInfo = new DbServerInfo(DataBaseType.SQLite, "SQLite_DBTool.db", "", "", "", "", "", "");

            builder.RegisterInstance(AutoSQLExecutors.Connect(serverInfo)).As<IDataAccess>().Named<IDataAccess>(DBTGlobalValue.DataAccessConfigKey);
            builder.RegisterType<DDBConfigSet>().As<IDDBConfigSet>();
            builder.RegisterType<DDBTDBInitializer>().As<IDBInitializer>().Named<IDBInitializer>(DBTGlobalValue.DBInitializerConfigKey);
        }
    }
}