using Breezee.Core.Tool;
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
    /// <summary>
    /// 控件扩展类
    /// </summary>
    public static class ControlExtension
    {
        public static void SaveDataGridViewColunmStyle(this Control ctr)
        {
            if (ctr is DataGridView)
            {
                DataGridView dgv = (DataGridView)ctr;
                FlexGridColumnDefinition def = FlexGridColumnDefinition.GetDefinitionFromGrid(dgv);

                string strPath = dgv.GetStylePathString();
                def.SaveToFile(strPath);
            }
            if (ctr.HasChildren)
            {
                foreach (Control c in ctr.Controls)
                {
                    c.SaveDataGridViewColunmStyle();
                }
            }
        }

        
    }
}
