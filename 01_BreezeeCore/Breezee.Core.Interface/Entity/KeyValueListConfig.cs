using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
    /// XML方式的键值表
    /// </summary>
    public class KeyValueListConfig
    {
        public XDocument Doc;
        public KeyValueListConfig(string sPath)
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

        public DataTable GetKeys()
        {
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add(XmlKeyValueStr.KeyProp.Kid);
            dtKeys.Columns.Add(XmlKeyValueStr.KeyProp.Name);

            foreach (XElement e in Doc.Root.Elements())
            {
                DataRow dr = dtKeys.NewRow();
                dr[XmlKeyValueStr.KeyProp.Kid] = e.Attribute(XmlKeyValueStr.KeyProp.Kid).Value;
                dr[XmlKeyValueStr.KeyProp.Name] = e.Attribute(XmlKeyValueStr.KeyProp.Name).Value;

                dtKeys.ImportRow(dr);
            }
            return dtKeys;
        }

        public DataTable GetValues(string sKey)
        {
            DataTable dtValues = new DataTable();
            dtValues.Columns.Add(XmlKeyValueStr.ValueProp.Kid);
            dtValues.Columns.Add(XmlKeyValueStr.ValueProp.Vid);
            dtValues.Columns.Add(XmlKeyValueStr.ValueProp.Name);

            IEnumerable<XElement> key = Doc.Root.Elements(XmlKeyValueStr.Key).Where(t => t.Attribute(XmlKeyValueStr.KeyProp.Kid).Value.Equals(sKey));
            if(key.Count()>0)
            {
                foreach (XElement e in key.Elements())
                {
                    DataRow dr = dtValues.NewRow();
                    dr[XmlKeyValueStr.ValueProp.Kid] = e.Parent.Attribute(XmlKeyValueStr.ValueProp.Kid).Value;
                    dr[XmlKeyValueStr.ValueProp.Vid] = e.Attribute(XmlKeyValueStr.ValueProp.Vid).Value;
                    dr[XmlKeyValueStr.ValueProp.Name] = e.Attribute(XmlKeyValueStr.ValueProp.Name).Value;
                    dtValues.Rows.Add(dr);
                }
            }
            return dtValues;
        }

        public static class XmlKeyValueStr
        {
            public static string Root = "xml";
            public static string Key = "key";
            public static string Value = "value";
            public static class KeyProp
            {
                public static string Kid = "kid";
                public static string Name = "name";
            }
            public static class ValueProp
            {
                public static string Kid = "kid";
                public static string Vid = "vid";
                public static string Name = "name";
            }
        }
    }
}
