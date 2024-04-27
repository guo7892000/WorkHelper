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
 * 创建日期：2022/11/15 23:40:04	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/15 23:40:04 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 类
    /// </summary>
    public abstract class Menu : IMenu
    {
        IDictionary<string, MenuEntity> _Childs = new Dictionary<string, MenuEntity>();
        MenuEntity menuEntity = new MenuEntity();
        public virtual MenuEntity Root => menuEntity;
        public virtual IDictionary<string, MenuEntity> Childs => _Childs;
        public virtual MenuConfigType MenuConfigType => MenuConfigType.Xml;
        public abstract void Init();
    }

    public class MenuConfigString
    {
        public static readonly string Root = "xml";
        public static readonly string Model = "Model";
        public static readonly string Class = "Class";
        public static readonly string Menu = "Menu";
    }

    public class MemuAttrString
    {
        public static readonly string Guid = "Guid";
        public static readonly string Name = "Name";
        public static readonly string Code = "Code";
        public static readonly string ShortCutKey = "ShortCutKey";

        public static readonly string DLLName = "DLLName";
        public static readonly string FormName = "FormName";
        public static readonly string HelpPath = "HelpPath";
        public static readonly string ShowInToolStrip = "ShowInToolStrip"; //是否显示在工具栏
    }
    public class DShortCutMenu
    {
        public string Guid;
        public string Name;
    }
}