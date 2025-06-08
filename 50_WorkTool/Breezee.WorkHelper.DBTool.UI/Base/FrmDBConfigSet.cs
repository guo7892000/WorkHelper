using System;
using System.Data;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.Core.IOC;
using Breezee.Core.WinFormUI;
using System.IO;
using System.Reflection;
using Breezee.Core.Interface;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 创建作者：黄国辉
    /// 创建日期：2013-10-19
    /// 功能说明：数据库连接配置
    /// </summary>
    public partial class FrmDBConfigSet : BaseForm
    {
        #region 变量
        IDBConfigSet _IDBConfigSet;
        DataTable dtIsEnabel;
        DataTable dtDbType;
        bool _IsMoreSelected = false; //是否多选，默认为否
        public bool In_IsMoreSelected
        {
            get { return _IsMoreSelected; }
            set { _IsMoreSelected = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseType"></param>
        public FrmDBConfigSet()
        {
            InitializeComponent();
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmVersionInfo_Load(object sender, EventArgs e)
        {
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();

            SetTag();//设置Tag
            //数据库类型
            dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbDbType.BindTypeValueDropDownList(dtDbType, true, true);
            //状态
            _dicString["1"] = "启用";
            _dicString["0"] = "禁用";
            dtIsEnabel = _dicString.GetTextValueTable(false);
            cbbStatus.BindTypeValueDropDownList(dtIsEnabel.Copy(), true, true);
        }
        #endregion

        #region 设置Tag方法
        private void SetTag()
        {
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn("ROWNO", "序号", DataGridViewColumnTypeEnum.TextBox, true, 40, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn("IS_SELECTED", "选择", DataGridViewColumnTypeEnum.CheckBox, _IsMoreSelected, 60, DataGridViewContentAlignment.MiddleCenter, true, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_CODE, "配置编码", DataGridViewColumnTypeEnum.TextBox, true, 80, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_NAME, "配置名称", DataGridViewColumnTypeEnum.TextBox, true, 100, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.DB_TYPE, "数据库类型", DataGridViewColumnTypeEnum.ComboBox, true, 160, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.SERVER_IP, "服务器IP", DataGridViewColumnTypeEnum.TextBox, true, 260, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.DB_NAME, "数据库名称", DataGridViewColumnTypeEnum.TextBox, true, 160, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED, "状态", DataGridViewColumnTypeEnum.ComboBox, true, 80, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.SORT_ID, "排序号", DataGridViewColumnTypeEnum.TextBox, true, 80, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.REMARK, "备注", DataGridViewColumnTypeEnum.TextBox, true, 160, DataGridViewContentAlignment.MiddleLeft, false, 800);
            fdc.AddColumn(DT_DBT_BD_DB_CONFIG.SqlString.CREATE_TIME, "创建日期", DataGridViewColumnTypeEnum.TextBox, true, 100, DataGridViewContentAlignment.MiddleLeft, false, 800);
            dgvQuery.Tag = fdc.GetGridTagString();
            dgvQuery.BindDataGridView(null, true);           
        }
        #endregion

        #region 查询按钮事件
        private void tsbQuery_Click(object sender, EventArgs e)
        {
            _dicQuery[IDBConfigSet.QueryDbConfig_InDicKey.DB_TYPE] = cbbDbType.SelectedValue.ToString();
            _dicQuery[IDBConfigSet.QueryDbConfig_InDicKey.DB_CONFIG_CODE] = txbConfigCode.Text.Trim();
            _dicQuery[IDBConfigSet.QueryDbConfig_InDicKey.DB_NAME] = txbDbName.Text.Trim();
            _dicQuery[IDBConfigSet.QueryDbConfig_InDicKey.IS_ENABLED] = cbbStatus.SelectedValue.ToString();
            DataTable dtQuery = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            dgvQuery.BindDataGridView(dtQuery);

            //状态
            var cmbBig = (DataGridViewComboBoxColumn)dgvQuery.Columns[DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED];
            cmbBig.ValueMember = DT_BAS_VALUE.VALUE_CODE;
            cmbBig.DisplayMember = DT_BAS_VALUE.VALUE_NAME;
            cmbBig.DataSource = dtIsEnabel;
            //数据库类型
            cmbBig = (DataGridViewComboBoxColumn)dgvQuery.Columns[DT_DBT_BD_DB_CONFIG.SqlString.DB_TYPE];
            cmbBig.ValueMember = DT_BAS_VALUE.VALUE_CODE;
            cmbBig.DisplayMember = DT_BAS_VALUE.VALUE_NAME;
            cmbBig.DataSource = dtDbType;
        } 
        #endregion

        #region 选择按钮事件
        private void tsbSelect_Click(object sender, EventArgs e)
        {
            try
            {
                dgvQuery.EndEdit();

                BindingSource bs = (BindingSource)dgvQuery.DataSource;
                if (bs == null)
                {
                    ShowInfo("没有选择一条数据，请先查询！");
                    return;
                }
                bs.EndEdit();
                DataTable dtSelect;
                if (_IsMoreSelected)
                {
                    DataTable roleList = (DataTable)bs.DataSource;
                    dtSelect = roleList.Select("IS_SELECTED = 'True'").CopyToDataTable();
                }
                else
                {
                    DataRow dr = (bs.Current as DataRowView).Row;
                    dtSelect = dr.Table.Clone();
                    dtSelect.ImportRow(dr);
                }
                if (dtSelect.Rows.Count == 0)
                {
                    ShowInfo("请至少选择一条数据！");
                    return;
                }
                Tag = dtSelect;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message);
            }
        }
        #endregion

        #region 新增按钮事件
        private void tsbNew_Click(object sender, EventArgs e)
        {
            FrmDBConfigSet_D frm = new FrmDBConfigSet_D();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                tsbQuery.PerformClick();
            }
        } 
        #endregion

        #region 编辑按钮事件
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            DataRow drSelect = dgvQuery.GetCurrentRow();
            if (drSelect == null)
            {
                ShowErr("请选择一条数据！");
                return;
            }
            FrmDBConfigSet_D frm = new FrmDBConfigSet_D(drSelect);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                tsbQuery.PerformClick();
            }
        } 
        #endregion

        #region 关闭按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region 网格双击
        private void dgvQuery_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit.PerformClick();
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow drSelect = dgvQuery.GetCurrentRow();
            if (drSelect == null)
            {
                ShowErr("请选择一条数据！");
                return;
            }
            if (MsgHelper.ShowYesNo("确定要删除该配置？") == DialogResult.Yes)
            {
                _dicString[IDBConfigSet.DeleteDbConfig_InDicKey.DB_CONFIG_ID] = drSelect[DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_ID].ToString();
                _IDBConfigSet.DeleteDbConfig(_dicString).SafeGetDictionary();
                ShowInfo("删除成功！");
                tsbQuery.PerformClick();
            }
        }
    }
}