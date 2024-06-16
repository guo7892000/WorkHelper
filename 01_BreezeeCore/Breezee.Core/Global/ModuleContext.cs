using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core
{
    /// <summary>
    /// 模块上下文
    /// </summary>
    public abstract class ModuleContext: IContext
    {
        /// <summary>
        /// 值列表配置
        /// </summary>
        public abstract KeyValueGroupConfig KeyValuCfg { get; }
    }
}
