using System;
using System.Collections.Generic;
using Breezee.Core.Entity;
using System.Data;
using Breezee.Core.Tool;
using Breezee.Core.IOC;
using System.Data.Common;
using Breezee.Framework.Mini.IDAL;
using Breezee.Framework.Mini.IBLL;
using Breezee.Framework.Mini.Entity;

/***************************************
 * 对象名称：登录类
 * 对象类别：业务实现层
 * 创建作者：黄国辉
 * 创建日期：2013-2-5
 * 对象说明：主要提供框架的登录功能
 * 修改历史：
 *      V1.0 新建 hgh 2013-2-5
 * ************************************/
namespace Breezee.Framework.Mini.BLL
{
    /// <summary>
    /// 类名：登录实现类
    /// 作者：黄国辉
    /// 日期：2013-2-5
    /// </summary>
    public class BMiniLogin : IMiniLogin
    {
        #region 获取登录用户信息
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <author>黄国辉</creator>
        /// <status>还需要进一步完善</status>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public override IDictionary<string, object> Login(IDictionary<string, string> dicQuery)
        {
            IDictionary<string, object> dicRet = ExecuteResultHelper.Success();
            try
            {
                var loginDac = ContainerContext.Container.Resolve<IDMiniLogin>();

                DataTable dtUserExists = DataAccess.GetSchemaTables(DT_SYS_USER.TName);
                if(dtUserExists.Rows.Count==0)
                {
                    //var dbInitializer = ContainerContext.Container.Resolve<IDMiniDBInitializer>();
                    DBInitializer.InitDataBase();
                    return ExecuteResultHelper.Fail("登录失败，因为登录前数据库未初始化。目前已初始化，请以用户名：xtadmin，密码：1重新登录！");
                }

                DataTable dtUser = loginDac.QueryUser(dicQuery);
                dicRet[StaticConstant.FRA_QUERY_RESULT] = dtUser;
                //将行转换为实体
                if (dtUser.Rows.Count > 0)
                {
                    DataRow dr = dtUser.Rows[0];
                    Mini_SYS_USER user = new Mini_SYS_USER();
                    user.USER_ID = dr[DT_SYS_USER.USER_ID].ToString();
                    user.USER_CODE = dr[DT_SYS_USER.USER_CODE].ToString();
                    user.USER_NAME = dr[DT_SYS_USER.USER_NAME].ToString();
                    user.USER_NAME = dr[DT_SYS_USER.USER_NAME].ToString();
                    user.USER_NAME_EN = dr[DT_SYS_USER.USER_NAME_EN].ToString();
                    user.USER_PASSWORD = dr[DT_SYS_USER.USER_PASSWORD].ToString();
                    user.USER_TYPE = dr[DT_SYS_USER.USER_TYPE].ToString();
                    user.PIN_YIN = dr[DT_SYS_USER.PIN_YIN].ToString();
                    user.REMARK = dr[DT_SYS_USER.REMARK].ToString();
                    user.TICKET_ID = dr[DT_SYS_USER.TICKET_ID].ToString();
                    user.DESCRIPTION = dr[DT_SYS_USER.DESCRIPTION].ToString();
                    user.CREATOR_ID = dr[DT_SYS_USER.CREATOR_ID].ToString();
                    user.CREATOR = dr[DT_SYS_USER.CREATOR].ToString();
                    user.MODIFIER_ID = dr[DT_SYS_USER.MODIFIER_ID].ToString();
                    user.MODIFIER = dr[DT_SYS_USER.MODIFIER].ToString();

                    user.IS_ENABLED = dr[DT_SYS_USER.IS_ENABLED].ToString();
                    user.IS_SYSTEM = dr[DT_SYS_USER.IS_SYSTEM].ToString();
                    
                    user.UPDATE_CONTROL_ID = dr[DT_SYS_USER.UPDATE_CONTROL_ID].ToString();
                    user.TFLAG = dr[DT_SYS_USER.TFLAG].ToString();

                    #region 为兼容满功能模式赋空值
                    //员工信息
                    user.EMP_ID = "1";
                    user.EMP_NAME = "";
                    user.EMP_NAME = "";
                    //组织信息
                    user.ORG_ID = "1";
                    user.ORG_NAME_FULL = "";
                    user.ORG_NAME_SHORT = "";
                    user.FORM_CODE_SHORT = "";
                    user.PARENT_ORG_ID = "";
                    user.BELONG_ORG_ID = "";
                    //部门信息
                    user.DEPT_ORG_ID = "";
                    user.DEPT_ID = "1";
                    user.DEPT_NAME_FULL = "";
                    user.DEPT_NAME_SHORT = "";
                    //岗位信息
                    user.POSTION_ORG_ID = "1";
                    user.POSTION_ID = "";
                    user.POSTION_NAME_FULL = "";
                    user.POSTION_NAME_SHORT = "";
                    //岗位级别信息
                    user.POS_LEVEL_ORG_ID = "1";
                    user.POS_LEVEL_ID = "";
                    user.POS_LEVEL_NAME_SHORT = ""; 
                    #endregion

                    string strLAST_LOGIN_TIME = dr[DT_SYS_USER.LAST_LOGIN_TIME].ToString();
                    if (!string.IsNullOrEmpty(strLAST_LOGIN_TIME))
                    {
                        user.LAST_LOGIN_TIME = DateTime.Parse(strLAST_LOGIN_TIME);
                    }

                    string strLOGIN_STATE = dr[DT_SYS_USER.LOGIN_STATE].ToString();
                    if (!string.IsNullOrEmpty(strLOGIN_STATE))
                    {
                        user.LOGIN_STATE = int.Parse(strLOGIN_STATE);
                    }

                    string strLAST_UPDATED_TIME = dr[DT_SYS_USER.LAST_UPDATED_TIME].ToString();
                    if (!string.IsNullOrEmpty(strLAST_UPDATED_TIME))
                    {
                        user.LAST_UPDATED_TIME = DateTime.Parse(strLAST_UPDATED_TIME);
                    }

                    string strACTIVE_TIME = dr[DT_SYS_USER.ACTIVE_TIME].ToString();
                    if (!string.IsNullOrEmpty(strACTIVE_TIME))
                    {
                        user.ACTIVE_TIME = DateTime.Parse(strACTIVE_TIME);
                    }
                    string strDISABLE_TIME = dr[DT_SYS_USER.DISABLE_TIME].ToString();
                    if (!string.IsNullOrEmpty(strDISABLE_TIME))
                    {
                        user.DISABLE_TIME = DateTime.Parse(strDISABLE_TIME);
                    }
                    string strSORT_ID = dr[DT_SYS_USER.SORT_ID].ToString();
                    if (!string.IsNullOrEmpty(strSORT_ID))
                    {
                        user.LOGIN_STATE = int.Parse(strSORT_ID);
                    }
                    string strCREATE_TIME = dr[DT_SYS_USER.CREATE_TIME].ToString();
                    if (!string.IsNullOrEmpty(strCREATE_TIME))
                    {
                        user.CREATE_TIME = DateTime.Parse(strCREATE_TIME);
                    }

                    dicRet[IMiniLogin.Login_ReturnDicKey.SysUser] = user;
                    //全局变量赋值
                    user.MainFormMode = MainFormModelEnum.Mini;
                    //GlobalContext.Instance.SetLoginUser(user);

                }
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
            }
            return dicRet;
        } 
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
        public override int UpdateUserPasswd(IDictionary<string, string> dicQuery)
        {
            IDictionary<string, object> dicRet = ExecuteResultHelper.Success();
            try
            {
                var loginDac = ContainerContext.Container.Resolve<IDMiniLogin>();
                using (DbConnection con = DataAccess.GetCurrentConnection())
                {
                    con.Open();
                    DbTransaction tran = con.BeginTransaction();
                    //更新用户密码
                    int iEff = loginDac.UpdateUserPasswd(dicQuery, con, tran);
                    tran.Commit();
                    return iEff;
                }
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
                return 0;
            }
        }
        #endregion

        #region 查询全局配置
        /// <summary>
        /// 查询全局配置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public override IDictionary<string, object> QueryGlobalConfig(IDictionary<string, string> dicQuery)
        {
            IDictionary<string, object> dicRet = new Dictionary<string, object>();
            try
            {
                var dal = ContainerContext.Container.Resolve<IDMiniLogin>();
                dicRet = ExecuteResultHelper.QuerySuccess();
                dicRet[StaticConstant.FRA_QUERY_RESULT] = dal.QueryGlobalConfig(dicQuery);
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
            }
            return dicRet;
        }
        #endregion

    }
}
