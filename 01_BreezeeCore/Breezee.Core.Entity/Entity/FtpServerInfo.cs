using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Breezee.Core.Interface;


namespace Breezee.Core.Entity
{
    /// <summary>
    /// FTP服务器信息
    /// </summary>
    public class FtpServerInfo
    {
        private string iPAddr;//IP地址
        private int portNum;//端口号
        private string remotePath;//根目录
        //
        private string ftpUserID;
        private string ftpPassword;
        private FtpProtocolType protocol = FtpProtocolType.FTP;
        //
        private string ftpURI;//IPAddr:PortNum/RemotePath
        private string initDir;
        private string curWorkDir;

        public FtpServerInfo(string iPAddr, int portNum, string ftpUserID, string ftpPassword, FtpProtocolType protocol = FtpProtocolType.FTP, string remotePath = "", string initDir = null)
        {
            this.iPAddr = iPAddr;
            this.portNum = portNum;
            this.remotePath = remotePath;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
            this.protocol = protocol;
            this.InitDir = initDir;
        }

        public string IPAddr { get => iPAddr; set => iPAddr = value; }
        public int PortNum { get => portNum; set => portNum = value; }
        public string RemotePath { get => remotePath; set => remotePath = value; }
        public string FtpUserID { get => ftpUserID; set => ftpUserID = value; }
        public string FtpPassword { get => ftpPassword; set => ftpPassword = value; }
        public FtpProtocolType Protocol { get => protocol; set => protocol = value; }
        public string FtpURI { get => ftpURI; set => ftpURI = value; }
        public string InitDir { get => initDir; set => initDir = value; }
        public string CurWorkDir { get => curWorkDir; set => curWorkDir = value; }
    }

}
