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
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Breezee.WorkHelper.DBTool.Entity;
using org.breezee.MyPeachNet;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.IOC;
using Breezee.AutoSQLExecutor.Common;
using Breezee.WorkHelper.DBTool.IBLL;
using static Breezee.Core.Interface.KeyValuePairString;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并数据：将复制进来的两个Excel内容进行拼接
    /// 测试结果：通过
    /// @history:
    ///   2023-10-05 huangguohui 去掉SQL方式，增加异集，即两个集合不同的部分。    
    /// </summary>
    public partial class FrmDBTMergeData : BaseForm
    {
        #region 变量
        private string _strAutoSqlSuccess = "生成成功，详细见“生成结果”页签！";
        string sRowNo1 = "ROWNUM_0";
        string sRowNo2 = "ROWNUM_1";
        DataGridViewFindText dgvFindText;
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
            //合并类型
            _dicString.Add(((int)MergeDoubleDataStyle.Diff).ToString(), "异集");
            _dicString.Add(((int)MergeDoubleDataStyle.InnerJoin).ToString(), "交集");
            _dicString.Add(((int)MergeDoubleDataStyle.LeftJoin).ToString(), "左集");
            _dicString.Add(((int)MergeDoubleDataStyle.RightJoin).ToString(), "右集");
            _dicString.Add(((int)MergeDoubleDataStyle.UnionAll).ToString(), "并集");
            cbbSqlType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            DataTable dtCopy = new DataTable();
            dgvExcel1.BindAutoColumn(dtCopy);
            dgvExcel2.BindAutoColumn(dtCopy.Copy());
            dgvResult.BindAutoColumn(dtCopy.Copy());
            //
            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            lblInfo2.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            ckbAutoColumnName.Checked = true;
            toolTip1.SetToolTip(ckbNullNotEquals, "选中时为忽略条件中的空值，即两个空值不相等。如取消选中，且两个Excel中条件对应列存在较多空值时，\n就会因为大量的笛卡尔乘积记录消耗内存，而导致【System.OutOfMemoryException】内存不足错误！所以还是建议选中该项！");
        }
        #endregion

        #region 网格粘贴事件
        private void dgvExcel1_KeyDown(object sender, KeyEventArgs e)
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

                    DataTable dtMain = new DataTable();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "", false, true, sRowNo1);
                    dgvExcel1.BindAutoColumn(dtMain, false, new List<FlexGridColumn> {
                        new FlexGridColumn.Builder().Name(sRowNo1).Caption("序号1").Width(60).Build()
                    });
                    dgvExcel1.ShowRowNum(false, sRowNo1);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
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

                    DataTable dtMain = new DataTable();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "1", true, true, sRowNo2);
                    dgvExcel2.BindAutoColumn(dtMain, false, new List<FlexGridColumn> {
                        new FlexGridColumn.Builder().Name(sRowNo2).Caption("序号2").Width(60).Build()
                    });
                    dgvExcel2.ShowRowNum(false, sRowNo2);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        private async void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                #region 处理前验证
                DataTable dtMain = dgvExcel1.GetBindingTable();
                DataTable dtSec = dgvExcel2.GetBindingTable();
                if (dtMain.Rows.Count == 0 || dtSec.Rows.Count == 0)
                {
                    ShowInfo("两个Excel都必须要有数据！");
                    return;
                }

                string sType = cbbSqlType.SelectedValue == null ? "" : cbbSqlType.SelectedValue.ToString();
                int iMergeType = int.Parse(sType);
                MergeDoubleDataStyle mergeTye = (MergeDoubleDataStyle)iMergeType;

                string sErrorCondition = "条件格式不正确，正确示例为：A=A1,B=B1";
                string[] arrCondition = rtbConString.Text.Trim().Split(',', '，');
                if (arrCondition == null || arrCondition.Length == 0)
                {
                    ShowInfo(sErrorCondition);
                    return;
                }

                IDictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var sOne in arrCondition)
                {
                    string[] arrColumn = sOne.Trim().Split('=');
                    if (arrCondition == null || arrCondition.Length == 0 || arrColumn.Length != 2)
                    {
                        ShowInfo(sErrorCondition);
                        return;
                    }

                    if (!dtMain.Columns.Contains(arrColumn[0]))
                    {
                        ShowInfo("第一个Excel不存在列：“" + arrColumn[0] + "”！");
                        return;
                    }
                    if (!dtSec.Columns.Contains(arrColumn[1]))
                    {
                        ShowInfo("第二个Excel不存在列：“" + arrColumn[1] + "”！");
                        return;
                    }
                    dic.Add(arrColumn[0], arrColumn[1]);
                }
                #endregion

                #region 表结构处理
                HashSet<string> doubleCol = new HashSet<string>();
                DataTable dtResult = dgvResult.GetBindingTable();
                dtResult.Columns.Clear();
                foreach (DataColumn item in dtMain.Columns)
                {
                    if (!dtResult.Columns.Contains(item.ColumnName))
                    {
                        dtResult.Columns.Add(item.ColumnName,item.DataType); //这里要跟原表类型一致
                    }
                }

                foreach (DataColumn item in dtSec.Columns)
                {
                    if (!dtResult.Columns.Contains(item.ColumnName))
                    {
                        dtResult.Columns.Add(item.ColumnName,item.DataType); //这里要跟原表类型一致
                    }
                    else
                    {
                        dtResult.Columns.Add(item.ColumnName + "_1");
                        doubleCol.Add(item.ColumnName);
                    }
                }
                if (doubleCol.Count > 0)
                {
                    ShowInfo("Excel-2存在跟Excel-1重复的列名(包括：" + string.Join(",", doubleCol) + "),合并后其会自动增加数字后缀！");
                }
                #endregion

                //异步处理数据
                tsbAutoSQL.Enabled = false;
                ShowDestopTipMsg("正在异步处理数据，请稍等一会...");
                var recordTime = System.Diagnostics.Stopwatch.StartNew();
                SyncExcResult excResult = null;
                // 使用LINQ合并数据：速度快
                excResult = await Task.Run(() => DataTableConnectStringByLinq(dtMain, dtSec, dtResult, mergeTye, dic));
                // 使用循环合并数据：速度慢，已取消
                //excResult = await Task.Run(() => DataTableConnectStringByLoop(dtMain, dtSec, dtResult, mergeTye, dic));
                string sTotalSecode = "耗时: " + recordTime.Elapsed.ToString("ss") + "." + recordTime.Elapsed.ToString("fff") + " 秒。";
                recordTime.Stop();
                if (excResult.Success)
                {
                    //绑定已知的两个序号列
                    dgvResult.BindAutoColumn(excResult.DtResult,false,new List<FlexGridColumn> { 
                        new FlexGridColumn.Builder().Name(sRowNo1).Caption("序号1").Width(60).Build(),
                        new FlexGridColumn.Builder().Name(sRowNo2).Caption("序号2").Width(60).Build()
                    });
                    dgvResult.ShowRowNum(true);
                    tabControl1.SelectedTab = tpAutoSQL;
                    ShowInfo(_strAutoSqlSuccess + sTotalSecode);//生成SQL成功后提示
                }
                else
                {
                    ShowErr(excResult.Message);//生成失败后提示
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                tsbAutoSQL.Enabled = true;
            }
        }

        /// <summary>
        /// 使用LINQ处理表连接数据（速度快）
        /// </summary>
        /// <param name="dtMain"></param>
        /// <param name="dtSec"></param>
        /// <param name="dtResultIn"></param>
        /// <param name="sType"></param>
        /// <param name="dic"></param>
        private async Task<SyncExcResult> DataTableConnectStringByLinq(DataTable dtMain, DataTable dtSec, DataTable dtResultIn, MergeDoubleDataStyle sType, IDictionary<string, string> dic)
        {
            try
            {
                SyncExcResult result = null;
                DataTable dtResult = dtResultIn.Clone();

                int iCondCount = dic.Keys.Count;
                // 使用动态属性
                //查询
                var query = from f in dtMain.AsEnumerable()
                            join s in dtSec.AsEnumerable()
                            on GetLinqDynamicOnJoin(dic, f, true)
                            equals GetLinqDynamicOnJoin(dic, s, false)
                            where ckbNullNotEquals.Checked ? GetLinqDynamicWhere(dic, f, s) : true
                            select new { F1 = f, S1 = s };

                //查询交集数据
                var joinList = query.ToList();
                var restult = joinList.Select(t => t.F1.ItemArray.Concat(t.S1.ItemArray).ToArray()); //这里最后必须要加上ToArray
                foreach (var item in restult)
                {
                    dtResult.Rows.Add(item); //增加行数据：当行太多时，这里会报内存溢出System.OutOfMemoryException错误
                }
                //如是交集，则直接返回
                switch (sType)
                {
                    case MergeDoubleDataStyle.InnerJoin:
                        result = new SyncExcResult(true, "转换成功！", dtResult);
                        break;
                    case MergeDoubleDataStyle.LeftJoin:
                        //左边独有部分
                        var leftJoin = from f in dtMain.AsEnumerable()
                                       where !joinList.Any(j => j.F1.Field<int>(sRowNo1) == f.Field<int>(sRowNo1))
                                       select new { F1 = f };
                        var LeftJoinRes = leftJoin.ToList();
                        foreach (var item in LeftJoinRes)
                        {
                            dtResult.Rows.Add(item.F1.ItemArray.Concat(new object[dtSec.Columns.Count]).ToArray()); //增加行数据
                        }
                        result = new SyncExcResult(true, "转换成功！", dtResult);
                        break;
                    case MergeDoubleDataStyle.RightJoin:
                        //右边独有部分
                        var rightJoin = from f in dtSec.AsEnumerable()
                                       where !joinList.Any(j => j.S1.Field<int>(sRowNo2) == f.Field<int>(sRowNo2))
                                       select new { F1 = f };
                        var rightJoinRes = rightJoin.ToList();
                        foreach (var item in rightJoinRes)
                        {
                            dtResult.Rows.Add(new object[dtMain.Columns.Count].Concat(item.F1.ItemArray).ToArray()); //增加行数据
                        }
                        result = new SyncExcResult(true, "转换成功！", dtResult);
                        break;
                    case MergeDoubleDataStyle.Diff:
                        DataTable dtResultDif = dtResultIn.Clone();
                        //左边独有部分
                        var leftJoin1 = from f in dtMain.AsEnumerable()
                                       where !joinList.Any(j => j.F1.Field<int>(sRowNo1) == f.Field<int>(sRowNo1))
                                       select new { F1 = f };
                        var LeftJoinRes1 = leftJoin1.ToList();
                        foreach (var item in LeftJoinRes1)
                        {
                            dtResultDif.Rows.Add(item.F1.ItemArray.Concat(new object[dtSec.Columns.Count]).ToArray()); //增加行数据
                        }
                        //右边独有部分
                        var rightJoin1 = from f in dtSec.AsEnumerable()
                                        where !joinList.Any(j => j.S1.Field<int>(sRowNo2) == f.Field<int>(sRowNo2))
                                        select new { F1 = f };
                        var rightJoinRes1 = rightJoin1.ToList();
                        foreach (var item in rightJoinRes1)
                        {
                            dtResultDif.Rows.Add(new object[dtMain.Columns.Count].Concat(item.F1.ItemArray).ToArray()); //增加行数据
                        }
                        result = new SyncExcResult(true, "转换成功！", dtResultDif);
                        break;
                    case MergeDoubleDataStyle.UnionAll:
                        //左边独有部分
                        var leftJoin2 = from f in dtMain.AsEnumerable()
                                        where !joinList.Any(j => j.F1.Field<int>(sRowNo1) == f.Field<int>(sRowNo1))
                                        select new { F1 = f };
                        var LeftJoinRes2 = leftJoin2.ToList();
                        foreach (var item in LeftJoinRes2)
                        {
                            dtResult.Rows.Add(item.F1.ItemArray.Concat(new object[dtSec.Columns.Count]).ToArray()); //增加行数据
                        }
                        //右边独有部分
                        var rightJoin2 = from f in dtSec.AsEnumerable()
                                         where !joinList.Any(j => j.S1.Field<int>(sRowNo2) == f.Field<int>(sRowNo2))
                                         select new { F1 = f };
                        var rightJoinRes2 = rightJoin2.ToList();
                        foreach (var item in rightJoinRes2)
                        {
                            dtResult.Rows.Add(new object[dtMain.Columns.Count].Concat(item.F1.ItemArray).ToArray()); //增加行数据
                        }
                        result = new SyncExcResult(true, "转换成功！", dtResult);
                        break;
                    default:
                        break;
                }
                //返回结果
                return result;
            }
            catch (Exception ex)
            {
                return new SyncExcResult(false, "转换失败：" + ex.Message, null);
            }
        }

        /// <summary>
        /// 获取LINQ动态条件
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="dr"></param>
        /// <param name="isKey"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static object GetLinqDynamicOnJoin(IDictionary<string, string> dic, DataRow dr,bool isKey)
        {
            int iCondCount = dic.Count();
            if (iCondCount == 1)
            {
                return isKey ? new { c1 = dr.Field<string>(dic.ElementAt(0).Key) } : new { c1 = dr.Field<string>(dic.ElementAt(0).Value) };
            }
            else if (iCondCount == 2)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value)
                };
            }
            else if (iCondCount == 3)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value)
                };
            }
            else if (iCondCount == 4)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value)
                };
            }
            else if (iCondCount == 5)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value)
                };
            }
            else if (iCondCount == 6)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key),
                    c6 = dr.Field<string>(dic.ElementAt(5).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value),
                    c6 = dr.Field<string>(dic.ElementAt(5).Value)
                };
            }
            else if (iCondCount == 7)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key),
                    c6 = dr.Field<string>(dic.ElementAt(5).Key),
                    c7 = dr.Field<string>(dic.ElementAt(6).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value),
                    c6 = dr.Field<string>(dic.ElementAt(5).Value),
                    c7 = dr.Field<string>(dic.ElementAt(6).Value)
                };
            }
            else if (iCondCount == 8)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key),
                    c6 = dr.Field<string>(dic.ElementAt(5).Key),
                    c7 = dr.Field<string>(dic.ElementAt(6).Key),
                    c8 = dr.Field<string>(dic.ElementAt(7).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value),
                    c6 = dr.Field<string>(dic.ElementAt(5).Value),
                    c7 = dr.Field<string>(dic.ElementAt(6).Value),
                    c8 = dr.Field<string>(dic.ElementAt(7).Value)
                };
            }
            else if (iCondCount == 9)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key),
                    c6 = dr.Field<string>(dic.ElementAt(5).Key),
                    c7 = dr.Field<string>(dic.ElementAt(6).Key),
                    c8 = dr.Field<string>(dic.ElementAt(7).Key),
                    c9 = dr.Field<string>(dic.ElementAt(8).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value),
                    c6 = dr.Field<string>(dic.ElementAt(5).Value),
                    c7 = dr.Field<string>(dic.ElementAt(6).Value),
                    c8 = dr.Field<string>(dic.ElementAt(7).Value),
                    c9 = dr.Field<string>(dic.ElementAt(8).Value)
                };
            }
            else if (iCondCount == 10)
            {
                return isKey ? new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Key),
                    c2 = dr.Field<string>(dic.ElementAt(1).Key),
                    c3 = dr.Field<string>(dic.ElementAt(2).Key),
                    c4 = dr.Field<string>(dic.ElementAt(3).Key),
                    c5 = dr.Field<string>(dic.ElementAt(4).Key),
                    c6 = dr.Field<string>(dic.ElementAt(5).Key),
                    c7 = dr.Field<string>(dic.ElementAt(6).Key),
                    c8 = dr.Field<string>(dic.ElementAt(7).Key),
                    c9 = dr.Field<string>(dic.ElementAt(8).Key),
                    c10 = dr.Field<string>(dic.ElementAt(9).Key)
                } :
                new
                {
                    c1 = dr.Field<string>(dic.ElementAt(0).Value),
                    c2 = dr.Field<string>(dic.ElementAt(1).Value),
                    c3 = dr.Field<string>(dic.ElementAt(2).Value),
                    c4 = dr.Field<string>(dic.ElementAt(3).Value),
                    c5 = dr.Field<string>(dic.ElementAt(4).Value),
                    c6 = dr.Field<string>(dic.ElementAt(5).Value),
                    c7 = dr.Field<string>(dic.ElementAt(6).Value),
                    c8 = dr.Field<string>(dic.ElementAt(7).Value),
                    c9 = dr.Field<string>(dic.ElementAt(8).Value),
                    c10 = dr.Field<string>(dic.ElementAt(9).Value)
                };
            }
            else
            {
                throw new Exception("最多只能输入 10 个条件！");
            }
        }

        /// <summary>
        /// LINQ动态Where条件（去掉条件为空值的列）
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="drF"></param>
        /// <param name="drS"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static bool GetLinqDynamicWhere(IDictionary<string, string> dic, DataRow drF, DataRow drS)
        {
            int iCondCount = dic.Count();
            if (iCondCount == 1)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key)) && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value));
            }
            else if (iCondCount == 2)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key)) 
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    ;
            }
            else if (iCondCount == 3)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    ;
            }
            else if (iCondCount == 4)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    ;
            }
            else if (iCondCount == 5)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    ;
            }
            else if (iCondCount == 6)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(5).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(5).Value))
                    ;
            }
            else if (iCondCount == 7)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(5).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(6).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(5).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(6).Value))
                    ;
            }
            else if (iCondCount == 8)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(5).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(6).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(7).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(5).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(6).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(7).Value))
                    ;
            }
            else if (iCondCount == 9)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(5).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(6).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(7).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(8).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(5).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(6).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(7).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(8).Value))
                    ;
            }
            else if (iCondCount == 10)
            {
                return !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(0).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(1).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(2).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(3).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(4).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(5).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(6).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(7).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(8).Key))
                    && !string.IsNullOrEmpty(drF.Field<string>(dic.ElementAt(9).Key))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(0).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(1).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(2).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(3).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(4).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(5).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(6).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(7).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(8).Value))
                    && !string.IsNullOrEmpty(drS.Field<string>(dic.ElementAt(9).Value))
                    ;
            }
            else
            {
                throw new Exception("最多只能输入 10 个条件！");
            }
        }


        /// <summary>
        /// 循环处理表连接数据(速度慢)
        /// </summary>
        /// <param name="dtMain"></param>
        /// <param name="dtSec"></param>
        /// <param name="dtResultIn"></param>
        /// <param name="sType"></param>
        /// <param name="dic"></param>
        private async Task<SyncExcResult> DataTableConnectStringByLoop(DataTable dtMain, DataTable dtSec, DataTable dtResultIn, MergeDoubleDataStyle sType, IDictionary<string, string> dic)
        {
            try
            {
                DataTable dtResult = dtResultIn.Clone();
                //为第二个Excel表增加一个未处理数据的列。最终结果表也要增加
                string sDealColumnName = "DataIsDealFlagColumnNameUniq";
                if (!dtSec.Columns.Contains(sDealColumnName))
                {
                    dtSec.Columns.Add(sDealColumnName, typeof(string)); //增加处理列
                }
                if (!dtResultIn.Columns.Contains(sDealColumnName))
                {
                    dtResultIn.Columns.Add(sDealColumnName, typeof(string)); //增加处理列
                }
                //确定要循环的表
                if (sType == MergeDoubleDataStyle.RightJoin)
                {
                    #region 右集的处理
                    //循环第二个Excel
                    for (int i = 0; i < dtSec.Rows.Count; i++)
                    {
                        string sConndition = "";
                        int iStep = 0;
                        foreach (string sKey in dic.Keys)
                        {
                            string sSource = ckbCondTrim.Checked ? dtSec.Rows[i][dic[sKey]].ToString().trim() : dtSec.Rows[i][dic[sKey]].ToString(); //注：这里要取字典的值作为第二个表的列名
                            if (iStep == 0)
                            {
                                sConndition += sKey + "= '" + sSource + "'"; //注：这里要取字典的键作为第一个表的过滤条件
                            }
                            else
                            {
                                sConndition += " and " + sKey + " = '" + sSource + "' "; //注：这里要取字典的键作为第一个表的过滤条件
                            }
                            iStep++;
                        }

                        //合并能关联上的数据
                        DataRow[] arrSec = dtMain.Select(sConndition);
                        if (arrSec.Length > 0)
                        {
                            foreach (DataRow item in arrSec)
                            {
                                dtResult.Rows.Add(item.ItemArray.Concat(dtSec.Rows[i].ItemArray).ToArray()); //增加行数据
                            }
                        }
                        else
                        {
                            dtResult.Rows.Add(new string[dtMain.Columns.Count].Concat(dtSec.Rows[i].ItemArray).ToArray());//增加行数据：首Excel表所有列为空数据
                        }
                    }
                    #endregion
                }
                else
                {
                    //交集、左集、差集、全集处理
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        string sConndition = "";
                        int iStep = 0;
                        foreach (string sKey in dic.Keys)
                        {
                            string sSource = ckbCondTrim.Checked ? dtMain.Rows[i][sKey].ToString().Trim() : dtMain.Rows[i][sKey].ToString(); //注：这里要取字典的键作为第一个表的列名
                            if (iStep == 0)
                            {
                                sConndition += dic[sKey] + "= '" + sSource + "'"; //注：这里要取字典的值作为第二个表的过滤条件
                            }
                            else
                            {
                                sConndition += " and " + dic[sKey] + " = '" + sSource + "' "; //注：这里要取字典的值作为第二个表的过滤条件
                            }

                            iStep++;
                        }

                        //合并能关联上的数据
                        DataRow[] arrSec = dtSec.Select(sConndition);
                        if (arrSec.Length > 0)
                        {
                            foreach (DataRow item in arrSec)
                            {
                                item[sDealColumnName] = 1; //修改为已处理
                                if (sType != MergeDoubleDataStyle.Diff)
                                {
                                    dtResult.Rows.Add(dtMain.Rows[i].ItemArray.Concat(item.ItemArray).ToArray()); //增加行数据
                                }
                            }
                        }
                        else
                        {
                            //非交集，即左集、异集、并集时
                            if (sType != MergeDoubleDataStyle.InnerJoin)
                            {
                                dtResult.Rows.Add(dtMain.Rows[i].ItemArray.Concat(new string[dtSec.Columns.Count]).ToArray()); //增加行数据：次Excel表所有列为空数据
                            }
                        }
                    }
                    //针对交集，直接退出
                    if (sType == MergeDoubleDataStyle.InnerJoin)
                    {
                        return new SyncExcResult(true, "转换成功！", dtResult);
                    }
                    //非左集，即异集或并集
                    if (sType != MergeDoubleDataStyle.LeftJoin)
                    {
                        DataRow[] arrSecOther = dtSec.Select("" + sDealColumnName + " is null");
                        foreach (DataRow item in arrSecOther)
                        {
                            dtResult.Rows.Add(new string[dtMain.Columns.Count].Concat(item.ItemArray).ToArray()); //增加行数据：首Excel表所有列为空数据
                        }
                    }
                }
                return new SyncExcResult(true, "转换成功！", dtResult);
            }
            catch (Exception ex)
            {
                return new SyncExcResult(false, "转换失败：" + ex.Message, null);
            }
        }

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
                string sText = "CITY_ID\tPROVINCE_ID\tCITY_CODE\tCITY_NAME\r\n1\t1\t1\t北京\r\n10\t6\t10\t沈阳市\r\n100\t30\t100\t番禺\r\n10000\t8\t10000\t邢台\r\n10001\t8\t10001\t衡水";
                DataTable dtMain = new DataTable();//dgvExcel1.GetBindingTable();
                //dtMain.Clear();
                //dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "", false, true, sRowNo1);
                //dgvExcel1.BindAutoColumn(dtMain);
                dgvExcel1.BindAutoColumn(dtMain, false, new List<FlexGridColumn> {
                        new FlexGridColumn.Builder().Name(sRowNo1).Caption("序号1").Width(60).Build()
                    });
                dgvExcel1.ShowRowNum(false, sRowNo1);

                sText = "CITY_ID\tPROVINCE_ID\tCITY_CODE\r\n1\t1\t1\r\n10\t6\t10\r\n100\t303\t100\r\nA20\tB21\t测试";
                dtMain = new DataTable();//dgvExcel2.GetBindingTable();
                //dtMain.Clear();
                //dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "1", false, true, sRowNo2);
                //dgvExcel2.BindAutoColumn(dtMain);
                dgvExcel2.BindAutoColumn(dtMain, false, new List<FlexGridColumn> {
                        new FlexGridColumn.Builder().Name(sRowNo2).Caption("序号2").Width(60).Build()
                    });
                dgvExcel2.ShowRowNum(false, sRowNo2);

                ckbAutoColumnName.Checked = true;
                rtbConString.Text = "A=A1";

            }
            else
            {
                DataTable dtMain = dgvExcel1.GetBindingTable();
                dtMain.Clear();
                dtMain.Columns.Clear();

                dtMain = dgvExcel2.GetBindingTable();
                dtMain.Clear();
                dtMain.Columns.Clear();

                rtbConString.Text = "";
            }

            DataTable dtRet = dgvResult.GetBindingTable();
            if (dtRet != null)
            {
                dtRet.Clear();
            }

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
            ExportHelper.ExportExcel(dtResult, "合并后数据_" + DateTime.Now.ToString("yyyyMMddHHmmss"), true);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            dgvSelect.GetBindingTable().Columns.Clear();
            dgvSelect.GetBindingTable().Clear();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            DataRow dataRow = dgvSelect.GetCurrentRow();
            if (dataRow == null || dataRow.RowState == DataRowState.Detached) return;
            dt.Rows.Remove(dataRow);
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            FindGridText(true);
        }

        private void btnFindFront_Click(object sender, EventArgs e)
        {
            FindGridText(false);
        }
        private void FindGridText(bool isNext)
        {
            string sSearch = txbSearchColumn.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvResult.SeachText(sSearch, ref dgvFindText, null, isNext);
            lblFind.Text = dgvFindText.CurrentMsg;
        }

        private void tsmiJoin_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            if (dgvSelect.CurrentCell == null) return;
            int iCurCol = dgvSelect.CurrentCell.ColumnIndex;
            if(dgvExcel1.Name.Equals(dgvSelect.Name))
            {
                if (rtbConString.Text.Trim().Length == 0)
                {
                    rtbConString.AppendText(dgvSelect.Columns[iCurCol].Name);
                }
                else
                {
                    rtbConString.AppendText(","+dgvSelect.Columns[iCurCol].Name);
                }
            }
            else
            {
                rtbConString.AppendText("="+dgvSelect.Columns[iCurCol].Name);
            }
        }
    }

    /// <summary>
    /// 两个数据合并方式
    /// </summary>
    public enum MergeDoubleDataStyle
    {
        /// <summary>
        /// 交集
        /// </summary>
        InnerJoin = 1,
        /// <summary>
        /// 左集
        /// </summary>
        LeftJoin = 2,
        /// <summary>
        /// 右集
        /// </summary>
        RightJoin = 3,
        /// <summary>
        /// 异集
        /// </summary>
        Diff = 4,
        /// <summary>
        /// 全集
        /// </summary>
        UnionAll = 5,
    }

    public class SyncExcResult
    {
        bool success = false;
        string message = string.Empty;
        DataTable dtResult;

        public SyncExcResult(bool success, string message, DataTable dtResult)
        {
            this.Success = success;
            this.Message = message;
            this.dtResult = dtResult;
        }

        public bool Success { get => success; set => success = value; }
        public string Message { get => message; set => message = value; }

        public DataTable DtResult { get => dtResult; set => dtResult = value; }
    }

}
