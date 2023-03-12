using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Breezee.Core.Interface;
using System.Collections;
using Breezee.Core.Entity;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// DataTable扩展方法
    /// </summary>
    public static class DataTableExtension
    {
        #region 设置表通用列默认值
        /// <summary>
        /// 设置表通用列默认值
        /// </summary>
        /// <param name="dt">单表</param>
        /// <returns></returns>
        public static DataTable DefaultValue(this DataTable dt, LoginUserInfo loginUser = null)
        {
            //if (dt.Columns.Contains(DT_SYS_USER.CREATE_TIME))
            //{
            //    dt.Columns[DT_SYS_USER.CREATE_TIME].ExtProp(TableCoulnmDefaultType.DateTime);
            //}
            //if (dt.Columns.Contains(DT_SYS_USER.LAST_UPDATED_TIME))
            //{
            //    dt.Columns[DT_SYS_USER.LAST_UPDATED_TIME].ExtProp(TableCoulnmDefaultType.DateTime);
            //}
            //if (dt.Columns.Contains(DT_SYS_USER.UPDATE_CONTROL_ID))
            //{
            //    dt.Columns[DT_SYS_USER.UPDATE_CONTROL_ID].ExtProp(TableCoulnmDefaultType.Guid);
            //}
            //if (dt.Columns.Contains(DT_SYS_USER.IS_ENABLED))
            //{
            //    dt.Columns[DT_SYS_USER.IS_ENABLED].DefaultValue = "1";
            //}
            //if (dt.Columns.Contains(DT_SYS_USER.IS_SYSTEM))
            //{
            //    dt.Columns[DT_SYS_USER.IS_SYSTEM].DefaultValue = "0";
            //}
            //if (dt.Columns.Contains(DT_SYS_USER.TFLAG))
            //{
            //    dt.Columns[DT_SYS_USER.TFLAG].DefaultValue = "0";
            //}
            //if (loginUser != null)
            //{
            //    if (dt.Columns.Contains(DT_SYS_USER.ORG_ID))
            //    {
            //        dt.Columns[DT_SYS_USER.ORG_ID].DefaultValue = loginUser.ORG_ID;
            //    }
            //    if (dt.Columns.Contains(DT_SYS_USER.CREATOR_ID))
            //    {
            //        dt.Columns[DT_SYS_USER.CREATOR_ID].DefaultValue = loginUser.USER_ID;
            //    }
            //    if (dt.Columns.Contains(DT_SYS_USER.MODIFIER_ID))
            //    {
            //        dt.Columns[DT_SYS_USER.MODIFIER_ID].DefaultValue = loginUser.USER_ID;
            //    }
            //    if (dt.Columns.Contains(DT_SYS_USER.CREATOR))
            //    {
            //        dt.Columns[DT_SYS_USER.CREATOR].DefaultValue = loginUser.USER_NAME;
            //    }
            //    if (dt.Columns.Contains(DT_SYS_USER.MODIFIER))
            //    {
            //        dt.Columns[DT_SYS_USER.MODIFIER].DefaultValue = loginUser.USER_NAME;
            //    }
            //}
            return dt;
        }
        #endregion

        #region 设置列默认值类型
        public static void DefaultType(this DataTable dt, string strColumnName, DbDefaultValueType defaultType)
        {
            if (dt.Columns.Contains(strColumnName))
            {
                dt.Columns[strColumnName].ExtendedProperties[StaticConstant.FRA_TABLE_EXTEND_PROPERTY_COLUMNS_FIX_VALUE] = DbDefaultValueType.DateTime;
            }
            else
            {
                throw new Exception(string.Format("表没有名为{0}的列！", strColumnName));
            }
        }
        #endregion
    }
}
