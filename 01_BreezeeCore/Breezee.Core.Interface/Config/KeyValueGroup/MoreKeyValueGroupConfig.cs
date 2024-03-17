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
 * 创建日期：2023/11/24 22:29:28		
 * 对象说明：值列表分组配置，类似值列表数据，即一种类型下有多个值的配置。		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/24 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 更多XML方式的键值表
    /// </summary>
    public class MoreKeyValueGroupConfig
    {
        public DataTable KeyData { get; private set; }
        public DataTable ValData { get; private set; }
        public MoreKeyValueEntity MoreKeyValue { get; private set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDirectory">XML配置所在的目录</param>
        /// <param name="sFileName">XML配置文件名</param>
        /// <param name="lstColKeys">键属性名集合</param>
        /// <param name="lstColValus">值属性名集合</param>
        /// <param name="sColKeyId">键主键列名</param>
        /// <param name="sColKeyId">值主键列名</param>
        /// <param name="sRoot">根目录名</param>
        /// <param name="sChild">子目录名</param>
        /// <param name="saveType">XML配置保存类型：属性或者子项</param>
        public MoreKeyValueGroupConfig(MoreKeyValueEntity moreKeyValueE)
        {
            if (string.IsNullOrEmpty(moreKeyValueE.DirectoryName) || string.IsNullOrEmpty(moreKeyValueE.FileName))
            {
                throw new Exception("传参错误，目录、文件名不能为空！");
            }
            MoreKeyValue = moreKeyValueE;

            if (!MoreKeyValue.ColKeys.Contains(MoreKeyValue.KeyIdPropName)) 
            {
                MoreKeyValue.ColKeys.Add(MoreKeyValue.KeyIdPropName);
            }

            if (!MoreKeyValue.ColVals.Contains(MoreKeyValue.KeyIdPropName))
            {
                MoreKeyValue.ColVals.Add(MoreKeyValue.KeyIdPropName);
            }
            if (!MoreKeyValue.ColVals.Contains(MoreKeyValue.ValIdPropName))
            {
                MoreKeyValue.ColVals.Add(MoreKeyValue.ValIdPropName);
            }

            //不存在目录时创建
            if (!File.Exists(MoreKeyValue.FullFileName))
            {
                if (!Directory.Exists(MoreKeyValue.DirectoryName))
                {
                    Directory.CreateDirectory(MoreKeyValue.DirectoryName);
                }
                //创建一个根实体
                XElement ele = new XElement(MoreKeyValue.XmlRootName);
                ele.Save(MoreKeyValue.FullFileName);
            }
            Load();
        }

        /// <summary>
        /// 加载文件数据
        /// </summary>
        /// <returns></returns>
        private void Load()
        {
            string sFullName = MoreKeyValue.FullFileName;
            KeyData = GenerateKeyDataStruct();
            ValData = GenerateValDataStruct();
            if (!File.Exists(sFullName))
            {
                return;
            }
            FileInfo f = new FileInfo(sFullName);
            f.Attributes = FileAttributes.Normal;//去掉文件的的只读属性

            XDocument XmlDoc = XDocument.Load(sFullName);
            if (MoreKeyValue.XmlSaveType == XmlConfigSaveType.Element)
            {
                //针对键
                foreach (XElement ele in XmlDoc.Root.Elements(MoreKeyValue.KeyXmlNodeName))
                {
                    DataRow row = KeyData.NewRow();
                    foreach (string s in MoreKeyValue.ColKeys)
                    {
                        row[s] = (string)ele.Element(s);
                    }
                    KeyData.Rows.Add(row);
                    //针对值
                    foreach (XElement eleVal in ele.Elements(MoreKeyValue.ValXmlNodeName))
                    {
                        DataRow rowVal = ValData.NewRow();
                        foreach (string s in MoreKeyValue.ColVals)
                        {
                            rowVal[s] = (string)eleVal.Element(s);
                        }
                        ValData.Rows.Add(rowVal);
                    }
                }
            }
            else
            {
                //针对键
                foreach (XElement ele in XmlDoc.Root.Elements(MoreKeyValue.KeyXmlNodeName))
                {
                    DataRow row = KeyData.NewRow();
                    foreach (XAttribute att in ele.Attributes())
                    {
                        foreach (string s in MoreKeyValue.ColKeys)
                        {
                            if (att.Name.ToString().ToLower().Equals(s.ToLower()))
                            {
                                row[s] = att.Value.ToString();
                                break;
                            }
                        }
                    }
                    KeyData.Rows.Add(row);

                    //针对值
                    foreach (XElement eleVal in ele.Elements(MoreKeyValue.ValXmlNodeName))
                    {
                        DataRow rowVal = ValData.NewRow();
                        foreach (XAttribute att in eleVal.Attributes())
                        {
                            foreach (string s in MoreKeyValue.ColVals)
                            {
                                if (att.Name.ToString().ToLower().Equals(s.ToLower()))
                                {
                                    rowVal[s] = att.Value.ToString();
                                    break;
                                }
                            }
                        }
                        ValData.Rows.Add(rowVal);
                    }
                }
            }
        }

        /// <summary>
        /// 保存XML文件方法
        /// </summary>
        public void Save()
        {
            if (KeyData == null)
            {
                KeyData = GenerateKeyDataStruct();
            }
            if (ValData == null)
            {
                ValData = GenerateValDataStruct();
            }

            List<XElement> keyItems = new List<XElement>(KeyData.Rows.Count);
            if (MoreKeyValue.XmlSaveType == XmlConfigSaveType.Element)
            {
                foreach (DataRow row in KeyData.Rows)
                {
                    XElement elChile = new XElement(MoreKeyValue.KeyXmlNodeName);
                    foreach (string s in MoreKeyValue.ColKeys)
                    {
                        elChile.Add(new XElement(s, row[s].ToString()));
                    }
                    
                    //值处理
                    DataRow[] drArr = ValData.Select(MoreKeyValue.KeyIdPropName + "='" + row[MoreKeyValue.KeyIdPropName].ToString() + "'");
                    foreach (DataRow dr in drArr)
                    {
                        XElement elVal = new XElement(MoreKeyValue.ValXmlNodeName);
                        foreach (string s in MoreKeyValue.ColVals)
                        {
                            elVal.Add(new XElement(s, dr[s].ToString()));
                        }
                        elChile.Add(elVal);
                    }

                    keyItems.Add(elChile);
                }
            }
            else
            {
                foreach (DataRow row in KeyData.Rows)
                {
                    XElement elChile = new XElement(MoreKeyValue.KeyXmlNodeName);
                    foreach (string s in MoreKeyValue.ColKeys)
                    {
                        elChile.SetAttributeValue(s, row[s].ToString());
                    }
                    
                    //值处理
                    DataRow[] drArr = ValData.Select(MoreKeyValue.KeyIdPropName + "='" + row[MoreKeyValue.KeyIdPropName].ToString() + "'");
                    foreach (DataRow dr in drArr)
                    {
                        XElement elVal = new XElement(MoreKeyValue.ValXmlNodeName);
                        foreach (string s in MoreKeyValue.ColVals)
                        {
                            elVal.SetAttributeValue(s, dr[s].ToString());
                        }
                        elChile.Add(elVal);
                    }

                    keyItems.Add(elChile);
                }
            }

            //保存文件
            XElement ele = new XElement(MoreKeyValue.XmlRootName, keyItems);
            if (!Directory.Exists(MoreKeyValue.DirectoryName))
            {
                Directory.CreateDirectory(MoreKeyValue.DirectoryName);
            }
            ele.Save(MoreKeyValue.FullFileName);
        }

        /// <summary>
        /// 生成表结构
        /// </summary>
        /// <returns></returns>
        private DataTable GenerateKeyDataStruct()
        {
            KeyData = new DataTable();
            foreach (string s in MoreKeyValue.ColKeys)
            {
                KeyData.Columns.Add(s, typeof(string));
            }
            KeyData.PrimaryKey = new DataColumn[] { KeyData.Columns[MoreKeyValue.KeyIdPropName] };
            if(MoreKeyValue.ValueType == IDValueType.Guid)
            {
                KeyData.Columns[MoreKeyValue.KeyIdPropName].DefaultValue = Guid.NewGuid().ToString();
            }
            else
            {
                KeyData.Columns[MoreKeyValue.KeyIdPropName].AutoIncrement= true;
                KeyData.Columns[MoreKeyValue.KeyIdPropName].AutoIncrementSeed = 1;
                KeyData.Columns[MoreKeyValue.KeyIdPropName].AutoIncrementStep = 1;
            }
            
            return KeyData;
        }

        private DataTable GenerateValDataStruct()
        {
            ValData = new DataTable();
            foreach (string s in MoreKeyValue.ColVals)
            {
                ValData.Columns.Add(s, typeof(string));
            }
            ValData.PrimaryKey = new DataColumn[] { ValData.Columns[MoreKeyValue.ValIdPropName] };
            if (MoreKeyValue.ValueType == IDValueType.Guid)
            {
                ValData.Columns[MoreKeyValue.ValIdPropName].DefaultValue = Guid.NewGuid().ToString();
            }
            else
            {
                ValData.Columns[MoreKeyValue.ValIdPropName].AutoIncrement = true;
                ValData.Columns[MoreKeyValue.ValIdPropName].AutoIncrementSeed = 1;
                ValData.Columns[MoreKeyValue.ValIdPropName].AutoIncrementStep = 1;
            }
            return ValData;
        }

    }
}
