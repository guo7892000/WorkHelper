﻿namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBConfigSet_D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBConfigSet_D));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbDBConfigCode = new System.Windows.Forms.TextBox();
            this.cbbDatabaseType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.txbServerIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.txbDbName = new System.Windows.Forms.TextBox();
            this.lblPortNO = new System.Windows.Forms.Label();
            this.txbPortNO = new System.Windows.Forms.TextBox();
            this.txbSchemaName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblLoginType = new System.Windows.Forms.Label();
            this.cbbLoginType = new System.Windows.Forms.ComboBox();
            this.btnSelectDbFile = new System.Windows.Forms.Button();
            this.txbID = new System.Windows.Forms.TextBox();
            this.txbUPDATE_CONTROL_ID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbDBConfigName = new System.Windows.Forms.TextBox();
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
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(796, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(82, 24);
            this.tsbSave.Text = "保存(&S)";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
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
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(796, 214);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编辑";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbDBConfigCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbDatabaseType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblServerAddr, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbServerIP, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbUserName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbPassword, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDbName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbDbName, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPortNO, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbPortNO, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbSchemaName, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblLoginType, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbLoginType, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectDbFile, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbID, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbUPDATE_CONTROL_ID, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbRemark, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbDBConfigName, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 189);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置编码：";
            // 
            // txbDBConfigCode
            // 
            this.txbDBConfigCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbDBConfigCode, 3);
            this.txbDBConfigCode.Location = new System.Drawing.Point(109, 4);
            this.txbDBConfigCode.Margin = new System.Windows.Forms.Padding(4);
            this.txbDBConfigCode.Name = "txbDBConfigCode";
            this.txbDBConfigCode.Size = new System.Drawing.Size(256, 25);
            this.txbDBConfigCode.TabIndex = 0;
            // 
            // cbbDatabaseType
            // 
            this.cbbDatabaseType.FormattingEnabled = true;
            this.cbbDatabaseType.Location = new System.Drawing.Point(109, 37);
            this.cbbDatabaseType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDatabaseType.Name = "cbbDatabaseType";
            this.cbbDatabaseType.Size = new System.Drawing.Size(132, 23);
            this.cbbDatabaseType.TabIndex = 2;
            this.cbbDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cbbDatabaseType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据库类型：";
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServerAddr.AutoSize = true;
            this.lblServerAddr.ForeColor = System.Drawing.Color.Red;
            this.lblServerAddr.Location = new System.Drawing.Point(18, 77);
            this.lblServerAddr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(83, 15);
            this.lblServerAddr.TabIndex = 0;
            this.lblServerAddr.Text = "服务器IP：";
            // 
            // txbServerIP
            // 
            this.txbServerIP.Location = new System.Drawing.Point(109, 70);
            this.txbServerIP.Margin = new System.Windows.Forms.Padding(4);
            this.txbServerIP.Name = "txbServerIP";
            this.txbServerIP.Size = new System.Drawing.Size(132, 25);
            this.txbServerIP.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(34, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "用户名：";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(313, 112);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "密码：";
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(109, 107);
            this.txbUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(132, 25);
            this.txbUserName.TabIndex = 8;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(373, 107);
            this.txbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(132, 25);
            this.txbPassword.TabIndex = 9;
            // 
            // lblDbName
            // 
            this.lblDbName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDbName.AutoSize = true;
            this.lblDbName.ForeColor = System.Drawing.Color.Red;
            this.lblDbName.Location = new System.Drawing.Point(298, 42);
            this.lblDbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(67, 15);
            this.lblDbName.TabIndex = 0;
            this.lblDbName.Text = "数据库：";
            // 
            // txbDbName
            // 
            this.txbDbName.Location = new System.Drawing.Point(373, 37);
            this.txbDbName.Margin = new System.Windows.Forms.Padding(4);
            this.txbDbName.Name = "txbDbName";
            this.txbDbName.Size = new System.Drawing.Size(132, 25);
            this.txbDbName.TabIndex = 3;
            // 
            // lblPortNO
            // 
            this.lblPortNO.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPortNO.AutoSize = true;
            this.lblPortNO.ForeColor = System.Drawing.Color.Red;
            this.lblPortNO.Location = new System.Drawing.Point(298, 77);
            this.lblPortNO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPortNO.Name = "lblPortNO";
            this.lblPortNO.Size = new System.Drawing.Size(67, 15);
            this.lblPortNO.TabIndex = 0;
            this.lblPortNO.Text = "端口号：";
            // 
            // txbPortNO
            // 
            this.txbPortNO.Location = new System.Drawing.Point(373, 70);
            this.txbPortNO.Margin = new System.Windows.Forms.Padding(4);
            this.txbPortNO.Name = "txbPortNO";
            this.txbPortNO.Size = new System.Drawing.Size(132, 25);
            this.txbPortNO.TabIndex = 6;
            // 
            // txbSchemaName
            // 
            this.txbSchemaName.Location = new System.Drawing.Point(603, 70);
            this.txbSchemaName.Margin = new System.Windows.Forms.Padding(4);
            this.txbSchemaName.Name = "txbSchemaName";
            this.txbSchemaName.Size = new System.Drawing.Size(132, 25);
            this.txbSchemaName.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(513, 77);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "架构名称：";
            // 
            // lblLoginType
            // 
            this.lblLoginType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLoginType.AutoSize = true;
            this.lblLoginType.ForeColor = System.Drawing.Color.Black;
            this.lblLoginType.Location = new System.Drawing.Point(513, 42);
            this.lblLoginType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoginType.Name = "lblLoginType";
            this.lblLoginType.Size = new System.Drawing.Size(82, 15);
            this.lblLoginType.TabIndex = 0;
            this.lblLoginType.Text = "登录方式：";
            // 
            // cbbLoginType
            // 
            this.cbbLoginType.FormattingEnabled = true;
            this.cbbLoginType.Location = new System.Drawing.Point(603, 37);
            this.cbbLoginType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbLoginType.Name = "cbbLoginType";
            this.cbbLoginType.Size = new System.Drawing.Size(132, 23);
            this.cbbLoginType.TabIndex = 4;
            this.cbbLoginType.SelectedIndexChanged += new System.EventHandler(this.cbbDatabaseType_SelectedIndexChanged);
            // 
            // btnSelectDbFile
            // 
            this.btnSelectDbFile.Location = new System.Drawing.Point(249, 70);
            this.btnSelectDbFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectDbFile.Name = "btnSelectDbFile";
            this.btnSelectDbFile.Size = new System.Drawing.Size(19, 29);
            this.btnSelectDbFile.TabIndex = 3;
            this.btnSelectDbFile.Text = "...";
            this.btnSelectDbFile.UseVisualStyleBackColor = true;
            this.btnSelectDbFile.Click += new System.EventHandler(this.btnSelectDbFile_Click);
            // 
            // txbID
            // 
            this.txbID.Location = new System.Drawing.Point(773, 37);
            this.txbID.Margin = new System.Windows.Forms.Padding(4);
            this.txbID.Name = "txbID";
            this.txbID.Size = new System.Drawing.Size(12, 25);
            this.txbID.TabIndex = 1;
            this.txbID.Visible = false;
            // 
            // txbUPDATE_CONTROL_ID
            // 
            this.txbUPDATE_CONTROL_ID.Location = new System.Drawing.Point(773, 4);
            this.txbUPDATE_CONTROL_ID.Margin = new System.Windows.Forms.Padding(4);
            this.txbUPDATE_CONTROL_ID.Name = "txbUPDATE_CONTROL_ID";
            this.txbUPDATE_CONTROL_ID.Size = new System.Drawing.Size(12, 25);
            this.txbUPDATE_CONTROL_ID.TabIndex = 1;
            this.txbUPDATE_CONTROL_ID.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(49, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "备注：";
            // 
            // txbRemark
            // 
            this.txbRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbRemark, 6);
            this.txbRemark.Location = new System.Drawing.Point(109, 140);
            this.txbRemark.Margin = new System.Windows.Forms.Padding(4);
            this.txbRemark.Multiline = true;
            this.txbRemark.Name = "txbRemark";
            this.txbRemark.Size = new System.Drawing.Size(656, 36);
            this.txbRemark.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(423, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "配置名称：";
            // 
            // txbDBConfigName
            // 
            this.txbDBConfigName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbDBConfigName, 2);
            this.txbDBConfigName.Location = new System.Drawing.Point(513, 4);
            this.txbDBConfigName.Margin = new System.Windows.Forms.Padding(4);
            this.txbDBConfigName.Name = "txbDBConfigName";
            this.txbDBConfigName.Size = new System.Drawing.Size(252, 25);
            this.txbDBConfigName.TabIndex = 1;
            // 
            // FrmDBConfigSet_D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 241);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDBConfigSet_D";
            this.Text = "数据库连接配置";
            this.Load += new System.EventHandler(this.FrmDBConfigSet_D_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblLoginType;
        private System.Windows.Forms.ComboBox cbbLoginType;
        private System.Windows.Forms.Button btnSelectDbFile;
        private System.Windows.Forms.TextBox txbID;
        private System.Windows.Forms.TextBox txbUPDATE_CONTROL_ID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbDBConfigName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbRemark;
    }
}