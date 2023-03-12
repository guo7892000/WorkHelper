using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.IDAL
{
    public abstract class IDDBDefaultValue : IDBToolModule
    {
        public abstract DataTable QueryDefaultValue(IDictionary<string, string> dicQuery);

        #region 查询是否存在
        public abstract DataTable QueryDefaultValueExist(IDictionary<string, string> dicQuery);
        #endregion
    }
}
