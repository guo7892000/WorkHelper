using Breezee.Core.Entity;
using Breezee.Core.Interface;
using org.breezee.MyPeachNet;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// SFTP协议工具
    ///  使用开源Renci.SshNet实现
    /// </summary>
    public class SftpTool
    {

        #region SFTP相关的递归方法
        /// <summary>
        /// 递归显示SFTP服务器文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="dt"></param>
        public static void ListDirectory(SftpClient client, string dirName, DataTable dt)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.EndsWith("."))
                    {
                        ListDirectory(client, entry.FullName, dt);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    dt.Rows.Add(dt.Rows.Count + 1, "1", entry.FullName);
                }
            }
        }

        /// <summary>
        /// 递归删除SFTP服务器目录或文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        public static void DeleteDirectoryAndFile(SftpClient client, string dirName)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.EndsWith("."))
                    {
                        DeleteDirectoryAndFile(client, entry.FullName);
                        client.Delete(entry.FullName);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    client.Delete(entry.FullName);
                }
            }
        }

        /// <summary>
        /// 递归下载SFTP服务器目录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="localPath"></param>
        public static void DownSFTPDirectory(SftpClient client, string dirName, string localPath, FtpServerInfo ftpServer,ref int iIdx, List<DownFileInfo> downFileInfos)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.EndsWith("."))
                    {
                        //如果本地目录不存在，则创建
                        string sFullDir = localPath + entry.FullName;
                        if (ftpServer.DownloadIsExcludeReadDir)
                        {
                            sFullDir = localPath + entry.FullName.Replace(ftpServer.ReadServerDir, "");//排除读取路径
                        }
                        if (!Directory.Exists(sFullDir))
                        {
                            Directory.CreateDirectory(sFullDir);
                        }
                        //递归下载文件
                        DownSFTPDirectory(client, entry.FullName, localPath, ftpServer,ref iIdx, downFileInfos);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    string sFullDir = localPath + entry.FullName;
                    string sRelPath = entry.FullName.Replace(ftpServer.ReadServerDir, "");
                    if (ftpServer.DownloadIsExcludeReadDir)
                    {
                        sFullDir = localPath + sRelPath;//排除读取路径
                    }

                    var byt = client.ReadAllBytes(entry.FullName);
                    if (byt.Length > 0)
                    {
                        File.WriteAllBytes(sFullDir, byt);
                    }
                    //将本地文件加到清单中
                    DownFileInfo downFile = new DownFileInfo(iIdx, "1", "文件", sFullDir, sRelPath.Substring(0, sRelPath.LastIndexOf(@"/")));
                    downFileInfos.Add(downFile);
                    iIdx++;
                }
            }
        }

        /// <summary>
        /// 递归备份SFTP服务器文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="TargetPath"></param>
        public static void BackupFilesSFTP(SftpClient client, string dirName, string TargetPath, FtpServerInfo ftpServer)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.EndsWith("."))
                    {
                        //如果本地目录不存在，则创建
                        string sFullDir = "";
                        if (ftpServer.UploadBackupDirType== FtpServerInfo.UploadBackupDirStyle.BackupUploadDir)
                        {
                            sFullDir = TargetPath + entry.FullName.Replace(ftpServer.UploadServerDir, "");
                        }
                        else
                        {
                            sFullDir = TargetPath + entry.FullName.Replace(ftpServer.ReadServerDir, "");
                        }

                        if (!client.Exists(sFullDir))
                        {
                            client.CreateDirectory(sFullDir);
                            client.ChangeDirectory(sFullDir);
                        }
                        else
                        {
                            client.ChangeDirectory(sFullDir);
                        }
                        //递归调用备份目录
                        BackupFilesSFTP(client, entry.FullName, TargetPath, ftpServer);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    //注：这里使用先读取原文件，再写到新位置实现复制
                    string sNewFilePath = "";
                    if (ftpServer.UploadBackupDirType == FtpServerInfo.UploadBackupDirStyle.BackupUploadDir)
                    {
                        //备份上传目录
                        sNewFilePath = entry.FullName.Replace(ftpServer.UploadServerDir, TargetPath);
                    }
                    else
                    {
                        //备份读取目录
                        sNewFilePath = entry.FullName.Replace(ftpServer.ReadServerDir, TargetPath);
                    }
                    string sRead = client.ReadAllText(entry.FullName, ftpServer.FileEncoding);
                    client.WriteAllText(sNewFilePath, sRead);
                    //client.RenameFile(entry.FullName, sNewFilePath); //这个方式相当于剪切文件。注：目录还存在
                }
            }
        }
        #endregion

    }
}
