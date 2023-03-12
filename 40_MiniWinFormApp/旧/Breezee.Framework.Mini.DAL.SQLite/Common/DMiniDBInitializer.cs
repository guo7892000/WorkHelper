using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.Framework.Mini.IDAL;

namespace Breezee.Framework.Mini.DAL.SQLite
{
    public class DMiniDBInitializer : DAL.DMiniDBInitializer
    {
        public virtual string Id => "289F12B4-40FB-41AF-9128-1A181F9A3E2E";

        public virtual string Code => "Mini_SQLITE";

        public virtual string Name => "迷你框架-SQLITE";

        public virtual string Desc => "迷你框架，使用SQLITE数据库";

        public override void InitDataBase()
        {
            DropObject();
            InitTableStruct();
            InitTableData();
        }

        public override void DropObject()
        {
            DataAccess.ExecuteNonQueryHadParamSql(@"
            DROP TABLE IF EXISTS ""SYS_USER"" ;
            DROP TABLE IF EXISTS ""SYS_LOG"" ;
            ", new Dictionary<string, string>());
        }       

        public override void InitFunction()
        {
            //throw new NotImplementedException();
        }

        public override void InitProduce()
        {
            //throw new NotImplementedException();
        }

        public override void InitTableData()
        {
            string sSql = @"INSERT INTO ""main"".""SYS_USER"" (""USER_ID"", ""USER_CODE"", ""EMP_ID"", ""USER_NAME"", ""USER_NAME_EN"", ""USER_PASSWORD"", ""USER_TYPE"", ""ENCRYPT_SALT"", ""PIN_YIN"", ""LAST_LOGIN_TIME"", ""LOGIN_STATE"", ""TICKET_ID"", ""DESCRIPTION"", ""ACTIVE_TIME"", ""DISABLE_TIME"", ""SORT_ID"", ""REMARK"", ""CREATE_TIME"", ""CREATOR_ID"", ""CREATOR"", ""MODIFIER_ID"", ""MODIFIER"", ""LAST_UPDATED_TIME"", ""IS_ENABLED"", ""IS_SYSTEM"", ""ORG_ID"", ""UPDATE_CONTROL_ID"", ""TFLAG"", ""ROWID"") VALUES ('B0C0AB05-3680-48BF-83D7-92A1AE2584BE', 'xtadmin', '', '系统管理员', 'admin', '1000:ZABU9kVvhaDIQ9iytGv3wifttihEHNrU:l9E/XP0BgNr0YIZ6pfTuXhGuzE3YFA1Z', 1, NULL, NULL, NULL, NULL, NULL, NULL, '2015-04-19 20:21:48', '2042-09-04 20:21:48', 1, '', '2015-04-19 20:21:48', 1, 'SYSTEM', 1, 'SYSTEM', '2015-04-19 20:21:48', 1, 0, 1, 'D053D241-4B34-4F92-8E2F-868E0FB4CEC1', 0, 3);";
            DataAccess.ExecuteNonQueryHadParamSql(sSql, new Dictionary<string, string>());
        }

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
                  1 新增用户表（SYS_USER）
                  2 新增系统日志表（SYS_LOG）
            ***********************************************************************************/
            /*1、新增表：用户表(SYS_USER)*/
            CREATE TABLE IF NOT EXISTS ""SYS_USER"" (
            USER_ID varchar(36) PRIMARY KEY  NOT NULL,/*用户ID*/
            USER_CODE varchar(30) NOT NULL,/*用户编码*/
            EMP_ID varchar(36),/*职员ID*/
            USER_NAME varchar(50),/*用户名*/
            USER_NAME_EN varchar(100),/*用户英文名*/
            USER_PASSWORD varchar(200),/*用户密码*/
            USER_TYPE varchar(10) NOT NULL,/*用户类型*/
            ENCRYPT_SALT varchar(50),/*加密盐*/
            PIN_YIN varchar(60),/*拼音码*/
            LAST_LOGIN_TIME datetime,/*最后登录时间*/
            LOGIN_STATE int,/*登录状态*/
            TICKET_ID varchar(36),/*令牌号*/
            DESCRIPTION varchar(255),/*描述*/
            ACTIVE_TIME datetime  NOT NULL,/*激活时间*/
            DISABLE_TIME datetime  NOT NULL,/*失效时间*/
            SORT_ID int,/*排序ID*/
            REMARK varchar(200),/*备注*/
            CREATE_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*创建时间*/
            CREATOR_ID varchar(36) NOT NULL,/*创建人ID*/
            CREATOR varchar(50),/*创建人*/
            MODIFIER_ID varchar(36),/*修改人ID*/
            MODIFIER varchar(50),/*修改人*/
            LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*最后更新时间*/
            IS_ENABLED varchar(2) NOT NULL  DEFAULT 1,/*是否有效*/
            IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0,/*系统标志*/
            ORG_ID varchar(36) NOT NULL,/*组织ID*/
            UPDATE_CONTROL_ID varchar(36) NOT NULL,/*并发控制ID*/
            TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
            );
            /*2、新增表：系统日志表(SYS_LOG)*/
            CREATE TABLE IF NOT EXISTS ""SYS_LOG""(
            LogID varchar(36) PRIMARY KEY  NOT NULL,/*日志ID*/
            AppName varchar(100),/*应用程序名称*/
            ModuleName varchar(50),/*模块名称*/
            ProcName varchar(100),/*处理过程名称*/
            LogLevel varchar(20),/*日志级别*/
            LogTitle varchar(100),/*日志标题*/
            LogMessage varchar(4000),/*日志内容*/
            LogDate datetime,/*记录日期*/
            StackTrace varchar(4000),/*跟踪信息*/
            REMARK varchar(200),/*备注*/
            CREATE_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*创建时间*/
            CREATOR_ID varchar(36),/*创建人ID*/
            CREATOR varchar(50),/*创建人*/
            MODIFIER_ID varchar(36),/*修改人ID*/
            MODIFIER varchar(50),/*修改人*/
            LAST_UPDATED_TIME datetime  NOT NULL  DEFAULT (datetime('now','localtime')),/*最后更新时间*/
            IS_ENABLED varchar(2) NOT NULL  DEFAULT 1,/*是否有效*/
            IS_SYSTEM varchar(2) NOT NULL  DEFAULT 0,/*系统标志*/
            ORG_ID varchar(36),/*组织ID*/
            UPDATE_CONTROL_ID varchar(36) NOT NULL,/*并发控制ID*/
            TFLAG varchar(2) NOT NULL  DEFAULT 0 /*传输标志*/
            );
            ", new Dictionary<string, string>());
        }

        public override void InitView()
        {
            //throw new NotImplementedException();
        }

        public override bool IsNeedInit()
        {
            throw new NotImplementedException();
        }
    }
}
