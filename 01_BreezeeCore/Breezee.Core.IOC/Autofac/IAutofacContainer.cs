using Castle.MicroKernel;
using System;
using System.Collections;

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
    /// <summary>
    /// IoC窗口接口
    /// </summary>
    public interface IAutofacContainer : IDisposable
    {
        T Resolve<T>();
        T Resolve<T>(string key);
    }
}
