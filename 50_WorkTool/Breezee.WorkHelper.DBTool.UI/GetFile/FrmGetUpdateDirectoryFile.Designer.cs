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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckbNowModify = new System.Windows.Forms.CheckBox();
            this.ckbNowAdd = new System.Windows.Forms.CheckBox();
            this.ckbIncludeCommit = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.txbEmail = new System.Windows.Forms.TextBox();
            this.cbbDirType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckbSetBeginAsLastSaveEnd = new System.Windows.Forms.CheckBox();
            this.ckbEndToNow = new System.Windows.Forms.CheckBox();
            this.ckbSaveEndTime = new System.Windows.Forms.CheckBox();
            this.ckbDateDir = new System.Windows.Forms.CheckBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txbReadPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReadPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTargetPath = new System.Windows.Forms.Button();
            this.txbTargetPath = new System.Windows.Forms.TextBox();
            this.grbExclude = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txbExcludeEndprx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbExcludeDirName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbExcludeFullDir = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txbExcludeFile = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txbExcludeFullFile = new System.Windows.Forms.TextBox();
            this.lblExcludeTip = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbString = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(937, 27);
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
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.cbbDirType);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.ckbSetBeginAsLastSaveEnd);
            this.groupBox2.Controls.Add(this.ckbEndToNow);
            this.groupBox2.Controls.Add(this.ckbSaveEndTime);
            this.groupBox2.Controls.Add(this.ckbDateDir);
            this.groupBox2.Controls.Add(this.dtpEnd);
            this.groupBox2.Controls.Add(this.txbReadPath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnReadPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpBegin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnTargetPath);
            this.groupBox2.Controls.Add(this.txbTargetPath);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(937, 158);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读取与生成配置";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ckbNowModify);
            this.panel2.Controls.Add(this.ckbNowAdd);
            this.panel2.Controls.Add(this.ckbIncludeCommit);
            this.panel2.Location = new System.Drawing.Point(265, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 25);
            this.panel2.TabIndex = 19;
            // 
            // ckbNowModify
            // 
            this.ckbNowModify.AutoSize = true;
            this.ckbNowModify.Checked = true;
            this.ckbNowModify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNowModify.Location = new System.Drawing.Point(2, 5);
            this.ckbNowModify.Name = "ckbNowModify";
            this.ckbNowModify.Size = new System.Drawing.Size(84, 16);
            this.ckbNowModify.TabIndex = 18;
            this.ckbNowModify.Text = "含正在修改";
            this.ckbNowModify.UseVisualStyleBackColor = true;
            // 
            // ckbNowAdd
            // 
            this.ckbNowAdd.AutoSize = true;
            this.ckbNowAdd.Checked = true;
            this.ckbNowAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNowAdd.Location = new System.Drawing.Point(92, 5);
            this.ckbNowAdd.Name = "ckbNowAdd";
            this.ckbNowAdd.Size = new System.Drawing.Size(72, 16);
            this.ckbNowAdd.TabIndex = 18;
            this.ckbNowAdd.Text = "含将新增";
            this.ckbNowAdd.UseVisualStyleBackColor = true;
            // 
            // ckbIncludeCommit
            // 
            this.ckbIncludeCommit.AutoSize = true;
            this.ckbIncludeCommit.Checked = true;
            this.ckbIncludeCommit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbIncludeCommit.Location = new System.Drawing.Point(170, 5);
            this.ckbIncludeCommit.Name = "ckbIncludeCommit";
            this.ckbIncludeCommit.Size = new System.Drawing.Size(84, 16);
            this.ckbIncludeCommit.TabIndex = 15;
            this.ckbIncludeCommit.Text = "含之前提交";
            this.ckbIncludeCommit.UseVisualStyleBackColor = true;
            this.ckbIncludeCommit.CheckedChanged += new System.EventHandler(this.ckbIncludeCommit_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.txbUserName);
            this.panel1.Controls.Add(this.txbEmail);
            this.panel1.Location = new System.Drawing.Point(524, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 25);
            this.panel1.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(231, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "用户名：";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(4, 7);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(47, 12);
            this.lblEmail.TabIndex = 17;
            this.lblEmail.Text = "Email：";
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(288, 3);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(106, 21);
            this.txbUserName.TabIndex = 16;
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(58, 2);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(167, 21);
            this.txbEmail.TabIndex = 16;
            // 
            // cbbDirType
            // 
            this.cbbDirType.FormattingEnabled = true;
            this.cbbDirType.Location = new System.Drawing.Point(106, 131);
            this.cbbDirType.Name = "cbbDirType";
            this.cbbDirType.Size = new System.Drawing.Size(153, 20);
            this.cbbDirType.TabIndex = 14;
            this.cbbDirType.SelectedIndexChanged += new System.EventHandler(this.cbbDirType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(23, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "源目录类型：";
            // 
            // ckbSetBeginAsLastSaveEnd
            // 
            this.ckbSetBeginAsLastSaveEnd.AutoSize = true;
            this.ckbSetBeginAsLastSaveEnd.Location = new System.Drawing.Point(265, 80);
            this.ckbSetBeginAsLastSaveEnd.Name = "ckbSetBeginAsLastSaveEnd";
            this.ckbSetBeginAsLastSaveEnd.Size = new System.Drawing.Size(168, 16);
            this.ckbSetBeginAsLastSaveEnd.TabIndex = 13;
            this.ckbSetBeginAsLastSaveEnd.Text = "从上次保存的结束时间开始";
            this.ckbSetBeginAsLastSaveEnd.UseVisualStyleBackColor = true;
            this.ckbSetBeginAsLastSaveEnd.CheckedChanged += new System.EventHandler(this.ckbSetBeginAsLastSaveEnd_CheckedChanged);
            // 
            // ckbEndToNow
            // 
            this.ckbEndToNow.AutoSize = true;
            this.ckbEndToNow.Location = new System.Drawing.Point(265, 107);
            this.ckbEndToNow.Name = "ckbEndToNow";
            this.ckbEndToNow.Size = new System.Drawing.Size(168, 16);
            this.ckbEndToNow.TabIndex = 13;
            this.ckbEndToNow.Text = "获取时结束时间取当前时间";
            this.ckbEndToNow.UseVisualStyleBackColor = true;
            this.ckbEndToNow.CheckedChanged += new System.EventHandler(this.ckbEndToNow_CheckedChanged);
            // 
            // ckbSaveEndTime
            // 
            this.ckbSaveEndTime.AutoSize = true;
            this.ckbSaveEndTime.Location = new System.Drawing.Point(452, 107);
            this.ckbSaveEndTime.Name = "ckbSaveEndTime";
            this.ckbSaveEndTime.Size = new System.Drawing.Size(96, 16);
            this.ckbSaveEndTime.TabIndex = 13;
            this.ckbSaveEndTime.Text = "保存结束时间";
            this.ckbSaveEndTime.UseVisualStyleBackColor = true;
            // 
            // ckbDateDir
            // 
            this.ckbDateDir.AutoSize = true;
            this.ckbDateDir.Location = new System.Drawing.Point(630, 49);
            this.ckbDateDir.Name = "ckbDateDir";
            this.ckbDateDir.Size = new System.Drawing.Size(96, 16);
            this.ckbDateDir.TabIndex = 13;
            this.ckbDateDir.Text = "生成日期目录";
            this.ckbDateDir.UseVisualStyleBackColor = true;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(106, 104);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(153, 21);
            this.dtpEnd.TabIndex = 12;
            // 
            // txbReadPath
            // 
            this.txbReadPath.Location = new System.Drawing.Point(106, 17);
            this.txbReadPath.Multiline = true;
            this.txbReadPath.Name = "txbReadPath";
            this.txbReadPath.Size = new System.Drawing.Size(516, 23);
            this.txbReadPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "读取目录：";
            // 
            // btnReadPath
            // 
            this.btnReadPath.Location = new System.Drawing.Point(81, 19);
            this.btnReadPath.Name = "btnReadPath";
            this.btnReadPath.Size = new System.Drawing.Size(19, 19);
            this.btnReadPath.TabIndex = 2;
            this.btnReadPath.Text = "...";
            this.btnReadPath.UseVisualStyleBackColor = true;
            this.btnReadPath.Click += new System.EventHandler(this.btnReadPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(71, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "至：";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(106, 77);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(153, 21);
            this.dtpBegin.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(13, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "生成目录：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(23, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "修改时间从：";
            // 
            // btnTargetPath
            // 
            this.btnTargetPath.Location = new System.Drawing.Point(81, 46);
            this.btnTargetPath.Name = "btnTargetPath";
            this.btnTargetPath.Size = new System.Drawing.Size(19, 19);
            this.btnTargetPath.TabIndex = 2;
            this.btnTargetPath.Text = "...";
            this.btnTargetPath.UseVisualStyleBackColor = true;
            this.btnTargetPath.Click += new System.EventHandler(this.btnTargetPath_Click);
            // 
            // txbTargetPath
            // 
            this.txbTargetPath.Location = new System.Drawing.Point(106, 46);
            this.txbTargetPath.Multiline = true;
            this.txbTargetPath.Name = "txbTargetPath";
            this.txbTargetPath.Size = new System.Drawing.Size(516, 23);
            this.txbTargetPath.TabIndex = 1;
            // 
            // grbExclude
            // 
            this.grbExclude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.grbExclude.Controls.Add(this.tableLayoutPanel1);
            this.grbExclude.Controls.Add(this.lblExcludeTip);
            this.grbExclude.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbExclude.ForeColor = System.Drawing.Color.Red;
            this.grbExclude.Location = new System.Drawing.Point(0, 185);
            this.grbExclude.Name = "grbExclude";
            this.grbExclude.Size = new System.Drawing.Size(937, 75);
            this.grbExclude.TabIndex = 32;
            this.grbExclude.TabStop = false;
            this.grbExclude.Text = "排除目录或文件选项";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 319F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeEndprx, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeDirName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeFullDir, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeFile, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbExcludeFullFile, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(931, 55);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(637, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "排除后缀：";
            // 
            // txbExcludeEndprx
            // 
            this.txbExcludeEndprx.Location = new System.Drawing.Point(708, 3);
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
            this.txbExcludeFullDir.Location = new System.Drawing.Point(415, 3);
            this.txbExcludeFullDir.Name = "txbExcludeFullDir";
            this.txbExcludeFullDir.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeFullDir.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(3, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "排除文件名：";
            // 
            // txbExcludeFile
            // 
            this.txbExcludeFile.Location = new System.Drawing.Point(86, 30);
            this.txbExcludeFile.Name = "txbExcludeFile";
            this.txbExcludeFile.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeFile.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(308, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "排除绝对文件名：";
            // 
            // txbExcludeFullFile
            // 
            this.txbExcludeFullFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbExcludeFullFile.Location = new System.Drawing.Point(415, 30);
            this.txbExcludeFullFile.Name = "txbExcludeFullFile";
            this.txbExcludeFullFile.Size = new System.Drawing.Size(216, 21);
            this.txbExcludeFullFile.TabIndex = 13;
            // 
            // lblExcludeTip
            // 
            this.lblExcludeTip.AutoSize = true;
            this.lblExcludeTip.ForeColor = System.Drawing.Color.Red;
            this.lblExcludeTip.Location = new System.Drawing.Point(230, 2);
            this.lblExcludeTip.Name = "lblExcludeTip";
            this.lblExcludeTip.Size = new System.Drawing.Size(29, 12);
            this.lblExcludeTip.TabIndex = 3;
            this.lblExcludeTip.Text = "提示";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(937, 242);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成的字符";
            // 
            // rtbString
            // 
            this.rtbString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbString.Location = new System.Drawing.Point(3, 17);
            this.rtbString.Name = "rtbString";
            this.rtbString.Size = new System.Drawing.Size(931, 222);
            this.rtbString.TabIndex = 0;
            this.rtbString.Text = "";
            // 
            // FrmGetUpdateDirectoryFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 502);
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
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTargetPath;
        private System.Windows.Forms.TextBox txbTargetPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckbDateDir;
        private System.Windows.Forms.Label lblExcludeTip;
        private System.Windows.Forms.ComboBox cbbDirType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbSetBeginAsLastSaveEnd;
        private System.Windows.Forms.CheckBox ckbSaveEndTime;
        private System.Windows.Forms.CheckBox ckbEndToNow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.CheckBox ckbIncludeCommit;
        private System.Windows.Forms.CheckBox ckbNowAdd;
        private System.Windows.Forms.CheckBox ckbNowModify;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txbExcludeFile;
        private System.Windows.Forms.TextBox txbExcludeDirName;
        private System.Windows.Forms.TextBox txbExcludeEndprx;
        private System.Windows.Forms.TextBox txbExcludeFullDir;
        private System.Windows.Forms.TextBox txbExcludeFullFile;
    }
}