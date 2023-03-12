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
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSqlWHere = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.rtbWhere = new System.Windows.Forms.RichTextBox();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.lblTableWhereInfo = new System.Windows.Forms.Label();
            this.grbConSting = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbConString = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.cmsGird = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.tsmiAndEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrLike = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAndIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrIn = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsbConnect.Size = new System.Drawing.Size(72, 24);
            this.tsbConnect.Text = "连接(&C)";
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
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(312, 7);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(53, 12);
            this.lblInfo.TabIndex = 31;
            this.lblInfo.Text = "提示信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Controls.Add(this.lblTableWhereInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 104);
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 447F));
            this.tableLayoutPanel1.Controls.Add(this.lblSqlWHere, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
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
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "表名：";
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
            this.tableLayoutPanel1.SetColumnSpan(this.rtbWhere, 2);
            this.rtbWhere.Location = new System.Drawing.Point(80, 29);
            this.rtbWhere.Name = "rtbWhere";
            this.rtbWhere.Size = new System.Drawing.Size(356, 43);
            this.rtbWhere.TabIndex = 4;
            this.rtbWhere.Text = "";
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(332, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(104, 16);
            this.ckbGetTableList.TabIndex = 2;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // lblTableWhereInfo
            // 
            this.lblTableWhereInfo.AutoSize = true;
            this.lblTableWhereInfo.ForeColor = System.Drawing.Color.Red;
            this.lblTableWhereInfo.Location = new System.Drawing.Point(222, 0);
            this.lblTableWhereInfo.Name = "lblTableWhereInfo";
            this.lblTableWhereInfo.Size = new System.Drawing.Size(29, 12);
            this.lblTableWhereInfo.TabIndex = 31;
            this.lblTableWhereInfo.Text = "说明";
            // 
            // grbConSting
            // 
            this.grbConSting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbConSting.Controls.Add(this.label21);
            this.grbConSting.Controls.Add(this.rtbConString);
            this.grbConSting.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbConSting.Location = new System.Drawing.Point(0, 199);
            this.grbConSting.Name = "grbConSting";
            this.grbConSting.Size = new System.Drawing.Size(893, 82);
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
            this.rtbConString.Size = new System.Drawing.Size(887, 62);
            this.rtbConString.TabIndex = 5;
            this.rtbConString.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 281);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(893, 261);
            this.tabControl1.TabIndex = 39;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpImport.Size = new System.Drawing.Size(885, 235);
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
            this.grbTable.Size = new System.Drawing.Size(879, 229);
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
            this.dgvTableList.Size = new System.Drawing.Size(873, 209);
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
            this.cmsGird.Size = new System.Drawing.Size(181, 180);
            // 
            // tsmiInsert
            // 
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(180, 22);
            this.tsmiInsert.Text = "加入";
            this.tsmiInsert.Click += new System.EventHandler(this.TsmiInsert_Click);
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
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpAutoSQL.Size = new System.Drawing.Size(885, 235);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(879, 229);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(893, 77);
            this.uC_DbConnection1.TabIndex = 36;
            // 
            // tsmiAndEqual
            // 
            this.tsmiAndEqual.Name = "tsmiAndEqual";
            this.tsmiAndEqual.Size = new System.Drawing.Size(180, 22);
            this.tsmiAndEqual.Text = "And =";
            this.tsmiAndEqual.Click += new System.EventHandler(this.TsmiAndEqual_Click);
            // 
            // tsmiAndLike
            // 
            this.tsmiAndLike.Name = "tsmiAndLike";
            this.tsmiAndLike.Size = new System.Drawing.Size(180, 22);
            this.tsmiAndLike.Text = "And Like";
            this.tsmiAndLike.Click += new System.EventHandler(this.TsmiAndLike_Click);
            // 
            // tsmiOrEqual
            // 
            this.tsmiOrEqual.Name = "tsmiOrEqual";
            this.tsmiOrEqual.Size = new System.Drawing.Size(180, 22);
            this.tsmiOrEqual.Text = "Or =";
            this.tsmiOrEqual.Click += new System.EventHandler(this.TsmiOrEqual_Click);
            // 
            // tsmiOrLike
            // 
            this.tsmiOrLike.Name = "tsmiOrLike";
            this.tsmiOrLike.Size = new System.Drawing.Size(180, 22);
            this.tsmiOrLike.Text = "Or Like";
            this.tsmiOrLike.Click += new System.EventHandler(this.TsmiOrLike_Click);
            // 
            // tsmiAndIn
            // 
            this.tsmiAndIn.Name = "tsmiAndIn";
            this.tsmiAndIn.Size = new System.Drawing.Size(180, 22);
            this.tsmiAndIn.Text = "And In";
            this.tsmiAndIn.Click += new System.EventHandler(this.TsmiAndIn_Click);
            // 
            // tsmiOrIn
            // 
            this.tsmiOrIn.Name = "tsmiOrIn";
            this.tsmiOrIn.Size = new System.Drawing.Size(180, 22);
            this.tsmiOrIn.Text = "Or In";
            this.tsmiOrIn.Click += new System.EventHandler(this.TsmiOrIn_Click);
            // 
            // FrmDBTReadDataBaseString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 542);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbConSting);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmDBTReadDataBaseString";
            this.Text = "读取数据库拼接字符串";
            this.Load += new System.EventHandler(this.FrmReadDataBaseString_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Label lblInfo;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbWhere;
        private System.Windows.Forms.Label lblSqlWHere;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.Label lblTableWhereInfo;
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
    }
}