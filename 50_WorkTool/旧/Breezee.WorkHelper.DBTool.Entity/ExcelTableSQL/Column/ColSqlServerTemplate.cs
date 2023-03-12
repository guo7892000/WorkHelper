using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColSqlServerTemplate
    {
        /*自增长设置	唯一性	外键*/
        public string AutoNum;
        public YesNoType Unique;
        public string FK;

        public static ColSqlServerTemplate GetEntity(DataRow dr)
        {
            ColSqlServerTemplate ent = new ColSqlServerTemplate();            
            ent.AutoNum = dr[ExcelCol.AutoNum].ToString().Trim();
            ent.Unique = TableSqlCommon.GetYesNoType(dr[ExcelCol.Unique].ToString().Trim());
            ent.FK = dr[ExcelCol.FK].ToString().Trim();
  
            return ent;
        }

        public static class ExcelCol
        {
            public static string AutoNum = "自增长设置";
            public static string Unique = "唯一性";
            public static string FK = "外键";
        }
    }
}
