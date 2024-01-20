namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTExcelGenerateTableSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTExcelGenerateTableSQL));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChooseOrNot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDirectShow = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTableData = new System.Windows.Forms.Label();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.btnFindFront = new System.Windows.Forms.Button();
            this.lblFind = new System.Windows.Forms.Label();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.txbSearchColumn = new System.Windows.Forms.TextBox();
            this.lblColumnInfo = new System.Windows.Forms.Label();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbInputType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbTargetDbType = new System.Windows.Forms.ComboBox();
            this.ckbDefaultPKName = new System.Windows.Forms.CheckBox();
            this.ckbFullTypeDoc = new System.Windows.Forms.CheckBox();
            this.ckbLYTemplate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblThree = new System.Windows.Forms.Label();
            this.cbbImportDBType = new System.Windows.Forms.ComboBox();
            this.cbbCreateType = new System.Windows.Forms.ComboBox();
            this.ckbAllConvert = new System.Windows.Forms.CheckBox();
            this.btnSaveOther = new System.Windows.Forms.Button();
            this.lblTableName = new System.Windows.Forms.Label();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbDownLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.gbTable = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbExcludeColumn = new System.Windows.Forms.CheckBox();
            this.txbExcludeColumn = new System.Windows.Forms.TextBox();
            this.ckbQueryColumnRealTime = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbTable.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 240);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1035, 277);
            this.tabControl1.TabIndex = 25;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.splitContainer1);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(1027, 251);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbColumn);
            this.splitContainer1.Size = new System.Drawing.Size(1021, 245);
            this.splitContainer1.SplitterDistance = 49;
            this.splitContainer1.TabIndex = 9;
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.dgvTableList);
            this.grbTable.Controls.Add(this.lblTableData);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTable.Location = new System.Drawing.Point(0, 0);
            this.grbTable.Name = "grbTable";
            this.grbTable.Size = new System.Drawing.Size(1021, 49);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "表清单";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTableList.Location = new System.Drawing.Point(3, 17);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(1015, 29);
            this.dgvTableList.TabIndex = 0;
            this.dgvTableList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableList_ColumnHeaderMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChooseOrNot,
            this.tsmiDirectShow});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 48);
            // 
            // tsmiChooseOrNot
            // 
            this.tsmiChooseOrNot.Name = "tsmiChooseOrNot";
            this.tsmiChooseOrNot.Size = new System.Drawing.Size(153, 22);
            this.tsmiChooseOrNot.Text = "选中/取消选中";
            this.tsmiChooseOrNot.Click += new System.EventHandler(this.tsmiChooseOrNot_Click);
            // 
            // tsmiDirectShow
            // 
            this.tsmiDirectShow.Name = "tsmiDirectShow";
            this.tsmiDirectShow.Size = new System.Drawing.Size(153, 22);
            this.tsmiDirectShow.Text = "显示方向切换";
            this.tsmiDirectShow.Click += new System.EventHandler(this.tsmiDirectShow_Click);
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(174, 1);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(53, 12);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "提示信息";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.btnFindFront);
            this.grbColumn.Controls.Add(this.lblFind);
            this.grbColumn.Controls.Add(this.btnFindNext);
            this.grbColumn.Controls.Add(this.txbSearchColumn);
            this.grbColumn.Controls.Add(this.lblColumnInfo);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(0, 0);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(1021, 192);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // btnFindFront
            // 
            this.btnFindFront.Location = new System.Drawing.Point(314, -2);
            this.btnFindFront.Name = "btnFindFront";
            this.btnFindFront.Size = new System.Drawing.Size(55, 21);
            this.btnFindFront.TabIndex = 22;
            this.btnFindFront.Text = "往前找";
            this.btnFindFront.UseVisualStyleBackColor = true;
            this.btnFindFront.Click += new System.EventHandler(this.btnFindFront_Click);
            // 
            // lblFind
            // 
            this.lblFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFind.AutoSize = true;
            this.lblFind.ForeColor = System.Drawing.Color.Red;
            this.lblFind.Location = new System.Drawing.Point(375, 0);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(53, 12);
            this.lblFind.TabIndex = 21;
            this.lblFind.Text = "查找提示";
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(255, -2);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(56, 21);
            this.btnFindNext.TabIndex = 20;
            this.btnFindNext.Text = "下一个";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // txbSearchColumn
            // 
            this.txbSearchColumn.Location = new System.Drawing.Point(65, -3);
            this.txbSearchColumn.Name = "txbSearchColumn";
            this.txbSearchColumn.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumn.TabIndex = 19;
            // 
            // lblColumnInfo
            // 
            this.lblColumnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColumnInfo.AutoSize = true;
            this.lblColumnInfo.ForeColor = System.Drawing.Color.Red;
            this.lblColumnInfo.Location = new System.Drawing.Point(644, 0);
            this.lblColumnInfo.Name = "lblColumnInfo";
            this.lblColumnInfo.Size = new System.Drawing.Size(53, 12);
            this.lblColumnInfo.TabIndex = 13;
            this.lblColumnInfo.Text = "提示信息";
            // 
            // dgvColList
            // 
            this.dgvColList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvColList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColList.Location = new System.Drawing.Point(3, 17);
            this.dgvColList.Name = "dgvColList";
            this.dgvColList.RowTemplate.Height = 23;
            this.dgvColList.Size = new System.Drawing.Size(1015, 172);
            this.dgvColList.TabIndex = 0;
            this.dgvColList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvColList_ColumnHeaderMouseDoubleClick);
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(1027, 261);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1021, 255);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 79);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 295F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbInputType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbTargetDbType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbDefaultPKName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbFullTypeDoc, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbLYTemplate, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblThree, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbImportDBType, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbCreateType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbAllConvert, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveOther, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1029, 59);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(39, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "录入方式：";
            // 
            // cbbInputType
            // 
            this.cbbInputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbInputType.FormattingEnabled = true;
            this.cbbInputType.Location = new System.Drawing.Point(110, 3);
            this.cbbInputType.Name = "cbbInputType";
            this.cbbInputType.Size = new System.Drawing.Size(146, 20);
            this.cbbInputType.TabIndex = 18;
            this.cbbInputType.SelectedIndexChanged += new System.EventHandler(this.cbbInputType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "目标数据库类型：";
            // 
            // cbbTargetDbType
            // 
            this.cbbTargetDbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTargetDbType.FormattingEnabled = true;
            this.cbbTargetDbType.Location = new System.Drawing.Point(110, 29);
            this.cbbTargetDbType.Name = "cbbTargetDbType";
            this.cbbTargetDbType.Size = new System.Drawing.Size(146, 20);
            this.cbbTargetDbType.TabIndex = 3;
            // 
            // ckbDefaultPKName
            // 
            this.ckbDefaultPKName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbDefaultPKName.AutoSize = true;
            this.ckbDefaultPKName.Checked = true;
            this.ckbDefaultPKName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDefaultPKName.Location = new System.Drawing.Point(496, 5);
            this.ckbDefaultPKName.Name = "ckbDefaultPKName";
            this.ckbDefaultPKName.Size = new System.Drawing.Size(84, 16);
            this.ckbDefaultPKName.TabIndex = 21;
            this.ckbDefaultPKName.Text = "默认主键名";
            this.ckbDefaultPKName.UseVisualStyleBackColor = true;
            // 
            // ckbFullTypeDoc
            // 
            this.ckbFullTypeDoc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbFullTypeDoc.AutoSize = true;
            this.ckbFullTypeDoc.Location = new System.Drawing.Point(586, 5);
            this.ckbFullTypeDoc.Name = "ckbFullTypeDoc";
            this.ckbFullTypeDoc.Size = new System.Drawing.Size(60, 16);
            this.ckbFullTypeDoc.TabIndex = 22;
            this.ckbFullTypeDoc.Text = "全类型";
            this.ckbFullTypeDoc.UseVisualStyleBackColor = true;
            // 
            // ckbLYTemplate
            // 
            this.ckbLYTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbLYTemplate.AutoSize = true;
            this.ckbLYTemplate.Location = new System.Drawing.Point(663, 5);
            this.ckbLYTemplate.Name = "ckbLYTemplate";
            this.ckbLYTemplate.Size = new System.Drawing.Size(60, 16);
            this.ckbLYTemplate.TabIndex = 23;
            this.ckbLYTemplate.Text = "LY模板";
            this.ckbLYTemplate.UseVisualStyleBackColor = true;
            this.ckbLYTemplate.CheckedChanged += new System.EventHandler(this.ckbLYTemplate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(262, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "导入数据库类型：";
            // 
            // lblThree
            // 
            this.lblThree.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblThree.AutoSize = true;
            this.lblThree.ForeColor = System.Drawing.Color.Red;
            this.lblThree.Location = new System.Drawing.Point(262, 7);
            this.lblThree.Name = "lblThree";
            this.lblThree.Size = new System.Drawing.Size(65, 12);
            this.lblThree.TabIndex = 1;
            this.lblThree.Text = "创建方式：";
            // 
            // cbbImportDBType
            // 
            this.cbbImportDBType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbImportDBType.FormattingEnabled = true;
            this.cbbImportDBType.Location = new System.Drawing.Point(369, 29);
            this.cbbImportDBType.Name = "cbbImportDBType";
            this.cbbImportDBType.Size = new System.Drawing.Size(121, 20);
            this.cbbImportDBType.TabIndex = 4;
            this.cbbImportDBType.SelectedIndexChanged += new System.EventHandler(this.cbbImportDBType_SelectedIndexChanged);
            // 
            // cbbCreateType
            // 
            this.cbbCreateType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbCreateType.FormattingEnabled = true;
            this.cbbCreateType.Location = new System.Drawing.Point(369, 3);
            this.cbbCreateType.Name = "cbbCreateType";
            this.cbbCreateType.Size = new System.Drawing.Size(121, 20);
            this.cbbCreateType.TabIndex = 5;
            // 
            // ckbAllConvert
            // 
            this.ckbAllConvert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAllConvert.AutoSize = true;
            this.ckbAllConvert.Location = new System.Drawing.Point(496, 31);
            this.ckbAllConvert.Name = "ckbAllConvert";
            this.ckbAllConvert.Size = new System.Drawing.Size(72, 16);
            this.ckbAllConvert.TabIndex = 7;
            this.ckbAllConvert.Text = "综合转换";
            this.ckbAllConvert.UseVisualStyleBackColor = true;
            this.ckbAllConvert.CheckedChanged += new System.EventHandler(this.ckbAllConvert_CheckedChanged);
            // 
            // btnSaveOther
            // 
            this.btnSaveOther.Location = new System.Drawing.Point(585, 28);
            this.btnSaveOther.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveOther.Name = "btnSaveOther";
            this.btnSaveOther.Size = new System.Drawing.Size(73, 20);
            this.btnSaveOther.TabIndex = 17;
            this.btnSaveOther.Text = "SQL另存为";
            this.btnSaveOther.UseVisualStyleBackColor = true;
            this.btnSaveOther.Click += new System.EventHandler(this.BtnSaveOther_Click);
            // 
            // lblTableName
            // 
            this.lblTableName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTableName.AutoSize = true;
            this.lblTableName.ForeColor = System.Drawing.Color.Red;
            this.lblTableName.Location = new System.Drawing.Point(3, 7);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(41, 12);
            this.lblTableName.TabIndex = 1;
            this.lblTableName.Text = "表名：";
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(248, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 20;
            this.ckbGetTableList.Text = "加载表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(50, 3);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(192, 20);
            this.cbbTableName.TabIndex = 19;
            this.cbbTableName.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImport,
            this.tsbAutoSQL,
            this.tsbDownLoad,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1035, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(68, 24);
            this.tsbImport.Text = "导入(&I)";
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
            // tsbDownLoad
            // 
            this.tsbDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownLoad.Image")));
            this.tsbDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownLoad.Name = "tsbDownLoad";
            this.tsbDownLoad.Size = new System.Drawing.Size(97, 24);
            this.tsbDownLoad.Text = "下载模板(&D)";
            this.tsbDownLoad.Click += new System.EventHandler(this.tsbDownLoad_Click);
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
            // gbTable
            // 
            this.gbTable.Controls.Add(this.tableLayoutPanel2);
            this.gbTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTable.Location = new System.Drawing.Point(0, 187);
            this.gbTable.Name = "gbTable";
            this.gbTable.Size = new System.Drawing.Size(1035, 53);
            this.gbTable.TabIndex = 37;
            this.gbTable.TabStop = false;
            this.gbTable.Text = "表信息";
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lblTableName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbTableName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbExcludeColumn, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.txbExcludeColumn, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbQueryColumnRealTime, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbGetTableList, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1029, 30);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ckbExcludeColumn
            // 
            this.ckbExcludeColumn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbExcludeColumn.AutoSize = true;
            this.ckbExcludeColumn.Checked = true;
            this.ckbExcludeColumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbExcludeColumn.Location = new System.Drawing.Point(428, 5);
            this.ckbExcludeColumn.Name = "ckbExcludeColumn";
            this.ckbExcludeColumn.Size = new System.Drawing.Size(60, 16);
            this.ckbExcludeColumn.TabIndex = 21;
            this.ckbExcludeColumn.Text = "排除列";
            this.ckbExcludeColumn.UseVisualStyleBackColor = true;
            this.ckbExcludeColumn.CheckedChanged += new System.EventHandler(this.ckbExcludeColumn_CheckedChanged);
            // 
            // txbExcludeColumn
            // 
            this.txbExcludeColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.txbExcludeColumn, 2);
            this.txbExcludeColumn.Location = new System.Drawing.Point(494, 3);
            this.txbExcludeColumn.Name = "txbExcludeColumn";
            this.txbExcludeColumn.Size = new System.Drawing.Size(506, 21);
            this.txbExcludeColumn.TabIndex = 22;
            // 
            // ckbQueryColumnRealTime
            // 
            this.ckbQueryColumnRealTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbQueryColumnRealTime.AutoSize = true;
            this.ckbQueryColumnRealTime.Location = new System.Drawing.Point(338, 5);
            this.ckbQueryColumnRealTime.Name = "ckbQueryColumnRealTime";
            this.ckbQueryColumnRealTime.Size = new System.Drawing.Size(84, 16);
            this.ckbQueryColumnRealTime.TabIndex = 23;
            this.ckbQueryColumnRealTime.Text = "实时查询列";
            this.ckbQueryColumnRealTime.UseVisualStyleBackColor = true;
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 106);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1035, 81);
            this.uC_DbConnection1.TabIndex = 36;
            // 
            // FrmDBTExcelGenerateTableSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 517);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gbTable);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTExcelGenerateTableSQL";
            this.Text = "生成表SQL";
            this.Load += new System.EventHandler(this.FrmDBTImportExcelGenerateTableSQL_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbTable.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbColumn;
        private System.Windows.Forms.Label lblColumnInfo;
        private System.Windows.Forms.DataGridView dgvColList;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.Label lblTableData;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblThree;
        private System.Windows.Forms.ComboBox cbbCreateType;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbDownLoad;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ComboBox cbbImportDBType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbTargetDbType;
        private System.Windows.Forms.CheckBox ckbAllConvert;
        private System.Windows.Forms.Button btnSaveOther;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbInputType;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.ComboBox cbbTableName;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.CheckBox ckbDefaultPKName;
        private System.Windows.Forms.CheckBox ckbFullTypeDoc;
        private System.Windows.Forms.CheckBox ckbLYTemplate;
        private System.Windows.Forms.Button btnFindFront;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.TextBox txbSearchColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDirectShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiChooseOrNot;
        private System.Windows.Forms.GroupBox gbTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox ckbExcludeColumn;
        private System.Windows.Forms.TextBox txbExcludeColumn;
        private System.Windows.Forms.CheckBox ckbQueryColumnRealTime;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}