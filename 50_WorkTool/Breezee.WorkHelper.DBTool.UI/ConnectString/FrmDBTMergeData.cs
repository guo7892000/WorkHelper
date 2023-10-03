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

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并数据：将复制进来的两个Excel内容进行拼接
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBTMergeData : BaseForm
    {
        #region 变量
        private string _strAutoSqlSuccess = "生成成功，详细见“生成结果”页签！";
        string sDealColumnName = "DataIsDealFlagColumnNameUniq";
        private IDBConfigSet _IDBConfigSet;
        private IDataAccess _dataAccess;
        DbServerInfo _dbServer;
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
            _dicString.Add(((int)MergeDoubleDataStyle.InnerJoin).ToString(), "交集");
            _dicString.Add(((int)MergeDoubleDataStyle.LeftJoin).ToString(), "左集");
            _dicString.Add(((int)MergeDoubleDataStyle.RightJoin).ToString(), "右集");
            _dicString.Add(((int)MergeDoubleDataStyle.UnionAll).ToString(), "并集");
            cbbSqlType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            //合并算法
            _dicString.Clear();
            _dicString["1"] = "表方式(推荐)";
            _dicString["2"]= "SQL方式";
            cbbMergeFunc.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            DataTable dtCopy = new DataTable();
            //dtCopy.TableName = _strExcel1;
            //bsExcel1.DataSource = dtCopy;
            //dgvExcel1.DataSource = bsExcel1;
            //WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy });
            dgvExcel1.BindAutoColumn(dtCopy);

            //DataTable dtCopy2 = dtCopy.Copy();
            //dtCopy2.TableName = _strExcel2;
            //bsExcel2.DataSource = dtCopy2;
            //dgvExcel2.DataSource = bsExcel2;
            //WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy2 });
            dgvExcel2.BindAutoColumn(dtCopy.Copy());

            //DataTable dtCopy3 = dtCopy.Copy();
            //dtCopy3.TableName = _strExcel3;
            //bsExcel3.DataSource = dtCopy3;
            //dgvResult.DataSource = bsExcel3;
            //WinFormGlobalValue.SetPublicDataSource(new DataTable[] { dtCopy3 });
            dgvResult.BindAutoColumn(dtCopy.Copy());

            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            //uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            #endregion
            //
            lblTableData.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            lblInfo2.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：第一行为列名！";
            ckbAutoColumnName.Checked = true;
            toolTip1.SetToolTip(cbbMergeFunc, "当对比项太多时，请使用【表方式】；因为使用SQL方式时，SQL会很长，那样数据库会报错！");
            rtbConString.Text = "A=A1";
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

                    DataTable dtMain = dgvExcel1.GetBindingTable();
                    dtMain.Clear();
                    dtMain.Columns.Clear();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain);
                    dgvExcel1.ShowRowNum();
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 生成SQL按钮事件
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

                string sErrorCondition = "条件格式不正确，正确示例为：A=B,B=E！";
                string[] arrCondition = rtbConString.Text.Trim().Split(',', '，');
                if (arrCondition == null || arrCondition.Length == 0)
                {
                    ShowInfo(sErrorCondition);
                    return;
                }

                IDictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var sOne in arrCondition)
                {
                    string[] arrColumn = rtbConString.Text.Trim().Split('=');
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

                    #endregion
                }

                #region 表结构处理
                DataTable dtMainCopy = dtMain.Copy();
                DataTable dtSecCopy = dtSec.Copy();
                dtMainCopy.Columns.Remove(GlobalKey.RowNum);
                dtSecCopy.Columns.Remove(GlobalKey.RowNum);
                if (!dtSecCopy.Columns.Contains(sDealColumnName))
                {
                    dtSecCopy.Columns.Add(sDealColumnName, typeof(string)); //增加处理列
                }

                DataTable dtResult = dgvResult.GetBindingTable();
                dtResult.Columns.Clear();
                dtResult.Rows.Clear();
                foreach (DataColumn item in dtMainCopy.Columns)
                {
                    if (!dtResult.Columns.Contains(item.ColumnName))
                    {
                        dtResult.Columns.Add(item.ColumnName);
                    }
                }
                foreach (DataColumn item in dtSecCopy.Columns)
                {
                    if (!dtResult.Columns.Contains(item.ColumnName))
                    {
                        dtResult.Columns.Add(item.ColumnName);
                    }
                }
                if (dgvResult.Columns.Contains(sDealColumnName))
                {
                    dgvResult.Columns[sDealColumnName].Visible = false;
                }
                #endregion

                if (cbbMergeFunc.SelectedValue == null) return;
                bool isSqlConnString = "2".Equals(cbbMergeFunc.SelectedValue.ToString()) ? true : false;
                bool isFromDual = false;
                if (isSqlConnString)
                {
                    _dbServer = uC_DbConnection1.GetDbServerInfo();
                    if (_dbServer == null)
                    {
                        ShowInfo("请先选择一个连接！");
                        return;
                    }
                    if(_dbServer.DatabaseType== DataBaseType.Oracle)
                    {
                        isFromDual = true;
                    }
                    _dataAccess = AutoSQLExecutors.Connect(_dbServer);
                }

                //异步处理数据
                tsbAutoSQL.Enabled = false;
                ShowDestopTipMsg("正在异步处理数据，请稍等一会...");
                var recordTime = System.Diagnostics.Stopwatch.StartNew();
                SyncExcResult excResult;
               
                if (isSqlConnString)
                {
                    excResult = await Task.Run(() => SqlConnectString(dtMainCopy, dtSecCopy, dtResult, mergeTye, dic, isFromDual));//SQL异步处理数据
                }
                else
                {
                    excResult = await Task.Run(() => DataTableConnectString(dtMainCopy, dtSecCopy, dtResult, mergeTye, dic));//表合并数据异步处理数据
                }
                string sTotalSecode = "耗时: " + recordTime.Elapsed.ToString("ss") + "." + recordTime.Elapsed.ToString("fff") + " 秒。";
                recordTime.Stop();
                if (excResult.Success)
                {
                    dgvResult.BindAutoColumn(excResult.DtResult);
                    dgvResult.ShowRowNum(true);
                    if (dgvResult.Columns.Contains(sDealColumnName))
                    {
                        dgvResult.Columns[sDealColumnName].Visible = false;
                    }
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
        /// 表处理连接字符串
        /// </summary>
        /// <param name="dtMain"></param>
        /// <param name="dtSec"></param>
        /// <param name="dtResultIn"></param>
        /// <param name="sType"></param>
        /// <param name="dic"></param>
        private async Task<SyncExcResult> DataTableConnectString(DataTable dtMain, DataTable dtSec, DataTable dtResultIn, MergeDoubleDataStyle sType, IDictionary<string, string> dic)
        {
            try
            {
                DataTable dtResult = dtResultIn.Clone();
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
                            string sSource = dtSec.Rows[i][dic[sKey]].ToString(); //注：这里要取字典的值作为第二个表的列名
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
                            dtResult.Rows.Add(new string[dtMain.Columns.Count - 1].Concat(dtSec.Rows[i].ItemArray).ToArray());//增加行数据：首Excel表所有列为空数据
                        }
                    }
                    #endregion
                }
                else
                {
                    //交集、左集、全集处理
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        string sConndition = "";
                        int iStep = 0;
                        bool isAdd = false;
                        foreach (string sKey in dic.Keys)
                        {
                            string sSource = dtMain.Rows[i][sKey].ToString(); //注：这里要取字典的键作为第一个表的列名
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
                        foreach (DataRow item in arrSec)
                        {
                            item[sDealColumnName] = 1; //修改为已处理
                            dtResult.Rows.Add(dtMain.Rows[i].ItemArray.Concat(item.ItemArray).ToArray()); //增加行数据
                            isAdd = true;
                        }
                        //左集或并集，并且未处理时
                        if ((sType == MergeDoubleDataStyle.LeftJoin || sType == MergeDoubleDataStyle.UnionAll) && !isAdd)
                        {
                            dtResult.Rows.Add(dtMain.Rows[i].ItemArray.Concat(new string[dtSec.Columns.Count - 1]).ToArray()); //增加行数据：次Excel表所有列为空数据
                        }
                    }
                    if (sType == MergeDoubleDataStyle.InnerJoin)
                    {
                        return new SyncExcResult(true, "转换成功！",dtResult); //针对交集，直接退出
                    }
                    //并集
                    if (sType == MergeDoubleDataStyle.UnionAll)
                    {
                        DataRow[] arrSecOther = dtSec.Select("" + sDealColumnName + " is null");
                        foreach (DataRow item in arrSecOther)
                        {
                            dtResult.Rows.Add(new string[dtMain.Columns.Count - 1].Concat(item.ItemArray).ToArray()); //增加行数据：首Excel表所有列为空数据
                        }
                    }
                }
                return new SyncExcResult(true, "转换成功！",dtResult);
            }
            catch (Exception ex)
            {
                return new SyncExcResult(false, "转换失败：" + ex.Message,null);
            }
        }

        private async Task<SyncExcResult> SqlConnectString(DataTable dtMain, DataTable dtSec, DataTable dtResult, MergeDoubleDataStyle sType, IDictionary<string, string> dic, bool isFromDual)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder();
                switch (sType)
                {
                    case MergeDoubleDataStyle.InnerJoin:
                        getDataUnionSql(dtMain, dtSec, dic, sbSql, " JOIN ",isFromDual, false);
                        break;
                    case MergeDoubleDataStyle.LeftJoin:
                        getDataUnionSql(dtMain, dtSec, dic, sbSql, " LEFT JOIN ", isFromDual, false);
                        break;
                    case MergeDoubleDataStyle.RightJoin:
                        getDataUnionSql(dtMain, dtSec, dic, sbSql, " LEFT JOIN ", isFromDual, true);
                        break;
                    case MergeDoubleDataStyle.UnionAll:
                        getDataFullOutUnionSql(dtMain, dtSec, dic, sbSql, isFromDual);
                        break;
                    default:
                        break;
                }

                DataTable dtQuery = _dataAccess.QueryHadParamSqlData(sbSql.toString(), new Dictionary<string, string>());               
                return new SyncExcResult(true,"转换成功！", dtQuery);
            }
            catch(Exception ex)
            {
                return new SyncExcResult(false, "转换失败：" + ex.Message,null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtMain"></param>
        /// <param name="dtSec"></param>
        /// <param name="dic"></param>
        /// <param name="sbSql"></param>
        /// <param name="sJoin"></param>
        /// <param name="isReverse"></param>
        private static void getDataUnionSql(DataTable dtMain, DataTable dtSec, IDictionary<string, string> dic, StringBuilder sbSql,string sJoin, bool isFromDual, bool isReverse=false)
        {
            sbSql.AppendLine("WITH TMP_A AS (");
            sbSql.AppendLine(isReverse ? dtSec.getUnionDataSql(isFromDual) : dtMain.getUnionDataSql(isFromDual)); //如是反转，则取第二个表
            sbSql.AppendLine("),TMP_B AS (");
            sbSql.AppendLine(isReverse ? dtMain.getUnionDataSql(isFromDual) : dtSec.getUnionDataSql(isFromDual));  //如是反转，则取第一个表
            sbSql.AppendLine(")");
            sbSql.AppendLine("SELECT * FROM TMP_A A");
            sbSql.AppendLine(sJoin + " TMP_B B ON ");
            string sAnd = "";
            foreach (string sKey in dic.Keys)
            {
                //针对A表别名：如是反转，则取值，否则取键；针对B表别名正好相反，如是反转，则取键，否则取值。
                sbSql.Append(sAnd + "A." + (isReverse? dic[sKey]:sKey) + "=" + "B." + (isReverse ? sKey:dic[sKey]) + " ");
                sAnd = "AND ";
            }
        }

        private static void getDataFullOutUnionSql(DataTable dtMain, DataTable dtSec, IDictionary<string, string> dic, StringBuilder sbSql, bool isFromDual)
        {
            sbSql.AppendLine("WITH TMP_A AS (");
            sbSql.AppendLine(dtMain.getUnionDataSql(isFromDual)); //如是反转，则取第二个表
            sbSql.AppendLine("),TMP_B AS (");
            sbSql.AppendLine(dtSec.getUnionDataSql(isFromDual));  //如是反转，则取第一个表
            sbSql.AppendLine(")");
            sbSql.AppendLine("SELECT A.*,B.* FROM TMP_A A");
            sbSql.AppendLine("LEFT JOIN TMP_B B ON ");
            string sAnd = "";
            foreach (string sKey in dic.Keys)
            {
                //针对A表别名：如是反转，则取值，否则取键；针对B表别名正好相反，如是反转，则取键，否则取值。
                sbSql.Append(sAnd + "A." + sKey + "=" + "B." + dic[sKey] + " ");
                sAnd = "AND ";
            }
            sbSql.AppendLine("UNION ALL");
            sbSql.AppendLine("SELECT B.*,A.* FROM TMP_B A");
            sbSql.AppendLine("LEFT JOIN  TMP_A B ON ");
            sAnd = "";
            foreach (string sKey in dic.Keys)
            {
                //针对A表别名：如是反转，则取值，否则取键；针对B表别名正好相反，如是反转，则取键，否则取值。
                sbSql.Append(sAnd + "A." + dic[sKey] + "=" + "B." + sKey + " ");
                sAnd = "AND ";
            }
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

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

                    DataTable dtMain = dgvExcel2.GetBindingTable();
                    dtMain.Clear();
                    dtMain.Columns.Clear();
                    pasteText.GetStringTable(ckbAutoColumnName.Checked, dtMain,"1",true);
                    dgvExcel2.ShowRowNum();
                    dtMain.Columns.Add(sDealColumnName, typeof(string)); //增加处理列
                    dgvExcel2.Columns[sDealColumnName].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void CkbLoadExampleData_CheckedChanged(object sender, EventArgs e)
        {
            if (CkbLoadExampleData.Checked)
            {
                string sText = "CITY_ID\tPROVINCE_ID\r\n1\t1\r\n10\t6\r\n100\t30\r\n10000\t8";
                DataTable dtMain = dgvExcel1.GetBindingTable();
                dtMain.Clear();
                dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain);

                sText = "CITY_CODE\tCITY_NAME\r\n1\t北京\r\n10\t沈阳市\r\n100\t番禺\r\n11\t茂名";
                dtMain = dgvExcel2.GetBindingTable();
                dtMain.Clear();
                dtMain.Columns.Clear();
                sText.GetStringTable(ckbAutoColumnName.Checked, dtMain, "1");

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
            if (dtResult==null || dtResult.Rows.Count==0)
            {
                ShowErr("没有要导出的记录！", "提示");
                return;
            }
            //导出Excel
            ExportHelper.ExportExcel(dtResult, "合并后数据_" + DateTime.Now.ToString("yyyyMMddHHmmss"),true);
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
            if (dataRow == null) return;
            dt.Rows.Remove(dataRow);
        }

        private void cbbMergeFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbMergeFunc.SelectedValue==null) return;
            if("2".Equals(cbbMergeFunc.SelectedValue.ToString()))
            {
                uC_DbConnection1.Visible = true;
            }
            else
            {
                uC_DbConnection1.Visible = false;
            }
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

        
    }

    /// <summary>
    /// 两个数据合并方式
    /// </summary>
    public enum MergeDoubleDataStyle
    {
        /// <summary>
        /// 交集
        /// </summary>
        InnerJoin=1,
        /// <summary>
        /// 左集
        /// </summary>
        LeftJoin=2,
        /// <summary>
        /// 右集
        /// </summary>
        RightJoin=3,
        /// <summary>
        /// 交集
        /// </summary>
        UnionAll=4,
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
