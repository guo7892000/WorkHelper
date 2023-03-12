using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_ORG_PERSON : DbEntity
	{

		//自动生成表名
		public static string TName = "ORG_PERSON"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_ORG_PERSON();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "ORG_PERSON";} }
		//自动生成字段
		 public static string ADDR_CITY = "ADDR_CITY";
		 public DbField EF_ADDR_CITY = new DbField().DBColName("ADDR_CITY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ADDR_COUNTY = "ADDR_COUNTY";
		 public DbField EF_ADDR_COUNTY = new DbField().DBColName("ADDR_COUNTY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ADDR_FULL = "ADDR_FULL";
		 public DbField EF_ADDR_FULL = new DbField().DBColName("ADDR_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ADDR_PROVINCE = "ADDR_PROVINCE";
		 public DbField EF_ADDR_PROVINCE = new DbField().DBColName("ADDR_PROVINCE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ADDR_STREET = "ADDR_STREET";
		 public DbField EF_ADDR_STREET = new DbField().DBColName("ADDR_STREET").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string BIRTH_DATE = "BIRTH_DATE";
		 public DbField EF_BIRTH_DATE = new DbField().DBColName("BIRTH_DATE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CERT_NO = "CERT_NO";
		 public DbField EF_CERT_NO = new DbField().DBColName("CERT_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CERT_TYPE = "CERT_TYPE";
		 public DbField EF_CERT_TYPE = new DbField().DBColName("CERT_TYPE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_NAME_1 = "CONTACT_NAME_1";
		 public DbField EF_CONTACT_NAME_1 = new DbField().DBColName("CONTACT_NAME_1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_NAME_2 = "CONTACT_NAME_2";
		 public DbField EF_CONTACT_NAME_2 = new DbField().DBColName("CONTACT_NAME_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_PHONE_1 = "CONTACT_PHONE_1";
		 public DbField EF_CONTACT_PHONE_1 = new DbField().DBColName("CONTACT_PHONE_1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_PHONE_2 = "CONTACT_PHONE_2";
		 public DbField EF_CONTACT_PHONE_2 = new DbField().DBColName("CONTACT_PHONE_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_RELATION_1 = "CONTACT_RELATION_1";
		 public DbField EF_CONTACT_RELATION_1 = new DbField().DBColName("CONTACT_RELATION_1").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CONTACT_RELATION_2 = "CONTACT_RELATION_2";
		 public DbField EF_CONTACT_RELATION_2 = new DbField().DBColName("CONTACT_RELATION_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string DEGREE = "DEGREE";
		 public DbField EF_DEGREE = new DbField().DBColName("DEGREE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string DRIVER_TIME = "DRIVER_TIME";
		 public DbField EF_DRIVER_TIME = new DbField().DBColName("DRIVER_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EMAIL = "EMAIL";
		 public DbField EF_EMAIL = new DbField().DBColName("EMAIL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FAX_NO = "FAX_NO";
		 public DbField EF_FAX_NO = new DbField().DBColName("FAX_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string HOME_PERSON = "HOME_PERSON";
		 public DbField EF_HOME_PERSON = new DbField().DBColName("HOME_PERSON").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string HOME_PHONE = "HOME_PHONE";
		 public DbField EF_HOME_PHONE = new DbField().DBColName("HOME_PHONE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ID_CARD_NO = "ID_CARD_NO";
		 public DbField EF_ID_CARD_NO = new DbField().DBColName("ID_CARD_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_DRIVER = "IS_DRIVER";
		 public DbField EF_IS_DRIVER = new DbField().DBColName("IS_DRIVER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_ENABLED = "IS_ENABLED";
		 public DbField EF_IS_ENABLED = new DbField().DBColName("IS_ENABLED").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string IS_SYSTEM = "IS_SYSTEM";
		 public DbField EF_IS_SYSTEM = new DbField().DBColName("IS_SYSTEM").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string LAST_UPDATED_TIME = "LAST_UPDATED_TIME";
		 public DbField EF_LAST_UPDATED_TIME = new DbField().DBColName("LAST_UPDATED_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MARITAL_STATU = "MARITAL_STATU";
		 public DbField EF_MARITAL_STATU = new DbField().DBColName("MARITAL_STATU").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER = "MODIFIER";
		 public DbField EF_MODIFIER = new DbField().DBColName("MODIFIER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string MODIFIER_ID = "MODIFIER_ID";
		 public DbField EF_MODIFIER_ID = new DbField().DBColName("MODIFIER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string NATION = "NATION";
		 public DbField EF_NATION = new DbField().DBColName("NATION").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string NATIVE_PLACE = "NATIVE_PLACE";
		 public DbField EF_NATIVE_PLACE = new DbField().DBColName("NATIVE_PLACE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string OFFICE_PHONE = "OFFICE_PHONE";
		 public DbField EF_OFFICE_PHONE = new DbField().DBColName("OFFICE_PHONE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ORG_ID = "ORG_ID";
		 public DbField EF_ORG_ID = new DbField().DBColName("ORG_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_ABILITY = "PERSON_ABILITY";
		 public DbField EF_PERSON_ABILITY = new DbField().DBColName("PERSON_ABILITY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_ID = "PERSON_ID";
		 public DbField EF_PERSON_ID = new DbField().DBColName("PERSON_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_INTEREST = "PERSON_INTEREST";
		 public DbField EF_PERSON_INTEREST = new DbField().DBColName("PERSON_INTEREST").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_NAME = "PERSON_NAME";
		 public DbField EF_PERSON_NAME = new DbField().DBColName("PERSON_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_NAME_EN = "PERSON_NAME_EN";
		 public DbField EF_PERSON_NAME_EN = new DbField().DBColName("PERSON_NAME_EN").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_PHONE = "PERSON_PHONE";
		 public DbField EF_PERSON_PHONE = new DbField().DBColName("PERSON_PHONE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PERSON_PHONE_2 = "PERSON_PHONE_2";
		 public DbField EF_PERSON_PHONE_2 = new DbField().DBColName("PERSON_PHONE_2").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string POLITICS = "POLITICS";
		 public DbField EF_POLITICS = new DbField().DBColName("POLITICS").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string QQ = "QQ";
		 public DbField EF_QQ = new DbField().DBColName("QQ").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SCHOOL = "SCHOOL";
		 public DbField EF_SCHOOL = new DbField().DBColName("SCHOOL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SEX = "SEX";
		 public DbField EF_SEX = new DbField().DBColName("SEX").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ZIP = "ZIP";
		 public DbField EF_ZIP = new DbField().DBColName("ZIP").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_ADDR_CITY,EF_ADDR_COUNTY,EF_ADDR_FULL,EF_ADDR_PROVINCE,EF_ADDR_STREET,EF_BIRTH_DATE,EF_CERT_NO,EF_CERT_TYPE,EF_CONTACT_NAME_1,EF_CONTACT_NAME_2,EF_CONTACT_PHONE_1,EF_CONTACT_PHONE_2,EF_CONTACT_RELATION_1,EF_CONTACT_RELATION_2,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_DEGREE,EF_DRIVER_TIME,EF_EMAIL,EF_FAX_NO,EF_HOME_PERSON,EF_HOME_PHONE,EF_ID_CARD_NO,EF_IS_DRIVER,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MARITAL_STATU,EF_MODIFIER,EF_MODIFIER_ID,EF_NATION,EF_NATIVE_PLACE,EF_OFFICE_PHONE,EF_ORG_ID,EF_PERSON_ABILITY,EF_PERSON_ID,EF_PERSON_INTEREST,EF_PERSON_NAME,EF_PERSON_NAME_EN,EF_PERSON_PHONE,EF_PERSON_PHONE_2,EF_POLITICS,EF_QQ,EF_REMARK,EF_SCHOOL,EF_SEX,EF_SORT_ID,EF_TFLAG,EF_UPDATE_CONTROL_ID,EF_ZIP });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "ADDR_CITY","ADDR_COUNTY","ADDR_FULL","ADDR_PROVINCE","ADDR_STREET","BIRTH_DATE","CERT_NO","CERT_TYPE","CONTACT_NAME_1","CONTACT_NAME_2","CONTACT_PHONE_1","CONTACT_PHONE_2","CONTACT_RELATION_1","CONTACT_RELATION_2","CREATE_TIME","CREATOR","CREATOR_ID","DEGREE","DRIVER_TIME","EMAIL","FAX_NO","HOME_PERSON","HOME_PHONE","ID_CARD_NO","IS_DRIVER","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MARITAL_STATU","MODIFIER","MODIFIER_ID","NATION","NATIVE_PLACE","OFFICE_PHONE","ORG_ID","PERSON_ABILITY","PERSON_ID","PERSON_INTEREST","PERSON_NAME","PERSON_NAME_EN","PERSON_PHONE","PERSON_PHONE_2","POLITICS","QQ","REMARK","SCHOOL","SEX","SORT_ID","TFLAG","UPDATE_CONTROL_ID","ZIP" });
				 return DbColumn;
			}
		 }
	}
}
