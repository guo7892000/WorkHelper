using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    [Serializable]
    public partial class DT_DBT_BD_COLUMN_DEFAULT : IBaseEntity
    {
        public static string TName = "DBT_BD_COLUMN_DEFAULT"; //自动生成表名
		public override string DBTableNameCN { get{ return ""; } }
		public override string DBTableComment { get{ return ""; } }

        public static IBaseEntity NewEntity()
        {
            return new DT_DBT_BD_COLUMN_DEFAULT();//本身实例
        }
        public override EntityType EntType { get { return EntityType.Table; } }
        public override string DBTableName { get { return "DBT_BD_COLUMN_DEFAULT"; } }
        //自动生成列属性字段
        public BaseEntityField EF_COL_DEFAULT_ID = BaseEntityField.New().DBColName("COL_DEFAULT_ID").DBColType("varchar").DBTypeSize("varchar(36)").PK().DBLen(36);
        public BaseEntityField EF_COLUMN_NAME = BaseEntityField.New().DBColName("COLUMN_NAME").DBColType("varchar").DBTypeSize("varchar(50)").DBLen(50);
        public BaseEntityField EF_DEFAULT_MYSQL = BaseEntityField.New().DBColName("DEFAULT_MYSQL").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_DEFAULT_POSTGRESQL = BaseEntityField.New().DBColName("DEFAULT_POSTGRESQL").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_DEFAULT_ORACLE = BaseEntityField.New().DBColName("DEFAULT_ORACLE").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_DEFAULT_SQLSERVER = BaseEntityField.New().DBColName("DEFAULT_SQLSERVER").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_DEFAULT_SQLITE = BaseEntityField.New().DBColName("DEFAULT_SQLITE").DBColType("varchar").DBTypeSize("varchar(100)").DBLen(100);
        public BaseEntityField EF_IS_USED_ADD = BaseEntityField.New().DBColName("IS_USED_ADD").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_IS_USED_UPDATE = BaseEntityField.New().DBColName("IS_USED_UPDATE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_IS_CONDITION_QUERY = BaseEntityField.New().DBColName("IS_CONDITION_QUERY").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_IS_CONDITION_UPDATE = BaseEntityField.New().DBColName("IS_CONDITION_UPDATE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
        public BaseEntityField EF_IS_CONDITION_DELETE = BaseEntityField.New().DBColName("IS_CONDITION_DELETE").DBColType("varchar").DBTypeSize("varchar(2)").DBLen(2);
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
                DbColumn.AddRange(new BaseEntityField[] { EF_COL_DEFAULT_ID,EF_COLUMN_NAME,EF_DEFAULT_MYSQL,EF_DEFAULT_POSTGRESQL,EF_DEFAULT_ORACLE,EF_DEFAULT_SQLSERVER,EF_DEFAULT_SQLITE,EF_IS_USED_ADD,EF_IS_USED_UPDATE,EF_IS_CONDITION_QUERY,EF_IS_CONDITION_UPDATE,EF_IS_CONDITION_DELETE,EF_SORT_ID,EF_REMARK,EF_CREATE_TIME,EF_CREATOR_ID,EF_CREATOR,EF_MODIFIER_ID,EF_MODIFIER,EF_LAST_UPDATED_TIME,EF_IS_ENABLED,EF_IS_SYSTEM,EF_ORG_ID,EF_UPDATE_CONTROL_ID,EF_TFLAG });
                return DbColumn;
            }
        }
        public override List<string> ColumnStringList
        {
            get
            {
                var DbColumn = new List<string>();
                DbColumn.AddRange(new string[] { "COL_DEFAULT_ID","COLUMN_NAME","DEFAULT_MYSQL","DEFAULT_POSTGRESQL","DEFAULT_ORACLE","DEFAULT_SQLSERVER","DEFAULT_SQLITE","IS_USED_ADD","IS_USED_UPDATE","IS_CONDITION_QUERY","IS_CONDITION_UPDATE","IS_CONDITION_DELETE","SORT_ID","REMARK","CREATE_TIME","CREATOR_ID","CREATOR","MODIFIER_ID","MODIFIER","LAST_UPDATED_TIME","IS_ENABLED","IS_SYSTEM","ORG_ID","UPDATE_CONTROL_ID","TFLAG" });
                return DbColumn;
            }
        }

        public static class SqlString
        {
            public static string COL_DEFAULT_ID = "COL_DEFAULT_ID";
            public static string COLUMN_NAME = "COLUMN_NAME";
            public static string DEFAULT_MYSQL = "DEFAULT_MYSQL";
            public static string DEFAULT_POSTGRESQL = "DEFAULT_POSTGRESQL";
            public static string DEFAULT_ORACLE = "DEFAULT_ORACLE";
            public static string DEFAULT_SQLSERVER = "DEFAULT_SQLSERVER";
            public static string DEFAULT_SQLITE = "DEFAULT_SQLITE";
            public static string IS_USED_ADD = "IS_USED_ADD";
            public static string IS_USED_UPDATE = "IS_USED_UPDATE";
            public static string IS_CONDITION_QUERY = "IS_CONDITION_QUERY";
            public static string IS_CONDITION_UPDATE = "IS_CONDITION_UPDATE";
            public static string IS_CONDITION_DELETE = "IS_CONDITION_DELETE";
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