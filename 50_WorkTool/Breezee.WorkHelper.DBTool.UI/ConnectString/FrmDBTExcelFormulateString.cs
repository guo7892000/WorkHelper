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
using Setting = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using System.IO;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Collections;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：生成新增数据的Excel公式字符
    /// 创建作者：黄国辉
    /// 创建日期：2023-6-9
    /// 功能说明：生成新增数据的Excel公式字符
    /// </summary>
    public partial class FrmDBTExcelFormulateString : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";

        private readonly string _sGridColumnCondition = "IsCondition";
        private readonly string _sGridColumnSelect = "IsSelect";
        private readonly string _sGridColumnDynamic = "IsDynamic";
        //
        private readonly string _sGridColumnGlobalValue = "GlobalValue";
        private readonly string _sGridColumnGlobalValueInsert = "GlobalValueInsertUsed";
        private readonly string _sGridColumnGlobalValueUpdate = "GlobalValueUpdateUsed";
        private readonly string _sGridColumnIsUpdateCondition = "GlobalIsUpdateCondtion";
        private readonly string _sGridColumnIsDeleteCondition = "GlobalIsDeleteCondtion";
        private readonly string _sGridColumnIsQueryCondition = "GlobalIsQueryCondtion";
        private readonly string _sGridColumnDefault = "DefaultVlue2";
        private bool _allSelect = false;//默认全选，这里取反
        private bool _allCondition = true;//默认全不选，这里取反
        private bool _allDynamic = false;//默认全选，这里取反
        //常量
        private static string strTableAlias = "A"; //查询和修改中的表别名
        private static string strTableAliasAndDot = "";
        private static readonly string _strUpdateCtrolColumnCode = "UPDATE_CONTROL_ID";
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        private string _strImportSuccess = "导入成功！可点“生成”按钮得到结果。";
        //数据集
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
        private IDBDefaultValue _IDBDefaultValue;
        private DataTable _dtDefault = null;
        DBSqlEntity sqlEntity;
        #endregion

        #region 构造函数
        public FrmDBTExcelFormulateString()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();

            #region 绑定下拉框
            _dicString.Add("1", "查询数据库");
            _dicString.Add("2", "自定义");
            cmbType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //
            _dicString.Clear();
            _dicString.Add("1", "&字符");
            _dicString.Add("2", "CONCATENATE函数");
            cbbConnString.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            #endregion

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            #endregion

            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //加载用户偏好值
            cmbType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelFomulate_Type, "1").Value;
            txbTableName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelFomulate_TableName, "").Value;
            nudColumnNum.Value = int.Parse(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelFomulate_ColumnNum, "1").Value);
            //
            lblInfo.Text = "请将生成的字符复制到Excel数据模板中最后列的右边前两行。注：列名要跟表的列名一致！";
            tsbAutoSQL.ToolTipText = "生成新增表数据的Excel数据模板公式字符";
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

            _dataAccess = AutoSQLExecutors.Connect(_dbServer);
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
                dr[DBTableEntity.SqlString.NameCN] = drArr[0][DBTableEntity.SqlString.NameCN].ToString();
                dr[DBTableEntity.SqlString.Schema] = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;

            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
            _dtDefault = _IDBDefaultValue.QueryDefaultValue(_dicQuery).SafeGetDictionaryTable(); //获取默认值、排除列配置信息
            //设置Tag
            SetTableTag(dtTable);
            SetColTag(dtTable);
            
            //导入成功后处理
            tsbAutoSQL.Enabled = true;
            tabControl1.SelectedTab = tpImport;
            //导入成功提示
            ShowInfo(_strImportSuccess);
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
            dgvTableList.BindDataGridView(dt, true);
        }

        #region 设置Tag方法
        private void SetColTag(DataTable dtTable)
        {
            string sSchema = "";
            if (dtTable.Rows.Count > 0)
            {
                sSchema = dtTable.Rows[0][DBTableEntity.SqlString.Schema].ToString();
            }
            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim(), sSchema);
            //增加条件列
            DataColumn dcCondiction = new DataColumn(_sGridColumnCondition);
            dcCondiction.DefaultValue = "0";
            dtCols.Columns.Add(dcCondiction);
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtCols.Columns.Add(dcSelected);
            //增加MyBatis动态列
            DataColumn dcDynamic = new DataColumn(_sGridColumnDynamic);
            dcDynamic.DefaultValue = "1";
            dtCols.Columns.Add(dcDynamic);
            //全局配置值
            dcDynamic = new DataColumn(_sGridColumnGlobalValue);
            dtCols.Columns.Add(dcDynamic);
            dcDynamic = new DataColumn(_sGridColumnGlobalValueInsert);
            dcDynamic.DefaultValue = "0";
            dtCols.Columns.Add(dcDynamic);
            dcDynamic = new DataColumn(_sGridColumnGlobalValueUpdate);
            dcDynamic.DefaultValue = "0";
            dtCols.Columns.Add(dcDynamic);

            dcDynamic = new DataColumn(_sGridColumnIsUpdateCondition);
            dcDynamic.DefaultValue = "0";
            dtCols.Columns.Add(dcDynamic);
            dcDynamic = new DataColumn(_sGridColumnIsDeleteCondition);
            dcDynamic.DefaultValue = "0";
            dtCols.Columns.Add(dcDynamic);
            dcDynamic = new DataColumn(_sGridColumnIsQueryCondition);
            dcDynamic.DefaultValue = "0";
            dtCols.Columns.Add(dcDynamic);

            dcDynamic = new DataColumn(_sGridColumnDefault);//增加一个备份的默认值
            dtCols.Columns.Add(dcDynamic);


            
            dtCols.TableName = _strColName;

            foreach (DataRow dr in dtCols.Rows)
            {
                //备份默认值
                dr[_sGridColumnDefault] = dr[DBColumnEntity.SqlString.Default];
                if(_dtDefault!=null && _dtDefault.Rows.Count > 0)
                {
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
                    //过滤数据
                    string sFiter = string.Format("{0}='{1}'", DT_DBT_BD_COLUMN_DEFAULT.SqlString.COLUMN_NAME, dr[DBColumnEntity.SqlString.Name].ToString());
                    var drArr = _dtDefault.Select(sFiter);
                    if (drArr.Length > 0)
                    {
                        dr[_sGridColumnGlobalValue] = drArr[0][sDefaultColName];//使用全局配置中的默认值
                        dr[_sGridColumnGlobalValueInsert] = drArr[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_ADD];//默认值是否新增使用
                        dr[_sGridColumnGlobalValueUpdate] = drArr[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_USED_UPDATE];//默认值是否修改使用

                        dr[_sGridColumnIsUpdateCondition] = drArr[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_UPDATE];//是否修改条件
                        dr[_sGridColumnIsDeleteCondition] = drArr[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_DELETE];//是否删除条件
                        dr[_sGridColumnIsQueryCondition] = drArr[0][DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_CONDITION_QUERY];//是否查询条件
                    }
                }
            }
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                //new FlexGridColumn.Builder().Name(_sGridColumnCondition).Caption("条件").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnDynamic).Caption("加引号").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(80).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Default).Caption("固定值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NameCN).Caption("列名称").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Name).Caption("列编码").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataTypeFull).Caption("类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NameUpper).Caption("大驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NameLower).Caption("小驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.SortNum).Caption("排序号").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.KeyType).Caption("主键").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NotNull).Caption("非空").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataType).Caption("数据类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataLength).Caption("字符长度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataPrecision).Caption("精度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataScale).Caption("小数位数").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Comments).Caption("备注").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnGlobalValue).Caption("全局配置值").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Visible().Build(),
                FlexGridColumn.NewHideCol(DBColumnEntity.SqlString.Extra)
            );
            dgvColList.Tag = fdc.GetGridTagString();
            dgvColList.BindDataGridView(dtCols, true);
            //dgvColList.AllowUserToAddRows = true;//设置网格样式
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            if (cmbType.SelectedValue == null) return;
            string sConnString = cbbConnString.SelectedValue.ToString();

            if ("2".Equals(cmbType.SelectedValue.ToString()))
            {
                //2、直接输入表名称列数据来生成Excel公式
                string sTableName = txbTableName.Text.Trim();
                if (string.IsNullOrEmpty(sTableName))
                {
                    ShowErr("请输入表名！");
                    txbTableName.Focus();
                    return;
                }
                
                int iColumnNum = int.Parse(nudColumnNum.Value.ToString());
                StringBuilder sbHead = new StringBuilder();
                StringBuilder sbTail = new StringBuilder();
                if ("1".Equals(sConnString))
                {
                    //&拼接
                    sbHead.Append("=\"INSERT INTO " + sTableName + " (\"");
                    sbTail.Append("=$" + iColumnNum.ToExcelColumnWord() + "$1");
                }
                else
                {
                    //CONCATENATE函数拼接
                    sbHead.Append("=CONCATENATE(\"INSERT INTO " + sTableName + " (\",");
                    sbTail.Append("=CONCATENATE($" + iColumnNum.ToExcelColumnWord() + "$1");
                }
                
                
                for (int i = 0; i < iColumnNum; i++)
                {
                    string sExcelCol = i.ToExcelColumnWord();
                    bool isLast = i == iColumnNum - 1;
                    if ("1".Equals(sConnString))
                    {
                        //&拼接
                        string sColChar = string.Format("&{0}1&\",\"", sExcelCol);//非最后的列字符
                        string sColCharLast = string.Format("&{0}1&\") VALUES (\"", sExcelCol);//最后的列字符
                                                                                               // 数据拼接
                        string sDataChar = string.Format("&\"'\"&{0}2&\"'\"", sExcelCol);
                        string sDouhao = string.Format("&\"{0}\"", ",");
                        string sLastKuohao = string.Format("&\"{0}\"", ")");
                        //拼接公式
                        sbHead.Append(isLast ? sColCharLast : sColChar);
                        sbTail.Append(isLast ? sDataChar + sLastKuohao : sDataChar + sDouhao);//加引号
                    }
                    else
                    {
                        //CONCATENATE函数拼接：列名拼接
                        string sColChar = string.Format("{0}1,\",\",", sExcelCol);//非最后的列字符
                        string sColCharLast = string.Format("{0}1,\") VALUES (\")", sExcelCol);//最后的列字符
                                                                                               // 数据拼接
                        string sDataChar = string.Format(",\"'\",{0}2,\"'\"", sExcelCol);
                        string sDouhao = string.Format(",\"{0}\"", ",");
                        string sLastKuohao = string.Format(",\"{0}\"", ")");
                        //拼接公式
                        sbHead.Append(isLast ? sColCharLast : sColChar);
                        sbTail.Append(isLast ? sDataChar + sLastKuohao : sDataChar + sDouhao);//加引号
                    }
                }

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelFomulate_Type, cmbType.SelectedValue.ToString(), "【生成数据Excel公式】的列名来源");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelFomulate_TableName, txbTableName.Text.Trim(), "【生成数据Excel公式】的表名");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelFomulate_ColumnNum, nudColumnNum.Value.ToString(), "【生成数据Excel公式】的表列数量");
                WinFormContext.UserLoveSettings.Save();

                rtbResult.Clear();
                rtbResult.AppendText(sbHead.ToString());
                rtbResult.AppendText(System.Environment.NewLine);
                rtbResult.AppendText(sbTail.ToString());
                Clipboard.SetData(DataFormats.UnicodeText, rtbResult.Text);
                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                ShowInfo(_strAutoSqlSuccess);
                return;
            }

            //1、读取数据库生成Excel公式
            txbTableName.Focus();
            //取得数据源
            DataTable dtMain = dgvTableList.GetBindingTable();
            DataTable dtSec = dgvColList.GetBindingTable();
            if (dtMain == null) return;
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

            #region 得到有效的数据
            DataTable dtColumnSelect = dtSec.Clone();
            DataTable dtColumnCondition = dtSec.Clone();
            DataTable dtColumnDynamic = dtSec.Clone();

            //得到【选择】选中的列
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);
            foreach (DataRow dr in dtSec.Select(sFiter, DBColumnEntity.SqlString.SortNum + " ASC"))
            {
                dtColumnSelect.ImportRow(dr);
            }

            //得到【条件】选中的列
            //sFiter = string.Format("{0}='1'", _sGridColumnCondition);
            //foreach (DataRow dr in dtSec.Select(sFiter))
            //{
            //    dtColumnCondition.ImportRow(dr);//对非修改，不是排除列就导入
            //}

            //得到【MyBatis动态列】选中的列：这里作为是否加引号
            sFiter = string.Format("{0}='1'", _sGridColumnDynamic);
            foreach (DataRow dr in dtSec.Select(sFiter))
            {
                dtColumnDynamic.ImportRow(dr);
            }
            #endregion

            //得到表名
            string strDataTableName = dtMain.Rows[0][DBTableEntity.SqlString.Name].ToString().Trim();
            StringBuilder sbBegin = new StringBuilder();
            StringBuilder sbEnd = new StringBuilder();
            sbBegin.Append("INSERT INTO " + strDataTableName + " (");
            if ("1".Equals(sConnString))
            {
                sbEnd.Append("=$" + dtColumnSelect.Rows.Count.ToExcelColumnWord() + "$1");
            }
            else
            {
                /** 拖动公式时使其中部分内容保持不变方法：
                 * 默认情况下excel是相对引用，因此当我们往下拖鼠标时，在A2变成A3的同时，E2:E4也变成 E3：E5，这不是我们想要的。
                 * 为了解决上述问题，需要用到绝对引用。$表示绝对引用，也就是说在拖鼠标时，它保持不变。我们将公式改成=COUNTIF($E$2:$E$4,A2)，也就是绝对引用的形式
                 */
                sbEnd.Append("=CONCATENATE($" + dtColumnSelect.Rows.Count.ToExcelColumnWord() + "$1");
            }
                
            for (int j = 0; j < dtColumnSelect.Rows.Count; j++)//针对列清单循环：因为只有一个表，所以第二个网格是该表的全部列
            {
                DataRow drCol = dtColumnSelect.Rows[j];
                string strColCode = drCol[DBColumnEntity.SqlString.Name].ToString().Trim().ToUpper();
                string strColType = drCol[DBColumnEntity.SqlString.DataType].ToString().Trim().ToUpper();
                string strColFixedValue = drCol[DBColumnEntity.SqlString.Default].ToString().Trim();//固定值
                sbBegin.Append(j == dtColumnSelect.Rows.Count - 1 ? strColCode + ") VALUES (" : strColCode + ",");
                bool isLast = j == dtColumnSelect.Rows.Count - 1;
                string sExcelCol = j.ToExcelColumnWord();

                string sFixedValue = string.Format(",\"{0}\"", strColFixedValue);
                string sDataChar = string.Format(",\"'\",{0}2,\"'\"", sExcelCol);
                string sDataCharNo = string.Format(",{0}2", sExcelCol);
                string sDouhao = string.Format(",\"{0}\"", ",");
                string sLastKuohao = string.Format(",\"{0}\"", ")");
                if ("1".Equals(sConnString))
                {
                    //&拼接
                    sFixedValue = string.Format("&\"{0}\"", strColFixedValue);
                    sDataChar = string.Format("&\"'\"&{0}2&\"'\"", sExcelCol);
                    sDataCharNo = string.Format("&{0}2", sExcelCol);
                    sDouhao = string.Format("&\"{0}\"", ",");
                    sLastKuohao = string.Format("&\"{0}\"", ")");
                }                    

                if (!ckbCancelDefault.Checked && !string.IsNullOrEmpty(strColFixedValue))
                {
                    //不忽略默认值，且默认值非空，这里直接连接默认值，且不加引号（如要加引号，则在默认值中增加）
                    sbEnd.Append(isLast ? sFixedValue + sLastKuohao : sFixedValue + sDouhao);
                }
                else
                {
                    if ("1".Equals(drCol[_sGridColumnDynamic].ToString().Trim()))
                    {
                        sbEnd.Append(isLast ? sDataChar + sLastKuohao : sDataChar + sDouhao);//加引号
                    }
                    else
                    {
                        sbEnd.Append(isLast ? sDataCharNo + sLastKuohao : sDataCharNo + sDouhao); //不加引号
                    }
                }
            }
            rtbResult.Clear();
            rtbResult.AppendText(sbBegin.ToString());
            rtbResult.AppendText(System.Environment.NewLine);
            rtbResult.AppendText(sbEnd.ToString());
            Clipboard.SetData(DataFormats.UnicodeText, rtbResult.Text);
            tabControl1.SelectedTab = tpAutoSQL;
            //生成SQL成功后提示
            ShowInfo(_strAutoSqlSuccess);
        }

        private static string FirstLetterUpper(string strColCode,bool isFirstWorldUpper=true)
        {
            strColCode = strColCode.ToLower();
            string[] firstUpper = strColCode.Split('_');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var s in firstUpper)
            {
                if(i==0 && !isFirstWorldUpper)
                {
                    sb.Append(s);
                }
                else
                {
                    sb.Append(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s));
                }
                i++;
            }
            strColCode = sb.ToString();
            return strColCode;
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
                cbbTableName.BindDropDownList(uC_DbConnection1.UserTableList.Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", false);
                //查找自动完成数据源
                cbbTableName.AutoCompleteCustomSource.AddRange(uC_DbConnection1.UserTableList.AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableName.DataSource = null;
            }
        }
        #endregion
    

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbType.SelectedValue == null) return;
            if("1".Equals(cmbType.SelectedValue.ToString()))
            {
                //查询数据库
                uC_DbConnection1.Visible = true;
                grbOrcNet.Visible = true;
                tsbImport.Visible = true;

                lblTable.Visible = false;
                lblColumnNum.Visible = false;
                txbTableName.Visible = false;
                nudColumnNum.Visible = false;
                if (!tabControl1.TabPages.Contains(tpImport))
                {
                    tabControl1.TabPages.Insert(0,tpImport);
                }
            }
            else
            {
                //Excel数据模板
                uC_DbConnection1.Visible = false;
                grbOrcNet.Visible = false;
                tsbImport.Visible = false;

                lblTable.Visible = true;
                lblColumnNum.Visible = true;
                txbTableName.Visible = true;
                nudColumnNum.Visible = true;
                if (tabControl1.TabPages.Contains(tpImport))
                {
                    tabControl1.TabPages.Remove(tpImport);
                }
            }
        }

        private void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableName.Text.Trim())) return;
            tsbImport.PerformClick();
        }

        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvColList.Columns[_sGridColumnSelect].Index)
            {
                dgvColList.AllChecked(_sGridColumnSelect,ref _allSelect);
            }
            else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnCondition].Index)
            {
                dgvColList.AllChecked(_sGridColumnCondition, ref _allCondition);
            }
            else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnDynamic].Index)
            {
                dgvColList.AllChecked(_sGridColumnDynamic, ref _allDynamic);
            }
            
        }

    }


}
