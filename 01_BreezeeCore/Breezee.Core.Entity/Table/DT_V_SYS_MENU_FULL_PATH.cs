using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_V_SYS_MENU_FULL_PATH : DbEntity
	{

		//自动生成表名
		public static string TName = "V_SYS_MENU_FULL_PATH"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_V_SYS_MENU_FULL_PATH();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "V_SYS_MENU_FULL_PATH";} }
		//自动生成字段
		 public static string LAST_FORM_FULL_PATH = "LAST_FORM_FULL_PATH";
		 public DbField EF_LAST_FORM_FULL_PATH = new DbField().DBColName("LAST_FORM_FULL_PATH").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string LAST_MENU_ID = "LAST_MENU_ID";
		 public DbField EF_LAST_MENU_ID = new DbField().DBColName("LAST_MENU_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_FULL_PATH = "MENU_FULL_PATH";
		 public DbField EF_MENU_FULL_PATH = new DbField().DBColName("MENU_FULL_PATH").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_LAYER_NUM = "MENU_LAYER_NUM";
		 public DbField EF_MENU_LAYER_NUM = new DbField().DBColName("MENU_LAYER_NUM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME_1 = "MENU_NAME_1";
		 public DbField EF_MENU_NAME_1 = new DbField().DBColName("MENU_NAME_1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME_2 = "MENU_NAME_2";
		 public DbField EF_MENU_NAME_2 = new DbField().DBColName("MENU_NAME_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME_3 = "MENU_NAME_3";
		 public DbField EF_MENU_NAME_3 = new DbField().DBColName("MENU_NAME_3").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME_4 = "MENU_NAME_4";
		 public DbField EF_MENU_NAME_4 = new DbField().DBColName("MENU_NAME_4").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MENU_NAME_5 = "MENU_NAME_5";
		 public DbField EF_MENU_NAME_5 = new DbField().DBColName("MENU_NAME_5").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SYS_ID = "SYS_ID";
		 public DbField EF_SYS_ID = new DbField().DBColName("SYS_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_LAST_FORM_FULL_PATH,EF_LAST_MENU_ID,EF_MENU_FULL_PATH,EF_MENU_LAYER_NUM,EF_MENU_NAME_1,EF_MENU_NAME_2,EF_MENU_NAME_3,EF_MENU_NAME_4,EF_MENU_NAME_5,EF_SYS_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "LAST_FORM_FULL_PATH","LAST_MENU_ID","MENU_FULL_PATH","MENU_LAYER_NUM","MENU_NAME_1","MENU_NAME_2","MENU_NAME_3","MENU_NAME_4","MENU_NAME_5","SYS_ID" });
				 return DbColumn;
			}
		 }
	}
}
