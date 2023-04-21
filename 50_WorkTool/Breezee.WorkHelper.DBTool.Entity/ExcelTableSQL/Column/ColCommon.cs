using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL.EntTable;

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
        //这个不在Excel模板内，会自动转换
        public string DataTypeFull;
        public string DataTypeNew;
        public string DataTypeFullNew;
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
            //这个不在Excel模板内，会自动转换
            ent.DataTypeNew = ent.DataType;
            string sDataType_Full = GetFullDataType(ent.DataType,ent.DataLength,ent.DataDotLength);
            ent.DataTypeFull = sDataType_Full;
            ent.DataTypeFullNew = sDataType_Full;
            return ent;
        }

        public static string GetFullDataType(string sDataType,string sDataLength,string sDataDotLength)
        {
            //得到全类型
            if(sDataType.Contains("("))
            {
                return sDataType;
            }
            string sDataType_Full = string.Empty;
            if (!string.IsNullOrEmpty(sDataLength))
            {
                if (!string.IsNullOrEmpty(sDataDotLength))
                {
                    sDataType_Full = sDataType + AddLeftRightKuoHao(sDataLength + "," + sDataDotLength);
                }
                else
                {
                    sDataType_Full = sDataType + AddLeftRightKuoHao(sDataLength);
                }
            }
            else
            {
                sDataType_Full = AddRightBand(sDataType);
            }

            return sDataType_Full;
        }

        public static DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(ExcelCol.ChangeType),
                new DataColumn(ExcelCol.TableCode),
                new DataColumn(ExcelCol.Name),
                new DataColumn(ExcelCol.Code),
                new DataColumn(ExcelCol.DataType),
                new DataColumn(ExcelCol.DataLength),
                new DataColumn(ExcelCol.DataDotLength),
                new DataColumn(ExcelCol.KeyType),
                new DataColumn(ExcelCol.NotNull),
                new DataColumn(ExcelCol.Default),
                new DataColumn(ExcelCol.Remark),
                //这个不在Excel模板内，会自动转换
                new DataColumn(ExcelCol.DataTypeFull),
                new DataColumn(ExcelCol.DataTypeNew),
                new DataColumn(ExcelCol.DataTypeFullNew),
            });
            return dt;
        }

        private static string AddRightBand(string strColCode)
        {
            return strColCode + " ";
        }
        private static string AddLeftRightKuoHao(string strColCode)
        {
            return "(" + strColCode + ")";
        }

        public static bool ValidateData(DataTable dtTable, DataTable dtAllCol, out StringBuilder sb)
        {
            sb = new StringBuilder();

            if (dtTable.Select(ExcelCol.ChangeType + " not in ('新增','修改')").Length > 0)
            {
                sb.AppendLine(ExcelCol.ChangeType + "只能是“新增”或“修改”！");
            }
            if (dtTable.Select(ExcelCol.ChangeType + "='新增' and (" + ExcelTable.Name + "='' or " + ExcelTable.Code + "='')").Length > 0)
            {
                sb.AppendLine("新增的表，其“" + ExcelTable.Name + "、" + ExcelTable.Code + "”都不能为空！");
            }
            if (dtAllCol.Select(ExcelTable.Code + "=''").Length > 0)
            {
                sb.AppendLine("新增的列中" + ExcelTable.Code + "不能为空！");
            }
            if (dtAllCol.Select(ExcelTable.Code + "='' or " + ExcelCol.Name + "='' or " + ExcelCol.Code + "='' or " + ExcelCol.DataType + "=''").Length > 0)
            {
                sb.AppendLine("新增的列中" + ExcelTable.Code + "、" + ExcelCol.Name + "、" + ExcelCol.Code + "、" + ExcelCol.DataType + "不能为空！");
            }
            foreach (DataRow dr in dtAllCol.Select("(" + ExcelCol.DataType + " like 'VARCHAR%' or " + ExcelCol.DataType + " like 'NVARCHAR%' or "
                + ExcelCol.DataType + " like 'CHAR%') and (" + ExcelCol.DataLength + " is null)"))
            {
                sb.AppendLine(dr[ExcelTable.Code].ToString() + "的" + dr[ExcelCol.Code].ToString() + "，其" + dr[ExcelCol.DataType].ToString() + "类型的长度不能为空！");
            }

            DataRow[] drNewArray = dtTable.Select(ColCommon.ExcelCol.ChangeType + "='新增'");

            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew[EntTable.ExcelTable.Code].ToString();
                string strTableName = drNew[EntTable.ExcelTable.Name].ToString();
                string strChangeType = drNew[ColCommon.ExcelCol.ChangeType].ToString();
                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "'").Length == 0)
                {
                    sb.AppendLine("表" + strTableCode + "中没有本次更变的列，请删除该表或新增列！");
                }
                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and (" + ColCommon.ExcelCol.ChangeType + " is not null and " + ColCommon.ExcelCol.ChangeType + "<>'新增')").Length > 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "中，只能全部为新增列！");
                }

                if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='PK'").Length == 0)
                {
                    sb.AppendLine("新增的表" + strTableCode + "没有主键！");
                }
            }
            //返回结果
            return string.IsNullOrEmpty(sb.ToString());
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
            //这个不在Excel模板内，会自动转换
            public static string DataTypeFull = "类型长度";
            public static string DataTypeNew = "新类型";
            public static string DataTypeFullNew = "新类型长度";
        }
    }
}
