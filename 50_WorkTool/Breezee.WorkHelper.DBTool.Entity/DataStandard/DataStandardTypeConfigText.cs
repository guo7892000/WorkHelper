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
    /// 数据类型标准（文本）
    /// </summary>
    public class DataStandardTypeConfigText
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public DataStandardTypeConfigText()
        {
            var entity = new MoreKeyValueEntity();
            entity.KeyXmlNodeName = "key";
            entity.ValXmlNodeName = "value";
            entity.DirectoryName = Path.Combine(GlobalContext.AppEntryAssemblyPath, DBTGlobalValue.ConfigFile_Pre);
            entity.FileName = Path.Combine(GlobalContext.AppEntryAssemblyPath, DBTGlobalValue.Cfg.DBTDataTypeConfigText);
            entity.ColKeys.Add(KeyString.Kid);
            entity.ColKeys.Add(KeyString.Name);
            entity.ColKeys.Add(KeyString.Oracle);
            entity.ColKeys.Add(KeyString.MySql);
            entity.ColKeys.Add(KeyString.SqlServer);
            entity.ColKeys.Add(KeyString.PostgreSql);
            entity.ColKeys.Add(KeyString.SQLite);

            entity.ColVals.Add(ValueString.Vid);
            entity.ColVals.Add(ValueString.Name);
            entity.ColVals.Add(ValueString.Length);

            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Kid = "kid";
            public static readonly string Name = "name";

            public static readonly string Oracle = "oracle";
            public static readonly string MySql = "mysql";
            public static readonly string SqlServer = "sqlserver";
            public static readonly string PostgreSql = "postgresql";
            public static readonly string SQLite = "sqlite";
        }

        public static class ValueString
        {
            public static readonly string Vid = "vid";
            public static readonly string Name = "name";

            public static readonly string Length = "length";
        }
    }
}
