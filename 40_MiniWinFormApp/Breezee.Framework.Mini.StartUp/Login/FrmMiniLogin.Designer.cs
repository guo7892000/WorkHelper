namespace Breezee.Framework.Mini.StartUp
{
    partial class FrmMiniLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMiniLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblLoginTipInfo = new System.Windows.Forms.Label();
            this.cbbUserName = new System.Windows.Forms.ComboBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.pictureBoxPassword = new System.Windows.Forms.PictureBox();
            this.pictureBoxUsername = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.PictureBox();
            this.chbSavePwd = new System.Windows.Forms.CheckBox();
            this.ckbAutoLogin = new System.Windows.Forms.CheckBox();
            this.pnl_All = new System.Windows.Forms.Panel();
            this.btnFormMin = new System.Windows.Forms.PictureBox();
            this.btnFormClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).BeginInit();
            this.pnl_All.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFormMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFormClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlLogin.Controls.Add(this.lblLoginTipInfo);
            this.pnlLogin.Controls.Add(this.cbbUserName);
            this.pnlLogin.Controls.Add(this.txbPassword);
            this.pnlLogin.Controls.Add(this.pictureBoxPassword);
            this.pnlLogin.Controls.Add(this.pictureBoxUsername);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.chbSavePwd);
            this.pnlLogin.Controls.Add(this.ckbAutoLogin);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogin.Location = new System.Drawing.Point(0, 273);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(425, 157);
            this.pnlLogin.TabIndex = 106;
            this.pnlLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            // 
            // lblLoginTipInfo
            // 
            this.lblLoginTipInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoginTipInfo.AutoSize = true;
            this.lblLoginTipInfo.Location = new System.Drawing.Point(257, 144);
            this.lblLoginTipInfo.Name = "lblLoginTipInfo";
            this.lblLoginTipInfo.Size = new System.Drawing.Size(89, 12);
            this.lblLoginTipInfo.TabIndex = 107;
            this.lblLoginTipInfo.Text = "请输入登录账号";
            // 
            // cbbUserName
            // 
            this.cbbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbUserName.FormattingEnabled = true;
            this.cbbUserName.IntegralHeight = false;
            this.cbbUserName.Location = new System.Drawing.Point(266, 10);
            this.cbbUserName.Name = "cbbUserName";
            this.cbbUserName.Size = new System.Drawing.Size(146, 20);
            this.cbbUserName.TabIndex = 107;
            this.toolTip1.SetToolTip(this.cbbUserName, "默认用户名：xtadmin");
            this.cbbUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbbUserName_KeyUp);
            // 
            // txbPassword
            // 
            this.txbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPassword.BackColor = System.Drawing.Color.White;
            this.txbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPassword.Font = new System.Drawing.Font("宋体", 14F);
            this.txbPassword.Location = new System.Drawing.Point(266, 50);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(140, 22);
            this.txbPassword.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txbPassword, "默认密码：1");
            this.txbPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txbPassword_KeyUp);
            // 
            // pictureBoxPassword
            // 
            this.pictureBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPassword.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPassword.Image")));
            this.pictureBoxPassword.Location = new System.Drawing.Point(260, 46);
            this.pictureBoxPassword.Name = "pictureBoxPassword";
            this.pictureBoxPassword.Size = new System.Drawing.Size(154, 29);
            this.pictureBoxPassword.TabIndex = 105;
            this.pictureBoxPassword.TabStop = false;
            // 
            // pictureBoxUsername
            // 
            this.pictureBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxUsername.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUsername.Image")));
            this.pictureBoxUsername.Location = new System.Drawing.Point(260, 6);
            this.pictureBoxUsername.Name = "pictureBoxUsername";
            this.pictureBoxUsername.Size = new System.Drawing.Size(154, 29);
            this.pictureBoxUsername.TabIndex = 105;
            this.pictureBoxUsername.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.ErrorImage = null;
            this.btnLogin.Location = new System.Drawing.Point(258, 109);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(164, 29);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.TabStop = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chbSavePwd
            // 
            this.chbSavePwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSavePwd.AutoSize = true;
            this.chbSavePwd.BackColor = System.Drawing.Color.Transparent;
            this.chbSavePwd.CausesValidation = false;
            this.chbSavePwd.Location = new System.Drawing.Point(339, 86);
            this.chbSavePwd.Name = "chbSavePwd";
            this.chbSavePwd.Size = new System.Drawing.Size(72, 16);
            this.chbSavePwd.TabIndex = 103;
            this.chbSavePwd.Text = "记住密码";
            this.chbSavePwd.UseVisualStyleBackColor = true;
            // 
            // ckbAutoLogin
            // 
            this.ckbAutoLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbAutoLogin.AutoSize = true;
            this.ckbAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.ckbAutoLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ckbAutoLogin.CausesValidation = false;
            this.ckbAutoLogin.Checked = true;
            this.ckbAutoLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogin.Location = new System.Drawing.Point(261, 87);
            this.ckbAutoLogin.Name = "ckbAutoLogin";
            this.ckbAutoLogin.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoLogin.TabIndex = 103;
            this.ckbAutoLogin.Text = "自动登录";
            this.ckbAutoLogin.UseVisualStyleBackColor = true;
            this.ckbAutoLogin.Visible = false;
            // 
            // pnl_All
            // 
            this.pnl_All.BackColor = System.Drawing.Color.Transparent;
            this.pnl_All.Controls.Add(this.btnFormMin);
            this.pnl_All.Controls.Add(this.btnFormClose);
            this.pnl_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_All.Location = new System.Drawing.Point(0, 0);
            this.pnl_All.Name = "pnl_All";
            this.pnl_All.Size = new System.Drawing.Size(425, 273);
            this.pnl_All.TabIndex = 108;
            this.pnl_All.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            // 
            // btnFormMin
            // 
            this.btnFormMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormMin.BackColor = System.Drawing.Color.Transparent;
            this.btnFormMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFormMin.BackgroundImage")));
            this.btnFormMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFormMin.Location = new System.Drawing.Point(353, 3);
            this.btnFormMin.Name = "btnFormMin";
            this.btnFormMin.Size = new System.Drawing.Size(26, 19);
            this.btnFormMin.TabIndex = 102;
            this.btnFormMin.TabStop = false;
            this.btnFormMin.Click += new System.EventHandler(this.btnFormMin_Click);
            // 
            // btnFormClose
            // 
            this.btnFormClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormClose.BackColor = System.Drawing.Color.Transparent;
            this.btnFormClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFormClose.BackgroundImage")));
            this.btnFormClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFormClose.Location = new System.Drawing.Point(385, 3);
            this.btnFormClose.Name = "btnFormClose";
            this.btnFormClose.Size = new System.Drawing.Size(26, 19);
            this.btnFormClose.TabIndex = 103;
            this.btnFormClose.TabStop = false;
            this.btnFormClose.Click += new System.EventHandler(this.btnFormClose_Click);
            // 
            // FrmMiniLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 430);
            this.Controls.Add(this.pnl_All);
            this.Controls.Add(this.pnlLogin);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FrmMiniLogin";
            this.Text = "欢迎登录工作助手";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).EndInit();
            this.pnl_All.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnFormMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFormClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.PictureBox pictureBoxPassword;
        private System.Windows.Forms.PictureBox pictureBoxUsername;
        private System.Windows.Forms.PictureBox btnLogin;
        private System.Windows.Forms.CheckBox chbSavePwd;
        private System.Windows.Forms.CheckBox ckbAutoLogin;
        private System.Windows.Forms.ComboBox cbbUserName;
        private System.Windows.Forms.Label lblLoginTipInfo;
        private System.Windows.Forms.Panel pnl_All;
        private System.Windows.Forms.PictureBox btnFormMin;
        private System.Windows.Forms.PictureBox btnFormClose;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

