using Breezee.Core;
using Breezee.Core.WinFormUI;
using Breezee.Framework.Mini.Entity;
using System.Text;

namespace Breezee.Framework.Mini.StartUp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            FrmMiniLogin frmLogin = new FrmMiniLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                frmLogin.Dispose();
                FrmMiniMainMDI frmMain = new FrmMiniMainMDI();
                WinFormContext.Instance.SetMdiParent(frmMain);

                FormApp app = new MiniApp();
                app.SetMain();
                app.LoginForm = frmLogin;
                app.MainForm = frmMain;
                app.Init();

                Application.Run(frmMain);
            }
        }

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