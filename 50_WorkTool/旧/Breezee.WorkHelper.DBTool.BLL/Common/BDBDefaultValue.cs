using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.IDAL;
using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.Core.IOC;

namespace Breezee.WorkHelper.DBTool.BLL
{
    /// <summary>
    /// 数据库配置设置
    /// </summary>
    public class BDBDefaultValue : IDBDefaultValue
    {
        #region 查询数据库配置设置
        /// <summary>
        /// 查询数据库配置设置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public override IDictionary<string, object> QueryDefaultValue(IDictionary<string, string> dicQuery)
        {
            IDictionary<string, object> dicRet = new Dictionary<string, object>();
            try
            {
                var dal = ContainerContext.Container.Resolve<IDDBDefaultValue>();
                dicRet = ExecuteResultHelper.QuerySuccess();
                dicRet[StaticConstant.FRA_QUERY_RESULT] = dal.QueryDefaultValue(dicQuery);
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
            }
            return dicRet;
        }
        #endregion

        #region 保存数据库配置
        /// <summary>
        /// 保存数据库配置
        /// </summary>
        /// <author>pansq</author>
        /// <status>已完成</status>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public override IDictionary<string, object> SaveDefaultValue(IDictionary<string, object> dicIn)
        {
            IDictionary<string, object> dicRet;
            DbTransaction DbTran = null;
            try
            {
                //1.保存前判断是否存在
                DataTable dtSaveSupply = dicIn[IDBDefaultValue.SaveDefaultValue_InDicKey.DT_TABLE] as DataTable;

                IDictionary<string, string> dicQuery = new Dictionary<string, string>();
                dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME] = dtSaveSupply.Rows[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME].ToString();
                //修改判断需要排除当前记录
                if (dtSaveSupply.Rows[0].RowState == DataRowState.Modified)
                {
                    dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.COL_DEFAULT_ID] = dtSaveSupply.Rows[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.COL_DEFAULT_ID].ToString();
                }

                var dal = ContainerContext.Container.Resolve<IDDBDefaultValue>();
                DataTable dtExists = dal.QueryDefaultValueExist(dicQuery);
                if (dtExists != null && dtExists.Rows.Count > 0)
                {
                    return ExecuteResultHelper.Fail("保存失败：该配置已存在！");
                }

                //2.保存
                
                DbConnection con = DataAccess.GetCurrentConnection();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DbTran = con.BeginTransaction();
                //保存
                dtSaveSupply = DataAccess.SaveTable(dtSaveSupply, con, DbTran);

                DbTran.Commit();
                return ExecuteResultHelper.Success();
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
                if (DbTran != null)
                {
                    DbTran.Rollback();
                }
            }
            return dicRet;
        }
        #endregion
    }
}
