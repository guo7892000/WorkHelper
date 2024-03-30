using Breezee.Core.Interface;
using Breezee.Framework.Mini.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据字典中的字符模板相关
    /// </summary>
    public class TableColumnDicTemplate
    {
        public static string DicDirRoot { get { return Path.Combine(GlobalContext.AppBaseDirectory, "DataTemplate", "DBTool", "ConnectString"); } }
        public static string YapiDirPath { get { return Path.Combine(DicDirRoot, "YAPI"); } }
        public static string StringTempDirPath { get { return Path.Combine(DicDirRoot, "StringTemplate"); } }

        public static string MyBatisDynDirPath { get { return Path.Combine(DicDirRoot, "MyBatisDynamicCondition"); } }

        /// <summary>
        /// YAPI的相关模板
        /// </summary>
        public static class YapiString
        {
            public static readonly string ConditionNoPage = "ConditionNoPage.json";
            public static readonly string ConditionPage = "ConditionPage.json";
            public static readonly string ResultNoPage = "ResultNoPage.json";
            public static readonly string ResultPage = "ResultPage.json";
            public static readonly string ResultArrayNoPage = "ResultArrayNoPage.json";
            public static readonly string ResultArrayPage = "ResultArrayPage.json";
        }

        /// <summary>
        /// 字符模板
        /// </summary>
        public static class StringTemplate
        {
            public static readonly string CommonEntity = "CommonEntity.txt";
            public static readonly string MyBatisDynamicConditon = "MyBatisDynamicConditon.txt";
            public static readonly string MyBatisTableEntity = "MyBatisTableEntity.txt";
            public static readonly string YapiColumnDesc = "YapiColumnDesc.txt";
        }

        public static class MyBatisDynamicCond
        {
            public static readonly string If = "If.txt";
            public static readonly string In = "In.txt";
            public static readonly string Date = "Date.txt";
        }

        public class StringTemplateConfig
        {
            public static readonly string Id = "id";
            public static readonly string Name = "name";
            public static readonly string TemplateString = "text";
        }
    }
}
