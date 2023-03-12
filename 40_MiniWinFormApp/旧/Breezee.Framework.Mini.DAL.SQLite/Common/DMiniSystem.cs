using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.DAL.SQLite
{
    public class DMiniSystem : DAL.DMiniSystem
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
