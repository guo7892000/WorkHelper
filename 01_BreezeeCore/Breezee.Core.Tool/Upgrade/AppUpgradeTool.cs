using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Breezee.Core.Tool.Helper
{
    /// <summary>
    /// 应用升级工具
    /// </summary>
    public class AppUpgradeTool
    {
        public static UpgradeFinishData data;
        /// <summary>
        /// 升级配置文件地址
        /// 注：新的使用NewLatestVersion.json；旧的LatestVersion.json不再使用，但仍保留一段时间
        /// </summary>
        public static string UpgradeConfigUrl = "https://gitee.com/breezee2000/WorkHelper/raw/master/NewLatestVersion.json";

        /// <summary>
        /// 设置升级配置文件地址
        /// </summary>
        /// <param name="sUrl"></param>
        public static void SetUpgradeConfigUrl(string sUrl)
        {
            UpgradeConfigUrl = sUrl;
        }

        /// <summary>
        /// 下载Web压缩包并解压
        /// </summary>
        /// <param name="upgradeInfo">升级信息</param>
        /// <returns></returns>
        public static async Task DownloadWebZipAndUnZipAsync(UpgradeInfo upgradeInfo)
        {
            //下载地址：https://gitee.com/breezee2000/WorkHelper/releases/download/1.2.24/WorkHelper1.2.24.rar
            //string sFileName = mc[0].Value.Substring(1);
            //string sDirName = mc[0].Value.Substring(1, sFileName.LastIndexOf("."));
            string sFullZipPath = Path.Combine(upgradeInfo.Client.ParentDir, upgradeInfo.Server.ZipFileName);
            //如已存在，修改压缩包名称：如删除文件，杀毒软件会误报为病毒
            if (File.Exists(sFullZipPath))
            {
                if (upgradeInfo.Client.IsDeleteNewVerZipFile)
                {
                    try
                    {
                        File.Delete(sFullZipPath); // 删除压缩包
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                    }
                }
                else
                {
                    // 不需要删除已有压缩包
                    // 找出版本号
                    Regex regex = new Regex(@"(\d+.)+", RegexOptions.IgnoreCase);
                    MatchCollection mc = regex.Matches(upgradeInfo.Server.ZipFileName);
                    if (mc.Count > 0)
                    {
                        //有版本号：一般是有版本号的
                        string sVersion = mc[0].Value.Substring(0, mc[0].Value.Length - 1);
                        string sSetupName = upgradeInfo.Server.ZipFileName.Substring(0, mc[0].Index);
                        string sExtName = upgradeInfo.Server.ZipFileName.Substring(mc[0].Index + mc[0].Length);
                        int iNewFile = 1;//从所有父目录查找同名文件还有没有
                        while (File.Exists(Path.Combine(upgradeInfo.Client.ParentDir, sSetupName + sVersion + "(" + iNewFile + ")." + sExtName)))
                        {
                            iNewFile++;
                        }
                        string sFileName = sSetupName + sVersion + "(" + iNewFile + ")." + sExtName;
                        sFullZipPath = Path.Combine(upgradeInfo.Client.ParentDir, sFileName);
                    }
                    else
                    {
                        //无版本号，直接取压缩包后缀：这种情况很少
                        regex = new Regex(@".(rar|zip|7z)$", RegexOptions.IgnoreCase);
                        mc = regex.Matches(upgradeInfo.Server.ZipFileName);
                        if (mc.Count > 0)
                        {
                            string sSetupName = upgradeInfo.Server.ZipFileName.Substring(0, mc[0].Index);
                            int iNewFile = 1;
                            while (File.Exists(Path.Combine(upgradeInfo.Client.ParentDir, sSetupName + "(" + iNewFile + ")" + mc[0].Value)))
                            {
                                iNewFile++;
                            }
                            string sFileName = sSetupName + "(" + iNewFile + ")" + mc[0].Value;
                            sFullZipPath = Path.Combine(upgradeInfo.Client.ParentDir, sFileName);
                        }
                    }
                }
            }
            //下载到临时文件
            var tmpZipName = sFullZipPath + ".tmp";
            using (var web = new WebClient())
            {
                /* 只有访问网站提供的发布目录才正常，如访问工程其他文件夹（master或dev分支的），是需要登录的，那样就下载不了发布包了。
                 * 所以为了解决这个问题，只能在多个代码托管服务器发布，从其中任一个下载即可。
                 * 注：gitee上发布包占用的空间是有限的。如果超了，发布包发布不上去的，删除之前的一些发布包，再重新发布即可。
                 */
                await web.DownloadFileTaskAsync(upgradeInfo.Server.DownloadUrl, tmpZipName);
            }
            // 解压文件
            UpgradeFinishData data = new UpgradeFinishData(upgradeInfo.Client.ParentDir, upgradeInfo.Server.AppVersion, sFullZipPath, tmpZipName, upgradeInfo.Client.IsDeleteNewVerZipFile);
            DownFileFinished(data);
        }

        private static void DownFileFinished(UpgradeFinishData data)
        {
            //重命名文件
            File.Move(data.TempZipName, data.FullZipPath);
            // 使用SharpCompress，支持zip、rar、7z
            bool isExtractOk = FileDirHelper.ExtractArchive(data.FullZipPath, data.LocalDirPath);

            if (isExtractOk)
            {
                try
                {
                    File.Delete(data.FullZipPath);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                }
                return;
            }

            //备份数据库文件
            try
            {
                //SQLite的数据库文件也复制一份到目标目录：如表结构有变更
                DirectoryInfo dirNow = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                string dbFile = Path.Combine(dirNow.FullName, "SQLite_DBTool.db");
                string sNewDir = Path.Combine(data.LocalDirPath, data.DirName);
                if (File.Exists(dbFile))
                {
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(dbFile, sNewDir);
                }
                dbFile = Path.Combine(dirNow.FullName, "SQLite_Mini.db");
                if (File.Exists(dbFile))
                {
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(dbFile, sNewDir);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
            }

            //删除新版本压缩包
            if (data.IsDeleteNewVersionZipFile)
            {
                try
                {
                    File.Delete(data.FullZipPath);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                }
            }
        }

        public static string ReadWebText()
        {
            using (var web = new WebClient())
            {
                Stream myStream = web.OpenRead(new Uri(UpgradeConfigUrl));
                StreamReader sr = new StreamReader(myStream);
                string sReturn = sr.ReadToEnd();
                myStream.Close();// Close the stream. 
                return sReturn;
            }
        }

        public class UpgradeFinishData
        {
            public string LocalDirPath { get; set; }

            public UpgradeFinishData(string localDirPath, string dirName, string fullZipPath, string tempZipName, bool isDeleteNewVersionZipFile)
            {
                LocalDirPath = localDirPath;
                DirName = dirName;
                FullZipPath = fullZipPath;
                TempZipName = tempZipName;
                IsDeleteNewVersionZipFile = isDeleteNewVersionZipFile;
            }
            public string DirName { get; set; }
            public string FullZipPath { get; set; }
            public string TempZipName { get; set; }

            public bool IsDeleteNewVersionZipFile { get; set; }
        }

    }
}
