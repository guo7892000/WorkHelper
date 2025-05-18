using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
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
        public static bool ValidateData(DataTable dtTable, DataTable dtAllCol, GenerateParamEntity paramEntity, out StringBuilder sb)
        {
            sb = new StringBuilder();
            DataRow[] drNewArray = dtTable.Select(ColCommon.ExcelCol.ChangeType + "='新增'");
            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew[EntTable.ExcelTable.Code].ToString().Trim();
                string sFilter = string.Format("{0}='{1}' and {2}='PK' and ({3} is null or {3} not like 'PK_%')", EntTable.ExcelTable.Code, strTableCode, ColCommon.ExcelCol.KeyType, ExcelCol.PKName);
                DataRow[] dataRows = dtAllCol.Select(sFilter);
                if (dataRows.Length > 0)
                {
                    // sb.AppendLine("新增的表" + strTableCode + "中键为“PK”时，“" + ExcelCol.PKName + "”列内容不能为空，且必须以PK_开头！");
                    dataRows[0][ExcelCol.PKName] = "PK_" + strTableCode; //这里直接赋值，就不报错了
                }

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and ("+ ExcelCol.UniqueName + " is not null and "+ ExcelCol.UniqueName + " not like 'UQ_%')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中，“"+ ExcelCol.UniqueName + "”列内容格式必须以“UQ_”开头！");
                }

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and "+ ColCommon.ExcelCol.KeyType + "='FK' and ("+ ExcelCol.FK + " is null or "+ ExcelCol.FKName + " is null)").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“"+ ExcelCol.FK + "、"+ ExcelCol.FKName + "”列为必填！");
                }

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='FK' and ("
                    + ExcelCol.FK + " is not null and " + ExcelCol.FKName + " not like 'FK_%')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“"+ ExcelCol.FKName + "”列内容格式必须以“FK_”开头！");
                }
            }

            //返回结果
            return string.IsNullOrEmpty(sb.ToString());
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
