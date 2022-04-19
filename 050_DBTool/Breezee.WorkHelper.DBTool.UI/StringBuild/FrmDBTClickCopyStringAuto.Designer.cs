namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    partial class FrmDBTClickCopyStringAuto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTClickCopyStringAuto));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDownLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.pnlAll = new System.Windows.Forms.Panel();
            this.gbGlobal = new System.Windows.Forms.GroupBox();
            this.ckbOpenPath = new System.Windows.Forms.CheckBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txbXmlPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.pnlAll.SuspendLayout();
            this.gbGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDownLoad,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(887, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDownLoad
            // 
            this.tsbDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownLoad.Image")));
            this.tsbDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownLoad.Name = "tsbDownLoad";
            this.tsbDownLoad.Size = new System.Drawing.Size(114, 24);
            this.tsbDownLoad.Text = "下载模板(&D)";
            this.tsbDownLoad.Click += new System.EventHandler(this.TsbDownLoad_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(83, 24);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.TsbExit_Click);
            // 
            // pnlAll
            // 
            this.pnlAll.Controls.Add(this.gbGlobal);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAll.Location = new System.Drawing.Point(0, 27);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(887, 462);
            this.pnlAll.TabIndex = 24;
            // 
            // gbGlobal
            // 
            this.gbGlobal.Controls.Add(this.ckbOpenPath);
            this.gbGlobal.Controls.Add(this.btnSelectPath);
            this.gbGlobal.Controls.Add(this.txbXmlPath);
            this.gbGlobal.Controls.Add(this.label3);
            this.gbGlobal.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGlobal.Location = new System.Drawing.Point(0, 0);
            this.gbGlobal.Name = "gbGlobal";
            this.gbGlobal.Size = new System.Drawing.Size(887, 84);
            this.gbGlobal.TabIndex = 25;
            this.gbGlobal.TabStop = false;
            this.gbGlobal.Text = "全局选项";
            // 
            // ckbOpenPath
            // 
            this.ckbOpenPath.AutoSize = true;
            this.ckbOpenPath.Location = new System.Drawing.Point(14, 60);
            this.ckbOpenPath.Name = "ckbOpenPath";
            this.ckbOpenPath.Size = new System.Drawing.Size(119, 19);
            this.ckbOpenPath.TabIndex = 6;
            this.ckbOpenPath.Text = "点击打开目录";
            this.ckbOpenPath.UseVisualStyleBackColor = true;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(102, 19);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(23, 34);
            this.btnSelectPath.TabIndex = 5;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.BtnSelectPath_Click);
            // 
            // txbXmlPath
            // 
            this.txbXmlPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbXmlPath.Location = new System.Drawing.Point(130, 19);
            this.txbXmlPath.Margin = new System.Windows.Forms.Padding(4);
            this.txbXmlPath.Multiline = true;
            this.txbXmlPath.Name = "txbXmlPath";
            this.txbXmlPath.Size = new System.Drawing.Size(744, 38);
            this.txbXmlPath.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "配置文件：";
            // 
            // FrmDBTClickCopyStringAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 489);
            this.Controls.Add(this.pnlAll);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "FrmDBTClickCopyStringAuto";
            this.Text = "FrmDBTClickCopyStringAuto";
            this.Load += new System.EventHandler(this.FrmDBTClickCopyStringAuto_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlAll.ResumeLayout(false);
            this.gbGlobal.ResumeLayout(false);
            this.gbGlobal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Panel pnlAll;
        private System.Windows.Forms.GroupBox gbGlobal;
        private System.Windows.Forms.CheckBox ckbOpenPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txbXmlPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbDownLoad;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}