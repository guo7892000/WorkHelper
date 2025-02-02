using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    public partial class FrmDBTFiltSameRow : BaseForm
    {
        private readonly string _strTableName = "变更表清单";
        public FrmDBTFiltSameRow()
        {
            InitializeComponent();
        }

        private void FrmDBTFiltSameRow_Load(object sender, EventArgs e)
        {
            //合并类型
            _dicString.Add("1", "筛选重复项");
            _dicString.Add("2", "筛选唯一项");
            cbbFiltType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            DataTable dtCopy = new DataTable();
            dtCopy.TableName = _strTableName;
            dgvTableList.BindAutoTable(dtCopy);

            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
        }

        #region 网格粘贴事件
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
                dtMain.Clear();
                dtMain.Columns.Clear();
                pasteText.GetStringTable(false, dtMain);
                dgvTableList.ShowRowNum(); //显示行号
                ShowInfo("粘贴成功！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        #endregion

        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            DataTable dtMain = dgvTableList.GetBindingTable();
            if (dtMain==null || dtMain.Rows.Count == 0)
            {
                ShowInfo("请先粘贴Excel数据！");
                return;
            }

            if (dtMain.Columns.Contains("ROWNO"))
            {
                dtMain.Columns.Remove("ROWNO");
            }
            // 更新原始DataTable
            DataTable dtNew = dtMain.Clone();
            dtNew.Columns.Add("@重复行数");

            IDictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataColumn item in dtMain.Columns)
            {
                dic[item.ColumnName] = item.ColumnName;
            }

            bool isRepeat = (cbbFiltType.SelectedValue == null || "1".Equals(cbbFiltType.SelectedValue.ToString())) ? true : false;
            // 使用LINQ删除重复行
            var query = from data in dtMain.AsEnumerable()
                        group data by dic.GetLinqDynamicTableColumnObj(data, true) into gData  // OK，但使用匿名类，需要根据字典长度创建很多属性，有大量重复代码
                        // group data by dic.GetLinqDynamicTableColumnObjEasy(data, true) into gData // 不行
                        where isRepeat ? gData.Count() > 1 : gData.Count() == 1
                        select new { g = gData, c = gData.Count() };



            var rs = query.ToList();
            foreach (var item in rs)
            {
                Type type = item.g.Key.GetType();
                PropertyInfo[] properties = type.GetProperties();
                DataRow drNew = dtNew.NewRow();
                int i = 0;
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(item.g.Key);
                    drNew[i] = value;
                    i++;
                }

                drNew[i] = item.c;
                dtNew.Rows.Add(drNew);
            }

            dgvResult.BindAutoColumn(dtNew);
            tabControl1.SelectedTab = tpResult;
            ShowInfo("筛选成功！");
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            DataTable dtResult = dgvResult.GetBindingTable();
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                ShowErr("没有要导出的记录！", "提示");
                return;
            }
            //导出Excel
            ExportHelper.ExportExcel(dtResult, "筛选重复行_" + DateTime.Now.ToString("yyyyMMddHHmmss"), true);
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            PasteTextFromClipse();
        }
    }
}
