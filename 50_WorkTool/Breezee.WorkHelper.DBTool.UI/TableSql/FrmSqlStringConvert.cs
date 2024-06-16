using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.WorkHelper.DBTool.UI.TableSql;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据库间SQL语句转换
    /// </summary>
    public partial class FrmSqlStringConvert : BaseForm
    {
        private readonly string _sGridColumnSelect = "IsSelect";
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。";
        private bool _allSelectOldNewChar = false;//默认全选，这里取反
        private bool _allSelectOldNewColumnChar = false;//默认全选，这里取反
        private bool _allSelectTableSource = false;//默认全选，这里取反
        private bool _allSelectTableTarget = false;//默认全选，这里取反
        private bool _isChangeTabPage = true;
        private bool _isLoadDataFirst = true;
        DataGridViewFindText dgvFindTextSource;
        DataGridViewFindText dgvFindTextTarget;
        ReplaceStringXmlConfig replaceStringData;//替换字符模板XML配置
        NewOldColumnXmlConfig replaceColumnStringData;//列的替换字符模板XML配置
        //数据集
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServerSource;
        private DbServerInfo _dbServerTarget;
        private IDataAccess _dataAccessSource;
        private IDataAccess _dataAccessTarget;

        public FrmSqlStringConvert()
        {
            InitializeComponent();
        }

        private void FrmSqlStringConvert_Load(object sender, EventArgs e)
        {
            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            //源数据库连接
            uC_DbConnectionSource.SetDbConnComboBoxSource(dtConn);
            uC_DbConnectionSource.IsDbNameNotNull = true;
            uC_DbConnectionSource.DBType_SelectedIndexChanged += cbbDatabaseTypeSource_SelectedIndexChanged;//数据库类型下拉框变化事件
            //uC_DbConnectionSource.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            uC_DbConnectionSource.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_DbConnectionSource.SetGroupTitle("源数据库连接");
            //目标数据库连接
            uC_DbConnectionTarget.SetGroupTitle("目标数据库连接");
            uC_DbConnectionTarget.SetDbConnComboBoxSource(dtConn);
            uC_DbConnectionTarget.IsDbNameNotNull = true;
            uC_DbConnectionTarget.DBType_SelectedIndexChanged += cbbDatabaseTypeTarget_SelectedIndexChanged;//数据库类型下拉框变化事件
            //uC_DbConnectionTarget.DBConnName_SelectedIndexChanged += cbbConnName_SelectedIndexChanged;
            uC_DbConnectionTarget.ShowGlobalMsg += ShowGlobalMsg_Click;
            #endregion
            //模板
            _dicString.Add("1", "自定义");
            _dicString.Add("2", "读取数据库");
            DataTable dtStringTemplate = _dicString.GetTextValueTable(false);
            cbbNewOldColumnSourceType.BindTypeValueDropDownList(dtStringTemplate,false,true);
            //数据库类型
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbSourceDbType.BindTypeValueDropDownList(dtDbType, true, true);
            cbbTargetDbType.BindTypeValueDropDownList(dtDbType.Copy(), true, true);
            //绑定新旧表关系模板下拉框
            replaceStringData = new ReplaceStringXmlConfig(DBTGlobalValue.DbConvertSql.Xml_NewOldTableFileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, ReplaceStringXmlConfig.KeyString.Name, true, true);
            //绑定新旧列关系模板下拉框
            replaceColumnStringData = new NewOldColumnXmlConfig(DBTGlobalValue.DbConvertSql.Xml_NewOldColumnFileName);
            sColName = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbColumnTemplateType.BindDropDownList(replaceColumnStringData.MoreXmlConfig.KeyData, sColName, NewOldColumnXmlConfig.KeyString.Name, true, true);
            //设置Tag
            SetColTag();
            //加载用户喜好值
            ckbIsAutoExcludeSource.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableSource, "1").Value) ? true : false;//自动参数化查询
            txbExcludeTableSource.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListSource, "_bak,_temp,_tmp").Value; //排除表列表
            ckbIsAutoExcludeTarget.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableTarget, "1").Value) ? true : false;//自动参数化查询
            txbExcludeTableTarget.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListTarget, "_bak,_temp,_tmp").Value; //排除表列表
            rtbInputSql.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_LatestSql, "").Value; //最后输入的SQL
            cbbNewOldColumnSourceType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_NewOldColumnSourceType, "1").Value;
            cbbSourceDbType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_SourceDbType, ((int)DataBaseType.Oracle).ToString()).Value;
            cbbTargetDbType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_TargetDbType, ((int)DataBaseType.MySql).ToString()).Value;
            ckbParamToHash.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DbSqlConvert_IsParaToHash, "1").Value) ? true : false;//@参数转#参数#
            //
            lblFuncInfo.Text = "转换只是对部分字符进行替换，减少部分手工替换工作，生成结果仅供参考，需要复制出来，确认并修正！";
            toolTip1.SetToolTip(cbbNewOldColumnSourceType, "当选择【表、列、函数】时，必须选择源和目标数据，并加载所有列数据；\r\n并且要选择或录入新旧表。");
            toolTip1.SetToolTip(btnLoadAllGenerate, "");

            //加载说明
            string sRootDir = Path.Combine(GlobalContext.AppBaseDirectory, "DataTemplate", "DBTool", "QuerySQL", "SqlConvertRemark.txt");
            rtbConvertRemark.AppendText(File.ReadAllText(sRootDir));
            rtbConvertRemark.ReadOnly = true;
            //GroupBox增加右键折叠菜单
            groupBox2.AddFoldRightMenu();
        }

        private void cbbDatabaseTypeTarget_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {
            cbbTargetDbType.SelectedValue = (int)e.SelectDBType;
        }

        private void cbbDatabaseTypeSource_SelectedIndexChanged(object sender, DBTypeSelectedChangeEventArgs e)
        {
            cbbSourceDbType.SelectedValue = (int)e.SelectDBType;
        }

        /// <summary>
        /// 排除分表：以【_数字】结尾的表
        /// </summary>
        /// <param name="dtTable"></param>
        private void ExcludeSplitTable(DataTable dtTable)
        {
            if (ckbNotIncludeSplitTableSource.Checked)
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

        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            #region 判断并取得数据
            rtbInputSql.Focus();
            //取得数据源
            StringBuilder sbAllSql = new StringBuilder(); //模板字符接接
            StringBuilder sbAllMust = new StringBuilder(); //必填列字符接接
            Stopwatch stopwatch = null;
            string sTemplateString = rtbInputSql.Text;
            if (string.IsNullOrEmpty(sTemplateString.Trim()))
            {
                ShowInfo("请输入要转换的SQL！");
                return;
            }

            if (string.IsNullOrEmpty(cbbSourceDbType.Text.Trim()))
            {
                ShowInfo("请选择源数据库类型！");
                return;
            }

            if (string.IsNullOrEmpty(cbbTargetDbType.Text.Trim()))
            {
                ShowInfo("请选择目标数据库类型！");
                return;
            }
            #endregion

            string sConvertType = cbbNewOldColumnSourceType.SelectedValue.ToString();
            DataTable dtOldCol = dgvColListSource.GetBindingTable();
            DataTable dtNewCol = dgvColListTarget.GetBindingTable();
            DataTable dtOldNew = dgvOldNewChar.GetBindingTable();
            DataTable dtOldNewColumn = dgvOldNewColumnChar.GetBindingTable();
            //得到新旧表或列的对照关系
            bool isHasOldNewTableRelation = false;
            bool isHasOldNewColumnRelation = false;
            bool isNeedConvertColumn = false;
            if (dtOldNew != null && dtOldNew.Rows.Count > 0)
            {
                isHasOldNewTableRelation = true;
            }
            if (dtOldNewColumn != null && dtOldNewColumn.Rows.Count > 0)
            {
                isHasOldNewColumnRelation = true;
                isNeedConvertColumn = true; //有新旧列关系时，也要转换列
            }

            if ("2".Equals(sConvertType))
            {
                //连接数据库
                if (dtOldCol == null || dtOldCol.Rows.Count <= 0)
                {
                    ShowInfo("请加载源数据库的所有列数据！");
                    return;
                }
                if (dtNewCol == null || dtNewCol.Rows.Count <= 0)
                {
                    ShowInfo("请加载目标数据库的所有列数据！");
                    return;
                }
                if (!isHasOldNewTableRelation && !isHasOldNewColumnRelation)
                {
                    ShowInfo("请选择或输入【新旧表关系】或【新旧列关系】！");
                    return;
                }
                isNeedConvertColumn = true; //读取数据时，也是必须转换列
            }

            int iDbType = int.Parse(cbbSourceDbType.SelectedValue.ToString());
            DataBaseType sourceDBType = (DataBaseType)iDbType;
            SQLBuilder builder;
            switch (sourceDBType)
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

            try
            {
                //开始计时
                stopwatch = Stopwatch.StartNew();
                //转换SQl
                iDbType = int.Parse(cbbTargetDbType.SelectedValue.ToString());
                DataBaseType targetDBType = (DataBaseType)iDbType;
                builder.ConvertToDbSql(ref sTemplateString, targetDBType);

                List<SqlTableEntity> tablesOkEntity = new List<SqlTableEntity>();
                //从SQL中获取表清单,并获取其别名
                List<string> tables = SqlAnalyzer.GetTableList(sTemplateString);
                string sFiter = string.Empty;
                //1.针对SQL中的表，查换存在于新旧关系中的表，加入到tablesOk中
                foreach (string sTableName in tables)
                {
                    //新旧表关系
                    if (isHasOldNewTableRelation)
                    {
                        sFiter = string.Format("{0}='{1}'", "OLD", sTableName);
                        if (dtOldNew.Select(sFiter).Length > 0)
                        {
                            if (tablesOkEntity.Where(t => t.Name == sTableName).Count() == 0)
                            {
                                tablesOkEntity.Add(new SqlTableEntity() { Name = sTableName });
                            }
                            continue;
                        }
                    }
                    //新旧列关系
                    if (isHasOldNewColumnRelation)
                    {
                        sFiter = string.Format("{0}='{1}'", NewOldColumnXmlConfig.ValueString.OldTable, sTableName);
                        if (dtOldNewColumn.Select(sFiter).Length > 0)
                        {
                            //能找到旧表
                            if (tablesOkEntity.Where(t => t.Name == sTableName).Count() == 0)
                            {
                                tablesOkEntity.Add(new SqlTableEntity() { Name = sTableName });
                            }
                            continue;
                        }
                    }
                }

                //2.针对有新旧关系的表清单，找出其别名，然后加到【旧表别名字典】中：dicTableAlias
                foreach (SqlTableEntity tableEnt in tablesOkEntity)
                {
                    //注：SQL中的别名前有可能加上AS；表名前面还有可能有SCHEMA
                    string sPatterT = @"\s*(\w+.)?" + tableEnt.Name + @"\s+(AS\s+)?\w+\s*";
                    Regex regexTimeT = new Regex(sPatterT, RegexOptions.IgnoreCase);
                    MatchCollection mcCollTimeT = regexTimeT.Matches(sTemplateString);
                    foreach (Match m in mcCollTimeT)
                    {
                        //2.1 分析得到别名
                        string sValue = m.Value.Trim();
                        int iLast = sValue.LastIndexOf(tableEnt.Name);
                        if (iLast > -1)
                        {
                            //键为旧表名，值为旧表别名
                            sValue = sValue.Substring(iLast + tableEnt.Name.Length).Trim();
                            if (sValue.StartsWith("AS", StringComparison.OrdinalIgnoreCase))
                            {
                                sValue = sValue.Substring("AS".Length).Trim();
                            }
                            tableEnt.Alias = sValue;
                        }
                        //2.2 分析得到SCHEMA
                        sValue = m.Value.Trim();
                        iLast = sValue.LastIndexOf(".");
                        if (iLast > -1)
                        {
                            //键为旧表名，值为旧表别名
                            sValue = sValue.Substring(0, iLast).Trim();
                            tableEnt.Schema = sValue;
                        }
                    }
                }

                //判断是否要转换列
                if (isNeedConvertColumn)
                {
                    #region 替换列名的处理
                    //替换列清单 
                    List<SqlReplaceColumnEntity> lstReplaceCols = new List<SqlReplaceColumnEntity>();

                    //3.获取要替换的列名：只针对有别名的
                    string sPatter = @"\s*\w+\.+\w+\s*";
                    Regex regexTime = new Regex(sPatter, RegexOptions.IgnoreCase);
                    MatchCollection mcCollTime = regexTime.Matches(sTemplateString);

                    foreach (Match m in mcCollTime)
                    {
                        string sValue = m.Value.Trim();
                        string[] arr = sValue.Split(new char[] { '.' });
                        if (arr.Length < 2)
                        {
                            continue;
                        }

                        //创建实体
                        SqlReplaceColumnEntity colEntity = new SqlReplaceColumnEntity();
                        colEntity.OldTableAlias = arr[0]; //旧表别名
                        colEntity.OldCol = arr[1]; //旧表列编码
                        //得到列编码和表别名：根据表别名获取旧表编码
                        var listTable = tablesOkEntity.Where(t => t.Alias.Equals(colEntity.OldTableAlias, StringComparison.OrdinalIgnoreCase)); 
                        if (listTable.Count() == 0)
                        {
                            continue; //找不到别名对应的旧表，直接跳下一个
                        }
                        colEntity.OldTable = listTable.First().Name; //旧表别名：注这里只读第一个。目前无法解决表出现多次有多个别名的问题

                        //优先使用选择的新旧列关系：如能找到，则直接下一个
                        if (isHasOldNewColumnRelation)
                        {
                            sFiter = string.Format("{0}='{1}' and {2}='{3}'", NewOldColumnXmlConfig.ValueString.OldTable, colEntity.OldTable, NewOldColumnXmlConfig.ValueString.OldColumn, colEntity.OldCol);
                            DataRow[] drSelectNewOldColumn = dtOldNewColumn.Select(sFiter);
                            if (drSelectNewOldColumn.Length > 0)
                            {
                                if(lstReplaceCols.Where(t=>t.OldTable== colEntity.OldTable && t.OldCol == colEntity.OldCol).Count() > 0)
                                {
                                    continue;
                                }
                                //能找到新旧列关系，那么得到新表名和新列名
                                colEntity.NewTable = drSelectNewOldColumn[0][NewOldColumnXmlConfig.ValueString.NewTable].ToString();
                                colEntity.NewCol = drSelectNewOldColumn[0][NewOldColumnXmlConfig.ValueString.NewColumn].ToString();
                                lstReplaceCols.Add(colEntity);
                                continue;
                            }
                        }

                        //根据旧表名和列名取出列中文名
                        sFiter = string.Format("{0}='{1}' and {2}='{3}'", DBColumnSimpleEntity.SqlString.TableName, colEntity.OldTable, DBColumnSimpleEntity.SqlString.Name, colEntity.OldCol);
                        DataRow[] drOld = dtOldCol.Select(sFiter);
                        if (drOld.Length == 0)
                        {
                            continue; //找不到旧表字段，直接跳下一个
                        }
                        //得到旧表列中文名
                        colEntity.OldColCn = drOld[0][DBColumnSimpleEntity.SqlString.NameCN].ToString();

                        //从新旧表关系中取出新表
                        sFiter = string.Format("OLD='{0}'", colEntity.OldTable);
                        DataRow[] drOldNewRel = dtOldNew.Select(sFiter);
                        if (drOldNewRel.Length == 0)
                        {
                            continue; //找不到旧表对应的新表，直接跳下一个
                        }
                        colEntity.NewTable = drOldNewRel[0]["NEW"].ToString();
                        //根据新表名和列中文名查找新列编码
                        sFiter = string.Format("{0}='{1}' and {2}='{3}'", DBColumnSimpleEntity.SqlString.TableName, colEntity.NewTable, DBColumnSimpleEntity.SqlString.NameCN, colEntity.OldColCn);
                        DataRow[] drNewCol = dtNewCol.Select(sFiter);
                        if (drNewCol.Length == 0)
                        {
                            continue; //找不到旧表对应的新表，直接跳下一个
                        }
                        colEntity.NewCol = drNewCol[0][DBColumnSimpleEntity.SqlString.Name].ToString();
                        //加载替换清单
                        lstReplaceCols.Add(colEntity);
                    }

                    //4.处理新旧字段替换：旧字段长度长的优先处理
                    var ordTable = lstReplaceCols.AsEnumerable().OrderByDescending(t => t.OldCol.Length);
                    List<string> lstHasReplaceCol = new List<string>(); //已替换的列表
                    foreach (var ent in ordTable)
                    {
                        string sSourceCol = ent.OldTableAlias + "." + ent.OldCol;
                        if (!lstHasReplaceCol.Contains(sSourceCol))
                        {
                            //要加上别名一起替换
                            sTemplateString = sTemplateString.Replace(sSourceCol, ent.OldTableAlias + "." + ent.NewCol);
                            lstHasReplaceCol.Add(sSourceCol);
                        }                        
                    }
                    //5.实体绑定替换字段列表
                    var fdc = new FlexGridColumnDefinition();
                    fdc.AddColumn(
                        FlexGridColumn.NewRowNoCol(),
                        new FlexGridColumn.Builder().Name(SqlReplaceColumnEntity.PropString.OldTable).Caption("旧表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                        new FlexGridColumn.Builder().Name(SqlReplaceColumnEntity.PropString.OldCol).Caption("旧列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                        new FlexGridColumn.Builder().Name(SqlReplaceColumnEntity.PropString.OldColCn).Caption("旧列中文名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                        new FlexGridColumn.Builder().Name(SqlReplaceColumnEntity.PropString.NewCol).Caption("新列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                        new FlexGridColumn.Builder().Name(SqlReplaceColumnEntity.PropString.NewTable).Caption("新表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build()
                        );
                    dgvBeReplaceCol.Tag = fdc.GetGridTagString();
                    dgvBeReplaceCol.BindEntity(lstReplaceCols, fdc);
                    dgvBeReplaceCol.ShowRowNum(); 
                    #endregion
                }

                //加到字符构造对象中
                sbAllSql.Append(sTemplateString);
                rtbResult.Clear();

                //6.表名替换：要放最后
                //6.1 新旧表关系替换
                DataTable dtReplace = dgvOldNewChar.GetBindingTable();
                DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
                List<string> lstHasReplace = new List<string>(); //已替换的列表
                if (drReplace.Length > 0)
                {
                    var ordTable = drReplace.AsEnumerable().OrderByDescending(t => t["NEW"].ToString().Length);
                    foreach (DataRow dr in ordTable)
                    {
                        string sOldTableName = dr["OLD"].ToString().Trim();
                        string sNewTableName = dr["NEW"].ToString().Trim();
                        //替换SQL表
                        ReplaceSqlTable(sbAllSql, tablesOkEntity, lstHasReplace, sOldTableName, sNewTableName);
                    }
                }
                //6.2 新旧列关系中的新旧替换
                dtReplace = dgvOldNewColumnChar.GetBindingTable();
                drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
                if (drReplace.Length > 0)
                {
                    var ordTable = drReplace.AsEnumerable().OrderByDescending(t => t[NewOldColumnXmlConfig.ValueString.NewTable].ToString().Length);
                    foreach (DataRow dr in ordTable)
                    {
                        string sOldTableName = dr[NewOldColumnXmlConfig.ValueString.OldTable].ToString().Trim();
                        string sNewTableName = dr[NewOldColumnXmlConfig.ValueString.NewTable].ToString().Trim();
                        //替换SQL表
                        ReplaceSqlTable(sbAllSql, tablesOkEntity, lstHasReplace, sOldTableName, sNewTableName);
                    }
                }

                //7、针对@参数，替换为'#参数#'格式
                if (ckbParamToHash.Checked)
                {
                    string sParamPatter = @"@\w+";
                    Regex regexParam = new Regex(sParamPatter, RegexOptions.IgnoreCase);
                    MatchCollection mcCollParam = regexParam.Matches(sbAllSql.ToString());
                    var listParam = new SortedSet<string>();
                    foreach (Match m in mcCollParam)
                    {
                        listParam.Add(m.Value);
                    }
                    //按长度从长到短来替换
                    foreach (var sValue in listParam.OrderByDescending(t => t.Length))
                    {
                        string sNewValue = @"'#" + sValue.Replace("@", "") + "#'";
                        sbAllSql.Replace(sValue, sNewValue); //替换掉参数
                    }
                }

                 //得到最终字符
                rtbResult.AppendText(sbAllSql.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_SourceDbType, cbbSourceDbType.SelectedValue.ToString(), "【数据库间SQL转换】源数据库类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_TargetDbType, cbbTargetDbType.SelectedValue.ToString(), "【数据库间SQL转换】目标数据库类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_NewOldColumnSourceType, cbbNewOldColumnSourceType.SelectedValue.ToString(), "【数据库间SQL转换】转换类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_LatestSql, rtbInputSql.Text.Trim(), "【数据库间SQL转换】最后输入的SQL");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_IsParaToHash, ckbParamToHash.Checked?"1":"0", "【数据库间SQL转换】@参数转#参数#");
                WinFormContext.UserLoveSettings.Save();

                stopwatch.Stop(); //结束计时
                string sNeedSecond = (stopwatch.ElapsedMilliseconds/1000.00).ToString();
                //生成SQL成功后提示
                ShowInfo(_strAutoSqlSuccess + "共耗时："+ sNeedSecond +" 秒");
                rtbResult.Select(0, 0); //返回到第一行
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
                if (stopwatch != null)
                {
                    stopwatch.Stop(); //结束计时
                }
            }
        }

        /// <summary>
        /// 替换SQL表
        /// </summary>
        /// <param name="sbAllSql"></param>
        /// <param name="tablesOkEntity"></param>
        /// <param name="lstHasReplace"></param>
        /// <param name="sOldTableName"></param>
        /// <param name="sNewTableName"></param>
        private static void ReplaceSqlTable(StringBuilder sbAllSql, List<SqlTableEntity> tablesOkEntity, List<string> lstHasReplace,string sOldTableName,string sNewTableName)
        {
            if (!lstHasReplace.Contains(sOldTableName))
            {
                //替换包含Schema的旧表名为新表名
                var listTable = tablesOkEntity.Where(t => t.Name.Equals(sOldTableName, StringComparison.OrdinalIgnoreCase));
                if (listTable.Count() > 0)
                {
                    string sSchema = listTable.First().Schema;
                    if (!string.IsNullOrEmpty(sSchema))
                    {
                        sbAllSql.Replace(sSchema + "." + sOldTableName, sNewTableName);
                        lstHasReplace.Add(sOldTableName);
                    }
                }
                else
                {
                    //替换旧表名为新表名
                    sbAllSql.Replace(sOldTableName, sNewTableName);
                    lstHasReplace.Add(sOldTableName);
                }
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoadDataSource_Click(object sender, EventArgs e)
        {
            rtbInputSql.Focus();
            bool isChangeTap = false;
            int iTableNum = 500;//大于等于多少个时提示
            //获取当前绑定表
            DataTable dtMain = dgvTableListSource.GetBindingTable();
            DataTable dtAllCol = dgvColListSource.GetBindingTable();
            if (ckbAllTableColumnSource.Checked)
            {
                if (dtMain != null && dtMain.Rows.Count >= iTableNum)
                {
                    if (MsgHelper.ShowYesNo("【获取所有表列清单】可能需要花点时间，是否继续？") == DialogResult.No)
                    {
                        return;
                    }
                }
                //全部获取
                isChangeTap = AddAllColumnsSource(dtAllCol, new List<string>());
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
                        ckbAllTableColumnSource.Checked = false;
                        return;
                    }
                }
                List<string> list = new List<string>();
                foreach (DataRow dr in drArr)
                {
                    list.Add(dr[DBTableEntity.SqlString.Name].ToString());
                }
                isChangeTap = AddAllColumnsSource(dtAllCol, list);
            }
            dgvColListSource.ShowRowNum(true); //显示序号
            if (_isChangeTabPage)
            {
                tabControl1.SelectedTab = tpTargetDb;
            }
        }

        private bool AddAllColumnsSource(DataTable dtAllCol, List<string> list)
        {
            bool isChangeTap = false;
            DataTable dtQueryCols = _dataAccessSource.GetSqlSchemaTableColumns(list, _dbServerSource.SchemaName);
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

        private void btnLoadDataTarget_Click(object sender, EventArgs e)
        {
            rtbInputSql.Focus();
            bool isChangeTap = false;
            int iTableNum = 500;//大于等于多少个时提示
            //获取当前绑定表
            DataTable dtMain = dgvTableListTarget.GetBindingTable();
            DataTable dtAllCol = dgvColListTarget.GetBindingTable();
            if (ckbAllTableColumnTarget.Checked)
            {
                if (dtMain != null && dtMain.Rows.Count >= iTableNum)
                {
                    if (MsgHelper.ShowYesNo("【获取所有表列清单】可能需要花点时间，是否继续？") == DialogResult.No)
                    {
                        return;
                    }
                }
                //全部获取
                isChangeTap = AddAllColumnsTarget(dtAllCol, new List<string>());
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
                        ckbAllTableColumnTarget.Checked = false;
                        return;
                    }
                }
                List<string> list = new List<string>();
                foreach (DataRow dr in drArr)
                {
                    list.Add(dr[DBTableEntity.SqlString.Name].ToString());
                }
                isChangeTap = AddAllColumnsTarget(dtAllCol, list);
            }
            dgvColListTarget.ShowRowNum(true); //显示序号
            if (_isChangeTabPage)
            {
                tabControl1.SelectedTab = tpConvert;
            } 
        }

        private bool AddAllColumnsTarget(DataTable dtAllCol, List<string> list)
        {
            bool isChangeTap = false;
            DataTable dtQueryCols = _dataAccessTarget.GetSqlSchemaTableColumns(list, _dbServerTarget.SchemaName);
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


        private void btnExcludeTableSource_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbExcludeTableSource.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            DataTable dtFtpFile = dgvTableListSource.GetBindingTable();
            if (dtFtpFile.Rows.Count == 0) return;

            var query = from f in dtFtpFile.AsEnumerable()
                        where GetLinqDynamicWhere(sFilter, f)
                        select f;
            foreach (var item in query.ToList())
            {
                item[_sGridColumnSelect] = "0"; //设置为不选中
            }

            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableSource, ckbIsAutoExcludeSource.Checked ? "1" : "0", "【数据库间SQL转换】是否自动排除源表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListSource, txbExcludeTableSource.Text.Trim(), "【数据库间SQL转换】排除源表列表");
            WinFormContext.UserLoveSettings.Save();
        }

        private void btnExcludeTableTarget_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbExcludeTableTarget.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            DataTable dtFtpFile = dgvTableListTarget.GetBindingTable();
            if (dtFtpFile.Rows.Count == 0) return;

            var query = from f in dtFtpFile.AsEnumerable()
                        where GetLinqDynamicWhere(sFilter, f)
                        select f;
            foreach (var item in query.ToList())
            {
                item[_sGridColumnSelect] = "0"; //设置为不选中
            }

            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableTarget, ckbIsAutoExcludeTarget.Checked ? "1" : "0", "【数据库间SQL转换】是否自动排除目标表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListTarget, txbExcludeTableTarget.Text.Trim(), "【数据库间SQL转换】排除目标表列表");
            WinFormContext.UserLoveSettings.Save();
        }

        private static bool GetLinqDynamicWhere(string[] filterArr, DataRow drF)
        {
            foreach (var item in filterArr)
            {
                string sFilePath = drF.Field<string>(DBTableEntity.SqlString.Name);
                if (sFilePath.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }


        #region 表清单查找事件
        private void btnFindNextSource_Click(object sender, EventArgs e)
        {
            FindGridTextSource(true);
        }

        private void btnFindFrontSource_Click(object sender, EventArgs e)
        {
            FindGridTextSource(false);
        }

        private void FindGridTextSource(bool isNext)
        {
            string sSearch = txbSearchTableNameSource.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvTableListSource.SeachText(sSearch, ref dgvFindTextSource, null, isNext);
            lblFindSource.Text = dgvFindTextSource.CurrentMsg;
        }

        private void btnFindNextTarget_Click(object sender, EventArgs e)
        {
            FindGridTextTarget(true);
        }

        private void btnFindFrontTarget_Click(object sender, EventArgs e)
        {
            FindGridTextTarget(false);
        }

        private void FindGridTextTarget(bool isNext)
        {
            string sSearch = txbSearchTableNameTarget.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvTableListTarget.SeachText(sSearch, ref dgvFindTextTarget, null, isNext);
            lblFindTarget.Text = dgvFindTextTarget.CurrentMsg;
        } 
        #endregion

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

        #region 新旧表模板相关
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
                ShowInfo("请录入新旧表编码！");
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
            if (isAdd)
            {
                txbReplaceTemplateName.Text = string.Empty;
            }
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

        private void dgvOldNewChar_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvOldNewChar, ref _allSelectOldNewChar, e);
        }
        #endregion

        #region 网格头双击选择
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

        private void dgvTableListSource_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvTableListSource, ref _allSelectTableSource, e);
        }
        private void dgvTableListTarget_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvTableListTarget, ref _allSelectTableTarget, e);
        } 
        #endregion

        #region 设置Tag方法
        private void SetColTag()
        {
            DataTable dtColsAll = DBColumnSimpleEntity.GetTableStruct();
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtColsAll.Columns.Add(dcSelected);

            //绑定源列清单
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

            dgvColListSource.Tag = fdc.GetGridTagString();
            dgvColListSource.BindDataGridView(dtColsAll, true);
            //绑定目标列清单
            dgvColListTarget.Tag = fdc.GetGridTagString();
            dgvColListTarget.BindDataGridView(dtColsAll.Copy(), true);

            //新旧表字符网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("OLD").Caption("旧表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("NEW").Caption("新表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build()
                );
            dgvOldNewChar.Tag = fdc.GetGridTagString();
            dgvOldNewChar.BindDataGridView(null, false);
            dgvOldNewChar.AllowUserToAddRows = true;
            //新旧列字符网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(NewOldColumnXmlConfig.ValueString.OldTable).Caption("旧表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(NewOldColumnXmlConfig.ValueString.OldColumn).Caption("旧列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(NewOldColumnXmlConfig.ValueString.NewTable).Caption("新表名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(NewOldColumnXmlConfig.ValueString.NewColumn).Caption("新列名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(true).Visible().Build()
                );
            dgvOldNewColumnChar.Tag = fdc.GetGridTagString();
            dgvOldNewColumnChar.BindDataGridView(null, false);
            dgvOldNewColumnChar.AllowUserToAddRows = true;
        }
        #endregion

        private void cbbNewOldColumnSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbNewOldColumnSourceType.SelectedValue==null) return;

            if ("1".Equals(cbbNewOldColumnSourceType.SelectedValue.ToString()))
            {
                //自行录入
                uC_DbConnectionTarget.Visible = false;
                uC_DbConnectionSource.Visible= false;
                tabControl1.TabPages.Remove(tpSourceDb);
                tabControl1.TabPages.Remove(tpTargetDb);
                btnConnectLoadAll.Visible = false;
                cbbSourceDbType.SetControlReadOnly(false);
                cbbTargetDbType.SetControlReadOnly(false);
                btnLoadAllGenerate.Visible = false;
            }
            else
            {
                //读取数据库
                uC_DbConnectionTarget.Visible = true;
                uC_DbConnectionSource.Visible = true;
                tabControl1.TabPages.Insert(0, tpTargetDb);
                tabControl1.TabPages.Insert(0,tpSourceDb);
                btnConnectLoadAll.Visible = true;
                cbbSourceDbType.SetControlReadOnly(true);
                cbbTargetDbType.SetControlReadOnly(true);
                btnLoadAllGenerate.Visible = true;
            }
        }

        /// <summary>
        /// 源连接数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSourceConnect_Click(object sender, EventArgs e)
        {
            _dbServerSource = await uC_DbConnectionSource.GetDbServerInfo();
            if (_dbServerSource == null) return;
            _dataAccessSource = AutoSQLExecutors.Connect(_dbServerSource);
            DataTable dtTable = uC_DbConnectionSource.userTableDic[uC_DbConnectionSource.LatestDbServerInfo.DbConnKey];
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
            dgvTableListSource.Tag = fdc.GetGridTagString();
            dgvTableListSource.BindDataGridView(dtTable, true);
            dgvTableListSource.ShowRowNum();
            //tabControl1.SelectedTab = tpSourceDb;//选中表页签

            if (ckbIsAutoExcludeSource.Checked)
            {
                btnExcludeTableSource.PerformClick();
            }
            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableSource, ckbIsAutoExcludeSource.Checked ? "1" : "0", "【数据库间SQL转换】是否自动排除表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListSource, txbExcludeTableSource.Text.Trim(), "【数据库间SQL转换】排除表列表");
            WinFormContext.UserLoveSettings.Save();

            //是否清除数据
            if (ckbClearAllColSource.Checked)
            {
                dgvColListSource.GetBindingTable().Clear();
            }
        }

        /// <summary>
        /// 目标连接数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnTargetConnect_Click(object sender, EventArgs e)
        {
            _dbServerTarget = await uC_DbConnectionTarget.GetDbServerInfo();
            if (_dbServerTarget == null) return;
            _dataAccessTarget = AutoSQLExecutors.Connect(_dbServerTarget);
            DataTable dtTable = uC_DbConnectionTarget.userTableDic[uC_DbConnectionTarget.LatestDbServerInfo.DbConnKey];
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
            dgvTableListTarget.Tag = fdc.GetGridTagString();
            dgvTableListTarget.BindDataGridView(dtTable, true);
            dgvTableListTarget.ShowRowNum();
            //tabControl1.SelectedTab = tpTargetDb;//选中表页签

            if (ckbIsAutoExcludeTarget.Checked)
            {
                btnExcludeTableTarget.PerformClick();
            }
            //保存喜好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_IsAutoExcludeTableTarget, ckbIsAutoExcludeTarget.Checked ? "1" : "0", "【数据库间SQL转换】是否自动排除表名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DbSqlConvert_ExcludeTableListTarget, txbExcludeTableTarget.Text.Trim(), "【数据库间SQL转换】排除表列表");
            WinFormContext.UserLoveSettings.Save();

            //是否清除数据
            if (ckbClearAllColTarget.Checked)
            {
                dgvColListTarget.GetBindingTable().Clear();
            }
        }

        private async void btnConnectLoadAll_Click(object sender, EventArgs e)
        {
            await LoadAllData(true);
        }

        private async Task<bool> LoadAllData(bool isOnlyLoadData)
        {
            if (!_isLoadDataFirst)
            {
                string sTipLoadGenerate = "在不改变源和目标数据库的情况下，修改SQL后，直接点击【生成】即可。\r\n不需要重新加载数据源的，是否重新加载数据源并生成？";
                string sTip = isOnlyLoadData ? "在不改变源和目标数据库的情况下,不需要重新加载数据源的，是否重新加载数据源？" : sTipLoadGenerate;
                if (ShowOkCancel(sTip) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            _dbServerSource = await uC_DbConnectionSource.GetDbServerInfo();
            if (_dbServerSource == null)
            {
                ShowInfo("请选择源数据库的连接！");
                return false;
            }

            _dbServerTarget = await uC_DbConnectionTarget.GetDbServerInfo();
            if (_dbServerTarget == null)
            {
                ShowInfo("请选择目标数据库的连接！");
                return false;
            }
            _isChangeTabPage = false;
            btnSourceConnect_Click(null, null);
            btnLoadDataSource_Click(null, null);
            btnTargetConnect_Click(null, null);
            btnLoadDataTarget_Click(null, null);
            ShowInfo("源、目标数据库的数据全部加载完成！");
            _isChangeTabPage = true;
            _isLoadDataFirst = false; //非第一次加载数据
            return true;
        }

        private async void btnLoadAllGenerate_Click(object sender, EventArgs e)
        {
            if(await LoadAllData(false))
            {
                btnGenerate_Click(null, null);
            }
        }

        #region 新旧列模板相关
        private void btnSaveColumnReplaceTemplate_Click(object sender, EventArgs e)
        {
            string sTempName = txbReplaceColumnTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("模板名称不能为空！");
                return;
            }
            DataTable dtReplace = dgvOldNewColumnChar.GetBindingTable();
            dtReplace.DeleteNullRow();
            if (dtReplace.Rows.Count == 0)
            {
                ShowInfo("请录入新旧表编码！");
                return;
            }

            if (ShowOkCancel("确定要保存模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceColumnStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceColumnStringData.MoreXmlConfig.ValData;

            string sKeyIdNew = string.Empty;
            bool isAdd = string.IsNullOrEmpty(cbbColumnTemplateType.Text.Trim()) ? true : false;
            if (isAdd)
            {
                //新增
                sKeyIdNew = Guid.NewGuid().ToString();
                DataRow dr = dtKeyConfig.NewRow();
                dr[sKeyId] = sKeyIdNew;
                dr[NewOldColumnXmlConfig.KeyString.Name] = sTempName;
                dtKeyConfig.Rows.Add(dr);
            }
            else
            {
                //修改
                string sKeyIDValue = cbbColumnTemplateType.SelectedValue.ToString();
                sKeyIdNew = sKeyIDValue;
                DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                if (drArrKey.Length == 0)
                {
                    DataRow dr = dtKeyConfig.NewRow();
                    dr[sKeyId] = sKeyIdNew;
                    dr[NewOldColumnXmlConfig.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][NewOldColumnXmlConfig.KeyString.Name] = sTempName;//修改名称
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
                drNew[NewOldColumnXmlConfig.ValueString.IsSelected] = dr[NewOldColumnXmlConfig.ValueString.IsSelected].ToString();
                drNew[NewOldColumnXmlConfig.ValueString.OldTable] = dr[NewOldColumnXmlConfig.ValueString.OldTable].ToString();
                drNew[NewOldColumnXmlConfig.ValueString.OldColumn] = dr[NewOldColumnXmlConfig.ValueString.OldColumn].ToString();
                drNew[NewOldColumnXmlConfig.ValueString.NewTable] = dr[NewOldColumnXmlConfig.ValueString.NewTable].ToString();
                drNew[NewOldColumnXmlConfig.ValueString.NewColumn] = dr[NewOldColumnXmlConfig.ValueString.NewColumn].ToString();
                dtValConfig.Rows.Add(drNew);
            }
            replaceColumnStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbColumnTemplateType.BindDropDownList(replaceColumnStringData.MoreXmlConfig.KeyData, sKeyId, NewOldColumnXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板保存成功！");
            if (isAdd)
            {
                txbReplaceColumnTemplateName.Text = string.Empty;
            }
        }

        private void btnRemoveColumnTemplate_Click(object sender, EventArgs e)
        {
            if (cbbColumnTemplateType.SelectedValue == null)
            {
                ShowInfo("请选择一个模板！");
                return;
            }
            string sKeyIDValue = cbbColumnTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue))
            {
                ShowInfo("请选择一个模板！");
                return;
            }

            if (ShowOkCancel("确定要删除该模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceColumnStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceColumnStringData.MoreXmlConfig.ValData;
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
            replaceColumnStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbColumnTemplateType.BindDropDownList(replaceColumnStringData.MoreXmlConfig.KeyData, sKeyId, NewOldColumnXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板删除成功！");
        }

        private void cbbColumnTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumnTemplateType.SelectedValue == null) return;
            string sTempType = cbbColumnTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sTempType))
            {
                //txbReplaceColumnTemplateName.ReadOnly = false;
                txbReplaceColumnTemplateName.Text = string.Empty;
                return;
            }

            txbReplaceColumnTemplateName.Text = cbbColumnTemplateType.Text;
            //txbReplaceColumnTemplateName.ReadOnly = true;
            string sKeyId = replaceColumnStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            DataRow[] drArr = replaceColumnStringData.MoreXmlConfig.ValData.Select(sKeyId + "='" + sTempType + "'");

            DataTable dtReplace = dgvOldNewColumnChar.GetBindingTable();
            if (drArr.Length > 0)
            {
                dtReplace.Rows.Clear();
                foreach (DataRow dr in drArr)
                {
                    dtReplace.Rows.Add(dtReplace.Rows.Count + 1,
                        dr[NewOldColumnXmlConfig.ValueString.IsSelected].ToString(),
                        dr[NewOldColumnXmlConfig.ValueString.OldTable].ToString(),
                        dr[NewOldColumnXmlConfig.ValueString.OldColumn].ToString(),
                        dr[NewOldColumnXmlConfig.ValueString.NewTable].ToString(),
                        dr[NewOldColumnXmlConfig.ValueString.NewColumn].ToString()
                        );
                }
            }
            else if (dtReplace != null)
            {
                dtReplace.Clear();
            }
        }

        private void dgvOldNewColumnChar_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvOldNewColumnChar.Columns[e.ColumnIndex].Name == _sGridColumnSelect)
            {
                return;
            }
        }

        private void dgvOldNewColumnChar_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvOldNewColumnChar, ref _allSelectOldNewColumnChar, e);
        }

        private void dgvOldNewColumnChar_KeyDown(object sender, KeyEventArgs e)
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
                    if (iRow == 0 || iColumn < 4)
                    {
                        return;
                    }

                    DataTable dtMain = dgvOldNewColumnChar.GetBindingTable();
                    dtMain.Rows.Clear();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string strData2 = data[j, 1].ToString().Trim();
                        string strData3 = data[j, 2].ToString().Trim();
                        string strData4 = data[j, 3].ToString().Trim();
                        //只要有其中一个为空，就跳过
                        if (string.IsNullOrEmpty(strData) || string.IsNullOrEmpty(strData2) || string.IsNullOrEmpty(strData3) || string.IsNullOrEmpty(strData4))
                        {
                            continue;
                        }

                        string sFilter = string.Format("{0}='{1}' AND {2}='{3}'", NewOldColumnXmlConfig.ValueString.OldTable, strData, NewOldColumnXmlConfig.ValueString.OldColumn, strData2);
                        if (dtMain.Select(sFilter).Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.Rows.Count + 1, "1", strData, strData2, strData3, strData4);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        } 
        #endregion
    }
}
