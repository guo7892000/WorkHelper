using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 可执行程序辅助类
    /// </summary>
    public class EXEProgramHelper
    {
        /// <summary>
        /// 启动EXE应用程序
        /// </summary>
        /// <param name="ExeFileFullPath"></param>
        /// <param name="ExeArg"></param>
        public static void StartEXEProgram(string ExeFileFullPath, string ExeArg, bool IsWaitForExit = false)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ExeFileFullPath;//@"路径\exe的文件名";
            info.Arguments = ExeArg;
            info.WindowStyle = ProcessWindowStyle.Normal;
            Process pro = Process.Start(info);
            if (IsWaitForExit)
            {
                pro.WaitForExit();
            }
        }
    }
}
