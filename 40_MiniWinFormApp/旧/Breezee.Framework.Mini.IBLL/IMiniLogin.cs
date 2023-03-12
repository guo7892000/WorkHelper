using System;
using System.Collections.Generic;
using System.Linq;
using Breezee.Framework.Mini.Entity;
using Breezee.Framework.Mini.IDAL;

/***************************************************
 * 对象名称：登录类
 * 对象类别：接口层
 * 创建作者：黄国辉
 * 创建日期：2014-9-4
 * 对象说明：主要提供框架的登录查询相关功能
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-4
 * ************************************************/
namespace Breezee.Framework.Mini.IBLL
{
    /// <summary>
    /// 登录类
    /// </summary>
    public abstract class IMiniLogin : IMiniModule
    {
        #region 登录方法
        public abstract IDictionary<string, object> Login(IDictionary<string, string> dicQuery);
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <author>黄国辉</creator>
        /// <status>已完成</status>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public abstract int UpdateUserPasswd(IDictionary<string, string> dicQuery);
        #endregion

        #region 查询全局配置
        /// <summary>
        /// 查询全局配置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public abstract IDictionary<string, object> QueryGlobalConfig(IDictionary<string, string> dicQuery);
        #endregion

        #region 方法参数内部类
        /// <summary>
        /// Login方法的传入参数字典键
        /// </summary>
        public static class Login_InDicKey
        {
            public static readonly string UserCode = "USER_CODE";
        }

        /// <summary>
        /// Login方法的返回字典键
        /// </summary>
        public static class Login_ReturnDicKey
        {
            public static readonly string SysUser = "SYS_USER";
        }

        /// <summary>
        /// UpdateUserPasswd方法的传入参数字典键
        /// </summary>
        public static class UpdateUserPasswd_InDicKey
        {
            public static readonly string NEW_USER_PASSWORD = "NEW_USER_PASSWORD";
            public static readonly string USER_ID = "USER_ID";
        }

        #endregion
    }


}
