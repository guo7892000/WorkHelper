﻿using System;
using System.Collections.Generic;
using System.Data;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 数据库服务器信息
    /// </summary>
    public class DbServerInfo
    {
        #region 变量
        private string _database;
        private DataBaseType _databaseType;
        private string _password;
        private string _serverName;
        private string _userName;
        private string _portNo;
        private string _schemaName;
        private string _otherString;

        private bool _useConnString = false;
        private string _ConnString = "";
        private string _dbConnConfigName;//连接配置的名称
        private string _dbConnKey;//连接配置的关键词

        private static string _splitChar = "||";
        #endregion

        #region 构造函数
        public DbServerInfo()
        {

        }

        public DbServerInfo(DataBaseType databaseType, string serverName, string userName, string password, string database, string portNo, string schemaName, string otherString, string dbConnConfigName=null)
        {
            _databaseType = databaseType;
            _serverName = serverName;
            _userName = userName;
            _password = password;
            _database = database;
            _portNo = portNo;
            _schemaName = schemaName;
            _otherString = otherString;
            DbConnConfigName = dbConnConfigName;
            //重新设置键
            ResetConnKey(this);
        }
        #endregion

        #region 属性
        public string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public DataBaseType DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
        public string PortNo
        {
            get
            {
                return _portNo;
            }
            set
            {
                _portNo = value;
            }
        }
        public string SchemaName
        {
            get
            {
                return _schemaName;
            }
            set
            {
                _schemaName = value;
            }
        }

        public string OtherString
        {
            get
            {
                return _otherString;
            }
            set
            {
                _otherString = value;
            }
        }

        public bool UseConnString
        {
            get
            {
                return _useConnString;
            }
            set
            {
                _useConnString = value;
            }
        }

        public string ConnString
        {
            get
            {
                return _ConnString;
            }
            set
            {
                _ConnString = value;
            }
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 是否主要数据库
        /// </summary>
        public string IsMain { get; set; }
        public string DbConnConfigName { get => _dbConnConfigName; set => _dbConnConfigName = value; }
        public string DbConnKey { get => _dbConnKey; private set => _dbConnKey = value; }

        #endregion

        public static class XmlNodeString
        {
            public static string root = "xml";
            public static string node = "dbConfig";
            public static string pk = "key";//主键：要取XmlAttrString中的成员值
        }
        public static class XmlAttrString
        {
            public static string key = "key";
            public static string isMain = "isMain";
            public static string dbType = "dbType";
            public static string serverName = "serverName";
            public static string userName = "userName";
            public static string password = "password";
            public static string dataBase = "dataBase";
            public static string portNo = "portNo";
            public static string schemaName = "schemaName";
            public static string otherString = "otherString";

            public static List<string> getList()
            {
                return new List<string>()
                {
                    key,
                    isMain,
                    dbType,
                    serverName,
                    userName,
                    password,
                    dataBase,
                    portNo,
                    schemaName,
                    otherString
                };
            }

        }

        public static DbServerInfo GetDbServer(DataRow dr)
        {
            DbServerInfo dbServer = new DbServerInfo();
            dbServer.Key = dr[XmlAttrString.key].ToString(); //主键ID
            dbServer.IsMain = dr[XmlAttrString.isMain].ToString();
            dbServer.ServerName = dr[XmlAttrString.serverName].ToString();
            dbServer.PortNo = dr[XmlAttrString.portNo].ToString();
            dbServer.UserName = dr[XmlAttrString.userName].ToString();
            dbServer.SchemaName = dr[XmlAttrString.schemaName].ToString();
            dbServer.Database = dr[XmlAttrString.dataBase].ToString();
            dbServer.Password = dr[XmlAttrString.password].ToString();
            //转换为枚举
            dbServer.DatabaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), dr[XmlAttrString.dbType].ToString());
            dbServer._otherString = dr[XmlAttrString.otherString].ToString();
            //重新设置连接键
            ResetConnKey(dbServer);
            return dbServer;
        }

        /// <summary>
        /// 重新设置连接键
        /// </summary>
        /// <param name="dbServer"></param>
        public static void ResetConnKey(DbServerInfo dbServer)
        {
            if (dbServer == null) return;
            string sCommonName = getRightKeyChar(dbServer.ServerName + _splitChar + dbServer.Database);
            switch (dbServer.DatabaseType)
            {
                case DataBaseType.SqlServer:
                case DataBaseType.PostgreSql:
                case DataBaseType.MySql:
                    dbServer.DbConnKey = sCommonName;
                    break;
                case DataBaseType.Oracle:
                    dbServer.DbConnKey = getRightKeyChar(dbServer.ServerName + _splitChar + dbServer.UserName);
                    break;
                case DataBaseType.SQLite:
                    dbServer.DbConnKey = getRightKeyChar(dbServer.ServerName);
                    break;
                case DataBaseType.None:
                default:
                    dbServer.DbConnKey = string.IsNullOrEmpty(dbServer.Key) ? Guid.NewGuid().ToString() : dbServer.Key;
                    break;
            }
        }

        private static string getRightKeyChar(string sKey)
        {
            return sKey.Trim().Replace("/","").Replace("\\", "").Replace(":", "").Replace("|", "");
        }

        public static bool IsSameServer(DbServerInfo dbServer, DbServerInfo dbOtherServer)
        {
            if (dbServer == null || dbOtherServer == null) return false;
            if (!dbServer.ServerName.Equals(dbOtherServer.ServerName, StringComparison.OrdinalIgnoreCase)) return false;
            if (!dbServer.PortNo.Equals(dbOtherServer.PortNo, StringComparison.OrdinalIgnoreCase)) return false;
            if (!dbServer.UserName.Equals(dbOtherServer.UserName, StringComparison.OrdinalIgnoreCase)) return false;
            if (!dbServer.Password.Equals(dbOtherServer.Password, StringComparison.OrdinalIgnoreCase)) return false;
            if (!dbServer.SchemaName.Equals(dbOtherServer.SchemaName, StringComparison.OrdinalIgnoreCase)) return false;
            if (!dbServer.Database.Equals(dbOtherServer.Database, StringComparison.OrdinalIgnoreCase)) return false;
            return true;
        }

        /// <summary>
        /// 返回数据库类型表
        /// </summary>
        /// <param name="isHasEmpty">是否有一个空白行</param>
        /// <param name="hasNone">是否包括一个None类型的数据库</param>
        /// <returns></returns>
        public static DataTable GetDbTypeTable(bool isHasEmpty,bool hasNone=false)
        {
            IDictionary<string, string> map = new Dictionary<string, string>()
            {
                {"0","SqlServer" },
                {"1","Oracle" },
                {"2","MySql" },
                {"3","SQLite" },
                {"4","PostgreSql" }
            };
            if (hasNone)
            {
                map["9999"] = "None";
            }
            return map.GetTextValueTable(isHasEmpty);
        }
    }
}

