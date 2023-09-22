namespace Breezee.WorkHelper.DBTool.UI
{
    partial class FrmDBTDefaultValueSet_D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBTDefaultValueSet_D));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbColumnName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbQueryCondition = new System.Windows.Forms.CheckBox();
            this.ckbDeleteCondition = new System.Windows.Forms.CheckBox();
            this.ckbModifyCondition = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbEnabled = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSqlServer = new System.Windows.Forms.TextBox();
            this.lblPortNO = new System.Windows.Forms.Label();
            this.txbMySql = new System.Windows.Forms.TextBox();
            this.txbID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbAdd = new System.Windows.Forms.CheckBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.txbOracle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbSQLite = new System.Windows.Forms.TextBox();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.txbPostgreSql = new System.Windows.Forms.TextBox();
            this.ckbUpdate = new System.Windows.Forms.CheckBox();
            this.txbUPDATE_CONTROL_ID = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(490, 27);
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
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 191);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编辑";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbColumnName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.ckbQueryCondition, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.ckbDeleteCondition, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.ckbModifyCondition, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ckbEnabled, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbSqlServer, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPortNO, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbMySql, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ckbAdd, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblDbName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbOracle, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbSQLite, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblServerAddr, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbPostgreSql, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbUpdate, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 171);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(51, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段名：";
            // 
            // txbColumnName
            // 
            this.txbColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbColumnName.Location = new System.Drawing.Point(110, 3);
            this.txbColumnName.Name = "txbColumnName";
            this.txbColumnName.Size = new System.Drawing.Size(124, 21);
            this.txbColumnName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "字段条件类型：";
            // 
            // ckbQueryCondition
            // 
            this.ckbQueryCondition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbQueryCondition.AutoSize = true;
            this.ckbQueryCondition.Location = new System.Drawing.Point(109, 130);
            this.ckbQueryCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbQueryCondition.Name = "ckbQueryCondition";
            this.ckbQueryCondition.Size = new System.Drawing.Size(72, 16);
            this.ckbQueryCondition.TabIndex = 8;
            this.ckbQueryCondition.Text = "查询条件";
            this.ckbQueryCondition.UseVisualStyleBackColor = true;
            // 
            // ckbDeleteCondition
            // 
            this.ckbDeleteCondition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbDeleteCondition.AutoSize = true;
            this.ckbDeleteCondition.Location = new System.Drawing.Point(358, 130);
            this.ckbDeleteCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDeleteCondition.Name = "ckbDeleteCondition";
            this.ckbDeleteCondition.Size = new System.Drawing.Size(72, 16);
            this.ckbDeleteCondition.TabIndex = 10;
            this.ckbDeleteCondition.Text = "删除条件";
            this.ckbDeleteCondition.UseVisualStyleBackColor = true;
            // 
            // ckbModifyCondition
            // 
            this.ckbModifyCondition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbModifyCondition.AutoSize = true;
            this.ckbModifyCondition.Location = new System.Drawing.Point(239, 130);
            this.ckbModifyCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbModifyCondition.Name = "ckbModifyCondition";
            this.ckbModifyCondition.Size = new System.Drawing.Size(72, 16);
            this.ckbModifyCondition.TabIndex = 9;
            this.ckbModifyCondition.Text = "修改条件";
            this.ckbModifyCondition.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(63, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "状态：";
            // 
            // ckbEnabled
            // 
            this.ckbEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbEnabled.AutoSize = true;
            this.ckbEnabled.Location = new System.Drawing.Point(109, 150);
            this.ckbEnabled.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbEnabled.Name = "ckbEnabled";
            this.ckbEnabled.Size = new System.Drawing.Size(48, 16);
            this.ckbEnabled.TabIndex = 11;
            this.ckbEnabled.Text = "启用";
            this.ckbEnabled.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(246, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "SqlServer默认值：";
            // 
            // txbSqlServer
            // 
            this.txbSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSqlServer.Location = new System.Drawing.Point(359, 57);
            this.txbSqlServer.Name = "txbSqlServer";
            this.txbSqlServer.Size = new System.Drawing.Size(124, 21);
            this.txbSqlServer.TabIndex = 4;
            // 
            // lblPortNO
            // 
            this.lblPortNO.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPortNO.AutoSize = true;
            this.lblPortNO.ForeColor = System.Drawing.Color.Black;
            this.lblPortNO.Location = new System.Drawing.Point(21, 34);
            this.lblPortNO.Name = "lblPortNO";
            this.lblPortNO.Size = new System.Drawing.Size(83, 12);
            this.lblPortNO.TabIndex = 0;
            this.lblPortNO.Text = "MySql默认值：";
            // 
            // txbMySql
            // 
            this.txbMySql.Location = new System.Drawing.Point(110, 30);
            this.txbMySql.Name = "txbMySql";
            this.txbMySql.Size = new System.Drawing.Size(124, 21);
            this.txbMySql.TabIndex = 1;
            // 
            // txbID
            // 
            this.txbID.Location = new System.Drawing.Point(3, 0);
            this.txbID.Name = "txbID";
            this.txbID.Size = new System.Drawing.Size(10, 21);
            this.txbID.TabIndex = 5;
            this.txbID.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "默认值适用语句：";
            // 
            // ckbAdd
            // 
            this.ckbAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAdd.AutoSize = true;
            this.ckbAdd.Location = new System.Drawing.Point(109, 110);
            this.ckbAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAdd.Name = "ckbAdd";
            this.ckbAdd.Size = new System.Drawing.Size(72, 16);
            this.ckbAdd.TabIndex = 6;
            this.ckbAdd.Text = "新增语句";
            this.ckbAdd.UseVisualStyleBackColor = true;
            // 
            // lblDbName
            // 
            this.lblDbName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDbName.AutoSize = true;
            this.lblDbName.ForeColor = System.Drawing.Color.Black;
            this.lblDbName.Location = new System.Drawing.Point(15, 88);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(89, 12);
            this.lblDbName.TabIndex = 0;
            this.lblDbName.Text = "Oralce默认值：";
            // 
            // txbOracle
            // 
            this.txbOracle.Location = new System.Drawing.Point(110, 84);
            this.txbOracle.Name = "txbOracle";
            this.txbOracle.Size = new System.Drawing.Size(124, 21);
            this.txbOracle.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(15, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "SQLite默认值：";
            // 
            // txbSQLite
            // 
            this.txbSQLite.Location = new System.Drawing.Point(110, 57);
            this.txbSQLite.Name = "txbSQLite";
            this.txbSQLite.Size = new System.Drawing.Size(124, 21);
            this.txbSQLite.TabIndex = 3;
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServerAddr.AutoSize = true;
            this.lblServerAddr.ForeColor = System.Drawing.Color.Black;
            this.lblServerAddr.Location = new System.Drawing.Point(240, 34);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(113, 12);
            this.lblServerAddr.TabIndex = 0;
            this.lblServerAddr.Text = "PostgreSql默认值：";
            // 
            // txbPostgreSql
            // 
            this.txbPostgreSql.Location = new System.Drawing.Point(359, 30);
            this.txbPostgreSql.Name = "txbPostgreSql";
            this.txbPostgreSql.Size = new System.Drawing.Size(124, 21);
            this.txbPostgreSql.TabIndex = 2;
            // 
            // ckbUpdate
            // 
            this.ckbUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbUpdate.AutoSize = true;
            this.ckbUpdate.Location = new System.Drawing.Point(239, 110);
            this.ckbUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbUpdate.Name = "ckbUpdate";
            this.ckbUpdate.Size = new System.Drawing.Size(72, 16);
            this.ckbUpdate.TabIndex = 7;
            this.ckbUpdate.Text = "修改语句";
            this.ckbUpdate.UseVisualStyleBackColor = true;
            // 
            // txbUPDATE_CONTROL_ID
            // 
            this.txbUPDATE_CONTROL_ID.Location = new System.Drawing.Point(19, 0);
            this.txbUPDATE_CONTROL_ID.Name = "txbUPDATE_CONTROL_ID";
            this.txbUPDATE_CONTROL_ID.Size = new System.Drawing.Size(10, 21);
            this.txbUPDATE_CONTROL_ID.TabIndex = 5;
            this.txbUPDATE_CONTROL_ID.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbID);
            this.panel1.Controls.Add(this.txbUPDATE_CONTROL_ID);
            this.panel1.Location = new System.Drawing.Point(240, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 21);
            this.panel1.TabIndex = 12;
            // 
            // FrmDBTDefaultValueSet_D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 218);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDBTDefaultValueSet_D";
            this.Text = "字段默认值与条件全局配置明细";
            this.Load += new System.EventHandler(this.FrmDBConfigSet_D_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox txbColumnName;
        private System.Windows.Forms.Label lblServerAddr;
        private System.Windows.Forms.TextBox txbPostgreSql;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbSqlServer;
        private System.Windows.Forms.TextBox txbMySql;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.TextBox txbOracle;
        private System.Windows.Forms.Label lblPortNO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbSQLite;
        private System.Windows.Forms.CheckBox ckbAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbUpdate;
        private System.Windows.Forms.CheckBox ckbQueryCondition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbDeleteCondition;
        private System.Windows.Forms.CheckBox ckbModifyCondition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbEnabled;
        private System.Windows.Forms.TextBox txbID;
        private System.Windows.Forms.TextBox txbUPDATE_CONTROL_ID;
        private System.Windows.Forms.Panel panel1;
    }
}