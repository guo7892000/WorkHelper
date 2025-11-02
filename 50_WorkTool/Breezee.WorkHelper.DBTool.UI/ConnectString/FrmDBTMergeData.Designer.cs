namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTMergeData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTMergeData));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbbGroupType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbSqlType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbAutoColumnName = new System.Windows.Forms.CheckBox();
            this.ckbCondTrim = new System.Windows.Forms.CheckBox();
            this.CkbLoadExampleData = new System.Windows.Forms.CheckBox();
            this.ckbNullNotEquals = new System.Windows.Forms.CheckBox();
            this.grbConSting = new System.Windows.Forms.GroupBox();
            this.btnRemoveCond = new System.Windows.Forms.Button();
            this.btnAddCond = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbConString = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.ckbColumnFixed1 = new System.Windows.Forms.CheckBox();
            this.txbSearchColumn1 = new System.Windows.Forms.TextBox();
            this.btnFindFrontColumn1 = new System.Windows.Forms.Button();
            this.btnFindNextColumn1 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.lblFindColum1 = new System.Windows.Forms.Label();
            this.dgvExcel1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiJoin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFindColum2 = new System.Windows.Forms.Label();
            this.ckbColumnFixed2 = new System.Windows.Forms.CheckBox();
            this.txbSearchColumn2 = new System.Windows.Forms.TextBox();
            this.btnFindFrontColumn2 = new System.Windows.Forms.Button();
            this.btnFindNextColumn2 = new System.Windows.Forms.Button();
            this.dgvExcel2 = new System.Windows.Forms.DataGridView();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.btnFindFront = new System.Windows.Forms.Button();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.txbSearchColumn = new System.Windows.Forms.TextBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.lblTableData = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckbColumnFixed = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grbConSting.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel2)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAutoSQL,
            this.tsbExport,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1002, 27);
            this.toolStrip1.TabIndex = 22;
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
            // tsbExport
            // 
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(71, 24);
            this.tsbExport.Text = "导出(&E)";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 60);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 683F));
            this.tableLayoutPanel1.Controls.Add(this.cbbGroupType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbSqlType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbAutoColumnName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbCondTrim, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.CkbLoadExampleData, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbNullNotEquals, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 40);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // cbbGroupType
            // 
            this.cbbGroupType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbGroupType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbGroupType.FormattingEnabled = true;
            this.cbbGroupType.Location = new System.Drawing.Point(275, 4);
            this.cbbGroupType.Name = "cbbGroupType";
            this.cbbGroupType.Size = new System.Drawing.Size(178, 25);
            this.cbbGroupType.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "分组方式：";
            // 
            // cbbSqlType
            // 
            this.cbbSqlType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbSqlType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbbSqlType.FormattingEnabled = true;
            this.cbbSqlType.ItemHeight = 19;
            this.cbbSqlType.Location = new System.Drawing.Point(74, 3);
            this.cbbSqlType.Name = "cbbSqlType";
            this.cbbSqlType.Size = new System.Drawing.Size(124, 27);
            this.cbbSqlType.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "合并类型：";
            // 
            // ckbAutoColumnName
            // 
            this.ckbAutoColumnName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAutoColumnName.AutoSize = true;
            this.ckbAutoColumnName.Location = new System.Drawing.Point(458, 8);
            this.ckbAutoColumnName.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAutoColumnName.Name = "ckbAutoColumnName";
            this.ckbAutoColumnName.Size = new System.Drawing.Size(96, 16);
            this.ckbAutoColumnName.TabIndex = 4;
            this.ckbAutoColumnName.Text = "自动字母列名";
            this.ckbAutoColumnName.UseVisualStyleBackColor = true;
            this.ckbAutoColumnName.CheckedChanged += new System.EventHandler(this.ckbAutoColumnName_CheckedChanged);
            // 
            // ckbCondTrim
            // 
            this.ckbCondTrim.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbCondTrim.AutoSize = true;
            this.ckbCondTrim.Checked = true;
            this.ckbCondTrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCondTrim.Location = new System.Drawing.Point(559, 8);
            this.ckbCondTrim.Name = "ckbCondTrim";
            this.ckbCondTrim.Size = new System.Drawing.Size(132, 16);
            this.ckbCondTrim.TabIndex = 8;
            this.ckbCondTrim.Text = "条件值去掉前后空格";
            this.ckbCondTrim.UseVisualStyleBackColor = true;
            // 
            // CkbLoadExampleData
            // 
            this.CkbLoadExampleData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CkbLoadExampleData.AutoSize = true;
            this.CkbLoadExampleData.Location = new System.Drawing.Point(798, 8);
            this.CkbLoadExampleData.Margin = new System.Windows.Forms.Padding(2);
            this.CkbLoadExampleData.Name = "CkbLoadExampleData";
            this.CkbLoadExampleData.Size = new System.Drawing.Size(96, 16);
            this.CkbLoadExampleData.TabIndex = 7;
            this.CkbLoadExampleData.Text = "加载示例数据";
            this.CkbLoadExampleData.UseVisualStyleBackColor = true;
            this.CkbLoadExampleData.CheckedChanged += new System.EventHandler(this.CkbLoadExampleData_CheckedChanged);
            // 
            // ckbNullNotEquals
            // 
            this.ckbNullNotEquals.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbNullNotEquals.AutoSize = true;
            this.ckbNullNotEquals.Checked = true;
            this.ckbNullNotEquals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNullNotEquals.Location = new System.Drawing.Point(697, 8);
            this.ckbNullNotEquals.Name = "ckbNullNotEquals";
            this.ckbNullNotEquals.Size = new System.Drawing.Size(96, 16);
            this.ckbNullNotEquals.TabIndex = 9;
            this.ckbNullNotEquals.Text = "忽略条件空值";
            this.ckbNullNotEquals.UseVisualStyleBackColor = true;
            // 
            // grbConSting
            // 
            this.grbConSting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbConSting.Controls.Add(this.btnRemoveCond);
            this.grbConSting.Controls.Add(this.btnAddCond);
            this.grbConSting.Controls.Add(this.label21);
            this.grbConSting.Controls.Add(this.rtbConString);
            this.grbConSting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbConSting.Location = new System.Drawing.Point(0, 0);
            this.grbConSting.Name = "grbConSting";
            this.grbConSting.Size = new System.Drawing.Size(1002, 47);
            this.grbConSting.TabIndex = 26;
            this.grbConSting.TabStop = false;
            this.grbConSting.Text = "关联条件";
            // 
            // btnRemoveCond
            // 
            this.btnRemoveCond.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveCond.Image")));
            this.btnRemoveCond.Location = new System.Drawing.Point(108, -3);
            this.btnRemoveCond.Name = "btnRemoveCond";
            this.btnRemoveCond.Size = new System.Drawing.Size(22, 23);
            this.btnRemoveCond.TabIndex = 14;
            this.btnRemoveCond.UseVisualStyleBackColor = true;
            this.btnRemoveCond.Click += new System.EventHandler(this.btnRemoveCond_Click);
            // 
            // btnAddCond
            // 
            this.btnAddCond.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCond.Image")));
            this.btnAddCond.Location = new System.Drawing.Point(77, -3);
            this.btnAddCond.Name = "btnAddCond";
            this.btnAddCond.Size = new System.Drawing.Size(25, 23);
            this.btnAddCond.TabIndex = 14;
            this.btnAddCond.UseVisualStyleBackColor = true;
            this.btnAddCond.Click += new System.EventHandler(this.btnAddCond_Click);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(204, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(119, 12);
            this.label21.TabIndex = 13;
            this.label21.Text = "条件格式：A=A1,B=B1";
            // 
            // rtbConString
            // 
            this.rtbConString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConString.Location = new System.Drawing.Point(3, 17);
            this.rtbConString.Name = "rtbConString";
            this.rtbConString.Size = new System.Drawing.Size(996, 27);
            this.rtbConString.TabIndex = 5;
            this.rtbConString.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1002, 303);
            this.tabControl1.TabIndex = 27;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.ckbColumnFixed1);
            this.tpImport.Controls.Add(this.txbSearchColumn1);
            this.tpImport.Controls.Add(this.btnFindFrontColumn1);
            this.tpImport.Controls.Add(this.btnFindNextColumn1);
            this.tpImport.Controls.Add(this.splitContainer2);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(994, 277);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "Excel列表";
            // 
            // ckbColumnFixed1
            // 
            this.ckbColumnFixed1.AutoSize = true;
            this.ckbColumnFixed1.Location = new System.Drawing.Point(251, 3);
            this.ckbColumnFixed1.Name = "ckbColumnFixed1";
            this.ckbColumnFixed1.Size = new System.Drawing.Size(48, 16);
            this.ckbColumnFixed1.TabIndex = 30;
            this.ckbColumnFixed1.Text = "精确";
            this.ckbColumnFixed1.UseVisualStyleBackColor = true;
            // 
            // txbSearchColumn1
            // 
            this.txbSearchColumn1.Location = new System.Drawing.Point(104, 0);
            this.txbSearchColumn1.Name = "txbSearchColumn1";
            this.txbSearchColumn1.Size = new System.Drawing.Size(141, 21);
            this.txbSearchColumn1.TabIndex = 29;
            // 
            // btnFindFrontColumn1
            // 
            this.btnFindFrontColumn1.Location = new System.Drawing.Point(364, 0);
            this.btnFindFrontColumn1.Name = "btnFindFrontColumn1";
            this.btnFindFrontColumn1.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontColumn1.TabIndex = 28;
            this.btnFindFrontColumn1.Text = "往前找";
            this.btnFindFrontColumn1.UseVisualStyleBackColor = true;
            this.btnFindFrontColumn1.Click += new System.EventHandler(this.btnFindFrontColumn1_Click);
            // 
            // btnFindNextColumn1
            // 
            this.btnFindNextColumn1.Location = new System.Drawing.Point(302, 0);
            this.btnFindNextColumn1.Name = "btnFindNextColumn1";
            this.btnFindNextColumn1.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextColumn1.TabIndex = 27;
            this.btnFindNextColumn1.Text = "下一个";
            this.btnFindNextColumn1.UseVisualStyleBackColor = true;
            this.btnFindNextColumn1.Click += new System.EventHandler(this.btnFindNextColumn1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.grbTable);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(988, 271);
            this.splitContainer2.SplitterDistance = 479;
            this.splitContainer2.TabIndex = 3;
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.lblFindColum1);
            this.grbTable.Controls.Add(this.dgvExcel1);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTable.Location = new System.Drawing.Point(0, 0);
            this.grbTable.Name = "grbTable";
            this.grbTable.Size = new System.Drawing.Size(479, 271);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "粘贴的Excel-1";
            // 
            // lblFindColum1
            // 
            this.lblFindColum1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindColum1.AutoSize = true;
            this.lblFindColum1.ForeColor = System.Drawing.Color.Red;
            this.lblFindColum1.Location = new System.Drawing.Point(420, 2);
            this.lblFindColum1.Name = "lblFindColum1";
            this.lblFindColum1.Size = new System.Drawing.Size(53, 12);
            this.lblFindColum1.TabIndex = 23;
            this.lblFindColum1.Text = "查找提示";
            // 
            // dgvExcel1
            // 
            this.dgvExcel1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcel1.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvExcel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcel1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvExcel1.Location = new System.Drawing.Point(3, 17);
            this.dgvExcel1.Name = "dgvExcel1";
            this.dgvExcel1.RowTemplate.Height = 23;
            this.dgvExcel1.Size = new System.Drawing.Size(473, 251);
            this.dgvExcel1.TabIndex = 0;
            this.dgvExcel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvExcel1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiJoin,
            this.tsmiDelete,
            this.tsmiClear,
            this.tsmiPaste});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 92);
            // 
            // tsmiJoin
            // 
            this.tsmiJoin.Name = "tsmiJoin";
            this.tsmiJoin.Size = new System.Drawing.Size(100, 22);
            this.tsmiJoin.Text = "加入";
            this.tsmiJoin.Click += new System.EventHandler(this.tsmiJoin_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(100, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(100, 22);
            this.tsmiClear.Text = "清空";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(100, 22);
            this.tsmiPaste.Text = "粘贴";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox2.Controls.Add(this.lblFindColum2);
            this.groupBox2.Controls.Add(this.ckbColumnFixed2);
            this.groupBox2.Controls.Add(this.txbSearchColumn2);
            this.groupBox2.Controls.Add(this.btnFindFrontColumn2);
            this.groupBox2.Controls.Add(this.btnFindNextColumn2);
            this.groupBox2.Controls.Add(this.dgvExcel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 271);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "粘贴的Excel-2";
            // 
            // lblFindColum2
            // 
            this.lblFindColum2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindColum2.AutoSize = true;
            this.lblFindColum2.ForeColor = System.Drawing.Color.Red;
            this.lblFindColum2.Location = new System.Drawing.Point(440, 0);
            this.lblFindColum2.Name = "lblFindColum2";
            this.lblFindColum2.Size = new System.Drawing.Size(53, 12);
            this.lblFindColum2.TabIndex = 31;
            this.lblFindColum2.Text = "查找提示";
            // 
            // ckbColumnFixed2
            // 
            this.ckbColumnFixed2.AutoSize = true;
            this.ckbColumnFixed2.Location = new System.Drawing.Point(266, 0);
            this.ckbColumnFixed2.Name = "ckbColumnFixed2";
            this.ckbColumnFixed2.Size = new System.Drawing.Size(48, 16);
            this.ckbColumnFixed2.TabIndex = 30;
            this.ckbColumnFixed2.Text = "精确";
            this.ckbColumnFixed2.UseVisualStyleBackColor = true;
            // 
            // txbSearchColumn2
            // 
            this.txbSearchColumn2.Location = new System.Drawing.Point(119, -3);
            this.txbSearchColumn2.Name = "txbSearchColumn2";
            this.txbSearchColumn2.Size = new System.Drawing.Size(141, 21);
            this.txbSearchColumn2.TabIndex = 29;
            // 
            // btnFindFrontColumn2
            // 
            this.btnFindFrontColumn2.Location = new System.Drawing.Point(379, -3);
            this.btnFindFrontColumn2.Name = "btnFindFrontColumn2";
            this.btnFindFrontColumn2.Size = new System.Drawing.Size(55, 21);
            this.btnFindFrontColumn2.TabIndex = 28;
            this.btnFindFrontColumn2.Text = "往前找";
            this.btnFindFrontColumn2.UseVisualStyleBackColor = true;
            this.btnFindFrontColumn2.Click += new System.EventHandler(this.btnFindFrontColumn2_Click);
            // 
            // btnFindNextColumn2
            // 
            this.btnFindNextColumn2.Location = new System.Drawing.Point(317, -3);
            this.btnFindNextColumn2.Name = "btnFindNextColumn2";
            this.btnFindNextColumn2.Size = new System.Drawing.Size(56, 21);
            this.btnFindNextColumn2.TabIndex = 27;
            this.btnFindNextColumn2.Text = "下一个";
            this.btnFindNextColumn2.UseVisualStyleBackColor = true;
            this.btnFindNextColumn2.Click += new System.EventHandler(this.btnFindNextColumn2_Click);
            // 
            // dgvExcel2
            // 
            this.dgvExcel2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcel2.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvExcel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcel2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvExcel2.Location = new System.Drawing.Point(3, 17);
            this.dgvExcel2.Name = "dgvExcel2";
            this.dgvExcel2.RowTemplate.Height = 23;
            this.dgvExcel2.Size = new System.Drawing.Size(499, 251);
            this.dgvExcel2.TabIndex = 0;
            this.dgvExcel2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvExcel2_KeyDown);
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.groupBox3);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(994, 277);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbColumnFixed);
            this.groupBox3.Controls.Add(this.lblFind);
            this.groupBox3.Controls.Add(this.btnFindFront);
            this.groupBox3.Controls.Add(this.btnFindNext);
            this.groupBox3.Controls.Add(this.txbSearchColumn);
            this.groupBox3.Controls.Add(this.dgvResult);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(988, 271);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成结果";
            // 
            // lblFind
            // 
            this.lblFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFind.AutoSize = true;
            this.lblFind.ForeColor = System.Drawing.Color.Red;
            this.lblFind.Location = new System.Drawing.Point(459, 2);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(53, 12);
            this.lblFind.TabIndex = 22;
            this.lblFind.Text = "查找提示";
            // 
            // btnFindFront
            // 
            this.btnFindFront.Location = new System.Drawing.Point(391, -2);
            this.btnFindFront.Name = "btnFindFront";
            this.btnFindFront.Size = new System.Drawing.Size(55, 21);
            this.btnFindFront.TabIndex = 21;
            this.btnFindFront.Text = "往前找";
            this.btnFindFront.UseVisualStyleBackColor = true;
            this.btnFindFront.Click += new System.EventHandler(this.btnFindFront_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(332, -2);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(56, 21);
            this.btnFindNext.TabIndex = 20;
            this.btnFindNext.Text = "下一个";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // txbSearchColumn
            // 
            this.txbSearchColumn.Location = new System.Drawing.Point(85, -3);
            this.txbSearchColumn.Name = "txbSearchColumn";
            this.txbSearchColumn.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumn.TabIndex = 19;
            // 
            // dgvResult
            // 
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvResult.Location = new System.Drawing.Point(3, 17);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(982, 251);
            this.dgvResult.TabIndex = 1;
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(306, 9);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(53, 12);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "提示信息";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 87);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbConSting);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 354);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 28;
            // 
            // ckbColumnFixed
            // 
            this.ckbColumnFixed.AutoSize = true;
            this.ckbColumnFixed.Location = new System.Drawing.Point(278, 2);
            this.ckbColumnFixed.Name = "ckbColumnFixed";
            this.ckbColumnFixed.Size = new System.Drawing.Size(48, 16);
            this.ckbColumnFixed.TabIndex = 31;
            this.ckbColumnFixed.Text = "精确";
            this.ckbColumnFixed.UseVisualStyleBackColor = true;
            // 
            // FrmDBTMergeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 441);
            this.Controls.Add(this.lblTableData);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTMergeData";
            this.Text = "复制两个Excel数据拼接";
            this.Load += new System.EventHandler(this.FrmCopyData_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grbConSting.ResumeLayout(false);
            this.grbConSting.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.tpImport.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel2)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grbConSting;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox rtbConString;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvExcel1;
        private System.Windows.Forms.Label lblTableData;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox ckbAutoColumnName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvExcel2;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.ComboBox cbbSqlType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CkbLoadExampleData;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.CheckBox ckbCondTrim;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnFindFront;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.TextBox txbSearchColumn;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox ckbNullNotEquals;
        private System.Windows.Forms.ToolStripMenuItem tsmiJoin;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbGroupType;
        private System.Windows.Forms.Button btnRemoveCond;
        private System.Windows.Forms.Button btnAddCond;
        private System.Windows.Forms.CheckBox ckbColumnFixed1;
        private System.Windows.Forms.TextBox txbSearchColumn1;
        private System.Windows.Forms.Button btnFindFrontColumn1;
        private System.Windows.Forms.Button btnFindNextColumn1;
        private System.Windows.Forms.CheckBox ckbColumnFixed2;
        private System.Windows.Forms.TextBox txbSearchColumn2;
        private System.Windows.Forms.Button btnFindFrontColumn2;
        private System.Windows.Forms.Button btnFindNextColumn2;
        private System.Windows.Forms.Label lblFindColum1;
        private System.Windows.Forms.Label lblFindColum2;
        private System.Windows.Forms.CheckBox ckbColumnFixed;
    }
}