using Breezee.Framework.BaseUI;
using Breezee.Framework.Tool;
using Breezee.Global.IOC;
using Breezee.Global.Entity;
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
using Breezee.Framework.DataAccess.INF;
using Breezee.Framework.Interface;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 获取表的增删改SQL
    /// </summary>
    public partial class FrmDBTGetTableQuerySql : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";

        private readonly string _sGridColumnCondition = "IsCondition";
        private readonly string _sGridColumnSelect = "IsSelect";
        //常量
        private static string strTableAlias = "A"; //查询和修改中的表别名
        private static string strTableAliasAndDot = "";
        private static readonly string _strComma = ",";
        private static readonly string _strUpdateCtrolColumnCode = "UPDATE_CONTROL_ID";
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        private string _strImportSuccess = "导入成功！可点“生成SQL”按钮得到本次导入的变更SQL。";
        //数据集
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private ICustomDataAccess _customDataAccess = ContainerContext.Container.Resolve<ICustomDataAccess>();
        private IDataAccess _dataAccess;
        private IDBDefaultValue _IDBDefaultValue;
        private DataTable _dtDefault = null;
        #endregion

        #region 构造函数
        public FrmDBTGetTableQuerySql()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();

            #region 绑定下拉框
            _dicString.Add("1", "新增");
            _dicString.Add("2", "修改");
            _dicString.Add("3", "查询");
            _dicString.Add("4", "删除");
            UIHelper.BindTypeValueDropDownList(cmbType, _dicString.GetTextValueTable(false), false, true);
            //
            _dicString.Clear();
            _dicString.Add("1", "左右#号");
            _dicString.Add("2", "SQL参数化");
            UIHelper.BindTypeValueDropDownList(cbbParaType, _dicString.GetTextValueTable(false), false, true);
            #endregion

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            #endregion
        }
        #endregion

        #region 数据库类型下拉框变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {
            switch (e.SelectDBType)
            {
                case DataBaseType.PostgreSql:
                case DataBaseType.Oracle:
                    txbParamPre.Text = ":";
                    break;
                case DataBaseType.SQLite:
                case DataBaseType.SqlServer:
                case DataBaseType.MySql:
                    txbParamPre.Text = "@";
                    break;
                default:
                    txbParamPre.Text = "@";
                    break;
            }
        } 
        #endregion

        #region 连接数据库事件
        private void tsbImport_Click(object sender, EventArgs e)
        {
            _dbServer = uC_DbConnection1.GetDbServerInfo();
            string sTableName = cbbTableName.Text.Trim();
            if (_dbServer == null || sTableName.IsNullOrEmpty())
            {
                return;
            }

            _dataAccess = _customDataAccess.GetDataAcess(_dbServer);
            DataTable dtTable = DBTableEntity.GetTableStruct();

            DataRow[] drArr;
            string sFilter = DBTableEntity.SqlString.Name + "='" + sTableName + "'";
            if (uC_DbConnection1.UserTableList == null || uC_DbConnection1.UserTableList.Rows.Count == 0)
            {
                drArr = _dataAccess.GetSchemaTables().Select(sFilter);
            }
            else
            {
                drArr = uC_DbConnection1.UserTableList.Select(sFilter);
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
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;
            //设置Tag
            SetTableTag(dtTable);
            SetColTag();
            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
            _dtDefault = _IDBDefaultValue.QueryDefaultValue(_dicQuery).SafeGetDictionaryTable(); //获取默认值、排除列配置信息
            //导入成功后处理
            tsbAutoSQL.Enabled = true;
            tabControl1.SelectedTab = tpImport;
            SetDefaultValue(null);
            //导入成功提示
            lblInfo.Text = _strImportSuccess;
        }
        #endregion

        private void SetTableTag(DataTable dt)
        {
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Name).Caption("表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.NameCN).Caption("表名中文").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Schema).Caption("架构").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Owner).Caption("拥有者").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.DBName).Caption("所属数据库").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Comments).Caption("备注").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(false).Visible().Build()
            );
            dgvTableList.Tag = fdc.GetGridTagString();
            UIHelper.BindDataGridView(dgvTableList, dt, true);
        }

        #region 设置Tag方法
        private void SetColTag()
        {
           DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim());
            //增加条件列
            DataColumn dcCondiction = new DataColumn(_sGridColumnCondition);
            dcCondiction.DefaultValue = "0";
            dtCols.Columns.Add(dcCondiction);
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtCols.Columns.Add(dcSelected);
            dtCols.TableName = _strColName;

            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnCondition).Caption("条件").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Default).Caption("固定值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Name).Caption("列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataTypeFull).Caption("类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.SortNum).Caption("排序号").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.KeyType).Caption("主键").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NotNull).Caption("非空").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataType).Caption("数据类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataLength).Caption("字符长度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataPrecision).Caption("精度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataScale).Caption("尺度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Comments).Caption("备注").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(false).Visible().Build(),
                FlexGridColumn.NewHideCol(DBColumnEntity.SqlString.NameCN),
                FlexGridColumn.NewHideCol(DBColumnEntity.SqlString.Extra)
            );
            dgvColList.Tag = fdc.GetGridTagString();
            UIHelper.BindDataGridView(dgvColList, dtCols, true);
            //dgvColList.AllowUserToAddRows = true;//设置网格样式
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            txbTableShortName.Focus();
            //取得数据源
            DataTable dtMain = dgvTableList.GetBindingTable();
            DataTable dtSec = dgvColList.GetBindingTable();
            //移除空行
            dtMain.DeleteNullRow();
            //得到变更后数据
            dtMain.AcceptChanges();
            dtSec.AcceptChanges();
            if (dtMain.Rows.Count == 0 || dtSec.Rows.Count == 0)
            {
                ShowInfo("请先查询！");
                return;
            } 
            #endregion

            #region 生成增删改查SQL

            #region 变量
            StringBuilder sbAllSql = new StringBuilder();
            StringBuilder sbWhereSql = new StringBuilder();
            string strWhereFirst = "WHERE 1=1 \r";
            string strWhereNoFirst = "WHERE ";
            string strAnd = " AND ";
            bool _isQueryParm = cbbParaType.SelectedValue.ToString() == "2" ? true : false;//是否SQL参数化
            string sParamPre = txbParamPre.Text.Trim();
            if (_isQueryParm && string.IsNullOrEmpty(sParamPre))
            {
                ShowInfo("当选择参数化时，其后的参数化字符不能为空！");
                txbParamPre.Focus();
                return;
            }

            string strTwoType = cmbType.SelectedValue.ToString();
            SqlType sqlTypeNow = GetSqlType(); 
            string strTSName = txbTableShortName.Text.Trim().Replace(".", "").Replace("'", "");

            strTableAlias = string.IsNullOrEmpty(strTSName) ? " A" : " " + strTSName;//查询和修改中的别名:注前面的空格为必须
            strTableAliasAndDot = strTableAlias + ".";

            string sColumnSelectPre = strTableAliasAndDot;   //where条件中的列前缀
            string sColumnWherePre = strTableAliasAndDot;   //where条件中的列前缀

            if (sqlTypeNow == SqlType.Insert || sqlTypeNow == SqlType.Update)
            {
                strTableAlias = "";
                strTableAliasAndDot = "";
                sColumnSelectPre = "";
                sColumnWherePre = "";
            }
            #endregion

            #region 得到有效的数据
            DataTable dtColumnSelect = dtSec.Clone();
            DataTable dtColumnCondition = dtSec.Clone();

            //得到【选择】选中的列
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);
            foreach (DataRow dr in dtSec.Select(sFiter))
            {
                dtColumnSelect.ImportRow(dr); //对非修改，不是排除列就导入
            }

            //得到【条件】选中的列
            sFiter = string.Format("{0}='1'", _sGridColumnCondition);
            foreach (DataRow dr in dtSec.Select(sFiter))
            {
                dtColumnCondition.ImportRow(dr);//对非修改，不是排除列就导入
            }
            #endregion

            #region 得到条件
            string strNowAnd;
            if (sqlTypeNow == SqlType.Delete || sqlTypeNow == SqlType.Update)
            {
                sbWhereSql.Append(strWhereNoFirst); //删除和更新去掉1=1条件，是为了防止空条件时更新或删除全部数据；如空条件则会提示SQL不正确而导致执行失败。
                strNowAnd = "";
            }
            else
            {
                sbWhereSql.Append(strWhereFirst);
                strNowAnd = strAnd;
            }
            for (int i = 0; i < dtColumnCondition.Rows.Count; i++)
            {
                //变量声明
                string strColCode = dtColumnCondition.Rows[i][DBColumnEntity.SqlString.Name].ToString().Trim().ToUpper();
                string strColType = dtColumnCondition.Rows[i][DBColumnEntity.SqlString.DataType].ToString().Trim().ToUpper();
                string strColFixedValue = dtColumnCondition.Rows[i][DBColumnEntity.SqlString.Default].ToString().Trim();//固定值
                string strColComments = dtColumnCondition.Rows[i][DBColumnEntity.SqlString.Comments].ToString().Trim();//列说明
                string strColCodeParm = "#" + strColCode + "#"; //加上#号的列编码
                
                if (_isQueryParm)
                {
                    strColCodeParm = sParamPre + strColCode;
                }
                //列空注释的处理
                strColComments = DataBaseCommon.GetColumnComment(strColCode, strColComments);
                string sConditionColumn = strNowAnd + strTableAliasAndDot + strColCode;
                

                if (sqlTypeNow == SqlType.Query && (strColType == "DATE" || strColType == "DATETIME" || strColType.Contains("TIMESTAMP"))) //列为日期时间类型
                {
                    #region 查询的日期时间段处理
                    string strQueryWhereDateRange;
                    string strBeginDateParm = "#BEGIN_" + strColCode + "#";
                    string strEndDateParm = "#END_" + strColCode + "#";
                    if (_isQueryParm)
                    {
                        strBeginDateParm = sParamPre + "BEGIN_" + strColCode;
                        strEndDateParm = sParamPre + "END_" + strColCode;
                    }
                    
                    if (_dbServer.DatabaseType == DataBaseType.SqlServer)//SQL Server的时间范围
                    {
                        strQueryWhereDateRange = sConditionColumn + " >='" + strBeginDateParm + "' \n" + sConditionColumn + " < '" + strBeginDateParm + "' \r"; //结束日期：注要传入界面结束时间的+1天。
                    }
                    else
                    {
                        strQueryWhereDateRange = sConditionColumn + " >= TO_DATE('" + strBeginDateParm + "','YYYY-MM-DD') \n" + sConditionColumn + " < TO_DATE('" + strEndDateParm + "','YYYY-MM-DD') + 1 \r"; //结束日期：注要传入界面结束时间的+1天
                    }
                    sbWhereSql.Append(strQueryWhereDateRange); //使用范围查询条件
                    #endregion
                }
                else
                {
                    string sColParam = _isQueryParm ? strColCodeParm : "'" + strColCodeParm + "'";
                    sbWhereSql.Append(strNowAnd + DataBaseCommon.MakeConditionColumnComment(strColCode, sColParam, "", _isQueryParm, sColumnWherePre));
                }
                strNowAnd = strAnd;
            }
            #endregion

            for(int i=0;i< dtMain.Rows.Count;i++)//针对表清单循环
            {
                DataRow drTable = dtMain.Rows[i];
                #region 变量声明
                string strDataTableName = drTable[DBTableEntity.SqlString.Name].ToString().Trim(); 
                string strDataTableComment = drTable[DBTableEntity.SqlString.Comments].ToString().Trim();

                StringBuilder sbSelect = new StringBuilder();
                StringBuilder sbInsertColums = new StringBuilder();
                StringBuilder sbInsertVale = new StringBuilder();
                StringBuilder sbUpdate = new StringBuilder();

                string strOneSql = "";
                #endregion

                #region 生成SQL
                int iSelectLastNumber = dtColumnSelect.Rows.Count - 1; //选择列的最后一行数值
                for (int j = 0; j < dtColumnSelect.Rows.Count; j++)//针对列清单循环：因为只有一个表，所以第二个网格是该表的全部列
                {
                    DataRow drCol = dtColumnSelect.Rows[j];
                    #region 变量
                    string strColCode = drCol[DBColumnEntity.SqlString.Name].ToString().Trim().ToUpper();
                    string strColType = drCol[DBColumnEntity.SqlString.DataType].ToString().Trim().ToUpper();
                    string strColFixedValue = drCol[DBColumnEntity.SqlString.Default].ToString().Trim();//固定值
                    string strColComments = drCol[DBColumnEntity.SqlString.Comments].ToString().Trim();//列说明
                    string strColValue = ""; //列值 
                    string strColCodeParm = "#" + strColCode + "#"; //加上#号的列编码
                    string strNowComma = ","; //当前使用的逗号，最后一列的新增和修改是不用加逗号的，该值将会改为空值
                    #endregion

                    strColComments = DataBaseCommon.GetColumnComment(strColCode, strColComments);//默认字段注释处理

                    if (string.IsNullOrEmpty(strColFixedValue)) //没有输入固定值
                    {
                        if (_isQueryParm)
                        {
                            strColValue = sParamPre + strColCodeParm.Replace("#", "");
                        }
                        else if(strColType.Contains("CHAR")|| strColType.Contains("TEXT"))
                        {
                            strColValue = "'" + strColCodeParm + "'";
                        }
                        else
                        {
                            strColValue = strColCodeParm;
                        }

                    }
                    else //网格输入了固定值
                    {
                        strColValue = strColFixedValue;
                    }
                    
                    //生成SQL
                    if (sqlTypeNow == SqlType.Insert)
                    {
                        #region 新增(只能首尾分开拼接，没有条件)
                        sbWhereSql.Clear();
                        strTableAlias = "";
                        if (j == 0) //首行
                        {
                            if (j == iSelectLastNumber)//只有一列
                            {
                                strNowComma = "";
                                sbInsertColums.Append("INSERT INTO " + DataBaseCommon.MakeTableComment(strDataTableName + DataBaseCommon.AddRightBand(strTableAlias), strDataTableComment)
                                        + "(\n" + "\t" + strTableAliasAndDot + strColCode + strNowComma + "\n)\n");
                                sbInsertVale.Append("VALUES\n(\n" + DataBaseCommon.MakeAddValueColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm) + "\n)\n");
                            }
                            else
                            {
                                sbInsertColums.Append("INSERT INTO " + DataBaseCommon.MakeTableComment(strDataTableName + DataBaseCommon.AddRightBand(strTableAlias), strDataTableComment)
                                        + "(\n" + "\t" + strTableAliasAndDot + strColCode + strNowComma + "\n");
                                sbInsertVale.Append("VALUES\n(\n" + DataBaseCommon.MakeAddValueColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm));
                            }
                        }
                        else if (j != iSelectLastNumber) //非首行，并且非尾行
                        {
                            sbInsertColums.Append("\t" + strTableAliasAndDot + strColCode + _strComma + "\n"); 
                            sbInsertVale.Append(DataBaseCommon.MakeAddValueColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm));
                        }
                        else //尾行
                        {
                            strNowComma = "";
                            sbInsertColums.Append("\t" + strTableAliasAndDot + strColCode + "\n)\n");
                            //最后一行不用加逗号
                            sbInsertVale.Append(DataBaseCommon.MakeAddValueColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm) + ")\n");
                        }
                        #endregion
                    }
                    else if (sqlTypeNow == SqlType.Update)
                    {
                        #region 修改（直接拼接，条件为独立拼接）
                        if (j == 0) //首行
                        {
                            if (j == iSelectLastNumber)//只有一列
                            {
                                strNowComma = "";
                            }
                            sbUpdate.Append("UPDATE " + DataBaseCommon.MakeTableComment(strDataTableName + DataBaseCommon.AddRightBand(strTableAlias), strDataTableComment)
                                    + "SET " + DataBaseCommon.MakeUpdateColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm));
                        }
                        else if (j != iSelectLastNumber) //中间行
                        {
                            sbUpdate.Append(DataBaseCommon.MakeUpdateColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm));
                        }
                        else //尾行
                        {
                            strNowComma = "";
                            sbUpdate.Append(DataBaseCommon.MakeUpdateColumnComment(strNowComma, strColCode, strColValue, strColComments, strColType, _isQueryParm));
                        }
                        #endregion}
                    }
                    else if (sqlTypeNow == SqlType.Query)
                    {
                        #region 查询（直接拼接，条件为独立拼接）
                        if (j == 0) //首行
                        {
                            if (j == iSelectLastNumber)//只有一列
                            {
                                strNowComma = "";
                                sbSelect.Append("SELECT " + DataBaseCommon.MakeQueryColumnComment(strNowComma, strTableAliasAndDot + strColCode, strColComments) + "FROM "
                                + DataBaseCommon.MakeTableComment(strDataTableName + DataBaseCommon.AddRightBand(strTableAlias), strDataTableComment));
                            }
                            else
                            {
                                sbSelect.Append("SELECT " + DataBaseCommon.MakeQueryColumnComment(strNowComma, strTableAliasAndDot + strColCode, strColComments));
                            }
                        }
                        else if (j != iSelectLastNumber) //中间行
                        {
                            sbSelect.Append(DataBaseCommon.MakeQueryColumnComment(strNowComma, strTableAliasAndDot + strColCode, strColComments));
                        }
                        else //尾行
                        {
                            strNowComma = "";
                            sbSelect.Append(DataBaseCommon.MakeQueryColumnComment(strNowComma, strTableAliasAndDot + strColCode, strColComments) + "FROM "
                                + DataBaseCommon.MakeTableComment(strDataTableName + DataBaseCommon.AddRightBand(strTableAlias), strDataTableComment));
                        }
                        #endregion
                    }
                }

                if(sqlTypeNow == SqlType.Insert)
                {
                    strOneSql = sbInsertColums.ToString() + sbInsertVale.ToString();
                }
                else if (sqlTypeNow == SqlType.Delete)
                {
                    //最简单：使用表名和独立的条件拼接即可
                    strOneSql = "DELETE FROM " + strDataTableName + DataBaseCommon.AddRightBand(strTableAlias) + "\n" + sbWhereSql.ToString();
                }
                else if (sqlTypeNow == SqlType.Update)
                {
                    strOneSql = sbUpdate.ToString() + sbWhereSql.ToString();
                }
                else if (sqlTypeNow == SqlType.Query)
                {
                    strOneSql = sbSelect.ToString() + sbWhereSql.ToString();
                }
                else //SqlType.Parameter
                {
                }
                #endregion

                sbAllSql.Append(strOneSql);
                i++;//下一个表
            }
            rtbResult.Clear();
            rtbResult.AppendText(sbAllSql.ToString() + "\n");
            Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());
            tabControl1.SelectedTab = tpAutoSQL;
            //生成SQL成功后提示
            ShowInfo(_strAutoSqlSuccess);
            return;
            #endregion
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
                UIHelper.BindDropDownList(cbbTableName, uC_DbConnection1.UserTableList.Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", false);
            }
            else
            {
                cbbTableName.DataSource = null;
            }
        }
        #endregion

        private void CkbUseDefaultConfig_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultValue(null);
        }

        private void SetDefaultValue(DataTable dtSec)
        {
            if (dtSec == null)
            {
                if (!ckbUseDefaultConfig.Checked || _dbServer == null || _dtDefault == null || _dtDefault.Rows.Count == 0)
                {
                    return;
                }
                txbTableShortName.Focus();
                dtSec = dgvColList.GetBindingTable();
                if (dtSec.Rows.Count == 0)
                {
                    return;
                }
            }
            
            string sDefaultColName;
            switch (_dbServer.DatabaseType)
            {
                case DataBaseType.SqlServer:
                    sDefaultColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLSERVER;
                    break;
                case DataBaseType.Oracle:
                    sDefaultColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_ORACLE;
                    break;
                case DataBaseType.MySql:
                    sDefaultColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_MYSQL;
                    break;
                case DataBaseType.SQLite:
                    sDefaultColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_SQLITE;
                    break;
                case DataBaseType.PostgreSql:
                    sDefaultColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.DEFAULT_POSTGRESQL;
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！！");
            }

            string sConditionColName = "";
            SqlType sqlTypeNow = GetSqlType();
            switch (sqlTypeNow)
            {
                case SqlType.Insert:
                    break;
                case SqlType.Update:
                    sConditionColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_UPDATE;
                    break;
                case SqlType.Query:
                    sConditionColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_QUERY;
                    break;
                case SqlType.Delete:
                    sConditionColName = DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_DELETE;
                    break;
                case SqlType.Parameter:
                    break;
                default:
                    break;
            }

            foreach (DataRow drD in _dtDefault.Rows)//一般全局配置的数据比较少，以它循环速度快
            {
                string sColCode = drD[DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME].ToString().Trim().ToUpper();
                string sFiter = string.Format("{0}='{1}'", DBColumnEntity.SqlString.Name, sColCode);
                var drArr = dtSec.Select(sFiter);
                if (drArr.Length == 0) return;

                drArr[0][DBColumnEntity.SqlString.Default] = drD[sDefaultColName];//使用全局配置中的默认值
                if (string.IsNullOrEmpty(sConditionColName)) return;
                drArr[0][_sGridColumnCondition] = drD[sConditionColName];//选择为条件

            }
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlType sqlTypeNow = GetSqlType();
            if (sqlTypeNow == SqlType.Insert || sqlTypeNow == SqlType.Parameter) 
            {
                return;
            }

            DataTable dtSec = dgvColList.GetBindingTable();
            if (_dbServer == null || dtSec == null || dtSec.Rows.Count == 0)
            {
                return;
            }

            if(sqlTypeNow == SqlType.Update)//只针对更新，其条件要加上并发控制ID
            {
                DataRow[] drUpdateControlColumn = dtSec.Select(DBColumnEntity.SqlString.Name + "='" + _strUpdateCtrolColumnCode + "'");//得到并发ID行
                if (drUpdateControlColumn.Length == 0)
                {
                    return;
                }
                if (drUpdateControlColumn[0][_sGridColumnCondition].ToString().Equals("0"))//
                {
                    drUpdateControlColumn[0][_sGridColumnCondition] = "1";
                }
            }

            if (ckbUseDefaultConfig.Checked)
            {
                SetDefaultValue(dtSec);
            }
        }

        private SqlType GetSqlType()
        {
            SqlType sqlTypeNow;
            switch (cmbType.SelectedValue.ToString())
            {
                case "1":
                    sqlTypeNow = SqlType.Insert;
                    break;
                case "2":
                    sqlTypeNow = SqlType.Update;
                    break;
                case "3":
                    sqlTypeNow = SqlType.Query;
                    break;
                case "4":
                    sqlTypeNow = SqlType.Delete;
                    break;
                default:
                    throw new Exception("暂不支持该SqlType枚举");
            }
            return sqlTypeNow;
        }

        private void CbbParaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbParaType.SelectedValue !=null && cbbParaType.SelectedValue.ToString().Equals("2"))
            {
                txbParamPre.Visible = true;
            }
            else
            {
                txbParamPre.Visible = false;
            }
        }
    }

}
