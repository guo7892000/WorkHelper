using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core;
using System.Text.RegularExpressions;
using org.breezee.MyPeachNet;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// MyBatis参数化（占位符）的SQL转换实际可执行的SQL
    /// </summary>
    public partial class FrmMyBatisSqlParamReplace : BaseForm
    {
        public FrmMyBatisSqlParamReplace()
        {
            InitializeComponent();
        }

        private void FrmMyBatisSqlParamReplace_Load(object sender, EventArgs e)
        {
            // 分隔类型
            _dicString.Add("1", "参数化SQL和参数值同时录入");
            _dicString.Add("2", "参数化SQL和参数值分开输入");
            cbbSqlTextType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            rtbFrom.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.MyBatisSqlConvert_SqlAndParam, "\n").Value; //
            rtbParam.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.MyBatisSqlConvert_Param, ",").Value; //
        }

        private void tsbConvertSQL_Click(object sender, EventArgs e)
        {
            if (cbbSqlTextType.SelectedValue == null) return;
            var splitType = cbbSqlTextType.SelectedValue.ToString();
            string sSqlAndParam = rtbFrom.Text.Trim();
            string sRealSql = string.Empty;
            List<SortSqlParam> listParam = new List<SortSqlParam>();
            if (string.IsNullOrEmpty(sSqlAndParam))
            {
                ShowErr("参数化SQL不能为空！");
                return;
            }

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.MyBatisSqlConvert_SqlAndParam, rtbFrom.Text.Trim(), "【MyBatis参数化（占位符）的SQL转换】SQL和参数");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.MyBatisSqlConvert_Param, rtbParam.Text.Trim(), "【MyBatis参数化（占位符）的SQL转换】参数");
            WinFormContext.UserLoveSettings.Save();

            StringBuilder sbSql = new StringBuilder();

            string sPatterT = @"\s*==>\s*Parameters:\s*";
            Regex regexTimeT = new Regex(sPatterT, RegexOptions.IgnoreCase);
            MatchCollection mcCollTimeT;
            if ("1".Equals(splitType))
            {
                // 同时转换
                /**
                 ==> Preparing: SELECT COUNT(*) AS total FROM t_usc_mdmn_db_base_dlr_info A WHERE A.IS_ENABLE = '1' AND A.DLR_ID = ? OR A.PARENT_DLR_ID = ?

==> Parameters: 212(String), 212(String)
                 */
                // 判断是否包括参数化部分字符
                mcCollTimeT = regexTimeT.Matches(sSqlAndParam);
                if(mcCollTimeT.Count == 0)
                {
                    ShowErr("没有包括【==> Parameters:】的参数，请重新输入！");
                    return;
                }
                if (mcCollTimeT.Count > 1)
                {
                    ShowErr("【==> Parameters:】的参数只能录入一个，请重新输入！");
                    return;
                }
                foreach (Match m in mcCollTimeT)
                {
                    string sMatchValue = m.Value;
                    sRealSql = sSqlAndParam.Substring(0, m.Index);
                    string sParam  = sSqlAndParam.Substring(m.Index + sMatchValue.Length);
                    string[] arrParam = sParam.Split(',');
                    int i = 0;
                    foreach(string strParam in arrParam)
                    {
                        string sValueAndType = strParam.Trim();
                        string sValue = sValueAndType.Substring(0,sValueAndType.IndexOf("("));
                        if (ckbValueRemoveEmpty.Checked)
                        {
                            sValue = sValue.Trim();
                        }
                        string sType = sValueAndType.Substring(sValueAndType.IndexOf("(") + 1).TrimEnd(')');
                        listParam.Add(new SortSqlParam(i, sValue, sType));
                        i++;
                    }
                    break;
                }
                // 先去掉多余的字符，再将参数加入
            }
            else
            {
                // 分别转换
                string sParam = rtbParam.Text.Trim();
                if (string.IsNullOrEmpty(sParam))
                {
                    ShowErr("参数字符不能为空！");
                    return;
                }
                // 先去掉多余的字符，再将参数加入
                mcCollTimeT = regexTimeT.Matches(sParam);
                if (mcCollTimeT.Count == 0)
                {
                    string[] arrParam = sParam.Split(',');
                    if (arrParam.Length == 0)
                    {
                        ShowErr("没有类似【212(String), H2901(String)】的参数字符，请重新输入！");
                        return;
                    }
                    int i = 0;
                    foreach (string strParam in arrParam)
                    {
                        string sValueAndType = strParam.Trim();
                        string sValue = sValueAndType.Substring(0, sValueAndType.IndexOf("("));
                        if (ckbValueRemoveEmpty.Checked)
                        {
                            sValue = sValue.Trim();
                        }
                        string sType = sValueAndType.Substring(sValueAndType.IndexOf("(")+1).TrimEnd(')');
                        listParam.Add(new SortSqlParam(i, sValue, sType));
                        i++;
                    }
                }
                else if (mcCollTimeT.Count == 1)
                {
                    // 只匹配一项
                    foreach (Match m in mcCollTimeT)
                    {
                        string sMatchValue = m.Value.Trim();
                        sParam = sParam.Substring(m.Index + sMatchValue.Length);
                        string[] arrParam = sParam.Split(',');
                        int i = 0;
                        foreach (string strParam in arrParam)
                        {
                            string sValueAndType = strParam.Trim();
                            string sValue = sValueAndType.Substring(0, sValueAndType.IndexOf("("));
                            if (ckbValueRemoveEmpty.Checked)
                            {
                                sValue = sValue.Trim();
                            }
                            string sType = sValueAndType.Substring(sValueAndType.IndexOf("(") + 1).TrimEnd(')');
                            listParam.Add(new SortSqlParam(i, sValue, sType));
                            i++;
                        }
                        break;
                    }
                }
                else
                {
                    ShowErr("【==> Parameters:】的参数只能录入一个，请重新输入！");
                    return;
                }
                // 实际SQL
                sRealSql = rtbFrom.Text.Trim(); 
            }

            // SQL转换
            sPatterT = @"\s*==>\s*Preparing:\s*";
            regexTimeT = new Regex(sPatterT, RegexOptions.IgnoreCase);
            mcCollTimeT = regexTimeT.Matches(sRealSql);
            if (mcCollTimeT.Count == 0)
            {
                // 啥都不用做
            }
            else if (mcCollTimeT.Count == 1)
            {
                // 只匹配一项
                foreach (Match m in mcCollTimeT)
                {
                    string sValue = m.Value.Trim();
                    sRealSql = sRealSql.Substring(m.Index + sValue.Length);
                    break;
                }
            }
            else
            {
                ShowErr("【==> Preparing:】的SQL只能录入一个，请重新输入！");
                return;
            }
            // 查找问号并替换
            sPatterT = @"\?{1}";
            regexTimeT = new Regex(sPatterT, RegexOptions.IgnoreCase);
            mcCollTimeT = regexTimeT.Matches(sRealSql);
            int iStart = 0;
            foreach (Match m in mcCollTimeT)
            {
                sbSql.Append(sRealSql.Substring(iStart, m.Index- iStart));
                if ("String".Equals(listParam[0].DataType))
                {
                    sbSql.Append("'"+listParam[0].DataValue+"'");
                }
                else
                {
                    sbSql.Append(listParam[0].DataValue);
                }
                listParam.RemoveAt(0);
                iStart = m.Index + m.Length;
            }
            if(iStart < sRealSql.Length)
            {
                sbSql.Append(sRealSql.substring(iStart));
            }
            rtbTo.Clear();
            rtbTo.AppendText(sbSql.ToString());
            Clipboard.SetText(sbSql.ToString());
            ShowInfo("转换成功！");
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            tsbConvertSQL.PerformClick();
        }
        private void cbbSqlTextType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSqlTextType.SelectedValue == null) return;
            var splitType = cbbSqlTextType.SelectedValue.ToString();
            if ("1".Equals(splitType))
            {
                splitContainer2.Panel2Collapsed = true;
            }
            else
            {
                splitContainer2.Panel2Collapsed = false;
            }
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class SortSqlParam
        {
            public int SortNo;
            public string DataValue;
            public string DataType;
            public SortSqlParam(int sortNo, string dataValue,string dataType)
            {
                SortNo = sortNo;
                DataValue = dataValue;
                DataType = dataType;                
            }
        }
    }
}
