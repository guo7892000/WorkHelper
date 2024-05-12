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
    /// PostgreSQL的SQL构造器
    /// 包括两部分功能：
    /// 1、表结构生成SQL
    /// 2、将SQL转换为其他DB的SQL
    /// </summary>
    public class PostgreSQLBuilder : SQLBuilder
    {
        public override void GenerateTableSQL(EntTable entTable, GenerateParamEntity paramEntity)
        {
            string strTableCode = entTable.Code;
            string strTableName = entTable.Name;
            string strTableRemark = entTable.Remark;
            TableChangeType tableDealType = entTable.ChangeTypeEnum;

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
            string strUqueList = "";

            if ((createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create) && tableDealType == TableChangeType.Create)
            {
                sbDelete = sbDelete.Insert(0, "DROP TABLE IF EXISTS " + strTableCode + ";\n");//倒着删除掉
            }
            if (tableDealType == TableChangeType.Create)
            {
                #region 新增处理
                sbSql.Append("/*" + iCalNum.ToString() + "、新增表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");
                sbSql.Append(AddRightBand("CREATE TABLE IF NOT EXISTS") + AddRightBand(strTableCode) + "(\n");

                //表说明SQL
                if (string.IsNullOrEmpty(strTableRemark))
                {
                    sbRemark.Append("COMMENT ON TABLE " + strTableCode + " IS '" + strTableName + "';\n");
                }
                else
                {
                    sbRemark.Append("COMMENT ON TABLE " + strTableCode + " IS '" + strTableName + "：" + strTableRemark + "';\n");
                }
                    
                int j = tableCols.Count();
                //string strDefaultList = "";//默认值
                //string strUqueList = "";
                //string strSequence = "";//序列：一张表就一个，名字为:Se_表名
                foreach (EntCol drCol in tableCols)
                {
                    //增加MySql列
                    GeneratePostgreSqlColumn(TableChangeType.Create, strTableCode, drCol, ref strPK, ref j, ref strUqueList);
                }
                if (strPK != "")
                {
                    sbSql.Append(strPK);//主键的处理,MySql是一句独立的
                }
                //唯一和外键
                if (!string.IsNullOrEmpty(strUqueList))
                {
                    //去掉最后一个逗号和换行
                    sbSql.Append(strUqueList.Substring(0, strUqueList.Length - 2) + "\n");
                }

                //表创建完毕
                sbSql.Append(");\n");

                sbSql.Append(sbRemark.ToString());//添加列说明
                sbRemark = new StringBuilder();
                #endregion
            }
            else
            {
                #region 修改表处理
                //alter table TEST1 drop column planmonth;
                sbSql.Append("/*" + iCalNum.ToString() + "、修改表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");
                int j = 1;
                foreach (EntCol drCol in tableCols)
                {
                    //增加MySql列
                    GeneratePostgreSqlColumn(TableChangeType.Alter, strTableCode, drCol, ref strPK, ref j, ref strUqueList);
                }
                #endregion
            }
            iCalNum++;
        }

        private void GeneratePostgreSqlColumn(TableChangeType tableDealType, string strTableCode, EntCol drCol, ref string strPK, ref int j, ref string strUqueList)
        {
            //表编码,列名称,列编码,类型,长度,键,必填,约束,备注,自增长设置
            ColumnChangeType strColumnDealType = drCol.commonCol.ChangeTypeEnum;
            ColKeyType strKey = drCol.commonCol.KeyTypeEnum;//键
            string strColCode = drCol.commonCol.Code;//列编码
            string strColName = drCol.commonCol.Name;//列名称
            string strColDataType = drCol.commonCol.DataType;//类型
            string strColLen = drCol.commonCol.DataLength;//长度
            string strColDecimalDigits = drCol.commonCol.DataDotLength;//小数位
            string strColDefault = drCol.commonCol.Default; //默认值
            YesNoType strColNoNull = drCol.commonCol.NotNull;
            string strColRemark = drCol.commonCol.Remark;

            //独有字段
            string strColPKName = string.Empty;//主键名
            string strColUnique = string.Empty;//唯一约束名
            string strColForgKey = string.Empty;//外键
            string strColForgKeyCode = string.Empty;//外键名
            if (_isAllConvert)
            {
                //综合转换时，使用综合转换模板的独有值
                strColPKName = drCol.allInOne.PostgreSql_PKName;//主键名
                strColUnique = drCol.allInOne.PostgreSql_UniqueName;//唯一约束名
                strColForgKey = drCol.allInOne.PostgreSql_FK;//外键
                strColForgKeyCode = drCol.allInOne.PostgreSql_FKName;//外键名
            }
            else if (importDBType == targetDBType)
            {
                //只有导入类型与目标类型一致才使用独有值
                strColPKName = drCol.postgreSqlCol.PKName;//主键名
                strColUnique = drCol.postgreSqlCol.UniqueName;//唯一约束名
                strColForgKey = drCol.postgreSqlCol.FK;//外键
                strColForgKeyCode = drCol.postgreSqlCol.FKName;//外键名
            }
            //其他变量
            string strTable_Col = strTableCode + "_" + strColCode;//表编码+"_"+列编码
            string sOneSql = "";

            #region 转换字段类型与默认值
            if (importDBType != targetDBType)
            {
                ConvertDBTypeDefaultValueString(ref strColDataType, ref strColDefault, importDBType);
            }
            #endregion

            //数据类型(类型+长度+小数点)
            string sDataType_Full = (_isAllConvert && !String.IsNullOrEmpty(drCol.allInOne.PostgreSql_FullDataType)) ? drCol.allInOne.PostgreSql_FullDataType : GetFullTypeString(drCol, strColDataType, strColLen, strColDecimalDigits);

            if (tableDealType == TableChangeType.Create)
            {
                #region 新增表处理

                #region 列类型和长度处理
                sbSql.Append(AddRightBand(strColCode) + sDataType_Full);
                #endregion

                #region 非空的处理
                if (strColNoNull == YesNoType.Yes)
                {
                    sbSql.Append(sNotNull);
                }
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    //string strColDefaultName = "DF_" + strTableCode + "_" + strColCode;
                    sbSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                #region 主键
                if (strKey == ColKeyType.PK)
                {
                    if (string.IsNullOrEmpty(strColPKName))
                    {
                        strUqueList += " PRIMARY KEY (" + strColCode + "),\n";
                    }
                    else
                    {
                        strUqueList += " CONSTRAINT " + strColPKName + " PRIMARY KEY (" + strColCode + "),\n";
                    }
                }
                #endregion

                #region 唯一性约束
                if (!string.IsNullOrEmpty(strColUnique))
                {
                    strUqueList += " CONSTRAINT \"" + strColUnique + "\" UNIQUE (" + strColCode + "),\n";
                }
                #endregion

                #region 外键的处理
                if (!string.IsNullOrEmpty(strColForgKeyCode))
                {
                    //这里表名
                    strUqueList += string.Format(" CONSTRAINT \"{0}\" FOREIGN KEY ({1}) REFERENCES {2},\n", strColForgKeyCode, strColCode, strColForgKey);
                }
                #endregion

                #region 列备注的处理
                //备注
                if (string.IsNullOrEmpty(strColRemark))
                {
                    sbRemark.Append(" COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "';\n");
                }
                else
                {
                    sbRemark.Append(" COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "：" + strColRemark + "';\n");
                }
                #endregion

                #region 增加逗号
                if (j == 1 && string.IsNullOrEmpty(strUqueList)) //最后一个字段，且没有唯一性和外键约束
                {
                    sbSql.Append("" + ChangeIntoInfo(strColName) + "\n");
                }
                else
                {
                    sbSql.Append("," + "" + ChangeIntoInfo(strColName) + "\n");
                }
                #endregion
                //计数器
                j--;
                #endregion
            }
            else
            {
                #region 修改表处理
                StringBuilder sbColSql = new StringBuilder();//列的SQL

                if (strColDataType.Contains("("))
                {
                    if (string.IsNullOrEmpty(strColLen))
                    {
                        strColLen = strColDataType.Substring(strColDataType.IndexOf("(") + 1, strColDataType.IndexOf(")") - strColDataType.IndexOf("(") - 1);
                        drCol.commonCol.DataLength = strColLen;
                        drCol.commonCol.DataType = strColDataType.Substring(0, strColDataType.IndexOf("("));
                    }
                    else
                    {
                        strColDataType = strColDataType.Substring(0, strColDataType.IndexOf("("));
                        drCol.commonCol.DataType = strColDataType;
                    }
                }

                //生成删除列SQL脚本
                if (createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create)
                {
                    if (strColumnDealType ==  ColumnChangeType.Create || strColumnDealType == ColumnChangeType.Drop_Create)
                    {
                        sbDelete.Append("ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + ";\n");
                    }
                    //对于删除，直接下一个字段
                    if (createType == SQLCreateType.Drop)
                    {
                        return;//continue;
                    }
                }

                //类型处理
                sbColSql.Append(AddRightBand(strColCode) + sDataType_Full);

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    sbColSql.Append(sNotNull);
                }
                //else
                //{
                //    sbSql.Append(_strNull);
                //}
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    //string strColDefaultName = "DF_" + strTableCode + "_" + strColCode;
                    sbColSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                #region 列备注的处理
                //备注
                sbRemark.Append(" COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + ":" + strColRemark + "'");
                #endregion

                if (strColumnDealType == ColumnChangeType.Create)
                {
                    //得到修改表增加列语句
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType == ColumnChangeType.Alter)
                {
                    sbSql.AppendLine("/*注：对修改字段，如要变更是否可空类型，则自己在最后加上NULL 或NOT NULL。对字段类型的变更，需要先清空该字段值或删除该列再新增*/");
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " ALTER " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType == ColumnChangeType.Drop)
                {
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " DROP COLUMN " + AddRightBand(strColCode) + ";");
                }
                else if (strColumnDealType == ColumnChangeType.Drop_Create)
                {
                    //得到修改表增加列语句
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sbColSql.ToString());
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
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar", "character varying").Replace("datetime", "date");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("getdate()", "now()");
                    break;
                case DataBaseType.Oracle:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar2", "character varying");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("sysdate", "now()");
                    break;
                case DataBaseType.MySql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar", "character varying").Replace("datetime", "date");
                    break;
                case DataBaseType.SQLite:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar", "character varying").Replace("datetime", "date");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("(datetime('now','localtime'))", "now()");
                    break;
                case DataBaseType.PostgreSql:
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
        }

        /// <summary>
        /// PostgreSql的SQL转换其他DB类型
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
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLServerBuilder.SqlFuncString.NowDate);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLServerBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLServerBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.Oracle:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, OracleBuilder.SqlFuncString.NowDate, BracketDealType.Remove);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, OracleBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, OracleBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.MySql:
                    //MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, MySQLBuilder.SqlFuncString.NowDate); //相同
                    //MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, MySQLBuilder.SqlFuncString.IfNull); //相同
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, MySQLBuilder.SqlFuncString.Guid); //相同
                    break;
                case DataBaseType.SQLite:
                    //MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLiteBuilder.SqlFuncString.NowDate); //相同
                    //MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLiteBuilder.SqlFuncString.IfNull); //相同
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLiteBuilder.SqlFuncString.Guid); //不支持
                    break;
                case DataBaseType.PostgreSql:
                    break;
            }
        }

        public class SqlFuncString
        {
            public static string NowDate = "NOW";
            public static string IfNull = "IFNULL";
            public static string Guid = "UUID";
        }
    }
}
