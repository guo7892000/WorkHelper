using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Framework.Mini.StartUp
{
    [Serializable]
    public class LatestVerion
    {
        /// <summary>
        /// 最新版本
        /// </summary>
        public string version;
        /// <summary>
        /// 发布日期
        /// </summary>
        public string date;
    }
}
