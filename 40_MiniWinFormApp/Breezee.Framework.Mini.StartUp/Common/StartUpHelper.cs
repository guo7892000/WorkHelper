using Breezee.Core.Entity;
using Breezee.Framework.Mini.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 启动辅助类
    /// </summary>
    public class StartUpHelper
    {
        /// <summary>
        /// 替换桌面快捷方式
        /// </summary>
        /// <param name="sNewRoot">新应用根目录</param>
        public static void ReplaceDesktopQuickLink(string sNewRoot)
        {
            string sDesktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string[] deskLinks = Directory.GetFiles(sDesktopDir, "*.lnk");
            bool isFound = false;
            if (deskLinks != null && deskLinks.Length > 0)
            {
                foreach (string sLink in deskLinks)
                {
                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(sLink);//获取快捷方式对象
                    if (shortcut.TargetPath.EndsWith(MiniGlobalValue.AppStartUpExeName, StringComparison.OrdinalIgnoreCase))
                    {
                        //分别设置目标路径、工作目录、图标
                        shortcut.TargetPath = Path.Combine(sNewRoot, MiniGlobalValue.AppStartUpExeName);
                        shortcut.WorkingDirectory = sNewRoot;
                        shortcut.IconLocation = Path.Combine(sNewRoot, MiniGlobalValue.AppStartUpIconName);
                        isFound = true;
                        shortcut.Save();
                        break;
                    }
                }
            }
            if (!isFound)
            {
                //创建快捷方式
                string sTarLink = Path.Combine(sDesktopDir, "WorkHelper.lnk");
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(sTarLink);
                //分别设置目标路径、工作目录、图标
                shortcut.TargetPath = Path.Combine(sNewRoot, MiniGlobalValue.AppStartUpExeName);
                shortcut.WorkingDirectory = sNewRoot;
                shortcut.IconLocation = Path.Combine(sNewRoot, MiniGlobalValue.AppStartUpIconName);
                shortcut.Save();
            }
        }
    }
}
