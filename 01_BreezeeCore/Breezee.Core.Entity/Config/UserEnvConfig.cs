using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// 用户环境配置
    /// 跟用户相关的配置信息
    /// </summary>
    public class UserEnvConfig
    {
        /// <summary>
        /// 主窗体皮肤
        /// </summary>
        public FormSkin MainFormSkin = new FormSkin();
        /// <summary>
        /// 子窗体皮肤
        /// </summary>
        public FormSkin ChildFormSkin = new FormSkin();

        /// <summary>
        /// 保存提示方式
        /// </summary>
        public SaveMsgPromptTypeEnum SaveMsgPrompt = SaveMsgPromptTypeEnum.PopupPrompt;
    }

    /// <summary>
    /// 窗体皮肤
    /// </summary>
    public class FormSkin
    {
        public FormSkinTypeEnum SkinType = FormSkinTypeEnum.Default;
        public string ColorRBGOrImagePath = "";
    }
    
    /// <summary>
    /// 窗体皮肤枚举
    /// </summary>
    public enum FormSkinTypeEnum
    {
        /// <summary>
        /// 默认方式：提供清单选择
        /// </summary>
        Default=0,
        /// <summary>
        /// 颜色清单：提供选择
        /// </summary>
        ColorList=1,
        /// <summary>
        /// 自定义颜色：根据颜色弹出框来点击选取
        /// </summary>
        ColorDefine=2,
        /// <summary>
        /// 图片
        /// </summary>
        Picture=3
    }

    public enum SaveMsgPromptTypeEnum
    { 
        /// <summary>
        /// 弹出式提示
        /// </summary>
        PopupPrompt = 1,
        /// <summary>
        /// 提示但不弹出 
        /// </summary>
        OnlyPromptNotPopup = 2
    }
}
