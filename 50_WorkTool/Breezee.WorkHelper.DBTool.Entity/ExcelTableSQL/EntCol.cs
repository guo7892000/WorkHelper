using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL
{
    /// <summary>
    /// Excel导入生成模块-列
    /// </summary>
    public class EntCol
    {
        /*变更类型	表编码	列名称	列编码	类型	长度	小数位	键	必填	默认值	备注	
         * SqlServer全类型	SqlServer自增长设置	SqlServer唯一性	SqlServer外键	
         * Oracle全类型	Oracle主键名	Oracle序列名	Oracle唯一约束名	Oracle外键	Oracle外键名	
         * MySql全类型	MySql标志位	MySql自增长	MySql外键	
         * SQLite全类型	SQLite自增长	SQLite主键名	SQLite唯一约束名	SQLite外键	SQLite外键名	
         * PostgreSql全类型	PostgreSql主键名	PostgreSql唯一约束名	PostgreSql外键	PostgreSql外键名*/

        public ColCommon commonCol;
        public ColAllInOne allInOne;
        public ColSqlServerTemplate sqlServerCol;
        public ColOracleTemplate oracleCol;
        public ColMySqlTemplate mySqlCol;
        public ColSQLiteTemplate sqliteCol;
        public ColPostgreSqlTemplate postgreSqlCol;
        /// <summary>
        /// 模板类型
        /// </summary>
        public ColumnTemplateType templateType;

        public static EntCol GetEntity(DataRow dr,ColumnTemplateType templateType)
        {
            EntCol ent = new EntCol();
            ent.templateType = templateType;
            ent.commonCol = ColCommon.GetEntity(dr);
            switch (templateType)
            {
                case ColumnTemplateType.AllInOne:
                    ent.allInOne = ColAllInOne.GetEntity(dr);
                    break;
                case ColumnTemplateType.SqlServer:
                    ent.sqlServerCol = ColSqlServerTemplate.GetEntity(dr);
                    break;
                case ColumnTemplateType.Oracle:
                    ent.oracleCol = ColOracleTemplate.GetEntity(dr);
                    break;
                case ColumnTemplateType.MySql:
                    ent.mySqlCol = ColMySqlTemplate.GetEntity(dr);
                    break;
                case ColumnTemplateType.SQLite:
                    ent.sqliteCol = ColSQLiteTemplate.GetEntity(dr);
                    break;
                case ColumnTemplateType.PostgreSql:
                    ent.postgreSqlCol = ColPostgreSqlTemplate.GetEntity(dr);
                    break;
                default:
                    break;
            }
                        
            return ent;
        }

        public static DataTable GetTable(ColumnTemplateType templateType)
        {
            DataTable dt = ColCommon.GetTable();
            switch (templateType)
            {
                case ColumnTemplateType.AllInOne:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColAllInOne.ExcelCol.SqlServer.FullDataType),
                        new DataColumn(ColAllInOne.ExcelCol.SqlServer.AutoNum),
                        new DataColumn(ColAllInOne.ExcelCol.SqlServer.Unique),
                        new DataColumn(ColAllInOne.ExcelCol.SqlServer.FK),

                        new DataColumn(ColAllInOne.ExcelCol.Oracle.FullDataType),
                        new DataColumn(ColAllInOne.ExcelCol.Oracle.PKName),
                        new DataColumn(ColAllInOne.ExcelCol.Oracle.Sequence),
                        new DataColumn(ColAllInOne.ExcelCol.Oracle.UniqueName),
                        new DataColumn(ColAllInOne.ExcelCol.Oracle.FK),
                        new DataColumn(ColAllInOne.ExcelCol.Oracle.FKName),

                        new DataColumn(ColAllInOne.ExcelCol.MySql.FullDataType),
                        new DataColumn(ColAllInOne.ExcelCol.MySql.Nonnegative),
                        new DataColumn(ColAllInOne.ExcelCol.MySql.AutoNum),
                        new DataColumn(ColAllInOne.ExcelCol.MySql.FK),

                        new DataColumn(ColAllInOne.ExcelCol.SQLite.FullDataType),
                        new DataColumn(ColAllInOne.ExcelCol.SQLite.PKName),
                        new DataColumn(ColAllInOne.ExcelCol.SQLite.UniqueName),
                        new DataColumn(ColAllInOne.ExcelCol.SQLite.AutoNum),
                        new DataColumn(ColAllInOne.ExcelCol.SQLite.FK),
                        new DataColumn(ColAllInOne.ExcelCol.SQLite.FKName),

                        new DataColumn(ColAllInOne.ExcelCol.PostgreSql.FullDataType),
                        new DataColumn(ColAllInOne.ExcelCol.PostgreSql.PKName),
                        new DataColumn(ColAllInOne.ExcelCol.PostgreSql.UniqueName),
                        new DataColumn(ColAllInOne.ExcelCol.PostgreSql.FK),
                        new DataColumn(ColAllInOne.ExcelCol.PostgreSql.FKName),
                    });
                    break;
                case ColumnTemplateType.SqlServer:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColSqlServerTemplate.ExcelCol.FK),
                        new DataColumn(ColSqlServerTemplate.ExcelCol.AutoNum),
                        new DataColumn(ColSqlServerTemplate.ExcelCol.Unique),
                    });
                    break;
                case ColumnTemplateType.Oracle:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColOracleTemplate.ExcelCol.FK),
                        new DataColumn(ColOracleTemplate.ExcelCol.FKName),
                        new DataColumn(ColOracleTemplate.ExcelCol.PKName),
                        new DataColumn(ColOracleTemplate.ExcelCol.SequenceName),
                        new DataColumn(ColOracleTemplate.ExcelCol.UniqueName),
                    });
                    break;
                case ColumnTemplateType.MySql:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColMySqlTemplate.ExcelCol.FK),
                        new DataColumn(ColMySqlTemplate.ExcelCol.AutoNum),
                        new DataColumn(ColMySqlTemplate.ExcelCol.Nonnegative),
                    });
                    break;
                case ColumnTemplateType.SQLite:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColSQLiteTemplate.ExcelCol.FK),
                        new DataColumn(ColSQLiteTemplate.ExcelCol.AutoNum),
                        new DataColumn(ColSQLiteTemplate.ExcelCol.PKName),
                        new DataColumn(ColSQLiteTemplate.ExcelCol.UniqueName),
                        new DataColumn(ColSQLiteTemplate.ExcelCol.FKName),
                    });
                    break;
                case ColumnTemplateType.PostgreSql:
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn(ColPostgreSqlTemplate.ExcelCol.FK),
                        new DataColumn(ColPostgreSqlTemplate.ExcelCol.FKName),
                        new DataColumn(ColPostgreSqlTemplate.ExcelCol.PKName),
                        new DataColumn(ColPostgreSqlTemplate.ExcelCol.UniqueName),
                    });
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }

            return dt;
        }

    }

    
}
