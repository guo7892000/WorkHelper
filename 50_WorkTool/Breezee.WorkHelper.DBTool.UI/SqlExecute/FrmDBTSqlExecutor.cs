using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    public partial class FrmDBTSqlExecutor : BaseForm
    {
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;

        public FrmDBTSqlExecutor()
        {
            InitializeComponent();
        }

        private void FrmDBTSqlExecutor_Load(object sender, EventArgs e)
        {
            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            _dicQuery[DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED] = "1";
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            //uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            #endregion
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

        private async void tsbImport_Click(object sender, EventArgs e)
        {
            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                return;
            }
            _dataAccess = AutoSQLExecutors.Connect(_dbServer);
        }

        private async void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                ShowErr("请选择一个连接！");
                return;
            }
            else
            {
                if (_dataAccess==null)
                {
                    _dataAccess = AutoSQLExecutors.Connect(_dbServer);
                }
            }

            //优先使用选中的文件
            bool isAllExec = false;
            string sSql = rtbSql.SelectedText.Trim();
            string sSqlAll = rtbSql.Text.Trim();
            if (string.IsNullOrEmpty(sSql) && string.IsNullOrEmpty(sSqlAll))
            {
                ShowErr("请输入SQL！");
                return;
            }
            if (string.IsNullOrEmpty(sSql))
            {
                if(ShowQuestion("执行文本内所有SQL?", MyButtons.OKCancel) == DialogResult.Cancel) 
                {
                    ShowInfo("已取消执行！");
                    return;
                }
                isAllExec = true;
                sSql = sSqlAll;//没有选择，那么获取SQL中所有文本
            }
            //针对更新和删除
            string updateSetPattern = "^UPDATE\\s*\\S*\\s*SET\\s*";//正则式：UPDATE TABLE_NAME SET
            string deletePattern = "^DELETE\\s+FROM\\s+\\S+\\s+"; //正则式:DELETE FROM TABALE_NAME
            Regex regex = new Regex(updateSetPattern, RegexOptions.IgnoreCase);
            var mcCollUpdate = regex.Matches(sSql);
            regex = new Regex(deletePattern, RegexOptions.IgnoreCase);
            var mcCollDelete = regex.Matches(sSql);

            if (mcCollUpdate.Count > 0 || mcCollDelete.Count > 0)
            {
                int i = _dataAccess.ExecuteNonQueryHadParamSql(sSql,new List<FuncParam>(),null,null);
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("影响行");
                dtNew.Rows.Add(i);
                dgvQuery.BindAutoColumn(dtNew);
            }
            else
            {
                DataTable dtQuery = _dataAccess.QueryHadParamSqlData(sSql, new List<FuncParam>(), null, null);
                dgvQuery.BindAutoColumn(dtQuery);
            }
            ShowErr(isAllExec? "全部执行完毕！" : "执行完毕！");
            rtbSql.Focus();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
    }
}
