using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.IOC;
using Breezee.Framework.Mini.Entity;
using Breezee.Framework.Mini.IDAL;

namespace Breezee.Framework.Mini.DAL
{
    public abstract class DMiniDBInitializer : DBInitializer
    {
        public override IDataAccess DataAccess { get { return ContainerContext.Container.Resolve<IDataAccess>("IDataAccessMini"); } }
    }
}
