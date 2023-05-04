using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 键值对的XML配置实体
    /// </summary>
    public class KeyValuePairEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public KeyValuePairEntity(string key,string value,string comment)
        {
            Key = key;
            Value= value;
            Comment = comment;
        }
    } 
}
