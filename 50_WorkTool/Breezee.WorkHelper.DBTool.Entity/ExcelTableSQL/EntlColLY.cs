using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class EntlColLY
    {
        /// <summary>
        /// LY模板（xxx.xlsm）
        /// </summary>
        public static class ExcelCol
        {
            //模板页签名
            public static string SheetName = "1.0基础数据表结构";
            //字段信息
            public static string OrderNo = "序号";
            public static string Name = "列名称";
            public static string Code = "列编码";
            public static string DataType = "类型";
            public static string DataLength = "长度";
            public static string KeyType = "键";
            public static string NotNull = "必填";
            public static string Default = "默认值";
            public static string Remark = "备注";
            public static string ChangeType = "变更类型";
            //数据迁移
            public static string OldCode = "原字段";
            public static string OldName = "原中文名";
        }
    }
}
