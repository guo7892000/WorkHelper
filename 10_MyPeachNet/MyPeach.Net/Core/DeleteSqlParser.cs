﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace org.breezee.MyPeachNet
{
    /**
     * @objectName:Delete Sql Analyzer(删除SQL分析器)
     * @description:
     * @author: guohui.huang
     * @email: guo7892000@126.com
     * @wechat: BreezeeHui
     * @date: 2022/4/12 16:45
     */
    public class DeleteSqlParser : AbstractSqlParser
    {
        public DeleteSqlParser(MyPeachNetProperties properties) : base(properties)
        {
            sqlTypeEnum = SqlTypeEnum.DELETE;
        }

        protected override string headSqlConvert(string sSql)
        {
            StringBuilder sb = new StringBuilder();
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.deletePattern);//抽取出INSERT INTO TABLE_NAME(部分
            if(mc.find())
            {
                sb.append(mc.group());//不变的INSERT INTO TABLE_NAME(部分先加入
                sqlTypeEnum = SqlTypeEnum.DELETE;
                //FROM部分SQL处理
                string sWhereSql = fromWhereSqlConvert(sSql.substring(mc.end()), false);
                //如果禁用全表更新，并且条件为空，则抛错！
                if (ToolHelper.IsNull(sWhereSql) && myPeachProp.isForbidAllTableUpdateOrDelete())
                {
                    mapError.put("出现全表删除，已停止", "删除语句不能没有条件，那样会清除整张表数据！");//错误列表
                }
                sb.append(sWhereSql);
            }
            else
            {
                sqlTypeEnum = SqlTypeEnum.Unknown;
            }
            return sb.toString();
        }

        /**
         * FROM前段SQL处理
         * @param sSql
         * @return
         */
        protected override string beforeFromConvert(string sSql)
        {
            return "";
        }

        /// <summary>
        /// 是否SQL正确抽象方法
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        public override bool isRightSqlType(string sSql)
        {
            MatchCollection mc = ToolHelper.getMatcher(sSql, StaticConstants.deletePattern);//抽取出INSERT INTO TABLE_NAME(部分
            return mc.find();
        }
    }
}
