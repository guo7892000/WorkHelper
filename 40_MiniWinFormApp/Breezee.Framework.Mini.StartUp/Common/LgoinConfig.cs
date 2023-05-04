using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：登录配置类	
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
    /// 登录配置类
    /// </summary>
    public class LoginConfig : IXmlConfigPair
    {
        public override IXmlConfigPair XmlConfig { get; }
        public LoginConfig(string sPath, string sConfigName, XmlConfigSaveType saveType)
        {
            Dir = sPath;
            FileName = sConfigName;
            XmlConfig = new KeyValuePairConfig(sPath, sConfigName, saveType);
        }

        public override void Set(string sKey, string sValue, string sRemark)
        {
            XmlConfig.Set(sKey, sValue, sRemark);
        }

        public override KeyValuePairEntity GetEntity(string sKey, string sDefault)
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