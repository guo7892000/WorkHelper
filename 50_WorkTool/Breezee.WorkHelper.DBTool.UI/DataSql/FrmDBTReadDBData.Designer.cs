namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTReadDBData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTReadDBData));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.grbSqlNet = new System.Windows.Forms.GroupBox();
            this.tlpNetCon = new System.Windows.Forms.TableLayoutPanel();
            this.lblSqlWHere = new System.Windows.Forms.Label();
            this.rtbWhere = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbTargetDbType = new System.Windows.Forms.ComboBox();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.lblThree = new System.Windows.Forms.Label();
            this.cbbCommitType = new System.Windows.Forms.ComboBox();
            this.ckbAutoGetSql = new System.Windows.Forms.CheckBox();
            this.ckbMainKeyInsert = new System.Windows.Forms.CheckBox();
            this.ckbShowSaveFileDialog = new System.Windows.Forms.CheckBox();
            this.btnSaveOther = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.cmsGird = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAndEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrIn = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpData = new System.Windows.Forms.TabPage();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            this.grbSqlNet.SuspendLayout();
            this.tlpNetCon.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.cmsGird.SuspendLayout();
            this.tpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImport,
            this.tsbAutoSQL,
            this.tsbExport,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(879, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(68, 24);
            this.tsbImport.Text = "连接(&I)";
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
            // grbSqlNet
            // 
            this.grbSqlNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbSqlNet.Controls.Add(this.tlpNetCon);
            this.grbSqlNet.Controls.Add(this.lblInfo);
            this.grbSqlNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbSqlNet.Location = new System.Drawing.Point(0, 107);
            this.grbSqlNet.Name = "grbSqlNet";
            this.grbSqlNet.Size = new System.Drawing.Size(879, 153);
            this.grbSqlNet.TabIndex = 33;
            this.grbSqlNet.TabStop = false;
            this.grbSqlNet.Text = "选项配置";
            // 
            // tlpNetCon
            // 
            this.tlpNetCon.ColumnCount = 9;
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpNetCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpNetCon.Controls.Add(this.lblSqlWHere, 0, 3);
            this.tlpNetCon.Controls.Add(this.rtbWhere, 1, 3);
            this.tlpNetCon.Controls.Add(this.label7, 0, 2);
            this.tlpNetCon.Controls.Add(this.label1, 0, 0);
            this.tlpNetCon.Controls.Add(this.cbbTargetDbType, 1, 0);
            this.tlpNetCon.Controls.Add(this.cbbTableName, 1, 2);
            this.tlpNetCon.Controls.Add(this.ckbGetTableList, 2, 2);
            this.tlpNetCon.Controls.Add(this.lblThree, 2, 0);
            this.tlpNetCon.Controls.Add(this.cbbCommitType, 3, 0);
            this.tlpNetCon.Controls.Add(this.ckbAutoGetSql, 3, 2);
            this.tlpNetCon.Controls.Add(this.ckbMainKeyInsert, 4, 0);
            this.tlpNetCon.Controls.Add(this.ckbShowSaveFileDialog, 4, 2);
            this.tlpNetCon.Controls.Add(this.btnSaveOther, 5, 2);
            this.tlpNetCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNetCon.Location = new System.Drawing.Point(3, 17);
            this.tlpNetCon.Name = "tlpNetCon";
            this.tlpNetCon.RowCount = 4;
            this.tlpNetCon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNetCon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNetCon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNetCon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNetCon.Size = new System.Drawing.Size(873, 133);
            this.tlpNetCon.TabIndex = 4;
            // 
            // lblSqlWHere
            // 
            this.lblSqlWHere.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSqlWHere.AutoSize = true;
            this.lblSqlWHere.Location = new System.Drawing.Point(3, 86);
            this.lblSqlWHere.Name = "lblSqlWHere";
            this.lblSqlWHere.Size = new System.Drawing.Size(71, 12);
            this.lblSqlWHere.TabIndex = 1;
            this.lblSqlWHere.Text = "where条件：";
            // 
            // rtbWhere
            // 
            this.tlpNetCon.SetColumnSpan(this.rtbWhere, 8);
            this.rtbWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbWhere.Location = new System.Drawing.Point(86, 55);
            this.rtbWhere.Name = "rtbWhere";
            this.rtbWhere.Size = new System.Drawing.Size(784, 75);
            this.rtbWhere.TabIndex = 4;
            this.rtbWhere.Text = "";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(39, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "表名：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "目标数据库：";
            // 
            // cbbTargetDbType
            // 
            this.cbbTargetDbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTargetDbType.FormattingEnabled = true;
            this.cbbTargetDbType.Location = new System.Drawing.Point(86, 3);
            this.cbbTargetDbType.Name = "cbbTargetDbType";
            this.cbbTargetDbType.Size = new System.Drawing.Size(248, 20);
            this.cbbTargetDbType.TabIndex = 0;
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(86, 29);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(248, 20);
            this.cbbTableName.TabIndex = 2;
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(340, 31);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 10;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            // 
            // lblThree
            // 
            this.lblThree.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblThree.AutoSize = true;
            this.lblThree.ForeColor = System.Drawing.Color.Red;
            this.lblThree.Location = new System.Drawing.Point(359, 7);
            this.lblThree.Name = "lblThree";
            this.lblThree.Size = new System.Drawing.Size(65, 12);
            this.lblThree.TabIndex = 1;
            this.lblThree.Text = "提交方式：";
            // 
            // cbbCommitType
            // 
            this.cbbCommitType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbCommitType.FormattingEnabled = true;
            this.cbbCommitType.Location = new System.Drawing.Point(430, 3);
            this.cbbCommitType.Name = "cbbCommitType";
            this.cbbCommitType.Size = new System.Drawing.Size(134, 20);
            this.cbbCommitType.TabIndex = 1;
            // 
            // ckbAutoGetSql
            // 
            this.ckbAutoGetSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbAutoGetSql.AutoSize = true;
            this.ckbAutoGetSql.Location = new System.Drawing.Point(430, 31);
            this.ckbAutoGetSql.Name = "ckbAutoGetSql";
            this.ckbAutoGetSql.Size = new System.Drawing.Size(134, 16);
            this.ckbAutoGetSql.TabIndex = 11;
            this.ckbAutoGetSql.Text = "表变化自动生成";
            this.ckbAutoGetSql.UseVisualStyleBackColor = true;
            // 
            // ckbMainKeyInsert
            // 
            this.ckbMainKeyInsert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbMainKeyInsert.AutoSize = true;
            this.ckbMainKeyInsert.Enabled = false;
            this.ckbMainKeyInsert.Location = new System.Drawing.Point(570, 5);
            this.ckbMainKeyInsert.Name = "ckbMainKeyInsert";
            this.ckbMainKeyInsert.Size = new System.Drawing.Size(96, 16);
            this.ckbMainKeyInsert.TabIndex = 15;
            this.ckbMainKeyInsert.Text = "自增长列插入";
            this.ckbMainKeyInsert.UseVisualStyleBackColor = true;
            // 
            // ckbShowSaveFileDialog
            // 
            this.ckbShowSaveFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbShowSaveFileDialog.AutoSize = true;
            this.ckbShowSaveFileDialog.Location = new System.Drawing.Point(570, 31);
            this.ckbShowSaveFileDialog.Name = "ckbShowSaveFileDialog";
            this.ckbShowSaveFileDialog.Size = new System.Drawing.Size(96, 16);
            this.ckbShowSaveFileDialog.TabIndex = 13;
            this.ckbShowSaveFileDialog.Text = "保存文件";
            this.ckbShowSaveFileDialog.UseVisualStyleBackColor = true;
            // 
            // btnSaveOther
            // 
            this.btnSaveOther.Location = new System.Drawing.Point(671, 28);
            this.btnSaveOther.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveOther.Name = "btnSaveOther";
            this.btnSaveOther.Size = new System.Drawing.Size(55, 20);
            this.btnSaveOther.TabIndex = 16;
            this.btnSaveOther.Text = "另存为";
            this.btnSaveOther.UseVisualStyleBackColor = true;
            this.btnSaveOther.Click += new System.EventHandler(this.BtnSaveOther_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(177, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(53, 12);
            this.lblInfo.TabIndex = 28;
            this.lblInfo.Text = "提示信息";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpData);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 260);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(879, 298);
            this.tabControl1.TabIndex = 34;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(871, 272);
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
            this.grbTable.Size = new System.Drawing.Size(865, 266);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "表的列信息";
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
            this.dgvTableList.Size = new System.Drawing.Size(859, 246);
            this.dgvTableList.TabIndex = 0;
            // 
            // cmsGird
            // 
            this.cmsGird.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsGird.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAndEqual,
            this.tsmiAndLike,
            this.tsmiOrEqual,
            this.tsmiOrLike,
            this.tsmiAndIn,
            this.tsmiOrIn});
            this.cmsGird.Name = "contextMenuStrip1";
            this.cmsGird.Size = new System.Drawing.Size(127, 136);
            // 
            // tsmiAndEqual
            // 
            this.tsmiAndEqual.Name = "tsmiAndEqual";
            this.tsmiAndEqual.Size = new System.Drawing.Size(126, 22);
            this.tsmiAndEqual.Text = "And =";
            this.tsmiAndEqual.Click += new System.EventHandler(this.TsmiAndEqual_Click);
            // 
            // tsmiAndLike
            // 
            this.tsmiAndLike.Name = "tsmiAndLike";
            this.tsmiAndLike.Size = new System.Drawing.Size(126, 22);
            this.tsmiAndLike.Text = "And Like";
            this.tsmiAndLike.Click += new System.EventHandler(this.TsmiAndLike_Click);
            // 
            // tsmiOrEqual
            // 
            this.tsmiOrEqual.Name = "tsmiOrEqual";
            this.tsmiOrEqual.Size = new System.Drawing.Size(126, 22);
            this.tsmiOrEqual.Text = "Or =";
            this.tsmiOrEqual.Click += new System.EventHandler(this.TsmiOrEqual_Click);
            // 
            // tsmiOrLike
            // 
            this.tsmiOrLike.Name = "tsmiOrLike";
            this.tsmiOrLike.Size = new System.Drawing.Size(126, 22);
            this.tsmiOrLike.Text = "Or Like";
            this.tsmiOrLike.Click += new System.EventHandler(this.TsmiOrLike_Click);
            // 
            // tsmiAndIn
            // 
            this.tsmiAndIn.Name = "tsmiAndIn";
            this.tsmiAndIn.Size = new System.Drawing.Size(126, 22);
            this.tsmiAndIn.Text = "And In";
            this.tsmiAndIn.Click += new System.EventHandler(this.TsmiAndIn_Click);
            // 
            // tsmiOrIn
            // 
            this.tsmiOrIn.Name = "tsmiOrIn";
            this.tsmiOrIn.Size = new System.Drawing.Size(126, 22);
            this.tsmiOrIn.Text = "Or In";
            this.tsmiOrIn.Click += new System.EventHandler(this.TsmiOrIn_Click);
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
            // tpData
            // 
            this.tpData.Controls.Add(this.dgvData);
            this.tpData.Location = new System.Drawing.Point(4, 22);
            this.tpData.Margin = new System.Windows.Forms.Padding(2);
            this.tpData.Name = "tpData";
            this.tpData.Size = new System.Drawing.Size(871, 288);
            this.tpData.TabIndex = 2;
            this.tpData.Text = "数据清单";
            this.tpData.UseVisualStyleBackColor = true;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 27;
            this.dgvData.Size = new System.Drawing.Size(871, 288);
            this.dgvData.TabIndex = 0;
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(871, 288);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(865, 282);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(879, 80);
            this.uC_DbConnection1.TabIndex = 32;
            // 
            // FrmDBTReadDBData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 558);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbSqlNet);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTReadDBData";
            this.Text = "读取数据库表生成数据SQL";
            this.Load += new System.EventHandler(this.FrmReadSqlServerDBData_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbSqlNet.ResumeLayout(false);
            this.grbSqlNet.PerformLayout();
            this.tlpNetCon.ResumeLayout(false);
            this.tlpNetCon.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.cmsGird.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.GroupBox grbSqlNet;
        private System.Windows.Forms.TableLayoutPanel tlpNetCon;
        private System.Windows.Forms.Label lblSqlWHere;
        private System.Windows.Forms.RichTextBox rtbWhere;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbTargetDbType;
        private System.Windows.Forms.ComboBox cbbTableName;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.Label lblThree;
        private System.Windows.Forms.ComboBox cbbCommitType;
        private System.Windows.Forms.CheckBox ckbAutoGetSql;
        private System.Windows.Forms.CheckBox ckbMainKeyInsert;
        private System.Windows.Forms.CheckBox ckbShowSaveFileDialog;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.Label lblTableData;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.TabPage tpData;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnSaveOther;
        private System.Windows.Forms.ContextMenuStrip cmsGird;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndLike;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndEqual;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrEqual;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrLike;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndIn;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrIn;
    }
}