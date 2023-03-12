using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    public class GlobalForm : Form
    {
        public string Id => "GlobalForm";

        public string Code => "GlobalForm";

        public string Description { get => "GlobalForm"; set => throw new NotImplementedException(); }

        private IDictionary<string, object> globDic= new Dictionary<string, object>();
        public IDictionary<string, object> DicObjects { get => globDic; set => throw new NotImplementedException(); }
    }
}
