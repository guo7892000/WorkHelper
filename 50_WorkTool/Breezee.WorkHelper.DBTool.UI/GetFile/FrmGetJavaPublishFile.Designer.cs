namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmGetJavaPublishFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetJavaPublishFile));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txbCodePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReadPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTargetPath = new System.Windows.Forms.Button();
            this.txbCopyToPath = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveReplaceTemplate = new System.Windows.Forms.Button();
            this.btnRemoveTemplate = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTemplateType = new System.Windows.Forms.ComboBox();
            this.btnGetFile = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txbReplaceTemplateName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClassPath = new System.Windows.Forms.Button();
            this.txbClassPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbCopyType = new System.Windows.Forms.ComboBox();
            this.btnGetChangeFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbExcludeRelateDir = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbIsPasteAppend = new System.Windows.Forms.CheckBox();
            this.lblReplaceInfo = new System.Windows.Forms.Label();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.cmsInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCodeClassRelConfig = new System.Windows.Forms.DataGridView();
            this.cmsCfg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCfgPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCfgClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSource = new System.Windows.Forms.TabPage();
            this.tpResult = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rtbExcludeRelateFile = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.cmsInput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeClassRelConfig)).BeginInit();
            this.cmsCfg.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSource.SuspendLayout();
            this.tpResult.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1123, 27);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(126, 24);
            this.tsbAutoSQL.Text = "复制Class文件(&A)";
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
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(315, 115);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(153, 21);
            this.dtpEnd.TabIndex = 12;
            // 
            // txbCodePath
            // 
            this.txbCodePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.txbCodePath, 4);
            this.txbCodePath.Location = new System.Drawing.Point(111, 36);
            this.txbCodePath.Multiline = true;
            this.txbCodePath.Name = "txbCodePath";
            this.txbCodePath.Size = new System.Drawing.Size(548, 19);
            this.txbCodePath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "代码目录：";
            // 
            // btnReadPath
            // 
            this.btnReadPath.Location = new System.Drawing.Point(86, 35);
            this.btnReadPath.Name = "btnReadPath";
            this.btnReadPath.Size = new System.Drawing.Size(19, 19);
            this.btnReadPath.TabIndex = 2;
            this.btnReadPath.Text = "...";
            this.btnReadPath.UseVisualStyleBackColor = true;
            this.btnReadPath.Click += new System.EventHandler(this.btnReadPath_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(245, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "至：";
            // 
            // dtpBegin
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.dtpBegin, 2);
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(86, 115);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(153, 21);
            this.dtpBegin.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "复制到目录：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "修改时间从：";
            // 
            // btnTargetPath
            // 
            this.btnTargetPath.Location = new System.Drawing.Point(86, 89);
            this.btnTargetPath.Name = "btnTargetPath";
            this.btnTargetPath.Size = new System.Drawing.Size(19, 19);
            this.btnTargetPath.TabIndex = 2;
            this.btnTargetPath.Text = "...";
            this.btnTargetPath.UseVisualStyleBackColor = true;
            this.btnTargetPath.Click += new System.EventHandler(this.btnTargetPath_Click);
            // 
            // txbCopyToPath
            // 
            this.txbCopyToPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.txbCopyToPath, 4);
            this.txbCopyToPath.Location = new System.Drawing.Point(111, 89);
            this.txbCopyToPath.Multiline = true;
            this.txbCopyToPath.Name = "txbCopyToPath";
            this.txbCopyToPath.Size = new System.Drawing.Size(548, 19);
            this.txbCopyToPath.TabIndex = 1;
            // 
            // btnSaveReplaceTemplate
            // 
            this.btnSaveReplaceTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSaveReplaceTemplate.Location = new System.Drawing.Point(665, 3);
            this.btnSaveReplaceTemplate.Name = "btnSaveReplaceTemplate";
            this.btnSaveReplaceTemplate.Size = new System.Drawing.Size(62, 26);
            this.btnSaveReplaceTemplate.TabIndex = 7;
            this.btnSaveReplaceTemplate.Text = "保存配置";
            this.toolTip1.SetToolTip(this.btnSaveReplaceTemplate, "保存模板");
            this.btnSaveReplaceTemplate.UseVisualStyleBackColor = false;
            this.btnSaveReplaceTemplate.Click += new System.EventHandler(this.btnSaveReplaceTemplate_Click);
            // 
            // btnRemoveTemplate
            // 
            this.btnRemoveTemplate.Location = new System.Drawing.Point(736, 3);
            this.btnRemoveTemplate.Name = "btnRemoveTemplate";
            this.btnRemoveTemplate.Size = new System.Drawing.Size(66, 25);
            this.btnRemoveTemplate.TabIndex = 8;
            this.btnRemoveTemplate.Text = "删除配置";
            this.toolTip1.SetToolTip(this.btnRemoveTemplate, "删除模板");
            this.btnRemoveTemplate.UseVisualStyleBackColor = true;
            this.btnRemoveTemplate.Click += new System.EventHandler(this.btnRemoveTemplate_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel7);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(0, 27);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1123, 162);
            this.groupBox7.TabIndex = 35;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "配置选择";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 11;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbbTemplateType, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnReadPath, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnTargetPath, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.txbCodePath, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnGetFile, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.label21, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.txbReplaceTemplateName, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnSaveReplaceTemplate, 6, 0);
            this.tableLayoutPanel7.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.btnClassPath, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.txbClassPath, 2, 2);
            this.tableLayoutPanel7.Controls.Add(this.txbCopyToPath, 2, 3);
            this.tableLayoutPanel7.Controls.Add(this.btnRemoveTemplate, 7, 0);
            this.tableLayoutPanel7.Controls.Add(this.label7, 6, 3);
            this.tableLayoutPanel7.Controls.Add(this.cbbCopyType, 7, 3);
            this.tableLayoutPanel7.Controls.Add(this.dtpBegin, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.label2, 3, 4);
            this.tableLayoutPanel7.Controls.Add(this.dtpEnd, 4, 4);
            this.tableLayoutPanel7.Controls.Add(this.btnGetChangeFile, 5, 4);
            this.tableLayoutPanel7.Controls.Add(this.label6, 6, 1);
            this.tableLayoutPanel7.Controls.Add(this.rtbExcludeRelateDir, 7, 1);
            this.tableLayoutPanel7.Controls.Add(this.label8, 6, 2);
            this.tableLayoutPanel7.Controls.Add(this.rtbExcludeRelateFile, 7, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1117, 139);
            this.tableLayoutPanel7.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "配置选择：";
            // 
            // cbbTemplateType
            // 
            this.cbbTemplateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.cbbTemplateType, 2);
            this.cbbTemplateType.FormattingEnabled = true;
            this.cbbTemplateType.Location = new System.Drawing.Point(86, 6);
            this.cbbTemplateType.Name = "cbbTemplateType";
            this.cbbTemplateType.Size = new System.Drawing.Size(153, 20);
            this.cbbTemplateType.TabIndex = 0;
            this.cbbTemplateType.SelectedIndexChanged += new System.EventHandler(this.cbbTemplateType_SelectedIndexChanged);
            // 
            // btnGetFile
            // 
            this.btnGetFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGetFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnGetFile.Location = new System.Drawing.Point(245, 3);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(64, 26);
            this.btnGetFile.TabIndex = 0;
            this.btnGetFile.Text = "复制文件";
            this.btnGetFile.UseVisualStyleBackColor = false;
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(403, 10);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 5;
            this.label21.Text = "配置名称：";
            // 
            // txbReplaceTemplateName
            // 
            this.txbReplaceTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReplaceTemplateName.Location = new System.Drawing.Point(474, 5);
            this.txbReplaceTemplateName.Name = "txbReplaceTemplateName";
            this.txbReplaceTemplateName.Size = new System.Drawing.Size(185, 21);
            this.txbReplaceTemplateName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "class目录：";
            // 
            // btnClassPath
            // 
            this.btnClassPath.Location = new System.Drawing.Point(86, 62);
            this.btnClassPath.Name = "btnClassPath";
            this.btnClassPath.Size = new System.Drawing.Size(19, 19);
            this.btnClassPath.TabIndex = 2;
            this.btnClassPath.Text = "...";
            this.btnClassPath.UseVisualStyleBackColor = true;
            this.btnClassPath.Click += new System.EventHandler(this.btnClassPath_Click);
            // 
            // txbClassPath
            // 
            this.txbClassPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.txbClassPath, 4);
            this.txbClassPath.Location = new System.Drawing.Point(111, 63);
            this.txbClassPath.Multiline = true;
            this.txbClassPath.Name = "txbClassPath";
            this.txbClassPath.Size = new System.Drawing.Size(548, 19);
            this.txbClassPath.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(665, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "覆盖类型：";
            // 
            // cbbCopyType
            // 
            this.cbbCopyType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.cbbCopyType, 2);
            this.cbbCopyType.FormattingEnabled = true;
            this.cbbCopyType.Location = new System.Drawing.Point(736, 89);
            this.cbbCopyType.Name = "cbbCopyType";
            this.cbbCopyType.Size = new System.Drawing.Size(121, 20);
            this.cbbCopyType.TabIndex = 15;
            // 
            // btnGetChangeFile
            // 
            this.btnGetChangeFile.Location = new System.Drawing.Point(474, 115);
            this.btnGetChangeFile.Name = "btnGetChangeFile";
            this.btnGetChangeFile.Size = new System.Drawing.Size(118, 23);
            this.btnGetChangeFile.TabIndex = 16;
            this.btnGetChangeFile.Text = "获取代码变更清单";
            this.btnGetChangeFile.UseVisualStyleBackColor = true;
            this.btnGetChangeFile.Click += new System.EventHandler(this.btnGetChangeFile_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(665, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "排除目录：";
            // 
            // rtbExcludeRelateDir
            // 
            this.rtbExcludeRelateDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.rtbExcludeRelateDir, 3);
            this.rtbExcludeRelateDir.Location = new System.Drawing.Point(736, 35);
            this.rtbExcludeRelateDir.Name = "rtbExcludeRelateDir";
            this.rtbExcludeRelateDir.Size = new System.Drawing.Size(249, 21);
            this.rtbExcludeRelateDir.TabIndex = 17;
            this.rtbExcludeRelateDir.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1111, 354);
            this.splitContainer1.SplitterDistance = 659;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 36;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbIsPasteAppend);
            this.groupBox4.Controls.Add(this.lblReplaceInfo);
            this.groupBox4.Controls.Add(this.dgvInput);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.Red;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(659, 354);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "变化文件录入";
            // 
            // ckbIsPasteAppend
            // 
            this.ckbIsPasteAppend.AutoSize = true;
            this.ckbIsPasteAppend.Checked = true;
            this.ckbIsPasteAppend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbIsPasteAppend.Location = new System.Drawing.Point(120, 0);
            this.ckbIsPasteAppend.Name = "ckbIsPasteAppend";
            this.ckbIsPasteAppend.Size = new System.Drawing.Size(72, 16);
            this.ckbIsPasteAppend.TabIndex = 38;
            this.ckbIsPasteAppend.Text = "粘贴累加";
            this.ckbIsPasteAppend.UseVisualStyleBackColor = true;
            // 
            // lblReplaceInfo
            // 
            this.lblReplaceInfo.AutoSize = true;
            this.lblReplaceInfo.ForeColor = System.Drawing.Color.Red;
            this.lblReplaceInfo.Location = new System.Drawing.Point(257, 2);
            this.lblReplaceInfo.Name = "lblReplaceInfo";
            this.lblReplaceInfo.Size = new System.Drawing.Size(83, 12);
            this.lblReplaceInfo.TabIndex = 37;
            this.lblReplaceInfo.Text = "从src目录开始";
            // 
            // dgvInput
            // 
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.ContextMenuStrip = this.cmsInput;
            this.dgvInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInput.Location = new System.Drawing.Point(3, 17);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowHeadersWidth = 82;
            this.dgvInput.RowTemplate.Height = 23;
            this.dgvInput.Size = new System.Drawing.Size(653, 334);
            this.dgvInput.TabIndex = 0;
            this.dgvInput.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInput_ColumnHeaderMouseDoubleClick);
            this.dgvInput.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvInput_DataError);
            this.dgvInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvInput_KeyDown);
            // 
            // cmsInput
            // 
            this.cmsInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPaste,
            this.tsmiClear});
            this.cmsInput.Name = "contextMenuStrip1";
            this.cmsInput.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(100, 22);
            this.tsmiPaste.Text = "粘贴";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(100, 22);
            this.tsmiClear.Text = "清空";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCodeClassRelConfig);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 354);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "代码与class目录对照关系配置";
            // 
            // dgvCodeClassRelConfig
            // 
            this.dgvCodeClassRelConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeClassRelConfig.ContextMenuStrip = this.cmsCfg;
            this.dgvCodeClassRelConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCodeClassRelConfig.Location = new System.Drawing.Point(3, 17);
            this.dgvCodeClassRelConfig.Name = "dgvCodeClassRelConfig";
            this.dgvCodeClassRelConfig.RowHeadersWidth = 82;
            this.dgvCodeClassRelConfig.RowTemplate.Height = 23;
            this.dgvCodeClassRelConfig.Size = new System.Drawing.Size(444, 334);
            this.dgvCodeClassRelConfig.TabIndex = 0;
            this.dgvCodeClassRelConfig.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCodeClassRelConfig_ColumnHeaderMouseDoubleClick);
            this.dgvCodeClassRelConfig.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCodeClassRelConfig_DataError);
            this.dgvCodeClassRelConfig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCodeClassRelConfig_KeyDown);
            // 
            // cmsCfg
            // 
            this.cmsCfg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCfgPaste,
            this.tsmiCfgClear});
            this.cmsCfg.Name = "contextMenuStrip2";
            this.cmsCfg.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiCfgPaste
            // 
            this.tsmiCfgPaste.Name = "tsmiCfgPaste";
            this.tsmiCfgPaste.Size = new System.Drawing.Size(100, 22);
            this.tsmiCfgPaste.Text = "粘贴";
            this.tsmiCfgPaste.Click += new System.EventHandler(this.tsmiCfgPaste_Click);
            // 
            // tsmiCfgClear
            // 
            this.tsmiCfgClear.Name = "tsmiCfgClear";
            this.tsmiCfgClear.Size = new System.Drawing.Size(100, 22);
            this.tsmiCfgClear.Text = "清空";
            this.tsmiCfgClear.Click += new System.EventHandler(this.tsmiCfgClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSource);
            this.tabControl1.Controls.Add(this.tpResult);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 189);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1123, 384);
            this.tabControl1.TabIndex = 37;
            // 
            // tpSource
            // 
            this.tpSource.Controls.Add(this.splitContainer1);
            this.tpSource.Location = new System.Drawing.Point(4, 22);
            this.tpSource.Margin = new System.Windows.Forms.Padding(2);
            this.tpSource.Name = "tpSource";
            this.tpSource.Padding = new System.Windows.Forms.Padding(2);
            this.tpSource.Size = new System.Drawing.Size(1115, 358);
            this.tpSource.TabIndex = 0;
            this.tpSource.Text = "变更的源文件";
            this.tpSource.UseVisualStyleBackColor = true;
            // 
            // tpResult
            // 
            this.tpResult.Controls.Add(this.groupBox3);
            this.tpResult.Location = new System.Drawing.Point(4, 22);
            this.tpResult.Margin = new System.Windows.Forms.Padding(2);
            this.tpResult.Name = "tpResult";
            this.tpResult.Padding = new System.Windows.Forms.Padding(2);
            this.tpResult.Size = new System.Drawing.Size(1115, 358);
            this.tpResult.TabIndex = 1;
            this.tpResult.Text = "生成结果";
            this.tpResult.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1111, 354);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成文件信息";
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(3, 17);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(1105, 334);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(665, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "排除文件：";
            // 
            // rtbExcludeRelateFile
            // 
            this.rtbExcludeRelateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.SetColumnSpan(this.rtbExcludeRelateFile, 3);
            this.rtbExcludeRelateFile.Location = new System.Drawing.Point(736, 62);
            this.rtbExcludeRelateFile.Name = "rtbExcludeRelateFile";
            this.rtbExcludeRelateFile.Size = new System.Drawing.Size(249, 21);
            this.rtbExcludeRelateFile.TabIndex = 17;
            this.rtbExcludeRelateFile.Text = "";
            // 
            // FrmGetJavaPublishFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmGetJavaPublishFile";
            this.Text = "获取Java发布文件";
            this.Load += new System.EventHandler(this.FrmDirectoryFileString_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.cmsInput.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeClassRelConfig)).EndInit();
            this.cmsCfg.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpSource.ResumeLayout(false);
            this.tpResult.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbCodePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReadPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTargetPath;
        private System.Windows.Forms.TextBox txbCopyToPath;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnGetFile;
        private System.Windows.Forms.ComboBox cbbTemplateType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txbReplaceTemplateName;
        private System.Windows.Forms.Button btnSaveReplaceTemplate;
        private System.Windows.Forms.Button btnRemoveTemplate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSource;
        private System.Windows.Forms.TabPage tpResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbString;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCodeClassRelConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClassPath;
        private System.Windows.Forms.TextBox txbClassPath;
        private System.Windows.Forms.Label lblReplaceInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbCopyType;
        private System.Windows.Forms.CheckBox ckbIsPasteAppend;
        private System.Windows.Forms.ContextMenuStrip cmsInput;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ContextMenuStrip cmsCfg;
        private System.Windows.Forms.ToolStripMenuItem tsmiCfgClear;
        private System.Windows.Forms.ToolStripMenuItem tsmiCfgPaste;
        private System.Windows.Forms.Button btnGetChangeFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbExcludeRelateDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtbExcludeRelateFile;
    }
}