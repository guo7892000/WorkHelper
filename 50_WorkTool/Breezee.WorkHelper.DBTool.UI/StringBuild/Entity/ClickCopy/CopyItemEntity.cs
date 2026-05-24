using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 复制项实体（即string节点的所有属性）
    /// </summary>
    public class CopyItemEntity
    {
        /// <summary>
        /// type类型，值范围包括：
        ///     text文本，用于复制字符的；
        ///     path路径，可复制或打开；
        ///     file是文件，可读取文件内容来复制，当文件不存在时会显示黄色。
        /// </summary>
        public string Type;
        /// <summary>
        /// ctrol控件，值范围包括：
        ///     TextBox是一般文本框；
        ///     RichTextBox是富文件框，针对type为file的配置。
        /// </summary>
        public string Ctrol;
        /// <summary>
        /// label标签，显示的复制项名称
        /// </summary>
        public string Lable;
        /// <summary>
        /// 按钮的提示信息
        /// </summary>
        public string Tip;
        /// <summary>
        /// 实际拷贝的内容。支持内置参数的字符替换，如【#yyyyMMdd#】表示替换为当前日期字符，适用场景如当天备份目录名。如果要输出实际的#，请使用{{@JH@}}代替。
        /// 如果已加密，则该文本显示为密文。
        /// </summary>
        public string Text;
        /// <summary>
        /// 设置密码掩码字符，如复制的是密码，请设置其值为*
        /// </summary>
        public string Pwdchar;
        /// <summary>
        /// 读取文本文件的绝对路径，针对type为file的配置。
        /// </summary>
        public string PathAbs;
        /// <summary>
        /// 读取文本文件的相对当前选择的配置文件所在目录的路径，针对type为file的配置。
        /// </summary>
        public string PathRel;
        /// <summary>
        /// 按钮事件绑定到其他按钮事件
        /// </summary>
        public string Method;
        /// <summary>
        /// 输入文本框：一般文本框、富文本框
        /// </summary>
        public TextBoxBase tbb;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor;
        /// <summary>
        /// 是否需要加密：0-否，1-是
        /// </summary>
        public bool NeedEncrypt;
        /// <summary>
        /// 是否已加密：0-否，1-是
        /// </summary>
        public bool HadEncrypt;

        #region 非配置字段
        /// <summary>
        /// 密文：读取配置时赋值
        /// </summary>
        public string TextEncrypt;
        /// <summary>
        /// 明文：读取配置时动态转换
        /// </summary>
        public string TextFact;
        public bool IsChange;
        #endregion
    }
}
