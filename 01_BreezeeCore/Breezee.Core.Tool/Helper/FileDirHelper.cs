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

    }
}
