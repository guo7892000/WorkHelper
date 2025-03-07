﻿namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTExcelCol2Row
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTExcelCol2Row));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDbType = new System.Windows.Forms.Label();
            this.ckbAutoColumnName = new System.Windows.Forms.CheckBox();
            this.nudFixRowCount = new System.Windows.Forms.NumericUpDown();
            this.nudEachDataRowCount = new System.Windows.Forms.NumericUpDown();
            this.CkbLoadExampleData = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImport = new System.Windows.Forms.TabPage();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.dgvTableList = new System.Windows.Forms.DataGridView();
            this.lblTableData = new System.Windows.Forms.Label();
            this.tpAutoSQL = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEachDataRowCount)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpImport.SuspendLayout();
            this.grbTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).BeginInit();
            this.tpAutoSQL.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(625, 27);
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
            this.groupBox1.Size = new System.Drawing.Size(625, 50);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDbType, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbAutoColumnName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudFixRowCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudEachDataRowCount, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.CkbLoadExampleData, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 30);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "固定行数：";
            // 
            // lblDbType
            // 
            this.lblDbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDbType.AutoSize = true;
            this.lblDbType.ForeColor = System.Drawing.Color.Red;
            this.lblDbType.Location = new System.Drawing.Point(168, 6);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(89, 12);
            this.lblDbType.TabIndex = 3;
            this.lblDbType.Text = "每组数据行数：";
            // 
            // ckbAutoColumnName
            // 
            this.ckbAutoColumnName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAutoColumnName.AutoSize = true;
            this.ckbAutoColumnName.Location = new System.Drawing.Point(356, 4);
            this.ckbAutoColumnName.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAutoColumnName.Name = "ckbAutoColumnName";
            this.ckbAutoColumnName.Size = new System.Drawing.Size(96, 16);
            this.ckbAutoColumnName.TabIndex = 3;
            this.ckbAutoColumnName.Text = "自动字母列名";
            this.ckbAutoColumnName.UseVisualStyleBackColor = true;
            // 
            // nudFixRowCount
            // 
            this.nudFixRowCount.Location = new System.Drawing.Point(73, 2);
            this.nudFixRowCount.Margin = new System.Windows.Forms.Padding(2);
            this.nudFixRowCount.Name = "nudFixRowCount";
            this.nudFixRowCount.Size = new System.Drawing.Size(90, 21);
            this.nudFixRowCount.TabIndex = 1;
            // 
            // nudEachDataRowCount
            // 
            this.nudEachDataRowCount.Location = new System.Drawing.Point(262, 2);
            this.nudEachDataRowCount.Margin = new System.Windows.Forms.Padding(2);
            this.nudEachDataRowCount.Name = "nudEachDataRowCount";
            this.nudEachDataRowCount.Size = new System.Drawing.Size(90, 21);
            this.nudEachDataRowCount.TabIndex = 2;
            this.nudEachDataRowCount.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // CkbLoadExampleData
            // 
            this.CkbLoadExampleData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CkbLoadExampleData.AutoSize = true;
            this.CkbLoadExampleData.Location = new System.Drawing.Point(456, 4);
            this.CkbLoadExampleData.Margin = new System.Windows.Forms.Padding(2);
            this.CkbLoadExampleData.Name = "CkbLoadExampleData";
            this.CkbLoadExampleData.Size = new System.Drawing.Size(96, 16);
            this.CkbLoadExampleData.TabIndex = 3;
            this.CkbLoadExampleData.Text = "加载示例数据";
            this.CkbLoadExampleData.UseVisualStyleBackColor = true;
            this.CkbLoadExampleData.CheckedChanged += new System.EventHandler(this.CkbLoadExampleData_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 378);
            this.tabControl1.TabIndex = 27;
            // 
            // tpImport
            // 
            this.tpImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.tpImport.Controls.Add(this.grbTable);
            this.tpImport.Location = new System.Drawing.Point(4, 22);
            this.tpImport.Name = "tpImport";
            this.tpImport.Padding = new System.Windows.Forms.Padding(3);
            this.tpImport.Size = new System.Drawing.Size(617, 352);
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
            this.grbTable.Size = new System.Drawing.Size(611, 346);
            this.grbTable.TabIndex = 1;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "粘贴的数据列表";
            // 
            // dgvTableList
            // 
            this.dgvTableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTableList.Location = new System.Drawing.Point(3, 17);
            this.dgvTableList.Name = "dgvTableList";
            this.dgvTableList.RowTemplate.Height = 23;
            this.dgvTableList.Size = new System.Drawing.Size(605, 326);
            this.dgvTableList.TabIndex = 0;
            this.dgvTableList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTableList_KeyDown);
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
            this.tpAutoSQL.Size = new System.Drawing.Size(617, 352);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(3, 3);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(611, 346);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPaste});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(100, 22);
            this.tsmiPaste.Text = "粘贴";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // FrmDBTExcelCol2Row
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 455);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDBTExcelCol2Row";
            this.Text = "Excel列转行";
            this.Load += new System.EventHandler(this.FrmCopyData_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEachDataRowCount)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableList)).EndInit();
            this.tpAutoSQL.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImport;
        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.DataGridView dgvTableList;
        private System.Windows.Forms.Label lblTableData;
        private System.Windows.Forms.TabPage tpAutoSQL;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label lblDbType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox ckbAutoColumnName;
        private System.Windows.Forms.NumericUpDown nudFixRowCount;
        private System.Windows.Forms.NumericUpDown nudEachDataRowCount;
        private System.Windows.Forms.CheckBox CkbLoadExampleData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
    }
}