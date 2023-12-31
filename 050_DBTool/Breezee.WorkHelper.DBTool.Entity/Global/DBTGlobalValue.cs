﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class DBTGlobalValue
    {
        private static string Base_Pre = @"DataTemplate\DBTool\Base\";
        private static string TableSQL_Pre = @"DataTemplate\DBTool\TableSQL\";
        private static string DataSQL_Pre = @"DataTemplate\DBTool\DataSQL\";
        private static string AutoFile_Pre = @"DataTemplate\DBTool\AutoFile\";
        private static string AutoEntity_Pre = @"DataTemplate\DBTool\AutoEntity\";
        private static string StringBuild_Pre = @"DataTemplate\DBTool\StringBuild\";

        private static string DataSQL_SourcePre = "Breezee.WorkHelper.DBTool.UI.DataTemplate.DBTool.TableSQL.";

        public static class Base
        {
            public static string DBConfig = Base_Pre + "DBConfig.xml";
            public static string DefaultValue_Oracle = Base_Pre + "Oracle表列默认值清单.xml";
            public static string DefaultValue_SqlServer = Base_Pre + "SQL表列默认值清单.xml";
            public static string Exclude_Oracle = Base_Pre + "Oracle生成SQL排除字段清单.xml";
            public static string Exclude_SqlServer = Base_Pre + "SQL生成SQL排除字段清单.xml";

        }
        public static class TableSQL
        {
            public static string Excel_TableColumn = TableSQL_Pre + "模板_表列结构.xlsx";
            public static string Excel_TableColumnRemark = TableSQL_Pre + "模板_表列备注扩展信息.xlsx";
            //资源路径
            public static string Html_Html = DataSQL_SourcePre + "Html.txt";
            public static string Html_Table = DataSQL_SourcePre + "Table.txt";
            public static string Html_Column = DataSQL_SourcePre + "Columns.txt";

        }

        public static class StringBuild
        {
            public static string Xml_MergeScript = StringBuild_Pre + "模板_合并脚本配置.xml";
            public static string Xml_CopyString = StringBuild_Pre + "模板_点击拷贝字符.xml";
        }

        public static class AutoFile
        {
            public static string Excel_IBD = AutoFile_Pre + "模板_生成IBD文件.xlsx";
            public static string I = AutoFile_Pre + "I.txt";
            public static string B = AutoFile_Pre + "B.txt";
            public static string D = AutoFile_Pre + "D.txt";
            public static string ID = AutoFile_Pre + "ID.txt";
            public static string Frm = AutoFile_Pre + "Frm.txt";
            public static string Frm_Designer = AutoFile_Pre + "Frm.designer.txt";
            public static string Para = AutoFile_Pre + "Para.txt";
            
        }
        public static class AutoEntity
        {
            public static string Param = AutoEntity_Pre + "param.txt";
            public static string Table = AutoEntity_Pre + "table.txt";
            public static string ColumnProp = AutoEntity_Pre + "columnProperty.txt";
            public static string ColumnStr = AutoEntity_Pre + "columnString.txt";
            public static string Xml_Column = AutoEntity_Pre + "ColumnConfig.xml";

        }

        public static class DataSQL
        {
            public static string Excel_Data = DataSQL_Pre + "模板_生成数据.xlsx";

        }


        public static string AppPath
        {
            get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\"; }
        }

        public static readonly string DataAccessConfigKey = "IDataAccessDBTool";
    }
}
