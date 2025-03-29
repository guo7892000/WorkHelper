using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 静态值类
    /// </summary>
    public class StaticValue
    {
        /// <summary>
        /// 生成结果并复制到粘贴板
        /// </summary>
        public static readonly string GenResultCopySuccessMsg = "生成成功，并已复制到了粘贴板。详细见“生成结果”页签！";
        /// <summary>
        /// 生成结果
        /// </summary>
        public static readonly string GenResultOnlySuccessMsg = "生成成功，详细见“生成结果”页签！";
        /// <summary>
        /// 导入成功
        /// </summary>
        public static readonly string ImportSuccessMsg = "导入成功！可点“生成”按钮得到本次导入的变更SQL。";
    }
}
