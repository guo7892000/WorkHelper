using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
        public static void CopyFilesToDirKeepSrcDirName(string srcPath, string destDir, bool isMove = false)
        {
            if (Directory.Exists(srcPath))
            {
                DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);
                CopyDirectory(srcPath, destDir + @"\" + srcDirectory.Name, isMove);
                if (isMove)
                {
                    srcDirectory.Delete();
                }
            }
            else
            {
                CopyFile(srcPath, destDir, isMove);
            }
        }
        public static void CopyFilesToDir(string srcPath, string destDir, bool isMove = false)
        {
            if (Directory.Exists(srcPath))
            {
                CopyDirectory(srcPath, destDir, isMove);
                if (isMove)
                {
                    new DirectoryInfo(srcPath).Delete();
                }
            }
            else
            {
                CopyFile(srcPath, destDir, isMove);
            }
        }
        private static void CopyDirectory(string srcDir, string destDir, bool isMove)
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
                CopyFile(files[i].FullName, destDirectory.FullName, isMove);
            }

            DirectoryInfo[] dirs = srcDirectory.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, destDirectory.FullName + @"\" + dirs[j].Name, isMove);
                if (isMove)
                {
                    dirs[j].Delete();
                }
            }
        }

        private static void CopyFile(string srcFile, string destDir, bool isMove)
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
                File.SetAttributes(sTargetPath, FileAttributes.Normal); //去除文件的只读属性
            }
            if (isMove)
            {
                File.Move(srcFile, sTargetPath);
            }
            else
            {
                File.Copy(srcFile, sTargetPath, true);
            }
        }

        /// <summary>
        /// 下载Web压缩包并解压
        /// </summary>
        /// <param name="sSoureUrl"></param>
        /// <param name="sLocalDirPath"></param>
        /// <returns></returns>
        public static async Task DownloadWebZipAndUnZipAsync(string sSoureUrl, string sLocalDirPath)
        {
            //下载地址：https://gitee.com/breezee2000/WorkHelper/releases/download/1.2.24/WorkHelper1.2.24.rar
            Regex regex = new Regex(@"/(\w|\.)+.(rar|zip|7z)", RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(sSoureUrl);
            if (mc.Count > 0)
            {
                string sFileName = mc[0].Value.Substring(1);
                string sFullZipPath = Path.Combine(sLocalDirPath, sFileName);
                if (!File.Exists(sFullZipPath))
                {
                    //先下载到临时文件
                    var tmp = sFullZipPath + ".tmp";
                    using (var web = new WebClient())
                    {
                        await web.DownloadFileTaskAsync(sSoureUrl, tmp);
                    }
                    //重命名文件
                    File.Move(tmp, sFullZipPath);
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
