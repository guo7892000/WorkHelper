using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Breezee.Framework.Mini.StartUp
{
    partial class AboutAuthor : BaseForm
    {
        public AboutAuthor()
        {
            InitializeComponent();
            this.Text = String.Format("关于 {0}", MiniStartUpAssemblyInfo.AssemblyTitle);
            this.labelProductName.Text = MiniStartUpAssemblyInfo.AssemblyProduct;
            this.labelVersion.Text = string.Format("版本 {0}", MiniStartUpAssemblyInfo.AssemblyVersion);
            this.labelCopyright.Text = MiniStartUpAssemblyInfo.AssemblyCopyright;
            this.labelCompanyName.Text = MiniStartUpAssemblyInfo.AssemblyCompany;
            this.textBoxDescription.Text = Breezee.Framework.Mini.StartUp.Properties.Resources.About_Remark;//AssemblyDescription;
        }

        #region 退出窗体
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void btnOpenDownLoad_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitee.com/breezee2000/WorkHelper/releases");
        }
    }
}
