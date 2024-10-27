namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    partial class FrmDBTSplitString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTSplitString));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAutoSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.grbSplitCharCfg = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbFirstSplitBySpace = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.ckbEveryDataTrim = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreEmptyData = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbExample = new System.Windows.Forms.ComboBox();
            this.ckbNewLine = new System.Windows.Forms.CheckBox();
            this.lblSplitCharList = new System.Windows.Forms.Label();
            this.txbSplitList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbSplitListSplitChar = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbSplitList = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbFormat = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckbOneRowToOneColumn = new System.Windows.Forms.CheckBox();
            this.btnSplit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbbSplitModule = new System.Windows.Forms.ComboBox();
            this.lblSplitModule = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbSplitType = new System.Windows.Forms.ComboBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.grbSplitList = new System.Windows.Forms.GroupBox();
            this.dgvSplitChar = new System.Windows.Forms.DataGridView();
            this.cmsSplitChar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClean = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.grbGetSplitChar = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGetSplitChar = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.grbSplitCharCfg.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.grbSplitList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitChar)).BeginInit();
            this.cmsSplitChar.SuspendLayout();
            this.grbGetSplitChar.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1247, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAutoSQL
            // 
            this.tsbAutoSQL.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoSQL.Image")));
            this.tsbAutoSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoSQL.Name = "tsbAutoSQL";
            this.tsbAutoSQL.Size = new System.Drawing.Size(71, 24);
            this.tsbAutoSQL.Text = "生成(&S)";
            this.tsbAutoSQL.Click += new System.EventHandler(this.tsbAutoSQL_Click);
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
            // grbSplitCharCfg
            // 
            this.grbSplitCharCfg.Controls.Add(this.tableLayoutPanel1);
            this.grbSplitCharCfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbSplitCharCfg.Location = new System.Drawing.Point(0, 82);
            this.grbSplitCharCfg.Margin = new System.Windows.Forms.Padding(2);
            this.grbSplitCharCfg.Name = "grbSplitCharCfg";
            this.grbSplitCharCfg.Padding = new System.Windows.Forms.Padding(2);
            this.grbSplitCharCfg.Size = new System.Drawing.Size(1247, 47);
            this.grbSplitCharCfg.TabIndex = 24;
            this.grbSplitCharCfg.TabStop = false;
            this.grbSplitCharCfg.Text = "分隔配置";
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ckbFirstSplitBySpace, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReset, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbEveryDataTrim, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbIgnoreEmptyData, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1243, 31);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // ckbFirstSplitBySpace
            // 
            this.ckbFirstSplitBySpace.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbFirstSplitBySpace.AutoSize = true;
            this.ckbFirstSplitBySpace.Location = new System.Drawing.Point(3, 6);
            this.ckbFirstSplitBySpace.Name = "ckbFirstSplitBySpace";
            this.ckbFirstSplitBySpace.Size = new System.Drawing.Size(108, 16);
            this.ckbFirstSplitBySpace.TabIndex = 10;
            this.ckbFirstSplitBySpace.Text = "首次按空白分隔";
            this.ckbFirstSplitBySpace.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(405, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(53, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ckbEveryDataTrim
            // 
            this.ckbEveryDataTrim.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbEveryDataTrim.AutoSize = true;
            this.ckbEveryDataTrim.Checked = true;
            this.ckbEveryDataTrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEveryDataTrim.Location = new System.Drawing.Point(255, 6);
            this.ckbEveryDataTrim.Name = "ckbEveryDataTrim";
            this.ckbEveryDataTrim.Size = new System.Drawing.Size(144, 16);
            this.ckbEveryDataTrim.TabIndex = 9;
            this.ckbEveryDataTrim.Text = "每项剔除前后空白字符";
            this.ckbEveryDataTrim.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreEmptyData
            // 
            this.ckbIgnoreEmptyData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbIgnoreEmptyData.AutoSize = true;
            this.ckbIgnoreEmptyData.Location = new System.Drawing.Point(117, 6);
            this.ckbIgnoreEmptyData.Name = "ckbIgnoreEmptyData";
            this.ckbIgnoreEmptyData.Size = new System.Drawing.Size(132, 16);
            this.ckbIgnoreEmptyData.TabIndex = 8;
            this.ckbIgnoreEmptyData.Text = "忽略分隔后的空数据";
            this.ckbIgnoreEmptyData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "示例：";
            // 
            // cbbExample
            // 
            this.cbbExample.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbExample.FormattingEnabled = true;
            this.cbbExample.Location = new System.Drawing.Point(426, 3);
            this.cbbExample.Name = "cbbExample";
            this.cbbExample.Size = new System.Drawing.Size(156, 25);
            this.cbbExample.TabIndex = 4;
            this.cbbExample.SelectedIndexChanged += new System.EventHandler(this.cbbExample_SelectedIndexChanged);
            // 
            // ckbNewLine
            // 
            this.ckbNewLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbNewLine.AutoSize = true;
            this.ckbNewLine.Checked = true;
            this.ckbNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNewLine.Location = new System.Drawing.Point(111, 23);
            this.ckbNewLine.Name = "ckbNewLine";
            this.ckbNewLine.Size = new System.Drawing.Size(72, 16);
            this.ckbNewLine.TabIndex = 10;
            this.ckbNewLine.Text = "结果换行";
            this.ckbNewLine.UseVisualStyleBackColor = true;
            // 
            // lblSplitCharList
            // 
            this.lblSplitCharList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSplitCharList.AutoSize = true;
            this.lblSplitCharList.ForeColor = System.Drawing.Color.Red;
            this.lblSplitCharList.Location = new System.Drawing.Point(2, 6);
            this.lblSplitCharList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSplitCharList.Name = "lblSplitCharList";
            this.lblSplitCharList.Size = new System.Drawing.Size(65, 12);
            this.lblSplitCharList.TabIndex = 0;
            this.lblSplitCharList.Text = "分隔符列表";
            // 
            // txbSplitList
            // 
            this.txbSplitList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.SetColumnSpan(this.txbSplitList, 3);
            this.txbSplitList.Location = new System.Drawing.Point(71, 2);
            this.txbSplitList.Margin = new System.Windows.Forms.Padding(2);
            this.txbSplitList.Name = "txbSplitList";
            this.txbSplitList.Size = new System.Drawing.Size(199, 21);
            this.txbSplitList.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(50, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "以";
            // 
            // txbSplitListSplitChar
            // 
            this.txbSplitListSplitChar.Location = new System.Drawing.Point(71, 27);
            this.txbSplitListSplitChar.Margin = new System.Windows.Forms.Padding(2);
            this.txbSplitListSplitChar.Name = "txbSplitListSplitChar";
            this.txbSplitListSplitChar.Size = new System.Drawing.Size(59, 21);
            this.txbSplitListSplitChar.TabIndex = 2;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(93, 0);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(77, 12);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "字符分隔拼接";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbSplitList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(369, 238);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要分隔的字符（集合）";
            // 
            // rtbSplitList
            // 
            this.rtbSplitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSplitList.Location = new System.Drawing.Point(2, 16);
            this.rtbSplitList.Margin = new System.Windows.Forms.Padding(2);
            this.rtbSplitList.Name = "rtbSplitList";
            this.rtbSplitList.Size = new System.Drawing.Size(365, 220);
            this.rtbSplitList.TabIndex = 3;
            this.rtbSplitList.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbFormat);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 48);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(581, 96);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "要拼接的字符格式（输入）";
            // 
            // rtbFormat
            // 
            this.rtbFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbFormat.Location = new System.Drawing.Point(2, 16);
            this.rtbFormat.Margin = new System.Windows.Forms.Padding(2);
            this.rtbFormat.Name = "rtbFormat";
            this.rtbFormat.Size = new System.Drawing.Size(577, 78);
            this.rtbFormat.TabIndex = 5;
            this.rtbFormat.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rtbOutput);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 48);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(581, 284);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "转换后的字符（输出）";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(2, 16);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(2);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(577, 266);
            this.rtbOutput.TabIndex = 6;
            this.rtbOutput.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(954, 480);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.TabIndex = 28;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer3.Size = new System.Drawing.Size(369, 480);
            this.splitContainer3.SplitterDistance = 238;
            this.splitContainer3.TabIndex = 26;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvData);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(369, 238);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "分拆后结果";
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 17);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(363, 218);
            this.dgvData.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(581, 480);
            this.splitContainer2.SplitterDistance = 144;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ckbOneRowToOneColumn);
            this.groupBox6.Controls.Add(this.btnSplit);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(581, 48);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "分拆操作";
            // 
            // ckbOneRowToOneColumn
            // 
            this.ckbOneRowToOneColumn.AutoSize = true;
            this.ckbOneRowToOneColumn.Location = new System.Drawing.Point(115, 19);
            this.ckbOneRowToOneColumn.Name = "ckbOneRowToOneColumn";
            this.ckbOneRowToOneColumn.Size = new System.Drawing.Size(108, 16);
            this.ckbOneRowToOneColumn.TabIndex = 1;
            this.ckbOneRowToOneColumn.Text = "单行合并为单列";
            this.ckbOneRowToOneColumn.UseVisualStyleBackColor = true;
            // 
            // btnSplit
            // 
            this.btnSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSplit.Location = new System.Drawing.Point(22, 14);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(76, 27);
            this.btnSplit.TabIndex = 0;
            this.btnSplit.Text = "分拆字符";
            this.btnSplit.UseVisualStyleBackColor = false;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConvert);
            this.groupBox1.Controls.Add(this.ckbNewLine);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 48);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成操作";
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnConvert.Location = new System.Drawing.Point(22, 18);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(62, 25);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "生成";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel2);
            this.groupBox7.Controls.Add(this.lblInfo);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(0, 27);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1247, 55);
            this.groupBox7.TabIndex = 29;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "选项";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cbbSplitModule, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSplitModule, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbSplitType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbExample, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1241, 38);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cbbSplitModule
            // 
            this.cbbSplitModule.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbSplitModule.FormattingEnabled = true;
            this.cbbSplitModule.Location = new System.Drawing.Point(263, 3);
            this.cbbSplitModule.Name = "cbbSplitModule";
            this.cbbSplitModule.Size = new System.Drawing.Size(110, 25);
            this.cbbSplitModule.TabIndex = 7;
            // 
            // lblSplitModule
            // 
            this.lblSplitModule.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSplitModule.AutoSize = true;
            this.lblSplitModule.ForeColor = System.Drawing.Color.Red;
            this.lblSplitModule.Location = new System.Drawing.Point(193, 9);
            this.lblSplitModule.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSplitModule.Name = "lblSplitModule";
            this.lblSplitModule.Size = new System.Drawing.Size(65, 12);
            this.lblSplitModule.TabIndex = 6;
            this.lblSplitModule.Text = "分隔模式：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(2, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "分隔类型：";
            // 
            // cbbSplitType
            // 
            this.cbbSplitType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbSplitType.FormattingEnabled = true;
            this.cbbSplitType.Location = new System.Drawing.Point(72, 3);
            this.cbbSplitType.Name = "cbbSplitType";
            this.cbbSplitType.Size = new System.Drawing.Size(116, 25);
            this.cbbSplitType.TabIndex = 4;
            this.cbbSplitType.SelectedIndexChanged += new System.EventHandler(this.cbbSplitType_SelectedIndexChanged);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 129);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.grbSplitList);
            this.splitContainer4.Panel1.Controls.Add(this.grbGetSplitChar);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer4.Size = new System.Drawing.Size(1247, 480);
            this.splitContainer4.SplitterDistance = 289;
            this.splitContainer4.TabIndex = 30;
            // 
            // grbSplitList
            // 
            this.grbSplitList.Controls.Add(this.dgvSplitChar);
            this.grbSplitList.Controls.Add(this.label1);
            this.grbSplitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSplitList.Location = new System.Drawing.Point(0, 80);
            this.grbSplitList.Name = "grbSplitList";
            this.grbSplitList.Size = new System.Drawing.Size(289, 400);
            this.grbSplitList.TabIndex = 1;
            this.grbSplitList.TabStop = false;
            this.grbSplitList.Text = "分隔符列表";
            // 
            // dgvSplitChar
            // 
            this.dgvSplitChar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSplitChar.ContextMenuStrip = this.cmsSplitChar;
            this.dgvSplitChar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSplitChar.Location = new System.Drawing.Point(3, 17);
            this.dgvSplitChar.Name = "dgvSplitChar";
            this.dgvSplitChar.RowTemplate.Height = 23;
            this.dgvSplitChar.Size = new System.Drawing.Size(283, 380);
            this.dgvSplitChar.TabIndex = 1;
            // 
            // cmsSplitChar
            // 
            this.cmsSplitChar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClean});
            this.cmsSplitChar.Name = "cmsSplitChar";
            this.cmsSplitChar.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiClean
            // 
            this.tsmiClean.Name = "tsmiClean";
            this.tsmiClean.Size = new System.Drawing.Size(100, 22);
            this.tsmiClean.Text = "清除";
            this.tsmiClean.Click += new System.EventHandler(this.tsmiClean_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(114, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "支持网格中录入";
            // 
            // grbGetSplitChar
            // 
            this.grbGetSplitChar.Controls.Add(this.tableLayoutPanel3);
            this.grbGetSplitChar.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbGetSplitChar.Location = new System.Drawing.Point(0, 0);
            this.grbGetSplitChar.Name = "grbGetSplitChar";
            this.grbGetSplitChar.Size = new System.Drawing.Size(289, 80);
            this.grbGetSplitChar.TabIndex = 0;
            this.grbGetSplitChar.TabStop = false;
            this.grbGetSplitChar.Text = "分隔符生成";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblSplitCharList, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txbSplitListSplitChar, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txbSplitList, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnGetSplitChar, 3, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(283, 58);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "字符拆分";
            // 
            // btnGetSplitChar
            // 
            this.btnGetSplitChar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnGetSplitChar.Location = new System.Drawing.Point(194, 28);
            this.btnGetSplitChar.Name = "btnGetSplitChar";
            this.btnGetSplitChar.Size = new System.Drawing.Size(75, 23);
            this.btnGetSplitChar.TabIndex = 4;
            this.btnGetSplitChar.Text = "获取分隔符";
            this.btnGetSplitChar.UseVisualStyleBackColor = false;
            this.btnGetSplitChar.Click += new System.EventHandler(this.btnGetSplitChar_Click);
            // 
            // FrmDBTSplitString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 609);
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.grbSplitCharCfg);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmDBTSplitString";
            this.Text = "分隔拼接字符";
            this.Load += new System.EventHandler(this.FrmDBTExchangeStringPlace_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbSplitCharCfg.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.grbSplitList.ResumeLayout(false);
            this.grbSplitList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitChar)).EndInit();
            this.cmsSplitChar.ResumeLayout(false);
            this.grbGetSplitChar.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAutoSQL;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.GroupBox grbSplitCharCfg;
        private System.Windows.Forms.TextBox txbSplitList;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblSplitCharList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rtbSplitList;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.RichTextBox rtbFormat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txbSplitListSplitChar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbExample;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox ckbIgnoreEmptyData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ckbEveryDataTrim;
        private System.Windows.Forms.CheckBox ckbNewLine;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbSplitType;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox grbGetSplitChar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox grbSplitList;
        private System.Windows.Forms.DataGridView dgvSplitChar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGetSplitChar;
        private System.Windows.Forms.CheckBox ckbFirstSplitBySpace;
        private System.Windows.Forms.ComboBox cbbSplitModule;
        private System.Windows.Forms.Label lblSplitModule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbOneRowToOneColumn;
        private System.Windows.Forms.ContextMenuStrip cmsSplitChar;
        private System.Windows.Forms.ToolStripMenuItem tsmiClean;
        private System.Windows.Forms.Label label1;
    }
}