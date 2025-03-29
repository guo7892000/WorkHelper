using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.IOC;
using Breezee.AutoSQLExecutor.Common;
using Breezee.Core.Tool;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Breezee.Core;
using FluentFTP;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using static Breezee.Core.WinFormUI.FlexGridColumn;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 表列比较
    /// </summary>
    public partial class FrmDBTTableColumnDiff : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";

        private readonly string _sGridColumnSelect = "IsSelect";
        private bool _allSelect = false;//默认全选，这里取反
        //常量
        private static string strTableAliasAndDot = "";
        //数据集
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServerSource;
        private DbServerInfo _dbServerTarget;
        private IDataAccess _dataAccess;
        DBSqlEntity sqlEntity;
        string _sColumnList = "#COL_LIST#";
        DataGridViewFindText dgvFindTextSource;
        DataGridViewFindText dgvFindTextTarget;
        DataGridViewFindText dgvFindTextResult;
        string sRowNo = "ROWNO";
        string sColumnPre = "_";
        #endregion

        #region 构造函数
        public FrmDBTTableColumnDiff()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();

            #region 设置数据库连接控件
            //数据库1
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnectionSource.SetDbConnComboBoxSource(dtConn);
            uC_DbConnectionSource.IsDbNameNotNull = true;
            uC_DbConnectionSource.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnectionSource.DBConnName_SelectedIndexChanged += cbbDBConnNameSource_SelectedIndexChanged;
            //数据库2
            uC_DbConnectionTarget.SetDbConnComboBoxSource(dtConn.Copy());
            uC_DbConnectionTarget.IsDbNameNotNull = true;
            uC_DbConnectionTarget.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnectionTarget.DBConnName_SelectedIndexChanged += cbbDBConnNameTarget_SelectedIndexChanged;
            #endregion

            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable(false);
            //目标数据库类型
            cbbTargetDbType.BindTypeValueDropDownList(dtDbType, false, true);

            //设置下拉框查找数据源
            cbbTableNameSource.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableNameSource.AutoCompleteSource = AutoCompleteSource.CustomSource;

            SetColTag();
        }

        private void cbbDBConnNameTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbGetTableListTarget_CheckedChanged(null, null);
        }

        private void cbbDBConnNameSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbGetTableList_CheckedChanged(null,null);
        }
        #endregion

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

        #region 连接数据库事件
        private void btnCompareSource_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }

        private void btnCompareTarge_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
        private async void btnConnectSource_Click(object sender, EventArgs e)
        {
            await GetSourceTableColumn();
        }

        private async void btnConnectTarget_Click(object sender, EventArgs e)
        {
            await GetTargetTableColumn();
        }
        private async void tsbImport_Click(object sender, EventArgs e)
        {
            await GetSourceTableColumn();
            await GetTargetTableColumn();
        }

        private async Task GetSourceTableColumn()
        {
            _dbServerSource = await uC_DbConnectionSource.GetDbServerInfo();
            string sTableName = cbbTableNameSource.Text.Trim();
            if (_dbServerSource == null || sTableName.IsNullOrEmpty())
            {
                return;
            }

            _dataAccess = AutoSQLExecutors.Connect(_dbServerSource);
            DataTable dtTable = DBTableEntity.GetTableStruct();

            DataRow[] drArr;
            string sFilter = DBTableEntity.SqlString.Name + "='" + sTableName + "'";
            if (!uC_DbConnectionSource.userTableDic.ContainsKey(uC_DbConnectionSource.LatestDbServerInfo.DbConnKey)|| uC_DbConnectionSource.userTableDic[uC_DbConnectionSource.LatestDbServerInfo.DbConnKey].Rows.Count == 0)
            {
                drArr = _dataAccess.GetSchemaTables().Select(sFilter);
            }
            else
            {
                drArr = uC_DbConnectionSource.userTableDic[uC_DbConnectionSource.LatestDbServerInfo.DbConnKey].Select(sFilter);
            }
            if (drArr.Count() == 0)
            {
                return;
            }
            else
            {
                DataRow dr = dtTable.NewRow();
                dr[DBTableEntity.SqlString.Owner] = drArr[0][DBTableEntity.SqlString.Owner].ToString();
                dr[DBTableEntity.SqlString.Name] = drArr[0][DBTableEntity.SqlString.Name].ToString();
                dr[DBTableEntity.SqlString.NameCN] = drArr[0][DBTableEntity.SqlString.NameCN].ToString();
                dr[DBTableEntity.SqlString.Schema] = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;
            //查询列数据
            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(sTableName, drArr[0][DBTableEntity.SqlString.Schema].ToString());
            DataTable dtColsNew = dgvColListSource.GetBindingTable();
            dtColsNew.Clear();
            foreach (DataRow dr in dtCols.Rows)
            {
                DBColumnEntity entity = DBColumnEntity.GetEntity(dr);

                DataTable dt = DBColumnSimpleEntity.GetDataRow(new List<DBColumnEntity> { entity });
                if (dt.Rows.Count > 0)
                {
                    dtColsNew.ImportRow(dt.Rows[0]);
                }
            }
            dgvColListSource.ShowRowNum(true, sRowNo);
            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
        }

        private async Task GetTargetTableColumn()
        {
            _dbServerTarget = await uC_DbConnectionTarget.GetDbServerInfo();
            string sTableName = cbbTableNameTarget.Text.Trim();
            if (_dbServerTarget == null || sTableName.IsNullOrEmpty())
            {
                return;
            }

            _dataAccess = AutoSQLExecutors.Connect(_dbServerTarget);
            DataTable dtTable = DBTableEntity.GetTableStruct();

            DataRow[] drArr;
            string sFilter = DBTableEntity.SqlString.Name + "='" + sTableName + "'";
            if (!uC_DbConnectionTarget.userTableDic.ContainsKey(uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey) || uC_DbConnectionTarget.userTableDic[uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey].Rows.Count == 0)
            {
                drArr = _dataAccess.GetSchemaTables().Select(sFilter);
            }
            else
            {
                drArr = uC_DbConnectionTarget.userTableDic[uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey].Select(sFilter);
            }
            if (drArr.Count() == 0)
            {
                return;
            }
            else
            {
                DataRow dr = dtTable.NewRow();
                dr[DBTableEntity.SqlString.Owner] = drArr[0][DBTableEntity.SqlString.Owner].ToString();
                dr[DBTableEntity.SqlString.Name] = drArr[0][DBTableEntity.SqlString.Name].ToString();
                dr[DBTableEntity.SqlString.NameCN] = drArr[0][DBTableEntity.SqlString.NameCN].ToString();
                dr[DBTableEntity.SqlString.Schema] = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;
            //查询列数据
            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(sTableName, drArr[0][DBTableEntity.SqlString.Schema].ToString());
            DataTable dtColsNew = dgvColListTarget.GetBindingTable();
            dtColsNew.Clear();
            foreach (DataRow dr in dtCols.Rows)
            {
                DBColumnEntity entity = DBColumnEntity.GetEntity(dr);

                DataTable dt = DBColumnSimpleEntity.GetDataRow(new List<DBColumnEntity> { entity });
                if (dt.Rows.Count > 0)
                {
                    dtColsNew.Rows.Add(dt.Rows[0].ItemArray);
                }
            }
            dgvColListTarget.ShowRowNum(true,sColumnPre + sRowNo);
            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
        }
        #endregion

        #region 设置Tag方法
        private void SetColTag()
        {
            DataTable dtColsNew = DBColumnSimpleEntity.GetTableStruct();
            dtColsNew.Columns.Add(sRowNo, typeof(int));

            DataTable dtColsNewTarget = new DataTable();
            foreach (DataColumn dc in dtColsNew.Columns)
            {
                if (dc.ColumnName.Equals(sRowNo))
                {
                    dtColsNewTarget.Columns.Add(sColumnPre + dc.ColumnName, typeof(int));//列名加前缀
                    continue;
                }
                dtColsNewTarget.Columns.Add(sColumnPre + dc.ColumnName);//列名加前缀
            }
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(sRowNo),
                //new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            dgvColListSource.Tag = fdc.GetGridTagString();
            dgvColListSource.BindDataGridView(dtColsNew, true);
            //目标网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(sColumnPre + sRowNo),
                //new FlexGridColumn.Builder().Name(sColumnPre + _sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            dgvColListTarget.Tag = fdc.GetGridTagString();
            dgvColListTarget.BindDataGridView(dtColsNewTarget, true);
            //结果网格绑定一个空表
            FlexGridColumnDefinition fdcResult = new FlexGridColumnDefinition();
            fdcResult.AddColumn(
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                FlexGridColumn.NewRowNoCol(sRowNo),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                FlexGridColumn.NewRowNoCol(sColumnPre + sRowNo),
                //new FlexGridColumn.Builder().Name(sColumnPre + _sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(sColumnPre + DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            dgvResult.Tag = fdcResult.GetGridTagString();
            dgvResult.BindAutoColumn(new DataTable(), true);
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            //取得数据源
            DataTable dtMain = dgvColListSource.GetBindingTable();
            DataTable dtSec = dgvColListTarget.GetBindingTable();

            if (dtMain == null || dtSec==null) return;
            //移除空行
            dtMain.DeleteNullRow();
            dtSec.DeleteNullRow();
            if (dtSec.Columns.Contains(sRowNo))
            {
                dtSec.Columns.Remove(sRowNo);
            }
            //得到变更后数据
            dtMain.AcceptChanges();
            dtSec.AcceptChanges();

            #region 表结构处理
            DataTable dtResult = new DataTable();
            //dtResult.Columns.Clear();
            foreach (DataColumn item in dtMain.Columns)
            {
                if (!dtResult.Columns.Contains(item.ColumnName))
                {
                    dtResult.Columns.Add(item.ColumnName, item.DataType); //这里要跟原表类型一致
                }
            }
            foreach (DataColumn item in dtSec.Columns)
            {
                if (!dtResult.Columns.Contains(item.ColumnName))
                {
                    dtResult.Columns.Add(item.ColumnName, item.DataType); //这里要跟原表类型一致
                }
            }
            #endregion

            //查询
            var query = from f in dtMain.AsEnumerable()
                        join s in dtSec.AsEnumerable()
                        on new { c1 = f.Field<string>(DBColumnSimpleEntity.SqlString.Name) }
                        equals new { c1 = s.Field<string>(sColumnPre + DBColumnSimpleEntity.SqlString.Name) }
                        select new { F1 = f, S1 = s };
            var joinList = query.ToList();
            var restult = joinList.Select(t => t.F1.ItemArray.Concat(t.S1.ItemArray).ToArray()); //这里最后必须要加上ToArray

            //交集部分内容
            //foreach (var item in restult)
            //{
            //    dtResult.Rows.Add(item); //增加行数据：当行太多时，这里会报内存溢出System.OutOfMemoryException错误
            //}

            //左边独有部分
            var leftJoin2 = from f in dtMain.AsEnumerable()
                            where !joinList.Any(j => j.F1.Field<int>(sRowNo) == f.Field<int>(sRowNo))
                            select new { F1 = f };
            var LeftJoinRes2 = leftJoin2.ToList();
            foreach (var item in LeftJoinRes2)
            {
                dtResult.Rows.Add(item.F1.ItemArray.Concat(new object[dtSec.Columns.Count]).ToArray()); //增加行数据
            }
            //右边独有部分
            var rightJoin2 = from f in dtSec.AsEnumerable()
                             where !joinList.Any(j => j.S1.Field<int>(sColumnPre + sRowNo) == f.Field<int>(sColumnPre + sRowNo))
                             select new { F1 = f };
            var rightJoinRes2 = rightJoin2.ToList();
            foreach (var item in rightJoinRes2)
            {
                dtResult.Rows.Add(new object[dtMain.Columns.Count].Concat(item.F1.ItemArray).ToArray()); //增加行数据
            }

            //增加选择列：注用BindDataGridView时，就不用指定表的列类型为bool类型；只有使用BindAutoColumn时，才需要指定表的列类型为bool类型。
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtResult.Columns.Add(dcSelected);
            dtResult.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面

            //绑定已知的两个序号列
            dgvResult.BindDataGridView(dtResult, false);
            dgvResult.ShowRowNum(true);
            tabControl1.SelectedTab = tpResult;
            ShowInfo("比较成功，请查看差异！");//生成SQL成功后提示
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 获取表清单复选框变化事件
        private async void ckbGetTableList_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGetTableListSource.Checked)
            {
                _dbServerSource = await uC_DbConnectionSource.GetDbServerInfo();
                if (_dbServerSource == null)
                {
                    return;
                }
                //绑定下拉框
                cbbTableNameSource.BindDropDownList(uC_DbConnectionSource.userTableDic[uC_DbConnectionSource.LatestDbServerInfo.DbConnKey].Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", false);
                //查找自动完成数据源
                cbbTableNameSource.AutoCompleteCustomSource.AddRange(uC_DbConnectionSource.userTableDic[uC_DbConnectionSource.LatestDbServerInfo.DbConnKey].AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableNameSource.DataSource = null;
            }
        }
        private async void ckbGetTableListTarget_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGetTableListTarget.Checked)
            {
                _dbServerTarget = await uC_DbConnectionTarget.GetDbServerInfo();
                if (_dbServerTarget == null)
                {
                    return;
                }
                //绑定下拉框
                cbbTableNameTarget.BindDropDownList(uC_DbConnectionTarget.userTableDic[uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey].Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", false);
                //查找自动完成数据源
                cbbTableNameTarget.AutoCompleteCustomSource.AddRange(uC_DbConnectionTarget.userTableDic[uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey].AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableNameTarget.DataSource = null;
            }
        }
        #endregion

        #region 生成增删改查SQL方法
        /// <summary>
        /// 设置表说明
        /// </summary>
        /// <param name="strTableCode"></param>
        /// <param name="strColComments"></param>
        /// <returns></returns>
        public string MakeTableComment(string strTableCode, string strColComments)
        {
            if (!string.IsNullOrEmpty(strColComments))
            {
                return DataBaseCommon.AddLeftBand(strTableCode) + sqlEntity.Tab + "/*" + strColComments + "*/" + sqlEntity.NewLine;
            }
            return DataBaseCommon.AddLeftBand(strTableCode) + sqlEntity.Tab + sqlEntity.NewLine;
        }

        /// <summary>
        /// 设置查询列说明
        /// </summary>
        /// <param name="strComma">为逗号或空</param>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColComments">列说明</param>
        /// <returns></returns>
        public string MakeQueryColumnComment(string strComma, string strColCode, string strColComments)
        {
            if (!string.IsNullOrEmpty(strColComments))
            {
                return sqlEntity.Tab + strColCode + strComma + sqlEntity.Tab + "/*" + strColComments + "*/";
            }
            return sqlEntity.Tab + strColCode + strComma;
        }

        /// <summary>
        /// 设置列值说明方法
        /// </summary>
        /// <param name="strComma">为逗号或空</param>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColValue">列值</param>
        /// <param name="strColComments">列说明</param>
        /// <param name="strColType">列数据类型</param>
        /// <returns></returns>
        private string MakeColumnValueComment(SqlType sqlTypeNow, string strComma, string strColCode, string strColValue, string strColComments, string strColType, SqlParamFormatType paramType, string colParam)
        {
            string strColRemark = "";

            if (!string.IsNullOrEmpty(strColComments))
            {
                if (sqlTypeNow == SqlType.Insert)
                {
                    strColRemark = "/*" + strColCode + ":" + strColComments + "*/";//新增显示列名和备注
                }
                else if (sqlTypeNow == SqlType.Update)
                {
                    strColRemark = "/*" + strColComments + "*/"; //修改不显示列名，只显示备注
                }
            }

            string strColRelValue = "";
            if (string.IsNullOrEmpty(strColValue)) //列没有默认值则加引号
            {
                //列值为空时
                if (strColType == "DATE")
                {
                    //如果是Oracle的时间类型DATE，则需要将字符转为时间格式，要不会报“文字与字符串格式不匹配”错误
                    strColRelValue = "TO_DATE(" + DataBaseCommon.QuotationMark + colParam + DataBaseCommon.QuotationMark + ",'YYYY-MM-DD')";
                    if (paramType == SqlParamFormatType.SqlParm)
                    {
                        strColRelValue = "TO_DATE(" + colParam + ",'YYYY-MM-DD')";
                    }
                }
                else
                {
                    strColRelValue = colParam;
                    if (paramType == SqlParamFormatType.BeginEndHash)
                    {
                        strColRelValue = DataBaseCommon.QuotationMark + colParam + DataBaseCommon.QuotationMark;
                    }
                }
            }
            else //列有默认值则不加引号
            {
                strColRelValue = strColValue;
            }

            if (sqlTypeNow == SqlType.Insert)
            {
                return sqlEntity.Tab + strColRelValue + strComma + sqlEntity.Tab + strColRemark;
            }
            else //sqlTypeNow == SqlType.Update
            {
                return sqlEntity.Tab + strTableAliasAndDot + strColCode + "=" + strColRelValue + strComma + sqlEntity.Tab + strColRemark;
            }
        }


        /// <summary>
        /// 设置条件列说明
        /// </summary>
        /// <param name="strColCode">列编码</param>
        /// <param name="strColValue">列值</param>
        /// <param name="strColComments">列说明</param>
        /// <returns></returns>
        public string MakeConditionColumnComment(string strColCode, string strColValue, string strColComments, SqlParamFormatType paramType, string sTableAliasAndDot, string colParam)
        {
            string strRemark = sqlEntity.NewLine;
            if (!string.IsNullOrEmpty(strColComments))
            {
                strRemark = "/*" + strColComments + "*/" + sqlEntity.NewLine + "";
            }
            if (string.IsNullOrEmpty(strColValue))
            {
                if (paramType == SqlParamFormatType.SqlParm)
                {
                    return sTableAliasAndDot + strColCode + " = " + colParam + sqlEntity.Tab + strRemark;
                }
                //列值为空时，设置为：'#列编码#'
                return sTableAliasAndDot + strColCode + "=" + DataBaseCommon.QuotationMark + colParam + DataBaseCommon.QuotationMark + sqlEntity.Tab + strRemark;
            }
            else
            {
                //有固定值时
                return sTableAliasAndDot + strColCode + "=" + strColValue + sqlEntity.Tab + strRemark;
            }
        }

        #endregion

        private async void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableNameSource.Text.Trim())) return;
            await GetSourceTableColumn();
        }
        private async void cbbTableNameTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableNameTarget.Text.Trim())) return;
            await GetTargetTableColumn();
        }

        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvColListSource.Columns[_sGridColumnSelect].Index)
            {
                dgvColListSource.AllChecked(_sGridColumnSelect, ref _allSelect);
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
            string sSearch = txbSearchColumnSource.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvColListSource.SeachText(sSearch, ref dgvFindTextSource, null, isNext, ckbTableFixedSource.Checked);
            lblFindSource.Text = dgvFindTextSource.CurrentMsg;
        }

        private void FindGridTextTarget(bool isNext)
        {
            string sSearch = txbSearchColumnTarget.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvColListTarget.SeachText(sSearch, ref dgvFindTextTarget, null, isNext, ckbTableFixedTarget.Checked);
            lblFindTarget.Text = dgvFindTextTarget.CurrentMsg;
        }

        private void btnFindNextTarget_Click(object sender, EventArgs e)
        {
            FindGridTextTarget(true);
        }

        private void btnFindFrontTarget_Click(object sender, EventArgs e)
        {
            FindGridTextTarget(false);
        }

        private void FindGridTextResult(bool isNext)
        {
            string sSearch = txbSearchColumnResult.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvResult.SeachText(sSearch, ref dgvFindTextResult, null, isNext, ckbTableFixedResult.Checked);
            lblFindResult.Text = dgvFindTextResult.CurrentMsg;
        }
        private void btnFindNextResult_Click(object sender, EventArgs e)
        {
            FindGridTextResult(true);
        }

        private void btnFindFrontResult_Click(object sender, EventArgs e)
        {
            FindGridTextResult(false);
        }

        private async void btnGenerateDbDoc_Click(object sender, EventArgs e)
        {
            DataTable dtColSource = dgvResult.GetBindingTable();
            if (dtColSource == null || dtColSource.Rows.Count == 0) return;

            DataRow[] drArr = dtColSource.Select(_sGridColumnSelect + "='1'");
            if (drArr.Length == 0)
            {
                ShowErr("请至少选列一条数据！");
                return;
            }

            string sTableCode = txbTableCode.Text.Trim();
            if (string.IsNullOrEmpty(sTableCode))
            {
                sTableCode = drArr[0][DBColumnSimpleEntity.SqlString.TableName].ToString();
                if (string.IsNullOrEmpty(sTableCode))
                {
                    sTableCode = drArr[0][sColumnPre + DBColumnSimpleEntity.SqlString.TableName].ToString();
                }
            }

            string sTableName = txbTableCN.Text.Trim();
            if (string.IsNullOrEmpty(sTableName)) 
            {
                sTableName = drArr[0][DBColumnSimpleEntity.SqlString.TableNameCN].ToString();
                if (string.IsNullOrEmpty(sTableName))
                {
                    sTableName = drArr[0][sColumnPre + DBColumnSimpleEntity.SqlString.TableNameCN].ToString();
                }
            }

            string sTableRemark = txbTableRemark.Text.Trim();
            if (string.IsNullOrEmpty(sTableRemark))
            {
                sTableRemark = drArr[0][DBColumnSimpleEntity.SqlString.TableExtra].ToString();
                if (string.IsNullOrEmpty(sTableRemark))
                {
                    sTableRemark = drArr[0][sColumnPre + DBColumnSimpleEntity.SqlString.TableExtra].ToString();
                }
            }

            //构造表结构
            DataTable dtTableCopy = EntTable.GetTable();
            DataRow drTable = dtTableCopy.NewRow();
            drTable[EntTable.ExcelTable.Num] = "1";
            drTable[EntTable.ExcelTable.Code] = sTableCode;
            drTable[EntTable.ExcelTable.Name] = sTableName;
            drTable[EntTable.ExcelTable.ChangeType] = "修改";
            drTable[EntTable.ExcelTable.CommonColumnTableCode] = "";
            drTable[EntTable.ExcelTable.Remark] = sTableRemark;
            dtTableCopy.Rows.Add(drTable);

            ColumnTemplateType templateType;
            DataBaseType dataBaseType  = (DataBaseType)int.Parse(cbbTargetDbType.SelectedValue.ToString());
            SQLBuilder sqlBuilder;
            DataBaseType importBaseType = DataBaseType.Oracle;
            DbServerInfo sourceServer = await uC_DbConnectionSource.GetDbServerInfo();
            DbServerInfo targetServer = await uC_DbConnectionTarget.GetDbServerInfo();

            switch (dataBaseType)
            {
                case DataBaseType.SqlServer:
                    templateType = ColumnTemplateType.SqlServer;
                    sqlBuilder = new SQLServerBuilder();
                    break;
                case DataBaseType.Oracle:
                    templateType = ColumnTemplateType.Oracle;
                    sqlBuilder = new OracleBuilder();
                    break;
                case DataBaseType.MySql:
                    templateType = ColumnTemplateType.MySql;
                    sqlBuilder = new MySQLBuilder();
                    break;
                case DataBaseType.SQLite:
                    templateType = ColumnTemplateType.SQLite;
                    sqlBuilder = new SQLiteBuilder();
                    break;
                case DataBaseType.PostgreSql:
                    templateType = ColumnTemplateType.PostgreSql;
                    sqlBuilder = new PostgreSQLBuilder();
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
            }
            DataTable dtColsNew = EntCol.GetTable(templateType);
            string sPreString = string.Empty;
            foreach (DataRow drSource in drArr)
            {
                if (string.IsNullOrEmpty(drSource[DBColumnSimpleEntity.SqlString.Name].ToString()))
                {
                    sPreString = sColumnPre;
                    importBaseType = targetServer.DatabaseType;
                }
                else
                {
                    sPreString = string.Empty;
                    importBaseType = sourceServer.DatabaseType;
                }

                DataRow dr = dtColsNew.NewRow();
                string sDataType = drSource[sPreString + DBColumnSimpleEntity.SqlString.DataType].ToString();
                string sDataLength = drSource[sPreString + DBColumnSimpleEntity.SqlString.DataLength].ToString();
                string sDataScale = drSource[sPreString + DBColumnSimpleEntity.SqlString.DataScale].ToString();
                string sColName = drSource[sPreString + DBColumnSimpleEntity.SqlString.Name].ToString();
                string sDefault = drSource[sPreString + DBColumnSimpleEntity.SqlString.Default].ToString();
                sqlBuilder.ConvertDBTypeDefaultValueString(ref sDataType, ref sDefault, importBaseType);
                dr[ColCommon.ExcelCol.ChangeType] = "新增";
                dr[ColCommon.ExcelCol.TableCode] = sTableCode;
                dr[ColCommon.ExcelCol.Code] = sColName;
                dr[ColCommon.ExcelCol.Name] = drSource[sPreString + DBColumnSimpleEntity.SqlString.NameCN].ToString();
                dr[ColCommon.ExcelCol.DataType] = sDataType;
                dr[ColCommon.ExcelCol.DataLength] = sDataLength;
                dr[ColCommon.ExcelCol.DataDotLength] = sDataScale;
                dr[ColCommon.ExcelCol.Default] = sDefault;
                dr[ColCommon.ExcelCol.KeyType] = "";// drSource[sPreString + DBColumnSimpleEntity.SqlString.KeyType].ToString();
                dr[ColCommon.ExcelCol.NotNull] = "1".Equals(drSource[sPreString + DBColumnSimpleEntity.SqlString.NotNull].ToString()) ? "是" : "";
                dr[ColCommon.ExcelCol.Remark] = drSource[sPreString + DBColumnSimpleEntity.SqlString.Extra].ToString();
                dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataScale); //全类型：未转换
                dtColsNew.Rows.Add(dr);
            }

            TableStructGeneratorParamEntity docEntity = new TableStructGeneratorParamEntity();
            docEntity.builder = sqlBuilder;
            docEntity.importDBType = importBaseType;
            docEntity.useDataTypeFull = true;
            docEntity.useLYTemplate = true;
            docEntity.useRemarkContainsName = true;
            TableStructGenerator.Generate(tabControl2, dtTableCopy, dtColsNew, docEntity);
            tabControl2.SelectTab(TableStructGenerator.TABKEY_TABLE_STRUCT);
        }
    }
}