using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Ude;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并脚本
    /// </summary>
    public partial class FrmReadSameFormatExcel : BaseForm
    {
        private string sReadDir;
        public FrmReadSameFormatExcel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDBTScriptMerge_Load(object sender, EventArgs e)
        {
            lblMergeInfo.Text= "针对同一目录下、具有相同格式、非结构化的一个或多个Excel文件，按结构化方式取出里边的数据！";
            //Excel文件类型
            _dicString.Clear();
            _dicString.Add("xls", ".xls");
            _dicString.Add("xlsx", ".xlsx");
            cbbExcelType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //加载用户偏好值
            txbSelectPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ReadSameFormatExcel_Path,"").Value;
        }
        
        /// <summary>
        /// 选择文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ReadSameFormatExcel_Path, "").Value;
            //这里不使用自带的FolderBrowserDialog，那样选择目录很不方便。这个第3方库Ookii Dialogs，显示的界面更好用！！
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.Description = "请选择一个目录";
            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                folderBrowserDialog.SelectedPath = strLastSelectedPath;
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txbSelectPath.Text = folderBrowserDialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ReadSameFormatExcel_Path, folderBrowserDialog.SelectedPath, "【相同格式Excel内容读取】最后选择的目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }

        /// <summary>
        /// 合并脚本按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                sReadDir = txbSelectPath.Text.Trim();
                if (string.IsNullOrEmpty(sReadDir))
                {
                    ShowErr("请先选择文件所有目录！");
                    return;
                }

                if (!Directory.Exists(sReadDir))
                {
                    ShowErr("输入的目录不存在，请重新输入或选择！");
                    return;
                }

                rtbString.Clear();
                string sExcelExtend = cbbExcelType.SelectedValue.ToString();
                IEnumerable<string> sqlFiles = Directory.GetFiles(sReadDir, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(sExcelExtend));
                  
                if (sqlFiles.Count() == 0)
                {
                    //没有配置相对目录和绝对目录时,读取源目录下的所有文件
                    ShowErr("所选目录没有Excel文件！");
                    return;
                }

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ReadSameFormatExcel_Path, txbSelectPath.Text, "【相同格式Excel内容读取】最后选择的目录");
                WinFormContext.UserLoveSettings.Save();

                DataSet _dsExcelData = new DataSet();
                DataTable dt = new DataTable();
                int iFile = 1;//文件数
                int iColumn = 0;//列数
                int iColumnMax = 0;//最大列数
                int iExcelMaxColumn = 16384; //Excel限制的最大列数
                

                foreach (string sPath in sqlFiles)
                {
                    if ("xlsx".Equals(sExcelExtend))
                    {
                        iColumn = GetXlsxContent(dt, iFile, iColumn, iColumnMax, iExcelMaxColumn, sPath);
                    }
                    else
                    {
                        iColumn = GetXlsContent(dt, iFile, iColumn, iColumnMax, iExcelMaxColumn, sPath);
                    }

                    //重新计算最大列数
                    iColumnMax = iColumnMax > iColumn ? iColumnMax : iColumn;
                    rtbString.AppendText(sPath + Environment.NewLine);
                    iFile++;//文件数累加
                    iColumn = 0;//列数要归0
                }
                //绑定最终网格
                if (ckbRemoveEmptyColumn.Checked)
                {
                    RemoveAllEmptyColumns(dt);
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("ROWNUM",typeof(int));
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt2.Columns.Add(i.ToExcelColumnWord());
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt2.Rows.Add((new object[] { i + 1 }).Concat(dt.Rows[i].ItemArray).ToArray()); //最前面为序号，再连接后面的数据
                    }
                    
                    dt = dt2;
                }
                dgvResult.BindAutoColumn(dt);
                tabControl1.SelectedTab = tpResult;
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        /// <summary>
        /// 读取Xlsx文件内容（使用ClosedXML第三方库）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="iFile"></param>
        /// <param name="iColumn"></param>
        /// <param name="iColumnMax"></param>
        /// <param name="iExcelMaxColumn"></param>
        /// <param name="sPath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private int GetXlsxContent(DataTable dt, int iFile, int iColumn, int iColumnMax, int iExcelMaxColumn, string sPath)
        {
            using (var workbook = new XLWorkbook(sPath))
            {
                int iSheetIdx = int.Parse(nudSheetNum.Value.ToString());
                if (workbook.Worksheets.Count < iSheetIdx)
                {
                    throw new Exception("Excel中的页签数最大数" + workbook.Worksheets.Count + "，我们无法读取第" + iSheetIdx + "个页签的数据！");
                }
                var worksheet = workbook.Worksheet(iSheetIdx);
                dt.Rows.Add(dt.NewRow());
                // 读取数据
                for (int row = 1; row <= worksheet.LastRowUsed().RowNumber(); row++)
                {
                    for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                    {
                        IXLCell cell = worksheet.Cell(row, col);
                        object cellValue = string.Empty;
                        if (cell.Value.IsText)
                        {
                            cellValue = cell.Value.GetText();
                        }
                        else if (cell.Value.IsNumber)
                        {
                            cellValue = cell.Value.GetNumber();
                        }
                        else if (cell.Value.IsDateTime)
                        {
                            cellValue = cell.Value.GetDateTime();
                        }
                        else if (cell.Value.IsBoolean)
                        {
                            cellValue = cell.Value.GetBoolean();
                        }
                        else
                        {
                            string sValue;
                            cell.Value.TryGetText(out sValue);
                            cellValue = sValue;
                        }

                        // 在这里，您可以根据需要对cellValue进行进一步的处理，比如转换类型、验证数据等。
                        // 示例：将单元格值转换为字符串并打印
                        string cellText = cellValue?.ToString() ?? "";
                        if (iFile == 1)
                        {
                            dt.Columns.Add(iColumn.ToExcelColumnWord());
                        }
                        else
                        {
                            if (iColumn >= iColumnMax)
                            {
                                dt.Columns.Add(iColumn.ToExcelColumnWord());
                            }
                        }
                        //赋值
                        dt.Rows[dt.Rows.Count - 1][iColumn.ToExcelColumnWord()] = cellText;
                        iColumn++;
                        if (iColumn >= iExcelMaxColumn)
                        {
                            throw new Exception("已超过了Excel最大的列数" + iExcelMaxColumn + "，已停止。请检查是否是读取非结构化的Excel数据！");
                        }
                    }
                }
            }

            return iColumn;
        }

        /// <summary>
        /// 读取Xls文件内容（使用NPOI第三方库）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="iFile"></param>
        /// <param name="iColumn"></param>
        /// <param name="iColumnMax"></param>
        /// <param name="iExcelMaxColumn"></param>
        /// <param name="sPath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private int GetXlsContent(DataTable dt, int iFile, int iColumn, int iColumnMax, int iExcelMaxColumn, string sPath)
        {
            //读取Excel内容
            NPOI.SS.UserModel.IWorkbook workbookNpoi;
            using (FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read))
            {
                // 根据文件扩展名选择工作簿类
                if (Path.GetExtension(sPath) == ".xls")
                    workbookNpoi = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                else
                    workbookNpoi = new HSSFWorkbook(fs);
            }
            int iSheetIdx = int.Parse(nudSheetNum.Value.ToString());
            if (workbookNpoi.NumberOfSheets < iSheetIdx)
            {
                throw new Exception("Excel中的页签数最大数" + workbookNpoi.NumberOfSheets + "，我们无法读取第" + iSheetIdx + "个页签的数据！");
            }
            NPOI.SS.UserModel.ISheet sheet = workbookNpoi.GetSheetAt(iSheetIdx-1); // 获取第一个工作表，注这里要减1，因为其第一个页签为0
            dt.Rows.Add(dt.NewRow());
            for (int rowNum = sheet.FirstRowNum; rowNum <= sheet.LastRowNum; rowNum++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(rowNum);
                if (row == null) continue; // 跳过空行
                for (int colNum = row.FirstCellNum; colNum < row.LastCellNum; colNum++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(colNum);
                    //Console.WriteLine($"单元格({rowNum},{colNum})值: {cell?.ToString() ?? "空"}");
                    // 在这里，您可以根据需要对cellValue进行进一步的处理，比如转换类型、验证数据等。
                    // 示例：将单元格值转换为字符串并打印
                    string cellText = cell?.ToString() ?? "";
                    if (iFile == 1)
                    {
                        dt.Columns.Add(iColumn.ToExcelColumnWord());
                    }
                    else
                    {
                        if (iColumn >= iColumnMax)
                        {
                            dt.Columns.Add(iColumn.ToExcelColumnWord());
                        }
                    }
                    //赋值
                    dt.Rows[dt.Rows.Count - 1][iColumn.ToExcelColumnWord()] = cellText;
                    iColumn++;
                    if (iColumn >= iExcelMaxColumn)
                    {
                        throw new Exception("已超过了Excel最大的列数" + iExcelMaxColumn + "，已停止。请检查是否是读取非结构化的Excel数据！");
                    }
                }
            }

            return iColumn;
        }

        private void RemoveAllEmptyColumns(DataTable dt)
        {
            // 创建一个数组来存储要删除的列的索引
            int[] columnsToRemove = new int[dt.Columns.Count];
            int count = 0;

            // 遍历所有列，检查是否所有行都为空
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                bool allEmpty = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[i] != DBNull.Value && !string.IsNullOrEmpty(row[i].ToString()))
                    {
                        allEmpty = false;
                        break;
                    }
                }
                if (allEmpty)
                {
                    columnsToRemove[count] = i; // 标记要删除的列索引
                    count++;
                }
            }

            // 删除标记的列（从后往前删除以避免索引变化问题）
            for (int i = count - 1; i >= 0; i--)
            {
                dt.Columns.RemoveAt(columnsToRemove[i]);
            }
        }
        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExport_Click(object sender, EventArgs e)
        {
            DataTable dtCount = dgvResult.GetBindingTable();
            ExportHelper.ExportExcel(dtCount, "Excel数据_" + DateTime.Now.ToyyyyMMddHHmmss());
        }
    }
}
