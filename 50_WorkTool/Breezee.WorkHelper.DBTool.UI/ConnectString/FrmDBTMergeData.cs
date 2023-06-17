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

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并数据：将复制进来的两个Excel内容进行拼接
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTMergeData : BaseForm
    {
        #region 变量
        private readonly string _strExcel1 = "excel1";
        private readonly string _strExcel2 = "excel2";
        private readonly string _strExcel3 = "excel3";
        private BindingSource bsExcel1 = new BindingSource();
        private BindingSource bsExcel2 = new BindingSource();
        private BindingSource bsExcel3 = new BindingSource();
        private string _strAutoSqlSuccess = "生成成功，详细见“生成结果”页签！";
        string sDealColumnName = Guid.NewGuid().ToString().Replace("-","");
        #endregion

        #region 构造函数
        public FrmDBTMergeData()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        private void FrmCopyData_Load(object sender, EventArgs e)
        {
            IDictionary<string, string> dic_List = new Dictionary<string, string>();
            dic_List.Add("1", "交集");
            dic_List.Add("2", "左集");
            dic_List.Add("3", "右集");
            dic_List.Add("4", "全集");
            cbbSqlType.BindTypeValueDropDownList(dic_List.GetTextValueTable(false), false, true);

            ////数据库类型
            //DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            //cbbDbType.BindTypeValueDropDownList(dtDbType, false, true);

            DataTable dtCopy = new DataTable();
            dtCopy.TableName = _strExcel1;
            bsExcel1.DataSource = dtCopy;
            dgvExcel1.DataSource = bsExcel1;
            WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy });

            DataTable dtCopy2 = dtCopy.Copy();
            dtCopy2.TableName = _strExcel2;
            bsExcel2.DataSource = dtCopy2;
            dgvExcel2.DataSource = bsExcel2;
            WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy2 });

            DataTable dtCopy3 = dtCopy.Copy();
            dtCopy3.TableName = _strExcel3;
            bsExcel3.DataSource = dtCopy3;
            dgvResult.DataSource = bsExcel3;
            WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy3 });
            //
            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            lblInfo2.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            ckbAutoColumnName.Checked = true;
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

                    DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel1].DataSource;
                    dtMain.Clear();
                    dtMain.Columns.Clear();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain);
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
                DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel1].DataSource;
                DataTable dtSec = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel2].DataSource;
                DataTable dtResult = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel3].DataSource;
                if (dtMain.Rows.Count == 0 || dtSec.Rows.Count == 0)
                {
                    ShowInfo("两个Excel都必须要有数据！");
                    return;
                }

                string sType = cbbSqlType.SelectedValue == null ? "" : cbbSqlType.SelectedValue.ToString();

                string sErrorCondition = "条件格式不正确，正确示例为：A=B,B=E！";
                string[] arrCondition = rtbConString.Text.Trim().Split(',');
                if (arrCondition == null || arrCondition.Length == 0)
                {
                    ShowInfo(sErrorCondition);
                    return;
                }

                dtResult.Clear();
                IDictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var sOne in arrCondition)
                {
                    string[] arrColumn = rtbConString.Text.Trim().Split('=');
                    if (arrCondition == null || arrCondition.Length == 0 || arrColumn.Length != 2)
                    {
                        ShowInfo(sErrorCondition);
                        return;
                    }

                    if (!dtMain.Columns.Contains(arrColumn[0]))
                    {
                        ShowInfo("第一个Excel不存在列：“"+arrColumn[0]+ "”！");
                        return;
                    }
                    if (!dtSec.Columns.Contains(arrColumn[1]))
                    {
                        ShowInfo("第二个Excel不存在列：“"+arrColumn[1]+"”！");
                        return;
                    }
                    dic.Add(arrColumn[0], arrColumn[1]);
                }

                foreach (DataColumn item in dtMain.Columns)
                {
                    if(!dtResult.Columns.Contains(item.ColumnName))
                    {
                        dtResult.Columns.Add(item.ColumnName);
                    }
                }
                foreach (DataColumn item in dtSec.Columns)
                {
                    if (!dtResult.Columns.Contains(item.ColumnName))
                    {
                        if (!item.ColumnName.Equals(sDealColumnName))
                        {
                            dtResult.Columns.Add(item.ColumnName);
                        }
                    }
                }
                if (!dtSec.Columns.Contains(sDealColumnName))
                {
                    dtSec.Columns.Add(sDealColumnName, typeof(string));
                    if(dgvExcel2.Columns.Contains(sDealColumnName))
                    {
                        dgvExcel2.Columns[sDealColumnName].Visible = false;
                    }
                }
                #region 拼接字符串
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    string sConndition = "";
                    int iStep = 0;
                    bool isAdd = false;
                    foreach (string sKey in dic.Keys)
                    {
                        string sSource = dtMain.Rows[i][sKey].ToString();
                        if (iStep == 0)
                        {
                            sConndition += dic[sKey] + "= '" + sSource + "'";
                        }
                        else
                        {
                            sConndition += " and " + dic[sKey] + " = '" + sSource + "' ";
                        }

                        iStep++;
                    }

                    DataRow[] arrSec = dtSec.Select(sConndition);
                    foreach (DataRow item in arrSec)
                    {
                        DataRow drNew = dtResult.NewRow();
                        foreach (DataColumn col in dtMain.Columns)
                        {
                            drNew[col.ColumnName] = dtMain.Rows[i][col.ColumnName];
                        }

                        foreach (DataColumn col in dtSec.Columns)
                        {
                            if (col.ColumnName.Equals(sDealColumnName))
                            {
                                continue;
                            }
                            drNew[col.ColumnName] = item[col.ColumnName];
                        }
                        dtResult.Rows.Add(drNew);
                        isAdd = true;
                        item[sDealColumnName] = 1;
                    }
                    //处理第一个Excel独有的数据
                    if (!isAdd && (sType=="2" || sType == "4"))
                    {
                        DataRow drNew = dtResult.NewRow();
                        foreach (DataColumn col in dtMain.Columns)
                        {
                            drNew[col.ColumnName] = dtMain.Rows[i][col.ColumnName];
                        }
                        dtResult.Rows.Add(drNew);
                    }
                }

                //处理第二个Excel独有的数据
                if(sType == "3" || sType == "4")
                {
                    DataRow[] arrSecOther = dtSec.Select("["+sDealColumnName + "] is null");
                    foreach (DataRow item in arrSecOther)
                    {
                        DataRow drNew = dtResult.NewRow();
                        foreach (DataColumn col in dtSec.Columns)
                        {
                            if (col.ColumnName.Equals(sDealColumnName))
                            {
                                continue;
                            }
                            drNew[col.ColumnName] = item[col.ColumnName];
                        }
                        dtResult.Rows.Add(drNew);
                    }
                }
                #endregion

                tabControl1.SelectedTab = tpAutoSQL;
                ShowInfo(_strAutoSqlSuccess);//生成SQL成功后提示
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        private void dgvExcel2_KeyDown(object sender, KeyEventArgs e)
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

                    DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel2].DataSource;
                    dtMain.Clear();
                    dtMain.Columns.Clear();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain,"1",true);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void CkbLoadExampleData_CheckedChanged(object sender, EventArgs e)
        {
            if (CkbLoadExampleData.Checked)
            {
                string sText = "CITY_ID\tPROVINCE_ID\r\n1\t1\r\n10\t6\r\n100\t30\r\n10000\t8";
                DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel1].DataSource;
                dtMain.Clear();
                dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain);

                sText = "CITY_CODE\tCITY_NAME\r\n1\t北京\r\n10\t沈阳市\r\n100\t番禺\r\n11\t茂名";
                dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel2].DataSource;
                dtMain.Clear();
                dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "1");

                ckbAutoColumnName.Checked = true;
                rtbConString.Text = "A=A1";
            }
            else
            {
                DataTable dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel1].DataSource;
                dtMain.Clear();
                dtMain.Columns.Clear();

                dtMain = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel2].DataSource;
                dtMain.Clear();
                dtMain.Columns.Clear();

                rtbConString.Text = "";
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            DataTable dtResult = (DataTable)WinFormGlobalValue.dicBindingSource[_strExcel3].DataSource;
            if (dtResult==null || dtResult.Rows.Count==0)
            {
                ShowErr("没有要导出的记录！", "提示");
                return;
            }
            //导出Excel
            ExportHelper.ExportExcel(dtResult, "合并后数据_" + DateTime.Now.ToString("yyyyMMddHHmmss"),true);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            dgvSelect.GetBindingTable().Clear();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            DataRow dataRow = dgvSelect.GetCurrentRow();
            dt.Rows.Remove(dataRow);
        }
    }
}
