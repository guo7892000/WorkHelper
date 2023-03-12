using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

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
namespace Breezee.Core.Adapter.IBLL
{
    /// <summary>
    /// 导入导出类
    /// </summary>
    public interface IADPExcel: IBaseRefOuter
    {
        #region 判断是否excel
        /// <summary>
        /// 判断是否excel
        /// TODO:只使用于WIN7 & XP
        /// </summary>
        /// <returns></returns>
        bool ExistsRegeditOffice();
        #endregion

        #region 尝试设置文件为可写
        /// <summary>
        /// 尝试设置文件为可写
        /// </summary>
        /// <param name="fileName">文件路径（全路径）</param>
        void TrySetFileWritable(string fileName);
        #endregion

        #region 导入Excel到DataSet（无对话框）
        /// <summary>
        /// 导入Excel到DataTable
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <returns></returns>
        DataTable ImportDataTable(string fileName);

        /// <summary>
        /// 导入Excel到DataSet，需要指定sheet数
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <param name="sheetNums">要导入的sheet数</param>
        /// <returns></returns>
        DataSet ImportDataSet(string fileName, int sheetNums);

        DataSet ImportDataSet(string fileName, IDictionary<string, string> dicTableSheet = null, DataSet ds = null);

        /// <summary>
        /// 导入Excel到DataSet，需要指定sheet数
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <param name="sheetNames">要导入的sheet名</param>
        /// <returns></returns>
        DataSet ImportDataSet(string fileName, string[] sheetNames, DataSet ds = null);
        #endregion


    }

    public class ExcelFileInfo
    {
        public string FileName = string.Empty;
        public ExcelSuffixType suffix = ExcelSuffixType.xls;
        public string OleDbConnection = string.Empty;
    }

    public enum ExcelSuffixType
    {
        xls,
        xlsx,
        Unknow
    }
}
