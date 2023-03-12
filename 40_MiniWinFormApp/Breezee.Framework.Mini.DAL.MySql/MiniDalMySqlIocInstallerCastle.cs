using Breezee.Core;
using Breezee.Framework.Mini.IDAL;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/26 22:15:46	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/26 22:15:46 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Framework.Mini.DAL
{
    /// <summary>
    /// 类
    /// </summary>
    public class MiniDalMySqlIocInstallerCastle: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDMiniLogin>().ImplementedBy<MySql.DMiniLogin>());
            container.Register(Component.For<IDMiniSystem>().ImplementedBy<MySql.DMiniSystem>());
            container.Register(Component.For<IDBInitializer>().ImplementedBy<MySql.DMiniDBInitializer>());
        }
    }
}