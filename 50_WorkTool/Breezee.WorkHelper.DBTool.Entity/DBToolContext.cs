using Breezee.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// 数据库工具上下文
    /// </summary>
    public class DBToolContext : ModuleContext
    {
        private KeyValueGroupConfig xmlKeyValue;
        public override KeyValueGroupConfig KeyValuCfg => xmlKeyValue;
        public DBToolContext()
        {
            xmlKeyValue = new KeyValueGroupConfig(Path.Combine(GlobalContext.AppEntryAssemblyPath, DBTGlobalValue.Cfg.KeyValueXmlPath));
        }
    }
}
