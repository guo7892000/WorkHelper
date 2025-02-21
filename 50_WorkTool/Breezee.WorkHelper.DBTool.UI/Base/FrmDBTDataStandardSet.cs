using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.DataStandard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 名称：数据标准维护
    /// 作者：黄国辉
    /// 日期：2024-6-1
    /// 说明：维护数据标准
    /// </summary>
    public partial class FrmDBTDataStandardSet : BaseForm
    {
        IDBToolModule toolModule;
        DataStandardTypeConfigDate DbRelation;
        DataStandardConfig dataCfg;
        
        //数据类型配置文件
        DataStandardTypeConfigDate standardDate;
        DataStandardTypeConfigDecimal standardDecimal;
        DataStandardTypeConfigInt standardInt;
        DataStandardTypeConfigText standardText;
        //
        List<DBColumnControlRelation> listData = new List<DBColumnControlRelation>();
        bool isAdd = true;
        DataTable dtDataType;
        DataTable dtDataTypeSmall;
        DataTable dtIsEnabel;
        bool isModifyDate = false; //修改数据时，长度不要使用下拉框联动查出来的值（即不修改长度）

        public FrmDBTDataStandardSet()
        {
            InitializeComponent();
        }

        private void FrmDBTDataStandardSet_Load(object sender, EventArgs e)
        {
            standardDate = new DataStandardTypeConfigDate();
            standardDecimal = new DataStandardTypeConfigDecimal();
            standardInt = new DataStandardTypeConfigInt();
            standardText = new DataStandardTypeConfigText();

            //绑定下拉框
            toolModule = new IDBToolModule();
            dtDataType = toolModule.Context.KeyValuCfg.GetValues(DBToolKeyValueString.DataStandardType);

            cbbDataTypeBig.BindDropDownList(dtDataType, "vid", "name", true, true);
            cbbDataTypeQueryBig.BindDropDownList(dtDataType.Copy(), "vid", "name", true, true);
            //获取所有小类
            GetFullSmallTpey();
            //
            _dicString["1"] = "启用";
            _dicString["0"] = "禁用";
            dtIsEnabel = _dicString.GetTextValueTable(false);
            cbbQueryStatus.BindTypeValueDropDownList(dtIsEnabel, true, true);
            cbbStatus.BindTypeValueDropDownList(dtIsEnabel.Copy(), false, true);
            //
            dataCfg = new DataStandardConfig();
            SetTag();
            SetControlsRel();
        }

        /// <summary>
        /// 获取小类
        /// </summary>
        private void GetFullSmallTpey()
        {
            //小类
            dtDataTypeSmall = dtDataType.Clone();
            foreach (DataRow item in standardDate.MoreXmlConfig.ValData.Rows)
            {
                DataRow drNew = dtDataTypeSmall.NewRow();
                drNew["vid"] = item["vid"];
                drNew["name"] = item["name"];
                dtDataTypeSmall.Rows.Add(drNew);
            }
            foreach (DataRow item in standardDecimal.MoreXmlConfig.ValData.Rows)
            {
                DataRow drNew = dtDataTypeSmall.NewRow();
                drNew["vid"] = item["vid"];
                drNew["name"] = item["name"];
                dtDataTypeSmall.Rows.Add(drNew);
            }
            foreach (DataRow item in standardInt.MoreXmlConfig.ValData.Rows)
            {
                DataRow drNew = dtDataTypeSmall.NewRow();
                drNew["vid"] = item["vid"];
                drNew["name"] = item["name"];
                dtDataTypeSmall.Rows.Add(drNew);
            }
            foreach (DataRow item in standardText.MoreXmlConfig.ValData.Rows)
            {
                DataRow drNew = dtDataTypeSmall.NewRow();
                drNew["vid"] = item["vid"];
                drNew["name"] = item["name"];
                dtDataTypeSmall.Rows.Add(drNew);
            }
        }

        private void SetControlsRel()
        {
            listData.Add(new DBColumnControlRelation(DataStandardStr.Id, txbId, DBColumnControlReadSaveEnum.ReadAndSave));
            listData.Add(new DBColumnControlRelation(DataStandardStr.Name, txbColCode, DBColumnControlReadSaveEnum.ReadAndSave,"编码",true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.NameCN, txbColName, DBColumnControlReadSaveEnum.ReadAndSave, "名称", true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.DataLength, nudLength, DBColumnControlReadSaveEnum.ReadAndSave, "长度", true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.DataScale, nudDataScale, DBColumnControlReadSaveEnum.ReadAndSave, "小数位", true)); 
            listData.Add(new DBColumnControlRelation(DataStandardStr.IsEnable, cbbStatus, DBColumnControlReadSaveEnum.ReadAndSave, "状态", true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.BigType, cbbDataTypeBig, DBColumnControlReadSaveEnum.ReadAndSave, "大类", true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.SmallType, cbbDataTypeSmall, DBColumnControlReadSaveEnum.ReadAndSave, "小类", true));
            listData.Add(new DBColumnControlRelation(DataStandardStr.DataTypeFull, txbTypeLenSample));
            listData.Add(new DBColumnControlRelation(DataStandardStr.OracleDataTypeFull, txbTypeLenOralce));
            listData.Add(new DBColumnControlRelation(DataStandardStr.MySqlDataTypeFull, txbTypeLenMySql));
            listData.Add(new DBColumnControlRelation(DataStandardStr.SqlServerDataTypeFull, txbTypeLenSqlServer));
            listData.Add(new DBColumnControlRelation(DataStandardStr.PostgreSqlDataTypeFull, txbTypeLenPG));
            listData.Add(new DBColumnControlRelation(DataStandardStr.SQLiteDataTypeFull, txbTypeLenSQLite));
            listData.Add(new DBColumnControlRelation(DataStandardStr.Comments, txbRemark));
        }

        private void SetTag()
        {
            //通用列网格跟所有列网格结构一样
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(DataStandardStr.BigType).Caption("大类").Type(DataGridViewColumnTypeEnum.ComboBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(60).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.SmallType).Caption("小类").Type(DataGridViewColumnTypeEnum.ComboBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.Name).Caption("编码").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.NameCN).Caption("名称").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.NameUpper).Caption("大驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.NameLower).Caption("小驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.DataType).Caption("类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.DataLength).Caption("长度").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(60).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.DataScale).Caption("小数").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(60).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.MySqlDataTypeFull).Caption("全类型(MySql)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.OracleDataTypeFull).Caption("全类型(Oracle)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.SqlServerDataTypeFull).Caption("全类型(SqlServer)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.PostgreSqlDataTypeFull).Caption("全类型(PostgreSql)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.SQLiteDataTypeFull).Caption("全类型(SQLite)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.DataTypeFull).Caption("参考类型(全)").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.IsEnable).Caption("状态").Type(DataGridViewColumnTypeEnum.ComboBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(60).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name(DataStandardStr.Comments).Caption("备注").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(false).Visible().Build()
            );
            dgvQuery.Tag = fdc.GetGridTagString();
        }

        private void cbbDataTypeBig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbDataTypeBig.SelectedValue==null)
            {
                return;
            }
            string sKey = cbbDataTypeBig.SelectedValue.ToString();
            DataTable dtBind = null;
            if ("text".Equals(sKey))
            {
                dtBind = standardText.MoreXmlConfig.ValData;
            }
            else if("decimal".Equals(sKey))
            {
                dtBind = standardDecimal.MoreXmlConfig.ValData;
            }
            else if ("date".Equals(sKey))
            {
                dtBind = standardDate.MoreXmlConfig.ValData;
                nudLength.Value = 0;
            }
            else if ("int".Equals(sKey))
            {
                dtBind = standardInt.MoreXmlConfig.ValData;
            }
            if (dtBind != null)
            {
                cbbDataTypeSmall.BindDropDownList(dtBind, "vid", "name", true, true);
            }
        }

        private void cbbDataTypeQueryBig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDataTypeQueryBig.SelectedValue == null)
            {
                return;
            }
            string sKey = cbbDataTypeQueryBig.SelectedValue.ToString();
            DataTable dtBind = null;
            if ("text".Equals(sKey))
            {
                dtBind = standardText.MoreXmlConfig.ValData;
            }
            else if ("decimal".Equals(sKey))
            {
                dtBind = standardDecimal.MoreXmlConfig.ValData;
            }
            else if ("date".Equals(sKey))
            {
                dtBind = standardDate.MoreXmlConfig.ValData;
            }
            else if ("int".Equals(sKey))
            {
                dtBind = standardInt.MoreXmlConfig.ValData;
            }
            if (dtBind != null)
            {
                cbbDataTypeQuerySmall.BindDropDownList(dtBind, "vid", "name", true, true);
            }  
        }

        private void tsbQuery_Click(object sender, EventArgs e)
        {
            DataTable dtCommonCol = dataCfg.XmlConfig.Load();
            string sFilter = " 1=1 ";
            string sCode = txbQueryColCode.Text.Trim();
            string sName = txbQueryColName.Text.Trim();
            string sLen = txbQueryColLength.Text.Trim();
            string sStatus = cbbQueryStatus.SelectedValue.ToString();
            string sBig = cbbDataTypeQueryBig.SelectedValue.ToString();
            string sSamll = cbbDataTypeQuerySmall.SelectedValue == null ? "" : cbbDataTypeQuerySmall.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(sCode))
            {
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", DataStandardStr.Name, sCode);
            }
            if (!string.IsNullOrEmpty(sName))
            {
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", DataStandardStr.NameCN, sName);
            }
            if (!string.IsNullOrEmpty(sLen))
            {
                sFilter += string.Format(" AND {0} = '{1}'", DataStandardStr.DataLength, sLen);
            }
            if (!string.IsNullOrEmpty(sStatus))
            {
                sFilter += string.Format(" AND {0} = '{1}'", DataStandardStr.IsEnable, sStatus);
            }
            if (!string.IsNullOrEmpty(sBig))
            {
                sFilter += string.Format(" AND {0} = '{1}'", DataStandardStr.BigType, sBig);
            }
            if (!string.IsNullOrEmpty(sSamll))
            {
                sFilter += string.Format(" AND {0} = '{1}'", DataStandardStr.SmallType, sSamll);
            }

            DataRow[] drArr = dtCommonCol.Select(sFilter);
            if (drArr.Length > 0)
            {
                DataTable dtQuery = drArr.CopyToDataTable();
                dgvQuery.BindDataGridView(dtQuery, true);
            }
            else
            {
                dgvQuery.BindDataGridView(dtCommonCol.Clone(), true);
            }
            //大类下拉框
            var cmbBig = (DataGridViewComboBoxColumn)dgvQuery.Columns[DataStandardStr.BigType];
            cmbBig.ValueMember = "vid";
            cmbBig.DisplayMember = "name";
            cmbBig.DataSource = dtDataType;
            //小类下拉框
            cmbBig = (DataGridViewComboBoxColumn)dgvQuery.Columns[DataStandardStr.SmallType];
            cmbBig.ValueMember = "vid";
            cmbBig.DisplayMember = "name";
            cmbBig.DataSource = dtDataTypeSmall;
            //状态
            cmbBig = (DataGridViewComboBoxColumn)dgvQuery.Columns[DataStandardStr.IsEnable];
            cmbBig.ValueMember = DT_BAS_VALUE.VALUE_CODE;
            cmbBig.DisplayMember = DT_BAS_VALUE.VALUE_NAME;
            cmbBig.DataSource = dtIsEnabel;            
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var dr = dgvQuery.GetCurrentRow();
            if (dr == null)
            {
                ShowInfo("请选择一条记录！");
                return;
            }
            isModifyDate = true;
            listData.SetControlValue(dr, true);
            isAdd = false;
            isModifyDate = false;
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvQuery_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbEdit.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sErr = listData.JudgeNotNull(true);
            if (!string.IsNullOrEmpty(sErr))
            {
                ShowInfo(sErr);
                return;
            }
            DataTable dtSave = dataCfg.XmlConfig.Data;
            DataRow dr;
            string sId = txbId.Text.Trim();
            string sColCode = txbColCode.Text.Trim();
            string sKey = cbbDataTypeBig.SelectedValue.ToString();
            if ("decimal".Equals(sKey))
            {
                if (nudLength.Value < nudDataScale.Value)
                {
                    ShowInfo(string.Format("小数位应小于长度！", sColCode));
                    return;
                }
            }

            if (isAdd)
            {
                if (string.IsNullOrEmpty(sId))
                {
                    txbId.Text = Guid.NewGuid().ToString();
                }
                
                if (dtSave.FilterValue(DataStandardStr.Name, sColCode).Length > 0)
                {
                    ShowInfo(string.Format("列编码：{0} 已存在，不能新增，请选择双击修改！", sColCode));
                    return;
                }
                dr = dtSave.NewRow();
                listData.GetControlValue(dr, true);
            }
            else
            {

                DataRow[] drArr = dtSave.FilterValue(DataStandardStr.Name, sColCode);
                if (drArr.Length >1)
                {
                    ShowInfo(string.Format("列编码：{0} 已存在，请修改列编码！", sColCode));
                    return;
                }

                drArr = dtSave.FilterValue(DataStandardStr.Id, sId);
                if (drArr.Length == 0)
                {
                    ShowInfo(string.Format("数据已被修改，请查询后重试！", sColCode));
                    return;
                }
                dr = drArr[0];
                listData.GetControlValue(dr, false);
            }
            
            if ("decimal".Equals(sKey))
            {
                dr[DataStandardStr.DataPrecision] = dr[DataStandardStr.DataLength]; //针对小数，将长度赋值给精度
            }
            dr[DataStandardStr.NameUpper] = sColCode.FirstLetterUpper(true); //大驼峰
            dr[DataStandardStr.NameLower] = sColCode.FirstLetterUpper(false);//小驼峰
            //获取类型与长度
            GetDbTypeLength(dr);
            //保存数据
            dataCfg.XmlConfig.Save(dtSave);
            tsbQuery.PerformClick();
            btnReset.PerformClick();
            ShowInfo("保存成功！");
        }

        private void GetDbTypeLength(DataRow dr)
        {
            string dCol = dr[DataStandardStr.OracleDataTypeFull].ToString();
            if (dCol.IndexOf("(") > 0)
            {
                dr[DataStandardStr.OracleDataType] = dCol.Substring(0, dCol.IndexOf("("));
                dr[DataStandardStr.OracleDataLength] = dCol.Substring(dCol.IndexOf("(") + 1).Replace(")","");
            }
            dCol = dr[DataStandardStr.MySqlDataTypeFull].ToString();
            if (dCol.IndexOf("(") > 0)
            {
                dr[DataStandardStr.MySqlDataType] = dCol.Substring(0, dCol.IndexOf("("));
                dr[DataStandardStr.MySqlDataLength] = dCol.Substring(dCol.IndexOf("(") + 1).Replace(")", "");
            }
            dCol = dr[DataStandardStr.SqlServerDataTypeFull].ToString();
            if (dCol.IndexOf("(") > 0)
            {
                dr[DataStandardStr.SqlServerDataType] = dCol.Substring(0, dCol.IndexOf("("));
                dr[DataStandardStr.SqlServerDataLength] = dCol.Substring(dCol.IndexOf("(") + 1).Replace(")", "");
            }
            dCol = dr[DataStandardStr.PostgreSqlDataTypeFull].ToString();
            if (dCol.IndexOf("(") > 0)
            {
                dr[DataStandardStr.PostgreSqlDataType] = dCol.Substring(0, dCol.IndexOf("("));
                dr[DataStandardStr.PostgreSqlDataLength] = dCol.Substring(dCol.IndexOf("(") + 1).Replace(")", "");
            }
            dCol = dr[DataStandardStr.SQLiteDataTypeFull].ToString();
            if (dCol.IndexOf("(") > 0)
            {
                dr[DataStandardStr.SQLiteDataType] = dCol.Substring(0, dCol.IndexOf("("));
                dr[DataStandardStr.SQLiteDataLength] = dCol.Substring(dCol.IndexOf("(") + 1).Replace(")", "");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            groupBox3.ResetControl();
            groupBox4.ResetControl();
            isAdd = true;
        }

        private void cbbDataTypeSmall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbDataTypeQueryBig.SelectedValue==null || cbbDataTypeSmall.SelectedValue == null)
            {
                return;
            }
            string sKey = cbbDataTypeBig.SelectedValue.ToString();
            string sValue = cbbDataTypeSmall.SelectedValue.ToString();
            string sLen = nudLength.Value.ToString();
            nudDataScale.Visible = false;
            lblDataScale.Visible = false;
            DataTable dtKey = null;
            if ("text".Equals(sKey))
            {
                dtKey = standardText.MoreXmlConfig.KeyData;
                DataRow[] drArr = standardText.MoreXmlConfig.ValData.FilterValue(DataStandardTypeConfigText.ValueString.Vid,sValue);
                if(drArr != null && drArr.Length > 0)
                {
                    string sNewLen = drArr[0][DataStandardTypeConfigText.ValueString.Length].ToString();
                    if (!"?".Equals(sNewLen))
                    {
                        sLen = sNewLen;
                        if (!isModifyDate)
                        {
                            nudLength.Value = Convert.ToInt32(sLen);
                        }
                    }
                    txbTypeLenOralce.Text = string.Format("{0}({1})", dtKey.Rows[0][DataStandardTypeConfigText.KeyString.Oracle].ToString(), sLen);
                    txbTypeLenMySql.Text = string.Format("{0}({1})", dtKey.Rows[0][DataStandardTypeConfigText.KeyString.MySql].ToString(), sLen);
                    txbTypeLenSqlServer.Text = string.Format("{0}({1})", dtKey.Rows[0][DataStandardTypeConfigText.KeyString.SqlServer].ToString(), sLen);
                    txbTypeLenPG.Text = string.Format("{0}({1})", dtKey.Rows[0][DataStandardTypeConfigText.KeyString.PostgreSql].ToString(), sLen);
                    txbTypeLenSQLite.Text = string.Format("{0}({1})", dtKey.Rows[0][DataStandardTypeConfigText.KeyString.SQLite].ToString(), sLen);
                }
            }
            else if ("decimal".Equals(sKey))
            {
                nudDataScale.Visible = true;
                lblDataScale.Visible = true;
                dtKey = standardDecimal.MoreXmlConfig.KeyData;
                DataRow[] drArr = standardDecimal.MoreXmlConfig.ValData.FilterValue(DataStandardTypeConfigDecimal.ValueString.Vid, sValue);
                if (drArr != null && drArr.Length > 0)
                {
                    string sScaleLen = drArr[0][DataStandardTypeConfigDecimal.ValueString.Length].ToString();
                    if ("?".Equals(sScaleLen))
                    {
                        sScaleLen = nudDataScale.Value.ToString();
                    }
                    else
                    {
                        nudDataScale.Value = int.Parse(sScaleLen);
                    }
                    txbTypeLenOralce.Text = string.Format("{0}({1},{2})", dtKey.Rows[0][DataStandardTypeConfigDecimal.KeyString.Oracle].ToString(), sLen, sScaleLen);
                    txbTypeLenMySql.Text = string.Format("{0}({1},{2})", dtKey.Rows[0][DataStandardTypeConfigDecimal.KeyString.MySql].ToString(), sLen, sScaleLen);
                    txbTypeLenSqlServer.Text = string.Format("{0}({1},{2})", dtKey.Rows[0][DataStandardTypeConfigDecimal.KeyString.SqlServer].ToString(), sLen, sScaleLen);
                    txbTypeLenPG.Text = string.Format("{0}({1},{2})", dtKey.Rows[0][DataStandardTypeConfigDecimal.KeyString.PostgreSql].ToString(), sLen, sScaleLen);
                    txbTypeLenSQLite.Text = string.Format("{0}({1},{2})", dtKey.Rows[0][DataStandardTypeConfigDecimal.KeyString.SQLite].ToString(), sLen, sScaleLen);
                }
            }
            else if ("date".Equals(sKey))
            {
                DataRow[] drArr = standardDate.MoreXmlConfig.ValData.FilterValue(DataStandardTypeConfigDate.ValueString.Vid, sValue);
                if (drArr != null && drArr.Length > 0)
                {
                    if (nudLength.Value == 0)
                    {
                        txbTypeLenOralce.Text = drArr[0][DataStandardTypeConfigDate.ValueString.Oracle].ToString();
                        txbTypeLenMySql.Text = drArr[0][DataStandardTypeConfigDate.ValueString.MySql].ToString();
                        txbTypeLenSqlServer.Text = drArr[0][DataStandardTypeConfigDate.ValueString.SqlServer].ToString();
                        txbTypeLenPG.Text = drArr[0][DataStandardTypeConfigDate.ValueString.PostgreSql].ToString();
                        txbTypeLenSQLite.Text = drArr[0][DataStandardTypeConfigDate.ValueString.SQLite].ToString();
                    }
                    else
                    {
                        //日期加上括号长度，是否准确？？
                        txbTypeLenOralce.Text = string.Format("{0}({1})", drArr[0][DataStandardTypeConfigDate.ValueString.Oracle].ToString(), sLen);
                        txbTypeLenMySql.Text = string.Format("{0}({1})", drArr[0][DataStandardTypeConfigDate.ValueString.MySql].ToString(), sLen);
                        txbTypeLenSqlServer.Text = string.Format("{0}({1})", drArr[0][DataStandardTypeConfigDate.ValueString.SqlServer].ToString(), sLen);
                        txbTypeLenPG.Text = string.Format("{0}({1})", drArr[0][DataStandardTypeConfigDate.ValueString.PostgreSql].ToString(), sLen);
                        txbTypeLenSQLite.Text = string.Format("{0}({1})", drArr[0][DataStandardTypeConfigDate.ValueString.SQLite].ToString(), sLen);
                    }
                }
            }
            else if ("int".Equals(sKey))
            {
                DataRow[] drArr = standardInt.MoreXmlConfig.ValData.FilterValue(DataStandardTypeConfigInt.ValueString.Vid, sValue);
                if (drArr != null && drArr.Length > 0)
                {
                    if (nudLength.Value == 0)
                    {
                        txbTypeLenOralce.Text = drArr[0][DataStandardTypeConfigInt.ValueString.Oracle].ToString();
                        txbTypeLenMySql.Text = drArr[0][DataStandardTypeConfigInt.ValueString.MySql].ToString();
                        txbTypeLenSqlServer.Text = drArr[0][DataStandardTypeConfigInt.ValueString.SqlServer].ToString();
                        txbTypeLenPG.Text = drArr[0][DataStandardTypeConfigInt.ValueString.PostgreSql].ToString();
                        txbTypeLenSQLite.Text = drArr[0][DataStandardTypeConfigInt.ValueString.SQLite].ToString();
                    }
                    else
                    {
                        txbTypeLenOralce.Text = string.Format("{0}", drArr[0][DataStandardTypeConfigInt.ValueString.Oracle].ToString());
                        txbTypeLenMySql.Text = string.Format("{0}", drArr[0][DataStandardTypeConfigInt.ValueString.MySql].ToString());
                        txbTypeLenSqlServer.Text = string.Format("{0}", drArr[0][DataStandardTypeConfigInt.ValueString.SqlServer].ToString());
                        txbTypeLenPG.Text = string.Format("{0}", drArr[0][DataStandardTypeConfigInt.ValueString.PostgreSql].ToString());
                        txbTypeLenSQLite.Text = string.Format("{0}", drArr[0][DataStandardTypeConfigInt.ValueString.SQLite].ToString());
                    }
                }
            }
        }

        private void nudLength_ValueChanged(object sender, EventArgs e)
        {
            cbbDataTypeSmall_SelectedIndexChanged(null, null);
        }

        private void nudDataScale_ValueChanged(object sender, EventArgs e)
        {
            cbbDataTypeSmall_SelectedIndexChanged(null, null);
        }
    }
}
