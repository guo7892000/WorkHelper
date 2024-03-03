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
    public class ReplaceStringXmlConfig
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public ReplaceStringXmlConfig(string sFileName)
        {
            var entity = new MoreKeyValueEntity();
            entity.DirectoryName = GlobalContext.PathData();
            entity.FileName = sFileName;
            entity.ColKeys.Add(KeyString.Name);
            entity.ColVals.Add(ValueString.IsSelected);
            entity.ColVals.Add(ValueString.OldString);
            entity.ColVals.Add(ValueString.NewString);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Name = "name";
        }

        public static class ValueString
        {
            public static readonly string IsSelected = "IsSelect";
            public static readonly string OldString = "OLD";
            public static readonly string NewString = "NEW";
        }
    }
}
