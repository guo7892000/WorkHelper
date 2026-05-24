using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 点击复制实体转换
    /// </summary>
    public class ClickCopyEntityConvert
    {
        /// <summary>
        /// 获取复制项实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        public static CopyItemEntity getCopyItemEntity(XmlNode xn)
        {
            CopyItemEntity cs = null;
            string sText = "";
            if (xn.TryGetAttrValue(CopyItemPropertyName.Type, out sText))
            {
                //能正常获取type属性
                cs = new CopyItemEntity();
                cs.Type = sText;
                if (xn.TryGetAttrValue(CopyItemPropertyName.NeedEncrypt, out sText))
                {
                    cs.NeedEncrypt = "1".Equals(sText); //是否需要加密
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.HadEncrypt, out sText))
                {
                    cs.HadEncrypt = "1".Equals(sText); //是否已加密
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.CtrolType, out sText))
                {
                    cs.Ctrol = sText; //控件类型
                }
                else
                {
                    return null;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Lable, out sText))
                {
                    cs.Lable = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.PathAbs, out sText))
                {
                    cs.PathAbs = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.PathRel, out sText))
                {
                    cs.PathRel = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Pwdchar, out sText))
                {
                    cs.Pwdchar = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Text, out sText))
                {
                    cs.Text = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Tip, out sText))
                {
                    cs.Tip = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Method, out sText))
                {
                    cs.Method = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.FontColor, out sText))
                {
                    cs.FontColor = sText;
                }

                //是否需要加密
                if(cs.NeedEncrypt)
                {
                    if (!cs.HadEncrypt)
                    {
                        //未加密
                        cs.TextFact = cs.Text; //加密前字符
                        cs.TextEncrypt = EncryptHelper.AESEncrypt(cs.Text, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector); //加密后字符
                        xn.Attributes[CopyItemPropertyName.Text].Value = cs.TextEncrypt; //将XML中的Tex修改为加密后，并后续要保存该XML文件。
                        xn.Attributes[CopyItemPropertyName.HadEncrypt].Value = "1";
                        cs.IsChange = true;
                    }
                    else
                    {
                        //已加密
                        cs.TextFact = EncryptHelper.AESDecrypt(cs.Text, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector); //解密后字符
                        cs.TextEncrypt = cs.Text; //解密前字符，即是密文
                        cs.IsChange= false;
                    }
                    if (string.IsNullOrEmpty(cs.Pwdchar))
                    {
                        cs.Pwdchar = "*";
                    }
                }
            }
            return cs;
        }

        /// <summary>
        /// 获取组实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        public static GroupEntity getGroupEntity(XmlNode xn)
        {
            GroupEntity cs = new GroupEntity();
            string sText = "";
            if (xn.TryGetAttrValue(GroupPropertyName.Text, out sText))
            {
                cs.Text = sText;
            }
            if (xn.TryGetAttrValue(GroupPropertyName.Max, out sText))
            {
                cs.Max = int.Parse(sText);
            }
            if (xn.TryGetAttrValue(GroupPropertyName.FontColor, out sText))
            {
                cs.FontColor = sText.SafeParseColor();
            }
            if (xn.TryGetAttrValue(GroupPropertyName.ItemFontColor, out sText))
            {
                cs.ItemFontColor = sText.SafeParseColor();
            }
            return cs;
        }

        /// <summary>
        /// 获取Tap页签实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        public static TapEntity getTapEnity(XmlNode xn)
        {
            TapEntity cs = new TapEntity();
            string sText = "";
            if (xn.TryGetAttrValue(ClickCopyConfigFileStr.Name, out sText))
            {
                cs.Name = sText;
            }
            return cs;
        }

        /// <summary>
        /// 获取参数实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        public static ParamEntity getParamEnity(XmlNode xn)
        {
            ParamEntity cs = new ParamEntity();
            string sText = "";
            if (xn.TryGetAttrValue(ParamPropStr.Key, out sText))
            {
                cs.Key = sText.ToUpper(); //转换为大写
            }
            if (xn.TryGetAttrValue(ParamPropStr.Value, out sText))
            {
                cs.Value = sText;
            }
            if (xn.TryGetAttrValue(ParamPropStr.Remark, out sText))
            {
                cs.Remark = sText;
            }
            return cs;
        }
    }
}
