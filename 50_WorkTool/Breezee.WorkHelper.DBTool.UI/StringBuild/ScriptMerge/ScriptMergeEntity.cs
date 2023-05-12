using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并脚本实体
    /// </summary>
    public class ScriptMergeEntity
    {
        /// <summary>
        /// 数据库类型：包含SqlServer、Oracle、MySql、SQLite、PostgreSql
        /// </summary>
        public static string DBType;
        /// <summary>
        /// 源目录：查找文件的根目录，如不设置，那么以选择的配置文件所在的目录为根目录
        /// </summary>
        public static string SourcePath;
        /// <summary>
        /// 目标目录：生成目录，如不设置，那么为配置文件路径
        /// </summary>
        public static string TargetPath;

        /// <summary>
        /// 分类节点属性
        /// </summary>
        public static class ScriptMergeItem
        {
            /// <summary>
            /// 对象类型：包括t(表)、f(函数)、v(视图)、p(存储过程)、d(数据)。
            /// </summary>
            public static string ObjectType;
            /// <summary>
            /// 最终文件名
            /// </summary>
            public static string FinalName;
            /// <summary>
            /// 源相对目录名：可只对某个相对目录
            /// </summary>
            public static string SourcePathRel;
            /// <summary>
            /// 源绝对目录名：可只对某个绝对目录，这样会忽略根目录配置的源路径
            /// </summary>
            public static string SourcePathAbs;

            /// <summary>
            /// 字符集类型（单选，包括utf8,utf16,utf32）
            /// </summary>
            public static string CharSet;

            /// <summary>
            /// 文件扩展名(逗号分隔多个扩展名，如txt,sql,prc)
            /// </summary>
            public static string FileExt;
        }

        
    }
}
