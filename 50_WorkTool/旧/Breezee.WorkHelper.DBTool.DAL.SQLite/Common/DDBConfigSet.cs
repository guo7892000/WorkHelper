using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.WorkHelper.DBTool.IDAL;

namespace Breezee.WorkHelper.DBTool.DAL.SQLite
{
    public class DDBConfigSet : Breezee.WorkHelper.DBTool.DAL.DDBConfigSet
    {
        /// <summary>
        /// SQLite需要对时间字符串做转换
        /// STRFTIME(A.CREATE_TIME) AS CREATE_TIME
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        //public override DataTable QueryDbConfig(IDictionary<string, string> dicQuery)
        //{
        //    return DataAccess.QueryDataFromConfigPath(@"SQLConfig/DBTool/DBConfigSet/QueryDbConfig", dicQuery);
        //}
    }
}
