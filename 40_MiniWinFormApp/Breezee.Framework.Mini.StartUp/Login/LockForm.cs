using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 功能名称：锁定系统窗体
    /// 作者：黄国辉
    /// 日期：2013-5-6
    /// </summary>
    public partial class LockForm : BaseForm
    {
        #region 变量
        //是否通过密码验证
        bool bPassed = false;

        //当前登录用户
        LoginUserInfo mUser; 
        #endregion

        #region 默认构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LockForm()
        {
            InitializeComponent();
        } 
        #endregion

        #region 窗体关闭时事件
        /// <summary>
        /// 窗体关闭时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bPassed)
            {
                e.Cancel = true; //验证不通过，不关闭窗体
            }
        } 
        #endregion

        #region 解锁密码按钮事件
        /// <summary>
        /// 解锁密码按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string strPass = txtPass.Text.Trim();
            //查询用户信息
            //验证密码是否正确
            if (EncryptHelper.ValidatePassword(strPass, mUser.USER_PASSWORD))
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

        #region 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockForm_Load(object sender, EventArgs e)
        {
            this.mUser = WinFormContext.Instance.LoginUser;
            txtUserName.Text = mUser.USER_NAME;
        } 
        #endregion

        #region 密码框按下事件
        /// <summary>
        /// 密码框按下事件
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