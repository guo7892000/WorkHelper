using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColOracleTemplate
    {
        /*主键名	序列名	唯一约束名	外键	外键名*/
        public string PKName;
        public string SequenceName;
        public string UniqueName;
        public string FK;
        public string FKName;

        public static ColOracleTemplate GetEntity(DataRow dr)
        {
            ColOracleTemplate ent = new ColOracleTemplate();
            ent.PKName = dr[ExcelCol.PKName].ToString().Trim();
            ent.SequenceName = dr[ExcelCol.SequenceName].ToString().Trim().ToUpper();
            ent.UniqueName = dr[ExcelCol.UniqueName].ToString().Trim();
            ent.FK = dr[ExcelCol.UniqueName].ToString().Trim();
            ent.FKName = dr[ExcelCol.FKName].ToString().ToLower().Trim();

            return ent;
        }

        public static bool ValidateData(DataTable dtTable, DataTable dtAllCol, out StringBuilder sb)
        {
            sb = new StringBuilder();
            DataRow[] drNewArray = dtTable.Select(ColCommon.ExcelCol.ChangeType + "='新增'");
            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew[EntTable.ExcelTable.Code].ToString();
                string strTableName = drNew[EntTable.ExcelTable.Name].ToString();
                string strChangeType = drNew[ColCommon.ExcelCol.ChangeType].ToString();

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='FK' and (外键 is not null and 外键名 not like 'FK_%')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“外键名”列内容格式必须以“FK_”开头！");
                }

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and (序列名 is not null and 序列名 not like 'SQ_%')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中，“序列名”列内容格式必须以“SQ_”开头！");
                }
                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and (唯一约束名 is not null and 唯一约束名 not like 'UQ_%')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中，“唯一约束名”列内容格式必须以“UQ_”开头！");
                }
                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and 键='FK' and (外键 is null or 外键名 is null)").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“外键、外键名”列为必填！");
                }
            }

            //返回结果
            return string.IsNullOrEmpty(sb.ToString());
        }

        public static class ExcelCol
        {
            public static string PKName = "主键名";
            public static string SequenceName = "序列名";
            public static string UniqueName = "唯一约束名";
            public static string FK = "外键";
            public static string FKName = "外键名";
        }
    }
}
