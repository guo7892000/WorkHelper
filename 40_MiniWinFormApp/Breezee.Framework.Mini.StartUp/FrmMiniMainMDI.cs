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

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 对象名称：MDI主窗体
    /// 对象类型：窗体
    /// 创建日期：2016-10-20
    /// 创建作者：黄国辉
    /// </summary>
    public partial class FrmMiniMainMDI : Form, IMainForm, IForm
    {
        #region 变量
        public event EventHandler<EventArgs> FormClosed;
        string _strAppPath = AppDomain.CurrentDomain.BaseDirectory;
        string _strConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkHelper/Config");
        int iStartMenu = 0;
        XmlMenu _xmlMenu;
        IDictionary<string, MenuEntity> _MenuDic;
        ShortCutList _ShortCutMenuList;
        bool IsReLoad = false;
        string MenuXmlFilePath = Path.Combine(GlobalValue.EntryAssemblyPath, MiniStaticString.ConfigDataPath, MiniStaticString.MenuFileName);
        public ToolStripStatusLabel StatusBarMessagePanel => throw new NotImplementedException();
        private string _FrameworkHelpPath = "/Help/Html/Mini/WorkHelper.html";
        private string _FrameworkHelpName = "工作助手";
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
            Text = string.Format("工作助手（Work Helper） v{0} 正式版  2023-04-21", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            iStartMenu = menuStrip.Items.IndexOfKey(tsbStartMenu.Name);
            this.WindowState = FormWindowState.Maximized;
#if DEBUG
            tsbAutoGuid.Visible = true;
#endif
            tcMenu.Dock = DockStyle.Top;
            _xmlMenu = new XmlMenu(MenuXmlFilePath);

            this.SetFormBackGroupStyle(WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinType), WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinValue));//设置主窗体样式
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp(_FrameworkHelpPath, _FrameworkHelpName + " > 概述", _FrameworkHelpName));
            WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp("/Help/Html/Mini/ChangeHistory.html", _FrameworkHelpName + " > 变更历史", _FrameworkHelpName));
            //加载菜单
            LoadMenu();
            //增加快捷菜单
            LoadShortCutMenu();
            //加载用户偏好设定
            LoadUserLoveSettings();
            //默认桌面布满
            pnlDestop.Dock = DockStyle.Fill;
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
        }
        #endregion

        #region 加载菜单方法
        private void LoadMenu()
        {
            //加载菜单
            tvLeftMenu.Nodes.Clear();
            //XML中的菜单处理
            _MenuDic = _MenuDic = _xmlMenu.GetAllMenu(); ;
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
                    AddMenuItem(tmiNew, new ToolStripMenuItem(), childMenu);
                    if (!string.IsNullOrEmpty(childMenu.HelpPath))
                    {
                        WinFormContext.Instance.MenuHelpList.Add(new EntMenuHelp(childMenu.HelpPath, childMenu.FullPath, childMenu.Name));
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
            if (!Directory.Exists(_strConfigFilePath))
            {
                Directory.CreateDirectory(_strConfigFilePath);
            }
            string strShortCutFilePath = Path.Combine(_strConfigFilePath, MiniStaticString.ShortCutMenuFileName);
            if (!File.Exists(strShortCutFilePath))
            {
                XmlDocument xmlShortCut = new XmlDocument();
                XmlElement xmRoot = xmlShortCut.CreateElement("xml");
                xmlShortCut.AppendChild(xmRoot);
                xmlShortCut.Save(strShortCutFilePath);
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
                        _ShortCutMenuList.AddItem(scItem);
                    }
                }
                //
                _ShortCutMenuList.Dock = DockStyle.Fill;
                pnlDestop.Controls.Add(_ShortCutMenuList);

            }
        }
        #endregion

        private void LoadUserLoveSettings()
        {
            UserLoveSettings miniXmlConfig = new UserLoveSettings(_strConfigFilePath, MiniStaticString.UserLoveSettings, XmlConfigSaveType.Attribute);
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
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
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
            LockForm f = new LockForm();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }
        #endregion

        #region 窗体关闭中事件
        private void FrmMainMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgHelper.ShowOkCancel("确定要关闭所有窗体退出吗？") == DialogResult.Cancel)
            {
                e.Cancel = true;
                IsReLoad = false;
            }
            else
            {
                e.Cancel = false;
                IsReLoad = true;

                if (FormClosed != null)
                {
                    FormClosed(this, e);
                }
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

            if (IsExpandTreeNode)
            {
                OpenTreeNodeMenu(dOpenMenu.Name);
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
                    pnlDestop.Hide();
                    frm.Activate();
                    WinFormContext.Instance.CurrentForm = frm;
                    return;
                }
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
            SaveShortCutMenuConfig(e.Menu,false);
        }
        #endregion

        #region 增加快捷菜单项点击事件
        private void AddShortCutMenuItem(object sender, ShortCutItemClickEventArgs e)
        {
            SaveShortCutMenuConfig(e.Menu, true);
        }
        #endregion

        #region 保存快捷菜单配置
        private void SaveShortCutMenuConfig(MenuEntity dMenu,bool IsAdd)
        {
            string strShortCutFilePath = Path.Combine(_strConfigFilePath, MiniStaticString.ShortCutMenuFileName);
            XmlDocument xmlMenu = new XmlDocument();
            xmlMenu.Load(strShortCutFilePath);
            XmlNodeList xmlList = xmlMenu.SelectNodes("xml/Menu");
            XmlNode xnRemove = xmlMenu.SelectSingleNode("xml/Menu[@Guid='" + dMenu.Guid + "']");
            if (xnRemove != null)
            {                
                if (!IsAdd)
                {
                    xnRemove.ParentNode.RemoveChild(xnRemove);
                    //从快捷菜单中移除
                    if(_ShortCutMenuList.ItemList.ContainsKey(dMenu.Guid))
                    {
                        _ShortCutMenuList.MenuListPanl.Controls.Remove(_ShortCutMenuList.ItemList[dMenu.Guid]);
                        _ShortCutMenuList.ItemList.Remove(dMenu.Guid);
                        
                    }
                }
            }
            else
            { 
                if (IsAdd)
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
                        _ShortCutMenuList.AddItem(scItem);
                    }
                }
            }
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
                pnlDestop.Show();
                pnlDestop.Dock = DockStyle.Fill;
                txbMenuPath.Text = "桌面";
            }
            else
            {
                pnlDestop.Hide();
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
                OpenTreeNodeMenu(strSearchMenu);
            }
        }
        #endregion

        #region 打开树节点方法
        private void OpenTreeNodeMenu(string strSearchMenu)
        {
            TreeNode tnFind = null;
            foreach (TreeNode xn in tvLeftMenu.Nodes)
            {
                tnFind = FindNodeByText(xn, strSearchMenu);
                if (tnFind != null)
                {
                    ExpandParentNode(tnFind);
                    MenuEntity findMenu = tnFind.Tag as MenuEntity;
                    OpenMenu(findMenu, false);//打开菜单
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

        #region 菜单Tag页位置
        private void cmsMenuTabPositionUp_Click(object sender, EventArgs e)
        {
            tcMenu.Dock = DockStyle.Top;
        }

        private void cmsMenuTabPositionDown_Click(object sender, EventArgs e)
        {
            tcMenu.Dock = DockStyle.Bottom;
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

        private void TsmiUserEnvrSet_Click(object sender, EventArgs e)
        {
            FrmUserEnvironmentSet f = new FrmUserEnvironmentSet();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowGlobalMsg += ShowGlobalMsg_Click;//绑定全局信息提示
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.SetFormBackGroupStyle(WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinType), WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinValue));//设置主窗体样式
            }
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, ShowGlobalMsgEventArgs e)
        {
            txbGlobalMsg.Text = e.Msg;
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
    }
}
