using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*********************************************************************		
 * 对象名称：键值对配置	
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：使用属性方式		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class KeyValuePairConfig
    {
        public XDocument Doc;
        public KeyValuePairConfig(string sPath)
        {
            if (File.Exists(sPath))
            {
                FileInfo f = new FileInfo(sPath);
                f.Attributes = FileAttributes.Normal;//去掉文件的的只读属性

                Doc = XDocument.Load(sPath);
            }
            else
            {
                Doc = new XDocument();
                Doc.Declaration = new XDeclaration("1.0", "utf-8","no");
                XElement xRoot = new XElement(XmlKeyValueStr.Root);// 添加根节点
                Doc.Add(xRoot);
                Doc.Save(sPath);
            }
        }

        public DataTable Get()
        {
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add(XmlKeyValueStr.KeyProp.Key);
            dtKeys.Columns.Add(XmlKeyValueStr.KeyProp.Value);
            dtKeys.Columns.Add(XmlKeyValueStr.KeyProp.Comment);

            foreach (XElement e in Doc.Root.Elements())
            {
                DataRow dr = dtKeys.NewRow();

                dr[XmlKeyValueStr.KeyProp.Key] = e.Attribute(XmlKeyValueStr.KeyProp.Key).Value;
                dr[XmlKeyValueStr.KeyProp.Value] = e.Attribute(XmlKeyValueStr.KeyProp.Value).Value;
                dr[XmlKeyValueStr.KeyProp.Comment] = e.Attribute(XmlKeyValueStr.KeyProp.Comment).Value;
                dtKeys.Rows.InsertAt(dr,0);
            }
            return dtKeys;
        }

        public void Save(DataTable dtItems, string strFileName)
        {
            if (dtItems == null)
            {
                return;
            }
            List<XElement> keyItems = new List<XElement>(dtItems.Rows.Count);
            foreach (DataRow row in dtItems.Rows)
            {
                keyItems.Add
                (
                    new XElement
                    (
                        XmlKeyValueStr.Data,
                        new XAttribute(XmlKeyValueStr.KeyProp.Key, row[XmlKeyValueStr.KeyProp.Key].ToString()),
                        new XAttribute(XmlKeyValueStr.KeyProp.Value, row[XmlKeyValueStr.KeyProp.Value].ToString()),
                        new XAttribute(XmlKeyValueStr.KeyProp.Comment, row[XmlKeyValueStr.KeyProp.Comment].ToString())
                    )
                );
            }

            XElement ele = new XElement(XmlKeyValueStr.Root, keyItems);
            ele.Save(strFileName);
        }

        public static class XmlKeyValueStr
        {
            public static string Root = "config";
            public static string Data = "data";
            public static class KeyProp
            {
                public static string Key = "key";
                public static string Value = "value";
                public static string Comment = "comment";
            }

        }
    }
}
