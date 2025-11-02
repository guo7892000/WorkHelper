using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// Java发布文件配置
    /// </summary>
    public class JavaPublishFileConfig
    {
        public MoreKeyValueGroupConfig MoreXmlConfig { get; set; }
        public JavaPublishFileConfig(string sFileName)
        {
            var entity = new MoreKeyValueEntity();
            entity.DirectoryName = GlobalContext.PathData();
            entity.FileName = sFileName;
            // 键
            entity.ColKeys.Add(KeyString.Name);
            entity.ColKeys.Add(KeyString.CodeDir);
            entity.ColKeys.Add(KeyString.CopyToDir);
            entity.ColKeys.Add(KeyString.CopyCoverType);
            entity.ColKeys.Add(KeyString.ClassDir);
            entity.ColKeys.Add(KeyString.ExcludeRelateDir);
            entity.ColKeys.Add(KeyString.ExcludeRelateFile);
            entity.ColKeys.Add(KeyString.ExcludeExt);
            entity.ColKeys.Add(KeyString.DateTimeBegin);
            entity.ColKeys.Add(KeyString.DateTimeEnd);
            entity.ColKeys.Add(KeyString.Email);
            entity.ColKeys.Add(KeyString.UserName);
            entity.ColKeys.Add(KeyString.SourceGetType);
            // 值
            entity.ColVals.Add(ValueString.IsCopyFromSrc);
            entity.ColVals.Add(ValueString.RelCodeDir);
            entity.ColVals.Add(ValueString.RelClassDir);
            entity.ColVals.Add(ValueString.RelCopyToDir);
            MoreXmlConfig = new MoreKeyValueGroupConfig(entity);
        }

        /// <summary>
        /// 代码目录、class目录、复制到目录等配置
        /// </summary>
        public static class KeyString
        {
            public static readonly string Name = "Name";
            public static readonly string CodeDir = "CodeDir";
            public static readonly string CopyToDir = "CopyToDir";
            public static readonly string ClassDir = "ClassDir";
            public static readonly string CopyCoverType = "CopyCoverType";
            public static readonly string ExcludeRelateDir = "ExcludeRelateDir";
            public static readonly string ExcludeRelateFile = "ExcludeRelateFile";
            public static readonly string ExcludeExt = "ExcludeExt";
            public static readonly string DateTimeBegin = "DateTimeBegin";
            public static readonly string DateTimeEnd = "DateTimeEnd";
            public static readonly string Email = "Email";
            public static readonly string UserName = "UserName";
            public static readonly string SourceGetType = "SourceGetType";
        }

        /// <summary>
        /// 代码与class对应关系配置
        /// </summary>
        public static class ValueString
        {
            /// <summary>
            /// 是否从源码复制
            /// </summary>
            public static readonly string IsCopyFromSrc = "IsCopyFromSrc";
            public static readonly string RelCodeDir = "RelCodeDir";
            public static readonly string RelClassDir = "RelClassDir";
            public static readonly string RelCopyToDir = "RelCopyToDir";
        }

    }
}
