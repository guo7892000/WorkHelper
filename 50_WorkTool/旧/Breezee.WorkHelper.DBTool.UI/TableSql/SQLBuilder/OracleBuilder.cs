using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class OracleBuilder : SQLBuilder
    {
        string strUqueList = "";//唯一性
        public override void GenerateTableSQL(EntTable entTable)
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
                sbRemark.Append("comment on table " + strTableCode + " is '" + strTableName + "：" + strTableRemark + "';\n");

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

            //其他独有字段
            string strColPKName = _isAllConvert ? drCol.allInOne.Oracle_PKName : drCol.oracleCol.PKName;
            string strColSeries = _isAllConvert ? drCol.allInOne.Oracle_Sequence : drCol.oracleCol.SequenceName;
            string strColUnique = _isAllConvert ? drCol.allInOne.Oracle_UniqueName: drCol.oracleCol.UniqueName;
            string strColForgKey = _isAllConvert ? drCol.allInOne.Oracle_FK : drCol.oracleCol.FK;
            string strColForgKeyCode = _isAllConvert ? drCol.allInOne.Oracle_FKName : drCol.oracleCol.FKName; //外键名

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

        protected override void ConvertDBTypeDefaultValueString(ref string sDbType, ref string sDefaultValue, DataBaseType impDbType)
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
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("now()", "sysdate");
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


    }
}
