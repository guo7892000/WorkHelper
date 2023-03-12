using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.IO;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 对象名称：迷你XML通用类
    /// 创建作者：黄国辉
    /// 创建日期：2021-08-30
    /// 说明：对固定格式的XML文件维护类。
    ///     感谢水哥的支持！
    /// </summary>
    public class MiniXmlCommon
    {
        private string _sFileName;
        private string _sRoot; //根节点
        private string _sElement; //子节点
        private XmlConfigSaveType _saveType;
        private List<string> _lstCol;
        private string _sPK;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sPath">XML文件路径</param>
        /// <param name="column">属性名或子项名数组：第一个作为主键</param>
        /// <param name="sRoot">根目录名</param>
        /// <param name="sChild">子目录名</param>
        /// <param name="saveType">XML配置保存类型：属性或者子项</param>
        public MiniXmlCommon(string sPath,List<string> lstCol, string sPK, string sRoot="items",string sChild="item", XmlConfigSaveType saveType = XmlConfigSaveType.Attribute)
        {
            if(string.IsNullOrEmpty(sPath) || lstCol == null || lstCol.Count==0)
            {
                throw new Exception("传参错误，路径、列集合不能为空！");
            }

            _sFileName = sPath;
            _lstCol = lstCol;
            if(!_lstCol.Contains(sPK))
            {
                _lstCol.Add(sPK);
            }
            _sPK = sPK;
            _sRoot = sRoot;
            _sElement = sChild;
            _saveType = saveType;
        }

        /// <summary>
        /// 获取配置文件方法
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return _sFileName;
        }
        
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <returns></returns>
        public DataTable LoadXMLFile()
        {
            DataTable dt = GenerateDataStruct();
            if (!File.Exists(_sFileName))
            {
                return dt;
            }
            FileInfo f = new FileInfo(_sFileName);
            f.Attributes = FileAttributes.Normal;//去掉文件的的只读属性

            XDocument doc = XDocument.Load(_sFileName);
            if (_saveType == XmlConfigSaveType.Element)
            {
                foreach (XElement ele in doc.Root.Elements(_sElement))
                {
                    DataRow row = dt.NewRow();
                    foreach (string s in _lstCol)
                    {
                        row[s] = (string)ele.Element(s);
                    }
                    dt.Rows.Add(row);
                }
            }
            else
            {
                foreach (XElement ele in doc.Root.Elements(_sElement))
                {
                    DataRow row = dt.NewRow();
                    foreach (XAttribute att in ele.Attributes())
                    {
                        foreach (string s in _lstCol)
                        {
                            if (att.Name.ToString().ToLower().Equals(s.ToLower()))
                            {
                                row[s] = att.Value.ToString();
                                break;
                            }
                        }

                    }
                    dt.Rows.Add(row);
                }
                
            }
            return dt;
        }

        /// <summary>
        /// 生成表结构
        /// </summary>
        /// <returns></returns>
        public DataTable GenerateDataStruct()
        {
            DataTable dt = new DataTable();
            foreach (string s in _lstCol)
            {
                dt.Columns.Add(s, typeof(string));
            }
            dt.PrimaryKey = new DataColumn[] { dt.Columns[_sPK] };
            return dt;
        }

        /// <summary>
        /// 保存XML文件方法
        /// </summary>
        /// <param name="dtItems"></param>
        public void SaveXMLFile(DataTable dtItems, string strFileName)
        {
            if (dtItems == null)
            {
                return;
            }
            List<XElement> keyItems = new List<XElement>(dtItems.Rows.Count);
            if (_saveType == XmlConfigSaveType.Element)
            {
                foreach (DataRow row in dtItems.Rows)
                {
                    XElement elChile = new XElement(_sElement);
                    foreach (string s in _lstCol)
                    {
                        elChile.Add(new XElement(s, row[s].ToString()));
                    }
                    keyItems.Add(elChile);
                }
            }
            else
            {
                foreach (DataRow row in dtItems.Rows)
                {
                    XElement elChile = new XElement(_sElement);
                    foreach (string s in _lstCol)
                    {
                        elChile.SetAttributeValue(s, row[s].ToString());
                    }
                    keyItems.Add(elChile);
                }
            }

            XElement ele = new XElement(_sRoot, keyItems);
            ele.Save(strFileName);
        }
    }

    public enum XmlConfigSaveType
    {
        /// <summary>
        /// 属性方式保存
        /// </summary>
        Attribute,
        /// <summary>
        /// 子对象方式
        /// </summary>
        Element
    }
}
