namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTTableColumnDictionary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTTableColumnDictionary));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.ckbAllTableColumns = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAllTableCol = new System.Windows.Forms.TabPage();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.cmsAddCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddCommonCol = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblColumnInfo = new System.Windows.Forms.Label();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txbSearchCol = new System.Windows.Forms.TextBox();
            this.tpCommonCol = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvCommonCol = new System.Windows.Forms.DataGridView();
            this.cmsRemoveCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemoveCommon = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCommonSave = new System.Windows.Forms.Button();
            this.btnFindCommon = new System.Windows.Forms.Button();
            this.txbSearchCommon = new System.Windows.Forms.TextBox();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSelect = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbConString = new System.Windows.Forms.RichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnMatch = new System.Windows.Forms.Button();
            this.cmsRemoveSelect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemoveSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpAllTableCol.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).BeginInit();
            this.cmsAddCommon.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpCommonCol.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonCol)).BeginInit();
            this.cmsRemoveCommon.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tpAutoSQL.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.cmsRemoveSelect.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1054, 27);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(74, 24);
            this.tsbImport.Text = "连接(&Q)";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
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
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(317, 6);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(53, 12);
            this.lblInfo.TabIndex = 31;
            this.lblInfo.Text = "提示信息";
            // 
            // ckbAllTableColumns
            // 
            this.ckbAllTableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbAllTableColumns.AutoSize = true;
            this.ckbAllTableColumns.Location = new System.Drawing.Point(297, 19);
            this.ckbAllTableColumns.Name = "ckbAllTableColumns";
            this.ckbAllTableColumns.Size = new System.Drawing.Size(120, 16);
            this.ckbAllTableColumns.TabIndex = 3;
            this.ckbAllTableColumns.Text = "获取所有表列清单";
            this.ckbAllTableColumns.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAllTableCol);
            this.tabControl1.Controls.Add(this.tpCommonCol);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 422);
            this.tabControl1.TabIndex = 43;
            // 
            // tpAllTableCol
            // 
            this.tpAllTableCol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAllTableCol.Controls.Add(this.grbColumn);
            this.tpAllTableCol.Controls.Add(this.groupBox3);
            this.tpAllTableCol.Location = new System.Drawing.Point(4, 22);
            this.tpAllTableCol.Name = "tpAllTableCol";
            this.tpAllTableCol.Padding = new System.Windows.Forms.Padding(3);
            this.tpAllTableCol.Size = new System.Drawing.Size(551, 396);
            this.tpAllTableCol.TabIndex = 0;
            this.tpAllTableCol.Text = "所有表列";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(3, 46);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(545, 347);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // dgvColList
            // 
            this.dgvColList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColList.ContextMenuStrip = this.cmsAddCommon;
            this.dgvColList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColList.Location = new System.Drawing.Point(3, 17);
            this.dgvColList.Name = "dgvColList";
            this.dgvColList.RowTemplate.Height = 23;
            this.dgvColList.Size = new System.Drawing.Size(539, 327);
            this.dgvColList.TabIndex = 0;
            // 
            // cmsAddCommon
            // 
            this.cmsAddCommon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddCommonCol});
            this.cmsAddCommon.Name = "contextMenuStrip1";
            this.cmsAddCommon.Size = new System.Drawing.Size(181, 48);
            // 
            // tsmiAddCommonCol
            // 
            this.tsmiAddCommonCol.Name = "tsmiAddCommonCol";
            this.tsmiAddCommonCol.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddCommonCol.Text = "加入字典";
            this.tsmiAddCommonCol.Click += new System.EventHandler(this.tsmiAddCommonCol_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblColumnInfo);
            this.groupBox3.Controls.Add(this.btnLoadData);
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.ckbAllTableColumns);
            this.groupBox3.Controls.Add(this.txbSearchCol);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(545, 43);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查找";
            // 
            // lblColumnInfo
            // 
            this.lblColumnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColumnInfo.AutoSize = true;
            this.lblColumnInfo.ForeColor = System.Drawing.Color.Red;
            this.lblColumnInfo.Location = new System.Drawing.Point(133, 2);
            this.lblColumnInfo.Name = "lblColumnInfo";
            this.lblColumnInfo.Size = new System.Drawing.Size(119, 12);
            this.lblColumnInfo.TabIndex = 13;
            this.lblColumnInfo.Text = "只支持C、C1、C2定位";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(223, 15);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(68, 23);
            this.btnLoadData.TabIndex = 3;
            this.btnLoadData.Text = "加载数据";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(168, 15);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(49, 23);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "定位";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txbSearchCol
            // 
            this.txbSearchCol.Location = new System.Drawing.Point(6, 17);
            this.txbSearchCol.Name = "txbSearchCol";
            this.txbSearchCol.Size = new System.Drawing.Size(155, 21);
            this.txbSearchCol.TabIndex = 2;
            // 
            // tpCommonCol
            // 
            this.tpCommonCol.Controls.Add(this.groupBox5);
            this.tpCommonCol.Controls.Add(this.groupBox4);
            this.tpCommonCol.Location = new System.Drawing.Point(4, 22);
            this.tpCommonCol.Name = "tpCommonCol";
            this.tpCommonCol.Size = new System.Drawing.Size(551, 396);
            this.tpCommonCol.TabIndex = 2;
            this.tpCommonCol.Text = "数据字典";
            this.tpCommonCol.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox5.Controls.Add(this.dgvCommonCol);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 43);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(551, 353);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "列清单";
            // 
            // dgvCommonCol
            // 
            this.dgvCommonCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommonCol.ContextMenuStrip = this.cmsRemoveCommon;
            this.dgvCommonCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCommonCol.Location = new System.Drawing.Point(3, 17);
            this.dgvCommonCol.Name = "dgvCommonCol";
            this.dgvCommonCol.RowTemplate.Height = 23;
            this.dgvCommonCol.Size = new System.Drawing.Size(545, 333);
            this.dgvCommonCol.TabIndex = 0;
            // 
            // cmsRemoveCommon
            // 
            this.cmsRemoveCommon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveCommon});
            this.cmsRemoveCommon.Name = "cmsRemove";
            this.cmsRemoveCommon.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiRemoveCommon
            // 
            this.tsmiRemoveCommon.Name = "tsmiRemoveCommon";
            this.tsmiRemoveCommon.Size = new System.Drawing.Size(100, 22);
            this.tsmiRemoveCommon.Text = "移除";
            this.tsmiRemoveCommon.Click += new System.EventHandler(this.tsmiRemoveCommon_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnCommonSave);
            this.groupBox4.Controls.Add(this.btnFindCommon);
            this.groupBox4.Controls.Add(this.txbSearchCommon);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(551, 43);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "查找";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(133, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "只支持C、C1、C2定位";
            // 
            // btnCommonSave
            // 
            this.btnCommonSave.Location = new System.Drawing.Point(223, 15);
            this.btnCommonSave.Name = "btnCommonSave";
            this.btnCommonSave.Size = new System.Drawing.Size(49, 23);
            this.btnCommonSave.TabIndex = 3;
            this.btnCommonSave.Text = "保存";
            this.btnCommonSave.UseVisualStyleBackColor = true;
            this.btnCommonSave.Click += new System.EventHandler(this.btnCommonSave_Click);
            // 
            // btnFindCommon
            // 
            this.btnFindCommon.Location = new System.Drawing.Point(168, 15);
            this.btnFindCommon.Name = "btnFindCommon";
            this.btnFindCommon.Size = new System.Drawing.Size(49, 23);
            this.btnFindCommon.TabIndex = 3;
            this.btnFindCommon.Text = "定位";
            this.btnFindCommon.UseVisualStyleBackColor = true;
            this.btnFindCommon.Click += new System.EventHandler(this.btnFindCommon_Click);
            // 
            // txbSearchCommon
            // 
            this.txbSearchCommon.Location = new System.Drawing.Point(6, 17);
            this.txbSearchCommon.Name = "txbSearchCommon";
            this.txbSearchCommon.Size = new System.Drawing.Size(155, 21);
            this.txbSearchCommon.TabIndex = 2;
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(551, 396);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(545, 390);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.dgvSelect);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 149);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(281, 273);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "已选择";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(310, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "提示信息";
            // 
            // dgvSelect
            // 
            this.dgvSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelect.ContextMenuStrip = this.cmsRemoveSelect;
            this.dgvSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelect.Location = new System.Drawing.Point(3, 17);
            this.dgvSelect.Name = "dgvSelect";
            this.dgvSelect.RowTemplate.Height = 23;
            this.dgvSelect.Size = new System.Drawing.Size(275, 253);
            this.dgvSelect.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 105);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 422);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 44;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(206, 422);
            this.splitContainer3.SplitterDistance = 88;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dgvTableList);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.ForeColor = System.Drawing.Color.Black;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(88, 422);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "表清单";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.Location = new System.Drawing.Point(3, 17);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(82, 402);
            this.dgvTableList.TabIndex = 0;
            this.dgvTableList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableList_ColumnHeaderMouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvInput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 422);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "粘贴列";
            // 
            // dgvInput
            // 
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInput.GridColor = System.Drawing.Color.Black;
            this.dgvInput.Location = new System.Drawing.Point(3, 17);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowTemplate.Height = 23;
            this.dgvInput.Size = new System.Drawing.Size(108, 402);
            this.dgvInput.TabIndex = 0;
            this.dgvInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvInput_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox6);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(844, 422);
            this.splitContainer2.SplitterDistance = 281;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbConString);
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 149);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拼接的字符格式";
            // 
            // rtbConString
            // 
            this.rtbConString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConString.Location = new System.Drawing.Point(3, 60);
            this.rtbConString.Name = "rtbConString";
            this.rtbConString.Size = new System.Drawing.Size(275, 86);
            this.rtbConString.TabIndex = 15;
            this.rtbConString.Text = "";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnMatch);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(3, 17);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(275, 43);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(6, 14);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(72, 23);
            this.btnMatch.TabIndex = 3;
            this.btnMatch.Text = "匹配";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // cmsRemoveSelect
            // 
            this.cmsRemoveSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveSelect});
            this.cmsRemoveSelect.Name = "cmsRemove";
            this.cmsRemoveSelect.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiRemoveSelect
            // 
            this.tsmiRemoveSelect.Name = "tsmiRemoveSelect";
            this.tsmiRemoveSelect.Size = new System.Drawing.Size(100, 22);
            this.tsmiRemoveSelect.Text = "移除";
            this.tsmiRemoveSelect.Click += new System.EventHandler(this.tsmiRemoveSelect_Click);
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1054, 78);
            this.uC_DbConnection1.TabIndex = 35;
            // 
            // FrmDBTTableColumnDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 527);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTTableColumnDictionary";
            this.Text = "数据字典";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpAllTableCol.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.cmsAddCommon.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tpCommonCol.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonCol)).EndInit();
            this.cmsRemoveCommon.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tpAutoSQL.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.cmsRemoveSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Label lblInfo;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAllTableCol;
        private System.Windows.Forms.GroupBox grbColumn;
        private System.Windows.Forms.Label lblColumnInfo;
        private System.Windows.Forms.DataGridView dgvColList;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.CheckBox ckbAllTableColumns;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbConString;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSelect;
        private System.Windows.Forms.TextBox txbSearchCol;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TabPage tpCommonCol;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCommonCol;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCommonSave;
        private System.Windows.Forms.Button btnFindCommon;
        private System.Windows.Forms.TextBox txbSearchCommon;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ContextMenuStrip cmsAddCommon;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCommonCol;
        private System.Windows.Forms.ContextMenuStrip cmsRemoveCommon;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCommon;
        private System.Windows.Forms.ContextMenuStrip cmsRemoveSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSelect;
    }
}