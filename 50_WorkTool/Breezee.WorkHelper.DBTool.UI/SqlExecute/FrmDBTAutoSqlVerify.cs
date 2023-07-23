using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDictionaryExtension = Breezee.Core.Interface.IDictionaryExtension;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 自动参数化SQL验证（MyPeach.Net项目的算法验证）
    /// </summary>
    public partial class FrmDBTAutoSqlVerify : BaseForm
    {
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
        private DataTable dtInputType;
        public FrmDBTAutoSqlVerify()
        {
            InitializeComponent();
        }

        private void FrmDBTAutoSqlVerify_Load(object sender, EventArgs e)
        {
            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            //uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            #endregion

            _dicString.Add("String", "String");
            _dicString.Add("DateTime", "DateTime");
            _dicString.Add("Int", "Int");
            dtInputType = _dicString.GetTextValueTable(false);
            SetTag();

            //加载用户偏好值
            rtbSqlInput.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SQLAutoParamVerify_BeforeSql, "").Value;
            lblBefore.Text = "条件格式：#键名:N:R:LS#，其中N或M表示非空，R表示值替换，LS表示字符列表，LI为整型列表，即IN括号里的部分字符。";
            lblFuncInfo.Text = "针对自动参数化SQL的个人项目（Java版和C#版）：MyPeach、MyPeach.Net的有效性验证！";
            rtbSqlOutput.ReadOnly= true;
        }

        private void SetTag()
        {
            //通用列网格跟所有列网格结构一样
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name("IN_KEY").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("IN_VALUE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("IN_TYPE").Type(DataGridViewColumnTypeEnum.ComboBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(0).Edit(true).Visible(false).Build()
            );
            dgvConditionInput.Tag = fdc.GetGridTagString();
            dgvConditionInput.BindDataGridView(fdc.GetNullTable()); 
            //仓库名称
            DataGridViewComboBoxColumn cmbWarehouse = dgvConditionInput.Columns["IN_TYPE"] as DataGridViewComboBoxColumn;
            cmbWarehouse.Name = IDictionaryExtension.Dictionary_Key;
            cmbWarehouse.HeaderText = "类型";
            cmbWarehouse.DataPropertyName = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.ValueMember = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.DisplayMember = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.DataSource = dtInputType;

            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name("OUT_KEY").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("OUT_VALUE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("OUT_TYPE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(0).Edit(false).Visible(false).Build()
            );
            dgvConditionOutput.Tag = fdc.GetGridTagString();
            dgvConditionOutput.BindDataGridView(fdc.GetNullTable());
        }

        /// <summary>
        /// 获取条件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCondition_Click(object sender, EventArgs e)
        {
            string sSqlBefore = rtbSqlInput.Text.Trim();
            if (string.IsNullOrEmpty(sSqlBefore))
            {
                ShowErr("请输入要参数化的SQL！");
                return;
            }
            _dbServer = uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                ShowErr("请选择一个连接！");
                return;
            }
            else
            {
                _dataAccess = AutoSQLExecutors.Connect(_dbServer); //每次都要获取一个连接，因为可能会连接不同数据库
            }
            IDictionary<string, SqlKeyValueEntity> dicPreCondition = _dataAccess.SqlParsers.PreGetParam(sSqlBefore);
            DataTable dt = dgvConditionInput.GetBindingTable();
            dt.Clear();
            foreach (var item in dicPreCondition.Keys)
            {
                DataRow drNew = dt.NewRow();
                drNew["IN_KEY"] = item;
                dt.Rows.Add(drNew);
            }
            tabControl1.SelectedTab = tpAutoAfter;
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {
            btnGetCondition.PerformClick();
        }

        private void tsbConvert_Click(object sender, EventArgs e)
        {
            string sSql = rtbSqlInput.Text.Trim();
            if (string.IsNullOrEmpty(sSql))
            {
                ShowErr("请输入要参数化的SQL！");
                return;
            }

            _dbServer = uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                ShowErr("请选择一个连接！");
                return;
            }
            else
            {
                _dataAccess = AutoSQLExecutors.Connect(_dbServer);
            }

            DataTable dt = dgvConditionInput.GetBindingTable();
            IDictionary<string,object> data = new Dictionary<string,object>();
            foreach (DataRow dr in dt.Rows)
            {
                string sKey = dr["IN_KEY"].ToString();
                string sValue = dr["IN_VALUE"].ToString();
                if (!data.ContainsKey(sKey))
                {
                    data.Add(sKey, sValue);
                }
            }
            //
            ParserResult result = _dataAccess.SqlParsers.parse(sSql, data);
            if ("0".equals(result.Code))
            {
                rtbSqlOutput.Text = result.Sql;
                dt = dgvConditionOutput.GetBindingTable();
                dt.Clear();
                foreach (var item in result.entityQuery)
                {
                    DataRow drNew = dt.NewRow();
                    drNew["OUT_KEY"] = item.Key;
                    drNew["OUT_VALUE"] = item.Value.KeyValue;
                    dt.Rows.Add(drNew);
                }
                _dicObject = result.ObjectQuery;
                //tabControl1.SelectedTab = tpAutoAfter;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SQLAutoParamVerify_BeforeSql, rtbSqlInput.Text, "SQL自动参数化验证_参数化前SQL");
                WinFormContext.UserLoveSettings.Save();
                ShowInfo("SQL参数化转换成功！");
            }
            else
            {
                ShowErr(result.Message);
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 参数化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            tsbConvert.PerformClick();
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            string sSqlOut = rtbSqlOutput.Text.Trim();
            if (string.IsNullOrEmpty(sSqlOut))
            {
                ShowErr("请先转换包含#参数#的SQL，得到参数化后的SQL，才能执行！");
                return;
            }
            try
            {
                DataTable dtResult = _dataAccess.QueryHadParamSqlData(sSqlOut, _dicObject);
                dgvQuery.BindAutoColumn(dtResult);
            }
            catch(Exception ex) 
            { 
                ShowErr(ex.Message);
            }
        }

        private void ckbExample_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbExample.Checked)
            {
                rtbSqlInput.Text = @"SELECT DISTINCT A.[PROVINCE_ID] AS PROVINCE_ID
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME]
 ,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#' AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB AS A
LEFT  OUTER JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
FULL JOIN BC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
RIGHT  OUTER  JOIN (
    SELECT ID,PROVINCE_ID,UPDATE_CONTROL_ID,ID
    FROM TAB_C
    WHERE PROVINCE_ID = '#PROVINCE_ID#'
       AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
) D on D.ID = B.ID AND D.TNAME ='#TNAME#'
WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
    AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
    AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd')
	AND MODIFIER IN ('#MDLIST:LS#')
    AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#BF#' )
GROUP BY 	PROVINCE_ID
HAVING A.SORT_ID > #SORT_ID:M:R#
ORDER BY A.[PROVINCE_CODE],B.[CITY_NAME]
LIMIT 100,#PAGE_SIZE:M#";
            }
        }
    }
}
