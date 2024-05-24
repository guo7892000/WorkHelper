using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// Oracle的SQL构造器
    /// 包括两部分功能：
    /// 1、表结构生成SQL
    /// 2、将SQL转换为其他DB的SQL
    /// </summary>
    public class OracleBuilder : SQLBuilder
    {
        string strUqueList = "";//唯一性
        //Oracle转换日期和时间的正则式
        //static string _ToDateCharPatter = @"\s*,*\s*TO_((DATE)|(CHAR))\s*\(\s*(((\w+\.)*@?\w+)|(\'?#?\w+#?\'?)|(\'[^']+\'))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*";
        //static string _ToDateCharPatter = @"\s*,*\s*TO_((DATE)|(CHAR))\s*\(\s*(((\w+\.)*@?\w+)|(\'?#?\w+#?\'?))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*";
        //static string _ToDateCharAddPatter = _ToDateCharPatter + @"[+-]?\s*[\d./]+";
        public override void GenerateTableSQL(EntTable entTable, GenerateParamEntity paramEntity)
        {
            string strTableCode = entTable.Code;
            string strTableName = entTable.Name;

            TableChangeType strTableDealType = entTable.ChangeTypeEnum;
            string strTableRemark = entTable.Remark;

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
            strUqueList = "";

            if ((createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create) && strTableDealType ==  TableChangeType.Create)
            {
                //strDeleteSql = strDeleteSql.Insert(0, "DROP TABLE " + strTableCode + ";\n");//倒着删除掉
                sbDelete = sbDelete.Insert(0, "declare  nCount  number;\n"
                    + "begin\n"
                    + "  nCount:=0;\n"
                    + "  select count(1) into nCount from user_objects\n "
                    + "  where upper(object_name) = '" + strTableCode + "' and upper(object_type) = 'TABLE';\n"
                    + "  if nCount = 1 then \n"
                    + "    begin \n"
                    + "      execute immediate 'drop TABLE " + strTableCode + "';\n"
                    + "    end;\n"
                    + "  end if;\n"
                    + "end;\n"
                    + "/\n");
            }

            string strSequence = "";//序列：一张表就一个，名字为:Se_表名

            if (strTableDealType ==  TableChangeType.Create)
            {
                #region 新增处理
                sbSql.Append("/*" + iCalNum.ToString() + "、新增表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");
                sbSql.Append(AddRightBand("CREATE TABLE") + AddRightBand(strTableCode) + "(\n");
                //表说明SQL
                if (string.IsNullOrEmpty(strTableRemark))
                {
                    sbRemark.Append("comment on table " + strTableCode + " is '" + strTableName + "';\n");
                }
                else
                {
                    sbRemark.Append("comment on table " + strTableCode + " is '" + strTableName + "：" + strTableRemark + "';\n");
                }
                    
                int j = tableCols.Count();
                //string strDefaultList = "";//默认值

                foreach (EntCol drCol in tableCols)
                {
                    GenerateOracleColumn(TableChangeType.Create, strTableCode, drCol, ref strPK, ref j, ref strSequence);
                }
                //表创建完毕
                sbSql.Append(");\n");
                if (strPK != "")
                {
                    sbSql.Append(strPK);//主键的处理
                }
                sbSql.Append(strUqueList);//唯一和外键
                sbSql.Append(sbRemark.ToString());//添加列说明
                sbSql.Append(strSequence + "/\n");//添加序列
                sbRemark = new StringBuilder();
                #endregion
            }
            else
            {
                #region 修改列处理
                //alter table TEST1 drop column planmonth;
                sbSql.Append("/*" + iCalNum.ToString() + "、修改表：" + strTableName + AddLeftRightKuoHao(strTableCode) + "*/\n");
                int j = 1;
                foreach (EntCol drCol in tableCols)
                {
                    GenerateOracleColumn(TableChangeType.Alter, strTableCode, drCol, ref strPK, ref j, ref strSequence);
                }
                //sbSql.Append("/\n");
                #endregion
            }
            iCalNum++;
        }

        private void GenerateOracleColumn(TableChangeType tableDealType, string strTableCode, EntCol drCol, ref string strPK, ref int j, ref string strSequence)
        {
            //表编码,列名称,列编码,类型,长度,键,必填,约束,备注,自增长设置
            //列处理类型，当为空时表示新增
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
            string strColPKName = string.Empty;
            string strColSeries = string.Empty;
            string strColUnique = string.Empty;
            string strColForgKey = string.Empty;
            string strColForgKeyCode = string.Empty; //外键名
            if (_isAllConvert)
            {
                //综合转换时，使用综合转换模板的独有值
                strColPKName = drCol.allInOne.Oracle_PKName;
                strColSeries = drCol.allInOne.Oracle_Sequence;
                strColUnique = drCol.allInOne.Oracle_UniqueName;
                strColForgKey = drCol.allInOne.Oracle_FK;
                strColForgKeyCode = drCol.allInOne.Oracle_FKName; //外键名
            }
            else if (importDBType == targetDBType)
            {
                //只有导入类型与目标类型一致才使用独有值
                strColPKName = drCol.oracleCol.PKName;
                strColSeries = drCol.oracleCol.SequenceName;
                strColUnique = drCol.oracleCol.UniqueName;
                strColForgKey = drCol.oracleCol.FK;
                strColForgKeyCode = drCol.oracleCol.FKName; //外键名
            }
            //其他变量
            string strTable_Col = strTableCode + "_" + strColCode;//表编码+"_"+列编码
            string strCanNull = "";//是否可空
            string strDefault_Full = "";//默认值

            #region 转换字段类型与默认值
            if (importDBType != targetDBType)
            {
                ConvertDBTypeDefaultValueString(ref strColDataType, ref strColDefault, importDBType);
            }
            #endregion

            //数据类型(类型+长度+小数点)
            string sDataType_Full = (_isAllConvert && !String.IsNullOrEmpty(drCol.allInOne.Oracle_FullDataType)) ? drCol.allInOne.Oracle_FullDataType : GetFullTypeString(drCol, strColDataType, strColLen, strColDecimalDigits);

            if (tableDealType == TableChangeType.Create)
            {
                #region 新增表处理

                #region 列类型和长度处理
                sbSql.Append(AddRightBand(strColCode) + sDataType_Full);
                if (strKey ==  ColKeyType.PK) //主键处理
                {
                    string strPK_Name = string.IsNullOrEmpty(strColPKName) ? "PK_" + strTableCode : strColPKName;
                    strPK = "alter table " + strTableCode + " add constraint " + strPK_Name + " primary key (" + strColCode + ");\n";
                }
                #endregion

                #region 序列的处理
                if (string.IsNullOrEmpty(strSequence) && !string.IsNullOrEmpty(strColSeries))
                {
                    if (createType ==  SQLCreateType.Drop_Create || createType == SQLCreateType.Drop)
                    {
                        //删除序列SQL
                        sbDelete.Append("declare  nCount  number;\n"
                            + "begin\n"
                            + "  nCount:=0;\n"
                            + "  select count(1) into nCount from user_objects\n "
                            + "  where upper(object_name) = upper('" + strColSeries.ToUpper() + "') and upper(object_type) = 'SEQUENCE';\n"
                            + "  if nCount = 1 then \n"
                            + "    begin \n"
                            + "      execute immediate 'drop sequence " + strColSeries + "';\n"
                            + "    end;\n"
                            + "  end if;\n"
                            + "end;\n"
                            + "/\n");
                        if (createType ==  SQLCreateType.Drop)
                        {
                            return;
                        }
                    }
                    strSequence += "create sequence " + strColSeries + " minvalue 1 maxvalue 9999999999999999999999999999 start with 1 increment by 1 cache 20;\n";
                }
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    //string strColDefaultName = "DF_" + strTableCode + "_" + strColCode;
                    sbSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    sbSql.Append(sNotNull);
                }
                else
                {
                    sbSql.Append(sNull);
                }
                #endregion

                #region 增加逗号和列注释处理
                if (j > 1)
                {
                    sbSql.Append("," + "" + ChangeIntoInfo(strColName) + "\n");
                }
                else
                {
                    //最后一列不加逗号
                    sbSql.Append("" + ChangeIntoInfo(strColName) + "\n");
                }
                #endregion

                #region 列备注的处理
                if (string.IsNullOrEmpty(strColRemark))
                {
                    //备注为空时
                    if (!string.IsNullOrEmpty(strColSeries))
                    {
                        //序列不为空时
                        sbRemark.Append("comment on column " + strTableCode + "." + strColCode + " is '" + strColName + "。序列为" + strColSeries + "';\n");
                    }
                    else
                    {
                        //序列为空时
                        sbRemark.Append("comment on column " + strTableCode + "." + strColCode + " is '" + strColName + "';\n");
                    }
                }
                else
                {
                    //备注不为空时
                    if (!string.IsNullOrEmpty(strColSeries))
                    {
                        //序列不为空时
                        sbRemark.Append("comment on column " + strTableCode + "." + strColCode + " is '" + strColName + "：" + strColRemark + "。序列为" + strColSeries + "';\n");
                    }
                    else
                    {
                        //序列为空时
                        sbRemark.Append("comment on column " + strTableCode + "." + strColCode + " is '" + strColName + "：" + strColRemark + strColSeries + "';\n");
                    }
                }
                #endregion

                #region 唯一性的处理
                if (!string.IsNullOrEmpty(strColUnique))
                {
                    //string strColDefaultName = "UQ_" + strTable_Col;
                    strUqueList += "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColUnique + " UNIQUE (" + strColCode + ");\n ";
                }
                #endregion

                #region 外键的处理
                if (strKey ==  ColKeyType.FK && !string.IsNullOrEmpty(strColForgKey) && strColForgKey.Contains("(") && strColForgKey.Contains(")"))
                {
                    //string strColDefaultName = "FK_" + strTable_Col;
                    strUqueList += "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColForgKeyCode + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey + ";\n";
                }
                #endregion

                //计数器
                j--;
                #endregion
            }
            else
            {
                #region 修改表处理

                //生成删除列SQL脚本
                if (createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create)
                {
                    if (strColumnDealType ==  ColumnChangeType.Create || strColumnDealType == ColumnChangeType.Drop_Create)
                    {
                        //strDeleteSql += "alter table " + strTableCode + " drop column " + strColCode + ";\n";
                        sbDelete.Append("declare iCount number;\n"
                        + "begin\n"
                        + "  iCount:=0;\n"
                        + "  select count(1) into iCount \n"
                        + "  from user_tab_columns where upper(table_name)= upper('" + strTableCode + "') and upper(column_name)=  upper('" + strColCode.ToUpper() + "'); \n"
                        + "  if iCount = 1 then \n"
                        + "    begin \n"
                        + "      execute immediate 'alter table " + strTableCode + " drop column " + strColCode + "';\n"
                        + "    end;\n"
                        + "  end if;\n"
                        + "end;\n"
                        + "/\n");
                    }
                    //对于删除，直接下一个字段
                    if (createType ==  SQLCreateType.Drop)
                    {
                        return; 
                    }
                }

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    strCanNull = sNotNull;
                }
                else
                {
                    strCanNull = sNull;
                }
                #endregion
                #region 唯一性的处理
                if (!string.IsNullOrEmpty(strColUnique))
                {
                    //string strColDefaultName = "UQ_" + strTable_Col;
                    strUqueList = "\n" +
                    "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColUnique + " UNIQUE +(" + strColCode + ");\n ";
                    sbSql.Append(strUqueList);
                }
                #endregion
                #region 外键的处理
                if (strKey ==  ColKeyType.FK && !string.IsNullOrEmpty(strColForgKey) && strColForgKey.Contains("(") && strColForgKey.Contains(")"))
                {
                    //string strColDefaultName = "FK_" + strTable_Col;
                    strUqueList = "\n" +
                    "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColForgKeyCode + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey + ";\n";
                    sbSql.Append(strUqueList);
                }
                #endregion
                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    //string strColDefaultName = "DF_" + strTableCode + "_" + strColCode;
                    if (strColDataType == "varchar2" || strColDataType == "nvarchar2" || strColDataType == "char")
                    {
                        //字符型处理
                        strDefault_Full = AddRightBand("default '" + strColDefault + "'");
                    }
                    else
                    {
                        //数值型处理
                        strDefault_Full = AddRightBand("default " + strColDefault.Replace("'", ""));
                    }
                }
                #endregion

                if (strColumnDealType ==  ColumnChangeType.Create)
                {
                    //得到修改表增加列语句
                    sbSql.Append("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sDataType_Full + strDefault_Full + strCanNull + ";\n");
                    //增加注解
                    if (string.IsNullOrEmpty(strColRemark))
                    {
                        sbSql.Append("COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "';\n");
                    }
                    else
                    {
                        sbSql.Append("COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "：" + strColRemark + "';\n");
                    }
                }
                else if (strColumnDealType ==  ColumnChangeType.Alter)
                {
                    sbSql.Append("/*注：对修改字段，如要变更是否可空类型，则自己在最后加上NULL 或NOT NULL。对字段类型的变更，需要先清空该字段值或删除该列再新增*/\n");
                    sbSql.Append("ALTER TABLE " + strTableCode + " MODIFY " + AddRightBand(strColCode) + sDataType_Full + strDefault_Full + ";\n");
                }
                else if (strColumnDealType ==  ColumnChangeType.Drop)
                {
                    sbSql.Append("ALTER TABLE " + strTableCode + " DROP COLUMN " + AddRightBand(strColCode) + ";\n");
                }
                else if (strColumnDealType ==  ColumnChangeType.Drop_Create)
                {
                    sbSql.Append("ALTER TABLE " + strTableCode + " DROP COLUMN " + AddRightBand(strColCode) + ";\n");
                    //得到修改表增加列语句
                    sbSql.Append("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sDataType_Full + strDefault_Full + strCanNull + ";\n");
                    //增加注解
                    if (string.IsNullOrEmpty(strColRemark))
                    {
                        sbSql.Append("COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "';\n");
                    }
                    else
                    {
                        sbSql.Append("COMMENT ON COLUMN " + strTableCode + "." + strColCode + " IS '" + strColName + "：" + strColRemark + "';\n");
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
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar", "varchar2").Replace("datetime", "date");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("getdate()", "sysdate")
                        .Replace("newid()", "sys_guid()");
                    break;
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.MySql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("decimal", "number");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "sysdate").Replace("uuid()", "sys_guid()");
                    break;
                case DataBaseType.SQLite:
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("(datetime('now','localtime'))", "sysdate");
                    break;
                case DataBaseType.PostgreSql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("character varying", "varchar2");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "sysdate");
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
        }

        /// <summary>
        /// Oracle的SQL转换其他DB类型
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="targetDbType"></param>
        public override void ConvertToDbSql(ref string sSql, DataBaseType targetDbType)
        {
            string sMatchSysdate = @"\s*,*\s*SYSDATE\s*,*\s*";
            string sMatchSysdate2 = @"\s*(,|=)*\s*SYSTIMESTAMP\s*,*\s*"; 
            string sMatchIfNull = @"\s*,*\s*NVL\s*\(\s*";
            string sMatchsSysGuid = @"\s*,*\s*SYS_GUID\s*\(\s*\)\s*,?";
            string sMatchNumber = @"\s+NUMBER\s*\(";

            switch (targetDbType)
            {
                case DataBaseType.SqlServer:
                    MatchReplace(ref sSql, sMatchSysdate2, SqlFuncString.SysTimStamp, SQLServerBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLServerBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLServerBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLServerBuilder.SqlFuncString.Guid);
                    MatchReplace(ref sSql, sMatchNumber, SqlFuncString.Decimal, SQLServerBuilder.SqlFuncString.Decimal);
                    break;
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.MySql:
                    MatchReplace(ref sSql, sMatchSysdate2, SqlFuncString.SysTimStamp, MySQLBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, MySQLBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, MySQLBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, MySQLBuilder.SqlFuncString.Guid);
                    MatchReplace(ref sSql, sMatchNumber, SqlFuncString.Decimal, MySQLBuilder.SqlFuncString.Decimal);
                    break;
                case DataBaseType.SQLite:
                    MatchReplace(ref sSql, sMatchSysdate2, SqlFuncString.SysTimStamp, SQLiteBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, SQLiteBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, SQLiteBuilder.SqlFuncString.IfNull);
                    //MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, SQLiteBuilder.SqlFuncString.Guid); //不支持
                    MatchReplace(ref sSql, sMatchNumber, SqlFuncString.Decimal, SQLiteBuilder.SqlFuncString.Decimal);
                    break;
                case DataBaseType.PostgreSql:
                    MatchReplace(ref sSql, sMatchSysdate2, SqlFuncString.SysTimStamp, PostgreSQLBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchSysdate, SqlFuncString.NowDate, PostgreSQLBuilder.SqlFuncString.NowDate, BracketDealType.Add);
                    MatchReplace(ref sSql, sMatchIfNull, SqlFuncString.IfNull, PostgreSQLBuilder.SqlFuncString.IfNull);
                    MatchReplace(ref sSql, sMatchsSysGuid, SqlFuncString.Guid, PostgreSQLBuilder.SqlFuncString.Guid);
                    MatchReplace(ref sSql, sMatchNumber, SqlFuncString.Decimal, PostgreSQLBuilder.SqlFuncString.Decimal);
                    break;
            }
            //先替换TO_DATE多加一天的
            MatchDateAddOneReplace(ref sSql, targetDbType);
            //替换Date的TO_CHAR
            MatchDateToCharReplace(ref sSql, targetDbType);
            //替换Decode：要放最后
            MatchDecodeReplace(ref sSql);
            //替换字符拼接
            MatchContact(ref sSql);
        }

        private void MatchDecodeReplace(ref string sSql)
        {
            /****SQL示例： ****************************
select sysdate AS CUR_DATE,
SYS_guid() AS NEW_ID,
decode( A.OUT_TYPE ,'0','未出库','1','已出库' , '其他' ) AS OUT_TYPE_NAME,
Nvl(A.OUT_STATUS,'0')   AS dd
FROm T_PA_OUT_M
LEFT JOIn T_PA_OUT_D B
 ON A.OUT_ID=B.OUT_ID
WHERE 1=1
             *****************************/
            //注：匹配中文[\u4e00-\u9fa5]
            string sIfnullPatter = @"\s*I(S|F)NULL\s*\(\s*(\w+.)*\w+\s*,\s*\'?([\u4e00-\u9fa5]|\w)+\'?\s*\)";
            //DECODE正常正则式：遇到IFNULL，匹配的结果会包括IFNULL部分字符，但DECODE不全。
            //string sDecodePatterNormal = @",?\s*DECODE\s*\(\s*(\w+.)*\w+(\s*,\s*\'?([\u4e00-\u9fa5]|\w)+\'?\s*)+\)";
            string sDecodePatterNormal = @",?\s*DECODE\s*\(\s*(\w+.)*\w+(\s*,\s*((\'?([\u4e00-\u9fa5]|\w)+\'?)|((\w+.)*\w+))\s*)+\)";
            //DECODE包括IFNULL部分的正则式(OK)：将判断列的那部分：(\w+.)*\w+   替换为IFNULL部分的正则
            string sDecodePatterIncludeIfnull = @",?\s*DECODE\s*\(\s*"+ sIfnullPatter + @"(\s*,\s*\'?([\u4e00-\u9fa5]|\w)+\'?\s*)+\)";
            
            //先处理包括Ifnull的DECODE语句
            Regex regex = new Regex(sDecodePatterIncludeIfnull, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sSql);
            bool isNowWhen = false;
            foreach (Match mt in mcColl)
            {
                OneDecodeDeal(ref sSql, sIfnullPatter, ref isNowWhen, mt,false); //不排除ifnull的字符处理
            }

            //再处理不包括Ifnull的DECODE语句
            regex = new Regex(sDecodePatterNormal, RegexOptions.IgnoreCase);
            mcColl = regex.Matches(sSql);
            isNowWhen = false;
            foreach (Match mt in mcColl)
            {
                OneDecodeDeal(ref sSql, sIfnullPatter, ref isNowWhen, mt, true); //排除ifnull的字符处理
            }
        }

        /// <summary>
        /// 单个DECODE语句处理
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="sIfnullPatter"></param>
        /// <param name="isNowWhen"></param>
        /// <param name="mt"></param>
        /// <param name="isExcludeIfnull">是否排除ifnull的字符处理</param>
        private static void OneDecodeDeal(ref string sSql, string sIfnullPatter, ref bool isNowWhen, Match mt,bool isExcludeIfnull)
        {
            string sMustColumnName = string.Empty;
            string sFind = string.Empty;
            bool isStartWithComma = false;

            sMustColumnName = mt.Value;
            sMustColumnName = sMustColumnName.Trim();

            if (isExcludeIfnull)
            {
                if(sMustColumnName.IndexOf("IFNULL", StringComparison.OrdinalIgnoreCase)>-1
                   || sMustColumnName.IndexOf("ISNULL", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    return;
                }
            }


            //要处理掉ifnull先
            Regex regexTime = new Regex(sIfnullPatter, RegexOptions.IgnoreCase);
            MatchCollection mcCollTime = regexTime.Matches(sMustColumnName);
            string sDecodeColun = string.Empty;
            foreach (Match mtTime in mcCollTime)
            {
                sDecodeColun = mtTime.Value;
                sMustColumnName = sMustColumnName.Replace(sDecodeColun, "#DECODE_COLUMN_PLACE#");
                break;
            }

            sFind = ",";
            if (sMustColumnName.StartsWith(sFind))
            {
                sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                isStartWithComma = true;
            }
            sFind = "decode";
            if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
            {
                sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
            }
            sFind = "(";
            if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
            {
                sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
            }
            sFind = ")";
            if (sMustColumnName.EndsWith(sFind, StringComparison.OrdinalIgnoreCase))
            {
                sMustColumnName = sMustColumnName.Substring(0, sMustColumnName.Length - 1).Trim();
            }

            //这里开始构造CASE WHEN
            string[] arrList = sMustColumnName.Split(new char[] { ',' });
            StringBuilder sb = new StringBuilder();
            if (isStartWithComma)
            {
                sb.AppendLine(",");
            }
            else
            {
                sb.AppendLine("");
            }
            sb.Append("CASE ");

            for (int i = 0; i < arrList.Length; i++)
            {
                if (i == 0)
                {
                    sb.Append(arrList[0] + " "); //这里要加上空格
                    isNowWhen = true;
                }
                else
                {
                    if (isNowWhen)
                    {
                        if (i == arrList.Length - 1)
                        {
                            sb.Append(string.Format("ELSE {0} ", arrList[i]));
                        }
                        else
                        {
                            sb.Append(string.Format("WHEN {0} ", arrList[i]));
                        }
                    }
                    else
                    {
                        sb.Append(string.Format("THEN {0} ", arrList[i]));
                    }
                    isNowWhen = !isNowWhen;
                }
            }
            sb.Append("END ");
            if (!string.IsNullOrEmpty(sDecodeColun))
            {
                sb = sb.Replace("#DECODE_COLUMN_PLACE#", sDecodeColun + " ");
            }
            //替换字符
            sSql = sSql.Replace(mt.Value, sb.ToString());
        }

        private void MatchDateToCharReplace(ref string sSql, DataBaseType targetDbType)
        {
            /****SQL示例： ****************************
SELECT  TO_CHAR(A.OUT_STORE_DATE, 'YYYY/MM/DD') OUT_STORE_DATE,
                           TO_DATE(A.OUT_STORE_DATE, 'YYYY-MM-DD') OUT_STORE_DATE,
                           DECODE(NVL(A.FI_AUDIT_FLAG,'0'), '1', '已审核', '未审核') AUDIT_FLAG,
                           TO_CHAR( A.FI_AUDIT_DATE ,     'YYYY-MM-DD hh24:mi:ss:ff3') FI_AUDIT_DATE
                      FROM T_PA_BU_OEM_OUT_STORE A
                     INNER JOIN T_MDM_ORG_DLR B
                        ON A.DLR_ID = B.DLR_ID
 *****************************/
            /*支持TO_DATE、TO_CHAR，且日期且使用-或/分隔*/
            //全部匹配的
            //string sToDateCharPatter = @"\s*,*\s*TO_((DATE)|(CHAR))\s*\(\s*(((\w+\.)*\w+)|(@\w+)|(\'?#?\w+#?\'?)|(\'[^']+\'))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*,?";
            string sToDateCharPatter = @"\s*,*\s*TO_((DATE)|(CHAR))\s*\(\s*(((\w+\.)*\w+)|(@\w+)|(\'?#?\w+#?\'?)|(\'[^']+\'))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*,?";
            Regex regex = new Regex(sToDateCharPatter, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sSql.ToString());

            foreach (Match mt in mcColl)
            {
                string sMustColumnName = string.Empty;
                string sFind = string.Empty;
                bool isStartWithComma = false;
                bool isHourMinuteSecond = false; //是否具体到时分秒

                sMustColumnName = mt.Value;
                sMustColumnName = sMustColumnName.Trim();
                
                sFind = ",";
                if (sMustColumnName.StartsWith(sFind))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                    isStartWithComma = true;
                }
                sFind = "TO_CHAR";
                if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                }
                sFind = "TO_DATE";
                if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                }
                sFind = "(";
                if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                }

                //确定是否具体至时分秒
                string sTimePatter = @"\s+((HH:)|(HH24:))+";
                Regex regexTime = new Regex(sTimePatter, RegexOptions.IgnoreCase);
                MatchCollection mcCollTime = regexTime.Matches(sMustColumnName);
                foreach (Match mtTime in mcCollTime)
                {
                    isHourMinuteSecond = true;
                    break;
                }

                sFind = ",";
                int iComma = sMustColumnName.LastIndexOf(sFind);
                if (iComma>-1)
                {
                    sMustColumnName = sMustColumnName.Substring(0, iComma).Trim();
                }

                StringBuilder sb = new StringBuilder();
                if (isStartWithComma)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.Append(" ");
                }
                //构造新字符
                if (targetDbType== DataBaseType.SqlServer)
                {
                    if (isHourMinuteSecond)
                    {
                        sb.Append(string.Format("format({0},'yyyy-MM-dd HH:mm:ss') ", sMustColumnName));
                    }
                    else
                    {
                        sb.Append(string.Format("format({0},'yyyy-MM-dd') ", sMustColumnName));
                    }
                }
                else
                {
                    if (isHourMinuteSecond)
                    {
                        sb.Append(string.Format("DATE_FORMAT({0},'%Y-%m-%d %H:%i:%s') ", sMustColumnName));
                    }
                    else
                    {
                        sb.Append(string.Format("DATE_FORMAT({0},'%Y-%m-%d') ", sMustColumnName));
                    }
                }

                //替换字符
                sSql = sSql.Replace(mt.Value, sb.ToString());
            }
        }

        /// <summary>
        /// 针对支持TO_DATE且加一天
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="targetDbType"></param>
        private void MatchDateAddOneReplace(ref string sSql, DataBaseType targetDbType)
        {
            /****SQL示例： ****************************
SELECT  TO_CHAR(A.OUT_STORE_DATE, 'YYYY/MM/DD') OUT_STORE_DATE,
                           TO_DATE(A.OUT_STORE_DATE, 'YYYY-MM-DD') OUT_STORE_DATE,
                           DECODE(NVL(A.FI_AUDIT_FLAG,'0'), '1', '已审核', '未审核') AUDIT_FLAG,
                           TO_CHAR( A.FI_AUDIT_DATE ,     'YYYY-MM-DD hh24:mi:ss:ff3') FI_AUDIT_DATE
                      FROM T_PA_BU_OEM_OUT_STORE A
                     INNER JOIN T_MDM_ORG_DLR B
                        ON A.DLR_ID = B.DLR_ID
            where A.OUT_STORE_DATE < TO_DATE(@OUT_STORE_DATE, 'YYYY-MM-DD') + 1
 *****************************/
            /*支持TO_DATE且加指定天；列名支持A.COL_NAME，@COL_NAME，#COL_NAME#，'#COL_NAME#','{0}'*/
            //全部匹配：但很慢，要24秒。最后经过分析发现，不加最后的[\d./]+，就是0.03秒出来，加上后就为24秒以上了。
            //string sDateAddPatter = @"\s*,*\s*TO_DATE\s*\(\s*(((\w+\.)*\w+)|(@\w+)|(\'?#?\w+#?\'?)|(\'[^']+\'))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*[+-]?\s*[\d./]+";
            //优化后：进一步将\d改为\w，速度立刻为0.03秒。由些可见，谨慎使用\d。
            //另外，还有[+-]?，如果去掉?号，也会很慢，或者直接将其改为+，也是很慢，有点奇怪。最终只能保留这个正则式，因为速度比较快，在匹配项中要包括+或-才处理
            string sDateAddPatter = @"\s*,*\s*TO_DATE\s*\(\s*(((\w+\.)*\w+)|(@\w+)|(\'?#?\w+#?\'?)|(\'[^']+\'))+\s*,\s*\'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'\)\s*[+-]?\s*[\w./]+";

            Regex regex = new Regex(sDateAddPatter, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sSql.ToString());

            foreach (Match mt in mcColl)
            {
                string sMustColumnName = string.Empty;
                string sFind = string.Empty;
                bool isStartWithComma = false;
                bool isHourMinuteSecond = false; //是否具体到时分秒

                sMustColumnName = mt.Value;
                sMustColumnName = sMustColumnName.Trim();

                sFind = ",";
                if (sMustColumnName.StartsWith(sFind))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                    isStartWithComma = true;
                }
                sFind = "TO_DATE";
                if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                }
                sFind = "(";
                if (sMustColumnName.StartsWith(sFind, StringComparison.OrdinalIgnoreCase))
                {
                    sMustColumnName = sMustColumnName.Substring(sFind.Length).Trim();
                }

                //查找天数
                int iDays = sMustColumnName.LastIndexOf("+");
                string sAddDays = "1";
                if (iDays > -1)
                {
                    //有+加
                    sAddDays = sMustColumnName.Substring(iDays).Trim();
                }
                else
                {
                    //判断有没-号：先替换掉年月日中的-
                    string sDatePatter1 = @"'YYYY[-/]?MM[-/]?DD(\s+\w+(:\w+)*)*\'";
                    Regex regex1 = new Regex(sDatePatter1, RegexOptions.IgnoreCase);
                    MatchCollection mcColl1 = regex1.Matches(sMustColumnName);
                    foreach (Match mt1 in mcColl1)
                    {
                        sMustColumnName = sMustColumnName.Replace(mt1.Value, "");
                    }

                    //再判断有没-号
                    iDays = sMustColumnName.LastIndexOf("-");
                    if (iDays > -1)
                    {
                        sAddDays = sMustColumnName.Substring(iDays).Trim();
                    }
                    else
                    {
                        continue;//没有加号或减号，说明不是日期增加的SQL，则跳过该正则式
                    }
                }

                //确定是否具体至时分秒
                string sTimePatter = @"\s+((HH:)|(HH24:))+";
                Regex regexTime = new Regex(sTimePatter, RegexOptions.IgnoreCase);
                MatchCollection mcCollTime = regexTime.Matches(sMustColumnName);
                foreach (Match mtTime in mcCollTime)
                {
                    isHourMinuteSecond = true;
                    break;
                }

                sFind = ",";
                int iComma = sMustColumnName.LastIndexOf(sFind);
                if (iComma > -1)
                {
                    sMustColumnName = sMustColumnName.Substring(0, iComma).Trim();
                }

                StringBuilder sb = new StringBuilder();
                if (isStartWithComma)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.Append(" ");
                }
                //构造新字符
                if (targetDbType == DataBaseType.SqlServer)
                {
                    if (isHourMinuteSecond)
                    {
                        sb.Append(string.Format("DATEADD(day, {0},format({1},'yyyy-MM-dd HH:mm:ss')) ", sAddDays,sMustColumnName));
                    }
                    else
                    {
                        sb.Append(string.Format("DATEADD(day, {0},format({1},'yyyy-MM-dd')) ", sAddDays,sMustColumnName));
                    }
                }
                else
                {
                    if (isHourMinuteSecond)
                    {
                        sb.Append(string.Format("DATE_ADD(DATE_FORMAT({0},'%Y-%m-%d %H:%i:%s'), INTERVAL {1} DAY) ", sMustColumnName, sAddDays));
                    }
                    else
                    {
                        sb.Append(string.Format("DATE_ADD(DATE_FORMAT({0},'%Y-%m-%d'), INTERVAL {1} DAY) ", sMustColumnName, sAddDays));
                    }
                }

                //替换字符
                sSql = sSql.Replace(mt.Value, sb.ToString());
            }
        }

        /// <summary>
        /// 匹配连接符||
        /// </summary>
        /// <param name="sSql"></param>
        private void MatchContact(ref string sSql)
        {
            //注：匹配双单引号包裹的内部不含单引号的正则为：\'[^']+\'
            string sConcatPatter = @",?\s*(((\'[^']+\')|(\w+\.)*\w+))\s*(\s*\|\|\s*((\'[^']+\')|((\w+\.)*\w+)))+";
            Regex regex = new Regex(sConcatPatter, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sSql);

            foreach (Match mt in mcColl)
            {
                string sFind = ",";
                string sCurMatch = mt.Value;
                bool isStartWithComma = false;
                if (sCurMatch.StartsWith(sFind))
                {
                    sCurMatch = sCurMatch.Substring(sFind.Length).Trim();
                    isStartWithComma = true;
                }

                string[] arrList = sCurMatch.Split(new string[] { "||" },StringSplitOptions.None);
                StringBuilder sb = new StringBuilder();
                if (isStartWithComma)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.Append(" ");
                }
                sb.Append("CONCAT(");
                for (int i = 0; i < arrList.Length; i++)
                {
                    if(i==0)
                    {
                        sb.Append(arrList[i]);
                    }
                    else
                    {
                        sb.Append(","+ arrList[i]);
                    }
                }
                sb.Append(")");
                //替换字符
                sSql = sSql.Replace(mt.Value, sb.ToString());
            }

        }

        public class SqlFuncString
        {
            public static string NowDate = "SYSDATE";
            public static string SysTimStamp = "SYSTIMESTAMP";
            public static string IfNull = "NVL";
            public static string Guid = "SYS_GUID";
            public static string Decimal = "NUMBER";
        }
    }
}
