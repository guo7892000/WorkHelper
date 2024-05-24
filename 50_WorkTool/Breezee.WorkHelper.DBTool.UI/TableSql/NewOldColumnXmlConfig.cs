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
    /// 新旧表的列关系配置
    /// </summary>
    public class NewOldColumnXmlConfig
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public NewOldColumnXmlConfig(string sFileName)
        {
            var entity = new MoreKeyValueEntity();
            entity.DirectoryName = GlobalContext.PathData();
            entity.FileName = sFileName;
            entity.ColKeys.Add(KeyString.Name);
            entity.ColVals.Add(ValueString.IsSelected);
            entity.ColVals.Add(ValueString.OldTable);
            entity.ColVals.Add(ValueString.OldColumn);
            entity.ColVals.Add(ValueString.NewTable);
            entity.ColVals.Add(ValueString.NewColumn);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Name = "name";
        }

        public static class ValueString
        {
            public static readonly string IsSelected = "IsSelect";
            public static readonly string OldTable = "OldTable";
            public static readonly string OldColumn = "OldColumn";
            public static readonly string NewTable = "NewTable";
            public static readonly string NewColumn = "NewColumn";
        }
    }
}
