using Breezee.Core.Interface;
using System.Collections.Generic;

/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/14 23:29:46	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/14 23:29:46 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 类
    /// </summary>
    public abstract class App : IApp
    {
        List<IModule> _Modules = new List<IModule>(); 
        AppEntity appEntity = new AppEntity();

        public abstract IConfig Config { get; set; }       
        public List<IModule> Modules { get => _Modules; }
        public abstract IMenu Menu { get; }       
        public AppEntity Root { get => appEntity; }
        public abstract void Init();

        public void SetMain()
        {
            CoreGlobalContext.Instance.MainApp = this;
        }
    }
}