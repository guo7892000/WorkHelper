using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.IOC
{
    /// <summary>
    /// 实现DLL信息
    /// </summary>
    public class ImplementDllInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// DLL名称：必填，用于IoC导入安装类
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

    }
}
