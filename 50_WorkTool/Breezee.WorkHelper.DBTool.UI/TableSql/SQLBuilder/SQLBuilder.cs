using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
   /// <summary>
   /// SQL构造器
   /// </summary>
    public abstract class SQLBuilder
    {
        protected readonly string sNull = " NULL ";   //可空
        protected readonly string sNotNull = " NOT NULL "; //非空
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
                GenerateTableSQL(entTable);               
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

        public abstract void GenerateTableSQL(EntTable entTable);
        public abstract void ConvertDBTypeDefaultValueString(ref string sDbType, ref string sDefaultValue,DataBaseType impDbType);
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
    }
}
