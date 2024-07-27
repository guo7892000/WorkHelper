namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmSqlStringConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlStringConvert));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSourceDb = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblFindSource = new System.Windows.Forms.Label();
            this.txbSearchTableNameSource = new System.Windows.Forms.TextBox();
            this.btnFindFrontSource = new System.Windows.Forms.Button();
            this.btnFindNextSource = new System.Windows.Forms.Button();
            this.dgvTableListSource = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadDataSource = new System.Windows.Forms.Button();
            this.btnExcludeTableSource = new System.Windows.Forms.Button();
            this.txbExcludeTableSource = new System.Windows.Forms.TextBox();
            this.ckbNotIncludeSplitTableSource = new System.Windows.Forms.CheckBox();
            this.ckbIsAutoExcludeSource = new System.Windows.Forms.CheckBox();
            this.ckbAllTableColumnSource = new System.Windows.Forms.CheckBox();
            this.btnSourceConnect = new System.Windows.Forms.Button();
            this.ckbClearAllColSource = new System.Windows.Forms.CheckBox();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.dgvColListSource = new System.Windows.Forms.DataGridView();
            this.tpTargetDb = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblFindTarget = new System.Windows.Forms.Label();
            this.txbSearchTableNameTarget = new System.Windows.Forms.TextBox();
            this.btnFindFrontTarget = new System.Windows.Forms.Button();
            this.btnFindNextTarget = new System.Windows.Forms.Button();
            this.dgvTableListTarget = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadDataTarget = new System.Windows.Forms.Button();
            this.ckbClearAllColTarget = new System.Windows.Forms.CheckBox();
            this.btnExcludeTableTarget = new System.Windows.Forms.Button();
            this.txbExcludeTableTarget = new System.Windows.Forms.TextBox();
            this.ckbNotIncludeSplitTableTarget = new System.Windows.Forms.CheckBox();
            this.ckbIsAutoExcludeTarget = new System.Windows.Forms.CheckBox();
            this.ckbAllTableColumnTarget = new System.Windows.Forms.CheckBox();
            this.btnTargetConnect = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvColListTarget = new System.Windows.Forms.DataGridView();
            this.tpConvert = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grbInputSql = new System.Windows.Forms.GroupBox();
            this.rtbInputSql = new System.Windows.Forms.RichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTargetDbType = new System.Windows.Forms.Label();
            this.cbbTargetDbType = new System.Windows.Forms.ComboBox();
            this.lblSourceDbType = new System.Windows.Forms.Label();
            this.cbbSourceDbType = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadAllGenerate = new System.Windows.Forms.Button();
            this.ckbParamToHash = new System.Windows.Forms.CheckBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tpNewOldTable = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.lblReplaceInfo = new System.Windows.Forms.Label();
            this.dgvOldNewChar = new System.Windows.Forms.DataGridView();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTemplateType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txbReplaceTemplateName = new System.Windows.Forms.TextBox();
            this.btnSaveReplaceTemplate = new System.Windows.Forms.Button();
            this.btnRemoveTemplate = new System.Windows.Forms.Button();
            this.tpNewOldColum = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvOldNewColumnChar = new System.Windows.Forms.DataGridView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbColumnTemplateType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbReplaceColumnTemplateName = new System.Windows.Forms.TextBox();
            this.btnSaveColumnReplaceTemplate = new System.Windows.Forms.Button();
            this.btnRemoveColumnTemplate = new System.Windows.Forms.Button();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvBeReplaceCol = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbConvertRemark = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFuncInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnConnectLoadAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbNewOldColumnSourceType = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.uC_DbConnectionTarget = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.uC_DbConnectionSource = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.ckbTableFixedTarget = new System.Windows.Forms.CheckBox();
            this.ckbTableFixedSource = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSourceDb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableListSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListSource)).BeginInit();
            this.tpTargetDb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableListTarget)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListTarget)).BeginInit();
            this.tpConvert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grbInputSql.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tpNewOldTable.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldNewChar)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tpNewOldColum.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldNewColumnChar)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeReplaceCol)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAutoSQL,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1016, 27);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(72, 24);
            this.tsbAutoSQL.Text = "生成(&A)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.tsbAutoSQL_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(72, 24);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSourceDb);
            this.tabControl1.Controls.Add(this.tpTargetDb);
            this.tabControl1.Controls.Add(this.tpConvert);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 239);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 449);
            this.tabControl1.TabIndex = 37;
            // 
            // tpSourceDb
            // 
            this.tpSourceDb.Controls.Add(this.splitContainer1);
            this.tpSourceDb.Location = new System.Drawing.Point(4, 22);
            this.tpSourceDb.Name = "tpSourceDb";
            this.tpSourceDb.Padding = new System.Windows.Forms.Padding(3);
            this.tpSourceDb.Size = new System.Drawing.Size(1008, 423);
            this.tpSourceDb.TabIndex = 0;
            this.tpSourceDb.Text = "源数据库";
            this.tpSourceDb.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbColumn);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 417);
            this.splitContainer1.SplitterDistance = 496;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ckbTableFixedSource);
            this.groupBox7.Controls.Add(this.lblFindSource);
            this.groupBox7.Controls.Add(this.txbSearchTableNameSource);
            this.groupBox7.Controls.Add(this.btnFindFrontSource);
            this.groupBox7.Controls.Add(this.btnFindNextSource);
            this.groupBox7.Controls.Add(this.dgvTableListSource);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.ForeColor = System.Drawing.Color.Black;
            this.groupBox7.Location = new System.Drawing.Point(0, 80);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(496, 337);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "表清单";
            // 
            // lblFindSource
            // 
            this.lblFindSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindSource.AutoSize = true;
            this.lblFindSource.ForeColor = System.Drawing.Color.Red;
            this.lblFindSource.Location = new System.Drawing.Point(346, 1);
            this.lblFindSource.Name = "lblFindSource";
            this.lblFindSource.Size = new System.Drawing.Size(53, 12);
            this.lblFindSource.TabIndex = 22;
            this.lblFindSource.Text = "查找提示";
            // 
            // txbSearchTableNameSource
            // 
            this.txbSearchTableNameSource.Location = new System.Drawing.Point(47, -3);
            this.txbSearchTableNameSource.Name = "txbSearchTableNameSource";
            this.txbSearchTableNameSource.Size = new System.Drawing.Size(128, 21);
            this.txbSearchTableNameSource.TabIndex = 21;
            // 
            // btnFindFrontSource
            // 
            this.btnFindFrontSource.Location = new System.Drawing.Point(285, -3);
            this.btnFindFrontSource.Name = "btnFindFrontSource";
            this.btnFindFrontSource.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontSource.TabIndex = 20;
            this.btnFindFrontSource.Text = "往前找";
            this.btnFindFrontSource.UseVisualStyleBackColor = true;
            this.btnFindFrontSource.Click += new System.EventHandler(this.btnFindFrontSource_Click);
            // 
            // btnFindNextSource
            // 
            this.btnFindNextSource.Location = new System.Drawing.Point(228, -3);
            this.btnFindNextSource.Name = "btnFindNextSource";
            this.btnFindNextSource.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextSource.TabIndex = 19;
            this.btnFindNextSource.Text = "下一个";
            this.btnFindNextSource.UseVisualStyleBackColor = true;
            this.btnFindNextSource.Click += new System.EventHandler(this.btnFindNextSource_Click);
            // 
            // dgvTableListSource
            // 
            this.dgvTableListSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableListSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableListSource.Location = new System.Drawing.Point(3, 17);
            this.dgvTableListSource.Name = "dgvTableListSource";
            this.dgvTableListSource.RowTemplate.Height = 23;
            this.dgvTableListSource.Size = new System.Drawing.Size(490, 317);
            this.dgvTableListSource.TabIndex = 0;
            this.dgvTableListSource.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableListSource_ColumnHeaderMouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnLoadDataSource, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnExcludeTableSource, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.txbExcludeTableSource, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.ckbNotIncludeSplitTableSource, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbIsAutoExcludeSource, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.ckbAllTableColumnSource, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSourceConnect, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbClearAllColSource, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(490, 61);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // btnLoadDataSource
            // 
            this.btnLoadDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadDataSource.Location = new System.Drawing.Point(3, 32);
            this.btnLoadDataSource.Name = "btnLoadDataSource";
            this.btnLoadDataSource.Size = new System.Drawing.Size(72, 23);
            this.btnLoadDataSource.TabIndex = 3;
            this.btnLoadDataSource.Text = "加载数据";
            this.btnLoadDataSource.UseVisualStyleBackColor = true;
            this.btnLoadDataSource.Click += new System.EventHandler(this.btnLoadDataSource_Click);
            // 
            // btnExcludeTableSource
            // 
            this.btnExcludeTableSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcludeTableSource.Location = new System.Drawing.Point(327, 32);
            this.btnExcludeTableSource.Name = "btnExcludeTableSource";
            this.btnExcludeTableSource.Size = new System.Drawing.Size(60, 23);
            this.btnExcludeTableSource.TabIndex = 22;
            this.btnExcludeTableSource.Text = "排除";
            this.btnExcludeTableSource.UseVisualStyleBackColor = true;
            this.btnExcludeTableSource.Click += new System.EventHandler(this.btnExcludeTableSource_Click);
            // 
            // txbExcludeTableSource
            // 
            this.txbExcludeTableSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.txbExcludeTableSource, 2);
            this.txbExcludeTableSource.Location = new System.Drawing.Point(159, 33);
            this.txbExcludeTableSource.Name = "txbExcludeTableSource";
            this.txbExcludeTableSource.Size = new System.Drawing.Size(162, 21);
            this.txbExcludeTableSource.TabIndex = 15;
            // 
            // ckbNotIncludeSplitTableSource
            // 
            this.ckbNotIncludeSplitTableSource.AutoSize = true;
            this.ckbNotIncludeSplitTableSource.Checked = true;
            this.ckbNotIncludeSplitTableSource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNotIncludeSplitTableSource.Location = new System.Drawing.Point(3, 3);
            this.ckbNotIncludeSplitTableSource.Name = "ckbNotIncludeSplitTableSource";
            this.ckbNotIncludeSplitTableSource.Size = new System.Drawing.Size(72, 16);
            this.ckbNotIncludeSplitTableSource.TabIndex = 7;
            this.ckbNotIncludeSplitTableSource.Text = "排除分表";
            this.ckbNotIncludeSplitTableSource.UseVisualStyleBackColor = true;
            // 
            // ckbIsAutoExcludeSource
            // 
            this.ckbIsAutoExcludeSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbIsAutoExcludeSource.AutoSize = true;
            this.ckbIsAutoExcludeSource.Location = new System.Drawing.Point(81, 35);
            this.ckbIsAutoExcludeSource.Name = "ckbIsAutoExcludeSource";
            this.ckbIsAutoExcludeSource.Size = new System.Drawing.Size(72, 16);
            this.ckbIsAutoExcludeSource.TabIndex = 23;
            this.ckbIsAutoExcludeSource.Text = "自动排除";
            this.ckbIsAutoExcludeSource.UseVisualStyleBackColor = true;
            // 
            // ckbAllTableColumnSource
            // 
            this.ckbAllTableColumnSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAllTableColumnSource.AutoSize = true;
            this.ckbAllTableColumnSource.Location = new System.Drawing.Point(159, 6);
            this.ckbAllTableColumnSource.Name = "ckbAllTableColumnSource";
            this.ckbAllTableColumnSource.Size = new System.Drawing.Size(60, 16);
            this.ckbAllTableColumnSource.TabIndex = 3;
            this.ckbAllTableColumnSource.Text = "所有表";
            this.ckbAllTableColumnSource.UseVisualStyleBackColor = true;
            // 
            // btnSourceConnect
            // 
            this.btnSourceConnect.Location = new System.Drawing.Point(225, 3);
            this.btnSourceConnect.Name = "btnSourceConnect";
            this.btnSourceConnect.Size = new System.Drawing.Size(65, 23);
            this.btnSourceConnect.TabIndex = 24;
            this.btnSourceConnect.Text = "连接";
            this.btnSourceConnect.UseVisualStyleBackColor = true;
            this.btnSourceConnect.Click += new System.EventHandler(this.btnSourceConnect_Click);
            // 
            // ckbClearAllColSource
            // 
            this.ckbClearAllColSource.AutoSize = true;
            this.ckbClearAllColSource.Checked = true;
            this.ckbClearAllColSource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbClearAllColSource.Location = new System.Drawing.Point(81, 3);
            this.ckbClearAllColSource.Name = "ckbClearAllColSource";
            this.ckbClearAllColSource.Size = new System.Drawing.Size(72, 16);
            this.ckbClearAllColSource.TabIndex = 5;
            this.ckbClearAllColSource.Text = "清除表列";
            this.ckbClearAllColSource.UseVisualStyleBackColor = true;
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.dgvColListSource);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(0, 0);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(502, 417);
            this.grbColumn.TabIndex = 9;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // dgvColListSource
            // 
            this.dgvColListSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColListSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColListSource.Location = new System.Drawing.Point(3, 17);
            this.dgvColListSource.Name = "dgvColListSource";
            this.dgvColListSource.RowTemplate.Height = 23;
            this.dgvColListSource.Size = new System.Drawing.Size(496, 397);
            this.dgvColListSource.TabIndex = 0;
            // 
            // tpTargetDb
            // 
            this.tpTargetDb.Controls.Add(this.splitContainer4);
            this.tpTargetDb.Location = new System.Drawing.Point(4, 22);
            this.tpTargetDb.Name = "tpTargetDb";
            this.tpTargetDb.Size = new System.Drawing.Size(1008, 423);
            this.tpTargetDb.TabIndex = 2;
            this.tpTargetDb.Text = "目标数据库";
            this.tpTargetDb.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer4.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer4.Size = new System.Drawing.Size(1008, 423);
            this.splitContainer4.SplitterDistance = 503;
            this.splitContainer4.TabIndex = 38;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbTableFixedTarget);
            this.groupBox4.Controls.Add(this.lblFindTarget);
            this.groupBox4.Controls.Add(this.txbSearchTableNameTarget);
            this.groupBox4.Controls.Add(this.btnFindFrontTarget);
            this.groupBox4.Controls.Add(this.btnFindNextTarget);
            this.groupBox4.Controls.Add(this.dgvTableListTarget);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(0, 80);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(503, 343);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "表清单";
            // 
            // lblFindTarget
            // 
            this.lblFindTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindTarget.AutoSize = true;
            this.lblFindTarget.ForeColor = System.Drawing.Color.Red;
            this.lblFindTarget.Location = new System.Drawing.Point(353, 1);
            this.lblFindTarget.Name = "lblFindTarget";
            this.lblFindTarget.Size = new System.Drawing.Size(53, 12);
            this.lblFindTarget.TabIndex = 22;
            this.lblFindTarget.Text = "查找提示";
            // 
            // txbSearchTableNameTarget
            // 
            this.txbSearchTableNameTarget.Location = new System.Drawing.Point(47, -3);
            this.txbSearchTableNameTarget.Name = "txbSearchTableNameTarget";
            this.txbSearchTableNameTarget.Size = new System.Drawing.Size(128, 21);
            this.txbSearchTableNameTarget.TabIndex = 21;
            // 
            // btnFindFrontTarget
            // 
            this.btnFindFrontTarget.Location = new System.Drawing.Point(292, -3);
            this.btnFindFrontTarget.Name = "btnFindFrontTarget";
            this.btnFindFrontTarget.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontTarget.TabIndex = 20;
            this.btnFindFrontTarget.Text = "往前找";
            this.btnFindFrontTarget.UseVisualStyleBackColor = true;
            this.btnFindFrontTarget.Click += new System.EventHandler(this.btnFindFrontTarget_Click);
            // 
            // btnFindNextTarget
            // 
            this.btnFindNextTarget.Location = new System.Drawing.Point(235, -3);
            this.btnFindNextTarget.Name = "btnFindNextTarget";
            this.btnFindNextTarget.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextTarget.TabIndex = 19;
            this.btnFindNextTarget.Text = "下一个";
            this.btnFindNextTarget.UseVisualStyleBackColor = true;
            this.btnFindNextTarget.Click += new System.EventHandler(this.btnFindNextTarget_Click);
            // 
            // dgvTableListTarget
            // 
            this.dgvTableListTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableListTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableListTarget.Location = new System.Drawing.Point(3, 17);
            this.dgvTableListTarget.Name = "dgvTableListTarget";
            this.dgvTableListTarget.RowTemplate.Height = 23;
            this.dgvTableListTarget.Size = new System.Drawing.Size(497, 323);
            this.dgvTableListTarget.TabIndex = 0;
            this.dgvTableListTarget.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableListTarget_ColumnHeaderMouseDoubleClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(503, 80);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "选项";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnLoadDataTarget, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.ckbClearAllColTarget, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnExcludeTableTarget, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.txbExcludeTableTarget, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.ckbNotIncludeSplitTableTarget, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.ckbIsAutoExcludeTarget, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.ckbAllTableColumnTarget, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnTargetConnect, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(497, 57);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // btnLoadDataTarget
            // 
            this.btnLoadDataTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadDataTarget.Location = new System.Drawing.Point(3, 32);
            this.btnLoadDataTarget.Name = "btnLoadDataTarget";
            this.btnLoadDataTarget.Size = new System.Drawing.Size(72, 23);
            this.btnLoadDataTarget.TabIndex = 3;
            this.btnLoadDataTarget.Text = "加载数据";
            this.btnLoadDataTarget.UseVisualStyleBackColor = true;
            this.btnLoadDataTarget.Click += new System.EventHandler(this.btnLoadDataTarget_Click);
            // 
            // ckbClearAllColTarget
            // 
            this.ckbClearAllColTarget.AutoSize = true;
            this.ckbClearAllColTarget.Checked = true;
            this.ckbClearAllColTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbClearAllColTarget.Location = new System.Drawing.Point(81, 3);
            this.ckbClearAllColTarget.Name = "ckbClearAllColTarget";
            this.ckbClearAllColTarget.Size = new System.Drawing.Size(72, 16);
            this.ckbClearAllColTarget.TabIndex = 5;
            this.ckbClearAllColTarget.Text = "清除表列";
            this.ckbClearAllColTarget.UseVisualStyleBackColor = true;
            // 
            // btnExcludeTableTarget
            // 
            this.btnExcludeTableTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcludeTableTarget.Location = new System.Drawing.Point(327, 32);
            this.btnExcludeTableTarget.Name = "btnExcludeTableTarget";
            this.btnExcludeTableTarget.Size = new System.Drawing.Size(60, 23);
            this.btnExcludeTableTarget.TabIndex = 22;
            this.btnExcludeTableTarget.Text = "排除";
            this.btnExcludeTableTarget.UseVisualStyleBackColor = true;
            this.btnExcludeTableTarget.Click += new System.EventHandler(this.btnExcludeTableTarget_Click);
            // 
            // txbExcludeTableTarget
            // 
            this.txbExcludeTableTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.SetColumnSpan(this.txbExcludeTableTarget, 2);
            this.txbExcludeTableTarget.Location = new System.Drawing.Point(159, 33);
            this.txbExcludeTableTarget.Name = "txbExcludeTableTarget";
            this.txbExcludeTableTarget.Size = new System.Drawing.Size(162, 21);
            this.txbExcludeTableTarget.TabIndex = 15;
            // 
            // ckbNotIncludeSplitTableTarget
            // 
            this.ckbNotIncludeSplitTableTarget.AutoSize = true;
            this.ckbNotIncludeSplitTableTarget.Checked = true;
            this.ckbNotIncludeSplitTableTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNotIncludeSplitTableTarget.Location = new System.Drawing.Point(3, 3);
            this.ckbNotIncludeSplitTableTarget.Name = "ckbNotIncludeSplitTableTarget";
            this.ckbNotIncludeSplitTableTarget.Size = new System.Drawing.Size(72, 16);
            this.ckbNotIncludeSplitTableTarget.TabIndex = 7;
            this.ckbNotIncludeSplitTableTarget.Text = "排除分表";
            this.ckbNotIncludeSplitTableTarget.UseVisualStyleBackColor = true;
            // 
            // ckbIsAutoExcludeTarget
            // 
            this.ckbIsAutoExcludeTarget.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbIsAutoExcludeTarget.AutoSize = true;
            this.ckbIsAutoExcludeTarget.Location = new System.Drawing.Point(81, 35);
            this.ckbIsAutoExcludeTarget.Name = "ckbIsAutoExcludeTarget";
            this.ckbIsAutoExcludeTarget.Size = new System.Drawing.Size(72, 16);
            this.ckbIsAutoExcludeTarget.TabIndex = 23;
            this.ckbIsAutoExcludeTarget.Text = "自动排除";
            this.ckbIsAutoExcludeTarget.UseVisualStyleBackColor = true;
            // 
            // ckbAllTableColumnTarget
            // 
            this.ckbAllTableColumnTarget.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAllTableColumnTarget.AutoSize = true;
            this.ckbAllTableColumnTarget.Location = new System.Drawing.Point(159, 6);
            this.ckbAllTableColumnTarget.Name = "ckbAllTableColumnTarget";
            this.ckbAllTableColumnTarget.Size = new System.Drawing.Size(60, 16);
            this.ckbAllTableColumnTarget.TabIndex = 3;
            this.ckbAllTableColumnTarget.Text = "所有表";
            this.ckbAllTableColumnTarget.UseVisualStyleBackColor = true;
            // 
            // btnTargetConnect
            // 
            this.btnTargetConnect.Location = new System.Drawing.Point(225, 3);
            this.btnTargetConnect.Name = "btnTargetConnect";
            this.btnTargetConnect.Size = new System.Drawing.Size(65, 23);
            this.btnTargetConnect.TabIndex = 25;
            this.btnTargetConnect.Text = "连接";
            this.btnTargetConnect.UseVisualStyleBackColor = true;
            this.btnTargetConnect.Click += new System.EventHandler(this.btnTargetConnect_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox6.Controls.Add(this.dgvColListTarget);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(501, 423);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "列清单";
            // 
            // dgvColListTarget
            // 
            this.dgvColListTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColListTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColListTarget.Location = new System.Drawing.Point(3, 17);
            this.dgvColListTarget.Name = "dgvColListTarget";
            this.dgvColListTarget.RowTemplate.Height = 23;
            this.dgvColListTarget.Size = new System.Drawing.Size(495, 403);
            this.dgvColListTarget.TabIndex = 0;
            // 
            // tpConvert
            // 
            this.tpConvert.Controls.Add(this.splitContainer2);
            this.tpConvert.Location = new System.Drawing.Point(4, 22);
            this.tpConvert.Name = "tpConvert";
            this.tpConvert.Padding = new System.Windows.Forms.Padding(3);
            this.tpConvert.Size = new System.Drawing.Size(1008, 423);
            this.tpConvert.TabIndex = 1;
            this.tpConvert.Text = "SQL转换";
            this.tpConvert.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer2.Size = new System.Drawing.Size(1002, 417);
            this.splitContainer2.SplitterDistance = 617;
            this.splitContainer2.TabIndex = 0;
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
            this.splitContainer3.Panel1.Controls.Add(this.grbInputSql);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox8);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer3.Size = new System.Drawing.Size(617, 417);
            this.splitContainer3.SplitterDistance = 220;
            this.splitContainer3.TabIndex = 0;
            // 
            // grbInputSql
            // 
            this.grbInputSql.Controls.Add(this.rtbInputSql);
            this.grbInputSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbInputSql.Location = new System.Drawing.Point(0, 51);
            this.grbInputSql.Name = "grbInputSql";
            this.grbInputSql.Size = new System.Drawing.Size(617, 169);
            this.grbInputSql.TabIndex = 18;
            this.grbInputSql.TabStop = false;
            this.grbInputSql.Text = "要转换的SQL";
            // 
            // rtbInputSql
            // 
            this.rtbInputSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInputSql.Location = new System.Drawing.Point(3, 17);
            this.rtbInputSql.Name = "rtbInputSql";
            this.rtbInputSql.Size = new System.Drawing.Size(611, 149);
            this.rtbInputSql.TabIndex = 16;
            this.rtbInputSql.Text = "";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tableLayoutPanel4);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(617, 51);
            this.groupBox8.TabIndex = 19;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "转换选项";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.Controls.Add(this.lblTargetDbType, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTargetDbType, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblSourceDbType, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbSourceDbType, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnGenerate, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnLoadAllGenerate, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbParamToHash, 6, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(611, 33);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // lblTargetDbType
            // 
            this.lblTargetDbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTargetDbType.AutoSize = true;
            this.lblTargetDbType.Location = new System.Drawing.Point(183, 8);
            this.lblTargetDbType.Name = "lblTargetDbType";
            this.lblTargetDbType.Size = new System.Drawing.Size(77, 12);
            this.lblTargetDbType.TabIndex = 10;
            this.lblTargetDbType.Text = "目标DB类型：";
            // 
            // cbbTargetDbType
            // 
            this.cbbTargetDbType.FormattingEnabled = true;
            this.cbbTargetDbType.Location = new System.Drawing.Point(266, 3);
            this.cbbTargetDbType.Name = "cbbTargetDbType";
            this.cbbTargetDbType.Size = new System.Drawing.Size(103, 20);
            this.cbbTargetDbType.TabIndex = 11;
            // 
            // lblSourceDbType
            // 
            this.lblSourceDbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSourceDbType.AutoSize = true;
            this.lblSourceDbType.Location = new System.Drawing.Point(3, 8);
            this.lblSourceDbType.Name = "lblSourceDbType";
            this.lblSourceDbType.Size = new System.Drawing.Size(65, 12);
            this.lblSourceDbType.TabIndex = 14;
            this.lblSourceDbType.Text = "源DB类型：";
            // 
            // cbbSourceDbType
            // 
            this.cbbSourceDbType.FormattingEnabled = true;
            this.cbbSourceDbType.Location = new System.Drawing.Point(74, 3);
            this.cbbSourceDbType.Name = "cbbSourceDbType";
            this.cbbSourceDbType.Size = new System.Drawing.Size(103, 20);
            this.cbbSourceDbType.TabIndex = 15;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGenerate.ForeColor = System.Drawing.Color.Red;
            this.btnGenerate.Location = new System.Drawing.Point(375, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(53, 23);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadAllGenerate
            // 
            this.btnLoadAllGenerate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLoadAllGenerate.ForeColor = System.Drawing.Color.Red;
            this.btnLoadAllGenerate.Location = new System.Drawing.Point(434, 3);
            this.btnLoadAllGenerate.Name = "btnLoadAllGenerate";
            this.btnLoadAllGenerate.Size = new System.Drawing.Size(79, 23);
            this.btnLoadAllGenerate.TabIndex = 16;
            this.btnLoadAllGenerate.Text = "加载并生成";
            this.btnLoadAllGenerate.UseVisualStyleBackColor = true;
            this.btnLoadAllGenerate.Click += new System.EventHandler(this.btnLoadAllGenerate_Click);
            // 
            // ckbParamToHash
            // 
            this.ckbParamToHash.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbParamToHash.AutoSize = true;
            this.ckbParamToHash.Location = new System.Drawing.Point(519, 6);
            this.ckbParamToHash.Name = "ckbParamToHash";
            this.ckbParamToHash.Size = new System.Drawing.Size(72, 16);
            this.ckbParamToHash.TabIndex = 17;
            this.ckbParamToHash.Text = "@参数变#";
            this.ckbParamToHash.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tpNewOldTable);
            this.tabControl3.Controls.Add(this.tpNewOldColum);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(617, 193);
            this.tabControl3.TabIndex = 6;
            // 
            // tpNewOldTable
            // 
            this.tpNewOldTable.Controls.Add(this.groupBox12);
            this.tpNewOldTable.Controls.Add(this.groupBox13);
            this.tpNewOldTable.Location = new System.Drawing.Point(4, 22);
            this.tpNewOldTable.Name = "tpNewOldTable";
            this.tpNewOldTable.Padding = new System.Windows.Forms.Padding(3);
            this.tpNewOldTable.Size = new System.Drawing.Size(609, 167);
            this.tpNewOldTable.TabIndex = 0;
            this.tpNewOldTable.Text = "新旧表";
            this.tpNewOldTable.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.lblReplaceInfo);
            this.groupBox12.Controls.Add(this.dgvOldNewChar);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox12.Location = new System.Drawing.Point(3, 86);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(603, 78);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "新旧表关系";
            // 
            // lblReplaceInfo
            // 
            this.lblReplaceInfo.AutoSize = true;
            this.lblReplaceInfo.ForeColor = System.Drawing.Color.Red;
            this.lblReplaceInfo.Location = new System.Drawing.Point(107, 0);
            this.lblReplaceInfo.Name = "lblReplaceInfo";
            this.lblReplaceInfo.Size = new System.Drawing.Size(161, 12);
            this.lblReplaceInfo.TabIndex = 3;
            this.lblReplaceInfo.Text = "支持粘贴旧表编码和新表编码";
            // 
            // dgvOldNewChar
            // 
            this.dgvOldNewChar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOldNewChar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOldNewChar.Location = new System.Drawing.Point(3, 17);
            this.dgvOldNewChar.Name = "dgvOldNewChar";
            this.dgvOldNewChar.RowTemplate.Height = 23;
            this.dgvOldNewChar.Size = new System.Drawing.Size(597, 58);
            this.dgvOldNewChar.TabIndex = 0;
            this.dgvOldNewChar.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOldNewChar_ColumnHeaderMouseDoubleClick);
            this.dgvOldNewChar.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOldNewChar_DataError);
            this.dgvOldNewChar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOldNewChar_KeyDown);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label7);
            this.groupBox13.Controls.Add(this.tableLayoutPanel7);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox13.Location = new System.Drawing.Point(3, 3);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(603, 83);
            this.groupBox13.TabIndex = 4;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "新旧表清单";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(78, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(437, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "匹配条件：通过新旧表中列的中文备注来匹配（匹配率低）。可用于只替换表名！";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 5;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 318F));
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbbTemplateType, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label21, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.txbReplaceTemplateName, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.btnSaveReplaceTemplate, 3, 3);
            this.tableLayoutPanel7.Controls.Add(this.btnRemoveTemplate, 4, 3);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(597, 59);
            this.tableLayoutPanel7.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "模板选择：";
            // 
            // cbbTemplateType
            // 
            this.cbbTemplateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTemplateType.FormattingEnabled = true;
            this.cbbTemplateType.Location = new System.Drawing.Point(74, 3);
            this.cbbTemplateType.Name = "cbbTemplateType";
            this.cbbTemplateType.Size = new System.Drawing.Size(148, 20);
            this.cbbTemplateType.TabIndex = 0;
            this.cbbTemplateType.SelectedIndexChanged += new System.EventHandler(this.cbbTemplateType_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 5;
            this.label21.Text = "模板名称：";
            // 
            // txbReplaceTemplateName
            // 
            this.txbReplaceTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReplaceTemplateName.Location = new System.Drawing.Point(74, 32);
            this.txbReplaceTemplateName.Name = "txbReplaceTemplateName";
            this.txbReplaceTemplateName.Size = new System.Drawing.Size(148, 21);
            this.txbReplaceTemplateName.TabIndex = 6;
            // 
            // btnSaveReplaceTemplate
            // 
            this.btnSaveReplaceTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSaveReplaceTemplate.Location = new System.Drawing.Point(228, 32);
            this.btnSaveReplaceTemplate.Name = "btnSaveReplaceTemplate";
            this.btnSaveReplaceTemplate.Size = new System.Drawing.Size(62, 20);
            this.btnSaveReplaceTemplate.TabIndex = 7;
            this.btnSaveReplaceTemplate.Text = "保存模板";
            this.btnSaveReplaceTemplate.UseVisualStyleBackColor = true;
            this.btnSaveReplaceTemplate.Click += new System.EventHandler(this.btnSaveReplaceTemplate_Click);
            // 
            // btnRemoveTemplate
            // 
            this.btnRemoveTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRemoveTemplate.Location = new System.Drawing.Point(296, 32);
            this.btnRemoveTemplate.Name = "btnRemoveTemplate";
            this.btnRemoveTemplate.Size = new System.Drawing.Size(66, 20);
            this.btnRemoveTemplate.TabIndex = 8;
            this.btnRemoveTemplate.Text = "删除模板";
            this.btnRemoveTemplate.UseVisualStyleBackColor = true;
            this.btnRemoveTemplate.Click += new System.EventHandler(this.btnRemoveTemplate_Click);
            // 
            // tpNewOldColum
            // 
            this.tpNewOldColum.Controls.Add(this.groupBox11);
            this.tpNewOldColum.Controls.Add(this.groupBox10);
            this.tpNewOldColum.Location = new System.Drawing.Point(4, 22);
            this.tpNewOldColum.Name = "tpNewOldColum";
            this.tpNewOldColum.Padding = new System.Windows.Forms.Padding(3);
            this.tpNewOldColum.Size = new System.Drawing.Size(609, 167);
            this.tpNewOldColum.TabIndex = 1;
            this.tpNewOldColum.Text = "新旧列";
            this.tpNewOldColum.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Controls.Add(this.dgvOldNewColumnChar);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Location = new System.Drawing.Point(3, 86);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(603, 78);
            this.groupBox11.TabIndex = 6;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "新旧列关系";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(107, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "支持粘贴旧表名、旧列名、新表名、新列名";
            // 
            // dgvOldNewColumnChar
            // 
            this.dgvOldNewColumnChar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOldNewColumnChar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOldNewColumnChar.Location = new System.Drawing.Point(3, 17);
            this.dgvOldNewColumnChar.Name = "dgvOldNewColumnChar";
            this.dgvOldNewColumnChar.RowTemplate.Height = 23;
            this.dgvOldNewColumnChar.Size = new System.Drawing.Size(597, 58);
            this.dgvOldNewColumnChar.TabIndex = 0;
            this.dgvOldNewColumnChar.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOldNewColumnChar_ColumnHeaderMouseDoubleClick);
            this.dgvOldNewColumnChar.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOldNewColumnChar_DataError);
            this.dgvOldNewColumnChar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOldNewColumnChar_KeyDown);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label8);
            this.groupBox10.Controls.Add(this.tableLayoutPanel5);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(603, 83);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "新旧列清单";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(90, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(341, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "匹配条件：通过新旧表的新旧列关系来匹配（准确度高，推荐）";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 318F));
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbbColumnTemplateType, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txbReplaceColumnTemplateName, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.btnSaveColumnReplaceTemplate, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.btnRemoveColumnTemplate, 4, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(597, 59);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "模板选择：";
            // 
            // cbbColumnTemplateType
            // 
            this.cbbColumnTemplateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbColumnTemplateType.FormattingEnabled = true;
            this.cbbColumnTemplateType.Location = new System.Drawing.Point(74, 3);
            this.cbbColumnTemplateType.Name = "cbbColumnTemplateType";
            this.cbbColumnTemplateType.Size = new System.Drawing.Size(148, 20);
            this.cbbColumnTemplateType.TabIndex = 0;
            this.cbbColumnTemplateType.SelectedIndexChanged += new System.EventHandler(this.cbbColumnTemplateType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "模板名称：";
            // 
            // txbReplaceColumnTemplateName
            // 
            this.txbReplaceColumnTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReplaceColumnTemplateName.Location = new System.Drawing.Point(74, 32);
            this.txbReplaceColumnTemplateName.Name = "txbReplaceColumnTemplateName";
            this.txbReplaceColumnTemplateName.Size = new System.Drawing.Size(148, 21);
            this.txbReplaceColumnTemplateName.TabIndex = 6;
            // 
            // btnSaveColumnReplaceTemplate
            // 
            this.btnSaveColumnReplaceTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSaveColumnReplaceTemplate.Location = new System.Drawing.Point(228, 32);
            this.btnSaveColumnReplaceTemplate.Name = "btnSaveColumnReplaceTemplate";
            this.btnSaveColumnReplaceTemplate.Size = new System.Drawing.Size(62, 20);
            this.btnSaveColumnReplaceTemplate.TabIndex = 7;
            this.btnSaveColumnReplaceTemplate.Text = "保存模板";
            this.btnSaveColumnReplaceTemplate.UseVisualStyleBackColor = true;
            this.btnSaveColumnReplaceTemplate.Click += new System.EventHandler(this.btnSaveColumnReplaceTemplate_Click);
            // 
            // btnRemoveColumnTemplate
            // 
            this.btnRemoveColumnTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRemoveColumnTemplate.Location = new System.Drawing.Point(296, 32);
            this.btnRemoveColumnTemplate.Name = "btnRemoveColumnTemplate";
            this.btnRemoveColumnTemplate.Size = new System.Drawing.Size(66, 20);
            this.btnRemoveColumnTemplate.TabIndex = 8;
            this.btnRemoveColumnTemplate.Text = "删除模板";
            this.btnRemoveColumnTemplate.UseVisualStyleBackColor = true;
            this.btnRemoveColumnTemplate.Click += new System.EventHandler(this.btnRemoveColumnTemplate_Click);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer5.Size = new System.Drawing.Size(381, 417);
            this.splitContainer5.SplitterDistance = 207;
            this.splitContainer5.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbResult);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 207);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 17);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(375, 187);
            this.rtbResult.TabIndex = 4;
            this.rtbResult.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(381, 206);
            this.tabControl2.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox9);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(373, 180);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "替换列清单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.dgvBeReplaceCol);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(367, 174);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "被替换列清单";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(99, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "替换旧列编码范围：表别名.列编码";
            // 
            // dgvBeReplaceCol
            // 
            this.dgvBeReplaceCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBeReplaceCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBeReplaceCol.Location = new System.Drawing.Point(3, 17);
            this.dgvBeReplaceCol.Name = "dgvBeReplaceCol";
            this.dgvBeReplaceCol.RowTemplate.Height = 23;
            this.dgvBeReplaceCol.Size = new System.Drawing.Size(361, 154);
            this.dgvBeReplaceCol.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbConvertRemark);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(373, 180);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "转换说明";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbConvertRemark
            // 
            this.rtbConvertRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConvertRemark.Location = new System.Drawing.Point(3, 3);
            this.rtbConvertRemark.Name = "rtbConvertRemark";
            this.rtbConvertRemark.Size = new System.Drawing.Size(367, 174);
            this.rtbConvertRemark.TabIndex = 0;
            this.rtbConvertRemark.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFuncInfo);
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1016, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "转换选项";
            // 
            // lblFuncInfo
            // 
            this.lblFuncInfo.AutoSize = true;
            this.lblFuncInfo.ForeColor = System.Drawing.Color.Red;
            this.lblFuncInfo.Location = new System.Drawing.Point(104, 0);
            this.lblFuncInfo.Name = "lblFuncInfo";
            this.lblFuncInfo.Size = new System.Drawing.Size(29, 12);
            this.lblFuncInfo.TabIndex = 11;
            this.lblFuncInfo.Text = "说明";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 627F));
            this.tableLayoutPanel1.Controls.Add(this.btnConnectLoadAll, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbNewOldColumnSourceType, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1010, 27);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // btnConnectLoadAll
            // 
            this.btnConnectLoadAll.ForeColor = System.Drawing.Color.Red;
            this.btnConnectLoadAll.Location = new System.Drawing.Point(261, 3);
            this.btnConnectLoadAll.Name = "btnConnectLoadAll";
            this.btnConnectLoadAll.Size = new System.Drawing.Size(99, 23);
            this.btnConnectLoadAll.TabIndex = 26;
            this.btnConnectLoadAll.Text = "连接加载所有";
            this.btnConnectLoadAll.UseVisualStyleBackColor = true;
            this.btnConnectLoadAll.Click += new System.EventHandler(this.btnConnectLoadAll_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "新旧列关系获取方式：";
            // 
            // cbbNewOldColumnSourceType
            // 
            this.cbbNewOldColumnSourceType.FormattingEnabled = true;
            this.cbbNewOldColumnSourceType.Location = new System.Drawing.Point(134, 3);
            this.cbbNewOldColumnSourceType.Name = "cbbNewOldColumnSourceType";
            this.cbbNewOldColumnSourceType.Size = new System.Drawing.Size(121, 20);
            this.cbbNewOldColumnSourceType.TabIndex = 13;
            this.cbbNewOldColumnSourceType.SelectedIndexChanged += new System.EventHandler(this.cbbNewOldColumnSourceType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(104, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "局限性：当旧表出现多次，且有不同别名时，只会取第一次出现的别名来匹配对应表";
            // 
            // uC_DbConnectionTarget
            // 
            this.uC_DbConnectionTarget.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnectionTarget.Location = new System.Drawing.Point(0, 158);
            this.uC_DbConnectionTarget.Name = "uC_DbConnectionTarget";
            this.uC_DbConnectionTarget.Size = new System.Drawing.Size(1016, 81);
            this.uC_DbConnectionTarget.TabIndex = 37;
            // 
            // uC_DbConnectionSource
            // 
            this.uC_DbConnectionSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnectionSource.Location = new System.Drawing.Point(0, 77);
            this.uC_DbConnectionSource.Name = "uC_DbConnectionSource";
            this.uC_DbConnectionSource.Size = new System.Drawing.Size(1016, 81);
            this.uC_DbConnectionSource.TabIndex = 36;
            // 
            // ckbTableFixedTarget
            // 
            this.ckbTableFixedTarget.AutoSize = true;
            this.ckbTableFixedTarget.Location = new System.Drawing.Point(181, -1);
            this.ckbTableFixedTarget.Name = "ckbTableFixedTarget";
            this.ckbTableFixedTarget.Size = new System.Drawing.Size(48, 16);
            this.ckbTableFixedTarget.TabIndex = 28;
            this.ckbTableFixedTarget.Text = "精确";
            this.ckbTableFixedTarget.UseVisualStyleBackColor = true;
            // 
            // ckbTableFixedSource
            // 
            this.ckbTableFixedSource.AutoSize = true;
            this.ckbTableFixedSource.Location = new System.Drawing.Point(177, 1);
            this.ckbTableFixedSource.Name = "ckbTableFixedSource";
            this.ckbTableFixedSource.Size = new System.Drawing.Size(48, 16);
            this.ckbTableFixedSource.TabIndex = 29;
            this.ckbTableFixedSource.Text = "精确";
            this.ckbTableFixedSource.UseVisualStyleBackColor = true;
            // 
            // FrmSqlStringConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 688);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.uC_DbConnectionTarget);
            this.Controls.Add(this.uC_DbConnectionSource);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "FrmSqlStringConvert";
            this.Text = "数据库间SQL语句转换";
            this.Load += new System.EventHandler(this.FrmSqlStringConvert_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSourceDb.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableListSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grbColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListSource)).EndInit();
            this.tpTargetDb.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableListTarget)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListTarget)).EndInit();
            this.tpConvert.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.grbInputSql.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tpNewOldTable.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldNewChar)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tpNewOldColum.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldNewColumnChar)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeReplaceCol)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private UC_DbConnection uC_DbConnectionSource;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSourceDb;
        private System.Windows.Forms.TabPage tpConvert;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnLoadDataSource;
        private System.Windows.Forms.CheckBox ckbClearAllColSource;
        private System.Windows.Forms.Button btnExcludeTableSource;
        private System.Windows.Forms.TextBox txbExcludeTableSource;
        private System.Windows.Forms.CheckBox ckbNotIncludeSplitTableSource;
        private System.Windows.Forms.CheckBox ckbIsAutoExcludeSource;
        private System.Windows.Forms.CheckBox ckbAllTableColumnSource;
        private System.Windows.Forms.GroupBox grbColumn;
        private System.Windows.Forms.DataGridView dgvColListSource;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txbSearchTableNameSource;
        private System.Windows.Forms.Button btnFindFrontSource;
        private System.Windows.Forms.Button btnFindNextSource;
        private System.Windows.Forms.DataGridView dgvTableListSource;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grbInputSql;
        private System.Windows.Forms.RichTextBox rtbInputSql;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbTemplateType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txbReplaceTemplateName;
        private System.Windows.Forms.Button btnSaveReplaceTemplate;
        private System.Windows.Forms.Button btnRemoveTemplate;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label lblReplaceInfo;
        private System.Windows.Forms.DataGridView dgvOldNewChar;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFindSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbNewOldColumnSourceType;
        private System.Windows.Forms.TabPage tpTargetDb;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblFindTarget;
        private System.Windows.Forms.TextBox txbSearchTableNameTarget;
        private System.Windows.Forms.Button btnFindFrontTarget;
        private System.Windows.Forms.Button btnFindNextTarget;
        private System.Windows.Forms.DataGridView dgvTableListTarget;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnLoadDataTarget;
        private System.Windows.Forms.CheckBox ckbClearAllColTarget;
        private System.Windows.Forms.Button btnExcludeTableTarget;
        private System.Windows.Forms.TextBox txbExcludeTableTarget;
        private System.Windows.Forms.CheckBox ckbNotIncludeSplitTableTarget;
        private System.Windows.Forms.CheckBox ckbIsAutoExcludeTarget;
        private System.Windows.Forms.CheckBox ckbAllTableColumnTarget;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvColListTarget;
        private UC_DbConnection uC_DbConnectionTarget;
        private System.Windows.Forms.Button btnSourceConnect;
        private System.Windows.Forms.Button btnTargetConnect;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblTargetDbType;
        private System.Windows.Forms.ComboBox cbbTargetDbType;
        private System.Windows.Forms.Label lblSourceDbType;
        private System.Windows.Forms.ComboBox cbbSourceDbType;
        private System.Windows.Forms.Button btnConnectLoadAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblFuncInfo;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.DataGridView dgvBeReplaceCol;
        private System.Windows.Forms.Button btnLoadAllGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbConvertRemark;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tpNewOldTable;
        private System.Windows.Forms.TabPage tpNewOldColum;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvOldNewColumnChar;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbColumnTemplateType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbReplaceColumnTemplateName;
        private System.Windows.Forms.Button btnSaveColumnReplaceTemplate;
        private System.Windows.Forms.Button btnRemoveColumnTemplate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbParamToHash;
        private System.Windows.Forms.CheckBox ckbTableFixedTarget;
        private System.Windows.Forms.CheckBox ckbTableFixedSource;
    }
}