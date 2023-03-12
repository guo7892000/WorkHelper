using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 方法参数集合基类
    /// 作为每个方法条件的继承基类
    /// </summary>
    public abstract class FuncParams
    {
        #region 获取所有参数_抽象属性
        /// <summary>
        /// 获取所有参数_抽象属性
        /// </summary>
        public abstract List<FuncParam> AllParam { get; }
        #endregion

        #region 获取所有非空值的参数_公共属性
        /// <summary>
        /// 为查询方法过虑空条件使用
        /// </summary>
        public List<FuncParam> AllNotNullParam
        {
            get
            {
                //排除空对象和空字符的参数
                var list = AllParam.Where(r => (r.Value != null && !string.IsNullOrEmpty(r.Value.ToString())));
                if (list == null || !list.Any())
                {
                    return new List<FuncParam>();
                }
                else
                {
                    return list.ToList();
                }
            }
        } 
        #endregion
    }
}
