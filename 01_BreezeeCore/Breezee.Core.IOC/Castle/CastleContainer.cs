﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System.Collections;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using System.Reflection;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：
 * container.Install(
   FromAssembly.This(),
   FromAssembly.Named("Acme.Crm.Bootstrap"),
   FromAssembly.Containing<ServicesInstaller>(),
   FromAssembly.InDirectory(new AssemblyFilter("Extensions")),
   FromAssembly.Instance(this.GetPluginAssembly()));		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.IOC
{
    /// <summary>
    /// IoC容器实现类
    /// 注：必须放在一起，因为IoC是基础，这里是直接New实现类。
    /// </summary>
    public class CastleContainer : ICastleContainer
    {
        public static string UriConfig = "Config/IOC.Main.config";

        #region IOC容器
        private static readonly object lockob = new object();
        private static IWindsorContainer container;

        /// <summary>
        /// 容器
        /// </summary>
        private static IWindsorContainer InnerContainer
        {
            get
            {
                if (container == null)
                {
                    lock (lockob)
                    {
                        if (container == null)
                        {
                            //创建容器
                            //方式一：使用注册DLL方式。对应DLL中有IOC注册所有接口和实现清单类。优点：能直接配置接口（或父类）与实现类
                            container = new WindsorContainer();
                            foreach (ImplementDllInfo assembly in IoCDllRegister.ImplementDlls)
                            {
                                container.Install(FromAssembly.Named(assembly.AssemblyName));
                            }

                            //方式二：使用XML文件方式，castle的主配置文件在App.config中，不推荐使用。
                            //缺点：接口（或父类）与实现类的配置在XML文件中，当有错时不好发现
                            //container = (IWindsorContainer)new WindsorContainer(new XmlInterpreter(UriConfig));
                            //container = new WindsorContainer(new XmlInterpreter(UriConfig));
                            //container.Install(Configuration.FromXmlFile(UriConfig));
                            //Configuration.FromXml()
                        }
                    }
                }
                return container;
            }
        }
        #endregion

        #region ICastleContainer 成员

        public T Resolve<T>()
        {
            return InnerContainer.Resolve<T>();
        }

        public T Resolve<T>(Arguments arguments)
        {
            return InnerContainer.Resolve<T>(arguments);
        }

        public T Resolve<T>(string key)
        {
            return InnerContainer.Resolve<T>(key);
        }

        public T Resolve<T>(string key, Arguments arguments)
        {
            return InnerContainer.Resolve<T>(key, arguments);
        }


        public T[] ResolveAll<T>()
        {
            return InnerContainer.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(Arguments arguments)
        {
            return InnerContainer.ResolveAll<T>(arguments);
        }

        public IWindsorContainer Install(IWindsorInstaller[] installers)
        {
            return InnerContainer.Install(installers);
        }

        public void Dispose()
        {
            InnerContainer.Dispose();
        }

        #endregion
    }
}
