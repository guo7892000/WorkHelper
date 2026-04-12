using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 标准列分类配置
    /// </summary>
    public class StandardColumnClassXmlConfig
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public StandardColumnClassXmlConfig(string sFileName)
        {
            var entity = new MoreKeyValueEntity();
            entity.DirectoryName = GlobalContext.PathData();
            entity.FileName = sFileName;
            entity.ColKeys.Add(KeyString.Name);
            entity.ColVals.Add(ValueString.IsSelected);
            entity.ColVals.Add(ValueString.Name);
            entity.ColVals.Add(ValueString.NameCN);
            entity.ColVals.Add(ValueString.NameUpper);
            entity.ColVals.Add(ValueString.NameLower);
            entity.ColVals.Add(ValueString.DataType);
            entity.ColVals.Add(ValueString.DataLength);
            entity.ColVals.Add(ValueString.DataPrecision);
            entity.ColVals.Add(ValueString.DataScale);
            entity.ColVals.Add(ValueString.DataTypeFull);
            entity.ColVals.Add(ValueString.NotNull);
            entity.ColVals.Add(ValueString.Default);
            entity.ColVals.Add(ValueString.KeyType);
            entity.ColVals.Add(ValueString.Comments);
            entity.ColVals.Add(ValueString.Extra);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Name = "class";
        }

        public static class ValueString
        {
            public static readonly string IsSelected = "IsSelect";
            public static readonly string Name = DBColumnSimpleEntity.SqlString.Name;
            public static readonly string NameCN = DBColumnSimpleEntity.SqlString.NameCN;
            public static readonly string NameUpper = DBColumnSimpleEntity.SqlString.NameUpper;
            public static readonly string NameLower = DBColumnSimpleEntity.SqlString.NameLower;
            public static readonly string DataType = DBColumnSimpleEntity.SqlString.DataType;
            public static readonly string DataLength = DBColumnSimpleEntity.SqlString.DataLength;
            public static readonly string DataPrecision = DBColumnSimpleEntity.SqlString.DataPrecision;
            public static readonly string DataScale = DBColumnSimpleEntity.SqlString.DataScale;
            public static readonly string DataTypeFull = DBColumnSimpleEntity.SqlString.DataTypeFull;
            public static readonly string NotNull = DBColumnSimpleEntity.SqlString.NotNull;
            public static readonly string Default = DBColumnSimpleEntity.SqlString.Default;
            public static readonly string KeyType = DBColumnSimpleEntity.SqlString.KeyType;
            public static readonly string Comments = DBColumnSimpleEntity.SqlString.Comments;
            public static readonly string Extra = DBColumnSimpleEntity.SqlString.Extra;
        }
    }
}
