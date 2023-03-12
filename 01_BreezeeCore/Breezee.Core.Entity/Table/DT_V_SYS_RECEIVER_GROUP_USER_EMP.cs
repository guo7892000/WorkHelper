using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_V_SYS_RECEIVER_GROUP_USER_EMP : DbEntity
	{

		//自动生成表名
		public static string TName = "V_SYS_RECEIVER_GROUP_USER_EMP"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_V_SYS_RECEIVER_GROUP_USER_EMP();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "V_SYS_RECEIVER_GROUP_USER_EMP";} }
		//自动生成字段
		 public static string EMP_ID = "EMP_ID";
		 public DbField EF_EMP_ID = new DbField().DBColName("EMP_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string RECEIVER_GROUP_ID = "RECEIVER_GROUP_ID";
		 public DbField EF_RECEIVER_GROUP_ID = new DbField().DBColName("RECEIVER_GROUP_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string USER_ID = "USER_ID";
		 public DbField EF_USER_ID = new DbField().DBColName("USER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_EMP_ID,EF_RECEIVER_GROUP_ID,EF_USER_ID });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "EMP_ID","RECEIVER_GROUP_ID","USER_ID" });
				 return DbColumn;
			}
		 }
	}
}
