using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 表结构生成文档的参数实体
    /// </summary>
    public class TableStructGeneratorParamEntity
    {
        public SQLBuilder builder { get; set; }
        /// <summary>
        /// 是否使用全类型
        /// </summary>
        public bool useDataTypeFull { get; set; }
        /// <summary>
        /// 是否使用LY模板
        /// </summary>
        public bool useLYTemplate { get; set; }
        /// <summary>
        /// 是否备注包含列名称
        /// </summary>
        public bool useRemarkContainsName { get; set; }
        /// <summary>
        /// 是否使用旧列编码：给数据迁移使用
        /// </summary>
        public bool useOldColumnCode { get; set; }
        /// <summary>
        /// 是否列名称跟备注一样
        /// </summary>
        public bool useNameSameWithRemark { get; set; }
        /// <summary>
        /// 导入数据库类型
        /// </summary>
        public DataBaseType importDBType { get; set; }
        /// <summary>
        /// 是否查询数据库
        /// </summary>
        public bool isQueryDataBase { get; set; }
        /// <summary>
        /// 默认表名或列名
        /// </summary>
        public string defaultColumnOrTableName { get; set; }
    }
}
