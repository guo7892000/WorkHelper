using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：数据库实体接口		
 * 对象类别：接口	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/5	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/5新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface IDbEntity : IEntity
    {
        /// <summary>
        /// 表编码
        /// </summary>
        string TableCode { get; }
    }
}