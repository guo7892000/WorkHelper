using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.IBLL;
using System.Linq;
using Breezee.AutoSQLExecutor.Core;
using Breezee.AutoSQLExecutor.Common;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using System.Collections.Generic;
using static Breezee.WorkHelper.DBTool.Entity.DBTGlobalValue;
using System.ComponentModel;
using static Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL.EntTable;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 生成表结构SQL
    /// </summary>
    public partial class FrmDBTExcelGenerateTableSQL : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";
        //数据库类型
        private DataBaseType importDBType;//导入数据库类型
        private DataBaseType targetDBType;//目标数据库类型
        private bool _isAllConvert = false;//是否综合转换，即导入一种数据库模块，而生成另一种数据库类型
        private bool _allSelectOldNewChar = false;//默认全选，这里取反
        //
        private DataSet _dsExcelData;
        //
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        private string _strImportSuccess = "导入成功！可点“生成SQL”按钮得到本次导入的变更SQL。";
        private string strTipInfo = "不需要的行，请选择整行后，按Delete键即可删除！";

        //生成过程用到的全局变量
        private StringBuilder sbSql = new StringBuilder();
        //private StringBuilder sbRemark = new StringBuilder();
        private DataTable dtTable;
        private DataTable dtAllCol;

        private string createType;
        private string _ImportInput = "1";
        private string _ImportInputLY = "3";
        private readonly string _sGridTableSelect = "选择";
        private readonly string _sGridColumnSelect = "选择";
        private readonly string _sGridColumnSelectEn = "IsSelect";
        private bool _allTableSelect = false;//默认全选，这里取反
        private bool _allColumnSelect = false;//默认全选，这里取反
        DataGridViewFindText dgvFindText;

        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;

        MiniXmlConfig commonColumn;
        StandardColumnClassXmlConfig replaceStringData;//标准列分类配置
        #endregion

        #region 构造函数
        public FrmDBTExcelGenerateTableSQL()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmDBTImportExcelGenerateTableSQL_Load(object sender, EventArgs e)
        {
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable(true);
            //目标数据库类型
            cbbTargetDbType.BindTypeValueDropDownList(dtDbType, false, true);
            //导入数据库类型
            cbbImportDBType.BindTypeValueDropDownList(dtDbType, false, true);
            
            //创建方式
            _dicString.Add(((int)SQLCreateType.Create).ToString(), "不判断增加");
            _dicString.Add(((int)SQLCreateType.Drop_Create).ToString(), "先删后增加");
            _dicString.Add(((int)SQLCreateType.Drop).ToString(), "生成删除SQL");
            cbbCreateType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            _dicString.Clear();
            //录入方式
            _dicString.Add(_ImportInput, "模板导入");
            _dicString.Add("2", "读取数据库");
            _dicString.Add(_ImportInputLY, "LY模板导入");
            cbbInputType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //设置表、列的删除提示
            lblTableData.Text = strTipInfo;
            lblColumnInfo.Text = strTipInfo;
            //ckbAllConvert.Checked = true; //为了减少工作量，统一使用模板中的【列】页签来生成表SQL

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.IsAsyncLoadOracleDefaultValue = true; //要异步加载默认值
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.DBConnName_SelectedIndexChanged += cbbDBConnName_SelectedIndexChanged;
            uC_DbConnection1.ColumnDefaultValue_LoadCompleted += columnDefaultValue_LoadCompleted;
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            #endregion
            //加载用户喜好值
            cbbInputType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_InputType, _ImportInput).Value;
            cbbTargetDbType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_TargetDbType, ((int)DataBaseType.MySql).ToString()).Value;
            ckbExcludeColumn.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsExcludeColumn, "1").Value) ? true : false;
            ckbQueryColumnRealTime.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_QueryColumnRealTime, "0").Value) ? true : false;
            txbExcludeColumn.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_ExcludeColumnList, "").Value;
            ckbFullTypeDoc.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsFullType, "0").Value) ? true : false;
            ckbLYTemplate.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsLYTemplate, "0").Value) ? true : false;
            ckbOnlyRemark.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsOnlyRemark, "0").Value) ? true : false;
            ckbDefaulePK.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsDefaultPK, "0").Value) ? true : false;
            ckbDefaultColNameCn.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_IsDefaultColNameCN, "0").Value) ? true : false;
            txbDefaultColNameCN.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GenerateTableSQL_DefaultColNameCN, "未知").Value;
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lblClassInfo.Text = "当【分类选择】不为空时，生成时所有表都会加上该分类的所有标准列信息";
            toolTip1.SetToolTip(ckbQueryColumnRealTime, "当选中时，会实时准确的查询表和列信息，速度较慢。\n如原库表结构没有变化，就不建议选中！");
            toolTip1.SetToolTip(ckbOnlyRemark, "选中时，表示备注里已包含了表或列名称，将只使用备注作为表或列的注释；\n否则使用列名称+备注作为表或列的备注！");
            toolTip1.SetToolTip(ckbDefaulePK, "当选中时，某些表没有主键时，则默认以第一行的列作为主键！");
            toolTip1.SetToolTip(ckbDefaultColNameCn, "当选中时，某些表或列没有中文名称时，\n会以其后的名称作为名称 +“表”或“列”来生成SQL。\n后续请自行修改这些名称！");
            toolTip1.SetToolTip(ckbUpdateDefault, "该选项只针对Oracle。当选中时，会在查询完列信息后，异步查询和更新全局默认值信息。\n否则都是使用应用启动后，第一次查询所在库的默认值！");
            toolTip1.SetToolTip(ckbDoubleColName, "当选中时，多显示几列【列编码、列名称、表编码】。\n作用：当新字段名修改时，还有旧字段信息给数据迁移参考！");
            toolTip1.SetToolTip(ckbColNameSameRemark, "当选中时，列名和备注都将使用【列名称：列说明】！");
            string sImpRemark = "导入数据库类型，使用Excel导入时先选择；\n查询数据时，会自动带出，不能手工选择！";
            toolTip1.SetToolTip(cbbImportDBType, sImpRemark);
            toolTip1.SetToolTip(label2, sImpRemark);
            toolTip1.SetToolTip(cbbTargetDbType, "目标数据库类型，即生成哪种类型数据库的SQL或文档！");
            toolTip1.SetToolTip(ckbLYTemplate, "当选中时，生成的文档会以LY模板方式呈现，然后我们可以复制出来，\n并粘贴到数据库变更文档中做数据库变更申请！");
            toolTip1.SetToolTip(ckbFullTypeDoc, "当选中时，生成的文档中，列类型包括类型、长度或精度信息！");
            toolTip1.SetToolTip(cbbCreateType, "生成的SQL类型，如新增表、修改表、删除表等！"); 
            //加载通用列数据
            LoadCommonColumnData();
            //增加折叠功能
            gbTable.AddFoldRightMenu();
            groupBox5.AddFoldRightMenu();
            groupBox1.AddFoldRightMenu();

        }

        /// <summary>
        /// 默认值加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnDefaultValue_LoadCompleted(object sender, EventArgs e)
        {
            DataTable dtDefault;
            if (UC_DbConnection.defaultValueDic.TryGetValue(uC_DbConnection1.LatestDbServerInfo.DbConnKey, out dtDefault))
            {
                if (dtDefault.Rows.Count == 0) return;

                if(uC_DbConnection1.LatestDbServerInfo.DatabaseType!= DataBaseType.Oracle)
                {
                    return; //目前只有Oracle需要异步加载默认值
                }

                DataTable dtAllColumns = dgvColList.GetBindingTable();
                if (dtAllColumns == null) return;

                dtAllColumns = dtAllColumns.Copy();
                try
                {
                    ShowDestopTipMsg("正在异步加载列的默认值信息...");
                    //使用LINQ：速度比较快
                    var query = from f in dtDefault.AsEnumerable()
                                join s in dtAllColumns.AsEnumerable()
                                on new
                                {
                                    c1 = f.Field<string>(DBColumnEntity.SqlString.TableName),
                                    c2 = f.Field<string>(DBColumnEntity.SqlString.Name)
                                }
                                equals new
                                {
                                    c1 = s.Field<string>(ColCommon.ExcelCol.TableCode),
                                    c2 = s.Field<string>(ColCommon.ExcelCol.Code)
                                }
                                select new { F1 = f, S1 = s };
                    var joinList = query.ToList();
                    foreach (var item in joinList)
                    {
                        item.S1[ColCommon.ExcelCol.Default] = item.F1[DBColumnEntity.SqlString.Default].ToString();
                    }
                    dgvColList.BindAutoColumn(dtAllColumns);
                    ShowDestopTipMsg("加载列的默认值信息完成！");
                }
                catch(Exception ex)
                {
                    ShowDestopTipMsg(ex.Message);
                }
            }
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
                DBColumnSimpleEntity.SqlString.KeyType,
                DBColumnSimpleEntity.SqlString.Comments,
                DBColumnSimpleEntity.SqlString.Extra,
                //DBColumnSimpleEntity.SqlString.TableName,
                //DBColumnSimpleEntity.SqlString.TableNameCN,
                //DBColumnSimpleEntity.SqlString.TableNameUpper
            });
            commonColumn = new MiniXmlConfig(GlobalContext.PathData(), "CommonColumnConfig.xml", list, DBColumnSimpleEntity.SqlString.Name);
            DataTable dtCommonCol = commonColumn.Load();
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelectEn);
            dcSelected.DefaultValue = "1";
            dtCommonCol.Columns.Add(dcSelected);
            //通用列网格跟所有列网格结构一样
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelectEn).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NameLower).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataLength).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(60).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataPrecision).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataScale).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.DataTypeFull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.SortNum).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.NotNull).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Default).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.KeyType).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Comments).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.Extra).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible().Build()
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameCN).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
            //new FlexGridColumn.Builder().Name(DBColumnSimpleEntity.SqlString.TableNameUpper).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
            );
            //
            dgvDataDictionary.Tag = fdc.GetGridTagString();
            dgvDataDictionary.BindDataGridView(dtCommonCol, true);

            //分类网格
            replaceStringData = new StandardColumnClassXmlConfig(DBTGlobalValue.TableSQL.Xml_StandardClassFileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, StandardColumnClassXmlConfig.KeyString.Name, true, true);
            dgvOldNewChar.Tag = fdc.GetGridTagString();
            dgvOldNewChar.BindDataGridView(dtCommonCol.Clone(), true);
            //新表名相关
            DataTable dtTableCopy = EntTable.GetTable();
            dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtTableCopy.Columns.Add(dcSelected);

            dcSelected = new DataColumn(EntTable.ExcelTable.Code + "_OLD");
            dtTableCopy.Columns.Add(dcSelected);
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(EntTable.ExcelTable.Code + "_OLD").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(EntTable.ExcelTable.Code).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(EntTable.ExcelTable.Name).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(EntTable.ExcelTable.Remark).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build()
            );
            //
            dgvNewTableInfo.Tag = fdc.GetGridTagString();
            dgvNewTableInfo.BindDataGridView(dtTableCopy, true);
        }
        private void cbbDBConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;
            if (!_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                //导入类型为查询的数据库类型
                string sDbType = uC_DbConnection1.getSelectedDatabaseType();
                if (!string.IsNullOrEmpty(sDbType))
                {
                    cbbImportDBType.SelectedValue = sDbType;
                    cbbImportDBType.SetControlReadOnly();
                }
            }
            //调用【获取表清单复选框变化事件】
            ckbGetTableList_CheckedChanged(null, null);
        }
        #endregion

        #region 数据库类型下拉框变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {
            //导入类型为查询的数据库类型
            string sDbType = uC_DbConnection1.getSelectedDatabaseType();
            if (!string.IsNullOrEmpty(sDbType))
            {
                cbbImportDBType.SelectedValue = sDbType;
                cbbImportDBType.SetControlReadOnly();

                int iDbType = int.Parse(sDbType);
                //只有Oracle才显示实时更新默认值
                ckbUpdateDefault.Visible = (DataBaseType)iDbType == DataBaseType.Oracle ? true : false;
            }
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
                    ckbGetTableList.Checked = false;
                    return;
                }
                //绑定下拉框
                cbbTableName.BindDropDownList(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", true,false);
                //查找自动完成数据源
                cbbTableName.AutoCompleteCustomSource.AddRange(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
            }
            else
            {
                cbbTableName.DataSource = null;
            }
        }
        #endregion

        #region 导入按钮事件
        private async void tsbImport_Click(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;

            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            ColumnTemplateType templateType;

            if (_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                #region 正常导入模板处理
                //导入
                if (importDBType == DataBaseType.None)
                {
                    ShowInfo("请选择【导入数据库类型】");
                    return;
                }

                _dsExcelData = ExportHelper.GetExcelDataSet();//得到Excel数据
                if (_dsExcelData != null)
                {
                    //绑定表
                    DataTable dtTable = _dsExcelData.Tables[EntExcelSheet.Table];
                    DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtTable.Columns.Add(dcSelected);
                    dtTable.Columns[_sGridTableSelect].SetOrdinal(0);//设置选择列在最前面
                    //移除空行
                    foreach (DataRow dr in dtTable.Select("表编码 is null or 序号 is null"))
                    {
                        dtTable.Rows.Remove(dr);
                    }
                    //绑定网格
                    //bsTable.DataSource = dtTable;
                    //dgvTableList.DataSource = bsTable;
                    dgvTableList.BindAutoColumn(dtTable);
                    dgvTableList.ShowRowNum();
                    //绑定列
                    dtTable = _dsExcelData.Tables[GetExcelSheetNameCol(importDBType)];
                    //默认主键处理
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        if ("PK".Equals(dr[ColCommon.ExcelCol.KeyType].ToString()))
                        {
                            //统一主键处理
                            string sPKName = "PK_" + dr[ColCommon.ExcelCol.TableCode].ToString();
                            if (ckbAllConvert.Checked)
                            {
                                //针对特殊数据的字段赋值
                                switch (importDBType)
                                {
                                    case DataBaseType.SqlServer:
                                    case DataBaseType.MySql:
                                        break;
                                    case DataBaseType.Oracle:
                                        dr[ColAllInOne.ExcelCol.Oracle.PKName] = sPKName;
                                        break;
                                    case DataBaseType.SQLite:
                                        dr[ColAllInOne.ExcelCol.SQLite.PKName] = sPKName;
                                        break;
                                    case DataBaseType.PostgreSql:
                                        dr[ColAllInOne.ExcelCol.PostgreSql.PKName] = sPKName;
                                        break;
                                    default:
                                        throw new Exception("暂不支持该数据库类型！");
                                }
                            }
                            else
                            {
                                //针对特殊数据的字段赋值
                                switch (importDBType)
                                {
                                    case DataBaseType.SqlServer:
                                    case DataBaseType.MySql:
                                        break;
                                    case DataBaseType.Oracle:
                                        dr[ColOracleTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    case DataBaseType.SQLite:
                                        dr[ColSQLiteTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    case DataBaseType.PostgreSql:
                                        dr[ColPostgreSqlTemplate.ExcelCol.PKName] = sPKName;
                                        break;
                                    default:
                                        throw new Exception("暂不支持该数据库类型！");
                                }
                            }
                        }
                    }

                    dcSelected = new DataColumn(_sGridColumnSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtTable.Columns.Add(dcSelected);
                    dtTable.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面
                    foreach (DataRow dr in dtTable.Select("列编码 is null or 表编码 is null"))
                    {
                        dtTable.Rows.Remove(dr);
                    }
                    //绑定网格
                    //bsCos.DataSource = dtTable;
                    //dgvColList.DataSource = bsCos;
                    dgvColList.BindAutoColumn(dtTable);
                    dgvColList.ShowRowNum();
                    ShowInfo(_strImportSuccess);
                } 
                #endregion
            }
            else if (_ImportInputLY.Equals(cbbInputType.SelectedValue.ToString()))
            {
                #region LY模板导入的处理
                if (importDBType == DataBaseType.None)
                {
                    ShowInfo("请选择【导入数据库类型】");
                    return;
                }

                _dsExcelData = ExportHelper.GetExcelDataSet();//得到Excel数据
                if (_dsExcelData != null)
                {
                    //得到表结构的信息
                    DataTable dtExcelSource = _dsExcelData.Tables[EntlColLY.ExcelCol.SheetName];
                    //移除空行
                    foreach (DataRow dr in dtExcelSource.Select(EntlColLY.ExcelCol.Name + " is null"))
                    {
                        dtExcelSource.Rows.Remove(dr);
                    }
                    //得到表结构
                    DataTable dtTable = EntTable.GetTable();
                    DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtTable.Columns.Add(dcSelected);
                    dtTable.Columns[_sGridTableSelect].SetOrdinal(0);//设置选择列在最前面
                    
                    //得到导入模板类型
                    switch (importDBType)
                    {
                        case DataBaseType.SqlServer:
                            templateType = ColumnTemplateType.SqlServer;
                            break;
                        case DataBaseType.Oracle:
                            templateType = ColumnTemplateType.Oracle;
                            break;
                        case DataBaseType.MySql:
                            templateType = ColumnTemplateType.MySql;
                            break;
                        case DataBaseType.SQLite:
                            templateType = ColumnTemplateType.SQLite;
                            break;
                        case DataBaseType.PostgreSql:
                            templateType = ColumnTemplateType.PostgreSql;
                            break;
                        default:
                            throw new Exception("暂不支持该数据库类型！");
                    }

                    //获取列结构
                    DataTable dtColumn = EntCol.GetTable(templateType);
                    //新增选择列
                    dcSelected = new DataColumn(_sGridColumnSelect, typeof(bool));
                    dcSelected.DefaultValue = "True";
                    dtColumn.Columns.Add(dcSelected);
                    dtColumn.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面
                    //表清单处理
                    DataRow drNew = null;
                    DataRow drColNew = null;
                    int iTalbe = 1;
                    int iColumn = 1;
                    string sTableCode = string.Empty;
                    string sTableName = string.Empty;

                    string sFullDataType = string.Empty;
                    string sDataType = string.Empty;
                    string sDataLength = string.Empty;
                    string sDataDotLength = string.Empty;
                    foreach (DataRow dr in dtExcelSource.Rows)
                    {
                        string sColNameCn = dr[EntlColLY.ExcelCol.Name].ToString().Trim();
                        if (sColNameCn.StartsWith("表名称:") || sColNameCn.StartsWith("表名称："))
                        {
                            //表编码和名称处理
                            drNew = dtTable.NewRow();
                            sTableCode = dr[EntlColLY.ExcelCol.DataType].ToString().Trim().Replace("表编码:", "").Replace("表编码：", "");
                            sTableName = sColNameCn.Replace("表名称:", "").Replace("表名称：", ""); 
                            drNew[ExcelTable.Num] = iTalbe;
                            drNew[ExcelTable.Name] = sTableName;
                            drNew[ExcelTable.Code] = sTableCode;
                            drNew[ExcelTable.ChangeType] = dr[EntlColLY.ExcelCol.ChangeType].ToString().Trim().Replace("创建表", "新增").Replace("修改表", "修改");
                            dtTable.Rows.Add(drNew);
                            iTalbe++;
                            iColumn = 1;
                        }
                        else if(sColNameCn.StartsWith("约束说明:") || sColNameCn.StartsWith("约束说明：")) //约束说明: 
                        {
                            //表更多注释处理
                            string sRemark = sColNameCn.Replace("约束说明:", "").Replace("约束说明：", "").Trim();
                            if (drNew!=null && !sRemark.Equals(drNew[ExcelTable.Name].ToString()))
                            {
                                drNew[ExcelTable.Remark] = sRemark;
                            }
                        }
                        else if("列名称".Equals(sColNameCn))
                        {
                            continue; //跳过列头信息
                        }
                        else
                        {
                            //列处理
                            drColNew = dtColumn.NewRow();
                            drColNew[ColCommon.ExcelCol.TableCode] = sTableCode;
                            drColNew[ColCommon.ExcelCol.OrderNo] = iColumn;
                            drColNew[ColCommon.ExcelCol.Name] = dr[EntlColLY.ExcelCol.Name].ToString().Trim();
                            drColNew[ColCommon.ExcelCol.Code] = dr[EntlColLY.ExcelCol.Code].ToString().Trim();

                            sFullDataType = dr[EntlColLY.ExcelCol.DataType].ToString().Trim();
                            drColNew[ColCommon.ExcelCol.DataTypeFull] = sFullDataType; //全类型
                            ColCommon.SplitFullDataType(sFullDataType,ref sDataType, ref sDataLength, ref sDataDotLength);
                            drColNew[ColCommon.ExcelCol.DataType] = sDataType;
                            drColNew[ColCommon.ExcelCol.DataLength] = sDataLength;
                            drColNew[ColCommon.ExcelCol.DataDotLength] = sDataDotLength;
                            drColNew[ColCommon.ExcelCol.KeyType] = dr[EntlColLY.ExcelCol.KeyType].ToString().Trim();
                            drColNew[ColCommon.ExcelCol.NotNull] = dr[EntlColLY.ExcelCol.NotNull].ToString().Trim();
                            drColNew[ColCommon.ExcelCol.Default] = dr[EntlColLY.ExcelCol.Default].ToString().Trim();
                            drColNew[ColCommon.ExcelCol.Remark] = dr[EntlColLY.ExcelCol.Remark].ToString().Trim(); 
                            drColNew[ColCommon.ExcelCol.ChangeType] = dr[EntlColLY.ExcelCol.ChangeType].ToString().Trim();
                            dtColumn.Rows.Add(drColNew);
                            iColumn++;
                        }
                    }

                    //绑定表网格
                    dgvTableList.BindAutoColumn(dtTable);
                    dgvTableList.ShowRowNum();
                    //绑定列网格
                    dgvColList.BindAutoColumn(dtColumn);
                    dgvColList.ShowRowNum();
                    ShowInfo(_strImportSuccess);
                } 
                #endregion
            }
            else
            {
                //查询数据库
                _dbServer = await uC_DbConnection1.GetDbServerInfo(ckbQueryColumnRealTime.Checked);
                string sTableName = cbbTableName.Text.Trim();
                if (_dbServer == null)
                {
                    return;
                }

                //重新加载表清单下拉框
                if (uC_DbConnection1.IsConnChange && ckbGetTableList.Checked)
                {
                    //绑定下拉框
                    cbbTableName.BindDropDownList(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].Sort("TABLE_NAME"), "TABLE_NAME", "TABLE_NAME", true, false);
                    //查找自动完成数据源
                    cbbTableName.AutoCompleteCustomSource.AddRange(uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].AsEnumerable().Select(x => x.Field<string>("TABLE_NAME")).ToArray());
                }

                switch (_dbServer.DatabaseType)
                {
                    case DataBaseType.SqlServer:
                        templateType = ColumnTemplateType.SqlServer;
                        break;
                    case DataBaseType.Oracle:
                        templateType = ColumnTemplateType.Oracle;
                        break;
                    case DataBaseType.MySql:
                        templateType = ColumnTemplateType.MySql;
                        break;
                    case DataBaseType.SQLite:
                        templateType = ColumnTemplateType.SQLite;
                        break;
                    case DataBaseType.PostgreSql:
                        templateType = ColumnTemplateType.PostgreSql;
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                }

                DataRow[] drArr = null;
                string sFilter = DBTableEntity.SqlString.Name + "='" + sTableName + "'";
                DataTable dtTable;
                string sSchma = "";

                DataTable dtTableCopy = EntTable.GetTable();
                //增加选择列
                DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                dcSelected.DefaultValue = "True";
                dtTableCopy.Columns.Add(dcSelected);
                dtTableCopy.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面

                if (!uC_DbConnection1.userTableDic.ContainsKey(uC_DbConnection1.LatestDbServerInfo.DbConnKey) || uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].Rows.Count == 0)
                {
                    _dataAccess = AutoSQLExecutors.Connect(_dbServer);
                    dtTable = _dataAccess.GetSchemaTables();
                }
                else
                {
                    if (ckbQueryColumnRealTime.Checked)
                    {
                        _dataAccess = AutoSQLExecutors.Connect(_dbServer);
                    }
                    dtTable = uC_DbConnection1.userTableDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey];
                    sSchma = dtTable.Rows[0][DBTableEntity.SqlString.Schema].ToString();
                }

                if (!sTableName.IsNullOrEmpty())
                {
                    drArr = dtTable.Select(sFilter);
                    if (drArr == null || drArr.Count() == 0)
                    {
                        return;
                    }
                    sSchma = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                    DataRow dr = dtTableCopy.NewRow();
                    dr[EntTable.ExcelTable.Num] = "1";
                    dr[EntTable.ExcelTable.Code] = drArr[0][DBTableEntity.SqlString.Name].ToString();
                    dr[EntTable.ExcelTable.Name] = drArr[0][DBTableEntity.SqlString.NameCN].ToString();
                    dr[EntTable.ExcelTable.ChangeType] = "新增";
                    dr[EntTable.ExcelTable.CommonColumnTableCode] = "";
                    dr[EntTable.ExcelTable.Remark] = drArr[0][DBTableEntity.SqlString.Extra].ToString();
                    dtTableCopy.Rows.Add(dr);
                }
                else
                {
                    int i = 1;
                    foreach (DataRow drSource in dtTable.Rows)
                    {
                        DataRow dr = dtTableCopy.NewRow();
                        string sTableCode = drSource[DBTableEntity.SqlString.Name].ToString();
                        dr[EntTable.ExcelTable.Num] = i++;
                        dr[EntTable.ExcelTable.Code] = sTableCode;
                        dr[EntTable.ExcelTable.Name] = drSource[DBTableEntity.SqlString.NameCN].ToString();
                        dr[EntTable.ExcelTable.ChangeType] = "新增";
                        dr[EntTable.ExcelTable.CommonColumnTableCode] = "";
                        dr[EntTable.ExcelTable.Remark] = drSource[DBTableEntity.SqlString.Extra].ToString();
                        if (sTableCode.Contains("$"))
                        {
                            dr[_sGridTableSelect] = "False";
                        }
                        dtTableCopy.Rows.Add(dr);
                    }
                }
                dtTableCopy.TableName = _strTableName;
                SetTableTag(dtTableCopy);
                SetColTag(sSchma, sTableName, templateType);
                //保存用户喜好配置
                SaveUserLoveConfig();
            }
            //设置不能增加行
            dgvTableList.AutoGenerateColumns = true;
            dgvTableList.AllowUserToAddRows = false;
            dgvColList.AutoGenerateColumns = true;
            dgvColList.AllowUserToAddRows = false;
            tabControl1.SelectedTab = tpImport;

        }

        private void SaveUserLoveConfig()
        {
            //保存用户喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_InputType, cbbInputType.SelectedValue.ToString(), "【生成表SQL】录入方式");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_TargetDbType, cbbTargetDbType.SelectedValue.ToString(), "【生成表SQL】目标数据库类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_ExcludeColumnList, txbExcludeColumn.Text.Trim(), "【生成表SQL】排除指定列清单");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsExcludeColumn, ckbExcludeColumn.Checked ? "1" : "0", "【生成表SQL】是否排除指定列");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_QueryColumnRealTime, ckbQueryColumnRealTime.Checked ? "1" : "0", "【生成表SQL】是否实时查询列信息");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsFullType, ckbFullTypeDoc.Checked ? "1" : "0", "【生成表SQL】是否全类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsLYTemplate, ckbLYTemplate.Checked ? "1" : "0", "【生成表SQL】是否LY模板");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsOnlyRemark, ckbOnlyRemark.Checked ? "1" : "0", "【生成表SQL】是否仅使用备注作为表或列的说明");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsDefaultPK, ckbDefaulePK.Checked ? "1" : "0", "【生成表SQL】是否使用默认主键");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_IsDefaultColNameCN, ckbDefaultColNameCn.Checked ? "1" : "0", "【生成表SQL】是否使用默认列中文名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GenerateTableSQL_DefaultColNameCN, txbDefaultColNameCN.Text.Trim(), "【生成表SQL】列中文名为空时使用的列名");
            WinFormContext.UserLoveSettings.Save();
        }
        #endregion

        private string GetExcelSheetNameCol(DataBaseType impDBType)
        {
            string colTableName;
            if (ckbAllConvert.Checked)
            {
                colTableName = EntExcelSheet.Col;
                _isAllConvert = true;
            }
            else
            {
                _isAllConvert = false;
                switch (impDBType)
                {
                    case DataBaseType.SqlServer:
                        colTableName = EntExcelSheet.Col_SqlServer;
                        break;
                    case DataBaseType.Oracle:
                        colTableName = EntExcelSheet.Col_Oracle;
                        break;
                    case DataBaseType.MySql:
                        colTableName = EntExcelSheet.Col_MySql;
                        break;
                    case DataBaseType.SQLite:
                        colTableName = EntExcelSheet.Col_SQLite;
                        break;
                    case DataBaseType.PostgreSql:
                        colTableName = EntExcelSheet.Col_PostgreSql;
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                }
            }

            return colTableName;
        }
        
        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            //移除数据库变更记录页签
            TableStructGenerator.RemoveTab(tabControl1);
            rtbResult.Clear();
            label3.Focus();

            #region 结束编辑处理
            //bsTable.EndEdit();
            dtTable = dgvTableList.GetBindingTable();
            dtAllCol = dgvColList.GetBindingTable();
            string sFilter;

            if (dtTable == null || dtTable.Rows.Count == 0)
            {
                ShowErr("请先导入数据库结构生成模板，或查询数据库中的表结构信息！");
                return;
            }

            //只针对录入的新表修改为选中状态
            DataTable dtNewTable = dgvNewTableInfo.GetBindingTable();
            if (dtNewTable.Rows.Count > 0)
            {
                DataRow[] newTableArr = dtTable.Select(_sGridColumnSelect + "='True'");
                foreach (var item in newTableArr)
                {
                    item[_sGridColumnSelect] = "False"; //设置为不选中
                }

                var query = from f in dtTable.AsEnumerable()
                            where GetLinqDynamicWhere(dtNewTable.Rows, f)
                            select f;
                foreach (var item in query.ToList())
                {
                    item[_sGridColumnSelect] = "True"; //设置为不选中
                }
            }

            //移除空行
            sFilter = EntTable.ExcelTable.Code + " is null or " + EntTable.ExcelTable.Num + " is null";
            foreach (DataRow dr in dtTable.Select(sFilter))
            {
                dtTable.Rows.Remove(dr);
            }
            sFilter= ColCommon.ExcelCol.Code + " is null or " + ColCommon.ExcelCol.TableCode + " is null";
            foreach (DataRow dr in dtAllCol.Select(sFilter))
            {
                dtAllCol.Rows.Remove(dr);
            }
            dtTable.AcceptChanges();
            dtAllCol.AcceptChanges();

            //筛选选中的表
            sFilter = _sGridTableSelect + " = 'True' ";
            DataTable dtTalbeSelect = dtTable.Clone();
            foreach (DataRow dr in dtTable.Select(sFilter))
            {
                dtTalbeSelect.ImportRow(dr);
            }

            //筛选选中表的所有列
            DataTable dtColumnSelect = dtAllCol.Clone();
            foreach (DataRow drT in dtTalbeSelect.Rows)
            {
                sFilter = EntTable.ExcelTable.Code + "='"+ drT[EntTable.ExcelTable.Code].ToString() + "' and "  + _sGridColumnSelect + " = 'True' ";
                foreach (DataRow dr in dtAllCol.Select(sFilter))
                {
                    dtColumnSelect.ImportRow(dr);
                }
            }

            //还需要导入Excel中的通用列
            sFilter = EntTable.ExcelTable.CommonColumnTableCode + " is not null";
            foreach (DataRow drT in dtTalbeSelect.Select(sFilter))
            {
                string sCommonTableCode = drT[EntTable.ExcelTable.CommonColumnTableCode].ToString();
                sFilter = ColCommon.ExcelCol.TableCode + "='" + sCommonTableCode + "'";
                foreach (DataRow dr in dtAllCol.Select(sFilter))
                {
                    dtColumnSelect.ImportRow(dr);
                }
            }

            if (dtTalbeSelect.Rows.Count == 0 || dtColumnSelect.Rows.Count == 0)
            {
                ShowErr("请至少选择一个表及它的列！");
                return;
            }
            #endregion

            int importDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)importDbType;
            if (importDBType == DataBaseType.None)
            {
                ShowInfo("请选择【导入数据库类型】");
                return;
            }
            //目标数据库类型
            int iDbType = int.Parse(cbbTargetDbType.SelectedValue.ToString());
            targetDBType = (DataBaseType)iDbType;
            if(targetDBType== DataBaseType.None)
            {
                ShowInfo("请选择【目标数据库类型】");
                return;
            }

            if(ckbDefaultColNameCn.Checked && string.IsNullOrEmpty(txbDefaultColNameCN.Text.Trim()))
            {
                ShowInfo("当选中【默认列名称】时，其后的文本框不能为空！");
                return;
            }

            //创建方式
            createType = cbbCreateType.SelectedValue.ToString();

            SQLBuilder builder;
            switch (targetDBType)
            {
                case DataBaseType.SqlServer:
                    builder = new SQLServerBuilder();
                    break;
                case DataBaseType.Oracle:
                    builder = new OracleBuilder();
                    break;
                case DataBaseType.MySql:
                    builder = new MySQLBuilder();
                    break;
                case DataBaseType.SQLite:
                    builder = new SQLiteBuilder();
                    break;
                case DataBaseType.PostgreSql:
                    builder = new PostgreSQLBuilder();
                    break;
                default:
                    builder = new SQLServerBuilder();
                    break;
            }

            if (!dtColumnSelect.Columns.Contains(ColCommon.ExcelCol.DataTypeNew))
            {
                dtColumnSelect.Columns.Add(ColCommon.ExcelCol.DataTypeNew);
            }
            if (!dtColumnSelect.Columns.Contains(ColCommon.ExcelCol.DataTypeFullNew))
            {
                dtColumnSelect.Columns.Add(ColCommon.ExcelCol.DataTypeFullNew);
            }

            foreach (DataRow dr in dtColumnSelect.Rows)
            {
                string sDataType = dr[ColCommon.ExcelCol.DataType].ToString();
                string sDefault = dr[ColCommon.ExcelCol.Default].ToString();
                string sDataLength = dr[ColCommon.ExcelCol.DataLength].ToString();
                string sDataDotLength = dr[ColCommon.ExcelCol.DataDotLength].ToString();
                builder.ConvertDBTypeDefaultValueString(ref sDataType, ref sDefault, importDBType);
                dr[ColCommon.ExcelCol.DataTypeNew] = sDataType; //得到新类型
                dr[ColCommon.ExcelCol.Default] = sDefault; //得到新默认值
                dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataDotLength);
                if (string.IsNullOrEmpty(dr[ColCommon.ExcelCol.Name].ToString()))
                {
                    dr[ColCommon.ExcelCol.Name] = txbDefaultColNameCN.Text.Trim() + "列";
                }
            }

            #region 通用列模板的处理
            string sCommonTable = cbbTemplateType.Text.Trim(); //模板名称
            if (!string.IsNullOrEmpty(sCommonTable))
            {
                foreach (DataRow dr in dtTalbeSelect.Rows)
                {
                    //所有选中的表，其通用列都加上所选的模板名称
                    if ("新增".Equals(dr[ExcelTable.ChangeType].ToString()))
                    {
                        dr[ExcelTable.CommonColumnTableCode] = sCommonTable; //只针对新增表加上通用列
                    }
                }
                DataTable dtStandarCol = dgvOldNewChar.GetBindingTable();
                foreach (DataRow drSource in dtStandarCol.Rows)
                {
                    DataRow dr = dtColumnSelect.NewRow();
                    /**
                     *  public static string TableName = "T";//表编码
                        public static string TableNameCN = "T1";
                        public static string TableNameUpper = "T2";//表编码的大驼峰式
                        public static string TableNameLower = "T3";//表编码的小驼峰式
                        public static string TableComments = "T7";
                        public static string TableExtra = "T8";
                        public static string TableSchema = "T9";

                        public static string Name = "C";//列名
                        public static string NameCN = "C1";//列中文名称：从列备注中拆分
                        public static string NameUpper = "C2";//列编码的大驼峰式
                        public static string NameLower = "C3";//列编码的小驼峰式
            
                        public static string DataType = "D";//列类型（不含长度或精度）
                        public static string DataLength = "D1";//长度
                        public static string DataPrecision = "D2";//精度
                        public static string DataScale = "D3";//尺度，数值范围
                        public static string DataTypeFull = "D9";//类型全称，如decimail(14,4)

                        public static string Default = "F";//默认值
                        public static string NotNull = "N";//非空
                        public static string KeyType = "K";//主外键：PK主键，FK外键
                        public static string SortNum = "S";//排序号

                        public static string Comments = "R";//包括列名称和额外信息
                        public static string Extra = "R1";//列额外信息：从列备注中拆分
                     */
                    string sDataType = drSource[DBColumnSimpleEntity.SqlString.DataType].ToString();
                    string sDataLength = drSource[DBColumnSimpleEntity.SqlString.DataLength].ToString();
                    //string sDataPrecision = drSource[DBColumnSimpleEntity.SqlString.DataPrecision].ToString();
                    string sDataScale = drSource[DBColumnSimpleEntity.SqlString.DataScale].ToString();
                    string sColName = drSource[DBColumnSimpleEntity.SqlString.Name].ToString();
                    dr[_sGridColumnSelect] = "True";
                    dr[ColCommon.ExcelCol.ChangeType] = "新增";
                    dr[ColCommon.ExcelCol.TableCode] = sCommonTable; //模板名称作为模板表的表编码，方便筛选
                    dr[ColCommon.ExcelCol.Code] = sColName;
                    dr[ColCommon.ExcelCol.Name] = drSource[DBColumnSimpleEntity.SqlString.NameCN].ToString();
                    dr[ColCommon.ExcelCol.DataType] = sDataType;
                    dr[ColCommon.ExcelCol.DataLength] = sDataLength;
                    dr[ColCommon.ExcelCol.DataDotLength] = sDataScale;
                    dr[ColCommon.ExcelCol.Default] = drSource[DBColumnSimpleEntity.SqlString.Default].ToString();
                    dr[ColCommon.ExcelCol.KeyType] = drSource[DBColumnSimpleEntity.SqlString.KeyType].ToString();
                    dr[ColCommon.ExcelCol.NotNull] = "1".Equals(drSource[DBColumnSimpleEntity.SqlString.NotNull].ToString()) ? "是" : "";
                    dr[ColCommon.ExcelCol.Remark] = drSource[DBColumnSimpleEntity.SqlString.Extra].ToString();
                    dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataScale); //全类型

                    dtColumnSelect.Rows.Add(dr);
                }
            }

            string sFilterNew = "";
            foreach (DataRow dr in dtNewTable.Rows)
            {
                string sOldTableCode = dr[EntTable.ExcelTable.Code + "_OLD"].ToString();
                string sNewTableCode = dr[EntTable.ExcelTable.Code].ToString();
                string sRemark = dr[EntTable.ExcelTable.Remark].ToString();
                if(string.IsNullOrEmpty(sOldTableCode) || string.IsNullOrEmpty(sNewTableCode))
                {
                    continue;
                }
                sFilterNew = EntTable.ExcelTable.Code + "='" + sOldTableCode + "'";
                DataRow[] drArrNew = dtTalbeSelect.Select(sFilterNew);
                if (drArrNew.Length==0) continue;
                //修改表名
                drArrNew[0][EntTable.ExcelTable.Code] = sNewTableCode;
                drArrNew[0][EntTable.ExcelTable.Remark] = sRemark;
                //修改列清单
                sFilterNew = ColCommon.ExcelCol.TableCode + "='" + sOldTableCode + "'";
                DataRow[] drArrCols = dtColumnSelect.Select(sFilterNew);
                foreach (DataRow drCol in drArrCols)
                {
                    drCol[ColCommon.ExcelCol.TableCode] = sNewTableCode;
                }
            }
            #endregion

            //构造实体并赋值
            GenerateParamEntity paramEntity = new GenerateParamEntity();
            paramEntity.sqlCreateType = (SQLCreateType)int.Parse(createType);
            paramEntity.importDBType = importDBType;
            paramEntity.targetDBType = targetDBType;
            paramEntity.isAllConvert = _isAllConvert;
            paramEntity.isDefaultPK = ckbDefaulePK.Checked;
            paramEntity.isDefaultColNameCN = ckbDefaultColNameCn.Checked;
            paramEntity.defaultColNameCN = txbDefaultColNameCN.Text.Trim();

            if (!ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity))//校验数据
            {
                return;
            }
            //调用生成SQL方法
            string sSql = builder.GenerateTableContruct(dtTalbeSelect, dtColumnSelect, paramEntity) + "\n";
            rtbResult.AppendText(sSql);
            Clipboard.SetData(DataFormats.UnicodeText, sSql);
            tabControl1.SelectedTab = tpAutoSQL;

            //增加生成表结构的功能
            dtAllCol.AcceptChanges();
            TableStructGeneratorParamEntity docEntity = new TableStructGeneratorParamEntity();
            docEntity.builder = builder;
            docEntity.importDBType = importDBType;
            docEntity.useDataTypeFull = ckbFullTypeDoc.Checked;
            docEntity.useLYTemplate = ckbLYTemplate.Checked;
            docEntity.useRemarkContainsName = ckbOnlyRemark.Checked;
            docEntity.useNameSameWithRemark = ckbColNameSameRemark.Checked;
            docEntity.useOldColumnCode = ckbDoubleColName.Checked; //重复列编码：数据迁移使用
            docEntity.defaultColumnOrTableName = txbDefaultColNameCN.Text.Trim();
            docEntity.isQueryDataBase = "2".Equals(cbbInputType.SelectedValue)?true:false;
            TableStructGenerator.Generate(tabControl1, dtTalbeSelect, dtColumnSelect, docEntity);
            //生成SQL成功后提示
            ShowSuccessMsg(_strAutoSqlSuccess);
            //保存用户喜好配置
            SaveUserLoveConfig();
            //初始化控件
            ckbAllConvert.Enabled = true;
        }

        private static bool GetLinqDynamicWhere(DataRowCollection filterArr, DataRow drF)
        {
            foreach (DataRow item in filterArr)
            {
                string sFilePath = drF.Field<string>(EntTable.ExcelTable.Code);
                if (sFilePath.Equals(item[EntTable.ExcelTable.Code + "_OLD"].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 检查数据的有效性
        private bool ValidateData(DataTable dtTalbeSelect, DataTable dtColumnSelect, GenerateParamEntity paramEntity)
        {
            //通用的判断
            if(!ColCommon.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity,out StringBuilder sb))
            {
                ShowErr(sb.ToString()); 
                return false;
            }

            if (_isAllConvert)
            {
                //综合转换判断
                if (!ColAllInOne.ValidateData(dtTalbeSelect, dtColumnSelect, targetDBType, paramEntity, out StringBuilder sbAll))
                {
                    ShowErr(sbAll.ToString());
                    return false;
                }
            }
            else if(paramEntity.importDBType == paramEntity.targetDBType)
            {
                #region 非综合转换，且导入类型跟目标类型一致的判断
                if (targetDBType == DataBaseType.Oracle)
                {
                    if (!ColOracleTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.SqlServer)
                {
                    if (!ColSqlServerTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.MySql)
                {
                    if (!ColMySqlTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.SQLite)
                {
                    if (!ColSQLiteTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else if (targetDBType == DataBaseType.PostgreSql)
                {
                    if (!ColPostgreSqlTemplate.ValidateData(dtTalbeSelect, dtColumnSelect, paramEntity, out StringBuilder sbAll))
                    {
                        ShowErr(sbAll.ToString());
                        return false;
                    }
                }
                else
                {
                    throw new Exception("暂不支持该数据库类型！");
                }
                #endregion
            }
            return true;
        }
        #endregion

        #region 导入数据库类型选择变化
        private void cbbImportDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeImprotDataColsSource();
        }
        #endregion

        private void ChangeImprotDataColsSource()
        {
            if (_dsExcelData == null) return;
            int iDbType = int.Parse(cbbImportDBType.SelectedValue.ToString());
            importDBType = (DataBaseType)iDbType;
            if (importDBType == DataBaseType.None) return;
            string sExcelSheetName = GetExcelSheetNameCol(importDBType);
            DataTable dtCol = _dsExcelData.Tables[sExcelSheetName]; //更换【列】网格数据源
            if (!dtCol.Columns.Contains(_sGridTableSelect))
            {
                DataColumn dcSelected = new DataColumn(_sGridTableSelect, typeof(bool));
                dcSelected.DefaultValue = "True";
                dtCol.Columns.Add(dcSelected);
                dtCol.Columns[_sGridTableSelect].SetOrdinal(0);//设置选择列在最前面
            }
            dgvColList.BindAutoColumn(dtCol);
            //bsCos.DataSource = dtCol;
            if (!ckbAllConvert.Checked)
            {
                cbbTargetDbType.SelectedValue = cbbImportDBType.SelectedValue;
            }
        }

        #region 下载模板按钮事件
        private void tsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.TableSQL.Excel_TableColumn, "模板_表列结构变更", true);
        }

        private void tsbDownloadLYTemplate_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.TableSQL.Excel_TableColumnLY, "模板_LY数据库变更", true);
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 成功提示方法
        /// <summary>
        /// 成功提示方法
        /// </summary>
        private void ShowSuccessMsg(string strInfo)
        {
            ShowInfo(strInfo);
            rtbResult.Select(0, 0); //返回到第一行
        }
        #endregion

        #region 综合转换复选框变化事件
        private void ckbAllConvert_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImprotDataColsSource();
        }
        #endregion

        private void BtnSaveOther_Click(object sender, EventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.FileName = ".sql";
            diag.Filter = "Sql文件|*.sql";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(diag.FileName, rtbResult.Text);
            }
        }

        private void cbbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbInputType.SelectedValue == null) return;
            if (_ImportInput.Equals(cbbInputType.SelectedValue.ToString()))
            {
                gbTable.Visible = false;
                uC_DbConnection1.Visible = false;
                tsbImport.Text = "导入";
                ckbAllConvert.Checked = true;
                ckbAllConvert.Visible = true;
                cbbImportDBType.SetControlReadOnly(false);
                //tpDataStandard.Parent = null;
            }
            else if (_ImportInputLY.Equals(cbbInputType.SelectedValue.ToString()))
            {
                gbTable.Visible = false;
                uC_DbConnection1.Visible = false;
                tsbImport.Text = "导入";
                ckbAllConvert.Checked = false;
                ckbAllConvert.Visible = false;
                cbbImportDBType.SetControlReadOnly(false);
            }
            else
            {
                //连接数据库
                gbTable.Visible = true;
                uC_DbConnection1.Visible = true;
                tsbImport.Text = "连接";
                ckbAllConvert.Checked = false;
                ckbAllConvert.Visible = false;
                cbbImportDBType.SetControlReadOnly(true);
                //tabControl1.TabPages.Insert(1, tpDataStandard);
                //加载通用列数据
                LoadCommonColumnData();
                //在切换回输入方式为数据时，要将导入数据库类型还原为连接类型，不然会报错
                if (uC_DbConnection1.LatestDbServerInfo != null)
                {
                    cbbImportDBType.SelectedValue = uC_DbConnection1.LatestDbServerInfo.Database;
                }        
            }
            dgvTableList.Columns.Clear();
            dgvTableList.DataSource = null;
            dgvColList.Columns.Clear();
            dgvColList.DataSource = null;
        }

        private void cbbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTableName.Text.Trim())) return;
            tsbImport.PerformClick();
        }

        private void SetTableTag(DataTable dt)
        {
            //bsTable.DataSource = dt;
            dgvTableList.BindAutoColumn(dt);
            dgvTableList.ShowRowNum();
        }

        #region 设置Tag方法
        private void SetColTag(string sSchema, string sTableName, ColumnTemplateType templateType)
        {
            DataTable dtCols;
            if (ckbQueryColumnRealTime.Checked)
            {
                //实时查询列信息
                dtCols = _dataAccess.GetSqlSchemaTableColumns(sTableName, sSchema);
                
            }
            else
            {
                
                //通过之前的查询结果过滤：速度快
                if (!string.IsNullOrEmpty(sTableName))
                {
                    string sFilter = string.IsNullOrEmpty(sSchema) ? DBColumnEntity.SqlString.TableName + "='" + sTableName + "'" : DBColumnEntity.SqlString.TableName + "='" + sTableName + "' AND " + DBColumnEntity.SqlString.TableSchema + "='" + sSchema + "'";
                    DataRow[] drArrCols = uC_DbConnection1.userColumnDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey].Select(sFilter); 
                    if (drArrCols.Length > 0)
                    {
                        dtCols = drArrCols.CopyToDataTable();
                    }
                    else
                    {
                        throw new Exception("表的列清单不存在，请重新打开本功能重试！");
                    }
                }
                else
                {
                    dtCols = uC_DbConnection1.userColumnDic[uC_DbConnection1.LatestDbServerInfo.DbConnKey];
                }
            }

            //如果选中更新默认值，那么直接调用
            if (ckbUpdateDefault.Checked)
            {
                uC_DbConnection1.QueryColumnsDefaultValue(uC_DbConnection1.LatestDbServerInfo);
            }

            DataTable dtColsNew = EntCol.GetTable(templateType);
            bool isExclude = ckbExcludeColumn.Checked;
            string[] arrExclude = txbExcludeColumn.Text.Trim().ToLower().Split(new char[] { ',','，',';','；' }, StringSplitOptions.RemoveEmptyEntries);
            //增加选择列：注用BindDataGridView时，就不用指定表的列类型为bool类型；只有使用BindAutoColumn时，才需要指定表的列类型为bool类型。
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect, typeof(bool));
            dcSelected.DefaultValue = "True";
            dtColsNew.Columns.Add(dcSelected);
            dtColsNew.Columns[_sGridColumnSelect].SetOrdinal(0);//设置选择列在最前面

            foreach (DataRow drSource in dtCols.Rows)
            {
                DataRow dr = dtColsNew.NewRow();
                string sDataType = drSource[DBColumnEntity.SqlString.DataType].ToString();
                string sDataLength = drSource[DBColumnEntity.SqlString.DataLength].ToString();
                //string sDataPrecision = drSource[DBColumnEntity.SqlString.DataPrecision].ToString();
                string sDataScale = drSource[DBColumnEntity.SqlString.DataScale].ToString();
                string sColName = drSource[DBColumnEntity.SqlString.Name].ToString();
                if (isExclude && arrExclude.Length>0 && arrExclude.Contains(sColName.ToLower()))
                {
                    dr[_sGridColumnSelect] = "False";
                }
                if (sColName.Contains("$"))
                {
                    dr[_sGridColumnSelect] = "False";//默认有美元符号的，也不选中
                }

                dr[ColCommon.ExcelCol.ChangeType] = "新增";
                dr[ColCommon.ExcelCol.TableCode] = drSource[DBColumnEntity.SqlString.TableName].ToString();
                dr[ColCommon.ExcelCol.Code] = sColName;
                dr[ColCommon.ExcelCol.Name] = drSource[DBColumnEntity.SqlString.NameCN].ToString();
                dr[ColCommon.ExcelCol.DataType] = sDataType;
                dr[ColCommon.ExcelCol.DataLength] = sDataLength;
                dr[ColCommon.ExcelCol.DataDotLength] = sDataScale;
                dr[ColCommon.ExcelCol.Default] = drSource[DBColumnEntity.SqlString.Default].ToString();
                dr[ColCommon.ExcelCol.KeyType] = drSource[DBColumnEntity.SqlString.KeyType].ToString();
                dr[ColCommon.ExcelCol.NotNull] = "1".Equals(drSource[DBColumnEntity.SqlString.NotNull].ToString()) ? "是" : "";
                dr[ColCommon.ExcelCol.Remark] = drSource[DBColumnEntity.SqlString.Extra].ToString();
                dr[ColCommon.ExcelCol.DataTypeFullNew] = ColCommon.GetFullDataType(sDataType, sDataLength, sDataScale); //全类型
                dr[ColCommon.ExcelCol.OrderNo] = drSource[DBColumnEntity.SqlString.SortNum].ToString(); //排序号
                //统一主键处理
                string sPKName = "PK_" + drSource[DBColumnEntity.SqlString.TableName].ToString();
                //针对特殊数据的字段赋值
                switch (_dbServer.DatabaseType)
                {
                    case DataBaseType.SqlServer:
                        dr[ColSqlServerTemplate.ExcelCol.FK] = "";
                        break;
                    case DataBaseType.Oracle:
                        dr[ColOracleTemplate.ExcelCol.FK] = "";
                        if ("PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColOracleTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    case DataBaseType.MySql:
                        dr[ColMySqlTemplate.ExcelCol.FK] = "";
                        break;
                    case DataBaseType.SQLite:
                        dr[ColSQLiteTemplate.ExcelCol.FK] = "";
                        if ("PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColSQLiteTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    case DataBaseType.PostgreSql:
                        dr[ColPostgreSqlTemplate.ExcelCol.FK] = "";
                        if ("PK".Equals(drSource[DBColumnEntity.SqlString.KeyType].ToString()))
                        {
                            dr[ColPostgreSqlTemplate.ExcelCol.PKName] = sPKName;
                        }
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                }

                dtColsNew.Rows.Add(dr);
            }

            dtColsNew.TableName = _strColName;

            //bsCos.DataSource = dtColsNew;
            //dgvColList.DataSource= dtColsNew;
            dgvColList.BindAutoColumn(dtColsNew, false);
            dgvColList.ShowRowNum();
        }
        #endregion

        private void dgvTableList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvTableList.Columns[_sGridTableSelect].Index)
            {
                dgvTableList.AllChecked(_sGridTableSelect, ref _allTableSelect);
            }
        }

        private void dgvColList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvColList.Columns[_sGridColumnSelect].Index)
            {
                dgvColList.AllChecked(_sGridColumnSelect, ref _allColumnSelect);
            }
        }

        private void ckbLYTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbLYTemplate.Checked)
            {
                ckbFullTypeDoc.Checked = true;
                ckbOnlyRemark.Checked = true;
            }
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
            string sSearch = txbSearchColumn.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvColList.SeachText(sSearch, ref dgvFindText, null, isNext);
            lblFind.Text = dgvFindText.CurrentMsg;
        }

        /// <summary>
        /// 显示方向右键按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDirectShow_Click(object sender, EventArgs e)
        {
            Orientation orientationNew = splitContainer1.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
            if (orientationNew.Equals(Orientation.Vertical))
            {
                splitContainer1.SplitterDistance = int.Parse(Math.Ceiling(splitContainer1.Width * 0.3).ToString());
            }
            else
            {
                splitContainer1.SplitterDistance = int.Parse(Math.Ceiling(splitContainer1.Height * 0.3).ToString());
            }
            splitContainer1.Orientation = orientationNew;
        }

        private void tsmiChooseOrNot_Click(object sender, EventArgs e)
        {
            DataGridView dgvOldNewChar = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            if (dgvOldNewChar.SelectedCells == null || dgvOldNewChar.SelectedCells.Count == 0) return;
            if (dgvOldNewChar.CurrentCell.ColumnIndex != dgvOldNewChar.Columns[_sGridColumnSelect].Index)
            {
                return; //选择、条件、MyBatis动态列
            }
            //选择
            bool sNew = bool.Parse(dgvOldNewChar.CurrentCell.Value.ToString()) ? false : true;
            foreach (DataGridViewCell item in dgvOldNewChar.SelectedCells)
            {
                //为了防止选了其他列，这里只针对选择列赋值
                if(item.ColumnIndex == dgvOldNewChar.Columns[_sGridColumnSelect].Index)
                {
                    item.Value = sNew;
                }
            }

            dgvOldNewChar.CurrentCell.Value = sNew;

            //解决当开始是全部选中，双击后全部取消选 中，但因为焦点没有离开选择列，显示还是选中状态的问题
            dgvOldNewChar.ChangeCurrentCell(dgvOldNewChar.CurrentCell.ColumnIndex);
        }

        private void ckbExcludeColumn_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtCol = dgvColList.GetBindingTable();
            if (ckbExcludeColumn.Checked && dtCol != null)
            {
                bool isExclude = ckbExcludeColumn.Checked;
                string[] arrExclude = txbExcludeColumn.Text.Trim().ToLower().Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (DataRow drSource in dtCol.Rows)
                {
                    string sColName = drSource[ColCommon.ExcelCol.Code].ToString(); //注：这里要使用Excel的列名
                    if (isExclude && arrExclude.Length > 0 && arrExclude.Contains(sColName.ToLower()))
                    {
                        drSource[_sGridColumnSelect] = "False";
                    }
                }
            }
        }

        #region 字符替换模板加载和保存相关
        private void btnSaveReplaceTemplate_Click(object sender, EventArgs e)
        {
            string sTempName = txbReplaceTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("分类名称不能为空！");
                return;
            }
            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            dtReplace.DeleteNullRow();
            if (dtReplace.Rows.Count == 0)
            {
                ShowInfo("请录入【分类数据列清单】！");
                return;
            }

            if (ShowOkCancel("确定要保存分类？") == DialogResult.Cancel) return;

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
                dr[StandardColumnClassXmlConfig.KeyString.Name] = sTempName;
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
                    dr[StandardColumnClassXmlConfig.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][StandardColumnClassXmlConfig.KeyString.Name] = sTempName;//修改名称
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
                drNew[StandardColumnClassXmlConfig.ValueString.IsSelected] = dr[StandardColumnClassXmlConfig.ValueString.IsSelected].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.Name] = dr[StandardColumnClassXmlConfig.ValueString.Name].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.NameCN] = dr[StandardColumnClassXmlConfig.ValueString.NameCN].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.NameUpper] = dr[StandardColumnClassXmlConfig.ValueString.NameUpper].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.NameLower] = dr[StandardColumnClassXmlConfig.ValueString.NameLower].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.DataType] = dr[StandardColumnClassXmlConfig.ValueString.DataType].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.DataLength] = dr[StandardColumnClassXmlConfig.ValueString.DataLength].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.DataPrecision] = dr[StandardColumnClassXmlConfig.ValueString.DataPrecision].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.DataScale] = dr[StandardColumnClassXmlConfig.ValueString.DataScale].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.DataTypeFull] = dr[StandardColumnClassXmlConfig.ValueString.DataTypeFull].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.NotNull] = dr[StandardColumnClassXmlConfig.ValueString.NotNull].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.Default] = dr[StandardColumnClassXmlConfig.ValueString.Default].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.KeyType] = dr[StandardColumnClassXmlConfig.ValueString.KeyType].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.Comments] = dr[StandardColumnClassXmlConfig.ValueString.Comments].ToString();
                drNew[StandardColumnClassXmlConfig.ValueString.Extra] = dr[StandardColumnClassXmlConfig.ValueString.Extra].ToString();
                dtValConfig.Rows.Add(drNew);
            }
            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, StandardColumnClassXmlConfig.KeyString.Name, true, true);
            ShowInfo("分类保存成功！");
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null)
            {
                ShowInfo("请选择一个分类！");
                return;
            }
            string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue))
            {
                ShowInfo("请选择一个分类！");
                return;
            }

            if (ShowOkCancel("确定要删除该分类？") == DialogResult.Cancel) return;

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
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, StandardColumnClassXmlConfig.KeyString.Name, true, true);
            ShowInfo("分类删除成功！");
        }

        private void dgvOldNewChar_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvOldNewChar.Columns[e.ColumnIndex].Name == _sGridColumnSelectEn)
            {
                return;
            }
        }

        private void SelectAllOrCancel(DataGridView dgv, ref bool isSelect, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns[_sGridColumnSelectEn].Index)
            {
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Cells[_sGridColumnSelectEn].Value = isSelect ? "1" : "0";
                }
                isSelect = !isSelect;
            }
        }
        private void dgvOldNewChar_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvOldNewChar, ref _allSelectOldNewChar, e);
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
                    dtReplace.Rows.Add(
                        dr[StandardColumnClassXmlConfig.ValueString.Name].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.NameCN].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.NameUpper].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.NameLower].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.DataType].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.DataLength].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.DataPrecision].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.DataScale].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.DataTypeFull].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.NotNull].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.Default].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.KeyType].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.Comments].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.Extra].ToString(),
                        dr[StandardColumnClassXmlConfig.ValueString.IsSelected].ToString(),
                        dtReplace.Rows.Count + 1
                        );
                }
            }
            else if (dtReplace != null)
            {
                dtReplace.Clear();
            }
        }
        #endregion

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            DataGridView dgvOldNewChar = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvOldNewChar.GetBindingTable();
            DataRow dataRow = dgvOldNewChar.GetCurrentRow();
            if (dataRow == null || dataRow.RowState == DataRowState.Detached) return;
            dt.Rows.Remove(dataRow);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            label3.Focus();
            DataRow dataRow = dgvDataDictionary.GetCurrentRow();
            if (dataRow == null) return;
            dgvOldNewChar.GetBindingTable().ImportRow(dataRow);
        }

        private void dgvNewTableInfo_KeyDown(object sender, KeyEventArgs e)
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
                    if (iRow == 0 || iColumn < 3)
                    {
                        return;
                    }

                    DataTable dtMain = dgvNewTableInfo.GetBindingTable();
                    dtMain.Rows.Clear();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string strData2 = data[j, 1].ToString().Trim();
                        string strData3 = data[j, 2].ToString().Trim();
                        string strData4 = string.Empty;
                        if (iColumn >= 4)
                        {
                            strData4 = data[j, 3].ToString().Trim();
                        }
                        if (string.IsNullOrEmpty(strData) || string.IsNullOrEmpty(strData2))
                        {
                            continue;
                        }

                        if (dtMain.Select(ExcelTable.Code+"_OLD" + "='" + data[j, 0] + "'").Length == 0)
                        {
                            DataRow drNew = dtMain.NewRow();
                            drNew[ExcelTable.Code + "_OLD"] = strData;
                            drNew[ExcelTable.Code ] = strData2;
                            drNew[ExcelTable.Name] = strData3;
                            drNew[ExcelTable.Remark] = strData4;
                            dtMain.Rows.Add(drNew);
                        }
                    }
                    if(dtMain.Rows.Count > 0)
                    {
                        ckbIsOnlyReplaceTable.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void tsmiSortBySelectItem_Click(object sender, EventArgs e)
        {
            //按选中状态反选
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            dgvSelect.Sort(dgvSelect.Columns[_sGridColumnSelect], ListSortDirection.Descending);
        }

        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }

        
    }
}
