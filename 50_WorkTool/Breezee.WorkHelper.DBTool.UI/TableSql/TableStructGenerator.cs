using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.IO;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using org.breezee.MyPeachNet;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 对象名称：生成表结构的html文件
    /// 创建作者：潘水庆
    /// 创建日期：2012-11-06
    /// 说明：生成表结构的html文件，方便复制到Excel
    /// </summary>
    internal class TableStructGenerator
    {
        #region 变量
        public static readonly string TABKEY_TABLE_STRUCT = "Tab_Key_Table_Struct";

        private static string htmlString = string.Empty;

        private static string HtmlString
        {
            get { return htmlString; }
            set { htmlString = value; }
        } 
        #endregion

        /// <summary>
        /// 移除表结构Tab页
        /// </summary>
        /// <param name="tabControl"></param>
        public static void RemoveTab(TabControl tabControl)
        {
            if (tabControl.TabPages.ContainsKey(TABKEY_TABLE_STRUCT))
            {
                tabControl.TabPages.RemoveByKey(TABKEY_TABLE_STRUCT);
            }
        }

        /// <summary>
        /// 生成表结构方法
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="tableList"></param>
        /// <param name="columnList">列清单</param>
        /// <param name="useDataTypeFull">全类型</param>
        /// <param name="useLYTemplate">是否使用LY模板</param>
        /// <param name="useNameRemark">是否使用包括列名的备注</param>
        public static void Generate(TabControl tabControl, DataTable tableList, DataTable columnList, TableStructGeneratorParamEntity docEntity)
        {
            HtmlString = GenerateHtmlString(tableList, columnList, docEntity);

            if (!tabControl.TabPages.ContainsKey(TABKEY_TABLE_STRUCT))
            {
                tabControl.TabPages.Add(TABKEY_TABLE_STRUCT, "表结构变更");                
            }

            TabPage tabStruct = tabControl.TabPages[TABKEY_TABLE_STRUCT];
            tabStruct.Controls.Clear();

            WebBrowser browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);

            tabStruct.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            browser.Navigate("about:blank");           
        }

        /// <summary>
        /// 浏览器文档完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;            
            browser.Document.Write(HtmlString);
        }

        /// <summary>
        /// 生成Html字符
        /// </summary>
        /// <param name="tableList">表清单</param>
        /// <param name="columnList">列清单</param>
        /// <returns></returns>
        private static string GenerateHtmlString(DataTable tableList, DataTable columnList, TableStructGeneratorParamEntity docEntity)
        {
            string htmlTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Html);
            string tableTemplate;
            string columnsTemplate;
            if (docEntity.useOldColumnCode)
            {
                tableTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Table_LY);
                columnsTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Column_Move);
            }
            else
            {
                if (docEntity.useLYTemplate)
                {
                    tableTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Table_LY);
                    columnsTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Column_LY);
                }
                else
                {
                    tableTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Table);
                    columnsTemplate = LoadTemplate(DBTGlobalValue.TableSQL.Html_Column);
                }
            }            

            StringBuilder tableBuilder = new StringBuilder();
            foreach(DataRow rowTable in tableList.Rows)
            {
                string tableCode = rowTable[EntTable.ExcelTable.Code].ToString();
                string commonColumn = rowTable[EntTable.ExcelTable.CommonColumnTableCode].ToString().Trim();
                DataRow[] rowColumns;

                if (string.IsNullOrEmpty(commonColumn))
                {
                    rowColumns = columnList.Select(EntTable.ExcelTable.Code + "='" + tableCode + "'");
                }
                else
                {
                    rowColumns = columnList.Select(EntTable.ExcelTable.Code + "='" + tableCode + "'");
                    DataRow[] rowCommonColumns = columnList.Select(EntTable.ExcelTable.Code + "='" + commonColumn + "'");
                    DataRow[] rowArr = new DataRow[rowColumns.Length + rowCommonColumns.Length];
                    rowColumns.CopyTo(rowArr,0);
                    rowCommonColumns.CopyTo(rowArr, rowColumns.Length);
                    rowColumns = rowArr;
                }

                int index = 1;
                StringBuilder columnBuilder = new StringBuilder();
                foreach (DataRow row in rowColumns)
                {
                    string strColumnChangeType = row[EntTable.ExcelTable.ChangeType].ToString().Trim() == "" ? "新增" : row[EntTable.ExcelTable.ChangeType].ToString().Trim();
                    if (strColumnChangeType == "先删后增")
                    {
                        strColumnChangeType = "修改";
                    }

                    //是否使用全类型替换
                    string sDataType = docEntity.useDataTypeFull ? row[ColCommon.ExcelCol.DataTypeFullNew].ToString() : row[ColCommon.ExcelCol.DataTypeNew].ToString();
                    string sColName = row[ColCommon.ExcelCol.Name].ToString();
                    string sSourceRemark = row[ColCommon.ExcelCol.Remark].ToString();
                    string sColRemark;
                    string sColNameAndRemark = sColName + "：" + sSourceRemark;
                    if (docEntity.useNameSameWithRemark)
                    {
                        //如列名称同备注，那么列名和备注都将使用【列名称：列说明】
                        sColName = sColNameAndRemark;
                        sColRemark = sColNameAndRemark;
                    }
                    else
                    {
                        if (docEntity.useRemarkContainsName)
                        {
                            sColRemark = string.IsNullOrEmpty(sSourceRemark) ? sColName : sColNameAndRemark;
                        }
                        else
                        {
                            sColRemark = sSourceRemark;
                        }
                    }

                    //列Web字符
                    string columnString = columnsTemplate.Replace("${ColumnName}", sColName)
                        .Replace("${ColumnCode}", row[ColCommon.ExcelCol.Code].ToString())
                        .Replace("${ColumnType}", sDataType)
                        .Replace("${ColumnWidth}", row[ColCommon.ExcelCol.DataLength].ToString())
                        .Replace("${DecimalPlace}", row[ColCommon.ExcelCol.DataDotLength].ToString())
                        .Replace("${PrimaryKey}", row[ColCommon.ExcelCol.KeyType].ToString())
                        .Replace("${DefaultValue}", row[ColCommon.ExcelCol.Default].ToString())
                        .Replace("${Rule}", row[ColCommon.ExcelCol.NotNull].ToString())
                        .Replace("${Remark}", sColRemark)
                        .Replace("$(No)", index.ToString())
                        .Replace("${ChangeType}", strColumnChangeType);
                    columnBuilder.Append(columnString);
                    index++;
                }

                //表相关
                string sTableChangeType = rowTable[EntTable.ExcelTable.ChangeType].ToString();
                if (docEntity.useLYTemplate)
                {
                    sTableChangeType = sTableChangeType.Replace("新增","创建") +"表";
                }
                string sTableName = rowTable[EntTable.ExcelTable.Name].ToString();
                if(string.IsNullOrEmpty(sTableName))
                {
                    sTableName = docEntity.defaultColumnOrTableName + "表";
                }
                string sTableSoureRemark = rowTable[EntTable.ExcelTable.Remark].ToString();
                string sTableRemark;
                if (docEntity.useRemarkContainsName)
                {
                    sTableRemark = string.IsNullOrEmpty(sTableSoureRemark) ? sTableName : sTableName + "：" + sTableSoureRemark;
                }
                else
                {
                    sTableRemark = sTableSoureRemark;
                }

                string tableString = tableTemplate.Replace("$$(ColumnsHolder)", columnBuilder.ToString())
                    .Replace("${tableName}", sTableName)
                    .Replace("${tableCode}", rowTable[EntTable.ExcelTable.Code].ToString())
                    .Replace("${changeType}", sTableChangeType)
                    .Replace("${tableRemark}", sTableRemark);
                tableBuilder.Append(tableString);
            }

            string html = htmlTemplate.Replace("$$(TableHolder)", tableBuilder.ToString());
            return html;
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        private static string LoadTemplate(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
