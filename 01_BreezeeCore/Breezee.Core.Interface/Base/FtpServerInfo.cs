using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Breezee.Core.Interface;
using static Breezee.Core.Interface.FtpServerInfo;


namespace Breezee.Core.Interface
{
    /// <summary>
    /// FTP服务器信息
    /// </summary>
    public class FtpServerInfo
    {
        #region 基本信息
        private string id;//ID
        private string iPAddr;//IP地址
        private int portNum;//端口号
        private string userID;
        private string password;
        private FtpProtocolType protocol = FtpProtocolType.FTP;
        private string initDir;//初始目录
        //更多配置
        private string configCode;
        private string configName;
        private string connEncodingString;
        private string fileEncodingString;
        private Encoding connEncoding;
        private Encoding fileEncoding;
        #endregion

        #region 下载相关
        private string readDir; //读取目录
        /// <summary>
        /// 下载的本地目录
        /// </summary>
        private string downloadLocalDir;
        private bool downloadIsDeleteDir = true;
        private bool downloadIsExcludeReadDir = true;
        private bool downloadIsAutoExclude = true;
        private string excludeFileList;
        private string excludeDirList;
        #endregion

        #region 上传相关
        private string uploadDir; //上传目录
        private string uploadLocalDir;
        private string uploadBackupDir;
        private FtpUploadStyle uploadStyle;
        private UploadBackupDirStyle uploadBackupDirStyle;

        public string IPAddr { get => iPAddr; set => iPAddr = value; }
        public int PortNum { get => portNum; set => portNum = value; }
        public string UserID { get => userID; set => userID = value; }
        public string Password { get => password; set => password = value; }
        public FtpProtocolType Protocol { get => protocol; set => protocol = value; }
        public string InitDir { get => initDir; set => initDir = value; }
        public string ConfigCode { get => configCode; set => configCode = value; }
        public string ConfigName { get => configName; set => configName = value; }
        public string ConnEncodingString { get => connEncodingString; set => connEncodingString = value; }
        public string FileEncodingString { get => fileEncodingString; set => fileEncodingString = value; }
        public Encoding ConnEncoding { get => connEncoding; set => connEncoding = value; }
        public Encoding FileEncoding { get => fileEncoding; set => fileEncoding = value; }
        public string ReadServerDir { get => readDir; set => readDir = value; }
        public string DownloadLocalDir { get => downloadLocalDir; set => downloadLocalDir = value; }
        public bool DownloadIsDeleteDir { get => downloadIsDeleteDir; set => downloadIsDeleteDir = value; }
        public bool DownloadIsExcludeReadDir { get => downloadIsExcludeReadDir; set => downloadIsExcludeReadDir = value; }
        public bool DownloadIsAutoExclude { get => downloadIsAutoExclude; set => downloadIsAutoExclude = value; }
        public string ExcludeFileList { get => excludeFileList; set => excludeFileList = value; }
        public string ExcludeDirList { get => excludeDirList; set => excludeDirList = value; }
        public string UploadServerDir { get => uploadDir; set => uploadDir = value; }
        public string UploadLocalDir { get => uploadLocalDir; set => uploadLocalDir = value; }
        public string UploadBackupDir { get => uploadBackupDir; set => uploadBackupDir = value; }
        public FtpUploadStyle UploadStyle { get => uploadStyle; set => uploadStyle = value; }
        public UploadBackupDirStyle UploadBackupDirType { get => uploadBackupDirStyle; set => uploadBackupDirStyle = value; }
        public string ID { get => id; set => id = value; }
        #endregion

        public static IList<string> getPropList()
        {
            return PropString.getPropList();
        }

        public static FtpServerInfo getServerInfo(DataRow dr)
        {
            FtpServerInfo ftpServer = new FtpServerInfo();
            ftpServer.ID = dr[PropString.ID].ToString();
            ftpServer.ConfigCode = dr[PropString.ConfigCode].ToString();
            ftpServer.ConfigName = dr[PropString.ConfigName].ToString();
            ftpServer.IPAddr = dr[PropString.IPAddr].ToString();
            ftpServer.UserID = dr[PropString.UserID].ToString();
            ftpServer.Password = dr[PropString.Password].ToString();
            ftpServer.Protocol = (FtpProtocolType)int.Parse(dr[PropString.Protocol].ToString());
            string sPort = dr[PropString.PortNum].ToString();
            if (string.IsNullOrEmpty(sPort))
            {
                ftpServer.PortNum = ftpServer.Protocol == FtpProtocolType.SFTP ? 22 : 21;
            }
            else
            {
                ftpServer.PortNum = int.Parse(sPort);
            }
            ftpServer.ConnEncodingString = dr[PropString.ConnEncodingString].ToString();
            ftpServer.connEncoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);
            ftpServer.FileEncodingString = dr[PropString.FileEncodingString].ToString();
            ftpServer.FileEncoding = BaseFileEncoding.GetEncodingByKey(ftpServer.FileEncodingString);

