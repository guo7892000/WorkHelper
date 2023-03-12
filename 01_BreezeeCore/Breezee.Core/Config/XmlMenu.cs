using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/19 17:57:56	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/19 17:57:56 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 类
    /// </summary>
    public class XmlMenu : Menu
    {
        private string _sPath = "";
        private XmlDocument xmlMenu =new XmlDocument();
        public XmlDocument XmlMenus { get { return xmlMenu; } }
        public string XmlPath { get { return _sPath; } }

        AppType _at;
        private MenuEntity _Root = new MenuEntity();
        private IDictionary<string, MenuEntity> _Childs = new Dictionary<string, MenuEntity>();
        public static readonly string FRA_FULL_MENU_PATH_SPLIT_CHAR = " > ";
        public XmlMenu(string sPath,AppType at= AppType.WinForm)
        {
            _sPath = sPath;
            _at = at;
            xmlMenu.Load(sPath);
        }
        public override MenuEntity Root { get => _Root; }
        public override IDictionary<string, MenuEntity> Childs { get => (IDictionary<string, MenuEntity>)_Childs; }
        public override MenuConfigType MenuConfigType { get => MenuConfigType.Xml; }

        public override void Init()
        {
            _Root = new MenuEntity();
            _Childs = new Dictionary<string, MenuEntity>();

        }

        #region 增加所有菜单
        public IDictionary<string, MenuEntity> GetAllMenu()
        {
            var dicAllMenu = _Childs;
            XmlNodeList xmlList = xmlMenu.SelectNodes("xml/Model");

            foreach (XmlNode xnModel in xmlList)
            {
                if (xnModel.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                MenuEntity dMenu = NodeToMenu(xnModel, MenuType.Modul, string.Empty);
                dicAllMenu[dMenu.Guid] = dMenu;

                foreach (XmlNode xnClass in xnModel.ChildNodes)
                {
                    if (xnClass.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }
                    if (xnClass.Name == MenuConfigString.Class)
                    {
                        MenuEntity dMenuChild = NodeToMenu(xnClass, MenuType.Class, dMenu.FullPath);
                        dicAllMenu[dMenuChild.Guid] = dMenuChild;
                        dMenu.Childs[dMenuChild.Guid] = dMenuChild;
                        //增加子菜单分类
                        AddChildMenuClass(xnClass, dMenuChild, dicAllMenu);
                    }
                    else
                    {
                        MenuEntity dMenuChild = NodeToMenu(xnClass, MenuType.Menu, dMenu.FullPath);
                        dicAllMenu[dMenuChild.Guid] = dMenuChild;
                        dMenu.Childs[dMenuChild.Guid] = dMenuChild;
                    }
                }
            }

            return dicAllMenu;
        }
        #endregion

        #region 节点变为菜单
        public MenuEntity NodeToMenu(XmlNode xnModel, MenuType mte, string parentMenuPath)
        {
            MenuEntity dMenu = new MenuEntity();
            dMenu.MenuType = mte;
            dMenu.Guid = xnModel.GetAttributeValue(MemuAttrString.Guid);
            dMenu.Name = xnModel.GetAttributeValue(MemuAttrString.Name);
            dMenu.Code = xnModel.GetAttributeValue(MemuAttrString.Code);
            dMenu.ShortCutKey = xnModel.GetAttributeValue(MemuAttrString.ShortCutKey);
            dMenu.DLLName = xnModel.GetAttributeValue(MemuAttrString.DLLName);
            dMenu.FormName = xnModel.GetAttributeValue(MemuAttrString.FormName);
            dMenu.HelpPath = xnModel.GetAttributeValue(MemuAttrString.HelpPath);
            if (!string.IsNullOrEmpty(parentMenuPath))
            {
                dMenu.FullPath = parentMenuPath + FRA_FULL_MENU_PATH_SPLIT_CHAR + dMenu.Name;
            }
            else
            {
                dMenu.FullPath = dMenu.Name;
            }
            return dMenu;
        }
        #endregion

        #region 增加子菜单分类
        /// <summary>
        /// 增加子菜单分类
        /// </summary>
        /// <param name="xParentNode"></param>
        /// <param name="dParentMenu"></param>
        /// <param name="allMenu"></param>
        private void AddChildMenuClass(XmlNode xParentNode, MenuEntity dParentMenu, IDictionary<string, MenuEntity> allMenu)
        {
            foreach (XmlNode xnChild in xParentNode.ChildNodes)
            {
                if (xnChild.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                if (xnChild.Name == MenuConfigString.Class)
                {
                    MenuEntity dMenuChild = NodeToMenu(xnChild, MenuType.Class, dParentMenu.FullPath);
                    dParentMenu.Childs[dMenuChild.Guid] = dMenuChild;
                    allMenu[dMenuChild.Guid] = dMenuChild;
                }
                else
                {
                    MenuEntity dMenuChild = NodeToMenu(xnChild, MenuType.Menu, dParentMenu.FullPath);
                    dParentMenu.Childs[dMenuChild.Guid] = dMenuChild;
                    allMenu[dMenuChild.Guid] = dMenuChild;
                }
            }
        }
        #endregion
    }
}