using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Framework.Mini.Entity;
using Breezee.Framework.Mini.IDAL;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    public class MiniDalSQLiteIocInstallerCastle: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            DbServerInfo serverInfo = new DbServerInfo(DataBaseType.SQLite, "SQLite_Mini.db", "", "", "", "", "", "");
            DbConfigEntity dbConfigEntity = new DbConfigEntity(MiniGlobalValue.DbConfigFileDir, MiniGlobalValue.DbConfigFileName, XmlConfigSaveType.Element);
            if(dbConfigEntity.MainDb!=null )
            {
                serverInfo = dbConfigEntity.MainDb;
            }
            container.Register(Component.For<IDataAccess>().Instance(AutoSQLExecutors.Connect(serverInfo)).Named(MiniGlobalValue.DataAccessConfigKey));
            container.Register(Component.For<IDMiniLogin>().ImplementedBy<SQLite.DMiniLogin>());
            container.Register(Component.For<IDMiniSystem>().ImplementedBy<SQLite.DMiniSystem>());
            container.Register(Component.For<IDBInitializer>().ImplementedBy<SQLite.DMiniDBInitializer>().Named(MiniGlobalValue.DBInitializerConfigKey));
        }
    }
}