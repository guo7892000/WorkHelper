using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并脚本类型
    /// </summary>
    public class ScriptMergeString
    {
        /// <summary>
        /// 节点字符
        /// </summary>
        public static class NodeString
        {
            public static string Root = "configuration";
            public static string Class = "class";
            public static string ClassPath = Root + "/" + Class;
        }
        /// <summary>
        /// 根节点属性
        /// </summary>
        public static class RootProp
        {
            /// <summary>
            /// 数据库类型：包含SqlServer、Oracle、MySql、SQLite、PostgreSql
            /// </summary>
            public static string DBType = "dbType";
            /// <summary>
            /// 源目录：查找文件的根目录，如不设置，那么以选择的配置文件所在的目录为根目录
            /// </summary>
            public static string SourcePath = "sourcePath";
            /// <summary>
            /// 目标目录：生成目录，如不设置，那么为配置文件路径
            /// </summary>
            public static string TargetPath = "targetPath";
        }
        /// <summary>
        /// 分类节点属性
        /// </summary>
        public static class ClassProp
        {
            /// <summary>
            /// 对象类型：包括t(表)、f(函数)、v(视图)、p(存储过程)、d(数据)。
            /// </summary>
            public static string ObjectType = "objectType";
            /// <summary>
            /// 最终文件名
            /// </summary>
            public static string FinalName = "finalName";
            /// <summary>
            /// 源相对目录名：可只对某个相对目录
            /// </summary>
            public static string SourcePathRel = "sourcePathRel";
            /// <summary>
            /// 源绝对目录名：可只对某个绝对目录，这样会忽略根目录配置的源路径
            /// </summary>
            public static string SourcePathAbs = "sourcePathAbs";

            public static string CharSet = "charset";

            /// <summary>
            /// 文件扩展名(逗号分隔多个扩展名，如txt,sql,prc)
            /// </summary>
            public static string FileExt = "fileExt";
        }
    }
}
