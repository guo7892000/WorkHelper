using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
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
        public static bool ValidateData(DataTable dtTable, DataTable dtAllCol, GenerateParamEntity paramEntity, out StringBuilder sb)
        {
            sb = new StringBuilder();
            DataRow[] drNewArray = dtTable.Select(ColCommon.ExcelCol.ChangeType + "='新增'");
            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew[EntTable.ExcelTable.Code].ToString();
                string strTableName = drNew[EntTable.ExcelTable.Name].ToString();
                string strChangeType = drNew[ColCommon.ExcelCol.ChangeType].ToString();

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='FK' and " + ExcelCol.FK + " is null ").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“" + ExcelCol.FK + "”列内容不能为空，格式：外键表(外键)！");
                }
                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ExcelCol.AutoNum + " ='是' and " + ColCommon.ExcelCol.DataType + " not in ('int','bigint') ").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中，“" + ExcelCol.AutoNum + "”列内容为【是】时，" + ColCommon.ExcelCol.DataType + "必须为int或bigint！");
                }

            }

            //返回结果
            return string.IsNullOrEmpty(sb.ToString());
        }
        public static class ExcelCol
        {
            public static string AutoNum = "自增长设置";
            public static string Unique = "唯一性";
            public static string FK = "外键";
        }
    }
}
