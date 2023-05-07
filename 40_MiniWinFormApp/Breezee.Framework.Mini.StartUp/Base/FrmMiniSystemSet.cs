using Breezee.Core.WinFormUI;
using Breezee.Framework.Mini.IBLL;
using Breezee.Core.IOC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Tool;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.Interface;

namespace Breezee.Framework.Mini.StartUp
{
    public partial class FrmMiniSystemSet : BaseForm
    {
        IMiniSystem _IMiniSystem;
        public FrmMiniSystemSet()
        {
            InitializeComponent();
        }

        private void FrmMiniSystemSet_Load(object sender, EventArgs e)
        {
            _IMiniSystem = ContainerContext.Container.Resolve<IMiniSystem>();
            //密码相关
            txbEncryptBeforeString.PasswordChar = '*';
            ckbIsHide.Checked = true;
            txbEncryptAfterString.ReadOnly = true;
            lblEncryptInfo.Text = "一般功能已支持加密，如果想后台更新密码，可使用本功能得到加密后字符，然后手工更新数据库！";
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {

        }

        private void TsbDBInit_Click(object sender, EventArgs e)
        {
            if (ShowOkCancel("数据库（Mini）将还原到首次登录时状态，请自行做好数据备份。确定继续？") != DialogResult.OK) return;
            _IMiniSystem.InitDB(_dicQuery).SafeGetDictionary();
            ShowInfo("初始化成功！！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbMiniDbConfig_Click(object sender, EventArgs e)
        {
            FrmDBConfig frm = new FrmDBConfig(MiniGlobalValue.DataAccessConfigKey, GlobalContext.PathDb(), GlobalFile.DbConfigMini, "主框架的数据库连接配置");
            frm.ShowDialog();
        }

        #region 加密相关
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string sWaitEnt = txbEncryptBeforeString.Text.Trim();
            if (string.IsNullOrEmpty(sWaitEnt))
            {
                ShowInfo("请先输入要加密的字符！");
                return;
            }
            txbEncryptAfterString.Text = EncryptHelper.AESEncrypt(sWaitEnt, MiniGlobalValue.MiniDesEncryKey, MiniGlobalValue.MiniDesEncryVector);
        }

        private void ckbIsHide_CheckedChanged(object sender, EventArgs e)
        {
            /**WinForm中TextBox控件的PasswordChar属性默认是没有设置的或者说没有开启密码模式。取消PasswordChar的设置的三种方法：
             * this .textBox1.PasswordChar =  new  char ();
             * this .textBox1.PasswordChar =  '\0' ;
             * this .textBox1.PasswordChar =  default ( char );
             */
            txbEncryptBeforeString.PasswordChar = ckbIsHide.Checked ? '*' : new char();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txbEncryptAfterString.Text);
        }
        #endregion
    }
}
