using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************		
 * 对象名称：控制层抽象类		
 * 对象类别：抽象类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 23:30:29		
 * 对象说明：一般提供API地址		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 23:30:29 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// Controller接口
    /// </summary>
    public abstract class Controller : IController
    {
        public abstract IModule Module { get; set; }
    }
}