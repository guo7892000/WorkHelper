using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/16 23:13:05	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/16 23:13:05 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 类
    /// </summary>
    public class UserEntity : Entity
    {
        public virtual string UserId => "UserID";
        public virtual string UserCode => "UserCode";
        public virtual string UserName => "UserName";
        public virtual string UserDesc => "UserDescription";

        public IMenu Menu { get; }
    }
}