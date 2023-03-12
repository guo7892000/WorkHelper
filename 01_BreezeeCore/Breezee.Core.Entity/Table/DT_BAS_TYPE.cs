using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_BAS_TYPE : DbEntity
	{

		//自动生成表名
		public static string TName = "BAS_TYPE"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_BAS_TYPE();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "BAS_TYPE";} }
		//自动生成字段
		 public static string BELONG_OBJECT_TYPE = "BELONG_OBJECT_TYPE";
		 public DbField EF_BELONG_OBJECT_TYPE = new DbField().DBColName("BELONG_OBJECT_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_VALUE1 = "EXTEND_VALUE1";
		 public DbField EF_EXTEND_VALUE1 = new DbField().DBColName("EXTEND_VALUE1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_VALUE2 = "EXTEND_VALUE2";
		 public DbField EF_EXTEND_VALUE2 = new DbField().DBColName("EXTEND_VALUE2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_VALUE3 = "EXTEND_VALUE3";
		 public DbField EF_EXTEND_VALUE3 = new DbField().DBColName("EXTEND_VALUE3").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_VALUE4 = "EXTEND_VALUE4";
		 public DbField EF_EXTEND_VALUE4 = new DbField().DBColName("EXTEND_VALUE4").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EXTEND_VALUE5 = "EXTEND_VALUE5";
		 public DbField EF_EXTEND_VALUE5 = new DbField().DBColName("EXTEND_VALUE5").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_ENABLED = "IS_ENABLED";
		 public DbField EF_IS_ENABLED = new DbField().DBColName("IS_ENABLED").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_FROM_INTERFACE = "IS_FROM_INTERFACE";
		 public DbField EF_IS_FROM_INTERFACE = new DbField().DBColName("IS_FROM_INTERFACE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_SYSTEM = "IS_SYSTEM";
		 public DbField EF_IS_SYSTEM = new DbField().DBColName("IS_SYSTEM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string LAST_UPDATED_TIME = "LAST_UPDATED_TIME";
		 public DbField EF_LAST_UPDATED_TIME = new DbField().DBColName("LAST_UPDATED_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER = "MODIFIER";
		 public DbField EF_MODIFIER = new DbField().DBColName("MODIFIER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER_ID = "MODIFIER_ID";
		 public DbField EF_MODIFIER_ID = new DbField().DBColName("MODIFIER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFY_MODE = "MODIFY_MODE";
		 public DbField EF_MODIFY_MODE = new DbField().DBColName("MODIFY_MODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_ID = "ORG_ID";
		 public DbField EF_ORG_ID = new DbField().DBColName("ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_CLASS_BIG = "TYPE_CLASS_BIG";
		 public DbField EF_TYPE_CLASS_BIG = new DbField().DBColName("TYPE_CLASS_BIG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_CLASS_SMALL = "TYPE_CLASS_SMALL";
		 public DbField EF_TYPE_CLASS_SMALL = new DbField().DBColName("TYPE_CLASS_SMALL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_CODE = "TYPE_CODE";
		 public DbField EF_TYPE_CODE = new DbField().DBColName("TYPE_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_DESC = "TYPE_DESC";
		 public DbField EF_TYPE_DESC = new DbField().DBColName("TYPE_DESC").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_ID = "TYPE_ID";
		 public DbField EF_TYPE_ID = new DbField().DBColName("TYPE_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TYPE_NAME = "TYPE_NAME";
		 public DbField EF_TYPE_NAME = new DbField().DBColName("TYPE_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_BELONG_OBJECT_TYPE,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_EXTEND_VALUE1,EF_EXTEND_VALUE2,EF_EXTEND_VALUE3,EF_EXTEND_VALUE4,EF_EXTEND_VALUE5,EF_IS_ENABLED,EF_IS_FROM_INTERFACE,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MODIFIER,EF_MODIFIER_ID,EF_MODIFY_MODE,EF_ORG_ID,EF_REMARK,EF_SORT_ID,EF_TFLAG,EF_TYPE_CLASS_BIG,EF_TYPE_CLASS_SMALL,EF_TYPE_CODE,EF_TYPE_DESC,EF_TYPE_ID,EF_TYPE_NAME,EF_UPDATE_CONTROL_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "BELONG_OBJECT_TYPE","CREATE_TIME","CREATOR","CREATOR_ID","EXTEND_VALUE1","EXTEND_VALUE2","EXTEND_VALUE3","EXTEND_VALUE4","EXTEND_VALUE5","IS_ENABLED","IS_FROM_INTERFACE","IS_SYSTEM","LAST_UPDATED_TIME","MODIFIER","MODIFIER_ID","MODIFY_MODE","ORG_ID","REMARK","SORT_ID","TFLAG","TYPE_CLASS_BIG","TYPE_CLASS_SMALL","TYPE_CODE","TYPE_DESC","TYPE_ID","TYPE_NAME","UPDATE_CONTROL_ID" });
				 return DbColumn;
			}
		 }
	}
}
