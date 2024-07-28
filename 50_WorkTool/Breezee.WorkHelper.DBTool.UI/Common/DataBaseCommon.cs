using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class DataBaseCommon
    {
        public static readonly string Null = " NULL";   //可空
        public static readonly string NotNull = " NOT NULL"; //非空
        //public static readonly string _strImportSuccessDealType = "0";//导入成功的功能处理类型
        //public static readonly string strTableAliasAndDot = "";
        public static readonly string Blank = " "; //空格
        public static readonly string Tab = "\t"; //制表键
        public static readonly string QuotationMark = "'";
        public static readonly string Enpty = ""; //空白
        public static readonly string Comma = ",";
        public static readonly string NewLine = System.Environment.NewLine;

        private static string strTableAlias = "A"; //查询和修改中的表别名

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

        #region 增加左边空格方法
        public static string AddLeftBand(string strColCode)
        {
            return Blank + strColCode;
        }
        #endregion

        #region 增加右边空格方法
        public static string AddRightBand(string strColCode)
        {
            if(string.IsNullOrEmpty(strColCode))
            {
                return "";
            }
            return strColCode + Blank;
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
            return Blank + strColCode + Blank;
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

    /// <summary>
    /// 参数格式类型
    /// </summary>
    public enum SqlParamFormatType
    {
        /// <summary>
        /// 前后#号
        /// </summary>
        BeginEndHash,
        /// <summary>
        /// SQL参数化
        /// </summary>
        SqlParm,
        /// <summary>
        /// MyBaits格式：#{}
        /// </summary>
        MyBatis,
        /// <summary>
        /// MyBaits动态列：#{param.}
        /// </summary>
        MyBatisDynamicColoumn,
        /// <summary>
        /// 自定义格式
        /// </summary>
        UserDefine
    }
}
