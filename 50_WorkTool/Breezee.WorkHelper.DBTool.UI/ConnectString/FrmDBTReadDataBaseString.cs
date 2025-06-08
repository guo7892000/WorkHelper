using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 读取数据拼接字符
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTReadDataBaseString : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        //
        private BindingSource bsTable = new BindingSource();
        private BindingSource bsCos = new BindingSource();//
        private BindingSource bsThree = new BindingSource();//
        //常量

        //导入的SQL变量值
        private string _strMainSql = "";//主SQL
        //数据访问层
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
        #endregion

        #region 构造函数
        public FrmDBTReadDataBaseString()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmReadDataBaseString_Load(object sender, EventArgs e)
        {
            //设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            _dicQuery[DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED] = "1";
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = false;
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbDBConnName_SelectedIndexChanged;
            // 数据类型
            _dicString.Add("1", "单表数据");
            _dicString.Add("2", "多表数据");
            cbbDataType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //
            toolTip1.SetToolTip(cbbDataType, "单表数据：通过选择表名及条件查询单表数据；\r\n多表数据：通过自定义SQL实现多表数据查询。");
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            groupBox2.AddFoldRightMenu();
        }

        private void cbbDBConnName_SelectedIndexChanged(object sender, EventArgs e)
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

        #region 连接按钮事件
        private async void tsbConnect_Click(object sender, EventArgs e)
        {
            if (cbbDataType.SelectedValue == null) return;
            var splitType = cbbDataType.SelectedValue.ToString();
            try
            {
                string strWhere = rtbWhere.Text.Trim();
                if ("1".Equals(splitType))
                {
                    //非空判断
                    string strTableName = cbbTableName.Text.Trim();
                    if (string.IsNullOrEmpty(strTableName))
                    {
                        ShowErr("表名不能为空！");
                        return;
                    }

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        if (strWhere.ToLower().StartsWith("where"))
                        {
                            strWhere = " " + rtbWhere.Text.Trim();
                        }
                        else
                        {
                            strWhere = " WHERE " + rtbWhere.Text.Trim();
                        }
                        _strMainSql = "SELECT *  FROM " + strTableName + strWhere;
                    }
                    else
                    {
                        _strMainSql = "SELECT *  FROM " + strTableName;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(strWhere))
                    {
                        ShowErr("自定义SQL不能为空！");
                        return;
                    }
                    _strMainSql = strWhere;
                }
                    
                //得到服务器对象
                _dbServer = await uC_DbConnection1.GetDbServerInfo();
                if (_dbServer == null)
                {
                    return;
                }
                //得到数据库访问对象
                IDataAccess dataAccess = AutoSQLExecutor.Common.AutoSQLExecutors.Connect(_dbServer);
                //查询数据
                DataTable dtMain = dataAccess.QueryHadParamSqlData(_strMainSql,_dicQuery);
                dtMain.TableName = _strTableName;
                bsTable.DataSource = dtMain;
                //设置数据源
                WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtMain });
                dgvTableList.DataSource = bsTable;
                dgvTableList.ShowRowNum();
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        } 
        #endregion

        #region 自动生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                string sbAllSql = "";
                DataTable dtMain = (DataTable)bsTable.DataSource;
                if (dtMain.Rows.Count == 0)
                {
                    ShowInfo("没有可生成的数据！");
                    return;
                }
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    //初始化单条数据为书写的文本
                    string strOneData = rtbConString.Text.Trim();
                    for (int j = 0; j < dtMain.Columns.Count; j++)
                    {
                        string strData = dtMain.Rows[i][j].ToString().Trim();
                        //将数据中的列名替换为单元格中的数据
                        strOneData = strOneData.Replace("#" + dtMain.Columns[j].ColumnName + "#", strData);
                    }
                    //所有SQL文本累加
                    sbAllSql += strOneData + (ckbResultNewLine.Checked ? "\n" : "");
                }
                //保存属性
                //PropSetting.Default.Save();
                rtbResult.Clear();
                rtbResult.AppendText(sbAllSql.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());
                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                ShowInfo(StaticValue.GenResultCopySuccessMsg);
                rtbResult.Select(0, 0); //返回到第一行
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        } 
        #endregion

        #region 帮助按钮事件
        private void tsbHelp_Click(object sender, EventArgs e)
        {

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
            if (ckbGetTableList.Checked)
            {
                _dbServer = await uC_DbConnection1.GetDbServerInfo();
                if (_dbServer == null)
                {
                    return;
                }
                //绑定下拉框
                cbbTableName.BindDropDownList(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", false);
                //查找自动完成数据源
                cbbTableName.AutoCompleteCustomSource.AddRange(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableName.DataSource = null;
            }
        }
        #endregion

        /// <summary>
        /// 右键加入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiInsert_Click(object sender, EventArgs e)
        {
            if (dgvTableList.CurrentCell == null) return;

            int iCurCol = dgvTableList.CurrentCell.ColumnIndex;
            rtbConString.AppendText(string.Format("#{0}#",dgvTableList.Columns[iCurCol].Name));
        }

        private void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableName.Text.Trim())) return;
            tsbConnect.PerformClick();
        }

        #region 网格右键事件
        private void AppendCondition(string sPre, string sLikeEqual, string sValue = "''")
        {
            if (dgvTableList.CurrentCell == null) return;
            string sColName = dgvTableList.Columns[dgvTableList.CurrentCell.ColumnIndex].Name;

            if (string.IsNullOrEmpty(rtbWhere.Text.Trim()))
            {
                sPre = "";
            }
            rtbWhere.AppendText(string.Format("{0}{1}{2}{3}", sPre, sColName, sLikeEqual, sValue));
        }
        private void TsmiAndLike_Click(object sender, EventArgs e)
        {
            AppendCondition(" AND ", " Like ", "'%%'");
        }

        private void TsmiAndEqual_Click(object sender, EventArgs e)
        {
            AppendCondition(" AND ", " = ");
        }

        private void TsmiOrEqual_Click(object sender, EventArgs e)
        {
            AppendCondition(" OR ", " = ");
        }

        private void TsmiOrLike_Click(object sender, EventArgs e)
        {
            AppendCondition(" OR ", " Like ", "'%%'");
        }
        private void TsmiAndIn_Click(object sender, EventArgs e)
        {
            AppendCondition(" AND ", " in ", "('','','')");
        }

        private void TsmiOrIn_Click(object sender, EventArgs e)
        {
            AppendCondition(" OR ", " in ", "('','','')");
        }
        #endregion

        private void cbbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDataType.SelectedValue == null) return;
            var splitType = cbbDataType.SelectedValue.ToString();
            if ("1".Equals(splitType))
            {
                lblTable.Visible = true;
                cbbTableName.Visible = true;
                ckbGetTableList.Visible = true;
                lblSqlWHere.Text = "where条件";
            }
            else
            {
                lblTable.Visible = false;
                cbbTableName.Visible = false;
                ckbGetTableList.Visible = false;
                lblSqlWHere.Text = "自定义SQL";
            }
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            tsbConnect.PerformClick();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
    }
}
