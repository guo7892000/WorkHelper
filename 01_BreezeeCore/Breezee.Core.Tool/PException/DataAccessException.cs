using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***************************************
 * 对象名称：数据访问层异常类
 * 对象类别：异常类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：数据访问层异常类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 数据访问层异常类
    /// </summary>
    public class DataAccessException : Exception
    {
        /// <summary>
        /// 初始化 <see cref="DataAccessException"/> 类的实例
        /// </summary>
        public DataAccessException() : base() 
        { 
        
        }

        /// <summary>
        /// 使用指定的异常消息初始化 <see cref="DataAccessException"/> 类的新实例。
        /// </summary>
        /// <param name="message">异常消息</param>
        public DataAccessException(string message): base(message)
        { 

        }
    }
}
