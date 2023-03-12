using Breezee.Core.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class MiniGlobalValue
    {
        public static string AppPath => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";

        public static readonly string DataAccessConfigKey = "IDataAccessMini";

        public static readonly string DBInitializerConfigKey = "IDBInitializerMini";

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
