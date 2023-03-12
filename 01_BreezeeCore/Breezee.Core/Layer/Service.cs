using Breezee.Core.Interface;

/*********************************************************************		
 * 对象名称：业务逻辑层抽象类		
 * 对象类别：抽象类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 服务抽象类
    /// </summary>
    public abstract class Service : IService
    {
        public abstract IModule Module { get; set; }
    }
}