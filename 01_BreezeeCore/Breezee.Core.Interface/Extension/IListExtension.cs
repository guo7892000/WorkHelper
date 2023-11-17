using System;
using System.Collections.Generic;
using System.Data;
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
namespace Breezee.Core.Interface
{
    /// <summary>
    /// List<string>扩展
    /// </summary>
    public static class IListExtension
    {
        #region 转换为In字符
        /// <summary>
        /// 转换为In字符
        /// 例如IN ('11','22')
        /// </summary>
        /// <param name="RelList">列名与控件关系类集合</param>
        /// <returns></returns>
        public static string ToInString(this List<string> RelList)
        {
            StringBuilder sb = new StringBuilder();
            string strReturn = "";
            if (RelList != null && RelList.Count > 0)
            {
                foreach (string str in RelList)
                {
                    sb.Append("'" + str + "',");
                }
                strReturn = " IN (" + sb.ToString().Substring(0, sb.Length - 1) + ")";
            }
            return strReturn;
        }
        #endregion

        #region 根据列表来构造自定义表方法
        /// <summary>
        /// 根据列表来构造自定义表方法
        /// </summary>
        /// <param name="list">字符列表</param>
        /// <returns></returns>
        public static DataTable GetTable(this IList<string> list)
        {
            DataTable dtSource = new DataTable();
            foreach (string strKey in list)
            {
                dtSource.Columns.Add(strKey);
            }
            return dtSource;
        }
        #endregion

    }
}
