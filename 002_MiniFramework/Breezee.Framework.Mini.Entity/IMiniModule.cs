using Breezee.Framework.DataAccess.INF;
using Breezee.Global.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// IMiniBaseDAC模块数据库访问层基类
    /// IDAL下的所有对象都应继承于它
    /// </summary>
    public abstract class IMiniModule : IGlobalModule
    {
        public override string ModuleID { get { return "DBTool"; } }
        public override string ModuleName { get { return "迷你框架"; } }
        public override IDataAccess DataAccess { get { return ContainerContext.Container.Resolve<IDataAccess>("IDataAccessMini"); } }
    }
}
