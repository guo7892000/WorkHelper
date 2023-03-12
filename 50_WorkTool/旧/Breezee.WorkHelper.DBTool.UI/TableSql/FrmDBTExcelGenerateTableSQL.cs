using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// Excel导入生成表结构(新)
    /// </summary>
    public partial class FrmDBTExcelGenerateTableSQL : BaseForm
    {
        #region 变量
        //数据库类型
        private DataBaseType importDBType;//导入数据库类型
        private DataBaseType targetDBType;//目标数据库类型
        private bool _isAllConvert = false;//是否综合转换，即导入一种数据库模块，而生成另一种数据库类型
        //
        private DataSet _dsExcelData;
        private BindingSource bsTable = new BindingSource();
        private BindingSource bsCos = new BindingSource();
        //
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        private string _strImportSuccess = "导入成功！可点“生成SQL”按钮得到本次导入的变更SQL。";
        private string strTipInfo = "不需要的行，请选择整行后，按Delete键即可删除！";

        //生成过程用到的全局变量
        private StringBuilder sbSql = new StringBuilder();
        //private StringBuilder sbRemark = new StringBuilder();
        private DataTable dtTable;
        private DataTable dtAllCol;
        private string createType;
        #endregion

        #region 构造函数
        public FrmDBTExcelGenerateTableSQL()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmDBTImportExcelGenerateTableSQL_Load(object sender, EventArgs e)
        {
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            //目标数据库类型
            cbbTargetDbType.BindTypeValueDropDownList(dtDbType, false, true);
            //导入数据库类型
            cbbImportDBType.BindTypeValueDropDownList(dtDbType, false, true);
            
            //创建方式
            _dicString.Add(((int)SQLCreateType.Create).ToString(), "不判断增加");
            _dicString.Add(((int)SQLCreateType.Drop_Create).ToString(), "先删后增加");
            _dicString.Add(((int)SQLCreateType.Drop).ToString(), "生成删除SQL");
            cbbCreateType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            _dicString.Clear();
            //设置表、列的删除提示
            lblTableData.Text = strTipInfo;
            lblColumnInfo.Text = strTipInfo;
            ckbAllConvert.Checked = true; //为了减少工作量，统一使用模板中的【列】页签来生成表SQL
        }
        #endregion

        #region 导入按钮事件
        private void tsbImport_Click(object sender, EventArgs e)
        {
            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            _dsExcelData = ExportHelper.GetExcelDataSet();//得到Excel数据
            if (_dsExcelData != null)
            {
                bsTable.DataSource = _dsExcelData.Tables[EntExcelSheet.Table];
                bsCos.DataSource = _dsExcelData.Tables[GetExcelSheetNameCol(importDBType)];

                dgvTableList.DataSource = bsTable;
                dgvColList.DataSource = bsCos;
                //ShowInfo("导入成功！");
                lblInfo.Text = _strImportSuccess;
                //ckbAllConvert.Enabled = false;
            }

        }
        #endregion

        private string GetExcelSheetNameCol(DataBaseType impDBType)
        {
            string colTableName;
            if (ckbAllConvert.Checked)
            {
                colTableName = EntExcelSheet.Col;
                _isAllConvert = true;
            }
            else
            {
                switch (impDBType)
                {
                    case DataBaseType.SqlServer:
                        colTableName = EntExcelSheet.Col_SqlServer;
                        break;
                    case DataBaseType.Oracle:
                        colTableName = EntExcelSheet.Col_Oracle;
                        break;
                    case DataBaseType.MySql:
                        colTableName = EntExcelSheet.Col_MySql;
                        break;
                    case DataBaseType.SQLite:
                        colTableName = EntExcelSheet.Col_SQLite;
                        break;
                    case DataBaseType.PostgreSql:
                        colTableName = EntExcelSheet.Col_PostgreSql;
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                        //break;
                }
            }

            return colTableName;
        }
        
        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            //移除数据库变更记录页签
            TableStructGenerator.RemoveTab(tabControl1);
            rtbResult.Clear();

            #region 结束编辑处理
            bsTable.EndEdit();
            dtTable = (DataTable)bsTable.DataSource;
            dtAllCol = (DataTable)bsCos.DataSource;

            if (dtTable == null || dtTable.Rows.Count == 0)
            {
                ShowErr("请先导入数据库结构生成模块！");
                return;
            }

            //移除空行
            foreach (DataRow dr in dtTable.Select("表编码 is null or 序号 is null"))
            {
                dtTable.Rows.Remove(dr);
            }
            foreach (DataRow dr in dtAllCol.Select("列编码 is null or 表编码 is null"))
            {
                dtAllCol.Rows.Remove(dr);
            }

            dtTable.AcceptChanges();
            dtAllCol.AcceptChanges();
            #endregion

            //目标数据库类型
            int iDbType = int.Parse(cbbTargetDbType.SelectedValue.ToString());
            targetDBType = (DataBaseType)iDbType;
            //创建方式
            createType = cbbCreateType.SelectedValue.ToString();

            if (!ValidateData())//校验数据
            {
                return;
            }

            SQLBuilder builder;
            switch (targetDBType)
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
            string sSql = builder.GenerateTableContruct(dtTable, dtAllCol, (SQLCreateType)int.Parse(createType), importDBType, targetDBType, _isAllConvert) + "\n";
            rtbResult.AppendText(sSql);
            Clipboard.SetData(DataFormats.UnicodeText, sSql);
            tabControl1.SelectedTab = tpAutoSQL;

            //增加生成表结构的功能
            dtAllCol.AcceptChanges();
            TableStructGenerator.Generate(tabControl1, dtTable, dtAllCol);
            //生成SQL成功后提示
            ShowSuccessMsg(_strAutoSqlSuccess);
            //初始化控件
            ckbAllConvert.Enabled = true;
        }
        #endregion

        #region 导入数据库类型选择变化
        private void cbbImportDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ckbAllConvert.Checked)
            {
                ChangeImprotDataColsSource();
            }
        }
        #endregion

        private void ChangeImprotDataColsSource()
        {
            if (_dsExcelData == null) return;
            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            string sExcelSheetName = GetExcelSheetNameCol(importDBType);
            bsCos.DataSource = _dsExcelData.Tables[sExcelSheetName]; //更换【列】网格数据源
        }

        #region 下载模板按钮事件
        private void tsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.TableSQL.Excel_TableColumn, "模板_表列结构变更", true);
        } 
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 成功提示方法
        /// <summary>
        /// 成功提示方法
        /// </summary>
        private void ShowSuccessMsg(string strInfo)
        {
            //ShowInfo(strInfo);
            lblInfo.Text = strInfo;
            rtbResult.Select(0, 0); //返回到第一行
        }
        #endregion

        #region 检查数据的有效性
        private bool ValidateData()
        {
            #region 全部判断
            if (dtTable.Select("变更类型 not in ('新增','修改')").Length > 0)
            {
                ShowErr("变更类型只能是“新增”或“修改”！");
                return false;
            }
            if (dtTable.Select("变更类型='新增' and (表名称='' or 表编码='')").Length > 0)
            {
                ShowErr("新增的表，其“表名称、表编码”都不能为空！");
                return false;
            }
            if (dtAllCol.Select("表编码=''").Length > 0)
            {
                ShowErr("新增的列中表编码不能为空！");
                return false;
            }
            if (dtAllCol.Select("表编码='' or 列名称='' or 列编码='' or 类型=''").Length > 0)
            {
                ShowErr("新增的列中表编码、列名称、列编码、类型不能为空！");
                return false;
            }
            foreach (DataRow dr in dtAllCol.Select("(类型 like 'VARCHAR%' or 类型 like 'NVARCHAR%' or 类型 like 'CHAR%') and (长度 is null)"))
            {
                ShowErr(dr["表编码"].ToString() + "的" + dr["列编码"].ToString() + "，其" + dr["类型"].ToString() + "类型的长度不能为空！");
                return false;
            }
            #endregion

            #region 新增表和列的判断
            DataRow[] drNewArray = dtTable.Select("变更类型='新增'");
            
            foreach (DataRow drNew in drNewArray)
            {
                string strTableCode = drNew["表编码"].ToString();
                string strTableName = drNew["表名称"].ToString();
                string strChangeType = drNew["变更类型"].ToString();
                if (dtAllCol.Select("表编码='" + strTableCode + "'").Length == 0)
                {
                    ShowErr("表" + strTableCode + "中没有本次更变的列，请删除该表或新增列！");
                    return false;
                }
                if (dtAllCol.Select("表编码='" + strTableCode + "' and (变更类型 is not null and 变更类型<>'新增')").Length > 0)
                {
                    ShowErr("新增的表" + strTableCode + "中，只能全部为新增列！");
                    return false;
                }

                if (dtAllCol.Select("表编码='" + strTableCode + "' and 键='PK'").Length == 0)
                {
                    ShowErr("新增的表" + strTableCode + "没有主键！");
                    return false;
                }

                if (_isAllConvert)
                {
                    #region 综合转换
                    if (targetDBType == DataBaseType.Oracle)
                    {
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and 键='FK' and (Oracle外键 is not null and Oracle外键名 not like 'FK_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中键为“FK”时，“Oracle外键名”列内容格式必须以“FK_”开头！");
                            return false;
                        }

                        if (dtAllCol.Select("表编码='" + strTableCode + "' and (Oracle序列名 is not null and Oracle序列名 not like 'SQ_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中，“Oracle序列名”列内容格式必须以“SQ_”开头！");
                            return false;
                        }
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and (Oracle唯一约束名 is not null and Oracle唯一约束名 not like 'UQ_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中，“Oracle唯一约束名”列内容格式必须以“UQ_”开头！");
                            return false;
                        }
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and 键='FK' and (Oracle外键 is null or Oracle外键名 is null)").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中键为“FK”时，“Oracle外键、Oracle外键名”列为必填！");
                            return false;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 非综合转换
                    if (importDBType == DataBaseType.Oracle)
                    {
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and 键='FK' and (外键 is not null and 外键名 not like 'FK_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中键为“FK”时，“外键名”列内容格式必须以“FK_”开头！");
                            return false;
                        }

                        if (dtAllCol.Select("表编码='" + strTableCode + "' and (序列名 is not null and 序列名 not like 'SQ_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中，“序列名”列内容格式必须以“SQ_”开头！");
                            return false;
                        }
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and (唯一约束名 is not null and 唯一约束名 not like 'UQ_%')").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中，“唯一约束名”列内容格式必须以“UQ_”开头！");
                            return false;
                        }
                        if (dtAllCol.Select("表编码='" + strTableCode + "' and 键='FK' and (外键 is null or 外键名 is null)").Length > 0)
                        {
                            ShowErr("新增的表" + strTableCode + "中键为“FK”时，“外键、外键名”列为必填！");
                            return false;
                        }
                    } 
                    #endregion
                }
                
            }
            #endregion

            return true;
        }
        #endregion
   
        #region 综合转换复选框变化事件
        private void ckbAllConvert_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImprotDataColsSource();
        }
        #endregion

        private void BtnSaveOther_Click(object sender, EventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.FileName = ".sql";
            diag.Filter = "Sql文件|*.sql";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(diag.FileName, rtbResult.Text);
            }
        }
    }
}
