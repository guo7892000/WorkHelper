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
using System.Text;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    /// <summary>
    /// 功能名称：分割拼接字符
    /// 使用场景：定义表中所有列的字符常量设置（常量名与值都包含列名）
    /// 最后更新日期：2021-08-20
    /// 修改人员：黄国辉
    /// </summary>
    public partial class FrmDBTSplitString : BaseForm
    {
        public FrmDBTSplitString()
        {
            InitializeComponent();
        }

        private void FrmDBTExchangeStringPlace_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "实现对分割后的字符重新接拼！";
            txbSplitList.Text = ",";
            txbSplitListSplitChar.Text = "-";
            _dicString.Add("1", "单行数据示例");
            _dicString.Add("2", "多行数据示例");
            cbbExample.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);
            toolTip1.SetToolTip(ckbOneRowOneColumn, "选中时，如数据只有一行时，分隔后所有值都在A列中");
            toolTip1.SetToolTip(ckbIgnoreEmptyData, "选中时，分隔的空字符将被去掉，即列值会前移");
        }
        private void btnSplit_Click(object sender, EventArgs e)
        {
            var sSplitList = txbSplitList.Text.Trim(); //分隔符列表
            var sSpliListSplitChar = txbSplitListSplitChar.Text.Trim(); //分隔符列表的分隔符
            var sWillSplitList = rtbSplitList.Text.Trim(); //要分隔的字符

            if (string.IsNullOrEmpty(sSplitList))
            {
                ShowInfo("请输入【分隔符列表】！");
                txbSplitList.Focus();
                return;
            }
            if (string.IsNullOrEmpty(sSpliListSplitChar))
            {
                ShowInfo("请输入【分隔符列表的分隔符】！");
                txbSplitListSplitChar.Focus();
                return;
            }
            else
            {
                if (sSpliListSplitChar.Length > 1)
                {
                    ShowInfo("【分隔符列表的分隔符】只能是一个字符！");
                    txbSplitListSplitChar.Focus();
                    return;
                }
            }
            if (string.IsNullOrEmpty(sWillSplitList))
            {
                ShowInfo("请输入【要分割的字符】！");
                rtbSplitList.Focus();
                return;
            }
            //分割的行数数组
            string[] dataRows = sWillSplitList.Trim().Split(new string[] { System.Environment.NewLine,"\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] sSplitCharArr = sSplitList.Split(new string[] { sSpliListSplitChar }, StringSplitOptions.RemoveEmptyEntries);
            SplitConvert(dataRows, sSplitCharArr, ref sWillSplitList);
        }

        private void SplitConvert(string[] dataArr, string[] sSplitCharArr, ref string sWillSplitList)
        {
            StringSplitOptions splitOptions = ckbIgnoreEmptyData.Checked ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            DataTable dtData = dgvData.GetBindingTable();
            dtData = new DataTable();
            // 循环处理每行数据
            foreach (var sOne in dataArr)
            {
                // 分隔当前行数据
                string[] splitList = sOne.Split(sSplitCharArr, splitOptions);
                // 只有一行数据，且所有分隔得到的字符合为一列数据中
                if (dataArr.Length == 1 && ckbOneRowOneColumn.Checked)
                {
                    dtData.Columns.Add("A");
                    foreach (var item in splitList)
                    {
                        dtData.Rows.Add(item);
                    }
                    dgvData.BindAutoColumn(dtData);
                    return;
                }

                // 多行或单行但所有分隔得到的字符不合为一列数据中
                // 先处理表的列
                for (int i = 0; i < splitList.Length; i++)
                {
                    string sColName = i.ToUpperWord();
                    if (!dtData.Columns.Contains(sColName))
                    {
                        dtData.Columns.Add(sColName);
                    }
                }
                DataRow dr = dtData.NewRow();
                // 再处理数据
                for (int i = 0; i < splitList.Length; i++)
                {
                    dr[i] = splitList[i];
                }
                dtData.Rows.Add(dr);
            }
            dgvData.BindAutoColumn(dtData);
        }

        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            var sFormat = rtbFormat.Text.Trim();
            if (string.IsNullOrEmpty(sFormat))
            {
                ShowInfo("请输入【要拼接的字符格式】！");
                return;
            }

            rtbOutput.Clear();
            DataTable dtData = dgvData.GetBindingTable();
            if(dtData==null || dtData.Rows.Count == 0)
            {
                ShowInfo("请先分拆字符！");
                return;
            }
            foreach (DataRow dr in dtData.Rows)
            {
                string sOneData = sFormat;
                foreach (DataColumn dc in dtData.Columns)
                {
                    string sColName = dc.ColumnName;
                    sOneData = sOneData.Replace("#" + sColName + "#", dr[sColName].ToString());
                }
                rtbOutput.AppendText(sOneData + System.Environment.NewLine);
            }
            Clipboard.SetText(rtbOutput.Text);
            ShowInfo("转换成功！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbbExample_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sExampleType = cbbExample.SelectedValue.ToString();
            if(string.IsNullOrEmpty(sExampleType))
            {
                return;
            }
            if("1".Equals(sExampleType))
            {
                //单行数据示例
                txbSplitListSplitChar.Text = "-";
                txbSplitList.Text = ",";
                rtbSplitList.Text = "ID,Code,Name";
                rtbFormat.Text = @"public static string col_#A# = ""#A#"";";
                rtbOutput.Text = "";
            }
            else
            {
                //多行数据示例
                txbSplitListSplitChar.Text = ",";
                txbSplitList.Text = "#,&,**";
                rtbSplitList.Text = @"ID#Code**Name
11&22**33
66**77&88";
                rtbFormat.Text = @"#A#-#B#-#C#";
                rtbOutput.Text = "";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txbSplitListSplitChar.Text = "";
            txbSplitList.Text = "";
            rtbSplitList.Text = "";
            rtbFormat.Text = "";
            rtbOutput.Text = "";
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
    }
}
