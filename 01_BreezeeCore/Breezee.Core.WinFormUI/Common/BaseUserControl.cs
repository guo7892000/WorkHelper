using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.Core.WinFormUI.Common
{
    /// <summary>
    /// 用户控件基类
    /// </summary>
    public class BaseUserControl: UserControl
    {
        //显示提示全局信息事件
        public EventHandler<string> ShowGlobalMsg;
    }
}
