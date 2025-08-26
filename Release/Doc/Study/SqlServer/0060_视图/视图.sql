/*删除视图*/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_SYS_AND_MENU]'))
DROP VIEW [dbo].[V_SYS_AND_MENU]
GO

/*创建视图*/
CREATE VIEW [dbo].[V_SYS_AND_MENU] 
AS
SELECT
/*******************************************
* 对象名称：系统菜单视图
* 对象类型：视图
* 创建作者：黄国辉
* 创建日期：2014-7-25
* 修改历史：
*	V1.0：hgh 创建 2014-7-25
*******************************************/
	  MU.MENU_ID ,/*菜单ID*/
	  MU.MENU_CODE ,/*菜单编码*/
	  MU.MENU_NAME ,/*菜单名称*/
	  CASE WHEN MU.PARENT_MENU_ID IS NULL THEN MU.SYS_ID
			ELSE MU.PARENT_MENU_ID
	  END AS PARENT_MENU_ID ,/*父菜单ID*/
	  MU.MENU_TYPE ,/*菜单类型*/
	  MU.SYS_ID ,/*系统ID*/
	  MU.SORT_ID, /*排序号*/
	  MU.IS_ENABLED,
	  /*只有菜单有的字段*/
	  MU.FORM_ID ,
	  MU.MENU_DESC ,
	  MU.MENU_ACTION ,
	  MU.MENU_PARM ,
	  MU.MENU_ICON ,
	  V.MENU_FULL_PATH,
	  MU.DLL_ID ,
	  MU.DLL_NAME ,
	  MU.FORM_FULL_PATH ,
	  MU.FORM_TYPE ,
	  MU.REMARK ,
	  MU.CREATE_TIME ,
	  MU.CREATOR_ID ,
	  MU.CREATOR ,
	  MU.MODIFIER_ID ,
	  MU.MODIFIER ,
	  MU.LAST_UPDATED_TIME ,
	  MU.IS_SYSTEM ,
	  MU.ORG_ID ,
	  MU.UPDATE_CONTROL_ID ,
	  MU.RIGHT_TYPE,
	  MU.NOT_RIGHT_BUTTON_DISPLAY_TYPE
FROM    SYS_MENU MU
LEFT JOIN V_SYS_MENU_FULL_PATH V
	ON MU.MENU_ID = V.LAST_MENU_ID
WHERE MU.IS_ENABLED = '1';
GO

/*修改视图：注，视图一定要存在，否则会报错*/
ALTER VIEW [dbo].[V_SYS_AND_MENU] 
AS
SELECT
/*******************************************
* 对象名称：系统菜单视图
* 对象类型：视图
* 创建作者：黄国辉
* 创建日期：2014-7-25
* 修改历史：
*	V1.0：hgh 创建 2014-7-25
*******************************************/
	MU.MENU_ID ,/*菜单ID*/
	MU.MENU_CODE /*菜单编码*/
FROM    SYS_MENU MU
LEFT JOIN V_SYS_MENU_FULL_PATH V
	ON MU.MENU_ID = V.LAST_MENU_ID
WHERE MU.IS_ENABLED = '1';
GO

