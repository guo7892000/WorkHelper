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
 * 对象名称：Ioc安装类-Mini模块-DAL层
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/26 22:15:46	
 * 对象说明：对于所有数据库类型都通用的实现	
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
    public class MiniDalIocInstallerCastle: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<IDMiniLogin>().ImplementedBy<DMiniLogin>());
            //container.Register(Component.For<IDMiniSystem>().ImplementedBy<DMiniSystem>());
            //container.Register(Component.For<IDBInitializer>().ImplementedBy<DMiniDBInitializer>());
        }
    }
}