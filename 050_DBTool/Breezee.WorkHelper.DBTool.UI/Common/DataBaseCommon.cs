using Breezee.Framework.Interface;
using Breezee.Framework.Tool;
using Breezee.Global.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class DataBaseCommon
    {
        private readonly string _strNull = " NULL";   //可空
        private readonly string _strNotNull = " NOT NULL"; //非空
        private string _strImportSuccessDealType = "0";//导入成功的功能处理类型
        private static string strTableAlias = "A"; //查询和修改中的表别名
        private static string strTableAliasAndDot = "";
        private static readonly string _strBlank = " "; //空格
        private static readonly string _strTab = "\t"; //制表键
        private static readonly string _strQuotationMark = "'";
        private static readonly string strEnpty = ""; //空白
        private static readonly string _strComma = ",";

        public static AutoClassInfo ClassInfo = new AutoClassInfo();


        #region 生成单条数据SQL
        /// <summary>
        /// 生成单条数据SQL
        /// </summary>
        /// <param name="dataBaseType">导入类型</param>
        /// <param name="strDataStyle">提交方式</param>
        /// <param name="strCommit">提交字符</param>
        /// <param name="strEnd">Orcale结束的字符串</param>
        /// <param name="strUpdateSQLOne">返回的字符</param>
        /// <param name="iDataNum">第几条数据</param>
        /// <param name="iCommitCount">第几次要加提交语句</param>
        /// <param name="iDataRowCount">记录数</param>
        /// <returns></returns>
        public static string GenOneDataSql(DataBaseType dataBaseType, string strDataStyle, string strCommit, string strEnd, string strUpdateSQLOne, int iDataNum, int iCommitCount, int iDataRowCount)
        {
            if (dataBaseType == DataBaseType.SqlServer)
            {
                if (strDataStyle != "0" && strDataStyle != "1")//非每行提交或最后一行提交
                {
                    if (iDataNum % iCommitCount == 0 || iDataNum == iDataRowCount)//每多少行或最后一行加上提交
                    {
                        strUpdateSQLOne = strUpdateSQLOne + strEnd +  "\n" + strCommit;
                    }
                    else
                    {
                        strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n";
                    }
                }
                else if (strDataStyle == "1")//每次提交
                {
                    strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n" + strCommit;
                }
                else
                {
                    strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n";
                }

                
            }
            else
            {
                if (strDataStyle != "0" && strDataStyle != "1")//非每行提交或最后一行提交
                {
                    if (iDataNum % iCommitCount == 0 || iDataNum == iDataRowCount)//每多少行或最后一行加上提交
                    {
                        strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n" + strCommit;
                    }
                    else
                    {
                        strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n";
                    }
                }
                else if (strDataStyle == "1")//每次提交
                {
                    strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n" + strCommit;
                }
                else
                {
                    strUpdateSQLOne = strUpdateSQLOne + strEnd + "\n";
                }

            }
            return strUpdateSQLOne;
        }
        #endregion

        #region 获取列的默认注释
        /// <summary>
        /// 获取列的默认注释：只有列说明为空时才获取
        /// </summary>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColComments">列目前的注释</param>
        /// <returns></returns>
        public static string GetColumnComment(string strColCode, string strColComments)
        {
            if (string.IsNullOrEmpty(strColComments))
            {
                switch (strColCode)
                {
                    case "IS_SYSTEM":
                        strColComments = "是否系统";
                        break;
                    case "ORDER_NO":
                        strColComments = "排序号";
                        break;
                    case "CREATOR":
                        strColComments = "创建人";
                        break;
                    case "REMARK":
                        strColComments = "备注";
                        break;
                    case "PART_BRAND_CODE":
                        strColComments = "备件品牌";
                        break;
                    case "CREATED_DATE":
                        strColComments = "创建时间";
                        break;
                    case "MODIFIER":
                        strColComments = "修改人";
                        break;
                    case "LAST_UPDATED_DATE":
                        strColComments = "最后更新时间";
                        break;
                    case "IS_ENABLE":
                        strColComments = "是否启用";
                        break;
                    case "SDP_USER_ID":
                        strColComments = "SDP用户ID";
                        break;
                    case "SDP_ORG_ID":
                        strColComments = "SDP组织ID";
                        break;
                    case "UPDATE_CONTROL_ID":
                        strColComments = "并发控制ID";
                        break;
                    default:
                        break;
                }
            }
            return strColComments;
        }
        #endregion

        #region 生成增删改查SQL方法
        /// <summary>
        /// 设置表说明
        /// </summary>
        /// <param name="strTableCode"></param>
        /// <param name="strColComments"></param>
        /// <returns></returns>
        public static string MakeTableComment(string strTableCode, string strColComments)
        {
            return AddLeftBand(strTableCode) + _strTab + "/*" + strColComments + "*/\n";
        }

        /// <summary>
        /// 设置查询列说明
        /// </summary>
        /// <param name="strComma">为逗号或空</param>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColComments">列说明</param>
        /// <returns></returns>
        public static string MakeQueryColumnComment(string strComma, string strColCode, string strColComments)
        {
            return _strTab + strColCode + strComma + _strTab + "/*" + strColComments + "*/\n";
        }

        /// <summary>
        /// 设置新增列值说明方法
        /// </summary>
        /// <param name="strComma">为逗号或空</param>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColValue">列值</param>
        /// <param name="strColComments">列说明</param>
        /// <param name="strColType">列数据类型</param>
        /// <returns></returns>
        public static string MakeAddValueColumnComment(string strComma, string strColCode, string strColValue, string strColComments, string strColType, bool isParmQuery)
        {
            string strColRemark = "/*" + strColCode + ":" + strColComments + "*/\n";
            string strFixedValue = "#" + strColCode + "#";
            if (isParmQuery)
            {
                strFixedValue = "@" + strColCode;
            }
            string strColRelValue = "";
            if (string.IsNullOrEmpty(strColValue))
            {
                //列没有默认值则加引号
                //列值为空时
                if (strColType == "DATE")
                {
                    //如果是Oracle的时间类型DATE，则需要将字符转为时间格式，要不会报“文字与字符串格式不匹配”错误
                    strColRelValue = "TO_DATE(" + _strQuotationMark + strFixedValue + _strQuotationMark + ",'YYYY-MM-DD')";
                    if (isParmQuery)
                    {
                        strColRelValue = "TO_DATE(" + strFixedValue + ",'YYYY-MM-DD')";
                    }
                }
                else
                {
                    strColRelValue = _strQuotationMark + strFixedValue + _strQuotationMark;
                    if (isParmQuery)
                    {
                        strColRelValue = strFixedValue;
                    }
                }
            }
            else
            {
                //列有默认值则不加引号
                strColRelValue = strColValue;
            }
            return _strTab + strColRelValue + strComma + _strTab + strColRemark;
        }

        /// <summary>
        /// 设置修改列值说明方法
        /// </summary>
        /// <param name="strComma">为逗号或空</param>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColValue">列值</param>
        /// <param name="strColComments">列说明</param>
        /// <param name="strColType">列数据类型</param>
        /// <returns></returns>
        public static string MakeUpdateColumnComment(string _strComma, string strColCode, string strColValue, string strColComments, string strColType, bool isParmQuery)
        {
            string strRemark = "\n";
            if (!string.IsNullOrEmpty(strColComments))
            {
                //修改列只显示备注，不显示列名
                strRemark = "/*" + strColComments + "*/\n";
            }
            string strColRelValue = "";
            if (string.IsNullOrEmpty(strColValue))
            {
                //列值为空时
                if (strColType == "DATE")
                {
                    //如果是Oracle的时间类型DATE，则需要将字符转为时间格式，要不会报“文字与字符串格式不匹配”错误
                    strColRelValue = "TO_DATE(" + _strQuotationMark + "#" + strColCode + "#" + _strQuotationMark + ",'YYYY-MM-DD')";
                    if (isParmQuery)
                    {
                        strColRelValue = "TO_DATE(@" + strColCode + ",'YYYY-MM-DD')";
                    }
                }
                else
                {
                    strColRelValue = _strQuotationMark + "#" + strColCode + "#" + _strQuotationMark;
                    if (isParmQuery)
                    {
                        strColRelValue = "@" + strColCode;
                    }
                }
            }
            else
            {
                //列值非空时
                if (isParmQuery)
                {
                    strColRelValue = strColValue;
                    if (!strColValue.Contains("@"))
                    {
                        strColRelValue = strColValue.Replace("#", "");
                    }
                }
                else
                {
                    strColRelValue = strColValue;
                }
            }
            return _strTab + strTableAliasAndDot + strColCode + "=" + strColRelValue + _strComma + _strTab + strRemark;
        }

        /// <summary>
        /// 设置条件列说明
        /// </summary>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColValue">列值</param>
        /// <param name="strColComments">列说明</param>
        /// <returns></returns>
        public static string MakeConditionColumnComment(string strColCode, string strColValue, string strColComments, bool isParmQuery,string sTableAliasAndDot)
        {
            string strRemark = "\n";
            if (!string.IsNullOrEmpty(strColComments))
            {
                strRemark = "/*" + strColComments + "*/\n";
            }
            if (string.IsNullOrEmpty(strColValue))
            {
                if (isParmQuery)
                {
                    return sTableAliasAndDot + strColCode + " = @" + strColCode + _strTab + strRemark;
                }
                //列值为空时，设置为：'#列编码#'
                return sTableAliasAndDot + strColCode + "=" + _strQuotationMark + "#" + strColCode + "#" + _strQuotationMark + _strTab + strRemark;
            }
            else
            {
                //有固定值时
                return sTableAliasAndDot + strColCode + "=" + strColValue + _strTab + strRemark;
            }
        }
        #endregion

        #region 增加左边空格方法
        public static string AddLeftBand(string strColCode)
        {
            return _strBlank + strColCode;
        }
        #endregion

        #region 增加右边空格方法
        public static string AddRightBand(string strColCode)
        {
            return strColCode + _strBlank;
        }
        #endregion

        #region 增加左右边空格方法
        /// <summary>
        /// 增加左右边空格方法
        /// </summary>
        /// <param name="strColCode"></param>
        /// <returns></returns>
        public static string AddLeftRightBand(string strColCode)
        {
            return _strBlank + strColCode + _strBlank;
        }
        #endregion

        #region 增加左右括号方法
        public static string AddLeftRightKuoHao(string strColCode)
        {
            return "(" + strColCode + ")";
        }
        #endregion

        #region 增加左右单引号方法
        public static string ChangeIntoSqlString(string strColCode)
        {
            return " '" + strColCode + "' ";
        }
        #endregion
    }

    public enum AutoSqlColumnSetType
    {
        /// <summary>
        /// 默认值设置
        /// </summary>
        Default = 1,

        /// <summary>
        /// 排除的字段
        /// </summary>
        Exclude = 0
    }

    public class AutoClassInfo
    {
        public string Author = string.Empty;
        public string ClassCode = string.Empty;
        public string ClassName = string.Empty;

    }
}
