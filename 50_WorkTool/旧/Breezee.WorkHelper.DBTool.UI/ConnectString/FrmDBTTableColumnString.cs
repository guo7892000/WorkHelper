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

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 表列字符拼接
    /// </summary>
    public partial class FrmDBTTableColumnString : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private readonly string _strColName = "变更列清单";

        private readonly string _sGridColumnCondition = "IsCondition";
        private readonly string _sGridColumnSelect = "IsSelect";
        private readonly string _sGridColumnDynamic = "IsDynamic";
        private bool _allSelect = false;//默认全选，这里取反
        private bool _allCondition = true;//默认全不选，这里取反
        private bool _allDynamic = false;//默认全选，这里取反
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
        #endregion

        #region 构造函数
        public FrmDBTTableColumnString()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmGetOracleSql_Load(object sender, EventArgs e)
        {
            _IDBDefaultValue = ContainerContext.Container.Resolve<IDBDefaultValue>();

            _dicString.Add("1", "Mybatis实体");
            _dicString.Add("2", "自定义");
            cbbModule.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);            

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            #endregion

            rtbOther.Visible = false;
            //设置下拉框查找数据源
            cbbTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
                dr[DBTableEntity.SqlString.Schema] = drArr[0][DBTableEntity.SqlString.Schema].ToString();
                dr[DBTableEntity.SqlString.Comments] = drArr[0][DBTableEntity.SqlString.Comments].ToString();
                dtTable.Rows.Add(dr);
            }
            dtTable.TableName = _strTableName;
            //设置Tag
            SetTableTag(dtTable);
            SetColTag(dtTable);
            //查询全局的默认值配置
            _dicQuery[DT_DBT_BD_COLUMN_DEFAULT.SqlString.IS_ENABLED] = "1";
            _dtDefault = _IDBDefaultValue.QueryDefaultValue(_dicQuery).SafeGetDictionaryTable(); //获取默认值、排除列配置信息
            //导入成功后处理
            tsbAutoSQL.Enabled = true;
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
            DataTable dtCols = _dataAccess.GetSqlSchemaTableColumns(cbbTableName.Text.Trim(), sSchema);
            DataTable dtColsNew = new DataTable();
            dtColsNew.Columns.AddRange(new DataColumn[] {
            new DataColumn("T1"),new DataColumn("T2"),new DataColumn("T3"),
            new DataColumn("A1"),new DataColumn("A2"),new DataColumn("A3"),
            new DataColumn("B"),
            new DataColumn("C"),
            new DataColumn("D"),
            new DataColumn("E"),
            new DataColumn("F"),
            new DataColumn("G"),
            new DataColumn("H"),
            new DataColumn("I"),
            new DataColumn("J"),
            new DataColumn("K"),
            new DataColumn("L"),
            });
            foreach (DataRow dr in dtCols.Rows)
            {
                DataRow drNew = dtColsNew.NewRow();
                string sTableName = dr[DBColumnEntity.SqlString.TableName].ToString();//表名
                string sColName = dr[DBColumnEntity.SqlString.Name].ToString();//列名
                drNew["T1"] = sTableName;
                drNew["T2"] = FirstLetterUpper(sTableName);//表名首字母大写
                drNew["T3"] = FirstLetterUpper(sTableName,false);//表名首字母大写（第一个除外）
                drNew["A1"] = sColName;
                drNew["A2"] = FirstLetterUpper(sColName);//列名首字母大写
                drNew["A3"] = FirstLetterUpper(sColName, false);//列名首字母大写
                drNew["B"] = dr[DBColumnEntity.SqlString.NameCN];
                drNew["C"] = dr[DBColumnEntity.SqlString.DataType];
                drNew["D"] = dr[DBColumnEntity.SqlString.DataLength];
                drNew["E"] = dr[DBColumnEntity.SqlString.DataPrecision];
                drNew["F"] = dr[DBColumnEntity.SqlString.DataScale];
                drNew["G"] = dr[DBColumnEntity.SqlString.DataTypeFull];
                drNew["H"] = dr[DBColumnEntity.SqlString.KeyType];
                drNew["I"] = dr[DBColumnEntity.SqlString.NotNull];
                drNew["J"] = dr[DBColumnEntity.SqlString.SortNum];
                drNew["K"] = dr[DBColumnEntity.SqlString.Comments];
                dtColsNew.Rows.Add(drNew);
            }
            //增加选择列
            DataColumn dcSelected = new DataColumn(_sGridColumnSelect);
            dcSelected.DefaultValue = "1";
            dtColsNew.Columns.Add(dcSelected);
            //查询结果
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(40).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name("T1").Caption("T1").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("T2").Caption("T2").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("T3").Caption("T3").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("A1").Caption("A1").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("A2").Caption("A2").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("A3").Caption("A3").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("B").Caption("B").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("C").Caption("C").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("D").Caption("D").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("E").Caption("E").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("F").Caption("F").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("G").Caption("G").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("H").Caption("H").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("I").Caption("I").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("J").Caption("J").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("K").Caption("K").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build()
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
            DataTable dtMain = dgvTableList.GetBindingTable();
            DataTable dtSec = dgvColList.GetBindingTable();
            StringBuilder sbAllSql = new StringBuilder();
            if (dtMain == null) return;
            //移除空行
            dtMain.DeleteNullRow();
            //得到变更后数据
            dtMain.AcceptChanges();
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
                string sModule = "";
                if (cbbModule.SelectedValue != null && !string.IsNullOrEmpty(cbbModule.SelectedValue.ToString()))
                {
                    sModule = cbbModule.SelectedValue.ToString();
                }

                for (int i = 0; i < dtColumnSelect.Rows.Count; i++)
                {
                    string strOneData = rtbConString.Text;
                    foreach (DataColumn dc in dtColumnSelect.Columns)
                    {
                        string strData = dtColumnSelect.Rows[i][dc.ColumnName].ToString();
                        //将数据中的列名替换为单元格中的数据
                        strOneData = strOneData.Replace("#" + dc.ColumnName + "#", strData);
                        
                    }
                    if ("PK".Equals(dtColumnSelect.Rows[i]["H"].ToString()))
                    {
                        strOneData = strOneData.Replace("@TableField", "@TableId");
                    }

                    //所有SQL文本累加
                    sbAllSql.Append(strOneData + "\n");
                }
                rtbResult.Clear();
                //保存属性
                if ("1".Equals(sModule))
                {
                    //Mybatis实体拼接
                    string strEntity = rtbOther.Text.Replace("#T1#", dtColumnSelect.Rows[0]["T1"].ToString())
                            .Replace("#T2#", dtColumnSelect.Rows[0]["T2"].ToString())
                            .Replace("#T3#", dtColumnSelect.Rows[0]["T3"].ToString())
                            .Replace("#COL_LIST#", sbAllSql.ToString() + "\n");
                    rtbResult.AppendText(strEntity);
                }
                else
                {
                    rtbResult.AppendText(sbAllSql.ToString() + "\n");
                    Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());
                }
                
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
            if (e.ColumnIndex == dgvColList.Columns[_sGridColumnSelect].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnSelect].Value = _allSelect ? "1" : "0";
                }
                _allSelect = !_allSelect;
            } else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnCondition].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnCondition].Value = _allCondition ? "1" : "0";
                }
                _allCondition = !_allCondition;
            }
            else if (e.ColumnIndex == dgvColList.Columns[_sGridColumnDynamic].Index)
            {
                foreach (DataGridViewRow item in dgvColList.Rows)
                {
                    item.Cells[_sGridColumnDynamic].Value = _allDynamic ? "1" : "0";
                }
                _allDynamic = !_allDynamic;
            }
        }

        private void cbbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModule.SelectedValue != null && !string.IsNullOrEmpty(cbbModule.SelectedValue.ToString()))
            {
                string sModule = cbbModule.SelectedValue.ToString();
                if("1".Equals(sModule))
                {
                    rtbOther.Visible = true;
                    rtbConString.Clear();
                    rtbConString.AppendText(@"@ApiModelProperty(""#B#"")
@TableField(""#A1#"")
private String #A3#;
");
                    rtbOther.AppendText(@"@TableName(""#T1#"")
public class #T2# implements Serializable{
	private static final long serialVersionUID = 1L;

	#COL_LIST#
}");
                }else if ("2".Equals(sModule))
                {
                    rtbOther.Visible = false;
                    rtbConString.Clear();
                    rtbConString.AppendText(@"@ApiModelProperty(""#B#"")
private String #A3#;
");
                }
                else
                {
                    rtbOther.Visible= false;
                }
            }
        }
    }
}