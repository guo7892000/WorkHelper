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
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string Guid;

        /// <summary>
        /// 打开同一个菜单的新窗体的GUID：一般一个菜单只能打开一次，但有时为了方便功能使用，支持一个菜单打开多个实例，那么该字段就唯一标识该打开的窗体
        /// </summary>
        public string SameMenuNewFormGuid = string.Empty;
        /// <summary>
        /// 是否打开同一个菜单的新窗体
        /// </summary>
        public bool IsOpenSameMenuNewForm = false;

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

        public MenuEntity Clone()
        {
            var clone = new MenuEntity();
            clone.Guid= Guid;
            clone.SameMenuNewFormGuid = SameMenuNewFormGuid;
            clone.IsOpenSameMenuNewForm = IsOpenSameMenuNewForm;
            clone.MenuType = MenuType;
            clone.Name = Name;
            clone.Code = Code;
            clone.ShortCutKey = ShortCutKey;
            clone.DLLName = DLLName;
            clone.FormName = FormName;
            clone.FullPath = FullPath;
            clone.HelpPath = HelpPath;
            clone.ParentGuid = ParentGuid;
            return clone;
        }
    }
}