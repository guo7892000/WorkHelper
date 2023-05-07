namespace Breezee.Framework.Mini.StartUp
{
    partial class FrmMiniSystemSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMiniSystemSet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbDBInit = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEncryptInfo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbIsHide = new System.Windows.Forms.CheckBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txbEncryptAfterString = new System.Windows.Forms.TextBox();
            this.txbEncryptBeforeString = new System.Windows.Forms.TextBox();
            this.tsbMiniDbConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMiniDbConfig,
            this.tsbSave,
            this.tsbDBInit,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(600, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(71, 24);
            this.tsbSave.Text = "保存(&S)";
            this.tsbSave.Visible = false;
            this.tsbSave.Click += new System.EventHandler(this.TsbSave_Click);
            // 
            // tsbDBInit
            // 
            this.tsbDBInit.Image = ((System.Drawing.Image)(resources.GetObject("tsbDBInit.Image")));
            this.tsbDBInit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDBInit.Name = "tsbDBInit";
            this.tsbDBInit.Size = new System.Drawing.Size(104, 24);
            this.tsbDBInit.Text = "数据库初始化";
            this.tsbDBInit.Visible = false;
            this.tsbDBInit.Click += new System.EventHandler(this.TsbDBInit_Click);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.lblEncryptInfo);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.ckbIsHide);
            this.groupBox5.Controls.Add(this.btnCopy);
            this.groupBox5.Controls.Add(this.btnEncrypt);
            this.groupBox5.Controls.Add(this.txbEncryptAfterString);
            this.groupBox5.Controls.Add(this.txbEncryptBeforeString);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(600, 48);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "主框架的加密";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(322, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "加密后：";
            // 
            // lblEncryptInfo
            // 
            this.lblEncryptInfo.AutoSize = true;
            this.lblEncryptInfo.ForeColor = System.Drawing.Color.Red;
            this.lblEncryptInfo.Location = new System.Drawing.Point(106, 2);
            this.lblEncryptInfo.Name = "lblEncryptInfo";
            this.lblEncryptInfo.Size = new System.Drawing.Size(29, 12);
            this.lblEncryptInfo.TabIndex = 4;
            this.lblEncryptInfo.Text = "说明";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(7, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "加密前：";
            // 
            // ckbIsHide
            // 
            this.ckbIsHide.AutoSize = true;
            this.ckbIsHide.Location = new System.Drawing.Point(220, 19);
            this.ckbIsHide.Name = "ckbIsHide";
            this.ckbIsHide.Size = new System.Drawing.Size(48, 16);
            this.ckbIsHide.TabIndex = 2;
            this.ckbIsHide.Text = "隐藏";
            this.ckbIsHide.UseVisualStyleBackColor = true;
            this.ckbIsHide.CheckedChanged += new System.EventHandler(this.ckbIsHide_CheckedChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(540, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(42, 23);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(274, 15);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(42, 23);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txbEncryptAfterString
            // 
            this.txbEncryptAfterString.Location = new System.Drawing.Point(381, 17);
            this.txbEncryptAfterString.Name = "txbEncryptAfterString";
            this.txbEncryptAfterString.Size = new System.Drawing.Size(153, 21);
            this.txbEncryptAfterString.TabIndex = 0;
            // 
            // txbEncryptBeforeString
            // 
            this.txbEncryptBeforeString.Location = new System.Drawing.Point(64, 17);
            this.txbEncryptBeforeString.Name = "txbEncryptBeforeString";
            this.txbEncryptBeforeString.Size = new System.Drawing.Size(153, 21);
            this.txbEncryptBeforeString.TabIndex = 0;
            // 
            // tsbMiniDbConfig
            // 
            this.tsbMiniDbConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsbMiniDbConfig.Image")));
            this.tsbMiniDbConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMiniDbConfig.Name = "tsbMiniDbConfig";
            this.tsbMiniDbConfig.Size = new System.Drawing.Size(109, 24);
            this.tsbMiniDbConfig.Text = "主框架DB配置";
            this.tsbMiniDbConfig.Click += new System.EventHandler(this.tsbMiniDbConfig_Click);
            // 
            // FrmMiniSystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmMiniSystemSet";
            this.Text = "主框架系统设置";
            this.Load += new System.EventHandler(this.FrmMiniSystemSet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbDBInit;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEncryptInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbIsHide;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txbEncryptAfterString;
        private System.Windows.Forms.TextBox txbEncryptBeforeString;
        private System.Windows.Forms.ToolStripButton tsbMiniDbConfig;
    }
}