using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Breezee.Core.Entity;

/*****************************************************************************************
 * 对象名称：登录用户实体类
 * 对象类别：实体类
 * 创建作者：黄国辉
 * 创建日期：2014-9-10
 * 对象说明：主要提供登录用户信息。
 *      注：针对SYS_USER类，只提供读取属性，禁用修改。如该类有修改，也请修改本类！！
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-10
 * **************************************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 登录用户类
    /// </summary>
    public class LoginUserInfo
    {
        #region 变量
        private readonly SYS_USER _currentUser;
        #endregion

        #region 构造函数
        public LoginUserInfo(SYS_USER su)
        {
            _currentUser = su;
        } 
        #endregion

        #region 接口属性
        public string DataState { get { return _currentUser.DataState; } }
        public bool IS_SELECTED { get { return _currentUser.IS_SELECTED; } }
        #endregion

        #region 组织相关信息
        //员工信息
        public string EMP_ID { get { return _currentUser.EMP_ID; } }
        public string EMP_NAME { get { return _currentUser.EMP_NAME; } }
        //所属组织信息
        public string ORG_ID { get { return _currentUser.ORG_ID; } }
        public string ORG_NAME_SHORT { get { return _currentUser.ORG_NAME_SHORT; } }
        public string ORG_NAME_FULL { get { return _currentUser.ORG_NAME_FULL; } }
        public string FORM_CODE_SHORT { get { return _currentUser.FORM_CODE_SHORT; } }
        public string PARENT_ORG_ID { get { return _currentUser.PARENT_ORG_ID; } }
        public string BELONG_ORG_ID { get { return _currentUser.BELONG_ORG_ID; } }
        //部门信息
        public string DEPT_ORG_ID { get { return _currentUser.DEPT_ORG_ID; } }
        public string DEPT_ID { get { return _currentUser.DEPT_ID; } }
        public string DEPT_NAME_SHORT { get { return _currentUser.DEPT_NAME_SHORT; } }
        public string DEPT_NAME_FULL { get { return _currentUser.DEPT_NAME_FULL; } }

        //岗位信息
        public string POSTION_ORG_ID { get { return _currentUser.POSTION_ORG_ID; } }
        public string POSTION_ID { get { return _currentUser.POSTION_ID; } }
        public string POSTION_NAME_SHORT { get { return _currentUser.POSTION_NAME_SHORT; } }
        public string POSTION_NAME_FULL { get { return _currentUser.POSTION_NAME_FULL; } }

        //岗位级别信息
        public string POS_LEVEL_ORG_ID { get { return _currentUser.POS_LEVEL_ORG_ID; } }
        public string POS_LEVEL_ID { get { return _currentUser.POS_LEVEL_ID; } }
        public string POS_LEVEL_NAME_SHORT { get { return _currentUser.POS_LEVEL_NAME_SHORT; } }
        public string POS_LEVEL_NAME_FULL { get { return _currentUser.POS_LEVEL_NAME_FULL; } }
        #endregion

        #region 用户信息
        public string USER_ID { get { return _currentUser.USER_ID; } }
        public string USER_CODE { get { return _currentUser.USER_CODE; } }
        public string USER_NAME { get { return _currentUser.USER_NAME; } }
        public string USER_NAME_EN { get { return _currentUser.USER_NAME_EN; } }
        public string USER_PASSWORD { get { return _currentUser.USER_PASSWORD; } }
        public string USER_TYPE { get { return _currentUser.USER_TYPE; } }
        public string PIN_YIN { get { return _currentUser.PIN_YIN; } }

        public DateTime? LAST_LOGIN_TIME { get { return _currentUser.LAST_LOGIN_TIME; } }
        public int? LOGIN_STATE { get { return _currentUser.LOGIN_STATE; } }
        public string TICKET_ID { get { return _currentUser.TICKET_ID; } }
        public string DESCRIPTION { get { return _currentUser.DESCRIPTION; } }
        public DateTime ACTIVE_TIME { get { return _currentUser.ACTIVE_TIME; } }
        public DateTime DISABLE_TIME { get { return _currentUser.DISABLE_TIME; } }
        public int? SORT_ID { get { return _currentUser.SORT_ID; } }
        public string REMARK { get { return _currentUser.REMARK; } }
        public DateTime CREATE_TIME { get { return _currentUser.CREATE_TIME; } }
        public string CREATOR_ID { get { return _currentUser.CREATOR_ID; } }
        public string CREATOR { get { return _currentUser.CREATOR; } }
        public string MODIFIER_ID { get { return _currentUser.MODIFIER_ID; } }
        public string MODIFIER { get { return _currentUser.MODIFIER; } }

        public DateTime LAST_UPDATED_TIME { get { return _currentUser.LAST_UPDATED_TIME; } }
        public string IS_ENABLED { get { return _currentUser.IS_ENABLED; } }
        public string IS_SYSTEM { get { return _currentUser.IS_SYSTEM; } }
        public string UPDATE_CONTROL_ID { get { return _currentUser.UPDATE_CONTROL_ID; } }
        public string TFLAG { get { return _currentUser.TFLAG; } }
        #endregion

        //public DateTime SERVER_TIME { get { return _currentUser.SERVER_TIME; } set { _currentUser.SERVER_TIME = value; } } //服务器时间
        #region 表
        //public DataTable DT_POSITION { get { return _currentUser.DT_POSITION; } } //岗位清单表
        #endregion
    }
}
