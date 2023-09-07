namespace Breezee.WorkHelper.DBTool.UI
{
	partial class FrmDBTDBAutoEntityFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTDBAutoEntityFile));
            this.ctxmBuilt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFtable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStable = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnImport = new System.Windows.Forms.Button();
            this.cbbModuleList = new System.Windows.Forms.ComboBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPath = new System.Windows.Forms.Button();
            this.txbRemoveTablePre = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tvDataBaseInfo = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rtbEntityTemplate = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMyDefine = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvSysParam = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvTypeConvert = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLinkServer = new System.Windows.Forms.ToolStripButton();
            this.tsbDownloadModel = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.txbModuleFileSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctxmBuilt.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDefine)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSysParam)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeConvert)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxmBuilt
            // 
            this.ctxmBuilt.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxmBuilt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFtable,
            this.tsmiStable});
            this.ctxmBuilt.Name = "ctxmBuilt";
            this.ctxmBuilt.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiFtable
            // 
            this.tsmiFtable.Name = "tsmiFtable";
            this.tsmiFtable.Size = new System.Drawing.Size(124, 22);
            this.tsmiFtable.Text = "全部生成";
            this.tsmiFtable.Click += new System.EventHandler(this.tsmiFtable_Click);
            // 
            // tsmiStable
            // 
            this.tsmiStable.Name = "tsmiStable";
            this.tsmiStable.Size = new System.Drawing.Size(124, 22);
            this.tsmiStable.Text = "单表生成";
            this.tsmiStable.Click += new System.EventHandler(this.tsmiStable_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvTableInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 199);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表的列信息";
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.RowTemplate.Height = 23;
            this.dgvTableInfo.Size = new System.Drawing.Size(957, 179);
            this.dgvTableInfo.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 80);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "生成的类信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnImport, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbModuleList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPath, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbRemoveTablePre, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbModuleFileSuffix, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(370, 60);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnImport
            // 
            this.btnImport.ForeColor = System.Drawing.Color.Red;
            this.btnImport.Location = new System.Drawing.Point(3, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(63, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cbbModuleList
            // 
            this.cbbModuleList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbModuleList.FormattingEnabled = true;
            this.cbbModuleList.Location = new System.Drawing.Point(72, 4);
            this.cbbModuleList.Name = "cbbModuleList";
            this.cbbModuleList.Size = new System.Drawing.Size(142, 20);
            this.cbbModuleList.TabIndex = 7;
            this.cbbModuleList.SelectedIndexChanged += new System.EventHandler(this.cbbModuleList_SelectedIndexChanged);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(72, 33);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(142, 21);
            this.txtPath.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(220, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "剔除表前缀：";
            // 
            // btnPath
            // 
            this.btnPath.ForeColor = System.Drawing.Color.Red;
            this.btnPath.Location = new System.Drawing.Point(3, 32);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(63, 23);
            this.btnPath.TabIndex = 6;
            this.btnPath.Text = "保存路径";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // txbRemoveTablePre
            // 
            this.txbRemoveTablePre.Location = new System.Drawing.Point(303, 32);
            this.txbRemoveTablePre.Name = "txbRemoveTablePre";
            this.txbRemoveTablePre.Size = new System.Drawing.Size(49, 21);
            this.txbRemoveTablePre.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvDataBaseInfo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 479);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库清单";
            // 
            // tvDataBaseInfo
            // 
            this.tvDataBaseInfo.ContextMenuStrip = this.ctxmBuilt;
            this.tvDataBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDataBaseInfo.Location = new System.Drawing.Point(3, 17);
            this.tvDataBaseInfo.Name = "tvDataBaseInfo";
            this.tvDataBaseInfo.Size = new System.Drawing.Size(370, 459);
            this.tvDataBaseInfo.TabIndex = 5;
            this.tvDataBaseInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDataBaseInfo_AfterSelect);
            this.tvDataBaseInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDataBaseInfo_NodeMouseClick);
            this.tvDataBaseInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvDataBaseInfo_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(963, 356);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rtbEntityTemplate);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(520, 356);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "实体文件内容总模板";
            // 
            // rtbEntityTemplate
            // 
            this.rtbEntityTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEntityTemplate.Location = new System.Drawing.Point(2, 16);
            this.rtbEntityTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.rtbEntityTemplate.Name = "rtbEntityTemplate";
            this.rtbEntityTemplate.Size = new System.Drawing.Size(516, 338);
            this.rtbEntityTemplate.TabIndex = 1;
            this.rtbEntityTemplate.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(440, 356);
            this.tabControl2.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMyDefine);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(432, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "自定义变量";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMyDefine
            // 
            this.dgvMyDefine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyDefine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyDefine.Location = new System.Drawing.Point(3, 3);
            this.dgvMyDefine.Name = "dgvMyDefine";
            this.dgvMyDefine.RowTemplate.Height = 23;
            this.dgvMyDefine.Size = new System.Drawing.Size(426, 324);
            this.dgvMyDefine.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvSysParam);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(432, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "系统变量";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvSysParam
            // 
            this.dgvSysParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSysParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSysParam.Location = new System.Drawing.Point(3, 3);
            this.dgvSysParam.Name = "dgvSysParam";
            this.dgvSysParam.RowTemplate.Height = 23;
            this.dgvSysParam.Size = new System.Drawing.Size(426, 324);
            this.dgvSysParam.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvTypeConvert);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(432, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "类型转换";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvTypeConvert
            // 
            this.dgvTypeConvert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypeConvert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTypeConvert.Location = new System.Drawing.Point(0, 0);
            this.dgvTypeConvert.Name = "dgvTypeConvert";
            this.dgvTypeConvert.RowTemplate.Height = 23;
            this.dgvTypeConvert.Size = new System.Drawing.Size(432, 330);
            this.dgvTypeConvert.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLinkServer,
            this.tsbDownloadModel,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1343, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLinkServer
            // 
            this.btnLinkServer.Image = ((System.Drawing.Image)(resources.GetObject("btnLinkServer.Image")));
            this.btnLinkServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLinkServer.Name = "btnLinkServer";
            this.btnLinkServer.Size = new System.Drawing.Size(106, 24);
            this.btnLinkServer.Text = "连接服务器(&L)";
            this.btnLinkServer.Click += new System.EventHandler(this.btnLinkServer_Click);
            // 
            // tsbDownloadModel
            // 
            this.tsbDownloadModel.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownloadModel.Image")));
            this.tsbDownloadModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownloadModel.Name = "tsbDownloadModel";
            this.tsbDownloadModel.Size = new System.Drawing.Size(80, 24);
            this.tsbDownloadModel.Text = "模板下载";
            this.tsbDownloadModel.Click += new System.EventHandler(this.tsbDownloadModel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 24);
            this.btnExit.Text = "退出(&X)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 98);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1343, 559);
            this.splitContainer2.SplitterDistance = 376;
            this.splitContainer2.TabIndex = 16;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(963, 559);
            this.splitContainer3.SplitterDistance = 199;
            this.splitContainer3.TabIndex = 23;
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1343, 71);
            this.uC_DbConnection1.TabIndex = 20;
            // 
            // txbFileExtName
            // 
            this.txbModuleFileSuffix.Location = new System.Drawing.Point(303, 3);
            this.txbModuleFileSuffix.Name = "txbFileExtName";
            this.txbModuleFileSuffix.Size = new System.Drawing.Size(49, 21);
            this.txbModuleFileSuffix.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(232, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件后缀：";
            // 
            // FrmDBTDBAutoEntityFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 657);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTDBAutoEntityFile";
            this.Text = "生成实体工具";
            this.Load += new System.EventHandler(this.FrmAutoEntity_Load);
            this.ctxmBuilt.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDefine)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSysParam)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeConvert)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLinkServer;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ContextMenuStrip ctxmBuilt;
        private System.Windows.Forms.ToolStripMenuItem tsmiFtable;
        private System.Windows.Forms.ToolStripMenuItem tsmiStable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView tvDataBaseInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label5;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox rtbEntityTemplate;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvMyDefine;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvSysParam;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvTypeConvert;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox cbbModuleList;
        private System.Windows.Forms.ToolStripButton tsbDownloadModel;
        private System.Windows.Forms.TextBox txbRemoveTablePre;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox txbModuleFileSuffix;
        private System.Windows.Forms.Label label1;
    }
}