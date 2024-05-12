using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI.TableSql
{
    /// <summary>
    /// SQL替换中替换后的列清单
    /// </summary>
    public class SqlReplaceColumnEntity: IEntity
    {
        public string OldTable { get; set; }
        public string OldTableAlias { get; set; }
        public string OldCol { get; set; }
        public string OldColCn { get; set; }
        public string NewCol { get; set; }
        public string NewTable { get; set; }

        public string Id => Guid.NewGuid().ToString();

        public class PropString
        {
            public static readonly string OldTable = "OldTable";
            public static readonly string OldTableAlias = "OldTableAlias";
            public static readonly string OldCol = "OldCol";
            public static readonly string OldColCn = "OldColCn";
            public static readonly string NewCol = "NewCol";
            public static readonly string NewTable = "NewTable";
        }
    }
}
