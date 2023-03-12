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
    /// 复制数据生成拼接字符
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTExcelCol2Row : BaseForm
    {
        #region 变量
        private readonly string _strTableName = "变更表清单";
        private BindingSource bsTable = new BindingSource();
        private string _strAutoSqlSuccess = "生成成功，并已复制到了粘贴板。详细见“生成的SQL”页签！";
        #endregion

        #region 构造函数
        public FrmDBTExcelCol2Row()
        {
            InitializeComponent();
        } 
        #endregion

        #region 加载事件
        private void FrmCopyData_Load(object sender, EventArgs e)
        {
            //读取属性配置
            nudFixRowCount.Value = Setting.Default.ExcelCol2Row_FixRowCount;
            nudEachDataRowCount.Value = Setting.Default.ExcelCol2Row_EachDataRowCount;
            //
            DataTable dtCopy = new DataTable();
            dtCopy.TableName = _strTableName;
            bsTable.DataSource = dtCopy;
            dgvTableList.DataSource = bsTable;
            GlobalValue.Instance.SetPublicDataSource(new DataTable[] { dtCopy });
            //
            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
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

                    PasteString(pasteText);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void PasteString(string pasteText)
        {
            DataTable dtMain = (DataTable)GlobalValue.Instance.dicBindingSource[_strTableName].DataSource;
            dtMain.Clear();
            dtMain.Columns.Clear();
            pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain);
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sbAll = new StringBuilder();
                int iFixRowCount = (int)nudFixRowCount.Value;
                int iEachDataRowCount = (int)nudEachDataRowCount.Value;

                if(iEachDataRowCount<=0)
                {
                    ShowInfo("每组数据行数必须大于0！"); 
                    return;
                }

                string sDouhao = ",";
                DataTable dtMain = (DataTable)GlobalValue.Instance.dicBindingSource[_strTableName].DataSource;
                if (dtMain.Rows.Count == 0)
                {
                    ShowInfo("没有可生成的数据！"); 
                    return;
                }

                #region UNION清单生成
                for (int iCol = 0; iCol < dtMain.Columns.Count; iCol++)//列循环
                {
                    StringBuilder sbHead = new StringBuilder();
                    if (iFixRowCount > 0)
                    {
                        for (int i = 0; i < iFixRowCount; i++)//固定行循环
                        {
                            sbHead.Append(dtMain.Rows[i][iCol].ToString().Trim() + sDouhao);
                        }
                    }
                    StringBuilder sbOne = new StringBuilder();
                    for (int i = 0; i < dtMain.Rows.Count - iFixRowCount; i++)//行循环
                    {
                        string sData = dtMain.Rows[i + iFixRowCount][iCol].ToString().Trim();
                        if (string.IsNullOrEmpty(sData))
                        {
                            break; //当有单元格为空时结束
                        }
                        if ((i+1) % iEachDataRowCount == 0)//下一条是新数据
                        {
                            sbAll.Append(sbHead.ToString() + sbOne.Append(sData).ToString() + "\r\n");
                            sbOne = new StringBuilder();
                        }
                        else
                        {
                            sbOne.Append(sData + sDouhao);
                        }
                    }
                }
                #endregion

                rtbResult.Clear();
                rtbResult.AppendText(sbAll.ToString() + "\n");
                Clipboard.SetData(DataFormats.UnicodeText, sbAll.ToString());
                tabControl1.SelectedTab = tpAutoSQL;
                //生成SQL成功后提示
                lblInfo.Text = _strAutoSqlSuccess;
                rtbResult.Select(0, 0); //返回到第一
                //保存属性配置
                Setting.Default.ExcelCol2Row_FixRowCount = (int)nudFixRowCount.Value;
                Setting.Default.ExcelCol2Row_EachDataRowCount = (int)nudEachDataRowCount.Value;
                Setting.Default.Save();
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

        private void CkbLoadExampleData_CheckedChanged(object sender, EventArgs e)
        {
            if (CkbLoadExampleData.Checked)
            {
                string sText = "列1\t列2\t列3\r\n2021\t2020\t2019\r\n12月\t12月\t12月\r\n张三：86\t张三：76\t张三：66\r\n李四：89\t李四：79\t李四：80\r\n11月\t11月\t11月\r\n张三：81\t张三：70\t张三：66\r\n李四：83\t李四：69\t李四：70\r\n10月\t10月\t10月\r\n张三：88\t张三：76\t张三：86\r\n李四：89\t李四：72\t李四：82";
                PasteString(sText);
                nudFixRowCount.Value = 1;
                nudEachDataRowCount.Value = 3;
            }
            else
            {
                DataTable dtMain = (DataTable)GlobalValue.Instance.dicBindingSource[_strTableName].DataSource;
                dtMain.Clear();
                dtMain.Columns.Clear();
            }
        }
    }
}
