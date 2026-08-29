namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTScriptMerge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTScriptMerge));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbDownLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSelectPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbCharSetEncode = new System.Windows.Forms.ComboBox();
            this.ckbAutoOpen = new System.Windows.Forms.CheckBox();
            this.lblMergeInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRealReadDir = new System.Windows.Forms.Label();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.ckbDelDirBfGen = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAutoSQL,
            this.tsbDownLoad,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(933, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(72, 24);
            this.tsbAutoSQL.Text = "合并(&A)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.TsbAutoSQL_Click);
            // 
            // tsbDownLoad
            // 
            this.tsbDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownLoad.Image")));
            this.tsbDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownLoad.Name = "tsbDownLoad";
            this.tsbDownLoad.Size = new System.Drawing.Size(97, 24);
            this.tsbDownLoad.Text = "下载模板(&D)";
            this.tsbDownLoad.Click += new System.EventHandler(this.TsbDownLoad_Click);
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
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.lblMergeInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 78);
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 801F));
            this.tableLayoutPanel1.Controls.Add(this.btnSelectFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbSelectPath, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbCharSetEncode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbAutoOpen, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbDelDirBfGen, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 58);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(110, 3);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(23, 23);
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
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "配置文件选择：";
            // 
            // txbSelectPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txbSelectPath, 5);
            this.txbSelectPath.Location = new System.Drawing.Point(139, 3);
            this.txbSelectPath.Multiline = true;
            this.txbSelectPath.Name = "txbSelectPath";
            this.txbSelectPath.Size = new System.Drawing.Size(624, 21);
            this.txbSelectPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "生成文件字符集：";
            // 
            // cbbCharSetEncode
            // 
            this.cbbCharSetEncode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.cbbCharSetEncode, 2);
            this.cbbCharSetEncode.FormattingEnabled = true;
            this.cbbCharSetEncode.Location = new System.Drawing.Point(110, 32);
            this.cbbCharSetEncode.Name = "cbbCharSetEncode";
            this.cbbCharSetEncode.Size = new System.Drawing.Size(121, 20);
            this.cbbCharSetEncode.TabIndex = 7;
            // 
            // ckbAutoOpen
            // 
            this.ckbAutoOpen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAutoOpen.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.ckbAutoOpen, 2);
            this.ckbAutoOpen.Location = new System.Drawing.Point(237, 34);
            this.ckbAutoOpen.Name = "ckbAutoOpen";
            this.ckbAutoOpen.Size = new System.Drawing.Size(120, 16);
            this.ckbAutoOpen.TabIndex = 5;
            this.ckbAutoOpen.Text = "自动打开生成目录";
            this.ckbAutoOpen.UseVisualStyleBackColor = true;
            // 
            // lblMergeInfo
            // 
            this.lblMergeInfo.AutoSize = true;
            this.lblMergeInfo.ForeColor = System.Drawing.Color.Red;
            this.lblMergeInfo.Location = new System.Drawing.Point(46, 0);
            this.lblMergeInfo.Name = "lblMergeInfo";
            this.lblMergeInfo.Size = new System.Drawing.Size(29, 12);
            this.lblMergeInfo.TabIndex = 0;
            this.lblMergeInfo.Text = "说明";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRealReadDir);
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(933, 417);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成的文件清单";
            // 
            // lblRealReadDir
            // 
            this.lblRealReadDir.AutoSize = true;
            this.lblRealReadDir.ForeColor = System.Drawing.Color.Red;
            this.lblRealReadDir.Location = new System.Drawing.Point(164, 0);
            this.lblRealReadDir.Name = "lblRealReadDir";
            this.lblRealReadDir.Size = new System.Drawing.Size(89, 12);
            this.lblRealReadDir.TabIndex = 1;
            this.lblRealReadDir.Text = "实际读取的目录";
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(3, 17);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(927, 397);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // ckbDelDirBfGen
            // 
            this.ckbDelDirBfGen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbDelDirBfGen.AutoSize = true;
            this.ckbDelDirBfGen.Location = new System.Drawing.Point(363, 34);
            this.ckbDelDirBfGen.Name = "ckbDelDirBfGen";
            this.ckbDelDirBfGen.Size = new System.Drawing.Size(222, 16);
            this.ckbDelDirBfGen.TabIndex = 5;
            this.ckbDelDirBfGen.Text = "生成前清空【900_FinalScript】目录";
            this.ckbDelDirBfGen.UseVisualStyleBackColor = true;
            // 
            // FrmDBTScriptMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 522);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmDBTScriptMerge";
            this.Text = "脚本合并";
            this.Load += new System.EventHandler(this.FrmDBTScriptMerge_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton tsbDownLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbCharSetEncode;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbString;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbSelectPath;
        private System.Windows.Forms.CheckBox ckbAutoOpen;
        private System.Windows.Forms.Label lblMergeInfo;
        private System.Windows.Forms.Label lblRealReadDir;
        private System.Windows.Forms.CheckBox ckbDelDirBfGen;
    }
}