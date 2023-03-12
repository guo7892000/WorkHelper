using Breezee.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***************************************
 * 对象名称：执行结果处理辅助类
 * 对象类别：辅助类
 * 创建作者：黄国辉
 * 创建日期：2014-7-26
 * 对象说明：主要提供用户查询、保存的方法
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-26
 * ************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 方法执行结果辅助类
    /// </summary>
    public class ExecuteResultHelper
    {
        private ExecuteResultHelper(bool result, string message)
        {
            this.Result = result;
            this.Message = message ?? string.Empty;
        }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public bool Result { get; private set; }

        private Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic[StaticConstant.FRA_RETURN_FLAG] = this.Result ? 1 : 0;
            dic[StaticConstant.FRA_USER_MSG] = this.Message;
            return dic;
        }

        /// <summary>
        /// 执行成功返回消息字典
        /// </summary>
        /// <param name="message">提示消息文本</param>
        /// <returns>消息字典</returns>
        public static IDictionary<string, object> Success(string message)
        {
            return new ExecuteResultHelper(true, message).ToDictionary();
        }

        /// <summary>
        /// 执行成功返回默认提示消息字典
        /// </summary>
        /// <returns>默认提示消息字典</returns>
        public static IDictionary<string, object> Success()
        {
            return ExecuteResultHelper.Success(MessageType.SaveSuccess);
        }

        /// <summary>
        /// 执行失败返回消息字典
        /// </summary>
        /// <param name="message">消息文本</param>
        /// <returns>消息字典</returns>
        public static IDictionary<string, object> Fail(string message)
        {
            return new ExecuteResultHelper(false, message).ToDictionary();
        }

        /// <summary>
        /// 执行失败返回默认消息字典
        /// </summary>
        /// <returns>执行失败默认消息字典</returns>
        public static IDictionary<string, object> FailDefault()
        {
            return new ExecuteResultHelper(false, MessageType.SaveFailure).ToDictionary();
        }

        /// <summary>
        /// 执行出现异常时返回异常消息字典
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>异常消息字典</returns>
        public static IDictionary<string, object> FailException(Exception ex)
        {
            IDictionary<string, string> iEx = ExceptionHelper.ExceptioinManage(ex);
            return new ExecuteResultHelper(false, iEx[StaticConstant.FRA_USER_MSG]).ToDictionary();
        }

        /// <summary>
        /// 查询成功返回默认提示消息字典
        /// </summary>
        /// <returns>默认提示消息字典</returns>
        public static IDictionary<string, object> QuerySuccess()
        {
            return ExecuteResultHelper.Success(MessageType.QuerySuccess);
        }

        /// <summary>
        /// 删除成功返回默认提示消息字典
        /// </summary>
        /// <returns>默认提示消息字典</returns>
        public static IDictionary<string, object> DeleteSuccess()
        {
            return ExecuteResultHelper.Success(MessageType.DeleteSuccess);
        }

    }
}
