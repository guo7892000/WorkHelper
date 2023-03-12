using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/19 19:15:15	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/19 19:15:15 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 类
    /// </summary>
    public class DllEntity: IEntity
    {
        public virtual string Guid { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Id => new Guid().ToString();


    }
}