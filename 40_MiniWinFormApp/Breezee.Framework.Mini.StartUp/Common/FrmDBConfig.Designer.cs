namespace Breezee.Framework.Mini.StartUp
{
    partial class FrmDBConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBConfig));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbConnetTest = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbDBConfigCode = new System.Windows.Forms.TextBox();
            this.cbbDatabaseType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.txbServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbRemark = new System.Windows.Forms.TextBox();
            this.btnSelectDbFile = new System.Windows.Forms.Button();
            this.txbDbName = new System.Windows.Forms.TextBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.txbPortNO = new System.Windows.Forms.TextBox();
            this.lblPortNO = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txbSchemaName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbConnetTest,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(550, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(71, 24);
            this.tsbSave.Text = "保存(&S)";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbConnetTest
            // 
            this.tsbConnetTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnetTest.Image")));
            this.tsbConnetTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnetTest.Name = "tsbConnetTest";
            this.tsbConnetTest.Size = new System.Drawing.Size(56, 24);
            this.tsbConnetTest.Text = "测试";
            this.tsbConnetTest.Click += new System.EventHandler(this.tsbConnetTest_Click);
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
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 186);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(55, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(365, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "从简单起见，最好使用SQLite数据。修改后需要重启应用才能生效！";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbDBConfigCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbDatabaseType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblServerAddr, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbServerIP, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbRemark, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectDbFile, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbDbName, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDbName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbPortNO, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPortNO, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbUserName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbPassword, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbSchemaName, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 151);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置编码：";
            // 
            // txbDBConfigCode
            // 
            this.txbDBConfigCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbDBConfigCode.Location = new System.Drawing.Point(86, 3);
            this.txbDBConfigCode.Name = "txbDBConfigCode";
            this.txbDBConfigCode.Size = new System.Drawing.Size(158, 21);
            this.txbDBConfigCode.TabIndex = 0;
            // 
            // cbbDatabaseType
            // 
            this.cbbDatabaseType.FormattingEnabled = true;
            this.cbbDatabaseType.Location = new System.Drawing.Point(86, 30);
            this.cbbDatabaseType.Name = "cbbDatabaseType";
            this.cbbDatabaseType.Size = new System.Drawing.Size(158, 20);
            this.cbbDatabaseType.TabIndex = 2;
            this.cbbDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cbbDatabaseType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据库类型：";
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServerAddr.AutoSize = true;
            this.lblServerAddr.ForeColor = System.Drawing.Color.Red;
            this.lblServerAddr.Location = new System.Drawing.Point(15, 61);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(65, 12);
            this.lblServerAddr.TabIndex = 0;
            this.lblServerAddr.Text = "服务器IP：";
            // 
            // txbServerIP
            // 
            this.txbServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbServerIP.Location = new System.Drawing.Point(86, 57);
            this.txbServerIP.Name = "txbServerIP";
            this.txbServerIP.Size = new System.Drawing.Size(158, 21);
            this.txbServerIP.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(39, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "备注：";
            // 
            // txbRemark
            // 
            this.txbRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbRemark, 4);
            this.txbRemark.Location = new System.Drawing.Point(86, 111);
            this.txbRemark.Multiline = true;
            this.txbRemark.Name = "txbRemark";
            this.txbRemark.Size = new System.Drawing.Size(420, 30);
            this.txbRemark.TabIndex = 10;
            // 
            // btnSelectDbFile
            // 
            this.btnSelectDbFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectDbFile.Location = new System.Drawing.Point(250, 58);
            this.btnSelectDbFile.Name = "btnSelectDbFile";
            this.btnSelectDbFile.Size = new System.Drawing.Size(21, 19);
            this.btnSelectDbFile.TabIndex = 3;
            this.btnSelectDbFile.Text = "...";
            this.btnSelectDbFile.UseVisualStyleBackColor = true;
            this.btnSelectDbFile.Click += new System.EventHandler(this.btnSelectDbFile_Click);
            // 
            // txbDbName
            // 
            this.txbDbName.Location = new System.Drawing.Point(348, 30);
            this.txbDbName.Name = "txbDbName";
            this.txbDbName.Size = new System.Drawing.Size(158, 21);
            this.txbDbName.TabIndex = 3;
            // 
            // lblDbName
            // 
            this.lblDbName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDbName.AutoSize = true;
            this.lblDbName.ForeColor = System.Drawing.Color.Red;
            this.lblDbName.Location = new System.Drawing.Point(289, 34);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(53, 12);
            this.lblDbName.TabIndex = 0;
            this.lblDbName.Text = "数据库：";
            // 
            // txbPortNO
            // 
            this.txbPortNO.Location = new System.Drawing.Point(348, 57);
            this.txbPortNO.Name = "txbPortNO";
            this.txbPortNO.Size = new System.Drawing.Size(158, 21);
            this.txbPortNO.TabIndex = 6;
            // 
            // lblPortNO
            // 
            this.lblPortNO.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPortNO.AutoSize = true;
            this.lblPortNO.ForeColor = System.Drawing.Color.Red;
            this.lblPortNO.Location = new System.Drawing.Point(289, 61);
            this.lblPortNO.Name = "lblPortNO";
            this.lblPortNO.Size = new System.Drawing.Size(53, 12);
            this.lblPortNO.TabIndex = 0;
            this.lblPortNO.Text = "端口号：";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(27, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "用户名：";
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(86, 84);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(158, 21);
            this.txbUserName.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(301, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "密码：";
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(348, 84);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(158, 21);
            this.txbPassword.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(277, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "架构名称：";
            // 
            // txbSchemaName
            // 
            this.txbSchemaName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSchemaName.Location = new System.Drawing.Point(348, 3);
            this.txbSchemaName.Name = "txbSchemaName";
            this.txbSchemaName.Size = new System.Drawing.Size(158, 21);
            this.txbSchemaName.TabIndex = 7;
            // 
            // FrmDBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 213);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDBConfig";
            this.Text = "数据库连接配置";
            this.Load += new System.EventHandler(this.FrmDBConfigSet_D_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbDBConfigCode;
        private System.Windows.Forms.ComboBox cbbDatabaseType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblServerAddr;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.TextBox txbDbName;
        private System.Windows.Forms.TextBox txbServerIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label lblPortNO;
        private System.Windows.Forms.TextBox txbPortNO;
        private System.Windows.Forms.TextBox txbSchemaName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSelectDbFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbRemark;
        private System.Windows.Forms.ToolStripButton tsbConnetTest;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
    }
}