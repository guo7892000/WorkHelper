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
            this.gbGlobal = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbXmlPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnReloadFile = new System.Windows.Forms.Button();
            this.cbbCfgFile = new System.Windows.Forms.ComboBox();
            this.ckbFlowDesign = new System.Windows.Forms.CheckBox();
            this.ckbOpenPath = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txbCfgName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tapAll = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.pnlGlobalFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlGlobal = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.gbGlobal.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tapAll.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(832, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
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
            // gbGlobal
            // 
            this.gbGlobal.Controls.Add(this.tableLayoutPanel1);
            this.gbGlobal.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGlobal.Location = new System.Drawing.Point(0, 27);
            this.gbGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.gbGlobal.Name = "gbGlobal";
            this.gbGlobal.Padding = new System.Windows.Forms.Padding(2);
            this.gbGlobal.Size = new System.Drawing.Size(832, 89);
            this.gbGlobal.TabIndex = 25;
            this.gbGlobal.TabStop = false;
            this.gbGlobal.Text = "全局选项";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbXmlPath, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnReloadFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbCfgFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbFlowDesign, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbOpenPath, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbCfgName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(828, 71);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "配置文件";
            // 
            // txbXmlPath
            // 
            this.txbXmlPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbXmlPath, 6);
            this.txbXmlPath.Location = new System.Drawing.Point(97, 36);
            this.txbXmlPath.Multiline = true;
            this.txbXmlPath.Name = "txbXmlPath";
            this.txbXmlPath.Size = new System.Drawing.Size(666, 31);
            this.txbXmlPath.TabIndex = 4;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(74, 36);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(17, 27);
            this.btnSelectPath.TabIndex = 5;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.BtnSelectPath_Click);
            // 
            // btnReloadFile
            // 
            this.btnReloadFile.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReloadFile.Location = new System.Drawing.Point(3, 36);
            this.btnReloadFile.Name = "btnReloadFile";
            this.btnReloadFile.Size = new System.Drawing.Size(65, 27);
            this.btnReloadFile.TabIndex = 8;
            this.btnReloadFile.Text = "重新加载";
            this.btnReloadFile.UseVisualStyleBackColor = false;
            this.btnReloadFile.Click += new System.EventHandler(this.btnReloadFile_Click);
            // 
            // cbbCfgFile
            // 
            this.cbbCfgFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.cbbCfgFile, 2);
            this.cbbCfgFile.FormattingEnabled = true;
            this.cbbCfgFile.Location = new System.Drawing.Point(74, 6);
            this.cbbCfgFile.Name = "cbbCfgFile";
            this.cbbCfgFile.Size = new System.Drawing.Size(168, 20);
            this.cbbCfgFile.TabIndex = 4;
            this.cbbCfgFile.SelectedIndexChanged += new System.EventHandler(this.cbbCfgFile_SelectedIndexChanged);
            // 
            // ckbFlowDesign
            // 
            this.ckbFlowDesign.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbFlowDesign.AutoSize = true;
            this.ckbFlowDesign.Checked = true;
            this.ckbFlowDesign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFlowDesign.Location = new System.Drawing.Point(628, 8);
            this.ckbFlowDesign.Name = "ckbFlowDesign";
            this.ckbFlowDesign.Size = new System.Drawing.Size(72, 16);
            this.ckbFlowDesign.TabIndex = 7;
            this.ckbFlowDesign.Text = "流式布局";
            this.ckbFlowDesign.UseVisualStyleBackColor = true;
            this.ckbFlowDesign.CheckedChanged += new System.EventHandler(this.ckbFlowDesign_CheckedChanged);
            // 
            // ckbOpenPath
            // 
            this.ckbOpenPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbOpenPath.AutoSize = true;
            this.ckbOpenPath.Location = new System.Drawing.Point(527, 8);
            this.ckbOpenPath.Margin = new System.Windows.Forms.Padding(2);
            this.ckbOpenPath.Name = "ckbOpenPath";
            this.ckbOpenPath.Size = new System.Drawing.Size(96, 16);
            this.ckbOpenPath.TabIndex = 6;
            this.ckbOpenPath.Text = "点击打开目录";
            this.ckbOpenPath.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(475, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(47, 27);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txbCfgName
            // 
            this.txbCfgName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbCfgName.Location = new System.Drawing.Point(248, 6);
            this.txbCfgName.Name = "txbCfgName";
            this.txbCfgName.Size = new System.Drawing.Size(168, 21);
            this.txbCfgName.TabIndex = 10;
            this.toolTip1.SetToolTip(this.txbCfgName, "下拉框中显示的配置文件名称");
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSave.Location = new System.Drawing.Point(422, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 27);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tapAll
            // 
            this.tapAll.Controls.Add(this.tpMain);
            this.tapAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapAll.Location = new System.Drawing.Point(0, 179);
            this.tapAll.Name = "tapAll";
            this.tapAll.SelectedIndex = 0;
            this.tapAll.Size = new System.Drawing.Size(832, 305);
            this.tapAll.TabIndex = 26;
            // 
            // tpMain
            // 
            this.tpMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(824, 279);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // pnlGlobalFlow
            // 
            this.pnlGlobalFlow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGlobalFlow.Location = new System.Drawing.Point(0, 116);
            this.pnlGlobalFlow.Name = "pnlGlobalFlow";
            this.pnlGlobalFlow.Size = new System.Drawing.Size(832, 35);
            this.pnlGlobalFlow.TabIndex = 28;
            // 
            // pnlGlobal
            // 
            this.pnlGlobal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGlobal.Location = new System.Drawing.Point(0, 151);
            this.pnlGlobal.Name = "pnlGlobal";
            this.pnlGlobal.Size = new System.Drawing.Size(832, 28);
            this.pnlGlobal.TabIndex = 29;
            // 
            // FrmDBTClickCopyStringAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 484);
            this.Controls.Add(this.tapAll);
            this.Controls.Add(this.pnlGlobal);
            this.Controls.Add(this.pnlGlobalFlow);
            this.Controls.Add(this.gbGlobal);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmDBTClickCopyStringAuto";
            this.Text = "点击复制字符";
            this.Load += new System.EventHandler(this.FrmDBTClickCopyStringAuto_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbGlobal.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tapAll.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox gbGlobal;
        private System.Windows.Forms.CheckBox ckbOpenPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txbXmlPath;
        private System.Windows.Forms.ToolStripButton tsbDownLoad;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ckbFlowDesign;
        private System.Windows.Forms.Button btnReloadFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbCfgFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TabControl tapAll;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.TextBox txbCfgName;
        private System.Windows.Forms.FlowLayoutPanel pnlGlobalFlow;
        private System.Windows.Forms.Panel pnlGlobal;
    }
}