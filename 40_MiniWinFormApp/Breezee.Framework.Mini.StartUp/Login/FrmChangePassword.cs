using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.Core.Tool;
using Breezee.Framework.Mini.IBLL;
using Breezee.Core.WinFormUI;
using Breezee.Core.IOC;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public partial class FrmChangePassword : BaseForm
    {
        #region 变量
        IMiniLogin _ILogin = ContainerContext.Container.Resolve<IMiniLogin>();
        //非空集合
        IDictionary<string, Control> _dicSaveNotNullControl; //非空判断的控件名称与控件关系表
        int iFaileNum = 0;//失败次数
        #endregion

        #region 构造函数
        public FrmChangePassword()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体加载事件
        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            _dicSaveNotNullControl = new Dictionary<string, Control>();
            _dicSaveNotNullControl["旧密码"] = txbOldPassword;
            _dicSaveNotNullControl["新密码"] = txbNewPassword;
            _dicSaveNotNullControl["确认新密码"] = txbNewPasswordConfirm;
            txbOldPassword.Focus();
        }
        #endregion

        #region 保存按钮事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            //非空提示
            string strInfo = _dicSaveNotNullControl.JudgeNotNull(true);
            if (!string.IsNullOrEmpty(strInfo))
            {
                ShowInfo("保存失败！具体信息见详细。\n", "提示", strInfo);
                return;
            }

            string strOldPwd = txbOldPassword.Text.Trim();
            string strNewPwd = txbNewPassword.Text.Trim();

            if (string.Compare(strNewPwd,txbNewPasswordConfirm.Text.Trim())!=0)
            {
                ShowInfo("新密码与确认新密码不一致，请重新录入！", "提示");
                txbNewPassword.Focus();
                txbNewPasswordConfirm.Text = "";
                return;
            }
            //验证密码是否正确
            _dicString = new Dictionary<string,string>();
            _dicString[IMiniLogin.Login_InDicKey.UserCode] = _loginUser.USER_CODE;
            IDictionary<string, object> dicResult = _ILogin.Login(_dicString);
            DataTable dtUser = dicResult[StaticConstant.FRA_QUERY_RESULT] as DataTable;
            if (dtUser == null || dtUser.Rows.Count == 0)
            {
                ShowInfo("旧密码错误，请重新录入！", "提示"); //当用户不存在时也提示这个
                return;
            }
            //验证密码是否正确
            if (!EncryptHelper.ValidatePassword(strOldPwd, dtUser.Rows[0][DT_SYS_USER.USER_PASSWORD].ToString()))
            {
                ShowInfo( "旧密码错误，请重新录入！", "提示");
                iFaileNum += 1;
                lblInfo.Text = "失败次数：" + iFaileNum.ToString();
                if (iFaileNum >= 3)
                {
                    btnSave.Enabled = false;
                }
                return;
            }
            //更新密码
            _dicString = new Dictionary<string, string>();
            _dicString[IMiniLogin.UpdateUserPasswd_InDicKey.USER_ID] = _loginUser.USER_ID;
            _dicString[IMiniLogin.UpdateUserPasswd_InDicKey.NEW_USER_PASSWORD] = EncryptHelper.CreateHash(strNewPwd); //创建哈希盐
            int iEff = _ILogin.UpdateUserPasswd(_dicString);
            if (iEff > 0)
            {
                ShowInfo("密码修改成功！", "提示");
                this.Close();
            }
            else
            {
                ShowInfo("密码修改失败！", "提示");
            }
        }
        #endregion

        #region 关闭按钮事件
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion


    }
}
