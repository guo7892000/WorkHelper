using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.Entity.DataStandard
{
    /// <summary>
    /// 数据类型标准（整数）
    /// </summary>
    public class DataStandardTypeConfigInt
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public DataStandardTypeConfigInt()
        {
            var entity = new MoreKeyValueEntity();
            entity.KeyXmlNodeName = "key";
            entity.ValXmlNodeName = "value";
            entity.DirectoryName = Path.Combine(GlobalContext.AppEntryAssemblyPath, DBTGlobalValue.ConfigFile_Pre);
            entity.FileName = Path.Combine(GlobalContext.AppEntryAssemblyPath, DBTGlobalValue.Cfg.DBTDataTypeConfigInt);
            entity.ColKeys.Add(KeyString.Kid);
            entity.ColKeys.Add(KeyString.Name);
            entity.ColVals.Add(ValueString.Vid);
            entity.ColVals.Add(ValueString.Name);
            entity.ColVals.Add(ValueString.Oracle);
            entity.ColVals.Add(ValueString.MySql);
            entity.ColVals.Add(ValueString.SqlServer);
            entity.ColVals.Add(ValueString.PostgreSql);
            entity.ColVals.Add(ValueString.SQLite);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Kid = "kid";
            public static readonly string Name = "name";
        }

        public static class ValueString
        {
            public static readonly string Vid = "vid";
            public static readonly string Name = "name";
            public static readonly string Oracle = "oracle";
            public static readonly string MySql = "mysql";
            public static readonly string SqlServer = "sqlserver";
            public static readonly string PostgreSql = "postgresql";
            public static readonly string SQLite = "sqlite";
        }
    }
}
