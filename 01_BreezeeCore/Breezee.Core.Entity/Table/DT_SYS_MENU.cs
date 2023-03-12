using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_SYS_MENU : DbEntity
	{

		//自动生成表名
		public static string TName = "SYS_MENU"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_SYS_MENU();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "SYS_MENU";} }
		//自动生成字段
		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string DLL_ID = "DLL_ID";
		 public DbField EF_DLL_ID = new DbField().DBColName("DLL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string DLL_NAME = "DLL_NAME";
		 public DbField EF_DLL_NAME = new DbField().DBColName("DLL_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FORM_FULL_PATH = "FORM_FULL_PATH";
		 public DbField EF_FORM_FULL_PATH = new DbField().DBColName("FORM_FULL_PATH").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FORM_ID = "FORM_ID";
		 public DbField EF_FORM_ID = new DbField().DBColName("FORM_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FORM_TYPE = "FORM_TYPE";
		 public DbField EF_FORM_TYPE = new DbField().DBColName("FORM_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_ENABLED = "IS_ENABLED";
		 public DbField EF_IS_ENABLED = new DbField().DBColName("IS_ENABLED").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_SYSTEM = "IS_SYSTEM";
		 public DbField EF_IS_SYSTEM = new DbField().DBColName("IS_SYSTEM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string LAST_UPDATED_TIME = "LAST_UPDATED_TIME";
		 public DbField EF_LAST_UPDATED_TIME = new DbField().DBColName("LAST_UPDATED_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_ACTION = "MENU_ACTION";
		 public DbField EF_MENU_ACTION = new DbField().DBColName("MENU_ACTION").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_CODE = "MENU_CODE";
		 public DbField EF_MENU_CODE = new DbField().DBColName("MENU_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_DESC = "MENU_DESC";
		 public DbField EF_MENU_DESC = new DbField().DBColName("MENU_DESC").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_ICON = "MENU_ICON";
		 public DbField EF_MENU_ICON = new DbField().DBColName("MENU_ICON").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_ID = "MENU_ID";
		 public DbField EF_MENU_ID = new DbField().DBColName("MENU_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME = "MENU_NAME";
		 public DbField EF_MENU_NAME = new DbField().DBColName("MENU_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_PARM = "MENU_PARM";
		 public DbField EF_MENU_PARM = new DbField().DBColName("MENU_PARM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_TYPE = "MENU_TYPE";
		 public DbField EF_MENU_TYPE = new DbField().DBColName("MENU_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER = "MODIFIER";
		 public DbField EF_MODIFIER = new DbField().DBColName("MODIFIER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER_ID = "MODIFIER_ID";
		 public DbField EF_MODIFIER_ID = new DbField().DBColName("MODIFIER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string NOT_RIGHT_BUTTON_DISPLAY_TYPE = "NOT_RIGHT_BUTTON_DISPLAY_TYPE";
		 public DbField EF_NOT_RIGHT_BUTTON_DISPLAY_TYPE = new DbField().DBColName("NOT_RIGHT_BUTTON_DISPLAY_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_ID = "ORG_ID";
		 public DbField EF_ORG_ID = new DbField().DBColName("ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PARENT_MENU_ID = "PARENT_MENU_ID";
		 public DbField EF_PARENT_MENU_ID = new DbField().DBColName("PARENT_MENU_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RIGHT_TYPE = "RIGHT_TYPE";
		 public DbField EF_RIGHT_TYPE = new DbField().DBColName("RIGHT_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SYS_ID = "SYS_ID";
		 public DbField EF_SYS_ID = new DbField().DBColName("SYS_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_DLL_ID,EF_DLL_NAME,EF_FORM_FULL_PATH,EF_FORM_ID,EF_FORM_TYPE,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MENU_ACTION,EF_MENU_CODE,EF_MENU_DESC,EF_MENU_ICON,EF_MENU_ID,EF_MENU_NAME,EF_MENU_PARM,EF_MENU_TYPE,EF_MODIFIER,EF_MODIFIER_ID,EF_NOT_RIGHT_BUTTON_DISPLAY_TYPE,EF_ORG_ID,EF_PARENT_MENU_ID,EF_REMARK,EF_RIGHT_TYPE,EF_SORT_ID,EF_SYS_ID,EF_TFLAG,EF_UPDATE_CONTROL_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "CREATE_TIME","CREATOR","CREATOR_ID","DLL_ID","DLL_NAME","FORM_FULL_PATH","FORM_ID","FORM_TYPE","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MENU_ACTION","MENU_CODE","MENU_DESC","MENU_ICON","MENU_ID","MENU_NAME","MENU_PARM","MENU_TYPE","MODIFIER","MODIFIER_ID","NOT_RIGHT_BUTTON_DISPLAY_TYPE","ORG_ID","PARENT_MENU_ID","REMARK","RIGHT_TYPE","SORT_ID","SYS_ID","TFLAG","UPDATE_CONTROL_ID" });
				 return DbColumn;
			}
		 }
	}
}
