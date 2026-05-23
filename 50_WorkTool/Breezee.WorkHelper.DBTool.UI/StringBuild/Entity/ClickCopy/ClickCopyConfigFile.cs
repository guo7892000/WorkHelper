using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 点击复制配置文件
    /// </summary>
    public class ClickCopyConfigFile
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public MiniXmlConfig XmlConfig { get; }

        public ClickCopyConfigFile() 
        {
            //通用列相关
            List<string> list = new List<string>();
            list.AddRange(new string[] {
                ClickCopyConfigFileStr.Id,
                ClickCopyConfigFileStr.Name,
                ClickCopyConfigFileStr.FilePath,
                ClickCopyConfigFileStr.IsOpenDir,
                ClickCopyConfigFileStr.IsFlowShow,
                ClickCopyConfigFileStr.Text,
            });
            XmlConfig = new MiniXmlConfig(GlobalContext.PathData(), "ClickCopyConfigFileConfig.xml", list, ClickCopyConfigFileStr.Id);
            XmlConfig.Load(); 
        }
    }

    public static class ClickCopyConfigFileStr
    {
        public static string Id = "id"; //ID
        public static string Name = "name"; //名称，下拉框中显示的
        public static string FilePath = "filePath"; //文件路径
        public static string IsOpenDir = "isOpenDir"; //是否点击打开目录：0-否，1-是
        public static string IsFlowShow = "isFlowShow"; //是否流式布局：0-否，1-是
        public static string Text = "text"; //说明
    }

}
