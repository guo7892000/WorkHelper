using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Breezee.Core.Interface;

namespace Breezee.Core
{
    public class XmlDll:IDll
    {
        private string _sPath="";
        private IDictionary<string, DllEntity> _DicDll;
        XmlDocument xmlMenu = new XmlDocument();
        public XmlDll(string sPath)
        {
            _sPath=sPath;
        }
        public IDictionary<string, DllEntity> DicDll => _DicDll;
        /// <summary>
        /// 节点变为菜单
        /// </summary>
        public void Init()
        {
            _DicDll = new Dictionary<string, DllEntity>();
            xmlMenu.Load(_sPath);
            var xmlNodeList = xmlMenu.SelectNodes("xml/Dll");
            foreach (XmlNode xnModel in xmlNodeList)
            {
                if (xnModel.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                DllEntity dMenu = new DllEntity();
                dMenu.Guid = xnModel.GetAttributeValue(DllAttrString.Guid);
                dMenu.Name = xnModel.GetAttributeValue(DllAttrString.Name);
                dMenu.Code = xnModel.GetAttributeValue(DllAttrString.Code);
                _DicDll[dMenu.Guid] = dMenu;
            }
        }

    }

    public class DllAttrString
    {
        public static readonly string Guid = "Guid";
        public static readonly string Name = "Name";
        public static readonly string Code = "Code";
    }
}
