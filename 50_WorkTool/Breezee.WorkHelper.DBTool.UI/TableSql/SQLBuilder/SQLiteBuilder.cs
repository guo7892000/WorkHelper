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
    /// SQLite的SQL构造器
    /// 包括两部分功能：
    /// 1、表结构生成SQL
    /// 2、将SQL转换为其他DB的SQL
    /// </summary>
    public class SQLiteBuilder : SQLBuilder
    {
        public override void GenerateTableSQL(EntTable entTable, GenerateParamEntity paramEntity)
        {
            string _tableColumnAroundChar_Left = "\"";
            string _tableColumnAroundChar_Right = "\"";
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
                sbSql.Append(AddRightBand("CREATE TABLE IF NOT EXISTS") + AddRightBand(_tableColumnAroundChar_Left + strTableCode + _tableColumnAroundChar_Right) + "(\n");
                //表说明SQL：注SQLite不支持表说明
                int j = tableCols.Count();
                //string strDefaultList = "";//默认值
                //string strUqueList = "";
                //string strSequence = "";//序列：一张表就一个，名字为:Se_表名
                foreach (EntCol drCol in tableCols)
                {
                    //增加MySql列
                    GenerateSQLiteColumn(paramEntity, TableChangeType.Create, strTableCode, drCol, ref strPK, ref j, ref strUqueList);
                }
                //唯一和外键
                if (!string.IsNullOrEmpty(strUqueList))
                {
                    //去掉最后的逗号
                    sbSql.Append(strUqueList.Substring(0, strUqueList.Length - 1));
                }

                //表创建完毕
                sbSql.Append(");\n");

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
                    GenerateSQLiteColumn(paramEntity, TableChangeType.Alter, strTableCode, drCol, ref strPK, ref j, ref strUqueList);
                }
                #endregion
            }
            iCalNum++;
        }

        private void GenerateSQLiteColumn(GenerateParamEntity paramEntity, TableChangeType tableDealType, string strTableCode, EntCol drCol, ref string strPK, ref int j, ref string strUqueList)
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
            //其他独有字段

            //独有字段
            YesNoType strColAutoAdd = YesNoType.No;//自增长
            string strColPKName = string.Empty;//主键名
            string strColUnique = string.Empty;//唯一约束名
            string strColForgKey = string.Empty;//外键
            string strColForgKeyCode = string.Empty;//外键名
            if (_isAllConvert)
            {
                //综合转换时，使用综合转换模板的独有值
                strColAutoAdd = drCol.allInOne.SQLite_AutoNum;//自增长
                strColPKName = drCol.allInOne.SQLite_PKName;//主键名
                strColUnique = drCol.allInOne.SQLite_UniqueName;//唯一约束名
                strColForgKey = drCol.allInOne.SQLite_FK;//外键
                strColForgKeyCode = drCol.allInOne.SQLite_FKName;//外键名
            }
            else if (importDBType == targetDBType)
            {
                //只有导入类型与目标类型一致才使用独有值
                strColAutoAdd = drCol.sqliteCol.AutoNum;//自增长
                strColPKName = drCol.sqliteCol.PKName;//主键名
                strColUnique = drCol.sqliteCol.UniqueName;//唯一约束名
                strColForgKey = drCol.sqliteCol.FK;//外键
                strColForgKeyCode = drCol.sqliteCol.FKName;//外键名
            }
            //其他变量
            string strTable_Col = strTableCode + "_" + strColCode;//表编码+"_"+列编码
            StringBuilder sbColSql = new StringBuilder();//列的SQL

            #region 转换字段类型与默认值
            if (importDBType != targetDBType && paramEntity.isNeedColumnTypeConvert)
            {
                ConvertDBTypeDefaultValueString(ref strColDataType, ref strColDefault, importDBType);
            }
            #endregion

            //数据类型(类型+长度+小数点)、
            string sDataType_Full = (_isAllConvert && !String.IsNullOrEmpty(drCol.allInOne.SQLite_FullDataType)) ? drCol.allInOne.SQLite_FullDataType : GetFullTypeString(drCol, strColDataType, strColLen, strColDecimalDigits);

            if (tableDealType == TableChangeType.Create)
            {
                #region 新增表处理

                #region 列类型和长度处理
                sbColSql.Append(AddRightBand(strColCode) + sDataType_Full);
                #endregion

                #region 主键
                if (strKey== ColKeyType.PK)
                {
                    sbColSql.Append(" PRIMARY KEY ");
                }
                #endregion

                #region 自增长
                if (strColAutoAdd == YesNoType.Yes)
                {
                    sbColSql.Append(" AUTOINCREMENT ");
                }
                #endregion

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
                    sbColSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                #region 列备注的处理
                //备注为空时
                //sbSql.Append(" COMMENT " + "'" + strColName + ":" + strColRemark + "'");
                #endregion

                #region 唯一性的处理
                if (!string.IsNullOrEmpty(strColUnique))
                {
                    //string strColDefaultName = "UQ_" + strTable_Col;
                    strUqueList += "CONSTRAINT " + strColUnique + " UNIQUE (" + strColCode + "),\n ";
                }
                #endregion

                #region 外键的处理
                if (strKey ==  ColKeyType.FK && !string.IsNullOrEmpty(strColForgKey) && strColForgKey.Contains("(") && strColForgKey.Contains(")"))
                {
                    string strColDefaultName = "FK_" + strTable_Col;
                    strUqueList += "CONSTRAINT" + strColForgKeyCode + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey + ",\n";
                }
                #endregion

                #region 增加逗号
                if (j == 1 && string.IsNullOrEmpty(strUqueList)) //最后一个字段，且没有唯一性和外键约束
                {
                    sbColSql.Append("" + ChangeIntoInfo(strColName) + "\n");
                }
                else
                {
                    sbColSql.Append("," + "" + ChangeIntoInfo(strColName) + "\n");
                }
                #endregion

                //
                sbSql.Append(sbColSql.ToString());
                //计数器
                j--;
                #endregion
            }
            else
            {
                //生成删除列SQL脚本
                if (createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create)
                {
                    if (strColumnDealType ==  ColumnChangeType.Create || strColumnDealType == ColumnChangeType.Drop_Create)
                    {
                        sbDelete.AppendLine("ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + ";");
                    }
                    //对于删除，直接下一个字段
                    if (createType == SQLCreateType.Drop)
                    {
                        return;//continue;
                    }
                }

                #region 修改表处理
                //类型长度小数点
                sbColSql.Append(AddRightBand(sDataType_Full));

                #region 主键
                if (strKey.Equals("PK"))
                {
                    sbColSql.Append(" PRIMARY KEY ");
                }
                #endregion

                #region 自增长
                if (strColAutoAdd.Equals("是"))
                {
                    sbColSql.Append(" AUTOINCREMENT ");
                }
                #endregion

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    sbColSql.Append(sNotNull);
                }
                //else
                //{
                //    sbColSql.Append(_strNull);
                //}
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    sbColSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                //备注
                //sbColSql.Append(" COMMENT " + "'" + strColName + ":" + strColRemark + "';");
                if (strColumnDealType == ColumnChangeType.Create)
                {
                    //得到修改表增加列语句
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType == ColumnChangeType.Alter)
                {
                    //改变表名： ALTER TABLE 旧表名 RENAME TO 新表名
                    //增加一列：ALTER TABLE 表名 ADD COLUMN 列名 数据类型 限定符
                    sbSql.AppendLine("/*注：SQLite不支持修改列类型等。*/");
                    //sbSql.AppendLine("ALTER TABLE " + strTableCode + " CHANGE " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType == ColumnChangeType.Drop)
                {
                    sbSql.AppendLine("/*注：SQlite不支持DROP COLUMN。*/");
                    //sbSql.AppendLine("ALTER TABLE " + strTableCode + " DROP COLUMN " + AddRightBand(strColCode) + ";");
                }
                else if (strColumnDealType == ColumnChangeType.Drop_Create)
                {
                    sbSql.AppendLine("/*注：SQlite不支持DROP COLUMN。*/");
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
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("getdate()", "(datetime('now','localtime'))")
                        .Replace("newid()", "");
                    break;
                case DataBaseType.Oracle:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar2", "varchar").
                        Replace("date", "datetime");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("sysdate", "(datetime('now','localtime'))")
                        .Replace("sys_guid()", "");
                    break;
                case DataBaseType.MySql:
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "");
                    break;
                case DataBaseType.SQLite:
                    break;
                case DataBaseType.PostgreSql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("character varying", "varchar")
                        .Replace("date", "datetime");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "");
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
        }

        /// <summary>
        /// SQLite的SQL转换其他DB类型
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="targetDbType"></param>
        public override void ConvertToDbSql(ref string sSql, DataBaseType targetDbType)
        {
            string sMatchSysdate = @"\s*,*\s*NOW\s*\(\s*\)\s*,?";
            string sMatchIfNull = @"\s*,*\s*IFNULL\s*\(\s*";
            //string sMatchsSysGuid = @"\s*,*\s*UUID\s*\(\s*\)\s*,?"; //不支持

            switch (targetDbType)
            {
                case DataBaseType.SqlServer:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLServerBuilder.SqlFuncString.NowDate);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLServerBuilder.SqlFuncString.IfNull);
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLServerBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.Oracle:
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, OracleBuilder.SqlFuncString.NowDate, BracketDealType.Remove);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, OracleBuilder.SqlFuncString.IfNull);
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, OracleBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.MySql:
                    //MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, MySQLBuilder.SqlFuncString.NowDate);
                    //MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, MySQLBuilder.SqlFuncString.IfNull);
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, MySQLBuilder.SqlFuncString.Guid);
                    break;
                case DataBaseType.SQLite:
                    break;
                case DataBaseType.PostgreSql:
                    //MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, "NOW"); //相同
                    //MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, "IFNULL"); //相同
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, "UUID"); 
                    break;
            }
        }

        public class SqlFuncString
        {
            public static string NowDate = "NOW";
            public static string IfNull = "IFNULL";
            //public static string Guid = "UUID";
            public static string Decimal = "DECIMAL";
        }
    }
}
