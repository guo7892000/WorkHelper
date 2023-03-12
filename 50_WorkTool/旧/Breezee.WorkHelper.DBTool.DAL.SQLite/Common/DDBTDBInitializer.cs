using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.WorkHelper.DBTool.IDAL;

namespace Breezee.WorkHelper.DBTool.DAL.SQLite
{
    public class DDBTDBInitializer : DAL.DDBTDBInitializer
    {
        public override void DropObject()
        {
            DataAccess.ExecuteNonQueryHadParamSql(@"
            DROP TABLE IF EXISTS ""DBT_BD_DB_CONFIG"" ;
            DROP TABLE IF EXISTS ""DBT_BD_COLUMN_DEFAULT"" ;
            ", new Dictionary<string, string>());
        }

        public override void InitDataBase()
        {
            DropObject();
            InitTableStruct();
            InitTableData();
        }

        public override void InitFunction()
        {
            throw new NotImplementedException();
        }

        public override void InitProduce()
        {
            throw new NotImplementedException();
        }

        public override void InitTableData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 生成表结构【可重复运行】
        /// 注：SQL语句中的双引用请用两个双引号代替
        /// </summary>
        public override void InitTableStruct()
        {
            DataAccess.ExecuteNonQueryHadParamSql(@"
            /***********************************************************************************
            * 脚本描述: 新增修改表
            * 创建作者: 
            * 创建日期: 2021/11/2 
            * 使用模块：
            * 使用版本: 
            * 说    明：
                  1 新增数据库配置（DBT_BD_DB_CONFIG）
                  2 新增通用字段默认值配置（DBT_BD_COLUMN_DEFAULT）
            ***********************************************************************************/
            /*1、新增表：数据库配置(DBT_BD_DB_CONFIG)*/
            CREATE TABLE IF NOT EXISTS ""DBT_BD_DB_CONFIG"" (
            DB_CONFIG_ID varchar(36) PRIMARY KEY  NOT NULL,/*数据库配置ID*/
            DB_CONFIG_CODE varchar(50),/*数据库配置编码*/
            DB_CONFIG_NAME varchar(100),/*数据库配置名称*/
            DB_TYPE varchar(2),/*数据库类型*/
            SERVER_IP varchar(400),/*服务器IP*/
            PORT_NO varchar(20),/*端口号*/
            SCHEMA_NAME varchar(30),/*架构名称*/
            DB_NAME varchar(30),/*数据库名称*/
            USER_NAME varchar(50),/*用户名*/
            USER_PASSWORD varchar(50),/*用户密码*/
            LOGIN_TYPE varchar(2),/*登录类型*/
            TYPE_DESC varchar(200),/*描述*/
            SORT_ID int,/*排序ID*/
            REMARK varchar(200),/*备注*/
            CREATE_TIME datetime  NOT NULL  DEFAULT(datetime('now', 'localtime')),/*创建时间*/
            CREATOR_ID varchar(36) NOT NULL,/*创建人ID*/
            CREATOR varchar(50),/*创建人*/
            MODIFIER_ID varchar(36),/*修改人ID*/
            MODIFIER varchar(50),/*修改人*/
            LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT(datetime('now', 'localtime')),/*最后更新时间*/
            IS_ENABLED varchar(2) NOT NULL  DEFAULT 1,/*是否有效*/
            IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0,/*系统标志*/
            ORG_ID varchar(36) NOT NULL,/*组织ID*/
            UPDATE_CONTROL_ID varchar(36) NOT NULL,/*并发控制ID*/
            TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
            );
            /*2、新增表：通用字段默认值配置(DBT_BD_COLUMN_DEFAULT)*/
            CREATE TABLE IF NOT EXISTS ""DBT_BD_COLUMN_DEFAULT""(
            COL_DEFAULT_ID varchar(36) PRIMARY KEY  NOT NULL,/*列默认配置ID*/
            COLUMN_NAME varchar(50),/*列编码*/
            DEFAULT_MYSQL varchar(100),/*MySql默认值*/
            DEFAULT_POSTGRESQL varchar(100),/*PostgreSql默认值*/
            DEFAULT_ORACLE varchar(100),/*Oracle默认值*/
            DEFAULT_SQLSERVER varchar(100),/*SqlServer默认值*/
            DEFAULT_SQLITE varchar(100),/*SQLite默认值*/
            IS_USED_ADD varchar(2),/*是否新增使用*/
            IS_USED_UPDATE varchar(2),/*是否修改使用*/
            IS_CONDITION_QUERY varchar(2),/*是否查询条件*/
            IS_CONDITION_UPDATE varchar(2),/*是否更新条件*/
            IS_CONDITION_DELETE varchar(2),/*是否删除条件*/
            SORT_ID int,/*排序ID*/
            REMARK varchar(200),/*备注*/
            CREATE_TIME datetime  NOT NULL  DEFAULT(datetime('now', 'localtime')),/*创建时间*/
            CREATOR_ID varchar(36) NOT NULL,/*创建人ID*/
            CREATOR varchar(50),/*创建人*/
            MODIFIER_ID varchar(36),/*修改人ID*/
            MODIFIER varchar(50),/*修改人*/
            LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT(datetime('now', 'localtime')),/*最后更新时间*/
            IS_ENABLED varchar(2) NOT NULL  DEFAULT 1,/*是否有效*/
            IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0,/*系统标志*/
            ORG_ID varchar(36) NOT NULL,/*组织ID*/
            UPDATE_CONTROL_ID varchar(36) NOT NULL,/*并发控制ID*/
            TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
            );
            ", new Dictionary<string, string>());
        }

        public override void InitView()
        {
            throw new NotImplementedException();
        }

        public override bool IsNeedInit()
        {
            throw new NotImplementedException();
        }
    }
}
