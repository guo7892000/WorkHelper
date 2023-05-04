using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Tool.Helper
{
    /// <summary>
    /// 文件目录辅助类
    /// </summary>
    public class FileDirHelper
    {
        public static void CopyFilesToDirKeepSrcDirName(string srcPath, string destDir,bool isMove=false)
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
            if(isMove)
            {
                File.Move(srcFile, destDirectory.FullName + @"\" + fileName);
            }
            else
            {
                File.Copy(srcFile, destDirectory.FullName + @"\" + fileName, true);
            }
        }
    }
}
