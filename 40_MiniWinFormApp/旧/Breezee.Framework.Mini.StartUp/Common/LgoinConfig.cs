using Breezee.Core.Interface;
using Breezee.Core.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/10 0:46:13	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/10 0:46:13 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 类
    /// </summary>
    public class LoginConfig
    {
        private static string configPath;
        private static KeyValuePairConfig config;
        private static DataTable dtConfig;

        #region IOC容器
        private static readonly object lockob = new object();
        private static LoginConfig _instance;

        /// <summary>
        /// 容器
        /// </summary>
        public static LoginConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockob)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoginConfig();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
        public LoginConfig()
        {
            configPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Mini\Data\LoginConfig.xml";
            config = new KeyValuePairConfig(configPath);
            dtConfig = config.Get();
        }
        public string Get(string sKey)
        {
            DataRow[] drArr = dtConfig.Select(KeyValuePairConfig.XmlKeyValueStr.KeyProp.Key + "='"+ sKey + "'");
            if (drArr == null || drArr.Length == 0)
            {
                dtConfig.Rows.Add(dtConfig.NewRow());
            }
            return drArr[0][KeyValuePairConfig.XmlKeyValueStr.KeyProp.Value].ToString();
        }

        public void Set(string sKey,string sValue) {
            DataRow[] drArr = dtConfig.Select(KeyValuePairConfig.XmlKeyValueStr.KeyProp.Key + "='" + sKey + "'");
            if (drArr != null || drArr.Length == 0)
            {
                dtConfig.Rows.Add(dtConfig.NewRow());
            }
            drArr[0][KeyValuePairConfig.XmlKeyValueStr.KeyProp.Value] = sValue;
        }

        public void Save()
        {
            config.Save(dtConfig, configPath);
        }

        public static class LgoinConfigString
        {
            public static string LastLoginUserName = "LastLoginUserName";
            public static string IsRememberUserName = "IsRememberUserName";
            public static string BrandCode = "BrandCode";
            public static string IsRememberPwd = "IsRememberPwd";
            public static string LastLoginPwd = "LastLoginPwd";
            public static string AutoCompleteUserList = "AutoCompleteUserList";
        }
    }

    
}