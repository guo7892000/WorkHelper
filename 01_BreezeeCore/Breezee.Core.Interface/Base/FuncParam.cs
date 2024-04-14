using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：方法参数基类		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：单个参数，主要提供给UI使用		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 方法参数基类
    /// 单个参数，主要提供给UI使用
    /// </summary>
    public class FuncParam
    {
        public string Code;//参数编码
        public FuncParamType FuncParamType = FuncParamType.String;//参数类型
        public object Value;//参数值
        public FuncParamInputType InputType = FuncParamInputType.Option;//是否必须的参数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="funcParamType"></param>
        /// <param name="inputType"></param>
        /// <param name="value">参数值：因为创建该类在接口层，而赋值在UI层，所以值参数放最后，可用于设默认值</param>
        public FuncParam(string code, FuncParamType funcParamType = FuncParamType.String, FuncParamInputType inputType = FuncParamInputType.Option, object value = null)
        {
            Code = code;
            FuncParamType = funcParamType;
            InputType = inputType;
            Value = value;
        }
        public override string ToString()
        {
            return string.Format("参数编码：{0}，参数值：{1}，参数类型：{2}，是否必须的参数：{3}", Code, Value.ToString(), FuncParamType.ToString(), InputType.ToString());
        }
    }

    /// <summary>
    /// 方法参数类型
    /// </summary>
    public enum FuncParamType
    {
        String,
        DateTime,
        Int,
        DataTable,
        IDictionaryString,
        IDictionaryObject
    }

    /// <summary>
    /// 方法参数录入类型
    /// </summary>
    public enum FuncParamInputType
    {
        Option,//可选的参数
        MustNotNull, //必须的参数且值不能为空
        MustCanNull//必须的参数但可为空值
    }
}
