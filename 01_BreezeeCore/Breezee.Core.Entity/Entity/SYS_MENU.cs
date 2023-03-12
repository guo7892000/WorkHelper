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
        public string MENU_ID { get; set; }

        public string SYS_ID { get; set; }

        public string FORM_ID { get; set; }

        public string BUTTON_ID { get; set; }

        public string MENU_TYPE { get; set; }

        public string MENU_CODE { get; set; }

        public string MENU_NAME { get; set; }

        public string PARENT_MENU_ID { get; set; }

        public string MENU_DESC { get; set; }

        public string MENU_ACTION { get; set; }

        public string MENU_PARM { get; set; }

        public string MENU_ICON { get; set; }

        public string MENU_FULL_PATH { get; set; }

        public string DLL_ID { get; set; }

        public string DLL_NAME { get; set; }

        public string FORM_FULL_PATH { get; set; }

        public string FORM_TYPE { get; set; }

        public string RIGHT_TYPE { get; set; }

        public string NOT_RIGHT_BUTTON_DISPLAY_TYPE { get; set; }

        public int? SORT_ID { get; set; }

        public string REMARK { get; set; }

        public DateTime CREATE_TIME { get; set; }

        public string CREATOR_ID { get; set; }

        public string CREATOR { get; set; }

        public string MODIFIER_ID { get; set; }

        public string MODIFIER { get; set; }

        public DateTime LAST_UPDATED_TIME { get; set; }

        public string IS_ENABLED { get; set; }

        public string IS_SYSTEM { get; set; }

        public string ORG_ID { get; set; }

        public string UPDATE_CONTROL_ID { get; set; }

        public string TFLAG { get; set; } 
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
            //menu.MENU_ID = dr[DT_SYS_MENU.MENU_ID].ToString();
            //menu.MENU_NAME = dr[DT_SYS_MENU.MENU_NAME].ToString();
            //if (dr[DT_SYS_MENU.PARENT_MENU_ID].GetType().Name != "DBNull")
            //{
            //    menu.PARENT_MENU_ID = dr[DT_SYS_MENU.PARENT_MENU_ID].ToString();
            //}
            //menu.MENU_TYPE = dr[DT_SYS_MENU.MENU_TYPE].ToString();
            //menu.MENU_PARM = dr[DT_SYS_MENU.MENU_PARM].ToString();
            //menu.SORT_ID = Convert.ToInt32(dr[DT_SYS_MENU.SORT_ID]);
            //if (dr[DT_SYS_MENU.DLL_ID].GetType().Name != "DBNull")
            //{
            //    menu.DLL_ID = dr[DT_SYS_MENU.DLL_ID].ToString();
            //}
            //menu.SYS_ID = dr[DT_SYS_MENU.SYS_ID].ToString();
            //menu.DLL_NAME = dr[DT_SYS_MENU.DLL_NAME].ToString();
            //menu.MENU_ICON = dr[DT_SYS_MENU.MENU_ICON].ToString();
            //menu.MENU_DESC = dr[DT_SYS_MENU.MENU_DESC].ToString();
            //menu.FORM_FULL_PATH = dr[DT_SYS_MENU.FORM_FULL_PATH].ToString();
            //menu.MENU_CODE = dr[DT_SYS_MENU.MENU_CODE].ToString();
            //menu.MENU_FULL_PATH = dr[DT_V_SYS_MENU_FULL_PATH.MENU_FULL_PATH].ToString();
            //menu.RIGHT_TYPE = dr[DT_SYS_MENU.RIGHT_TYPE].ToString();
            //menu.NOT_RIGHT_BUTTON_DISPLAY_TYPE = dr[DT_SYS_MENU.NOT_RIGHT_BUTTON_DISPLAY_TYPE].ToString();
            return menu;
        } 
        #endregion
		
	}
}
