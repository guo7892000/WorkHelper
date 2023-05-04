using Breezee.Core.Interface;
using Breezee.Core.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// 全局值类
    /// </summary>
    public class WinFormGlobalValue : GlobalContext
    {
        public static IDictionary<string, BindingSource> dicBindingSource = new Dictionary<string, BindingSource>();
        //应用程序路径相关
        public static string StartupPath = Application.StartupPath;

        #region 设置绑定源集合
        /// <summary>
        /// 设置绑定源集合
        /// </summary>
        /// <param name="dicBindingSource"></param>
        /// <param name="dataTableArr"></param>
        private static void CreateBingSource(IDictionary<string, BindingSource> dicBindingSource, DataTable[] dataTableArr)
        {
            for (int i = 0; i < dataTableArr.Length; i++)
            {
                if (!dicBindingSource.ContainsKey(dataTableArr[i].TableName))
                {
                    BindingSource bindingSource = new BindingSource();
                    dicBindingSource[dataTableArr[i].TableName] = bindingSource;
                }
            }
        }
        #endregion

        #region 设置全局数据源
        public static void SetPublicDataSource(DataTable[] dataTableArr)
        {
            CreateBingSource(dicBindingSource, dataTableArr);
            for (int i = 0; i < dataTableArr.Length; i++)
            {
                string sTableName = dataTableArr[i].TableName;
                if (dicBindingSource.ContainsKey(sTableName))
                {
                    dicBindingSource[sTableName].DataSource = dataTableArr[i];
                }
            }
        }
        #endregion
    }
}
