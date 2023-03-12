using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Breezee.Core.Interface;

/***************************************************
 * 对象名称：登录用户实体类
 * 对象类别：实体类
 * 创建作者：黄国辉
 * 创建日期：2014-9-10
 * 对象说明：主要提供登录用户信息
 * 修改历史：
 *      V1.0 新建 hgh 2014-9-10
 * ************************************************/
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class SYS_USER: UserEntity
    {
        public MainFormModelEnum MainFormMode = MainFormModelEnum.FullFunction;

        #region 接口属性
        public string DataState { get; set; }
        public bool IS_SELECTED { get; set; }  
        #endregion

        #region 组织相关信息
        //员工信息
        public string EMP_ID { get; set; }
        public string EMP_NAME { get; set; }
        //所属组织信息
        public string ORG_ID { get; set; }
        public string ORG_NAME_SHORT { get; set; }
        public string ORG_NAME_FULL { get; set; }
        public string FORM_CODE_SHORT { get; set; }
        public string PARENT_ORG_ID { get; set; }
        public string BELONG_ORG_ID { get; set; }
        //部门信息
        public string DEPT_ORG_ID { get; set; }
        public string DEPT_ID { get; set; }
        public string DEPT_NAME_SHORT { get; set; }
        public string DEPT_NAME_FULL { get; set; }

        //岗位信息
        public string POSTION_ORG_ID { get; set; }
        public string POSTION_ID { get; set; }
        public string POSTION_NAME_SHORT { get; set; }
        public string POSTION_NAME_FULL { get; set; }

        //岗位级别信息
        public string POS_LEVEL_ORG_ID { get; set; }
        public string POS_LEVEL_ID { get; set; }
        public string POS_LEVEL_NAME_SHORT { get; set; }
        public string POS_LEVEL_NAME_FULL { get; set; }
        #endregion

        #region 用户信息
        public string USER_ID { get; set; }
        public string USER_CODE { get; set; }
        public string USER_NAME { get; set; }
        public string USER_NAME_EN { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_TYPE { get; set; }
        public string PIN_YIN { get; set; }

        public DateTime? LAST_LOGIN_TIME { get; set; }
        public int? LOGIN_STATE { get; set; }
        public string TICKET_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime ACTIVE_TIME { get; set; }
        public DateTime DISABLE_TIME { get; set; }
        public int? SORT_ID { get; set; }
        public string REMARK { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string CREATOR_ID { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER_ID { get; set; }
        public string MODIFIER { get; set; }

        public DateTime LAST_UPDATED_TIME { get; set; }
        public string IS_ENABLED { get; set; }
        public string IS_SYSTEM { get; set; }
        public string UPDATE_CONTROL_ID { get; set; }
        public string TFLAG { get; set; } 
        #endregion

        //public DateTime SERVER_TIME { get; set; } //服务器时间

        //public DataTable DT_POSITION { get; set; } //岗位清单表
    }

}
