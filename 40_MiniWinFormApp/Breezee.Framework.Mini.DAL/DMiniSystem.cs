using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.Framework.Mini.IDAL;

namespace Breezee.Framework.Mini.DAL
{
    public class DMiniSystem : IDMiniSystem
    {
        //public override DataTable QueryDbConfig(IDictionary<string, string> dicQuery)
        //{
        //    //return DataAccess.QueryDataFromConfigPath(@"SQL/DBTool/DBConfigSet/QueryDBConfig", dicQuery);
        //    return DBToolBaseDAC.Dac.QueryDataFromConfigPath(@"SQL/DBTool/DBConfigSet/QueryDBConfig", dicQuery);
        //}

        #region 查询供应商是否存在
        //public override DataTable QueryDbConfigExist(IDictionary<string, string> dicQuery)
        //{
        //    //查询并返回结果
        //    var querySqlBuilder = new QuerySqlBuilder(DBToolBaseDAC.Dac);
        //    querySqlBuilder.Sql(
        //    @"SELECT 1 FROM DBT_BD_DB_CONFIG A
        //        WHERE 1=1
        //        AND A.DB_CONFIG_CODE = @DB_CONFIG_CODE
        //    ");
        //    querySqlBuilder.Sql("DB_CONFIG_ID", "AND A.DB_CONFIG_ID <> @DB_CONFIG_ID");
        //    querySqlBuilder.Sql("DB_CONFIG_CODE", ""); //此处必须添加
        //    return querySqlBuilder.Query(dicQuery);
        //}
        #endregion
    }
}
