using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using LibGit2Sharp;
using org.breezee.MyPeachNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

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
        private string _FixRemarkColumnName = "InputText";
        SplitStringTemplate replaceStringData;
        public FrmDBTSplitString()
        {
            InitializeComponent();
        }

        private void FrmDBTExchangeStringPlace_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "实现对分隔后的字符重新接拼！";
            txbSplitList.Text = ",";
            txbSplitListSplitChar.Text = "-";
            // 绑定分隔符列表网格
            DataTable dtData = new DataTable();
            dtData.Columns.Add("A");
            dgvSplitChar.BindAutoColumn(dtData);
            dgvSplitChar.AllowUserToAddRows = true;
            dgvSplitChar.AllowUserToOrderColumns = false;
            // 分隔类型
            _dicString.Add("1", "分隔符分隔");
            _dicString.Add("2", "固定长度分隔");
            cbbSplitType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            // 分隔模式
            _dicString.Clear();
            _dicString.Add("1", "同时分隔");
            _dicString.Add("2", "递归分隔");
            cbbSplitModule.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            _dicString.Clear();
            _dicString.Add("1", "多行数据示例");
            _dicString.Add("2", "单行汇总为单列示例");
            _dicString.Add("3", "单行2次分隔示例");
            _dicString.Add("4", "单行3次分隔示例");
            _dicString.Add("10", "固定长度分隔示例");
            cbbExample.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);

            _dicString.Clear();
            _dicString.Add("1", "默认换行符");
            _dicString.Add("2", "指定换行符");
            _dicString.Add("3", "指定和默认换行符");
            cbbNewLineType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            
            //读取喜好配置
            cbbSplitType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_SplitType, "1").Value;
            cbbSplitModule.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_SplitModel, "1").Value;
            ckbIgnoreEmptyData.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_IsIgnoreEmptyData, "1").Value) ? true : false; //是否忽略分隔后的空数据
            ckbEveryDataTrim.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_IsTrimData, "1").Value) ? true : false;  //是否每项剔除前后空白字符
            // ckbEveryLineEndChar.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_IsFixNewLine, "0").Value) ? true : false;  //是否指定换行符
            txbEveryLineEndChar.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_NewLineString, "\n").Value; //换行符
            txbSplitList.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_SplitList, ",").Value; //分隔符列表
            txbSplitListSplitChar.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_SplitListSplitByChar, "-").Value; //分隔符列表的分隔符
            rtbSplitList.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SplitConnString_LastInputSplitString, "").Value; //最后输入的要分隔的字符串

            toolTip1.SetToolTip(ckbIgnoreEmptyData, "选中时，分隔的空字符将被去掉，即列值会前移");
            toolTip1.SetToolTip(txbSplitListSplitChar, "分隔符清单中的分隔符，如分隔符清单为【,-;-|】，横杆为其分隔符，\r\n那么分隔符清单就为：逗号、分号、竖线。");
            toolTip1.SetToolTip(txbSplitList, "分隔符清单，即有哪些分隔符，使用固定字符分隔表示。注：支持空格作为分隔符！");
            toolTip1.SetToolTip(ckbOneRowToOneColumn, "选中时，如数据只有一行时，分隔后所有值都在A列中");
            toolTip1.SetToolTip(cbbSplitModule, "同时分隔：一次性将多个分隔符同时分隔；\r\n递归分隔：先按第一个分隔符分隔，得到的结果再按第二个字符分组，依次类推。最多只能3次，并且只取分隔后的前两组数据。");
            toolTip1.SetToolTip(cbbNewLineType, "其中【指定和默认换行符】包括默认的换行符和指定的换行符");

            cbbNewLineType.SelectedValue = "1";

            //加载模板数据
            replaceStringData = new SplitStringTemplate(DBTGlobalValue.SplitTextTemplateFileString.Xml_FileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, SplitStringTemplate.KeyString.Name, true, true);
        }

        /// <summary>
        /// 分拆字符按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void btnSplit_Click(object sender, EventArgs e)
        {
            #region 转换前判断
            if (cbbSplitType.SelectedValue == null) return;
            var splitType = cbbSplitType.SelectedValue.ToString();
            var splitModuleType = cbbSplitModule.SelectedValue == null ? "" : cbbSplitModule.SelectedValue.ToString();
            var newLineType = cbbNewLineType.SelectedValue == null ? "" : cbbNewLineType.SelectedValue.ToString();
            IList<string> listNewLine = new List<string>();
            // 分隔字符列表
            DataTable dtSplitChar = dgvSplitChar.GetBindingTable();
            foreach (DataRow dr in dtSplitChar.Select("A is null "))
            {
                dtSplitChar.Rows.Remove(dr); // 移除空行
            }
            if ((dtSplitChar == null || dtSplitChar.Rows.Count == 0) && !ckbFirstSplitBySpace.Checked)
            {
                ShowInfo("请【获取分隔符】，或输入得到【分隔符列表】！");
                rtbSplitList.Focus();
                return;
            }

            var sWillSplitList = rtbSplitList.Text.Trim(); //要分隔的字符
            if (string.IsNullOrEmpty(sWillSplitList))
            {
                ShowInfo("请输入【要分割的字符】！");
                rtbSplitList.Focus();
                return;
            }

            if ("1".Equals(newLineType) || "3".Equals(newLineType))
            {
                listNewLine.Add(Environment.NewLine);
                listNewLine.Add("\n");
            }
            if ("2".Equals(newLineType) || "3".Equals(newLineType))
            {
                listNewLine.Add(txbEveryLineEndChar.Text.Trim());
            }

            // 分隔的行数数组
            string[] dataArr = sWillSplitList.Trim().Split(listNewLine.ToArray(), StringSplitOptions.None);
            string[] sSplitCharArr;
            if (dataArr.Length == 0)
            {
                ShowInfo("请输入【要分割的字符】！");
                rtbSplitList.Focus();
                return;
            }
            #endregion

            DataTable dtData = new DataTable();
            #region 按固定长度截取
            if ("2".Equals(splitType))
            {
                // 分隔符列表
                DataTable dSplitChars = dgvSplitChar.GetBindingTable();
                var splitListFixErr = from f in dSplitChars.AsEnumerable()
                                      where int.TryParse(f.Field<string>("A"), out int iRigth) == false
                                      select f.Field<string>("A");
                sSplitCharArr = splitListFixErr.ToArray();
                foreach (var item in sSplitCharArr)
                {
                    ShowInfo("【固定长度清单】网格中存在非整数的值，请修正！");
                    return;
                }

                FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
                var splitListFix = from f in dSplitChars.AsEnumerable()
                                   select f.Field<string>("A");
                sSplitCharArr = splitListFix.ToArray();

                // 先处理表的列:列数跟分隔符列表个数一致
                for (int i = 0; i < sSplitCharArr.Length; i++)
                {
                    string sColName = i.ToUpperWord();
                    if (!dtData.Columns.Contains(sColName))
                    {
                        dtData.Columns.Add(sColName);
                        fdc.AddColumn(new FlexGridColumn.Builder().Name(sColName).Caption(sColName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(100).Visible().Build());
                    }
                }
                if (!dtData.Columns.Contains(_FixRemarkColumnName))
                {
                    dtData.Columns.Add(_FixRemarkColumnName); // 增加一个记录各行数据的长度是否满足
                    fdc.AddColumn(new FlexGridColumn.Builder().Name(_FixRemarkColumnName).Caption(_FixRemarkColumnName).Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(100).Visible().Build());
                }

                int[] iSplitArr = new int[sSplitCharArr.Length];
                int iCurrent;
                for (int i = 0; i < sSplitCharArr.Length; i++)
                {
                    if (int.TryParse(sSplitCharArr[i], out iCurrent))
                    {
                        iSplitArr[i] = iCurrent;
                    }
                    else
                    {
                        throw new Exception(sSplitCharArr[i] + "不是整数值，请修正！");
                    }
                }

                for (int i = 0; i < dataArr.Length; i++)
                {
                    // 每一行数据
                    DataRow dr = dtData.NewRow();
                    int iStart = 0;
                    string sCurRow = dataArr[i].ToString();
                    for (int j = 0; j < iSplitArr.Length; j++)
                    {
                        if (sCurRow.Length == (iStart + iSplitArr[j]))
                        {
                            dr[j] = sCurRow.Substring(iStart, iSplitArr[j]);
                            dr[_FixRemarkColumnName] = (j == iSplitArr.Length - 1) ? "" : "Short";
                            break;
                        }
                        else if (sCurRow.Length > (iStart + iSplitArr[j]))
                        {
                            dr[j] = sCurRow.Substring(iStart, iSplitArr[j]);
                            if (j == iSplitArr.Length - 1)
                            {
                                dr[_FixRemarkColumnName] = "Long";
                            }
                        }
                        else
                        {
                            dr[j] = sCurRow.Substring(iStart);
                            dr[_FixRemarkColumnName] = "Short";
                            break;
                        }
                        iStart = iStart + iSplitArr[j];
                    }
                    dtData.Rows.Add(dr);
                }
                SaveLoveCfg(dtData);
                //dgvData.Tag = fdc.GetGridTagString();
                //dgvData.BindDataGridView(dtData,false);
                dgvData.BindAutoTable(dtData);
                ShowInfo("转换成功！");
                return;
            }
            #endregion

            //按分隔符来分隔
            //得到分隔符列表
            var splitListAll = from f in dgvSplitChar.GetBindingTable().AsEnumerable()
                               select f.Field<string>("A");
            sSplitCharArr = splitListAll.ToArray();

            #region 单行合并为单列
            // 单行合并为单列：只转换第一行
            if (ckbOneRowToOneColumn.Checked)
            {
                if (ckbFirstSplitBySpace.Checked)
                {
                    sSplitCharArr[sSplitCharArr.Length] = " "; //增加一个空格作为分隔符
                }
                // 循环处理每行数据
                foreach (var sOne in dataArr)
                {
                    string[] splitList = sOne.Split(sSplitCharArr, StringSplitOptions.RemoveEmptyEntries);
                    // 只转换第一行数据
                    dtData.Columns.Add("A");
                    foreach (var item in splitList)
                    {
                        dtData.Rows.Add(ckbEveryDataTrim.Checked ? item.Trim() : item);//去掉前后空白字符
                    }
                    break;
                }
                SaveLoveCfg(dtData);
                dgvData.BindAutoColumn(dtData);
                return;
            } 
            #endregion

            // 同时分隔
            if ("1".Equals(splitModuleType))
            {
                if (ckbFirstSplitBySpace.Checked)
                {
                    Array.Resize(ref sSplitCharArr, sSplitCharArr.Length + 1); // 数组大小增加1
                    sSplitCharArr[sSplitCharArr.Length - 1] = " "; //增加一个空格作为分隔符
                }
                StringSplitOptions splitOptions = ckbIgnoreEmptyData.Checked ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
                // 循环处理每行数据
                foreach (var sOne in dataArr)
                {
                    string[] splitList = sOne.Split(sSplitCharArr, splitOptions);
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
                    if(ckbIgnoreEmptyData.Checked)
                    {
                        // 移除空数据
                        int iHadData = 0;
                        for (int i = 0; i < splitList.Length; i++)
                        {
                            string split = splitList[i].Trim();
                            if (!string.IsNullOrEmpty(split))
                            {
                                dr[iHadData] = split;
                                iHadData++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        // 不移除空数据
                        for (int i = 0; i < splitList.Length; i++)
                        {
                            string split = splitList[i];
                            dr[i] = ckbEveryDataTrim.Checked ? split.Trim() : split;
                        }
                    }
                    dtData.Rows.Add(dr);
                }
                SaveLoveCfg(dtData);
                dgvData.BindAutoColumn(dtData);
                return;
            }

            // 逐步分隔
            if ("2".Equals(splitModuleType))
            {
                if (ckbFirstSplitBySpace.Checked)
                {
                    Array.Resize(ref sSplitCharArr, sSplitCharArr.Length + 1); // 数组大小增加1
                    sSplitCharArr[sSplitCharArr.Length - 1] = " "; //增加一个空格作为分隔符
                }
                // 多次分隔的限制判断
                if (sSplitCharArr.Length < 2 || sSplitCharArr.Length > 3)
                {
                    ShowInfo("【逐步分隔】时，分隔符个数范围：2-3个。注：当选中【首次按空白分隔】时，也算一个分隔符！");
                    txbSplitList.Focus();
                    return;
                }

                // 生成表的处理
                int iCoumnCount = int.Parse(Math.Pow(2.0, sSplitCharArr.Count()-1).ToString()); 
                for (int i = 0; i < iCoumnCount; i++)
                {
                    string sColName = i.ToUpperWord();
                    if (!dtData.Columns.Contains(sColName))
                    {
                        dtData.Columns.Add(sColName);
                    }
                }

                StringSplitOptions splitOptions = ckbIgnoreEmptyData.Checked ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
                // 第一次分隔结果
                string[] dataOne = sWillSplitList.Trim().Split(new string[] { sSplitCharArr[0] }, StringSplitOptions.RemoveEmptyEntries);
                // 循环处理每行数据
                int iRowIdx = 0; //行号
                for (int i = 0; i < dataOne.Length; i++)
                {
                    // 第一行内容
                    dtData.Rows.Add(dtData.NewRow());
                    // 第二次分隔结果
                    string[] dataTwo = dataOne[i].Split(new string[] { sSplitCharArr[1] }, splitOptions);

                    if (sSplitCharArr.Length <= 2)
                    {
                        //只有两次分隔
                        if (dataTwo.Length > 0)
                        {
                            dtData.Rows[iRowIdx][0.ToUpperWord()] = ckbEveryDataTrim.Checked ? dataTwo[0].Trim() : dataTwo[0];
                        }
                        if (dataTwo.Length > 1)
                        {
                            dtData.Rows[iRowIdx][1.ToUpperWord()] = ckbEveryDataTrim.Checked ? dataTwo[1].Trim() : dataTwo[1];
                        }
                    }
                    else
                    {
                        // 有三次分隔
                        int iColIdx = 0;
                        foreach (var sTwo in dataTwo)
                        {
                            // 第三次分隔结果
                            string[] dataThree = sTwo.Split(new string[] { sSplitCharArr[2] }, splitOptions);
                            if (dataThree.Length > 0)
                            {
                                dtData.Rows[iRowIdx][(iColIdx*2+0).ToUpperWord()] = ckbEveryDataTrim.Checked ? dataThree[0].Trim() : dataThree[0];
                            }
                            if (dataThree.Length > 1)
                            {
                                dtData.Rows[iRowIdx][(iColIdx * 2 + 1).ToUpperWord()] = ckbEveryDataTrim.Checked ? dataThree[1].Trim() : dataThree[1];
                            }
                            iColIdx++;
                        }
                    }
                    iRowIdx++;
                }
                SaveLoveCfg(dtData);
                dgvData.BindAutoColumn(dtData);
                ShowInfo("转换成功！");
                return;
            }
        }

        /// <summary>
        /// 生成按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                rtbOutput.AppendText(sOneData + (ckbNewLine.Checked? System.Environment.NewLine:"")); //是否换行
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
            cbbNewLineType.SelectedValue = "1";  //默认都是使用默认的分隔符
            ckbOneRowToOneColumn.Checked = false;
            if ("1".Equals(sExampleType))
            {
                //多行数据分隔示例
                cbbSplitType.SelectedValue = "1";
                cbbSplitModule.SelectedValue = "1"; //1-同时分隔
                txbSplitListSplitChar.Text = ",";
                txbSplitList.Text = "#,&,**";
                rtbSplitList.Text = @"ID#Code**Name
11&22**33
66**77&88";
                rtbFormat.Text = @"#A#-#B#-#C#";
                rtbOutput.Text = "";
            }
            else if ("2".Equals(sExampleType))
            {
                //单行汇总为单列数据分隔示例
                cbbSplitType.SelectedValue = "1";
                cbbSplitModule.SelectedValue = "1"; //1-同时分隔
                txbSplitListSplitChar.Text = "-";
                txbSplitList.Text = ",";
                rtbSplitList.Text = "ID,Code,Name";
                rtbFormat.Text = @"public static string col_#A# = ""#A#"";";
                rtbOutput.Text = "";
                ckbOneRowToOneColumn.Checked = true;
            }
            else if ("3".Equals(sExampleType))
            {
                //单行2次分隔示例
                cbbSplitType.SelectedValue = "1";
                cbbSplitModule.SelectedValue = "2"; //2-逐步分隔
                txbSplitListSplitChar.Text = "-";
                txbSplitList.Text = ",-:";
                rtbSplitList.Text = "1:未出库,2:已出库,3:已结算";
                rtbFormat.Text = @"#A#,#B#";
                rtbOutput.Text = "";
                
            }
            else if ("4".Equals(sExampleType))
            {
                //单行3次分隔示例
                cbbSplitType.SelectedValue = "1";
                cbbSplitModule.SelectedValue = "2"; //2-逐步分隔
                txbSplitListSplitChar.Text = "-";
                txbSplitList.Text = ",-:-&";
                rtbSplitList.Text = "1&2:未出库&未入库,3&4:已退货&未退货,5&6:已结算&未结算";
                rtbFormat.Text = @"#A#,#B#";
                rtbOutput.Text = "";
                
            }
            else if ("10".Equals(sExampleType))
            {
                // 固定长度分隔示例
                cbbSplitType.SelectedValue = "2";
                cbbSplitModule.SelectedValue = "2"; //2-同时分隔
                txbSplitListSplitChar.Text = "-";
                txbSplitList.Text = "3-6-18";
                rtbSplitList.Text = @"1  出库类型  OSD290320240301001
2  出库类型  OSD290320240301002
3  入库类型  ISD290320240301001
";
                rtbFormat.Text = @"#A#,#B#";
                rtbOutput.Text = "";
            }
            else
            {
                cbbSplitType.SelectedValue = "1";
                cbbSplitModule.SelectedValue = "1"; //1-同时分隔
            }
            btnGetSplitChar.PerformClick();
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

        private void cbbSplitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSplitType.SelectedValue == null) return;
            if ("1".Equals(cbbSplitType.SelectedValue.ToString()))
            {
                // 分隔符分隔
                grbSplitCharCfg.Visible = true;
                ckbOneRowToOneColumn.Visible = true;
                grbSplitList.Text = "分隔符列表";
                lblSplitCharList.Text = "分隔符列表";
                // 分隔模式
                cbbSplitModule.SetControlReadOnly(false);
                dgvSplitChar.Columns["A"].SortMode = DataGridViewColumnSortMode.Automatic; //不允许排序
                dgvSplitChar.GetBindingTable().Clear();
            }
            else
            {
                // 固定长度分割
                grbSplitCharCfg.Visible = false;
                ckbOneRowToOneColumn.Visible = false;
                grbSplitList.Text = "固定长度清单";
                lblSplitCharList.Text = "固定长度列表";
                // 分隔模式
                cbbSplitModule.SetControlReadOnly();
                cbbSplitModule.SelectedValue = "1";
                dgvSplitChar.Columns["A"].SortMode = DataGridViewColumnSortMode.NotSortable; //允许排序
                dgvSplitChar.GetBindingTable().Clear();
            }
        }

        /// <summary>
        /// 获取分隔符事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetSplitChar_Click(object sender, EventArgs e)
        {
            var sSplitList = txbSplitList.Text; //分隔符列表：这里不去掉空格
            var sSpliListSplitChar = txbSplitListSplitChar.Text.Trim(); //分隔符列表的分隔符

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
            //得到分隔符列表
            string[] sSplitCharArr = sSplitList.Split(new string[] { sSpliListSplitChar }, StringSplitOptions.None); //这里不去掉空格
            DataTable dtSplitChar = dgvSplitChar.GetBindingTable();
            if (dtSplitChar == null || dtSplitChar.Rows.Count == 0)
            {
                // 什么都不用做
            }
            else
            {
                dtSplitChar.Clear();
            }
            //把数据加入到网格中
            foreach (var item in sSplitCharArr)
            {
                dtSplitChar.Rows.Add(item);
            }
        }

        private void tsmiClean_Click(object sender, EventArgs e)
        {
            dgvSplitChar.GetBindingTable().Clear();
        }

        private void dgvSplitChar_KeyDown(object sender, KeyEventArgs e)
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
                if (cbbSplitType.SelectedValue == null) return;
                var splitType = cbbSplitType.SelectedValue.ToString();


                string pasteText = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                {
                    return;
                }
                DataTable dtMain = dgvSplitChar.GetBindingTable();

                int iRow = 0;
                int iColumn = 0;
                Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                #region 生成IN清单
                if (pasteText.IndexOf("in (", StringComparison.CurrentCultureIgnoreCase) > 0)//包括IN的为生成的SQL，不用粘贴
                {
                    return;
                }
                if (!ckbIsPasteAppend.Checked && dtMain.Rows.Count > 0)
                {
                    dtMain.Clear();
                }
                foreach (DataRow dr in dtMain.Select("A is null or A=''"))
                {
                    dtMain.Rows.Remove(dr);
                }
                dtMain.AcceptChanges();
                int rowindex = dtMain.Rows.Count;
                int iGoodDataNum = 0;//有效数据号
                                     //获取获取当前选中单元格所在的行序号
                for (int j = 0; j < iRow; j++)
                {
                    string strData = data[j, 0].ToString().Trim();
                    if (string.IsNullOrEmpty(strData))
                    {
                        continue;
                    }

                    if ("1".Equals(splitType))
                    {
                        // 分隔符分隔
                        if (dtMain.Select("A='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.NewRow());
                            dtMain.Rows[rowindex + iGoodDataNum][0] = strData;
                            iGoodDataNum++;
                        }
                    }
                    else
                    {
                        // 固定长度分隔
                        dtMain.Rows.Add(dtMain.NewRow());
                        dtMain.Rows[rowindex + iGoodDataNum][0] = strData;
                        iGoodDataNum++;
                    }

                }
                dgvSplitChar.ShowRowNum(true); //显示行号
                tsbAutoSQL.Enabled = true;

                #endregion

            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void SaveLoveCfg(DataTable dtData)
        {
            if(ckbIgnoreEmptyData.Checked) 
            {
                dtData.RemoveEmptyRows();
                
                dtData.RemoveEmptyColumns();
            }
            // 只有不是选择示例，才保存喜好设置
            if(cbbExample.SelectedValue==null || string.IsNullOrEmpty(cbbExample.SelectedValue.ToString()))
            {
                //保存喜好配置
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_SplitType, cbbSplitType.SelectedValue.ToString(), "【分隔拼接字符】分隔类型");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_SplitModel, cbbSplitModule.SelectedValue.ToString(), "【分隔拼接字符】分隔模式");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_IsIgnoreEmptyData, ckbIgnoreEmptyData.Checked ? "1" : "0", "【分隔拼接字符】是否忽略分隔后的空数据");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_IsTrimData, ckbEveryDataTrim.Checked ? "1" : "0", "【分隔拼接字符】是否每项剔除前后空白字符");
                //WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_IsFixNewLine, ckbEveryLineEndChar.Checked ? "1" : "0", "【分隔拼接字符】是否指定换行符");

                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_NewLineString, txbEveryLineEndChar.Text.Trim(), "【分隔拼接字符】换行符");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_SplitList, txbSplitList.Text.Trim(), "【分隔拼接字符】分隔符列表");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_SplitListSplitByChar, txbSplitListSplitChar.Text.Trim(), "【分隔拼接字符】分隔符列表的分隔符");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SplitConnString_LastInputSplitString, rtbSplitList.Text.Trim(), "【分隔拼接字符】最后输入的要分隔的字符串");
                WinFormContext.UserLoveSettings.Save();
            }            
        }

        private void cbbNewLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNewLineType.SelectedValue == null) return;
            var splitType = cbbNewLineType.SelectedValue.ToString();
            if ("1".Equals(splitType))
            {
                txbEveryLineEndChar.Visible = false;
            }
            else
            {
                txbEveryLineEndChar.Visible = true;
            }
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
                rtbFormat.Clear();
                rtbFormat.AppendText(drArr[0][SplitStringTemplate.ValueString.TemplateString].ToString());
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

            string sContent = rtbFormat.Text.Trim();
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
                dr[SplitStringTemplate.KeyString.Name] = sTempName;
                dtKeyConfig.Rows.Add(dr);
                // 值处理
                DataRow drNew = dtValConfig.NewRow();
                drNew[sValId] = Guid.NewGuid().ToString();
                drNew[sKeyId] = sKeyIdNew;
                drNew[SplitStringTemplate.ValueString.TemplateString] = sContent;
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
                    dr[SplitStringTemplate.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][SplitStringTemplate.KeyString.Name] = sTempName;//修改名称
                }
                // 值处理
                if (drArrVal.Length > 0)
                {
                    drArrVal[0][SplitStringTemplate.ValueString.TemplateString] = sContent;
                }
                else
                {
                    DataRow drNew = dtValConfig.NewRow();
                    drNew[sValId] = Guid.NewGuid().ToString();
                    drNew[sKeyId] = sKeyIdNew;
                    drNew[SplitStringTemplate.ValueString.TemplateString] = sContent;
                    dtValConfig.Rows.Add(drNew);
                }
            }

            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, SplitStringTemplate.KeyString.Name, true, true);
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
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, SplitStringTemplate.KeyString.Name, true, true);
            ShowInfo("模板删除成功！");
        }
        #endregion

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            PasteTextFromClipse();
        }
    }
}
