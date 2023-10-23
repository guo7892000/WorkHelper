using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// FTP协议类型
    /// </summary>
    public enum FtpProtocolType
    {
        /// <summary>
        /// FTP文件传输协议（File Transfer Protocol）
        /// </summary>
        FTP = 1,
        /// <summary>
        /// SFTP安全文件传送协议（Secure File Transfer Protocol）
        /// 借助ssl协议加密。ssl是为http/smtp等加密设计的，SSL证书是HTTP明文协议升级HTTPS加密协议的重要渠道，是网络安全传输的加密到通道。
        /// </summary>
        SFTP = 2,
        /// <summary>
        /// FTPS也称作“FTP-SSL”和“FTP-over-SSL”，是一种多传输协议
        /// 相当于加密版的FTP。默认端口号是21。借助ssh加密。ssh是为telnet/ftp等加密、建立传输通道而设计的。
        /// </summary>
        FTPS = 3
    }
}
