using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
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
            //IoC容器的DLL注册类
            IoCDllRegister.Reg(new string[]
            {
                "Breezee.Core.Adapter.BLL",
                "Breezee.AutoSQLExecutor.Common",
                "Breezee.Framework.Mini.BLL",
                "Breezee.Framework.Mini.DAL",
                "Breezee.Framework.Mini.DAL.SQLite",
                "Breezee.WorkHelper.DBTool.BLL",
                "Breezee.WorkHelper.DBTool.DAL",
                "Breezee.WorkHelper.DBTool.DAL.SQLite"
            });
            //加载应用配置
            WinFormContext.Instance.LoadAppConfig();
            //打开登录界面
            FrmMiniLogin frmLogin = new FrmMiniLogin();
            if (frmLogin.ShowDialog() != DialogResult.OK) return;
            //删除旧版本文件
            DeleteOldVersionFile();
            //SQL日志配置取值
            LoadSqlLogConfig();
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

        /// <summary>
        /// SQL日志配置静态类的变量赋值
        /// </summary>
        private static void LoadSqlLogConfig()
        {
            var _WinFormConfig = WinFormContext.Instance.WinFormConfig;
            //正常日志
            SqlLogConfig.IsEnableRigthSqlLog = _WinFormConfig.Get(GlobalKey.OkSqlLog_IsEnableLog, "0").Equals("1") ? true : false;
            SqlLogConfig.RigthSqlLogPath = _WinFormConfig.Get(GlobalKey.OkSqlLog_LogPath, @"\SqlLog\ok");
            SqlLogConfig.RightSqlLogKeepDays = int.Parse(_WinFormConfig.Get(GlobalKey.OkSqlLog_KeepDays, "0"));
            SqlLogConfig.RightSqlLogAddType = "1".Equals(_WinFormConfig.Get(GlobalKey.OkSqlLog_AppendType, "1")) ? SqlLogAddType.InsertBegin : SqlLogAddType.AppendEnd;
            //异常日志
            SqlLogConfig.IsEnableErrorSqlLog = _WinFormConfig.Get(GlobalKey.ErrSqlLog_IsEnableLog, "0").Equals("1") ? true : false;
            SqlLogConfig.ErrorSqlLogPath = _WinFormConfig.Get(GlobalKey.ErrSqlLog_LogPath, @"\SqlLog\err");
            SqlLogConfig.ErrorSqlLogKeepDays = int.Parse(_WinFormConfig.Get(GlobalKey.ErrSqlLog_KeepDays, "0"));
            SqlLogConfig.ErrorSqlLogAddType = "1".Equals(_WinFormConfig.Get(GlobalKey.ErrSqlLog_AppendType, "1")) ? SqlLogAddType.InsertBegin : SqlLogAddType.AppendEnd;
        }
        #endregion

        #region 删除旧版本文件
        private static void DeleteOldVersionFile()
        {
            //删除旧版本逻辑：有上次更新路径，且是要删除旧版本
            WinFormConfig winConfig = WinFormContext.Instance.WinFormConfig;
            string sPrePath = string.Empty;
            try
            {
                sPrePath = winConfig.Get(GlobalKey.Upgrade_PreVersionPath, "");
                string sExePath = Path.GetFullPath(GlobalContext.AppEntryAssemblyPath);
                if (!string.IsNullOrEmpty(sPrePath) && "1".Equals(winConfig.Get(GlobalKey.Upgrade_IsDeleteOldVersion, "1")))
                {
                    string sPreVersionPath = Path.GetFullPath(sPrePath);
                    bool isSaveDir = sExePath.Equals(sPreVersionPath, StringComparison.OrdinalIgnoreCase);

                    if ("1".Equals(winConfig.Get(GlobalKey.Upgrade_IsDeleteOldVersionNeedConfirm, "1")))
                    {
                        if (Directory.Exists(sPrePath) && !isSaveDir)
                        {
                            if (MsgHelper.ShowOkCancel("旧版本路径：" + sPrePath + "，确认删除？") == DialogResult.OK)
                            {
                                Directory.Delete(sPrePath, true);
                                winConfig.Set(GlobalKey.Upgrade_PreVersionPathBak, sPrePath, "备份上个版本文件夹用于下次登录再次删除：上次正常删除");
                                winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "清空上个版本文件夹：正常删除");
                                winConfig.Save();
                            }
                            else
                            {
                                winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "清空上个版本文件夹：用户确认不删除");
                                winConfig.Save();
                            }
                        }
                    }
                    else
                    {
                        if (Directory.Exists(sPrePath) && !isSaveDir)
                        {
                            Directory.Delete(sPrePath, true);
                            winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "清空上个版本文件夹：不用确认直接删除");
                            winConfig.Save();
                        }
                    }
                }
                //再次删除上版本目录：因为第一次删除不能删除根目录，可能是重新登录还是占用了原目录
                string sPrePathBak = winConfig.Get(GlobalKey.Upgrade_PreVersionPathBak, "");
                if (!string.IsNullOrEmpty(sPrePathBak))
                {
                    if (!Directory.Exists(sPrePathBak))
                    {
                        winConfig.Set(GlobalKey.Upgrade_PreVersionPathBak, "", "清空再次删除上个版本文件夹的配置：目录已不存在");
                        winConfig.Save();
                        return;
                    }
                    try
                    {
                        Directory.Delete(sPrePathBak, true);
                        winConfig.Set(GlobalKey.Upgrade_PreVersionPathBak, "", "清空再次删除上个版本文件夹的配置：已再次删除");
                        winConfig.Save();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                winConfig.Set(GlobalKey.Upgrade_PreVersionPathBak, sPrePath, "备份上个版本文件夹用于下次登录再次删除：上次删除出错");
                winConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "清空上个版本文件夹：出错了");
                winConfig.Save();
            }
        } 
        #endregion

        #region 应用程序错误异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            //AP出现异常时提示
            if (e.Exception.StackTrace.Contains("SoapHttpClientProtocol"))
            {
                str = "与远程服务器无法连接，请联系管理员！";
            }
            MessageBox.Show(str, "系统错误！", MessageBoxButtons.OK);
            //写系统日志
            WinFormContext.Instance.LogErr(str, "系统错误",false);
            //错误复制到粘贴板
            Clipboard.SetText(str);
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