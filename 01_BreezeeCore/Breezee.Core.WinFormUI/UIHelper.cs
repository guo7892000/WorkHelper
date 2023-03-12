using System.Windows.Forms;

namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 类名称：UI公共库类
    /// 作者：黄国辉
    /// 日期：2013-11-10
    /// 说明：提供操作UI的辅助方法
    /// </summary>
    public class UIHelper
    {
        #region 重置控件值
        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="controls">可以是一个或者多个，还可以是容器控件</param>
        public static void ResetControl(params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.ResetControl();
            }
        }

        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="controls">可以是一个或者多个，还可以是容器控件</param>
        public void SetControlsReadOnly(bool IsOnlyRead = true, params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.SetControlReadOnly(IsOnlyRead);
            }
        }
        #endregion
    }
}
