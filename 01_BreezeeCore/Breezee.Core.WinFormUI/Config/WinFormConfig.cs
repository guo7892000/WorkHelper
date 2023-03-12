using Breezee.Core.Interface;
using System.Data;


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
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 类
    /// </summary>
    public class WinFormConfig
    {
        private static string configPath;
        private static KeyValuePairConfig config;
        private static DataTable dtConfig;

        #region IOC容器
        private static readonly object lockob = new object();
        private static WinFormConfig _instance;

        /// <summary>
        /// 容器
        /// </summary>
        public static WinFormConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockob)
                    {
                        if (_instance == null)
                        {
                            _instance = new WinFormConfig();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
        public WinFormConfig()
        {
            configPath = GlobalValue.StartupPath + @"\Config\Data\FormStyle.xml";
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

        public static class WinFormConfigString
        {
            public static string MainSkinType = "MainSkinType";
            public static string MainSkinValue = "MainSkinValue";
            public static string MainSkinColorName = "MainSkinColorName";
            public static string CommonSkinType = "CommonSkinType";
            public static string CommonSkinValue = "CommonSkinValue";
            public static string CommonSkinColorName = "CommonSkinColorName";
            public static string SavePromptType = "SavePromptType";
        }
    }

    
}