using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.IOC;
using Breezee.Core.Entity;

using System.IO;
using System.Runtime.InteropServices;
using Breezee.Core.Tool;
using System.Xml;
using System.Diagnostics;
using Breezee.Framework.Mini.IBLL;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;

/***************************************************************
 * 对象名称：用户登录界面
 * 对象类别：UI窗体
 * 创建作者：黄国辉
 * 创建日期：2013-2-6
 * 对象说明：实现系统登录
 * 修改历史：
 *      V1.0 新建 hgh 2013-2-6
 * ************************************************************/
namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 用户登录界面
    /// </summary>
    public partial class FrmMiniLogin : BaseForm, IForm
    {
        #region 全局变量
        IMiniLogin lg = ContainerContext.Container.Resolve<IMiniLogin>();
        string _LocalVersion;
        #endregion

        #region 默认构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FrmMiniLogin()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //设置背景颜色为透明
            pnl_All.BackColor = Color.Transparent;
            pnlLogin.BackColor = Color.Transparent;
            
            //设置为无边框窗体
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            if ("1".Equals(LoginConfig.Instance.Get(LoginConfig.LgoinConfigString.IsRememberPwd)))
            {
                this.chbSavePwd.Checked = true;
            }
            else
            {
                this.chbSavePwd.Checked = false;
            }

            cbbUserName.Text = LoginConfig.Instance.Get(LoginConfig.LgoinConfigString.LastLoginUserName);
            string[] users = LoginConfig.Instance.Get(LoginConfig.LgoinConfigString.AutoCompleteUserList).Split(',');
            cbbUserName.AutoCompleteCustomSource.AddRange(users);
            foreach (string str in users)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    cbbUserName.Items.Add(str);
                }
            }
            txbPassword.Text = LoginConfig.Instance.Get(LoginConfig.LgoinConfigString.LastLoginPwd);
            //设置最大的面板背景图片
            this.BackgroundImage = Properties.Resources.WorkHelp_Logo;
            //设置主SQL配置文件路径
            WinFormContext.Instance.SqlMainConfigPath = MiniGlobalValue.Config.SqlMainCofig_Path;
            //设置登录面板的背景图片
            //strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "login", "bg2.png");
            //if (File.Exists(strPath))
            //{
            //    pnlLogin.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "login", "bg2.png"));
            //    pnlLogin.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        #region 登录按钮单击事件
        /// <summary>
        /// 登录按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = cbbUserName.Text.Trim().ToLower();
                string password = txbPassword.Text.Trim();

                if (userName == "")
                {
                    cbbUserName.Focus();
                    return;
                }
                else if (password == "")
                {
                    txbPassword.Focus();
                    return;
                }

                IDictionary<string, string> _dicString = new Dictionary<string, string>();
                _dicString[IMiniLogin.Login_InDicKey.UserCode] = userName;
                //_dicString[Login_InDicKey.UserPassword] = password; //密码不传入
                lblLoginTipInfo.Text = "正在校验用户和密码...";
                lblLoginTipInfo.Refresh();
                //查询用户信息
                IDictionary<string, object> dicResult = lg.Login(_dicString);
                DataTable dtUser = dicResult.SafeGetDictionaryTable();
                if (dtUser == null || dtUser.Rows.Count == 0)
                {
                    ShowInfo("用户名不存在或密码错误！", "登录失败");
                    return;
                }
                //验证密码是否正确
                if (!EncryptHelper.ValidatePassword(password, dtUser.Rows[0][DT_SYS_USER.USER_PASSWORD].ToString()))
                {
                    ShowInfo("用户名不存在或密码错误！", "登录失败");
                    return;
                }

                lblLoginTipInfo.Text = "正在加载用户配置...";
                lblLoginTipInfo.Refresh();

                //更新系统
                //lblLoginTipInfo.Text = "正在检测新版本...";
                //lblLoginTipInfo.Refresh();
                //UpgradeSystem();           
                
                lblLoginTipInfo.Text = "登录成功！";
                lblLoginTipInfo.Refresh();
                //得到用户信息
                Mini_SYS_USER user = dicResult[IMiniLogin.Login_ReturnDicKey.SysUser] as Mini_SYS_USER;
                WinFormContext.Instance.SetLoginUser(user);
                BaseForm._loginUser = new LoginUserInfo(user);
                #region 保存设置信息
                if (chbSavePwd.Checked)
                {
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.LastLoginPwd, txbPassword.Text);
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.IsRememberPwd, "1");
                }
                else
                {
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.LastLoginPwd, "");
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.IsRememberPwd, "0");
                }
                if (ckbAutoLogin.Checked)
                {
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.LastLoginUserName, userName);
                    string sAutoCompleteUserList = LoginConfig.Instance.Get(LoginConfig.LgoinConfigString.AutoCompleteUserList);
                    bool isIncludeName = false;
                    foreach (string str in sAutoCompleteUserList.Split(','))
                    {
                        if (str.ToLower() == userName)
                        {
                            isIncludeName = true;
                            break;
                        }
                        
                    }
                    if (!isIncludeName)
                    {
                        LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.AutoCompleteUserList, sAutoCompleteUserList += userName + ",");
                    }
                }
                else
                {
                    LoginConfig.Instance.Set(LoginConfig.LgoinConfigString.LastLoginUserName, "");
                }

                LoginConfig.Instance.Save(); 
                #endregion

                //关闭本窗体
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message); 
            }
        } 
        #endregion

        #region 用户名按下键事件
        /// <summary>
        /// 用户名按下键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbUserName_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                case Keys.Separator:
                    if (cbbUserName.Text.Trim() != "")
                    {
                        txbPassword.Focus();
                        txbPassword.SelectAll();
                    }
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 密码框按下键事件
        /// <summary>
        /// 密码框按下键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                case Keys.Separator:
                    if ((cbbUserName.Text.Trim() != "") & (txbPassword.Text.Trim() != ""))
                    {
                        this.btnLogin_Click(sender, e);
                    }
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 关闭按钮事件
        private void btnFormClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        } 
        #endregion

        #region 最小化按钮事件
        private void btnFormMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        } 
        #endregion

        #region 设置按钮事件
        private void btnSet_Click(object sender, EventArgs e)
        {

        } 
        #endregion

        #region 鼠标拖动事件
        //导入动态库  
        [DllImport("User32.DLL")]  
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);  
        [DllImport("User32.DLL")]  
        public static extern bool ReleaseCapture();
        //变量
        public const uint WM_SYSCOMMAND = 0x0112;  
        public const int SC_MOVE = 61456;  
        public const int HTCAPTION = 2; 
        //窗体按下鼠标事件（注：还要在窗体的其他控件事件上调用该事件才能实现效果）
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前应用程序释放鼠标捕捉
            ReleaseCapture();
            //发送消息，让系统误以为在标题上按下鼠标
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 0);
        }

        #endregion

        #region 获取本地版本号
        private void ReadLocalVersionNo()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Breezee.Global.AutoUpgrade.CONFIG");
                if (!File.Exists(path))
                {
                    //File.Create(path);
                    return;
                }
                XmlDocument myDoc = new XmlDocument();
                myDoc.Load(path);
                XmlNode _xmlNode = myDoc.SelectSingleNode("/xml/CurrentVersion");
                if (_xmlNode != null)
                {
                    string ver = _xmlNode.InnerText;
                    _LocalVersion = ver;
                }
            }
            catch
            {

            }
        } 
        #endregion

        #region 升级方法
        private void UpgradeSystem()
        {
            ////调用自动更新方法
            //ToolUpgrade tu = new ToolUpgrade();
            //tu.UpgradeTool();
        } 
        #endregion
    }
}
