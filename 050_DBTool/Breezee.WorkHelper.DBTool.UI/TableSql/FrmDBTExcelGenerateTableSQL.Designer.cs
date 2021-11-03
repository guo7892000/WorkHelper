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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTExcelGenerateTableSQL));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbColumn = new System.Windows.Forms.GroupBox();
            this.lblColumnInfo = new System.Windows.Forms.Label();
            this.dgvColList = new System.Windows.Forms.DataGridView();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbTargetDbType = new System.Windows.Forms.ComboBox();
            this.cbbImportDBType = new System.Windows.Forms.ComboBox();
            this.ckbAllConvert = new System.Windows.Forms.CheckBox();
            this.lblThree = new System.Windows.Forms.Label();
            this.cbbCreateType = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbDownLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.btnSaveOther = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            this.grbColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).BeginInit();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 89);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1113, 436);
            this.tabControl1.TabIndex = 25;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbColumn);
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 25);
            this.tpImport.Margin = new System.Windows.Forms.Padding(4);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(4);
            this.tpImport.Size = new System.Drawing.Size(1105, 407);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.lblColumnInfo);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(4, 164);
            this.grbColumn.Margin = new System.Windows.Forms.Padding(4);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Padding = new System.Windows.Forms.Padding(4);
            this.grbColumn.Size = new System.Drawing.Size(1097, 239);
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
            this.lblColumnInfo.Location = new System.Drawing.Point(232, 1);
            this.lblColumnInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColumnInfo.Name = "lblColumnInfo";
            this.lblColumnInfo.Size = new System.Drawing.Size(67, 15);
            this.lblColumnInfo.TabIndex = 13;
            this.lblColumnInfo.Text = "提示信息";
            // 
            // dgvColList
            // 
            this.dgvColList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColList.Location = new System.Drawing.Point(4, 22);
            this.dgvColList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvColList.Name = "dgvColList";
            this.dgvColList.RowTemplate.Height = 23;
            this.dgvColList.Size = new System.Drawing.Size(1089, 213);
            this.dgvColList.TabIndex = 0;
            // 
            // grbTable
            // 
            this.grbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbTable.Controls.Add(this.dgvTableList);
            this.grbTable.Controls.Add(this.lblTableData);
            this.grbTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbTable.Location = new System.Drawing.Point(4, 4);
            this.grbTable.Margin = new System.Windows.Forms.Padding(4);
            this.grbTable.Name = "grbTable";
            this.grbTable.Padding = new System.Windows.Forms.Padding(4);
            this.grbTable.Size = new System.Drawing.Size(1097, 160);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "表清单";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTableList.Location = new System.Drawing.Point(4, 22);
            this.dgvTableList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(1089, 134);
            this.dgvTableList.TabIndex = 0;
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(232, 1);
            this.lblTableData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableData.Name = "lblTableData";
            this.lblTableData.Size = new System.Drawing.Size(67, 15);
            this.lblTableData.TabIndex = 12;
            this.lblTableData.Text = "提示信息";
            // 
            // tpAutoSQL
            // 
            this.tpAutoSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpAutoSQL.Controls.Add(this.rtbResult);
            this.tpAutoSQL.Location = new System.Drawing.Point(4, 25);
            this.tpAutoSQL.Margin = new System.Windows.Forms.Padding(4);
            this.tpAutoSQL.Name = "tpAutoSQL";
            this.tpAutoSQL.Padding = new System.Windows.Forms.Padding(4);
            this.tpAutoSQL.Size = new System.Drawing.Size(1073, 407);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(4, 4);
            this.rtbResult.Margin = new System.Windows.Forms.Padding(4);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1065, 399);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1113, 62);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(151, 2);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(67, 15);
            this.lblInfo.TabIndex = 13;
            this.lblInfo.Text = "提示信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbTargetDbType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbImportDBType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbAllConvert, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblThree, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbCreateType, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveOther, 7, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1105, 36);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(340, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "导入数据库类型：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "目标数据库类型：";
            // 
            // cbbTargetDbType
            // 
            this.cbbTargetDbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTargetDbType.FormattingEnabled = true;
            this.cbbTargetDbType.Location = new System.Drawing.Point(139, 4);
            this.cbbTargetDbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTargetDbType.Name = "cbbTargetDbType";
            this.cbbTargetDbType.Size = new System.Drawing.Size(193, 23);
            this.cbbTargetDbType.TabIndex = 3;
            // 
            // cbbImportDBType
            // 
            this.cbbImportDBType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbImportDBType.FormattingEnabled = true;
            this.cbbImportDBType.Location = new System.Drawing.Point(475, 4);
            this.cbbImportDBType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbImportDBType.Name = "cbbImportDBType";
            this.cbbImportDBType.Size = new System.Drawing.Size(160, 23);
            this.cbbImportDBType.TabIndex = 4;
            this.cbbImportDBType.SelectedIndexChanged += new System.EventHandler(this.cbbImportDBType_SelectedIndexChanged);
            // 
            // ckbAllConvert
            // 
            this.ckbAllConvert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAllConvert.AutoSize = true;
            this.ckbAllConvert.Location = new System.Drawing.Point(901, 6);
            this.ckbAllConvert.Margin = new System.Windows.Forms.Padding(4);
            this.ckbAllConvert.Name = "ckbAllConvert";
            this.ckbAllConvert.Size = new System.Drawing.Size(89, 19);
            this.ckbAllConvert.TabIndex = 7;
            this.ckbAllConvert.Text = "综合转换";
            this.ckbAllConvert.UseVisualStyleBackColor = true;
            this.ckbAllConvert.CheckedChanged += new System.EventHandler(this.ckbAllConvert_CheckedChanged);
            // 
            // lblThree
            // 
            this.lblThree.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblThree.AutoSize = true;
            this.lblThree.ForeColor = System.Drawing.Color.Red;
            this.lblThree.Location = new System.Drawing.Point(643, 8);
            this.lblThree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThree.Name = "lblThree";
            this.lblThree.Size = new System.Drawing.Size(82, 15);
            this.lblThree.TabIndex = 1;
            this.lblThree.Text = "创建方式：";
            // 
            // cbbCreateType
            // 
            this.cbbCreateType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbbCreateType.FormattingEnabled = true;
            this.cbbCreateType.Location = new System.Drawing.Point(733, 4);
            this.cbbCreateType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbCreateType.Name = "cbbCreateType";
            this.cbbCreateType.Size = new System.Drawing.Size(160, 23);
            this.cbbCreateType.TabIndex = 5;
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
            this.toolStrip1.Size = new System.Drawing.Size(1113, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(77, 24);
            this.tsbImport.Text = "导入(&I)";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(84, 24);
            this.tsbAutoSQL.Text = "生成(&A)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.tsbAutoSQL_Click);
            // 
            // tsbDownLoad
            // 
            this.tsbDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownLoad.Image")));
            this.tsbDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownLoad.Name = "tsbDownLoad";
            this.tsbDownLoad.Size = new System.Drawing.Size(114, 24);
            this.tsbDownLoad.Text = "下载模板(&D)";
            this.tsbDownLoad.Click += new System.EventHandler(this.tsbDownLoad_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(83, 24);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // btnSaveOther
            // 
            this.btnSaveOther.Location = new System.Drawing.Point(997, 3);
            this.btnSaveOther.Name = "btnSaveOther";
            this.btnSaveOther.Size = new System.Drawing.Size(73, 25);
            this.btnSaveOther.TabIndex = 17;
            this.btnSaveOther.Text = "另存为";
            this.btnSaveOther.UseVisualStyleBackColor = true;
            this.btnSaveOther.Click += new System.EventHandler(this.BtnSaveOther_Click);
            // 
            // FrmDBTExcelGenerateTableSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 525);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDBTExcelGenerateTableSQL";
            this.Text = "Excel导入生成表SQL";
            this.Load += new System.EventHandler(this.FrmDBTImportExcelGenerateTableSQL_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbTargetDbType;
        private System.Windows.Forms.CheckBox ckbAllConvert;
        private System.Windows.Forms.Button btnSaveOther;
    }
}