namespace Breezee.Framework.Mini.StartUp
{
    partial class LockForm
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
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(126, 117);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(132, 25);
            this.txtPass.TabIndex = 0;
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(81, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "系统已被锁定，请输入解锁密码";
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(140, 150);
            this.btnUnlock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(100, 29);
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "确定";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(126, 83);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(132, 25);
            this.txtUserName.TabIndex = 3;
            // 
            // LockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(382, 201);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPass);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "LockForm";
            this.Text = "系统锁定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockForm_FormClosing);
            this.Load += new System.EventHandler(this.LockForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.TextBox txtUserName;
    }
}