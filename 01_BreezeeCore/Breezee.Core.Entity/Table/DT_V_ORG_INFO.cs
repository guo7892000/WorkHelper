using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_V_ORG_INFO : DbEntity
	{

		//自动生成表名
		public static string TName = "V_ORG_INFO"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_V_ORG_INFO();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "V_ORG_INFO";} }
		//自动生成字段
		 public static string ADDR_FULL = "ADDR_FULL";
		 public DbField EF_ADDR_FULL = new DbField().DBColName("ADDR_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string BELONG_ORG_ID = "BELONG_ORG_ID";
		 public DbField EF_BELONG_ORG_ID = new DbField().DBColName("BELONG_ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FORM_CODE_SHORT = "FORM_CODE_SHORT";
		 public DbField EF_FORM_CODE_SHORT = new DbField().DBColName("FORM_CODE_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

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

		 public static string OBJECT_ID = "OBJECT_ID";
		 public DbField EF_OBJECT_ID = new DbField().DBColName("OBJECT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_CODE_FULL = "ORG_CODE_FULL";
		 public DbField EF_ORG_CODE_FULL = new DbField().DBColName("ORG_CODE_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_CODE_SHORT = "ORG_CODE_SHORT";
		 public DbField EF_ORG_CODE_SHORT = new DbField().DBColName("ORG_CODE_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_ID = "ORG_ID";
		 public DbField EF_ORG_ID = new DbField().DBColName("ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_NAME_FULL = "ORG_NAME_FULL";
		 public DbField EF_ORG_NAME_FULL = new DbField().DBColName("ORG_NAME_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_NAME_SHORT = "ORG_NAME_SHORT";
		 public DbField EF_ORG_NAME_SHORT = new DbField().DBColName("ORG_NAME_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_TYPE = "ORG_TYPE";
		 public DbField EF_ORG_TYPE = new DbField().DBColName("ORG_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PARENT_ORG_ID = "PARENT_ORG_ID";
		 public DbField EF_PARENT_ORG_ID = new DbField().DBColName("PARENT_ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PHONE1_NO = "PHONE1_NO";
		 public DbField EF_PHONE1_NO = new DbField().DBColName("PHONE1_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PHONE2_NO = "PHONE2_NO";
		 public DbField EF_PHONE2_NO = new DbField().DBColName("PHONE2_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TAX_NO = "TAX_NO";
		 public DbField EF_TAX_NO = new DbField().DBColName("TAX_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_ADDR_FULL,EF_BELONG_ORG_ID,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_FORM_CODE_SHORT,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MODIFIER,EF_MODIFIER_ID,EF_OBJECT_ID,EF_ORG_CODE_FULL,EF_ORG_CODE_SHORT,EF_ORG_ID,EF_ORG_NAME_FULL,EF_ORG_NAME_SHORT,EF_ORG_TYPE,EF_PARENT_ORG_ID,EF_PHONE1_NO,EF_PHONE2_NO,EF_REMARK,EF_SORT_ID,EF_TAX_NO,EF_UPDATE_CONTROL_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "ADDR_FULL","BELONG_ORG_ID","CREATE_TIME","CREATOR","CREATOR_ID","FORM_CODE_SHORT","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MODIFIER","MODIFIER_ID","OBJECT_ID","ORG_CODE_FULL","ORG_CODE_SHORT","ORG_ID","ORG_NAME_FULL","ORG_NAME_SHORT","ORG_TYPE","PARENT_ORG_ID","PHONE1_NO","PHONE2_NO","REMARK","SORT_ID","TAX_NO","UPDATE_CONTROL_ID" });
				 return DbColumn;
			}
		 }
	}
}
