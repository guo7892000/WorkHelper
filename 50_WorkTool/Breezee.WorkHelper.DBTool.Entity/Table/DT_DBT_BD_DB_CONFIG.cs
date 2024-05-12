using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    [Serializable]
    public partial class DT_DBT_BD_DB_CONFIG : DbEntity
    {
        public static string TName = "DBT_BD_DB_CONFIG"; //自动生成表名
		public override string DBTableNameCN { get{ return ""; } }
		public override string DBTableComment { get{ return ""; } }

        public static DbEntity NewEntity()
        {
            return new DT_DBT_BD_DB_CONFIG();//本身实例
        }
        public override DbObjectType EntType { get { return DbObjectType.Table; } }
        public override string DBTableName { get { return "DBT_BD_DB_CONFIG"; } }
        //自动生成列属性字段
        public DbField EF_DB_CONFIG_ID = DbField.New().DBColName("DB_CONFIG_ID").DBColType("varchar").DBTypeSize("varchar(36)").PK().DBLen(36);
        public DbField EF_DB_CONFIG_CODE = DbField.New().DBColName("DB_CONFIG_CODE").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public DbField EF_DB_CONFIG_NAME = DbField.New().DBColName("DB_CONFIG_NAME").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public DbField EF_DB_TYPE = DbField.New().DBColName("DB_TYPE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public DbField EF_SERVER_IP = DbField.New().DBColName("SERVER_IP").DBColType("varchar").DBTypeSize("varchar(400)").DBLen(400);
        public DbField EF_PORT_NO = DbField.New().DBColName("PORT_NO").DBColType("varchar").DBTypeSize("varchar(20)").DBLen(20);
        public DbField EF_SCHEMA_NAME = DbField.New().DBColName("SCHEMA_NAME").DBColType("varchar").DBTypeSize("varchar(30)").DBLen(30);
        public DbField EF_DB_NAME = DbField.New().DBColName("DB_NAME").DBColType("varchar").DBTypeSize("varchar(30)").DBLen(30);
        public DbField EF_USER_NAME = DbField.New().DBColName("USER_NAME").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public DbField EF_USER_PASSWORD = DbField.New().DBColName("USER_PASSWORD").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public DbField EF_LOGIN_TYPE = DbField.New().DBColName("LOGIN_TYPE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public DbField EF_TYPE_DESC = DbField.New().DBColName("TYPE_DESC").DBColType("varchar").DBTypeSize("varchar(200)").DBLen(200);
        public DbField EF_SORT_ID = DbField.New().DBColName("SORT_ID").DBColType("int").DBTypeSize("int");
        public DbField EF_REMARK = DbField.New().DBColName("REMARK").DBColType("varchar").DBTypeSize("varchar(200)").DBLen(200);
        public DbField EF_CREATE_TIME = DbField.New().DBColName("CREATE_TIME").DBColType("datetime").DBTypeSize("datetime");
        public DbField EF_CREATOR_ID = DbField.New().DBColName("CREATOR_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public DbField EF_CREATOR = DbField.New().DBColName("CREATOR").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public DbField EF_MODIFIER_ID = DbField.New().DBColName("MODIFIER_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public DbField EF_MODIFIER = DbField.New().DBColName("MODIFIER").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public DbField EF_LAST_UPDATED_TIME = DbField.New().DBColName("LAST_UPDATED_TIME").DBColType("datetime").DBTypeSize("datetime");
        public DbField EF_IS_ENABLED = DbField.New().DBColName("IS_ENABLED").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public DbField EF_IS_SYSTEM = DbField.New().DBColName("IS_SYSTEM").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public DbField EF_ORG_ID = DbField.New().DBColName("ORG_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public DbField EF_UPDATE_CONTROL_ID = DbField.New().DBColName("UPDATE_CONTROL_ID").DBColType("varchar").DBTypeSize("varchar(36)").DBLen(36);
        public DbField EF_TFLAG = DbField.New().DBColName("TFLAG").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
 

        public override List<DbField> DbColumnList
        {
            get
            {
                var DbColumn = new List<DbField>();
                DbColumn.AddRange(new DbField[] { EF_DB_CONFIG_ID,EF_DB_CONFIG_CODE,EF_DB_CONFIG_NAME,EF_DB_TYPE,EF_SERVER_IP,EF_PORT_NO,EF_SCHEMA_NAME,EF_DB_NAME,EF_USER_NAME,EF_USER_PASSWORD,EF_LOGIN_TYPE,EF_TYPE_DESC,EF_SORT_ID,EF_REMARK,EF_CREATE_TIME,EF_CREATOR_ID,EF_CREATOR,EF_MODIFIER_ID,EF_MODIFIER,EF_LAST_UPDATED_TIME,EF_IS_ENABLED,EF_IS_SYSTEM,EF_ORG_ID,EF_UPDATE_CONTROL_ID,EF_TFLAG });
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