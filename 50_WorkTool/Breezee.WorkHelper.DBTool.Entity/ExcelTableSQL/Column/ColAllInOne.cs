using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public class ColAllInOne
    {
        //SqlServer
        public string SqlServer_FullDataType;
        public string SqlServer_AutoNum;
        public YesNoType SqlServer_Unique;
        public string SqlServer_FK;
        //Oracle
        public string Oracle_FullDataType;
        public string Oracle_PKName;
        public string Oracle_Sequence;
        public string Oracle_UniqueName;
        public string Oracle_FK;
        public string Oracle_FKName;
        //MySql
        public string MySql_FullDataType;
        public YesNoType MySql_Nonnegative;
        public YesNoType MySql_AutoNum;
        public string MySql_FK;
        //SQLite
        public string SQLite_FullDataType;
        public YesNoType SQLite_AutoNum;
        public string SQLite_PKName;
        public string SQLite_UniqueName;
        public string SQLite_FK;
        public string SQLite_FKName;
        //PostgreSq
        public string PostgreSql_FullDataType;
        public string PostgreSql_PKName;
        public string PostgreSql_UniqueName;
        public string PostgreSql_FK;
        public string PostgreSql_FKName;

        public static ColAllInOne GetEntity(DataRow dr)
        {
            ColAllInOne ent = new ColAllInOne();
            //SqlServer
            ent.SqlServer_FullDataType = dr[ExcelCol.SqlServer.FullDataType].ToString().Trim();
            ent.SqlServer_AutoNum = dr[ExcelCol.SqlServer.AutoNum].ToString().Trim();
            ent.SqlServer_Unique = TableSqlCommon.GetYesNoType(dr[ExcelCol.SqlServer.Unique].ToString().Trim());
            ent.SqlServer_FK = dr[ExcelCol.SqlServer.FK].ToString().Trim();
            //Oracle
            ent.Oracle_FullDataType = dr[ExcelCol.Oracle.FullDataType].ToString().Trim();
            ent.Oracle_PKName = dr[ExcelCol.Oracle.PKName].ToString().Trim();
            ent.Oracle_Sequence = dr[ExcelCol.Oracle.Sequence].ToString().Trim();
            ent.Oracle_UniqueName = dr[ExcelCol.Oracle.UniqueName].ToString().Trim();
            ent.Oracle_FK = dr[ExcelCol.Oracle.FK].ToString().Trim();
            ent.Oracle_FKName = dr[ExcelCol.Oracle.FKName].ToString().Trim();
            //
            ent.MySql_FullDataType = dr[ExcelCol.MySql.FullDataType].ToString().Trim();
            ent.MySql_Nonnegative = TableSqlCommon.GetYesNoType(dr[ExcelCol.MySql.Nonnegative].ToString().Trim());
            ent.MySql_AutoNum = TableSqlCommon.GetYesNoType(dr[ExcelCol.MySql.AutoNum].ToString().Trim());
            ent.MySql_FK = dr[ExcelCol.MySql.FK].ToString().Trim();
            //SQLite
            ent.SQLite_FullDataType = dr[ExcelCol.SQLite.FullDataType].ToString().Trim();
            ent.SQLite_AutoNum = TableSqlCommon.GetYesNoType(dr[ExcelCol.SQLite.AutoNum].ToString().Trim());
            ent.SQLite_PKName = dr[ExcelCol.SQLite.PKName].ToString().Trim();
            ent.SQLite_UniqueName = dr[ExcelCol.SQLite.UniqueName].ToString().Trim();
            ent.SQLite_FK = dr[ExcelCol.SQLite.FK].ToString().Trim();
            ent.SQLite_FKName = dr[ExcelCol.SQLite.FKName].ToString().Trim();
            //PostgreSql
            ent.PostgreSql_FullDataType = dr[ExcelCol.PostgreSql.FullDataType].ToString().Trim();
            ent.PostgreSql_PKName = dr[ExcelCol.PostgreSql.PKName].ToString().Trim();
            ent.PostgreSql_UniqueName = dr[ExcelCol.PostgreSql.UniqueName].ToString().Trim();
            ent.PostgreSql_FK = dr[ExcelCol.PostgreSql.FK].ToString().Trim();
            ent.PostgreSql_FKName = dr[ExcelCol.PostgreSql.FKName].ToString().Trim();

            return ent;
        }

        public static bool ValidateData(DataTable dtTable, DataTable dtAllCol, DataBaseType targetDBType, GenerateParamEntity paramEntity, out StringBuilder sb)
        {
            sb = new StringBuilder();
            DataRow[] drNewArray = dtTable.Select(ColCommon.ExcelCol.ChangeType + "='新增'");

            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew[EntTable.ExcelTable.Code].ToString();
                string strTableName = drNew[EntTable.ExcelTable.Name].ToString();
                string strChangeType = drNew[ColCommon.ExcelCol.ChangeType].ToString();

                #region 综合转换
                if (targetDBType == DataBaseType.Oracle)
                {
                    if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='FK' and (" + ExcelCol.Oracle.FKName
                        + " is not null and " + ExcelCol.Oracle.FKName + " not like 'FK_%')").Length > 0)
                    {
                        sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“" + ExcelCol.Oracle.FKName + "”列内容格式必须以“FK_”开头！");
                        return false;
                    }

                    if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and (" + ExcelCol.Oracle.Sequence + " is not null and " + ExcelCol.Oracle.Sequence + " not like 'SQ_%')").Length > 0)
                    {
                        sb.AppendLine("新增的表" + strTableCode + "中，“" + ExcelCol.Oracle.Sequence + "”列内容格式必须以“SQ_”开头！");
                        return false;
                    }
                    if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and (" + ExcelCol.Oracle.UniqueName + " is not null and " + ExcelCol.Oracle.UniqueName + " not like 'UQ_%')").Length > 0)
                    {
                        sb.AppendLine("新增的表" + strTableCode + "中，“" + ExcelCol.Oracle.UniqueName + "”列内容格式必须以“UQ_”开头！");
                        return false;
                    }
                    if (dtAllCol.Select(EntTable.ExcelTable.Code + "='" + strTableCode + "' and " + ColCommon.ExcelCol.KeyType + "='FK' and (" + ExcelCol.Oracle.FK + " is null or " + ExcelCol.Oracle.FKName + " is null)").Length > 0)
                    {
                        sb.AppendLine("新增的表" + strTableCode + "中键为“FK”时，“" + ExcelCol.Oracle.FK + "、" + ExcelCol.Oracle.FK + "”列为必填！");
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.SqlServer)
                {

                }
                else if (targetDBType == DataBaseType.MySql)
                {

                }
                else if (targetDBType == DataBaseType.SQLite)
                {

                }
                else if (targetDBType == DataBaseType.PostgreSql)
                {

                }
                else
                {
                    throw new Exception("暂不支持该数据库类型！");
                }
                #endregion
            }
            return string.IsNullOrEmpty(sb.ToString());
        }
        public static class ExcelCol
        {
            public static class SqlServer
            {
                public static string FullDataType = "SqlServer全类型";
                public static string AutoNum = "SqlServer自增长设置";
                public static string Unique = "SqlServer唯一性";
                public static string FK = "SqlServer外键";
            }

            public static class Oracle
            {
                public static string FullDataType = "Oracle全类型";
                public static string PKName = "Oracle主键名";
                public static string Sequence = "Oracle序列名";
                public static string UniqueName = "Oracle唯一约束名";
                public static string FK = "Oracle外键";
                public static string FKName = "Oracle外键名";
            }            

            public static class MySql
            {
                public static string FullDataType = "MySql全类型";
                public static string Nonnegative = "MySql非负数";
                public static string AutoNum = "MySql自增长";
                public static string FK = "MySql外键";
            }            

            public static class SQLite
            {
                public static string FullDataType = "SQLite全类型";
                public static string AutoNum = "SQLite自增长";
                public static string PKName = "SQLite主键名";
                public static string UniqueName = "SQLite唯一约束名";
                public static string FK = "SQLite外键";
                public static string FKName = "SQLite外键名";
            }

            public static class PostgreSql
            {
                public static string FullDataType = "PostgreSql全类型";
                public static string PKName = "PostgreSql主键名";
                public static string UniqueName = "PostgreSql唯一约束名";
                public static string FK = "PostgreSql外键";
                public static string FKName = "PostgreSql外键名";
            }
        }
    }
}
