using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***************************************
 * 对象名称：业务逻辑层自定义异常
 * 对象类别：异常类
 * 创建作者：黄国辉
 * 创建日期：2014-9-15
 * 对象说明：数据层异常类
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-15
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 业务逻辑层自定义异常
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 初始化 <see cref="BusinessException"/> 类的实例
        /// </summary>
        public BusinessException() : base() 
        { 
        
        }

        /// <summary>
        /// 使用指定的异常消息初始化 <see cref="BusinessException"/> 类的新实例。
        /// </summary>
        /// <param name="message">异常消息</param>
        public BusinessException(string message) : base(message)
        { 

        }
    }
}
