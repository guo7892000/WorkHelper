using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbServerInfo = Breezee.Core.Interface.DbServerInfo;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// IMiniBaseDAC模块数据库访问层基类
    /// IDAL下的所有对象都应继承于它
    /// </summary>
    public class IDBToolModule : Module
    {
        private IApp _curApp;
        private IDictionary<string, IModule> _OutModules = new Dictionary<string, IModule>();
        private IDictionary<string, DbServerInfo> _DbServers = new Dictionary<string, DbServerInfo>();
        public virtual string Id => "280495DE-63AE-4C30-A243-B02C04883EE7";
        public virtual string Name => "数据库工具";

        public virtual string Code => "DBTool";

        public virtual string Desc => "数据库工具";

        public override IApp BelongApp { get => _curApp; set => _curApp = value; }

        public override IDataAccess DataAccess { get { return ContainerContext.Container.Resolve<IDataAccess>(DBTGlobalValue.DataAccessConfigKey); } }

        public override DbServerInfo MainDbServer => DataAccess.DbServer;

        public override IConfig Config { get; set; }
        public override IDBInitializer DBInitializer { get { return ContainerContext.Container.Resolve<IDBInitializer>(DBTGlobalValue.DBInitializerConfigKey); } }

        public override IDictionary<string, IModule> OutModules => _OutModules;

        public override IMenu Menu => throw new NotImplementedException();

       
    }
}
