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
    /// 功能名称：交换字符位置
    /// 使用场景：界面元素赋值和取值
    /// 最后更新日期：2021-08-17
    /// 修改人员：黄国辉
    /// </summary>
    public partial class FrmDBTExchangeStringPlace : BaseForm
    {
        public FrmDBTExchangeStringPlace()
        {
            InitializeComponent();
        }

        private void FrmDBTExchangeStringPlace_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "针对每行数据按分隔符两边交换后，再替换旧字符，并在尾部加上指定字符。典型的例子为控件赋值与取值。";
        }

        private void TsbAutoSQL_Click(object sender, EventArgs e)
        {
            var strSplit = txbSplit.Text.Trim();
            var strInput = rtbInput.Text.Trim();
            var strEndOldString = txbEndOldString.Text.Trim();
            var strEndReplace = txbEndReplace.Text.Trim();
            var strEndAppend = txbEndAppend.Text.Trim();
            if (string.IsNullOrEmpty(strSplit))
            {
                ShowInfo("请输入分隔符！");
                return;
            }
            if (string.IsNullOrEmpty(strInput))
            {
                ShowInfo("请输入要转换的字符！");
                return;
            }

            rtbOutput.Clear();
            var arrSplit = strInput.Split(new char[] { '\n' });
            foreach (var strOne in arrSplit)
            {
                var i = strOne.IndexOf(strSplit);
                var sb = new StringBuilder();
                if (i > -1)
                {
                    sb.Append(strOne.Substring(i + strSplit.Length).Trim() + strSplit + strOne.Substring(0, i).Trim());
                    if(!string.IsNullOrEmpty(strEndOldString))
                    {
                        sb.Replace(strEndOldString, strEndReplace);
                    }
                    sb.Append(strEndAppend + "\n");
                }
                else
                {
                    sb.Append(strOne + "\n");
                }
                rtbOutput.AppendText(sb.ToString());
            }
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CkbLoadExampleData_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbLoadExampleData.Checked)
            {
                rtbInput.Text = @"txbID.Text = dt.Row[i][""id""].ToString();
txbName.Text = dt.Row[i][""Name""].ToString();";
                txbSplit.Text = "=";
                txbEndOldString.Text = ".ToString();";
                txbEndReplace.Text = "";
                txbEndAppend.Text = ".Trim();";
            }
            else
            {
                rtbInput.Text = "";
                txbSplit.Text = "";
                txbEndOldString.Text = "";
                txbEndReplace.Text = "";
                txbEndAppend.Text = "";
            }
        }
    }
}
