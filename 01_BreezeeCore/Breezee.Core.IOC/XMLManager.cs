using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

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
namespace Peach.Core.Context
{
    public class XMLManager
    {
        /// <summary>
        /// 配置文件信息字典集合
        /// </summary>
        public static IDictionary<string, string> dicAllConfig = null;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string sConfigPath = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public XMLManager()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            sConfigPath = a.CodeBase.Substring(8, a.CodeBase.LastIndexOf(@"/") - 7);
            sConfigPath = sConfigPath.Replace(@"/", @"\");
        }

        /// <summary>
        /// 递归遍历xml文件中所有节点信息
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="sXMLPath"></param>
        /// <param name="dicDMSConfig"></param>
        public void XmlFileAllNodes(XmlNode xn, string sXMLPath, IDictionary<string, string> dicDMSConfig)
        {
            if (xn.Attributes == null)
            {
                dicDMSConfig[sXMLPath] = xn.Value; // 设置XPath节点下的值
            }
            else
            {
                sXMLPath += "/" + xn.Name; // 合成XPath
                dicDMSConfig[sXMLPath] = xn.Value;
                // 对于xml文件中的文件的二次递归遍历
                for (int i = 0; i < xn.Attributes.Count; i++)
                {
                    if (xn.Name.ToUpper().Trim() == "INCLUDE" && xn.Attributes[i].Name.ToUpper().Trim() == "URI")
                    {
                        string sFileNAme = xn.Attributes[i].Value;
                        string sPrex = "file://";
                        sFileNAme = sFileNAme.Substring(sPrex.Length, sFileNAme.Length - sPrex.Length);

                        XmlDocument dmsXml = new XmlDocument();
                        try
                        {
                            dmsXml.Load(sConfigPath + "config\\" + sFileNAme);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + "  XMLPath:" + sConfigPath + "config\\" + sFileNAme, ex);
                        }
                        string sXMLPath2 = dmsXml.DocumentElement.Name;

                        for (int j = 0; j < dmsXml.DocumentElement.ChildNodes.Count; j++)
                        {
                            XmlFileAllNodes(dmsXml.DocumentElement.ChildNodes[j], sXMLPath2, dicDMSConfig);
                        }
                    }
                }
            }

            string sValue = dicDMSConfig[sXMLPath];

            if (sValue != null)
            {
                //sValue = cRegex任何空白字符.Replace(sValue, " "); // 对回车,制表,空格的处理
                sValue = sValue.Replace("&lt;", "<");// 对小于号的处理
                sValue = sValue.Replace("&gt;", ">"); // 对大于号的处理
                sValue = sValue.Replace("&amp;", "&"); // 对 于符号的处理
                dicDMSConfig[sXMLPath] = sValue;
            }

            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                XmlFileAllNodes(xn.ChildNodes[i], sXMLPath, dicDMSConfig);
            }
        }

        /// <summary>
        /// 取得特定配置项
        /// </summary>
        /// <param name="sXPath">配置文件的XPath路径</param>
        /// <returns>返回特定配置项</returns>
        public string GetGlobalConfigInfo(string sXPath)
        {
            try
            {
                // 合成全局配置
                if (dicAllConfig == null)
                {
                    dicAllConfig = new Dictionary<string, string>();
                    XmlDocument dmsXml = new XmlDocument();
                    dmsXml.Load(sConfigPath + "config\\SqlFileList.config");
                    string sXMLPath = dmsXml.DocumentElement.Name;

                    for (int i = 0; i < dmsXml.DocumentElement.ChildNodes.Count; i++)
                    {
                        XmlFileAllNodes(dmsXml.DocumentElement.ChildNodes[i], sXMLPath, dicAllConfig);
                    }
                }

                if (dicAllConfig.ContainsKey(sXPath))
                {
                    if (dicAllConfig[sXPath] == null)
                    {
                        dicAllConfig[sXPath] = "";
                    }

                    return dicAllConfig[sXPath];
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
