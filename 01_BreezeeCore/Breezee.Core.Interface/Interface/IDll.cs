using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************		
 * 对象名称：程序集接口		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/15 23:30:48		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/15 23:30:48 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 程序集接口
    /// </summary>
    public interface IDll
    {
        /// <summary>
        /// 程序集集合
        /// </summary>
        IDictionary<string, DllEntity> DicDll { get; }
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
    }
}