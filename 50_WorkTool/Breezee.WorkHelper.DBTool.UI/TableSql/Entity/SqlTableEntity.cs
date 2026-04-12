using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// SQL中的表信息
    /// </summary>
    public class SqlTableEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 组织
        /// </summary>
        public string Schema { get; set; }
    }
}
