using Breezee.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/14 23:38:14	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/14 23:38:14 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// 类
    /// </summary>
    public class MiniConfig: Config
    {
        IDictionary<string, ConfigEntity> _Props = new Dictionary<string, ConfigEntity>();
        ConfigEntity _configEntity;
        /// <summary>
        /// 配置的属性集合
        /// </summary>
        public override IDictionary<string, ConfigEntity> Childs { get => _Props; set => _Props = value; }
        public override ConfigEntity Root { get => _configEntity; set => _configEntity=value; }

        public override void Init()
        {
            _Props = new Dictionary<string, ConfigEntity>();
            _configEntity = new ConfigEntity();
        }
    }
}