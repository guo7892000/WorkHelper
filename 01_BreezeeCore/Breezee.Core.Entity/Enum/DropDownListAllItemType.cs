using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************
 * 对象名称：下拉框全部项类型
 * 对象类别：枚举
 * 创建作者：黄国辉
 * 创建日期：2014-10-26
 * 对象说明：主要提供下拉框第一个全部显示文本类型
 * 修改历史：
 *      V1.0 新建 hgh 2014-10-26
 * ******************************************************/
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 下拉框全部项类型
    /// </summary>
    public enum DropDownListAllItemType
    {
        /// <summary>
        /// 空白
        /// </summary>
        Empty,

        /// <summary>
        /// 全部
        /// </summary>
        All,

        /// <summary>
        /// 请选择
        /// </summary>
        Select
    }
}
