using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Breezee.WorkHelper.DBTool.UI
{
   /// <summary>
   /// SQL构造器
   /// </summary>
    public abstract class SQLBuilder
    {
        protected readonly string sNull = " NULL ";   //可空
        protected readonly string sNotNull = " NOT NULL "; //非空
        public static string RemarkSpliChar =  "："; //列名和备注的分隔符
        protected int iCalNum;
        protected bool _isAllConvert;
        protected ColumnTemplateType templateType;

        protected StringBuilder sbSqlFisrt;
        protected StringBuilder sbSql;
        protected StringBuilder sbRemark;
        protected StringBuilder sbDelete;

        protected SQLCreateType createType;
        protected DataBaseType importDBType;
        protected DataBaseType targetDBType;

        protected List<EntTable> entTables;
        protected List<EntCol> entCols;

        public string GenerateTableContruct(DataTable dtTable, DataTable dtCols, GenerateParamEntity paramEntity)
        {
            sbSqlFisrt = new StringBuilder();//SQL前缀
            sbSql = new StringBuilder();           

            this.createType = paramEntity.sqlCreateType;
            this.importDBType = paramEntity.importDBType;
            this.targetDBType = paramEntity.targetDBType;
            this._isAllConvert = paramEntity.isAllConvert;

            if (_isAllConvert)
            {
                templateType = ColumnTemplateType.AllInOne;
            }
            else
            {
                switch (importDBType)
                {
                    case DataBaseType.SqlServer:
                        templateType = ColumnTemplateType.SqlServer;
                        break;
                    case DataBaseType.Oracle:
                        templateType = ColumnTemplateType.Oracle;
                        break;
                    case DataBaseType.MySql:
                        templateType = ColumnTemplateType.MySql;
                        break;
                    case DataBaseType.SQLite:
                        templateType = ColumnTemplateType.SQLite;
                        break;
                    case DataBaseType.PostgreSql:
                        templateType = ColumnTemplateType.PostgreSql;
                        break;
                    default:
                        break;
                }
            }

            entTables = new List<EntTable>();
            entCols = new List<EntCol>();

            int iTable = 1;
            iCalNum = 1;

            #region Sql前缀处理
            sbSqlFisrt.Append("/***********************************************************************************\n");
            sbSqlFisrt.Append("* 脚本描述: 新增修改表\n");
            sbSqlFisrt.Append("* 创建作者: \n");
            sbSqlFisrt.Append("* 创建日期: " + DateTime.Now.ToShortDateString() + " \n");
            sbSqlFisrt.Append("* 使用模块：\n");
            sbSqlFisrt.Append("* 使用版本: \n");
            sbSqlFisrt.Append("* 说    明：\n");

            foreach (DataRow dr in dtTable.Rows)
            {
                EntTable ent = EntTable.GetEntity(dr);
                entTables.Add(ent);
                if (string.IsNullOrEmpty(ent.Name))
                {
                    ent.Name = paramEntity.defaultColNameCN + "表";
                }

                string sTableDealTypeName = ent.ChangeTypeEnum == TableChangeType.Create ? "新增" : "修改";
                sbSqlFisrt.Append("      " + iTable + " " + sTableDealTypeName + ent.Name + "（" + ent.Code + "）\n");
                iTable++;
            }
            sbSqlFisrt.Append("***********************************************************************************/\n");
            #endregion

            foreach (DataRow dr in dtCols.Rows)
            {
                entCols.Add(EntCol.GetEntity(dr,templateType));
            }

            sbDelete = new StringBuilder();
            foreach (EntTable entTable in entTables)
            {
                sbRemark = new StringBuilder();
                GenerateTableSQL(entTable, paramEntity);               
            }
            sbSqlFisrt.Append(sbDelete.ToString());
            sbSql.Insert(0, sbSqlFisrt.ToString());
            if (createType == SQLCreateType.Drop)
            {
                sbSql = new StringBuilder();
                sbSql.Append(sbDelete.ToString());
            }
            return sbSql.ToString();
        }

        private static readonly string _strBlank = " "; //空格

        #region 辅助方法
        /// <summary>
        /// 增加左边空格方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string AddLeftBand(string strColCode) => _strBlank + strColCode;

        /// <summary>
        /// 增加右边空格方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string AddRightBand(string strColCode)
        {
            return strColCode + _strBlank;
        }

        /// <summary>
        /// 增加左右边空格方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string AddLeftRightBand(string strColCode)
        {
            return _strBlank + strColCode + _strBlank;
        }

        /// <summary>
        /// 增加左右括号方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string AddLeftRightKuoHao(string strColCode)
        {
            return "(" + strColCode + ")";
        }

        /// <summary>
        /// 增加左右单引号方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string ChangeIntoSqlString(string strColCode)
        {
            return " '" + strColCode + "' ";
        }

        /// <summary>
        /// 返回注释说明
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        protected string ChangeIntoInfo(string strColCode)
        {
            return "/*" + strColCode + "*/";
        }

        protected string getFitRemark(string sColNameCn,string sRemark)
        {
            if (string.IsNullOrEmpty(sRemark))
            {
                return sColNameCn;
            }
            sRemark= sRemark.Trim();
            if (sColNameCn.Equals(sRemark, StringComparison.OrdinalIgnoreCase))
            {
                return sColNameCn.Trim();
            }

            if(sRemark.StartsWith(sColNameCn))
            {
                return sRemark;
            }
            else
            {
                return sColNameCn + RemarkSpliChar + sRemark;
            }
        }
        #endregion


        #region 获取全类型字符
        protected string GetFullTypeString(EntCol drCol, string strColDataType, string strColLen, string strColDecimalDigits)
        {
            string sDataType_Full;
            #region 类型的处理
            if (strColDataType.Contains("("))
            {
                #region 类型已经包括了长度或长度及小数点，需要修正类型、长度和小数位
                sDataType_Full = strColDataType;
                drCol.commonCol.DataType = strColDataType.Substring(0, strColDataType.IndexOf("("));
                if (strColDataType.Contains(","))//有小数位
                {
                    drCol.commonCol.DataLength= strColDataType.Substring(strColDataType.IndexOf("(") + 1, strColDataType.IndexOf(",") - strColDataType.IndexOf("(") - 1);
                    drCol.commonCol.DataDotLength= strColDataType.Substring(strColDataType.IndexOf(",") + 1, strColDataType.IndexOf(")") - strColDataType.IndexOf(",") - 1);
                }
                else
                {
                    drCol.commonCol.DataLength = strColDataType.Substring(strColDataType.IndexOf("(") + 1, strColDataType.IndexOf(")") - strColDataType.IndexOf("(") - 1);
                }
                #endregion
            }
            else
            {
                #region 类型不包括了长度或长度及小数点
                if (!string.IsNullOrEmpty(strColLen))
                {
                    if (!string.IsNullOrEmpty(strColDecimalDigits))
                    {
                        sDataType_Full = strColDataType + AddLeftRightKuoHao(strColLen + "," + strColDecimalDigits);
                    }
                    else
                    {
                        sDataType_Full = strColDataType + AddLeftRightKuoHao(strColLen);
                    }
                }
                else
                {
                    sDataType_Full = AddRightBand(strColDataType);
                }
                #endregion
            }
            #endregion

            //针对有些不需要长度的字符处理
            return sDataType_Full.TableColTypeNotNeedLenDeal(); 
        }
        #endregion

        /// <summary>
        /// 正则匹配替换
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="sMatchFormat"></param>
        /// <param name="sOld"></param>
        /// <param name="sNew"></param>
        /// <param name="bracketDealType">增加括号类型</param>
        public void MatchReplace(ref string sSql, string sMatchFormat, string sOld, string sNew, BracketDealType bracketDealType = BracketDealType.None)
        {
            Regex regex = new Regex(sMatchFormat, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sSql.ToString());
            if (bracketDealType == BracketDealType.Add)
            {
                sNew = sNew + "()";
            }
            string sMustColumnName = string.Empty;
            foreach (Match mt in mcColl)
            {
                //使用String.Replace方法默认是区分大小写的；所以这里使用Regex.Replace
                sMustColumnName = Regex.Replace(mt.Value, sOld, sNew, RegexOptions.IgnoreCase);
                if (bracketDealType== BracketDealType.Remove)
                {
                    sMustColumnName = sMustColumnName.Replace("(","").Replace(")", "").Trim();
                }
                sSql = sSql.Replace(mt.Value, sMustColumnName);
            }
        }

        /// <summary>
        /// 生成表SQL
        /// </summary>
        /// <param name="entTable"></param>
        /// <param name="paramEntity"></param>
        public abstract void GenerateTableSQL(EntTable entTable, GenerateParamEntity paramEntity);
        /// <summary>
        /// 转换数据类型和默认值
        /// </summary>
        /// <param name="sDbType"></param>
        /// <param name="sDefaultValue"></param>
        /// <param name="impDbType"></param>
        public abstract void ConvertDBTypeDefaultValueString(ref string sDbType, ref string sDefaultValue, DataBaseType impDbType);
        /// <summary>
        /// 转换为对应数据库类型的SQL
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <param name="targetDbType">目标数据库类型</param>
        public abstract void ConvertToDbSql(ref string sSql, DataBaseType targetDbType);
    }

    public enum BracketDealType
    {
        None = 0,
        Add=1,
        Remove=2
    }
}
