namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmReadSameFormatExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReadSameFormatExcel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSelectPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSheetNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbExcelType = new System.Windows.Forms.ComboBox();
            this.ckbRemoveEmptyColumn = new System.Windows.Forms.CheckBox();
            this.lblMergeInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpReadFile = new System.Windows.Forms.TabPage();
            this.tpResult = new System.Windows.Forms.TabPage();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpReadFile.SuspendLayout();
            this.tpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAutoSQL,
            this.tsbExport,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1866, 41);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(118, 35);
            this.tsbAutoSQL.Text = "读取(&R)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.TsbAutoSQL_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(115, 35);
            this.tsbExport.Text = "导出(&E)";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(117, 35);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.TsbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.lblMergeInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(1866, 156);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1602F));
            this.tableLayoutPanel1.Controls.Add(this.btnSelectFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbSelectPath, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudSheetNum, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbExcelType, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbRemoveEmptyColumn, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 34);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1854, 116);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(232, 6);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(46, 46);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "配置文件选择：";
            // 
            // txbSelectPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txbSelectPath, 5);
            this.txbSelectPath.Location = new System.Drawing.Point(290, 6);
            this.txbSelectPath.Margin = new System.Windows.Forms.Padding(6);
            this.txbSelectPath.Multiline = true;
            this.txbSelectPath.Name = "txbSelectPath";
            this.txbSelectPath.Size = new System.Drawing.Size(1244, 46);
            this.txbSelectPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Excel第几个页签：";
            // 
            // nudSheetNum
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nudSheetNum, 2);
            this.nudSheetNum.Location = new System.Drawing.Point(229, 61);
            this.nudSheetNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSheetNum.Name = "nudSheetNum";
            this.nudSheetNum.Size = new System.Drawing.Size(120, 35);
            this.nudSheetNum.TabIndex = 7;
            this.nudSheetNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(358, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Excel类型：";
            // 
            // cbbExcelType
            // 
            this.cbbExcelType.FormattingEnabled = true;
            this.cbbExcelType.Location = new System.Drawing.Point(509, 61);
            this.cbbExcelType.Name = "cbbExcelType";
            this.cbbExcelType.Size = new System.Drawing.Size(204, 32);
            this.cbbExcelType.TabIndex = 8;
            // 
            // ckbRemoveEmptyColumn
            // 
            this.ckbRemoveEmptyColumn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbRemoveEmptyColumn.AutoSize = true;
            this.ckbRemoveEmptyColumn.Checked = true;
            this.ckbRemoveEmptyColumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRemoveEmptyColumn.Location = new System.Drawing.Point(719, 64);
            this.ckbRemoveEmptyColumn.Name = "ckbRemoveEmptyColumn";
            this.ckbRemoveEmptyColumn.Size = new System.Drawing.Size(258, 28);
            this.ckbRemoveEmptyColumn.TabIndex = 9;
            this.ckbRemoveEmptyColumn.Text = "移除所有列为空数据";
            this.ckbRemoveEmptyColumn.UseVisualStyleBackColor = true;
            // 
            // lblMergeInfo
            // 
            this.lblMergeInfo.AutoSize = true;
            this.lblMergeInfo.ForeColor = System.Drawing.Color.Red;
            this.lblMergeInfo.Location = new System.Drawing.Point(328, 4);
            this.lblMergeInfo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblMergeInfo.Name = "lblMergeInfo";
            this.lblMergeInfo.Size = new System.Drawing.Size(58, 24);
            this.lblMergeInfo.TabIndex = 0;
            this.lblMergeInfo.Text = "说明";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(1844, 794);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(6, 34);
            this.rtbString.Margin = new System.Windows.Forms.Padding(6);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(1832, 754);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpReadFile);
            this.tabControl1.Controls.Add(this.tpResult);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 197);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1866, 847);
            this.tabControl1.TabIndex = 36;
            // 
            // tpReadFile
            // 
            this.tpReadFile.Controls.Add(this.groupBox3);
            this.tpReadFile.Location = new System.Drawing.Point(8, 39);
            this.tpReadFile.Name = "tpReadFile";
            this.tpReadFile.Padding = new System.Windows.Forms.Padding(3);
            this.tpReadFile.Size = new System.Drawing.Size(1850, 800);
            this.tpReadFile.TabIndex = 0;
            this.tpReadFile.Text = "读取文件清单";
            this.tpReadFile.UseVisualStyleBackColor = true;
            // 
            // tpResult
            // 
            this.tpResult.Controls.Add(this.dgvResult);
            this.tpResult.Location = new System.Drawing.Point(8, 39);
            this.tpResult.Name = "tpResult";
            this.tpResult.Padding = new System.Windows.Forms.Padding(3);
            this.tpResult.Size = new System.Drawing.Size(1850, 800);
            this.tpResult.TabIndex = 1;
            this.tpResult.Text = "查询结果";
            this.tpResult.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 3);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(6);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersWidth = 82;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(1844, 794);
            this.dgvResult.TabIndex = 1;
            // 
            // FrmReadSameFormatExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1866, 1044);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmReadSameFormatExcel";
            this.Text = "非结构化Excel内容解析";
            this.Load += new System.EventHandler(this.FrmDBTScriptMerge_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpReadFile.ResumeLayout(false);
            this.tpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbString;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbSelectPath;
        private System.Windows.Forms.Label lblMergeInfo;
        private System.Windows.Forms.NumericUpDown nudSheetNum;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpReadFile;
        private System.Windows.Forms.TabPage tpResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbExcelType;
        private System.Windows.Forms.CheckBox ckbRemoveEmptyColumn;
    }
}