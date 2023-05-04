using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    public abstract class IXmlConfigPair: ConfigEntity
    {
        public abstract IXmlConfigPair XmlConfig { get; }
        public abstract void Set(string sKey, string sValue, string sRemark);
        public abstract KeyValuePairEntity GetEntity(string sKey, string sDefault);
        public abstract string Get(string sKey, string sDefault);
        public abstract bool Save();
    }
}
