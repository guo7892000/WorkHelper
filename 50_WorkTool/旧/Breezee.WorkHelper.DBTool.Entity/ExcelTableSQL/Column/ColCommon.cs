using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColCommon
    {
        /*变更类型	表编码	列名称	列编码	类型	长度	小数位	键	必填	默认值	备注	*/
        public string ChangeType;
        public ColumnChangeType ChangeTypeEnum;

        public string TableCode;
        public string Name;
        public string Code;
        public string DataType;
        public string DataLength;
        public string DataDotLength;

        public string KeyType;
        public ColKeyType KeyTypeEnum;

        public YesNoType NotNull;
        public string Default;
        public string Remark;

        public static ColCommon GetEntity(DataRow dr)
        {
            ColCommon ent = new ColCommon();
            ent.ChangeType = dr[ExcelCol.ChangeType].ToString().Trim();
            ent.ChangeTypeEnum = TableSqlCommon.GetColumnChangeType(dr[ExcelCol.ChangeType].ToString().Trim());
            ent.TableCode = dr[ExcelCol.TableCode].ToString().Trim();
            ent.Name = dr[ExcelCol.Name].ToString().Trim();
            ent.Code = dr[ExcelCol.Code].ToString().Trim();
            ent.DataType = dr[ExcelCol.DataType].ToString().ToLower().Trim();
            ent.DataLength = dr[ExcelCol.DataLength].ToString().Trim();
            ent.DataDotLength = dr[ExcelCol.DataDotLength].ToString().Trim();
            ent.KeyType = dr[ExcelCol.KeyType].ToString().Trim();
            ent.KeyTypeEnum = TableSqlCommon.GetColumnKeyType(dr[ExcelCol.KeyType].ToString().ToUpper().Trim());
            ent.NotNull = TableSqlCommon.GetYesNoType(dr[ExcelCol.NotNull].ToString().Trim());
            ent.Default = dr[ExcelCol.Default].ToString().Trim().Replace("'", "");
            ent.Remark = dr[ExcelCol.Remark].ToString().Trim();

            return ent;
        }

        public static class ExcelCol
        {
            public static string SheetName = "列";

            public static string ChangeType = "变更类型";
            public static string TableCode = "表编码";
            public static string Name = "列名称";
            public static string Code = "列编码";
            public static string DataType = "类型";
            public static string DataLength = "长度";
            public static string DataDotLength = "小数位";
            public static string KeyType = "键";
            public static string NotNull = "必填";
            public static string Default = "默认值";
            public static string Remark = "备注";
        }
    }
}
