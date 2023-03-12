using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.Core.Entity
{
	[Serializable]
	public partial class DT_ORG_COMPANY : DbEntity
	{

		//自动生成表名
		public static string TName = "ORG_COMPANY"; ////本身实例
		public static DbEntity NewEntity(){ return new DT_ORG_COMPANY();} ////本身实例
		 public override DbObjectType EntType {  get {return DbObjectType.Table;} }
		 public override string DBTableName {  get {return "ORG_COMPANY";} }
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

		 public static string COMP_CODE_FULL = "COMP_CODE_FULL";
		 public DbField EF_COMP_CODE_FULL = new DbField().DBColName("COMP_CODE_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_CODE_SHORT = "COMP_CODE_SHORT";
		 public DbField EF_COMP_CODE_SHORT = new DbField().DBColName("COMP_CODE_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_DESC = "COMP_DESC";
		 public DbField EF_COMP_DESC = new DbField().DBColName("COMP_DESC").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_ID = "COMP_ID";
		 public DbField EF_COMP_ID = new DbField().DBColName("COMP_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_KIND = "COMP_KIND";
		 public DbField EF_COMP_KIND = new DbField().DBColName("COMP_KIND").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_NAME_FULL = "COMP_NAME_FULL";
		 public DbField EF_COMP_NAME_FULL = new DbField().DBColName("COMP_NAME_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string COMP_NAME_SHORT = "COMP_NAME_SHORT";
		 public DbField EF_COMP_NAME_SHORT = new DbField().DBColName("COMP_NAME_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATE_TIME = "CREATE_TIME";
		 public DbField EF_CREATE_TIME = new DbField().DBColName("CREATE_TIME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR = "CREATOR";
		 public DbField EF_CREATOR = new DbField().DBColName("CREATOR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string CREATOR_ID = "CREATOR_ID";
		 public DbField EF_CREATOR_ID = new DbField().DBColName("CREATOR_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string EMAIL = "EMAIL";
		 public DbField EF_EMAIL = new DbField().DBColName("EMAIL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FAX1_NO = "FAX1_NO";
		 public DbField EF_FAX1_NO = new DbField().DBColName("FAX1_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FAX2_NO = "FAX2_NO";
		 public DbField EF_FAX2_NO = new DbField().DBColName("FAX2_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string FORM_CODE_SHORT = "FORM_CODE_SHORT";
		 public DbField EF_FORM_CODE_SHORT = new DbField().DBColName("FORM_CODE_SHORT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string GROUP_ID = "GROUP_ID";
		 public DbField EF_GROUP_ID = new DbField().DBColName("GROUP_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string GUNO = "GUNO";
		 public DbField EF_GUNO = new DbField().DBColName("GUNO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ACCOUNT = "INV_ACCOUNT";
		 public DbField EF_INV_ACCOUNT = new DbField().DBColName("INV_ACCOUNT").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ADDR = "INV_ADDR";
		 public DbField EF_INV_ADDR = new DbField().DBColName("INV_ADDR").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ADDR_CITY = "INV_ADDR_CITY";
		 public DbField EF_INV_ADDR_CITY = new DbField().DBColName("INV_ADDR_CITY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ADDR_COUNTY = "INV_ADDR_COUNTY";
		 public DbField EF_INV_ADDR_COUNTY = new DbField().DBColName("INV_ADDR_COUNTY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ADDR_PROVINCE = "INV_ADDR_PROVINCE";
		 public DbField EF_INV_ADDR_PROVINCE = new DbField().DBColName("INV_ADDR_PROVINCE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_ADDR_STREET = "INV_ADDR_STREET";
		 public DbField EF_INV_ADDR_STREET = new DbField().DBColName("INV_ADDR_STREET").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_BANK_NAME = "INV_BANK_NAME";
		 public DbField EF_INV_BANK_NAME = new DbField().DBColName("INV_BANK_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string INV_BANK_NAME_FULL = "INV_BANK_NAME_FULL";
		 public DbField EF_INV_BANK_NAME_FULL = new DbField().DBColName("INV_BANK_NAME_FULL").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

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

		 public static string ORG_SCALE = "ORG_SCALE";
		 public DbField EF_ORG_SCALE = new DbField().DBColName("ORG_SCALE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PARENT_COMP_ID = "PARENT_COMP_ID";
		 public DbField EF_PARENT_COMP_ID = new DbField().DBColName("PARENT_COMP_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PHONE1_NO = "PHONE1_NO";
		 public DbField EF_PHONE1_NO = new DbField().DBColName("PHONE1_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PHONE2_NO = "PHONE2_NO";
		 public DbField EF_PHONE2_NO = new DbField().DBColName("PHONE2_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PHONE3_NO = "PHONE3_NO";
		 public DbField EF_PHONE3_NO = new DbField().DBColName("PHONE3_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PRINCIPAL_GENDER = "PRINCIPAL_GENDER";
		 public DbField EF_PRINCIPAL_GENDER = new DbField().DBColName("PRINCIPAL_GENDER").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string PRINCIPAL_NAME = "PRINCIPAL_NAME";
		 public DbField EF_PRINCIPAL_NAME = new DbField().DBColName("PRINCIPAL_NAME").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REGISTER_CITY = "REGISTER_CITY";
		 public DbField EF_REGISTER_CITY = new DbField().DBColName("REGISTER_CITY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REGISTER_COUNTY = "REGISTER_COUNTY";
		 public DbField EF_REGISTER_COUNTY = new DbField().DBColName("REGISTER_COUNTY").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REGISTER_PROVINCE = "REGISTER_PROVINCE";
		 public DbField EF_REGISTER_PROVINCE = new DbField().DBColName("REGISTER_PROVINCE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REGISTER_STREET = "REGISTER_STREET";
		 public DbField EF_REGISTER_STREET = new DbField().DBColName("REGISTER_STREET").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string REMARK = "REMARK";
		 public DbField EF_REMARK = new DbField().DBColName("REMARK").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string SORT_ID = "SORT_ID";
		 public DbField EF_SORT_ID = new DbField().DBColName("SORT_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TAX_NO = "TAX_NO";
		 public DbField EF_TAX_NO = new DbField().DBColName("TAX_NO").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TFLAG = "TFLAG";
		 public DbField EF_TFLAG = new DbField().DBColName("TFLAG").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string TRADE = "TRADE";
		 public DbField EF_TRADE = new DbField().DBColName("TRADE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
		 public DbField EF_UPDATE_CONTROL_ID = new DbField().DBColName("UPDATE_CONTROL_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string WEBSITE = "WEBSITE";
		 public DbField EF_WEBSITE = new DbField().DBColName("WEBSITE").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public static string ZIP = "ZIP";
		 public DbField EF_ZIP = new DbField().DBColName("ZIP").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(true).PK(false).DBLen(36).FieldName("").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

		 public override List<DbField> DbColumnList
		 {
			get
			{
				 var DbColumn = new List<DbField>(); 
				 DbColumn.AddRange(new DbField[] { EF_ADDR_CITY,EF_ADDR_COUNTY,EF_ADDR_FULL,EF_ADDR_PROVINCE,EF_ADDR_STREET,EF_COMP_CODE_FULL,EF_COMP_CODE_SHORT,EF_COMP_DESC,EF_COMP_ID,EF_COMP_KIND,EF_COMP_NAME_FULL,EF_COMP_NAME_SHORT,EF_CREATE_TIME,EF_CREATOR,EF_CREATOR_ID,EF_EMAIL,EF_FAX1_NO,EF_FAX2_NO,EF_FORM_CODE_SHORT,EF_GROUP_ID,EF_GUNO,EF_INV_ACCOUNT,EF_INV_ADDR,EF_INV_ADDR_CITY,EF_INV_ADDR_COUNTY,EF_INV_ADDR_PROVINCE,EF_INV_ADDR_STREET,EF_INV_BANK_NAME,EF_INV_BANK_NAME_FULL,EF_IS_ENABLED,EF_IS_SYSTEM,EF_LAST_UPDATED_TIME,EF_MODIFIER,EF_MODIFIER_ID,EF_ORG_ID,EF_ORG_SCALE,EF_PARENT_COMP_ID,EF_PHONE1_NO,EF_PHONE2_NO,EF_PHONE3_NO,EF_PRINCIPAL_GENDER,EF_PRINCIPAL_NAME,EF_REGISTER_CITY,EF_REGISTER_COUNTY,EF_REGISTER_PROVINCE,EF_REGISTER_STREET,EF_REMARK,EF_SORT_ID,EF_TAX_NO,EF_TFLAG,EF_TRADE,EF_UPDATE_CONTROL_ID,EF_WEBSITE,EF_ZIP });
				 return DbColumn;
			}
		 }
		 public override List<string> ColumnStringList
		 {
			get
			{
				 var DbColumn = new List<string>();
				 DbColumn.AddRange(new string[] { "ADDR_CITY","ADDR_COUNTY","ADDR_FULL","ADDR_PROVINCE","ADDR_STREET","COMP_CODE_FULL","COMP_CODE_SHORT","COMP_DESC","COMP_ID","COMP_KIND","COMP_NAME_FULL","COMP_NAME_SHORT","CREATE_TIME","CREATOR","CREATOR_ID","EMAIL","FAX1_NO","FAX2_NO","FORM_CODE_SHORT","GROUP_ID","GUNO","INV_ACCOUNT","INV_ADDR","INV_ADDR_CITY","INV_ADDR_COUNTY","INV_ADDR_PROVINCE","INV_ADDR_STREET","INV_BANK_NAME","INV_BANK_NAME_FULL","IS_ENABLED","IS_SYSTEM","LAST_UPDATED_TIME","MODIFIER","MODIFIER_ID","ORG_ID","ORG_SCALE","PARENT_COMP_ID","PHONE1_NO","PHONE2_NO","PHONE3_NO","PRINCIPAL_GENDER","PRINCIPAL_NAME","REGISTER_CITY","REGISTER_COUNTY","REGISTER_PROVINCE","REGISTER_STREET","REMARK","SORT_ID","TAX_NO","TFLAG","TRADE","UPDATE_CONTROL_ID","WEBSITE","ZIP" });
				 return DbColumn;
			}
		 }
	}
}
