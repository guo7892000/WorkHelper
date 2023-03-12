using Breezee.Core.Entity;
using Breezee.Core.IOC;
using Breezee.Core.Tool;
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
    /// <summary>
    /// 数据库配置维护
    /// </summary>
    public partial class FrmDBTDefaultValueSet_D : BaseForm
    {
        #region 变量
        private DataRow _drEdit;
        private IDBDefaultValue _IDBDefaultValue;
        #endregion

        #region 构造函数
        public FrmDBTDefaultValueSet_D()
        {
            InitializeComponent();
        }

        public FrmDBTDefaultValueSet_D(DataRow drEdit)
        {
            InitializeComponent();
            _drEdit = drEdit;

        }
        #endregion

        #region 窗体加载事件
        private void FrmDBConfigSet_D_Load(object sender, EventArgs e)
        {
            //接口对象
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();
            //设置控件关系
            SetControlColumnRelation();

            if (_drEdit == null)//新增
            {

            }
            else //修改
            {
                _listControlColumn.SetControlValue(_drEdit);
            }
        }
        #endregion

        #region 设置列名与控件关系
        private void SetControlColumnRelation()
        {
            //配置表
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.COL_DEFAULT_ID, txbID));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.UPDATE_CONTROL_ID, txbUPDATE_CONTROL_ID));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME, txbColumnName, "字段名"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_MYSQL, txbMySql, "MySql默认值"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_POSTGRESQL, txbPostgreSql, "PostgreSql默认值"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_ORACLE, txbOracle, "Oracle默认值"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLSERVER, txbSqlServer, "SqlServer默认值"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLITE, txbSQLite, "SQLite默认值"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_ADD, ckbAdd, "新增语句"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_UPDATE, ckbUpdate, "修改语句"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_QUERY, ckbQueryCondition, "是否查询条件"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_UPDATE, ckbModifyCondition, "是否修改条件"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_DELETE, ckbDeleteCondition, "是否删除条件"));
            _listControlColumn.Add(new DBColumnControlRelation(DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED, ckbEnabled, "是否启用"));
        }
        #endregion

        #region 保存按钮事件
        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 保存前判断
                string strInfo = _listControlColumn.JudgeNotNull(true);
                if (!string.IsNullOrEmpty(strInfo))
                {
                    ShowInfo("保存失败！\n" + strInfo);
                    return;
                }
                #endregion

                _dicObject = CreateObjectDictionary(true);
                DataTable dtSave;

                #region 保存
                bool isAdd = txbID.Text.Length == 0;

                List<string> coloumns = isAdd ? null : _listControlColumn.GetSaveColumnNameList();
                dtSave = DBToolHelper.Instance.DataAccess.GetTableConstruct(DT_DBT_BD_COLUMN_DEFAULT.TName, coloumns);
                dtSave.DefaultValue(_loginUser);
                _listControlColumn.GetControlValue(dtSave, isAdd);
                if (isAdd)
                {
                    dtSave.Rows[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.COL_DEFAULT_ID] = StringHelper.GetGUID();
                }
                #endregion
                //保存传入参数处理
                _dicObject[DT_SYS_USER.ORG_ID] = _loginUser.ORG_ID;
                _dicObject[DT_SYS_USER.USER_ID] = _loginUser.USER_ID;
                _dicObject[DT_ORG_EMPLOYEE.EMP_ID] = _loginUser.EMP_ID;
                _dicObject[DT_ORG_EMPLOYEE.EMP_NAME] = _loginUser.EMP_NAME;
                _dicObject[IDBDefaultValue.SaveDefaultValue_InDicKey.DT_TABLE] = dtSave;
                //保存维修单
                _IDBDefaultValue.SaveDefaultValue(_dicObject).SafeGetDictionary();
                ShowInfo("保存成功！");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        } 
        #endregion
        
        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

    }
}
