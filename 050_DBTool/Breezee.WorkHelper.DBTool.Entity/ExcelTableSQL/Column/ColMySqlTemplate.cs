using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColMySqlTemplate
    {
        /*非负数 自增长	外键*/
        public YesNoType Nonnegative;
        public YesNoType AutoNum;
        public string FK;

        public static ColMySqlTemplate GetEntity(DataRow dr)
        {
            ColMySqlTemplate ent = new ColMySqlTemplate();
            ent.Nonnegative = TableSqlCommon.GetYesNoType(dr[ExcelCol.Nonnegative].ToString().Trim());
            ent.AutoNum = TableSqlCommon.GetYesNoType(dr[ExcelCol.AutoNum].ToString().Trim());
            ent.FK = dr[ExcelCol.FK].ToString().Trim();

            return ent;
        }

        public static class ExcelCol
        {
            public static string AutoNum = "自增长";
            public static string FK = "外键";
            public static string Nonnegative = "非负数";
        }
    }
}
