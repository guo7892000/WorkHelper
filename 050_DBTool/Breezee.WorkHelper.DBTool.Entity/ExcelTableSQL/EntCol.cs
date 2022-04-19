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

    }

    
}
