namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTGetTableQuerySql
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTGetTableQuerySql));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.grbOrcNet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbGetTableList = new System.Windows.Forms.CheckBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txbTableShortName = new System.Windows.Forms.TextBox();
            this.cbbParaType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbTableName = new System.Windows.Forms.ComboBox();
            this.txbParamPre = new System.Windows.Forms.TextBox();
            this.ckbUseDefaultConfig = new System.Windows.Forms.CheckBox();
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
            this.toolStrip1.SuspendLayout();
            this.grbOrcNet.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1212, 27);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(85, 24);
            this.tsbImport.Text = "查询(&Q)";
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
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(83, 24);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(423, 8);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(67, 15);
            this.lblInfo.TabIndex = 31;
            this.lblInfo.Text = "提示信息";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1212, 97);
            this.uC_DbConnection1.TabIndex = 35;
            // 
            // grbOrcNet
            // 
            this.grbOrcNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbOrcNet.Controls.Add(this.tableLayoutPanel4);
            this.grbOrcNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbOrcNet.Location = new System.Drawing.Point(0, 124);
            this.grbOrcNet.Margin = new System.Windows.Forms.Padding(4);
            this.grbOrcNet.Name = "grbOrcNet";
            this.grbOrcNet.Padding = new System.Windows.Forms.Padding(4);
            this.grbOrcNet.Size = new System.Drawing.Size(1212, 64);
            this.grbOrcNet.TabIndex = 40;
            this.grbOrcNet.TabStop = false;
            this.grbOrcNet.Text = "表和SQL类型";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 12;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel4.Controls.Add(this.ckbGetTableList, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbType, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label14, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label19, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.txbTableShortName, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbParaType, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbbTableName, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.txbParamPre, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.ckbUseDefaultConfig, 10, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1204, 38);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // ckbGetTableList
            // 
            this.ckbGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbGetTableList.AutoSize = true;
            this.ckbGetTableList.Location = new System.Drawing.Point(571, 7);
            this.ckbGetTableList.Margin = new System.Windows.Forms.Padding(4);
            this.ckbGetTableList.Name = "ckbGetTableList";
            this.ckbGetTableList.Size = new System.Drawing.Size(104, 19);
            this.ckbGetTableList.TabIndex = 3;
            this.ckbGetTableList.Text = "获取表清单";
            this.ckbGetTableList.UseVisualStyleBackColor = true;
            this.ckbGetTableList.CheckedChanged += new System.EventHandler(this.ckbGetTableList_CheckedChanged);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(88, 5);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(80, 23);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL类型：";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(176, 9);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 15);
            this.label14.TabIndex = 1;
            this.label14.Text = "表名：";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(683, 9);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 15);
            this.label19.TabIndex = 1;
            this.label19.Text = "别名：";
            // 
            // txbTableShortName
            // 
            this.txbTableShortName.Location = new System.Drawing.Point(743, 4);
            this.txbTableShortName.Margin = new System.Windows.Forms.Padding(4);
            this.txbTableShortName.Name = "txbTableShortName";
            this.txbTableShortName.Size = new System.Drawing.Size(32, 25);
            this.txbTableShortName.TabIndex = 6;
            this.txbTableShortName.Text = "A";
            // 
            // cbbParaType
            // 
            this.cbbParaType.FormattingEnabled = true;
            this.cbbParaType.Location = new System.Drawing.Point(873, 4);
            this.cbbParaType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbParaType.Name = "cbbParaType";
            this.cbbParaType.Size = new System.Drawing.Size(117, 23);
            this.cbbParaType.TabIndex = 8;
            this.cbbParaType.SelectedIndexChanged += new System.EventHandler(this.CbbParaType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(783, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "参数类型：";
            // 
            // cbbTableName
            // 
            this.cbbTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTableName.FormattingEnabled = true;
            this.cbbTableName.Location = new System.Drawing.Point(236, 5);
            this.cbbTableName.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTableName.Name = "cbbTableName";
            this.cbbTableName.Size = new System.Drawing.Size(327, 23);
            this.cbbTableName.TabIndex = 2;
            // 
            // txbParamPre
            // 
            this.txbParamPre.Location = new System.Drawing.Point(998, 4);
            this.txbParamPre.Margin = new System.Windows.Forms.Padding(4);
            this.txbParamPre.Name = "txbParamPre";
            this.txbParamPre.Size = new System.Drawing.Size(32, 25);
            this.txbParamPre.TabIndex = 9;
            this.txbParamPre.Text = "@";
            // 
            // ckbUseDefaultConfig
            // 
            this.ckbUseDefaultConfig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbUseDefaultConfig.AutoSize = true;
            this.ckbUseDefaultConfig.Location = new System.Drawing.Point(1038, 7);
            this.ckbUseDefaultConfig.Margin = new System.Windows.Forms.Padding(4);
            this.ckbUseDefaultConfig.Name = "ckbUseDefaultConfig";
            this.ckbUseDefaultConfig.Size = new System.Drawing.Size(119, 19);
            this.ckbUseDefaultConfig.TabIndex = 11;
            this.ckbUseDefaultConfig.Text = "使用全局配置";
            this.ckbUseDefaultConfig.UseVisualStyleBackColor = true;
            this.ckbUseDefaultConfig.CheckedChanged += new System.EventHandler(this.CkbUseDefaultConfig_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImport);
            this.tabControl1.Controls.Add(this.tpAutoSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 188);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1212, 412);
            this.tabControl1.TabIndex = 41;
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
            this.tpImport.Size = new System.Drawing.Size(1204, 383);
            this.tpImport.TabIndex = 0;
            this.tpImport.Text = "导入清单";
            // 
            // grbColumn
            // 
            this.grbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbColumn.Controls.Add(this.lblColumnInfo);
            this.grbColumn.Controls.Add(this.dgvColList);
            this.grbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbColumn.Location = new System.Drawing.Point(4, 100);
            this.grbColumn.Margin = new System.Windows.Forms.Padding(4);
            this.grbColumn.Name = "grbColumn";
            this.grbColumn.Padding = new System.Windows.Forms.Padding(4);
            this.grbColumn.Size = new System.Drawing.Size(1196, 279);
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
            this.lblColumnInfo.Location = new System.Drawing.Point(413, 2);
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
            this.dgvColList.Size = new System.Drawing.Size(1188, 253);
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
            this.grbTable.Size = new System.Drawing.Size(1196, 96);
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
            this.dgvTableList.Size = new System.Drawing.Size(1188, 70);
            this.dgvTableList.TabIndex = 0;
            // 
            // lblTableData
            // 
            this.lblTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableData.AutoSize = true;
            this.lblTableData.ForeColor = System.Drawing.Color.Red;
            this.lblTableData.Location = new System.Drawing.Point(407, 0);
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
            this.tpAutoSQL.Size = new System.Drawing.Size(1204, 383);
            this.tpAutoSQL.TabIndex = 1;
            this.tpAutoSQL.Text = "生成结果";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(4, 4);
            this.rtbResult.Margin = new System.Windows.Forms.Padding(4);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(1196, 375);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // FrmDBTGetTableQuerySql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 600);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.grbOrcNet);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDBTGetTableQuerySql";
            this.Text = "获取增删改查SQL";
            this.Load += new System.EventHandler(this.FrmGetOracleSql_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbOrcNet.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImport.ResumeLayout(false);
            this.grbColumn.ResumeLayout(false);
            this.grbColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColList)).EndInit();
            this.grbTable.ResumeLayout(false);
            this.grbTable.PerformLayout();
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
        private System.Windows.Forms.Label lblInfo;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.GroupBox grbOrcNet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox ckbGetTableList;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txbTableShortName;
        private System.Windows.Forms.ComboBox cbbParaType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbTableName;
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
        private System.Windows.Forms.CheckBox ckbUseDefaultConfig;
        private System.Windows.Forms.TextBox txbParamPre;
    }
}