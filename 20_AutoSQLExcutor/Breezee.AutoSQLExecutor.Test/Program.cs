using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.AutoSQLExecutor.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SqlServer示例
            SqlServerTest();

            //MySql示例
            MySqlTest();
            Console.ReadKey();
        }

        static void SqlServerTest()
        {
            DbServerInfo server = new DbServerInfo(DataBaseType.SqlServer,"localhost","sa","sa123456", "PeachBase","1433",null,null);
            IDataAccess dataAccess = Common.AutoSQLExecutors.Connect(server);
            string sql = "select * from bas_city where city_id=#CITY_ID#";//参数区分大小写
            IDictionary<string,object> dicCond = new Dictionary<string, object>();
            dicCond["CITY_ID"] = 100;
            DataTable dtQuery = dataAccess.QueryData(sql, dicCond);
            Console.WriteLine(dtQuery.Rows.Count);
        }

        static void MySqlTest()
        {
            DbServerInfo server = new DbServerInfo(DataBaseType.MySql, "localhost", "root", "root", "aprilspring", "3306", null, null);
            IDataAccess dataAccess = Common.AutoSQLExecutors.Connect(server);
            string sql = "select * from bas_city where city_id in (#CITY_ID:LI#)"; //这里必须加上LI，表示是整型的列表
            IDictionary<string, object> dicCond = new Dictionary<string, object>();
            dicCond["CITY_ID"] = new List<int> { 1, 100 }; 
            DataTable dtQuery = dataAccess.QueryData(sql, dicCond);
            Console.WriteLine(dtQuery.Rows.Count);
        }
    }
}
