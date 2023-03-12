using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：配置抽象类		
 * 对象类别：抽象类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5	
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 配置抽象类
    /// </summary>
    public abstract class Config : IConfig
    {
        ConfigEntity _Root = new ConfigEntity();
        IDictionary<string, ConfigEntity> _Childs = new Dictionary<string, ConfigEntity>();
        public virtual ConfigEntity Root { get => _Root; set => _Root = value; }
        public virtual IDictionary<string, ConfigEntity> Childs { get => _Childs; set => _Childs =value; }

        public abstract void Init();
    }
}
