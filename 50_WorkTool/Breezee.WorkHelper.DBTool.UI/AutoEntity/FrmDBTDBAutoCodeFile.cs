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
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.IOC;
using Breezee.Core.Interface;
using Breezee.Core.Tool;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 自动生成Java文件
    /// </summary>
    public partial class FrmDBTDBAutoCodeFile : BaseForm
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
        private IDataAccess _dataAccess;
        private IDBDefaultValue _IDBDefaultValue;
        private DataTable _dtDefault = null;
        DBSqlEntity sqlEntity;

        string _TableFirstUpper = "";
        string _TableFirstLower = "";
        string _ColumnSortInterge = "";

        DataSet _dsExcel;
        BindingSource _bsFileList;
        #endregion

        #region 构造函数
        public FrmDBTDBAutoCodeFile()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();          

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            #endregion

            //加载用户偏好值
            txbSavePath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.AutoCode_Path, "").Value;
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tsbAutoSQL.Enabled = false;

            //以下为测试使用
            //txbEntityName.Text= "BaseConfig";
            //txbEntityNameCN.Text = "基础配置";
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion
        private void cbbConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbGetTableList_CheckedChanged(null,null);
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
        private async void tsbImport_Click(object sender, EventArgs e)
        {
            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            string sTableName = cbbTableName.Text.Trim();
            if (_dbServer == null || sTableName.IsNullOrEmpty())
            {
                return;
            }

            _dataAccess = AutoSQLExecutor.Common.AutoSQLExecutors.Connect(_dbServer);
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

                //给实体账值
                txbEntityNameCN.Text = dr[DBTableEntity.SqlString.NameCN].ToString();
            }
            dtTable.TableName = _strTableName;
            
            if (!string.IsNullOrEmpty(txbRemoveTablePre.Text.Trim()))
            {
                txbEntityName.Text = cbbTableName.Text.Trim().ToUpper().Replace(txbRemoveTablePre.Text.Trim().ToUpper(), "").FirstLetterUpper();
            }
            else
            {
                txbEntityName.Text = cbbTableName.Text.Trim().FirstLetterUpper();
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
            //表名、列名大小写的列名
            _ColumnSortInterge = DBColumnEntity.SqlString.SortNum + "_INT";

            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim(), sSchema);
            DataTable dtColsNew = dtCols.Copy();
            dtColsNew.Columns.AddRange(new DataColumn[] {
            new DataColumn(_ColumnSortInterge,typeof(Int32)),
            });
            foreach (DataRow dr in dtColsNew.Rows)
            {
                string sTableName = dr[DBColumnEntity.SqlString.TableName].ToString();//表名
                string sColName = dr[DBColumnEntity.SqlString.Name].ToString();//列名
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
            txbEntityName.Focus();
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
            DataTable dtColumnList = dgvColList.GetBindingTable();
            //DataTable dtColumnAll;
            #endregion

            try
            {
                _dicString = new Dictionary<string, string>();
                //系统变量中固定值处理
                _dicString[AutoImportModuleString.AutoFileSysParam.EntName] = param["EntityName"];
                _dicString[AutoImportModuleString.AutoFileSysParam.EntNameCn] = param["EntityNameCn"];
                _dicString[AutoImportModuleString.AutoFileSysParam.EntNameLcc] = param["EntityName"].Substring(0, 1).ToLower() + param["EntityName"].Substring(1);
                _dicString[AutoImportModuleString.AutoFileSysParam.DateNow] = DateTime.Now.ToString("yyyy-MM-dd");

                //自定义变量中固定值的处理
                DataTable dtMyDefine = dgvMyDefine.GetBindingTable();
                AutoFileUtil.DealMyFixParam(_dicString, dtMyDefine);
                DataTable dtSysParam = dgvSysParam.GetBindingTable();

                if (dtColumnList == null || dtColumnList.Rows.Count == 0)
                {
                    if (ShowYesNo("目前没有查询数据库的列数据，确定继续生成代码？") == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    dtColumnList.AcceptChanges();

                    StringBuilder sbAllCol = new StringBuilder();
                    StringBuilder sbQueryIn = new StringBuilder();
                    StringBuilder sbQueryOut = new StringBuilder();
                    StringBuilder sbSaveIn = new StringBuilder();
                    StringBuilder sbEntity = new StringBuilder();
                    StringBuilder sbMap = new StringBuilder();
                    //系统变量中动态列的处理
                    _dicString[AutoImportModuleString.AutoFileSysParam.TableDbName] = cbbTableName.Text.Trim();//得到表名
                    DataTable dtConvert = dgvTypeConvert.GetBindingTable();

                    foreach (DataRow dr in dtColumnList.Rows)
                    {
                        sbAllCol.Append(dr[DBColumnEntity.SqlString.Name].ToString() + ",");

                        string sColApi;
                        //查询入参的API说明：过滤选中的项
                        DataRow[] drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColQueryIn + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                        {
                            //替换列变量
                            sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);
                            if ("1".Equals(dr[_sGridColumnQueryInParam].ToString()))
                            {
                                sbQueryIn.Append(sColApi);
                                sbQueryIn.Append(Environment.NewLine);
                            }
                        }
                        //查询出参的API说明：过滤选中的项
                        drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColQueryOut + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                        {
                            //替换列变量
                            sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);
                            if ("1".Equals(dr[_sGridColumnQueryOutParam].ToString()))
                            {
                                sbQueryOut.Append(sColApi);
                                sbQueryOut.Append(Environment.NewLine);
                            }
                        }
                        //保存入参的API说明：过滤选中的项
                        drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColSaveIn + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                        {
                            //替换列变量
                            sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);

                            if ("1".Equals(dr[_sGridColumnSaveInParam].ToString()))
                            {
                                sbSaveIn.Append(sColApi);
                                sbSaveIn.Append(Environment.NewLine);
                            }
                        }

                        //MyBatis的实体定义：这里有类型替换
                        drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColEntNote + "'");
                        DataRow[] drArrType = dtConvert.Select(AutoImportModuleString.ColumnNameTypeConvert.DbType + "='" + dr[DBColumnEntity.SqlString.DataType].ToString() + "'");

                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                        {
                            //替换列变量
                            sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);
                            //替换类型转换
                            if (drArrType.Length > 0)
                            {
                                sColApi = sColApi.Replace(AutoImportModuleString.ColumnNameSysParam.ChangeType, drArrType[0][AutoImportModuleString.ColumnNameTypeConvert.DevLangType].ToString());
                            }
                            //主键字符替换
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
                        drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColMapNode + "'");
                        if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                        {
                            sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);
                            //主键字符替换
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

                    //得到所有拼接的动态字符
                    //_dicString[AutoImportModuleString.AutoFileSysParam.ColDbNameAll] = sbAllCol.ToString().Substring(0, sbAllCol.ToString().Length-1);
                    _dicString[AutoImportModuleString.AutoFileSysParam.ColQueryIn] = sbQueryIn.ToString();
                    _dicString[AutoImportModuleString.AutoFileSysParam.ColQueryOut] = sbQueryOut.ToString();
                    _dicString[AutoImportModuleString.AutoFileSysParam.ColSaveIn] = sbSaveIn.ToString();
                    _dicString[AutoImportModuleString.AutoFileSysParam.ColEntNote] = sbEntity.ToString();
                    _dicString[AutoImportModuleString.AutoFileSysParam.ColMapNode] = sbMap.ToString();
                }

                //自定义变量中动态值的处理
                DataTable dtMyDefineDynamic = dgvMyDefine.GetBindingTable();
                AutoFileUtil.FixMyDynamicParam(_dicString, dtMyDefineDynamic, dtColumnList, dtSysParam,true);

                //循环文件，处理文件名和空间名
                foreach (DataRow drFile in _dtFileSelect.Rows)
                {
                    //
                    string sFilePath = Path.Combine(param["SavePath"], "AutoCodeFiles", drFile[AutoImportModuleString.ColumnNameClass.Path].ToString());
                    sFilePath = ReplaceParamKeyByValue(sFilePath);//FilePath中也可能包含变量，所以这里也要替换

                    string sPackName = drFile[AutoImportModuleString.ColumnNameClass.PackName].ToString();
                    sPackName = ReplaceParamKeyByValue(sPackName);//PackName中也可能包含变量，所以这里也要替换
                    //得到后终值
                    drFile[AutoImportModuleString.ColumnNameClass.FinalPath] = sFilePath;
                    drFile[AutoImportModuleString.ColumnNameClass.FinalPackName] = sPackName;
                    //将包路径或空间名加入全局变量中
                    string sPackNameKey = drFile[AutoImportModuleString.ColumnNameClass.PackNameKey].ToString();
                    if (!string.IsNullOrEmpty(sPackNameKey) && !string.IsNullOrEmpty(sPackName))
                    {
                        _dicString[sPackNameKey] = sPackName;
                    }
                }

                rtbResult.Clear();
                rtbResult.AppendText("最终得到的参数如下：\n");
                foreach (string sKey in _dicString.Keys)
                {
                    rtbResult.AppendText(sKey + "：" + _dicString[sKey] + "\n");
                }
                rtbResult.Select(0, 0); //返回到第一行
                tabControl1.SelectedTab = tpAutoSQL;
                //循环文件处理
                foreach (DataRow drFile in _dtFileSelect.Rows)
                {
                    string sFileContent = drFile[AutoImportModuleString.ColumnNameClass.FileContent].ToString();
                    string sFilePath = drFile[AutoImportModuleString.ColumnNameClass.FinalPath].ToString();
                    string sPackName = drFile[AutoImportModuleString.ColumnNameClass.FinalPackName].ToString();
                    //文件名：前缀+实体名+后缀
                    string sFileName = drFile[AutoImportModuleString.ColumnNameClass.BeginString].ToString() + param["EntityName"] + drFile[AutoImportModuleString.ColumnNameClass.EndString].ToString();

                    //替换包路径
                    sFileContent = sFileContent.Replace(AutoImportModuleString.AutoFileSysParam.PackPath, sPackName);
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
                ShowInfo(_strAutoSqlSuccess);
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
                txbEntityName.Text = cbbTableName.Text.Trim().ToUpper().Replace(txbRemoveTablePre.Text.Trim().ToUpper(), "").FirstLetterUpper();
            }
            else
            {
                txbEntityName.Text = cbbTableName.Text.Trim().FirstLetterUpper();
            }
        }

        private void btnImportPath_Click(object sender, EventArgs e)
        {
            _dicString.Clear();
            _dicString[AutoImportModuleString.SheetName.CodeModule] = AutoImportModuleString.SheetName.CodeModule;
            _dicString[AutoImportModuleString.SheetName.MyParam] = AutoImportModuleString.SheetName.MyParam;
            _dicString[AutoImportModuleString.SheetName.DbTypeConvert] = AutoImportModuleString.SheetName.DbTypeConvert;
            _dicString[AutoImportModuleString.SheetName.SysParam] = AutoImportModuleString.SheetName.SysParam;
            _dsExcel = ExportHelper.GetExcelDataSet(_dicString);//导入模板
            if (_dsExcel != null)
            {
                _bsFileList = new BindingSource();
                DataTable dt = _dsExcel.Tables[AutoImportModuleString.SheetName.CodeModule];
                if (dt == null)
                {
                    MsgHelper.ShowInfo("导入模板错误，请重新导入！");
                    return;
                }
                DataColumn dcSelected = new DataColumn(_sGridIsSelect);
                dcSelected.DefaultValue = "1";
                dt.Columns.Add(dcSelected);

                //增加两列表示最终值
                dcSelected = new DataColumn(AutoImportModuleString.ColumnNameClass.FinalPath);
                dt.Columns.Add(dcSelected);
                dcSelected = new DataColumn(AutoImportModuleString.ColumnNameClass.FinalPackName);
                dt.Columns.Add(dcSelected);
                //绑定数据源
                _bsFileList.DataSource = dt;

                //查询结果
                FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(_sGridIsSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.Path).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.PackName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.PackNameKey).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.BeginString).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.EndString).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameClass.FileContent).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()

                );
                dgvModule.Tag = fdc.GetGridTagString();
                dgvModule.BindDataGridView(dt, true);

                //绑定自定义变量网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameMyParam.ParamName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameMyParam.ParamContent).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameMyParam.ConcatString).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameMyParam.ParamValueInfo).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvMyDefine.Tag = fdc.GetGridTagString();
                dgvMyDefine.BindDataGridView(_dsExcel.Tables[AutoImportModuleString.SheetName.MyParam], true);
                //绑定系统变量网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameSysParam.ParamName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameSysParam.ParamContent).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameSysParam.ParamValueInfo).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameSysParam.Example).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvSysParam.Tag = fdc.GetGridTagString();
                dgvSysParam.BindDataGridView(_dsExcel.Tables[AutoImportModuleString.SheetName.SysParam], true);
                //绑定类型转换网格
                fdc = new FlexGridColumnDefinition();
                fdc.AddColumn(
                    FlexGridColumn.NewRowNoCol(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameTypeConvert.DbType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                    new FlexGridColumn.Builder().Name(AutoImportModuleString.ColumnNameTypeConvert.DevLangType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                );
                dgvTypeConvert.Tag = fdc.GetGridTagString();
                dgvTypeConvert.BindDataGridView(_dsExcel.Tables[AutoImportModuleString.SheetName.DbTypeConvert], true);

                ShowInfo("导入成功！");
                tsbAutoSQL.Enabled = true;
            }
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbSavePath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.AutoCode_Path, txbSavePath.Text, "【代码生成】保存路径");
                WinFormContext.UserLoveSettings.Save();
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
                rtbConString.Tag = dgvModule.Columns[e.ColumnIndex].Name;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dtResult = dgvModule.GetBindingTable();
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                ShowErr("没有要导出的记录！", "提示");
                return;
            }
            //导出Excel
            ExportHelper.ExportExcel(dtResult, "模板_生成代码文件" + DateTime.Now.ToString("yyyyMMddHHmmss"), true);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataRow dr = dgvModule.GetCurrentRow();
            if (rtbConString.Tag != null)
            {
                string sColName = rtbConString.Tag as string;
                dr[sColName] = rtbConString.Text;
            }
            
        }
    }

    
}