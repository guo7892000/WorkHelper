using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class DBSqlEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAlias { get; set; }
        /// <summary>
        /// SQL类型
        /// </summary>
        public SqlType SqlType { get; set; }

        /// <summary>
        /// SQL参数格式类型
        /// </summary>
        public SqlParamFormatType ParamType { get; set; }
        public string ParamCol { get; set; }

        public bool IsHasRemark { get; set; }
        public string NewLine { get; set; }
        public string Tab { get; set; }
        public bool IsUseGlobal { get; set; }
        public WordUpperType WordUpperType { get; set; }


    }
}
