using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Breezee.Core.Interface.KeyValuePairString;

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
            //分组类型
            _dicString.Clear();
            _dicString.Add("1", "列拼接分组(列数无限制)");
            _dicString.Add("2", "列组合分组(最多60列)");
            cbbGroupType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            DataTable dtCopy = new DataTable();
            dtCopy.TableName = _strTableName;
            dgvTableList.BindAutoTable(dtCopy);

            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            toolTip1.SetToolTip(cbbGroupType, "列拼接分组：没有列数限制，实现代码最简单优雅，但当值跟分隔符重叠时结果会出错（当然我们会尽力避免）；\r\n列组合分组：最准。但支持最大列数为60，且后台实现代码大量冗余且不优雅。");
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

            // 是否使用动态
            bool isUseDynamic = cbbGroupType.SelectedValue==null || "1".Equals(cbbGroupType.SelectedValue.ToString());
            if (isUseDynamic)
            {
                // 使用动态：经过测试，可以使用
                // 生成拼接字段字符串作为分组依据
                var separator = string.Empty; // 选择一个不易出现的分隔符
                var query = from data in dtMain.AsEnumerable()
                            group data by dic.GetLinqDynamicTableColumnString(data, true,ref separator) into gData
                            where isRepeat ? gData.Count() > 1 : gData.Count() == 1
                            select new { g = gData, c = gData.Count() };
                var rs = query.ToList();
                foreach (var item in rs)
                {
                    Regex regex = new Regex(separator, RegexOptions.IgnoreCase);
                    MatchCollection mc = regex.Matches(item.g.Key);
                    DataRow drNew = dtNew.NewRow();
                    int i = 0;
                    int iIdx = 0;
                    foreach (Match mat in mc)
                    {
                        drNew[i] = item.g.Key.substring(iIdx, mat.Index);
                        iIdx = mat.Index + mat.Value.Length;
                        i++;
                    }
                    drNew[i] = item.g.Key.substring(iIdx); // 最后一个元素
                    drNew[i+1] = item.c; // 重复数
                    dtNew.Rows.Add(drNew);
                }
            }
            else
            {
                // 不使用动态：使用冗余代码返回的匿名对象，也是测试OK，但不能超过60个。
                // 使用LINQ筛选重复行
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

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            if (dgvTableList.GetBindingTable() != null)
            {
                dgvTableList.GetBindingTable().Columns.Clear();
                dgvTableList.GetBindingTable().Clear();
            }
        }
    }
}
