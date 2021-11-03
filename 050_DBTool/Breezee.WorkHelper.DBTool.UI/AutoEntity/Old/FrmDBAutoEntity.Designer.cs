namespace Breezee.WorkHelper.DBTool.UI
{
	partial class FrmDBAutoEntity
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBAutoEntity));
            this.ctxmBuilt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFtable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStable = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgEntity = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAccess = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtClassEndString = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbEntityType = new System.Windows.Forms.ComboBox();
            this.txtClassPreString = new System.Windows.Forms.TextBox();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvEntityInfo = new System.Windows.Forms.DataGridView();
            this.nameSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tvDataBaseInfo = new System.Windows.Forms.TreeView();
            this.tpgTemplate = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbTable = new System.Windows.Forms.RichTextBox();
            this.rtbColumn = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbParamList = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLinkServer = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.ctxmBuilt.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgEntity.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityInfo)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tpgTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxmBuilt
            // 
            this.ctxmBuilt.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxmBuilt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFtable,
            this.tsmiStable});
            this.ctxmBuilt.Name = "ctxmBuilt";
            this.ctxmBuilt.Size = new System.Drawing.Size(139, 52);
            // 
            // tsmiFtable
            // 
            this.tsmiFtable.Name = "tsmiFtable";
            this.tsmiFtable.Size = new System.Drawing.Size(138, 24);
            this.tsmiFtable.Text = "全部生成";
            this.tsmiFtable.Click += new System.EventHandler(this.tsmiFtable_Click);
            // 
            // tsmiStable
            // 
            this.tsmiStable.Name = "tsmiStable";
            this.tsmiStable.Size = new System.Drawing.Size(138, 24);
            this.tsmiStable.Text = "单表生成";
            this.tsmiStable.Click += new System.EventHandler(this.tsmiStable_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgEntity);
            this.tabControl1.Controls.Add(this.tpgTemplate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1649, 672);
            this.tabControl1.TabIndex = 15;
            // 
            // tpgEntity
            // 
            this.tpgEntity.Controls.Add(this.groupBox2);
            this.tpgEntity.Controls.Add(this.groupBox4);
            this.tpgEntity.Controls.Add(this.uC_DbConnection1);
            this.tpgEntity.Controls.Add(this.groupBox5);
            this.tpgEntity.Controls.Add(this.groupBox3);
            this.tpgEntity.Location = new System.Drawing.Point(4, 25);
            this.tpgEntity.Margin = new System.Windows.Forms.Padding(4);
            this.tpgEntity.Name = "tpgEntity";
            this.tpgEntity.Padding = new System.Windows.Forms.Padding(4);
            this.tpgEntity.Size = new System.Drawing.Size(1641, 643);
            this.tpgEntity.TabIndex = 0;
            this.tpgEntity.Text = "生成实体文件";
            this.tpgEntity.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvTableInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(388, 229);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1249, 285);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表信息";
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvTableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableInfo.Location = new System.Drawing.Point(4, 22);
            this.dgvTableInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.RowTemplate.Height = 23;
            this.dgvTableInfo.Size = new System.Drawing.Size(1241, 259);
            this.dgvTableInfo.TabIndex = 6;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "fieldId";
            this.Column1.HeaderText = "字段编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "dbName";
            this.Column2.HeaderText = "数据库或所有者名称";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "tableName";
            this.Column3.HeaderText = "表名";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "fieldName";
            this.Column4.HeaderText = "字段名称";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "fieldType";
            this.Column5.HeaderText = "字段类型";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "typeSize";
            this.Column6.HeaderText = "最大字节数";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "numericPrecision";
            this.Column7.HeaderText = "近视精度";
            this.Column7.Name = "Column7";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(388, 98);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1249, 131);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "生成的类信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNameSpace, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAccess, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnPath, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtClassEndString, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbEntityType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtClassPreString, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1241, 110);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "保存路径：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtNameSpace, 3);
            this.txtNameSpace.Location = new System.Drawing.Point(403, 74);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(671, 25);
            this.txtNameSpace.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 79);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "命名空间：";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 79);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "字段修饰符：";
            // 
            // txtAccess
            // 
            this.txtAccess.Location = new System.Drawing.Point(109, 74);
            this.txtAccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccess.Name = "txtAccess";
            this.txtAccess.Size = new System.Drawing.Size(196, 25);
            this.txtAccess.TabIndex = 3;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(745, 4);
            this.btnPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(25, 29);
            this.btnPath.TabIndex = 6;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtPath, 4);
            this.txtPath.Location = new System.Drawing.Point(109, 6);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(628, 25);
            this.txtPath.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(655, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "类名后缀：";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(313, 46);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "类名前缀：";
            // 
            // txtClassEndString
            // 
            this.txtClassEndString.Location = new System.Drawing.Point(745, 41);
            this.txtClassEndString.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassEndString.Name = "txtClassEndString";
            this.txtClassEndString.Size = new System.Drawing.Size(188, 25);
            this.txtClassEndString.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 46);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "实体类型：";
            // 
            // cbbEntityType
            // 
            this.cbbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEntityType.FormattingEnabled = true;
            this.cbbEntityType.Location = new System.Drawing.Point(109, 41);
            this.cbbEntityType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEntityType.Name = "cbbEntityType";
            this.cbbEntityType.Size = new System.Drawing.Size(196, 23);
            this.cbbEntityType.TabIndex = 2;
            // 
            // txtClassPreString
            // 
            this.txtClassPreString.Location = new System.Drawing.Point(403, 41);
            this.txtClassPreString.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassPreString.Name = "txtClassPreString";
            this.txtClassPreString.Size = new System.Drawing.Size(188, 25);
            this.txtClassPreString.TabIndex = 3;
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(388, 4);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(5);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1249, 94);
            this.uC_DbConnection1.TabIndex = 20;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvEntityInfo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(388, 514);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(1249, 125);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "生成的实体文件路径";
            // 
            // dgvEntityInfo
            // 
            this.dgvEntityInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntityInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameSpace,
            this.accessType,
            this.entityName,
            this.path});
            this.dgvEntityInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntityInfo.Location = new System.Drawing.Point(4, 22);
            this.dgvEntityInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEntityInfo.Name = "dgvEntityInfo";
            this.dgvEntityInfo.RowTemplate.Height = 23;
            this.dgvEntityInfo.Size = new System.Drawing.Size(1241, 99);
            this.dgvEntityInfo.TabIndex = 6;
            // 
            // nameSpace
            // 
            this.nameSpace.DataPropertyName = "nameSpace";
            this.nameSpace.HeaderText = "命名空间";
            this.nameSpace.Name = "nameSpace";
            // 
            // accessType
            // 
            this.accessType.DataPropertyName = "accessType";
            this.accessType.HeaderText = "类修饰符";
            this.accessType.Name = "accessType";
            // 
            // entityName
            // 
            this.entityName.DataPropertyName = "entityName";
            this.entityName.HeaderText = "实体名称";
            this.entityName.Name = "entityName";
            // 
            // path
            // 
            this.path.DataPropertyName = "path";
            this.path.HeaderText = "存储路径";
            this.path.Name = "path";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvDataBaseInfo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(384, 635);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库清单";
            // 
            // tvDataBaseInfo
            // 
            this.tvDataBaseInfo.ContextMenuStrip = this.ctxmBuilt;
            this.tvDataBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDataBaseInfo.Location = new System.Drawing.Point(4, 22);
            this.tvDataBaseInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tvDataBaseInfo.Name = "tvDataBaseInfo";
            this.tvDataBaseInfo.Size = new System.Drawing.Size(376, 609);
            this.tvDataBaseInfo.TabIndex = 5;
            this.tvDataBaseInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDataBaseInfo_AfterSelect);
            this.tvDataBaseInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDataBaseInfo_NodeMouseClick);
            this.tvDataBaseInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvDataBaseInfo_MouseUp);
            // 
            // tpgTemplate
            // 
            this.tpgTemplate.Controls.Add(this.splitContainer1);
            this.tpgTemplate.Controls.Add(this.groupBox1);
            this.tpgTemplate.Location = new System.Drawing.Point(4, 25);
            this.tpgTemplate.Name = "tpgTemplate";
            this.tpgTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tpgTemplate.Size = new System.Drawing.Size(1641, 643);
            this.tpgTemplate.TabIndex = 1;
            this.tpgTemplate.Text = "实体模板";
            this.tpgTemplate.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(483, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbColumn);
            this.splitContainer1.Size = new System.Drawing.Size(1155, 637);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 1;
            // 
            // rtbTable
            // 
            this.rtbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTable.Location = new System.Drawing.Point(0, 0);
            this.rtbTable.Name = "rtbTable";
            this.rtbTable.Size = new System.Drawing.Size(1155, 392);
            this.rtbTable.TabIndex = 1;
            this.rtbTable.Text = "";
            // 
            // rtbColumn
            // 
            this.rtbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbColumn.Location = new System.Drawing.Point(0, 0);
            this.rtbColumn.Name = "rtbColumn";
            this.rtbColumn.Size = new System.Drawing.Size(1155, 241);
            this.rtbColumn.TabIndex = 1;
            this.rtbColumn.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbParamList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 637);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数说明";
            // 
            // rtbParamList
            // 
            this.rtbParamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbParamList.Location = new System.Drawing.Point(3, 21);
            this.rtbParamList.Name = "rtbParamList";
            this.rtbParamList.Size = new System.Drawing.Size(474, 613);
            this.rtbParamList.TabIndex = 0;
            this.rtbParamList.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLinkServer,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1649, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLinkServer
            // 
            this.btnLinkServer.Image = ((System.Drawing.Image)(resources.GetObject("btnLinkServer.Image")));
            this.btnLinkServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLinkServer.Name = "btnLinkServer";
            this.btnLinkServer.Size = new System.Drawing.Size(126, 24);
            this.btnLinkServer.Text = "连接服务器(&L)";
            this.btnLinkServer.Click += new System.EventHandler(this.btnLinkServer_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 24);
            this.btnExit.Text = "退出(&X)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmDBAutoEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1649, 699);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDBAutoEntity";
            this.Text = "生成实体工具";
            this.Load += new System.EventHandler(this.FrmAutoEntity_Load);
            this.ctxmBuilt.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgEntity.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityInfo)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tpgTemplate.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLinkServer;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ContextMenuStrip ctxmBuilt;
        private System.Windows.Forms.ToolStripMenuItem tsmiFtable;
        private System.Windows.Forms.ToolStripMenuItem tsmiStable;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgEntity;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvEntityInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView tvDataBaseInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAccess;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.TextBox txtClassPreString;
        private System.Windows.Forms.TextBox txtClassEndString;
        private System.Windows.Forms.ComboBox cbbEntityType;
        private System.Windows.Forms.Label label5;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tpgTemplate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbParamList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbTable;
        private System.Windows.Forms.RichTextBox rtbColumn;
    }
}