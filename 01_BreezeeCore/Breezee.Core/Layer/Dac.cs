using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************		
 * 对象名称：数据访问层抽象类		
 * 对象类别：抽象类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:43:54		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:43:54 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    public abstract class Dac : IDac
    {
        public abstract IModule Module { get; set; }
    }
}