using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 表列说明变更
    /// </summary>
    public partial class FrmDBTTableExtendProperty : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        //常量
        //文件路径
        string _DBConnString; //连接字符串
        //导入的SQL变量值
        private string _strMainSql = "";//主SQL
        private string _strErr = "";//错误提示

        //数据集
        private DataSet dsExcel;
        public IDictionary<string, BindingSource> dicBindingSource = new Dictionary<string, BindingSource>();

        //创建类型下拉框
        //private readonly string _strCreate_AddOnly = "0";//只是新增
        //private readonly string _strCreate_DeleteAndAdd = "1";//先删后增
        //private readonly string _strCreate_Delete = "2";//只是删除
        #endregion

        public FrmDBTTableExtendProperty()
        {
            InitializeComponent();
        }

        private void FrmTableExtendProperty_Load(object sender, EventArgs e)
        {
            //数据库类型
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbDbType.BindTypeValueDropDownList(dtDbType, false, true);
            IDictionary<string, string> dic_List = new Dictionary<string, string>
            {
                { "1", "表列索引" },
                { "2", "表列注释" }
            };
            cbbGenerateType.BindTypeValueDropDownList(dic_List.GetTextValueTable(false), false, true);
            //
        }

        private void cbbGenerateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbGenerateType.SelectedValue == null) return;
            if ("2".Equals(cbbGenerateType.SelectedValue.ToString()))
            {
                lblTableData.Text = "SQLite不支持注释。MySql只支持增加表注释，因为导入的信息不足以生成列注释！";
            }
            else
            {
                lblTableData.Text = "";
            }
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            try
            {
                dsExcel = new DataSet();
                dsExcel = ExportHelper.GetExcelDataSet();//得到Excel数据
                if (dsExcel == null)
                {
                    return;
                }

                dgvTableList.BindAutoColumn(dsExcel.Tables[0]);
                //导入成功后处理
                tsbAutoSQL.Enabled = true;
                tabControl1.SelectedTab = tpImport;

                //导入成功提示
                ShowInfo(StaticValue.ImportSuccessMsg);
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            if (cbbGenerateType.SelectedValue == null) return;
            int iDbType = int.Parse(cbbDbType.SelectedValue.ToString());
            DataBaseType selectDBType = (DataBaseType)iDbType;
            if ("2".Equals(cbbGenerateType.SelectedValue.ToString()) && selectDBType == DataBaseType.SQLite)
            {
                ShowInfo("SQLite不支持注释！");
                return;
            }

            //取得数据源
            DataTable dtMain = dgvTableList.GetBindingTable();

            #region 移除空行
            string strColNameList = "";
            for (int k = 0; k < dtMain.Columns.Count; k++)
            {
                string strOneDataCol = "([" + dtMain.Columns[k].ColumnName + "] is null or [" + dtMain.Columns[k].ColumnName + "]='') ";
                if (k != 0)
                {
                    strColNameList = strColNameList + "and " + strOneDataCol;
                }
                else
                {
                    strColNameList = strColNameList + strOneDataCol;
                }

            }
            foreach (DataRow dr in dtMain.Select(strColNameList))
            {
                dtMain.Rows.Remove(dr);
            }

            //得到变更后数据
            dtMain.AcceptChanges();
            if (dtMain.Rows.Count == 0)
            {
                ShowInfo("没有可生成的数据！");
                return;
            }
            #endregion

            StringBuilder sbTableColumnList = new StringBuilder();
            string sbAllSql = "";

            if ("2".Equals(cbbGenerateType.SelectedValue.ToString()))
            {
                #region 表列注释
                int iTableExtend = 1;
                if (selectDBType == DataBaseType.SqlServer)
                {
                    #region SqlServer
                    foreach (DataRow drCol in dtMain.Rows)
                    {
                        string strExtendTableName = drCol["表名"].ToString();
                        string strExtendColName = drCol["列名"].ToString();
                        string strExtendText = drCol["注释"].ToString();

                        string strExtend = " SELECT " + StringHelper.ChangeIntoSqlString(strExtendTableName) + " TABLE_NAME,"
                                + StringHelper.ChangeIntoSqlString(strExtendColName) + " COLUMNS_NAME,"
                                + StringHelper.ChangeIntoSqlString(strExtendText) + " EXTEND_TEXT"
                                + " \n";
                        if (iTableExtend != 1)
                        {
                            strExtend = " UNION " + strExtend;
                        }
                        sbTableColumnList.Append(strExtend);
                        iTableExtend++;
                    }
                    //读取并替换
                    sbAllSql = File.ReadAllText(GetSystemFullPath(@"DataTemplate/DBTool/TableSQL/1.001_修改表列说明.sql"), Encoding.Default).Replace("#EXTEND_LIST#", sbTableColumnList.ToString());
                    #endregion
                }
                else
                {
                    foreach (DataRow drCol in dtMain.Rows)
                    {
                        string strExtendTableName = drCol["表名"].ToString();
                        string strExtendColName = drCol["列名"].ToString();
                        string strExtendText = drCol["注释"].ToString().Trim();
                        if (string.IsNullOrEmpty(strExtendTableName))
                        {
                            iTableExtend++;
                            continue;
                        }

                        if (string.IsNullOrEmpty(strExtendColName))
                        {
                            if (selectDBType == DataBaseType.Oracle)
                            {
                                sbTableColumnList.Append(string.Format(@"COMMENT ON TABLE {0} IS '{1}';" + "\n", strExtendTableName, strExtendText));
                            }
                            else if (selectDBType == DataBaseType.MySql)
                            {
                                sbTableColumnList.Append(string.Format(@"ALTER TABLE {0} COMMENT = '{1}';" + "\n", strExtendTableName, strExtendText));
                            }
                            else if (selectDBType == DataBaseType.PostgreSql)
                            {
                                //
                                sbTableColumnList.Append(string.Format(@"COMMENT ON TABLE {0} IS '{1}';" + "\n", strExtendTableName, strExtendText));
                            }
                            else
                            {
                                throw new Exception("暂不支持该数据库类型");
                            }
                        }
                        else
                        {
                            if (selectDBType == DataBaseType.Oracle)
                            {
                                sbTableColumnList.Append(string.Format(@"COMMENT ON COLUMN {0}.{1} IS '{2}';" + "\n", strExtendTableName, strExtendColName, strExtendText));
                            }
                            else if (selectDBType == DataBaseType.MySql)
                            {
                                //需要加上原字段信息，所以建议利用修改表列的SQL来修改
                                //strExtendList += string.Format(@"ALTER TABLE {0} MODIFY COLUMN {1} COMMENT '{2}';" + "\n", strExtendTableName, strExtendColName, strExtendText);
                            }
                            else if (selectDBType == DataBaseType.PostgreSql)
                            {
                                //
                                sbTableColumnList.Append(string.Format(@"COMMENT ON COLUMN {0}.{1} IS '{2}';" + "\n", strExtendTableName, strExtendColName, strExtendText));
                            }
                            else
                            {
                                throw new Exception("暂不支持该数据库类型");
                            }

                        }
                        iTableExtend++;
                    }
                    sbAllSql = sbTableColumnList.ToString();
                }
                #endregion
            }
            else
            {
                #region 表列索引
                SQLBuilder builder;
                switch (selectDBType)
                {
                    case DataBaseType.SqlServer:
                        builder = new SQLServerBuilder();
                        break;
                    case DataBaseType.Oracle:
                        builder = new OracleBuilder();
                        break;
                    case DataBaseType.MySql:
                        builder = new MySQLBuilder();
                        break;
                    case DataBaseType.SQLite:
                        builder = new SQLiteBuilder();
                        break;
                    case DataBaseType.PostgreSql:
                        builder = new PostgreSQLBuilder();
                        break;
                    default:
                        builder = new SQLServerBuilder();
                        break;
                }

                foreach (DataRow drCol in dtMain.Rows)
                {
                    string sTableName = drCol["表名"].ToString();
                    string sColName = drCol["列名"].ToString();
                    if(string.IsNullOrEmpty(sTableName) || string.IsNullOrEmpty(sColName))
                    {
                        continue;
                    }
                    bool sIsUniqueIndex = "是".Equals(drCol["是否唯一索引"].ToString());
                    string sIndexName = drCol["索引名"].ToString();
                    sbTableColumnList.AppendLine(builder.GenerateIndexSql(sTableName, sColName, sIsUniqueIndex, sIndexName));
                }
                sbAllSql = sbTableColumnList.ToString();
                #endregion
            }

            //生成SQL并提示
            rtbResult.Clear();
            rtbResult.AppendText(sbAllSql.ToString() + "\n");
            Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());
            tabControl1.SelectedTab = tpAutoSQL;
            //生成SQL成功后提示
            ShowInfo(StaticValue.GenResultCopySuccessMsg);
            rtbResult.Select(0, 0); //返回到第一
        }

        private void tsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.TableSQL.Excel_TableColumnRemark, "模板_表列注释", true);
        }

        private void tsbIndexTemplateDownload_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.TableSQL.Excel_TableColumnIndex, "模板_表列索引", true); 
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        
    }
}
