using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.Core.Tool;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using System.IO;
using System.Text;
using Setting = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Core;
using System.Text.RegularExpressions;
using org.breezee.MyPeachNet;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：生成实体
    /// 创建作者：黄国辉
    /// 创建日期：2016-10-20
    /// 修改历史:
    ///   2023-09-07 BreezeeHui 模板增加文件后缀
    /// </summary>
	public partial class FrmDBTDBAutoEntityFile : BaseForm
	{
        #region 变量
        private DbServerInfo _dbServer;
        private TreeNode _selectedNode;
        private List<EntityInfo> list = new List<EntityInfo>();
        private IDBConfigSet _IDBConfigSet;
        private BindingSource bsTable = new BindingSource();
        //
        private IDataAccess _dataAccess;
        string sTip = "请您点击一个节点后，再按鼠标右键选择生成";
        string sGenPath = "GenPath";
        DataTable _dtGen;
        //
        MiniXmlConfig _miniXml;
        List<string> _lstCol;
        DataTable _dtSet;

        DataSet _dsExcel;
        BindingSource _bsFileList;
        #endregion

        #region 构造函数
        public FrmDBTDBAutoEntityFile()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        private void FrmAutoEntity_Load(object sender, EventArgs e)
        {
            Text = "生成实体文件";
            ctxmBuilt.Visible = false;
            //加载用户偏好值
            txtPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.AutoEntity_Path, "").Value;
            //txtPath.Text = Setting.Default.AutoEntity_Path;

            //设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = false;
            uC_DbConnection1.DBConnName_SelectedIndexChanged += DBConnNameSelectedIndexChanged;

            //表信息网格
            tvDataBaseInfo.Font = new Font("新宋体", 10f, FontStyle.Bold);
            dgvTableInfo.ReadOnly = true;
            dgvTableInfo.AutoGenerateColumns = false;
            dgvTableInfo.AllowUserToAddRows = false;
            dgvTableInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();//设置Tag
            fdc.AddColumn(
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.TableName).Name(DBColumnEntity.SqlString.TableName).Caption("表名").Type().Align().Width(100).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.NameCN).Name(DBColumnEntity.SqlString.NameCN).Caption("列中文").Type().Align().Width(100).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.Name).Name(DBColumnEntity.SqlString.Name).Caption("列名").Type().Align().Width(100).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.DataTypeFull).Name(DBColumnEntity.SqlString.DataTypeFull).Caption("类型和大小").Type().Align().Width(100).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.DataPrecision).Name(DBColumnEntity.SqlString.DataPrecision).Caption("精度").Type().Align().Width(40).Visible().Build(), 
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.DataScale).Name(DBColumnEntity.SqlString.DataScale).Caption("小数位数").Type().Align().Width(60).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.Default).Name(DBColumnEntity.SqlString.Default).Caption("默认值").Type().Align().Width(60).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.KeyType).Name(DBColumnEntity.SqlString.KeyType).Caption("主键").Type().Align().Width(40).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.NotNull).Name(DBColumnEntity.SqlString.NotNull).Caption("非空").Type().Align().Width(40).Visible().Build(),
            new FlexGridColumn.Builder().DBName(DBColumnEntity.SqlString.Extra).Name(DBColumnEntity.SqlString.Extra).Caption("备注").Type().Align().Width(200).Visible().Build()
            );
            dgvTableInfo.Tag = fdc.GetGridTagString();

            lblTableData.Text = "导入模板的【系统变量】：系统内置的参数，可直接在模板内容中引用；也可以在【自定义变量】中引用。更多说明见模板中列头的批注！";
            lblColumnInfo.Text = "导入模板的【自定义变量】：可自定义或组合系统变量，然后在模板内容中引用。【类型转换】：数据库类型转换为代码中的类型。";
            lblTemplateInfo.Text = "关键：理解并修改符合实际项目的模板，并提供给项目成员使用！";
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion
        private void DBConnNameSelectedIndexChanged(object sender, EventArgs e)
        {
            btnLinkServer.PerformClick();
        }
        #endregion

        #region 生成模板实体
        public void BuildTemplateEntity()
        {
            if (string.IsNullOrEmpty(txtPath.Text.Trim()))
            {
                ShowInfo("请您设置存放路径!");
                return;
            }

            if (string.IsNullOrEmpty(rtbEntityTemplate.Text.Trim()))
            {
                ShowInfo("请先导入模板!");
                return;
            }

            if (_selectedNode == null) return;
            DataTable dtTable = DBTableEntity.GetTableStruct();

            if (_selectedNode.Level == 1)
            {
                dtTable.Merge((DataTable)_selectedNode.Tag);
            }
            else if (_selectedNode.Level == 2)
            {
                dtTable.Merge(((DataTable[])_selectedNode.Tag)[0]);
            }

            List<EntityInfo> list = new List<EntityInfo>();
            _dicString = new Dictionary<string, string>();
            DataTable dtConvert = dgvTypeConvert.GetBindingTable();
            DataTable dtSysParam = dgvSysParam.GetBindingTable();
            //当前时间处理
            _dicString[AutoImportModuleString.AutoFileSysParam.DateNow] = DateTime.Now.ToString("yyyy-MM-dd");

            foreach (DataRow drT in dtTable.Rows)
            {
                //系统变量中固定值处理
                string sTableName = drT[DBTableEntity.SqlString.Name].ToString();
                _dicString[AutoImportModuleString.AutoFileSysParam.TableDbName] = drT[DBTableEntity.SqlString.Name].ToString();
                _dicString[AutoImportModuleString.AutoFileSysParam.EntNameCn] = drT[DBTableEntity.SqlString.NameCN].ToString();
                _dicString[AutoImportModuleString.AutoFileSysParam.EntExt] = drT[DBTableEntity.SqlString.Comments].ToString();

                if (!string.IsNullOrEmpty(txbRemoveTablePre.Text.Trim()))
                {
                    string eEntityName = sTableName.Replace(txbRemoveTablePre.Text.Trim(), "");
                    _dicString[AutoImportModuleString.AutoFileSysParam.EntName] = eEntityName.FirstLetterUpper();
                    _dicString[AutoImportModuleString.AutoFileSysParam.EntNameLcc] = eEntityName.FirstLetterUpper(false);
                }
                else
                {
                    _dicString[AutoImportModuleString.AutoFileSysParam.EntName] = sTableName.FirstLetterUpper();
                    _dicString[AutoImportModuleString.AutoFileSysParam.EntNameLcc] = sTableName.FirstLetterUpper(false);
                }

                //自定义变量中的固定值处理
                DataTable dtMyDefine = dgvMyDefine.GetBindingTable();
                AutoFileUtil.DealMyFixParam(_dicString, dtMyDefine);

                //重新查询列信息：不能从网格中取，因为存在多个生成
                DataTable dtColumn = _dataAccess.GetSqlSchemaTableColumns(sTableName, _dbServer.SchemaName);

                StringBuilder sbAllCol = new StringBuilder();
                StringBuilder sbEntity = new StringBuilder();

                //循环表的列清单
                foreach (DataRow dr in dtColumn.Rows)
                {
                    sbAllCol.Append(dr[DBColumnEntity.SqlString.Name].ToString() + ",");
                    //MyBatis的实体定义：这里有类型替换
                    DataRow[] drArr = dtSysParam.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + AutoImportModuleString.AutoFileSysParam.ColEntNote + "'");
                    DataRow[] drArrType = dtConvert.Select(AutoImportModuleString.ColumnNameTypeConvert.DbType + "='" + dr[DBColumnEntity.SqlString.DataType].ToString() + "'");

                    if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString()))
                    {
                        //替换列变量
                        string sColApi = AutoFileUtil.GetFinalString(drArr[0][AutoImportModuleString.ColumnNameSysParam.ParamContent].ToString(), dr, dtSysParam, _dicString, dtConvert);
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

                }

                //得到所有拼接的动态字符
                //_dicString[AutoImportModuleString.AutoFileSysParam.ColDbNameAll] = sbAllCol.ToString().Substring(0, sbAllCol.ToString().Length - 1);
                _dicString[AutoImportModuleString.AutoFileSysParam.ColEntNote] = sbEntity.ToString();

                //自定义变量中动态值的处理
                DataTable dtMyDefineDynamic = dgvMyDefine.GetBindingTable();
                AutoFileUtil.FixMyDynamicParam(_dicString, dtMyDefineDynamic, dtColumn, dtSysParam, false);

                //替换模板内容
                string sFileContent = rtbEntityTemplate.Text;
                string sFilePath = Path.Combine(txtPath.Text.Trim(), "AutoEntityFiles");
                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }
                //确定文件后缀名
                string sFileSuffix = txbModuleFileSuffix.Text.trim();
                if (!sFileSuffix.startsWith("."))
                {
                    sFileSuffix = "." + sFileSuffix;
                }
                //最终文件路径：包含文件
                sFilePath = Path.Combine(sFilePath, _dicString[AutoImportModuleString.AutoFileSysParam.EntNameClass] + sFileSuffix);
                foreach (string sKey in _dicString.Keys)
                {
                    sFileContent = sFileContent.Replace(sKey, _dicString[sKey]);
                }
                //输出文件
                using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(sFilePath, FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("utf-8")))
                {
                    writer.Write(sFileContent);
                }
            }
            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set("AutoEntity_Path", txtPath.Text, "实体保存路径");
            WinFormContext.UserLoveSettings.Save();
            //Setting.Default.AutoEntity_Path = txtPath.Text;
            //Setting.Default.Save();
            ShowInfo("实体成功生成！");
        }
        #endregion

        /// <summary>
        /// 导入按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            _dicString.Clear();
            _dicString[AutoImportModuleString.SheetName.EntityModule] = AutoImportModuleString.SheetName.EntityModule;
            _dicString[AutoImportModuleString.SheetName.MyParam] = AutoImportModuleString.SheetName.MyParam;
            _dicString[AutoImportModuleString.SheetName.DbTypeConvert] = AutoImportModuleString.SheetName.DbTypeConvert;
            _dicString[AutoImportModuleString.SheetName.SysParam] = AutoImportModuleString.SheetName.SysParam;
            _dsExcel = ExportHelper.GetExcelDataSet(_dicString);//导入模板
            if (_dsExcel != null)
            {
                DataTable dt = _dsExcel.Tables[AutoImportModuleString.SheetName.EntityModule];
                if (dt == null)
                {
                    MsgHelper.ShowInfo("导入模板错误，请重新导入！");
                    return;
                }
                //绑定下拉框
                cbbModuleList.BindDropDownList(dt, AutoImportModuleString.ColumnNameModule.ModuleCode, AutoImportModuleString.ColumnNameModule.ModuleCode, true);

                //绑定自定义变量网格
                FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
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
            }
        }

        #region 测试连接按钮事件
        private async void btnLinkServer_Click(object sender, EventArgs e)
        {
            try
            {
                _dbServer = await uC_DbConnection1.GetDbServerInfo();
                if (_dbServer == null)
                {
                    return;
                }
                _dataAccess = AutoSQLExecutor.Common.AutoSQLExecutors.Connect(_dbServer);
                //加载数据库
                LoadTreeView(_dbServer);
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        } 
        #endregion

        #region 关闭按钮事件
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion

        #region 选择路径按钮事件
        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.AutoEntity_Path, dialog.SelectedPath, "【实体生成】保存路径");
                WinFormContext.UserLoveSettings.Save();
                //Setting.Default.AutoEntity_Path1 = dialog.SelectedPath;
                //Setting.Default.Save();
            }
        } 
        #endregion

        #region 加载树方法
        private void LoadTreeView(DbServerInfo server)
        {
            try
            {
                //得到架构
                DataTable schema = _dataAccess.GetDataBases();
                tvDataBaseInfo.Nodes.Clear();
                TreeNode ndServer = new TreeNode();
                ndServer.ToolTipText = sTip;
                ndServer.Text = server.ServerName;
                ndServer.Tag = server;

                for (int i = 0; i < schema.Rows.Count; i++)
                {
                    TreeNode ndDB = new TreeNode();
                    string sDBName = schema.Rows[i][DBDataBaseEntity.SqlString.Name].ToString();
                    string sSchema = sDBName;
                    if (server.DatabaseType == DataBaseType.SqlServer)
                    {
                        server.Database = sDBName;
                        _dataAccess.ModifyConnectString(server);
                        sSchema = "";
                    }
                    //得到表清单
                    DataTable dtTables = _dataAccess.GetSqlSchemaTables(null, sSchema);
                    DataView dv = dtTables.DefaultView;
                    dtTables.DefaultView.Sort = "TABLE_NAME ASC";
                    for (int j = 0; j < dv.Count; j++)
                    {
                        TreeNode ndTable = new TreeNode();
                        ndTable.ToolTipText = sTip;
                        ndTable.Text = dv[j][DBTableEntity.SqlString.Name].ToString();
                        //保存当前行数据
                        DataTable[] arrTables = new DataTable[2];
                        DataTable dtTable = DBTableEntity.GetTableStruct();
                        dtTable.ImportRow(dv[j].Row);
                        arrTables[0] = dtTable;
                        ndTable.Tag = arrTables;

                        ndDB.Nodes.Add(ndTable);
                    }
                    ndDB.ToolTipText = sTip;
                    ndDB.Text = sDBName;
                    ndDB.Tag = dtTables;//保存整个表
                    ndServer.Nodes.Add(ndDB);
                }
                tvDataBaseInfo.Nodes.Add(ndServer);
                //展开默认的数据库表树
                string strDefuatExpandDBName = server.Database;
                foreach (TreeNode tnode in tvDataBaseInfo.Nodes)
                {
                    ExpandDefaultDB(tnode, strDefuatExpandDBName);
                }
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        } 
        #endregion

        #region 展开默认数据库表视图方法
        /// <summary>
        /// 找到默认数据库并展开
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="strDefuatExpandDBName"></param>
        private void ExpandDefaultDB(TreeNode tn, string strDefuatExpandDBName)
        {
            if (tn.Text == strDefuatExpandDBName)
            {
                tn.ExpandAll();
                ExpandParentNode(tn);
            }
            else
            {
                foreach (TreeNode node in tn.Nodes)
                {
                    ExpandDefaultDB(node, strDefuatExpandDBName);
                }
            }
        }

        /// <summary>
        /// 展开父节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandParentNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expand();
                ExpandParentNode(node.Parent);
            }
        } 
        #endregion

        #region 单部表生成右键菜单事件
        private void tsmiStable_Click(object sender, EventArgs e)
        {
            try
            {
                BuildTemplateEntity();
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        } 
        #endregion

        #region 全部表生成右键菜单事件
        private void tsmiFtable_Click(object sender, EventArgs e)
        {
            try
            {
                BuildTemplateEntity();
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        } 
        #endregion

        #region 选择树后事件
        private void tvDataBaseInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e != null)
            {
                _selectedNode = e.Node;
            }
        } 
        #endregion

        #region 按下鼠标事件
        private void tvDataBaseInfo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (_selectedNode == null) return;
                if ((_selectedNode.Level == 1) || (_selectedNode.Level == 2))
                {
                    if (_selectedNode.IsSelected && (_selectedNode.Level == 1))
                    {
                        if (_selectedNode.Nodes.Count > 0)
                        {
                            tsmiStable.Visible = false;
                            tsmiFtable.Visible = true;
                        }
                        else
                        {
                            ctxmBuilt.Visible = false;
                        }
                    }
                    else
                    {
                        tsmiFtable.Visible = false;
                        tsmiStable.Visible = true;
                    }
                }
                else
                {
                    ctxmBuilt.Visible = false;
                }
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        } 
        #endregion

        #region 树鼠标单击事件
        private void tvDataBaseInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Level == 2)
                {
                    DataTable[] dtArr = e.Node.Tag as DataTable[];
                    if (dtArr[1] == null)
                    {
                        _dbServer.Database = e.Node.Parent.Text;
                        _dataAccess.ModifyConnectString(_dbServer);
                        dtArr[1] = _dataAccess.GetSqlSchemaTableColumns(e.Node.Text,_dbServer.SchemaName);
                        dgvTableInfo.BindDataGridView(dtArr[1]);
                    }
                    else
                    {
                        dgvTableInfo.BindDataGridView(dtArr[1]);
                    }
                }
            }
            catch (Exception exception)
            {
                ShowErr(exception.Message);
            }
        }
        #endregion
       
        /// <summary>
        /// 模板下拉框变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModuleList.Text.Trim().Length > 0 && _dsExcel != null)
            {
                DataTable dt = _dsExcel.Tables[AutoImportModuleString.SheetName.EntityModule];
                DataRow[] drArr = dt.Select(AutoImportModuleString.ColumnNameModule.ModuleCode + "='"+cbbModuleList.Text+"'");
                if(drArr.Length> 0)
                {
                    rtbEntityTemplate.Clear();
                    rtbEntityTemplate.AppendText(drArr[0][AutoImportModuleString.ColumnNameModule.ModuleContent].ToString()); //模板内容
                    txbModuleFileSuffix.Text = drArr[0][AutoImportModuleString.ColumnNameModule.ModuleFileSuffix].ToString(); //后缀
                }
            }
        }

        /// <summary>
        /// 模板下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDownloadModel_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.AutoEntity.Excel_Code, "模板_生成代码文件", true);
        }
    }
}