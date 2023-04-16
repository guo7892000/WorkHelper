using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL
{
    /// <summary>
    /// Excel导入生成模块-表
    /// </summary>
    public class EntTable
    {
        /*序号	变更类型 表名称	表编码 通用列的表名	备注*/
        public string Num;

        public string ChangeType;
        public TableChangeType ChangeTypeEnum;

        public string Name;
        public string Code;
        public string Remark;
        public string CommonColumnTableCode;

        public static EntTable GetEntity(DataRow dr)
        {
            EntTable ent = new EntTable();
            ent.Num = dr[ExcelTable.Num].ToString().Trim();

            string sChangeType = dr[ExcelTable.ChangeType].ToString().Trim();
            sChangeType = string.IsNullOrEmpty(sChangeType) ? "新增" : sChangeType;
            ent.ChangeType = sChangeType;
            ent.ChangeTypeEnum = GetTableChangeType(sChangeType);

            ent.Name = dr[ExcelTable.Name].ToString().Trim();
            ent.Code = dr[ExcelTable.Code].ToString().Trim();
            ent.Remark = dr[ExcelTable.Remark].ToString().Trim();
            ent.CommonColumnTableCode = dr[ExcelTable.CommonColumnTableCode].ToString().Trim();
            return ent;
        }

        public static DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(ExcelTable.Num),
                new DataColumn(ExcelTable.ChangeType),
                new DataColumn(ExcelTable.Name),
                new DataColumn(ExcelTable.Code),
                new DataColumn(ExcelTable.CommonColumnTableCode),
                new DataColumn(ExcelTable.Remark),
            });
            return dt;
        }

        /// <summary>
        /// Excel模板中Sheet为【表】中的列信息
        /// </summary>
        public static class ExcelTable
        {
            public static string SheetName = "表";

            public static string Num = "序号";
            public static string ChangeType = "变更类型";
            public static string Name = "表名称";
            public static string Code = "表编码";
            public static string CommonColumnTableCode = "通用列的表名";
            public static string Remark = "备注";
        }

        private static TableChangeType GetTableChangeType(string s)
        {
            if (s.Equals("修改")) return TableChangeType.Alter;
            return TableChangeType.Create;
        }

    }

    
}
