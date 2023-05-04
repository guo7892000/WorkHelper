using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

/*********************************************************************		
 * 对象名称：键值对应用配置		
 * 对象类别：类		
 * 创建作者：黄国辉		
 * 创建日期：2023/04/24 22:29:28		
 * 对象说明：应用启动时加载的配置，如用户喜好设置路径。		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 键值对应用配置
    /// </summary>
    public class AppConfigPair : IXmlConfigPair
    {
        public override IXmlConfigPair XmlConfig { get;  }
        public AppConfigPair(string sPath,string sConfigName, XmlConfigSaveType saveType)
        {
            Dir = sPath;
            FileName = sConfigName;
            XmlConfig = new KeyValuePairConfig(sPath, sConfigName, saveType);
        }

        public override void Set(string sKey,string sValue, string sRemark)
        {
            XmlConfig.Set(sKey, sValue, sRemark);
        }

        public override KeyValuePairEntity GetEntity(string sKey,string sDefault)
        {
            return XmlConfig.GetEntity(sKey, sDefault);
        }

        public override string Get(string sKey, string sDefault)
        {
            return XmlConfig.Get(sKey, sDefault);
        }

        public override bool Save()
        {
            XmlConfig.Save();
            return true;
        }
    }
}
