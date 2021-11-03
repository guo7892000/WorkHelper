using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserSet = Breezee.Framework.BaseUI.Properties.Settings;

using Breezee.Global.Entity;
using Breezee.Global.IOC;
using Breezee.Framework.BaseUI;
using Breezee.Framework.Interface;
using Breezee.Framework.Tool;
using Breezee.Framework.Mini.Entity;
using Breezee.Global.Context;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 用户环境设置
    /// </summary>
    public partial class FrmUserEnvironmentSet : BaseForm
    {
        #region 全局变量
        //表变量
        DataTable _dtColorNum;//数值颜色类型表
        DataTable _dtColorName;//名称颜色类型表
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmUserEnvironmentSet()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUserSkinSet_Load(object sender, EventArgs e)
        {
            DataTable dtFormSksy = MiniKeyValue.GetValue(MiniKeyEnum.FORM_SKIN_TYPE);
            DataTable dtSaveTip = MiniKeyValue.GetValue(MiniKeyEnum.SAVE_TIP);
            _dtColorNum = MiniKeyValue.GetValue(MiniKeyEnum.RBG_VALUE);
            _dtColorName = MiniKeyValue.GetValue(MiniKeyEnum.RBG_NAME);
            //绑定下拉框
            MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbSkinTypeMain, dtFormSksy, false, true);
            MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbSkinTypeCommon, dtFormSksy, false, true);
            MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbMsgType, dtSaveTip, false, true);
            cbbMsgType.SelectedValue = UserSet.Default.SavePromptType;
            //主窗体皮肤类型
            cbbSkinTypeMain.SelectedValue = UserSet.Default.MainSkinType;
            if(UserSet.Default.MainSkinType.Equals("0") || UserSet.Default.MainSkinType.Equals("1"))
            {
                cbbColorMain.SelectedValue = UserSet.Default.MainSkinValue;
            }
            else
            {
                txbSkinValueMain.Text = UserSet.Default.MainSkinValue;
            }

            //子窗体
            if (UserSet.Default.CommonSkinType.Equals("0") || UserSet.Default.CommonSkinType.Equals("1"))
            {
                cbbColorCommon.SelectedValue = UserSet.Default.CommonSkinValue;
            }
            else
            {
                txbSkinValueCommon.Text = UserSet.Default.CommonSkinValue;
            }            
        } 
        #endregion

        #region 保存按钮事件
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSave_Click(object sender, EventArgs e)
        {
            //主窗体皮肤设置
            if (cbbSkinTypeMain.SelectedValue != null)
            {
                UserSet.Default.MainSkinType = cbbSkinTypeMain.SelectedValue.ToString();
                #region 主窗体皮肤设置保存
                switch (cbbSkinTypeMain.SelectedValue.ToString())
                {
                    case "0": //默认
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        UserSet.Default.MainSkinValue = cbbColorMain.SelectedValue.ToString();
                        break;
                    case "1": //选择颜色
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        UserSet.Default.MainSkinValue = cbbColorMain.SelectedValue.ToString();
                        break;
                    case "2": //自定义颜色
                        string strRBG = txbSkinValueMain.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strRBG))
                        {
                            ShowInfo("请选择一个自定义颜色！");
                            return;
                        }
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorDefine;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strRBG;
                        UserSet.Default.MainSkinValue = strRBG;
                        break;
                    case "3": //选择图片
                        string strPicPath = txbSkinValueMain.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strPicPath))
                        {
                            ShowInfo("请选择一个图片！");
                            return;
                        }
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Picture;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strPicPath;
                        UserSet.Default.MainSkinValue = strPicPath;
                        break;
                    default:
                        break;
                }
                #endregion
            }
            if (cbbSkinTypeCommon.SelectedValue != null)
            {
                UserSet.Default.CommonSkinType = cbbSkinTypeCommon.SelectedValue.ToString();
                #region 子窗体皮肤设置保存
                switch (cbbSkinTypeCommon.SelectedValue.ToString())
                {
                    case "0": //默认
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        UserSet.Default.CommonSkinValue = cbbColorCommon.SelectedValue.ToString();
                        break;
                    case "1": //选择颜色
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        UserSet.Default.CommonSkinValue = cbbColorCommon.SelectedValue.ToString();
                        break;
                    case "2": //自定义颜色
                        string strRBG = txbSkinValueCommon.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strRBG))
                        {
                            ShowInfo("请选择一个自定义颜色！");
                            return;
                        }
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorDefine;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strRBG;
                        UserSet.Default.CommonSkinValue = strRBG;
                        break;
                    case "3": //选择图片
                        string strPicPath = txbSkinValueCommon.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strPicPath))
                        {
                            ShowInfo("请选择一个图片！");
                            return;
                        }
                        GlobalContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Picture;
                        GlobalContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strPicPath;
                        UserSet.Default.CommonSkinValue = strPicPath;
                        break;
                    default:
                        break;
                }
                #endregion
            }

            if (cbbMsgType.SelectedValue != null)
            {
                #region 保存提示方式
                string strSavePromptType = cbbMsgType.SelectedValue.ToString();
                UserSet.Default.SavePromptType = strSavePromptType;
                if (strSavePromptType == "2")//仅显示，不弹出
                {
                    GlobalContext.UserEnvConfig.SaveMsgPrompt = SaveMsgPromptTypeEnum.OnlyPromptNotPopup;
                }
                else //弹出提示
                {
                    GlobalContext.UserEnvConfig.SaveMsgPrompt = SaveMsgPromptTypeEnum.PopupPrompt;
                }
                #endregion
            }
            UserSet.Default.Save();
            ShowInfo("【用户环境设置】保存成功！");
            DialogResult = System.Windows.Forms.DialogResult.OK; 
        } 
        #endregion

        #region 关闭按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion

        #region 主窗体皮肤类型下拉框选择变化事件
        /// <summary>
        /// 主窗体皮肤类型下拉框选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbSkinTypeMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSkinTypeMain.SelectedValue == null)
            {
                return;
            }

            switch (cbbSkinTypeMain.SelectedValue.ToString())
            {
                case "0": //无
                    cbbColorMain.Enabled = true;
                    txbSkinValueMain.Text = "";
                    MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbColorMain, _dtColorNum, false, true);
                    break;
                case "1": //选择颜色
                    cbbColorMain.Enabled = true;
                    txbSkinValueMain.Text = "";
                    MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbColorMain, _dtColorName, false, true);
                    break;
                case "2": //自定义颜色
                    cbbColorMain.Enabled = false;
                    //txbSkinValueMain.Text = "";
                    break;
                case "3": //选择图片
                    cbbColorMain.Enabled = false;
                    //txbSkinValueMain.Text = "";
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 子窗体皮肤类型下拉框选择变化事件
        /// <summary>
        /// 子窗体皮肤类型下拉框选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbSkinTypeCommon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSkinTypeCommon.SelectedValue == null)
            {
                return;
            }
            switch (cbbSkinTypeCommon.SelectedValue.ToString())
            {
                case "0": //无
                    cbbColorCommon.Enabled = true;
                    txbSkinValueCommon.Text = "";
                    MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbColorCommon, _dtColorNum, false, true);
                    break;
                case "1": //选择颜色
                    cbbColorCommon.Enabled = true;
                    txbSkinValueCommon.Text = "";
                    MiniKeyValue.KeyValue.BindXmlTypeValueDropDownList(cbbColorCommon, _dtColorName, false, true);
                    break;
                case "2": //自定义颜色
                    cbbColorCommon.Enabled = false;
                    break;
                case "3": //选择图片
                    cbbColorCommon.Enabled = false;
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 主窗体选择自定义颜色或图片
        /// <summary>
        /// 主窗体选择自定义颜色或图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPicMain_Click(object sender, EventArgs e)
        {
            if (cbbSkinTypeMain.SelectedValue == null)
            {
                return;
            }
            switch (cbbSkinTypeMain.SelectedValue.ToString())
            {
                case "0": //无
                    break;
                case "1": //选择颜色
                    break;
                case "2": //自定义颜色
                    if (this.cdlSelectColor.ShowDialog() == DialogResult.OK)
                    {
                        this.txbSkinValueMain.Text = cdlSelectColor.Color.ToArgb().ToString();
                    }
                    break;
                case "3": //选择图片
                    opfSelectPic.Filter = "(*.jpg,*.gif,*.bmp,*.png,*.jpeg)|*.JPG;*.GIF;*.BMP;*.PNG;*.JPEG";
                    if (this.opfSelectPic.ShowDialog() == DialogResult.OK)
                    {
                        this.txbSkinValueMain.Text = opfSelectPic.FileName;
                    }
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 子窗体选择自定义颜色或图片
        /// <summary>
        /// 子窗体选择自定义颜色或图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPicCommon_Click(object sender, EventArgs e)
        {
            if (cbbSkinTypeCommon.SelectedValue == null)
            {
                return;
            }
            switch (cbbSkinTypeCommon.SelectedValue.ToString())
            {
                case "0": //无
                    break;
                case "1": //选择颜色
                    break;
                case "2": //自定义颜色
                    if (this.cdlSelectColor.ShowDialog() == DialogResult.OK)
                    {
                        this.txbSkinValueCommon.Text = cdlSelectColor.Color.ToArgb().ToString();
                    }
                    break;
                case "3": //选择图片
                    opfSelectPic.Filter = "(*.jpg,*.gif,*.bmp,*.png,*.jpeg)|*.JPG;*.GIF;*.BMP;*.PNG;*.JPEG";
                    if (this.opfSelectPic.ShowDialog() == DialogResult.OK)
                    {
                        this.txbSkinValueCommon.Text = opfSelectPic.FileName;
                    }
                    break;
                default:
                    break;
            }
        } 
        #endregion
    }
}
