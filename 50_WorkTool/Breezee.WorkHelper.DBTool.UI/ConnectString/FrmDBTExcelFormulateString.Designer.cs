using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTExcelFormulateString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTExcelFormulateString));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grbOrcNet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.ckbSkipFixNull = new System.Windows.Forms.CheckBox();
            this.ckbCancelDefault = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.lblTableData = new System.Windows.Forms.Label();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.lblColumnInfo = new System.Windows.Forms.Label();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemoveDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.tpExcelCol = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvExcelCol = new System.Windows.Forms.DataGridView();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nudColumnNum = new System.Windows.Forms.NumericUpDown();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.txbTableName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbLastFengHao = new System.Windows.Forms.CheckBox();
            this.cbbConnString = new System.Windows.Forms.ComboBox();
            this.lblColumnNum = new System.Windows.Forms.Label();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            this.grbOrcNet.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tpExcelCol.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelCol)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1087, 27);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(74, 24);
            this.tsbImport.Text = "查询(&Q)";
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
            this.lblInfo.Location = new System.Drawing.Point(247, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(53, 12);
            this.lblInfo.TabIndex = 31;
            this.lblInfo.Text = "提示信息";
            // 
            // grbOrcNet
            // 
            this.grbOrcNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbOrcNet.Controls.Add(this.tableLayoutPanel4);
            this.grbOrcNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbOrcNet.Location = new System.Drawing.Point(0, 151);
            this.grbOrcNet.Name = "grbOrcNet";
            this.grbOrcNet.Size = new System.Drawing.Size(1087, 49);
            this.grbOrcNet.TabIndex = 40;
            this.grbOrcNet.TabStop = false;
            this.grbOrcNet.Text = "表信息";
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
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 486F));
            this.tableLayoutPanel4.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTableName, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbGetTableList, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbSkipFixNull, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbCancelDefault, 4, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1081, 29);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "表名：";
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(50, 3);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(264, 20);
            this.cbbTableName.TabIndex = 2;
            this.cbbTableName.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(320, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 3;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // ckbSkipFixNull
            // 
            this.ckbSkipFixNull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSkipFixNull.AutoSize = true;
            this.ckbSkipFixNull.Checked = true;
            this.ckbSkipFixNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSkipFixNull.Location = new System.Drawing.Point(410, 5);
            this.ckbSkipFixNull.Name = "ckbSkipFixNull";
            this.ckbSkipFixNull.Size = new System.Drawing.Size(72, 16);
            this.ckbSkipFixNull.TabIndex = 3;
            this.ckbSkipFixNull.Text = "忽略NULL";
            this.toolTip1.SetToolTip(this.ckbSkipFixNull, "不把固定值上的NULL当作默认值");
            this.ckbSkipFixNull.UseVisualStyleBackColor = true;
            // 
            // ckbCancelDefault
            // 
            this.ckbCancelDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbCancelDefault.AutoSize = true;
            this.ckbCancelDefault.Location = new System.Drawing.Point(488, 5);
            this.ckbCancelDefault.Name = "ckbCancelDefault";
            this.ckbCancelDefault.Size = new System.Drawing.Size(84, 16);
            this.ckbCancelDefault.TabIndex = 3;
            this.ckbCancelDefault.Text = "忽略默认值";
            this.toolTip1.SetToolTip(this.ckbCancelDefault, "不使用固定值，即全部列参数化。但选中【使用全局配置时】时，还是会用全局配置值！");
            this.ckbCancelDefault.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpExcelCol);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 200);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1087, 317);
            this.tabControl1.TabIndex = 41;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.splitContainer1);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(1079, 291);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbColumn);
            this.splitContainer1.Size = new System.Drawing.Size(1073, 285);
            this.splitContainer1.SplitterDistance = 279;
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
            this.grbTable.Size = new System.Drawing.Size(279, 285);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "表清单";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTableList.Location = new System.Drawing.Point(3, 17);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(273, 265);
            this.dgvTableList.TabIndex = 0;
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(69, 0);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(53, 12);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "提示信息";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.lblColumnInfo);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(0, 0);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(790, 285);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // lblColumnInfo
            // 
            this.lblColumnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColumnInfo.AutoSize = true;
            this.lblColumnInfo.ForeColor = System.Drawing.Color.Red;
            this.lblColumnInfo.Location = new System.Drawing.Point(310, 2);
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
            this.dgvColList.Size = new System.Drawing.Size(784, 265);
            this.dgvColList.TabIndex = 0;
            this.dgvColList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvColList_ColumnHeaderMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveDefault});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // tsmiRemoveDefault
            // 
            this.tsmiRemoveDefault.Name = "tsmiRemoveDefault";
            this.tsmiRemoveDefault.Size = new System.Drawing.Size(136, 22);
            this.tsmiRemoveDefault.Text = "清除默认值";
            this.tsmiRemoveDefault.Click += new System.EventHandler(this.tsmiRemoveDefault_Click);
            // 
            // tpExcelCol
            // 
            this.tpExcelCol.Controls.Add(this.groupBox2);
            this.tpExcelCol.Location = new System.Drawing.Point(4, 22);
            this.tpExcelCol.Name = "tpExcelCol";
            this.tpExcelCol.Padding = new System.Windows.Forms.Padding(3);
            this.tpExcelCol.Size = new System.Drawing.Size(1079, 291);
            this.tpExcelCol.TabIndex = 2;
            this.tpExcelCol.Text = "Excel列清单";
            this.tpExcelCol.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dgvExcelCol);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1073, 285);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列清单";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(310, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "提示信息";
            // 
            // dgvExcelCol
            // 
            this.dgvExcelCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcelCol.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvExcelCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcelCol.Location = new System.Drawing.Point(3, 17);
            this.dgvExcelCol.Name = "dgvExcelCol";
            this.dgvExcelCol.RowTemplate.Height = 23;
            this.dgvExcelCol.Size = new System.Drawing.Size(1067, 265);
            this.dgvExcelCol.TabIndex = 0;
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(1079, 291);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1073, 285);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // nudColumnNum
            // 
            this.nudColumnNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudColumnNum.Location = new System.Drawing.Point(671, 3);
            this.nudColumnNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudColumnNum.Name = "nudColumnNum";
            this.nudColumnNum.Size = new System.Drawing.Size(57, 21);
            this.nudColumnNum.TabIndex = 7;
            this.toolTip1.SetToolTip(this.nudColumnNum, "数量要跟Excel数据模板的列数量一致");
            this.nudColumnNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumnNum.ValueChanged += new System.EventHandler(this.nudColumnNum_ValueChanged);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(74, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(123, 20);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "连接字符：";
            // 
            // lblTable
            // 
            this.lblTable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(399, 7);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(41, 12);
            this.lblTable.TabIndex = 1;
            this.lblTable.Text = "表名：";
            // 
            // txbTableName
            // 
            this.txbTableName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbTableName.Location = new System.Drawing.Point(446, 3);
            this.txbTableName.Name = "txbTableName";
            this.txbTableName.Size = new System.Drawing.Size(160, 21);
            this.txbTableName.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1087, 46);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudColumnNum, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbLastFengHao, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbConnString, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblColumnNum, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTable, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbTableName, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1081, 24);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "生成方式：";
            // 
            // ckbLastFengHao
            // 
            this.ckbLastFengHao.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbLastFengHao.AutoSize = true;
            this.ckbLastFengHao.Checked = true;
            this.ckbLastFengHao.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLastFengHao.Location = new System.Drawing.Point(734, 5);
            this.ckbLastFengHao.Name = "ckbLastFengHao";
            this.ckbLastFengHao.Size = new System.Drawing.Size(84, 16);
            this.ckbLastFengHao.TabIndex = 4;
            this.ckbLastFengHao.Text = "最后加分号";
            this.ckbLastFengHao.UseVisualStyleBackColor = true;
            // 
            // cbbConnString
            // 
            this.cbbConnString.FormattingEnabled = true;
            this.cbbConnString.Location = new System.Drawing.Point(274, 3);
            this.cbbConnString.Name = "cbbConnString";
            this.cbbConnString.Size = new System.Drawing.Size(119, 20);
            this.cbbConnString.TabIndex = 8;
            // 
            // lblColumnNum
            // 
            this.lblColumnNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColumnNum.AutoSize = true;
            this.lblColumnNum.Location = new System.Drawing.Point(612, 7);
            this.lblColumnNum.Name = "lblColumnNum";
            this.lblColumnNum.Size = new System.Drawing.Size(53, 12);
            this.lblColumnNum.TabIndex = 1;
            this.lblColumnNum.Text = "列数量：";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 73);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1087, 78);
            this.uC_DbConnection1.TabIndex = 35;
            // 
            // FrmDBTExcelFormulateString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 517);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbOrcNet);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTExcelFormulateString";
            this.Text = "生成数据Excel公式";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbOrcNet.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpExcelCol.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelCol)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.GroupBox grbOrcNet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbTableName;
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
        private CheckBox ckbCancelDefault;
        private CheckBox ckbSkipFixNull;
        private ToolTip toolTip1;
        private ComboBox cmbType;
        private Label label2;
        private Label lblTable;
        private TextBox txbTableName;
        private GroupBox groupBox1;
        private Label lblColumnNum;
        private NumericUpDown nudColumnNum;
        private ComboBox cbbConnString;
        private Label label1;
        private SplitContainer splitContainer1;
        private CheckBox ckbLastFengHao;
        private TableLayoutPanel tableLayoutPanel1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsmiRemoveDefault;
        private TabPage tpExcelCol;
        private GroupBox groupBox2;
        private Label label3;
        private DataGridView dgvExcelCol;
    }
}