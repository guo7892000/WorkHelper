namespace Breezee.Core.WinFormUI
{
    partial class MsgBox
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnMore = new System.Windows.Forms.Button();
            this.labTimer = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnErrorInfo = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.panelErrorInfo = new System.Windows.Forms.Panel();
            this.Txt_ErrorInfo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelErrorInfo.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(320, 20);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.LightBlue;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(320, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "标题";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MDS_FrmMsgBox_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MDS_FrmMsgBox_MouseMove);
            // 
            // lblMessage
            // 
            this.lblMessage.ContextMenuStrip = this.contextMenuStrip1;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(67, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(253, 77);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "1内容\r\n2\r\n3\r\n4\r\n5";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMessage.MouseEnter += new System.EventHandler(this.lblMessage_MouseEnter);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnMore);
            this.panelMain.Controls.Add(this.lblMessage);
            this.panelMain.Controls.Add(this.labTimer);
            this.panelMain.Controls.Add(this.picIcon);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 20);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(320, 77);
            this.panelMain.TabIndex = 2;
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMore.Location = new System.Drawing.Point(261, 56);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(57, 20);
            this.btnMore.TabIndex = 8;
            this.btnMore.Text = "更多...";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Visible = false;
            // 
            // labTimer
            // 
            this.labTimer.AutoSize = true;
            this.labTimer.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labTimer.ForeColor = System.Drawing.Color.Red;
            this.labTimer.Location = new System.Drawing.Point(358, 32);
            this.labTimer.Name = "labTimer";
            this.labTimer.Size = new System.Drawing.Size(30, 16);
            this.labTimer.TabIndex = 7;
            this.labTimer.Text = "0秒";
            this.labTimer.Visible = false;
            // 
            // picIcon
            // 
            this.picIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picIcon.Location = new System.Drawing.Point(0, 0);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(67, 77);
            this.picIcon.TabIndex = 3;
            this.picIcon.TabStop = false;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.LightBlue;
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnNo);
            this.panelBottom.Controls.Add(this.btnErrorInfo);
            this.panelBottom.Controls.Add(this.btnConfirm);
            this.panelBottom.Controls.Add(this.btnYes);
            this.panelBottom.Controls.Add(this.btnRetry);
            this.panelBottom.Controls.Add(this.btnIgnore);
            this.panelBottom.Controls.Add(this.btnAbort);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottom.Location = new System.Drawing.Point(0, 97);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(320, 30);
            this.panelBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(215, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 20);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.Location = new System.Drawing.Point(179, 7);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(56, 20);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "否";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnErrorInfo
            // 
            this.btnErrorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnErrorInfo.Location = new System.Drawing.Point(261, 7);
            this.btnErrorInfo.Name = "btnErrorInfo";
            this.btnErrorInfo.Size = new System.Drawing.Size(56, 20);
            this.btnErrorInfo.TabIndex = 7;
            this.btnErrorInfo.Text = "详细..";
            this.btnErrorInfo.UseVisualStyleBackColor = true;
            this.btnErrorInfo.Click += new System.EventHandler(this.btnErrorInfo_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(140, 7);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(56, 20);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.Location = new System.Drawing.Point(99, 7);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(56, 20);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "是";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetry.Location = new System.Drawing.Point(54, 7);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(56, 20);
            this.btnRetry.TabIndex = 1;
            this.btnRetry.Text = "重试";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIgnore.Location = new System.Drawing.Point(14, 7);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(56, 20);
            this.btnIgnore.TabIndex = 3;
            this.btnIgnore.Text = "忽略";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(-23, 7);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(56, 20);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "中断";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // panelErrorInfo
            // 
            this.panelErrorInfo.Controls.Add(this.Txt_ErrorInfo);
            this.panelErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelErrorInfo.Location = new System.Drawing.Point(0, 127);
            this.panelErrorInfo.Name = "panelErrorInfo";
            this.panelErrorInfo.Size = new System.Drawing.Size(320, 123);
            this.panelErrorInfo.TabIndex = 4;
            // 
            // Txt_ErrorInfo
            // 
            this.Txt_ErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_ErrorInfo.Location = new System.Drawing.Point(0, 0);
            this.Txt_ErrorInfo.Multiline = true;
            this.Txt_ErrorInfo.Name = "Txt_ErrorInfo";
            this.Txt_ErrorInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_ErrorInfo.Size = new System.Drawing.Size(320, 123);
            this.Txt_ErrorInfo.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(152, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 250);
            this.Controls.Add(this.panelErrorInfo);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MDS_FrmMsgBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDS_FrmMsgBox_FormClosing);
            this.Load += new System.EventHandler(this.MDS_FrmMsgBox_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MDS_FrmMsgBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MDS_FrmMsgBox_MouseMove);
            this.panelTop.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelErrorInfo.ResumeLayout(false);
            this.panelErrorInfo.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnErrorInfo;
        private System.Windows.Forms.Panel panelErrorInfo;
        private System.Windows.Forms.Label labTimer;
        private System.Windows.Forms.TextBox Txt_ErrorInfo;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
    }
}