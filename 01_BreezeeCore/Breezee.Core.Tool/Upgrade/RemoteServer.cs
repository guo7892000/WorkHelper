using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Tool
{
    /// <summary>
    /// 远端服务器信息
    /// </summary>
    public class RemoteServer
    {
        /// <summary>
        /// 远端下载包后缀名
        /// 例如：zip或rar
        /// </summary>
        public string ZipFormat { get; set; }

        /// <summary>
        /// 真实的远端下载地址(含压缩包名后缀)
        /// 例如：https://gitee.com/breezee2000/WorkHelper/releases/download/Latest/WorkHelper1.3.6.11.rar
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 应用名和版本号
        /// 例如：WorkHelper1.3.6.11
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// 远端下载包名
        /// 例如：WorkHelper1.3.6.11.zip
        /// </summary>
        public string ZipFileName { get; set; }

        /// <summary>
        /// 临时的远端下载地址(不全)
        /// </summary>
        public string TempDownloadUrl { get; set; }

        /// <summary>
        /// 远端下载包支持的后缀格式数组
        /// 例如：zip|rar|7z
        /// </summary>
        public string[] SupportZipFormats { get; set; }
    }
}
