using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.IO;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 对象名称：最小化的XML通用类
    /// 创建作者：黄国辉
    /// 创建日期：2021-08-30
    /// 说明：对固定格式的XML文件维护类。
    ///     感谢水哥的支持！
    ///     注：属性名不能含空格，否则会报【名称不能以“ ”字符(十六进制值 0x20)开头】
    /// </summary>
    public class MiniXmlConfig
    {
        private string _sDirectory; //目录名
        private string _sFileName; //文件名
        private string _sFullFileName; //文件全路径
        private string _sRoot; //根节点
        private string _sElement; //子节点
        private XmlConfigSaveType _saveType;
        private List<string> _lstCol;
        private string _sPK;
        private DataTable _dtData;

        public DataTable Data { get => _dtData;  }
        public string DirName { get => _sDirectory;  }
        public string FileName { get => _sFileName; }
        public string FullFileName { get => _sFullFileName; }
        public List<string> Columns { get => _lstCol; }
        public XmlConfigSaveType SaveType { get => _saveType; }
        /// <summary>
        /// 主键列名
        /// </summary>
        public string PK { get => _sPK; }
        /// <summary>
        /// 根节点名称
        /// </summary>
        public string Root { get => _sRoot; }
        /// <summary>
        /// 子节点名称
        /// </summary>
        public string Element { get => _sElement; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDirectory">XML配置所在的目录</param>
        /// <param name="sFileName">XML配置文件名</param>
        /// <param name="column">属性名或子项名数组</param>
        /// <param name="sPK">主键</param>
        /// <param name="sRoot">根目录名</param>
        /// <param name="sChild">子目录名</param>
        /// <param name="saveType">XML配置保存类型：属性或者子项</param>
        public MiniXmlConfig(string sDirectory, string sFileName,List<string> lstCol, string sPK, string sRoot="items",string sChild="item", XmlConfigSaveType saveType = XmlConfigSaveType.Attribute)
        {
            if(string.IsNullOrEmpty(sDirectory) || string.IsNullOrEmpty(sDirectory) || lstCol == null || lstCol.Count==0)
            {
                throw new Exception("传参错误，目录、文件名、列集合不能为空！");
            }
            _sDirectory = sDirectory;
            _sFileName = sFileName;
            _sFullFileName = Path.Combine(_sDirectory, sFileName);
            _lstCol = lstCol;
            if (!_lstCol.Contains(sPK))
            {
                _lstCol.Add(sPK);
            }
            _sPK = sPK;
            _sRoot = sRoot;
            _sElement = sChild;
            _saveType = saveType;
            //不存在目录时创建
            if (!Directory.Exists(sDirectory))
            {
                if (!Directory.Exists(_sDirectory))
                {
                    Directory.CreateDirectory(_sDirectory);
                }
                //创建一个根实体
                XElement ele = new XElement(_sRoot);
                ele.Save(_sFullFileName);
            }
        }

        /// <summary>
        /// 加载文件数据
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            _dtData = GenerateDataStruct();
            if (!File.Exists(_sFullFileName))
            {
                return _dtData;
            }
            FileInfo f = new FileInfo(_sFullFileName);
            f.Attributes = FileAttributes.Normal;//去掉文件的的只读属性

            XDocument doc = XDocument.Load(_sFullFileName);
            if (_saveType == XmlConfigSaveType.Element)
            {
                foreach (XElement ele in doc.Root.Elements(_sElement))
                {
                    DataRow row = _dtData.NewRow();
                    foreach (string s in _lstCol)
                    {
                        row[s] = (string)ele.Element(s);
                    }
                    _dtData.Rows.Add(row);
                }
            }
            else
            {
                foreach (XElement ele in doc.Root.Elements(_sElement))
                {
                    DataRow row = _dtData.NewRow();
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
                    _dtData.Rows.Add(row);
                }
            }
            return _dtData;
        }

        /// <summary>
        /// 保存XML文件方法
        /// </summary>
        /// <param name="dtItems"></param>
        public void Save(DataTable dtItems = null)
        {
            if (dtItems == null)
            {
                dtItems = _dtData == null ? GenerateDataStruct() : _dtData;
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
            if (!Directory.Exists(_sDirectory))
            {
                Directory.CreateDirectory(_sDirectory);
            }
            ele.Save(_sFullFileName);
        }

        /// <summary>
        /// 生成表结构
        /// </summary>
        /// <returns></returns>
        private DataTable GenerateDataStruct()
        {
            _dtData = new DataTable();
            foreach (string s in _lstCol)
            {
                _dtData.Columns.Add(s, typeof(string));
            }
            _dtData.PrimaryKey = new DataColumn[] { _dtData.Columns[_sPK] };
            _dtData.Columns[_sPK].DefaultValue = Guid.NewGuid().ToString();
            return _dtData;
        }
    }
}
