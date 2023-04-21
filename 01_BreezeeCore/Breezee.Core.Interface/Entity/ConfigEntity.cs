using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class ConfigEntity: Entity
    {
        /// <summary>
        /// 配置文件目录
        /// </summary>
        public virtual string Dir { get; set; }
        /// <summary>
        /// 配置文件名
        /// </summary>
        public virtual string FileName { get; set; }

        IDictionary<string, object> _dic = new Dictionary<string, object>();
        /// <summary>
        /// 配置项
        /// </summary>
        public virtual IDictionary<string, object> ConfigItems => _dic;

    }
}
