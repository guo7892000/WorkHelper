using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Breezee.Core.Entity;
using Breezee.Core.Interface;

/***************************************************************************
 * 对象名称：菜单实体
 * 对象类别：实体类
 * 创建作者：黄国辉
 * 创建日期：2012-2-15
 * 对象说明：主要提供框架的主窗体的菜单加载等功能。
 * 注意：
 * 修改历史：
 *      V1.0 新建 hgh 2012-2-15
 * ************************************************************************/
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 类名：菜单实体
    /// 作者：黄国辉
    /// 日期：2013-7-20
    /// </summary>
	public partial class SYS_MENU:MenuEntity
	{
        #region 属性
        /// <summary>
        /// 模块名
        /// </summary>
        private string _ModuleName;

        /// <summary>
        /// 模块属性
        /// </summary>
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        } 
        #endregion

        #region 公共属性
        public string MenuId { get; set; }

        public string SysId { get; set; }

        public string FormId { get; set; }

        public string ButtonId { get; set; }

        public string MenuTypeString { get; set; }

        public string MenuCode { get; set; }

        public string MenuName { get; set; }

        public string ParentMenuId { get; set; }

        public string MenuDesc { get; set; }

        public string MenuAction { get; set; }

        public string MenuParm { get; set; }

        public string MenuIcon { get; set; }

        public string MenuFullPath { get; set; }

        public string DllId { get; set; }

        public string DllName { get; set; }

        public string FormFullPath { get; set; }

        public string FormType { get; set; }

        public string RightType { get; set; }

        public string notRightButtonDisplayType { get; set; }

        public int? SortId { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatorId { get; set; }

        public string Creator { get; set; }

        public string ModifierId { get; set; }

        public string Modifier { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        public string IsEnabled { get; set; }

        public string IsSystem { get; set; }

        public string OrgId { get; set; }

        public string UpdateControlId { get; set; }

        public string Tflag { get; set; } 
        #endregion

        #region 将数据行转换为菜单实体
        /// <summary>
        /// 将数据行转换为菜单实体
        /// </summary>
        /// <param name="row">行信息</param>
        /// <returns>菜单实体</returns>
        public static SYS_MENU FromDataRow(DataRow dr)
        {
            SYS_MENU menu = new SYS_MENU();
            menu.MenuId = dr[DT_SYS_MENU.MENU_ID].ToString();
            menu.MenuName = dr[DT_SYS_MENU.MENU_NAME].ToString();
            if (dr[DT_SYS_MENU.PARENT_MENU_ID].GetType().Name != "DBNull")
            {
                menu.ParentMenuId = dr[DT_SYS_MENU.PARENT_MENU_ID].ToString();
            }
            menu.MenuTypeString = dr[DT_SYS_MENU.MENU_TYPE].ToString();
            menu.MenuParm = dr[DT_SYS_MENU.MENU_PARM].ToString();
            menu.SortId = Convert.ToInt32(dr[DT_SYS_MENU.SORT_ID]);
            if (dr[DT_SYS_MENU.DLL_ID].GetType().Name != "DBNull")
            {
                menu.DllId = dr[DT_SYS_MENU.DLL_ID].ToString();
            }
            menu.SysId = dr[DT_SYS_MENU.SYS_ID].ToString();
            menu.DllName = dr[DT_SYS_MENU.DLL_NAME].ToString();
            menu.MenuIcon = dr[DT_SYS_MENU.MENU_ICON].ToString();
            menu.MenuDesc = dr[DT_SYS_MENU.MENU_DESC].ToString();
            menu.FormFullPath = dr[DT_SYS_MENU.FORM_FULL_PATH].ToString();
            menu.MenuCode = dr[DT_SYS_MENU.MENU_CODE].ToString();
            menu.MenuFullPath = dr[DT_V_SYS_MENU_FULL_PATH.MENU_FULL_PATH].ToString();
            menu.RightType = dr[DT_SYS_MENU.RIGHT_TYPE].ToString();
            menu.notRightButtonDisplayType = dr[DT_SYS_MENU.NOT_RIGHT_BUTTON_DISPLAY_TYPE].ToString();

            //父级对象的赋值
            menu.Guid = menu.MenuId;
            //menu.SameMenuNewFormGuid = SameMenuNewFormGuid;
            // menu.IsOpenSameMenuNewForm = IsOpenSameMenuNewForm;
            switch (menu.MenuTypeString)
            {
                case "F":
                    menu.MenuType = MenuType.Menu;
                    break;
                case "M":
                    menu.MenuType = MenuType.Class;
                    break;
                case "P":
                    menu.MenuType = MenuType.Page;
                    break;
                default:
                    break;
            }
            
            menu.Name = menu.MenuName;
            menu.Code = menu.MenuCode;
            //menu.ShortCutKey = ShortCutKey;
            menu.DLLName = menu.DllName;
            menu.FormName = menu.MenuFullPath;
            menu.FullPath = menu.FormFullPath;
            //menu.HelpPath = HelpPath;
            menu.ParentGuid = menu.ParentMenuId;
            menu.IsShowInToolStrip = false;
            return menu;
        } 
        #endregion
		
	}
}
