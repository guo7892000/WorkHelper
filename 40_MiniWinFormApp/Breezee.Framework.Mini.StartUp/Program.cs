using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using Breezee.Framework.Mini.Entity;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Breezee.Framework.Mini.StartUp
{
    internal static class Program
    {
        #region 应用程序的主入口点
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //加载应用配置
            WinFormContext.Instance.LoadAppConfig();
            //打开登录界面
            FrmMiniLogin frmLogin = new FrmMiniLogin();
            if (frmLogin.ShowDialog() != DialogResult.OK) return;

            //删除旧版本逻辑：有上次更新路径，且是要删除旧版本
            WinFormConfig  winConfig= WinFormContext.Instance.WinFormConfig;
            string sPrePath = winConfig.Get(GlobalKey.Upgrade_PreVersionPath, "");
            if (!string.IsNullOrEmpty(sPrePath) && "1".Equals(winConfig.Get(GlobalKey.Upgrade_IsDeleteOldVersion, "0")))
            {
                if("1".Equals(winConfig.Get(GlobalKey.Upgrade_IsDeleteOldVersionNeedConfirm, "1")))
                {
                    if(MessageBox.Show("旧版本路径："+ sPrePath + "，确认删除？", "删除旧版本提示", MessageBoxButtons.OKCancel)== DialogResult.OK)
                    {
                        Directory.Delete(sPrePath, true);
                        winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "","清空上个版本文件夹");
                    }
                }
                else
                {
                    Directory.Delete(sPrePath, true);
                    winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "清空上个版本文件夹");
                }
            }

            //创建主窗体
            FrmMiniMainMDI frmMain = new FrmMiniMainMDI();
            WinFormContext.Instance.SetMdiParent(frmMain);
            //全局应用类
            FormApp app = new MiniApp();
            app.SetMain();
            app.LoginForm = frmLogin;
            app.MainForm = frmMain;
            app.Init();
            //运行应用
            Application.Run(frmMain);
        }  
        #endregion

        #region 应用程序错误异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            //Log.FatalError(str);

            //AP出现异常时提示
            if (e.Exception.StackTrace.Contains("SoapHttpClientProtocol"))
            {
                str = "与远程服务器无法连接，请联系管理员！";
            }

            MessageBox.Show(str, "系统错误！", MessageBoxButtons.OK);
        }
        #endregion

        #region 生成自定义异常消息
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
        #endregion
    }
}