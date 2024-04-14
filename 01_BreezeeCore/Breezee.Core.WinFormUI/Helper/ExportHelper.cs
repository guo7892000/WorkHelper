using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data.OleDb;
using Breezee.Core.Adapter.IBLL;

/**************************************************************
 * 对象名称：导入导出类
 * 对象类别：辅助类
 * 创建作者：黄国辉
 * 创建日期：2015-6-20
 * 对象说明：提供导出Txt、Excel
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ***********************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 导入导出类
    /// </summary>
    public static class ExportHelper
    {
        /// <summary>
        /// Excel格式
        /// </summary>
        public const string Excel_Filter_0307 = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003(*.xls)|*.xls";

        #region 将txt文件内容转化成DataTable
        /// <summary>
        /// 将txt文件内容转化成DataTable
        /// 2014-06-25
        /// 将txt文件流转成datatable
        /// 1:若传入的datatable为空，则创建datatable,创建的datatable列为"col"+数字,类型为string
        /// 2:传入的datatable列必须与txt流的列一 一对应
        /// 3:通过colMappingArr数组可建立起DataTable列和txt流的列对应关系,colMappingArr内容必须为Datatable的列名
        /// </summary>
        /// <param name="inputPath">txt文件的路径</param>
        /// <param name="outDataTable">输出的DataTable,需要给出DataTable列,若列表不存在,则可以根据txt流的列自动生成列</param>
        /// <param name="splitFlag">文件分割符 默认为','</param>
        /// <param name="colMappingArr">列映射 默认为null</param>
        /// <param name="IsNeedHeader">是否需要传入表头 默认为false</param>
        public static void TxtConvertDataTable(this string inputPath, ref DataTable outDataTable, char splitFlag = ',', string[] colMappingArr = null, bool IsNeedHeader = false)
        {
            try
            {
                StreamReader streamReader = new StreamReader(inputPath, Encoding.Default);
                string rowData = string.Empty;
                rowData = streamReader.ReadLine();

                if (rowData == null)
                {
                    throw new Exception("数据不能为空!");
                }
                string[] rows = rowData.Split(splitFlag);
                int recordNum = rows.Length;
                if (recordNum > 0)
                {
                    if (outDataTable == null)
                    {//创建表格,指定表格列数
                        outDataTable = new DataTable();
                        for (int i = 0; i < recordNum; i++)
                        {
                            outDataTable.Columns.Add(rows[i], typeof(string));
                        }
                        outDataTable.AcceptChanges();
                    }
                    else if (outDataTable.Columns.Count < recordNum)
                    {
                        throw new Exception("传入的表格列数不能小于导入的流列数!");
                    }
                }
                int temp = 0;
                while (rowData != null)
                {
                    if (IsNeedHeader == true && temp == 0)
                    {
                        temp++;
                        rowData = streamReader.ReadLine();
                        continue;
                    }
                    rows = rowData.Split(splitFlag);
                    if (!string.IsNullOrEmpty(rows[0]))
                    {
                        DataRow dr = outDataTable.NewRow();
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (colMappingArr != null && colMappingArr.Length > 0)
                            {
                                if (dr[colMappingArr[i]].GetType() == typeof(decimal))
                                {
                                    dr[colMappingArr[i]] = Convert.ToDecimal(rows[i]);
                                }
                                else
                                {
                                    dr[colMappingArr[i]] = rows[i];
                                }
                            }
                            else
                            {
                                if (dr[i].GetType() == typeof(decimal))
                                {
                                    dr[i] = Convert.ToDecimal(rows[i]);
                                }
                                else
                                {
                                    dr[i] = rows[i];
                                }
                            }

                        }
                        outDataTable.Rows.Add(dr);
                    }
                    rowData = streamReader.ReadLine();
                }
                outDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将DataTable转化成Txt文档
        /// <summary>
        /// 将DataTable转化成Txt文档
        /// </summary>
        /// <param name="inputDataTable">需要转化的DataTable</param>
        /// <param name="filePath">文件保存的路径</param>
        /// <param name="splitFlag">文件分割符 默认为','</param>
        /// <param name="dicCol">过滤的DataTable中列</param>
        public static bool DataTableConvertTxt(this DataTable inputDataTable, string filePath, char splitFlag = ',', Dictionary<string, string> dicCol = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("文件路径不能为空!");
            }
            if (inputDataTable == null || inputDataTable.Rows.Count < 1)
            {
                throw new Exception("导入的数据不能为空!");
            }
            StreamWriter sw = new StreamWriter(filePath);
            try
            {
                foreach (DataRow dr in inputDataTable.Rows)
                {
                    StringBuilder builder = new StringBuilder();
                    if (dicCol == null)
                    {
                        for (int i = 0; i < inputDataTable.Columns.Count; i++)
                        {
                            builder.Append(dr[i].ToString() + splitFlag);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string> q in dicCol)
                        {
                            builder.Append(dr[q.Key].ToString() + splitFlag);
                        }
                    }
                    sw.WriteLine(builder.ToString().TrimEnd(splitFlag));
                    sw.Flush();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
            }
        }
        #endregion

        #region Txt及Excel下载
        /// <summary>
        /// 目前只是支持Txt及Excel下载
        /// </summary>
        /// <author>冯兴宝</author>
        /// <param name="fileDirectory">模板所在文件文件夹路径</param>
        /// <param name="fileName">模板文件名</param>
        /// <param name="askOpen">下载成功后是否提示打开文件</param>
        public static void DownloadModel(string fileDirectory, string fileName, bool askOpen = true, SaveFileDialog saveDialog = null)
        {
            DownloadTxtOrExcelFile(fileDirectory, fileName, askOpen, saveDialog, "文件", "下载");
        }
        #endregion

        #region Txt及Excel下载
        /// <summary>
        /// 目前只是支持Txt及Excel下载
        /// </summary>
        /// <author>黄国辉</author>
        /// <param name="fileDirectory">模板所在文件文件夹路径</param>
        /// <param name="fileName">模板文件名</param>
        /// <param name="askOpen">下载成功后是否提示打开文件</param>
        /// <param name="saveDialog">保存的对话框</param>
        /// <param name="strDataName">数据名称</param>
        /// <param name="strDealType">处理类型</param>
        public static void DownloadTxtOrExcelFile(string fileDirectory, string fileName, bool askOpen = true,
            SaveFileDialog saveDialog = null, string strDataName = "文件", string strDealType = "下载")
        {
            if (string.IsNullOrEmpty(fileDirectory) || string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(strDataName + "不存在，请联系系统管理员！");
                return;
            }
            try
            {
                if (saveDialog == null)
                {
                    saveDialog = new SaveFileDialog();
                }
                saveDialog.FileName = fileName;
                if (string.IsNullOrEmpty(saveDialog.Filter))
                {
                    saveDialog.Filter = "Excel 97-2003(*.xls)|*.xls|Excel 工作薄(*.xlsx)|*.xlsx";
                }
                saveDialog.Title = strDataName + strDealType;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    //保存模板
                    System.IO.FileInfo fi = new System.IO.FileInfo(System.IO.Path.Combine(fileDirectory, fileName));
                    if (fi.Exists)
                    {
                        fi.CopyTo(saveDialog.FileName, true);

                        //2014-08-15 增加设置模板文件为可写 by pansq 
                        TrySetFileWritable(saveDialog.FileName);

                        if (askOpen)
                        {
                            if (MessageBox.Show(strDataName + strDealType + "成功，是否立即打开？", "提示信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(saveDialog.FileName);
                                #region 已取消指定程序方式
                                //if (saveDialog.Filter.Substring(saveDialog.Filter.Length - 4) == ".xls" || saveDialog.Filter.Substring(saveDialog.Filter.Length - 5) == ".xlsx")
                                //{
                                //    if (ExistsRegeditOffice())
                                //    {
                                //        System.Diagnostics.Process.Start("Excel.exe", saveDialog.FileName);
                                //    }
                                //    else
                                //    {
                                //        //System.Diagnostics.Process.Start("et.exe", saveDialog.FileName);
                                //        System.Diagnostics.Process.Start(saveDialog.FileName);
                                //    }
                                //}
                                //if (saveDialog.Filter.Substring(saveDialog.Filter.Length - 4) == ".txt")
                                //{
                                //    System.Diagnostics.Process.Start("notepad.exe", saveDialog.FileName);
                                //} 
                                #endregion
                            }
                        }
                        else
                        {
                            MessageBox.Show(strDataName + strDealType + "成功！");
                        }
                    }
                    else
                    {
                        MessageBox.Show(strDataName + "不存在，请联系系统管理员！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件下载失败，原因是：" + ex.Message + "！");
            }
            finally
            {
                saveDialog.Dispose();
            }
        }
        #endregion

        #region 判断是否excel
        /// <summary>
        /// 判断是否excel
        /// TODO:只使用于WIN7 & XP
        /// </summary>
        /// <returns></returns>
        public static bool ExistsRegeditOffice()
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
        public static void TrySetFileWritable(string fileName)
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

        #region 导出到Excel(包括所有列)
        /// <summary>
        /// 导出DataTable数据到Excel(包括所有列)
        /// </summary>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="fileName">文件默认名</param>
        public static bool ExportExcel(DataTable dt, string fileName, bool isSetCellString = false)
        {
            Dictionary<string, string> colMap = new Dictionary<string, string>();
            foreach (DataColumn col in dt.Columns)
            {
                colMap[col.ColumnName] = col.Caption ?? col.ColumnName;
            }

            return ExportExcel(dt, fileName, colMap, isSetCellString);
        }
        #endregion

        #region 导出到Excel(指定列)
        /// <summary>
        /// 导出DataTable数据到Excel(指定列)
        /// </summary>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="fileName">文件默认名</param>
        /// <param name="colMap">列名和标题名的映射:Key-对应数据库字段；value-对应显示名</param>
        public static bool ExportExcel(DataTable dt, string fileName, Dictionary<string, string> colMap, bool isSetCellString = false)
        {
            if (dt == null)
            {
                MessageBox.Show("参数dt不能为Null值");
                return false;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyy-HH-dd") + ".xlsx";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Excel_Filter_0307;
            saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
            saveFileDialog.Title = "导出Excel文件";
            saveFileDialog.FileName = fileName;
            if (saveFileDialog.FileName.Trim().Length == 0)
            {
                MessageBox.Show("请输入要保存的文件名！");
                return false;
            }

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string name = saveFileDialog.FileName.Trim();
            if (IsFileOpened(name))
            {
                MessageBox.Show("文件已经在另一个进程中打开,导出失败，请关闭文件后重新进行导出");
                return false;
            }

            try
            {

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                sheet.DisplayGridlines = false;

                IFont fontHeader = workbook.CreateFont();
                fontHeader.FontHeightInPoints = 10;
                fontHeader.FontName = "微软雅黑";
                fontHeader.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

                ICellStyle headerStyle = workbook.CreateCellStyle();
                headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                headerStyle.VerticalAlignment = VerticalAlignment.Center;
                headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                headerStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                headerStyle.SetFont(fontHeader);

                int startColIndex = 1; //设置第一列的索引
                IRow headerRow = sheet.CreateRow(1);
                int colIndex = startColIndex;
                foreach (KeyValuePair<string, string> pair in colMap)
                {
                    ICell headerCell = headerRow.CreateCell(colIndex);
                    headerCell.SetCellValue(pair.Value);
                    headerCell.CellStyle = headerStyle;
                    if (isSetCellString)
                    {
                        headerCell.SetCellType(CellType.String);
                    }
                    colIndex++;
                }

                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                IFont cellFont = workbook.CreateFont();
                cellFont.FontHeightInPoints = 10;
                cellFont.FontName = "微软雅黑";
                cellStyle.SetFont(cellFont);

                int rowIndex = 2;
                foreach (DataRow row in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    colIndex = startColIndex;
                    foreach (KeyValuePair<string, string> pair in colMap)
                    {
                        ICell dataCell = dataRow.CreateCell(colIndex);
                        double numberVal;
                        if (double.TryParse(row[pair.Key].ToString(), out numberVal))
                        {
                            dataCell.SetCellValue(numberVal);
                        }
                        else
                        {
                            dataCell.SetCellValue(row[pair.Key].ToString());
                        }
                        if (isSetCellString)
                        {
                            dataCell.SetCellType(CellType.String);
                        }
                        dataCell.CellStyle = cellStyle;
                        colIndex++;
                    }

                    rowIndex++;
                }

                for (int i = startColIndex; i < startColIndex + colMap.Count(); i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                FileStream stream = new FileStream(name, FileMode.Create);
                workbook.Write(stream);

                MessageBox.Show("导出成功！保存的文件路径为：" + name,"温馨提示");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败！" + ex.Message, "错误信息");
                return false;
            }
        }
        #endregion

        #region 导入Excel到DataTable
        /// <summary>
        /// 导入Excel到DataTable
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <returns></returns>
        public static DataTable ImportDataTable(string fileName)
        {
            DataTable dt = null;

            string tempname = "\\" + Guid.NewGuid().ToString() + ".xls";
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
                ISheet sheet = workbook.GetSheetAt(0); //取第一个表

                dt = ReadFromSheet(sheet);
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

            return dt;
        }
        #endregion

        #region 导入Excel到DataSet
        /// <summary>
        /// 导入Excel到DataSet，需要指定sheet数
        /// </summary>
        /// <param name="fileName">文件路径名</param>
        /// <param name="sheetNums">要导入的sheet数</param>
        /// <returns></returns>
        public static DataSet ImportDataSet(string fileName, int sheetNums)
        {
            DataSet ds = new DataSet();

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

        #region 获取Excel数据
        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="dicTableNameSql">key为返回DataSet的表名，value为查询Excel的SQL语句</param>
        /// <returns></returns>
        public static DataSet GetExcelData(IDictionary<string, string> dicTableNameSql)
        {
            string sConnString = string.Empty;
            return GetExcelData(dicTableNameSql,out sConnString);
        }
        
        public static DataSet GetExcelData(IDictionary<string, string> dicTableNameSql, out string _DBConnString)
        {
            var dsExcel = new DataSet();
            _DBConnString = string.Empty;

            OpenFileDialog opd = new OpenFileDialog();
            //opd.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";  //支持2003、2007以上格式的Excel
            opd.Filter = "Excel文件(*.xlsx)|*.xlsx"; //只支持2007以上格式的Excel
            opd.FilterIndex = 0;
            opd.Title = "选择对应类型的导入模板Excel文件";
            opd.RestoreDirectory = false;
            if (DialogResult.Cancel == opd.ShowDialog())
            {
                return null;
            }
            string sFilePath = opd.FileName;
            string[] strFileNam = sFilePath.Split('.');


            #region 导入模板后处理
            string strFileFormart = strFileNam[strFileNam.Length - 1].ToString().ToLower();
            if (strFileFormart == "xls")
            {
                _DBConnString = @"Provider=Microsoft.jet.OleDb.4.0;Data Source=" + sFilePath + ";Extended Properties='Excel 8.0;IMEX=1'";
            }
            else if (strFileFormart == "xlsx")
            {
                _DBConnString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + sFilePath + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            }
            else
            {
                throw new Exception("不支持的Excel格式:" + strFileFormart);
            }

            using (OleDbConnection con = new OleDbConnection(_DBConnString))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                try
                {
                    foreach (string sKey in dicTableNameSql.Keys)
                    {
                        OleDbDataAdapter daTable = new OleDbDataAdapter(dicTableNameSql[sKey], con);
                        //打开连接并填充表
                        daTable.Fill(dsExcel, sKey);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK);
                    return null;
                }
            }
            #endregion

            return dsExcel;
        }
        #endregion

        public static ExcelFileInfo OpenSingleExcelDialog()
        {
            ExcelFileInfo excelFileInfo = new ExcelFileInfo();

            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Excel文件(*.xls,*.xlsx,*.xlsm)|*.xls;*.xlsx;*.xlsm";  //支持2003、2007以上格式的Excel
            //opd.Filter = "Excel文件(*.xlsx)|*.xlsx"; //只支持2007以上格式的Excel
            opd.FilterIndex = 0;
            opd.Title = "选择对应类型的导入模板Excel文件";
            opd.RestoreDirectory = false;
            opd.Multiselect = false;
            if (DialogResult.Cancel == opd.ShowDialog())
            {
                return null;
            }
            excelFileInfo.FileName = opd.FileName;
            string[] strFileNam = opd.FileName.Split('.');

            #region 导入模板后处理
            string strFileFormart = strFileNam[strFileNam.Length - 1].ToString().ToLower();
            if (strFileFormart == "xls")
            {
                excelFileInfo.suffix = ExcelSuffixType.xls;
                excelFileInfo.OleDbConnection = @"Provider=Microsoft.jet.OleDb.4.0;Data Source=" + excelFileInfo.FileName + ";Extended Properties='Excel 8.0;IMEX=1'";
            }
            else if (strFileFormart == "xlsx" || strFileFormart == "xlsm")
            {
                excelFileInfo.suffix = ExcelSuffixType.xlsx;
                excelFileInfo.OleDbConnection = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + excelFileInfo.FileName + "; Extended Properties='Excel 12.0 Xml;IMEX=1'";
            }
            else
            {
                throw new Exception("不支持的Excel格式:" + strFileFormart);
            }

            return excelFileInfo;
        }

        #region 获取Excel数据
        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="dicTableNameSql">key为返回DataSet的表名，value为查询Excel的SQL语句</param>
        /// <returns></returns>
        public static DataSet GetExcelDataBySql(IDictionary<string, string> dicTableNameSql)
        {
            string sConnString = string.Empty;
            return GetExcelDataBySql(dicTableNameSql, out sConnString);
        }

        public static DataSet GetExcelDataBySql(IDictionary<string, string> dicTableNameSql, out string _DBConnString)
        {
            var dsExcel = new DataSet();
            ExcelFileInfo excelFileInfo = OpenSingleExcelDialog();
            _DBConnString = excelFileInfo.OleDbConnection;

            using (OleDbConnection con = new OleDbConnection(_DBConnString))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                try
                {
                    foreach (string sKey in dicTableNameSql.Keys)
                    {
                        OleDbDataAdapter daTable = new OleDbDataAdapter(dicTableNameSql[sKey], con);
                        //打开连接并填充表
                        daTable.Fill(dsExcel, sKey);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK);
                    return null;
                }
            }
            return dsExcel;
        }
        #endregion

        #endregion

        #region 导入Excel到DataSet

        public static DataSet ImportDataSet(string fileName, IDictionary<string, string> dicTableSheet = null, DataSet ds = null)
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
        public static DataSet ImportDataSet(string fileName, string[] sheetNames, DataSet ds = null)
        {
            IDictionary<string, string> dicSheetTable = new Dictionary<string, string>();
            foreach (var sheetName in sheetNames)
            {
                dicSheetTable.Add(sheetName, sheetName);
            }
            return ImportDataSet(fileName, dicSheetTable, ds);
        }

        public static DataSet GetExcelDataSet(string[] sheetNames, DataSet ds = null)
        {
            ExcelFileInfo excelFileInfo = OpenSingleExcelDialog();
            if (excelFileInfo == null) return null;
            IDictionary<string, string> dicSheetTable = new Dictionary<string, string>();
            foreach (var sheetName in sheetNames)
            {
                dicSheetTable.Add(sheetName, sheetName);
            }
            return ImportDataSet(excelFileInfo.FileName, dicSheetTable, ds);
        }

        public static DataSet GetExcelDataSet(IDictionary<string, string> dicSheetTable = null, DataSet ds = null)
        {
            ExcelFileInfo excelFileInfo = OpenSingleExcelDialog();
            if (excelFileInfo == null) return null;
            return ImportDataSet(excelFileInfo.FileName, dicSheetTable, ds);
        }

        #endregion
    }
}
