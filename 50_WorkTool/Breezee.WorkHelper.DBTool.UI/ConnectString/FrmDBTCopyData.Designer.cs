using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTCopyData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTCopyData));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbDbType = new System.Windows.Forms.ComboBox();
            this.lblDbType = new System.Windows.Forms.Label();
            this.cbbSqlType = new System.Windows.Forms.ComboBox();
            this.lblWordConvert = new System.Windows.Forms.Label();
            this.cbbWordConvert = new System.Windows.Forms.ComboBox();
            this.ckbAutoColumnName = new System.Windows.Forms.CheckBox();
            this.ckbTrim = new System.Windows.Forms.CheckBox();
            this.ckbResultNewLine = new System.Windows.Forms.CheckBox();
            this.grbConSting = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbConString = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.cmsGird = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnRemoveTemplate = new System.Windows.Forms.Button();
            this.btnSaveReplaceTemplate = new System.Windows.Forms.Button();
            this.txbReplaceTemplateName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbTemplateType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grbConSting.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.cmsGird.SuspendLayout();
            this.tpAutoSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(926, 27);
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
            this.groupBox1.Size = new System.Drawing.Size(926, 52);
            this.groupBox1.TabIndex = 25;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbDbType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDbType, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbSqlType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWordConvert, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbWordConvert, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbAutoColumnName, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbTrim, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbResultNewLine, 8, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(920, 32);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "拼接类型：";
            // 
            // cbbDbType
            // 
            this.cbbDbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbDbType.FormattingEnabled = true;
            this.cbbDbType.Location = new System.Drawing.Point(305, 3);
            this.cbbDbType.Name = "cbbDbType";
            this.cbbDbType.Size = new System.Drawing.Size(142, 20);
            this.cbbDbType.TabIndex = 3;
            // 
            // lblDbType
            // 
            this.lblDbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDbType.AutoSize = true;
            this.lblDbType.ForeColor = System.Drawing.Color.Red;
            this.lblDbType.Location = new System.Drawing.Point(222, 7);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(77, 12);
            this.lblDbType.TabIndex = 3;
            this.lblDbType.Text = "数据库类型：";
            // 
            // cbbSqlType
            // 
            this.cbbSqlType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbSqlType.FormattingEnabled = true;
            this.cbbSqlType.Location = new System.Drawing.Point(74, 3);
            this.cbbSqlType.Name = "cbbSqlType";
            this.cbbSqlType.Size = new System.Drawing.Size(142, 20);
            this.cbbSqlType.TabIndex = 2;
            this.cbbSqlType.SelectedIndexChanged += new System.EventHandler(this.cbbSqlType_SelectedIndexChanged);
            // 
            // lblWordConvert
            // 
            this.lblWordConvert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWordConvert.AutoSize = true;
            this.lblWordConvert.ForeColor = System.Drawing.Color.Red;
            this.lblWordConvert.Location = new System.Drawing.Point(453, 7);
            this.lblWordConvert.Name = "lblWordConvert";
            this.lblWordConvert.Size = new System.Drawing.Size(65, 12);
            this.lblWordConvert.TabIndex = 3;
            this.lblWordConvert.Text = "字符转换：";
            // 
            // cbbWordConvert
            // 
            this.cbbWordConvert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbWordConvert.FormattingEnabled = true;
            this.cbbWordConvert.Location = new System.Drawing.Point(524, 3);
            this.cbbWordConvert.Name = "cbbWordConvert";
            this.cbbWordConvert.Size = new System.Drawing.Size(106, 20);
            this.cbbWordConvert.TabIndex = 2;
            this.cbbWordConvert.SelectedIndexChanged += new System.EventHandler(this.cbbSqlType_SelectedIndexChanged);
            // 
            // ckbAutoColumnName
            // 
            this.ckbAutoColumnName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAutoColumnName.AutoSize = true;
            this.ckbAutoColumnName.Location = new System.Drawing.Point(635, 5);
            this.ckbAutoColumnName.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAutoColumnName.Name = "ckbAutoColumnName";
            this.ckbAutoColumnName.Size = new System.Drawing.Size(96, 16);
            this.ckbAutoColumnName.TabIndex = 4;
            this.ckbAutoColumnName.Text = "自动字母列名";
            this.ckbAutoColumnName.UseVisualStyleBackColor = true;
            // 
            // ckbTrim
            // 
            this.ckbTrim.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbTrim.AutoSize = true;
            this.ckbTrim.Checked = true;
            this.ckbTrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTrim.Location = new System.Drawing.Point(735, 5);
            this.ckbTrim.Margin = new System.Windows.Forms.Padding(2);
            this.ckbTrim.Name = "ckbTrim";
            this.ckbTrim.Size = new System.Drawing.Size(96, 16);
            this.ckbTrim.TabIndex = 4;
            this.ckbTrim.Text = "去掉前后空格";
            this.ckbTrim.UseVisualStyleBackColor = true;
            // 
            // ckbResultNewLine
            // 
            this.ckbResultNewLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbResultNewLine.AutoSize = true;
            this.ckbResultNewLine.Checked = true;
            this.ckbResultNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbResultNewLine.Location = new System.Drawing.Point(836, 5);
            this.ckbResultNewLine.Name = "ckbResultNewLine";
            this.ckbResultNewLine.Size = new System.Drawing.Size(72, 16);
            this.ckbResultNewLine.TabIndex = 5;
            this.ckbResultNewLine.Text = "结果换行";
            this.ckbResultNewLine.UseVisualStyleBackColor = true;
            // 
            // grbConSting
            // 
            this.grbConSting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbConSting.Controls.Add(this.rtbConString);
            this.grbConSting.Controls.Add(this.tableLayoutPanel7);
            this.grbConSting.Controls.Add(this.label21);
            this.grbConSting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbConSting.Location = new System.Drawing.Point(0, 0);
            this.grbConSting.Name = "grbConSting";
            this.grbConSting.Size = new System.Drawing.Size(926, 133);
            this.grbConSting.TabIndex = 26;
            this.grbConSting.TabStop = false;
            this.grbConSting.Text = "拼接字符";
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
            this.label21.Size = new System.Drawing.Size(125, 12);
            this.label21.TabIndex = 13;
            this.label21.Text = "替换字符格式：#列名#";
            // 
            // rtbConString
            // 
            this.rtbConString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConString.Location = new System.Drawing.Point(3, 49);
            this.rtbConString.Name = "rtbConString";
            this.rtbConString.Size = new System.Drawing.Size(920, 81);
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
            this.tabControl1.Size = new System.Drawing.Size(926, 307);
            this.tabControl1.TabIndex = 27;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(918, 281);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.dgvTableList);
            this.grbTable.Controls.Add(this.lblTableData);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTable.Location = new System.Drawing.Point(3, 3);
            this.grbTable.Name = "grbTable";
            this.grbTable.Size = new System.Drawing.Size(912, 275);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "粘贴的数据列表";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.ContextMenuStrip = this.cmsGird;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTableList.Location = new System.Drawing.Point(3, 17);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(906, 255);
            this.dgvTableList.TabIndex = 0;
            this.dgvTableList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTableList_KeyDown);
            // 
            // cmsGird
            // 
            this.cmsGird.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsGird.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsert,
            this.tsmiClear});
            this.cmsGird.Name = "contextMenuStrip1";
            this.cmsGird.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiInsert
            // 
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(100, 22);
            this.tsmiInsert.Text = "加入";
            this.tsmiInsert.Click += new System.EventHandler(this.TsmiInsert_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(100, 22);
            this.tsmiClear.Text = "清空";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click_1);
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
            this.tpAutoSQL.Size = new System.Drawing.Size(918, 321);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(912, 315);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 79);
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
            this.splitContainer1.Size = new System.Drawing.Size(926, 444);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.TabIndex = 28;
            // 
            // btnRemoveTemplate
            // 
            this.btnRemoveTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRemoveTemplate.Location = new System.Drawing.Point(505, 3);
            this.btnRemoveTemplate.Name = "btnRemoveTemplate";
            this.btnRemoveTemplate.Size = new System.Drawing.Size(46, 20);
            this.btnRemoveTemplate.TabIndex = 8;
            this.btnRemoveTemplate.Text = "删除";
            this.btnRemoveTemplate.UseVisualStyleBackColor = true;
            this.btnRemoveTemplate.Click += new System.EventHandler(this.btnRemoveTemplate_Click);
            // 
            // btnSaveReplaceTemplate
            // 
            this.btnSaveReplaceTemplate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaveReplaceTemplate.Location = new System.Drawing.Point(453, 3);
            this.btnSaveReplaceTemplate.Name = "btnSaveReplaceTemplate";
            this.btnSaveReplaceTemplate.Size = new System.Drawing.Size(46, 20);
            this.btnSaveReplaceTemplate.TabIndex = 7;
            this.btnSaveReplaceTemplate.Text = "保存";
            this.btnSaveReplaceTemplate.UseVisualStyleBackColor = true;
            this.btnSaveReplaceTemplate.Click += new System.EventHandler(this.btnSaveReplaceTemplate_Click);
            // 
            // txbReplaceTemplateName
            // 
            this.txbReplaceTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReplaceTemplateName.Location = new System.Drawing.Point(299, 3);
            this.txbReplaceTemplateName.Name = "txbReplaceTemplateName";
            this.txbReplaceTemplateName.Size = new System.Drawing.Size(148, 21);
            this.txbReplaceTemplateName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "模板名称：";
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
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 7;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbbTemplateType, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.txbReplaceTemplateName, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnSaveReplaceTemplate, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnRemoveTemplate, 5, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(920, 32);
            this.tableLayoutPanel7.TabIndex = 7;
            // 
            // FrmDBTCopyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 523);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTCopyData";
            this.Text = "复制数据生成拼接字符";
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
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.cmsGird.ResumeLayout(false);
            this.tpAutoSQL.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbSqlType;
        private System.Windows.Forms.GroupBox grbConSting;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox rtbConString;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.Label lblTableData;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label lblDbType;
        private System.Windows.Forms.ComboBox cbbDbType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox ckbAutoColumnName;
        private System.Windows.Forms.ContextMenuStrip cmsGird;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsert;
        private System.Windows.Forms.CheckBox ckbTrim;
        private Label lblWordConvert;
        private ComboBox cbbWordConvert;
        private ToolStripMenuItem tsmiClear;
        private SplitContainer splitContainer1;
        private CheckBox ckbResultNewLine;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label14;
        private ComboBox cbbTemplateType;
        private Label label2;
        private TextBox txbReplaceTemplateName;
        private Button btnSaveReplaceTemplate;
        private Button btnRemoveTemplate;
    }
}