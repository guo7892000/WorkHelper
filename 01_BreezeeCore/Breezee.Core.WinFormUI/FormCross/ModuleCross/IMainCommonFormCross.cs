using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 交叉引用通用接口_系统管理
    /// 注：如果本接口方法已满足需求，但目标窗体直接继承该接口即可。否则需要新增接口
    /// </summary>
    [DllName(DllNameConstants.DLL_MAIN_MANAGE)]
    public interface IMainCommonFormCross : IFormCross
    {

    }
}
