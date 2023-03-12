using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    public static class TableSqlCommon
    {
        public static ColumnChangeType GetColumnChangeType(string s)
        {
            if (s.Equals("新增")) return ColumnChangeType.Create;
            if (s.Equals("修改")) return ColumnChangeType.Alter;
            if (s.Equals("删除")) return ColumnChangeType.Drop;
            if (s.Equals("先删后增")) return ColumnChangeType.Drop_Create;
            return ColumnChangeType.Create;
        }

        public static ColKeyType GetColumnKeyType(string s)
        {
            if (s.Equals("PK")) return ColKeyType.PK;
            if (s.Equals("FK")) return ColKeyType.FK;
            return ColKeyType.Empty;
        }

        public static YesNoType GetYesNoType(string s)
        {
            if (s.Equals("是")) return YesNoType.Yes;
            return YesNoType.No;
        }
    }
}
