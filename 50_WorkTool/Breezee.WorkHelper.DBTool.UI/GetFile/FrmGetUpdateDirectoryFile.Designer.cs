namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmGetUpdateDirectoryFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetUpdateDirectoryFile));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbReadPath = new System.Windows.Forms.TextBox();
            this.btnReadPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTargetPath = new System.Windows.Forms.Button();
            this.txbTargetPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.ckbDateDir = new System.Windows.Forms.CheckBox();
            this.grbExclude = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txbExcludeEndprx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbExcludeDirName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbExcludeFullDir = new System.Windows.Forms.TextBox();
            this.lblExcludeTip = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grbExclude.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(951, 27);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(96, 24);
            this.tsbAutoSQL.Text = "获取文件(&A)";
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
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(951, 83);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读取与生成配置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.Controls.Add(this.dtpBegin, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txbReadPath, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnReadPath, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnTargetPath, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txbTargetPath, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.dtpEnd, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.ckbDateDir, 5, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(945, 66);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(632, 3);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(153, 21);
            this.dtpBegin.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(549, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "修改时间从：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "读取目录：";
            // 
            // txbReadPath
            // 
            this.txbReadPath.Location = new System.Drawing.Point(103, 3);
            this.txbReadPath.Multiline = true;
            this.txbReadPath.Name = "txbReadPath";
            this.txbReadPath.Size = new System.Drawing.Size(440, 23);
            this.txbReadPath.TabIndex = 1;
            // 
            // btnReadPath
            // 
            this.btnReadPath.Location = new System.Drawing.Point(74, 3);
            this.btnReadPath.Name = "btnReadPath";
            this.btnReadPath.Size = new System.Drawing.Size(23, 19);
            this.btnReadPath.TabIndex = 2;
            this.btnReadPath.Text = "...";
            this.btnReadPath.UseVisualStyleBackColor = true;
            this.btnReadPath.Click += new System.EventHandler(this.btnReadPath_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "生成目录：";
            // 
            // btnTargetPath
            // 
            this.btnTargetPath.Location = new System.Drawing.Point(74, 32);
            this.btnTargetPath.Name = "btnTargetPath";
            this.btnTargetPath.Size = new System.Drawing.Size(23, 19);
            this.btnTargetPath.TabIndex = 2;
            this.btnTargetPath.Text = "...";
            this.btnTargetPath.UseVisualStyleBackColor = true;
            this.btnTargetPath.Click += new System.EventHandler(this.btnReadPath_Click);
            // 
            // txbTargetPath
            // 
            this.txbTargetPath.Location = new System.Drawing.Point(103, 32);
            this.txbTargetPath.Multiline = true;
            this.txbTargetPath.Name = "txbTargetPath";
            this.txbTargetPath.Size = new System.Drawing.Size(440, 23);
            this.txbTargetPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(597, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "至：";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(632, 32);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(153, 21);
            this.dtpEnd.TabIndex = 12;
            // 
            // ckbDateDir
            // 
            this.ckbDateDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbDateDir.AutoSize = true;
            this.ckbDateDir.Location = new System.Drawing.Point(791, 35);
            this.ckbDateDir.Name = "ckbDateDir";
            this.ckbDateDir.Size = new System.Drawing.Size(96, 16);
            this.ckbDateDir.TabIndex = 13;
            this.ckbDateDir.Text = "生成日期目录";
            this.ckbDateDir.UseVisualStyleBackColor = true;
            // 
            // grbExclude
            // 
            this.grbExclude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbExclude.Controls.Add(this.tableLayoutPanel1);
            this.grbExclude.Controls.Add(this.lblExcludeTip);
            this.grbExclude.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbExclude.ForeColor = System.Drawing.Color.Red;
            this.grbExclude.Location = new System.Drawing.Point(0, 110);
            this.grbExclude.Name = "grbExclude";
            this.grbExclude.Size = new System.Drawing.Size(951, 51);
            this.grbExclude.TabIndex = 32;
            this.grbExclude.TabStop = false;
            this.grbExclude.Text = "排除目录或文件选项";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeEndprx, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeDirName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeFullDir, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(945, 31);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(625, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "排除后缀：";
            // 
            // txbExcludeEndprx
            // 
            this.txbExcludeEndprx.Location = new System.Drawing.Point(696, 3);
            this.txbExcludeEndprx.Name = "txbExcludeEndprx";
            this.txbExcludeEndprx.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeEndprx.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "排除目录名：";
            // 
            // txbExcludeDirName
            // 
            this.txbExcludeDirName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbExcludeDirName.Location = new System.Drawing.Point(86, 3);
            this.txbExcludeDirName.Name = "txbExcludeDirName";
            this.txbExcludeDirName.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeDirName.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(308, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "排除绝对目录：";
            // 
            // txbExcludeFullDir
            // 
            this.txbExcludeFullDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbExcludeFullDir.Location = new System.Drawing.Point(403, 3);
            this.txbExcludeFullDir.Name = "txbExcludeFullDir";
            this.txbExcludeFullDir.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeFullDir.TabIndex = 13;
            // 
            // lblExcludeTip
            // 
            this.lblExcludeTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExcludeTip.AutoSize = true;
            this.lblExcludeTip.ForeColor = System.Drawing.Color.Red;
            this.lblExcludeTip.Location = new System.Drawing.Point(230, 1);
            this.lblExcludeTip.Name = "lblExcludeTip";
            this.lblExcludeTip.Size = new System.Drawing.Size(29, 12);
            this.lblExcludeTip.TabIndex = 3;
            this.lblExcludeTip.Text = "提示";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(951, 221);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成的字符";
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(3, 17);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(945, 201);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // FrmGetUpdateDirectoryFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 382);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grbExclude);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmGetUpdateDirectoryFile";
            this.Text = "获取修改过的文件";
            this.Load += new System.EventHandler(this.FrmDirectoryFileString_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grbExclude.ResumeLayout(false);
            this.grbExclude.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grbExclude;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbReadPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReadPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbString;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTargetPath;
        private System.Windows.Forms.TextBox txbTargetPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbExcludeEndprx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbExcludeDirName;
        private System.Windows.Forms.TextBox txbExcludeFullDir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckbDateDir;
        private System.Windows.Forms.Label lblExcludeTip;
    }
}