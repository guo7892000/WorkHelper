using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IDAL;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

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
    public class DBToolDacSQLiteIocInstallerCastle: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            DbServerInfo serverInfo = new DbServerInfo(DataBaseType.SQLite, "SQLite_DBTool.db", "", "", "", "", "", "");
            container.Register(Component.For<IDataAccess>().Instance(AutoSQLExecutors.Connect(serverInfo)).Named(DBTGlobalValue.DataAccessConfigKey));
            container.Register(Component.For<IDDBConfigSet>().ImplementedBy<SQLite.DDBConfigSet>());
            container.Register(Component.For<IDBInitializer>().ImplementedBy<SQLite.DDBTDBInitializer>().Named(DBTGlobalValue.DBInitializerConfigKey));
        }
    }
}