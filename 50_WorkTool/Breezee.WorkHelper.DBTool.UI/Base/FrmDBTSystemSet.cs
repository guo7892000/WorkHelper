using Breezee.Core.WinFormUI;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.Core.Interface;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// DBTool模块配置
    /// </summary>
    public partial class FrmDBTSystemSet : BaseForm
    {
        IDBTSystemSet _IDBTSystemSet;

        public FrmDBTSystemSet()
        {
            InitializeComponent();
        }

        private void FrmDBTSystemSet_Load(object sender, EventArgs e)
        {
            _IDBTSystemSet = ContainerContext.Container.Resolve<IDBTSystemSet>();
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
            if (ShowOkCancel("数据库（DBTool）将还原到首次登录时状态，请自行做好数据备份。确定继续？") != DialogResult.OK) return;
            _IDBTSystemSet.InitDB(_dicQuery);
            ShowInfo("初始化成功！！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
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
            txbEncryptAfterString.Text = EncryptHelper.AESEncrypt(sWaitEnt, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector);
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

        private void tsbDbToolDbSet_Click(object sender, EventArgs e)
        {
            var frm = FormCrossResolver.CreateCrossFrom<IMainCommonFormCross>("Breezee.Framework.Mini.StartUp.FrmDBConfig", new object[] {
                DBTGlobalValue.DataAccessConfigKey,GlobalContext.PathDb(),GlobalFile.DbConfigDBTool,"数据库工具的数据库连接配置"
            });
            frm.ShowDialog();
        }
    }
}
