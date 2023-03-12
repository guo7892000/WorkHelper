using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.DAL
{
    public abstract class DDBTDBInitializer : DBInitializer
    {
        public override IDataAccess DataAccess { get { return ContainerContext.Container.Resolve<IDataAccess>(DBTGlobalValue.DataAccessConfigKey); } }
    }
}