            ftpServer.ReadServerDir = dr[PropString.ReadServerDir].ToString();
            ftpServer.DownloadLocalDir = dr[PropString.DownloadLocalDir].ToString();
            ftpServer.DownloadIsDeleteDir = dr[PropString.DownloadIsDeleteDir].ToString().Equals("1") ? true : false;
            ftpServer.DownloadIsExcludeReadDir = dr[PropString.DownloadIsExcludeReadDir].ToString().Equals("1") ? true : false;
            ftpServer.DownloadIsAutoExclude = dr[PropString.DownloadIsAutoExclude].ToString().Equals("1") ? true : false;
            ftpServer.ExcludeFileList = dr[PropString.ExcludeFileList].ToString();
            ftpServer.ExcludeDirList = dr[PropString.ExcludeDirList].ToString();

            ftpServer.UploadServerDir = dr[PropString.UploadServerDir].ToString();
            ftpServer.UploadLocalDir = dr[PropString.UploadLocalDir].ToString();
            ftpServer.UploadBackupDir = dr[PropString.UploadBackupDir].ToString();
            ftpServer.UploadStyle = (FtpUploadStyle)int.Parse(dr[PropString.UploadStyle].ToString());
            ftpServer.UploadBackupDirType = (UploadBackupDirStyle)int.Parse(dr[PropString.UploadBackupDirStyle].ToString());

            return ftpServer;
        }

        /// <summary>
        /// 属性字符
        /// </summary>
        public static class PropString
        {
            public static readonly string ID = "ID";
            public static readonly string IPAddr = "IPAddr";
            public static readonly string PortNum = "PortNum";
            public static readonly string UserID = "UserID";
            public static readonly string Password = "Password";
            public static readonly string Protocol = "Protocol";
            public static readonly string InitDir = "InitDir";
            public static readonly string ConfigCode = "ConfigCode";
            public static readonly string ConfigName = "ConfigName";
            public static readonly string ConnEncodingString = "ConnEncodingString";
            public static readonly string FileEncodingString = "FileEncodingString";

            public static readonly string ReadServerDir = "ReadServerDir";
            public static readonly string DownloadLocalDir = "DownloadLocalDir";
            public static readonly string DownloadIsDeleteDir = "DownloadIsDeleteDir";
            public static readonly string DownloadIsExcludeReadDir = "DownloadIsExcludeReadDir";
            public static readonly string DownloadIsAutoExclude = "DownloadIsAutoExclude";
            public static readonly string ExcludeFileList = "ExcludeFileList";
            public static readonly string ExcludeDirList = "ExcludeDirList";

            public static readonly string UploadServerDir = "UploadServerDir";
            public static readonly string UploadLocalDir = "UploadLocalDir";
            public static readonly string UploadBackupDir = "UploadBackupDir";
            public static readonly string UploadStyle = "UploadStyle";
            public static readonly string UploadBackupDirStyle = "UploadBackupDirStyle";

            public static IList<string> getPropList()
            {
                return new List<string>()
                {
                    ID,IPAddr,PortNum,UserID,Password,Protocol,InitDir,ConfigCode,ConfigName,ConnEncodingString,FileEncodingString,
                    ReadServerDir,DownloadLocalDir,DownloadIsDeleteDir,DownloadIsExcludeReadDir,DownloadIsAutoExclude,ExcludeFileList,ExcludeDirList,
                    UploadServerDir,UploadLocalDir,UploadBackupDir,UploadStyle,UploadBackupDirStyle
                };
            }
        }

        public enum FtpUploadStyle
        {
            /// <summary>
            ///  先删后增（先删除整个目录后再新增每个文件）
            /// </summary>
            DeleteAdd = 1,
            /// <summary>
            /// 覆盖（只针对本次上传的文件直接覆盖）
            /// </summary>
            OverWrite = 2,
            /// <summary>
            /// 备份（备份后覆盖上传）
            /// </summary>
            Backup = 3
        }

        public enum UploadBackupDirStyle
        {
            /// <summary>
            ///  备份上传目录
            /// </summary>
            BackupUploadDir = 1,
            /// <summary>
            /// 备份下载目录（即读取目录）
            /// </summary>
            BackupDownloadDir = 2
        }
    }
}
