namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDirectoryFileString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDirectoryFileString));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txbSelectPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbOutType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbShowFileDir = new System.Windows.Forms.CheckBox();
            this.cbbPathType = new System.Windows.Forms.ComboBox();
            this.ckbSetDirectoryDept = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbPre = new System.Windows.Forms.TextBox();
            this.txbEnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbOld = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txbNew = new System.Windows.Forms.TextBox();
            this.nudDept = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDept)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(1041, 27);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(114, 24);
            this.tsbAutoSQL.Text = "生成字符(&A)";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectPath);
            this.groupBox2.Controls.Add(this.txbSelectPath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1041, 78);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(64, 41);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(31, 29);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txbSelectPath
            // 
            this.txbSelectPath.Location = new System.Drawing.Point(111, 21);
            this.txbSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.txbSelectPath.Multiline = true;
            this.txbSelectPath.Name = "txbSelectPath";
            this.txbSelectPath.Size = new System.Drawing.Size(728, 48);
            this.txbSelectPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "目录选择：";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 105);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1041, 99);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbOutType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbShowFileDir, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbPathType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbSetDirectoryDept, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbPre, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbEnd, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbOld, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbNew, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudDept, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1033, 73);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "输出类型：";
            // 
            // cbbOutType
            // 
            this.cbbOutType.FormattingEnabled = true;
            this.cbbOutType.Location = new System.Drawing.Point(94, 4);
            this.cbbOutType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbOutType.Name = "cbbOutType";
            this.cbbOutType.Size = new System.Drawing.Size(160, 23);
            this.cbbOutType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(262, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "路径类型：";
            // 
            // ckbShowFileDir
            // 
            this.ckbShowFileDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbShowFileDir.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.ckbShowFileDir, 2);
            this.ckbShowFileDir.Location = new System.Drawing.Point(756, 6);
            this.ckbShowFileDir.Name = "ckbShowFileDir";
            this.ckbShowFileDir.Size = new System.Drawing.Size(134, 19);
            this.ckbShowFileDir.TabIndex = 13;
            this.ckbShowFileDir.Text = "仅显示文件目录";
            this.toolTip1.SetToolTip(this.ckbShowFileDir, "针对文件复制到相对路径使用");
            this.ckbShowFileDir.UseVisualStyleBackColor = true;
            // 
            // cbbPathType
            // 
            this.cbbPathType.FormattingEnabled = true;
            this.cbbPathType.Location = new System.Drawing.Point(352, 4);
            this.cbbPathType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbPathType.Name = "cbbPathType";
            this.cbbPathType.Size = new System.Drawing.Size(160, 23);
            this.cbbPathType.TabIndex = 5;
            // 
            // ckbSetDirectoryDept
            // 
            this.ckbSetDirectoryDept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbSetDirectoryDept.AutoSize = true;
            this.ckbSetDirectoryDept.Location = new System.Drawing.Point(520, 6);
            this.ckbSetDirectoryDept.Margin = new System.Windows.Forms.Padding(4);
            this.ckbSetDirectoryDept.Name = "ckbSetDirectoryDept";
            this.ckbSetDirectoryDept.Size = new System.Drawing.Size(89, 19);
            this.ckbSetDirectoryDept.TabIndex = 6;
            this.ckbSetDirectoryDept.Text = "目录深度";
            this.ckbSetDirectoryDept.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "前缀：";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "后缀：";
            // 
            // txbPre
            // 
            this.txbPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPre.Location = new System.Drawing.Point(94, 39);
            this.txbPre.Margin = new System.Windows.Forms.Padding(4);
            this.txbPre.Name = "txbPre";
            this.txbPre.Size = new System.Drawing.Size(160, 25);
            this.txbPre.TabIndex = 9;
            // 
            // txbEnd
            // 
            this.txbEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbEnd.Location = new System.Drawing.Point(352, 39);
            this.txbEnd.Margin = new System.Windows.Forms.Padding(4);
            this.txbEnd.Name = "txbEnd";
            this.txbEnd.Size = new System.Drawing.Size(160, 25);
            this.txbEnd.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(520, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "替换前：";
            // 
            // txbOld
            // 
            this.txbOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbOld.Location = new System.Drawing.Point(617, 39);
            this.txbOld.Margin = new System.Windows.Forms.Padding(4);
            this.txbOld.Name = "txbOld";
            this.txbOld.Size = new System.Drawing.Size(132, 25);
            this.txbOld.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(757, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "替换后：";
            // 
            // txbNew
            // 
            this.txbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbNew.Location = new System.Drawing.Point(832, 39);
            this.txbNew.Margin = new System.Windows.Forms.Padding(4);
            this.txbNew.Name = "txbNew";
            this.txbNew.Size = new System.Drawing.Size(140, 25);
            this.txbNew.TabIndex = 12;
            // 
            // nudDept
            // 
            this.nudDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDept.Location = new System.Drawing.Point(616, 3);
            this.nudDept.Name = "nudDept";
            this.nudDept.Size = new System.Drawing.Size(134, 25);
            this.nudDept.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 204);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1041, 274);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成的字符";
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(4, 22);
            this.rtbString.Margin = new System.Windows.Forms.Padding(4);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(1033, 248);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // FrmDirectoryFileString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 478);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDirectoryFileString";
            this.Text = "目录文件路径字符生成";
            this.Load += new System.EventHandler(this.FrmDirectoryFileString_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDept)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbOutType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbPathType;
        private System.Windows.Forms.CheckBox ckbSetDirectoryDept;
        private System.Windows.Forms.TextBox txbSelectPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbPre;
        private System.Windows.Forms.TextBox txbEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbOld;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbNew;
        private System.Windows.Forms.NumericUpDown nudDept;
        private System.Windows.Forms.CheckBox ckbShowFileDir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbString;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}