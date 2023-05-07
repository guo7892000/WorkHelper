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
using Breezee.Core.Tool.Helper;

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
        private WinFormConfig _WinFormConfig;
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
            _WinFormConfig = WinFormContext.Instance.WinFormConfig;
            DataTable dtFormSksy = MiniKeyValue.GetValue(MiniKeyEnum.FORM_SKIN_TYPE);
            DataTable dtSaveTip = MiniKeyValue.GetValue(MiniKeyEnum.SAVE_TIP);
            _dtColorNum = MiniKeyValue.GetValue(MiniKeyEnum.RBG_VALUE);
            _dtColorName = MiniKeyValue.GetValue(MiniKeyEnum.RBG_NAME);
            //绑定下拉框
            cbbSkinTypeMain.BindXmlTypeValueDropDownList(dtFormSksy, false, true);
            cbbSkinTypeCommon.BindXmlTypeValueDropDownList(dtFormSksy, false, true);
            cbbMsgType.BindXmlTypeValueDropDownList(dtSaveTip, false, true);
            cbbMsgType.SelectedValue = _WinFormConfig.Get(GlobalKey.SavePromptType, "2");
            //主窗体皮肤类型
            string sMainSkinType = _WinFormConfig.Get(GlobalKey.MainSkinType, "0");
            cbbSkinTypeMain.SelectedValue = sMainSkinType;
            if (sMainSkinType.Equals("0") || sMainSkinType.Equals("1"))
            {
                cbbColorMain.SelectedValue = _WinFormConfig.Get(GlobalKey.MainSkinValue, "0");
            }
            else
            {
                txbSkinValueMain.Text = _WinFormConfig.Get(GlobalKey.MainSkinValue, "0");
            }

            //子窗体
            string sCommonSkinType = _WinFormConfig.Get(GlobalKey.CommonSkinType, "0");
            if (sCommonSkinType.Equals("0") || sCommonSkinType.Equals("1"))
            {
                cbbColorCommon.SelectedValue = _WinFormConfig.Get(GlobalKey.CommonSkinValue, "0");
            }
            else
            {
                txbSkinValueCommon.Text = _WinFormConfig.Get(GlobalKey.CommonSkinValue, "0");
            }
            //用户自定义的配置路径
            txbMyLoveSettingPath.Text = GlobalContext.AppRootPath;
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
                string sSkinTypeMain = cbbSkinTypeMain.SelectedValue.ToString();
                _WinFormConfig.Set(GlobalKey.MainSkinType, sSkinTypeMain, "主窗体皮肤设置");
                #region 主窗体皮肤设置保存
                switch (sSkinTypeMain)
                {
                    case "0": //默认
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        _WinFormConfig.Set(GlobalKey.MainSkinValue, cbbColorMain.SelectedValue.ToString(), "主窗体皮肤设置：" + cbbColorMain.Text);
                        break;
                    case "1": //选择颜色
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorMain.SelectedValue.ToString();
                        _WinFormConfig.Set(GlobalKey.MainSkinValue, cbbColorMain.SelectedValue.ToString(), "主窗体皮肤设置：选择颜色");
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
                        _WinFormConfig.Set(GlobalKey.MainSkinValue, strRBG, "主窗体皮肤设置：自定义颜色");
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
                        _WinFormConfig.Set(GlobalKey.MainSkinValue, strPicPath, "主窗体皮肤设置：选择图片");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            if (cbbSkinTypeCommon.SelectedValue != null)
            {
                string sSkinTypeCommon = cbbSkinTypeCommon.SelectedValue.ToString();
                _WinFormConfig.Set(GlobalKey.CommonSkinType, sSkinTypeCommon, "子窗体皮肤设置");
                #region 子窗体皮肤设置保存
                switch (sSkinTypeCommon)
                {
                    case "0": //默认
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.Default;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        _WinFormConfig.Set(GlobalKey.CommonSkinValue, cbbColorCommon.SelectedValue.ToString(), "主窗体皮肤设置：" + cbbColorCommon.Text);
                        break;
                    case "1": //选择颜色
                        WinFormContext.UserEnvConfig.MainFormSkin.SkinType = FormSkinTypeEnum.ColorList;
                        WinFormContext.UserEnvConfig.MainFormSkin.ColorRBGOrImagePath = cbbColorCommon.SelectedValue.ToString();
                        _WinFormConfig.Set(GlobalKey.CommonSkinValue, cbbColorCommon.SelectedValue.ToString(), "主窗体皮肤设置：选择颜色");
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
                        _WinFormConfig.Set(GlobalKey.CommonSkinValue, strRBG, "主窗体皮肤设置：自定义颜色");
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
                        _WinFormConfig.Set(GlobalKey.CommonSkinValue, strPicPath, "主窗体皮肤设置：选择图片");
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
                _WinFormConfig.Set(GlobalKey.SavePromptType, strSavePromptType, "保存提示方式");
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
            _WinFormConfig.Save();
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

        /// <summary>
        /// 选择我的配置路径按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMySettingPath_Click(object sender, EventArgs e)
        {
            if (fbdSelectPath.ShowDialog() != DialogResult.OK) return;
            string sDirName = fbdSelectPath.SelectedPath;
            if (Directory.GetFiles(sDirName, "*.*", SearchOption.AllDirectories).Length == 0)
            {
                DialogResult result = MsgHelper.ShowYesNo("确定修改默认配置文件路径？");
                if (result == DialogResult.Yes)
                {
                    //注：这里要取用户喜好目录下的所有文件
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(GlobalContext.PathConfig(), sDirName, true);
                    WinFormContext.Instance.LoadAppConfig(sDirName);//重新加载应用配置
                    txbMyLoveSettingPath.Text = sDirName;
                }
            }
            else
            {
                MsgHelper.ShowErr("请选择一个空目录！");
            }
        }

        
    }
}
