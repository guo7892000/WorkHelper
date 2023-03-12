using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    [Serializable]
    public partial class DT_DBT_BD_DB_CONFIG : IBaseEntity
    {
        public static string TName = "DBT_BD_DB_CONFIG"; //自动生成表名
		public override string DBTableNameCN { get{ return ""; } }
		public override string DBTableComment { get{ return ""; } }

        public static IBaseEntity NewEntity()
        {
            return new DT_DBT_BD_DB_CONFIG();//本身实例
        }
        public override EntityType EntType { get { return EntityType.Table; } }
        public override string DBTableName { get { return "DBT_BD_DB_CONFIG"; } }
        //自动生成列属性字段
        public BaseEntityField EF_DB_CONFIG_ID = BaseEntityField.New().DBColName("DB_CONFIG_ID").DBColType("varchar").DBTypeSize("varchar(36)").PK().DBLen(36);
        public BaseEntityField EF_DB_CONFIG_CODE = BaseEntityField.New().DBColName("DB_CONFIG_CODE").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_DB_CONFIG_NAME = BaseEntityField.New().DBColName("DB_CONFIG_NAME").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_DB_TYPE = BaseEntityField.New().DBColName("DB_TYPE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_SERVER_IP = BaseEntityField.New().DBColName("SERVER_IP").DBColType("varchar").DBTypeSize("varchar(400)").DBLen(400);
        public BaseEntityField EF_PORT_NO = BaseEntityField.New().DBColName("PORT_NO").DBColType("varchar").DBTypeSize("varchar(20)").DBLen(20);
        public BaseEntityField EF_SCHEMA_NAME = BaseEntityField.New().DBColName("SCHEMA_NAME").DBColType("varchar").DBTypeSize("varchar(30)").DBLen(30);
        public BaseEntityField EF_DB_NAME = BaseEntityField.New().DBColName("DB_NAME").DBColType("varchar").DBTypeSize("varchar(30)").DBLen(30);
        public BaseEntityField EF_USER_NAME = BaseEntityField.New().DBColName("USER_NAME").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_USER_PASSWORD = BaseEntityField.New().DBColName("USER_PASSWORD").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_LOGIN_TYPE = BaseEntityField.New().DBColName("LOGIN_TYPE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_TYPE_DESC = BaseEntityField.New().DBColName("TYPE_DESC").DBColType("varchar").DBTypeSize("varchar(200)").DBLen(200);
        public BaseEntityField EF_SORT_ID = BaseEntityField.New().DBColName("SORT_ID").DBColType("int").DBTypeSize("int");
        public BaseEntityField EF_REMARK = BaseEntityField.New().DBColName("REMARK").DBColType("varchar").DBTypeSize("varchar(200)").DBLen(200);
        public BaseEntityField EF_CREATE_TIME = BaseEntityField.New().DBColName("CREATE_TIME").DBColType("datetime").DBTypeSize("datetime");
        public BaseEntityField EF_CREATOR_ID = BaseEntityField.New().DBColName("CREATOR_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public BaseEntityField EF_CREATOR = BaseEntityField.New().DBColName("CREATOR").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_MODIFIER_ID = BaseEntityField.New().DBColName("MODIFIER_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public BaseEntityField EF_MODIFIER = BaseEntityField.New().DBColName("MODIFIER").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_LAST_UPDATED_TIME = BaseEntityField.New().DBColName("LAST_UPDATED_TIME").DBColType("datetime").DBTypeSize("datetime");
        public BaseEntityField EF_IS_ENABLED = BaseEntityField.New().DBColName("IS_ENABLED").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_IS_SYSTEM = BaseEntityField.New().DBColName("IS_SYSTEM").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_ORG_ID = BaseEntityField.New().DBColName("ORG_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public BaseEntityField EF_UPDATE_CONTROL_ID = BaseEntityField.New().DBColName("UPDATE_CONTROL_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public BaseEntityField EF_TFLAG = BaseEntityField.New().DBColName("TFLAG").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
 

        public override List<BaseEntityField> DbColumnList
        {
            get
            {
                var DbColumn = new List<BaseEntityField>();
                DbColumn.AddRange(new BaseEntityField[] { EF_DB_CONFIG_ID,EF_DB_CONFIG_CODE,EF_DB_CONFIG_NAME,EF_DB_TYPE,EF_SERVER_IP,EF_PORT_NO,EF_SCHEMA_NAME,EF_DB_NAME,EF_USER_NAME,EF_USER_PASSWORD,EF_LOGIN_TYPE,EF_TYPE_DESC,EF_SORT_ID,EF_REMARK,EF_CREATE_TIME,EF_CREATOR_ID,EF_CREATOR,EF_MODIFIER_ID,EF_MODIFIER,EF_LAST_UPDATED_TIME,EF_IS_ENABLED,EF_IS_SYSTEM,EF_ORG_ID,EF_UPDATE_CONTROL_ID,EF_TFLAG });
                return DbColumn;
            }
        }
        public override List<string> ColumnStringList
        {
            get
            {
                var DbColumn = new List<string>();
                DbColumn.AddRange(new string[] { "DB_CONFIG_ID","DB_CONFIG_CODE","DB_CONFIG_NAME","DB_TYPE","SERVER_IP","PORT_NO","SCHEMA_NAME","DB_NAME","USER_NAME","USER_PASSWORD","LOGIN_TYPE","TYPE_DESC","SORT_ID","REMARK","CREATE_TIME","CREATOR_ID","CREATOR","MODIFIER_ID","MODIFIER","LAST_UPDATED_TIME","IS_ENABLED","IS_SYSTEM","ORG_ID","UPDATE_CONTROL_ID","TFLAG" });
                return DbColumn;
            }
        }

        public static class SqlString
        {
            public static string DB_CONFIG_ID = "DB_CONFIG_ID";
            public static string DB_CONFIG_CODE = "DB_CONFIG_CODE";
            public static string DB_CONFIG_NAME = "DB_CONFIG_NAME";
            public static string DB_TYPE = "DB_TYPE";
            public static string SERVER_IP = "SERVER_IP";
            public static string PORT_NO = "PORT_NO";
            public static string SCHEMA_NAME = "SCHEMA_NAME";
            public static string DB_NAME = "DB_NAME";
            public static string USER_NAME = "USER_NAME";
            public static string USER_PASSWORD = "USER_PASSWORD";
            public static string LOGIN_TYPE = "LOGIN_TYPE";
            public static string TYPE_DESC = "TYPE_DESC";
            public static string SORT_ID = "SORT_ID";
            public static string REMARK = "REMARK";
            public static string CREATE_TIME = "CREATE_TIME";
            public static string CREATOR_ID = "CREATOR_ID";
            public static string CREATOR = "CREATOR";
            public static string MODIFIER_ID = "MODIFIER_ID";
            public static string MODIFIER = "MODIFIER";
            public static string LAST_UPDATED_TIME = "LAST_UPDATED_TIME";
            public static string IS_ENABLED = "IS_ENABLED";
            public static string IS_SYSTEM = "IS_SYSTEM";
            public static string ORG_ID = "ORG_ID";
            public static string UPDATE_CONTROL_ID = "UPDATE_CONTROL_ID";
            public static string TFLAG = "TFLAG";

        }
    }
}