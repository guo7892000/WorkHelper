using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColPostgreSqlTemplate
    {
        /*主键名	唯一约束名	外键	外键名*/
        public string PKName;
        public string UniqueName;
        public string FK;
        public string FKName;

        public static ColPostgreSqlTemplate GetEntity(DataRow dr)
        {
            ColPostgreSqlTemplate ent = new ColPostgreSqlTemplate();
            ent.PKName = dr[ExcelCol.PKName].ToString().Trim();
            ent.UniqueName = dr[ExcelCol.UniqueName].ToString().Trim();
            ent.FK = dr[ExcelCol.UniqueName].ToString().Trim();
            ent.FKName = dr[ExcelCol.FKName].ToString().ToLower().Trim();

            return ent;
        }

        public static class ExcelCol
        {
            public static string PKName = "主键名";
            public static string UniqueName = "唯一约束名";
            public static string FK = "外键";
            public static string FKName = "外键名";
        }
    }
}
