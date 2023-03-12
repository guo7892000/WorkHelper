using Breezee.Core.Interface;

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
    public abstract class FormApp : App
    {
        public abstract IForm LoginForm { get; set; }
        public abstract IForm MainForm { get; set; }

    }
}