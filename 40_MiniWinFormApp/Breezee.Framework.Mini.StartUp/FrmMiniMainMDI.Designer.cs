namespace Breezee.Framework.Mini.StartUp
{
    partial class FrmMiniMainMDI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMiniMainMDI));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsbStartMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbRestartMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserEnvrSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserManual = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenLatestDownURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbMenuSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsbAutoGuid = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tvLeftMenu = new System.Windows.Forms.TreeView();
            this.cmsOpenTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenNewForm = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLeftMenu = new System.Windows.Forms.Panel();
            this.btnHideTree = new System.Windows.Forms.Button();
            this.pnlMenuNavigate = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbGlobalMsg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbMenuPath = new System.Windows.Forms.TextBox();
            this.tcMenu = new System.Windows.Forms.TabControl();
            this.cmsMenuSheet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMenuSheetTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuSheetBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tpgDesktop = new System.Windows.Forms.TabPage();
            this.pnlDestop = new System.Windows.Forms.Panel();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.cmsOpenTreeMenu.SuspendLayout();
            this.pnlLeftMenu.SuspendLayout();
            this.pnlMenuNavigate.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcMenu.SuspendLayout();
            this.cmsMenuSheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStartMenu,
            this.viewMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(1263, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsbStartMenu
            // 
            this.tsbStartMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRestartMenu,
            this.tsmiUserEnvrSet,
            this.tsmiLock,
            this.tsmiChangePassword,
            this.toolStripSeparator5,
            this.tsbExitMenu});
            this.tsbStartMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsbStartMenu.Name = "tsbStartMenu";
            this.tsbStartMenu.Size = new System.Drawing.Size(60, 22);
            this.tsbStartMenu.Text = "开始(&B)";
            // 
            // tsbRestartMenu
            // 
            this.tsbRestartMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsbRestartMenu.Image")));
            this.tsbRestartMenu.Name = "tsbRestartMenu";
            this.tsbRestartMenu.Size = new System.Drawing.Size(140, 22);
            this.tsbRestartMenu.Text = "重新登录(&R)";
            // 
            // tsmiUserEnvrSet
            // 
            this.tsmiUserEnvrSet.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUserEnvrSet.Image")));
            this.tsmiUserEnvrSet.Name = "tsmiUserEnvrSet";
            this.tsmiUserEnvrSet.Size = new System.Drawing.Size(140, 22);
            this.tsmiUserEnvrSet.Text = "环境设置";
            this.tsmiUserEnvrSet.Click += new System.EventHandler(this.TsmiUserEnvrSet_Click);
            // 
            // tsmiLock
            // 
            this.tsmiLock.Image = ((System.Drawing.Image)(resources.GetObject("tsmiLock.Image")));
            this.tsmiLock.Name = "tsmiLock";
            this.tsmiLock.Size = new System.Drawing.Size(140, 22);
            this.tsmiLock.Text = "锁定系统";
            // 
            // tsmiChangePassword
            // 
            this.tsmiChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("tsmiChangePassword.Image")));
            this.tsmiChangePassword.Name = "tsmiChangePassword";
            this.tsmiChangePassword.Size = new System.Drawing.Size(140, 22);
            this.tsmiChangePassword.Text = "修改密码";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(137, 6);
            // 
            // tsbExitMenu
            // 
            this.tsbExitMenu.Name = "tsbExitMenu";
            this.tsbExitMenu.Size = new System.Drawing.Size(140, 22);
            this.tsbExitMenu.Text = "退出(&X)";
            this.tsbExitMenu.Click += new System.EventHandler(this.tsbExitMenu_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(60, 22);
            this.viewMenu.Text = "视图(&V)";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.toolBarToolStripMenuItem.Text = "工具栏(&T)";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.statusBarToolStripMenuItem.Text = "状态栏(&S)";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(64, 22);
            this.windowsMenu.Text = "窗口(&W)";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cascadeToolStripMenuItem.Text = "层叠(&C)";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileVerticalToolStripMenuItem.Text = "垂直平铺(&V)";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileHorizontalToolStripMenuItem.Text = "水平平铺(&H)";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.closeAllToolStripMenuItem.Text = "全部关闭(&L)";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.arrangeIconsToolStripMenuItem.Text = "排列图标(&A)";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUserManual,
            this.aboutToolStripMenuItem,
            this.tsmiOpenLatestDownURL,
            this.tsbCheckUpdate});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(61, 22);
            this.helpMenu.Text = "帮助(&H)";
            // 
            // tsmiUserManual
            // 
            this.tsmiUserManual.Name = "tsmiUserManual";
            this.tsmiUserManual.Size = new System.Drawing.Size(148, 22);
            this.tsmiUserManual.Text = "用户手册(&U)";
            this.tsmiUserManual.Click += new System.EventHandler(this.tsmiUserManual_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aboutToolStripMenuItem.Text = "关于(&A) ";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tsmiOpenLatestDownURL
            // 
            this.tsmiOpenLatestDownURL.Name = "tsmiOpenLatestDownURL";
            this.tsmiOpenLatestDownURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiOpenLatestDownURL.Text = "复制下载地址";
            this.tsmiOpenLatestDownURL.Click += new System.EventHandler(this.tsmiOpenLatestDownURL_Click);
            // 
            // tsbCheckUpdate
            // 
            this.tsbCheckUpdate.Name = "tsbCheckUpdate";
            this.tsbCheckUpdate.Size = new System.Drawing.Size(148, 22);
            this.tsbCheckUpdate.Text = "检查更新";
            this.tsbCheckUpdate.ToolTipText = "检测是否有新版本";
            this.tsbCheckUpdate.Click += new System.EventHandler(this.tsbCheckUpdate_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tstbMenuSearch,
            this.tsbAutoGuid,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator2,
            this.tsbHelp});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1263, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 24);
            this.toolStripLabel1.Text = "搜索：";
            // 
            // tstbMenuSearch
            // 
            this.tstbMenuSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstbMenuSearch.Name = "tstbMenuSearch";
            this.tstbMenuSearch.Size = new System.Drawing.Size(151, 27);
            this.tstbMenuSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbMenuSearch_KeyUp);
            // 
            // tsbAutoGuid
            // 
            this.tsbAutoGuid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutoGuid.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoGuid.Image")));
            this.tsbAutoGuid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoGuid.Name = "tsbAutoGuid";
            this.tsbAutoGuid.Size = new System.Drawing.Size(24, 24);
            this.tsbAutoGuid.Text = "生成GUID";
            this.tsbAutoGuid.Visible = false;
            this.tsbAutoGuid.Click += new System.EventHandler(this.tsbAutoGuid_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.saveToolStripButton.Text = "保存";
            this.saveToolStripButton.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.printToolStripButton.Text = "打印";
            this.printToolStripButton.Visible = false;
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.printPreviewToolStripButton.Text = "打印预览";
            this.printPreviewToolStripButton.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator2.Visible = false;
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(24, 24);
            this.tsbHelp.Text = "帮助";
            this.tsbHelp.Click += new System.EventHandler(this.TsbHelp_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUserInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 601);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
            this.statusStrip.Size = new System.Drawing.Size(1263, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // tsslUserInfo
            // 
            this.tsslUserInfo.Name = "tsslUserInfo";
            this.tsslUserInfo.Size = new System.Drawing.Size(44, 17);
            this.tsslUserInfo.Text = "用户名";
            // 
            // tvLeftMenu
            // 
            this.tvLeftMenu.ContextMenuStrip = this.cmsOpenTreeMenu;
            this.tvLeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvLeftMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvLeftMenu.Location = new System.Drawing.Point(0, 51);
            this.tvLeftMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvLeftMenu.Name = "tvLeftMenu";
            this.tvLeftMenu.Size = new System.Drawing.Size(175, 550);
            this.tvLeftMenu.TabIndex = 4;
            this.tvLeftMenu.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvLeftMenu_ItemDrag);
            this.tvLeftMenu.DoubleClick += new System.EventHandler(this.tvLeftMenu_DoubleClick);
            // 
            // cmsOpenTreeMenu
            // 
            this.cmsOpenTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenNewForm});
            this.cmsOpenTreeMenu.Name = "cmsOpenTreeMenu";
            this.cmsOpenTreeMenu.Size = new System.Drawing.Size(137, 26);
            // 
            // tsmiOpenNewForm
            // 
            this.tsmiOpenNewForm.Name = "tsmiOpenNewForm";
            this.tsmiOpenNewForm.Size = new System.Drawing.Size(136, 22);
            this.tsmiOpenNewForm.Text = "打开新窗体";
            this.tsmiOpenNewForm.ToolTipText = "支持同一个菜单打开多个窗体！";
            this.tsmiOpenNewForm.Click += new System.EventHandler(this.tsmiOpenNewForm_Click);
            // 
            // pnlLeftMenu
            // 
            this.pnlLeftMenu.Controls.Add(this.btnHideTree);
            this.pnlLeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftMenu.Location = new System.Drawing.Point(175, 51);
            this.pnlLeftMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlLeftMenu.Name = "pnlLeftMenu";
            this.pnlLeftMenu.Size = new System.Drawing.Size(13, 550);
            this.pnlLeftMenu.TabIndex = 5;
            // 
            // btnHideTree
            // 
            this.btnHideTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideTree.Location = new System.Drawing.Point(-1, 276);
            this.btnHideTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHideTree.Name = "btnHideTree";
            this.btnHideTree.Size = new System.Drawing.Size(15, 23);
            this.btnHideTree.TabIndex = 0;
            this.btnHideTree.Text = "<";
            this.btnHideTree.UseVisualStyleBackColor = true;
            this.btnHideTree.Click += new System.EventHandler(this.btnHideTree_Click);
            // 
            // pnlMenuNavigate
            // 
            this.pnlMenuNavigate.Controls.Add(this.tableLayoutPanel1);
            this.pnlMenuNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenuNavigate.Location = new System.Drawing.Point(188, 51);
            this.pnlMenuNavigate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlMenuNavigate.Name = "pnlMenuNavigate";
            this.pnlMenuNavigate.Size = new System.Drawing.Size(1075, 23);
            this.pnlMenuNavigate.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.Controls.Add(this.txbGlobalMsg, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbMenuPath, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1075, 23);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // txbGlobalMsg
            // 
            this.txbGlobalMsg.BackColor = System.Drawing.SystemColors.Control;
            this.txbGlobalMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbGlobalMsg.ForeColor = System.Drawing.Color.Red;
            this.txbGlobalMsg.Location = new System.Drawing.Point(399, 4);
            this.txbGlobalMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbGlobalMsg.Name = "txbGlobalMsg";
            this.txbGlobalMsg.ReadOnly = true;
            this.txbGlobalMsg.Size = new System.Drawing.Size(651, 14);
            this.txbGlobalMsg.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "菜单导航：";
            // 
            // txbMenuPath
            // 
            this.txbMenuPath.BackColor = System.Drawing.SystemColors.Control;
            this.txbMenuPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbMenuPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txbMenuPath.Location = new System.Drawing.Point(74, 4);
            this.txbMenuPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbMenuPath.Name = "txbMenuPath";
            this.txbMenuPath.Size = new System.Drawing.Size(319, 14);
            this.txbMenuPath.TabIndex = 1;
            // 
            // tcMenu
            // 
            this.tcMenu.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcMenu.ContextMenuStrip = this.cmsMenuSheet;
            this.tcMenu.Controls.Add(this.tpgDesktop);
            this.tcMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcMenu.Location = new System.Drawing.Point(188, 577);
            this.tcMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tcMenu.Name = "tcMenu";
            this.tcMenu.SelectedIndex = 0;
            this.tcMenu.Size = new System.Drawing.Size(1075, 24);
            this.tcMenu.TabIndex = 7;
            this.tcMenu.SelectedIndexChanged += new System.EventHandler(this.tcMenu_SelectedIndexChanged);
            this.tcMenu.DoubleClick += new System.EventHandler(this.TcMenu_DoubleClick);
            // 
            // cmsMenuSheet
            // 
            this.cmsMenuSheet.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMenuSheet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuSheetTop,
            this.tsmiMenuSheetBottom,
            this.tsmiCloseMenu,
            this.tsbCloseAll});
            this.cmsMenuSheet.Name = "cmsMenuSheet";
            this.cmsMenuSheet.Size = new System.Drawing.Size(125, 92);
            // 
            // tsmiMenuSheetTop
            // 
            this.tsmiMenuSheetTop.Name = "tsmiMenuSheetTop";
            this.tsmiMenuSheetTop.Size = new System.Drawing.Size(124, 22);
            this.tsmiMenuSheetTop.Text = "置顶部";
            this.tsmiMenuSheetTop.Click += new System.EventHandler(this.TsmiMenuSheetTop_Click);
            // 
            // tsmiMenuSheetBottom
            // 
            this.tsmiMenuSheetBottom.Name = "tsmiMenuSheetBottom";
            this.tsmiMenuSheetBottom.Size = new System.Drawing.Size(124, 22);
            this.tsmiMenuSheetBottom.Text = "置底部";
            this.tsmiMenuSheetBottom.Click += new System.EventHandler(this.TsmiMenuSheetBottom_Click);
            // 
            // tsmiCloseMenu
            // 
            this.tsmiCloseMenu.Name = "tsmiCloseMenu";
            this.tsmiCloseMenu.Size = new System.Drawing.Size(124, 22);
            this.tsmiCloseMenu.Text = "关闭";
            this.tsmiCloseMenu.Click += new System.EventHandler(this.TsmiCloseMenu_Click);
            // 
            // tsbCloseAll
            // 
            this.tsbCloseAll.Name = "tsbCloseAll";
            this.tsbCloseAll.Size = new System.Drawing.Size(124, 22);
            this.tsbCloseAll.Text = "关闭所有";
            this.tsbCloseAll.Click += new System.EventHandler(this.tsbCloseAll_Click);
            // 
            // tpgDesktop
            // 
            this.tpgDesktop.Location = new System.Drawing.Point(4, 4);
            this.tpgDesktop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpgDesktop.Name = "tpgDesktop";
            this.tpgDesktop.Size = new System.Drawing.Size(1067, 0);
            this.tpgDesktop.TabIndex = 0;
            this.tpgDesktop.Text = "桌面";
            this.tpgDesktop.UseVisualStyleBackColor = true;
            // 
            // pnlDestop
            // 
            this.pnlDestop.AccessibleDescription = "";
            this.pnlDestop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDestop.Location = new System.Drawing.Point(188, 74);
            this.pnlDestop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlDestop.Name = "pnlDestop";
            this.pnlDestop.Size = new System.Drawing.Size(1075, 47);
            this.pnlDestop.TabIndex = 8;
            // 
            // FrmMiniMainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 623);
            this.Controls.Add(this.pnlDestop);
            this.Controls.Add(this.tcMenu);
            this.Controls.Add(this.pnlMenuNavigate);
            this.Controls.Add(this.pnlLeftMenu);
            this.Controls.Add(this.tvLeftMenu);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMiniMainMDI";
            this.Text = "主窗体";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMainMDI_FormClosing);
            this.Load += new System.EventHandler(this.FrmMainMDI_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.cmsOpenTreeMenu.ResumeLayout(false);
            this.pnlLeftMenu.ResumeLayout(false);
            this.pnlMenuNavigate.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcMenu.ResumeLayout(false);
            this.cmsMenuSheet.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel tsslUserInfo;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbStartMenu;
        private System.Windows.Forms.ToolStripMenuItem tsbExitMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TreeView tvLeftMenu;
        private System.Windows.Forms.Panel pnlLeftMenu;
        private System.Windows.Forms.Panel pnlMenuNavigate;
        private System.Windows.Forms.TabControl tcMenu;
        private System.Windows.Forms.Panel pnlDestop;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbMenuSearch;
        private System.Windows.Forms.ToolStripButton tsbAutoGuid;
        private System.Windows.Forms.TabPage tpgDesktop;
        private System.Windows.Forms.TextBox txbMenuPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHideTree;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserManual;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripMenuItem tsbRestartMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePassword;
        private System.Windows.Forms.ToolStripMenuItem tsmiLock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txbGlobalMsg;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserEnvrSet;
        private System.Windows.Forms.ContextMenuStrip cmsMenuSheet;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuSheetTop;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuSheetBottom;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenLatestDownURL;
        private System.Windows.Forms.ContextMenuStrip cmsOpenTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenNewForm;
        private System.Windows.Forms.ToolStripMenuItem tsbCloseAll;
        private System.Windows.Forms.ToolStripMenuItem tsbCheckUpdate;
    }
}



