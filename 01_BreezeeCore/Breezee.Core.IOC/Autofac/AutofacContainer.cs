using Autofac;
using Autofac.Core;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.IOC
{
    public  class AutofacContainer: IAutofacContainer
    {
        private static readonly object lockob = new object();
        private static IContainer container;
        private static ContainerBuilder builder;
        private static IContainer InnerContainer
        {
            get
            {
                if (container == null)
                {
                    lock (lockob)
                    {
                        if (container == null)
                        {
                            //创建构造者
                            builder = new ContainerBuilder();
                            foreach (ImplementDllInfo assembly in IoCDllRegister.ImplementDlls)
                            {
                                builder.RegisterAssemblyModules(Assembly.Load(assembly.AssemblyName));
                            }
                            //根据构造者构建容器
                            container = builder.Build();
                        }
                    }
                }
                return container;
            }
        }

        public T Resolve<T>()
        {
            return InnerContainer.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return InnerContainer.ResolveNamed<T>(key);
        }
        public void Dispose()
        {
            InnerContainer.Dispose();
        }

    }
}
