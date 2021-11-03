using Breezee.Framework.DataAccess.INF;
using Breezee.Framework.Mini.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.IDAL
{
    public abstract class IDMiniDBInitializer : IMiniModule
    {
        public abstract void InitDB();
    }
}
