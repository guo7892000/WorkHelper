namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTAutoSqlVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTAutoSqlVerify));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbConvert = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.ckbExample = new System.Windows.Forms.CheckBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnGetCondition = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbSqlOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvQuery = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAutoBefore = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblBefore = new System.Windows.Forms.Label();
            this.rtbSqlInput = new System.Windows.Forms.RichTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tpAutoAfter = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvConditionInput = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvConditionOutput = new System.Windows.Forms.DataGridView();
            this.lblFuncInfo = new System.Windows.Forms.Label();
            this.uC_DbConnection1 = new Breezee.WorkHelper.DBTool.UI.UC_DbConnection();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpAutoBefore.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpAutoAfter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionInput)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImport,
            this.tsbConvert,
            this.tsbExport,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1100, 27);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(92, 24);
            this.tsbImport.Text = "获取条件(&I)";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbConvert
            // 
            this.tsbConvert.Image = ((System.Drawing.Image)(resources.GetObject("tsbConvert.Image")));
            this.tsbConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConvert.Name = "tsbConvert";
            this.tsbConvert.Size = new System.Drawing.Size(84, 24);
            this.tsbConvert.Text = "参数化(&A)";
            this.tsbConvert.Click += new System.EventHandler(this.tsbConvert_Click);
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
            // ckbExample
            // 
            this.ckbExample.AutoSize = true;
            this.ckbExample.Location = new System.Drawing.Point(152, 17);
            this.ckbExample.Name = "ckbExample";
            this.ckbExample.Size = new System.Drawing.Size(72, 16);
            this.ckbExample.TabIndex = 1;
            this.ckbExample.Text = "加载示例";
            this.ckbExample.UseVisualStyleBackColor = true;
            this.ckbExample.CheckedChanged += new System.EventHandler(this.ckbExample_CheckedChanged);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(132, 10);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(89, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "执行最终SQL";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnGetCondition
            // 
            this.btnGetCondition.Location = new System.Drawing.Point(39, 13);
            this.btnGetCondition.Name = "btnGetCondition";
            this.btnGetCondition.Size = new System.Drawing.Size(75, 23);
            this.btnGetCondition.TabIndex = 0;
            this.btnGetCondition.Text = "获取条件";
            this.btnGetCondition.UseVisualStyleBackColor = true;
            this.btnGetCondition.Click += new System.EventHandler(this.btnGetCondition_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(35, 10);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "参数化";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 38);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer1.Size = new System.Drawing.Size(616, 574);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 35;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.rtbSqlOutput);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 343);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "最终参数化后的SQL";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(218, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "根据传入条件动态生成最终可参数化执行的SQL";
            // 
            // rtbSqlOutput
            // 
            this.rtbSqlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSqlOutput.Location = new System.Drawing.Point(3, 17);
            this.rtbSqlOutput.Name = "rtbSqlOutput";
            this.rtbSqlOutput.Size = new System.Drawing.Size(610, 323);
            this.rtbSqlOutput.TabIndex = 0;
            this.rtbSqlOutput.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvQuery);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(616, 227);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "查询结果";
            // 
            // dgvQuery
            // 
            this.dgvQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuery.Location = new System.Drawing.Point(3, 17);
            this.dgvQuery.Name = "dgvQuery";
            this.dgvQuery.RowTemplate.Height = 23;
            this.dgvQuery.Size = new System.Drawing.Size(610, 207);
            this.dgvQuery.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAutoBefore);
            this.tabControl1.Controls.Add(this.tpAutoAfter);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 107);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1100, 644);
            this.tabControl1.TabIndex = 36;
            // 
            // tpAutoBefore
            // 
            this.tpAutoBefore.Controls.Add(this.groupBox6);
            this.tpAutoBefore.Controls.Add(this.groupBox7);
            this.tpAutoBefore.Location = new System.Drawing.Point(4, 22);
            this.tpAutoBefore.Name = "tpAutoBefore";
            this.tpAutoBefore.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoBefore.Size = new System.Drawing.Size(1092, 618);
            this.tpAutoBefore.TabIndex = 0;
            this.tpAutoBefore.Text = "自动参数化前";
            this.tpAutoBefore.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblBefore);
            this.groupBox6.Controls.Add(this.rtbSqlInput);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 45);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1086, 570);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "参数化前配置的SQL";
            // 
            // lblBefore
            // 
            this.lblBefore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBefore.AutoSize = true;
            this.lblBefore.ForeColor = System.Drawing.Color.Red;
            this.lblBefore.Location = new System.Drawing.Point(169, 0);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(137, 12);
            this.lblBefore.TabIndex = 14;
            this.lblBefore.Text = "动态条件形式：#参数名#";
            // 
            // rtbSqlInput
            // 
            this.rtbSqlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSqlInput.Location = new System.Drawing.Point(3, 17);
            this.rtbSqlInput.Name = "rtbSqlInput";
            this.rtbSqlInput.Size = new System.Drawing.Size(1080, 550);
            this.rtbSqlInput.TabIndex = 1;
            this.rtbSqlInput.Text = "";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ckbExample);
            this.groupBox7.Controls.Add(this.btnGetCondition);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1086, 42);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "操作";
            // 
            // tpAutoAfter
            // 
            this.tpAutoAfter.Controls.Add(this.splitContainer3);
            this.tpAutoAfter.Location = new System.Drawing.Point(4, 22);
            this.tpAutoAfter.Name = "tpAutoAfter";
            this.tpAutoAfter.Padding = new System.Windows.Forms.Padding(3);
            this.tpAutoAfter.Size = new System.Drawing.Size(1092, 618);
            this.tpAutoAfter.TabIndex = 1;
            this.tpAutoAfter.Text = "自动参数化后";
            this.tpAutoAfter.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer3.Size = new System.Drawing.Size(1086, 612);
            this.splitContainer3.SplitterDistance = 855;
            this.splitContainer3.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer2.Size = new System.Drawing.Size(855, 612);
            this.splitContainer2.SplitterDistance = 235;
            this.splitContainer2.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvConditionInput);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 612);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数化前传入的条件";
            // 
            // dgvConditionInput
            // 
            this.dgvConditionInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConditionInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditionInput.Location = new System.Drawing.Point(3, 17);
            this.dgvConditionInput.Name = "dgvConditionInput";
            this.dgvConditionInput.RowTemplate.Height = 23;
            this.dgvConditionInput.Size = new System.Drawing.Size(229, 592);
            this.dgvConditionInput.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnConvert);
            this.groupBox8.Controls.Add(this.btnExecute);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(616, 38);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "操作";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvConditionOutput);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(227, 612);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "最终参数化的条件";
            // 
            // dgvConditionOutput
            // 
            this.dgvConditionOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConditionOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditionOutput.Location = new System.Drawing.Point(3, 17);
            this.dgvConditionOutput.Name = "dgvConditionOutput";
            this.dgvConditionOutput.RowTemplate.Height = 23;
            this.dgvConditionOutput.Size = new System.Drawing.Size(221, 592);
            this.dgvConditionOutput.TabIndex = 1;
            // 
            // lblFuncInfo
            // 
            this.lblFuncInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFuncInfo.AutoSize = true;
            this.lblFuncInfo.ForeColor = System.Drawing.Color.Red;
            this.lblFuncInfo.Location = new System.Drawing.Point(362, 8);
            this.lblFuncInfo.Name = "lblFuncInfo";
            this.lblFuncInfo.Size = new System.Drawing.Size(29, 12);
            this.lblFuncInfo.TabIndex = 36;
            this.lblFuncInfo.Text = "说明";
            // 
            // uC_DbConnection1
            // 
            this.uC_DbConnection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_DbConnection1.Location = new System.Drawing.Point(0, 27);
            this.uC_DbConnection1.Name = "uC_DbConnection1";
            this.uC_DbConnection1.Size = new System.Drawing.Size(1100, 80);
            this.uC_DbConnection1.TabIndex = 33;
            // 
            // FrmDBTAutoSqlVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 751);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblFuncInfo);
            this.Controls.Add(this.uC_DbConnection1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "FrmDBTAutoSqlVerify";
            this.Text = "自动生成SQL验证";
            this.Load += new System.EventHandler(this.FrmDBTAutoSqlVerify_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpAutoBefore.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tpAutoAfter.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionInput)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbConvert;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private UC_DbConnection uC_DbConnection1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvQuery;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAutoBefore;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TabPage tpAutoAfter;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvConditionOutput;
        private System.Windows.Forms.RichTextBox rtbSqlInput;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnGetCondition;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.CheckBox ckbExample;
        private System.Windows.Forms.Label lblFuncInfo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvConditionInput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbSqlOutput;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}