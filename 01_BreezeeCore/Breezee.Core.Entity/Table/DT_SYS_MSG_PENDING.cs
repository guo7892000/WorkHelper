using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_SYS_MSG_PENDING : DbEntity
	{

		//自动生成表名
		public static string TName = "SYS_MSG_PENDING"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_SYS_MSG_PENDING();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "SYS_MSG_PENDING";} }
		//自动生成字段
		 public static string BELONG_MODULE_CODE = "BELONG_MODULE_CODE";
		 public DbField EF_BELONG_MODULE_CODE = new DbField().DBColName("BELONG_MODULE_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_1 = "EXTEND_1";
		 public DbField EF_EXTEND_1 = new DbField().DBColName("EXTEND_1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_2 = "EXTEND_2";
		 public DbField EF_EXTEND_2 = new DbField().DBColName("EXTEND_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_3 = "EXTEND_3";
		 public DbField EF_EXTEND_3 = new DbField().DBColName("EXTEND_3").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_4 = "EXTEND_4";
		 public DbField EF_EXTEND_4 = new DbField().DBColName("EXTEND_4").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_5 = "EXTEND_5";
		 public DbField EF_EXTEND_5 = new DbField().DBColName("EXTEND_5").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_ENABLED = "IS_ENABLED";
		 public DbField EF_IS_ENABLED = new DbField().DBColName("IS_ENABLED").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_SYSTEM = "IS_SYSTEM";
		 public DbField EF_IS_SYSTEM = new DbField().DBColName("IS_SYSTEM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string LAST_UPDATED_TIME = "LAST_UPDATED_TIME";
		 public DbField EF_LAST_UPDATED_TIME = new DbField().DBColName("LAST_UPDATED_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER = "MODIFIER";
		 public DbField EF_MODIFIER = new DbField().DBColName("MODIFIER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER_ID = "MODIFIER_ID";
		 public DbField EF_MODIFIER_ID = new DbField().DBColName("MODIFIER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_ID = "ORG_ID";
		 public DbField EF_ORG_ID = new DbField().DBColName("ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_BIG_TYPE = "PENDING_BIG_TYPE";
		 public DbField EF_PENDING_BIG_TYPE = new DbField().DBColName("PENDING_BIG_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_CODE = "PENDING_CODE";
		 public DbField EF_PENDING_CODE = new DbField().DBColName("PENDING_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_CONTENT = "PENDING_CONTENT";
		 public DbField EF_PENDING_CONTENT = new DbField().DBColName("PENDING_CONTENT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_ID = "PENDING_ID";
		 public DbField EF_PENDING_ID = new DbField().DBColName("PENDING_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_NAME = "PENDING_NAME";
		 public DbField EF_PENDING_NAME = new DbField().DBColName("PENDING_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PENDING_SMALL_TYPE = "PENDING_SMALL_TYPE";
		 public DbField EF_PENDING_SMALL_TYPE = new DbField().DBColName("PENDING_SMALL_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_CLASS_NAME = "RELATE_CLASS_NAME";
		 public DbField EF_RELATE_CLASS_NAME = new DbField().DBColName("RELATE_CLASS_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_DLL_NAME = "RELATE_DLL_NAME";
		 public DbField EF_RELATE_DLL_NAME = new DbField().DBColName("RELATE_DLL_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_FORM_NAME = "RELATE_FORM_NAME";
		 public DbField EF_RELATE_FORM_NAME = new DbField().DBColName("RELATE_FORM_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_FUNC_NAME = "RELATE_FUNC_NAME";
		 public DbField EF_RELATE_FUNC_NAME = new DbField().DBColName("RELATE_FUNC_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_FUNC_PARA = "RELATE_FUNC_PARA";
		 public DbField EF_RELATE_FUNC_PARA = new DbField().DBColName("RELATE_FUNC_PARA").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_ICON = "RELATE_ICON";
		 public DbField EF_RELATE_ICON = new DbField().DBColName("RELATE_ICON").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RELATE_MENU_ID = "RELATE_MENU_ID";
		 public DbField EF_RELATE_MENU_ID = new DbField().DBColName("RELATE_MENU_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_BELONG_MODULE_CODE,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_EXTEND_1,EF_EXTEND_2,EF_EXTEND_3,EF_EXTEND_4,EF_EXTEND_5,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MODIFIER,EF_MODIFIER_ID,EF_ORG_ID,EF_PENDING_BIG_TYPE,EF_PENDING_CODE,EF_PENDING_CONTENT,EF_PENDING_ID,EF_PENDING_NAME,EF_PENDING_SMALL_TYPE,EF_RELATE_CLASS_NAME,EF_RELATE_DLL_NAME,EF_RELATE_FORM_NAME,EF_RELATE_FUNC_NAME,EF_RELATE_FUNC_PARA,EF_RELATE_ICON,EF_RELATE_MENU_ID,EF_REMARK,EF_SORT_ID,EF_TFLAG,EF_UPDATE_CONTROL_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "BELONG_MODULE_CODE","CREATE_TIME","CREATOR","CREATOR_ID","EXTEND_1","EXTEND_2","EXTEND_3","EXTEND_4","EXTEND_5","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MODIFIER","MODIFIER_ID","ORG_ID","PENDING_BIG_TYPE","PENDING_CODE","PENDING_CONTENT","PENDING_ID","PENDING_NAME","PENDING_SMALL_TYPE","RELATE_CLASS_NAME","RELATE_DLL_NAME","RELATE_FORM_NAME","RELATE_FUNC_NAME","RELATE_FUNC_PARA","RELATE_ICON","RELATE_MENU_ID","REMARK","SORT_ID","TFLAG","UPDATE_CONTROL_ID" });
				 return DbColumn;
			}
		 }
	}
}
