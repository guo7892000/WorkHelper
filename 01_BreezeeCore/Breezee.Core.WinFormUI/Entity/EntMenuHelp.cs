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
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 帮助菜单实体
    /// </summary>
    public class EntMenuHelp
    {
        public string HelpPath;
        public string MenuFullPath;
        public string MenuName;
        public EntMenuHelp(string sHelpPath,string sMenuFullPath, string sMenuName)
        {
            HelpPath = sHelpPath;
            MenuFullPath = sMenuFullPath;
            MenuName = sMenuName;
        }
    }
}
