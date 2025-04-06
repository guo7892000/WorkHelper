using Breezee.Core.Tool;
using Breezee.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 控件扩展类
    /// </summary>
    public static class FormExtension
    {
        #region 设置容器的背景色
        /// <summary>
        /// 设置容器的背景色
        /// </summary>
        /// <param name="ctrls"></param>
        private static void SetControlBackColor(Control.ControlCollection ctrls, FormBackGroupStyleEntity styleEntity)
        {
            ImageLayout IL = ImageLayout.None;
            Color color = styleEntity.BackGroupColor;
            Image img = styleEntity.BackGroupImage;
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is GroupBox)
                {
                    ctrl.BackColor = color;
                    ctrl.BackgroundImage = null; //img;
                    ctrl.BackgroundImageLayout = IL;
                }
                else if (ctrl is MenuStrip)
                {
                    ctrl.BackColor = color;
                    ctrl.BackgroundImage = null; //img;
                    ctrl.BackgroundImageLayout = IL;
                }
                else if (ctrl is ToolStrip)
                {
                    ctrl.BackColor = color;
                    ctrl.BackgroundImage = null; //img;
                    ctrl.BackgroundImageLayout = IL;
                }
                else if (ctrl is Panel)
                {
                    ctrl.BackColor = color;
                    ctrl.BackgroundImage = img;
                    ctrl.BackgroundImageLayout = IL;
                    // 递归调用
                    SetControlBackColor(ctrl.Controls, styleEntity);
                }
                else if (ctrl is SplitContainer)
                {
                    ctrl.BackColor = color;
                    ctrl.BackgroundImage = null; //img;
                    ctrl.BackgroundImageLayout = IL;
                    // 递归调用
                    SetControlBackColor(ctrl.Controls, styleEntity);
                }
                else
                {
                    // 递归调用
                    SetControlBackColor(ctrl.Controls, styleEntity);
                }
            }
        }
        #endregion

        #region 设置窗体样式
        public static FormBackGroupStyleEntity SetFormBackGroupStyle(this Form frm, DataTable dtUserEnv, string SkinConfigCode)
        {
            FormBackGroupStyleEntity styleEntity = new FormBackGroupStyleEntity();
            DataRow[] drParent = dtUserEnv.Select(DT_BAS_VALUE.VALUE_CODE + "='" + SkinConfigCode + "'");
            Color formColor = Color.FromArgb(207, 226, 243);
            Image imgBack = null;

            if (drParent.Length > 0)
            {
                string strUserEvnCode = drParent[0][DT_SYS_REL_USER_ENVIRONMENT_SET.USER_ENV_CODE].ToString();

                if (strUserEvnCode == "3")
                {
                    string strImgPath = drParent[0][DT_SYS_REL_USER_ENVIRONMENT_SET.USER_ENV_VALUE].ToString();
                    if (File.Exists(strImgPath))//判断文件是否存在
                    {
                        imgBack = Image.FromFile(strImgPath);
                        frm.BackgroundImage = imgBack;
                        styleEntity.IsUseImage = true;
                        styleEntity.BackGroupImage = imgBack;
                    }
                }
                else
                {
                    if (strUserEvnCode == "0") //默认颜色：3个整型值
                    {
                        string[] iColorArr = drParent[0][DT_SYS_REL_USER_ENVIRONMENT_SET.USER_ENV_VALUE].ToString().Split(new char[] { ',', '，' });
                        if (iColorArr.Length >= 3)
                        {
                            formColor = Color.FromArgb(int.Parse(iColorArr[0]), int.Parse(iColorArr[1]), int.Parse(iColorArr[2]));
                            frm.BackColor = formColor;
                        }
                    }
                    else if (strUserEvnCode == "1") //颜色名称
                    {
                        formColor = Color.FromName(drParent[0][DT_SYS_REL_USER_ENVIRONMENT_SET.USER_ENV_VALUE].ToString());
                        frm.BackColor = formColor;
                    }
                    else if (strUserEvnCode == "2") //选择颜色：1个整形值
                    {
                        int iColor = int.Parse(drParent[0][DT_SYS_REL_USER_ENVIRONMENT_SET.USER_ENV_VALUE].ToString());
                        formColor = Color.FromArgb(iColor);
                        frm.BackColor = formColor;
                    }
                    styleEntity.IsUseImage = false;
                    styleEntity.BackGroupColor = formColor;
                }
            }
            SetControlBackColor(frm.Controls, styleEntity);
            return styleEntity;
        }

        public static FormBackGroupStyleEntity SetFormBackGroupStyle(this Form frm, string sType, string sValue)
        {
            FormBackGroupStyleEntity styleEntity = new FormBackGroupStyleEntity();
            Color formColor = Color.FromArgb(207, 226, 243);
            Image imgBack = null;
            if (sType == "3")
            {
                string strImgPath = sValue;
                if (File.Exists(strImgPath))//判断文件是否存在
                {
                    imgBack = Image.FromFile(strImgPath);
                    frm.BackgroundImage = imgBack;
                    styleEntity.IsUseImage = true;
                    styleEntity.BackGroupImage = imgBack;
                }
            }
            else
            {
                if (sType == "0") //默认颜色：3个整型值
                {
                    string[] iColorArr = sValue.Split(new char[] { ',', '，' });
                    if (iColorArr.Length >= 3)
                    {
                        formColor = Color.FromArgb(int.Parse(iColorArr[0]), int.Parse(iColorArr[1]), int.Parse(iColorArr[2]));
                        frm.BackColor = formColor;
                    }
                }
                else if (sType == "1") //颜色名称
                {
                    formColor = Color.FromName(sValue);
                    frm.BackColor = formColor;
                }
                else if (sType == "2") //选择颜色：1个整形值
                {
                    int iColor = int.Parse(sValue);
                    formColor = Color.FromArgb(iColor);
                    frm.BackColor = formColor;
                }
                styleEntity.IsUseImage = false;
                styleEntity.BackGroupColor = formColor;
            }
            SetControlBackColor(frm.Controls, styleEntity);
            return styleEntity;
        }
        #endregion

    }

    /// <summary>
    /// 窗体背景样式实体
    /// </summary>
    public class FormBackGroupStyleEntity
    {
        public bool IsUseImage=false;
        public Color BackGroupColor = Color.FromArgb(207, 226, 243);
        public Image BackGroupImage = null;
    }
}
