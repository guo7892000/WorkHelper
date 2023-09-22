namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    partial class FrmDBTExchangeStringPlace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTExchangeStringPlace));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbLoadExampleData = new System.Windows.Forms.CheckBox();
            this.txbSplit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txbEndAppend = new System.Windows.Forms.TextBox();
            this.txbEndOldString = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbEndReplace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(71, 24);
            this.tsbAutoSQL.Text = "转换(&S)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.TsbAutoSQL_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(72, 24);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.TsbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbLoadExampleData);
            this.groupBox1.Controls.Add(this.txbSplit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(625, 47);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分隔符";
            // 
            // ckbLoadExampleData
            // 
            this.ckbLoadExampleData.AutoSize = true;
            this.ckbLoadExampleData.Location = new System.Drawing.Point(148, 17);
            this.ckbLoadExampleData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbLoadExampleData.Name = "ckbLoadExampleData";
            this.ckbLoadExampleData.Size = new System.Drawing.Size(96, 16);
            this.ckbLoadExampleData.TabIndex = 2;
            this.ckbLoadExampleData.Text = "加载示例数据";
            this.ckbLoadExampleData.UseVisualStyleBackColor = true;
            this.ckbLoadExampleData.CheckedChanged += new System.EventHandler(this.CkbLoadExampleData_CheckedChanged);
            // 
            // txbSplit
            // 
            this.txbSplit.Location = new System.Drawing.Point(64, 15);
            this.txbSplit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbSplit.Name = "txbSplit";
            this.txbSplit.Size = new System.Drawing.Size(76, 21);
            this.txbSplit.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分隔符：";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(146, 2);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(329, 12);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "将分隔符两边交换后，再替换旧字符，并在尾部加上指定字符";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbInput);
            this.groupBox2.Controls.Add(this.lblInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(625, 110);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要转换的字符";
            // 
            // rtbInput
            // 
            this.rtbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInput.Location = new System.Drawing.Point(2, 16);
            this.rtbInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(621, 92);
            this.rtbInput.TabIndex = 2;
            this.rtbInput.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(625, 44);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "转换后进一步处理";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbEndAppend, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbEndOldString, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbEndReplace, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 26);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "替换前旧字符：";
            // 
            // txbEndAppend
            // 
            this.txbEndAppend.Location = new System.Drawing.Point(473, 2);
            this.txbEndAppend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbEndAppend.Name = "txbEndAppend";
            this.txbEndAppend.Size = new System.Drawing.Size(86, 21);
            this.txbEndAppend.TabIndex = 7;
            // 
            // txbEndOldString
            // 
            this.txbEndOldString.Location = new System.Drawing.Point(95, 2);
            this.txbEndOldString.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbEndOldString.Name = "txbEndOldString";
            this.txbEndOldString.Size = new System.Drawing.Size(86, 21);
            this.txbEndOldString.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "结尾附加的字符：";
            // 
            // txbEndReplace
            // 
            this.txbEndReplace.Location = new System.Drawing.Point(278, 2);
            this.txbEndReplace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbEndReplace.Name = "txbEndReplace";
            this.txbEndReplace.Size = new System.Drawing.Size(86, 21);
            this.txbEndReplace.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "替换后新字符：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rtbOutput);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 44);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(625, 232);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "转换后的字符";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(2, 16);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(621, 214);
            this.rtbOutput.TabIndex = 0;
            this.rtbOutput.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 74);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(625, 390);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 28;
            // 
            // FrmDBTExchangeStringPlace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 464);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmDBTExchangeStringPlace";
            this.Text = "转换字符位置";
            this.Load += new System.EventHandler(this.FrmDBTExchangeStringPlace_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbSplit;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.TextBox txbEndAppend;
        private System.Windows.Forms.TextBox txbEndReplace;
        private System.Windows.Forms.TextBox txbEndOldString;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.CheckBox ckbLoadExampleData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}