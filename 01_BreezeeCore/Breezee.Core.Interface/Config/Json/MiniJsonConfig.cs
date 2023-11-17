using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static Breezee.Core.Interface.KeyValuePairString;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 对象名称：最小化的Json通用类
    /// 创建作者：黄国辉
    /// 创建日期：2023-11-08
    /// 说明：对固定格式的JSON文件维护类。
    /// </summary>
    public class MiniJsonConfig
    {
        private string _sDirectory; //目录名
        private string _sFileName; //文件名
        private string _sFullFileName; //文件全路径

        private XmlConfigSaveType _saveType;
        private IList<string> _lstCol;
        private string _sPK;
        private DataTable _dtData;
        private JArray jsonArray;

        public DataTable Data { get => _dtData;  }
        public string DirName { get => _sDirectory;  }
        public string FileName { get => _sFileName; }
        public string FullFileName { get => _sFullFileName; }
        public IList<string> Columns { get => _lstCol; }
        public XmlConfigSaveType SaveType { get => _saveType; }
        /// <summary>
        /// 主键列名
        /// </summary>
        public string PK { get => _sPK; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDirectory">XML配置所在的目录</param>
        /// <param name="sFileName">XML配置文件名</param>
        /// <param name="column">属性名或子项名数组</param>
        /// <param name="sPK">主键</param>
        public MiniJsonConfig(string sDirectory, string sFileName,IList<string> lstCol, string sPK="ID")
        {
            if(string.IsNullOrEmpty(sDirectory) || string.IsNullOrEmpty(sDirectory) || lstCol == null || lstCol.Count==0)
            {
                throw new Exception("传参错误，目录、文件名、属性集合不能为空！");
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
            //不存在目录时创建
            if (!Directory.Exists(sDirectory))
            {
                if (!Directory.Exists(_sDirectory))
                {
                    Directory.CreateDirectory(_sDirectory);
                }
                //不存在，就创建
                string sNewArr = "[]";
                File.WriteAllText(_sFullFileName, sNewArr);
                jsonArray = new JArray(sNewArr);
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

            string sFileContent = File.ReadAllText(_sFullFileName);
            jsonArray = JsonConvert.DeserializeObject<JArray>(sFileContent);

            foreach (JObject obj in jsonArray)
            {
                DataRow row = _dtData.NewRow();
                foreach (string s in _lstCol)
                {
                    row[s] = obj[s];
                }
                _dtData.Rows.Add(row);
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

            jsonArray = new JArray();
            foreach (DataRow row in dtItems.Rows)
            {
                JObject jo = new JObject();
                foreach (DataColumn col in dtItems.Columns)
                {
                    jo.Add(new JProperty(col.ColumnName, row[col]));
                }
                jsonArray.Add(jo);
            }

            string sResult = jsonArray.ToString().Trim();
            File.WriteAllText(_sFullFileName, sResult);
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
