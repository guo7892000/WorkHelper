using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 键值对的XML配置字符
    /// </summary>
    public class KeyValuePairString
    {
        public static string Root = "config";
        public static string Item = "data";
        public static string PK = Columns.Key;
        public static List<string> getList()
         {
            return new List<string>()
            {
                Columns.Key,
                Columns.Value,
                Columns.Remark
            };
        }

        /// <summary>
        /// 内部类
        /// </summary>
        public static class Columns
        {
            public static string Key = "key";
            public static string Value = "value";
            public static string Remark = "remark";
        }
    }

    
}
