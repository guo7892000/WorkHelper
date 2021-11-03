using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public enum SQLCreateType
    {
        /// <summary>
        /// 仅增加
        /// </summary>
        Create,
        /// <summary>
        /// 先删除后增加
        /// </summary>
        Drop_Create,
        /// <summary>
        /// 仅删除
        /// </summary>
        Drop
    }
    public enum TableChangeType
    {
        Create,
        Alter
    }

    public enum ColumnChangeType
    {
        Create,
        Alter,
        Drop,
        Drop_Create
    }

    public enum ColKeyType
    {
        PK,
        FK,
        Empty
    }

    public enum YesNoType
    {
        Yes,
        No
    }
   
    public enum ColumnTemplateType
    {
        /// <summary>
        /// 含所有数据库类型的列模板
        /// </summary>
        AllInOne,
        SqlServer,
        Oracle,
        MySql,
        SQLite,
        PostgreSql
    }
}
