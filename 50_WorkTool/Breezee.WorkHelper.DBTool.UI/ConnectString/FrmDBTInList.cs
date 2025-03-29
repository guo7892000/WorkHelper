using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Interface;
using Breezee.AutoSQLExecutor.Core;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// In清单生成
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTInList : BaseForm
    {
        #region 变量
        //常量
        public IDictionary<string, BindingSource> dicBindingSource = new Dictionary<string, BindingSource>();

        private readonly string _sColIn = "IN字段";
        private readonly string _sColUpper = "ColumnUpper";
        private readonly string _sColLower = "ColumnLower";
        #endregion

        #region 构造函数
        public FrmDBTInList()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmInList_Load(object sender, EventArgs e)
        {
            //初始化下拉框
            IDictionary<string, string> dic_List = new Dictionary<string, string>
            {
                { "1", "IN清单" },
                { "2", "自定义前后缀" },
                { "3", "驼峰式" }
            };
            cbbSqlType.BindTypeValueDropDownList(dic_List.GetTextValueTable(false), false, true);
            cbbSqlType.SelectedValue = "1";
            //初始化网格
            DataTable dtIn = new DataTable();
            dtIn.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn(_sColIn),
                    new DataColumn(_sColUpper),
                    new DataColumn(_sColLower)
                });
            //设置Tag
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                new FlexGridColumn.Builder().Name(_sColIn).Caption("IN字段").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(150).Edit().Visible().Build(),
                new FlexGridColumn.Builder().Name(_sColUpper).Caption("大驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(100).Edit().Visible(false).Build(),
                new FlexGridColumn.Builder().Name(_sColLower).Caption("小驼峰").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit().Visible(false).Build()
            );
            dgvTableList.Tag = fdc.GetGridTagString();
            dgvTableList.BindDataGridView(dtIn, true);
            txbPreString.Text = "'";
            txbEndString.Text = "'";
            txbConcateString.Text = ",";
            grbInputString.Visible = false;
            lblRuleInfo.Text = "请在Excel中复制一列内容，然后点击网格后按ctrl + v粘贴即可。";
        }
        #endregion

        #region 网格按钮事件
        private void dgvTableList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                PasteTextFromClipse();
            }
        }

        private void PasteTextFromClipse()
        {
            try
            {
                string pasteText = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                {
                    return;
                }

                DataTable dtMain = dgvTableList.GetBindingTable();
                if (!ckbIsPasteAppend.Checked && dtMain.Rows.Count > 0)
                {
                    dtMain.Clear();
                }
                foreach (DataRow dr in dtMain.Select("IN字段 is null or IN字段=''"))
                {
                    dtMain.Rows.Remove(dr);
                }

                #region 旧方式(已取消)：当数据太多时很慢(如5万)
                //int iRow = 0;
                //int iColumn = 0;
                //Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                //if (pasteText.IndexOf("in (", StringComparison.CurrentCultureIgnoreCase) > 0)//包括IN的为生成的SQL，不用粘贴
                //{
                //    return;
                //}

                //dtMain.AcceptChanges();
                //int rowindex = dtMain.Rows.Count;
                //int iGoodDataNum = 0;//有效数据号
                //                     //获取获取当前选中单元格所在的行序号
                //for (int j = 0; j < iRow; j++)
                //{
                //    string strData = data[j, 0].ToString().Trim();
                //    if (string.IsNullOrEmpty(strData))
                //    {
                //        continue;
                //    }
                //    if (dtMain.Select("IN字段='" + data[j, 0] + "'").Length == 0)
                //    {
                //        //dtMain.Rows.Add(dtMain.NewRow());
                //        //dtMain.Rows[rowindex + iGoodDataNum][0] = strData;
                //        dtMain.Rows.Add(strData);
                //        iGoodDataNum++;
                //    }
                //} 
                #endregion

                pasteText.GetFirstColumnTable(dtMain, true, false, false);
                dgvTableList.ShowRowNum(true); //显示行号
                tsbAutoSQL.Enabled = true;
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
            //取得数据源
            DataTable dtMain = dgvTableList.GetBindingTable();
            string strSqlType = cbbSqlType.SelectedValue.ToString();
            string strPreStr = txbPreString.Text;
            string strEndStr = txbEndString.Text;
            string strConnStr = txbConcateString.Text;
            string strReturnStr = cbkIsNewLine.Checked ? "\n" : "";
            //得到变更后数据
            dtMain.AcceptChanges();
            if (dtMain.Rows.Count == 0)
            {
                ShowInfo("没有可生成的数据！");
                return;
            }
            if (strSqlType == "3")
            {
                // 3-驼峰式
                foreach (DataRow dr in dtMain.Rows)
                {
                    dr[_sColLower] = dr[_sColIn].ToString().FirstLetterUpper(false);
                    dr[_sColUpper] = dr[_sColIn].ToString().FirstLetterUpper();
                }
                dgvTableList.Columns[_sColLower].Visible = true;
                dgvTableList.Columns[_sColUpper].Visible = true;
                dgvTableList.ShowRowNum(true); //显示行号
                tabControl1.SelectedTab = tpImport;
                return;
            }
     
            StringBuilder sbAllSql = new StringBuilder();
            string sbAllSqlEnd = "";
            if (strSqlType == "1")
            {
                // 1-IN清单
                sbAllSql.Append(" in (");
                sbAllSqlEnd = ") ";
                strPreStr = "'";
                strEndStr = "'";
                strConnStr = ",";
                strReturnStr = "";
            }
            foreach (DataRow drTable in dtMain.Rows)//针对表清单循环
            {
                string strData = drTable[0].ToString().Trim();
                if (string.IsNullOrEmpty(strData)) continue;
                sbAllSql.Append(strPreStr).Append(strData).Append(strEndStr).Append(strConnStr).Append(strReturnStr);
            }
            dgvTableList.ShowRowNum(true); //显示行号
            sbAllSql.Remove(sbAllSql.Length - 1, 1);
            sbAllSql.Append(sbAllSqlEnd);
            rtbResult.Clear();
            rtbResult.AppendText(sbAllSql.ToString() + "\n");
            Clipboard.SetData(DataFormats.UnicodeText, sbAllSql.ToString());
            tabControl1.SelectedTab = tpAutoSQL;
            //生成SQL成功后提示
            //ShowInfo(strInfo);
            lblRuleInfo.Text = StaticValue.GenResultCopySuccessMsg;
            rtbResult.Select(0, 0); //返回到第一
        }
        #endregion

        #region 语句类型选择变化事件
        private void cbbSqlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1-IN清单,2-自定义前后缀,3-驼峰式
            string strSqlType = cbbSqlType.SelectedValue.ToString();
            if (strSqlType == "1")
            {
                dgvTableList.Columns[_sColLower].Visible = false;
                dgvTableList.Columns[_sColUpper].Visible = false;
                grbInputString.Visible = false;
            }
            else if (strSqlType == "2")
            {
                grbInputString.Visible = true;
            }
            else
            {
                grbInputString.Visible= false;
                dgvTableList.Columns[_sColLower].Visible = true;
                dgvTableList.Columns[_sColUpper].Visible = true;
            }
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion

        #region 帮助按钮事件
        private void tsbHelp_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            dgvTableList.GetBindingTable().Clear();
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            PasteTextFromClipse();
        }
    }
}
