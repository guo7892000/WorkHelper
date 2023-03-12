using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

/***********************************************************
 * 对象名称：Sql辅助类
 * 对象类别：共用方法类
 * 创建作者：黄国辉
 * 创建日期：2014-9-3
 * 对象说明：主要提供配置文件的读取、SQL的解析
 * 修改历史：
 *      V1.0 新建 huanggh 2014-9-3
 * ********************************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// Sql辅助类
    /// </summary>
    public class ConfigHelper
    {
        #region 全局变量
        /// <summary>
        /// 所有配置文件信息字典集合
        /// </summary>
        public static IDictionary<string, string> dicAllConfig = null;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string sConfigPath = "";

        #endregion      

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigHelper()
        {
            InitConfigPath();
        }
        #endregion

        public static void InitConfigPath()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            sConfigPath = a.CodeBase.Substring(8, a.CodeBase.LastIndexOf(@"/") - 7);
            sConfigPath = sConfigPath.Replace(@"/", @"\");
        }

        #region 递归遍历xml文件中所有节点信息
        /// <summary>
        /// 递归遍历xml文件中所有节点信息
        /// </summary>
        /// <author>黄国辉</author>
        /// <param name="xn"></param>
        /// <param name="sXMLPath"></param>
        /// <param name="dicDMSConfig"></param>
        public static void XmlFileAllNodes(XmlNode xn, string sXMLPath, IDictionary<string, string> dicDMSConfig)
        {
            if (xn.Attributes == null)
            {
                dicDMSConfig[sXMLPath] = xn.Value; // 设置XPath节点下的值
            }
            else
            {
                sXMLPath += "/" + xn.Name; // 合成XPath
                dicDMSConfig[sXMLPath] = xn.Value;
                // 对于xml文件中的文件的二次递归遍历
                for (int i = 0; i < xn.Attributes.Count; i++)
                {
                    if (xn.Name.Trim().Equals(CFGIoC.MainCofig_IncludeFile_Node, StringComparison.OrdinalIgnoreCase) 
                        && xn.Attributes[i].Name.Trim().Equals(CFGIoC.MainCofig_IncludeFile_Node_Attribute, StringComparison.OrdinalIgnoreCase) )
                    {
                        string sFileNAme = xn.Attributes[i].Value;
                        string sPrex = "file://";
                        sFileNAme = sFileNAme.Substring(sPrex.Length, sFileNAme.Length - sPrex.Length);

                        XmlDataDocument dmsXml = new XmlDataDocument();
                        try
                        {
                            dmsXml.Load(sConfigPath + "Config\\" + sFileNAme);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + "  XMLPath:" + sConfigPath + "Config\\" + sFileNAme, ex);
                        }
                        string sXMLPath2 = dmsXml.DocumentElement.Name;

                        for (int j = 0; j < dmsXml.DocumentElement.ChildNodes.Count; j++)
                        {
                            XmlFileAllNodes(dmsXml.DocumentElement.ChildNodes[j], sXMLPath2, dicDMSConfig);
                        }
                    }
                }
            }

            string sValue = dicDMSConfig[sXMLPath];

            if (sValue != null)
            {
                sValue = sValue.Replace("&lt;", "<");// 对小于号的处理
                sValue = sValue.Replace("&gt;", ">"); // 对大于号的处理
                sValue = sValue.Replace("&amp;", "&"); // 对于符号的处理
                dicDMSConfig[sXMLPath] = sValue;
            }

            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                XmlFileAllNodes(xn.ChildNodes[i], sXMLPath, dicDMSConfig);
            }
        }
        #endregion

        #region 取得特定配置项
        /// <summary>
        /// 取得特定配置项
        /// </summary>
        /// <author>黄国辉</author>
        /// <param name="sXPath">配置文件的XPath路径</param>
        /// <returns>返回特定配置项</returns>
        public static string GetGlobalConfigInfo(string sXPath)
        {
            try
            {
                // 合成全局配置
                if (dicAllConfig == null)
                {
                    dicAllConfig = new Dictionary<string, string>();
                    XmlDataDocument dmsXml = new XmlDataDocument();
                    if(string.IsNullOrEmpty(sConfigPath))
                    {
                        InitConfigPath();
                    }
                    //以下为配置根文件路径，必须保证有以下文件
                    dmsXml.Load(sConfigPath + CFGIoC.SqlMainCofig_Path); //所有SQL的主配置文件
                    string sXMLPath = dmsXml.DocumentElement.Name;

                    for (int i = 0; i < dmsXml.DocumentElement.ChildNodes.Count; i++)
                    {
                        XmlFileAllNodes(dmsXml.DocumentElement.ChildNodes[i], sXMLPath, dicAllConfig);
                    }
                }

                if (dicAllConfig.ContainsKey(sXPath))
                {
                    if (dicAllConfig[sXPath] == null)
                    {
                        dicAllConfig[sXPath] = "";
                    }

                    return dicAllConfig[sXPath];
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取指定路径的SQL
        /// <summary>
        /// 获取指定路径的SQL
        /// </summary>
        /// <author>黄国辉</author>
        /// <param name="sXPath">配置文件XPath路径</param>
        /// <param name="dicKeyValue">非SQL关键字的参数字典集合</param>
        /// <param name="dicSqlKeyWord">SQL关键字的参数字典集合：后台赋值</param>
        /// <returns>替换参数后可查询的SQL</returns>
        public static string GetSqlByPath(string sXPath)
        {
            try
            {
                // 取得当前配置的sql语句
                return GetGlobalConfigInfo(sXPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SQL参数化转换
        /// <summary>
        /// SQL参数化转换
        /// </summary>
        /// <param name="sSql">要转换SQL，注必须以“#参数名#”格式表达要替换的参数。没有传入的参数的条件将被替换为1=1以过滤掉。
        /// 另外：注意等号是不支持模糊查询的，所以类似='%#参数#%'的这种写法是会导致解析有问题的。
        /// </param>
        /// <param name="dicQuery">键值需要跟SQL中的参数名保持一致</param>
        /// <param name="likeConnect">LIKE条件的连接字符，对SQLite和Oracle为||，SqlServer为+</param>
        /// <param name="sParaPreString">参数前缀,SQLite、SqlServer、MySql为@，Oracle和PostgreSql为:</param>
        /// <param name="dicParam">实际使用的参数字符字典，为后面查询创建DbParameter使用</param>
        /// <returns></returns>
        public static string SqlParamConvert(string sSql, IDictionary<string, string> dicQuery, string slikeConnect, string sParaPreString, out IDictionary<string, string> dicParam)
        {
            //先去掉SQL前后空白，并转换为大写
            sSql = sSql.Trim().ToUpper();
            //参数
            string sPoundSign = "#";
            string sRegxParam = @"#\w#";

            //查询条件相关
            string sRegxEquale = @"\s*(\w+.\w+|\w+)\s*(=|>|>=|<|<=|<>)\s*'?$"; //等号表达式，包括大于、小于、大于等于、小于等于、不等于
            string sRegxLike = @"\s*(\w+.\w+|\w+)\s*LIKE\s*'?%?$";//LIKE表达式
            string sRegxLeftBracket = @"\(\s*(\w+.\w+|\w+)\s*(LIKE|=|>|>=|<|<=|<>)\s*'?%?";//左括号表达式

            //更新语句相关
            string sUpdatePara = @"^UPDATE\s+\w+";//判断是更新语句的正则式：以UPDATE字符开头
            //更新语句第一个Set地方，支持别名.和没别名的。注意：不要包括逗号。这里不会有注释
            string sUpdateSetPara = @"\s+SET\s+(\w+.\w+|\w+)\s*=\s*'?";
            //一个更新字段的正则：注单引号（如有）和逗号在最前面要包括进来，方便最后一个参数为空时被替换掉。支持--和/**/的注释。
            string sUpdateColumnPara = @"'?\s*,\s*(--.*|/\*.*/*/)*\s*(\w*.\w+|\w+)\s*=\s*'?";
            bool isUpdateSql = false;

            //新增语句相关
            string sInsertPara = @"^INSERT\s+INTO\s+\w+";//判断是新增语句的正则式：以UPDATE字符开头
            //每一个新增参数项（不包括最后一个），支持--和/**/的注释。支持有没单号号。比最后的正则优先处理。
            string sInsertEveryPara = @"'?#\w+#'?\s*,\s*(--.*|(/\*.*/*/))*";
            //最后一个新增参数项（如果有的话），支持--和/**/的注释。支持有没单号号。必须放最后处理
            string sInsertLastPara = @",\s*(--.*|/\*.*/*/)*\s*'?#\w+#'?\s*(--.*|(/\*.*/*/))*";

            StringBuilder sbSql = new StringBuilder();

            Regex regex = new Regex(sRegxParam, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(sSql);
            List<string> listPara = new List<string>();
            dicParam = new Dictionary<string, string>();
            foreach (var item in mc)
            {
                if (!listPara.Contains(item.ToString()))
                {
                    listPara.Add(item.ToString());
                }
            }

            //判断新增SQL判断
            regex = new Regex(sInsertPara, RegexOptions.IgnoreCase);
            if (regex.Matches(sSql).Count > 0) //匹配新增正则
            {
                //每个新增参数处理（除最后一个）
                sSql = InsertAddColumnsAutoParam(sSql, dicQuery, sParaPreString, dicParam, sInsertEveryPara);
                //最后一个新增参数的处理
                sSql = InsertAddColumnsAutoParam(sSql, dicQuery, sParaPreString, dicParam, sInsertLastPara);
            }

            //分隔SQL，注这里要去掉前后空白字符，并转换为大写
            string[] sSqlSplit = sSql.Split(new string[] { sPoundSign }, StringSplitOptions.RemoveEmptyEntries);
            //判断更新语句
            regex = new Regex(sUpdatePara, RegexOptions.IgnoreCase);
            if (regex.Matches(sSqlSplit[0]).Count > 0) //匹配更新正则
            {
                isUpdateSql = true;
            }

            bool IsPreParaExists = false; //是否前一参数存在，实现以“%'”开头的处理
            int iSegmentIndex = 1; //0首行，1中间行，2尾行
            for (int i = 0; i < sSqlSplit.Length; i++)
            {
                sSqlSplit[i] = sSqlSplit[i].Trim();//移除前后空白
                if (i % 2 == 1)//参数列
                {
                    continue;
                }
                else
                {
                    if (i == sSqlSplit.Length - 1) //最后一行
                    {
                        //对头尾做处理
                        sbSql.Append(SqlSegmentBeginEndDeal(sSqlSplit[i], slikeConnect, true, IsPreParaExists, 2));
                        IsPreParaExists = true;
                        continue;
                    }

                    iSegmentIndex = 1;
                    if (i == 0) //第一行
                    {
                        iSegmentIndex = 0;
                    }

                    //判断参数是否存在：支持参数或#参数#
                    if ((dicQuery.ContainsKey(sSqlSplit[i + 1]) && !string.IsNullOrEmpty(dicQuery[sSqlSplit[i + 1]]))
                            || dicQuery.ContainsKey(sPoundSign + sSqlSplit[i + 1] + sPoundSign) && !string.IsNullOrEmpty(dicQuery[sPoundSign + sSqlSplit[i + 1] + sPoundSign]) )
                    {
                        //对头尾做处理
                        sbSql.Append(SqlSegmentBeginEndDeal(sSqlSplit[i], slikeConnect, true, IsPreParaExists, iSegmentIndex));
                        sbSql.Append(sParaPreString + sSqlSplit[i + 1]);//将参数也加进来
                        //有效条件集合处理
                        if (!dicParam.ContainsKey(sSqlSplit[i + 1]))
                        {
                            if (dicQuery.ContainsKey(sSqlSplit[i + 1]))
                            {
                                dicParam[sSqlSplit[i + 1]] = dicQuery[sSqlSplit[i + 1]];
                            }
                            else
                            {
                                dicParam[sSqlSplit[i + 1]] = dicQuery[sPoundSign + sSqlSplit[i + 1] + sPoundSign];
                            }
                        }
                        IsPreParaExists = true;
                    }
                    else //参数不存在
                    {

                        if (isUpdateSql) //是更新语句
                        {
                            sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], sUpdateSetPara, " SET ");//保留SET

                            sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], sUpdateColumnPara, " ");//清空

                        }
                        //等号匹配替换
                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "WHERE" + sRegxEquale, "WHERE 1=1 ");

                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "AND" + sRegxEquale, "AND 1=1 ");

                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "OR" + sRegxEquale, "AND 1=1 ");//注：这里要用AND，而不是OR 1=1，因为这样会查询全部记录。

                        //LIKE匹配替换
                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "WHERE" + sRegxLike, "WHERE 1=1 ");

                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "AND" + sRegxLike, "AND 1=1 ");

                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], "OR" + sRegxLike, "AND 1=1 ");//注：这里要用AND，而不是OR 1=1，因为这样会查询全部记录。

                        //左括号匹配替换
                        sSqlSplit[i] = GetParamSqlSegment(sSqlSplit[i], sRegxLeftBracket, "(1=1 ");

                        //对头尾做处理
                        sbSql.Append(SqlSegmentBeginEndDeal(sSqlSplit[i], slikeConnect, false, IsPreParaExists, iSegmentIndex));
                        IsPreParaExists = false;

                    }

                }

            }
            if (isUpdateSql) //是更新语句
            {
                //针对第一个更新参数为空时，多出来的逗号删除
                string sUpdateSetDHPara = @"\s+SET\s+,";
                regex = new Regex(sUpdateSetDHPara, RegexOptions.IgnoreCase);
                mc = regex.Matches(sbSql.ToString());

                foreach (var item in mc)
                {
                    sbSql.Replace(item.ToString(), " SET ");
                }
            }
            return sbSql.ToString();
        }

        /// <summary>
        /// 新增SQL的自动参数化处理
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="dicQuery"></param>
        /// <param name="sParaPreString"></param>
        /// <param name="dicParam"></param>
        /// <param name="sInsertEveryPara"></param>
        /// <returns></returns>
        private static string InsertAddColumnsAutoParam(string sSql, IDictionary<string, string> dicQuery, string sParaPreString, IDictionary<string, string> dicParam, string sInsertEveryPara)
        {
            Regex regexAdd = new Regex(sInsertEveryPara, RegexOptions.IgnoreCase);
            MatchCollection mcEveryAdd = regexAdd.Matches(sSql);
            foreach (var item in mcEveryAdd)
            {
                //有效条件集合处理
                string Para = item.ToString().Split(new char[] { '#' })[1];//取1为键
                //将符合正则中新增字段中的单引号和#号去掉，并加上@前缀
                sSql = sSql.Replace(item.ToString(), item.ToString().Replace("'#" + Para + "#'", sParaPreString + Para).Replace("#" + Para + "#", sParaPreString + Para));

                if (!dicParam.ContainsKey(Para))
                {
                    if (dicQuery.ContainsKey(Para))
                    {
                        dicParam[Para] = dicQuery[Para];
                    }
                    else if (dicQuery.ContainsKey("#" + Para + "#"))
                    {
                        dicParam[Para] = dicQuery["#" + Para + "#"];
                    }
                    else
                    {
                        throw new Exception(string.Format("新增SQL的参数{0}没有传入！", Para));
                    }
                }
            }

            return sSql;
        }
        #endregion

        #region 获取SQL片段
        /// <summary>
        /// 得到参数SQL片段
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="sRegexPattern"></param>
        /// <param name="sReplace"></param>
        /// <returns></returns>
        private static string GetParamSqlSegment(string sSql, string sRegexPattern, string sReplace)
        {
            Regex regex = new Regex(sRegexPattern, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(sSql);

            foreach (var item in mc)
            {
                sSql = sSql.Replace(item.ToString(), sReplace);
            }
            
            return sSql;
        }
        #endregion

        #region SQL片段的头尾处理
        /// <summary>
        /// SQL片段的头尾处理
        /// 注：因为参数后面还可能有其他运算，要一同去掉
        /// </summary>
        /// <param name="sSql">要处理的SQL片段</param>
        /// <param name="IsCurrentParaExists">当前参数是否存在</param>
        /// <param name="IsPreParaExists">前一个参数是否存在</param>
        /// <param name="iSegmentIndex">SQL片段索引号：0开头，1中间，2结尾</param>
        /// <returns></returns>
        private static string SqlSegmentBeginEndDeal(string sSql,string sConnect,bool IsCurrentParaExists,bool IsPreParaExists,int iSegmentIndex)
        {
            #region 开头字符处理(即将上个参数相关字符剔除)
            if (sSql.StartsWith("%'")) //模糊查询场景
            {
                if (IsPreParaExists)//前一参数存在
                {
                    sSql = sConnect + " '" + sSql;//将%'替换为+ '%',作为上一个参数的后模糊查询
                }
                else //前一参数不存在
                {
                    //获取下一个条件开始的索引号
                    int iMinIndex = GetNextConditionBeginIndex(sSql);

                    if (iMinIndex > 0) //下一个条件存在
                    {
                        sSql = " " + sSql.Substring(iMinIndex, sSql.Length - iMinIndex); //去掉之前的字符
                    }
                    else //下一个条件不存在，即最后一个条件了
                    {
                        //sSql = sSql.Substring(2, sSql.Length - 2); //去掉%'
                        sSql = " ";//清空
                    }
                }
            }
            else if (iSegmentIndex > 0)//非第一行
            {
                if (IsPreParaExists)//前一参数存在
                {
                    if (sSql.StartsWith("'"))//单引号处理
                    {
                        sSql = sSql.Substring(1);//直接去掉单引号
                    }
                }
                else
                {
                    //获取下一个条件开始的索引号
                    int iMinIndex = GetNextConditionBeginIndex(sSql);

                    if (iMinIndex > 0) //下一个条件存在
                    {
                        sSql = " " + sSql.Substring(iMinIndex, sSql.Length - iMinIndex); //去掉之前的字符
                    }
                    else //下一个条件不存在，即最后一个条件了
                    {
                        sSql = " ";//清空
                    }
                }
            }
            #endregion

            #region 结束字符处理（当前参数相关字符的处理）
            if (sSql.EndsWith("'%")) //模糊查询场景
            {
                if (IsCurrentParaExists)
                {
                    sSql = sSql.Substring(0, sSql.Length - 2) + " '%' " + sConnect;//将'%替换为'%' +,作为下一个参数的前模糊查询
                }
                else
                {
                    sSql = sSql.Substring(0, sSql.Length - 2); //去掉'%
                }
            }
            else if (sSql.EndsWith("'")) //UPDATE的更新语句和精确查询场景
            {
                sSql = sSql.Substring(0, sSql.Length - 1);//直接去掉单引号
            } 
            #endregion

            return sSql;
        }
        #endregion

        #region 获取下一个条件开始的索引号
        /// <summary>
        /// 取)、AND、OR这三个关键字中出现的最小的那个索引值。注意：要排除不存在的-1值
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        private static int GetNextConditionBeginIndex(string sSql)
        {
            //无法解决的问题Convert(DateTime,'#DATE_BEGIN#')，所以如要用函数转换，请先自行写成参数化。
            int khIndex = sSql.IndexOf(")", StringComparison.OrdinalIgnoreCase);
            int andIndex = sSql.IndexOf("AND",StringComparison.OrdinalIgnoreCase);
            int orIndex = sSql.IndexOf("OR", StringComparison.OrdinalIgnoreCase);
            int whereIndex = sSql.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase);
            int dhIndex = sSql.IndexOf(",", StringComparison.OrdinalIgnoreCase);

            int iMinIndex = -1;
            if (khIndex > 0) //有括号
            {
                iMinIndex = khIndex;
            }

            if (andIndex > 0)
            {
                if (iMinIndex > 0)
                {
                    iMinIndex = Math.Min(andIndex, iMinIndex);
                }
                else
                {
                    iMinIndex = andIndex;
                }
            }

            if (orIndex > 0)
            {
                if (iMinIndex > 0)
                {
                    iMinIndex = Math.Min(orIndex, iMinIndex);
                }
                else
                {
                    iMinIndex = orIndex;
                }
            }

            if (whereIndex > 0)
            {
                if (iMinIndex > 0)
                {
                    iMinIndex = Math.Min(whereIndex, iMinIndex);
                }
                else
                {
                    iMinIndex = whereIndex;
                }
            }

            if (dhIndex > 0)
            {
                if (iMinIndex > 0)
                {
                    iMinIndex = Math.Min(dhIndex, iMinIndex);
                }
                else
                {
                    iMinIndex = dhIndex;
                }
            }

            return iMinIndex;
        }
        #endregion
        
    }

    public class CFGIoC
    {
        #region 主要配置文件
        /// <summary>
        /// 主要配置文件路径
        /// </summary>
        public static readonly string MainCofig_Path = @"Config/IOC.Main.config";
        public static readonly string MainCofig_Root = "configuration";
        /// <summary>
        /// 相对根目录的数据库子配置路径
        /// </summary>
        public static readonly string MainCofig_Root_ChildDBConfig_Path = @"components/component";
        public static readonly string MainCofig_DBConfig_Attibute_ID = "id";
        public static readonly string MainCofig_DBConfig_Node_Para = "parameters";
        public static readonly string MainCofig_DBConfig_Node_Para_ConnectString = "sConstr";
        public static readonly string MainCofig_DBConfig_Node_Para_DbType = "sDBType";
        //包括的文件清单配置
        public static readonly string MainCofig_IncludeFile_Node = "include";
        public static readonly string MainCofig_IncludeFile_Node_Attribute = "uri";
        #endregion

        #region SQL配置文件相关
        public static readonly string SqlMainCofig_Path = @"Config/SQL.App.MIS.config";
        #endregion

    }

}
