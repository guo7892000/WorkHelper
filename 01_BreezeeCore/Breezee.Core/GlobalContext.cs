using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：全局上下文类
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/19 22:36:08	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/19 22:36:08 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 全局上下文类
    /// </summary>
    public class GlobalContext
    {
        private static GlobalContext _Instance; //自身的一个静态实例
        private static readonly object lockob = new object();
        public IApp MainApp { get;internal set; }

        public static GlobalContext Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (lockob)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new GlobalContext();
                        }
                    }
                }
                return _Instance;
            }
        }
    }
}