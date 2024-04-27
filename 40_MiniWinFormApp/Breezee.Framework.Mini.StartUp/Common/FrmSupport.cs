using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Breezee.Framework.Mini.StartUp
{
    public partial class FrmSupport : BaseForm
    {
        public FrmSupport()
        {
            InitializeComponent();
        }

        private void Support_Load(object sender, EventArgs e)
        {
            //lblInfo.Text = Properties.Resources.Support_Remark;
            //pbWeixin.Image = Properties.Resources.MyWeixinGatherMin;
            pbWeixin.SizeMode = PictureBoxSizeMode.StretchImage;
            MaximizeBox = false;
            MinimizeBox = false;
        }
    }
}
