﻿using Breezee.WorkHelper.DBTool.IBLL;
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
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using LibGit2Sharp;
using org.breezee.MyPeachNet;
using static Breezee.WorkHelper.DBTool.UI.TableColumnDicTemplate;
using Breezee.Core.Entity;
using FluentFTP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private readonly string _sGridColumnIsMust = "ColumnIsMust";
        private readonly string _sGridColumnIsNoCnRemark = "IsNoCnRemark";
        private bool _allSelectOldNewChar = false;//默认全选，这里取反

        private bool _allSelect = false;//默认全选，这里取反
        private bool _allSelectAll = false;//默认全选，这里取反
        private bool _allSelectTable = false;//默认全选，这里取反
        private bool _allSelectCommon = false;//默认全选，这里取反
        private bool _allSelectNameCode = false;//默认全选，这里取反
        private bool _allSelectColumnIsMust = false;//默认全选，这里取反
        //常量
        private static string strTableAlias = "A"; //查询和修改中的表别名
        private static string strTableAliasAndDot = "";
        private static readonly string _strUpdateCtrolColumnCode = "UPDATE_CONTROL_ID";
        //数据集
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
        private IDBDefaultValue _IDBDefaultValue;
        private DataTable _dtDefault = null;
        DBSqlEntity sqlEntity;

        private readonly string _sInputColCode = "列编码";
        private readonly string _sInputColName = "列名称";
        private string _moduleColumnName; //模板参数列名
        private string _moduleParamName; //模板参数名
  
        //MiniXmlConfig commonColumn;
        DataStandardConfig dataCfg;
        MiniXmlConfig codeNameColumn;
        MiniXmlConfig stringTemplate;
        DataGridViewFindText dgvFindText;
        DataGridViewFindText dgvFindTextAllColumn;
        DataGridViewFindText dgvFindTextCommonCol;
        DataGridViewFindText dgvFindTextCodeNameCol;
        ReplaceStringXmlConfig replaceStringData;//替换字符模板XML配置
        IDictionary<string, string> _dicSystemStringTemplate = new Dictionary<string, string>();
        IDictionary<string,string> _dicYapiTemplate = new Dictionary<string,string>();
        
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

            //读取文件模板
            string sPath = YapiDirPath;
            _dicYapiTemplate[YapiString.ConditionNoPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ConditionNoPage), Encoding.UTF8);
            _dicYapiTemplate[YapiString.ConditionPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ConditionPage), Encoding.UTF8);
            _dicYapiTemplate[YapiString.ResultNoPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ResultNoPage), Encoding.UTF8);
            _dicYapiTemplate[YapiString.ResultPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ResultPage), Encoding.UTF8);
            _dicYapiTemplate[YapiString.ResultArrayNoPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ResultArrayNoPage), Encoding.UTF8);
            _dicYapiTemplate[YapiString.ResultArrayPage] = File.ReadAllText(Path.Combine(sPath, YapiString.ResultArrayPage), Encoding.UTF8);
            //读取字符模板
            sPath = StringTempDirPath;
            _dicYapiTemplate[StringTemplate.MyBatisDynamicConditon] = File.ReadAllText(Path.Combine(sPath, StringTemplate.MyBatisDynamicConditon), Encoding.UTF8);
            _dicYapiTemplate[StringTemplate.MyBatisTableEntity] = File.ReadAllText(Path.Combine(sPath, StringTemplate.MyBatisTableEntity), Encoding.UTF8);
            _dicYapiTemplate[StringTemplate.CommonEntity] = File.ReadAllText(Path.Combine(sPath, StringTemplate.CommonEntity), Encoding.UTF8);
            _dicYapiTemplate[StringTemplate.YapiColumnDesc] = File.ReadAllText(Path.Combine(sPath, StringTemplate.YapiColumnDesc), Encoding.UTF8);
            //MyBatis动态条件字符模板
            sPath = MyBatisDynDirPath;
            _dicYapiTemplate[MyBatisDynamicCond.If] = File.ReadAllText(Path.Combine(sPath, MyBatisDynamicCond.If), Encoding.UTF8);
            _dicYapiTemplate[MyBatisDynamicCond.In] = File.ReadAllText(Path.Combine(sPath, MyBatisDynamicCond.In), Encoding.UTF8);
            _dicYapiTemplate[MyBatisDynamicCond.Date] = File.ReadAllText(Path.Combine(sPath, MyBatisDynamicCond.Date), Encoding.UTF8);

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            _dicQuery[DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED] = "1";
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            #endregion

            SetColTag();
            //模板
            _dicSystemStringTemplate.Add("1", "YAPI查询条件");
            _dicSystemStringTemplate.Add("2", "YAPI查询结果");
            _dicSystemStringTemplate.Add("9", "YAPI参数格式");
            _dicSystemStringTemplate.Add("11", "Mybatis表实体属性");
            _dicSystemStringTemplate.Add("12", "通用实体属性");
            _dicSystemStringTemplate.Add("13", "Mybatis查询条件");
            DataTable dtStringTemplate = _dicSystemStringTemplate.GetTextValueTable(false);
            //编码名称配置网格相关
            var list = new List<string>();
            list.AddRange(new string[] {
                StringTemplateConfig.Id,
                StringTemplateConfig.Name,
                StringTemplateConfig.TemplateString
            });
            stringTemplate = new MiniXmlConfig(GlobalContext.PathData(), "ColumnDictionary_StringTemplateConfig.xml", list, StringTemplateConfig.Id);
            DataTable dtStringTemp = stringTemplate.Load();
            foreach (DataRow dr in dtStringTemp.Rows)
            {
                dtStringTemplate.Rows.Add(dr[StringTemplateConfig.Id].ToString(), dr[StringTemplateConfig.Name].ToString());
                _dicYapiTemplate[dr[StringTemplateConfig.Id].ToString()] = dr[StringTemplateConfig.TemplateString].ToString();
            }
            cbbModuleString.BindTypeValueDropDownList(dtStringTemplate, true, true);
            //
            _dicString.Clear();
            _dicString["1"] = "匹配SQL条件";
            _dicString["2"] = "匹配SQL结果";
            _dicString["3"] = "粘贴列";
            cbbInputType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //
            _dicString.Clear();
            _dicString["array"] = "array";
            _dicString["object"] = "object";
            cbbQueryResultType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //初始化网格
            DataTable dtIn = new DataTable();
            dtIn.Columns.Add(_sInputColCode, typeof(string));
            dgvInput.BindAutoColumn(dtIn);
            //加载通用列等内容
            LoadCommonColumnData();
            //加载用户偏好值：注对于模板字符要设置在放在模板下拉框绑定之前
            _moduleColumnName = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_QueryConditionParamColumnModule, "#C3#").Value; //模板参数列名
            txbParamColumn.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_QueryConditionParamColumnLatest, _moduleColumnName).Value; //最后设置的参数列
            _moduleParamName = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_QueryConditionParamNameModule, "param").Value; //模板参数名
            txbParamName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_QueryConditionParamNameLatest, _moduleParamName).Value; //最后设置的参数名

            cbbInputType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_ConfirmColumnType, "2").Value;
            cbbModuleString.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_TemplateType, "2").Value;
            cbbQueryResultType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_QueryResultType, "array").Value; //查询结果类型
            ckbIsPage.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_IsPage, "0").Value) ? true : false;//是否分页
            ckbAutoParamQuery.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_IsAutoParam, "1").Value) ? true : false;//自动参数化查询
            ckbIsAutoExclude.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_IsAutoExcludeTable, "1").Value) ? true : false;//自动排除表名
            ckbReMatchSqlTable.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_IsOnlyMatchSqlTable, "1").Value) ? true : false;//仅匹配SQL表
            txbExcludeTable.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_ExcludeTableList, "_bak,_temp,_tmp").Value; //排除表列表
            ckbColumnMust.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ColumnDic_IsColumnMust, "1").Value) ? true : false;//是否必填

            toolTip1.SetToolTip(ckbAutoParamQuery, "当选中时，是使用MyPeach.Net自动参数化SQL后，再执行的；否则是直接运行SQL。");
            toolTip1.SetToolTip(txbExcludeTable, "排除包括逗号分隔列表中所有字符的表名。");
            toolTip1.SetToolTip(ckbRemoveLastChar, "选中后，在生成时会去掉最后一个字符。"); 
            toolTip1.SetToolTip(cbbQueryResultType, "查询结果集的类型：\n为array时，其下会有一个item对象，其下才是数组对象的属性；\n为object时，其下直接是属性。");
            toolTip1.SetToolTip(ckbClearAllCol, "指加载数据时，会先清空【所有表列】页签下，【列清单】网格中的所有内容。");
            toolTip1.SetToolTip(ckbClearCopyCol, "指加载数据时，会先清空【列清单】页签下，【粘贴或查询的列】网格中的所有内容。");
            toolTip1.SetToolTip(ckbClearSelect, "指加载数据时，会先清空【已选择】网格下的所有内容。");
            toolTip1.SetToolTip(ckbIsAutoExclude, "指加载数据时，会排除包含其后的文本内容（逗号分隔多个）的表名。");
            toolTip1.SetToolTip(ckbAllTableColumns, "指加载数据时，会查询和加载所有表下面的列清单。");
            toolTip1.SetToolTip(ckbColumnMust, "选中后，在匹配列时，已选择网格中的【必填】列会处理选中状态，\r\n那样生成的YAPI参数是必填的。");
            toolTip1.SetToolTip(ckbIsPage, "选中后，在生成的YAPI参数里会包括分页相关参数。");
            toolTip1.SetToolTip(ckbNotFoundAdd, "选中后，在匹配时，如果找不到相关列信息，但还是会加到【已选择】网格中。");
            toolTip1.SetToolTip(cbbTemplateType, "如果我们需要对生成的字符，做进一步字符替换处理时，\r\n可选择预保存好的字符替换模板。");
            toolTip1.SetToolTip(btnMatch, "仅匹配数据");
            toolTip1.SetToolTip(btnGenCondition, "自动生成查询条件参数的API字符，并复制到粘贴板中。"); 
            toolTip1.SetToolTip(btnGenResult, "自动生成查询结果所有列的API字符，并复制到粘贴板中。");
            toolTip1.SetToolTip(ckbIncludeCommonColumn, "选中时，会将【编码名称】中【通用】列为选中的列加到生成结果中，\r\n例如：在保存时，【用户ID】和【员工姓名】通常需要前端传入。");
            //加载模板数据
            replaceStringData = new ReplaceStringXmlConfig(DBTGlobalValue.TableColumnDictionary.Xml_FileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, ReplaceStringXmlConfig.KeyString.Name, true, true);
            //新旧字符网格
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("OLD").Caption("旧字符").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("NEW").Caption("新字符").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build()
                );
            dgvOldNewChar.Tag = fdc.GetGridTagString();
            dgvOldNewChar.BindDataGridView(null, false);
            dgvOldNewChar.AllowUserToAddRows = true;
            // 设置初始分割比例
            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

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

            //commonColumn = new MiniXmlConfig(GlobalContext.PathData(), "CommonColumnConfig.xml", list, DBColumnSimpleEntity.SqlString.Name);
            //DataTable dtCommonCol = commonColumn.Load();
            dataCfg = new DataStandardConfig();
            DataTable dtCommonCol = dataCfg.XmlConfig.Load();
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
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(60).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.Id).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(0).Edit(false).Visible(false).Build()
            );
            //
            dgvCommonCol.Tag = fdc.GetGridTagString();
            dgvCommonCol.BindDataGridView(dtCommonCol, true);

            //编码名称配置网格相关
            list = new List<string>();
            list.AddRange(new string[] {
                DBColumnSimpleEntity.SqlString.Name,
                DBColumnSimpleEntity.SqlString.NameCN,
                DBColumnSimpleEntity.SqlString.NameUpper,
                DBColumnSimpleEntity.SqlString.NameLower,
                DBColumnSimpleEntity.SqlString.NotNull
            });
            codeNameColumn = new MiniXmlConfig(GlobalContext.PathData(), "CodeNameColumnConfig.xml", list, DBColumnSimpleEntity.SqlString.Name);
            DataTable dtCodeNameCol = codeNameColumn.Load();
            if (!dtCodeNameCol.Columns.Contains(DBColumnSimpleEntity.SqlString.NotNull))
            {
                DataColumn dcCodeName = new DataColumn(DBColumnSimpleEntity.SqlString.NotNull);
                dcCodeName.DefaultValue = "0";
                dtCodeNameCol.Columns.Add(dcCodeName);
            }
            foreach (DataRow row in dtCodeNameCol.Rows)
            {
                if (string.IsNullOrEmpty(row[DBColumnSimpleEntity.SqlString.NotNull].ToString()))
                {
                    row[DBColumnSimpleEntity.SqlString.NotNull] = "0";
                }
            }
            dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtCodeNameCol.Columns.Add(dcSelected);
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Caption("通用").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit().Visible().Build()
            );
            //
            dgvCodeNameCol.Tag = fdc.GetGridTagString();
            dgvCodeNameCol.BindDataGridView(dtCodeNameCol, true);

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
        private async void tsbImport_Click(object sender, EventArgs e)
        {
            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null) return;
            _dataAccess = AutoSQLExecutors.Connect(_dbServer);
            DataTable dtTable = uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey];
            if (!dtTable.Columns.Contains(_sGridColumnSelect))
            {
                //增加选择列
                DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
                dcSelected.DefaultValue = "1";
                dtTable.Columns.Add(dcSelected);
            }
            //排除分表
            ExcludeSplitTable(dtTable);

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
            dgvTableList.ShowRowNum();
            tabControl2.SelectedTab = tpTable;//选中表页签

            if (ckbIsAutoExclude.Checked)
            {
                btnExcludeTable.PerformClick();
            }
            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsAutoExcludeTable, ckbIsAutoExclude.Checked ? "1" : "0", "【数据字典】是否自动排除表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_ExcludeTableList, txbExcludeTable.Text.Trim(), "【数据字典】排除表列表");
            WinFormContext.UserLoveSettings.Save();

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

        /// <summary>
        /// 排除分表：以【_数字】结尾的表
        /// </summary>
        /// <param name="dtTable"></param>
        private void ExcludeSplitTable(DataTable dtTable)
        {
            if (ckbNotIncludeSplitTable.Checked)
            {
                foreach (DataRow dr in dtTable.Rows)
                {
                    string[] arrTable = dr[DBTableEntity.SqlString.Name].ToString().Split('_');
                    int num;
                    if (arrTable.Length > 0 && int.TryParse(arrTable[arrTable.Length - 1], out num))
                    {
                        dr[_sGridColumnSelect] = "0";
                    }
                }
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
            dgvColList.ShowRowNum(true); //显示序号
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
        /// 匹配并生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatchGenerate_Click(object sender, EventArgs e)
        {
            if (MatchData())
            {
                tsbAutoSQL.PerformClick();
            }
        }

        /// <summary>
        /// 匹配按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatch_Click(object sender, EventArgs e)
        {
            MatchData();
        }

        /// <summary>
        /// 匹配并生成查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenCondition_Click(object sender, EventArgs e)
        {
            GenSqlApiString("1");
        }

        /// <summary>
        /// 生成SQL的API字符
        /// </summary>
        /// <param name="sType">类型：1-匹配SQL条件,2-匹配SQL结果</param>
        private bool GenSqlApiString(string sType)
        {
            try
            {
                string sSql = rtbInputSql.Text.Trim();
                if (string.IsNullOrEmpty(sSql))
                {
                    ShowInfo("请先输入SQL！");
                    return false;
                }
                cbbInputType.SelectedValue = sType; //匹配SQL条件
                btnMatchGenerate.PerformClick();
            }
            catch (Exception ex)
            {
                ShowErr(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 匹配并生成查询结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenResult_Click(object sender, EventArgs e)
        {
            GenSqlApiString("2");
        }

        private bool MatchData()
        {
            DataTable dtInput = dgvInput.GetBindingTable();
            DataTable dtSelect = dgvSelect.GetBindingTable();
            DataTable dtAllCol = dgvColList.GetBindingTable();
            DataTable dtNameCodeCol = dgvCodeNameCol.GetBindingTable();
            List<string> listTable = new List<string>(); //表清单
            string sSql = string.Empty; //SQL信息

            string sInputType = cbbInputType.SelectedValue == null ? string.Empty : cbbInputType.SelectedValue.ToString();
            //得到表清单
            if ("1".Equals(sInputType) || "2".Equals(sInputType))
            {
                sSql = rtbInputSql.Text.Trim();
                //移除列名
                if (dtInput.Columns.Contains(_sInputColName))
                {
                    dtInput.Columns.Remove(_sInputColName);
                }
                //1查询条件和2查询结果处理
                if (string.IsNullOrEmpty(sSql))
                {
                    string sErr = "2".Equals(sInputType) ? "请输入查询空数据的SQL，这里只用到查询结果的列编码！" : "请输入条件字符（以@或:开头，或前后#的参数）";
                    ShowInfo(sErr);
                    return false;
                }

                //判断是否已输入了原无字段中文名但输入了
                string sFiter = string.Format("{0}='1' and {1} is not null", _sGridColumnIsNoCnRemark, DBColumnSimpleEntity.SqlString.NameCN);
                if (dtSelect.Select(sFiter).Length > 0)
                {
                    if (ShowOkCancel("将重新加载【选择列】网格中的所有内容，已录入的信息将丢失，确定继续？") == DialogResult.Cancel)
                    {
                        return false;
                    }
                }

                if (ckbOnlyMatchQueryResult.Checked)
                {
                    dtInput.Clear();
                    dtSelect.Clear();
                }

                //从SQL中获取表清单
                List<string> tables = SqlAnalyzer.GetTableList(sSql);
                DataTable dtTableList = dgvTableList.GetBindingTable();
                foreach (string sTableName in tables)
                {
                    sFiter = string.Format("{0}='{1}'", DBTableEntity.SqlString.Name, sTableName);
                    if(dtTableList.Select(sFiter).Length == 0)
                    {
                        continue;
                    }

                    sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.TableName, sTableName);
                    if (dtAllCol.Select(sFiter).Length > 0)
                    {
                        continue;
                    }
                    if (!listTable.Contains(sTableName))
                    {
                        listTable.Add(sTableName);
                    }
                }

                //仅匹配SQL中的表
                if (ckbReMatchSqlTable.Checked && listTable.Count>0)
                {
                    AddAllColumns(dtAllCol, listTable);//添加所有列
                }
            }
            else
            {
                if (dtAllCol.Rows.Count == 0)
                {
                    if (!dtInput.Columns.Contains(_sInputColName))
                    {
                        ShowInfo("请先选择表，并点击【加载数据】后，再匹配数据！");
                        return false;
                    }
                }
                //针对粘贴列，重新匹配时，也清空选择列网格
                if (ckbOnlyMatchQueryResult.Checked)
                {
                    dtSelect.Clear();
                }
                //移除空行
                string sFilter = _sInputColCode + " is null";
                foreach (DataRow dr in dtInput.Select(sFilter))
                {
                    dtInput.Rows.Remove(dr);
                }
            }

            if (!string.IsNullOrEmpty(sInputType))
            {
                IDictionary<string,string> dicCondition = new Dictionary<string,string>();
                if ("1".Equals(sInputType) || "2".Equals(sInputType))
                {
                    //1查询条件处理
                    HashSet<string> listRemark = new HashSet<string>();
                    string remarkPatter = "--.*|(/\\*.*/*/)";
                    Regex regex = new Regex(remarkPatter, RegexOptions.IgnoreCase);
                    MatchCollection mcColl = regex.Matches(sSql);
                    foreach (Match mt in mcColl)
                    {
                        listRemark.Add(mt.Value);
                    }
                    //注：替换时先要从长字符开始替换，再到短字符
                    var listRemarkSort = listRemark.OrderByDescending(t=>t.Length);
                    foreach (var item in listRemarkSort)
                    {
                        sSql = sSql.Replace(item, ""); //清除注释
                    }
                    //参数格式
                    string sPattern = @"([@:]\w+)|(#\w+#)|(#{\w+})|(#{param.\w+})|(#{para.\w+})";
                    regex = new Regex(sPattern, RegexOptions.IgnoreCase);
                    mcColl = regex.Matches(sSql);
                    foreach (Match mt in mcColl)
                    {
                        if (":".Equals(sSql.Substring(mt.Index - 1, 1)))
                        {
                            continue; //PostgreSQL中存在::转换数据类型，这里要跳过，不作为条件
                        }
                        //去掉参数前后缀
                        string sCol = mt.Value.Replace("#{param.", "")
                            .Replace("#{para.", "")
                            .Replace("@", "")
                            .Replace(":", "")
                            .Replace("#", "")
                            .Replace("{", "")
                            .Replace("}", "");
                        dicCondition[sCol] = sCol;
                        if ("1".Equals(sInputType))
                        {
                            //列名不存在，才添加
                            if (dtInput.Select(_sInputColCode + "='" + sCol + "'").Length == 0)
                            {
                                dtInput.Rows.Add(sCol);
                            }
                        }
                    }
                }

                if ("2".Equals(sInputType))
                {
                    //2查询结果处理
                    bool isOk = false;
                    DataTable dtSql = new DataTable();
                    try
                    {
                        if (ckbAutoParamQuery.Checked)
                        {
                            dtSql = _dataAccess.QueryAutoParamSqlData(sSql, dicCondition); //先调用自动参数化方法，再查询
                        }
                        else
                        {
                            dtSql = _dataAccess.QueryHadParamSqlData(sSql, dicCondition); //直接调用查询方法
                        }
                        isOk = true;
                    }
                    catch (Exception ex)
                    {
                        isOk = false;
                    }

                    //当执行出错时，已按参数化的方法重新执行一次
                    if (!isOk)
                    {
                        try
                        {
                            dtSql = _dataAccess.QueryHadParamSqlData(sSql, dicCondition); //直接调用查询方法
                        }
                        catch (Exception ex)
                        {
                            ShowErr("执行查询SQL报错，请保证SQL的正确性，详细信息：" + ex.Message);
                            return false;
                        }
                    }

                    //将表中的列添加到表中
                    foreach (DataColumn dc in dtSql.Columns)
                    {
                        //判断列是否只包含字母数据下划线：
                        string sPattern = @"^[a-zA-Z0-9_]+$";
                        Regex regex = new Regex(sPattern, RegexOptions.IgnoreCase);
                        var mcColl = regex.Matches(dc.ColumnName);
                        if (mcColl.Count == 0)
                        {
                            ShowErr("SQL中必须所有列都要有别名！存在没有别名的列：" + dc.ColumnName);
                            return false;
                        }

                        if (dtInput.Select(_sInputColCode + "='" + dc.ColumnName + "'").Length == 0)
                        {
                            dtInput.Rows.Add(dc.ColumnName);
                        }
                    }

                }
            }

            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_ConfirmColumnType, cbbInputType.SelectedValue.ToString(), "【数据字典】确认列类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsOnlyMatchSqlTable, ckbReMatchSqlTable.Checked ? "1" : "0", "【数据字典】是否仅匹配SQL表");
            WinFormContext.UserLoveSettings.Save();

            DataTable dtCommonCol = dgvCommonCol.GetBindingTable();
            bool isInputHasColName = dtInput.Columns.Contains(_sInputColName);
            //循环输入的列清单
            foreach (DataRow dr in dtInput.Rows)
            {
                string sCol = dr[_sInputColCode].ToString();
                string sColName = isInputHasColName ? dr[_sInputColName].ToString() : string.Empty;
                string sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, sCol);
                //判断列是否已加入
                DataRow[] drArr = dtSelect.Select(sFiter);
                if (drArr.Length > 0)
                {
                    continue;
                }
                //针对没有下框线，且不是查询类型
                if (!sCol.Contains("_") && !"2".Equals(sInputType))
                {
                    string sFiterUnderscore = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, sCol.ToUnderscoreCase());
                    drArr = dtSelect.Select(sFiterUnderscore);
                    if (drArr.Length > 0)
                    {
                        continue;
                    }
                }

                /*********************测试使用的SQL***************
                 select A.CITY_CODE,A.CITY_NAME, B.PROVINCE_NAME
                    from BAS_CITY A
                    join BAS_PROVINCE B on A.PROVINCE_ID=b.PROVINCE_ID
                    where 1=2
                    AND A.CITY_CODE ='#CITY_CODE#'
                    AND B.PROVINCE_NAME ='#PROVINCE_NAME#'
                 *************************************************/
                //针对1和2，找到语句中的表，优先从这些表里边找
                if ("1".Equals(sInputType) || "2".Equals(sInputType))
                {
                    bool isFind = false;
                    foreach (string sTableName in listTable)
                    {
                        sFiter = string.Format("{0}='{1}' and {2}='{3}'", DBColumnSimpleEntity.SqlString.TableName, sTableName, DBColumnSimpleEntity.SqlString.Name, sCol);
                        drArr = dtAllCol.Select(sFiter);
                        sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, sCol);
                        //判断优先表中是否存在列，且未加入清单
                        if (drArr.Length > 0 && dtSelect.Select(sFiter).Length == 0)
                        {
                            dtSelect.ImportRow(drArr[0]);
                            isFind = true;
                            break; //字段已找到，中止循环表
                        }
                    }
                    //已从优先表中找到，那么直接下一个字段处理
                    if (isFind)
                    {
                        continue;
                    }
                }

                sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, sCol);
                //查找通用列中是否存在
                drArr = dtCommonCol.Select(sFiter);
                if (drArr.Length > 0)
                {
                    dtSelect.ImportRow(drArr[0]);
                    if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                    {
                        dtSelect.Rows[dtSelect.Rows.Count - 1][DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                    }
                    //必填字段赋值
                    dtSelect.Rows[dtSelect.Rows.Count - 1][_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                    continue;
                }

                //查找所有列中是否存在
                drArr = dtAllCol.Select(sFiter);
                if (drArr.Length > 0)
                {
                    dtSelect.ImportRow(drArr[0]);
                    if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                    {
                        dtSelect.Rows[dtSelect.Rows.Count - 1][DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                    }
                    //必填字段赋值
                    dtSelect.Rows[dtSelect.Rows.Count - 1][_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                    continue;
                }

                //查找编码名称列中是否存在
                drArr = dtNameCodeCol.Select(sFiter);
                if (drArr.Length > 0)
                {
                    dtSelect.ImportRow(drArr[0]);
                    if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                    {
                        dtSelect.Rows[dtSelect.Rows.Count - 1][DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                    }
                    //必填字段赋值
                    dtSelect.Rows[dtSelect.Rows.Count - 1][_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                    continue;
                }

                //判断是否包括下横线：如不包含，那么转换为下横线找找看
                if (!sCol.Contains("_") && !"2".Equals(sInputType))
                {
                    sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, sCol.ToUnderscoreCase());
                    //查找通用列中是否存在
                    drArr = dtCommonCol.Select(sFiter);
                    if (drArr.Length > 0)
                    {
                        dtSelect.ImportRow(drArr[0]);
                        if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                        {
                            dtSelect.Rows[dtSelect.Rows.Count - 1][DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                        }
                        //必填字段赋值
                        dtSelect.Rows[dtSelect.Rows.Count - 1][_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                        continue;
                    }

                    //查找所有列中是否存在
                    drArr = dtAllCol.Select(sFiter);
                    if (drArr.Length > 0)
                    {
                        dtSelect.ImportRow(drArr[0]);
                        if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                        {
                            dtSelect.Rows[dtSelect.Rows.Count - 1][DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                        }
                        //必填字段赋值
                        dtSelect.Rows[dtSelect.Rows.Count - 1][_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                        continue;
                    }
                }

                //都找不到
                if (ckbNotFoundAdd.Checked)
                {
                    //找不到的也加入，但只有列编码
                    DataRow drNew = dtSelect.NewRow();
                    drNew[DBColumnSimpleEntity.SqlString.Name] = sCol;
                    drNew[DBColumnSimpleEntity.SqlString.NameUpper] = sCol.FirstLetterUpper();
                    drNew[DBColumnSimpleEntity.SqlString.NameLower] = sCol.FirstLetterUpper(false);
                    drNew[_sGridColumnSelect] = "1";
                    drNew[_sGridColumnIsNoCnRemark] = "1"; //没有中文备注的列
                    if (isInputHasColName && !string.IsNullOrEmpty(sColName))
                    {
                        drNew[DBColumnSimpleEntity.SqlString.NameCN] = sColName;
                    }
                    //必填字段赋值
                    drNew[_sGridColumnIsMust] = ckbColumnMust.Checked ? "1" : "0";
                    dtSelect.Rows.Add(drNew);
                    
                }
            }
            dgvSelect.ShowRowNum(true); //显示行号
            return true;
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
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(), 
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );

            dgvColList.Tag = fdc.GetGridTagString();
            dgvColList.BindDataGridView(dtColsAll, true);         
            //已选择列网格跟通用列网格结构一样
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sGridColumnIsMust).Caption("必填").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            dgvSelect.Tag = fdc.GetGridTagString();
            DataTable dtSelect = dtColsAll.Clone();
            //无中文名称列
            dcSelected = new DataColumn(_sGridColumnIsNoCnRemark);
            dcSelected.DefaultValue = "0";
            dtSelect.Columns.Add(dcSelected);
            //必填列
            dcSelected = new DataColumn(_sGridColumnIsMust);
            dcSelected.DefaultValue = "1";
            dtSelect.Columns.Add(dcSelected);
            dgvSelect.BindDataGridView(dtSelect, true);
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            rtbConString.Focus();
            //取得数据源
            DataTable dtSec = dgvSelect.GetBindingTable();
            StringBuilder sbAllSql = new StringBuilder(); //模板字符接接
            StringBuilder sbAllMust = new StringBuilder(); //必填列字符接接
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

            string sTemplateString = rtbConString.Text;
            if (string.IsNullOrEmpty(sTemplateString.Trim()))
            {
                ShowInfo("请输入拼接的字符格式！");
                return;
            }
            #endregion

            // 额外的公共列加入到选择中
            if (ckbIncludeCommonColumn.Checked)
            {
                DataTable dtCommonCol = dgvCodeNameCol.GetBindingTable();
                sFiter = string.Format("{0}='1'", DBColumnSimpleEntity.SqlString.NotNull);
                DataRow[] drArrCommon = dtCommonCol.Select(sFiter);
                if(drArrCommon.Length > 0)
                {
                    foreach (DataRow item in drArrCommon)
                    {
                        sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, item[DBColumnSimpleEntity.SqlString.Name]);
                        DataRow[] drArrCommonSelct = dtColumnSelect.Select(sFiter);
                        if (drArrCommonSelct.Length == 0)
                        {
                            dtColumnSelect.ImportRow(item);
                        }
                    }
                    
                }
            }

            //取出第一个符合#列名#的：这个列作为必填的字段名
            //1查询条件处理
            string firstParamPatter = @"#\w+#";
            Regex regex = new Regex(firstParamPatter, RegexOptions.IgnoreCase);
            MatchCollection mcColl = regex.Matches(sTemplateString);
            string sMustColumnName = string.Empty;
            foreach (Match mt in mcColl)
            {
                sMustColumnName = mt.Value.Replace("#","");
                break;
            }

            try
            {
                string sModule = cbbModuleString.SelectedValue == null ? string.Empty : cbbModuleString.SelectedValue.ToString();
                for (int i = 0; i < dtColumnSelect.Rows.Count; i++)
                {
                    string strOneData = rtbConString.Text;
                    if ("13".Equals(sModule))
                    {
                        //13：Mybatis查询条件
                        string sDataType = dtColumnSelect.Rows[i][DBColumnSimpleEntity.SqlString.DataType].ToString();
                        string sColumnName = dtColumnSelect.Rows[i][DBColumnSimpleEntity.SqlString.Name].ToString();
                        if ("date".Equals(sDataType, StringComparison.OrdinalIgnoreCase)|| "datetime".Equals(sDataType, StringComparison.OrdinalIgnoreCase))
                        {
                            strOneData = rtbDate.Text.Trim();
                        }
                        else if(sColumnName.EndsWith("list",StringComparison.OrdinalIgnoreCase))
                        {
                            strOneData = rtbIn.Text.Trim();
                        }
                        else
                        {
                            strOneData = rtbConString.Text.Trim();
                        }
                    }
                    foreach (DataColumn dc in dtColumnSelect.Columns)
                    {
                        string strData = dtColumnSelect.Rows[i][dc.ColumnName].ToString();
                        //将数据中的列名替换为单元格中的数据
                        strOneData = strOneData.Replace("#" + dc.ColumnName + "#", strData);
                        if (cbbModuleString.SelectedValue != null && "11".Equals(sModule)
                            && "PK".Equals(dtColumnSelect.Rows[i][DBColumnSimpleEntity.SqlString.KeyType].ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            strOneData = strOneData.Replace("@TableField", "@TableId");
                        }
                    }
                    //所有SQL文本累加
                    sbAllSql.Append(strOneData + "\n");
                    if ("1".Equals(dtColumnSelect.Rows[i][_sGridColumnIsMust].ToString()) && dtColumnSelect.Columns.Contains(sMustColumnName))
                    {
                        sbAllMust.AppendLine("\""+ dtColumnSelect.Rows[i][sMustColumnName].ToString()+"\",");
                    }
                }
                rtbResult.Clear();

                if (ckbRemoveLastChar.Checked)
                {
                    //列信息去掉最后一个字符
                    string sLast = sbAllSql.ToString().Trim();
                    if (!string.IsNullOrEmpty(sLast))
                    {
                        sLast = sLast.Length > 1 ? sLast.Substring(0, sLast.Length - 1) : string.Empty;
                    }
                    sbAllSql.Clear();
                    sbAllSql.Append(sLast);
                    //必填项去掉最后一个字符
                    sLast = sbAllMust.ToString().Trim();
                    if (!string.IsNullOrEmpty(sLast))
                    {
                        sLast = sLast.Length > 1 ? sLast.Substring(0, sLast.Length - 1) : string.Empty;
                    }
                    sbAllMust.Clear();
                    sbAllMust.Append(sLast);
                }

                //二次替换处理
                DataTable dtReplace = dgvOldNewChar.GetBindingTable();
                DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
                if (drReplace.Length > 0)
                {
                    foreach (DataRow dr in drReplace)
                    {
                        sbAllSql.Replace(dr["OLD"].ToString().Trim(), dr["NEW"].ToString().Trim());
                    }
                }

                //YAPI的字符模板处理
                YapiModuleStringDealTemplate(sbAllSql, sbAllMust);

                rtbResult.AppendText(sbAllSql.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());

                //针对无表名，但输入了中文名的新列，加入到
                sFiter = string.Format("{0}='1'", _sGridColumnIsNoCnRemark);
                DataTable dtNameCode = dgvCodeNameCol.GetBindingTable();
                bool isNeedSave = false;
                foreach (DataRow dr in dtSec.Select(sFiter))
                {
                    if (string.IsNullOrEmpty(dr[DBColumnSimpleEntity.SqlString.NameCN].ToString()))
                    {
                        continue;
                    }
                    sFiter = string.Format("{0}='{1}'", DBColumnSimpleEntity.SqlString.Name, dr[DBColumnSimpleEntity.SqlString.Name]);
                    if (dtNameCode.Select(sFiter).Length == 0)
                    {
                        dtNameCode.ImportRow(dr); //对非修改，不是排除列就导入
                        isNeedSave = true;
                        //重新修改其值为已有中文备注
                        dr[_sGridColumnIsNoCnRemark] = "0";
                    }

                }
                if (isNeedSave)
                {
                    codeNameColumn.Save(dtNameCode);//保存新的编码名称
                }

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_TemplateType, cbbModuleString.SelectedValue.ToString(), "【数据字典】模板类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_QueryResultType, cbbQueryResultType.SelectedValue.ToString(), "【数据字典】查询结果类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsPage, ckbIsPage.Checked ? "1" : "0", "【数据字典】是否分页");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsAutoParam, ckbAutoParamQuery.Checked ? "1" : "0", "【数据字典】是否自动参数化查询");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsColumnMust, ckbColumnMust.Checked ? "1" : "0", "【数据字典】参数是否必填");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_QueryConditionParamNameLatest, txbParamName.Text.Trim(), "【数据字典】查询参数名");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_QueryConditionParamColumnLatest, txbParamColumn.Text.Trim(), "【数据字典】最后设置的参数列");
                WinFormContext.UserLoveSettings.Save();

                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                ShowInfo(StaticValue.GenResultCopySuccessMsg);
                rtbResult.Select(0, 0); //返回到第一行
            }
            catch (Exception ex)
            {
                ShowErr(ex);
            }
        }

        private void YapiModuleStringDealTemplate(StringBuilder sbAllSql, StringBuilder sbAllMust)
        {
            string sModule = cbbModuleString.SelectedValue.ToString();
            string sTemplate = string.Empty;
            if ("1".Equals(sModule))
            {
                //查询条件
                sTemplate = ckbIsPage.Checked ? _dicYapiTemplate[YapiString.ConditionPage] : _dicYapiTemplate[YapiString.ConditionNoPage];
            }
            else if ("2".Equals(sModule))
            {
                //查询结果
                if (cbbQueryResultType.SelectedValue== null || "object".Equals(cbbQueryResultType.SelectedValue.ToString()))
                {
                    sTemplate = ckbIsPage.Checked ? _dicYapiTemplate[YapiString.ResultPage] : _dicYapiTemplate[YapiString.ResultNoPage];
                }
                else
                {
                    sTemplate = ckbIsPage.Checked ? _dicYapiTemplate[YapiString.ResultArrayPage] : _dicYapiTemplate[YapiString.ResultArrayNoPage];
                }
            }
            if ("1".Equals(sModule) || "2".Equals(sModule))
            {
                sTemplate = sTemplate.Replace("#COLUMN_DESC#", sbAllSql.ToString()).Replace("#COLUMN_MUST#", sbAllMust.ToString());
                sbAllSql.Clear();
                sbAllSql.append(sTemplate);
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

        #region 查找按钮事件
        private void btnFind_Click(object sender, EventArgs e)
        {
            FindAllColumnGridText(true);
        }

        private void btnFindCommon_Click(object sender, EventArgs e)
        {
            FindCommonColumnGridText(true);
        }

        private void btnFindCodeName_Click(object sender, EventArgs e)
        {
            FindCodeNameColumnGridText(true);
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            FindGridText(true);
        }

        private void btnFindFront_Click(object sender, EventArgs e)
        {
            FindGridText(false);
        }

        private void FindGridText(bool isNext)
        {
            string sSearch = txbSearchTableName.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvTableList.SeachText(sSearch, ref dgvFindText, null, isNext,ckbTableFixed.Checked);
            lblFind.Text = dgvFindText.CurrentMsg;
        }

        private void FindAllColumnGridText(bool isNext)
        {
            string sSearch = txbSearchCol.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvColList.SeachText(sSearch, ref dgvFindTextAllColumn, null, isNext);
            lblColumnInfo.Text = dgvFindTextAllColumn.CurrentMsg;
        }

        private void FindCommonColumnGridText(bool isNext)
        {
            string sSearch = txbSearchCommon.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvCommonCol.SeachText(sSearch, ref dgvFindTextCommonCol, null, isNext);
            lblCommonColumnInfo.Text = dgvFindTextCommonCol.CurrentMsg;
        }

        private void FindCodeNameColumnGridText(bool isNext)
        {
            string sSearch = txbSearchCodeName.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvCodeNameCol.SeachText(sSearch, ref dgvFindTextCodeNameCol, null, isNext);
            lblCodeNameInfo.Text = dgvFindTextCodeNameCol.CurrentMsg;
        }
        #endregion

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
                    //commonColumn.Save(dtSave);
                    foreach (DataRow item in dtSave.Rows)
                    {
                        if (string.IsNullOrEmpty(item[DataStandardStr.Id].ToString()))
                        {
                            item[DataStandardStr.Id] = Guid.NewGuid().ToString();
                        }
                    }
                    dataCfg.XmlConfig.Save(dtSave);
                    ShowInfo("保存成功！");
                }
            }
            else
            {
                ShowInfo("没有要保存的数据！");
            }
        }

        /// <summary>
        /// 保存编码名称列配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCodeName_Click(object sender, EventArgs e)
        {
            DataTable dtSave = dgvCodeNameCol.GetBindingTable();
            if (dtSave.Rows.Count > 0)
            {
                if (MsgHelper.ShowYesNo("确定要保存？") == DialogResult.Yes)
                {
                    codeNameColumn.Save(dtSave);
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
                    DataTable dtMain = dgvInput.GetBindingTable();
                    if(!ckbIsAppendCol.Checked)
                    {
                        dtMain.Clear(); //非追加，则清除所有数据
                    }
                    
                    int iRow = 0;
                    int iColumn = 0;
                    object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
 
                    foreach (DataRow dr in dtMain.Select(_sInputColCode+ " is null or "+ _sInputColCode + "=''"))
                    {
                        dtMain.Rows.Remove(dr);
                    }
                    dtMain.AcceptChanges();

                    //判断是否要加上列名称
                    if (iColumn <2)
                    {
                        //粘贴列数量小于2，即只有一列，那么移除列名称
                        if(dtMain.Columns.Contains(_sInputColName))
                        {
                            dtMain.Columns.Remove(_sInputColName);
                        }
                    }
                    else
                    {
                        //粘贴列数量大于等于2，需要增加列名称
                        if (!dtMain.Columns.Contains(_sInputColName))
                        {
                            dtMain.Columns.Add(_sInputColName);
                        }
                    }

                    int rowindex = dtMain.Rows.Count;
                    int iGoodDataNum = 0;//有效数据号
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string sColName = string.Empty;
                        if (iColumn >= 2)
                        {
                            sColName = data[j, 1].ToString().Trim();
                        }

                        if (string.IsNullOrEmpty(strData))
                        {
                            continue;
                        }
                        if (dtMain.Select(_sInputColCode + "='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.NewRow());
                            dtMain.Rows[rowindex + iGoodDataNum][0] = strData;
                            if (dtMain.Columns.Contains(_sInputColName))
                            {
                                dtMain.Rows[rowindex + iGoodDataNum][1] = sColName;
                            }
                            iGoodDataNum++;
                        }
                    }
                    dgvInput.ShowRowNum(true);
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
            label3.Focus();
            DataRow dataRow = dgvColList.GetCurrentRow();
            if (dataRow == null) return;
            dgvCommonCol.GetBindingTable().ImportRow(dataRow);
        }

        /// <summary>
        /// 加入通过列右键按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCodeName_Click(object sender, EventArgs e)
        {
            label3.Focus();
            DataRow dataRow = dgvSelect.GetCurrentRow();
            if (dataRow == null) return;
            if (string.IsNullOrEmpty(dataRow[DBColumnSimpleEntity.SqlString.NotNull].ToString()))
            {
                dataRow[DBColumnSimpleEntity.SqlString.NotNull] = "0";
            }
            dgvCodeNameCol.GetBindingTable().ImportRow(dataRow);
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
            if (dataRow == null || dataRow.RowState == DataRowState.Detached) return;
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

        private void btnRemoveSelectCodeName_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvCodeNameCol.GetBindingTable();
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
            if (dataRow == null || dataRow.RowState == DataRowState.Detached) return;
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

        /// <summary>
        /// 模板类型选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbModuleString_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sLower = DBColumnSimpleEntity.SqlString.NameLower;
            string sName = DBColumnSimpleEntity.SqlString.Name;

            if (cbbModuleString.SelectedValue != null && !string.IsNullOrEmpty(cbbModuleString.SelectedValue.ToString()))
            {
                string sModule = cbbModuleString.SelectedValue.ToString();
                txbStringTemplateName.ReadOnly = true;
                rtbConString.Clear();
                txbStringTemplateName.Text = string.Empty;
                string sTemplate = string.Empty;
                if (!"13".Equals(sModule))
                {
                    tpIn.Parent = null;
                    tpDate.Parent = null;
                    tpTemplate.Text = "字符模板";
                    lblParamName.Visible = false;
                    lblParamColumn.Visible = false;
                    txbParamName.Visible = false;
                    txbParamColumn.Visible = false;
                }

                if ("11".Equals(sModule))
                {
                    //11：Mybatis表实体属性
                    sTemplate = _dicYapiTemplate[StringTemplate.MyBatisTableEntity];
                    rtbConString.AppendText(sTemplate);
                    toolTip1.SetToolTip(cbbModuleString, "生成表实体属性(不含GET和SET，如需要请自行在IDE中生成)。");
                    ckbRemoveLastChar.Checked = false;
                }
                else if ("12".Equals(sModule))
                {
                    //12：通用实体属性
                    sTemplate = _dicYapiTemplate[StringTemplate.CommonEntity];
                    rtbConString.AppendText(sTemplate);
                    toolTip1.SetToolTip(cbbModuleString, "生成通用实体属性(不含GET和SET，如需要请自行在IDE中生成)。");
                    ckbRemoveLastChar.Checked = false;
                }
                else if ("13".Equals(sModule))
                {
                    //13：Mybatis查询条件
                    ResetQueryParam();//重新设置文本
                    tpIn.Parent = tabStringTemplate;
                    tpDate.Parent = tabStringTemplate;
                    tpTemplate.Text = "If";
                    toolTip1.SetToolTip(cbbModuleString, "当列为DateTime或Date类型时，会使用Date模板拼接；\n当列名以_LIST结尾时，会使用In模板拼接；\n其他以If模板接接。");
                    ckbRemoveLastChar.Checked = false;
                    lblParamName.Visible = true;
                    lblParamColumn.Visible = true;
                    txbParamName.Visible = true;
                    txbParamColumn.Visible = true;
                }
                else if ("1".Equals(sModule) || "2".Equals(sModule) || "9".Equals(sModule))
                {
                    //1：YAPI查询条件，2：YAPI查询结果，9：YAPI参数格式
                    sTemplate = _dicYapiTemplate[StringTemplate.YapiColumnDesc];
                    rtbConString.AppendText(sTemplate);
                    if (!"9".Equals(sModule))
                    {
                        ckbRemoveLastChar.Checked = true;
                    }
                    else
                    {
                        ckbRemoveLastChar.Checked = false;
                    }
                    toolTip1.SetToolTip(cbbModuleString, "生成JSON格式的YAPI入参、出参、中间参数部分信息。");
                }
                else
                {
                    if (_dicYapiTemplate.ContainsKey(sModule))
                    {
                        rtbConString.AppendText(_dicYapiTemplate[sModule]);
                    }
                    txbStringTemplateName.Text = cbbModuleString.Text.Trim();
                    txbStringTemplateName.ReadOnly = false;
                    toolTip1.SetToolTip(cbbModuleString, "这是自定义的字符模板，可修改或删除。");
                }
            }
            else
            {
                txbStringTemplateName.ReadOnly = false;
            }
        }

        /// <summary>
        /// 重新设置文本
        /// </summary>
        private void ResetQueryParam()
        {
            rtbConString.Clear();
            rtbIn.Clear();
            rtbDate.Clear();
            string sNewParmName = txbParamName.Text.Trim() + ".";
            string sNewParmColumn = txbParamColumn.Text.Trim();
            string sCon = _dicYapiTemplate[MyBatisDynamicCond.If].Replace(_moduleParamName + ".", sNewParmName);
            string sIn = _dicYapiTemplate[MyBatisDynamicCond.In].Replace(_moduleParamName + ".", sNewParmName);
            string sDate = _dicYapiTemplate[MyBatisDynamicCond.Date].Replace(_moduleParamName + ".", sNewParmName);
            if (!string.IsNullOrEmpty(sNewParmColumn))
            {
                //先替换包括时间的字段
                if ("#C#".equals(sNewParmColumn))
                {
                    sDate = sDate.Replace(_moduleColumnName + "Begin", sNewParmColumn + "_BEGIN");
                }
                else if ("#C2#".equals(sNewParmColumn)|| "#C3#".equals(sNewParmColumn))
                {
                    sDate = sDate.Replace(_moduleColumnName + "Begin", sNewParmColumn + "Begin");
                }
                else
                {

                }
                sCon = sCon.Replace(_moduleColumnName, sNewParmColumn);
                sIn = sIn.Replace(_moduleColumnName, sNewParmColumn);
                sDate = sDate.Replace(_moduleColumnName, sNewParmColumn);
            }
            rtbConString.AppendText(sCon);
            rtbIn.AppendText(sIn);
            rtbDate.AppendText(sDate);
        }

        private void cbbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbInputType.SelectedValue != null)
            {
                string sType = cbbInputType.SelectedValue.ToString();
                if("1".Equals(sType))
                {
                    //匹配SQL的查询条件
                    spcQuerySql.Panel1Collapsed = false;  //设计上方不折叠
                    ckbOnlyMatchQueryResult.Checked = true;
                    grbInputSql.Text = "查询SQL";
                    ckbOnlyMatchQueryResult.Text = "仅匹配SQL中的条件参数名称";
                    toolTip1.SetToolTip(cbbInputType, "根据参数字符来匹配所有列，支持#{param.colName}、#{colName}、@colName、:colName、#colName#格式");
                    toolTip1.SetToolTip(ckbOnlyMatchQueryResult, "选中后点【匹配】按钮会清空【粘贴或查询的列】网格");
                    cbbModuleString.SelectedValue = "1"; //YAPI查询条件
                    ckbIsAppendCol.Visible = false;
                }
                else if ("2".Equals(sType))
                {
                    //匹配SQL的查询结果
                    spcQuerySql.Panel1Collapsed = false;  //设计上方不折叠
                    ckbOnlyMatchQueryResult.Checked = true;
                    grbInputSql.Text = "查询SQL";
                    ckbOnlyMatchQueryResult.Text = "仅匹配查询结果中所有列名";
                    toolTip1.SetToolTip(cbbInputType, "根据查询的SQL来得到所有列（注：SQL必须运行不报错，且最好是查询空数据）");
                    toolTip1.SetToolTip(ckbOnlyMatchQueryResult, "选中后点【匹配】按钮会清空【粘贴或查询的列】网格");
                    cbbModuleString.SelectedValue = "2";//YAPI查询结果
                    ckbIsAppendCol.Visible = false;
                }
                else
                {
                    //粘贴列
                    spcQuerySql.Panel1Collapsed = true;  //设计上方折叠
                    ckbOnlyMatchQueryResult.Checked = true;
                    ckbOnlyMatchQueryResult.Text = "重新匹配";
                    toolTip1.SetToolTip(cbbInputType, "粘贴列编码");
                    toolTip1.SetToolTip(ckbOnlyMatchQueryResult, "选中后点【匹配】按钮会清空【已选择】网格后，重新根据粘贴的列匹配！");
                    toolTip1.SetToolTip(ckbIsAppendCol, "选中后支持多次复制追加数据");
                    ckbIsAppendCol.Visible = true;
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
            if (e.ColumnIndex == dgvSelect.Columns[_sGridColumnSelect].Index)
            {
                foreach (DataGridViewRow item in dgvSelect.Rows)
                {
                    item.Cells[_sGridColumnSelect].Value = _allSelect ? "1" : "0";
                }
                _allSelect = !_allSelect;
            }
            else if(e.ColumnIndex == dgvSelect.Columns[_sGridColumnIsMust].Index)
            {
                foreach (DataGridViewRow item in dgvSelect.Rows)
                {
                    item.Cells[_sGridColumnIsMust].Value = _allSelectColumnIsMust ? "1" : "0";
                }
                _allSelectColumnIsMust = !_allSelectColumnIsMust;
            }
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

        private void tsmiTableRemove_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvTableList.GetBindingTable();
            //得到表字符清单
            SortedSet<string> set = new SortedSet<string>();
            for (int i=0;i < dgvTableList.SelectedCells.Count;i++)
            {
                set.Add(dgvTableList.Rows[dgvTableList.SelectedCells[i].RowIndex].Cells[DBTableEntity.SqlString.Name].Value.ToString());
            }

            foreach (string sTable in set)
            {
                string sFiter = string.Format("{0}='{1}'", DBTableEntity.SqlString.Name, sTable);
                DataRow[] drArr = dt.Select(sFiter);
                foreach (DataRow dr in drArr)
                {
                    dt.Rows.Remove(dr);
                }
            }
        }

        private void ckbNotIncludeSplitTable_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvTableList.GetBindingTable();
            ExcludeSplitTable(dt);
        }

        /// <summary>
        /// 选择列网格编辑后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSelect_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex== dgvSelect.Columns[DBColumnSimpleEntity.SqlString.Name].Index)
            {
                dgvSelect.Rows[e.RowIndex].Cells[DBColumnSimpleEntity.SqlString.NameUpper].Value = dgvSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().FirstLetterUpper();
                dgvSelect.Rows[e.RowIndex].Cells[DBColumnSimpleEntity.SqlString.NameLower].Value = dgvSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().FirstLetterUpper(false);
            }
        }

        private void btnExcludeTable_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbExcludeTable.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            DataTable dtFtpFile = dgvTableList.GetBindingTable();
            if (dtFtpFile.Rows.Count == 0) return;

            //先把所有表未选中的先选中
            DataRow[] sOldSelectArr = dtFtpFile.Select(_sGridColumnSelect + "='0'");
            foreach (DataRow sRow in sOldSelectArr)
            {
                sRow[_sGridColumnSelect] = "1"; //设置为选中
            }

            var query = from f in dtFtpFile.AsEnumerable()
                        where GetLinqDynamicWhere(sFilter, f)
                        select f;
            foreach (var item in query.ToList())
            {
                item[_sGridColumnSelect] = "0"; //设置为不选中
            }

            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_IsAutoExcludeTable, ckbIsAutoExclude.Checked ? "1" : "0", "【数据字典】是否自动排除表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ColumnDic_ExcludeTableList, txbExcludeTable.Text.Trim(), "【数据字典】排除表列表");
            WinFormContext.UserLoveSettings.Save();
        }

        private static bool GetLinqDynamicWhere(string[] filterArr,DataRow drF)
        {
            foreach (var item in filterArr)
            {
                string sFilePath = drF.Field<string>(DBTableEntity.SqlString.Name);
                if (sFilePath.EndsWith(item))
                {
                    return true;
                }
            }
            return false;
        }

        private void dgvCodeNameCol_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCodeNameCol.Columns[DBColumnSimpleEntity.SqlString.Name].Index)
            {
                dgvCodeNameCol.Rows[e.RowIndex].Cells[DBColumnSimpleEntity.SqlString.NameUpper].Value = dgvCodeNameCol.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().FirstLetterUpper();
                dgvCodeNameCol.Rows[e.RowIndex].Cells[DBColumnSimpleEntity.SqlString.NameLower].Value = dgvCodeNameCol.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().FirstLetterUpper(false);
            }
        }

        #region 字符替换模板加载和保存相关
        private void btnSaveReplaceTemplate_Click(object sender, EventArgs e)
        {
            string sTempName = txbReplaceTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("模板名称不能为空！");
                return;
            }
            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            dtReplace.DeleteNullRow();
            if (dtReplace.Rows.Count == 0)
            {
                ShowInfo("请录入要替换的新旧字符！");
                return;
            }

            if (ShowOkCancel("确定要保存模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceStringData.MoreXmlConfig.ValData;

            string sKeyIdNew = string.Empty;
            bool isAdd = string.IsNullOrEmpty(cbbTemplateType.Text.Trim()) ? true : false;
            if (isAdd)
            {
                //新增
                sKeyIdNew = Guid.NewGuid().ToString();
                DataRow dr = dtKeyConfig.NewRow();
                dr[sKeyId] = sKeyIdNew;
                dr[ReplaceStringXmlConfig.KeyString.Name] = sTempName;
                dtKeyConfig.Rows.Add(dr);
            }
            else
            {
                //修改
                string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
                sKeyIdNew = sKeyIDValue;
                DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                if (drArrKey.Length == 0)
                {
                    DataRow dr = dtKeyConfig.NewRow();
                    dr[sKeyId] = sKeyIdNew;
                    dr[ReplaceStringXmlConfig.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][ReplaceStringXmlConfig.KeyString.Name] = sTempName;//修改名称
                }
                if (drArrVal.Length > 0)
                {
                    foreach (DataRow dr in drArrVal)
                    {
                        dtValConfig.Rows.Remove(dr);
                    }
                    dtValConfig.AcceptChanges();
                }
            }

            foreach (DataRow dr in dtReplace.Rows)
            {
                DataRow drNew = dtValConfig.NewRow();
                drNew[sValId] = Guid.NewGuid().ToString();
                drNew[sKeyId] = sKeyIdNew;
                drNew[ReplaceStringXmlConfig.ValueString.IsSelected] = dr[ReplaceStringXmlConfig.ValueString.IsSelected].ToString();
                drNew[ReplaceStringXmlConfig.ValueString.OldString] = dr[ReplaceStringXmlConfig.ValueString.OldString].ToString();
                drNew[ReplaceStringXmlConfig.ValueString.NewString] = dr[ReplaceStringXmlConfig.ValueString.NewString].ToString();
                dtValConfig.Rows.Add(drNew);
            }
            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, ReplaceStringXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板保存成功！");
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null)
            {
                ShowInfo("请选择一个模板！");
                return;
            }
            string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue))
            {
                ShowInfo("请选择一个模板！");
                return;
            }

            if (ShowOkCancel("确定要删除该模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceStringData.MoreXmlConfig.ValData;
            DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
            DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");

            if (drArrVal.Length > 0)
            {
                foreach (DataRow dr in drArrVal)
                {
                    dtValConfig.Rows.Remove(dr);
                }
                dtValConfig.AcceptChanges();
            }

            if (drArrKey.Length > 0)
            {
                foreach (DataRow dr in drArrKey)
                {
                    dtKeyConfig.Rows.Remove(dr);
                }
                dtKeyConfig.AcceptChanges();
            }
            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, ReplaceStringXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板删除成功！");
        }

        private void dgvOldNewChar_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvOldNewChar.Columns[e.ColumnIndex].Name == _sGridColumnSelect)
            {
                return;
            }
        }

        private void dgvOldNewChar_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvOldNewChar, ref _allSelectOldNewChar, e);
        }

        private void dgvOldNewChar_KeyDown(object sender, KeyEventArgs e)
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

                    int iRow = 0;
                    int iColumn = 0;
                    Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                    if (iRow == 0 || iColumn < 2)
                    {
                        return;
                    }

                    DataTable dtMain = dgvOldNewChar.GetBindingTable();
                    dtMain.Rows.Clear();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string strData2 = data[j, 1].ToString().Trim();
                        if (string.IsNullOrEmpty(strData) || string.IsNullOrEmpty(strData2))
                        {
                            continue;
                        }

                        if (dtMain.Select("OLD='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.Rows.Count + 1, "1", strData, strData2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void cbbTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null) return;
            string sTempType = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sTempType))
            {
                //txbReplaceTemplateName.ReadOnly = false;
                txbReplaceTemplateName.Text = string.Empty;
                return;
            }

            txbReplaceTemplateName.Text = cbbTemplateType.Text;
            //txbReplaceTemplateName.ReadOnly = true;
            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            DataRow[] drArr = replaceStringData.MoreXmlConfig.ValData.Select(sKeyId + "='" + sTempType + "'");

            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            if (drArr.Length > 0)
            {
                dtReplace.Rows.Clear();
                foreach (DataRow dr in drArr)
                {
                    dtReplace.Rows.Add(dtReplace.Rows.Count + 1, 
                        dr[ReplaceStringXmlConfig.ValueString.IsSelected].ToString(), 
                        dr[ReplaceStringXmlConfig.ValueString.OldString].ToString(), 
                        dr[ReplaceStringXmlConfig.ValueString.NewString].ToString());
                }
            }
            else if (dtReplace != null)
            {
                dtReplace.Clear();
            }
        }
        #endregion

        #region 字符模板维护
        private void btnStringTemplateSave_Click(object sender, EventArgs e)
        {
            DataTable dtSave = stringTemplate.Data;
            string sCurTemplateName = string.Empty;
            string sFilter = string.Empty;
            string sTemplateString = rtbConString.Text.Trim();
            DataRow drNewOrEdit = null;
            string sCurTemplateId;
            bool isAdd = false;
            if (string.IsNullOrEmpty(sTemplateString))
            {
                ShowInfo("请输入模板字符！");
                return;
            }
            if (cbbModuleString.SelectedValue == null || string.IsNullOrEmpty(cbbModuleString.Text.Trim()))
            {
                //新增场景
                sCurTemplateName = txbStringTemplateName.Text.Trim();
                if (string.IsNullOrEmpty(sCurTemplateName))
                {
                    ShowInfo("请输入模板名称！");
                    return;
                }
                if (_dicSystemStringTemplate.ContainsKey(sCurTemplateName) || _dicSystemStringTemplate.Values.Contains(sCurTemplateName))
                {
                    ShowInfo("模板名称不能跟现有的系统模板重复，请修改模板名称！");
                    return;
                }
                sFilter = StringTemplateConfig.Name + "='" + sCurTemplateName + "'";
                DataRow[] drArr = dtSave.Select(sFilter);
                if (drArr.Length > 0)
                {
                    ShowInfo("模板名称不能跟现有的系统模板重复，请修改模板名称！");
                    return;
                }
                //增加行
                sCurTemplateId = Guid.NewGuid().ToString();
                drNewOrEdit = dtSave.Rows.Add(sCurTemplateId, sCurTemplateName, sTemplateString);
                isAdd = true;
            }
            else
            {
                //修改场景
                sCurTemplateId = cbbModuleString.SelectedValue.ToString();
                sCurTemplateName = txbStringTemplateName.Text.Trim();
                if (string.IsNullOrEmpty(sCurTemplateName))
                {
                    ShowInfo("请输入模板名称！");
                    return;
                }
                sFilter = StringTemplateConfig.Id + "='" + sCurTemplateId + "'";
                DataRow[] drArr = dtSave.Select(sFilter);
                if (drArr.Length > 0)
                {
                    drArr[0][StringTemplateConfig.Name] = sCurTemplateName;
                    drArr[0][StringTemplateConfig.TemplateString] = sTemplateString;
                    drNewOrEdit = drArr[0];
                }
            }

            if (MsgHelper.ShowYesNo("确定要保存？") == DialogResult.Yes)
            {
                stringTemplate.Save(dtSave);
                ShowInfo("保存成功！");
                //修改全局模板文本内容
                _dicYapiTemplate[sCurTemplateId] = drNewOrEdit[StringTemplateConfig.TemplateString].ToString();
                DataTable dtStringTemaplate = cbbModuleString.DataSource as DataTable;
                //修改模板下拉框的数据源
                if (isAdd)
                {
                    //增加下拉框项
                    dtStringTemaplate.Rows.Add(drNewOrEdit[StringTemplateConfig.Id].ToString(), drNewOrEdit[StringTemplateConfig.Name].ToString());
                }
                else
                {
                    //修改模板下拉框中的模板名称
                    sFilter = DT_BAS_VALUE.VALUE_CODE + "='" + sCurTemplateId + "'";
                    DataRow[] drArr = dtStringTemaplate.Select(sFilter);
                    if (drArr.Length > 0)
                    {
                        drArr[0][DT_BAS_VALUE.VALUE_NAME] = sCurTemplateName;
                    }
                }
            }
        }

        private void btnStringTemplateDelete_Click(object sender, EventArgs e)
        {
            if (cbbModuleString.SelectedValue == null || string.IsNullOrEmpty(cbbModuleString.Text.Trim()))
            {
                ShowInfo("请选择要删除的模板！");
                return;
            }
            string sTemplateId = cbbModuleString.SelectedValue.ToString();
            if (_dicSystemStringTemplate.ContainsKey(sTemplateId))
            {
                ShowInfo("系统自带的模板不能删除！");
                return;
            }

            string sFilter = StringTemplateConfig.Id + "='" + sTemplateId + "'";
            DataRow[] drArr = stringTemplate.Data.Select(sFilter);
            if (drArr.Length > 0)
            {
                if (MsgHelper.ShowYesNo("确定要删除？") == DialogResult.No)
                {
                    return;
                }
                stringTemplate.Data.Rows.Remove(drArr[0]);
                stringTemplate.Save();
                ShowInfo("删除成功！");
                //重新绑定下拉框
                DataTable dtStringTemaplate = cbbModuleString.DataSource as DataTable;
                sFilter = DT_BAS_VALUE.VALUE_CODE + "='" + sTemplateId + "'";
                DataRow[] drArrCbb = dtStringTemaplate.Select(sFilter);
                if (drArrCbb.Length > 0)
                {
                    dtStringTemaplate.Rows.Remove(drArrCbb[0]);
                }
            }
            else
            {
                ShowInfo("未找到要删除的数据！");
            }
        }
        #endregion

        private void dgvSelect_KeyDown(object sender, KeyEventArgs e)
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

                    int iRow = 0;
                    int iColumn = 0;
                    Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                    if (iRow == 0 || iColumn < 2)
                    {
                        return;
                    }

                    DataTable dtMain = dgvSelect.GetBindingTable();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string strData2 = data[j, 1].ToString().Trim();
                        if (string.IsNullOrEmpty(strData) || string.IsNullOrEmpty(strData2))
                        {
                            continue;
                        }

                        DataRow[] drArr = dtMain.Select(DBColumnSimpleEntity.SqlString.Name + "='" + strData + "'");
                        if (drArr.Length > 0)
                        {
                            drArr[0][DBColumnSimpleEntity.SqlString.NameCN] = strData2;
                        }
                        else
                        {
                            drArr = dtMain.Select(DBColumnSimpleEntity.SqlString.Name + "='" + strData2 + "'");
                            if (drArr.Length > 0)
                            {
                                drArr[0][DBColumnSimpleEntity.SqlString.NameCN] = strData;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void txbParamName_Leave(object sender, EventArgs e)
        {
            ResetQueryParam();
        }

        private void txbParamColumn_Leave(object sender, EventArgs e)
        {
            ResetQueryParam();
        }

        private void dgvTableList_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            cmsRemoveSelect.Visible = false;
        }

        private void dgvSelect_MouseDown(object sender, MouseEventArgs e)
        {
            tsmiAddCodeName.Visible = true; //显示【加入编码名称】右键菜单
        }

        private void dgvInput_MouseDown(object sender, MouseEventArgs e)
        {
            tsmiAddCodeName.Visible = false; //隐藏【加入编码名称】右键菜单
        }

        private void dgvCodeNameCol_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvCodeNameCol, ref _allSelectNameCode, e);
        }
    }

    public enum MybatisStringType
    {
        If,
        List,
        Datetime
    }
}