namespace Breezee.Framework.Mini.StartUp
{
    partial class FrmUserEnvironmentSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserEnvironmentSet));
            this.cdlSelectColor = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectPicMain = new System.Windows.Forms.Button();
            this.txbSkinValueMain = new System.Windows.Forms.TextBox();
            this.cbbColorMain = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbSkinTypeMain = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectPicCommon = new System.Windows.Forms.Button();
            this.txbSkinValueCommon = new System.Windows.Forms.TextBox();
            this.cbbColorCommon = new System.Windows.Forms.ComboBox();
            this.cbbSkinTypeCommon = new System.Windows.Forms.ComboBox();
            this.opfSelectPic = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBase = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckbDelOldNeedConfirm = new System.Windows.Forms.CheckBox();
            this.ckbUpgradeSuccessDelOldVerion = new System.Windows.Forms.CheckBox();
            this.btnSelectUpgradeTmpPath = new System.Windows.Forms.Button();
            this.txbUpgradeTempDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbAutoCheckVersion = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectMySettingPath = new System.Windows.Forms.Button();
            this.txbMyLoveSettingPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbMsgType = new System.Windows.Forms.ComboBox();
            this.tpUserSkin = new System.Windows.Forms.TabPage();
            this.tpOther = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblDgvStyleInfo = new System.Windows.Forms.Label();
            this.btnClearDGVStyle = new System.Windows.Forms.Button();
            this.txbGirdStyleTempPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fbdSelectPath = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckbUpgradeDelNewZipFile = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpBase.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpUserSkin.SuspendLayout();
            this.tpOther.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "皮肤类型：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(774, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(67, 22);
            this.tsbSave.Text = "保存(&S)";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(68, 22);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 82);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主窗体样式设置";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 341F));
            this.tableLayoutPanel3.Controls.Add(this.btnSelectPicMain, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.txbSkinValueMain, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbbColorMain, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbbSkinTypeMain, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(754, 62);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // btnSelectPicMain
            // 
            this.btnSelectPicMain.Location = new System.Drawing.Point(356, 29);
            this.btnSelectPicMain.Name = "btnSelectPicMain";
            this.btnSelectPicMain.Size = new System.Drawing.Size(34, 23);
            this.btnSelectPicMain.TabIndex = 2;
            this.btnSelectPicMain.Text = "...";
            this.btnSelectPicMain.UseVisualStyleBackColor = true;
            this.btnSelectPicMain.Click += new System.EventHandler(this.btnSelectPicMain_Click);
            // 
            // txbSkinValueMain
            // 
            this.txbSkinValueMain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.txbSkinValueMain, 2);
            this.txbSkinValueMain.Enabled = false;
            this.txbSkinValueMain.Location = new System.Drawing.Point(74, 29);
            this.txbSkinValueMain.Multiline = true;
            this.txbSkinValueMain.Name = "txbSkinValueMain";
            this.txbSkinValueMain.Size = new System.Drawing.Size(276, 30);
            this.txbSkinValueMain.TabIndex = 3;
            // 
            // cbbColorMain
            // 
            this.cbbColorMain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbColorMain.FormattingEnabled = true;
            this.cbbColorMain.Location = new System.Drawing.Point(219, 3);
            this.cbbColorMain.Name = "cbbColorMain";
            this.cbbColorMain.Size = new System.Drawing.Size(131, 20);
            this.cbbColorMain.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "皮肤类型：";
            // 
            // cbbSkinTypeMain
            // 
            this.cbbSkinTypeMain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbSkinTypeMain.FormattingEnabled = true;
            this.cbbSkinTypeMain.Location = new System.Drawing.Point(74, 3);
            this.cbbSkinTypeMain.Name = "cbbSkinTypeMain";
            this.cbbSkinTypeMain.Size = new System.Drawing.Size(139, 20);
            this.cbbSkinTypeMain.TabIndex = 1;
            this.cbbSkinTypeMain.SelectedIndexChanged += new System.EventHandler(this.cbbSkinTypeMain_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 90);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "子窗体样式设置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 341F));
            this.tableLayoutPanel2.Controls.Add(this.btnSelectPicCommon, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.txbSkinValueCommon, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbbColorCommon, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbSkinTypeCommon, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(754, 70);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // btnSelectPicCommon
            // 
            this.btnSelectPicCommon.Location = new System.Drawing.Point(356, 38);
            this.btnSelectPicCommon.Name = "btnSelectPicCommon";
            this.btnSelectPicCommon.Size = new System.Drawing.Size(34, 23);
            this.btnSelectPicCommon.TabIndex = 6;
            this.btnSelectPicCommon.Text = "..";
            this.btnSelectPicCommon.UseVisualStyleBackColor = true;
            this.btnSelectPicCommon.Click += new System.EventHandler(this.btnSelectPicCommon_Click);
            // 
            // txbSkinValueCommon
            // 
            this.txbSkinValueCommon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.SetColumnSpan(this.txbSkinValueCommon, 2);
            this.txbSkinValueCommon.Enabled = false;
            this.txbSkinValueCommon.Location = new System.Drawing.Point(74, 38);
            this.txbSkinValueCommon.Multiline = true;
            this.txbSkinValueCommon.Name = "txbSkinValueCommon";
            this.txbSkinValueCommon.Size = new System.Drawing.Size(276, 29);
            this.txbSkinValueCommon.TabIndex = 7;
            // 
            // cbbColorCommon
            // 
            this.cbbColorCommon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbColorCommon.FormattingEnabled = true;
            this.cbbColorCommon.Location = new System.Drawing.Point(219, 7);
            this.cbbColorCommon.Name = "cbbColorCommon";
            this.cbbColorCommon.Size = new System.Drawing.Size(131, 20);
            this.cbbColorCommon.TabIndex = 8;
            // 
            // cbbSkinTypeCommon
            // 
            this.cbbSkinTypeCommon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbSkinTypeCommon.FormattingEnabled = true;
            this.cbbSkinTypeCommon.Location = new System.Drawing.Point(74, 7);
            this.cbbSkinTypeCommon.Name = "cbbSkinTypeCommon";
            this.cbbSkinTypeCommon.Size = new System.Drawing.Size(139, 20);
            this.cbbSkinTypeCommon.TabIndex = 5;
            this.cbbSkinTypeCommon.SelectedIndexChanged += new System.EventHandler(this.cbbSkinTypeCommon_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBase);
            this.tabControl1.Controls.Add(this.tpUserSkin);
            this.tabControl1.Controls.Add(this.tpOther);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 434);
            this.tabControl1.TabIndex = 9;
            // 
            // tpBase
            // 
            this.tpBase.Controls.Add(this.groupBox6);
            this.tpBase.Controls.Add(this.groupBox4);
            this.tpBase.Controls.Add(this.groupBox3);
            this.tpBase.Location = new System.Drawing.Point(4, 22);
            this.tpBase.Name = "tpBase";
            this.tpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tpBase.Size = new System.Drawing.Size(766, 408);
            this.tpBase.TabIndex = 1;
            this.tpBase.Text = "基本配置";
            this.tpBase.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ckbUpgradeDelNewZipFile);
            this.groupBox6.Controls.Add(this.ckbDelOldNeedConfirm);
            this.groupBox6.Controls.Add(this.ckbUpgradeSuccessDelOldVerion);
            this.groupBox6.Controls.Add(this.btnSelectUpgradeTmpPath);
            this.groupBox6.Controls.Add(this.txbUpgradeTempDir);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.ckbAutoCheckVersion);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 104);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(760, 67);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "升级配置";
            // 
            // ckbDelOldNeedConfirm
            // 
            this.ckbDelOldNeedConfirm.AutoSize = true;
            this.ckbDelOldNeedConfirm.Location = new System.Drawing.Point(273, 44);
            this.ckbDelOldNeedConfirm.Name = "ckbDelOldNeedConfirm";
            this.ckbDelOldNeedConfirm.Size = new System.Drawing.Size(132, 16);
            this.ckbDelOldNeedConfirm.TabIndex = 5;
            this.ckbDelOldNeedConfirm.Text = "删除旧版本需要确认";
            this.ckbDelOldNeedConfirm.UseVisualStyleBackColor = true;
            // 
            // ckbUpgradeSuccessDelOldVerion
            // 
            this.ckbUpgradeSuccessDelOldVerion.AutoSize = true;
            this.ckbUpgradeSuccessDelOldVerion.Location = new System.Drawing.Point(128, 44);
            this.ckbUpgradeSuccessDelOldVerion.Name = "ckbUpgradeSuccessDelOldVerion";
            this.ckbUpgradeSuccessDelOldVerion.Size = new System.Drawing.Size(132, 16);
            this.ckbUpgradeSuccessDelOldVerion.TabIndex = 4;
            this.ckbUpgradeSuccessDelOldVerion.Text = "升级成功删除旧版本";
            this.ckbUpgradeSuccessDelOldVerion.UseVisualStyleBackColor = true;
            // 
            // btnSelectUpgradeTmpPath
            // 
            this.btnSelectUpgradeTmpPath.Location = new System.Drawing.Point(703, 16);
            this.btnSelectUpgradeTmpPath.Name = "btnSelectUpgradeTmpPath";
            this.btnSelectUpgradeTmpPath.Size = new System.Drawing.Size(19, 23);
            this.btnSelectUpgradeTmpPath.TabIndex = 3;
            this.btnSelectUpgradeTmpPath.Text = ".";
            this.btnSelectUpgradeTmpPath.UseVisualStyleBackColor = true;
            this.btnSelectUpgradeTmpPath.Click += new System.EventHandler(this.btnSelectUpgradeTmpPath_Click);
            // 
            // txbUpgradeTempDir
            // 
            this.txbUpgradeTempDir.Location = new System.Drawing.Point(101, 17);
            this.txbUpgradeTempDir.Name = "txbUpgradeTempDir";
            this.txbUpgradeTempDir.ReadOnly = true;
            this.txbUpgradeTempDir.Size = new System.Drawing.Size(596, 21);
            this.txbUpgradeTempDir.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "下载临时目录：";
            // 
            // ckbAutoCheckVersion
            // 
            this.ckbAutoCheckVersion.AutoSize = true;
            this.ckbAutoCheckVersion.Location = new System.Drawing.Point(11, 44);
            this.ckbAutoCheckVersion.Name = "ckbAutoCheckVersion";
            this.ckbAutoCheckVersion.Size = new System.Drawing.Size(108, 16);
            this.ckbAutoCheckVersion.TabIndex = 0;
            this.ckbAutoCheckVersion.Text = "自动检测新版本";
            this.ckbAutoCheckVersion.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btnSelectMySettingPath);
            this.groupBox4.Controls.Add(this.txbMyLoveSettingPath);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(760, 49);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "个性化配置路径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "当前路径：";
            // 
            // btnSelectMySettingPath
            // 
            this.btnSelectMySettingPath.Location = new System.Drawing.Point(75, 16);
            this.btnSelectMySettingPath.Name = "btnSelectMySettingPath";
            this.btnSelectMySettingPath.Size = new System.Drawing.Size(23, 23);
            this.btnSelectMySettingPath.TabIndex = 2;
            this.btnSelectMySettingPath.Text = "...";
            this.btnSelectMySettingPath.UseVisualStyleBackColor = true;
            this.btnSelectMySettingPath.Click += new System.EventHandler(this.btnSelectMySettingPath_Click);
            // 
            // txbMyLoveSettingPath
            // 
            this.txbMyLoveSettingPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMyLoveSettingPath.Location = new System.Drawing.Point(101, 18);
            this.txbMyLoveSettingPath.Name = "txbMyLoveSettingPath";
            this.txbMyLoveSettingPath.Size = new System.Drawing.Size(643, 21);
            this.txbMyLoveSettingPath.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(760, 52);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "保存提示设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 378F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbMsgType, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "保存提示方式：";
            // 
            // cbbMsgType
            // 
            this.cbbMsgType.FormattingEnabled = true;
            this.cbbMsgType.Location = new System.Drawing.Point(98, 3);
            this.cbbMsgType.Name = "cbbMsgType";
            this.cbbMsgType.Size = new System.Drawing.Size(144, 20);
            this.cbbMsgType.TabIndex = 1;
            // 
            // tpUserSkin
            // 
            this.tpUserSkin.Controls.Add(this.groupBox2);
            this.tpUserSkin.Controls.Add(this.groupBox1);
            this.tpUserSkin.Location = new System.Drawing.Point(4, 22);
            this.tpUserSkin.Name = "tpUserSkin";
            this.tpUserSkin.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserSkin.Size = new System.Drawing.Size(766, 408);
            this.tpUserSkin.TabIndex = 0;
            this.tpUserSkin.Text = "窗体皮肤设置";
            this.tpUserSkin.UseVisualStyleBackColor = true;
            // 
            // tpOther
            // 
            this.tpOther.Controls.Add(this.groupBox5);
            this.tpOther.Location = new System.Drawing.Point(4, 22);
            this.tpOther.Name = "tpOther";
            this.tpOther.Padding = new System.Windows.Forms.Padding(3);
            this.tpOther.Size = new System.Drawing.Size(766, 408);
            this.tpOther.TabIndex = 2;
            this.tpOther.Text = "其他";
            this.tpOther.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblDgvStyleInfo);
            this.groupBox5.Controls.Add(this.btnClearDGVStyle);
            this.groupBox5.Controls.Add(this.txbGirdStyleTempPath);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(760, 46);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "临时文件";
            // 
            // lblDgvStyleInfo
            // 
            this.lblDgvStyleInfo.AutoSize = true;
            this.lblDgvStyleInfo.ForeColor = System.Drawing.Color.Red;
            this.lblDgvStyleInfo.Location = new System.Drawing.Point(101, 0);
            this.lblDgvStyleInfo.Name = "lblDgvStyleInfo";
            this.lblDgvStyleInfo.Size = new System.Drawing.Size(95, 12);
            this.lblDgvStyleInfo.TabIndex = 3;
            this.lblDgvStyleInfo.Text = "lblDgvStyleInfo";
            // 
            // btnClearDGVStyle
            // 
            this.btnClearDGVStyle.Location = new System.Drawing.Point(494, 16);
            this.btnClearDGVStyle.Name = "btnClearDGVStyle";
            this.btnClearDGVStyle.Size = new System.Drawing.Size(121, 23);
            this.btnClearDGVStyle.TabIndex = 2;
            this.btnClearDGVStyle.Text = "清除所有网格样式";
            this.btnClearDGVStyle.UseVisualStyleBackColor = true;
            this.btnClearDGVStyle.Click += new System.EventHandler(this.btnClearDGVStyle_Click);
            // 
            // txbGirdStyleTempPath
            // 
            this.txbGirdStyleTempPath.Location = new System.Drawing.Point(123, 18);
            this.txbGirdStyleTempPath.Name = "txbGirdStyleTempPath";
            this.txbGirdStyleTempPath.Size = new System.Drawing.Size(365, 21);
            this.txbGirdStyleTempPath.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "网格样式临时文件：";
            // 
            // ckbUpgradeDelNewZipFile
            // 
            this.ckbUpgradeDelNewZipFile.AutoSize = true;
            this.ckbUpgradeDelNewZipFile.Location = new System.Drawing.Point(411, 44);
            this.ckbUpgradeDelNewZipFile.Name = "ckbUpgradeDelNewZipFile";
            this.ckbUpgradeDelNewZipFile.Size = new System.Drawing.Size(144, 16);
            this.ckbUpgradeDelNewZipFile.TabIndex = 6;
            this.ckbUpgradeDelNewZipFile.Text = "升级成功删除新压缩包";
            this.ckbUpgradeDelNewZipFile.UseVisualStyleBackColor = true;
            // 
            // FrmUserEnvironmentSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 459);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "FrmUserEnvironmentSet";
            this.Text = "用户环境设置";
            this.Load += new System.EventHandler(this.FrmUserSkinSet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpBase.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tpUserSkin.ResumeLayout(false);
            this.tpOther.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog cdlSelectColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbSkinTypeMain;
        private System.Windows.Forms.Button btnSelectPicMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbbSkinTypeCommon;
        private System.Windows.Forms.Button btnSelectPicCommon;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.TextBox txbSkinValueMain;
        private System.Windows.Forms.TextBox txbSkinValueCommon;
        private System.Windows.Forms.OpenFileDialog opfSelectPic;
        private System.Windows.Forms.ComboBox cbbColorMain;
        private System.Windows.Forms.ComboBox cbbColorCommon;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpUserSkin;
        private System.Windows.Forms.TabPage tpBase;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbMsgType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectMySettingPath;
        private System.Windows.Forms.TextBox txbMyLoveSettingPath;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectPath;
        private System.Windows.Forms.TabPage tpOther;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClearDGVStyle;
        private System.Windows.Forms.TextBox txbGirdStyleTempPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDgvStyleInfo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSelectUpgradeTmpPath;
        private System.Windows.Forms.TextBox txbUpgradeTempDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbAutoCheckVersion;
        private System.Windows.Forms.CheckBox ckbUpgradeSuccessDelOldVerion;
        private System.Windows.Forms.CheckBox ckbDelOldNeedConfirm;
        private System.Windows.Forms.CheckBox ckbUpgradeDelNewZipFile;
    }
}