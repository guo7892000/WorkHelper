using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// 所有数据库标准保存的配置文件
    /// </summary>
    public class DataStandardConfig
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public MiniXmlConfig XmlConfig { get; }

        public DataStandardConfig() 
        {
            //通用列相关
            List<string> list = new List<string>();
            list.AddRange(new string[] {
                DataStandardStr.Id,
                DataStandardStr.Name,
                DataStandardStr.NameCN,
                DataStandardStr.NameUpper,
                DataStandardStr.NameLower,
                DataStandardStr.DataLength,
                DataStandardStr.DataPrecision,
                DataStandardStr.DataScale,
                //参考类型
                DataStandardStr.DataType,
                DataStandardStr.DataTypeFull,
                DataStandardStr.Comments,
                //新增类型
                DataStandardStr.BigType,
                DataStandardStr.SmallType,
                DataStandardStr.IsEnable,
                //
                DataStandardStr.OracleDataType,
                DataStandardStr.OracleDataLength,
                DataStandardStr.OracleDataTypeFull,
                DataStandardStr.MySqlDataType,
                DataStandardStr.MySqlDataLength,
                DataStandardStr.MySqlDataTypeFull,
                DataStandardStr.SqlServerDataType,
                DataStandardStr.SqlServerDataLength,
                DataStandardStr.SqlServerDataTypeFull,
                DataStandardStr.PostgreSqlDataType,
                DataStandardStr.PostgreSqlDataLength,
                DataStandardStr.PostgreSqlDataTypeFull,
                DataStandardStr.SQLiteDataType,
                DataStandardStr.SQLiteDataLength,
                DataStandardStr.SQLiteDataTypeFull,

            });
            XmlConfig = new MiniXmlConfig(GlobalContext.PathData(), "CommonColumnConfig.xml", list, DataStandardStr.Name);
            XmlConfig.Load(); 
        }
    }

    public static class DataStandardStr
    {
        public static string Name = DBColumnSimpleEntity.SqlString.Name;
        public static string NameCN = DBColumnSimpleEntity.SqlString.NameCN;
        public static string NameUpper = DBColumnSimpleEntity.SqlString.NameUpper;
        public static string NameLower = DBColumnSimpleEntity.SqlString.NameLower;
        
        public static string DataLength = DBColumnSimpleEntity.SqlString.DataLength;
        public static string DataPrecision = DBColumnSimpleEntity.SqlString.DataPrecision;
        public static string DataScale = DBColumnSimpleEntity.SqlString.DataScale;
        //加入字典时的原列信息
        public static string DataType = DBColumnSimpleEntity.SqlString.DataType;
        public static string DataTypeFull = DBColumnSimpleEntity.SqlString.DataTypeFull;
        public static string Comments = DBColumnSimpleEntity.SqlString.Comments;

        //新增的
        public static string Id = "Id";
        public static string BigType = "DataTypeBig";
        //public static string BigTypeName = "DataTypeBigName";
        public static string SmallType = "DataTypeSmall";
        //public static string SmallTypeName = "DataTypeSmallName";
        public static string IsEnable = "IsEnable";
        //特殊数据库类型配置
        //Oracle
        public static string OracleDataType = "OracleDataType";
        public static string OracleDataLength = "OracleDataLength";
        public static string OracleDataTypeFull = "OracleDataTypeFull";
        //MySql
        public static string MySqlDataType = "MySqlDataType";
        public static string MySqlDataLength = "MySqlDataLength";
        public static string MySqlDataTypeFull = "MySqlDataTypeFull";
        //SqlServer
        public static string SqlServerDataType = "SqlServerDataType";
        public static string SqlServerDataLength = "OSqlServerDataLength";
        public static string SqlServerDataTypeFull = "SqlServerDataTypeFull";
        //PostgreSql
        public static string PostgreSqlDataType = "PostgreSqlDataType";
        public static string PostgreSqlDataLength = "PostgreSqlDataLength";
        public static string PostgreSqlDataTypeFull = "PostgreSqlDataTypeFull";
        //SQLite
        public static string SQLiteDataType = "SQLiteDataType";
        public static string SQLiteDataLength = "SQLiteDataLength";
        public static string SQLiteDataTypeFull = "SQLiteDataTypeFull";
    }

    public enum DataTextType
    {
        Text,
        Date,
        Decimal,
        Int
    }
}
