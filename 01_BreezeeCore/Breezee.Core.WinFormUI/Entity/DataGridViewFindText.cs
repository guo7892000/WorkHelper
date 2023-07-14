using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 网格查找文本对象
    /// </summary>
    public class DataGridViewFindText
    {
        public int RowIndex {get;set;}
        public int ColumnIndex { get; set; }
        public bool IsFindEnd { get; set; }

        public int CurrentIndex { get; set; }
        public string CurrentMsg { get; set; }
        public DataGridViewFindText()
        {
            RowIndex = -1;
            ColumnIndex = -1;
            CurrentIndex= 0;
            IsFindEnd = false;
        }
    }
}
