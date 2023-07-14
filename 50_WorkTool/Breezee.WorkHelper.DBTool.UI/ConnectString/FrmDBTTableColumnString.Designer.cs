namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTTableColumnString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTTableColumnString));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.grbOrcNet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.cbbModule = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbConSting = new System.Windows.Forms.GroupBox();
            this.rtbConString = new System.Windows.Forms.RichTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbOther = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.btnFindFront = new System.Windows.Forms.Button();
            this.lblFind = new System.Windows.Forms.Label();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.txbSearchColumn = new System.Windows.Forms.TextBox();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            this.grbOrcNet.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grbConSting.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1054, 27);
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
            this.grbOrcNet.Size = new System.Drawing.Size(1054, 51);
            this.grbOrcNet.TabIndex = 40;
            this.grbOrcNet.TabStop = false;
            this.grbOrcNet.Text = "表和SQL类型";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 599F));
            this.tableLayoutPanel4.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTableName, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbGetTableList, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbModule, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1048, 31);
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
            this.cbbTableName.Size = new System.Drawing.Size(246, 20);
            this.cbbTableName.TabIndex = 2;
            this.cbbTableName.SelectedIndexChanged += new System.EventHandler(this.cbbTableName_SelectedIndexChanged);
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(302, 5);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(84, 16);
            this.ckbGetTableList.TabIndex = 3;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // cbbModule
            // 
            this.cbbModule.FormattingEnabled = true;
            this.cbbModule.Location = new System.Drawing.Point(439, 3);
            this.cbbModule.Name = "cbbModule";
            this.cbbModule.Size = new System.Drawing.Size(121, 20);
            this.cbbModule.TabIndex = 4;
            this.cbbModule.SelectedIndexChanged += new System.EventHandler(this.cbbModule_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "模板：";
            // 
            // grbConSting
            // 
            this.grbConSting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbConSting.Controls.Add(this.rtbConString);
            this.grbConSting.Controls.Add(this.label21);
            this.grbConSting.Controls.Add(this.rtbOther);
            this.grbConSting.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbConSting.Location = new System.Drawing.Point(0, 156);
            this.grbConSting.Name = "grbConSting";
            this.grbConSting.Size = new System.Drawing.Size(1054, 118);
            this.grbConSting.TabIndex = 42;
            this.grbConSting.TabStop = false;
            this.grbConSting.Text = "拼接字符";
            // 
            // rtbConString
            // 
            this.rtbConString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConString.Location = new System.Drawing.Point(3, 17);
            this.rtbConString.Name = "rtbConString";
            this.rtbConString.Size = new System.Drawing.Size(511, 98);
            this.rtbConString.TabIndex = 14;
            this.rtbConString.Text = "";
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
            // rtbOther
            // 
            this.rtbOther.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtbOther.Location = new System.Drawing.Point(514, 17);
            this.rtbOther.Name = "rtbOther";
            this.rtbOther.Size = new System.Drawing.Size(537, 98);
            this.rtbOther.TabIndex = 5;
            this.rtbOther.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 274);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1054, 253);
            this.tabControl1.TabIndex = 43;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbColumn);
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(1046, 227);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.btnFindFront);
            this.grbColumn.Controls.Add(this.lblFind);
            this.grbColumn.Controls.Add(this.btnFindNext);
            this.grbColumn.Controls.Add(this.txbSearchColumn);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(3, 80);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Size = new System.Drawing.Size(1040, 144);
            this.grbColumn.TabIndex = 8;
            this.grbColumn.TabStop = false;
            this.grbColumn.Text = "列清单";
            // 
            // btnFindFront
            // 
            this.btnFindFront.Location = new System.Drawing.Point(353, -2);
            this.btnFindFront.Name = "btnFindFront";
            this.btnFindFront.Size = new System.Drawing.Size(55, 21);
            this.btnFindFront.TabIndex = 18;
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
            this.lblFind.Location = new System.Drawing.Point(423, 1);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(53, 12);
            this.lblFind.TabIndex = 16;
            this.lblFind.Text = "查找提示";
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(294, -2);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(56, 21);
            this.btnFindNext.TabIndex = 15;
            this.btnFindNext.Text = "下一个";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // txbSearchColumn
            // 
            this.txbSearchColumn.Location = new System.Drawing.Point(104, -3);
            this.txbSearchColumn.Name = "txbSearchColumn";
            this.txbSearchColumn.Size = new System.Drawing.Size(187, 21);
            this.txbSearchColumn.TabIndex = 14;
            // 
            // dgvColList
            // 
            this.dgvColList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColList.Location = new System.Drawing.Point(3, 17);
            this.dgvColList.Name = "dgvColList";
            this.dgvColList.RowTemplate.Height = 23;
            this.dgvColList.Size = new System.Drawing.Size(1034, 124);
            this.dgvColList.TabIndex = 0;
            this.dgvColList.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvColList_ColumnHeaderMouseDoubleClick);
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.dgvTableList);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbTable.Location = new System.Drawing.Point(3, 3);
            this.grbTable.Name = "grbTable";
            this.grbTable.Size = new System.Drawing.Size(1040, 77);
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
            this.dgvTableList.Size = new System.Drawing.Size(1034, 57);
            this.dgvTableList.TabIndex = 0;
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 22);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoSQL.Size = new System.Drawing.Size(1046, 227);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1040, 221);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1054, 78);
            this.uC_DbConnection1.TabIndex = 35;
            // 
            // FrmDBTTableColumnString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 527);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbConSting);
            this.Controls.Add(this.grbOrcNet);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTTableColumnString";
            this.Text = "表列字符拼接";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbOrcNet.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.grbConSting.ResumeLayout(false);
            this.grbConSting.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.grbTable.ResumeLayout(false);
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
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbTableName;
        private System.Windows.Forms.GroupBox grbConSting;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbColumn;
        private System.Windows.Forms.DataGridView dgvColList;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.ComboBox cbbModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbOther;
        private System.Windows.Forms.RichTextBox rtbConString;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.TextBox txbSearchColumn;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.Button btnFindFront;
    }
}