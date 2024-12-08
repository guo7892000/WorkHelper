namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTReadDataBaseString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTReadDataBaseString));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSqlWHere = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.rtbWhere = new System.Windows.Forms.RichTextBox();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
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
            this.tsmiAndEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrIn = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbOption = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbDataType = new System.Windows.Forms.ComboBox();
            this.btnGetDate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGenerate = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.grbOption.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.tsbAutoSQL,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(893, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(96, 24);
            this.tsbConnect.Text = "获取数据(&C)";
            this.tsbConnect.Click += new System.EventHandler(this.tsbConnect_Click);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(893, 95);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询数据的表名或SQL";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 447F));
            this.tableLayoutPanel1.Controls.Add(this.lblSqlWHere, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbTableName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbWhere, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbGetTableList, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(887, 75);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblSqlWHere
            // 
            this.lblSqlWHere.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSqlWHere.AutoSize = true;
            this.lblSqlWHere.Location = new System.Drawing.Point(3, 44);
            this.lblSqlWHere.Name = "lblSqlWHere";
            this.lblSqlWHere.Size = new System.Drawing.Size(71, 12);
            this.lblSqlWHere.TabIndex = 1;
            this.lblSqlWHere.Text = "where条件：";
            // 
            // lblTable
            // 
            this.lblTable.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(33, 7);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(41, 12);
            this.lblTable.TabIndex = 1;
            this.lblTable.Text = "表名：";
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(80, 3);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(246, 20);
            this.cbbTableName.TabIndex = 1;
            this.cbbTableName.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // rtbWhere
            // 
            this.rtbWhere.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.rtbWhere, 4);
            this.rtbWhere.Location = new System.Drawing.Point(80, 29);
            this.rtbWhere.Name = "rtbWhere";
            this.rtbWhere.Size = new System.Drawing.Size(804, 43);
            this.rtbWhere.TabIndex = 4;
            this.rtbWhere.Text = "";
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(332, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 2;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // ckbResultNewLine
            // 
            this.ckbResultNewLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbResultNewLine.AutoSize = true;
            this.ckbResultNewLine.Checked = true;
            this.ckbResultNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbResultNewLine.Location = new System.Drawing.Point(261, 7);
            this.ckbResultNewLine.Name = "ckbResultNewLine";
            this.ckbResultNewLine.Size = new System.Drawing.Size(72, 16);
            this.ckbResultNewLine.TabIndex = 6;
            this.ckbResultNewLine.Text = "结果换行";
            this.ckbResultNewLine.UseVisualStyleBackColor = true;
            // 
            // grbConSting
            // 
            this.grbConSting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbConSting.Controls.Add(this.label21);
            this.grbConSting.Controls.Add(this.rtbConString);
            this.grbConSting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbConSting.Location = new System.Drawing.Point(0, 0);
            this.grbConSting.Name = "grbConSting";
            this.grbConSting.Size = new System.Drawing.Size(893, 69);
            this.grbConSting.TabIndex = 38;
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
            this.rtbConString.Location = new System.Drawing.Point(3, 17);
            this.rtbConString.Name = "rtbConString";
            this.rtbConString.Size = new System.Drawing.Size(887, 49);
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
            this.tabControl1.Size = new System.Drawing.Size(893, 204);
            this.tabControl1.TabIndex = 39;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(885, 178);
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
            this.grbTable.Size = new System.Drawing.Size(879, 172);
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
            this.dgvTableList.Size = new System.Drawing.Size(873, 152);
            this.dgvTableList.TabIndex = 0;
            // 
            // cmsGird
            // 
            this.cmsGird.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsGird.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsert,
            this.tsmiAndEqual,
            this.tsmiAndLike,
            this.tsmiOrEqual,
            this.tsmiOrLike,
            this.tsmiAndIn,
            this.tsmiOrIn});
            this.cmsGird.Name = "contextMenuStrip1";
            this.cmsGird.Size = new System.Drawing.Size(127, 158);
            // 
            // tsmiInsert
            // 
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(126, 22);
            this.tsmiInsert.Text = "加入";
            this.tsmiInsert.Click += new System.EventHandler(this.TsmiInsert_Click);
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
            this.lblTableData.Location = new System.Drawing.Point(216, 2);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(365, 12);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "选中一个单元格后，右击选【加入】可将【#列名#】加入拼接字符中";
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(885, 178);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(879, 172);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(893, 87);
            this.uC_DbConnection1.TabIndex = 36;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 265);
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
            this.splitContainer1.Size = new System.Drawing.Size(893, 277);
            this.splitContainer1.SplitterDistance = 69;
            this.splitContainer1.TabIndex = 40;
            // 
            // grbOption
            // 
            this.grbOption.Controls.Add(this.tableLayoutPanel2);
            this.grbOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbOption.Location = new System.Drawing.Point(0, 114);
            this.grbOption.Name = "grbOption";
            this.grbOption.Size = new System.Drawing.Size(893, 56);
            this.grbOption.TabIndex = 41;
            this.grbOption.TabStop = false;
            this.grbOption.Text = "选项";
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
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbDataType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ckbResultNewLine, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGetDate, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGenerate, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(887, 33);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据类型：";
            // 
            // cbbDataType
            // 
            this.cbbDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbDataType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbDataType.FormattingEnabled = true;
            this.cbbDataType.Location = new System.Drawing.Point(74, 3);
            this.cbbDataType.Name = "cbbDataType";
            this.cbbDataType.Size = new System.Drawing.Size(181, 25);
            this.cbbDataType.TabIndex = 1;
            this.cbbDataType.SelectedIndexChanged += new System.EventHandler(this.cbbDataType_SelectedIndexChanged);
            // 
            // btnGetDate
            // 
            this.btnGetDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGetDate.Location = new System.Drawing.Point(339, 3);
            this.btnGetDate.Name = "btnGetDate";
            this.btnGetDate.Size = new System.Drawing.Size(75, 23);
            this.btnGetDate.TabIndex = 7;
            this.btnGetDate.Text = "获取数据";
            this.btnGetDate.UseVisualStyleBackColor = false;
            this.btnGetDate.Click += new System.EventHandler(this.btnGetDate_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGenerate.Location = new System.Drawing.Point(420, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(47, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // FrmDBTReadDataBaseString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 542);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grbOption);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTReadDataBaseString";
            this.Text = "读取数据库拼接字符串";
            this.Load += new System.EventHandler(this.FrmReadDataBaseString_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
            this.grbOption.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbWhere;
        private System.Windows.Forms.Label lblSqlWHere;
        private System.Windows.Forms.Label lblTable;
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
        private System.Windows.Forms.ComboBox cbbTableName;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.ContextMenuStrip cmsGird;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndEqual;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndLike;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrEqual;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrLike;
        private System.Windows.Forms.ToolStripMenuItem tsmiAndIn;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrIn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox ckbResultNewLine;
        private System.Windows.Forms.GroupBox grbOption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDataType;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnGetDate;
        private System.Windows.Forms.Button btnGenerate;
    }
}