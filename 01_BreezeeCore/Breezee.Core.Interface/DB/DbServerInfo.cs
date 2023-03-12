using System;

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
        #endregion

        #region 构造函数
        public DbServerInfo()
        {

        }

        public DbServerInfo(DataBaseType databaseType, string serverName, string userName, string password, string database, string portNo, string schemaName, string otherString)
        {
            _databaseType = databaseType;
            _serverName = serverName;
            _userName = userName;
            _password = password;
            _database = database;
            _portNo = portNo;
            _schemaName = schemaName;
            _otherString = otherString;

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
        #endregion
    }
}

