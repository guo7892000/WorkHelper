using Breezee.Core.WinFormUI;
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
            lblInfo.Text = "实现对分割后的字符多次接拼！典型的例子为表中所有列的字符常量设置（常量名与值都包含列名）。";
            txbSplit.Text = ",";
            txbChildString.Text = "#";
        }

        private void TsbAutoSQL_Click(object sender, EventArgs e)
        {
            var strSplit = txbSplit.Text.Trim();
            var sItem = txbChildString.Text.Trim();

            var sSplitList = rtbSplitList.Text.Trim();
            var sFormat = rtbFormat.Text.Trim();
            
            if (string.IsNullOrEmpty(strSplit))
            {
                ShowInfo("请输入【分隔符]！");
                return;
            }
            if (string.IsNullOrEmpty(sSplitList))
            {
                ShowInfo("请输入【要分割的字符】！");
                return;
            }

            if (string.IsNullOrEmpty(sItem))
            {
                ShowInfo("请输入【每项符号表示】！");
                return;
            }

            if (sItem.Contains(strSplit))
            {
                ShowInfo("【每项符号表示】不能包含【分隔符】！");
                return;
            }

            rtbOutput.Clear();
            string[] arrSplit = sSplitList.Split(new string[] { strSplit  }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var strOne in arrSplit)
            {
                rtbOutput.AppendText(sFormat.Replace(sItem, strOne)+"\n");
            }
            Clipboard.SetText(rtbOutput.Text);
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CkbLoadExampleData_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLoadExampleData.Checked)
            {
                txbChildString.Text = "#";
                txbSplit.Text = ",";
                rtbSplitList.Text = "ID,Code,Name";
                rtbFormat.Text = @"public static string col_# = ""#"";";
                rtbOutput.Text = "";
            }
            else
            {
                txbChildString.Text = "";
                txbSplit.Text = "";
                rtbSplitList.Text = "";
                rtbFormat.Text = "";
                rtbOutput.Text = "";
            }
        }
    }
}
