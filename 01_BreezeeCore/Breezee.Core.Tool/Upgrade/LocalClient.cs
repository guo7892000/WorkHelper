using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Tool
{
    /// <summary>
    /// 本地客户端
    /// </summary>
    public class LocalClient
    {
        /// <summary>
        /// 本地版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// App所在目录的父目录
        /// 例如：D:\960_Test\WorkHelpTest\WorkHelper1.3.6.11中的【D:\960_Test\WorkHelpTest】
        /// </summary>
        public string ParentDir { get; set; }

        /// <summary>
        /// 旧版本目录
        /// </summary>
        public string OldDir { get; set; }

        /// <summary>
        /// 是否需要升级
        /// </summary>
        public bool IsNeedUpgrade { get; set; } = false;

        /// <summary>
        /// 是否删除新版本压缩包
        /// </summary>
        public bool IsDeleteNewVerZipFile { get; set; }
    }
}
