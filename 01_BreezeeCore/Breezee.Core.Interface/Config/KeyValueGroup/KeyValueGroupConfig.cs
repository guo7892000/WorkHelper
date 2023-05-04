using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*********************************************************************		
 * 对象名称：分组键值XML配置
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：值列表分组配置，类似值列表数据，即一种类型下有多个值的配置。		
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
    public class KeyValueGroupConfig
    {
        public XDocument Doc;
        public KeyValueGroupConfig(string sPath)
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
                XElement xRoot = new XElement(KeyValueGroupString.Root);// 添加根节点
                Doc.Add(xRoot);
                Doc.Save(sPath);
            }
        }

        public DataTable GetKeys()
        {
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add(KeyValueGroupString.KeyProp.Kid);
            dtKeys.Columns.Add(KeyValueGroupString.KeyProp.Name);

            foreach (XElement e in Doc.Root.Elements())
            {
                DataRow dr = dtKeys.NewRow();
                dr[KeyValueGroupString.KeyProp.Kid] = e.Attribute(KeyValueGroupString.KeyProp.Kid).Value;
                dr[KeyValueGroupString.KeyProp.Name] = e.Attribute(KeyValueGroupString.KeyProp.Name).Value;

                dtKeys.ImportRow(dr);
            }
            return dtKeys;
        }

        public DataTable GetValues(string sKey)
        {
            DataTable dtValues = new DataTable();
            dtValues.Columns.Add(KeyValueGroupString.ValueProp.Kid);
            dtValues.Columns.Add(KeyValueGroupString.ValueProp.Vid);
            dtValues.Columns.Add(KeyValueGroupString.ValueProp.Name);

            IEnumerable<XElement> key = Doc.Root.Elements(KeyValueGroupString.Key).Where(t => t.Attribute(KeyValueGroupString.KeyProp.Kid).Value.Equals(sKey));
            if(key.Count()>0)
            {
                foreach (XElement e in key.Elements())
                {
                    DataRow dr = dtValues.NewRow();
                    dr[KeyValueGroupString.ValueProp.Kid] = e.Parent.Attribute(KeyValueGroupString.ValueProp.Kid).Value;
                    dr[KeyValueGroupString.ValueProp.Vid] = e.Attribute(KeyValueGroupString.ValueProp.Vid).Value;
                    dr[KeyValueGroupString.ValueProp.Name] = e.Attribute(KeyValueGroupString.ValueProp.Name).Value;
                    dtValues.Rows.Add(dr);
                }
            }
            return dtValues;
        }
    }
}
