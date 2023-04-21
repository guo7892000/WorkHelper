using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.WinFormUI;
using System.IO;

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
            cbbSkinTypeMain.BindXmlTypeValueDropDownList(dtFormSksy, false, true);
            cbbSkinTypeCommon.BindXmlTypeValueDropDownList(dtFormSksy, false, true);
            cbbMsgType.BindXmlTypeValueDropDownList(dtSaveTip, false, true);
            cbbMsgType.SelectedValue = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.SavePromptType); 
            //主窗体皮肤类型
            string sMainSkinType = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinType);
            cbbSkinTypeMain.SelectedValue = sMainSkinType;
            if (sMainSkinType.Equals("0") || sMainSkinType.Equals("1"))
            {
                cbbColorMain.SelectedValue = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinValue);
            }
            else
            {
                txbSkinValueMain.Text = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.MainSkinValue);
            }

            //子窗体
            string sCommonSkinType = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.CommonSkinType);
            if (sCommonSkinType.Equals("0") || sCommonSkinType.Equals("1"))
            {
                cbbColorCommon.SelectedValue = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.CommonSkinValue);
            }
            else
            {
                txbSkinValueCommon.Text = WinFormConfig.Instance.Get(WinFormConfig.WinFormConfigString.CommonSkinValue);
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
                WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.MainSkinType, cbbSkinTypeMain.SelectedValue.ToString());
                #region 主窗体皮肤设置保存
                switch (cbbSkinTypeMain.SelectedValue.ToString())
                {
                    case "0": //默认
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.MainSkinValue, cbbColorMain.SelectedValue.ToString());
                        break;
                    case "1": //选择颜色
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.MainSkinValue, cbbColorMain.SelectedValue.ToString());
                        break;
                    case "2": //自定义颜色
                        string strRBG = txbSkinValueMain.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strRBG))
                        {
                            ShowInfo("请选择一个自定义颜色！");
                            return;
                        }
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorDefine;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strRBG;
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.MainSkinValue, strRBG);
                        break;
                    case "3": //选择图片
                        string strPicPath = txbSkinValueMain.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strPicPath))
                        {
                            ShowInfo("请选择一个图片！");
                            return;
                        }
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Picture;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strPicPath;
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.MainSkinValue, strPicPath);
                        break;
                    default:
                        break;
                }
                #endregion
            }
            if (cbbSkinTypeCommon.SelectedValue != null)
            {
                WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.CommonSkinType, cbbSkinTypeCommon.SelectedValue.ToString());
                #region 子窗体皮肤设置保存
                switch (cbbSkinTypeCommon.SelectedValue.ToString())
                {
                    case "0": //默认
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.CommonSkinValue, cbbColorCommon.SelectedValue.ToString());
                        break;
                    case "1": //选择颜色
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.CommonSkinValue, cbbColorCommon.SelectedValue.ToString());
                        break;
                    case "2": //自定义颜色
                        string strRBG = txbSkinValueCommon.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strRBG))
                        {
                            ShowInfo("请选择一个自定义颜色！");
                            return;
                        }
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorDefine;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strRBG;
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.CommonSkinValue, strRBG);
                        break;
                    case "3": //选择图片
                        string strPicPath = txbSkinValueCommon.Text.Trim().ToString();
                        if (string.IsNullOrEmpty(strPicPath))
                        {
                            ShowInfo("请选择一个图片！");
                            return;
                        }
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Picture;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = strPicPath;
                        WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.CommonSkinValue, strPicPath);
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
                WinFormConfig.Instance.Set(WinFormConfig.WinFormConfigString.SavePromptType, strSavePromptType); 
                if (strSavePromptType == "2")//仅显示，不弹出
                {
                    WinFormContext.UserEnvConfig.SaveMsgPrompt = SaveMsgPromptTypeEnum.OnlyPromptNotPopup;
                }
                else //弹出提示
                {
                    WinFormContext.UserEnvConfig.SaveMsgPrompt = SaveMsgPromptTypeEnum.PopupPrompt;
                }
                #endregion
            }
            WinFormConfig.Instance.Save();
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
                    cbbColorMain.BindXmlTypeValueDropDownList(_dtColorNum, false, true);
                    break;
                case "1": //选择颜色
                    cbbColorMain.Enabled = true;
                    txbSkinValueMain.Text = "";
                    cbbColorMain.BindXmlTypeValueDropDownList(_dtColorName, false, true);
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
                    cbbColorCommon.BindXmlTypeValueDropDownList(_dtColorNum, false, true);
                    break;
                case "1": //选择颜色
                    cbbColorCommon.Enabled = true;
                    txbSkinValueCommon.Text = "";
                    cbbColorCommon.BindXmlTypeValueDropDownList(_dtColorName, false, true);
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

        private void tsbMiniDbConfig_Click(object sender, EventArgs e)
        {
            FrmDBConfig frm = new FrmDBConfig(MiniGlobalValue.DataAccessConfigKey, MiniGlobalValue.DbConfigFileDir, MiniGlobalValue.DbConfigFileName);
            frm.ShowDialog();
        }
    }
}
