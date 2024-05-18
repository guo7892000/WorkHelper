using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 显示在工具栏上的配置
    /// </summary>
    public class ShowInToolStripXmlConfig
    {
        public KeyValuePairConfig MoreXmlConfig { get; set; }
        public ShowInToolStripXmlConfig(string sFileName)
        {
            MoreXmlConfig = new KeyValuePairConfig(GlobalContext.PathData(), sFileName, XmlConfigSaveType.Attribute);
        }
    }
}
