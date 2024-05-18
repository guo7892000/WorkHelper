using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Breezee.Core.Interface;
using Breezee.Core;
using System.IO;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public partial class FrmMenuManage : BaseForm
    {
        #region 变量
        IDictionary<string, MenuEntity> _MenuDic = new Dictionary<string, MenuEntity>();
        MenuEntity _saveMenu;
        XmlMenu _xmlMenu;
        private string sDllPath;
        private XmlDll _XmlDll;
        public static string MenuXmlFilePath;
        public static string MenuXmlFilePath_WPF;
        ShowInToolStripXmlConfig _stripXmlConfig;
        #endregion

        #region 构造函数
        public FrmMenuManage()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体加载
        private void FrmMenuManage_Load(object sender, EventArgs e)
        {
            sDllPath = Path.Combine(GlobalContext.AppEntryAssemblyPath, MiniStaticString.ConfigDataPath, MiniStaticString.DllFileName);
            MenuXmlFilePath = Path.Combine(GlobalContext.AppEntryAssemblyPath, MiniStaticString.ConfigDataPath, MiniStaticString.MenuFileName);
            MenuXmlFilePath_WPF = Path.Combine(GlobalContext.AppEntryAssemblyPath, "Config", MiniStaticString.MenuFileName_WPF);
            _saveMenu = new MenuEntity();
            _xmlMenu =  new XmlMenu(MenuXmlFilePath);
            _stripXmlConfig = new ShowInToolStripXmlConfig(MiniGlobalValue.ShowInToolStripXmlConfigFileName);

            _dicQuery.Clear();
            _dicQuery.Add(((int)MenuType.Modul).ToString(), "模块");
            _dicQuery.Add(((int)MenuType.Class).ToString(), "菜单分类");
            _dicQuery.Add(((int)MenuType.Menu).ToString(), "功能");
            cbbMenuType.BindTypeValueDropDownList(_dicQuery.GetTextValueTable(false), false, true);
            //
            _dicQuery.Clear();
            _XmlDll = new XmlDll(sDllPath);
            _XmlDll.Init();

            foreach (var item in _XmlDll.DicDll.Values)
            {
                _dicQuery[item.Code] = item.Code;
            }
            cbbDLL.BindTypeValueDropDownList(_dicQuery.GetTextValueTable(true), false, true);
            //
            cbbMenuType.Enabled = false;
            
            LoadMenu();
        } 
        #endregion

        #region 加载菜单方法
        private void LoadMenu()
        {
            //加载菜单
            tvLeftMenu.Nodes.Clear();
            _MenuDic = _xmlMenu.GetAllMenu();
            foreach (MenuEntity dMenu in _MenuDic.Values)
            {
                if (dMenu.MenuType != MenuType.Modul)
                {
                    continue;
                }

                //左边树
                TreeNode tnNew = new TreeNode();
                tnNew.Name = dMenu.Guid;
                tnNew.Text = dMenu.Name;
                tnNew.Tag = dMenu;
                tvLeftMenu.Nodes.Add(tnNew);

                foreach (MenuEntity childMenu in dMenu.Childs.Values)
                {
                    if (childMenu.MenuType == MenuType.Class)
                    {
                        AddMenuClassNode(tnNew, new TreeNode(), childMenu);
                    }
                    else
                    {
                        AddMenuNode(tnNew, new TreeNode(), childMenu);
                    }
                }
            }
        }
        #endregion

        #region 增加菜单分类
        private void AddMenuClassNode(TreeNode tnParent, TreeNode tnNew, MenuEntity dParentMenu)
        {
            AddMenuNode(tnParent, tnNew, dParentMenu);
            foreach (MenuEntity childMenu in dParentMenu.Childs.Values)
            {
                if (childMenu.MenuType == MenuType.Class)
                {
                    AddMenuClassNode(tnNew, new TreeNode(), childMenu);
                }
                else
                {
                    AddMenuNode(tnNew, new TreeNode(), childMenu);
                }
            }
        }
        #endregion

        #region 增加菜单
        private void AddMenuNode(TreeNode tnParent, TreeNode tnNew, MenuEntity dMenu)
        {
            if(dMenu.MenuType == MenuType.Menu)
            {
                dMenu.IsShowInToolStrip = "1".Equals(_stripXmlConfig.MoreXmlConfig.Get(dMenu.Guid, "0")) ? true : false;
            }
            tnNew.Text = dMenu.Name;
            tnNew.Tag = dMenu;
            tnParent.Nodes.Add(tnNew);
        }
        #endregion

        #region 保存按钮事件
        private void tsbSave_Click(object sender, EventArgs e)
        {
            _saveMenu.Name = txbMenuName.Text.Trim();
            _saveMenu.Code = txbMenuCode.Text.Trim();
            _saveMenu.DLLName = cbbDLL.Text;
            _saveMenu.FormName = txbClassFullPath.Text.Trim();
            _saveMenu.ShortCutKey = txbShortCutKey.Text.Trim();
            _saveMenu.HelpPath = txbHelpPath.Text.Trim();
            _saveMenu.IsShowInToolStrip = ckbShowInToolstrip.Checked;
            if (string.IsNullOrEmpty(_saveMenu.Name))
            {
                ShowErr("【菜单名称】不能为空！");
                return;
            }
            bool isAdd = false;
            string sMenuId = string.Empty;
            XmlDocument MenuEntity = _xmlMenu.XmlMenus;
            if (string.IsNullOrEmpty(_saveMenu.Guid))
            {
                isAdd = true;
                sMenuId = Guid.NewGuid().ToString();
            }
            else
            {
                sMenuId = _saveMenu.Guid;
            }

            #region 新增
            XmlElement xnNew;
            switch (_saveMenu.MenuType)
            {
                case MenuType.Modul:
                    xnNew = MenuEntity.CreateElement("Model");
                    if (isAdd)
                    {
                        xnNew.SetAttribute(MemuAttrString.Guid, sMenuId);
                        MenuEntity.DocumentElement.AppendChild(xnNew);
                    }
                    else
                    {
                        xnNew = MenuEntity.SelectSingleNode("xml/Model[@Guid='" + _saveMenu.Guid + "']") as XmlElement;
                    }
                    break;
                case MenuType.Class:
                    xnNew = MenuEntity.CreateElement("Class");
                    if (isAdd)
                    {
                        xnNew.SetAttribute(MemuAttrString.Guid, sMenuId);
                        //先找模块
                        XmlNode xnParent = MenuEntity.SelectSingleNode("xml/Model[@Guid='" + _saveMenu.ParentGuid + "']");
                        if (xnParent != null)
                        {
                            xnParent.AppendChild(xnNew);
                        }
                    }
                    if (xnNew.ParentNode == null)
                    {
                        //第一个分类
                        GetClassNode(isAdd, MenuEntity, "xml/Model/Class", ref xnNew);
                    }

                    if (xnNew.ParentNode == null)
                    {
                        //第二个菜单分类
                        GetClassNode(isAdd, MenuEntity, "xml/Model/Class/Class", ref xnNew);
                    }

                    if (xnNew.ParentNode == null)
                    {
                        //最多支持第三个菜单分类
                        GetClassNode(isAdd, MenuEntity, "xml/Model/Class/Class/Class", ref xnNew);
                    }
                    break;
                case MenuType.Menu:
                default:
                    if (string.IsNullOrEmpty(_saveMenu.DLLName))
                    {
                        ShowErr("【所在DLL文件】不能为空！");
                        return;
                    }
                    if (string.IsNullOrEmpty(_saveMenu.FormName))
                    {
                        ShowErr("【全路径类名】不能为空！");
                        return;
                    }

                    xnNew = MenuEntity.CreateElement("Menu");
                    if (isAdd)
                    {
                        xnNew.SetAttribute(MemuAttrString.Guid, sMenuId);
                    }
                    //第一个分类
                    GetMenuNode(isAdd, MenuEntity, "xml/Model/Class","xml /Model/Class/Menu", ref xnNew);

                    if (xnNew.ParentNode == null)
                    {
                        //第二个菜单分类
                        GetMenuNode(isAdd, MenuEntity, "xml/Model/Class/Class", "xml/Model/Class/Class/Menu", ref xnNew);
                    }

                    if (xnNew.ParentNode == null)
                    {
                        //最多支持第三个菜单分类
                        GetMenuNode(isAdd, MenuEntity, "xml/Model/Class/Class/Class", "xml/Model/Class/Class/Class/Menu", ref xnNew);
                    }
                    break;
            }
            #endregion

            xnNew.SetAttribute(MemuAttrString.Name, _saveMenu.Name);
            xnNew.SetAttribute(MemuAttrString.Code, _saveMenu.Code);
            xnNew.SetAttribute(MemuAttrString.DLLName, _saveMenu.DLLName);
            xnNew.SetAttribute(MemuAttrString.FormName, _saveMenu.FormName);
            xnNew.SetAttribute(MemuAttrString.ShortCutKey, _saveMenu.ShortCutKey);
            xnNew.SetAttribute(MemuAttrString.HelpPath, _saveMenu.HelpPath);
            xnNew.SetAttribute(MemuAttrString.ShowInToolStrip, _saveMenu.IsShowInToolStrip ? "1" : "0");
            if (_saveMenu.MenuType== MenuType.Menu)
            {
                _stripXmlConfig.MoreXmlConfig.Set(sMenuId, _saveMenu.IsShowInToolStrip ? "1" : "0","工具栏是否显示");
                _stripXmlConfig.MoreXmlConfig.Save();
            }
            //保存
            MenuEntity.Save(_xmlMenu.XmlPath);
            ShowInfo("保存成功！");
            UIHelper.ResetControl(grpEdit);
            //重新加载菜单
            LoadMenu();
        }
        #endregion

        #region 获取菜单分类节点
        private void GetClassNode(bool isAdd, XmlDocument MenuEntity, string path, ref XmlElement xnNew)
        {
            XmlNodeList xmlList = MenuEntity.SelectNodes(path);
            foreach (XmlNode xn in xmlList)
            {
                MenuEntity xnMenu = _xmlMenu.NodeToMenu(xn, MenuType.Class, "");

                if (isAdd)
                {
                    if (xnMenu.Guid == _saveMenu.ParentGuid)
                    {
                        xn.AppendChild(xnNew);
                        break;
                    }
                }
                else
                {
                    if (xnMenu.Guid == _saveMenu.Guid)
                    {
                        xnNew = xn as XmlElement;
                        break;
                    }
                }
            }
        }
        #endregion

        #region 获取菜单节点
        private void GetMenuNode(bool isAdd, XmlDocument MenuEntity, string AddPath, string ModifyPath, ref XmlElement xnNew)
        {
            XmlNodeList xmlList = MenuEntity.SelectNodes(isAdd==true? AddPath : ModifyPath);
            foreach (XmlNode xn in xmlList)
            {
                MenuEntity xnMenu = _xmlMenu.NodeToMenu(xn, MenuType.Menu, "");

                if (isAdd)
                {
                    if (xnMenu.Guid == _saveMenu.ParentGuid)
                    {
                        xn.AppendChild(xnNew);
                        break;
                    }
                }
                else
                {
                    if (xnMenu.Guid == _saveMenu.Guid)
                    {
                        xnNew = xn as XmlElement;
                        break;
                    }
                }
            }
        } 
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion

        #region 树选择后事件
        private void tvLeftMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode trSelect = e.Node;
            if (trSelect == null)
            {
                tsmiAddModel.Visible = true;
                tsmiAddClass.Visible = false;
                tsmiAddFunc.Visible = false;
                return;
            }
            MenuEntity selectMenu = trSelect.Tag as MenuEntity;
            switch (selectMenu.MenuType)
            {
                case MenuType.Modul:
                    tsmiAddModel.Visible = true;
                    tsmiAddClass.Visible = true;
                    tsmiAddFunc.Visible = false;
                    break;
                case MenuType.Class:
                    tsmiAddModel.Visible = false;
                    tsmiAddClass.Visible = true;
                    tsmiAddFunc.Visible = true;
                    break;
                case MenuType.Menu:
                default:
                    tsmiAddModel.Visible = false;
                    tsmiAddClass.Visible = false;
                    tsmiAddFunc.Visible = false;
                    break;
            }
            //要保存的为当前选择的菜单
            _saveMenu = selectMenu;
            //绑定界面数据
            cbbMenuType.SelectedValue = ((int)_saveMenu.MenuType).ToString();
            txbMenuName.Text = _saveMenu.Name;
            txbMenuCode.Text = _saveMenu.Code;
            cbbDLL.Text = _saveMenu.DLLName;
            txbClassFullPath.Text = _saveMenu.FormName;
            txbShortCutKey.Text = _saveMenu.ShortCutKey;
            txbHelpPath.Text = _saveMenu.HelpPath;
            ckbShowInToolstrip.Checked = _saveMenu.IsShowInToolStrip;
        }
        #endregion

        #region 菜单类型选择变化事件
        private void cbbMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMenuType.SelectedValue != null)
            {
                string sType = cbbMenuType.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(sType))
                {
                    MenuType selectType = (MenuType)int.Parse(sType);
                    switch (selectType)
                    {
                        case MenuType.Modul:
                            cbbDLL.Enabled = false;
                            txbClassFullPath.ReadOnly = true;
                            ckbShowInToolstrip.Visible = false;
                            break;
                        case MenuType.Class:
                            cbbDLL.Enabled = false;
                            txbClassFullPath.ReadOnly = true;
                            ckbShowInToolstrip.Visible = false;
                            break;
                        case MenuType.Menu:
                        default:
                            cbbDLL.Enabled = true;
                            txbClassFullPath.ReadOnly = false;
                            ckbShowInToolstrip.Visible = true;
                            break;
                    }
                }
            }
        }
        #endregion

        #region 右键菜单事件
        private void tsmiAddModel_Click(object sender, EventArgs e)
        {
            UIHelper.ResetControl(grpEdit);
            _saveMenu = new MenuEntity();
            _saveMenu.MenuType = MenuType.Modul;
            //改变下拉框
            cbbMenuType.SelectedValue = ((int)_saveMenu.MenuType).ToString();
        }

        private void tsmiAddClass_Click(object sender, EventArgs e)
        {
            //设置上级菜单
            TreeNode trSelect = tvLeftMenu.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            UIHelper.ResetControl(grpEdit);
            _saveMenu = new MenuEntity();
            _saveMenu.MenuType = MenuType.Class;
            MenuEntity selectMenu = trSelect.Tag as MenuEntity;
            _saveMenu.ParentGuid = selectMenu.Guid;
            //改变下拉框
            cbbMenuType.SelectedValue = ((int)_saveMenu.MenuType).ToString();
        }

        private void tsmiAddFunc_Click(object sender, EventArgs e)
        {
            //设置上级菜单
            TreeNode trSelect = tvLeftMenu.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            UIHelper.ResetControl(grpEdit);
            _saveMenu = new MenuEntity();
            _saveMenu.MenuType = MenuType.Menu;
            MenuEntity selectMenu = trSelect.Tag as MenuEntity;
            _saveMenu.ParentGuid = selectMenu.Guid;
            //改变下拉框
            cbbMenuType.SelectedValue = ((int)_saveMenu.MenuType).ToString();
        }
        #endregion

        #region 删除右键菜单事件
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            //设置上级菜单
            TreeNode trSelect = tvLeftMenu.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            if (trSelect.GetNodeCount(true) >0)
            {
                ShowErr("请先删除子节点！");
                return;
            }

            if (ShowOkCancel("确定要删除该菜单？")== DialogResult.Cancel)
            {
                return;
            }

            MenuEntity selectMenu = trSelect.Tag as MenuEntity;
            XmlDocument MenuEntity = _xmlMenu.XmlMenus;
            #region 删除
            XmlNode xnNew;
            switch (_saveMenu.MenuType)
            {
                case MenuType.Modul:
                    xnNew = MenuEntity.SelectSingleNode("xml/Model[@Guid='" + selectMenu.Guid + "']");
                    xnNew.ParentNode.RemoveChild(xnNew);
                    break;
                case MenuType.Class:
                    //第一个分类
                    xnNew = MenuEntity.SelectSingleNode("xml/Model/Class[@Guid='" + selectMenu.Guid + "']");
                    
                    if (xnNew == null)
                    {
                        //第二个菜单分类
                        xnNew = MenuEntity.SelectSingleNode("xml/Model/Class/Class[@Guid='" + selectMenu.Guid + "']");
                    }

                    if (xnNew == null)
                    {
                        //最多支持第三个菜单分类
                        xnNew = MenuEntity.SelectSingleNode("xml/Model/Class/Class/Class[@Guid='" + selectMenu.Guid + "']");
                    }
                    xnNew.ParentNode.RemoveChild(xnNew);
                    break;
                case MenuType.Menu:
                default:
                    //第一个分类
                    xnNew = MenuEntity.SelectSingleNode("xml/Model/Class/Menu[@Guid='" + selectMenu.Guid + "']");

                    if (xnNew == null)
                    {
                        //第二个菜单分类
                        xnNew = MenuEntity.SelectSingleNode("xml/Model/Class/Class/Menu[@Guid='" + selectMenu.Guid + "']");
                    }

                    if (xnNew == null)
                    {
                        //最多支持第三个菜单分类
                        xnNew = MenuEntity.SelectSingleNode("xml/Model/Class/Class/Class/Menu[@Guid='" + selectMenu.Guid + "']");
                    }

                    xnNew.ParentNode.RemoveChild(xnNew);
                    break;
            }
            #endregion

            //保存
            MenuEntity.Save(_xmlMenu.XmlPath);
            ShowInfo("删除成功！");
            UIHelper.ResetControl(grpEdit);
            //重新加载菜单
            LoadMenu();
        }
        #endregion
    }
}
