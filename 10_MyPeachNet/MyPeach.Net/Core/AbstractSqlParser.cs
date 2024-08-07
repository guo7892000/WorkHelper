﻿using MyPeach.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace org.breezee.MyPeachNet
{
    /**
     * @objectName: SQL分析抽象类
     * @description: 作为SELECT、INSERT、UPDATE、DELETE分析器的父类，包含SQL的前置处理，如SQL大写、去掉注释、提取键等。
     *      注：在匹配的SQL中，不能修改原字符，不然根据mc.start()或mc.end()取出的子字符会不对!!
     * @author: guohui.huang
     * @email: guo7892000@126.com
     * @wechat: BreezeeHui
     * @date: 2022/4/12 21:52
     * @history:
     *   2023/07/20 BreezeeHui 增加非空参数无值时抛错。已解决LIKE的问题
     *   2023/07/29 BreezeeHui 增加WITH INSERT INTO SELECT 和INSERT INTO WITH SELECT的支持。
     *   2023/08/04 BreezeeHui 键设置增加优先使用配置项（F）的支持，即当一个键出现多次时，优先使用带有F配置的内容。
     *   2023/08/05 BreezeeHui 增加#号注释支持；修正/**\/注释的匹配与移除。
     *   2023/08/10 BreezeeHui 将移除注释抽成一个独立方法RemoveSqlRemark；增加SQL类型是否正确的抽象方法isRightSqlType。
     *   2023/08/13 BreezeeHui 增加注释中动态SQL的条件拼接；统一将参数转换为##形式，方便统一处理。增加MERGE INTO语句支持！
     *   2023/08/18 BreezeeHui 针对注释中动态SQL的条件拼接，在预获取条件参数时，把动态SQL中的键也加进去！
     *   2023/08/19 BreezeeHui 只有在非预获取条件参数，且传入条件为空时，才把默认值赋给传入条件值！
     *   2023/08/24 BreezeeHui 修正子查询或之后中有多个()转换错误问题；修正SELECT有#参数#时转换错误问题；修正WITH正则式。
     *   2023/08/25 BreezeeHui 将unionOrUnionAllConvert抽取到父类中，方便针对所有SELECT语句先做union或Union All分析。
     *   2023/08/30 BreezeeHui 增加对IN配置多少项（默认1000）后分拆成AND (XX IN ('值1','值2') OR XX IN ('值N1','值N2'))。
     */
    public abstract class AbstractSqlParser
    {
        protected MyPeachNetProperties myPeachProp;

        /**
         * 优先处理的括号会被替换的两边字符加中间一个序号值，例如：##1##
         * 如其跟键前缀重复，则会在后面增加一个#号
         *
         */
        private string parenthesesRoundKey = StaticConstants.parenthesesRoundKey;
        private string parenthesesRoundKeyPattern;
        protected string withSelectPartn;

        protected IDictionary<string, SqlKeyValueEntity> mapSqlKey;//SQL中所有键
        protected IDictionary<string, SqlKeyValueEntity> mapSqlKeyValid;//SQL中有传值的所有键

        protected IDictionary<string, string> mapsParentheses;//优先处理的括号集合
        public IDictionary<string, string> mapError;//错误信息IDictionary
        public IDictionary<string, object> ObjectQuery;
        public IDictionary<string, string> StringQuery;
        public IDictionary<string, string> ReplaceOrInCondition; //值替换或IN清单的条件，这些不需要参数化，需要从返回的条件中移除
        List<object> positionParamConditonList;//位置参数化SQL时查询条件对象数组

        protected SqlTypeEnum sqlTypeEnum;

        /*针对SqlServer的WITH... INSERT INTO... SELECT...，示例
         * with TMP_A AS(select 3 as id,'zhanshan3' as name)
         * INSERT INTO TEST_TABLE(ID,CNAME)
         * select * from TMP_A
        */
        protected string withInsertIntoSelectPartn;

        /*针对MySql、Oracle、SQLite、PostgerSQL的的INSERT INTO... WITH.. SELECT...，示例
         * with TMP_A AS(select 3 as id,'zhanshan3' as name)
         * INSERT INTO TEST_TABLE(ID,CNAME)
         * select * from TMP_A
        */
        protected string insertIntoWithSelectPartn;
        protected string insertIntoWithSelectPartnCommon;

        /***
         * 构造函数：初始化所有变量
         * @param prop 全局配置
         */
        public AbstractSqlParser(MyPeachNetProperties prop)
        {
            myPeachProp = prop;
            
            if (parenthesesRoundKey.equals(StaticConstants.HASH))
            {
                parenthesesRoundKey += StaticConstants.HASH;
            }
            parenthesesRoundKeyPattern = parenthesesRoundKey + "\\d+" + parenthesesRoundKey;
            //因为括号已被替换为##序号##，所以原正则式已不能使用："\\)?\\s*,?\\s*WITH\\s+\\w+\\s+AS\\s*\\("+commonSelectPattern;
            insertIntoWithSelectPartnCommon = @"\s*,?\s*(WITH)*\s+\w+\s+AS\s"; //注：这里的WITH只是第一个临时表有，后面的是没有的
            withSelectPartn = insertIntoWithSelectPartnCommon + "+" + parenthesesRoundKeyPattern;
            /*最终正则式：^\s*,?\s*WITH\s+\w+\s+AS\s*##\d+##\s*INSERT\s+INTO\s+\S+\s*##\d+##*/
            withInsertIntoSelectPartn = "^" + insertIntoWithSelectPartnCommon+"*" + parenthesesRoundKeyPattern +"\\s*"
                + StaticConstants.insertIntoPatternCommon + parenthesesRoundKeyPattern; //SqlServer使用
            /*最终正则式：^INSERT\s+INTO\s+\S+\s*\s*,?\s*WITH\s+\w+\s+AS\s*##\d+##*/
            insertIntoWithSelectPartn = StaticConstants.insertIntoPattern + insertIntoWithSelectPartnCommon + "*" + parenthesesRoundKeyPattern;

            mapsParentheses = new Dictionary<string, string>();
            mapSqlKey = new Dictionary<string, SqlKeyValueEntity>();
            mapSqlKeyValid = new Dictionary<string, SqlKeyValueEntity>();
            mapError = new Dictionary<string, string>();//并发容器-错误信息
            ReplaceOrInCondition = new Dictionary<string, string>();
            ObjectQuery = new Dictionary<string, object>();
            StringQuery = new Dictionary<string, string>();
            positionParamConditonList = new List<object>();
        }

        /// <summary>
        /// 预获取SQL参数（方便给参数赋值用于测试）
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public IDictionary<string, SqlKeyValueEntity> PreGetParam(string sSql, IDictionary<string, object> dic)
        {
            IDictionary<string, SqlKeyValueEntity> dicReturn = new Dictionary<string, SqlKeyValueEntity>();
            //条件键优化
            IDictionary<string, object> dicNew = conditionKeyOptimize(dic);
            //1、移除所有注释
            string sSqlNew = RemoveSqlRemark(sSql, dicNew,true);
            //2、获取SQL中的#参数#
            MatchCollection mc = ToolHelper.getMatcher(sSqlNew, StaticConstants.keyPatternHash);
            while (mc.find())
            {
                string sParamName = ToolHelper.getKeyName(mc.group(), myPeachProp);
                SqlKeyValueEntity param = SqlKeyValueEntity.build(mc.group(), new Dictionary<string, object>(), myPeachProp,true);
                if (!dicReturn.ContainsKey(sParamName))
                {
                    dicReturn[sParamName] = param;
                }
            }
            //将动态条件拼接SQL段的键加入，方便测试
            foreach (string sKey in dicNew.Keys)
            {
                if (sKey.startsWith(StaticConstants.dynConditionKeyPre))
                {
                    string sRealKey = sKey.replace(StaticConstants.dynConditionKeyPre, "");
                    if(!string.IsNullOrEmpty(sRealKey) && !dicReturn.ContainsKey(sRealKey))
                    {
                        SqlKeyValueEntity entity = new SqlKeyValueEntity();
                        entity.KeyName = sRealKey; //目前外部只用到一个键名
                        entity.KeyValue = dicNew[sKey];
                        entity.HasValue = true;
                        dicReturn.Add(sRealKey, entity);
                    }
                }
            }
            return dicReturn;
        }

        /// <summary>
        /// 条件键优化
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private  IDictionary<string, object> conditionKeyOptimize(IDictionary<string, object> dic)
        {
            //1、对传入的条件集合中的KEY进行优化：如去掉#号，如有：分隔，那么取第一个值作为键
            IDictionary<string, object> dicNew = new Dictionary<string, object>();
            foreach (string key in dic.Keys)
            {
                string sKeyNew = key.replace("#", "").replace("{", "").replace("}", "");
                sKeyNew = sKeyNew.Split(StaticConstants.keyBigTypeSpit)[0].trim(); //去掉前后空格
                dicNew.put(sKeyNew, dic.get(key));
            }
            return dicNew;
        }

        /**
         * 转换SQL（主入口方法）
         * @param sSql 要转换的SQL
         * @param dic SQL键配置的值
         * @return 返回转换结果
         */
        public ParserResult parse(string sSql, IDictionary<string, object> dic)
        {
            //1、 条件键优化
            IDictionary<string, object> dicNew = conditionKeyOptimize(dic);

            //2、移除所有注释
            sSql = RemoveSqlRemark(sSql, dicNew);

            //3、获取SQL所有参数信息
            getAllParamKey(sSql, dicNew);

            //3.1、当传入参数不符合，则直接返回退出
            ParserResult result;
            if (mapSqlKey.size() == 0)
            {
                result = ParserResult.success(sSql, mapSqlKey, ObjectQuery, StringQuery, positionParamConditonList);

                result.setMessage("SQL中没有发现键(键配置样式为：" + StaticConstants.HASH + "key" + StaticConstants.HASH + "或"
                    + StaticConstants.HASH_LEFT_BRACE + "key" + StaticConstants.RIGHT_BRACE + ")，已直接返回原SQL！");
                return result;
            }

            if (mapError.size() > 0)
            {
                return ParserResult.fail("部分非空键（" + string.Join(",", mapError.keySet()) + "）没有传入值，已退出！", mapError);
            }

            //4、得到符合左右括号正则式的内容，并替换为类似：##序号##格式，方便先从大方面分析结构，之后再取出括号里的内容来进一步分析
            string sNewSql = generateParenthesesKey(sSql);
            if (ToolHelper.IsNotNull(sNewSql))
            {
                sSql = sNewSql;
            }

            //5、转换处理：边拆边处理
            string sFinalSql = headSqlConvert(sSql);

            //在处理过程中，也会往mapError写入错误信息，所以这里如有错误，也返回出错信息
            if (mapError.size() > 0)
            {
                return ParserResult.fail("部分非空键没有传入值或其他错误，关联信息：" + string.Join(",", mapError.keySet()) + "，已退出！", mapError);
            }
            //6、返回最终结果
            if (sFinalSql.isEmpty())
            {
                return ParserResult.fail("转换失败，原因不明。", mapError);
            }

            //6.1、针对值替换以及IN清单，要从条件中移除，防止参数化报错
            foreach (string sKey in ReplaceOrInCondition.Keys)
            {
                mapSqlKeyValid.Remove(sKey);
                ObjectQuery.Remove(sKey);
                StringQuery.Remove(sKey);
            }
            
            result = ParserResult.success(sFinalSql, mapSqlKeyValid, ObjectQuery, StringQuery, positionParamConditonList);
            result.setSql(sFinalSql);
            result.setEntityQuery(mapSqlKeyValid);
            //6.2、输出SQL到控制台
            if (myPeachProp.isShowDebugSql())
            {
                Console.WriteLine(sFinalSql);
            }
            //6.3、如有设置SQL输出路径，那么也记录SQL到日志文件中。
            string sPath = myPeachProp.getLogSqlPath();
            if (!sPath.isEmpty())
            {
                string sLogFileName = "/sql." + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!sPath.startsWith("/") && sPath.IndexOf(":") == 0)
                {
                    sPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sPath;
                }
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }

                string sFileName = sPath + sLogFileName;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(sFinalSql);
                sb.AppendLine("*******************【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】**************************");
                sb.Append(Environment.NewLine);
                File.AppendAllText(sPath + sLogFileName, sb.ToString(), Encoding.UTF8);//追加所有文本
            }
            return result;
        }

        /// <summary>
        /// 获取所有参数键
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="dicNew"></param>
        private void getAllParamKey(string sSql, IDictionary<string, object> dicNew)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.keyPatternHash);
            int iLastEnd = 0;
            while (mc.find())
            {
                string sParamName = ToolHelper.getKeyName(mc.group(), myPeachProp);
                SqlKeyValueEntity param = SqlKeyValueEntity.build(mc.group(), dicNew, myPeachProp);
                //确定IN列字符
                fixedInColumnName(sSql, mc, iLastEnd, param);

                if (!mapSqlKey.ContainsKey(sParamName))
                {
                    mapSqlKey.put(sParamName, param);//参数不存在，直接添加
                }
                else
                {
                    if (param.getKeyMoreInfo().IsFirst)
                    {
                        mapSqlKey[sParamName] = param; //如是优先配置，那么替换原存在的配置对象
                    }
                }

                if (!mapSqlKeyValid.ContainsKey(sParamName) && param.isHasValue())
                {
                    mapSqlKeyValid.put(sParamName, param);//有传值的键
                    ObjectQuery.put(sParamName, param.KeyValue);
                    StringQuery[sParamName] = param.KeyValue.ToString();
                }
                if (!param.isHasValue() && param.getKeyMoreInfo().IsMust)
                {
                    mapError.put(sParamName, sParamName + "参数非空，但未传值！");//非空参数空值报错
                }

                if (ToolHelper.IsNotNull(param.getErrorMessage()))
                {
                    mapError.put(sParamName, param.getErrorMessage());//错误列表
                }

                if (param.KeyMoreInfo.MustValueReplace)
                {
                    ReplaceOrInCondition.Add(sParamName, sParamName); //要被替换或IN清单的条件键
                }

                //位置参数的条件值数组
                if (param.isHasValue() && !param.KeyMoreInfo.MustValueReplace)
                {
                    positionParamConditonList.Add(param.KeyValue);
                }
                iLastEnd = mc.end(); //上次参数位置
            }
        }

        /// <summary>
        /// 确实In列字符
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="mc"></param>
        /// <param name="iLastEnd"></param>
        /// <param name="param"></param>
        private static void fixedInColumnName(string sSql, MatchCollection mc, int iLastEnd, SqlKeyValueEntity param)
        {
            //确定InColumnName:20230829
            if (!string.IsNullOrEmpty(param.KeyMoreInfo.InString))
            {
                string sBeforeSql = sSql.substring(iLastEnd, mc.end());
                MatchCollection mcIn = ToolHelper.getMatcher(sBeforeSql, StaticConstants.inPattern);
                while (mcIn.find())
                {
                    string sIncudeColumnName = mcIn.group().trim();
                    MatchCollection mcColunIn = ToolHelper.getMatcher(sIncudeColumnName, @"(WHERE|AND|OR)\s*");
                    if (mcColunIn.find())
                    {
                        sIncudeColumnName = sIncudeColumnName.replace(mcColunIn.group(), "").trim();
                    }
                    mcColunIn = ToolHelper.getMatcher(sIncudeColumnName, @"\s+IN");
                    if (mcColunIn.find())
                    {
                        sIncudeColumnName = sIncudeColumnName.replace(mcColunIn.group(), "").trim();
                    }

                    int iLeft = 0;
                    int iRight = 0;
                    while (sIncudeColumnName.startsWith("("))
                    {
                        foreach (char oneChar in sIncudeColumnName.ToCharArray())
                        {
                            if (oneChar.Equals('(')) iLeft++;
                            if (oneChar.Equals(')')) iRight++;
                        }
                        if (iLeft > iRight)
                        {
                            sIncudeColumnName = sIncudeColumnName.substring(1);
                            iLeft--;
                        }
                    }
                    param.getKeyMoreInfo().InColumnName = sIncudeColumnName;
                }
            }
        }

        /// <summary>
        /// SQL预优化
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        private string sqlPreOptimize(string sSql)
        {
            //去掉前后空格
            string sNoConditionSql = sSql;
            //将#{}的参数，转换为##形式，方便后面统一处理
            MatchCollection mc = ToolHelper.getMatcher(sNoConditionSql, StaticConstants.keyPatternHashLeftBrace);
            while (mc.find())
            {
                string sNewParam = mc.group().replace("#{", "#").replace("}", "#");
                sSql = sSql.replace(mc.group(), sNewParam);
            }
            return sSql;
        }

        /// <summary>
        /// 移除SQL注释方法
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="dic"></param>
        /// <param name="isPreGetCondition">是否预获取参数</param>
        /// <returns></returns>
        public string RemoveSqlRemark(string sSql, IDictionary<string, object> dic,bool isPreGetCondition=false)
        {
            //1、预处理
            //1.1 去掉前后空字符：注这里不要转换为大写，因为有些条件里有字母值，如转换为大写，则会使条件失效！！
            sSql = sSql.trim(); //.toUpperCase();//将SQL转换为大写

            //1.2 将参数中的#{}，转换为##，方便后续统一处理
            sSql = sqlPreOptimize(sSql);

            //2、删除所有注释，降低分析难度，提高准确性
            //2.1 先去掉--的单行注释
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.remarkPatterSingle2Reduce);//Pattern：explanatory note
            while (mc.find())
            {
                sSql = sSql.replace(mc.group(), "");//删除所有注释
            }

            //2.2 先去掉/***\/的多行注释：因为多行注释不好用正则匹配，所以其就要像左右括号一样，单独分析匹配
            sSql = removeMultiLineRemark(sSql, dic, isPreGetCondition);
            //参数#改为*后的SQL
            string sNoConditionSql = sSql;
            mc = ToolHelper.getMatcher(sSql, StaticConstants.keyPatternHash);
            while (mc.find())
            {
                //先将#号替换为*，防止跟原注释冲突。注：字符数量还是跟原SQL一样！
                string sNewParam = mc.group().replace("#", "*");
                sNoConditionSql = sNoConditionSql.replace(mc.group(), sNewParam); //将参数替换为新字符
            }

            //2.3、移除#开头的单行注释
            mc = ToolHelper.getMatcher(sNoConditionSql, StaticConstants.remarkPatterSingleHash);
            StringBuilder sbNoRemark = new StringBuilder();
            int iGroupStart = 0;//组开始的位置
            bool isHasHashRemark = false;
            while (mc.find())
            {
                sbNoRemark.append(sSql.substring(iGroupStart, mc.start()));
                iGroupStart = mc.end();
                isHasHashRemark = true;
            }
            if (iGroupStart > 0)
            {
                sbNoRemark.append(sSql.substring(iGroupStart)); //最后的字符
            }
            if (isHasHashRemark)
            {
                sSql = sbNoRemark.toString();
            }
            return sSql;
        }

        /**
        * 转换重载方法
        * @param sSql
        * @param dic
        * @param targetSqlParamTypeEnum
        * @return
        */
        public ParserResult parse(string sSql, IDictionary<String, Object> dic, TargetSqlParamTypeEnum targetSqlParamTypeEnum)
        {
            TargetSqlParamTypeEnum oldParamTypeEnum = myPeachProp.getTargetSqlParamTypeEnum();
            if (targetSqlParamTypeEnum == oldParamTypeEnum)
            {
                return parse(sSql, dic);
            }
            myPeachProp.TargetSqlParamTypeEnum = targetSqlParamTypeEnum;//设置为新的目标SQL类型
            ParserResult result = parse(sSql, dic);
            myPeachProp.TargetSqlParamTypeEnum = oldParamTypeEnum;//还原为旧的目标SQL类型
            return result;
        }

        /**
         * 移除多行注释
         * 目的：为了简化SQL分析
         * @param sSql
         * @return
         */
        protected string removeMultiLineRemark(string sSql, IDictionary<string, object> dic, bool isPreGetCondition=false)
        {
            MatchCollection mc;
            StringBuilder sb = new StringBuilder();
            mc = ToolHelper.getMatcher(sSql, StaticConstants.remarkPatterMultiLine);
            //int iGroup = 0;//第几组括号
            int iLeft = 0;//左注释数
            int iRight = 0;//右注释数
            int iGroupStart = 0;//组开始的位置

            int iRemarkBegin = 0;//注释开始的地方
            string sOneRemarkSql = "";

            //增加动态SQl语句的处理

            while (mc.find())
            {
                if ("/*".equals(mc.group()))
                {
                    iLeft++;
                    if (iLeft == 1)
                    {
                        string sNowStr = sSql.substring(iGroupStart, mc.start());
                        if (!string.IsNullOrWhiteSpace(sNowStr))
                        {
                            sb.append(sNowStr);//注：不要包括左括号
                        }
                        iGroupStart = mc.end();
                        iRemarkBegin=mc.start();
                    }
                }
                else
                {
                    iRight++;
                }
                //判断是否是一组数据
                if (iLeft == iRight)
                {
                    iGroupStart = mc.end();//下一个语句的开始
                    sOneRemarkSql = sSql.substring(iRemarkBegin, iGroupStart).trim();
                    int iLen = SqlKeyConfig.dynamicSqlRemarkFlagString.Length;
                    int iStart = sOneRemarkSql.IndexOf(SqlKeyConfig.dynamicSqlRemarkFlagString);
                    int iEnd = sOneRemarkSql.LastIndexOf(SqlKeyConfig.dynamicSqlRemarkFlagString);
                    if (iStart > -1 && iEnd>-1)
                    {
                        //包含动态SQL标志
                        sOneRemarkSql = sOneRemarkSql.substring(iStart+ iLen, iEnd).trim();
                        sOneRemarkSql = getDynamicSql(dic, sOneRemarkSql, isPreGetCondition);
                        sb.append(sOneRemarkSql.trim());//加入动态部分的SQL
                    }

                    iLeft = 0;
                    iRight = 0;
                }
            }
            //最后的字符
            if (iGroupStart > 0)
            {
                sb.append(sSql.substring(iGroupStart).trim());
                return sb.toString().trim();
            }
            //没有注释时，直接返回原SQL
            return sSql;
        }

        /// <summary>
        /// 获取动态SQL
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="sOneRemarkSql"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string getDynamicSql(IDictionary<string, object> dic, string sOneRemarkSql, bool isPreGetCondition=false)
        {
            try
            {
                MatchCollection mc = ToolHelper.getMatcher(sOneRemarkSql, StaticConstants.dynSqlSegmentConfigPatternCenter);
                if (mc.find())
                {
                    string sCond = sOneRemarkSql.substring(0,mc.start());
                    string sDynSql = sOneRemarkSql.substring(mc.end());

                    mc = ToolHelper.getMatcher(sCond, StaticConstants.dynSqlSegmentConfigPatternLeft);
                    if (mc.find())
                    {
                        sCond = sCond.substring(mc.end()).trim();
                    }

                    mc = ToolHelper.getMatcher(sDynSql, StaticConstants.dynSqlSegmentConfigPatternRight);
                    if (mc.find())
                    {
                        sDynSql = sDynSql.substring(0,mc.start()).trim();
                    }

                    int iFinStart = -1;
                    string sOperateStr = "";
                    //增加IN和NOT IN 支持
                    mc = ToolHelper.getMatcher(sCond, StaticConstants.dynSqlSegmentNotInPattern);
                    if (mc.find())
                    {
                        return dynSqlSegmentInOrNotConditionEqual(dic, isPreGetCondition, mc, sCond, sDynSql,true);
                    }
                    mc = ToolHelper.getMatcher(sCond, StaticConstants.dynSqlSegmentInPattern);
                    if (mc.find())
                    {
                        return dynSqlSegmentInOrNotConditionEqual(dic, isPreGetCondition, mc, sCond, sDynSql, false);
                    }

                    if (sCond.IndexOf(">=") > 0)
                    {
                        //大于等于：使用整型比较
                        sOperateStr = ">=";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            int iCondValue = int.Parse(dic[sKey].ToString());
                            int iSqlValue = int.Parse(sValue);
                            return (iCondValue.CompareTo(iSqlValue) >= 0) ? sDynSql : "";
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf("<=") > 0)
                    {
                        //小于等于：使用整型比较
                        sOperateStr = "<=";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            int iCondValue = int.Parse(dic[sKey].ToString());
                            int iSqlValue = int.Parse(sValue);
                            return (iCondValue.CompareTo(iSqlValue) <= 0) ? sDynSql : "";
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf("<") > 0)
                    {
                        //小于：使用整型比较
                        sOperateStr = "<";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            int iCondValue = int.Parse(dic[sKey].ToString());
                            int iSqlValue = int.Parse(sValue);
                            return (iCondValue.CompareTo(iSqlValue) < 0) ? sDynSql : "";
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf(">") > 0)
                    {
                        //大于：使用整型比较
                        sOperateStr = ">";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            int iCondValue = int.Parse(dic[sKey].ToString());
                            int iSqlValue = int.Parse(sValue);
                            return (iCondValue.CompareTo(iSqlValue) > 0) ? sDynSql : "";
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf("=") > 0)
                    {
                        //等于：使用字符比较
                        sOperateStr = "=";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            return sValue.equals(dic[sKey].ToString()) ? sDynSql : "";
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf("!=") > 0)
                    {
                        //不等于：使用字符比较
                        sOperateStr = "!=";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            return sValue.equals(dic[sKey].ToString()) ? "" : sDynSql;
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else if (sCond.IndexOf("<>") > 0)
                    {
                        //不等于：使用字符比较
                        sOperateStr = "<>";
                        iFinStart = sCond.IndexOf(sOperateStr);
                        string sKey = sCond.substring(0, iFinStart).trim();
                        string sValue = sCond.substring(iFinStart + sOperateStr.Length).trim();
                        if (dic.ContainsKey(sKey))
                        {
                            return sValue.equals(dic[sKey].ToString()) ? "" : sDynSql;
                        }
                        if (isPreGetCondition && !dic.ContainsKey(sKey))
                        {
                            dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
                        }
                    }
                    else
                    {
                        throw new Exception("不支持的动态SQl操作符，只能使用>=、>、<=、<、=、！=、<>这几种单值比较符！原始字符：" + sCond);
                    }
                }
                return "";
            }
            catch(Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 动态SQL段的In或Not IN判断
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="isPreGetCondition"></param>
        /// <param name="mc"></param>
        /// <param name="sCond"></param>
        /// <param name="sDynSql"></param>
        /// <param name="isNotIn"></param>
        /// <returns></returns>
        private static string dynSqlSegmentInOrNotConditionEqual(IDictionary<string, object> dic, bool isPreGetCondition, MatchCollection mc, string sCond, string sDynSql,bool isNotIn)
        {
            string sKey = sCond.substring(0, mc.start()).trim();
            string sValue = sCond.substring(mc.end()).replace("(", "").replace(")", "").replace("'", "");
            if (dic.ContainsKey(sKey))
            {
                string[] arrNotIn = sValue.split(",");
                foreach (string item in arrNotIn)
                {
                    if (item.equals(dic[sKey].ToString()))
                    {
                        return isNotIn ? "":sDynSql; //找到了：针对NOT IN返回空；针对IN返回动态SQL
                    }
                }
                return isNotIn ? sDynSql: ""; //没找到：针对NOT IN返回动态SQL；针对IN返回空
            }
            if (isPreGetCondition && !dic.ContainsKey(sKey))
            {
                dic.Add(StaticConstants.dynConditionKeyPre + sKey, sValue);//加上前缀，是为了更好区分这是注释里的动态键
            }
            return "";
        }

        /**
         * 生成包括##序号##键方法
         * 目的：为了简化大方面的分析
         * @param sSql
         * @return
         */
        public string generateParenthesesKey(string sSql)
        {
            MatchCollection mc;
            StringBuilder sb = new StringBuilder();
            mc = ToolHelper.getMatcher(sSql, StaticConstants.parenthesesPattern);
            //int iGroup = 0;//第几组括号
            int iLeft = 0;//左括号数
            int iRight = 0;//右括号数
            int iGroupStart = 0;//组开始的位置

            while (mc.find())
            {
                if ("(".equals(mc.group()))
                {
                    iLeft++;
                    if (iLeft == 1)
                    {
                        sb.append(sSql.substring(iGroupStart, mc.start()));//注：不要包括左括号
                        iGroupStart = mc.end();
                    }
                }
                else
                {
                    iRight++;
                }
                //判断是否是一组数据
                if (iLeft == iRight)
                {
                    string sKey = parenthesesRoundKey + mapsParentheses.size() + parenthesesRoundKey;
                    //符合左右括号正则式的内容，替换为：##序号##。把最外面的左右括号也放进去
                    string sParenthesesSql = "(" + sSql.substring(iGroupStart, mc.start()) + mc.group();
                    mapsParentheses.put(sKey, sParenthesesSql);
                    sb.append(sKey);//注：不要包括右括号
                    iGroupStart = mc.end();//下一个语句的开始

                    iLeft = 0;
                    iRight = 0;
                }
            }
            //最后的字符
            if (iGroupStart > 0)
            {
                sb.append(sSql.substring(iGroupStart));
            }

            string sNewSql = sb.toString();
            if (ToolHelper.IsNull(sNewSql))
            {
                //没有括号时，调用子查询方法
                return queryHeadSqlConvert(sSql, true);
            }
            return sNewSql;
        }

        /***
         * FROM段SQL的转换（包括WHERE部分）
         * @param sSql
         * @param childQuery 是否子查询
         */
        protected string fromWhereSqlConvert(string sSql, bool childQuery)
        {
            StringBuilder sb = new StringBuilder();
            string sSet = "";
            string sFromWhere = "";

            //分隔FROM段
            MatchCollection mcFrom = ToolHelper.getMatcher(sSql, StaticConstants.fromPattern);
            //因为只会有一个FROM，所以这里不用WHILE，而使用if
            if (!mcFrom.find())
            {
                //1。没有FROM语句
                string sFinalWhere = whereConvert(sSql);
                sb.append(sFinalWhere);
                return sb.toString();
            }

            //2.有FROM，及之后可能存在的WHERE段处理
            sSet = sSql.substring(0, mcFrom.start()).trim();
            sFromWhere = sSql.substring(mcFrom.end()).trim();

            //1、查询语句中查询的字段，或更新语句中的更新项
            if (childQuery)
            {
                string sFinalBeforeFrom = queryBeforeFromConvert(sSet);
                sb.append(sFinalBeforeFrom);//由子类来处理
            }
            else
            {
                string sFinalBeforeFrom = beforeFromConvert(sSet);
                sb.append(sFinalBeforeFrom);//由子类来处理
            }

            sb.append(mcFrom.group());//sbHead添加FROM字符

            //2、WHERE段分隔
            MatchCollection mcWhere = ToolHelper.getMatcher(sFromWhere, StaticConstants.wherePattern);
            string sFrom;//from段
            string sWhere;//where段
            //因为只会有一个FROM，所以这里不用WHILE，而使用if
            if (!mcWhere.find())
            {
                //没有WHERE段：但后面可能有GROUP BY或ORDER BY或LIMI等项，需要进一步匹配，从而确定FROM段和FROM段字符
                //GROUP BY的处理
                MatchCollection mcGroupBy = ToolHelper.getMatcher(sSql, StaticConstants.groupByPattern);
                if (mcGroupBy.find())
                {
                    sFrom = sSql.substring(mcFrom.end(), mcGroupBy.start());
                    sWhere = mcGroupBy.group() + sSql.substring(mcGroupBy.end());
                }
                else
                {
                    //ORDER BY的处理
                    MatchCollection mcOrder = ToolHelper.getMatcher(sSql, StaticConstants.orderByPattern);
                    if (mcOrder.find())
                    {
                        sFrom = sSql.substring(mcFrom.end(), mcOrder.start());
                        sWhere = mcOrder.group() + sSql.substring(mcOrder.end());
                    }
                    else
                    {
                        //LIMIT的处理
                        MatchCollection mcLimit = ToolHelper.getMatcher(sSql, StaticConstants.limitPattern);
                        if (mcLimit.find())
                        {
                            sFrom = sSql.substring(mcFrom.end(), mcLimit.start());
                            sWhere = mcLimit.group() + sSql.substring(mcLimit.end());
                        }
                        else
                        {
                            //没有GROUP BY、ORDER BY、LIMIT，那么就相当于只有FROM段的内容
                            sFrom = sSql.substring(mcFrom.end());
                            sWhere = "";
                        }
                    }
                }
            }
            else
            {
                sFrom = sFromWhere.substring(0, mcWhere.start());//
                sWhere = sFromWhere.substring(mcWhere.end() - mcWhere.group().length());
            }

            //3、FROM段的处理
            if (!hasKey(sFrom))
            {
                //FROM段没有参数时，直接拼接
                sb.append(sFrom);
                string sFinalWhere = whereConvert(sWhere);
                sb.append(sFinalWhere);

                return sb.toString();
            }

            //4 通过各种Join正则式分解语句
            MatchCollection mc2 = ToolHelper.getMatcher(sFrom, StaticConstants.joinPattern);
            int iStart2 = 0;
            int iCount = 0;
            string lastJoin = "";//最后一次JOIN语句的字符，这个在while循环外处理最后一段字符时用到
            while (mc2.find())
            {
                string oneJoin = sFrom.substring(iStart2, mc2.start());//第一条JOIN语句
                if (iCount > 0)
                {
                    sb.append(lastJoin);
                }
                lastJoin = mc2.group();
                iCount++;
                if (!hasKey(oneJoin))
                {
                    //没有参数，直接拼接
                    sb.append(oneJoin);
                    //sbHead.append(mc2.group());
                    iStart2 = mc2.end();
                    continue;//继续下一段处理
                }
                //AND和OR的条件转换
                string sAndOr = andOrConditionConvert(oneJoin);
                sb.append(sAndOr);
                iStart2 = mc2.end();

            }
            sb.append(lastJoin);
            //5 之前正则式中最后一段SQL的AND和OR的条件转换
            string sLastFrom = andOrConditionConvert(sFrom.substring(iStart2));
            sb.append(sLastFrom);

            //6.WHERE段的SQL处理
            string sConvertWhere = whereConvert(sWhere);
            sb.append(sConvertWhere);

            return sb.toString();
        }

        /**
         * where语句的转换
         * @param sSql
         * @return
         */
        private string whereConvert(string sSql)
        {
            StringBuilder sb = new StringBuilder();
            //二、 如果语句中没有FROM语句，那会直接进入
            MatchCollection mcWhere = ToolHelper.getMatcher(sSql, StaticConstants.wherePattern);
            if (!mcWhere.find())
            {
                //没有Where，那就是直接SELECT部分
                if (!hasKey(sSql))
                {
                    return sSql; //没有参数时直接返回
                }
                //有键
                string[] keyList = sSql.split(",");
                int iCount = 0;
                foreach (string item in keyList)
                {
                    string sValue = iCount == 0 ? "" : ",";
                    sb.append(sValue + singleKeyConvert(item));
                    iCount++;
                }
                return sb.ToString();
            }

            //sb.append(sSql.substring(0,mcWhere.start()));//确定FROM部分
            string sWhereString = mcWhere.group();

            //7.GROUP BY的处理
            //6、拆出WHERE至GROUP BY之间的片段
            bool needWhereSplit = true;//是否需要做WHERE分拆
            bool needGroupBySplit = false;//是否需要做GroupBy分拆
            bool needHavingSplit = false;//是否需要做GroupBy分拆
            MatchCollection mcGroupBy = ToolHelper.getMatcher(sSql, StaticConstants.groupByPattern);
            if (mcGroupBy.find())
            {
                needGroupBySplit = true;
                //2.1 AND和OR的条件转换
                string OneSql = sSql.substring(mcWhere.end(), mcGroupBy.start());
                string sConvertWHere = andOrConditionConvert(OneSql);
                if (ToolHelper.IsNotNull(sConvertWHere))
                {
                    sb.append(sWhereString + sConvertWHere);
                }

                sb.append(mcGroupBy.group());
                sSql = sSql.substring(mcGroupBy.end());
                if (!hasKey(sSql))
                {
                    //之后都没有key配置，那么直接将字符加到尾部，然后返回
                    sb.append(sSql);
                    return sb.toString();
                }

                needWhereSplit = false;
                MatchCollection mcHaving = ToolHelper.getMatcher(sSql, StaticConstants.havingPattern);
                if (mcHaving.find())
                {
                    needGroupBySplit = false;
                    string sOne = sSql.substring(0, mcHaving.start());
                    sOne = andOrConditionConvert(sOne);
                    sb.append(sOne);
                    sb.append(mcHaving.group());

                    sSql = sSql.substring(mcHaving.end());
                    needHavingSplit = true;
                }
            }

            //7、拆出ORDER片段
            bool needOrderSplit = false;
            MatchCollection mcOrder = ToolHelper.getMatcher(sSql, StaticConstants.orderByPattern);
            if (mcOrder.find())
            {
                if (needWhereSplit)
                {
                    string sConvertWHere = andOrConditionConvert(sSql.substring(mcWhere.end(), mcOrder.start()));
                    if (ToolHelper.IsNotNull(sConvertWHere))
                    {
                        sb.append(sWhereString + sConvertWHere);
                    }
                    sb.append(mcOrder.group());
                    sSql = sSql.substring(mcOrder.end());
                    needWhereSplit = false;
                    if (!hasKey(sSql))
                    {
                        //之后都没有key配置，那么直接将字符加到尾部，然后返回
                        sb.append(sSql);
                        return sb.toString();
                    }
                }
                if (needGroupBySplit)
                {
                    string sAndOr = andOrConditionConvert(sSql.substring(0, mcOrder.start()));
                    sb.append(sAndOr);
                    needGroupBySplit = false;
                }
                if (needHavingSplit)
                {
                    string sAndOr = andOrConditionConvert(sSql.substring(0, mcOrder.start()));
                    sb.append(sAndOr);
                    needHavingSplit = false;
                }
                sb.append(mcOrder.group());
                sSql = sSql.substring(mcOrder.end());
                needOrderSplit = true;
            }

            //8、拆出LIMIT段
            MatchCollection mcLimit = ToolHelper.getMatcher(sSql, StaticConstants.limitPattern);
            if (mcLimit.find())
            {
                if (needWhereSplit)
                {
                    string sConvertWHere = andOrConditionConvert(sSql.substring(0, mcLimit.start()));
                    if (ToolHelper.IsNotNull(sConvertWHere))
                    {
                        sb.append(sWhereString + sConvertWHere);
                    }
                }
                if (needGroupBySplit || needHavingSplit || needOrderSplit)
                {
                    string sAndOr = andOrConditionConvert(sSql.substring(0, mcLimit.start()));
                    sb.append(sAndOr);
                }

                sb.append(mcLimit.group());
                sSql = sSql.substring(mcLimit.end());
            }

            //9、最后一段字符的处理
            if (ToolHelper.IsNotNull(sSql.trim()))
            {
                string sWhere = "";
                if (needWhereSplit)
                {
                    sWhere = sWhereString;//有可能WHERE还未处理
                    sSql = sSql.substring(mcWhere.end());
                }

                string sSqlFinal = andOrConditionConvert(sSql);
                if (ToolHelper.IsNotNull(sSqlFinal))
                {
                    sb.append(sWhere + sSqlFinal);
                }
            }
            return sb.toString();
        }

        /**
         * AND和OR的条件转换处理
         * @param sCond 例如：PROVINCE_ID = '#PROVINCE_ID#' AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
         */
        protected string andOrConditionConvert(string sCond)
        {
            StringBuilder sb = new StringBuilder();
            //1、按AND（OR）正则式匹配
            MatchCollection mc = ToolHelper.getMatcher(sCond, StaticConstants.andOrPatter);
            int iStart = 0;
            string sBeforeAndOr = "";
            bool hasGoodCondition = false;
            while (mc.find())
            {
                if (!hasGoodCondition)
                {
                    sBeforeAndOr = ""; //只要没有一个条件时，前面的AND或OR为空
                }
                //2、得到一个AND或OR段
                string oneSql = sCond.substring(iStart, mc.start());

                //查看是否有：##序号##
                bool parenthesesRounFlag = false;//没有
                MatchCollection mc2 = ToolHelper.getMatcher(oneSql, parenthesesRoundKeyPattern);
                while (mc2.find())
                {
                    parenthesesRounFlag = true;
                }
                if (hasKey(oneSql) || parenthesesRounFlag)
                {
                    //2.1、当键存在，或存在：##序号##时，调用括号键转换处理方法
                    string sFinalSql = complexParenthesesKeyConvert(oneSql, sBeforeAndOr);
                    if (ToolHelper.IsNotNull(sFinalSql))
                    {
                        sb.append(sFinalSql);
                        sBeforeAndOr = mc.group();
                        hasGoodCondition = true;
                    }
                }
                else
                {
                    //2.2、当键存在时，调用括号键转换处理方法
                    sb.append(sBeforeAndOr + oneSql);
                    sBeforeAndOr = mc.group();
                    hasGoodCondition = true;
                }
                iStart = mc.end();
            }
            //最后一个AND或OR之后的的SQL字符串处理，也是调用括号键转换处理方法
            string sComplexSql = complexParenthesesKeyConvert(sCond.substring(iStart), sBeforeAndOr);
            sb.append(sComplexSql);

            return sb.toString();
        }

        /**
         * 复杂的括号键转换处理：
         *  之前为了降低复杂度，将包含()的子查询或函数替换为##序号##，这里需要取出来分析
         * @param sSql 包含##序号##的SQL
         * @param sLastAndOr 上次处理中最后的那个AND或OR字符
         * @param isSingleColumnDeal 是否单个字段处理，如查询列中也可能包含参数，无没有则直接跳过
         */
        protected string complexParenthesesKeyConvert(string sSql, string sLastAndOr,bool isSingleColumnDeal=false)
        {
            StringBuilder sb = new StringBuilder();
            string sValue = "";
            //1、分析是否有包含 ##序号## 正则式的字符
            MatchCollection mc = ToolHelper.getMatcher(sSql, parenthesesRoundKeyPattern);
            bool hasFirstMatcher = mc.find();
            if (!hasFirstMatcher)
            {
                //没有双括号，但可能存在单括号，如是要修改为1=1或AND 1=1 的形式
                return parenthesesConvert(sSql, sLastAndOr);
            }

            string sSqlNew = sSql; //注：在匹配的SQL中，不能修改原字符，不然根据mc.start()或mc.end()取出的子字符会不对!!
            Dictionary<string,String> dicReplace = new Dictionary<string,String>();
            //2、有 ##序号## 字符的语句分析：可能会有多个,需要针对每一个##序号##作详细分析
            //比如WITH...INSERT INTO...SELECT和INSERT INTO...WITH...INSERT INTO...
            string sSource = "";
            string sReturn = string.Empty;
            int iLastStart = 0;
            while (hasFirstMatcher)
            {
                sSource = mapsParentheses.get(mc.group());//取出 ##序号## 内容
                if (!hasKey(sSource))
                {
                    sSqlNew = sSqlNew.Replace(mc.group(), sSource);

                    dicReplace.Add(mc.group(), sSource);  //没有键的字符，先加到集合中。在返回前替换
                    //取出下个匹配##序号##的键，如果有，那么继续下个循环去替换##序号##
                    hasFirstMatcher = mc.find();
                    if (hasFirstMatcher)
                    {
                        //继续取出##序号##键的值来替换：WITH...INSERT INTO...SELECT和INSERT INTO...WITH...INSERT INTO...这两种情况会进入本段代码
                        continue;
                    }
                    //2.1 没有键，得到替换并合并之前的AND或OR字符
                    string sConnect = sLastAndOr + sSqlNew;
                    if (!hasKey(sConnect))
                    {
                        //2.2 合并后也没有键，则直接追加到头部字符构建器
                        sb.Append(sConnect);
                        return sb.toString();
                    }
                    //2.3 如果有键传入，那么进行单个键转换
                    sb.Append(singleKeyConvert(sConnect));
                    return sb.toString();
                }

                //判断是否所有键为空
                bool allKeyNull = true;
                MatchCollection mc1 = ToolHelper.getMatcher(sSource, StaticConstants.keyPatternHash);
                while (mc1.find())
                {
                    if (ToolHelper.IsNotNull(singleKeyConvert(mc1.group())))
                    {
                        allKeyNull = false;
                        break;
                    }
                }

                string sPre = sSql.substring(iLastStart, mc.start());
                iLastStart = mc.end();
                string sEnd = sSql.substring(iLastStart); //注：后续部分还可能用##序号##

                //查询单列的动态处理
                if (isSingleColumnDeal)
                {
                    if (!hasKey(sSource))
                    {
                        return sPre + sSource + sEnd; //无键时直接返回
                    }

                    string sSingleKeyName = getFirstKeyName(sSource);
                    if (!mapSqlKeyValid.ContainsKey(sSingleKeyName))
                    {
                        return ""; //没有值传入，直接返回空
                    }
                    return sPre + singleKeyConvert(sSource) + sEnd;
                }

                //3、子查询处理
                string sChildQuery = childQueryConvert(sLastAndOr + sPre, "", sSource);//这里先不把结束字符加上
                sb.append(sChildQuery);//加上子查询
                if (allKeyNull || ToolHelper.IsNotNull(sChildQuery))
                {
                    //取出下个匹配##序号##的键，如果有，那么继续下个循环去替换##序号##
                    hasFirstMatcher = mc.find();
                    if (hasFirstMatcher)
                    {
                        //继续取出##序号##键的值来替换
                        sSqlNew = sEnd;//剩余部分将要被处理
                        continue;
                    }
                    else
                    {
                        sb.append(sEnd);//这里把结束字符加上
                    }

                    sReturn = sb.toString();
                    foreach (string sKey in dicReplace.Keys)
                    {
                        sReturn = sReturn.Replace(sKey, dicReplace[sKey]); //在返回前替换不包含参数的##序号##字符
                    }
                    return sReturn;//如果全部参数为空，或者子查询已处理，直接返回
                }

                //4、非子查询的处理
                sb.append(sEnd);//这里把结束字符加上：TODO，这里会不会有问题？？、
                //判断是否IN表达式
                MatchCollection mcOnlyIn = ToolHelper.getMatcher(sSql, StaticConstants.onlyInPattern);
                string sInAnd = "";
                string sInColumn = "";
                if (mcOnlyIn.find())
                {
                    sInAnd = sLastAndOr;
                    sInColumn =  sPre; //把列名 IN ()这一段完整加上
                }
                else
                {
                    //有键值传入，并且非子查询，做AND或OR正则匹配分拆字符
                    sb.append(sLastAndOr + sPre);//因为不能移除"()"，所以这里先拼接收"AND"或"OR"，记得加上头部字符
                }

                //AND或OR正则匹配处理
                // 注：此处虽然与【andOrConditionConvert】有点类似，但有不同，不能将以下代码替换为andOrConditionConvert方法调用
                MatchCollection mc2 = ToolHelper.getMatcher(sSource, StaticConstants.andOrPatter);
                int iStart = 0;
                string beforeAndOr = "";
                while (mc2.find())
                {
                    //4.1 存在AND或OR
                    string sOne = sSource.substring(iStart, mc2.start()).trim();
                    //【括号SQL段转换方法】
                    sValue = parenthesesConvert(sOne, beforeAndOr);
                    sb.append(sValue);
                    iStart = mc2.end();
                    beforeAndOr = mc2.group();
                }
                //4.2 最后一个AND或OR之后的的SQL字符串处理，也是调用【括号SQL段转换方法】
                string sEndSql = sInColumn + sSource.substring(iStart);
                sValue = parenthesesConvert(sEndSql, beforeAndOr); //TODO:IN
                sb.append(sInAnd + sValue + sEnd);//加上尾部字符

                hasFirstMatcher = mc.find();//注：这里也要重新给hasFirstMatcher赋值，要不会有死循环
            }

            sReturn = sb.toString();
            foreach (string sKey in dicReplace.Keys)
            {
                sReturn = sReturn.Replace(sKey, dicReplace[sKey]); //在返回前替换不包含参数的##序号##字符
            }
            return sReturn;//如果全部参数为空，或者子查询已处理，直接返回
        }

        /// <summary>
        /// 含括号的SQL段转换
        /// 注：已经过AND或OR拆分，只含一个键，并且字符前有左括号，或者字符后有右括号
        /// 例如((CREATOR = '#CREATOR#'、CREATOR_ID = #CREATOR_ID# ) 、 TFLG = '#TFLG#')
        /// </summary>
        /// <param name="sSql">只有一个key的字符（即已经过AND或OR的正则表达式匹配后分拆出来的部分字符）</param>
        /// <param name="sLastAndOr">前一个拼接的AND或OR字符</param>
        /// <returns></returns>
        private string parenthesesConvert(string sSql, string sLastAndOr)
        {
            //1、剔除开头的一个或多个左括号，并且把这些左括号记录到变量中，方便后面拼接
            string sOne = sSql;
            string sStartsParentheses = "";
            while (sOne.startsWith("("))
            {
                sStartsParentheses += "(";
                sOne = sOne.substring(1).trim(); //remvoe the start position of string "("
            }

            //2、剔除结尾处的一个或多个括号，并将它记录到变量中，方便后面拼接
            string sEndRight = "";
            int leftCount = sOne.length() - sOne.replace("(", "").length();//left Parentheses count
            long rightCount = sOne.length() - sOne.replace(")", "").length();//right Parentheses count

            if (leftCount != rightCount)
            {
                while (rightCount - leftCount > 0)
                {
                    sEndRight += ")";
                    sOne = sOne.substring(0, sOne.length() - 1).trim();
                    rightCount--;
                }
            }

            string sParmFinal = singleKeyConvert(sOne);//有括号也一并去掉了。
            if (ToolHelper.IsNull(sParmFinal))
            {
                //没有键值传入
                if (ToolHelper.IsNotNull(sStartsParentheses) || ToolHelper.IsNotNull(sEndRight))
                {
                    //有左或右括号时，就替换为AND 1=1
                    sLastAndOr = sLastAndOr.replace("OR", "AND");
                    return sLastAndOr + sStartsParentheses + " 1=1 " + sEndRight;
                }
                return "";//没有括号时返回空，即可以直接去掉
            }
            else
            {
                //IN清单的括号在里边已组装
                return sLastAndOr + sStartsParentheses + sParmFinal + sEndRight;//有键值传入
            }

        }

        /**
         * 子查询转换
         * @param sPre 前缀
         * @param sEnd 后缀
         * @param sSource ##序号##的具体内容
         * @return
         */
        private string childQueryConvert(string sPre, string sEnd, string sSource)
        {
            StringBuilder sb = new StringBuilder();
            //1、判断是否有子查询:抽取出子查询的 (SELECT 部分
            MatchCollection mcChild = ToolHelper.getMatcher(sSource, StaticConstants.childSelectPattern);
            if (!mcChild.find())
            {
                return "";//没有子查询，返回空
            }

            //2、有子查询，将开头的一个或多个 ( 追加到头部字符构造器，这样剥开才能找到真正的参数控制部分的字符串
            sb.append(sPre);//拼接子查询前缀 (SELECT
            while (sSource.startsWith("("))
            {
                sb.append("(");
                sSource = sSource.substring(1).trim();
            }
            //3、结束位置 ) 的处理：如右括号数与左括数不相等，那么将右括号超过左括号的数量追加到尾部构造器。这样对于里边有方法的()能轻松处理！！
            string sEndRight = "";
            int leftCount = sSource.length() - sSource.replace("(", "").length();//左括号数
            long rightCount = sSource.length() - sSource.replace(")", "").length();//右括号数
            if (leftCount != rightCount)
            {
                //二者不等时，再根据右括号超过左括号的差值，递减到0为空。即左右括号数相等
                while (rightCount - leftCount > 0)
                {
                    sEndRight += ")"; //追加右括号到尾部构造器
                    sSource = sSource.substring(0, sSource.length() - 1).trim();//去掉尾部的右括号
                    rightCount--;
                }
            }

            //子查询中又可能存在子查询，所以这里还要对括号进一步分析
            sSource = generateParenthesesKey(sSource);
            if (!hasKey(sSource))
            {
                sb.append(sSource);//这里有可能已处理完子查询
            }
            else
            {
                /** 4、子查询又相当于一个SELECT语句，这里又存在FROM和WHERE处理，所以这部分是根据SELECT模式，再解析一次。
                 *   这就是为何将queryHeadSqlConvert和queryBeforeFromConvert放在本抽象父类的缘故。
                 */
                mcChild = ToolHelper.getMatcher(sSource, StaticConstants.selectPattern);//抽取出SELECT部分
                while (mcChild.find())
                {
                    //4.1 调用查询头部转换方法
                    string sSqlChild = queryHeadSqlConvert(sSource, true);
                    sb.append(sSqlChild);
                }
            }
            sb.append(sEndRight);//追加右括号
            sb.append(sEnd);//追加 ##序号## 之后部分字符
            return sb.toString(); //返回子查询已处理
        }

        /// <summary>
        /// 单个键SQL转换：一般在对AND（OR）分隔后调用本方法
        /// @param sSql: 例如："[PROVINCE_CODE] = '#PROVINCE_CODE#'" 或 ",[PROVINCE_NAME] = '#PROVINCE_NAME#'"
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        protected string singleKeyConvert(string sSql)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.keyPatternHash);
            while (mc.find())
            {
                string sKey = ToolHelper.getKeyName(mc.group(), myPeachProp);
                if (!mapSqlKeyValid.ContainsKey(sKey))
                {
                    return ""; //1、没有值传入，直接返回空
                }
                SqlKeyValueEntity entity = mapSqlKeyValid.get(sKey);
                string sList = entity.getKeyMoreInfo().getInString();
                //最终值处理标志
                if (ToolHelper.IsNotNull(sList))
                {
                    string[] sInArr = sList.split(",");
                    int iMaxIn = entity.getKeyMoreInfo().PerInListMax > 0 ? entity.getKeyMoreInfo().PerInListMax : myPeachProp.inMax;
                    double dCount = sInArr.Length * 1.0 / iMaxIn;
                    int iCount = (int)Math.Ceiling(dCount);
                    if (iCount <= 1)
                    {
                        return sSql.replace(mc.group(), sList);//替换IN的字符串
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.append("(");
                        for (int i = 0; i < iCount; i++)
                        {
                            string sOne = string.Join(",", sInArr.Skip(i * iMaxIn).Take(iMaxIn));
                            if (i == 0)
                            {
                                string sOneIn = sSql.replace(mc.group(), sOne);
                                sb.append(sOneIn + " ");
                            }
                            else
                            {
                                string sOneIn = "OR " + entity.getKeyMoreInfo().InColumnName + " IN (" + sOne + ") ";
                                sb.append(sOneIn + " ");
                            }
                        }
                        sb.Append(")");
                        return sb.toString();
                    }
                }
                if (entity.getKeyMoreInfo().isMustValueReplace() || myPeachProp.getTargetSqlParamTypeEnum() == TargetSqlParamTypeEnum.DIRECT_RUN)
                {
                    //2、返回替换键后只有值的SQL语句
                    return sSql.replace(mc.group(),entity.getReplaceKeyWithValue().ToString());
                }
                //3、返回参数化的SQL语句：LIKE的问题是在值的前或后或两边加上%解决
                if(myPeachProp.getTargetSqlParamTypeEnum() == TargetSqlParamTypeEnum.NameParam)
                {
                    return sSql.replace(mc.group(), myPeachProp.getParamPrefix() + sKey + myPeachProp.getParamSuffix());
                }
                return sSql.replace(mc.group(), "?");
            }
            return sSql;//4、没有键时，直接返回原语句
        }

        /**
         *获取第一个键的字符串
         * @param sSql
         * @return 例如：'%#CITY_NAME#%'
         */
        protected string getFirstKeyString(string sSql)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.keyPatternHash);
            if (mc.find())  
            {
                return mc.group();
            }
            else
            {
                return "";
            }
        }

        /**
         *获取第一个键的键名
         * @param sSql
         * @return 例如：CITY_NAME
         */
        protected string getFirstKeyName(string sSql)
        {
            string sParamString = getFirstKeyString(sSql);
            return ToolHelper.getKeyName(sParamString, myPeachProp);
        }

        /**
         * 判断SQL是否有键
         * @param sSql
         * @return
         */
        protected bool hasKey(string sSql)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.keyPatternHash);
            bool hasPara = false;
            while (mc.find())
            {
                hasPara = true;
                break;
            }
            return hasPara;
        }

        /***
         * 查询的头部处理
         * 注：放这里的原因是INSERT INTO ... SELECT 语句也用到该方法
         * @param sSql
         */
        protected string queryHeadSqlConvert(string sSql, bool childQuery)
        {
            StringBuilder sb = new StringBuilder();
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.selectPattern);//抽取出SELECT部分
            if (mc.find())
            {
                sb.append(mc.group());//不变的SELECT部分先加入
                sSql = sSql.substring(mc.end()).trim();

                //UNION 或 UNION ALL的处理
                sSql = unionOrUnionAllConvert(sSql, sb);
                if (ToolHelper.IsNull(sSql))
                {
                    return sb.toString();
                }
                //非UNION 且 非UNION ALL的处理
                string sFinalSql = fromWhereSqlConvert(sSql, childQuery);
                sb.append(sFinalSql);
            }
            else
            {
                //传过来的SQL有可能去掉了SELECT部分
                //UNION 或 UNION ALL的处理
                sSql = unionOrUnionAllConvert(sSql, sb);
                if (ToolHelper.IsNull(sSql))
                {
                    return sb.toString();
                }

                string sFinalSql = fromWhereSqlConvert(sSql, childQuery);
                sb.append(sFinalSql);
            }
            return sb.toString();
        }

        /***
         * 查询的FROM前段SQL处理
         * 注：放这里的原因是INSERT INTO ... SELECT 语句也用到该方法
         * @param sSql
         */
        protected string queryBeforeFromConvert(string sSql)
        {
            StringBuilder sb = new StringBuilder();
            string[] sSelectItemArray = sSql.split(",");
            string sComma = "";
            foreach (string col in sSelectItemArray)
            {
                //查看是否有：##序号##
                bool parenthesesRounFlag = false;//没有
                MatchCollection mc = ToolHelper.getMatcher(col, parenthesesRoundKeyPattern);
                if (mc.find())
                {
                    parenthesesRounFlag = true;
                }
                if (!hasKey(col) && !parenthesesRounFlag)
                {
                    sb.append(sComma + col);
                    sComma = ",";
                    continue;
                }
                //括号转换处理
                string colString = complexParenthesesKeyConvert(sComma + col, "", true);
                if(!hasKey(colString))
                {
                    sb.append(colString);
                    sComma = ",";
                    continue;
                }

                string sKey = getFirstKeyName(colString);
                if (mapSqlKeyValid.ContainsKey(sKey))
                {
                    sb.append(singleKeyConvert(colString));
                    sComma = ",";
                }

                if (sComma.isEmpty())
                {
                    sComma = ",";
                }
            }

            return sb.toString();
        }

        protected string dealUpdateSetItem(string sSql)
        {
            StringBuilder sb = new StringBuilder();
            string[] sSetArray = sSql.split(",");
            string sComma = "";
            foreach (string col in sSetArray)
            {
                if (!hasKey(col))
                {
                    sb.append(sComma + col);
                    sComma = ",";
                    continue;
                }

                sb.append(complexParenthesesKeyConvert(sComma + col, ""));

                if (sComma.isEmpty())
                {
                    string sKey = getFirstKeyName(col);
                    if (mapSqlKeyValid.ContainsKey(sKey))
                    {
                        sComma = ",";
                    }
                }
            }
            return sb.toString();
        }

        protected string dealInsertItemAndValue(string sSql, StringBuilder sbHead, StringBuilder sbTail)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.valuesPattern);//先根据VALUES关键字将字符分隔为两部分
            if (!mc.find())
            {
                return sSql;
            }
            string sInsert = "";
            string sPara = "";
            
            string sInsertKey = sSql.substring(0, mc.start()).trim();
            string sParaKey = sSql.substring(mc.end()).trim();

            sInsert = ToolHelper.removeBeginEndParentheses(mapsParentheses.get(sInsertKey));
            sPara = ToolHelper.removeBeginEndParentheses(mapsParentheses.get(sParaKey));
            sPara = generateParenthesesKey(sPara);//针对有括号的部分先替换为##序号##

            sbHead.append("(");//加入(
            sbTail.append(mc.group() + "(");//加入VALUES(

            //3、 insert into ... values形式
            string[] colArray = sInsert.split(",");
            string[] paramArray = sPara.split(",");

            int iGood = 0;
            for (int i = 0; i < colArray.Length; i++)
            {
                string sOneParam = paramArray[i];
                string sParamSql = complexParenthesesKeyConvert(sOneParam, "");
                if (ToolHelper.IsNotNull(sParamSql))
                {
                    if (iGood == 0)
                    {
                        sbHead.append(colArray[i]);
                        sbTail.append(sParamSql);
                    }
                    else
                    {
                        sbHead.append("," + colArray[i]);
                        sbTail.append("," + sParamSql);
                    }
                    iGood++;
                }
            }
            sbHead.append(")");
            sbTail.append(")");
            sSql = "";//处理完毕清空SQL
            return sSql;
        }

        /// <summary>
        /// UNION 或 UNION ALL 或 其他处理
        /// </summary>
        /// <param name="sSql">处理前SQL</param>
        /// <param name="sbHead">处理后的拼接SQL</param>
        /// <returns></returns>
        protected string unionOrUnionAllConvert(string sSql, StringBuilder sbHead)
        {
            //UNION和UNION ALL处理
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.unionAllPartner);
            int iStart = 0;
            while (mc.find())
            {
                string sOne = sSql.substring(iStart, mc.start());
                string sConvertSql = queryHeadSqlConvert(sOne, false);
                sbHead.append(sConvertSql);
                iStart = mc.end();
                sbHead.append(mc.group());
            }

            if (iStart > 0)
            {
                //UNION或UNION ALL处理剩下部分的处理
                string sOne = sSql.substring(iStart);
                string sConvertSql = queryHeadSqlConvert(sOne, false);
                sbHead.append(sConvertSql);
                return "";
            }
            return sSql;
        }

        /**
         * 头部SQL转换：子类实现
         * @param sSql
         */
        protected abstract string headSqlConvert(string sSql);

        /**
         * FROM前段的SQL转换：子类实现
         * @param sSql
         */
        protected abstract string beforeFromConvert(string sSql);

        /// <summary>
        /// 是否正确SQL类型抽象方法
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        public abstract bool isRightSqlType(string sSql);

    }
}