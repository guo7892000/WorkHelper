using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：配置接口
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5	
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 配置接口
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 主配置
        /// </summary>
        ConfigEntity Root { get; set; }
        /// <summary>
        /// 子配置集合
        /// </summary>
        IDictionary<string, ConfigEntity> Childs { get; set; }
        /// <summary>
        /// 配置初始化
        /// </summary>
        void Init();
    }
}
