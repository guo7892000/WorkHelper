using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// SQLServer的SQL构造器
    /// 包括两部分功能：
    /// 1、表结构生成SQL
    /// 2、将SQL转换为其他DB的SQL
    /// </summary>
    public class SQLServerBuilder : SQLBuilder
    {
        //private string _tableColumnAroundChar_Left = "[";//围绕表或列名的左字符，例如SqlServer的[
        //private string _tableColumnAroundChar_Right = "]";//围绕表或列名的左字符，例如SqlServer的]
        private bool _isSqlServerDefaultValueNameAuto = false;
        public override void GenerateTableSQL(EntTable entTable, GenerateParamEntity paramEntity)
        {
            string strTableCode = entTable.Code;
            string strTableName = entTable.Name;

            IEnumerable<EntCol> tableCols;

            if (string.IsNullOrEmpty(entTable.CommonColumnTableCode))
            {
                tableCols = entCols.Where(t => t.commonCol.TableCode == entTable.Code);
            }
            else
            {
                //把通用列也加进去
                tableCols = entCols.Where(t => t.commonCol.TableCode == entTable.Code).Union(entCols.Where(t => t.commonCol.TableCode == entTable.CommonColumnTableCode));
            }
            
            string strPK = "";
            if ((entTable.ChangeTypeEnum == TableChangeType.Create))
            {
                #region 新增处理
                sbSql.Append("/*" + iCalNum.ToString() + "、新增表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");

                if (createType == SQLCreateType.Create)
                {
                    sbSql.Append("IF OBJECT_ID('" + strTableCode + "', 'U') IS  NULL \n BEGIN \n");
                }
                else if (createType == SQLCreateType.Drop_Create || createType == SQLCreateType.Drop)
                {
                    sbDelete.Insert(0, "IF OBJECT_ID('" + strTableCode + "', 'U') IS NOT NULL \n BEGIN \n"
                        + "\tDROP TABLE " + strTableCode + "\n END\nGO\n");
                    if (createType ==  SQLCreateType.Drop)
                    {
                        return;
                    }
                }

                sbSql.Append(AddRightBand("CREATE TABLE") + AddRightBand(strTableCode) + AddRightBand("\n(\n"));
                //表说明SQL
                string sTableRemark = string.IsNullOrEmpty(entTable.Remark) ? strTableName : strTableName + "：" + entTable.Remark;
                sbRemark.Append("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + sTableRemark + "',\n" +
                        "   @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + strTableCode + "'\n");
                int j = tableCols.Count();
                string strDefaultList = "";//默认值
                foreach (EntCol drCol in tableCols)
                {
                    //生成SqlServer的列
                    GenerateSqlServerColumn(paramEntity, TableChangeType.Create, strTableCode, drCol, ref strPK, ref j, strTableName);
                }
                //最后加括号并换行
                sbSql.Append(")\n");
                if (createType == SQLCreateType.Create)
                {
                    sbSql.Append(sbRemark.ToString() + " END\nGO\n");//添加列说明
                }
                else if (createType == SQLCreateType.Drop_Create)
                {
                    sbSql.Append(sbRemark.ToString() + " \nGO\n");//添加列说明
                }
                if (!_isSqlServerDefaultValueNameAuto)
                {
                    sbSql.Append(strDefaultList);//添加默认值
                }
                //重新创建备注字符对象
                sbRemark = new StringBuilder();
                #endregion
            }
            else
            {
                #region 修改列处理
                sbSql.Append("/*" + iCalNum.ToString() + "、修改表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");
                int j = 1;
                foreach (EntCol drCol in tableCols)
                {
                    //生成SqlServer的列
                    GenerateSqlServerColumn(paramEntity, TableChangeType.Alter, strTableCode, drCol, ref strPK, ref j, strTableName);
                }
                #endregion
            }
            iCalNum++;
        }

        private void GenerateSqlServerColumn(GenerateParamEntity paramEntity, TableChangeType tableDealType, string strTableCode, EntCol drCol, ref string strPK, ref int j, string strTableName)
        {
            //公共字段
            ColumnChangeType strColumnDealType = drCol.commonCol.ChangeTypeEnum;//列处理类型，当为空时表示新增
            ColKeyType strKey = drCol.commonCol.KeyTypeEnum;//["键"].ToString().ToUpper().Trim();
            string strColCode = drCol.commonCol.Code;//["列编码"].ToString();
            string strColName = drCol.commonCol.Name;//["列名称"].ToString();
            string strColDataType = drCol.commonCol.DataType;//["类型"].ToString().ToLower();
            string strColLen = drCol.commonCol.DataLength;//["长度"].ToString().Trim();
            string strColDecimalDigits = drCol.commonCol.DataDotLength;//["小数位"].ToString().Trim();
            string strColDefault = drCol.commonCol.Default; //["默认值"].ToString().Trim().Replace("'", "");
            //独有字段
            YesNoType strColUnique = YesNoType.No;
            string strColAddNum = string.Empty;
            string strColForgKey = string.Empty;
            if (_isAllConvert)
            {
                //综合转换时，使用综合转换模板的独有值
                strColUnique = drCol.allInOne.SqlServer_Unique;
                strColAddNum = drCol.allInOne.SqlServer_AutoNum;
                strColForgKey = drCol.allInOne.SqlServer_FK;
            }
            else if (importDBType == targetDBType)
            {
                //只有导入类型与目标类型一致才使用独有值
                strColUnique = drCol.sqlServerCol.Unique;
                strColAddNum = drCol.sqlServerCol.AutoNum;
                strColForgKey = drCol.sqlServerCol.FK;
            }
            //其他变量
            string strTable_Col = strTableCode + "_" + strColCode;//表编码+"_"+列编码
            string strCanNull = "";//是否可空
            string strDefault_Full = "";//默认值
            string strDefaultList = "";//修改默认值
            string strUqueList = "";//唯一性
                                    //
            #region 转换字段类型与默认值
            if (importDBType != targetDBType && paramEntity.isNeedColumnTypeConvert)
            {
                ConvertDBTypeDefaultValueString(ref strColDataType, ref strColDefault, importDBType);
            }
            #endregion

            //数据类型(类型+长度+小数点)
            string sDataType_Full = (_isAllConvert && !String.IsNullOrEmpty(drCol.allInOne.SqlServer_FullDataType))? drCol.allInOne.SqlServer_FullDataType : GetFullTypeString(drCol, strColDataType, strColLen, strColDecimalDigits);


            if (tableDealType == TableChangeType.Create)
            {
                #region 新增

                sbSql.Append("\t");
                if (strKey == ColKeyType.PK)
                {
                    //主键处理
                    if (!string.IsNullOrEmpty(strColAddNum) && (strColDataType == "int" || strColDataType == "bigint"))
                    {
                        sbSql.Append(AddRightBand(strColCode) + AddRightBand(strColDataType) + AddRightBand("IDENTITY" + AddLeftRightKuoHao(strColAddNum)
                            + " CONSTRAINT [PK_" + strTableCode + "] PRIMARY KEY(" + strColCode + ") "));
                    }
                    else
                    {
                        sbSql.Append(AddRightBand(strColCode) + AddRightBand(sDataType_Full)
                            + " CONSTRAINT [PK_" + strTableCode + "] PRIMARY KEY(" + strColCode + ") ");
                    }
                    strPK = strColCode;
                }
                else
                {
                    //没有长度的字段处理
                    sbSql.Append(AddRightBand(strColCode) + AddRightBand(sDataType_Full));
                }

                #region 非空的处理
                if (drCol.commonCol.NotNull == YesNoType.Yes)
                {
                    sbSql.Append(AddRightBand(sNotNull));
                }
                else
                {
                    sbSql.Append(AddRightBand(sNull));
                }
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    string strColDefaultName = "DF_" + strTable_Col;
                    string sRealDefaultValue = strColDefault.Replace("'", "");
                    if (strColDataType == "varchar" || strColDataType == "nvarchar" || strColDataType == "char" || strColDataType == "nchar")
                    {
                        sRealDefaultValue = "'" + sRealDefaultValue + "'";
                    }

                    if (_isSqlServerDefaultValueNameAuto)
                    {
                        //方式一：采用默认名
                        sbSql.Append(AddRightBand(" DEFAULT(" + sRealDefaultValue + ")"));
                    }
                    else
                    {
                        //方式二：采用指定名(单独SQL)
                        //strDefaultList = strDefaultList + "\n" +
                        // "IF EXISTS(SELECT * FROM sys.objects WHERE type='D' AND name='" + strColDefaultName + "') \n" +
                        //"ALTER TABLE " + strTableCode + " DROP CONSTRAINT " + strColDefaultName + " \n" +
                        //"GO \n" +
                        //"alter table " + strTableCode + " add constraint " + strColDefaultName + "\n " +
                        //"	DEFAULT (" + sRealDefaultValue + ") for " + strColCode + " \n" +
                        //"GO	\n";

                        //方式三：采用指定名(包括在创建表列中)
                        sbSql.Append(AddRightBand(" CONSTRAINT " + strColDefaultName + " DEFAULT(" + sRealDefaultValue + ")"));
                    }
                }
                #endregion
                //唯一性处理
                if (strColUnique == YesNoType.Yes && strKey == ColKeyType.Empty)
                {
                    sbSql.Append(AddRightBand("CONSTRAINT " + " UQ_" + strTable_Col + " UNIQUE"));
                }
                //外键处理
                if (strKey == ColKeyType.FK)
                {
                    strColForgKey = " CONSTRAINT FK_" + strTable_Col + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey;
                    sbSql.Append(strColForgKey);
                }
                //最后增加逗号
                if (j > 1)
                {
                    sbSql.Append("," + ChangeIntoInfo(strColName) + "\n");
                }
                else
                {
                    //最后一行不加逗号
                    sbSql.Append("" + ChangeIntoInfo(strColName) + "\n");
                }
                //列备注SQL
                if (string.IsNullOrEmpty(drCol.commonCol.Remark))
                {
                    sbRemark.Append("EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'" + strColName + "',\n" +
                        "    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'" + strTableCode + "',  \n" +
                        "    @level2type = N'COLUMN', @level2name = N'" + strColCode + "'  \n");
                }
                else
                {
                    sbRemark.Append("EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'"
                        + getFitRemark(strColName, drCol.commonCol.Remark) + "',\n" +
                        "    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'" + strTableCode + "',  \n" +
                        "    @level2type = N'COLUMN', @level2name = N'" + strColCode + "'  \n");
                }
                j--;
                
                #endregion
            }
            else
            {
                #region 修改表处理

                #region 删除列的处理
                if (createType == SQLCreateType.Drop || createType == SQLCreateType.Drop_Create)
                {
                    if (strColumnDealType ==  ColumnChangeType.Create || strColumnDealType == ColumnChangeType.Drop_Create)
                    {
                        sbDelete.Insert(0, "IF EXISTS(SELECT 1 FROM SYSOBJECTS A,SYSCOLUMNS B \n" +
                            " WHERE A.ID = B.ID AND A.NAME = '" + strTableCode + "' AND B.NAME = '" + strColCode + "' AND A.TYPE = 'U') \n" +
                            " BEGIN \n" +
                            "    ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + "\n" +
                            " END\nGO\n");
                    }
                    //对于删除，直接下一个字段
                    if (createType == SQLCreateType.Drop)
                    {
                        return;//continue;
                    }
                }
                #endregion

                #region 非空的处理
                if (drCol.commonCol.NotNull== YesNoType.Yes)
                {
                    strCanNull = AddRightBand(sNotNull);
                }
                else
                {
                    strCanNull = AddRightBand(sNull);
                }
                #endregion
                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    string strColDefaultName = "DF_" + strTable_Col;
                    if (strColDataType == "varchar" || strColDataType == "nvarchar" || strColDataType == "char" || strColDataType == "nchar")
                    {
                        #region 字符型处理
                        if (_isSqlServerDefaultValueNameAuto)
                        {
                            //方式一：采用默认名
                            strDefault_Full = AddRightBand("default('" + strColDefault + "')");
                        }
                        else
                        {
                            //方式二：采用指定名
                            strDefaultList = "\n" +
                             "IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE='D' AND NAME='" + strColDefaultName + "') \n" +
                            "ALTER TABLE " + strTableCode + " DROP CONSTRAINT " + strColDefaultName + " \n" +
                            "GO \n" +
                            "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColDefaultName + "\n " +
                            "	DEFAULT ('" + strColDefault + "') for " + strColCode + " \n" +
                            "GO	\n";
                        }
                        #endregion
                    }
                    else
                    {
                        #region 数值型处理
                        if (_isSqlServerDefaultValueNameAuto)
                        {
                            //方式一：采用默认名
                            strDefault_Full = AddRightBand("default(" + strColDefault.Replace("'", "") + ")");
                        }
                        else
                        {
                            //方式二：采用指定名
                            strDefaultList = "\n" +
                             "IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE='D' AND NAME='" + strColDefaultName + "') \n" +
                            "  ALTER TABLE " + strTableCode + " DROP CONSTRAINT " + strColDefaultName + " \n" +
                            "GO \n" +
                            "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColDefaultName + "\n " +
                            "	DEFAULT (" + strColDefault + ") FOR " + strColCode + " \n" +
                            "GO	\n";
                        }
                        #endregion
                    }
                }
                #endregion
                #region 唯一性的处理
                if (strColUnique ==  YesNoType.Yes)
                {
                    string strColDefaultName = "UQ_" + strTable_Col;
                    strUqueList = "\n" +
                     "IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE='UQ' AND NAME='" + strColDefaultName + "') \n" +
                    "ALTER TABLE " + strTableCode + " DROP CONSTRAINT " + strColDefaultName + " \n" +
                    "GO \n" +
                    "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColDefaultName + " UNIQUE \n " +
                    "GO	\n";
                }
                #endregion
                #region 外键的处理
                if (strKey ==  ColKeyType.FK && !string.IsNullOrEmpty(strColForgKey) && strColForgKey.Contains("(") && strColForgKey.Contains(")"))
                {
                    string strColDefaultName = "FK_" + strTable_Col;
                    strUqueList = "\n" +
                     "IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE='F' AND NAME='" + strColDefaultName + "') \n" +
                    "ALTER TABLE " + strTableCode + " DROP CONSTRAINT " + strColDefaultName + " \n" +
                    "GO \n" +
                    "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColDefaultName + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey + "\n" +
                    "GO	\n";
                }
                #endregion
                //增加注解
                string strColRemarkInfo = drCol.commonCol.Remark;

                if (strColumnDealType ==  ColumnChangeType.Create)
                {
                    if (!string.IsNullOrEmpty(strColRemarkInfo))
                    {
                        strColRemarkInfo = getFitRemark(strColName, strColRemarkInfo);
                    }
                    sbSql.Append("/*" + iCalNum.ToString() + "." + j.ToString() + strTableName + AddLeftRightKuoHao(strTableCode) + "增加" + strColName + AddRightBand(AddLeftRightKuoHao(strColCode)) + strColDataType + "字段*/ \n" +
                    "IF NOT EXISTS(SELECT 1 FROM SYSOBJECTS A,SYSCOLUMNS B \n" +
                        " WHERE A.ID = B.ID AND A.NAME = '" + strTableCode + "' AND B.NAME = '" + strColCode + "' AND A.TYPE = 'U') \n" +
                        " BEGIN \n" +
                        "    ALTER TABLE " + strTableCode + " ADD " + strColCode + " " + sDataType_Full + " " + strDefault_Full + " " + strCanNull + "\n" +
                        "    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'" + strColRemarkInfo + "',  \n" +
                        "    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'" + strTableCode + "',  \n" +
                        "    @level2type = N'COLUMN', @level2name = N'" + strColCode + "'  \n" +
                        " END\nGO\n");
                    if (!_isSqlServerDefaultValueNameAuto)
                    {
                        sbSql.Append(strDefaultList);
                    }
                }
                else if (strColumnDealType ==  ColumnChangeType.Alter)
                {
                    sbSql.Append("/*" + iCalNum.ToString() + "." + j.ToString() + strTableName + AddLeftRightKuoHao(strTableCode) + "修改" + strColName + AddRightBand(AddLeftRightKuoHao(strColCode)) + strColDataType + "字段*/ \n" +
                        "IF EXISTS(SELECT 1 FROM SYSOBJECTS A,SYSCOLUMNS B \n" +
                            " WHERE A.ID = B.ID AND A.NAME = '" + strTableCode + "' AND B.NAME = '" + strColCode + "' AND A.TYPE = 'U') \n" +
                            " BEGIN \n" +
                            "    ALTER TABLE " + strTableCode + " ALTER COLUMN " + strColCode + " " + sDataType_Full + " " + strDefault_Full + " " + strCanNull + "\n" +
                            " END\nGO\n");
                }
                else if (strColumnDealType ==  ColumnChangeType.Drop)
                {
                    sbSql.Append("/*" + iCalNum.ToString() + "." + j.ToString() + strTableName + AddLeftRightKuoHao(strTableCode) + "修改" + strColName + AddRightBand(AddLeftRightKuoHao(strColCode)) + strColDataType + "字段*/ \n" +
                        "IF EXISTS(SELECT 1 FROM SYSOBJECTS A,SYSCOLUMNS B \n" +
                            " WHERE A.ID = B.ID AND A.NAME = '" + strTableCode + "' AND B.NAME = '" + strColCode + "' AND A.TYPE = 'U') \n" +
                            " BEGIN \n" +
                            "    ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + "\n" +
                            " END\nGO\n");
                }
                else if (strColumnDealType == ColumnChangeType.Drop_Create)
                {
                    if (!string.IsNullOrEmpty(strColRemarkInfo))
                    {
                        strColRemarkInfo = getFitRemark(strColName, strColRemarkInfo);
                    }
                    sbSql.Append("/*" + iCalNum.ToString() + "." + j.ToString() + strTableName + AddLeftRightKuoHao(strTableCode) + "增加" + strColName + AddRightBand(AddLeftRightKuoHao(strColCode)) + strColDataType + "字段*/ \n" +
                        "IF EXISTS(SELECT 1 FROM SYSOBJECTS A,SYSCOLUMNS B \n" +
                            " WHERE A.ID = B.ID AND A.NAME = '" + strTableCode + "' AND B.NAME = '" + strColCode + "' AND A.TYPE = 'U') \n" +
                            " BEGIN \n" +
                            "    ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + "\n" +
                            " END\n" +
                            "ALTER TABLE " + strTableCode + " ADD " + strColCode + " " + sDataType_Full + " " + strDefault_Full + " " + strCanNull + "\n" +
                            "    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'" + strColRemarkInfo + "',  \n" +
                            "    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'" + strTableCode + "',  \n" +
                            "    @level2type = N'COLUMN', @level2name = N'" + strColCode + "'  \nGO\n");
                    if (!_isSqlServerDefaultValueNameAuto)
                    {
                        sbSql.Append(strDefaultList);
                    }
                }
                j++;
                #endregion
            }
        }

        public override void ConvertDBTypeDefaultValueString(ref string sDbType, ref string sDefaultValue, DataBaseType impDbType)
        {
            switch (impDbType)
            {
                case DataBaseType.SqlServer:
                    break;
                case DataBaseType.Oracle:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar2", "varchar").Replace("nvarchar2", "nvarchar")
                        .Replace("date", "datetime").Replace("number", "decimal");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("sysdate", "getdate()")
                        .Replace("sys_guid()", "newid()");
                    break;
                case DataBaseType.MySql:
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "getdate()");
                    break;
                case DataBaseType.SQLite:
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("(datetime('now','localtime'))", "getdate()");
                    break;
                case DataBaseType.PostgreSql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("character varying", "varchar")
                        .Replace("date", "datetime")
                        .Replace("int", "integer");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "getdate()");
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
        }

        /// <summary>
        /// SqlServer的SQL转换其他DB类型
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="targetDbType"></param>
        public override void ConvertToDbSql(ref string sSql, DataBaseType targetDbType)
        {
            string sMatchSysdate = @"\s*,*\s*NOW\s*\(\s*\)\s*,?";
            string sMatchIfNull = @"\s*,*\s*IFNULL\s*\(\s*";
            string sMatchsSysGuid = @"\s*,*\s*UUID\s*\(\s*\)\s*,?";

            switch (targetDbType)
            {
                case DataBaseType.SqlServer:
                    break;
                case DataBaseType.Oracle:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, OracleBuilder.SqlFuncString.NowDate, BracketDealType.Remove);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, OracleBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, OracleBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.MySql:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, MySQLBuilder.SqlFuncString.NowDate);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, MySQLBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, MySQLBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.SQLite:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLiteBuilder.SqlFuncString.NowDate);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLiteBuilder.SqlFuncString.IfNull);
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLiteBuilder.SqlFuncString.Guid); //不支持
                    break;
                case DataBaseType.PostgreSql:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, PostgreSQLBuilder.SqlFuncString.NowDate);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, PostgreSQLBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, PostgreSQLBuilder.SqlFuncString.Guid);
                    break;
            }
        }

        public class SqlFuncString
        {
            public static string NowDate = "GETDATE";
            public static string IfNull = "ISNULL";
            public static string Guid = "NEWID";
            public static string Decimal = "DECIMAL";
        }
    }
}
