using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;
using System.Data;
using System.IO;
using System.Reflection;
using Breezee.Core.Interface;
using static System.Windows.Forms.AxHost;
using System.Security.AccessControl;

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
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 功能名称：全局上下文类
    /// 功能说明：保存登录成功后的一些状态信息
    /// 作者：黄国辉
    /// 最后更新日期：2021-10-28
    /// </summary>
    public class WinFormContext
    {
        #region 变量
        public static WinFormContext Instance = new WinFormContext(); //自身的一个静态实例
        private static LoginUserInfo _LoginUser = null;
        //表变量
        private static DataTable _dtUserMenuPriv; //用户的菜单权限清单
        private static DataTable _dtUserButtonPriv; //用户的菜单按钮权限清单
        private static DataTable _dtUserEnviromentConfig; //用户的环境配置清单
        private static DataTable _dtAllMenuButtonList; //所有按钮清单
        private DataTable _dtPendingInfo;//待办信息
        //配置信息
        private static RunningConfig _GlobalConfig = new RunningConfig();//全局配置
        private static UserEnvConfig _UserEnvConfig = new UserEnvConfig();//用户配置
        //用户偏好设置
        private static UserLoveSettings _userLoveSettings;

        private static FtpServer _FteServerDefault = new FtpServer();//默认的FTP服务器
        private static List<FtpServer> _FteServerList = new List<FtpServer>();//FTP服务器列表

        /// <summary>
        /// 是否正在升级
        /// </summary>
        public bool IsUpgradeRunning = false;
        public AppConfigPair AppConfigPair { get; set; }
        public WinFormConfig WinFormConfig { get; set; }

        public Form MdiForm => (Form)mMainForm; //主窗体
        /// <summary>
        /// 临时目录路径
        /// </summary>
        public string TempDirPath => GlobalContext.PathTemp();
        /// <summary>
        /// 网格的Tag历史保存路径
        /// </summary>
        public string DataGridTagHistoryPath => GlobalContext.PathGridStyle();

        //其他变量
        private Form mCurrentForm; //当前窗体
        private IMainForm mMainForm; //主窗体
        private string _userSessionID = Guid.NewGuid().ToString();

        public string mPath; //应用程序目录
        public IDictionary<string, object> mFormUpdateFlag;

        
        public bool IsLoginUseVerificationCode = false;//是否登录使用验证码
        /// <summary>
        /// 版本信息
        /// </summary>
        public string CurrentClientVersion = "";
        /// <summary>
        /// Net版本信息
        /// </summary>
        public string NetVersion = "4";
        /// <summary>
        /// 打开的最大窗体数
        /// </summary>
        public int MaxOpenFormNum = 15;
        /// <summary>
        /// HTML格式的功能帮助清单
        /// </summary>
        public IList<EntMenuHelp> MenuHelpList = new List<EntMenuHelp>();
        /// <summary>
        /// SQL主配置文件路径
        /// </summary>
        public string SqlMainConfigPath = "";
        #endregion

        #region 属性

        #region 用户的菜单按钮权限
        /// <summary>
        /// 用户的菜单按钮权限
        /// </summary>
        public static DataTable UserMenuButtonPriv
        {
            get { return _dtUserButtonPriv; }
            set { _dtUserButtonPriv = value; }
        } 
        #endregion

        #region 所有菜单按钮清单
        /// <summary>
        /// 所有菜单按钮清单
        /// </summary>
        public static DataTable AllMenuButtonList
        {
            get { return _dtAllMenuButtonList; }
            set { _dtAllMenuButtonList = value; }
        } 
        #endregion

        #region 用户Session
        /// <summary>
        /// 用户Session
        /// </summary>
        public string UserSessionID
        {
            get { return _userSessionID; }
        } 
        #endregion

        #region 当前登录用户
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public LoginUserInfo LoginUser
        {
            get { return _LoginUser; }
            internal set { _LoginUser = value; }
        }
        #endregion

        public void SetLoginUser(SYS_USER su)
        {
            _LoginUser = new LoginUserInfo(su);
        }

        #region 当前窗体
        /// <summary>
        /// 当前窗体
        /// </summary>
        public Form CurrentForm
        {
            get { return mCurrentForm; }
            set { mCurrentForm = value; }
        } 
        #endregion

        #region 用户权限菜单
        /// <summary>
        /// 用户权限菜单
        /// </summary>
        public static DataTable MenuPrivilegeDataTable
        {
            get { return _dtUserMenuPriv; }
            set { _dtUserMenuPriv = value; }
        } 
        #endregion

        #region 用户环境
        /// <summary>
        /// 用户环境
        /// </summary>
        public static DataTable DtUserEnviromentConfig
        {
            get { return _dtUserEnviromentConfig; }
            set { _dtUserEnviromentConfig = value; }
        } 
        #endregion

        #region 待办信息
        /// <summary>
        /// 待办信息
        /// </summary>
        public DataTable DtPendingInfo
        {
            get { return _dtPendingInfo; }
            set { _dtPendingInfo = value; }
        } 
        #endregion

        #region 全局配置
        public static RunningConfig GlobalConfig
        {
            get { return _GlobalConfig; }
            set { _GlobalConfig = value; }
        } 
        #endregion

        #region 用户配置
        public static UserEnvConfig UserEnvConfig
        {
            get { return _UserEnvConfig; }
            set { _UserEnvConfig = value; }
        } 
        #endregion

        #region 默认FTP服务器
        public static FtpServer FteServerDefault
        {
            get { return _FteServerDefault; }
            set { _FteServerDefault = value; }
        } 
        #endregion

        #region FTP服务器清单
        public static List<FtpServer> FteServerList
        {
            get { return _FteServerList; }
            set { _FteServerList = value; }
        }

        public static UserLoveSettings UserLoveSettings { get => _userLoveSettings; set => _userLoveSettings = value; }
        #endregion

        #endregion

        #region 构造函数
        private WinFormContext()
        {
            FileInfo fi = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            mPath = fi.Directory.FullName + "\\";

            mFormUpdateFlag = new Dictionary<string, object>();
        }
        #endregion

        #region MDI窗体关闭事件
        public void MdiFormClose(object sender, FormClosedEventArgs args)
        {
            if (sender is Form)
            {
                Form f = sender as Form;
                f.Dispose();
                f = null;
                GC.Collect();
            }
        }
        #endregion

        #region MDI窗体关闭后事件
        public void MdiFormClosed(Object sender, FormClosingEventArgs args)
        {
            if (sender is Form)
            {
                Form mdiForm = sender as Form;

                string sFromKey = mdiForm.GetType().FullName;

                if (mFormUpdateFlag.ContainsKey(sFromKey))
                {
                    string sValue = mFormUpdateFlag[sFromKey].ToString();
                    if (sValue == "True")
                    {
                        DialogResult dr = MessageBox.Show("当前有修改未保存，是否确认关闭?", "提示",MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                        {
                            args.Cancel = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 设置窗体为更新
        public void SetFormUpdate(Form f)
        {
            string sKey = f.GetType().FullName;

            if (mFormUpdateFlag.ContainsKey(sKey))
            {
                mFormUpdateFlag[sKey] = true;
            }
            else
            {
                mFormUpdateFlag.Add(sKey, true);
            }
        }
        #endregion

        #region 设置MDI窗体
        public void SetMdiParent(IMainForm form)
        {
            mMainForm = form;
        }
        #endregion

        #region 调用指定文件中的指定窗体
        /// <summary>
        /// 调用指定文件中的指定窗体
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns>调用成功返回FALSE,否则返回TRUE</returns>
        public bool GoToForm(SYS_MENU menu)
        {
            if (mMainForm.GetChildCount() > MaxOpenFormNum)
            {
                MessageBox.Show("您打开的窗口过多，请关闭其中一些再试。");
                return false;
            }

            if (string.IsNullOrEmpty(menu.DLL_NAME))
            {
                throw new FileLoadException("单击“" + menu.MENU_NAME + "”菜单项（ID=" + menu.MENU_ID.ToString() + "）时，" + this.GetType().Name + "对应的窗体文件（FileID=" + menu.DLL_ID.ToString() + "）的名称不能为空!");
            }
            //判断窗体文件是否存在
            string fullname = mPath + menu.DLL_NAME;
            if (!File.Exists(fullname))
            {
                throw new FileLoadException("单击“" + menu.MENU_NAME + "”菜单项（ID=" + menu.MENU_ID.ToString() + "）时，" + this.GetType().Name + "窗体文件（FileID=" + menu.DLL_ID.ToString() + "）" + fullname + "不存在!", fullname);
            }

            // 设置窗体键值,用于缓存窗体
            string formKey = GetCachedFormKey(menu);

            FileInfo fi = new FileInfo(mPath + menu.DLL_NAME);
            if (!fi.Exists)
            {
                MessageBox.Show("找不到文件" + fi.FullName + "！无法加载程序集！", "找不到文件",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }

            Assembly asm = null;
            //动态载入DLL
            try
            {
                asm = Assembly.LoadFrom(fi.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("从文件中加载程序集出错！\n" + "文件名：" + fi.FullName + "\n详细信息：" + ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            Type myType = null;
            //动态载入DLL
            myType = asm.GetType(menu.FORM_FULL_PATH);

            string strCodeBase = asm.CodeBase;
            string strEscapedCodeBase = asm.EscapedCodeBase;
            if (myType == null)
            {
                string prompt = "从dll中的程序集用反射获取类型时返回null：\n【Assembly】.GetType(“" + menu.FORM_FULL_PATH + "”)！\n"
                    + (menu.FORM_FULL_PATH.Contains(".") ? "" : "\n类型名称应当包括命名空间，但类型名称中并未包含“.”分隔符，请检查是否遗漏了命名空间？？")
                    + "\ndll文件名：（" + (fi.Exists ? "存在" : "不存在") + "）\n    " + fi.FullName
                    + "\n程序集名称：\n    " + asm.FullName
                    + "\n要获取的类型：\n    " + menu.FORM_FULL_PATH
                    + "\n菜单：ID=" + menu.MENU_ID.ToString() + ", Name=" + menu.MENU_NAME
                    + "\n文件：ID=" + menu.DLL_ID.ToString() + ", Name=" + menu.DLL_NAME
                    + "\n窗体：Name=" + menu.FORM_FULL_PATH;

                throw new Exception(prompt);
            }

            object obj;
            //创建对象
            //如果传入参数为空
            if (string.IsNullOrEmpty(menu.MENU_PARM))
            {
                try
                {
                    obj = Activator.CreateInstance(myType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("菜单无参数时，从" + fi.FullName + "中的程序集“" + asm.FullName + "\n中创建类型“" + menu.FORM_FULL_PATH + "”的实例出错！\n" + ex.Message, "创建类型实例出错", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            else
            {//如果不为空
                try
                {
                    //此处添加窗体最大化处理
                    if (menu.MENU_PARM.Trim().ToUpper() == "MAX")
                    {
                        obj = Activator.CreateInstance(myType);
                    }
                    else
                    {
                        obj = Activator.CreateInstance(myType, new object[] { menu.MENU_PARM });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("菜单有参数时，从" + fi.FullName + "中的程序集\n" + asm.FullName + "\n中创建类型“" + menu.FORM_FULL_PATH + "”的实例出错！\n" + ex.Message + "\n菜单的参数是：" + menu.MENU_PARM, "创建类型实例出错",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            //定义待创建窗体
            Form form = null;

            //如果窗体为空
            if (obj == null)
            {
                throw new FileLoadException("窗体加载失败：从" + fi.FullName + "中的程序集\n" + asm.FullName + "\n中创建类型“" + menu.FORM_FULL_PATH + "”的实例失败！返回对象为null！", fi.FullName);
            }
            else if (obj is Form)
            {
                //转化返回对象为窗体
                form = obj as Form;
            }
            else
            {
                string prompt = "窗体加载失败：从" + fi.FullName + "中的程序集\n" + asm.FullName + "\n中创建类型“" + menu.FORM_FULL_PATH + "”的实例失败！\n创建的obj实际是" + obj.GetType().FullName + "类型！";
                Exception ex = new Exception(prompt);
                throw ex;
            }

            //如果主窗体不为空
            if (mMainForm != null && menu.MENU_PARM.Trim().ToUpper() != "MAX")
            {
                //把返回窗体加入主窗体
                mMainForm.AddMdiChilden(form);

                //增加子窗体关闭事件委托
                if (!mFormUpdateFlag.ContainsKey(form.GetType().FullName))
                {
                    mFormUpdateFlag.Add(form.GetType().FullName, false);
                }

                form.FormClosing += new FormClosingEventHandler(MdiFormClosed);
                form.FormClosed += new FormClosedEventHandler(MdiFormClose);

            }
            else
            {
                if (mMainForm != null)
                {
                    form.FormClosed += new FormClosedEventHandler(MdiFormClose);
                }
            }

            //form.CallParameter = menu.Parameter;
            ////设置窗体的权限ID
            //form.PrivID = menu.MenuID;
            form.Tag = menu;
            //设置窗体标题
            form.Text = menu.MENU_NAME;

            #region 窗体按钮的权限处理
            if (_LoginUser.USER_TYPE != "1" && menu.RIGHT_TYPE != "1" && menu.RIGHT_TYPE != "4") //非管理员，且不是授权到菜单，通用功能，才控制按钮权限
            {
                foreach (Control ctrl in form.Controls)
                {
                    #region 窗体内所有控件处理
                    string fieldTypeName = ctrl.GetType().ToString();
                    //如果是Button,取其值，此处需要和BaseForm中的按钮权限控制部分一致
                    switch (fieldTypeName)
                    {
                        //如果是容器类，则递归查找子控件。
                        case "System.Windows.Forms.ToolStrip":
                            ToolStrip ts = ctrl as ToolStrip;
                            GetButton(ts.Items, menu);
                            break;
                        case "System.Windows.Forms.Panel":
                        case "System.Windows.Forms.SplitContainer":
                        case "System.Windows.Forms.TableLayoutPanel":
                        case "System.Windows.Forms.TabControl":
                            GetButton(ctrl.Controls, menu);
                            break;
                        //如果是按钮，则直接取出按钮信息到按钮DataTable中。
                        case "System.Windows.Forms.Button":
                        case "System.Windows.Forms.ToolStripButton":
                            //如果Form下面存在Button ,则取得设置Button 信息
                            SetMenuButtonPriv(menu, ctrl);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }
            #endregion

            //显示窗体
            try
            {
                form.WindowState = FormWindowState.Maximized;
                if (menu.MENU_PARM.Trim().ToUpper() == "MAX")
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("从" + fi.FullName + "中的程序集\n" + asm.FullName + "\n中创建窗体类型“" + menu.FORM_FULL_PATH + "”的实例完毕之后，在显示窗体时出错！\n" + ex.Message + "\n菜单的参数是：" + menu.MENU_PARM, "显示窗体出错",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            mCurrentForm = form;
            return false;
        }
        #endregion

        #region 设置菜单的按钮权限
        /// <summary>
        /// 设置菜单的按钮权限
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="ctrl"></param>
        private static void SetMenuButtonPriv(SYS_MENU menu, Control ctrl)
        {
            if (IsBtnExitButton(ctrl))
            {
                ctrl.Visible = true;
                return;
            }
            DataRow[] drBT = _dtAllMenuButtonList.Select(DT_SYS_MENU.MENU_ID + "='" + menu.MENU_ID + "' AND " + DT_SYS_BUTTON.BUTTON_CODE + "='" + ctrl.Name + "' AND " + DT_SYS_MENU_BUTTON.IS_ENABLED + "='1'");
            if (drBT.Length > 0) //按钮可用
            {
                if (drBT[0][DT_SYS_MENU_BUTTON.IS_GIVE_RIGHT].ToString() == "1") //要授权
                {
                    if (_dtUserButtonPriv.Select(DT_SYS_MENU.MENU_ID + "='" + menu.MENU_ID + "' AND " + DT_SYS_BUTTON.BUTTON_CODE + "='" + ctrl.Name + "' ").Length == 0) //无权限
                    {
                        if (menu.NOT_RIGHT_BUTTON_DISPLAY_TYPE == "1")
                        {
                            ctrl.Enabled = false; //无权限不可用
                        }
                        else
                        {
                            ctrl.Visible = false; //无权限不可见
                        }
                    }
                }
                else //不用授权
                {
                    ctrl.Visible = true;
                }
            }
            else //按钮找不到，那么不显示
            {
                ctrl.Visible = false;
            }
        }
        #endregion

        #region 对ToolStrip控件中按钮的处理
        /// <summary>
        /// 对ToolStrip控件中按钮的处理
        /// </summary>
        /// <param name="tsCollection"></param>
        private void GetButton(ToolStripItemCollection tsCollection, SYS_MENU menu)
        {
            if (tsCollection == null)
            {
                return;
            }

            foreach (ToolStripItem ctrl in tsCollection)
            {
                //如果是按钮
                if (ctrl is ToolStripButton)
                {
                    if (IsTsbExitButton(ctrl as ToolStripButton))
                    {
                        ctrl.Visible = true;
                        break;
                    }
                    DataRow[] drBT = _dtAllMenuButtonList.Select(DT_SYS_MENU.MENU_ID + "='" + menu.MENU_ID + "' AND " + DT_SYS_BUTTON.BUTTON_CODE + "='" + ctrl.Name + "' AND " + DT_SYS_MENU_BUTTON.IS_ENABLED + "='1'");
                    if (drBT.Length > 0) //按钮可用
                    {
                        if (drBT[0][DT_SYS_MENU_BUTTON.IS_GIVE_RIGHT].ToString() == "1") //要授权
                        {
                            if (_dtUserButtonPriv.Select(DT_SYS_MENU.MENU_ID + "='" + menu.MENU_ID + "' AND " + DT_SYS_BUTTON.BUTTON_CODE + "='" + ctrl.Name + "' ").Length == 0) //无权限
                            {
                                if (menu.NOT_RIGHT_BUTTON_DISPLAY_TYPE == "1")
                                {
                                    ctrl.Enabled = false; //无权限不可用
                                }
                                else
                                {
                                    ctrl.Visible = false; //无权限不可见
                                }
                            }
                        }
                        else //不用授权
                        {
                            ctrl.Visible = true;
                        }
                    }
                    else //按钮找不到，那么不显示
                    {
                        ctrl.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 取得每个集合下的按钮
        /// <summary>
        /// 取得每个集合下的按钮
        /// </summary>
        /// <param name="ctrls">控件</param>
        /// <param name="ctrls">当前窗体已存在的按钮信息</param>
        private void GetButton(Control.ControlCollection ctlConnection, SYS_MENU menu)
        {
            // 循环窗体下的每个控件
            foreach (Control ctrl in ctlConnection)
            {
                // 如果是按钮,将其保存下来
                if (ctrl is Button)
                {
                    #region 已取消
                    //if (_dtUserButtonPriv.Select(DT_SYS_MENU.MENU_ID + "='" + menu.MENU_ID + "' AND " + DT_SYS_BUTTON.BUTTON_CODE + "='" + ctrl.Name + "' ").Length == 0)
                    //{
                    //    if (menu.NOT_RIGHT_BUTTON_DISPLAY_TYPE == "1")
                    //    {
                    //        ctrl.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        ctrl.Visible = false;
                    //    }
                    //} 
                    #endregion
                    SetMenuButtonPriv(menu, ctrl);
                }
                else if (ctrl is GroupBox || ctrl is Panel || ctrl is SplitContainer || ctrl is TableLayoutPanel) // 如果是能包含子控件的GroupBox 或Panel控件
                {
                    // 递归调用
                    GetButton(ctrl.Controls, menu);
                }
                else if (ctrl is ToolStrip)
                {
                    ToolStrip ts = ctrl as ToolStrip;
                    GetButton(ts.Items, menu);
                }
                else if (ctrl is TabControl) // 如果是Tab页控件
                {
                    // 循环Tab页控件
                    foreach (TabPage tp in ((TabControl)ctrl).TabPages)
                    {
                        // 递归调用
                        GetButton(tp.Controls, menu);
                    }
                }

            }
        }
        #endregion

        #region 获取缓存值
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private string GetCachedFormKey(SYS_MENU menu)
        {
            string formKey = menu.FORM_FULL_PATH;
            //判断参数是否为空
            if (!string.IsNullOrEmpty(menu.MENU_PARM))
            {
                formKey += "<>" + menu.MENU_PARM.GetHashCode().ToString();
            }
            return formKey;
        }
        #endregion

        #region 初始化登录用户、权限表等信息
        /// <summary>
        /// 初始化登录用户、权限表等信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dtUserButtonPrivList"></param>
        public static void Initialize(LoginUserInfo user, DataTable dtUserButtonPrivList, DataTable mAllButtonTable, DataTable dtUserEnviromentConfig)
        {
            _LoginUser = user;
            _dtUserButtonPriv = dtUserButtonPrivList;
            _dtAllMenuButtonList = mAllButtonTable;
            _dtUserEnviromentConfig = dtUserEnviromentConfig;
        }
        #endregion

        #region 判断是否退出按钮
        private static bool IsBtnExitButton(Control ctrl)
        {
            if (ctrl.Name.ToLower() == "btnexit" || ctrl.Name.ToLower() == "tsbexit"
                || ctrl.Name.ToLower() == "btnclose" || ctrl.Name.ToLower() == "tsbclose")
            {
                return true;
            }
            return false;
        }

        private static bool IsTsbExitButton(ToolStripButton ctrl)
        {
            if (ctrl.Name.ToLower() == "btnexit" || ctrl.Name.ToLower() == "tsbexit"
                || ctrl.Name.ToLower() == "btnclose" || ctrl.Name.ToLower() == "tsbclose")
            {
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 加载应用配置
        /// </summary>
        /// <param name="sNewDir">新目录名</param>
        public void LoadAppConfig(string sNewDir = null)
        {
            //读取应用的配置
            AppConfigPair appConfig = new AppConfigPair(GlobalContext.AppStartConfigPath, GlobalFile.StartUp, XmlConfigSaveType.Attribute);
            //获取用户喜好设置路径
            string sConfigRootPath;
            if (sNewDir == null)
            {
                sConfigRootPath = appConfig.Get(GlobalKey.ConfigPathKey, GlobalContext.AppRootPath);
            }
            else
            {
                sConfigRootPath = sNewDir;
                appConfig.Set(GlobalKey.ConfigPathKey, sNewDir, "个性化配置文件路径");
            }
            GlobalContext.PathConfigRoot(sConfigRootPath);//改变根配置路径
            Instance.AppConfigPair = appConfig;
            WinFormConfig winConfig = new WinFormConfig(GlobalContext.PathConfig(), GlobalFile.FormStyle, XmlConfigSaveType.Attribute);
            Instance.WinFormConfig = winConfig;
            Instance.MaxOpenFormNum = int.Parse(winConfig.Get(GlobalKey.MaxOpenFormNum, "15"));
            appConfig.Save();
            winConfig.Save();

            //读取Net Version
            AppConfigPair versionConfig = new AppConfigPair(GlobalContext.RunPathMiniData(), GlobalFile.NetVersion, XmlConfigSaveType.Attribute);
            Instance.NetVersion = versionConfig.Get("netVersion", "4");
        }

        #region 记录系统日志
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="systemLogType"></param>
        /// <param name="sLogContent"></param>
        /// <param name="sLogTitle"></param>
        /// <param name="isLogDate"></param>
        private void Log(SystemLogTypeEnum systemLogType, string sLogContent, string sLogTitle = "",bool isLogDate=true)
        {
            try
            {
                //日志配置
                bool isLog = WinFormConfig.Get(GlobalKey.GlobalLog_IsEnableLog, "0").Equals("1") ? true : false;
                string sPath = WinFormConfig.Get(GlobalKey.GlobalLog_LogPath, @"\Log");
                int iDays = int.Parse(WinFormConfig.Get(GlobalKey.GlobalLog_KeepDays, "0"));
                string sAppendType = WinFormConfig.Get(GlobalKey.GlobalLog_AppendType, "1");
                if (!isLog) return;

                if (!sPath.EndsWith("\\"))
                {
                    sPath = sPath + "\\";
                }

                string sPre = "";
                switch (systemLogType)
                {
                    case SystemLogTypeEnum.Error:
                        sPath = sPath + "err\\";
                        sPre = "err.";
                        break;
                    case SystemLogTypeEnum.Warn:
                        sPath = sPath + "warn\\";
                        sPre = "warn.";
                        break;
                    case SystemLogTypeEnum.Info:
                    default:
                        sPath = sPath + "info\\";
                        sPre = "info.";
                        break;
                }

                string sLogFileName = sPre + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (sPath.IndexOf(":\\") < 0)
                {
                    sPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sPath;
                }
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                else
                {
                    FileInfo[] arrFiles = new DirectoryInfo(sPath).GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo file in arrFiles)
                    {
                        if (DateTime.Now.Subtract(file.CreationTime.AddDays(iDays)).TotalHours > 0)
                        {
                            file.Delete(); //删除文件
                        }
                    }
                }

                string sFileName = sPath + sLogFileName;
                StringBuilder sb = new StringBuilder();
                if (isLogDate)
                {
                    if (!string.IsNullOrEmpty(sLogTitle))
                    {
                        sb.AppendLine(string.Format("*******************【{0}】【{1}】**************************", sLogTitle, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("*******************【{0}】**************************", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
                
                sb.AppendLine(sLogContent); //记录日志内容             
                sb.Append(Environment.NewLine);

                if (int.Parse(sAppendType) == (int)SqlLogAddType.InsertBegin)
                {
                    string sOldContent = string.Empty;
                    if (File.Exists(sFileName))
                    {
                        sOldContent = File.ReadAllText(sFileName);
                    }
                    File.WriteAllText(sFileName, sb.ToString() + sOldContent, Encoding.UTF8); //最新内容写在前面
                }
                else
                {
                    File.AppendAllText(sFileName, sb.ToString(), Encoding.UTF8); //追加日志内容
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogErr(string sLogContent, string sLogTitle = "", bool isLogDate = true)
        {
            Log(SystemLogTypeEnum.Error, sLogContent, sLogTitle, isLogDate);
        }

        public void LogWarn(string sLogContent, string sLogTitle = "", bool isLogDate = true)
        {
            Log(SystemLogTypeEnum.Warn, sLogContent, sLogTitle, isLogDate);
        }

        public void LogInfo(string sLogContent, string sLogTitle = "", bool isLogDate = true)
        {
            Log(SystemLogTypeEnum.Info, sLogContent, sLogTitle, isLogDate);
        } 
        #endregion

    }
}
