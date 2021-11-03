using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Breezee.Global.Entity;
using Breezee.Framework.BaseUI;
using Breezee.Framework.Mini.Entity;
using Breezee.Global.Context;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// �������ƣ�����ϵͳ����
    /// ���ߣ��ƹ���
    /// ���ڣ�2013-5-6
    /// </summary>
    public partial class LockForm : BaseForm
    {
        #region ����
        //�Ƿ�ͨ��������֤
        bool bPassed = false;

        //��ǰ��¼�û�
        LoginUserInfo mUser; 
        #endregion

        #region Ĭ�Ϲ��캯��
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public LockForm()
        {
            InitializeComponent();
        } 
        #endregion

        #region ����ر�ʱ�¼�
        /// <summary>
        /// ����ر�ʱ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bPassed)
            {
                e.Cancel = true; //��֤��ͨ�������رմ���
            }
        } 
        #endregion

        #region �������밴ť�¼�
        /// <summary>
        /// �������밴ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string strPass = txtPass.Text.Trim();
            //��ѯ�û���Ϣ
            //��֤�����Ƿ���ȷ
            if (Breezee.Framework.Tool.EncryptHelper.ValidatePassword(strPass, mUser.USER_PASSWORD))
            {
                bPassed = true;
                this.Close();
            }
            else
            {
                bPassed = false;
            }
        } 
        #endregion

        #region ��������¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockForm_Load(object sender, EventArgs e)
        {
            this.mUser = GlobalContext.Instance.LoginUser;
            txtUserName.Text = mUser.USER_NAME;
        } 
        #endregion

        #region ��������¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.btnUnlock_Click(txtPass, null);
            }

        } 
        #endregion
    }
}