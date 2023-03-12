using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*********************************************************************	
 * 对象名称：菜单实体	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/15 22:44:36	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/15 22:44:36 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 菜单实体
    /// </summary>
    public class MenuEntity: Entity
    {
        IDictionary<string, MenuEntity> _Childs = new Dictionary<string, MenuEntity>();

        public string Guid;
        public MenuType MenuType;
        public string Name;
        public string Code;
        public string ShortCutKey;
        public string DLLName;
        public string FormName;
        public string FullPath;//菜单全路径
        public string HelpPath;//HTML格式帮助文件路径

        public string ParentGuid;//上级菜单Guid

        public IDictionary<string, MenuEntity> Childs => _Childs;


    }
}