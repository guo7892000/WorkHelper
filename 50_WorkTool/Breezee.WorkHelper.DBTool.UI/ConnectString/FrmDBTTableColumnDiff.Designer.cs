namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTTableColumnDiff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTTableColumnDiff));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.grbOrcNet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTableNameSource = new System.Windows.Forms.ComboBox();
            this.ckbGetTableListSource = new System.Windows.Forms.CheckBox();
            this.btnConnectSource = new System.Windows.Forms.Button();
            this.btnCompareSource = new System.Windows.Forms.Button();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.btnFindFrontSource = new System.Windows.Forms.Button();
            this.lblFindSource = new System.Windows.Forms.Label();
            this.btnFindNextSource = new System.Windows.Forms.Button();
            this.txbSearchColumnSource = new System.Windows.Forms.TextBox();
            this.dgvColListSource = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uC_DbConnectionSource = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFindFrontTarget = new System.Windows.Forms.Button();
            this.lblFindTarget = new System.Windows.Forms.Label();
            this.btnFindNextTarget = new System.Windows.Forms.Button();
            this.txbSearchColumnTarget = new System.Windows.Forms.TextBox();
            this.dgvColListTarget = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbTableNameTarget = new System.Windows.Forms.ComboBox();
            this.ckbGetTableListTarget = new System.Windows.Forms.CheckBox();
            this.btnConnectTarget = new System.Windows.Forms.Button();
            this.btnCompareTarge = new System.Windows.Forms.Button();
            this.uC_DbConnectionTarget = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tpResult = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFindFrontResult = new System.Windows.Forms.Button();
            this.lblFindResult = new System.Windows.Forms.Label();
            this.btnFindNextResult = new System.Windows.Forms.Button();
            this.txbSearchColumnResult = new System.Windows.Forms.TextBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbTargetDbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbTableCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbTableCN = new System.Windows.Forms.TextBox();
            this.btnGenerateDbDoc = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txbTableRemark = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.grbOrcNet.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListTarget)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tpResult.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImport,
            this.tsbAutoSQL,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1337, 27);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(98, 24);
            this.tsbImport.Text = "全部连接(&Q)";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(72, 24);
            this.tsbAutoSQL.Text = "比较(&A)";
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
            // grbOrcNet
            // 
            this.grbOrcNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbOrcNet.Controls.Add(this.tableLayoutPanel4);
            this.grbOrcNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbOrcNet.Location = new System.Drawing.Point(0, 77);
            this.grbOrcNet.Name = "grbOrcNet";
            this.grbOrcNet.Size = new System.Drawing.Size(710, 51);
            this.grbOrcNet.TabIndex = 40;
            this.grbOrcNet.TabStop = false;
            this.grbOrcNet.Text = "源数据库";
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
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel4.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTableNameSource, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbGetTableListSource, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnConnectSource, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCompareSource, 4, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(704, 31);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "表名：";
            // 
            // cbbTableNameSource
            // 
            this.cbbTableNameSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableNameSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbTableNameSource.FormattingEnabled = true;
            this.cbbTableNameSource.Location = new System.Drawing.Point(50, 4);
            this.cbbTableNameSource.Name = "cbbTableNameSource";
            this.cbbTableNameSource.Size = new System.Drawing.Size(246, 20);
            this.cbbTableNameSource.TabIndex = 2;
            this.cbbTableNameSource.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // ckbGetTableListSource
            // 
            this.ckbGetTableListSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableListSource.AutoSize = true;
            this.ckbGetTableListSource.Location = new System.Drawing.Point(302, 6);
            this.ckbGetTableListSource.Name = "ckbGetTableListSource";
            this.ckbGetTableListSource.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableListSource.TabIndex = 3;
            this.ckbGetTableListSource.Text = "获取表清单";
            this.ckbGetTableListSource.UseVisualStyleBackColor = true;
            this.ckbGetTableListSource.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // btnConnectSource
            // 
            this.btnConnectSource.Location = new System.Drawing.Point(392, 3);
            this.btnConnectSource.Name = "btnConnectSource";
            this.btnConnectSource.Size = new System.Drawing.Size(49, 23);
            this.btnConnectSource.TabIndex = 4;
            this.btnConnectSource.Text = "连接";
            this.btnConnectSource.UseVisualStyleBackColor = true;
            this.btnConnectSource.Click += new System.EventHandler(this.btnConnectSource_Click);
            // 
            // btnCompareSource
            // 
            this.btnCompareSource.Location = new System.Drawing.Point(447, 3);
            this.btnCompareSource.Name = "btnCompareSource";
            this.btnCompareSource.Size = new System.Drawing.Size(59, 23);
            this.btnCompareSource.TabIndex = 5;
            this.btnCompareSource.Text = "比较";
            this.btnCompareSource.UseVisualStyleBackColor = true;
            this.btnCompareSource.Click += new System.EventHandler(this.btnCompareSource_Click);
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.btnFindFrontSource);
            this.grbColumn.Controls.Add(this.lblFindSource);
            this.grbColumn.Controls.Add(this.btnFindNextSource);
            this.grbColumn.Controls.Add(this.txbSearchColumnSource);
            this.grbColumn.Controls.Add(this.dgvColListSource);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(0, 128);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(710, 411);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // btnFindFrontSource
            // 
            this.btnFindFrontSource.Location = new System.Drawing.Point(353, -2);
            this.btnFindFrontSource.Name = "btnFindFrontSource";
            this.btnFindFrontSource.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontSource.TabIndex = 18;
            this.btnFindFrontSource.Text = "往前找";
            this.btnFindFrontSource.UseVisualStyleBackColor = true;
            this.btnFindFrontSource.Click += new System.EventHandler(this.btnFindFront_Click);
            // 
            // lblFindSource
            // 
            this.lblFindSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindSource.AutoSize = true;
            this.lblFindSource.ForeColor = System.Drawing.Color.Red;
            this.lblFindSource.Location = new System.Drawing.Point(423, 1);
            this.lblFindSource.Name = "lblFindSource";
            this.lblFindSource.Size = new System.Drawing.Size(53, 12);
            this.lblFindSource.TabIndex = 16;
            this.lblFindSource.Text = "查找提示";
            // 
            // btnFindNextSource
            // 
            this.btnFindNextSource.Location = new System.Drawing.Point(294, -2);
            this.btnFindNextSource.Name = "btnFindNextSource";
            this.btnFindNextSource.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextSource.TabIndex = 15;
            this.btnFindNextSource.Text = "下一个";
            this.btnFindNextSource.UseVisualStyleBackColor = true;
            this.btnFindNextSource.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // txbSearchColumnSource
            // 
            this.txbSearchColumnSource.Location = new System.Drawing.Point(104, -3);
            this.txbSearchColumnSource.Name = "txbSearchColumnSource";
            this.txbSearchColumnSource.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumnSource.TabIndex = 14;
            // 
            // dgvColListSource
            // 
            this.dgvColListSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColListSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColListSource.Location = new System.Drawing.Point(3, 17);
            this.dgvColListSource.Name = "dgvColListSource";
            this.dgvColListSource.RowTemplate.Height = 23;
            this.dgvColListSource.Size = new System.Drawing.Size(704, 391);
            this.dgvColListSource.TabIndex = 0;
            this.dgvColListSource.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvColList_ColumnHeaderMouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbColumn);
            this.splitContainer1.Panel1.Controls.Add(this.grbOrcNet);
            this.splitContainer1.Panel1.Controls.Add(this.uC_DbConnectionSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.uC_DbConnectionTarget);
            this.splitContainer1.Size = new System.Drawing.Size(1323, 539);
            this.splitContainer1.SplitterDistance = 710;
            this.splitContainer1.TabIndex = 44;
            // 
            // uC_DbConnectionSource
            // 
            this.uC_DbConnectionSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnectionSource.Location = new System.Drawing.Point(0, 0);
            this.uC_DbConnectionSource.Name = "uC_DbConnectionSource";
            this.uC_DbConnectionSource.Size = new System.Drawing.Size(710, 77);
            this.uC_DbConnectionSource.TabIndex = 35;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox2.Controls.Add(this.btnFindFrontTarget);
            this.groupBox2.Controls.Add(this.lblFindTarget);
            this.groupBox2.Controls.Add(this.btnFindNextTarget);
            this.groupBox2.Controls.Add(this.txbSearchColumnTarget);
            this.groupBox2.Controls.Add(this.dgvColListTarget);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 411);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列清单";
            // 
            // btnFindFrontTarget
            // 
            this.btnFindFrontTarget.Location = new System.Drawing.Point(353, -2);
            this.btnFindFrontTarget.Name = "btnFindFrontTarget";
            this.btnFindFrontTarget.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontTarget.TabIndex = 18;
            this.btnFindFrontTarget.Text = "往前找";
            this.btnFindFrontTarget.UseVisualStyleBackColor = true;
            this.btnFindFrontTarget.Click += new System.EventHandler(this.btnFindFrontTarget_Click);
            // 
            // lblFindTarget
            // 
            this.lblFindTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindTarget.AutoSize = true;
            this.lblFindTarget.ForeColor = System.Drawing.Color.Red;
            this.lblFindTarget.Location = new System.Drawing.Point(423, 1);
            this.lblFindTarget.Name = "lblFindTarget";
            this.lblFindTarget.Size = new System.Drawing.Size(53, 12);
            this.lblFindTarget.TabIndex = 16;
            this.lblFindTarget.Text = "查找提示";
            // 
            // btnFindNextTarget
            // 
            this.btnFindNextTarget.Location = new System.Drawing.Point(294, -2);
            this.btnFindNextTarget.Name = "btnFindNextTarget";
            this.btnFindNextTarget.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextTarget.TabIndex = 15;
            this.btnFindNextTarget.Text = "下一个";
            this.btnFindNextTarget.UseVisualStyleBackColor = true;
            this.btnFindNextTarget.Click += new System.EventHandler(this.btnFindNextTarget_Click);
            // 
            // txbSearchColumnTarget
            // 
            this.txbSearchColumnTarget.Location = new System.Drawing.Point(104, -3);
            this.txbSearchColumnTarget.Name = "txbSearchColumnTarget";
            this.txbSearchColumnTarget.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumnTarget.TabIndex = 14;
            // 
            // dgvColListTarget
            // 
            this.dgvColListTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColListTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColListTarget.Location = new System.Drawing.Point(3, 17);
            this.dgvColListTarget.Name = "dgvColListTarget";
            this.dgvColListTarget.RowTemplate.Height = 23;
            this.dgvColListTarget.Size = new System.Drawing.Size(603, 391);
            this.dgvColListTarget.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 51);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目标数据库";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 599F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbTableNameTarget, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbGetTableListTarget, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnectTarget, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCompareTarge, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(603, 31);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "表名：";
            // 
            // cbbTableNameTarget
            // 
            this.cbbTableNameTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableNameTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbTableNameTarget.FormattingEnabled = true;
            this.cbbTableNameTarget.Location = new System.Drawing.Point(50, 4);
            this.cbbTableNameTarget.Name = "cbbTableNameTarget";
            this.cbbTableNameTarget.Size = new System.Drawing.Size(246, 20);
            this.cbbTableNameTarget.TabIndex = 2;
            this.cbbTableNameTarget.SelectedIndexChanged += new System.EventHandler(this.cbbTableNameTarget_SelectedIndexChanged);
            // 
            // ckbGetTableListTarget
            // 
            this.ckbGetTableListTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableListTarget.AutoSize = true;
            this.ckbGetTableListTarget.Location = new System.Drawing.Point(302, 6);
            this.ckbGetTableListTarget.Name = "ckbGetTableListTarget";
            this.ckbGetTableListTarget.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableListTarget.TabIndex = 3;
            this.ckbGetTableListTarget.Text = "获取表清单";
            this.ckbGetTableListTarget.UseVisualStyleBackColor = true;
            this.ckbGetTableListTarget.CheckedChanged += new System.EventHandler(this.ckbGetTableListTarget_CheckedChanged);
            // 
            // btnConnectTarget
            // 
            this.btnConnectTarget.Location = new System.Drawing.Point(392, 3);
            this.btnConnectTarget.Name = "btnConnectTarget";
            this.btnConnectTarget.Size = new System.Drawing.Size(49, 23);
            this.btnConnectTarget.TabIndex = 4;
            this.btnConnectTarget.Text = "连接";
            this.btnConnectTarget.UseVisualStyleBackColor = true;
            this.btnConnectTarget.Click += new System.EventHandler(this.btnConnectTarget_Click);
            // 
            // btnCompareTarge
            // 
            this.btnCompareTarge.Location = new System.Drawing.Point(447, 3);
            this.btnCompareTarge.Name = "btnCompareTarge";
            this.btnCompareTarge.Size = new System.Drawing.Size(49, 23);
            this.btnCompareTarge.TabIndex = 5;
            this.btnCompareTarge.Text = "比较";
            this.btnCompareTarge.UseVisualStyleBackColor = true;
            this.btnCompareTarge.Click += new System.EventHandler(this.btnCompareTarge_Click);
            // 
            // uC_DbConnectionTarget
            // 
            this.uC_DbConnectionTarget.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnectionTarget.Location = new System.Drawing.Point(0, 0);
            this.uC_DbConnectionTarget.Name = "uC_DbConnectionTarget";
            this.uC_DbConnectionTarget.Size = new System.Drawing.Size(609, 77);
            this.uC_DbConnectionTarget.TabIndex = 36;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tpResult);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1337, 571);
            this.tabControl1.TabIndex = 46;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1329, 545);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "列信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tpResult
            // 
            this.tpResult.Controls.Add(this.tabControl2);
            this.tpResult.Controls.Add(this.groupBox4);
            this.tpResult.Location = new System.Drawing.Point(4, 22);
            this.tpResult.Name = "tpResult";
            this.tpResult.Padding = new System.Windows.Forms.Padding(3);
            this.tpResult.Size = new System.Drawing.Size(1329, 545);
            this.tpResult.TabIndex = 1;
            this.tpResult.Text = "比较结果";
            this.tpResult.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 93);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1323, 449);
            this.tabControl2.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1315, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "差异清单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox3.Controls.Add(this.btnFindFrontResult);
            this.groupBox3.Controls.Add(this.lblFindResult);
            this.groupBox3.Controls.Add(this.btnFindNextResult);
            this.groupBox3.Controls.Add(this.txbSearchColumnResult);
            this.groupBox3.Controls.Add(this.dgvResult);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1309, 417);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "列清单";
            // 
            // btnFindFrontResult
            // 
            this.btnFindFrontResult.Location = new System.Drawing.Point(353, -2);
            this.btnFindFrontResult.Name = "btnFindFrontResult";
            this.btnFindFrontResult.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontResult.TabIndex = 18;
            this.btnFindFrontResult.Text = "往前找";
            this.btnFindFrontResult.UseVisualStyleBackColor = true;
            this.btnFindFrontResult.Click += new System.EventHandler(this.btnFindFrontResult_Click);
            // 
            // lblFindResult
            // 
            this.lblFindResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindResult.AutoSize = true;
            this.lblFindResult.ForeColor = System.Drawing.Color.Red;
            this.lblFindResult.Location = new System.Drawing.Point(423, 1);
            this.lblFindResult.Name = "lblFindResult";
            this.lblFindResult.Size = new System.Drawing.Size(53, 12);
            this.lblFindResult.TabIndex = 16;
            this.lblFindResult.Text = "查找提示";
            // 
            // btnFindNextResult
            // 
            this.btnFindNextResult.Location = new System.Drawing.Point(294, -2);
            this.btnFindNextResult.Name = "btnFindNextResult";
            this.btnFindNextResult.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextResult.TabIndex = 15;
            this.btnFindNextResult.Text = "下一个";
            this.btnFindNextResult.UseVisualStyleBackColor = true;
            this.btnFindNextResult.Click += new System.EventHandler(this.btnFindNextResult_Click);
            // 
            // txbSearchColumnResult
            // 
            this.txbSearchColumnResult.Location = new System.Drawing.Point(104, -3);
            this.txbSearchColumnResult.Name = "txbSearchColumnResult";
            this.txbSearchColumnResult.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumnResult.TabIndex = 14;
            // 
            // dgvResult
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 17);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(1303, 397);
            this.dgvResult.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1323, 90);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "选项";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 784F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbTargetDbType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txbTableCode, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txbTableCN, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnGenerateDbDoc, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.txbTableRemark, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1317, 67);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "目标数据库：";
            // 
            // cbbTargetDbType
            // 
            this.cbbTargetDbType.FormattingEnabled = true;
            this.cbbTargetDbType.Location = new System.Drawing.Point(86, 3);
            this.cbbTargetDbType.Name = "cbbTargetDbType";
            this.cbbTargetDbType.Size = new System.Drawing.Size(149, 20);
            this.cbbTargetDbType.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(241, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "表编码：";
            // 
            // txbTableCode
            // 
            this.txbTableCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTableCode.Location = new System.Drawing.Point(300, 4);
            this.txbTableCode.Name = "txbTableCode";
            this.txbTableCode.Size = new System.Drawing.Size(128, 21);
            this.txbTableCode.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "表名称：";
            // 
            // txbTableCN
            // 
            this.txbTableCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTableCN.Location = new System.Drawing.Point(86, 32);
            this.txbTableCN.Name = "txbTableCN";
            this.txbTableCN.Size = new System.Drawing.Size(149, 21);
            this.txbTableCN.TabIndex = 14;
            // 
            // btnGenerateDbDoc
            // 
            this.btnGenerateDbDoc.Location = new System.Drawing.Point(434, 3);
            this.btnGenerateDbDoc.Name = "btnGenerateDbDoc";
            this.btnGenerateDbDoc.Size = new System.Drawing.Size(95, 23);
            this.btnGenerateDbDoc.TabIndex = 10;
            this.btnGenerateDbDoc.Text = "生成变更文档";
            this.btnGenerateDbDoc.UseVisualStyleBackColor = true;
            this.btnGenerateDbDoc.Click += new System.EventHandler(this.btnGenerateDbDoc_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "表备注：";
            // 
            // txbTableRemark
            // 
            this.txbTableRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.txbTableRemark, 2);
            this.txbTableRemark.Location = new System.Drawing.Point(300, 32);
            this.txbTableRemark.Name = "txbTableRemark";
            this.txbTableRemark.Size = new System.Drawing.Size(229, 21);
            this.txbTableRemark.TabIndex = 12;
            // 
            // FrmDBTTableColumnDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 598);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTTableColumnDiff";
            this.Text = "表列差异比较";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbOrcNet.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColListTarget)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tpResult.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private UC_DbConnection uC_DbConnectionSource;
        private System.Windows.Forms.GroupBox grbOrcNet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox ckbGetTableListSource;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbTableNameSource;
        private System.Windows.Forms.GroupBox grbColumn;
        private System.Windows.Forms.DataGridView dgvColListSource;
        private System.Windows.Forms.Button btnFindNextSource;
        private System.Windows.Forms.TextBox txbSearchColumnSource;
        private System.Windows.Forms.Label lblFindSource;
        private System.Windows.Forms.Button btnFindFrontSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFindFrontTarget;
        private System.Windows.Forms.Label lblFindTarget;
        private System.Windows.Forms.Button btnFindNextTarget;
        private System.Windows.Forms.TextBox txbSearchColumnTarget;
        private System.Windows.Forms.DataGridView dgvColListTarget;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbTableNameTarget;
        private System.Windows.Forms.CheckBox ckbGetTableListTarget;
        private UC_DbConnection uC_DbConnectionTarget;
        private System.Windows.Forms.Button btnConnectSource;
        private System.Windows.Forms.Button btnCompareSource;
        private System.Windows.Forms.Button btnConnectTarget;
        private System.Windows.Forms.Button btnCompareTarge;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tpResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFindFrontResult;
        private System.Windows.Forms.Label lblFindResult;
        private System.Windows.Forms.Button btnFindNextResult;
        private System.Windows.Forms.TextBox txbSearchColumnResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbTargetDbType;
        private System.Windows.Forms.Button btnGenerateDbDoc;
        private System.Windows.Forms.TextBox txbTableRemark;
        private System.Windows.Forms.TextBox txbTableCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbTableCN;
    }
}