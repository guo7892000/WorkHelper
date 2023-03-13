using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.AutoSQLExecutor.Core;
using Breezee.WorkHelper.DBTool.IDAL;

namespace Breezee.WorkHelper.DBTool.DAL
{
    /// <summary>
    /// 数据库访问层-通用
    /// </summary>
    public class DDBDefaultValue : IDDBDefaultValue
    {
        public override DataTable QueryDefaultValue(IDictionary<string, string> dicQuery)
        {
            return DataAccess.QueryAutoParamConfigPathData(@"SQL/DBTool/DefaultValue/QueryDefaultValue", dicQuery);
        }

        #region 查询是否存在
        public override DataTable QueryDefaultValueExist(IDictionary<string, string> dicQuery)
        {
            //查询并返回结果
            var querySqlBuilder = new QuerySqlBuilder(DataAccess);
            querySqlBuilder.Sql(
            @"SELECT 1 FROM DBT_BD_COLUMN_DEFAULT A
                WHERE 1=1
                AND A.COLUMN_NAME = @COLUMN_NAME
            ");
            querySqlBuilder.Sql("COL_DEFAULT_ID", "AND A.COL_DEFAULT_ID <> @COL_DEFAULT_ID");
            querySqlBuilder.Sql("COLUMN_NAME", ""); //此处必须添加
            return querySqlBuilder.Query(dicQuery);
        }
        #endregion
    }
}
