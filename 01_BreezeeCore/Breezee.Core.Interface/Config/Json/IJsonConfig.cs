using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    public abstract class IJsonConfig : ConfigEntity
    {
        public abstract MiniJsonConfig MiniJsonConfig { get; }
        public abstract void Set(string sKey, IDictionary<string, string> dicConfig);
        /// <summary>
        /// 根据主键值获取
        /// </summary>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public abstract DataRow[] Get(string pkValue);
        /// <summary>
        /// 根据指定的键及其值来查找
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public abstract DataRow[] Get(string sKey,string sValue);
        public abstract bool Save();
    }
}
