using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    public enum WordUpperType
    {
        /// <summary>
        /// 大驼峰式（所有单词首字母大写）
        /// </summary>
        UpperCamelCase,
        /// <summary>
        /// 小驼峰式（所有单词首字母大写，除了第一个单词）
        /// </summary>
        LowerCamelCase,
        /// <summary>
        /// 不做转换
        /// </summary>
        None,
    }
}
