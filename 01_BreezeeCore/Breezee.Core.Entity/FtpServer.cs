using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// Ftp服务器类
    /// </summary>
    public class FtpServer
    {
        public string ConfigCode;//配置编码
        public string ConfigName;//配置名称：仅显示用
        public string IPAddr;//IP地址
        public string PortNum;//端口号
        public string RemotePath;//根目录

        public string UploadDir = "Upload";
        public string DownloadDir = "Download";
        public bool IsDefault = false;
        //
        public string ftpUserID;
        public string ftpPassword;
        //
        public string ftpURI;//IPAddr:PortNum/RemotePath

    }
}
