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
    /// 显示全局信息参数参数
    /// </summary>
    public class ShowGlobalMsgEventArgs : EventArgs
    {
        private string _Msg;

        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }
        public ShowGlobalMsgEventArgs(string strMsg)
        {
            _Msg = strMsg;
        }
    }
}
