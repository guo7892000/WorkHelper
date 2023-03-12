using Breezee.Core.IOC;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    public partial class FrmDBTDefaultValueSet : BaseForm
    {
        IDBDefaultValue _IDBDefaultValue;
        public FrmDBTDefaultValueSet()
        {
            InitializeComponent();
        }

        private void FrmDBTDefaultValueSet_Load(object sender, EventArgs e)
        {
            SetTag();//设置Tag
            //
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();
            dgvQuery.DoubleClick += dgvQuery_DoubleClick;
        }

        #region 设置Tag方法
        private void SetTag()
        {
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME).Caption("列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_MYSQL).Caption("MySql默认值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_POSTGRESQL).Caption("PostgreSql默认值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_ORACLE).Caption("Oracle默认值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLSERVER).Caption("SqlServer默认值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLITE).Caption("SQLite默认值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_ADD).Caption("新增使用").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_UPDATE).Caption("修改使用").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_QUERY).Caption("查询条件").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_UPDATE).Caption("更新条件").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_DELETE).Caption("删除条件").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                FlexGridColumn.NewHideCol(DT_DBT_BD_COLUMN_DEFAULT.SqlString.COL_DEFAULT_ID),
                FlexGridColumn.NewHideCol(DT_DBT_BD_COLUMN_DEFAULT.SqlString.UPDATE_CONTROL_ID)
            );
            dgvQuery.Tag = fdc.GetGridTagString();
            dgvQuery.BindDataGridView(null, true);
        }
        #endregion
        private void TsbQuery_Click(object sender, EventArgs e)
        {
            _dicQuery[IDBConfigSet.QueryDbConfig_InDicKey.DB_TYPE] = txbDbName.Text.Trim();
            DataTable dtQuery = _IDBDefaultValue.QueryDefaultValue(_dicQuery).SafeGetDictionaryTable();
            dgvQuery.BindDataGridView(dtQuery);
        }

        private void TsbNew_Click(object sender, EventArgs e)
        {
            FrmDBTDefaultValueSet_D frm = new FrmDBTDefaultValueSet_D();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                tsbQuery.PerformClick();
            }
        }

        private void TsbEdit_Click(object sender, EventArgs e)
        {
            DataRow drSelect = dgvQuery.GetCurrentRow();
            if (drSelect == null)
            {
                ShowErr("请选择一条数据！");
                return;
            }
            FrmDBTDefaultValueSet_D frm = new FrmDBTDefaultValueSet_D(drSelect);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                tsbQuery.PerformClick();
            }
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region 网格双击
        private void dgvQuery_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit.PerformClick();
        }
        #endregion
    }
}
