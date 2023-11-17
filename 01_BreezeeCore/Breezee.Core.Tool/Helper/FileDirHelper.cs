using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Breezee.Core.Tool.Helper
{
    /// <summary>
    /// 文件目录辅助类
    /// </summary>
    public class FileDirHelper
    {
        public static void CopyFilesToDirKeepSrcDirName(string srcPath, string destDir, bool isMove = false,bool isOverWrite = true)
        {
            if (Directory.Exists(srcPath))
            {
                DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);
                CopyDirectory(srcPath, destDir + @"\" + srcDirectory.Name, isMove, isOverWrite);
                if (isMove)
                {
                    srcDirectory.Delete();
                }
            }
            else
            {
                CopyFile(srcPath, destDir, isMove, isOverWrite);
            }
        }
        public static void CopyFilesToDir(string srcPath, string destDir, bool isMove = false, bool isOverWrite = true)
        {
            if (Directory.Exists(srcPath))
            {
                CopyDirectory(srcPath, destDir, isMove, isOverWrite);
                if (isMove)
                {
                    new DirectoryInfo(srcPath).Delete();
                }
            }
            else
            {
                CopyFile(srcPath, destDir, isMove, isOverWrite);
            }
        }
        private static void CopyDirectory(string srcDir, string destDir, bool isMove, bool isOverWrite)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(srcDir);
            DirectoryInfo destDirectory = new DirectoryInfo(destDir);

            if (destDirectory.FullName.StartsWith(srcDirectory.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("cannot copy parent to child directory.");
            }

            if (!srcDirectory.Exists)
            {
                return;
            }

            if (!destDirectory.Exists)
            {
                destDirectory.Create();
            }

            FileInfo[] files = srcDirectory.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                CopyFile(files[i].FullName, destDirectory.FullName, isMove, isOverWrite);
            }

            DirectoryInfo[] dirs = srcDirectory.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, destDirectory.FullName + @"\" + dirs[j].Name, isMove, isOverWrite);
                if (isMove)
                {
                    dirs[j].Delete();
                }
            }
        }

        private static void CopyFile(string srcFile, string destDir, bool isMove, bool isOverWrite)
        {
            DirectoryInfo destDirectory = new DirectoryInfo(destDir);
            string fileName = Path.GetFileName(srcFile);
            if (!File.Exists(srcFile))
            {
                return;
            }

            if (!destDirectory.Exists)
            {
                destDirectory.Create();
            }

            string sTargetPath = Path.Combine(destDirectory.FullName, fileName);
            if (File.Exists(sTargetPath))
            {
                if (!isOverWrite) return; //如果不覆盖，那么直接跳过这个文件
                File.SetAttributes(sTargetPath, FileAttributes.Normal); //去除文件的只读属性
            }
            if (isMove)
            {
                File.Move(srcFile, sTargetPath);
            }
            else
            {
                File.Copy(srcFile, sTargetPath, isOverWrite);
            }
        }

        /// <summary>
        /// 下载Web压缩包并解压
        /// </summary>
        /// <param name="sSoureUrl">下载Web源地址</param>
        /// <param name="sLocalDirPath">本地目录</param>
        /// <param name="isDeleteNewVersionZipFile">是否删除新版本的压缩包</param>
        /// <returns></returns>
        public static async Task DownloadWebZipAndUnZipAsync(string sSoureUrl, string sLocalDirPath,bool isDeleteNewVersionZipFile=false)
        {
            //下载地址：https://gitee.com/breezee2000/WorkHelper/releases/download/1.2.24/WorkHelper1.2.24.rar
            Regex regex = new Regex(@"/(\w|\.)+.(rar|zip|7z)", RegexOptions.IgnoreCase); //得到绿色版包名
            MatchCollection mc = regex.Matches(sSoureUrl);
            if (mc.Count > 0)
            {
                string sFileName = mc[0].Value.Substring(1);
                string sDirName = mc[0].Value.Substring(1, sFileName.LastIndexOf("."));
                string sFullZipPath = Path.Combine(sLocalDirPath, sFileName);
                //如已存在，修改压缩包名称：如删除文件，杀毒软件会误报为病毒
                if (File.Exists(sFullZipPath))
                {
                    if (isDeleteNewVersionZipFile)
                    {
                        try
                        {
                            File.Delete(sFullZipPath);
                        }
                        catch(Exception ex)
                        {
                            System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                        }
                    }
                    else
                    {
                        //找出版本号
                        regex = new Regex(@"(\d+.)+", RegexOptions.IgnoreCase);
                        mc = regex.Matches(sFileName);
                        if (mc.Count > 0)
                        {
                            //有版本号
                            string sVersion = mc[0].Value.Substring(0, mc[0].Value.Length - 1);
                            string sSetupName = sFileName.Substring(0, mc[0].Index);
                            string sExtName = sFileName.Substring(mc[0].Index + mc[0].Length);
                            int iNewFile = 1;//从所有父目录查找同名文件还有没有
                            while (File.Exists(Path.Combine(sLocalDirPath, sSetupName + sVersion + "(" + iNewFile + ")." + sExtName)))
                            {
                                iNewFile++;
                            }
                            sFileName = sSetupName + sVersion + "(" + iNewFile + ")." + sExtName;
                            sFullZipPath = Path.Combine(sLocalDirPath, sFileName);
                        }
                        else
                        {
                            //无版本号，直接取压缩包后缀
                            regex = new Regex(@".(rar|zip\7z)$", RegexOptions.IgnoreCase);
                            mc = regex.Matches(sFileName);
                            if (mc.Count > 0)
                            {
                                string sSetupName = sFileName.Substring(0, mc[0].Index);
                                int iNewFile = 1;
                                while (File.Exists(Path.Combine(sLocalDirPath, sSetupName + "(" + iNewFile + ")" + mc[0].Value)))
                                {
                                    iNewFile++;
                                }
                                sFileName = sSetupName + "(" + iNewFile + ")" + mc[0].Value;
                                sFullZipPath = Path.Combine(sLocalDirPath, sFileName);
                            }
                        }
                    }                   
                }
                //下载到临时文件
                var tmpZipName = sFullZipPath + ".tmp";
                using (var web = new WebClient())
                {
                    await web.DownloadFileTaskAsync(sSoureUrl, tmpZipName);
                }

                //重命名文件
                File.Move(tmpZipName, sFullZipPath);
                //解压文件:只针对.zip文件，不能解压.rar格式
                //ZipFile.ExtractToDirectory(sFullZipPath, sLocalDirPath, Encoding.UTF8);
                //解压的文件
                using (Stream stream = File.OpenRead(sFullZipPath))
                {
                    var reader = ReaderFactory.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            Console.WriteLine(reader.Entry.Key);
                            reader.WriteEntryToDirectory(sLocalDirPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }

                //备份数据库文件
                try
                {
                    //SQLite的数据库文件也复制一份到目标目录：如表结构有变更
                    DirectoryInfo dirNow = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                    string dbFile = Path.Combine(dirNow.FullName, "SQLite_DBTool.db");
                    string sNewDir = Path.Combine(sLocalDirPath, sDirName);
                    if (File.Exists(dbFile))
                    {
                        CopyFilesToDirKeepSrcDirName(dbFile, sNewDir);
                    }
                    dbFile = Path.Combine(dirNow.FullName, "SQLite_Mini.db");
                    if (File.Exists(dbFile))
                    {
                        CopyFilesToDirKeepSrcDirName(dbFile, sNewDir);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                }

                //删除新版本压缩包
                if (isDeleteNewVersionZipFile)
                {
                    try
                    {
                        File.Delete(sFullZipPath);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message); //复制文件出错，只在控制台输入错误信息
                    }
                }
            }
        }

        public static string ReadWebText(string sSoureUrl)
        {
            using (var web = new WebClient())
            {
                Stream myStream = web.OpenRead(new Uri(sSoureUrl));
                StreamReader sr = new StreamReader(myStream);
                string sReturn = sr.ReadToEnd();
                myStream.Close();// Close the stream. 
                return sReturn;
            }
        }

    }
}
