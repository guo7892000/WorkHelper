using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_V_COM_PROVICE_CITY : DbEntity
	{

		//自动生成表名
		public static string TName = "V_COM_PROVICE_CITY"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_V_COM_PROVICE_CITY();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "V_COM_PROVICE_CITY";} }
		//自动生成字段
		 public static string CITY_CODE = "CITY_CODE";
		 public DbField EF_CITY_CODE = new DbField().DBColName("CITY_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CITY_ID = "CITY_ID";
		 public DbField EF_CITY_ID = new DbField().DBColName("CITY_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CITY_NAME = "CITY_NAME";
		 public DbField EF_CITY_NAME = new DbField().DBColName("CITY_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COUNTY_CODE = "COUNTY_CODE";
		 public DbField EF_COUNTY_CODE = new DbField().DBColName("COUNTY_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COUNTY_ID = "COUNTY_ID";
		 public DbField EF_COUNTY_ID = new DbField().DBColName("COUNTY_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COUNTY_NAME = "COUNTY_NAME";
		 public DbField EF_COUNTY_NAME = new DbField().DBColName("COUNTY_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PROVINCE_CODE = "PROVINCE_CODE";
		 public DbField EF_PROVINCE_CODE = new DbField().DBColName("PROVINCE_CODE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PROVINCE_ID = "PROVINCE_ID";
		 public DbField EF_PROVINCE_ID = new DbField().DBColName("PROVINCE_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PROVINCE_NAME = "PROVINCE_NAME";
		 public DbField EF_PROVINCE_NAME = new DbField().DBColName("PROVINCE_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_CITY_CODE,EF_CITY_ID,EF_CITY_NAME,EF_COUNTY_CODE,EF_COUNTY_ID,EF_COUNTY_NAME,EF_PROVINCE_CODE,EF_PROVINCE_ID,EF_PROVINCE_NAME });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "CITY_CODE","CITY_ID","CITY_NAME","COUNTY_CODE","COUNTY_ID","COUNTY_NAME","PROVINCE_CODE","PROVINCE_ID","PROVINCE_NAME" });
				 return DbColumn;
			}
		 }
	}
}
