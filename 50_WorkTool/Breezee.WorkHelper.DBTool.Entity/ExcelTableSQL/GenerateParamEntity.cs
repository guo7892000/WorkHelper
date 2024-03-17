using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// 生成表结构的参数实体
    /// </summary>
    public class GenerateParamEntity
    {
        /// <summary>
        /// 创建的SQL类型
        /// </summary>
        public SQLCreateType sqlCreateType { get; set; }
        /// <summary>
        /// 导入数据库类型
        /// </summary>
        public DataBaseType importDBType { get; set; }
        /// <summary>
        /// 生成数据库类型
        /// </summary>
        public DataBaseType targetDBType { get; set; }
        /// <summary>
        /// 是否综合转换
        /// </summary>
        public bool isAllConvert { get; set; }
        /// <summary>
        /// 是否默认主键
        /// </summary>
        public bool isDefaultPK { get; set; }
        /// <summary>
        /// 是否默认字段中文名
        /// </summary>
        public bool isDefaultColNameCN { get; set; }
        /// <summary>
        /// 默认字段中文名
        /// </summary>
        public string defaultColNameCN { get; set; }
    }
}
