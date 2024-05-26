using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
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
            public static string Excel_TableColumnLY = TableSQL_Pre + @"\LY\模板_LY数据库变更.xlsm";
            public static string Excel_TableColumnRemark = TableSQL_Pre + "模板_表列备注扩展信息.xlsx";
            //资源路径
            public static string Html_Html = DataSQL_SourcePre + "Html.txt";
            public static string Html_Table = DataSQL_SourcePre + "Table.txt";
            public static string Html_Column = DataSQL_SourcePre + "Columns.txt";
            //LY模板的资源路径
            public static string Html_Table_LY = DataSQL_SourcePre + "LY.Table.txt";
            public static string Html_TableMerge_LY = DataSQL_SourcePre + "LY.TableMerge.txt";
            public static string Html_Column_LY = DataSQL_SourcePre + "LY.Columns.txt";
            public static string Html_Column_Move = DataSQL_SourcePre + "LY.ColumnsMove.txt";
            //数据标准分类配置文件
            public static string Xml_StandardClassFileName = "StandardColumnClassConfig.xml";
            //列默认值保存文件
            public static string Xml_ColumnDefaultValueFileName = "ColumnDefaultValueConfig.xml";

        }

        public static class StringBuild
        {
            public static string Xml_MergeScript = StringBuild_Pre + "模板_合并脚本配置.xml";
            public static string Xml_CopyString = StringBuild_Pre + "模板_点击拷贝字符.xml";
        }

        public static class AutoFile
        {
            public static string Excel_Code = AutoFile_Pre + "模板_生成代码文件.xlsx";
        }
        public static class AutoEntity
        {
            public static string Excel_Code = AutoEntity_Pre + "模板_生成实体.xlsx";
        }

        public static class DataSQL
        {
            public static string Excel_Data = DataSQL_Pre + "模板_生成数据.xlsx";

        }

        public static class ReplaceTextFileString
        {
            public static string Xml_FileName = "ReplaceStringConfig.xml";
        }

        public static class TableColumnDictionary
        {
            public static string Xml_FileName = "ColumnDictionaryReplaceStringConfig.xml";
            public static string Xml_StringTemplateFileName = "ColumnDictionary_StringTemplateConfig.xml"; //字符模板配置文件
        }

        /// <summary>
        /// 数据库间SQL转换
        /// </summary>
        public static class DbConvertSql
        {
            public static string Xml_NewOldTableFileName = "DbConvertSqlNewOldTableConfig.xml"; //新旧表配置
            public static string Xml_NewOldColumnFileName = "DbConvertSqlNewOldColumnConfig.xml"; //新旧列关系配置
        }

        public static string AppPath
        {
            get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\"; }
        }

        public static readonly string DataAccessConfigKey = "IDataAccessDBTool";

        public static readonly string DBInitializerConfigKey = "IDBInitializerDBTool";
        /// <summary>
        /// 加密使用的键：必须24个字符
        /// </summary>
        public static readonly string DBTDesEncryKey = "BrEe%eKiMLVPeaCh*DbTooL#";
        /// <summary>
        /// 加密使用的向量：必须16个字符
        /// </summary>
        public static readonly string DBTDesEncryVector = "=PAc&Ve&OEBrDbT+";
    }
}
