using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTGetTableQuerySql
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTGetTableQuerySql));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.grbOrcNet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txbTableShortName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbParaType = new System.Windows.Forms.ComboBox();
            this.lblParam = new System.Windows.Forms.Label();
            this.txbParamPre = new System.Windows.Forms.TextBox();
            this.lblDefineFormat = new System.Windows.Forms.Label();
            this.txbDefineFormart = new System.Windows.Forms.TextBox();
            this.ckbUseDefaultConfig = new System.Windows.Forms.CheckBox();
            this.ckbNewLine = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbWordConvert = new System.Windows.Forms.ComboBox();
            this.ckbCancelDefault = new System.Windows.Forms.CheckBox();
            this.ckbSkipFixNull = new System.Windows.Forms.CheckBox();
            this.ckbUseRemark = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.btnFindFront = new System.Windows.Forms.Button();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.txbSearchColumn = new System.Windows.Forms.TextBox();
            this.lblColumnInfo = new System.Windows.Forms.Label();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            this.grbOrcNet.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).BeginInit();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
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
            // grbOrcNet
            // 
            this.grbOrcNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbOrcNet.Controls.Add(this.tableLayoutPanel4);
            this.grbOrcNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbOrcNet.Location = new System.Drawing.Point(0, 105);
            this.grbOrcNet.Name = "grbOrcNet";
            this.grbOrcNet.Size = new System.Drawing.Size(1087, 74);
            this.grbOrcNet.TabIndex = 40;
            this.grbOrcNet.TabStop = false;
            this.grbOrcNet.Text = "表和SQL类型";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 15;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel4.Controls.Add(this.ckbGetTableList, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbType, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label14, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTableName, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label19, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.txbTableShortName, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbParaType, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblParam, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.txbParamPre, 10, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblDefineFormat, 11, 0);
            this.tableLayoutPanel4.Controls.Add(this.txbDefineFormart, 13, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbUseDefaultConfig, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.ckbNewLine, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 7, 1);
            this.tableLayoutPanel4.Controls.Add(this.cbbWordConvert, 8, 1);
            this.tableLayoutPanel4.Controls.Add(this.ckbCancelDefault, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.ckbSkipFixNull, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.ckbUseRemark, 2, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1081, 54);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(459, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 3;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(68, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(61, 20);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL类型：";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(142, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "表名：";
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tableLayoutPanel4.SetColumnSpan(this.cbbTableName, 3);
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(189, 3);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(264, 20);
            this.cbbTableName.TabIndex = 2;
            this.cbbTableName.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 1;
            this.label19.Text = "别名：";
            // 
            // txbTableShortName
            // 
            this.txbTableShortName.Location = new System.Drawing.Point(68, 30);
            this.txbTableShortName.Name = "txbTableShortName";
            this.txbTableShortName.Size = new System.Drawing.Size(61, 21);
            this.txbTableShortName.TabIndex = 6;
            this.txbTableShortName.Text = "A";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(561, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "参数类型：";
            // 
            // cbbParaType
            // 
            this.cbbParaType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbParaType.FormattingEnabled = true;
            this.cbbParaType.Location = new System.Drawing.Point(632, 3);
            this.cbbParaType.Name = "cbbParaType";
            this.cbbParaType.Size = new System.Drawing.Size(112, 20);
            this.cbbParaType.TabIndex = 8;
            this.cbbParaType.SelectedIndexChanged += new System.EventHandler(this.CbbParaType_SelectedIndexChanged);
            // 
            // lblParam
            // 
            this.lblParam.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblParam.AutoSize = true;
            this.lblParam.Location = new System.Drawing.Point(750, 7);
            this.lblParam.Name = "lblParam";
            this.lblParam.Size = new System.Drawing.Size(65, 12);
            this.lblParam.TabIndex = 7;
            this.lblParam.Text = "列名字符：";
            // 
            // txbParamPre
            // 
            this.txbParamPre.Location = new System.Drawing.Point(821, 3);
            this.txbParamPre.Name = "txbParamPre";
            this.txbParamPre.Size = new System.Drawing.Size(32, 21);
            this.txbParamPre.TabIndex = 9;
            this.txbParamPre.Text = "@";
            // 
            // lblDefineFormat
            // 
            this.lblDefineFormat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDefineFormat.AutoSize = true;
            this.lblDefineFormat.Location = new System.Drawing.Point(859, 7);
            this.lblDefineFormat.Name = "lblDefineFormat";
            this.lblDefineFormat.Size = new System.Drawing.Size(77, 12);
            this.lblDefineFormat.TabIndex = 7;
            this.lblDefineFormat.Text = "自定义格式：";
            // 
            // txbDefineFormart
            // 
            this.txbDefineFormart.Dock = System.Windows.Forms.DockStyle.Top;
            this.txbDefineFormart.Location = new System.Drawing.Point(942, 3);
            this.txbDefineFormart.Name = "txbDefineFormart";
            this.txbDefineFormart.Size = new System.Drawing.Size(100, 21);
            this.txbDefineFormart.TabIndex = 6;
            // 
            // ckbUseDefaultConfig
            // 
            this.ckbUseDefaultConfig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbUseDefaultConfig.AutoSize = true;
            this.ckbUseDefaultConfig.Location = new System.Drawing.Point(189, 32);
            this.ckbUseDefaultConfig.Name = "ckbUseDefaultConfig";
            this.ckbUseDefaultConfig.Size = new System.Drawing.Size(96, 16);
            this.ckbUseDefaultConfig.TabIndex = 11;
            this.ckbUseDefaultConfig.Text = "使用全局配置";
            this.toolTip1.SetToolTip(this.ckbUseDefaultConfig, "是否使用【字段默认值与条件配置】上的配置");
            this.ckbUseDefaultConfig.UseVisualStyleBackColor = true;
            this.ckbUseDefaultConfig.CheckedChanged += new System.EventHandler(this.ckbUseDefaultConfig_CheckedChanged);
            // 
            // ckbNewLine
            // 
            this.ckbNewLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbNewLine.AutoSize = true;
            this.ckbNewLine.Checked = true;
            this.ckbNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNewLine.Location = new System.Drawing.Point(459, 32);
            this.ckbNewLine.Name = "ckbNewLine";
            this.ckbNewLine.Size = new System.Drawing.Size(84, 16);
            this.ckbNewLine.TabIndex = 3;
            this.ckbNewLine.Text = "换行";
            this.toolTip1.SetToolTip(this.ckbNewLine, "生成的SQL是否换行");
            this.ckbNewLine.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(549, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "首字母方式：";
            // 
            // cbbWordConvert
            // 
            this.cbbWordConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbWordConvert.FormattingEnabled = true;
            this.cbbWordConvert.Location = new System.Drawing.Point(632, 30);
            this.cbbWordConvert.Name = "cbbWordConvert";
            this.cbbWordConvert.Size = new System.Drawing.Size(112, 20);
            this.cbbWordConvert.TabIndex = 8;
            this.cbbWordConvert.SelectedIndexChanged += new System.EventHandler(this.CbbParaType_SelectedIndexChanged);
            // 
            // ckbCancelDefault
            // 
            this.ckbCancelDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbCancelDefault.AutoSize = true;
            this.ckbCancelDefault.Location = new System.Drawing.Point(369, 32);
            this.ckbCancelDefault.Name = "ckbCancelDefault";
            this.ckbCancelDefault.Size = new System.Drawing.Size(84, 16);
            this.ckbCancelDefault.TabIndex = 3;
            this.ckbCancelDefault.Text = "忽略默认值";
            this.toolTip1.SetToolTip(this.ckbCancelDefault, "不使用固定值，即全部列参数化。但选中【使用全局配置时】时，还是会用全局配置值！");
            this.ckbCancelDefault.UseVisualStyleBackColor = true;
            // 
            // ckbSkipFixNull
            // 
            this.ckbSkipFixNull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSkipFixNull.AutoSize = true;
            this.ckbSkipFixNull.Checked = true;
            this.ckbSkipFixNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSkipFixNull.Location = new System.Drawing.Point(291, 32);
            this.ckbSkipFixNull.Name = "ckbSkipFixNull";
            this.ckbSkipFixNull.Size = new System.Drawing.Size(72, 16);
            this.ckbSkipFixNull.TabIndex = 3;
            this.ckbSkipFixNull.Text = "忽略NULL";
            this.toolTip1.SetToolTip(this.ckbSkipFixNull, "不把固定值上的NULL当作默认值");
            this.ckbSkipFixNull.UseVisualStyleBackColor = true;
            // 
            // ckbUseRemark
            // 
            this.ckbUseRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbUseRemark.AutoSize = true;
            this.ckbUseRemark.Checked = true;
            this.ckbUseRemark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUseRemark.Location = new System.Drawing.Point(135, 32);
            this.ckbUseRemark.Name = "ckbUseRemark";
            this.ckbUseRemark.Size = new System.Drawing.Size(48, 16);
            this.ckbUseRemark.TabIndex = 3;
            this.ckbUseRemark.Text = "备注";
            this.toolTip1.SetToolTip(this.ckbUseRemark, "SQL中不包括备注信息");
            this.ckbUseRemark.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 179);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1087, 338);
            this.tabControl1.TabIndex = 41;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbColumn);
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(1079, 312);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.btnFindFront);
            this.grbColumn.Controls.Add(this.btnFindNext);
            this.grbColumn.Controls.Add(this.txbSearchColumn);
            this.grbColumn.Controls.Add(this.lblColumnInfo);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(3, 80);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(1073, 229);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // btnFindFront
            // 
            this.btnFindFront.Location = new System.Drawing.Point(330, -3);
            this.btnFindFront.Name = "btnFindFront";
            this.btnFindFront.Size = new System.Drawing.Size(55, 21);
            this.btnFindFront.TabIndex = 17;
            this.btnFindFront.Text = "往前找";
            this.btnFindFront.UseVisualStyleBackColor = true;
            this.btnFindFront.Click += new System.EventHandler(this.btnFindFront_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(271, -3);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(54, 21);
            this.btnFindNext.TabIndex = 17;
            this.btnFindNext.Text = "下一个";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // txbSearchColumn
            // 
            this.txbSearchColumn.Location = new System.Drawing.Point(79, -3);
            this.txbSearchColumn.Name = "txbSearchColumn";
            this.txbSearchColumn.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumn.TabIndex = 16;
            // 
            // lblColumnInfo
            // 
            this.lblColumnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColumnInfo.AutoSize = true;
            this.lblColumnInfo.ForeColor = System.Drawing.Color.Red;
            this.lblColumnInfo.Location = new System.Drawing.Point(395, 0);
            this.lblColumnInfo.Name = "lblColumnInfo";
            this.lblColumnInfo.Size = new System.Drawing.Size(53, 12);
            this.lblColumnInfo.TabIndex = 13;
            this.lblColumnInfo.Text = "提示信息";
            // 
            // dgvColList
            // 
            this.dgvColList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColList.Location = new System.Drawing.Point(3, 17);
            this.dgvColList.Name = "dgvColList";
            this.dgvColList.RowTemplate.Height = 23;
            this.dgvColList.Size = new System.Drawing.Size(1067, 209);
            this.dgvColList.TabIndex = 0;
            this.dgvColList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvColList_ColumnHeaderMouseDoubleClick);
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.dgvTableList);
            this.grbTable.Controls.Add(this.lblTableData);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbTable.Location = new System.Drawing.Point(3, 3);
            this.grbTable.Name = "grbTable";
            this.grbTable.Size = new System.Drawing.Size(1073, 77);
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
            this.dgvTableList.Size = new System.Drawing.Size(1067, 57);
            this.dgvTableList.TabIndex = 0;
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(305, 0);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(53, 12);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "提示信息";
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(1079, 312);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1073, 306);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1087, 78);
            this.uC_DbConnection1.TabIndex = 35;
            // 
            // FrmDBTGetTableQuerySql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 517);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbOrcNet);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTGetTableQuerySql";
            this.Text = "获取增删改查SQL";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbOrcNet.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.GroupBox grbOrcNet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txbTableShortName;
        private System.Windows.Forms.ComboBox cbbParaType;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.CheckBox ckbUseDefaultConfig;
        private System.Windows.Forms.TextBox txbParamPre;
        private System.Windows.Forms.Label lblParam;
        private System.Windows.Forms.TextBox txbDefineFormart;
        private System.Windows.Forms.Label lblDefineFormat;
        private System.Windows.Forms.CheckBox ckbUseRemark;
        private System.Windows.Forms.CheckBox ckbNewLine;
        private Label label1;
        private ComboBox cbbWordConvert;
        private CheckBox ckbCancelDefault;
        private CheckBox ckbSkipFixNull;
        private ToolTip toolTip1;
        private Button btnFindNext;
        private TextBox txbSearchColumn;
        private Button btnFindFront;
    }
}