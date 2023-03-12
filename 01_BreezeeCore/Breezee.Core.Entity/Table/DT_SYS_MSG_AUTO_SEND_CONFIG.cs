using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_SYS_MSG_AUTO_SEND_CONFIG : DbEntity
	{

		//自动生成表名
		public static string TName = "SYS_MSG_AUTO_SEND_CONFIG"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_SYS_MSG_AUTO_SEND_CONFIG();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "SYS_MSG_AUTO_SEND_CONFIG";} }
		//自动生成字段
		 public static string BELONG_MODULE_CODE = "BELONG_MODULE_CODE";
		 public DbField EF_BELONG_MODULE_CODE = new DbField().DBColName("BELONG_MODULE_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONFIG_CODE = "CONFIG_CODE";
		 public DbField EF_CONFIG_CODE = new DbField().DBColName("CONFIG_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONFIG_NAME = "CONFIG_NAME";
		 public DbField EF_CONFIG_NAME = new DbField().DBColName("CONFIG_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

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

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_CONFIG_ID = "SEND_CONFIG_ID";
		 public DbField EF_SEND_CONFIG_ID = new DbField().DBColName("SEND_CONFIG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_CONFIG_TYPE = "SEND_CONFIG_TYPE";
		 public DbField EF_SEND_CONFIG_TYPE = new DbField().DBColName("SEND_CONFIG_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_FREQUENCY = "SEND_FREQUENCY";
		 public DbField EF_SEND_FREQUENCY = new DbField().DBColName("SEND_FREQUENCY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_TIME_SPAN = "SEND_TIME_SPAN";
		 public DbField EF_SEND_TIME_SPAN = new DbField().DBColName("SEND_TIME_SPAN").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_TIMES = "SEND_TIMES";
		 public DbField EF_SEND_TIMES = new DbField().DBColName("SEND_TIMES").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEND_TYPE = "SEND_TYPE";
		 public DbField EF_SEND_TYPE = new DbField().DBColName("SEND_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SQL_CONTENT = "SQL_CONTENT";
		 public DbField EF_SQL_CONTENT = new DbField().DBColName("SQL_CONTENT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_BELONG_MODULE_CODE,EF_CONFIG_CODE,EF_CONFIG_NAME,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_EXTEND_1,EF_EXTEND_2,EF_EXTEND_3,EF_EXTEND_4,EF_EXTEND_5,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MODIFIER,EF_MODIFIER_ID,EF_ORG_ID,EF_REMARK,EF_SEND_CONFIG_ID,EF_SEND_CONFIG_TYPE,EF_SEND_FREQUENCY,EF_SEND_TIME_SPAN,EF_SEND_TIMES,EF_SEND_TYPE,EF_SORT_ID,EF_SQL_CONTENT,EF_TFLAG,EF_UPDATE_CONTROL_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "BELONG_MODULE_CODE","CONFIG_CODE","CONFIG_NAME","CREATE_TIME","CREATOR","CREATOR_ID","EXTEND_1","EXTEND_2","EXTEND_3","EXTEND_4","EXTEND_5","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MODIFIER","MODIFIER_ID","ORG_ID","REMARK","SEND_CONFIG_ID","SEND_CONFIG_TYPE","SEND_FREQUENCY","SEND_TIME_SPAN","SEND_TIMES","SEND_TYPE","SORT_ID","SQL_CONTENT","TFLAG","UPDATE_CONTROL_ID" });
				 return DbColumn;
			}
		 }
	}
}
