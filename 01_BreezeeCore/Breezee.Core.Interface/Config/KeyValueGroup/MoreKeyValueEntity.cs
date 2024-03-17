using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 自定义键值构建实体
    /// </summary>
    public class MoreKeyValueEntity
    {
        /// <summary>
        /// 目录名
        /// </summary>
        public string DirectoryName { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string FullFileName { get { return Path.Combine(DirectoryName, FileName); } }
        /// <summary>
        /// XML配置根节点名
        /// </summary>
        public string XmlRootName { get; set; } = "xml";
        /// <summary>
        /// 键XML配置节点名
        /// </summary>
        public string KeyXmlNodeName { get; set; } = "items";
        /// <summary>
        /// 值XML配置节点名
        /// </summary>
        public string ValXmlNodeName { get; set; } = "item";
        /// <summary>
        /// XML保存类型
        /// </summary>
        public XmlConfigSaveType XmlSaveType { get; set; } = XmlConfigSaveType.Attribute;
        /// <summary>
        /// 更多键名集合
        /// </summary>
        public List<string> ColKeys { get; set; } = new List<string>();
        /// <summary>
        /// 更多值名集合
        /// </summary>
        public List<string> ColVals { get; set; } = new List<string>();
        /// <summary>
        /// 键主键属性名
        /// </summary>
        public string KeyIdPropName { get; set; } = "kid";
        /// <summary>
        /// 值主键属性名
        /// </summary>
        public string ValIdPropName { get; set; } = "vid";

        public IDValueType ValueType { get; set; } = IDValueType.Guid;
    }
}
