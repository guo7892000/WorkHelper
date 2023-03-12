using Breezee.WorkHelper.DBTool.IBLL;
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
namespace Breezee.WorkHelper.DBTool.BLL
{
    /// <summary>
    /// 类
    /// </summary>
    public class DBToolBllIocInstallerCastle: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDBConfigSet>().ImplementedBy<BDBConfigSet>());
            container.Register(Component.For<IDBDefaultValue>().ImplementedBy<BDBDefaultValue>());
            container.Register(Component.For<IDBTSystemSet>().ImplementedBy<BDBTSystemSet>());
        }
    }
}