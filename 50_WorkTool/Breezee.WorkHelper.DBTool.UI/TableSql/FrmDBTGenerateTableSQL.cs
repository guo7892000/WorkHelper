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
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.IBLL;
using System.Linq;
using Breezee.AutoSQLExecutor.Core;
using Breezee.AutoSQLExecutor.Common;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 生成表结构SQL
    /// </summary>
    public partial class FrmDBTExcelGenerateTableSQL : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";
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
        private string _ImportInput = "1";
        private readonly string _sGridTableSelect = "选择";
        private readonly string _sGridColumnSelect = "选择";
        private bool _allTableSelect = false;//默认全选，这里取反
        private bool _allColumnSelect = false;//默认全选，这里取反
        DataGridViewFindText dgvFindText;

        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
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
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable(true);
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
            //录入方式
            _dicString.Add(_ImportInput, "模板导入");
            _dicString.Add("2", "读取数据库");
            cbbInputType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //设置表、列的删除提示
            lblTableData.Text = strTipInfo;
            lblColumnInfo.Text = strTipInfo;
            //ckbAllConvert.Checked = true; //为了减少工作量，统一使用模板中的【列】页签来生成表SQL

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            //uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbDBConnName_SelectedIndexChanged;
            #endregion
            //加载用户喜好值
            ckbExcludeColumn.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsExcludeColumn, "1").Value) ? true : false;
            txbExcludeColumn.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_ExcludeColumnList, "").Value;
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void cbbDBConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;
            if (!_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                //导入类型为查询的数据库类型
                string sDbType = uC_DbConnection1.getSelectedDatabaseType();
                if (!string.IsNullOrEmpty(sDbType))
                {
                    cbbImportDBType.SelectedValue = sDbType;
                    cbbImportDBType.SetControlReadOnly();
                }

            }
        }
        #endregion

        #region 数据库类型下拉框变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {

        }
        #endregion

        #region 获取表清单复选框变化事件
        private void ckbGetTableList_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGetTableList.Checked)
            {
                _dbServer = uC_DbConnection1.GetDbServerInfo();
                if (_dbServer == null)
                {
                    return;
                }
                //绑定下拉框
                cbbTableName.BindDropDownList(uC_DbConnection1.UserTableList.Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", true,false);
                //查找自动完成数据源
                cbbTableName.AutoCompleteCustomSource.AddRange(uC_DbConnection1.UserTableList.AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableName.DataSource = null;
            }
        }
        #endregion

        #region 导入按钮事件
        private void tsbImport_Click(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;

            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            ColumnTemplateType templateType;
            if (_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                //导入
                if (importDBType == DataBaseType.None)
                {
                    ShowInfo("请选择【导入数据库类型】");
                    return;
                }

                _dsExcelData = ExportHelper.GetExcelDataSet();//得到Excel数据
                if (_dsExcelData != null)
                {
                    //绑定表
                    DataTable dtTable = _dsExcelData.Tables[EntExcelSheet.Table];
                    DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtTable.Columns.Add(dcSelected);
                    dtTable.Columns[_sGridTableSelect].SetOrdinal(0);//设置选择列在最前面
                    //移除空行
                    foreach (DataRow dr in dtTable.Select("表编码 is null or 序号 is null"))
                    {
                        dtTable.Rows.Remove(dr);
                    }
                    //绑定网格
                    bsTable.DataSource = dtTable;
                    //dgvTableList.DataSource = bsTable;
                    dgvTableList.BindAutoColumn(bsTable);
                    dgvTableList.ShowRowNum();
                    //绑定列
                    dtTable = _dsExcelData.Tables[GetExcelSheetNameCol(importDBType)];
                    //默认主键处理
                    if (ckbDefaultPKName.Checked)
                    {
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            if ("PK".Equals(dr[ColCommon.ExcelCol.KeyType].ToString()))
                            {
                                //统一主键处理
                                string sPKName = "PK_" + dr[ColCommon.ExcelCol.TableCode].ToString();
                                //针对特殊数据的字段赋值
                                switch (importDBType)
                                {
                                    case DataBaseType.SqlServer:
                                    case DataBaseType.MySql:
                                        break;
                                    case DataBaseType.Oracle:
                                        dr[ColOracleTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    case DataBaseType.SQLite:
                                        dr[ColSQLiteTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    case DataBaseType.PostgreSql:
                                        dr[ColPostgreSqlTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    default:
                                        throw new Exception("暂不支持该数据库类型！");
                                }
                            }
                        }
                    }

                    dcSelected = new DataColumn(_sGridColumnSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtTable.Columns.Add(dcSelected);
                    dtTable.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面
                    foreach (DataRow dr in dtTable.Select("列编码 is null or 表编码 is null"))
                    {
                        dtTable.Rows.Remove(dr);
                    }
                    //绑定网格
                    bsCos.DataSource = dtTable;
                    //dgvColList.DataSource = bsCos;
                    dgvColList.BindAutoColumn(bsCos);
                    dgvColList.ShowRowNum();
                    ShowInfo(_strImportSuccess);
                }
            }
            else
            {
                //查询数据库
                _dbServer = uC_DbConnection1.GetDbServerInfo();
                string sTableName = cbbTableName.Text.Trim();
                if (_dbServer == null)
                {
                    return;
                }

                switch (_dbServer.DatabaseType)
                {
                    case DataBaseType.SqlServer:
                        templateType = ColumnTemplateType.SqlServer;
                        break;
                    case DataBaseType.Oracle:
                        templateType = ColumnTemplateType.Oracle;
                        break;
                    case DataBaseType.MySql:
                        templateType = ColumnTemplateType.MySql;
                        break;
                    case DataBaseType.SQLite:
                        templateType = ColumnTemplateType.SQLite;
                        break;
                    case DataBaseType.PostgreSql:
                        templateType = ColumnTemplateType.PostgreSql;
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                }

                _dataAccess = AutoSQLExecutors.Connect(_dbServer);
                
                DataRow[] drArr = null;
                string sFilter = DBTableEntity.SqlString.Name + "='" + sTableName + "'";
                DataTable dtTable;
                string sSchma = "";

                DataTable dtTableCopy = EntTable.GetTable();
                if (uC_DbConnection1.UserTableList == null || uC_DbConnection1.UserTableList.Rows.Count == 0)
                {
                    dtTable = _dataAccess.GetSchemaTables();
                }
                else
                {
                    dtTable = uC_DbConnection1.UserTableList;
                    sSchma = dtTable.Rows[0][DBTableEntity.SqlString.Schema].ToString();
                }

                if (!sTableName.IsNullOrEmpty())
                {
                    drArr = dtTable.Select(sFilter);
                    if (drArr == null || drArr.Count() == 0)
                    {
                        return;
                    }
                    sSchma = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                    DataRow dr = dtTableCopy.NewRow();
                    dr[EntTable.ExcelTable.Num] = "1";
                    dr[EntTable.ExcelTable.Code] = drArr[0][DBTableEntity.SqlString.Name].ToString();
                    dr[EntTable.ExcelTable.Name] = drArr[0][DBTableEntity.SqlString.NameCN].ToString();
                    dr[EntTable.ExcelTable.ChangeType] = "新增";
                    dr[EntTable.ExcelTable.CommonColumnTableCode] = "";
                    dr[EntTable.ExcelTable.Remark] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                    dtTableCopy.Rows.Add(dr);
                }
                else
                {
                    int i = 1;
                    foreach (DataRow drSource in dtTable.Rows)
                    {
                        DataRow dr = dtTableCopy.NewRow();
                        dr[EntTable.ExcelTable.Num] = i++;
                        dr[EntTable.ExcelTable.Code] = drSource[DBTableEntity.SqlString.Name].ToString();
                        dr[EntTable.ExcelTable.Name] = drSource[DBTableEntity.SqlString.NameCN].ToString();
                        dr[EntTable.ExcelTable.ChangeType] = "新增";
                        dr[EntTable.ExcelTable.CommonColumnTableCode] = "";
                        dr[EntTable.ExcelTable.Remark] = drSource[DBTableEntity.SqlString.Comments].ToString();
                        dtTableCopy.Rows.Add(dr);
                    }
                }
                dtTableCopy.TableName = _strTableName;
                SetTableTag(dtTableCopy);
                SetColTag(sSchma, templateType);

                //保存用户喜好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_ExcludeColumnList, txbExcludeColumn.Text.Trim(), "【生成表SQL】排除指定列清单");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsExcludeColumn, ckbExcludeColumn.Checked ? "1" : "0", "【生成表SQL】是否排除指定列");
                WinFormContext.UserLoveSettings.Save();
            }
            //设置不能增加行
            dgvTableList.AutoGenerateColumns = true;
            dgvTableList.AllowUserToAddRows = false;
            dgvColList.AutoGenerateColumns = true;
            dgvColList.AllowUserToAddRows = false;

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
                _isAllConvert = false;
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
            label3.Focus();

            #region 结束编辑处理
            bsTable.EndEdit();
            dtTable = (DataTable)bsTable.DataSource;
            dtAllCol = (DataTable)bsCos.DataSource;

            if (dtTable == null || dtTable.Rows.Count == 0)
            {
                ShowErr("请先导入数据库结构生成模板，或查询数据库中的表结构信息！");
                return;
            }

            string sFilter = EntTable.ExcelTable.Code + " is null or " + EntTable.ExcelTable.Num + " is null";
            //移除空行
            foreach (DataRow dr in dtTable.Select(sFilter))
            {
                dtTable.Rows.Remove(dr);
            }
            sFilter= ColCommon.ExcelCol.Code + " is null or " + ColCommon.ExcelCol.TableCode + " is null";
            foreach (DataRow dr in dtAllCol.Select(sFilter))
            {
                dtAllCol.Rows.Remove(dr);
            }

            dtTable.AcceptChanges();
            dtAllCol.AcceptChanges();

            sFilter = _sGridTableSelect + " = 'True' ";
            DataTable dtTalbeSelect = dtTable.Clone();
            foreach (DataRow dr in dtTable.Select(sFilter))
            {
                dtTalbeSelect.ImportRow(dr);
            }

            
            DataTable dtColumnSelect = dtAllCol.Clone();
            foreach (DataRow drT in dtTalbeSelect.Rows)
            {
                sFilter = EntTable.ExcelTable.Code + "='"+ drT[EntTable.ExcelTable.Code].ToString() + "' and "  + _sGridColumnSelect + " = 'True' ";
                foreach (DataRow dr in dtAllCol.Select(sFilter))
                {
                    dtColumnSelect.ImportRow(dr);
                }
            }
            
            if(dtTalbeSelect.Rows.Count == 0 || dtColumnSelect.Rows.Count == 0)
            {
                ShowErr("请至少选择一个表及它的列！");
                return;
            }

            #endregion

            int importDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)importDbType;
            if (importDBType == DataBaseType.None)
            {
                ShowInfo("请选择【导入数据库类型】");
                return;
            }
            //目标数据库类型
            int iDbType = int.Parse(cbbTargetDbType.SelectedValue.ToString());
            targetDBType = (DataBaseType)iDbType;
            if(targetDBType== DataBaseType.None)
            {
                ShowInfo("请选择【目标数据库类型】");
                return;
            }
            //创建方式
            createType = cbbCreateType.SelectedValue.ToString();

            if (!ValidateData(importDBType, targetDBType, dtTalbeSelect, dtColumnSelect))//校验数据
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

            if (!dtColumnSelect.Columns.Contains(ColCommon.ExcelCol.DataTypeNew))
            {
                dtColumnSelect.Columns.Add(ColCommon.ExcelCol.DataTypeNew);
            }
            if (!dtColumnSelect.Columns.Contains(ColCommon.ExcelCol.DataTypeFullNew))
            {
                dtColumnSelect.Columns.Add(ColCommon.ExcelCol.DataTypeFullNew);
            }

            foreach (DataRow dr in dtColumnSelect.Rows)
            {
                string sDataType = dr[ColCommon.ExcelCol.DataType].ToString();
                string sDefault = dr[ColCommon.ExcelCol.Default].ToString();
                string sDataLength = dr[ColCommon.ExcelCol.DataLength].ToString();
                string sDataDotLength = dr[ColCommon.ExcelCol.DataDotLength].ToString();
                builder.ConvertDBTypeDefaultValueString(ref sDataType, ref sDefault, importDBType);
                dr[ColCommon.ExcelCol.DataTypeNew] = sDataType;
                dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataDotLength);
            }

            string sSql = builder.GenerateTableContruct(dtTalbeSelect, dtColumnSelect, (SQLCreateType)int.Parse(createType), importDBType, targetDBType, _isAllConvert) + "\n";
            rtbResult.AppendText(sSql);
            Clipboard.SetData(DataFormats.UnicodeText, sSql);
            tabControl1.SelectedTab = tpAutoSQL;

            //增加生成表结构的功能
            dtAllCol.AcceptChanges();
            TableStructGenerator.Generate(tabControl1, dtTalbeSelect, dtColumnSelect,ckbFullTypeDoc.Checked,ckbLYTemplate.Checked);
            //生成SQL成功后提示
            ShowSuccessMsg(_strAutoSqlSuccess);
            //初始化控件
            ckbAllConvert.Enabled = true;
        }
        #endregion

        #region 检查数据的有效性
        private bool ValidateData(DataBaseType importDbType, DataBaseType targetDbType,DataTable dtTalbeSelect, DataTable dtColumnSelect)
        {
            //通用的判断
            if(!ColCommon.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sb))
            {
                ShowErr(sb.ToString()); 
                return false;
            }

            if (_isAllConvert)
            {
                //综合转换判断
                if (!ColAllInOne.ValidateData(dtTalbeSelect, dtColumnSelect, targetDBType, out StringBuilder sbAll))
                {
                    ShowErr(sbAll.ToString());
                    return false;
                }
            }
            else if(importDbType == targetDbType)
            {
                #region 非综合转换，且导入类型跟目标类型一致的判断
                if (targetDBType == DataBaseType.Oracle)
                {
                    if (!ColOracleTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.SqlServer)
                {
                    if (!ColSqlServerTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.MySql)
                {
                    if (!ColMySqlTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.SQLite)
                {
                    if (!ColSQLiteTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.PostgreSql)
                {
                    if (!ColPostgreSqlTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else
                {
                    throw new Exception("暂不支持该数据库类型！");
                }
                #endregion
            }
            return true;
        }
        #endregion

        #region 导入数据库类型选择变化
        private void cbbImportDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeImprotDataColsSource();
        }
        #endregion

        private void ChangeImprotDataColsSource()
        {
            if (_dsExcelData == null) return;
            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            if (importDBType == DataBaseType.None) return;
            string sExcelSheetName = GetExcelSheetNameCol(importDBType);
            DataTable dtCol = _dsExcelData.Tables[sExcelSheetName]; //更换【列】网格数据源
            if (!dtCol.Columns.Contains(_sGridTableSelect))
            {
                DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                dcSelected.DefaultValue = "True";
                dtCol.Columns.Add(dcSelected);
                dtCol.Columns[_sGridTableSelect].SetOrdinal(0);//设置选择列在最前面
            }

            bsCos.DataSource = dtCol;
            if (!ckbAllConvert.Checked)
            {
                cbbTargetDbType.SelectedValue = cbbImportDBType.SelectedValue;
            }
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
            ShowInfo(strInfo);
            rtbResult.Select(0, 0); //返回到第一行
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

        private void cbbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;
            if (_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                gbTable.Visible = false;              
                tsbImport.Text = "导入";
                ckbAllConvert.Checked = true;
                ckbAllConvert.Visible = true;
                cbbImportDBType.SetControlReadOnly(false);
            }
            else
            {
                //连接数据库
                gbTable.Visible = true;
                tsbImport.Text = "连接";
                ckbAllConvert.Checked = false;
                ckbAllConvert.Visible = false;
                cbbImportDBType.SetControlReadOnly(true);
            }
            dgvTableList.Columns.Clear();
            dgvTableList.DataSource = null;
            dgvColList.Columns.Clear();
            dgvColList.DataSource = null;
        }

        private void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableName.Text.Trim())) return;
            tsbImport.PerformClick();
        }

        private void SetTableTag(DataTable dt)
        {
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridTableSelect,typeof(bool));
            dcSelected.DefaultValue = "True";
            dt.Columns.Add(dcSelected);
            dt.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面
            bsTable.DataSource = dt;
            dgvTableList.BindAutoColumn(bsTable);
            dgvTableList.ShowRowNum();
        }

        #region 设置Tag方法
        private void SetColTag(string sSchema, ColumnTemplateType templateType)
        {
            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim(), sSchema);

            DataTable dtColsNew = EntCol.GetTable(templateType);
            bool isExclude = ckbExcludeColumn.Checked;
            string[] arrExclude = txbExcludeColumn.Text.Trim().ToLower().Split(new char[] { ',','，',';','；' }, StringSplitOptions.RemoveEmptyEntries);
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect, typeof(bool));
            dcSelected.DefaultValue = "True";
            dtColsNew.Columns.Add(dcSelected);
            dtColsNew.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面

            foreach (DataRow drSource in dtCols.Rows)
            {
                DataRow dr = dtColsNew.NewRow();
                string sDataType = drSource[DBColumnEntity.SqlString.DataType].ToString();
                string sDataLength = drSource[DBColumnEntity.SqlString.DataLength].ToString();
                //string sDataPrecision = drSource[DBColumnEntity.SqlString.DataPrecision].ToString();
                string sDataScale = drSource[DBColumnEntity.SqlString.DataScale].ToString();
                string sColName = drSource[DBColumnEntity.SqlString.Name].ToString();
                if (isExclude && arrExclude.Length>0 && arrExclude.Contains(sColName.ToLower()))
                {
                    dr[_sGridColumnSelect] = "False";
                }
                dr[ColCommon.ExcelCol.ChangeType] = "新增";
                dr[ColCommon.ExcelCol.TableCode] = drSource[DBColumnEntity.SqlString.TableName].ToString();
                dr[ColCommon.ExcelCol.Code] = sColName;
                dr[ColCommon.ExcelCol.Name] = drSource[DBColumnEntity.SqlString.NameCN].ToString();
                dr[ColCommon.ExcelCol.DataType] = sDataType;
                dr[ColCommon.ExcelCol.DataLength] = sDataLength;
                dr[ColCommon.ExcelCol.DataDotLength] = sDataScale;
                dr[ColCommon.ExcelCol.Default] = drSource[DBColumnEntity.SqlString.Default].ToString();
                dr[ColCommon.ExcelCol.KeyType] = drSource[DBColumnEntity.SqlString.KeyType].ToString();
                dr[ColCommon.ExcelCol.NotNull] = "1".Equals(drSource[DBColumnEntity.SqlString.NotNull].ToString()) ? "是" : "";
                dr[ColCommon.ExcelCol.Remark] = drSource[DBColumnEntity.SqlString.Comments].ToString();
                dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataScale); //全类型
                //统一主键处理
                string sPKName = "PK_" + drSource[DBColumnEntity.SqlString.TableName].ToString();
                //针对特殊数据的字段赋值
                switch (_dbServer.DatabaseType)
                {
                    case DataBaseType.SqlServer:
                        dr[ColSqlServerTemplate.ExcelCol.FK] = "";
                        break;
                    case DataBaseType.Oracle:
                        dr[ColOracleTemplate.ExcelCol.FK] = "";
                        if (ckbDefaultPKName.Checked && "PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColOracleTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    case DataBaseType.MySql:
                        dr[ColMySqlTemplate.ExcelCol.FK] = "";
                        break;
                    case DataBaseType.SQLite:
                        dr[ColSQLiteTemplate.ExcelCol.FK] = "";
                        if (ckbDefaultPKName.Checked && "PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColSQLiteTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    case DataBaseType.PostgreSql:
                        dr[ColPostgreSqlTemplate.ExcelCol.FK] = "";
                        if (ckbDefaultPKName.Checked && "PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColPostgreSqlTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                }

                dtColsNew.Rows.Add(dr);
            }

            dtColsNew.TableName = _strColName;

            bsCos.DataSource = dtColsNew;
            //dgvColList.DataSource= dtColsNew;
            dgvColList.BindAutoColumn(bsCos,true);
            dgvColList.ShowRowNum();
        }
        #endregion

        private void dgvTableList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvTableList.Columns[_sGridTableSelect].Index)
            {
                dgvTableList.AllChecked(_sGridTableSelect, ref _allTableSelect);
            }
        }

        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvColList.Columns[_sGridColumnSelect].Index)
            {
                dgvColList.AllChecked(_sGridColumnSelect, ref _allColumnSelect);
            }
        }

        private void ckbLYTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbLYTemplate.Checked)
            {
                ckbFullTypeDoc.Checked = true;
            }
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            FindGridText(true);
        }

        private void btnFindFront_Click(object sender, EventArgs e)
        {
            FindGridText(false);
        }

        private void FindGridText(bool isNext)
        {
            string sSearch = txbSearchColumn.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvColList.SeachText(sSearch, ref dgvFindText, null, isNext);
            lblFind.Text = dgvFindText.CurrentMsg;
        }

        /// <summary>
        /// 显示方向右键按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDirectShow_Click(object sender, EventArgs e)
        {
            Orientation orientationNew = splitContainer1.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
            if (orientationNew.Equals(Orientation.Vertical))
            {
                splitContainer1.SplitterDistance = int.Parse(Math.Ceiling(splitContainer1.Width * 0.3).ToString());
            }
            else
            {
                splitContainer1.SplitterDistance = int.Parse(Math.Ceiling(splitContainer1.Height * 0.3).ToString());
            }
            splitContainer1.Orientation = orientationNew;
        }

        private void tsmiChooseOrNot_Click(object sender, EventArgs e)
        {
            if (dgvColList.SelectedCells == null || dgvColList.SelectedCells.Count == 0) return;
            if (dgvColList.CurrentCell.ColumnIndex != dgvColList.Columns[_sGridColumnSelect].Index)
            {
                return; //选择、条件、MyBatis动态列
            }
            //选择
            bool sNew = bool.Parse(dgvColList.CurrentCell.Value.ToString()) ? false : true;
            foreach (DataGridViewCell item in dgvColList.SelectedCells)
            {
                //为了防止选了其他列，这里只针对选择列赋值
                if(item.ColumnIndex == dgvColList.Columns[_sGridColumnSelect].Index)
                {
                    item.Value = sNew;
                }
            }

            dgvColList.CurrentCell.Value = sNew;

            //解决当开始是全部选中，双击后全部取消选 中，但因为焦点没有离开选择列，显示还是选中状态的问题
            dgvColList.ChangeCurrentCell(dgvColList.CurrentCell.ColumnIndex);
        }

        private void ckbExcludeColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbExcludeColumn.Checked && bsCos.DataSource != null)
            {
                DataTable dtCol = bsCos.DataSource as DataTable;
                bool isExclude = ckbExcludeColumn.Checked;
                string[] arrExclude = txbExcludeColumn.Text.Trim().ToLower().Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (DataRow drSource in dtCol.Rows)
                {
                    string sColName = drSource[DBColumnEntity.SqlString.Name].ToString();
                    if (isExclude && arrExclude.Length > 0 && arrExclude.Contains(sColName.ToLower()))
                    {
                        drSource[_sGridColumnSelect] = "False";
                    }
                }
            }
        }
    }
}
