using Breezee.Core.Interface;
using Breezee.Core.IOC;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class MiniGlobalValue
    {
        /// <summary>
        /// 启用应用的执行文件名称
        /// </summary>
        public static readonly string AppStartUpExeName = "Breezee.Framework.Mini.StartUp.exe";
        /// <summary>
        /// 启用应用的执行文件图标
        /// </summary>
        public static readonly string AppStartUpIconName = "WorkTool.ico";
        public static string AppPath => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";

        public static readonly string DataAccessConfigKey = "IDataAccessMini";

        public static readonly string DBInitializerConfigKey = "IDBInitializerMini";

        public static readonly string ShowInToolStripXmlConfigFileName = "ShowInToolStripConfig.xml";

        /// <summary>
        /// 加密使用的键：必须24个字符
        /// </summary>
        public static readonly string MiniDesEncryKey = "BrEe%eKiMLVPeaCh*WKTooLG";
        /// <summary>
        /// 加密使用的向量：必须16个字符
        /// </summary>
        public static readonly string MiniDesEncryVector = "PAchVe*&=OEBrMi+";

        /// 配置静态内部类
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// 主SQL配置文件路径
            /// </summary>
            public static readonly string SqlMainCofig_Path = @"Config/SQL.App.Mini.config";
            /// <summary>
            /// 值列表的配置文件路径
            /// </summary>
            public static readonly string KeyValueCofig_Path = @"Config/Mini/Data/KeyValue.xml";

            
        }
    }
}
