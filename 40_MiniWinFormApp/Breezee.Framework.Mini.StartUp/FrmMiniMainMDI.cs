using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using Breezee.Core;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Breezee.Core.Tool.Helper;
using System.Threading.Tasks;
using Breezee.Core.IOC;
using Breezee.Core.Adapter.IBLL;
using System.Net;
using static System.Windows.Forms.LinkLabel;
using Breezee.Framework.Mini.StartUp.Properties;
using org.breezee.MyPeachNet;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 对象名称：MDI主窗体
    /// 对象类型：窗体
    /// 创建日期：2016-10-20
    /// 创建作者：黄国辉
    /// 修改历史：
    ///   2023-08-30 BreezeeHui 增加自动升级
    ///   2023-11-19 BreezeeHui 修改升级下载逻辑，使用有优先级的多个下载路径。
    /// </summary>
    public partial class FrmMiniMainMDI : Form, IMainForm, IForm
    {
        #region 变量
        public event EventHandler<EventArgs> FormClosed;
        private delegate void ShowGlobalMsg(string msg);
        string _strAppPath = AppDomain.CurrentDomain.BaseDirectory;
        //string _strConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkHelper/Config");
        int iStartMenu = 0;
        XmlMenu _xmlMenu;
        IDictionary<string, MenuEntity> _MenuDic;
        ShortCutList _ShortCutMenuList;
        bool IsReLoad = false;
        //升级相关
        bool IsUpgradeColseOldApp = false; //是否升级关闭旧应用
        string sUpgradeNewAppFullPath = ""; //升级后的应用路径
        private delegate void UpgradeSuccessReloadApp();
        private UpgradeSuccessReloadApp UpgradeSuccessReloadAppDeleagte;
        private UpgradeInfo upgradeInfo;
        string _sUpgradeMsg = "升级成功！是否关闭所有窗体，登录新版本？";
        string _sExitMsg = "确定要关闭所有窗体退出吗？";
        string _sDestopLastMsg = string.Empty;

        string MenuXmlFilePath = Path.Combine(GlobalContext.AppEntryAssemblyPath, MiniStaticString.ConfigDataPath, MiniStaticString.MenuFileName);
        public ToolStripStatusLabel StatusBarMessagePanel => throw new NotImplementedException();
        private string _FrameworkHelpPath = "/Help/Html/Mini/WorkHelper.html";
        private string _FrameworkHelpName = "工作助手";
        private WinFormConfig _WinFormConfig;
        private IADPJson _IADPJson;//JSON转换
        ShowInToolStripXmlConfig _showInToolConfig; //工具栏显示
        #endregion

        #region 构造函数
        public FrmMiniMainMDI()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        private void FrmMainMDI_Load(object sender, EventArgs e)
        {
            Text = string.Format("工作助手（WorkHelper） v{0} 正式版  {1}", MiniStartUpAssemblyInfo.AssemblyVersion, MiniStartUpAssemblyInfo.AssemblyDescription);
            _IADPJson = ContainerContext.Container.Resolve<IADPJson>();

            _WinFormConfig = WinFormContext.Instance.WinFormConfig;
            iStartMenu = menuStrip.Items.IndexOfKey(tsbStartMenu.Name);
            this.WindowState = FormWindowState.Maximized;
            tsbAutoGuid.Visible = true;
            tcMenu.Dock = DockStyle.Top;
            _xmlMenu = new XmlMenu(MenuXmlFilePath);

            _showInToolConfig = new ShowInToolStripXmlConfig(MiniGlobalValue.ShowInToolStripXmlConfigFileName);

            this.SetFormBackGroupStyle(_WinFormConfig.Get(GlobalKey.MainSkinType, BaseForm.ChildFormStyleType), _WinFormConfig.Get(GlobalKey.MainSkinValue, BaseForm.ChildFormStyleValue));//设置主窗体样式
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp(_FrameworkHelpPath, _FrameworkHelpName + " > 概述", _FrameworkHelpName));
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp("/Help/Html/Mini/ChangeHistory.html", _FrameworkHelpName + " > 变更历史", _FrameworkHelpName));
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp("/Help/Html/Mini/PracticalFunction.html", _FrameworkHelpName + " > 推荐功能", _FrameworkHelpName));
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp("/Help/Html/Mini/DevelopHelper.html", _FrameworkHelpName + " > 开发说明", _FrameworkHelpName));
            //加载菜单
            LoadMenu();
            //增加快捷菜单
            LoadShortCutMenu();
            //加载用户偏好设定
            LoadUserLoveSettings();
            //默认桌面布满
            pnlDesktop.Dock = DockStyle.Fill;
            txbMenuPath.BackColor = txbMenuPath.Parent.BackColor;
            txbGlobalMsg.BackColor = txbMenuPath.Parent.BackColor;
            //设置菜单查找数据源
            tstbMenuSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tstbMenuSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            tvLeftMenu.ExpandAll();
            tsslUserInfo.Text = WinFormContext.Instance.LoginUser.USER_CODE;
            //绑定事件
            tsbRestartMenu.Click += tsbRestartMenu_Click;
            tsmiLock.Click += tsmiLock_Click;
            tsmiChangePassword.Click += tsmiChangePassword_Click;
            //自动检测是否有新版本
            UpgradeSuccessReloadAppDeleagte += UpgradeReloadApp;
            if ("1".Equals(_WinFormConfig.Get(GlobalKey.Upgrade_IsAutoCheckVersion, "1")))
            {
                Task.Run(() => UpgradeSystem(false));
            }
            // 设置样式
            FormBackGroupStyleEntity styleEntity = this.SetFormBackGroupStyle(_WinFormConfig.Get(GlobalKey.MainSkinType, BaseForm.ChildFormStyleType), _WinFormConfig.Get(GlobalKey.MainSkinValue, BaseForm.ChildFormStyleValue));//设置主窗体样式
            if (!styleEntity.IsUseImage)
            {
                txbMenuPath.BackColor = styleEntity.BackGroupColor;
                txbGlobalMsg.BackColor = styleEntity.BackGroupColor;
            }
            tableLayoutPanel1.BackgroundImage = null;
        }
        #endregion

        #region 加载菜单方法
        private void LoadMenu()
        {
            //加载菜单
            tvLeftMenu.Nodes.Clear();
            //XML中的菜单处理
            _MenuDic = _xmlMenu.GetAllMenu();
            
            foreach (MenuEntity dMenu in _MenuDic.Values)
            {
                if (dMenu.MenuType != MenuType.Modul)
                {
                    continue;
                }
                //菜单项
                ToolStripMenuItem tmiNew = new ToolStripMenuItem();
                tmiNew.Name = dMenu.Guid;
                tmiNew.Text = string.IsNullOrEmpty(dMenu.ShortCutKey)? dMenu.Name: string.Format("{0}(&{1})", dMenu.Name, dMenu.ShortCutKey);
                tmiNew.Tag = dMenu;
                dMenu.FullPath = dMenu.Name;
                menuStrip.Items.Insert(iStartMenu + 1, tmiNew);

                //左边树
                TreeNode tnNew = new TreeNode();
                tnNew.Name = dMenu.Guid;
                tnNew.Text = dMenu.Name;
                tnNew.Tag = dMenu;
                tvLeftMenu.Nodes.Add(tnNew);

                foreach (MenuEntity childMenu in dMenu.Childs.Values)
                {
                    if (childMenu.MenuType == MenuType.Class)
                    {
                        AddMenuClassItem(tmiNew,new ToolStripMenuItem(), childMenu);
                        AddMenuClassNode(tnNew,new TreeNode(), childMenu);
                    }
                    else
                    {
                        AddMenuItem(tmiNew, new ToolStripMenuItem(), childMenu);
                        AddMenuNode(tnNew,new TreeNode(), childMenu);
                    }
                }

                iStartMenu++;
            }
        }

        /// <summary>
        /// 动态工具栏点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            ToolStripMenuItem toolStripMenuItem = button.Tag as ToolStripMenuItem;
            toolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// 增加工具栏按钮
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="childMenu"></param>
        private void AddMainToolStrip(ToolStripMenuItem menuItem,MenuEntity childMenu)
        {
            //工具栏
            if (childMenu.IsShowInToolStrip)
            {
                int iToolIndex = tspMain.Items.IndexOf(saveToolStripButton);
                ToolStripButton button = new ToolStripButton();
                button.Tag = menuItem;
                button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                button.Text = childMenu.Name;
                button.Image = Resources.loveStar;
                button.ToolTipText = childMenu.Name;
                tspMain.Items.Insert(iToolIndex, button);
                button.Click += DynamicToolStripButton_Click;//点击时调用原菜单项点击事件打开功能
            }
        }
        #endregion

        #region 增加菜单分类
        private void AddMenuClassItem(ToolStripMenuItem tmiParent, ToolStripMenuItem tmiNew, MenuEntity dParentMenu)
        {
            AddMenuItem(tmiParent, tmiNew, dParentMenu);
            foreach (MenuEntity childMenu in dParentMenu.Childs.Values)
            {
                if (childMenu.MenuType == MenuType.Class)
                {
                    AddMenuClassItem(tmiNew, new ToolStripMenuItem(), childMenu);
                }
                else
                {
                    var childMenuItem = new ToolStripMenuItem();
                    AddMenuItem(tmiNew, childMenuItem, childMenu);
                    if (!string.IsNullOrEmpty(childMenu.HelpPath))
                    {
                        WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp(childMenu.HelpPath, childMenu.FullPath, childMenu.Name));
                    }

                    //判断菜单ID的工具栏显示配置是否为1是
                    var menShowInTool = _showInToolConfig.MoreXmlConfig.ListEntity.Where(x => x.Key.Equals(childMenu.Guid, StringComparison.OrdinalIgnoreCase) && "1".Equals(x.Value));
                    if (menShowInTool.Count() > 0)
                    {
                        childMenu.IsShowInToolStrip = true;
                        AddMainToolStrip(childMenuItem, childMenu);//增加工具栏按钮
                    }
                }
            }
        }

        private void AddMenuClassNode(TreeNode tnParent, TreeNode tnNew, MenuEntity dParentMenu)
        {
            AddMenuNode(tnParent, tnNew, dParentMenu);
            foreach (MenuEntity childMenu in dParentMenu.Childs.Values)
            {
                if (childMenu.MenuType == MenuType.Class)
                {
                    AddMenuClassNode(tnNew, new TreeNode(), childMenu);
                }
                else
                {
                    AddMenuNode(tnNew, new TreeNode(), childMenu);
                }
            }
        } 
        #endregion

        #region 增加菜单
        private void AddMenuItem(ToolStripMenuItem tmiParent, ToolStripMenuItem tmiNew, MenuEntity dMenu)
        {
            tmiNew.Text = string.IsNullOrEmpty(dMenu.ShortCutKey) ? dMenu.Name : string.Format("{0}(&{1})", dMenu.Name, dMenu.ShortCutKey);
            tmiNew.Name = dMenu.Guid;
            tmiNew.Tag = dMenu;
            tmiNew.Click += MenuItem_Click;
            
            //菜单查找自动完成数据源
            tstbMenuSearch.AutoCompleteCustomSource.Add(dMenu.Name);
            tmiParent.DropDownItems.Add(tmiNew);
        }

        private void AddMenuNode(TreeNode tnParent, TreeNode tnNew, MenuEntity dMenu)
        {
            tnNew.Text = dMenu.Name;
            tnNew.Tag = dMenu;
            tnParent.Nodes.Add(tnNew);
        } 
        #endregion
        
        #region 加载快捷菜单
        private void LoadShortCutMenu()
        {
            string sPathConfig = GlobalContext.PathConfig();
            if (!Directory.Exists(sPathConfig))
            {
                Directory.CreateDirectory(sPathConfig); 
            }
            string strShortCutFilePath = Path.Combine(sPathConfig, MiniStaticString.ShortCutMenuFileName);
            if (!File.Exists(strShortCutFilePath))
            {
                XmlDocument xmlShortCut = new XmlDocument();
                XmlElement xmRoot = xmlShortCut.CreateElement("xml");
                xmlShortCut.AppendChild(xmRoot);
                xmlShortCut.Save(strShortCutFilePath);
                //
                _ShortCutMenuList = new ShortCutList();
                _ShortCutMenuList.AddShortCutItem += AddShortCutMenuItem;//新增快捷菜单事件
                _ShortCutMenuList.Dock = DockStyle.Fill;
                pnlDesktop.Controls.Add(_ShortCutMenuList);
            }
            else
            {
                XmlDocument xmlMenu = new XmlDocument();
                xmlMenu.Load(strShortCutFilePath);
                XmlNodeList xmlList = xmlMenu.SelectNodes("xml/Menu");
                _ShortCutMenuList = new ShortCutList();
                _ShortCutMenuList.AddShortCutItem += AddShortCutMenuItem;//新增快捷菜单事件

                foreach (XmlNode xnModel in xmlList)
                {
                    if (xnModel.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }
                    string strGuid = xnModel.GetAttributeValue("Guid");
                    if (_MenuDic.ContainsKey(strGuid))
                    {
                        ShortCutItem scItem = new ShortCutItem(_MenuDic[strGuid]);
                        scItem.ShortCutItemClick += ShortCutMenuItem_Click;//点击快捷菜单事件
                        scItem.ShortCutItemCancel += CancelShortCutMenuItem_Click;//取消快捷菜单事件
                        scItem.ShortCutItemoMoveFirst += MoveFirstShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveLast += MoveLastShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveBefore += MoveBeforShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveBack += MoveNextShortCutMenuItem_Click;
                        _ShortCutMenuList.AddItem(scItem);
                    }
                }
                //
                _ShortCutMenuList.Dock = DockStyle.Fill;
                pnlDesktop.Controls.Add(_ShortCutMenuList);

            }
        }
        #endregion

        private void LoadUserLoveSettings()
        {
            UserLoveSettings miniXmlConfig = new UserLoveSettings(GlobalContext.PathConfig(), MiniStaticString.UserLoveSettings, XmlConfigSaveType.Attribute);
            WinFormContext.UserLoveSettings = miniXmlConfig;
        }

        #region 修改密码
        /// <summary>
        /// 用户信息设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            FrmChangePassword frm = new FrmChangePassword();
            frm.ShowDialog();
        }
        #endregion

        #region 退出系统
        private void tsbExitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region 工具栏可见菜单事件
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tspMain.Visible = toolBarToolStripMenuItem.Checked;
        } 
        #endregion

        #region 状态栏可见菜单事件
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }
        #endregion

        #region 重新排列窗体
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        } 
        #endregion

        #region 关闭所有窗体
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region 显示全局提示代理方法
        private void ShowGlobalMsg_Click(object sender, ShowGlobalMsgEventArgs e)
        {
            if (txbGlobalMsg.InvokeRequired)
            {
                ShowGlobalMsg showGlobalMsg = new ShowGlobalMsg(SetGlobalMsg);
                txbGlobalMsg.Invoke(showGlobalMsg, new object[] { e.Msg }); //跨线程调用时，要使用委托赋值
            }
            else
            {
                SetGlobalMsg(e.Msg);
            }
        }

        private void SetGlobalMsg(string msg)
        {
            txbGlobalMsg.Text = msg;
        }
        #endregion

        #region 升级后重启应用代理方法
        private void UpgradeReloadApp()
        {
            if (WinFormContext.Instance.MdiForm.InvokeRequired)
            {
                UpgradeSuccessReloadApp reloadApp = new UpgradeSuccessReloadApp(ReloadApp);
                WinFormContext.Instance.MdiForm.Invoke(reloadApp); //跨线程调用时，要使用委托执行关闭应用
            }
            else
            {
                ReloadApp();
            }
        }

        private void ReloadApp()
        {
            WinFormContext.Instance.MdiForm.Close();
        } 
        #endregion

        #region 重新启动
        private void tsbRestartMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            if (IsReLoad)
            {
                this.Dispose();
                Application.Restart();
            }
        }
        #endregion

        #region 锁定系统事件
        /// <summary>
        /// 锁定系统事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLock_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("确定要锁定系统？", "温馨提示", MyButtons.OKCancel) == DialogResult.Cancel)
                return;
            LockForm f = new LockForm();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }
        #endregion

        #region 窗体关闭中事件
        private void FrmMainMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            string sTipInfo = IsUpgradeColseOldApp ? _sUpgradeMsg : _sExitMsg;
            if (MsgHelper.ShowOkCancel(sTipInfo) == DialogResult.Cancel)
            {
                e.Cancel = true; 
                IsReLoad = false;
                IsUpgradeColseOldApp = false; //取消了升级后重新登录，这里把其设置为false。
                return;
            }
            //重新登录或升级登录新版本
            e.Cancel = false;
            if (IsUpgradeColseOldApp)
            {
                EXEProgramHelper.StartEXEProgram(sUpgradeNewAppFullPath, "");//运行新版本
                IsUpgradeColseOldApp = false;
            }
            else
            {
                IsReLoad = true;
            }

            if (FormClosed != null)
            {
                FormClosed(this, e);
            }
        } 
        #endregion

        #region 打开窗体方法
        private void OpenMenu(MenuEntity dOpenMenu, bool IsExpandTreeNode)
        {
            if (dOpenMenu == null || dOpenMenu.MenuType != MenuType.Menu)
            {
                return;
            }

            //判断窗体是否已经打开
            foreach (Form frm in MdiChildren)
            {
                MenuEntity dMenuFrm = frm.Tag as MenuEntity;
                //使用同菜单不同窗体GUID
                if (dMenuFrm.SameMenuNewFormGuid.Equals(dOpenMenu.SameMenuNewFormGuid))
                {
                    MenuEntity selectMenu = tcMenu.SelectedTab.Tag as MenuEntity;
                    //选中页签
                    if (tcMenu.SelectedTab == tpgDesktop || selectMenu.SameMenuNewFormGuid != dMenuFrm.SameMenuNewFormGuid)
                    {
                        tcMenu.SelectedTab = tcMenu.TabPages[dMenuFrm.SameMenuNewFormGuid];
                    }
                    txbMenuPath.Text = dOpenMenu.FullPath;
                    pnlDesktop.Hide();
                    frm.Activate();
                    WinFormContext.Instance.CurrentForm = frm;
                    //全局提示信息显示为当前菜单的提示信息
                    if(frm is BaseForm)
                    {
                        txbGlobalMsg.Text = ((BaseForm)frm).LastestTipMsg;
                    }
                    return;
                }
            }

            if (this.GetChildCount() > WinFormContext.Instance.MaxOpenFormNum - 1)
            {
                MessageBox.Show(string.Format("您打开的窗口超过了最大配置数{0}，不能再打开更多窗体。你可以在【开始】->【环境设置】中修改！",WinFormContext.Instance.MaxOpenFormNum), "温馨提示");
                return;
            }

            if (IsExpandTreeNode)
            {
                OpenTreeNodeMenu(dOpenMenu.Name);
            }

            //克隆一个新的菜单对象
            MenuEntity dMenu = dOpenMenu.Clone();
            //反射得到窗体
            Assembly dll = Assembly.LoadFile(Path.Combine(_strAppPath, dMenu.DLLName));
            object form = dll.CreateInstance(dMenu.FormName);
            if (form is Form)
            {
                Form newForm = form as Form;
                newForm.Tag = dMenu;
                newForm.MdiParent = this;
                newForm.WindowState = FormWindowState.Maximized;
                newForm.Activated += ChildForm_Active;
                newForm.FormClosed += MdiChild_Close;
                //增加页签：使用同菜单不同窗体GUID
                tcMenu.TabPages.Add(dMenu.SameMenuNewFormGuid, dMenu.Name);
                tcMenu.TabPages[dMenu.SameMenuNewFormGuid].Tag = dMenu;
                tcMenu.SelectedTab = tcMenu.TabPages[dMenu.SameMenuNewFormGuid];
                txbMenuPath.Text = dMenu.FullPath;
                
                newForm.Show();
                WinFormContext.Instance.CurrentForm = newForm;
                txbMenuPath.BackColor = txbMenuPath.Parent.BackColor;
                //给窗体加上统一显示提示信息事件委托
                if (newForm is BaseForm)
                {
                    BaseForm fr = newForm as BaseForm;
                    fr.MainFormMode = MainFormModelEnum.Mini;
                    fr.MenuName = dMenu.Name;
                    fr.ShowGlobalMsg += ShowGlobalMsg_Click;//显示全局提示信息
                }
            }
            else
            {
                MsgHelper.ShowErr("配置错误，【" + dMenu.Name + "】菜单不是窗体类型！");
            }
        } 
        #endregion

        #region 菜单项点击事件
        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiClick = sender as ToolStripMenuItem;
            OpenMenu(tsmiClick.Tag as MenuEntity, true);
        }
        #endregion

        #region 快捷菜单项点击事件
        private void ShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            OpenMenu(e.Menu, true);
        }
        #endregion

        #region 取消快捷菜单项点击事件
        private void CancelShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.Cancel);
        }
        #endregion

        #region 增加快捷菜单项点击事件
        private void AddShortCutMenuItem(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.New);
        }
        #endregion

        #region 移动快捷菜单项事件
        /// <summary>
        /// 移到首位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveFirstShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.MoveFirst);
        }
        /// <summary>
        /// 移到最后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveLastShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.MoveLast);
        }
        /// <summary>
        /// 前移一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveBeforShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.MoveBefore);
        }
        /// <summary>
        /// 后移一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveNextShortCutMenuItem_Click(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, ShortMenuEventEnum.MoveNext);
        }
        #endregion

        #region 保存快捷菜单配置
        private void SaveShortCutMenuConfig(MenuEntity dMenu, ShortMenuEventEnum menuEventEnum)
        {
            string strShortCutFilePath = Path.Combine(GlobalContext.PathConfig(), MiniStaticString.ShortCutMenuFileName);
            XmlDocument xmlMenu = new XmlDocument();
            xmlMenu.Load(strShortCutFilePath);
            XmlNodeList xmlList = xmlMenu.SelectNodes("xml/Menu");
            XmlNode xnRemove = xmlMenu.SelectSingleNode("xml/Menu[@Guid='" + dMenu.Guid + "']");
            if (xnRemove != null)
            {
                if (menuEventEnum == ShortMenuEventEnum.Cancel)
                {
                    xnRemove.ParentNode.RemoveChild(xnRemove);
                    //从快捷菜单中移除
                    if (_ShortCutMenuList.ItemList.ContainsKey(dMenu.Guid))
                    {
                        _ShortCutMenuList.MenuListPanl.Controls.Remove(_ShortCutMenuList.ItemList[dMenu.Guid]);
                        _ShortCutMenuList.ItemList.Remove(dMenu.Guid);

                    }
                }
            }
            else
            {
                if (menuEventEnum== ShortMenuEventEnum.New)
                {
                    XmlElement xnNew = xmlMenu.CreateElement("Menu");
                    xnNew.SetAttribute("Guid", dMenu.Guid);
                    xnNew.SetAttribute("Name", dMenu.Name);
                    xmlMenu.DocumentElement.AppendChild(xnNew);

                    if (!_ShortCutMenuList.ItemList.ContainsKey(dMenu.Guid))
                    {
                        ShortCutItem scItem = new ShortCutItem(dMenu);
                        scItem.ShortCutItemClick += ShortCutMenuItem_Click;//点击快捷菜单事件
                        scItem.ShortCutItemCancel += CancelShortCutMenuItem_Click;//取消快捷菜单事件
                        scItem.ShortCutItemoMoveFirst += MoveFirstShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveLast += MoveLastShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveBefore += MoveBeforShortCutMenuItem_Click;
                        scItem.ShortCutItemoMoveBack += MoveNextShortCutMenuItem_Click;
                        _ShortCutMenuList.AddItem(scItem);
                    }
                }
            }

            #region 快捷方式的位置调整
            if (!(menuEventEnum == ShortMenuEventEnum.New || menuEventEnum == ShortMenuEventEnum.Cancel))
            {
                int iIndex = 0;
                Control control = null;
                bool canSet = false;
                if (menuEventEnum == ShortMenuEventEnum.MoveFirst)
                {
                    // 设置当前控件索引=0
                    _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(_ShortCutMenuList.ItemList[dMenu.Guid], 0);                  

                    foreach (Control item in _ShortCutMenuList.MenuListPanl.Controls)
                    {
                        ShortCutItem curShortCutItem = (ShortCutItem)item;
                        if (curShortCutItem.Menu.Guid.Equals(dMenu.Guid))
                        {
                            // 调整XML配置文件中内容位置
                            xmlMenu.DocumentElement.InsertBefore(xnRemove, xnRemove.ParentNode.FirstChild);
                            continue;
                        }
                        // 所有控件索引+1
                        _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(item, _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item) + 1);
                    }
                }
                else if (menuEventEnum == ShortMenuEventEnum.MoveLast)
                {

                    foreach (Control item in _ShortCutMenuList.MenuListPanl.Controls)
                    {
                        iIndex = _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item);
                    }
                    // 设置当前控件索引=最后控件的索引+1
                    _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(_ShortCutMenuList.ItemList[dMenu.Guid], iIndex + 1);
                    xmlMenu.DocumentElement.InsertAfter(xnRemove, xnRemove.ParentNode.LastChild); // 调整XML配置文件中内容位置
                }
                else if (menuEventEnum == ShortMenuEventEnum.MoveBefore)
                {
                    foreach (Control item in _ShortCutMenuList.MenuListPanl.Controls)
                    {
                        ShortCutItem curShortCutItem = (ShortCutItem)item;
                        if (curShortCutItem.Menu.Guid.Equals(dMenu.Guid))
                        {
                            // 交换索引值
                            _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(control, _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item));
                            _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(item, iIndex);
                            xmlMenu.DocumentElement.InsertBefore(xnRemove, xnRemove.PreviousSibling); // 调整XML配置文件中内容位置
                            break;
                        }
                        // 得到上次循环控件索引值
                        iIndex = _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item);
                        control = item;
                    }
                }
                else if (menuEventEnum == ShortMenuEventEnum.MoveNext)
                {
                    foreach (Control item in _ShortCutMenuList.MenuListPanl.Controls)
                    {
                        if (canSet)
                        {
                            // 交换索引值
                            int iIndexNew = _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item);
                            _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(item, iIndex);
                            _ShortCutMenuList.MenuListPanl.Controls.SetChildIndex(control, iIndexNew);
                            xmlMenu.DocumentElement.InsertAfter(xnRemove, xnRemove.NextSibling); // 调整XML配置文件中内容位置
                            break;
                        }

                        ShortCutItem curShortCutItem = (ShortCutItem)item;
                        if (curShortCutItem.Menu.Guid.Equals(dMenu.Guid))
                        {
                            canSet = true;
                            // 得到上次循环控件索引值
                            iIndex = _ShortCutMenuList.MenuListPanl.Controls.GetChildIndex(item);
                            control = item;
                        }
                    }
                }
            } 
            #endregion
            xmlMenu.Save(strShortCutFilePath);
        }
        #endregion

        #region 生成GUID按钮事件
        private void tsbAutoGuid_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Guid.NewGuid().ToString().ToUpper());
        } 
        #endregion

        #region 页签选择变化
        private void tcMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tpSelect = tcMenu.SelectedTab;
            if (tpSelect == null || tcMenu.SelectedTab == tpgDesktop)
            {
                if (tpSelect == null) tcMenu.SelectedTab = tpgDesktop;
                pnlDesktop.Show();
                pnlDesktop.Dock = DockStyle.Fill;
                txbMenuPath.Text = "桌面";
                //显示桌面最后的信息，如升级成功信息！
                ShowGlobalMsg_Click(this, new ShowGlobalMsgEventArgs(_sDestopLastMsg));
            }
            else
            {
                pnlDesktop.Hide();
                MenuEntity menu = tpSelect.Tag as MenuEntity;
                OpenMenu(menu, false);
            }
        } 
        #endregion

        #region 双击打开树菜单
        private void tvLeftMenu_DoubleClick(object sender, EventArgs e)
        {
            TreeNode tnSelect = tvLeftMenu.SelectedNode;
            if (tnSelect == null)
            {
                return;
            }
            //打开菜单
            OpenMenu(tnSelect.Tag as MenuEntity, false);
        } 
        #endregion

        #region 树节点开始拖动事件
        private void tvLeftMenu_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // 开始进行拖放操作，并将拖放的效果设置成移动。
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        } 
        #endregion

        #region 查找菜单回车键事件
        private void tstbMenuSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strSearchMenu=tstbMenuSearch.Text.Trim();
                if (string.IsNullOrEmpty(strSearchMenu))
                {
                    return;
                }
                //打开树节点方法
                OpenTreeNodeMenu(strSearchMenu,true);
            }
        }
        #endregion

        #region 打开树节点方法
        /// <summary>
        /// 打开树节点方法
        /// </summary>
        /// <param name="strSearchMenu">查找的菜单名</param>
        /// <param name="isOpenMenu">是否打开菜单：默认否</param>
        private void OpenTreeNodeMenu(string strSearchMenu,bool isOpenMenu=false)
        {
            TreeNode tnFind = null;
            foreach (TreeNode xn in tvLeftMenu.Nodes)
            {
                tnFind = FindNodeByText(xn, strSearchMenu);
                if (tnFind != null)
                {
                    ExpandParentNode(tnFind);
                    MenuEntity findMenu = tnFind.Tag as MenuEntity;
                    if (isOpenMenu)
                    {
                        OpenMenu(findMenu, false);//打开菜单
                    }
                    break;
                }
            }
        }  
        #endregion

        #region 根据文本查找树节点
        private TreeNode FindNodeByText(TreeNode tnParent, string strText)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strText) return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNodeByText(tn, strText);
                if (tnRet != null) break;
            }
            return tnRet;
        } 
        #endregion

        #region 递归打开左边树节点方法
        private void ExpandParentNode(TreeNode tnParent)
        {
            tnParent.Expand();
            if (tnParent.Parent != null)
            {
                ExpandParentNode(tnParent.Parent);
            }
        } 
        #endregion

        #region 隐藏左边树按钮事件
        private void btnHideTree_Click(object sender, EventArgs e)
        {
            if (tvLeftMenu.Visible)
            {
                tvLeftMenu.Hide();
                btnHideTree.Text = ">";
            }
            else
            {
                tvLeftMenu.Show();
                btnHideTree.Text = "<";
            }
        }
        #endregion

        #region 子窗体相关事件
        /// <summary>
        /// 子窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildForm_Active(object sender, EventArgs e)
        {
            if (sender is Form)
            {
                Form frmCurrent = sender as Form;
                MenuEntity dMenu = frmCurrent.Tag as MenuEntity;
                //使用同菜单不同窗体GUID
                if (tcMenu.SelectedTab != tcMenu.TabPages[dMenu.SameMenuNewFormGuid])
                {
                    tcMenu.SelectedTab = tcMenu.TabPages[dMenu.SameMenuNewFormGuid];
                }
                txbMenuPath.Text = dMenu.FullPath;
            }
        }

        /// <summary>
        /// 子窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MdiChild_Close(object sender, EventArgs e)
        {
            try
            {
                if (sender is Form)
                {
                    Form frmCurrent = sender as Form;
                    MenuEntity dMenu = frmCurrent.Tag as MenuEntity;
                    //使用同菜单不同窗体GUID
                    if (tcMenu.TabPages.ContainsKey(dMenu.SameMenuNewFormGuid))
                    {
                        tcMenu.TabPages.Remove(tcMenu.TabPages[dMenu.SameMenuNewFormGuid]);
                    }
                }
            }
            catch(Exception ex)
            {
            }
        } 
        #endregion

        #region 实现IMainForm接口
        private void tsmiUserManual_Click(object sender, EventArgs e)
        {
            BaseForm.ShowHtmlHelpPage(_FrameworkHelpName, _FrameworkHelpPath);
        }

        public void AddMdiChilden(Form form)
        {
            form.MdiParent = this;
            //TabFormLink tab = new TabFormLink(this.tbcMain, form, null, txbMenuPath);
            //tab.InitMainToolBarEvent += new InitMainToolBarDelegate(tab_InitMainToolBarEvent);
        }

        public int GetChildCount()
        {
            return this.MdiChildren.GetLength(0);
        }

        public void SetHelpKeyword(Control ctl, string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 用户环境菜单事件
        private void TsmiUserEnvrSet_Click(object sender, EventArgs e)
        {
            FrmUserEnvironmentSet f = new FrmUserEnvironmentSet();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowGlobalMsg += ShowGlobalMsg_Click;//绑定全局信息提示
            if (f.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FormBackGroupStyleEntity styleEntity = this.SetFormBackGroupStyle(_WinFormConfig.Get(GlobalKey.MainSkinType, BaseForm.ChildFormStyleType), _WinFormConfig.Get(GlobalKey.MainSkinValue, BaseForm.ChildFormStyleValue));//设置主窗体样式
            if (!styleEntity.IsUseImage)
            {
                txbMenuPath.BackColor = styleEntity.BackGroupColor;
                txbGlobalMsg.BackColor = styleEntity.BackGroupColor;
            }
            tableLayoutPanel1.BackgroundImage = null;
        } 
        #endregion

        #region 帮助相关
        /// <summary>
        /// 关于菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutAuthor about = new AboutAuthor();
            about.ShowDialog();
        }
        private void TsmiSupport_Click(object sender, EventArgs e)
        {
            FrmSupport frm = new FrmSupport();
            frm.ShowDialog();
        }
        private void TsbHelp_Click(object sender, EventArgs e)
        {
            TabPage tpSelect = tcMenu.SelectedTab;
            if (tpSelect == null || tcMenu.SelectedTab == tpgDesktop)
            {
                BaseForm.ShowHtmlHelpPage(_FrameworkHelpName, _FrameworkHelpPath);
            }
            else
            {
                MenuEntity curMenu = tpSelect.Tag as MenuEntity;
                if (!string.IsNullOrEmpty(curMenu.HelpPath))
                {
                    BaseForm.ShowHtmlHelpPage(curMenu.Name, curMenu.HelpPath);
                }
            }
        }

        /// <summary>
        /// 复制下载地址链接菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenLatestDownURL_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.UnicodeText, "https://gitee.com/breezee2000/WorkHelper/releases");
            MsgHelper.ShowInfo("已成功复制【工作助手】的下载链接地址，请粘贴到浏览器地址栏上打开链接，选择最新版下载！");
        } 
        #endregion

        #region Tab菜单页签的位置与关闭事件
        private void TsmiMenuSheetTop_Click(object sender, EventArgs e)
        {
            tcMenu.Dock = DockStyle.Top;
        }

        private void TsmiMenuSheetBottom_Click(object sender, EventArgs e)
        {
            tcMenu.Dock = DockStyle.Bottom;
        }

        private void TsmiCloseMenu_Click(object sender, EventArgs e)
        {
            WinFormContext.Instance.CurrentForm.Close();
        }

        private void TcMenu_DoubleClick(object sender, EventArgs e)
        {
            WinFormContext.Instance.CurrentForm.Close();
        }
        private void tsmiPutFirst_Click(object sender, EventArgs e)
        {
            TabPage curPage = tcMenu.SelectedTab;
            tcMenu.TabPages.Remove(curPage);
            tcMenu.TabPages.Insert(1, curPage);
            tcMenu.SelectedTab = curPage;
        }

        private void tsmiPutLast_Click(object sender, EventArgs e)
        {
            TabPage curPage = tcMenu.SelectedTab;
            tcMenu.TabPages.Remove(curPage);
            tcMenu.TabPages.Add(curPage);
            tcMenu.SelectedTab = curPage;
        }

        private void tsmiMoveRightOne_Click(object sender, EventArgs e)
        {
            int iCur = tcMenu.SelectedIndex;
            TabPage curPage = tcMenu.SelectedTab;
            if (curPage == tpgDesktop)
            {
                return;
            }
            TabPage nextPage = null;
            bool isFound = false;
            foreach (TabPage page in tcMenu.TabPages)
            {
                if (isFound)
                {
                    nextPage = page;
                    break;
                }
                if(page== curPage)
                {
                    isFound = true;
                }
            }
            if (nextPage != null)
            {
                tcMenu.TabPages.Remove(curPage);
                tcMenu.TabPages.Remove(nextPage);
                tcMenu.TabPages.Insert(iCur, nextPage);
                tcMenu.TabPages.Insert(iCur + 1, curPage);
                tcMenu.SelectedTab = curPage;
            }
            
        }

        private void tsmiMoveLeftOne_Click(object sender, EventArgs e)
        {
            int iCur = tcMenu.SelectedIndex;
            TabPage curPage = tcMenu.SelectedTab;
            TabPage beforePage = null;
            foreach (TabPage page in tcMenu.TabPages)
            {
                if (page == curPage)
                {
                    break;
                }

                beforePage = page;
            }
            if (beforePage != null && beforePage!= tpgDesktop)
            {
                tcMenu.TabPages.Remove(curPage);
                tcMenu.TabPages.Remove(beforePage);
                tcMenu.TabPages.Insert(iCur-1, curPage);
                tcMenu.TabPages.Insert(iCur, beforePage);
                tcMenu.SelectedTab = curPage;
            }
        }
        #endregion

        /// <summary>
        /// 打开新窗体右键菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenNewForm_Click(object sender, EventArgs e)
        {
            TreeNode tnSelect = tvLeftMenu.SelectedNode;
            if (tnSelect == null)
            {
                return;
            }
            //打开新菜单：支持同一个功能打开多次
            MenuEntity dMenu = tnSelect.Tag as MenuEntity;
            dMenu.SameMenuNewFormGuid = StringHelper.GetGUID();//使用同菜单不同窗体GUID
            dMenu.IsOpenSameMenuNewForm = true;
            OpenMenu(dMenu, false);
            //还原为不重复打开
            dMenu.SameMenuNewFormGuid = dMenu.Guid;
            dMenu.IsOpenSameMenuNewForm = false;
        }

        private void tsbCloseAll_Click(object sender, EventArgs e)
        {
            closeAllToolStripMenuItem.PerformClick();
        }

        private async void tsbCheckUpdate_Click(object sender, EventArgs e)
        {
            await Task.Run(() => UpgradeSystem(true));
        }

        /// <summary>
        /// 检查升级的压缩包是否存在
        /// </summary>
        /// <param name="upgradeInfo">升级对象信息</param>
        /// <returns></returns>
        private bool CheckWebFileExists(UpgradeInfo upgradeInfo)
        {
            foreach (string sEndfix in upgradeInfo.Server.SupportZipFormats)
            {
                try
                {
                    // 当路径配置中没有压缩包后缀时才加上
                    string sFullPath = upgradeInfo.Server.TempDownloadUrl.EndsWith("." + sEndfix) ? upgradeInfo.Server.TempDownloadUrl : upgradeInfo.Server.TempDownloadUrl + "." + sEndfix;
                    WebRequest request = WebRequest.Create(sFullPath);
                    request.Timeout = 2000; //连接超时时间（毫秒）
                    WebResponse response = request.GetResponse();
                    response.Close();
                    request.Abort();
                    // 确定服务器信息
                    upgradeInfo.Server.DownloadUrl = sFullPath; //服务器下载全路径
                    upgradeInfo.Server.ZipFormat = sEndfix; //压缩包后缀
                    upgradeInfo.Server.AppVersion = upgradeInfo.ServerVerCfg.appName + upgradeInfo.ServerVerCfg.version;
                    upgradeInfo.Server.ZipFileName = upgradeInfo.ServerVerCfg.appName + upgradeInfo.ServerVerCfg.version + "." + sEndfix;
                    return true;
                }
                catch
                {
                    // 报错不用处理。继续下一个压缩包后缀的处理
                }
            }
            return false;
        }

        /// <summary>
        /// 升级系统方法
        /// </summary>
        /// <param name="isHandUpdate"></param>
        private async void UpgradeSystem(bool isHandUpdate)
        {
            try
            {
                /**
                 * 读取服务器的版本(blob为默认点开的网页形式)：https://gitee.com/breezee2000/WorkHelper/blob/master/LatestVersion.json
                 * 读取服务器的版本(raw为原始数据形式)：https://gitee.com/breezee2000/WorkHelper/raw/master/LatestVersion.json
                 * 使用有优先级的多个下载路径：
                 * 1、发布路径-Gitee：包含版本号的压缩包
                 * 2、发布路径-Github：包含版本号的压缩包
                 * 3、发布路径-Gitlab：包含版本号的压缩包
                 */
                string sServerVersionJson = AppUpgradeTool.ReadWebText().Trim();
                LatestVerionConfig ver = _IADPJson.Deserialize<LatestVerionConfig>(sServerVersionJson);
                upgradeInfo = new UpgradeInfo(ver);
                // string sServerVersion = ver.version;
                //本地版本：注这里必须以启动项目的版本号来判断才合理，因为可能本地可能会保留多个版本的目录
                string sNowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(); 

                string[] sNowArr = sNowVersion.Split(new char[] { '.' });
                string[] sServerArr = ver.version.Split(new char[] { '.' });
                for (int i = 0; i < sServerArr.Length; i++)
                {
                    if (sNowArr.Length > i)
                    {
                        if(int.Parse(sServerArr[i]) > int.Parse(sNowArr[i]))
                        {
                            //服务器版本大于本地版本
                            upgradeInfo.Client.IsNeedUpgrade = true;
                            break;
                        }
                        else if (int.Parse(sServerArr[i]) < int.Parse(sNowArr[i]))
                        {
                            upgradeInfo.Client.IsNeedUpgrade = false;
                            break;//服务器版本小于本地版本
                        }
                    }
                }
                if (upgradeInfo.Client.IsNeedUpgrade)
                {
                    if (WinFormContext.Instance.IsUpgradeRunning)
                    {
                        if (isHandUpdate)
                        {
                            MsgHelper.ShowInfo("目前正在升级下载中。。。");
                        }
                        return;
                    }

                    string sMsg = "存在新版本：" + ver.version + "，确定要升级？" + System.Environment.NewLine + "升级过程在后台运行，不影响正常使用，完成后会提示。";
                    if (!string.IsNullOrEmpty(ver.publishRemark.Trim()))
                    {
                        sMsg += "本次修改内容：" + Environment.NewLine + ver.publishRemark;
                    }

                    if (MsgHelper.ShowOkCancel(sMsg) == DialogResult.Cancel)
                    {
                        return;
                    }

                    _sDestopLastMsg = ver.version + "版本压缩包正在后台下载中，请稍等...";
                    ShowGlobalMsg_Click(this, new ShowGlobalMsgEventArgs(_sDestopLastMsg));
                    //版本升级
                    WinFormContext.Instance.IsUpgradeRunning = true;
                    //异步获取文件
                    DirectoryInfo sPrePath = new DirectoryInfo(GlobalContext.AppEntryAssemblyPath);
                    upgradeInfo.Client.ParentDir = _WinFormConfig.Get(GlobalKey.Upgrade_TempPath, sPrePath.Parent.FullName);//默认为当前运行程序的父目录
                    bool isDeleteNewVerZipFile = _WinFormConfig.Get(GlobalKey.Upgrade_IsDeleteNewVerZipFile, "1").Equals("1") ? true : false;
                    upgradeInfo.Client.IsDeleteNewVerZipFile = isDeleteNewVerZipFile;
                    //取消原来的写死下载路径，改为从配置文件上获取，下载优化先级：downUrlPublishLatest => downUrlPublishGitee => downUrlPublishGithub
                    //string sServerZipUrl = string.Format("https://gitee.com/breezee2000/WorkHelper/releases/download/{0}/WorkHelper{1}.rar", sServerVersion, sServerVersion);
                    string sServerZipUrl = ver.downUrlPublishLatest.Replace("#version#", ver.version).Replace("#appName#", ver.appName);
                    upgradeInfo.Server.TempDownloadUrl = sServerZipUrl;
                    bool isHaveZipNewVersion = CheckWebFileExists(upgradeInfo);
                    int iMaxDownCount = 20;
                    int iDownCount = 1; //总共下载十次

                    while(iDownCount <= iMaxDownCount)
                    {
                        try
                        {
                            if (isHaveZipNewVersion)
                            {
                                try
                                {
                                    //通用最新版本存在版本压缩包时，异步获取压缩包文件
                                    // await Task.Run(() => AppUpgradeTool.DownloadWebZipAndUnZipAsync(upgradeInfo));
                                    await AppUpgradeTool.DownloadWebZipAndUnZipAsync(upgradeInfo);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            //通用最新版本不存在版本压缩包时，取Gitee上的压缩包
                            sServerZipUrl = ver.downUrlPublishGitee.Replace("#version#", ver.version).Replace("#appName#", ver.appName);
                            upgradeInfo.Server.TempDownloadUrl = sServerZipUrl;
                            isHaveZipNewVersion = CheckWebFileExists(upgradeInfo);
                            if (isHaveZipNewVersion)
                            {
                                try
                                {
                                    //Gitee存在版本压缩包时，异步获取压缩包文件
                                    await Task.Run(() => AppUpgradeTool.DownloadWebZipAndUnZipAsync(upgradeInfo));
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                            //Gitee不存在时使用Github
                            sServerZipUrl = ver.downUrlPublishGithub.Replace("#version#", ver.version).Replace("#appName#", ver.appName);
                            upgradeInfo.Server.TempDownloadUrl = sServerZipUrl;
                            isHaveZipNewVersion = CheckWebFileExists(upgradeInfo);
                            if (isHaveZipNewVersion)
                            {
                                try
                                {
                                    //Github存在版本压缩包时，异步获取压缩包文件
                                    await Task.Run(() => AppUpgradeTool.DownloadWebZipAndUnZipAsync(upgradeInfo));
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            iDownCount++;
                        }
                    }

                    //试了最大次数还不成功，那么提示信息
                    if (iDownCount >= iMaxDownCount)
                    {
                        MsgHelper.ShowInfo(string.Format("升级失败，最新版本{0}发布包未找到，请联系作者！", ver.version));
                        return;
                    }

                    WinFormContext.Instance.IsUpgradeRunning = false;                   
                    if("release".Equals(sPrePath.Name,StringComparison.OrdinalIgnoreCase) || "bin".Equals(sPrePath.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        _WinFormConfig.Set(GlobalKey.Upgrade_PreVersionPath, "", "当前版本所在的目录，为升级完后删除旧版本使用！"); //开发环境，不记录原版本
                    }
                    else
                    {
                        //正式运行环境：记录原版本路径，还备份SQLite数据库文件
                        _WinFormConfig.Set(GlobalKey.Upgrade_PreVersionPath, sPrePath.FullName, "当前版本所在的目录，为升级完后删除旧版本使用！");
                    }

                    //File.Delete(Path.Combine(sLocalDir,string.Format("WorkHelper{0}.rar", sServerVersion))); //删除新版本的压缩包：已取消，杀毒软件会误报为病毒
                    //升级后重新登录提示：已取消，在关闭窗体时会有确认
                    //if (MsgHelper.ShowOkCancel("升级成功！是否打开新版本？")== DialogResult.Cancel) 
                    //{ 
                    //    return; 
                    //}
                    //设置新应用路径
                    string sNewRoot = Path.Combine(upgradeInfo.Client.ParentDir, "WorkHelper" + ver.version);
                    _WinFormConfig.Set(GlobalKey.Upgrade_LatestVersionRootDir, sNewRoot, "新版本的根目录");
                    _WinFormConfig.Set(GlobalKey.Upgrade_CurrentVersionNo, ver.version, "当前版本号");
                    sUpgradeNewAppFullPath = Path.Combine(sNewRoot, MiniGlobalValue.AppStartUpExeName);
                    _WinFormConfig.Save();//保存配置

                    if(_WinFormConfig.Get(GlobalKey.Upgrade_IsAutoCorrectDesktopQuickLink, "1").Equals("1"))
                    {
                        //覆盖桌面快捷方式：杀毒软件也有可能会误报为病毒（本人电脑装了360，亲测会误报病毒）
                        StartUpHelper.ReplaceDesktopQuickLink(sNewRoot);
                    }
                    
                    IsUpgradeColseOldApp = true;
                    _sDestopLastMsg = "WorkHelper" + ver.version + "版本已成功下载并解压！";
                    ShowGlobalMsg_Click(this, new ShowGlobalMsgEventArgs(_sDestopLastMsg));
                    if (UpgradeSuccessReloadAppDeleagte != null)
                    {
                        UpgradeSuccessReloadAppDeleagte(); //升级成功，重启应用
                    }
                }
                else
                {
                    if (isHandUpdate)
                    {
                        MsgHelper.ShowInfo("已经是最新版本，不需要升级！");
                    }
                }
            }
            catch(Exception ex) 
            {
                if (isHandUpdate)
                {
                    MsgHelper.ShowInfo("升级出错：" + ex.Message);
                }
                else
                {
                    WinFormContext.Instance.LogErr(ex.Message, "升级出错", false);
                }
                WinFormContext.Instance.IsUpgradeRunning = false;
            }

        }


        /// <summary>
        /// 下载最新的稳定版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsmiDowloadLatestStableVersion_Click(object sender, EventArgs e)
        {
            bool isHandUpdate = true;
            try
            {
                /**
                 * 读取服务器的版本(blob为默认点开的网页形式)：https://gitee.com/breezee2000/WorkHelper/blob/master/LatestVersion.json
                 * 读取服务器的版本(raw为原始数据形式)：https://gitee.com/breezee2000/WorkHelper/raw/master/LatestVersion.json
                 * 使用有优先级的多个下载路径：
                 * 1、发布路径-Gitee：包含版本号的压缩包
                 * 2、发布路径-Github：包含版本号的压缩包
                 * 3、发布路径-Gitlab：包含版本号的压缩包
                 */
                
                string sServerVersionJson = AppUpgradeTool.ReadWebText().Trim();
                LatestVerionConfig ver = _IADPJson.Deserialize<LatestVerionConfig>(sServerVersionJson);
                upgradeInfo = new UpgradeInfo(ver);
                // string sNowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(); //本地版本

                string sMsg = "确定要下载稳定版本：" + ver.stableVersion + "？" + System.Environment.NewLine + "下载过程在后台运行，不影响正常使用，完成后会提示。";
                if (!string.IsNullOrEmpty(ver.stableRemark.Trim()))
                {
                    sMsg += "本次修改内容：" + Environment.NewLine + ver.stableRemark;
                }

                if (MsgHelper.ShowOkCancel(sMsg) == DialogResult.Cancel)
                {
                    return;
                }

                _sDestopLastMsg = ver.stableVersion + "稳定版的压缩包正在后台下载中，请稍等...";
                ShowGlobalMsg_Click(this, new ShowGlobalMsgEventArgs(_sDestopLastMsg));
                //异步获取文件
                DirectoryInfo sPrePath = new DirectoryInfo(GlobalContext.AppEntryAssemblyPath);
                // string sLocalDir = _WinFormConfig.Get(GlobalKey.Upgrade_TempPath, sPrePath.Parent.FullName);//默认为当前运行程序的父目录
                // bool isDeleteNewVerZipFile = _WinFormConfig.Get(GlobalKey.Upgrade_IsDeleteNewVerZipFile, "1").Equals("1") ? true : false;

                //取消原来的写死下载路径，改为从配置文件上获取，下载优化先级：downUrlPublishLatest => downUrlPublishGitee => downUrlPublishGithub
                //string sServerZipUrl = string.Format("https://gitee.com/breezee2000/WorkHelper/releases/download/{0}/WorkHelper{1}.rar", sServerVersion, sServerVersion);
                string sServerZipUrl = ver.stableDownUrlPublish.Replace("#version#", ver.stableVersion).Replace("#appName#", ver.appName);
                upgradeInfo.Server.TempDownloadUrl = sServerZipUrl;
                bool isHaveZipNewVersion = CheckWebFileExists(upgradeInfo);
                int iMaxDownCount = 20;
                int iDownCount = 1; //总共下载十次

                while (iDownCount <= iMaxDownCount)
                {
                    try
                    {
                        if (isHaveZipNewVersion)
                        {
                            //通用最新版本存在版本压缩包时，异步获取压缩包文件
                            await Task.Run(() => AppUpgradeTool.DownloadWebZipAndUnZipAsync(upgradeInfo));
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        WinFormContext.Instance.LogErr(ex.Message, "第" + iDownCount + "下载出错", false);
                    }
                    finally
                    {
                        iDownCount++;
                    }
                }

                //试了最大次数还不成功，那么提示信息
                if (iDownCount >= iMaxDownCount)
                {
                    MsgHelper.ShowInfo(string.Format("下载失败，稳定版{0}发布包未找到，请联系作者！", ver.stableVersion));
                    return;
                }
                _sDestopLastMsg = "WorkHelper" + ver.stableVersion + "稳定版已成功下载并解压！";
                ShowGlobalMsg_Click(this, new ShowGlobalMsgEventArgs(_sDestopLastMsg));
            }
            catch (Exception ex)
            {
                if (isHandUpdate)
                {
                    MsgHelper.ShowInfo("升级出错：" + ex.Message);
                }
                else
                {
                    WinFormContext.Instance.LogErr(ex.Message, "升级出错", false);
                }
                WinFormContext.Instance.IsUpgradeRunning = false;
            }
        }

        private void tsbEnvironmentSet_Click(object sender, EventArgs e)
        {
            tsmiUserEnvrSet.PerformClick();
        }
    }
}
