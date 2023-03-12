using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// IMiniBaseDAC模块数据库访问层基类
    /// IDAL下的所有对象都应继承于它
    /// </summary>
    public class IMiniModule : Module
    {
        private IApp _curApp;
        private IDictionary<string, IModule> _OutModules = new Dictionary<string, IModule>();
        private IDictionary<string, DbServerInfo> _DbServers = new Dictionary<string, DbServerInfo>();
        public virtual string Id => "61B3130F-5DFA-4A3F-BE0A-B4C697D8E545";
        public virtual string Name => "迷你框架";

        public virtual string Code => "DBTool";

        public virtual string Desc => "轻量级的Window框架，用于一些工具";

        public override IDataAccess DataAccess { get { return ContainerContext.Container.Resolve<IDataAccess>(MiniGlobalValue.DataAccessConfigKey); } }

        public override DbServerInfo MainDbServer => DataAccess.DbServer;

        public override IApp BelongApp { get; set; }
        public override IConfig Config { get; set; }
        public override IDBInitializer DBInitializer { get { return ContainerContext.Container.Resolve<IDBInitializer>(MiniGlobalValue.DBInitializerConfigKey); } }
        public override IDictionary<string, IModule> OutModules => _OutModules;

        public override IMenu Menu => throw new NotImplementedException();
        
    }
}
