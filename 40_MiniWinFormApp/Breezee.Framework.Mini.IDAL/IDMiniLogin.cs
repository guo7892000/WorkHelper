using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Breezee.Framework.Mini.Entity;

/***************************************
 * 对象名称：登录类
 * 对象类别：数据访问层
 * 创建作者：黄国辉
 * 创建日期：2014-9-4
 * 对象说明：主要提供框架的登录功能
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-4
 * ************************************/
namespace Breezee.Framework.Mini.IDAL
{
    /// <summary>
    /// 登录_数据访问层_抽象类
    /// </summary>
    public abstract class IDMiniLogin: IMiniModule
    {
        #region 查询用户
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public abstract DataTable QueryUser(IDictionary<string, string> dicQuery);
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns>影响的行数</returns>
        public abstract int UpdateUserPasswd(IDictionary<string, string> dicQuery, DbConnection conn, DbTransaction tran);
        #endregion

        #region 查询全局配置
        /// <summary>
        /// 查询全局配置
        /// </summary>
        /// <author>黄国辉</author>
        /// <status>已完成</status>
        /// <param name="dicQuery"></param>
        /// <param name="dac"></param>
        /// <returns></returns>
        public abstract DataTable QueryGlobalConfig(IDictionary<string, string> dicQuery);
        #endregion
    }
}
