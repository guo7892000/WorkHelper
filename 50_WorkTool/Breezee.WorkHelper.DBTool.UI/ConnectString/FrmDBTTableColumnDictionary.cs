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
using System.Security.Policy;
using System.IO;
using System.Xml.Linq;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 表列字典
    /// </summary>
    public partial class FrmDBTTableColumnDictionary : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";

        private readonly string _sGridColumnSelect = "IsSelect";

        private bool _allSelect = false;//默认全选，这里取反
        private bool _allSelectAll = false;//默认全选，这里取反
        private bool _allSelectTable = false;//默认全选，这里取反
        private bool _allSelectCommon = false;//默认全选，这里取反
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

        private readonly string _sInputColCode = "列编码";
        private BindingSource bsFindColumn = new BindingSource();
        //IDictionary<string, string> _dicColCodeRelation = new Dictionary<string, string>();
        MiniXmlConfig commonColumn;
        #endregion

        #region 构造函数
        public FrmDBTTableColumnDictionary()
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
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            #endregion

            SetColTag();
            //模板
            _dicString.Add("1", "YAPI参数格式");
            _dicString.Add("2", "YAPI查询结果(不分页)");
            _dicString.Add("3", "YAPI查询结果(分页)");
            _dicString.Add("4", "YAPI查询条件(分页)");
            cbbModuleString.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);

            //
            _dicString.Clear();
            _dicString["1"]= "粘贴列";
            _dicString["2"] = "查询表";
            cbbInputType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //初始化网格
            DataTable dtIn = new DataTable();
            dtIn.Columns.Add(_sInputColCode, typeof(string));
            bsFindColumn.DataSource = dtIn;
            dgvInput.DataSource = bsFindColumn;
            //加载通用列内容
            LoadCommonColumnData();
        }

        /// <summary>
        /// 加载通用列数据
        /// </summary>
        private void LoadCommonColumnData()
        {
            //通用列相关
            List<string> list = new List<string>();
            list.AddRange(new string[] {
                DBColumnSimpleEntity.SqlString.Name,
                DBColumnSimpleEntity.SqlString.NameCN,
                DBColumnSimpleEntity.SqlString.NameUpper,
                DBColumnSimpleEntity.SqlString.NameLower,
                DBColumnSimpleEntity.SqlString.DataType,
                DBColumnSimpleEntity.SqlString.DataLength,
                DBColumnSimpleEntity.SqlString.DataPrecision,
                DBColumnSimpleEntity.SqlString.DataScale,
                DBColumnSimpleEntity.SqlString.DataTypeFull,
                DBColumnSimpleEntity.SqlString.NotNull,
                DBColumnSimpleEntity.SqlString.Default,
                //DBColumnSimpleEntity.SqlString.KeyType,
                DBColumnSimpleEntity.SqlString.Comments,
                DBColumnSimpleEntity.SqlString.Extra,
                //DBColumnSimpleEntity.SqlString.TableName,
                //DBColumnSimpleEntity.SqlString.TableNameCN,
                //DBColumnSimpleEntity.SqlString.TableNameUpper
            });
            commonColumn = new MiniXmlConfig(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Breezee", "WorkHelp"), "CommonColumnConfig.xml", list, DBColumnSimpleEntity.SqlString.Name);
            DataTable dtCommonCol = commonColumn.Load();
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtCommonCol.Columns.Add(dcSelected);
            //通用列网格跟所有列网格结构一样
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Extra).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            //
            dgvCommonCol.Tag = fdc.GetGridTagString();
            dgvCommonCol.BindDataGridView(dtCommonCol, true);
            
        }

        private void cbbConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsbImport.PerformClick();
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
            if (_dbServer == null) return;
            _dataAccess = AutoSQLExecutors.Connect(_dbServer);
            DataTable dtTable = uC_DbConnection1.UserTableList;
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtTable.Columns.Add(dcSelected);

            //绑定表网格
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol()
                , new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build()
                , new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Name).Caption("表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
                , new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.NameCN).Caption("表名中文").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            //,new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Schema).Caption("架构").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            //,new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Owner).Caption("拥有者").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            //,new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.DBName).Caption("所属数据库").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            //,new FlexGridColumn.Builder().Name(DBTableEntity.SqlString.Comments).Caption("备注").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(false).Visible().Build()
            );
            dgvTableList.Tag = fdc.GetGridTagString();
            dgvTableList.BindDataGridView(dtTable, true);
            tabControl2.SelectedTab = tpTable;//选中表页签

            //是否清除数据
            if (ckbClearAllCol.Checked)
            {
                dgvColList.GetBindingTable().Clear();
            }
            if (ckbClearCopyCol.Checked)
            {
                dgvInput.GetBindingTable().Clear();
            }
            if (ckbClearSelect.Checked)
            {
                dgvSelect.GetBindingTable().Clear();
            }
        }
        #endregion

        /// <summary>
        /// 加载数据按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            rtbConString.Focus();
            bool isChangeTap = false;
            int iTableNum = 500;//大于等于多少个时提示
            //获取当前绑定表
            DataTable dtMain = dgvTableList.GetBindingTable();
            DataTable dtAllCol = dgvColList.GetBindingTable();
            if (ckbAllTableColumns.Checked)
            {
                if (dtMain != null && dtMain.Rows.Count >= iTableNum)
                {
                    if (MsgHelper.ShowYesNo("【获取所有表列清单】可能需要花点时间，是否继续？") == DialogResult.No)
                    {
                        return;
                    }
                }
                //全部获取
                isChangeTap = AddAllColumns(dtAllCol, new List<string>());
            }
            else
            {
                if (dtMain == null) return;
                string sFiter1 = string.Format("{0}='1'", _sGridColumnSelect);
                DataRow[] drArr = dtMain.Select(sFiter1);
                if (drArr.Length == 0)
                {
                    return;
                }
                if (drArr.Length >= iTableNum)
                {
                    if (MsgHelper.ShowYesNo("查询的数据表较多，可能需要花点时间，是否继续？") == DialogResult.No)
                    {
                        ckbAllTableColumns.Checked = false;
                        return;
                    }
                }
                List<string> list = new List<string>();
                foreach (DataRow dr in drArr)
                {
                    list.Add(dr[DBTableEntity.SqlString.Name].ToString());
                }
                isChangeTap = AddAllColumns(dtAllCol, list);
            }

            if (isChangeTap)
            {
                tabControl2.SelectedTab = tpSelectColumn;
            }
        }

        private bool AddAllColumns(DataTable dtAllCol, List<string> list)
        {
            bool isChangeTap = false;
            DataTable dtQueryCols = _dataAccess.GetSqlSchemaTableColumns(list, _dbServer.SchemaName);
            DataTable dtNew = dtAllCol.Clone();
            foreach (DataRow dr in dtQueryCols.Rows)
            {
                DBColumnEntity entity = DBColumnEntity.GetEntity(dr);

                DataTable dt = DBColumnSimpleEntity.GetDataRow(new List<DBColumnEntity> { entity });
                if (dt.Rows.Count > 0)
                {
                    dtNew.ImportRow(dt.Rows[0]);
                }
            }

            DataTable dtQueryTable = new DataView(dtNew).ToTable(true, DBColumnSimpleEntity.SqlString.TableName);
            foreach (DataRow dr in dtQueryTable.Rows)
            {
                string sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.TableName, dr[DBColumnSimpleEntity.SqlString.TableName]);
                if (dtAllCol == null || dtAllCol.Select(sFiter).Length == 0)
                {
                    DataRow[] drQuery = dtNew.Select(sFiter);
                    foreach (DataRow drCol in drQuery)
                    {
                        dtAllCol.ImportRow(drCol);

                    }
                    isChangeTap = true;
                }
            }

            return isChangeTap;
        }

        /// <summary>
        /// 匹配按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatch_Click(object sender, EventArgs e)
        {
            DataTable dtInput = dgvInput.GetBindingTable();
            if (cbbInputType.SelectedValue != null && "2".Equals(cbbInputType.SelectedValue.ToString()))
            {
                string sSql = rtbInputSql.Text.Trim();
                if (string.IsNullOrEmpty(sSql))
                {
                    ShowInfo("请输入查询空数据的SQL，这里只用到查询结果的列编码！");
                    return;
                }
                try
                {
                    DataTable dtSql = _dataAccess.QueryAutoParamSqlData(sSql);
                    foreach (DataColumn dc in dtSql.Columns)
                    {
                        dtInput.Rows.Add(dc.ColumnName);
                    }
                }
                catch (Exception ex)
                {
                    ShowErr("执行查询SQL报错，请保证SQL的正确性，详细信息：" + ex.Message);
                    return;
                }
            }

            DataTable dtSelect = dgvSelect.GetBindingTable();

            DataTable dtAllCol = dgvColList.GetBindingTable();
            DataTable dtCommonCol = dgvCommonCol.GetBindingTable();

            foreach (DataRow dr in dtInput.Rows)
            {
                string sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, dr[_sInputColCode].ToString());
                DataRow[] drArr = dtSelect.Select(sFiter);
                if (drArr.Length > 0)
                {
                    continue;
                }
                sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, dr[_sInputColCode].ToString());
                //
                drArr = dtCommonCol.Select(sFiter);
                if (drArr.Length > 0)
                {
                    dtSelect.ImportRow(drArr[0]);
                }
                else
                {
                    drArr = dtAllCol.Select(sFiter);
                    if (drArr.Length > 0)
                    {
                        dtSelect.ImportRow(drArr[0]);
                    }
                    else if(ckbNotFoundAdd.Checked)
                    {
                        //找不到的也加入，但只有列编码
                        DataRow drNew = dtSelect.NewRow();
                        drNew[DBColumnSimpleEntity.SqlString.Name] = dr[_sInputColCode].ToString();
                        drNew[DBColumnSimpleEntity.SqlString.NameUpper] = dr[_sInputColCode].ToString().FirstLetterUpper();
                        drNew[DBColumnSimpleEntity.SqlString.NameLower] = dr[_sInputColCode].ToString().FirstLetterUpper(false);
                        drNew[_sGridColumnSelect] = "1";
                        dtSelect.Rows.Add(drNew);
                    }
                }
            }
        }

        #region 设置Tag方法
        private void SetColTag()
        {
            DataTable dtColsAll = DBColumnSimpleEntity.GetTableStruct();
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtColsAll.Columns.Add(dcSelected);

            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(), 
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );

            dgvColList.Tag = fdc.GetGridTagString();
            dgvColList.BindDataGridView(dtColsAll, true);        
            //dgvColList.AllowUserToAddRows = true;//设置网格样式
            //已选择列网格跟通用列网格结构一样
            dgvSelect.Tag = fdc.GetGridTagString();
            dgvSelect.BindDataGridView(dtColsAll.Clone(), true);
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            rtbConString.Focus();
            //取得数据源
            DataTable dtSec = dgvSelect.GetBindingTable();
            StringBuilder sbAllSql = new StringBuilder();
            if (dtSec == null) return;
            //移除空行
            dtSec.DeleteNullRow();
            //得到变更后数据
            dtSec.AcceptChanges();
            //得到【选择】选中的列
            DataTable dtColumnSelect = dtSec.Clone();
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);

            foreach (DataRow dr in dtSec.Select(sFiter))
            {
                dtColumnSelect.ImportRow(dr); //对非修改，不是排除列就导入
            }

            if (dtColumnSelect.Rows.Count == 0 || dtColumnSelect.Rows.Count == 0)
            {
                ShowInfo("请先选择列！");
                return;
            }

            if (string.IsNullOrEmpty(rtbConString.Text.Trim()))
            {
                ShowInfo("请输入拼接的字符格式！");
                return;
            }
            #endregion

            try
            {
                for (int i = 0; i < dtColumnSelect.Rows.Count; i++)
                {
                    string strOneData = rtbConString.Text;
                    foreach (DataColumn dc in dtColumnSelect.Columns)
                    {
                        string strData = dtColumnSelect.Rows[i][dc.ColumnName].ToString();
                        //将数据中的列名替换为单元格中的数据
                        strOneData = strOneData.Replace("#" + dc.ColumnName + "#", strData);
                    }
                    //所有SQL文本累加
                    sbAllSql.Append(strOneData + "\n");
                }
                rtbResult.Clear();

                if (ckbRemoveLastChar.Checked)
                {
                    string sLast = sbAllSql.ToString().Trim();
                    sLast = sLast.Substring(0, sLast.Length - 1);
                    sbAllSql.Clear();
                    sbAllSql.Append(sLast);
                }

                //YAPI的字符模板处理
                YapiModuleStringDeal(sbAllSql);
                rtbResult.AppendText(sbAllSql.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());

                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                //ShowInfo(strInfo);
                lblInfo.Text = _strAutoSqlSuccess;
                rtbResult.Select(0, 0); //返回到第一行
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        /// <summary>
        /// YAPI的字符模板处理
        /// </summary>
        /// <param name="sbAllSql"></param>
        private void YapiModuleStringDeal(StringBuilder sbAllSql)
        {
            string sModule = cbbModuleString.SelectedValue.ToString();
            if ("2".Equals(sModule))
            {
                sbAllSql.Insert(0, @"{
  ""type"": ""object"",
  ""title"": ""empty object"",
  ""properties"": {
    ""result"": {
      ""type"": ""string"",
      ""description"": ""执行结果""
    },
    ""msg"": {
      ""type"": ""string"",
      ""description"": ""返回信息""
    },
    ""rows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object"",
        ""properties"": {
           ");

                sbAllSql.AppendLine(@"
           }
      },
      ""description"": ""结果集""
    }
  },
  ""required"": [
    ""rows"",
    ""msg"",
    ""result""
  ]
}");
            }
            else if ("3".Equals(sModule))
            {
                sbAllSql.Insert(0, @"{
  ""type"": ""object"",
  ""title"": ""empty object"",
  ""properties"": {
    ""pageindex"": {
      ""type"": ""number"",
      ""description"": ""页号""
      },
    ""pages"": {
      ""type"": ""number"",
      ""description"": ""总页数""
      },
    ""records"": {
      ""type"": ""number"",
      ""description"": ""总记录数""
      },
    ""result"": {
      ""type"": ""number"",
      ""description"": ""执行结果""
      },
    ""msg"": {
      ""type"": ""string"",
      ""description"": ""返回信息""
      },
    ""rows"": {
      ""type"": ""object"",
      ""properties"": {
        ");
                sbAllSql.AppendLine(@"
        },
      ""description"": ""结果集"",
      ""required"": []
    }
  }
}");
            }
            else if ("4".Equals(sModule))
            {
                sbAllSql.Insert(0, @"{
  ""type"": ""object"",
  ""title"": ""empty object"",
  ""properties"": {
    ""pageindex"": {
      ""type"": ""number"",
      ""description"": ""当前页数""
      },
    ""pagesize"": {
      ""type"": ""number"",
      ""description"": ""每页条数""
      },
    ");

                sbAllSql.AppendLine(@"
    }
}");
            }
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            string sSearch = txbSearchCol.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;

            bool isFind = FindData(dgvColList, sSearch, DBColumnSimpleEntity.SqlString.Name);
            if (!isFind)
            {
                isFind = FindData(dgvColList, sSearch, DBColumnSimpleEntity.SqlString.NameCN);
            }
            if (!isFind)
            {
                isFind = FindData(dgvColList, sSearch, DBColumnSimpleEntity.SqlString.NameUpper);
            }
        }

        private void btnFindCommon_Click(object sender, EventArgs e)
        {
            string sSearch = txbSearchCommon.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;

            bool isFind = FindData(dgvCommonCol, sSearch, DBColumnSimpleEntity.SqlString.Name);
            if (!isFind)
            {
                isFind = FindData(dgvCommonCol, sSearch, DBColumnSimpleEntity.SqlString.NameCN);
            }
            if (!isFind)
            {
                isFind = FindData(dgvCommonCol, sSearch, DBColumnSimpleEntity.SqlString.NameUpper);
            }
        }

        private bool FindData(DataGridView dgv,string sSearch,string sColumnName)
        {
            BindingSource bs = dgv.DataSource as BindingSource;
            int iCureent = bs.Find(sColumnName, sSearch);
            if (iCureent > -1)
            {
                dgv.CurrentCell.Selected = false;
                dgv.Rows[iCureent].Cells[sColumnName].Selected = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存通用列配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommonSave_Click(object sender, EventArgs e)
        {
            DataTable dtSave = dgvCommonCol.GetBindingTable();
            if(dtSave.Rows.Count > 0)
            {
                if (MsgHelper.ShowYesNo("确定要保存？") == DialogResult.Yes)
                {
                    commonColumn.Save(dtSave);
                    ShowInfo("保存成功！");
                }
            }
            else
            {
                ShowInfo("没有要保存的数据！");
            }
        }

        /// <summary>
        /// 输入网格的右键按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInput_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                {
                    string pasteText = Clipboard.GetText().Trim();
                    if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                    {
                        return;
                    }
                    DataTable dtMain = ((BindingSource)dgvInput.DataSource).DataSource as DataTable;
                    int iRow = 0;
                    int iColumn = 0;
                    Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
 
                    foreach (DataRow dr in dtMain.Select(_sInputColCode+ " is null or "+ _sInputColCode + "=''"))
                    {
                        dtMain.Rows.Remove(dr);
                    }
                    dtMain.AcceptChanges();
                    int rowindex = dtMain.Rows.Count;
                    int iGoodDataNum = 0;//有效数据号
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        if (string.IsNullOrEmpty(strData))
                        {
                            continue;
                        }
                        if (dtMain.Select(_sInputColCode + "='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.NewRow());
                            dtMain.Rows[rowindex + iGoodDataNum][0] = strData;
                            iGoodDataNum++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        #region 网格增加或移除数据
        /// <summary>
        /// 加入通过列右键按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCommonCol_Click(object sender, EventArgs e)
        {
            DataRow dataRow = dgvColList.GetCurrentRow();
            if (dataRow == null) return;
            dgvCommonCol.GetBindingTable().ImportRow(dataRow);
        }

        /// <summary>
        /// 所选列加入字典按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllAddDic_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvColList.GetBindingTable();
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);
            DataRow[] drArr = dt.Select(sFiter);
            foreach (DataRow dr in drArr)
            {
                dgvCommonCol.GetBindingTable().ImportRow(dr); 
            }
        }

        private void tsmiRemoveCommon_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvCommonCol.GetBindingTable();
            DataRow dataRow = dgvCommonCol.GetCurrentRow();
            dt.Rows.Remove(dataRow);
        }

        private void btnCommonRemoveSelect_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvCommonCol.GetBindingTable();
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);
            DataRow[] drArr = dt.Select(sFiter);
            foreach (DataRow dr in drArr)
            {
                dt.Rows.Remove(dr);
            }
        }

        private void tsmiRemoveSelect_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            DataRow dataRow = dgvSelect.GetCurrentRow();
            dt.Rows.Remove(dataRow);
        }

        private void tsmiRemoveSelectAll_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            dt.Rows.Clear();
        }

        private void btnAllRemoveSelect_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvColList.GetBindingTable();
            string sFiter = string.Format("{0}='1'", _sGridColumnSelect);
            DataRow[] drArr = dt.Select(sFiter);
            foreach (DataRow dr in drArr)
            {
                dt.Rows.Remove(dr);
            }
        }
        private void tsmiRemoveAllCol_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvColList.GetBindingTable();
            dt.Clear();
        }

        
        #endregion

        private void cbbModuleString_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModuleString.SelectedValue != null && !string.IsNullOrEmpty(cbbModuleString.SelectedValue.ToString()))
            {
                string sModule = cbbModuleString.SelectedValue.ToString();
                rtbConString.Clear();
                if ("1".Equals(sModule) || "2".Equals(sModule) || "3".Equals(sModule) || "4".Equals(sModule))
                {
                    rtbConString.AppendText(@"""#C3#"":{
    ""type"":""string"",
    ""description"":""#C1#""
    },");
                    if (!"1".Equals(sModule))
                    {
                        ckbRemoveLastChar.Checked = true;
                    }
                }
            }
        }

        private void cbbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbInputType.SelectedValue != null)
            {
                string sType = cbbInputType.SelectedValue.ToString();
                if("1".Equals(sType))
                {
                    grbInputSql.Visible = false;
                }
                else
                {
                    grbInputSql.Visible = true;
                }
            }
        }

        #region 网格头双击全选或取消全选事件
        private void SelectAllOrCancel(DataGridView dgv, ref bool isSelect, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns[_sGridColumnSelect].Index)
            {
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Cells[_sGridColumnSelect].Value = isSelect ? "1" : "0";
                }
                isSelect = !isSelect;
            }
        }
        private void dgvTableList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvTableList, ref _allSelectTable, e);
        }
        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvColList, ref _allSelectAll, e);
        }

        private void dgvSelect_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvSelect, ref _allSelect, e);
        }

        private void dgvCommonCol_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvCommonCol, ref _allSelectCommon, e);
        }




        #endregion

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
    }
}