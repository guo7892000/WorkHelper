using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.IBLL
{
    public abstract class IDBTSystemSet : IDBToolModule
    {
        public abstract IDictionary<string, object> InitDB(IDictionary<string, string> dicQuery);
    }
}
