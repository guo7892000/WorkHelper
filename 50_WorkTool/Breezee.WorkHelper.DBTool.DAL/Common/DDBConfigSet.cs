using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.AutoSQLExecutor.Core;
using Breezee.WorkHelper.DBTool.IDAL;

namespace Breezee.WorkHelper.DBTool.DAL
{
    public class DDBConfigSet : IDDBConfigSet
    {
        public override DataTable QueryDbConfig(IDictionary<string, string> dicQuery)
        {
            return DataAccess.QueryAutoParamConfigPathData(@"SQL/DBTool/DBConfigSet/QueryDBConfig", dicQuery);
        }

        #region 查询是否存在
        public override DataTable QueryDbConfigExist(IDictionary<string, string> dicQuery)
        {
            //查询并返回结果
            var querySqlBuilder = new QuerySqlBuilder(DataAccess);
            querySqlBuilder.Sql(
            @"SELECT 1 FROM DBT_BD_DB_CONFIG A
                WHERE 1=1
                AND A.DB_CONFIG_CODE = @DB_CONFIG_CODE
            ");
            querySqlBuilder.Sql("DB_CONFIG_ID", "AND A.DB_CONFIG_ID <> @DB_CONFIG_ID");
            querySqlBuilder.Sql("DB_CONFIG_CODE", ""); //此处必须添加
            return querySqlBuilder.Query(dicQuery);
        }
        #endregion

        #region 删除
        public override bool DeleteDbConfig(IDictionary<string, string> dicQuery)
        {
            //查询并返回结果
            string sSql = @"DELETE FROM DBT_BD_DB_CONFIG WHERE DB_CONFIG_ID = '#DB_CONFIG_ID#' ";
            int iUpdate = DataAccess.ExecuteNonQueryAutoParamSql(sSql, dicQuery);
            return iUpdate>0;
        }
        #endregion
    }
}
