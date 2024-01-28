using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class MySQLBuilder : SQLBuilder
    {
        public override void GenerateTableSQL(EntTable entTable)
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
            strTableCode = "`" + strTableCode + "`";
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
                sbRemark.Append(string.Format("ALTER TABLE {0} COMMENT '{1}';\n", strTableCode, strTableName + "：" + strTableRemark));

                int j = tableCols.Count();
                //string strDefaultList = "";//默认值
                //string strUqueList = "";
                //string strSequence = "";//序列：一张表就一个，名字为:Se_表名
                foreach (EntCol drCol in tableCols)
                {
                    //增加MySql列
                    GenerateMySqlColumn(TableChangeType.Create, strTableCode, drCol, ref strPK, ref j);
                }
                if (strPK != "")
                {
                    sbSql.Append(strPK);//主键的处理,MySql是一句独立的
                }
                //表创建完毕
                sbSql.Append(");\n");

                //sbSql.Append(strUqueList);//唯一和外键
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
                    GenerateMySqlColumn(TableChangeType.Alter, strTableCode, drCol, ref strPK, ref j);
                }
                #endregion
            }
            iCalNum++;
        }

        private void GenerateMySqlColumn(TableChangeType tableDealType, string strTableCode, EntCol drCol, ref string strPK, ref int j)
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
            YesNoType strColUnsign = YesNoType.No;
            YesNoType strColAutoAdd = YesNoType.No;
            string strColForgKey = string.Empty;
            if (_isAllConvert)
            {
                //综合转换时，使用综合转换模板的独有值
                strColUnsign = drCol.allInOne.MySql_Nonnegative;
                strColAutoAdd = drCol.allInOne.MySql_AutoNum;
                strColForgKey = drCol.allInOne.MySql_FK;
            }
            else if (importDBType == targetDBType)
            {
                //只有导入类型与目标类型一致才使用独有值
                strColUnsign = drCol.mySqlCol.Nonnegative;
                strColAutoAdd = drCol.mySqlCol.AutoNum;
                strColForgKey = drCol.mySqlCol.FK;
            }
            //其他变量
            string strTable_Col = strTableCode + "_" + strColCode;//表编码+"_"+列编码

            #region 转换字段类型与默认值
            if (importDBType != targetDBType)
            {
                ConvertDBTypeDefaultValueString(ref strColDataType, ref strColDefault, importDBType);
            }
            #endregion

            //数据类型(类型+长度+小数点)
            string sDataType_Full = (_isAllConvert && !String.IsNullOrEmpty(drCol.allInOne.MySql_FullDataType)) ? drCol.allInOne.MySql_FullDataType : GetFullTypeString(drCol, strColDataType, strColLen, strColDecimalDigits);
            //列加上前后缀
            strColCode = "`" + strColCode + "`";
            if (tableDealType == TableChangeType.Create)
            {
                #region 新增表处理

                #region 列类型和长度处理
                sbSql.Append(AddRightBand(strColCode) + sDataType_Full);
                if (strKey ==  ColKeyType.PK)
                {
                    //主键处理
                    strPK = " primary key (" + strColCode + ") \n";
                }
                #endregion

                #region 标志位Unsign
                if (strColUnsign.Equals("是"))
                {
                    sbSql.Append(" UNSIGNED ");
                }
                #endregion

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    sbSql.Append(sNotNull);
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
                    sbSql.Append(AddRightBand(" DEFAULT " + strColDefault + ""));
                }
                #endregion

                #region 自增长
                if (strColAutoAdd.Equals("是"))
                {
                    sbSql.Append(" AUTO_INCREMENT ");
                }
                #endregion

                #region 列备注的处理
                if(string.IsNullOrEmpty(strColRemark)) 
                {
                    //备注为空时
                    sbSql.Append(" COMMENT " + "'" + strColName  + "'");
                }
                else
                {
                    sbSql.Append(" COMMENT " + "'" + strColName + "：" + strColRemark + "'");
                }
                #endregion

                #region 增加逗号
                sbSql.Append("," + "" + ChangeIntoInfo(strColName) + "\n");
                #endregion

                #region 外键的处理
                if (strKey ==  ColKeyType.FK && !string.IsNullOrEmpty(strColForgKey) && strColForgKey.Contains("(") && strColForgKey.Contains(")"))
                {
                    //string strColDefaultName = "FK_" + strTable_Col;
                    //strUqueList += "ALTER TABLE " + strTableCode + " ADD CONSTRAINT " + strColForgKeyCode + " FOREIGN KEY (" + strColCode + ") REFERENCES " + strColForgKey + ";\n";
                }
                #endregion

                //计数器
                j--;
                #endregion
            }
            else
            {
                #region 修改表处理
                string strDefault_Full = "";//默认值

                StringBuilder sbColSql = new StringBuilder();//列的SQL                
                //生成删除列SQL脚本
                if (createType ==  SQLCreateType.Drop || createType ==  SQLCreateType.Drop_Create)
                {
                    if (strColumnDealType ==  ColumnChangeType.Create || strColumnDealType == ColumnChangeType.Drop_Create)
                    {
                        sbDelete.AppendLine("ALTER TABLE " + strTableCode + " DROP COLUMN " + strColCode + ";");
                    }
                    //对于删除，直接下一个字段
                    if (createType ==  SQLCreateType.Drop)
                    {
                        return;//continue;
                    }
                }
                //列SQL
                sbColSql.Append(sDataType_Full);

                #region 标志位Unsign
                if (strColUnsign.Equals("是"))
                {
                    sbSql.Append(" UNSIGNED ");
                }
                #endregion

                #region 默认值的处理
                if (!string.IsNullOrEmpty(strColDefault))
                {
                    //string strColDefaultName = "DF_" + strTableCode + "_" + strColCode;
                    if (strColDataType == "varchar" || strColDataType == "nvarchar" || strColDataType == "char")
                    {
                        //字符型处理
                        strDefault_Full = AddRightBand("default '" + strColDefault + "'");
                    }
                    else
                    {
                        //数值型处理
                        strDefault_Full = AddRightBand("default " + strColDefault.Replace("'", ""));
                    }
                    sbColSql.Append(strDefault_Full);
                }
                #endregion

                #region 非空的处理
                if (strColNoNull ==  YesNoType.Yes)
                {
                    sbColSql.Append(sNotNull);
                }
                else
                {
                    sbColSql.Append(sNull);
                }
                #endregion

                #region 自增长
                if (strColAutoAdd.Equals("是"))
                {
                    sbColSql.Append(" AUTO_INCREMENT ");
                }
                //备注
                sbColSql.Append(" COMMENT " + "'" + strColName + ":" + strColRemark + "';");
                #endregion

                if (strColumnDealType ==  ColumnChangeType.Create)
                {
                    //得到修改表增加列语句
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " ADD " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType ==  ColumnChangeType.Alter)
                {
                    sbSql.AppendLine("/*注：对修改字段，如要变更是否可空类型，则自己在最后加上NULL 或NOT NULL。对字段类型的变更，需要先清空该字段值或删除该列再新增*/");
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " MODIFY " + AddRightBand(strColCode) + sbColSql.ToString());
                }
                else if (strColumnDealType ==  ColumnChangeType.Drop)
                {
                    sbSql.AppendLine("ALTER TABLE " + strTableCode + " DROP COLUMN " + AddRightBand(strColCode) + ";");
                }
                else if (strColumnDealType ==  ColumnChangeType.Drop_Create)
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
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("getdate()", "now()");
                    break;
                case DataBaseType.Oracle:
                    //类型
                    sDbType = sDbType.ToLower().Replace("varchar2", "varchar").Replace("date", "datetime").Replace("number", "decimal").Replace("timestamp", "datetime");
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("sysdate", "now()");
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.SQLite:
                    //默认值
                    sDefaultValue = sDefaultValue.ToLower().Replace("(datetime('now','localtime'))", "now()");
                    break;
                case DataBaseType.PostgreSql:
                    //类型
                    sDbType = sDbType.ToLower().Replace("character varying", "varchar").Replace("date", "datetime");
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
        }

    }
}
