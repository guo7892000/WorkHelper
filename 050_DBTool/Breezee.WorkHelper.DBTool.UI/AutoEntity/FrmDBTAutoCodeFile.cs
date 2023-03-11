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
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 自动生成Java文件
    /// </summary>
    public partial class FrmDBTAutoCodeFile : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";
        private readonly string _sGridIsSelect = "IS_SELECT";

        private readonly string _sGridColumnQueryOutParam = "IsQueryOutParam";
        private readonly string _sGridColumnQueryInParam = "IsQueryInParam";
        private readonly string _sGridColumnSaveInParam = "IsSaveInParam";
        private bool _allQueryIn = true;//默认全不选，这里取反
        private bool _allQueryOut = false;//默认全选，这里取反
        private bool _allSaveIn = false;//默认全选，这里取反
        private bool _allSaveSelect = false;//默认全选，这里取反

        private DataTable _dtFile;
        private DataTable _dtFileSelect;
        private DataTable _dtQueryIn;
        private DataTable _dtQueryOut;
        private DataTable _dtSaveIn;
        //常量
        private static string strTableAlias = "A"; //查询和修改中的表别名
        private static string strTableAliasAndDot = "";
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
        DBSqlEntity sqlEntity;

        string _TableFirstUpper = "";
        string _TableFirstLower = "";
        string _ColumnFirstUpper = "";
        string _ColumnFirstLower = "";
        string _ColumnSortInterge = "";
        IDictionary<string, string> _keyParamColRel = new Dictionary<string, string>();

        DataSet _dsExcel;
        BindingSource _bsFileList;
        #endregion

        #region 构造函数
        public FrmDBTAutoCodeFile()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();

            //_dicString.Add("1", "Mybatis实体");
            //_dicString.Add("2", "自定义");
            //cbbModule.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);            

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            #endregion

            txbSavePath.Text = "d:/javaAuto";
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            tsbAutoSQL.Enabled = false;
            //初始化导入模板中的动态系统变量与表列关系
            _keyParamColRel["#COL_NAME#"] = _ColumnFirstUpper;
            _keyParamColRel["#COL_NAME_FL#"] = _ColumnFirstLower;
            _keyParamColRel["#COL_NAME_CN#"] = DBColumnEntity.SqlString.NameCN;
            _keyParamColRel["#COL_DB_NAME#"] = DBColumnEntity.SqlString.Name;
        }

        private void cbbConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckbGetTableList.Checked)
            {
                ckbGetTableList.Checked = false;
            }
        }
        #endregion

        #region 数据库类型下拉框变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {
            switch (e.SelectDBType)
            {
                case DataBaseType.PostgreSql:
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.SQLite:
                case DataBaseType.SqlServer:
                case DataBaseType.MySql:
                    break;
                default:
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
                dr[DBTableEntity.SqlString.Schema] = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;
            
            if (!string.IsNullOrEmpty(txbRemoveTablePre.Text.Trim()))
            {
                txbEntityName.Text = FirstLetterUpper(cbbTableName.Text.Trim().ToUpper().Replace(txbRemoveTablePre.Text.Trim().ToUpper(), ""));
            }
            else
            {
                txbEntityName.Text = FirstLetterUpper(cbbTableName.Text.Trim());
            }
            
            //设置Tag
            SetTableTag(dtTable);
            SetColTag(dtTable);
            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
            _dtDefault = _IDBDefaultValue.QueryDefaultValue(_dicQuery).SafeGetDictionaryTable(); //获取默认值、排除列配置信息
            //导入成功后处理
            tabControl1.SelectedTab = tpImport;
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
            //表名、列名大小写的列名
            _TableFirstUpper = DBColumnEntity.SqlString.TableName + "_FU";
            _TableFirstLower = DBColumnEntity.SqlString.TableName + "_FL";
            _ColumnFirstUpper = DBColumnEntity.SqlString.Name + "_FU";
            _ColumnFirstLower = DBColumnEntity.SqlString.Name + "_FL";
            _ColumnSortInterge = DBColumnEntity.SqlString.SortNum + "_INT";

            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim(), sSchema);
            DataTable dtColsNew = dtCols.Copy();
            dtColsNew.Columns.AddRange(new DataColumn[] {
            //new DataColumn(_TableFirstUpper),
            //new DataColumn(_TableFirstLower),
            new DataColumn(_ColumnFirstUpper),
            new DataColumn(_ColumnFirstLower),
            new DataColumn(_ColumnSortInterge,typeof(Int32)),
            });
            foreach (DataRow dr in dtColsNew.Rows)
            {
                string sTableName = dr[DBColumnEntity.SqlString.TableName].ToString();//表名
                string sColName = dr[DBColumnEntity.SqlString.Name].ToString();//列名
                //dr[_TableFirstUpper] = FirstLetterUpper(sTableName);//表名首字母大写
                //dr[_TableFirstLower] = FirstLetterUpper(sTableName, false);//表名首字母大写（第一个除外）
                dr[_ColumnFirstUpper] = FirstLetterUpper(sColName);//列名首字母大写
                dr[_ColumnFirstLower] = FirstLetterUpper(sColName, false);//列名首字母大写
                dr[_ColumnSortInterge] = Int32.Parse(dr[DBColumnEntity.SqlString.SortNum].ToString());
            }
            //增加查询入参
            DataColumn dcSelected = new DataColumn(_sGridColumnQueryInParam);
            dcSelected.DefaultValue = "0";
            dtColsNew.Columns.Add(dcSelected);
            
            //增加查询出参
            DataColumn dcCondiction = new DataColumn(_sGridColumnQueryOutParam);
            dcCondiction.DefaultValue = "1";
            dtColsNew.Columns.Add(dcCondiction);

            //增加保存入参
            DataColumn dcDynamic = new DataColumn(_sGridColumnSaveInParam);
            dcDynamic.DefaultValue = "1";
            dtColsNew.Columns.Add(dcDynamic);

            dtColsNew.TableName = _strColName;


            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnQueryInParam).Caption("查询入参").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnQueryOutParam).Caption("查询出参").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnSaveInParam).Caption("保存入参").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(false).Visible().Build(),
                FlexGridColumn.NewHideCol(DBColumnEntity.SqlString.NameCN),
                FlexGridColumn.NewHideCol(DBColumnEntity.SqlString.Extra)
            );
            dgvColList.Tag = fdc.GetGridTagString();
            dgvColList.BindDataGridView(dtColsNew, true);
            //dgvColList.AllowUserToAddRows = true;//设置网格样式
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            //取得数据源
            DataTable dtFile = dgvModule.GetBindingTable();
            StringBuilder sbAllSql = new StringBuilder();
            IDictionary<string,string> param = new Dictionary<string,string>();
            param["SavePath"] = txbSavePath.Text.Trim();
            param["EntityName"] = txbEntityName.Text.Trim();
            param["EntityNameCn"] = txbEntityNameCN.Text.Trim();

            if (dtFile == null || dtFile.Rows.Count == 0)
            {
                ShowInfo("请先导入数据！");
                return;
            }

            if (string.IsNullOrEmpty(param["SavePath"]))
            {
                ShowInfo("请选择要保存的路径！");
                return;
            }
            if (string.IsNullOrEmpty(param["EntityName"]))
            {
                ShowInfo("实体编码不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(param["EntityNameCn"]))
            {
                ShowInfo("实体名称不能为空！");
                return;
            }

            string sFiter;
            //移除空行
            dtFile.DeleteNullRow();
            dtFile.AcceptChanges();//得到变更后数据
            _dtFileSelect = dtFile.Clone();
            sFiter = string.Format("{0}='1'", _sGridIsSelect);
            foreach (DataRow dr in dtFile.Select(sFiter))
            {
                _dtFileSelect.ImportRow(dr); //对非修改，不是排除列就导入
            }

            if (_dtFileSelect == null || _dtFileSelect.Rows.Count == 0)
            {
                ShowInfo("至少选中一个模板文件！");
                return;
            }

            DataTable dtMain = dgvTableList.GetBindingTable();
            DataTable dtSec = dgvColList.GetBindingTable();
            //DataTable dtColumnAll;
            #endregion

            try
            {
                //得到实体相关
                _dicString = new Dictionary<string, string>();
                _dicString[AutoFileSysParam.EntName] = param["EntityName"]; 
                _dicString[AutoFileSysParam.EntNameCn] = param["EntityNameCn"];
                _dicString[AutoFileSysParam.EntNameFirstLower] = param["EntityName"].Substring(0, 1).ToLower() + param["EntityName"].Substring(1);
                _dicString[AutoFileSysParam.DateNow] = DateTime.Now.ToString("yyyy-MM-dd");

                //自定义变量中固定值的处理
                DataTable dtMyDefine = dgvMyDefine.GetBindingTable();
                if (dtMyDefine != null && dtMyDefine.Rows.Count > 0)
                {
                    DataRow[] drArrMy = dtMyDefine.Select(ImportSheetColumnNameMyParam.ChangeType + "='1'");

                    foreach (DataRow dr in drArrMy)
                    {
                        string sContne = dr[ImportSheetColumnNameMyParam.ParamContent].ToString();
                        Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                        MatchCollection mc = regex.Matches(sContne);
                        foreach (Match item in mc)
                        {
                            if (_dicString.ContainsKey(item.Value))
                            {
                                sContne = item.Value.Replace(item.Value, _dicString[item.Value]);
                            }
                        }
                        _dicString[dr[ImportSheetColumnNameMyParam.ParamName].ToString()] = sContne;
                    }

                }

                if (dtSec == null || dtSec.Rows.Count == 0)
                {
                    if (ShowYesNo("目前没有查询数据库的列数据，确定继续生成代码？") == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    dtSec.AcceptChanges();

                    StringBuilder sbAllCol = new StringBuilder();
                    StringBuilder sbQueryIn = new StringBuilder();
                    StringBuilder sbQueryOut = new StringBuilder();
                    StringBuilder sbSaveIn = new StringBuilder();
                    StringBuilder sbEntity = new StringBuilder();
                    StringBuilder sbMap = new StringBuilder();
                    //得到表名
                    _dicString[AutoFileSysParam.TableDbName] = cbbTableName.Text.Trim();
                    DataTable dtConvert = dgvTypeConvert.GetBindingTable();
                    foreach (DataRow dr in dtSec.Rows)
                    {
                        sbAllCol.Append(dr[DBColumnEntity.SqlString.Name].ToString()+",");
                        //查询入参的API说明
                        string sColApi;
                        DataTable dtSysParam = dgvSysParam.GetBindingTable();
                        DataRow[] drArr = dtSysParam.Select(ImportSheetColumnNameSysParam.ParamName + "='"+ AutoFileSysParam.ColQueryIn + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = GetFinalString(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString(),dr);
                            if ("1".Equals(dr[_sGridColumnQueryInParam].ToString()))
                            {
                                sbQueryIn.Append(sColApi);
                                sbQueryIn.Append(Environment.NewLine);
                            }
                        }
                        //查询出参的API说明
                        drArr = dtSysParam.Select(ImportSheetColumnNameSysParam.ParamName + "='"+ AutoFileSysParam.ColQueryOut +"'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = GetFinalString(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString(), dr);
                            if ("1".Equals(dr[_sGridColumnQueryOutParam].ToString()))
                            {
                                sbQueryOut.Append(sColApi);
                                sbQueryOut.Append(Environment.NewLine);
                            }
                        }
                        //保存入参的API说明
                        drArr = dtSysParam.Select(ImportSheetColumnNameSysParam.ParamName + "='"+ AutoFileSysParam.ColSaveIn + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = GetFinalString(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString(), dr);

                            if ("1".Equals(dr[_sGridColumnSaveInParam].ToString()))
                            {
                                sbSaveIn.Append(sColApi);
                                sbSaveIn.Append(Environment.NewLine);
                            }
                        }

                        //MyBatis的实体定义：这里有类型替换
                        drArr = dtSysParam.Select(ImportSheetColumnNameSysParam.ParamName + "='"+ AutoFileSysParam.ColEntNote + "'");
                        DataRow[] drArrType = dtConvert.Select(ImportSheetColumnNameTypeConvert.DbType + "='" + dr[DBColumnEntity.SqlString.DataType].ToString() + "'");
                        
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = GetFinalString(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString(), dr);
                            if (drArrType.Length > 0)
                            {
                                sColApi = sColApi.Replace(ImportSheetColumnNameSysParam.ChangeType, drArrType[0][ImportSheetColumnNameTypeConvert.DevLangType].ToString());
                            }

                            if ("PK".Equals(dr[DBColumnEntity.SqlString.KeyType].ToString()))
                            {
                                sbEntity.Append(sColApi.Replace("@TableField", "@TableId"));
                                sbEntity.Append(Environment.NewLine);
                            }
                            else
                            {
                                sbEntity.Append(sColApi);
                                sbEntity.Append(Environment.NewLine);
                            }
                        }
                        //MyBatis的Map定义
                        drArr = dtSysParam.Select(ImportSheetColumnNameSysParam.ParamName + "='"+ AutoFileSysParam.ColMapNode + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = GetFinalString(drArr[0][ImportSheetColumnNameSysParam.ParamContent].ToString(), dr);
                            if ("PK".Equals(dr[DBColumnEntity.SqlString.KeyType].ToString()))
                            {
                                sbMap.Append(sColApi.Replace("<result", "<id"));
                                sbMap.Append(Environment.NewLine);
                            }
                            else
                            {
                                sbMap.Append(sColApi);
                                sbMap.Append(Environment.NewLine);
                            }
                        }
                    }

                    //自定义变量中动态值的处理
                    DataTable dtMyDefineDynamic = dgvMyDefine.GetBindingTable();
                    if (dtMyDefineDynamic != null && dtMyDefineDynamic.Rows.Count > 0)
                    {
                        DataRow[] drArrMy = dtMyDefineDynamic.Select(ImportSheetColumnNameMyParam.ChangeType + "='2'");
                        StringBuilder sbMy = new StringBuilder(); 
                        //循环动态值
                        foreach (DataRow drDynamic in drArrMy)
                        {
                            //取出变量内容
                            string sContne = drDynamic[ImportSheetColumnNameMyParam.ParamContent].ToString();
                            //循环列清单
                            foreach (DataRow dr in dtSec.Rows)
                            {
                                Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                                MatchCollection mc = regex.Matches(sContne);
                                //得到##匹配值
                                foreach (Match item in mc)
                                {
                                    //如果包含全局公共值，先替换
                                    if (_dicString.ContainsKey(item.Value))
                                    {
                                        sContne = sContne.Replace(item.Value, _dicString[item.Value].ToString());
                                    }
                                    else
                                    {
                                        //否则以当前行的值数据来替换
                                        sContne = sContne.Replace(item.Value, dr[_keyParamColRel[item.Value]].ToString());
                                    }
                                }
                                sbMy.AppendLine(sContne);
                                //还原为初始值
                                sContne = drDynamic[ImportSheetColumnNameMyParam.ParamContent].ToString();
                            }
                            //得到最终动态值
                            _dicString[drDynamic[ImportSheetColumnNameMyParam.ParamName].ToString()] = sbMy.ToString(); 
                        }

                    }

                    //得到所有拼接的动态字符
                    _dicString[AutoFileSysParam.ColDbNameAll] = sbAllCol.ToString();
                    _dicString[AutoFileSysParam.ColQueryIn] = sbQueryIn.ToString();
                    _dicString[AutoFileSysParam.ColQueryOut] = sbQueryOut.ToString();
                    _dicString[AutoFileSysParam.ColSaveIn] = sbSaveIn.ToString();
                    _dicString[AutoFileSysParam.ColEntNote] = sbEntity.ToString();
                    _dicString[AutoFileSysParam.ColMapNode] = sbMap.ToString();
                }

                rtbResult.AppendText("最终得到的参数如下：\n");
                foreach (string sKey in _dicString.Keys)
                {
                    rtbResult.AppendText(sKey + "：" + _dicString[sKey]+"\n");
                }
                rtbResult.Select(0, 0); //返回到第一行
                tabControl1.SelectedTab = tpAutoSQL;
                //循环文件处理
                foreach (DataRow drFile in _dtFileSelect.Rows)
                {
                    string sFileContent = drFile[ImportSheetColumnNameClass.FileContent].ToString();
                    string sFilePath = Path.Combine(param["SavePath"], drFile[ImportSheetColumnNameClass.Path].ToString());
                    sFilePath = ReplaceParamKeyByValue(sFilePath);//FilePath中也可能包含变量，所以这里也要替换
                    string sFileName = drFile[ImportSheetColumnNameClass.BeginString].ToString() + param["EntityName"] + drFile[ImportSheetColumnNameClass.EndString].ToString();
                    string sPackName = drFile[ImportSheetColumnNameClass.PackName].ToString();
                    sPackName = ReplaceParamKeyByValue(sPackName);//PackName中也可能包含变量，所以这里也要替换

                    string sPackNameKey = drFile[ImportSheetColumnNameClass.PackNameKey].ToString();
                    if (!string.IsNullOrEmpty(sPackNameKey) && !string.IsNullOrEmpty(sPackName))
                    {
                        _dicString[sPackNameKey] = sPackName;
                    }
                    //替换包路径
                    sFileContent = sFileContent.Replace(AutoFileSysParam.PackPath, sPackName);
                    if (!Directory.Exists(sFilePath))
                    {
                        Directory.CreateDirectory(sFilePath);
                    }
                    foreach (string sKey in _dicString.Keys)
                    {
                        sFileContent = sFileContent.Replace(sKey, _dicString[sKey]);
                    }

                    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(sFilePath, sFileName), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("utf-8")))
                    {
                        writer.Write(sFileContent);
                    }

                }
                //生成SQL成功后提示
                //ShowInfo(strInfo);
                lblInfo.Text = _strAutoSqlSuccess;
                
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        /// <summary>
        /// 替换字符中的键为实际值
        /// </summary>
        /// <param name="sPackName"></param>
        /// <returns></returns>
        private string ReplaceParamKeyByValue(string sPackName)
        {
            Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(sPackName);
            //得到##匹配值
            foreach (Match item in mc)
            {
                //如果包含全局公共值，先替换
                if (_dicString.ContainsKey(item.Value))
                {
                    sPackName = sPackName.Replace(item.Value, _dicString[item.Value].ToString());
                }
            }

            return sPackName;
        }

        private string GetFinalString(string sIn,DataRow dr)
        {
            string sColApi = sIn.Trim().Replace(AutoFileSysParam.ColName, dr[_ColumnFirstUpper].ToString())
              .Replace(AutoFileSysParam.ColNameFirstLower, dr[_ColumnFirstLower].ToString())
              .Replace(AutoFileSysParam.ColNameCn, dr[DBColumnEntity.SqlString.NameCN].ToString())
              .Replace(AutoFileSysParam.ColDbName, dr[DBColumnEntity.SqlString.Name].ToString());

            DataRow[] drArr = _dsExcel.Tables[ImportSheetName.DbTypeConvert].Select(ImportSheetColumnNameTypeConvert.DbType + "='"+ dr[DBColumnEntity.SqlString.DataType].ToString() + "'");
            if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][ImportSheetColumnNameTypeConvert.DevLangType].ToString()))
            {
                sColApi = sColApi.Replace(AutoFileSysParam.ColEntType, drArr[0][ImportSheetColumnNameTypeConvert.DevLangType].ToString());
            }
            else
            {
                sColApi = sColApi.Replace(AutoFileSysParam.ColEntType, "String");
            }
            return sColApi;
        }

        private static string FirstLetterUpper(string strColCode, bool isFirstWorldUpper = true)
        {
            strColCode = strColCode.ToLower();
            string[] firstUpper = strColCode.Split('_');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var s in firstUpper)
            {
                if (i == 0 && !isFirstWorldUpper)
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

        #region 选择生成IBD文件目录
        private void btnIBDSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //this.txbIBDMainPath.Text = dialog.SelectedPath;
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

        private void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableName.Text.Trim())) return;
            tsbImport.PerformClick();
        }

        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvColList.Columns[_sGridColumnQueryInParam].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnQueryInParam].Value = _allQueryIn ? "1" : "0";
                }
                _allQueryIn = !_allQueryIn;
            }
            else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnQueryOutParam].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnQueryOutParam].Value = _allQueryOut ? "1" : "0";
                }
                _allQueryOut = !_allQueryOut;
            }
            else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnSaveInParam].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnSaveInParam].Value = _allSaveIn ? "1" : "0";
                }
                _allSaveIn = !_allSaveIn;
            }
        }

        private void txbRemoveTablePre_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbRemoveTablePre.Text.Trim()))
            {
                txbEntityName.Text = FirstLetterUpper(cbbTableName.Text.Trim().ToUpper().Replace(txbRemoveTablePre.Text.Trim().ToUpper(), ""));
            }
            else
            {
                txbEntityName.Text = FirstLetterUpper(cbbTableName.Text.Trim());
            }
        }

        private void btnImportPath_Click(object sender, EventArgs e)
        {
            _dicString.Clear();
            _dicString[ImportSheetName.ClassFile] = ImportSheetName.ClassFile;
            _dicString[ImportSheetName.MyParam] = ImportSheetName.MyParam;
            _dicString[ImportSheetName.DbTypeConvert] = ImportSheetName.DbTypeConvert;
            _dicString[ImportSheetName.SysParam] = ImportSheetName.SysParam;
            _dsExcel = ExportHelper.GetExcelDataSet(_dicString);//导入模板
            if (_dsExcel != null)
            {
                _bsFileList = new BindingSource();
                DataTable dt = _dsExcel.Tables[ImportSheetName.ClassFile];
                DataColumn dcSelected = new DataColumn(_sGridIsSelect);
                dcSelected.DefaultValue = "1";
                dt.Columns.Add(dcSelected);
                _bsFileList.DataSource = dt;


                //查询结果
                FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(_sGridIsSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.Path).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.PackName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.PackNameKey).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.BeginString).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.EndString).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameClass.FileContent).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()

                );
                dgvModule.Tag = fdc.GetGridTagString();
                dgvModule.BindDataGridView(dt, true);

                //绑定自定义变量网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameMyParam.ParamName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameMyParam.ParamContent).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameMyParam.ParamValueInfo).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvMyDefine.Tag = fdc.GetGridTagString();
                dgvMyDefine.BindDataGridView(_dsExcel.Tables[ImportSheetName.MyParam], true);
                //绑定系统变量网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameSysParam.ParamName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameSysParam.ParamValueInfo).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameSysParam.Example).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvSysParam.Tag = fdc.GetGridTagString();
                dgvSysParam.BindDataGridView(_dsExcel.Tables[ImportSheetName.SysParam], true);
                //绑定类型转换网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameTypeConvert.DbType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(ImportSheetColumnNameTypeConvert.DevLangType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvTypeConvert.Tag = fdc.GetGridTagString();
                dgvTypeConvert.BindDataGridView(_dsExcel.Tables[ImportSheetName.DbTypeConvert], true);

                lblInfo.Text = "导入成功！";
                tsbAutoSQL.Enabled = true;
            }
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txbSavePath.Text = dialog.SelectedPath;
            }
        }

        private void tsbDownloadModel_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.AutoFile.Excel_Code, "模板_生成代码文件", true);
        }

        private void dgvModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex>1)
            {
                rtbConString.Clear();
                rtbConString.Text = dgvModule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void dgvModule_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvModule.Columns[_sGridIsSelect].Index)
            {
                foreach (DataGridViewRow item in dgvModule.Rows)
                {
                    item.Cells[_sGridIsSelect].Value = _allSaveSelect ? "1" : "0";
                }
                _allSaveSelect = !_allSaveSelect;
            }
        }
    }

    /// <summary>
    /// 导入模板的Sheet名称
    /// </summary>
    public class ImportSheetName
    {
        public static readonly string ClassFile = "类文件";
        public static readonly string MyParam = "自定义变量";
        public static readonly string DbTypeConvert = "类型转换";
        public static readonly string SysParam = "系统变量";
    }
    /// <summary>
    /// 导入模板的中【类文件】Sheet中的列名
    /// </summary>
    public class ImportSheetColumnNameClass
    {
        public static readonly string SortNum = "序号";
        public static readonly string Path = "路径";
        public static readonly string PackName = "包名";
        public static readonly string PackNameKey = "包名键";
        public static readonly string BeginString = "前缀";
        public static readonly string EndString = "后缀";
        public static readonly string FileContent = "文件内容";
    }
    /// <summary>
    /// 导入模板的中【自定义变量】Sheet中的列名
    /// </summary>
    public class ImportSheetColumnNameMyParam
    {
        public static readonly string ChangeType = "变化类型";
        public static readonly string ParamName = "变量名";
        public static readonly string ParamContent = "变量内容";
        public static readonly string ParamValueInfo = "变量值说明";
    }
    /// <summary>
    /// 导入模板的中【系统变量】Sheet中的列名
    /// </summary>
    public class ImportSheetColumnNameSysParam
    {
        public static readonly string ChangeType = "变化类型";
        public static readonly string ParamName = "变量名";
        public static readonly string ParamContent = "变量内容";
        public static readonly string ParamValueInfo = "变量值说明";
        public static readonly string Example = "示例";
    }
    /// <summary>
    /// 导入模板的中【类型转换】Sheet中的列名
    /// </summary>
    public class ImportSheetColumnNameTypeConvert
    {
        public static readonly string DbType = "数据库类型";
        public static readonly string DevLangType = "开发语言类型";
    }
    /// <summary>
    /// 导入模板的中【系统变量】Sheet中所有定义的系统变量键
    /// </summary>
    public class AutoFileSysParam
    {
        public static readonly string DateNow = "#DATE_NOW#";
        public static readonly string PackPath = "#PACK_PATH#";
        public static readonly string TableDbName = "#TABLE_DB_NAME#";
        public static readonly string EntName = "#ENT_NAME#";
        public static readonly string EntNameFirstLower = "#ENT_NAME_FL#";
        public static readonly string EntNameCn = "#ENT_NAME_CN#";
        public static readonly string ColName = "#COL_NAME#";
        public static readonly string ColNameFirstLower = "#COL_NAME_FL#";
        public static readonly string ColNameCn = "#COL_NAME_CN#";
        public static readonly string ColDbName = "#COL_DB_NAME#";
        public static readonly string ColEntType = "#COL_ENT_TYPE#";
        public static readonly string ColDbNameAll = "#COL_DB_NAME_ALL#";
        public static readonly string ColQueryIn = "#COL_QUERY_IN#";
        public static readonly string ColQueryOut = "#COL_QUERY_OUT#";
        public static readonly string ColSaveIn = "#COL_SAVE_IN#";
        public static readonly string ColEntNote = "#COL_ENT_NOTE#";
        public static readonly string ColMapNode = "#COL_MAP_NODE#";


    }
}