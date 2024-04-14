using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    public enum SqlLogAddType
    {
        /// <summary>
        /// 插入到开始位置
        /// </summary>
        InsertBegin = 1,
        /// <summary>
        /// 追加到末层
        /// </summary>
        AppendEnd = 2
    }
}
