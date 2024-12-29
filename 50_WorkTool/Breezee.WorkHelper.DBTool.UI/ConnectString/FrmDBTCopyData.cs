using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Setting = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 复制数据生成拼接字符
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTCopyData : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private BindingSource bsTable = new BindingSource();
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        CopyDataStringTemplate replaceStringData;//替换字符模板XML配置
        #endregion

        #region 构造函数
        public FrmDBTCopyData()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        private void FrmCopyData_Load(object sender, EventArgs e)
        {
            IDictionary<string, string> dic_List = new Dictionary<string, string>
            {
                { "1", "自定义拼接字符串" },
                { "2", "UNION清单" },
                { "3", "UNION ALL清单" }
            };
            cbbSqlType.BindTypeValueDropDownList(dic_List.GetTextValueTable(false), false, true);

            dic_List = new Dictionary<string, string>
            {
                { "0", "无" },
                { "1", "小驼峰式" },
                { "2", "大驼峰式" }
            };
            cbbWordConvert.BindTypeValueDropDownList(dic_List.GetTextValueTable(false), false, true);

            //数据库类型
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbDbType.BindTypeValueDropDownList(dtDbType, false, true);

            DataTable dtCopy = new DataTable();
            dtCopy.TableName = _strTableName;
            bsTable.DataSource = dtCopy;
            dgvTableList.DataSource = bsTable;
            WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy });
            //
            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            ckbAutoColumnName.Checked = true;
            
            //加载用户偏好值
            rtbConString.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelCopy_DataConnect, "").Value;
            cbbSqlType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelCopy_SqlType, "1").Value;
            cbbWordConvert.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelCopy_WordConvert, "0").Value;
            cbbDbType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelCopy_DbType, "2").Value;
            ckbAutoColumnName.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ExcelCopy_IsAutoWord, "1").Value) ? true : false;

            //加载模板数据
            replaceStringData = new CopyDataStringTemplate(DBTGlobalValue.CopyDataTemplateFileString.Xml_FileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, CopyDataStringTemplate.KeyString.Name, true, true);

        }
        #endregion

        #region 网格粘贴事件
        private void dgvTableList_KeyDown(object sender, KeyEventArgs e)
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

                    DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strTableName].DataSource;
                    dtMain.Clear();
                    dtMain.Columns.Clear();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain);
                    dgvTableList.ShowRowNum(); //显示行号
                    ShowInfo("粘贴成功，请选择拼接类型！");
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sbAll = new StringBuilder();
                string strSqlType = cbbSqlType.SelectedValue.ToString();
                if (strSqlType == "1")
                {
                    #region 拼接字符串
                    DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strTableName].DataSource;
                    if (dtMain.Rows.Count == 0)
                    {
                        ShowInfo("没有可生成的数据！");
                        return;
                    }
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        //初始化单条数据为书写的文本
                        string strOneData = ckbTrim.Checked ? rtbConString.Text.Trim(): rtbConString.Text;
                        string sConvert = cbbWordConvert.SelectedValue.ToString();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < dtMain.Columns.Count; j++)
                        {
                            string strData = ckbTrim.Checked ? dtMain.Rows[i][j].ToString().Trim(): dtMain.Rows[i][j].ToString();
                            if ("1".Equals(sConvert) || "2".Equals(sConvert))
                            {
                                string[] arrSplit = strData.ToLower().Split('_');
                                for (int k = 0; k < arrSplit.Length; k++)
                                {
                                    if(k==0)
                                    {
                                        sb.Append("2".Equals(sConvert) ? arrSplit[k][0].ToString().ToUpper() + arrSplit[k].Substring(1) : arrSplit[k]);
                                    }
                                    else
                                    {
                                        sb.Append(arrSplit[k][0].ToString().ToUpper() + arrSplit[k].Substring(1));
                                    }
                                }
                                strData = sb.ToString();
                            }

                            //将数据中的列名替换为单元格中的数据
                            strOneData = strOneData.Replace("#" + dtMain.Columns[j].ColumnName + "#", strData);
                        }
                        //所有SQL文本累加
                        sbAll.Append(strOneData + (ckbResultNewLine.Checked ? "\n" : ""));
                    }
                    #endregion
                }
                else
                {
                    #region UNION清单生成
                    string strUnion = "UNION SELECT ";
                    string strUnionAll = "UNION ALL SELECT ";
                    string strOrcEnd = " FROM DUAL ";
                    int iDbType = int.Parse(cbbDbType.SelectedValue.ToString());
                    DataBaseType selectDBType = (DataBaseType)iDbType;

                    DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strTableName].DataSource;
                    if (dtMain.Rows.Count == 0)
                    {
                        ShowInfo("没有可生成的数据！");
                        return;
                    }
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        //string strOneData = "";
                        StringBuilder sbOneData = new StringBuilder();
                        for (int j = 0; j < dtMain.Columns.Count; j++)
                        {
                            string strData = ckbTrim.Checked ? dtMain.Rows[i][j].ToString().Trim() : dtMain.Rows[i][j].ToString();
                            if (i == 0)
                            {
                                #region 第一行定义列名
                                if (selectDBType == DataBaseType.SqlServer)
                                {
                                    //SQL Server以中括号([])作为列别名
                                    if (string.IsNullOrEmpty(strData))
                                    {
                                        sbOneData.Append("NULL AS [" + dtMain.Columns[j].ColumnName + "],");
                                    }
                                    else
                                    {
                                        sbOneData.Append(StringHelper.ChangeIntoSqlString(strData) + " AS [" + dtMain.Columns[j].ColumnName + "],");
                                    }
                                }
                                else
                                {
                                    //其他类型以双括号("")作为列别名
                                    if (string.IsNullOrEmpty(strData))
                                    {
                                        sbOneData.Append("NULL AS \"" + dtMain.Columns[j].ColumnName + "\",");
                                    }
                                    else
                                    {
                                        sbOneData.Append(StringHelper.ChangeIntoSqlString(strData) + " AS \"" + dtMain.Columns[j].ColumnName + "\",");
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 非第一行不用定义列名
                                if (string.IsNullOrEmpty(strData))
                                {
                                    sbOneData.Append("NULL,");//其他列不定义列名
                                }
                                else
                                {
                                    sbOneData.Append(StringHelper.ChangeIntoSqlString(strData) + ",");//其他列不定义列名
                                }
                                #endregion
                            }
                        }

                        #region 构造生成SQL
                        sbOneData.Remove(sbOneData.Length - 1, 1);
                        if (i == 0)
                        {
                            if (selectDBType == DataBaseType.Oracle)
                            {
                                sbOneData.Append(strOrcEnd);//Oracle需要增加from dual
                            }
                        }
                        else if (i != 0)
                        {
                            if (strSqlType == "2")
                            {
                                sbOneData.Insert(0, strUnion);
                            }
                            else
                            {
                                sbOneData.Insert(0, strUnionAll);
                            }
                            if (selectDBType == DataBaseType.Oracle)
                            {
                                sbOneData.Append(strOrcEnd);
                            }
                        }
                        sbAll.Append(sbOneData.ToString() + "\n");
                        #endregion
                    }
                    sbAll.Insert(0, "SELECT ");

                    #endregion
                }
                rtbResult.Clear();
                rtbResult.AppendText(sbAll.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAll.ToString());
                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                ShowInfo(_strAutoSqlSuccess);
                rtbResult.Select(0, 0); //返回到第一

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelCopy_DataConnect, rtbConString.Text, "【复制拼接字符】字符模板");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelCopy_SqlType, cbbSqlType.SelectedValue.ToString(), "【复制拼接字符】拼接类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelCopy_DbType, cbbDbType.SelectedValue.ToString(), "【复制拼接字符】数据类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelCopy_WordConvert, cbbWordConvert.SelectedValue.ToString(), "【复制拼接字符】字符转换");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ExcelCopy_IsAutoWord, ckbAutoColumnName.Checked?"1":"0", "【复制拼接字符】是否自动列名");
                WinFormContext.UserLoveSettings.Save();
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 类型选择变化事件
        private void cbbSqlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nowHeight  = splitContainer1.SplitterDistance;
            string strSqlType = cbbSqlType.SelectedValue.ToString();
            if (strSqlType == "1")
            {
                grbConSting.Visible = true;
                cbbDbType.Visible = false;
                lblDbType.Visible = false;
                cbbWordConvert.Visible = true;
                lblWordConvert.Visible = true;
                splitContainer1.Panel1Collapsed = false; //设计上方非折叠
            }
            else
            {
                grbConSting.Visible = false;
                cbbDbType.Visible = true;
                lblDbType.Visible = true;
                cbbWordConvert.Visible = false;
                lblWordConvert.Visible = false;
                splitContainer1.Panel1Collapsed = true;  //设计上方折叠
            }
            
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void TsmiInsert_Click(object sender, EventArgs e)
        {
            if (dgvTableList.CurrentCell == null) return;
            int iCurCol = dgvTableList.CurrentCell.ColumnIndex;
            rtbConString.AppendText(string.Format("#{0}#", dgvTableList.Columns[iCurCol].Name));
        }

        private void tsmiClear_Click_1(object sender, EventArgs e)
        {
            dgvTableList.GetBindingTable().Clear();
        }

        #region 模板相关
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
            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            DataRow[] drArr = replaceStringData.MoreXmlConfig.ValData.Select(sKeyId + "='" + sTempType + "'");

            if (drArr.Length > 0)
            {
                rtbConString.Clear();
                rtbConString.AppendText(drArr[0][CopyDataStringTemplate.ValueString.TemplateString].ToString());
            }
        }
        private void btnSaveReplaceTemplate_Click(object sender, EventArgs e)
        {
            string sTempName = txbReplaceTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("模板名称不能为空！");
                return;
            }
            
            string sContent = rtbConString.Text.Trim();
            if (string.IsNullOrEmpty(sContent))
            {
                ShowInfo("请录入模板内容！");
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
                // 键处理
                sKeyIdNew = Guid.NewGuid().ToString();
                DataRow dr = dtKeyConfig.NewRow();
                dr[sKeyId] = sKeyIdNew;
                dr[CopyDataStringTemplate.KeyString.Name] = sTempName;
                dtKeyConfig.Rows.Add(dr);
                // 值处理
                DataRow drNew = dtValConfig.NewRow();
                drNew[sValId] = Guid.NewGuid().ToString();
                drNew[sKeyId] = sKeyIdNew;
                drNew[CopyDataStringTemplate.ValueString.TemplateString] = sContent;
                dtValConfig.Rows.Add(drNew);
            }
            else
            {
                //修改
                // 键处理
                string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
                sKeyIdNew = sKeyIDValue;
                DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                if (drArrKey.Length == 0)
                {
                    DataRow dr = dtKeyConfig.NewRow();
                    dr[sKeyId] = sKeyIdNew;
                    dr[CopyDataStringTemplate.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][CopyDataStringTemplate.KeyString.Name] = sTempName;//修改名称
                }
                // 值处理
                if (drArrVal.Length > 0)
                {
                    drArrVal[0][CopyDataStringTemplate.ValueString.TemplateString] = sContent;
                }
                else
                {
                    DataRow drNew = dtValConfig.NewRow();
                    drNew[sValId] = Guid.NewGuid().ToString();
                    drNew[sKeyId] = sKeyIdNew;
                    drNew[CopyDataStringTemplate.ValueString.TemplateString] = sContent;
                    dtValConfig.Rows.Add(drNew);
                }
            }

            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, CopyDataStringTemplate.KeyString.Name, true, true);
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
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, CopyDataStringTemplate.KeyString.Name, true, true);
            ShowInfo("模板删除成功！");
        }
        #endregion
    }
}
