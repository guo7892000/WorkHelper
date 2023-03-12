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
 * 创建日期：2022/11/19 17:57:56	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/19 17:57:56 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 类
    /// </summary>
    public class TableMenu : Menu
    {
        private MenuEntity _Root = new MenuEntity();
        private IDictionary<string, MenuEntity> _Childs = new Dictionary<string, MenuEntity>();
        public override MenuEntity Root { get => _Root; }
        public override IDictionary<string, MenuEntity> Childs { get => _Childs;  }

        public override MenuConfigType MenuConfigType { get => MenuConfigType.Table; }
        public override void Init()
        {
            _Root = new MenuEntity();
            _Childs = new Dictionary<string, MenuEntity>();
        }
    }
}