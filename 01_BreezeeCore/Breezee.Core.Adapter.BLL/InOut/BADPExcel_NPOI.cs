using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data.OleDb;
using Breezee.Core.Adapter.IBLL;

/**************************************************************
 * 对象名称：Excel实现类
 * 对象类别：接口实现
 * 创建作者：黄国辉
 * 创建日期：2018-11-20
 * 对象说明：使用NPOI来实现Txt、Excel
 * 修改历史：
 *      V1.0 新建 hgh 2018-11-20
 * ***********************************************************/
namespace Breezee.Core.Adapter.BLL
{
    /// <summary>
    /// 导入导出类
    /// </summary>
    public class BADPExcel_NPOI : IADPExcel
    {
        /// <summary>
        /// Excel格式
        /// </summary>
        public const string Excel_Filter_0307 = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003(*.xls)|*.xls";
    
        #region 判断是否excel
        /// <summary>
        /// 判断是否excel
        /// TODO:只使用于WIN7 & XP
        /// </summary>
        /// <returns></returns>
        public bool ExistsRegeditOffice()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey office03 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Excel\InstallRoot\");
            RegistryKey office07 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Excel\InstallRoot\");
            if (office03 != null || office07 != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 尝试设置文件为可写
        /// <summary>
        /// 尝试设置文件为可写
        /// </summary>
        /// <param name="fileName">文件路径（全路径）</param>
        public void TrySetFileWritable(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    FileInfo f = new FileInfo(fileName);
                    if ((f.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        f.Attributes = FileAttributes.Normal;
                    }
                }
            }
            catch
            {
                //Do nothing if fails
            }
        }
        #endregion       

        #region 导入Excel到DataTable
        /// <summary>
        /// 导入Excel到DataTable
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <returns></returns>
        public DataTable ImportDataTable(string fileName)
        {
            return ImportDataSet(fileName,0).Tables[0];
        }
        
        /// <summary>
        /// 导入Excel到DataSet，需要指定sheet数
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <param name="sheetNums">要导入的sheet数</param>
        /// <returns></returns>
        public DataSet ImportDataSet(string fileName, int sheetNums)
        {
            DataSet ds = new DataSet();

            string tempPath;
            FileStream fs;
            IWorkbook workbook;

            GetWorkBook(fileName, out tempPath, out fs, out workbook);

            try
            {
                for (int i = 0; i < sheetNums; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    DataTable dt = ReadFromSheet(sheet);
                    dt.TableName = sheet.SheetName;
                    ds.Tables.Add(dt);
                }
            }
            catch
            {
            }
            finally
            {
                try
                {
                    fs.Close();
                    File.Delete(tempPath);
                }
                catch { };
            }

            return ds;
        }
        #endregion

        public DataSet ImportDataSet(string fileName, IDictionary<string, string> dicTableSheet = null, DataSet ds = null)
        {
            if (ds == null)
                ds = new DataSet();

            string tempname = "/" + Guid.NewGuid().ToString() + ".xls";
            string tempPath = AppDomain.CurrentDomain.BaseDirectory + tempname;
            File.Copy(fileName, tempPath, true);

            FileStream fs = File.OpenRead(tempPath);
            IWorkbook workbook = null;
            try
            {
                //先尝试创建07以上格式
                workbook = new XSSFWorkbook(fs);
            }
            catch
            {
                //创建03格式
                workbook = new HSSFWorkbook(fs);
            }

            try
            {
                if (dicTableSheet == null)
                {
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        ISheet sheet = workbook.GetSheetAt(i);
                        DataTable dt = ReadFromSheet(sheet);
                        dt.TableName = sheet.SheetName;
                        ds.Tables.Add(dt);
                    }
                }
                else
                {
                    foreach (var pair in dicTableSheet)
                    {
                        ISheet sheet = workbook.GetSheet(pair.Value);
                        DataTable dt = ReadFromSheet(sheet);
                        dt.TableName = pair.Key;
                        ds.Tables.Add(dt);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                try
                {
                    fs.Close();
                    File.Delete(tempPath);
                }
                catch { };
            }

            return ds;
        }

        private static void GetWorkBook(string fileName, out string tempPath, out FileStream fs, out IWorkbook workbook)
        {
            string tempname = "/" + Guid.NewGuid().ToString() + ".xls";
            tempPath = AppDomain.CurrentDomain.BaseDirectory + tempname;
            File.Copy(fileName, tempPath, true);

            fs = File.OpenRead(tempPath);
            workbook = null;
            try
            {
                //先尝试创建07以上格式
                workbook = new XSSFWorkbook(fs);
            }
            catch
            {
                //创建03格式
                workbook = new HSSFWorkbook(fs);
            }
        }

        #region 导入Excel到DataSet

        public DataSet ImportDataSet(string fileName, string[] sheetNames, DataSet ds = null)
        {
            IDictionary<string, string> dicSheetTable = new Dictionary<string, string>();
            foreach (var sheetName in sheetNames)
            {
                dicSheetTable.Add(sheetName, sheetName);
            }
            return ImportDataSet(fileName, dicSheetTable,ds);
        }

        #endregion

        #region 判断文件是否打开
        private static bool IsFileOpened(string fullPath)
        {
            bool inUse = true;
            if (File.Exists(fullPath))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.None);
                    inUse = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        File.Delete(fullPath);
                    }
                }
                return inUse;           //true表示正在使用,false没有使用
            }
            else
            {
                return false;           //文件不存在则一定没有被使用
            }

        }
        #endregion

        #region 读取Sheet内容到DataTable
        private static DataTable ReadFromSheet(ISheet sheet)
        {
            DataTable dt = new DataTable();
            IRow headerRow = sheet.GetRow(sheet.FirstRowNum); //第一行为标题行
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                //DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue); //列为数值1,2,3等，此段代码报错 2014-6-20 廖凌峰
                DataColumn column = new DataColumn(headerRow.GetCell(i).ToString().ToUpper());
                dt.Columns.Add(column);
            }

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.Numeric)
                                dataRow[j - row.FirstCellNum] = row.GetCell(j).NumericCellValue;
                            else if (row.GetCell(j).CellType == CellType.String)
                                dataRow[j - row.FirstCellNum] = row.GetCell(j).StringCellValue;
                        }
                    }
                }

                dt.Rows.Add(dataRow);
            }

            return dt;
        }
        #endregion

        
    }
}
