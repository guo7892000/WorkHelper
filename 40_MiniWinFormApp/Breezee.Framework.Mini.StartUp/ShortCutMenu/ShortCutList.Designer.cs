namespace Breezee.Framework.Mini.StartUp
{
    partial class ShortCutList
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.flpMenuList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpMenuList
            // 
            this.flpMenuList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenuList.Location = new System.Drawing.Point(0, 0);
            this.flpMenuList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpMenuList.Name = "flpMenuList";
            this.flpMenuList.Size = new System.Drawing.Size(175, 190);
            this.flpMenuList.TabIndex = 0;
            this.flpMenuList.DragDrop += new System.Windows.Forms.DragEventHandler(this.flpMenuList_DragDrop);
            this.flpMenuList.DragEnter += new System.Windows.Forms.DragEventHandler(this.flpMenuList_DragEnter);
            // 
            // ShortCutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpMenuList);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ShortCutList";
            this.Size = new System.Drawing.Size(175, 190);
            this.Load += new System.EventHandler(this.ShortCutList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpMenuList;
    }
}
