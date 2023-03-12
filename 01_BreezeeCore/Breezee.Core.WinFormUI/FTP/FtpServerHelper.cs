using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

using Breezee.Core.Entity;
using Breezee.Core.Tool;
using Breezee.Core.Interface;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// FTP服务辅助类
    /// </summary>
    public class FtpServerHelper
    {
        #region 变量和属性
        FtpServer _ftpServer;//当前FTP服务器信息
        private string _sFtpFilePath = @"Config/Max/Data/FTPServer.config";
        public FtpServer FtpServer
        {
            get { return _ftpServer; }
        } 
        #endregion

        #region 默认构造函数
        public FtpServerHelper()
        { 
            _ftpServer = WinFormContext.FteServerDefault;
        } 
        #endregion

        #region 含参构造函数
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="strConfigCode">数据库的FTP配置编码</param>
        public FtpServerHelper(string strConfigCode)
        {
            bool isExists = false;
            foreach (FtpServer fs in WinFormContext.FteServerList)
            {
                if (fs.ConfigCode == strConfigCode)
                {
                    _ftpServer = WinFormContext.FteServerDefault;
                    isExists = true;
                    break;
                }
            }

            if (!isExists)
            {
                //找不到就抛出异常
                throw new Exception(string.Format("找不到配置编码为{0}的FTP服务器配置！", strConfigCode));
            }
        }
        #endregion

        #region 旧的读取配置文件来得到FTP服务器方法已设为私有（即不再使用）
        string _DefaultFtpServerName = "FTP_DEFAULT";
        string _DefaultFtpUploadDir = "Upload";
        string _DefaultFtpDownloadDir = "Download";

        //public FtpServerHelper()
        //{
        //    _ftpServer = new FtpServer();
        //    SetDefaultFtpServer();
        //} 

        #region 设置FTP服务器
        /// <summary> 
        /// 设置FTP服务器 
        /// </summary> 
        /// <param name="FtpServerIP">FTP连接地址</param> 
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param> 
        /// <param name="FtpUserID">用户名</param> 
        /// <param name="FtpPassword">密码</param> 
        private void SetFtpServer(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, string FtpUploadDir = "Upload", string FtpDownloadDir = "Download", string defaultFtpServerName = "")
        {
            _ftpServer.IPAddr = FtpServerIP;
            _ftpServer.RemotePath = FtpRemotePath;
            _ftpServer.ftpUserID = FtpUserID;
            _ftpServer.ftpPassword = FtpPassword;
            _ftpServer.UploadDir = FtpUploadDir;
            _ftpServer.DownloadDir = FtpDownloadDir;
            if (string.IsNullOrEmpty(defaultFtpServerName))
            {
                _ftpServer.ConfigCode = _DefaultFtpServerName;
            }
            else
            {
                _ftpServer.ConfigCode = defaultFtpServerName;
            }
            if (string.IsNullOrEmpty(FtpRemotePath))
            {
                _ftpServer.ftpURI = "ftp://" + FtpServerIP + "/";
            }
            else
            {
                _ftpServer.ftpURI = "ftp://" + FtpServerIP + "/" + FtpRemotePath + "/";
            }
        }
        #endregion

        #region 设置默认FTP服务器
        /// <summary>
        /// 设置默认FTP服务器
        /// </summary>
        private void SetDefaultFtpServer()
        {
            XmlNodeList xmlList = XmlHelper.GetXmlNodeListByXpath(_sFtpFilePath, @"FTPServer/add[@Name='" + _DefaultFtpServerName + "']");
            XmlNode xn = xmlList.Item(0);

            SetFtpServer(xn.Attributes["ServerIP"].Value, xn.Attributes["RootDir"].Value, xn.Attributes["UserName"].Value, xn.Attributes["Password"].Value,
                xn.Attributes["UploadDir"].Value, xn.Attributes["DownloadDir"].Value, _DefaultFtpServerName);
        }
        #endregion

        #region 根据配置名称获取FTP服务
        /// <summary>
        /// 从Config/Other/FTPServer.config中获取
        /// </summary>
        private void SetFtpServerByConfigName(string ConfigFtpRemoteName)
        {
            XmlNodeList xmlList = XmlHelper.GetXmlNodeListByXpath(_sFtpFilePath, @"FTPServer/add[@Name='" + ConfigFtpRemoteName + "']");
            XmlNode xn = xmlList.Item(0);
            SetFtpServer(xn.Attributes["ServerIP"].Value, xn.Attributes["RootDir"].Value, xn.Attributes["UserName"].Value, xn.Attributes["Password"].Value,
                xn.Attributes["UploadDir"].Value, xn.Attributes["DownloadDir"].Value, ConfigFtpRemoteName);
        }
        #endregion

        #region 根据APP设置获取FTP服务器地址
        /// <summary>
        /// 根据APP设置获取FTP服务器地址
        /// </summary>
        /// <param name="ConfigKey">appSettings中的键名</param>
        private void SetFtpServerByAppSetting(string ConfigKey)
        {
            //读取APP设置
            string strFtpServer = System.Configuration.ConfigurationManager.AppSettings[ConfigKey];
            string[] ftpArr = strFtpServer.Split(new char[] { ',' });

            SetFtpServer(ftpArr[0], ftpArr[1], ftpArr[2], ftpArr[3], _DefaultFtpUploadDir, _DefaultFtpDownloadDir, ConfigKey);
        }
        #endregion 
        #endregion

        #region 上传方法
        /// <summary> 
        /// 上传方法 
        /// </summary> 
        /// <param name="LocalFileFullPathName">文件全路径(含本地路径)</param> 
        public string Upload(string LocalFileFullPathName, string SavePath)
        {
            FileInfo fileInf = new FileInfo(LocalFileFullPathName);
            string SaveFileName = fileInf.Name;
            return Upload(LocalFileFullPathName, SavePath, SaveFileName);
        } 
        #endregion

        #region 上传方法
        /// <summary>
        /// 上传方法
        /// </summary>
        /// <param name="LocalFileFullPathName">本地文件全路径(含文件名)</param>
        /// <param name="SavePath">指定的保存路径</param>
        /// <param name="SaveFileName">保存文件名(不含路径)</param>
        /// <returns></returns>
        public string Upload(string LocalFileFullPathName, string SavePath, string SaveFileName)
        {
            FileInfo fileInf = new FileInfo(LocalFileFullPathName);
            return UploadFile(fileInf,SavePath, SaveFileName);
        }
        #endregion

        #region 上传方法
        /// <summary>
        /// 上传方法
        /// </summary>
        /// <param name="LocalFileFullPathName">本地文件全路径(含文件名)</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="SaveFileName">保存文件名(不含路径)</param>
        /// <returns></returns>
        public string Upload(string LocalFileFullPathName, SystemModelEnum sme)
        {
            FileInfo fileInf = new FileInfo(LocalFileFullPathName);
            string SaveFileName = fileInf.Name;
            return Upload(LocalFileFullPathName, sme, SaveFileName);
        }
        #endregion

        #region 上传方法
        /// <summary>
        /// 上传方法
        /// </summary>
        /// <param name="LocalFileFullPathName">本地文件全路径(含文件名)</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="SaveFileName">保存文件名(不含路径)</param>
        /// <returns></returns>
        public string Upload(string LocalFileFullPathName, SystemModelEnum sme, string SaveFileName)
        {
            FileInfo fileInf = new FileInfo(LocalFileFullPathName);
            string SavePath = "";
            switch (sme)
            {
                case SystemModelEnum.COM:
                    SavePath = "COM";
                    break;
                case SystemModelEnum.NULL:
                    SavePath = "";
                    break;
                default:
                    SavePath = "";
                    break;
            }
            if (string.IsNullOrEmpty(_ftpServer.UploadDir))
            {
                return UploadFile(fileInf, SavePath, SaveFileName);
            }
            else
            {
                return UploadFile(fileInf, _ftpServer.UploadDir + "/" + SavePath, SaveFileName);
            }
        }
        #endregion

        #region 上传文件方法_私有方法
        private string UploadFile(FileInfo fileInf,string SavePath, string SaveFileName)
        {
            string uri = _ftpServer.ftpURI + SavePath + SaveFileName;

            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = true;
            reqFTP.ContentLength = fileInf.Length;
            reqFTP.Proxy = WebRequest.DefaultWebProxy;// GlobalProxySelection.GetEmptyWebProxy();
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            try
            {
                using (FileStream fs = fileInf.OpenRead())
                {
                    using (Stream strm = reqFTP.GetRequestStream())
                    {
                        contentLen = fs.Read(buff, 0, buffLength);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return uri;
        } 
        #endregion

        #region 下载方法
        /// <summary> 
        /// 下载方法 
        /// </summary> 
        /// <param name="saveFilePath"></param> 
        /// <param name="downloadFileName"></param> 
        public Boolean Download(string saveFilePath, string downloadFileName)
        {
            return Download(saveFilePath, downloadFileName, downloadFileName);

            #region 取消
            //Boolean resFlag = false;
            //FtpWebRequest reqFTP;
            //try
            //{
            //    FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

            //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
            //    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            //    reqFTP.UseBinary = true;
            //    reqFTP.UsePassive = true;
            //    reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            //    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            //    Stream ftpStream = response.GetResponseStream();
            //    long cl = response.ContentLength;
            //    int bufferSize = 2048;
            //    int readCount;
            //    byte[] buffer = new byte[bufferSize];

            //    readCount = ftpStream.Read(buffer, 0, bufferSize);
            //    while (readCount > 0)
            //    {
            //        outputStream.Write(buffer, 0, readCount);
            //        readCount = ftpStream.Read(buffer, 0, bufferSize);
            //    }

            //    ftpStream.Close();
            //    outputStream.Close();
            //    response.Close();
            //    resFlag = true;
            //}
            //catch (Exception ex)
            //{
            //    ShowErr(ex.Message);
            //}
            //return resFlag; 
            #endregion
        } 
        #endregion

        #region 下载方法
        /// <summary> 
        /// 下载方法 
        /// </summary> 
        /// <param name="saveFilePath"></param> 
        /// <param name="downloadFileName"></param> 
        public Boolean Download(string saveFilePath, string downloadFileName, string saveFileName)
        {
            Boolean resFlag = false;
            FtpWebRequest reqFTP;
            
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI + downloadFileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                FileStream outputStream = new FileStream(saveFilePath + "\\" + saveFileName, FileMode.OpenOrCreate);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                resFlag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return resFlag;
        } 
        #endregion

        #region 加载FTP图片到图片控件
        /// <summary>
        /// 加载FTP图片到图片控件
        /// </summary>
        /// <param name="ftpFilePath"></param>
        /// <param name="pb"></param>
        /// <param name="IsAutoSize"></param>
        public void LoadFtpPictureToPictureBox(string ftpFilePath, PictureBox pb,bool IsAutoSize)
        {
            if (!IsAutoSize)
            {
                if (string.IsNullOrEmpty(_ftpServer.ftpUserID))
                {
                    pb.LoadAsync(ftpFilePath);
                }
                else
                {
                    string strImagePath = "ftp://" + _ftpServer.ftpUserID + ":" + _ftpServer.ftpPassword + "@" + ftpFilePath.Replace("ftp://", "");
                    pb.LoadAsync(strImagePath);//该方法做不到缩略
                }
            }
            else
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpFilePath));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                Image pic = Image.FromStream(ftpStream);
                pb.Image = PictureHelper.MakeThumbnail(pic, pb.Width, pb.Height, false);
            }

        } 
        #endregion

        #region 删除文件方法
        /// <summary> 
        /// 删除文件方法 
        /// </summary> 
        /// <param name="fileName"></param> 
        public void Delete(string fileName)
        {
            try
            {
                string uri = _ftpServer.ftpURI + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UsePassive = true;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 删除文件夹方法
        /// <summary> 
        /// 删除文件夹方法 
        /// </summary> 
        /// <param name="folderName"></param> 
        public void RemoveDirectory(string folderName)
        {
            try
            {
                string uri = _ftpServer.ftpURI + folderName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                reqFTP.UsePassive = true;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 获取当前目录下明细(包含文件和文件夹) 
        /// <summary> 
        /// 获取当前目录下明细(包含文件和文件夹) 
        /// </summary> 
        /// <returns></returns> 
        public string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;

                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI));
                ftp.UsePassive = true;
                ftp.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                MessageBox.Show(ex.Message);
                return downloadFiles;
            }
        } 
        #endregion

        #region 获取当前目录下文件列表(仅文件) 
        /// <summary> 
        /// 获取当前目录下文件列表(仅文件) 
        /// </summary> 
        /// <returns></returns> 
        public string[] GetFileList(string mask)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.UsePassive = true;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {

                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")
                {
                    MessageBox.Show(ex.Message);
                }
                return downloadFiles;
            }
        } 
        #endregion

        #region 获取当前目录下所有的文件夹列表(仅文件夹)
        /// <summary> 
        /// 获取当前目录下所有的文件夹列表(仅文件夹) 
        /// </summary> 
        /// <returns></returns> 
        public string[] GetDirectoryList()
        {
            string[] drectory = GetFilesDetailList();
            string m = string.Empty;
            foreach (string str in drectory)
            {
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos > 0)
                {
                    /*判断 Windows 风格*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    /*判断 Unix 风格*/
                    string dir = str.Substring(54).Trim();
                    if (dir != "." && dir != "..")
                    {
                        m += dir + "\n";
                    }
                }
            }

            char[] n = new char[] { '\n' };
            return m.Split(n);
        } 
        #endregion

        #region 判断当前目录下指定的子目录是否存在
        /// <summary> 
        /// 判断当前目录下指定的子目录是否存在 
        /// </summary> 
        /// <param name="RemoteDirectoryName">指定的目录名</param> 
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList();
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        } 
        #endregion

        #region 判断当前目录下指定的文件是否存在
        /// <summary> 
        /// 判断当前目录下指定的文件是否存在 
        /// </summary> 
        /// <param name="RemoteFileName">远程文件名</param> 
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        } 
        #endregion

        #region 创建文件夹
        /// <summary> 
        /// 创建文件夹 
        /// </summary> 
        /// <param name="dirName"></param> 
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create. 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 获取指定文件大小
        /// <summary> 
        /// 获取指定文件大小 
        /// </summary> 
        /// <param name="filename"></param> 
        /// <returns></returns> 
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fileSize;
        } 
        #endregion

        #region 改名
        /// <summary> 
        /// 改名 
        /// </summary> 
        /// <param name="currentFilename"></param> 
        /// <param name="newFilename"></param> 
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpServer.ftpURI + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(_ftpServer.ftpUserID, _ftpServer.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 移动文件
        /// <summary> 
        /// 移动文件 
        /// </summary> 
        /// <param name="currentFilename"></param> 
        /// <param name="newFilename"></param> 
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        } 
        #endregion

        #region 切换当前目录
        /// <summary> 
        /// 切换当前目录 
        /// </summary> 
        /// <param name="DirectoryName"></param> 
        /// <param name="IsRoot">true 绝对路径   false 相对路径</param> 
        public void GotoDirectory(string DirectoryName, bool IsRoot)
        {
            if (IsRoot)
            {
                _ftpServer.RemotePath = DirectoryName;
            }
            else
            {
                _ftpServer.RemotePath += DirectoryName + "/";
            }
            _ftpServer.ftpURI = "ftp://" + _ftpServer.IPAddr + "/" + _ftpServer.RemotePath + "/";
        } 
        #endregion
    }
}

