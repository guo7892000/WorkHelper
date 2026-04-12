using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// Excel复制拼接-字符模板
    /// </summary>
    public class CopyDataStringTemplate
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public CopyDataStringTemplate(string sFileName)
        {
            var entity = new MoreKeyValueEntity();
            entity.DirectoryName = GlobalContext.PathData();
            entity.FileName = sFileName;
            entity.ColKeys.Add(KeyString.Name);
            entity.ColVals.Add(ValueString.TemplateString);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }


        public static class KeyString
        {
            public static readonly string Name = "name";
        }

        public static class ValueString
        {
            public static readonly string TemplateString = "TemplateString";
        }
    }
}
