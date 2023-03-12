using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Breezee.Core.Tool;
using System.Data.Common;
using Breezee.Framework.Mini.IDAL;
using Breezee.AutoSQLExecutor.Core;

/***************************************
 * 对象名称：登录类
 * 对象类别：数据访问层
 * 创建作者：黄国辉
 * 创建日期：2014-9-4
 * 对象说明：主要提供框架的登录功能
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-4
 * ************************************/
namespace Breezee.Framework.Mini.DAL
{
    /// <summary>
    /// 登录类
    /// </summary>
    public class DMiniLogin: IDMiniLogin
    {
        #region 查询用户(非通用)
        /// <summary>
        /// 查询用户(非通用)
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public override DataTable QueryUser(IDictionary<string, string> dicQuery)
        {
            //查询并返回结果
            QuerySqlBuilder querySqlBuilder = new QuerySqlBuilder(DataAccess);
            querySqlBuilder.Sql(
            @"SELECT  UR.USER_ID ,
                    UR.USER_CODE ,
                    UR.EMP_ID ,
                    UR.USER_NAME,
                    UR.USER_NAME_EN ,
                    UR.USER_PASSWORD ,
                    UR.USER_TYPE ,
                    UR.PIN_YIN ,
                    UR.LAST_LOGIN_TIME ,
                    UR.LOGIN_STATE ,
                    UR.TICKET_ID ,
                    UR.DESCRIPTION ,
                    UR.ACTIVE_TIME ,
                    UR.DISABLE_TIME ,
                    UR.SORT_ID ,
                    UR.REMARK ,
                    UR.CREATE_TIME ,
                    UR.CREATOR_ID ,
                    UR.CREATOR ,
                    UR.MODIFIER_ID ,
                    UR.MODIFIER ,
                    UR.LAST_UPDATED_TIME ,
                    UR.IS_ENABLED ,
                    UR.IS_SYSTEM ,
                    UR.UPDATE_CONTROL_ID ,
                    UR.TFLAG
            FROM    SYS_USER UR /*用户*/ 
            WHERE   UR.USER_CODE = @USER_CODE
            ");
            querySqlBuilder.Sql("USER_CODE", "");
            DataTable dtButtonList = querySqlBuilder.Query(dicQuery);
            return dtButtonList;
        } 
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns>影响的行数</returns>
        public override int UpdateUserPasswd(IDictionary<string, string> dicQuery,DbConnection conn,DbTransaction tran)
        {
            ExecuteNonQuerySqlBuilder sqlBuilder = new ExecuteNonQuerySqlBuilder(DataAccess);
            sqlBuilder.Sql(
            @"UPDATE SYS_USER 
                SET USER_PASSWORD = :NEW_USER_PASSWORD
              WHERE USER_ID = :USER_ID
            ");
            int iEff = sqlBuilder.ExecuteNonQuery( dicQuery, conn, tran, new string[] { "NEW_USER_PASSWORD", "USER_ID" });
            return iEff;
        }
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
        public override DataTable QueryGlobalConfig(IDictionary<string, string> dicQuery)
        {
            //查询并返回结果
            var querySqlBuilder = new QuerySqlBuilder(DataAccess);
            querySqlBuilder.Sql(
            @"SELECT  A.GLOBAL_CONFIG_ID ,
                    A.DB_ENVIRONMENT_TYPE ,
                    A.IS_LOGIN_USE_VERIFICATION ,
                    A.SORT_ID ,
                    A.REMARK ,
                    A.CREATE_TIME ,
                    A.CREATOR_ID ,
                    A.CREATOR ,
                    A.MODIFIER_ID ,
                    A.MODIFIER ,
                    A.LAST_UPDATED_TIME ,
                    A.IS_ENABLED ,
                    A.IS_SYSTEM ,
                    A.ORG_ID ,
                    A.UPDATE_CONTROL_ID 
            FROM SYS_SYSTEM_GLOBAL_CONFIG A
            WHERE 1=1  
            ");
            return querySqlBuilder.Query(dicQuery);
        }
        #endregion
    }
}
