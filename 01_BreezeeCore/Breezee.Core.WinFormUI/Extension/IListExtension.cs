using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Entity;

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
    /// List<string>扩展
    /// </summary>
    public static class IListExtension
    {
        #region List<DBColumnControlRelation> 扩展

        #region 获取保存列清单
        /// <summary>
        /// 获取保存列清单
        /// </summary>
        /// <param name="RelList">列名与控件关系类集合</param>
        /// <returns></returns>
        public static List<string> GetSaveColumnNameList(this List<DBColumnControlRelation> RelList)
        {
            List<string> columnsList = new List<string>();
            foreach (DBColumnControlRelation col in RelList)
            {
                if (col.ReadSaveEnum != DBColumnControlReadSaveEnum.ReadOnly && !string.IsNullOrEmpty(col.SaveColumnName))
                {
                    columnsList.Add(col.SaveColumnName);
                }
            }
            return columnsList;
        }
        #endregion

        #region 获取保存列清单
        /// <summary>
        /// 获取保存列清单
        /// </summary>
        /// <param name="RelList">列名与控件关系类集合</param>
        /// <returns></returns>
        public static IDictionary<string, Control> GetColumnControlDictionary(this List<DBColumnControlRelation> RelList)
        {
            IDictionary<string, Control> columnsList = new Dictionary<string, Control>();
            foreach (DBColumnControlRelation col in RelList)
            {
                if (col.ReadSaveEnum != DBColumnControlReadSaveEnum.ReadOnly && !string.IsNullOrEmpty(col.SaveColumnName))
                {
                    columnsList[col.SaveColumnName] = col.ControlName;
                }
            }
            return columnsList;
        }
        #endregion
        #endregion
    }
}
