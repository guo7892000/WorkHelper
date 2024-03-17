using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Breezee.Core.Interface;
using Breezee.Core.Entity;
using System.Drawing;
using System.IO;
using NPOI.SS.Formula.Functions;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// DataGridView扩展
    /// </summary>
    public static class DataGridViewExtend
    {
        /// <summary>
        /// 使用自动的列名绑定网格数据源
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dtSource"></param>
        public static void BindAutoColumn(this DataGridView dgv, DataTable dtSource, bool isAutoSize = false,List<FlexGridColumn> fixCol = null)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dtSource;
            BindAutoColumn(dgv, bs, isAutoSize, fixCol);
        }

        /// <summary>
        /// 使用自动列名绑定数据源
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="bs"></param>
        /// <param name="isAutoSize"></param>
        public static void BindAutoColumn(this DataGridView dgv, BindingSource bs, bool isAutoSize = false, List<FlexGridColumn> fixCol = null)
        {
            dgv.DataSource = bs;
            if (isAutoSize)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //设置列和行自动调整
                dgv.AutoResizeColumns();
                dgv.AutoResizeRows();
                //设置网格列宽可手工调整
                //dgv.AllowUserToResizeColumns = true;
                //设置网格行高可手工调整
                //dgv.AllowUserToResizeRows = true;
            }
            //针对确定列名显示
            if (fixCol != null)
            {
                foreach (var item in fixCol)
                {
                    dgv.Columns[item.ColumnName].HeaderText = item.ColumnCaption;
                    dgv.Columns[item.ColumnName].Width = item.ColumnWidth;
                    dgv.Columns[item.ColumnName].DisplayIndex = (bs.DataSource as DataTable).Columns[item.ColumnName].Ordinal; //设置网格顺序跟表一致。
                }
            }
            //设置行的交互颜色
            SetRowColor(dgv);
        }

        /// <summary>
        /// 设置行奇偶行的交替颜色
        /// </summary>
        /// <param name="dgv"></param>
        private static void SetRowColor(DataGridView dgv)
        {
            if(WinFormContext.UserEnvConfig.IsUsedMyDefineGridHeaderStyle)
            {
                //设置第一行（列标题）高度
                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgv.ColumnHeadersHeight = WinFormContext.UserEnvConfig.GridHeaderHeight; //获取配置的标题行高度
                //设置列标题样式
                dgv.EnableHeadersVisualStyles = false;
                //dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Honeydew;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = WinFormContext.UserEnvConfig.GridHeaderBackColor; //获取配置的标题行的背景色
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //设置行的交替色
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = WinFormContext.UserEnvConfig.OddRowBackColor; //获取配置的奇数行的背景色
        }

        public static void BindAutoTable(this DataGridView dgv, DataTable dtSource, bool isAutoSize = false, List<FlexGridColumn> fixCol = null)
        {
            BindAutoColumn(dgv, dtSource, isAutoSize,fixCol);
        }

        public static void BindTagTable(this DataGridView dgv, DataTable dtSource, bool IsUseTagHistoryConfig = true, GridPager gPage = null)
        {
            BindDataGridView(dgv, dtSource, IsUseTagHistoryConfig, gPage);
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="isResetRowNum"></param>
        public static void ShowRowNum(this DataGridView dgv, bool isResetRowNum = false,string sRowNunColumnName="ROWNO")
        {
            int rowNumber = 1;
            int rowWidth = 60;
            if (dgv.Columns.Contains(sRowNunColumnName))
            {
                dgv.Columns[sRowNunColumnName].HeaderText = "序号";
                dgv.Columns[sRowNunColumnName].Width = rowWidth;
                dgv.Columns[sRowNunColumnName].ValueType = typeof(int); //设置序号为整型
                dgv.RowHeadersVisible = false;//不显示行标题，即第一个空白列
                if (!isResetRowNum)
                {
                    return;
                }
                DataTable dtBind = dgv.GetBindingTable();
                string sLineNumName = sRowNunColumnName;
                if (dtBind == null)
                {
                    //没有绑定表
                    if (dgv.Columns.Contains(sLineNumName))
                    {
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.IsNewRow) continue;
                            row.Cells[sLineNumName].Value = rowNumber; //这里必须是字符
                            row.Cells[sLineNumName].ValueType = typeof(int); //设置序号为整型
                            rowNumber++;
                        }
                    }
                }
                else
                {
                    //有绑定表
                    if (!dtBind.Columns.Contains(sLineNumName))
                    {
                        dtBind.AddLineNum();
                    }
                    foreach (DataRow row in dtBind.Rows)
                    {
                        row[sLineNumName] = rowNumber; //这里必须是字符
                        rowNumber++;
                    }
                }
                dgv.RowHeadersVisible = false;//不显示行标题，即第一个空白列
            }
            else
            {
                dgv.RowHeadersVisible = true;//显示行标题，即第一个空白列
                dgv.RowHeadersWidth = rowWidth;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    row.HeaderCell.Value = rowNumber.ToString(); //这里必须是字符，才能显示在行标题中
                    rowNumber++;
                }
                //dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            }
        }

        #region 绑定网格数据为对象集合
        /// <summary>
        /// 绑定网格数据为对象集合
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="list"></param>
        public static void BindDataGridView(this DataGridView dgv, IEnumerable<DbEntity> list)
        {
            bool isFirstColumnVisable = false;
            BindDataGridViewBeforeSetSource(dgv, out isFirstColumnVisable);
            BindingSource bs = new BindingSource();
            bs.DataSource = list;
            dgv.DataSource = bs;
            if (list.ToList().Count == 0)
            {
                if (dgv.Rows.Count != 0)
                {
                    dgv.Rows.Clear();
                }
            }
            if (!isFirstColumnVisable)
            {
                dgv.Columns[0].Visible = false;
            }
        }
        #endregion

        #region 绑定网格数据为表(使用Tag历史配置文件)
        /// <summary>
        /// 绑定网格数据为表(使用Tag历史配置文件)
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dtSource"></param>
        public static void BindDataGridView(this DataGridView dgv, DataTable dtSource, bool IsUseTagHistoryConfig = true, GridPager gPage = null)
        {
            if (dgv.Tag == null || string.IsNullOrEmpty(dgv.Tag.ToString()))
            {
                throw new Exception(string.Format("请设置网格{0}的列定义，可以通过Tag或者NewColumnDefinition设置", dgv.Name));
            }
            
            FlexGridColumnDefinition columnDef = dgv.Tag as FlexGridColumnDefinition;
            if (columnDef == null)
            {
                columnDef = FlexGridColumnDefinition.GetDefinitionFromTagString(dgv.Tag.ToString());
            }

            if (!columnDef.Columns.Exists(x => x.ColumnCaption == "序号"))
            {
                columnDef.Columns.Insert(0, FlexGridColumn.NewRowNoCol());
            }

            if (dtSource == null)
            {
                dtSource = GenerateEmptyTable(columnDef.Columns);
            }

            if (IsUseTagHistoryConfig)
            {
                #region 得到之前保存的Tag值
                string strPath = dgv.GetStylePathString();
                FlexGridColumnDefinition defOther = FlexGridColumnDefinition.GetDefinitionFromFile(strPath);//读取网格路径 

                //判断是否为同一个网格设置
                if (columnDef.IsSame(defOther))
                {
                    columnDef = defOther;
                    dgv.Tag = columnDef.GetGridTagString();
                }
                #endregion
            }

            //ROWNO赋值
            if (!dtSource.Columns.Contains("ROWNO"))
            {
                dtSource.Columns.Add("ROWNO", typeof(int));

                int i = 1;
                foreach (DataRow row in dtSource.Rows)
                {
                    row["ROWNO"] = i++;
                }
            }

            //检测网格列是否存在
            CheckGridColNotExists(columnDef.Columns, dtSource, dgv);
            //绑定网格
            bool isFirstColumnVisable = false;
            BindDataGridViewBeforeSetSource(dgv, out isFirstColumnVisable);
            BindingSource bs = new BindingSource();
            bs.DataSource = dtSource;
            dgv.DataSource = bs;
            if (dtSource.Rows.Count == 0)
            {
                if (dgv.Rows.Count > 1)
                {
                    dgv.Rows.Clear();
                }
            }
            if (!isFirstColumnVisable)
            {
                dgv.Columns[0].Visible = false;
            }
            if (gPage != null)
            {
                //绑定分页控件  
                gPage.DataBindingSource.DataSource = dtSource;
                gPage.RecordNavigator.BindingSource = bs;
            }
            //一些属性初始化
            dgv.AllowUserToAddRows = false; //不允许增加行
            dgv.RowHeadersVisible = false;//不显示行标题，即第一个空白列
        }
        #endregion

        #region 清空网格数据
        /// <summary>
        /// 清空网格数据
        /// </summary>
        /// <param name="dgv"></param>
        public static void ClearDataGridViewData(this DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                BindingSource bsUer = (BindingSource)dgv.DataSource;
                DataTable dtUser = (DataTable)bsUer.DataSource;
                dtUser.Clear();
            }
        }
        #endregion

        #region 获取当前行数据并返回表
        /// <summary>
        /// 获取当前行数据并返回表
        /// </summary>
        /// <param name="dgv">网格：其数据源必须是BindingSource</param>
        /// <returns></returns>
        public static DataTable GetCurrentRowTable(DataGridView dgv)
        {
            DataTable dtReturn = null;
            if (dgv.DataSource is BindingSource)
            {
                BindingSource bs = dgv.DataSource as BindingSource;

                if (bs.DataSource is DataTable)
                {
                    DataTable dt = bs.DataSource as DataTable;
                    DataRow dr = ((DataRowView)bs.Current).Row;
                    dtReturn = dt.Clone();
                    dtReturn.Rows.Add(dr);
                }
                else
                {
                    throw new Exception("开发错误！目前只支持DataGridView的数据源为BindingSource类型，且BindingSource类型的数据源为表的场景。");
                }
            }
            else
            {
                throw new Exception("开发错误！目前只支持DataGridView的数据源为BindingSource类型，且BindingSource类型的数据源为表的场景。");
            }
            return dtReturn;
        }
        #endregion

        #region 私有方法

        #region 绑定数据浏览网格列方法
        /// <summary>
        /// 名称：绑定数据浏览网格列方法
        /// 作者：黄国辉
        /// 日期：2013-11-18
        /// 说明：
        ///     设置的Tag值样式："0数据库列名,1列名,2显示文本,3列样式,4是否可见，5列宽度,6对齐方式，7可编辑,8可编辑列最大长度（可省略）"
        ///     示例：dgvMyRole.Tag = "ROLE_ID,USER_ID,角色ID,T,false,0,ML,false;"
        ///         + "ROLE_CODE,ROLE_CODE,角色编码,T,true,100,MC,false;"
        ///         + "ROLE_NAME,ROLE_NAME,角色名称,T,true,100,ML,false";
        ///     其中:
        ///     一、列样式(DataGridViewColumn)：
        ///         T表示DataGridViewTextBoxColumn
        ///         B表示DataGridViewButtonColumn
        ///         C表示DataGridViewComboBoxColumn
        ///         K表示DataGridViewCheckBoxColumn
        ///         L表示DataGridViewLinkColumn
        ///         I表示DataGridViewImageColumn
        ///     二、对齐方式
        ///         以Top、Middle、Button，加Left、Right、Center的首字母組成。还有一个特殊的NotSet
        ///         示例：TL、TR、ML、NS
        ///  注：1、如果数据库列名为空，请直接写为",1列名,2显示文本,3列样式,4是否只读，5列宽度,6对齐方式，7可编辑;"，其他列都不允许为空
        ///      2、最后一列不要中分号了。
        ///      3、要在调用该方法后，才设置网格数据源。
        ///      4、第一列必须为主键，如不显示，设置其宽度为0即可。
        /// </summary>
        /// <param name="dgv">DataGridView数据浏览网格</param>
        /// <param name="isFirstColumnVisable">数据浏览网格第一列是否可见</param>
        private static void BindDataGridViewBeforeSetSource(DataGridView dgv, out bool isFirstColumnVisable)
        {
            try
            {
                //第一列是否可见
                isFirstColumnVisable = false;
                string strEditColunmEndString = "+";

                #region Tag值设置判断
                if (dgv.Tag == null)
                {
                    MsgHelper.ShowErr("绑定失败，没有设置DataGridView的Tag值！");
                    return;
                }
                //设置不自动生成列
                dgv.AutoGenerateColumns = false;

                //清除所有列
                if (dgv.Columns.Count > 0)
                {
                    dgv.Columns.Clear();
                }
                //所有列头中间对齐
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                string strTag = (string)dgv.Tag;
                string[] strOneColumn = strTag.Split(new char[] { ';' });
                if (strOneColumn.Length == 0)
                {
                    MsgHelper.ShowErr("绑定失败，设置DataGridView的Tag值为空！");
                    return;
                }
                #endregion

                for (int i = 0; i < strOneColumn.Length; i++)
                {
                    if (string.IsNullOrEmpty(strOneColumn[i].ToString()))
                    {
                        continue;
                    }
                    string[] strOnePropety = strOneColumn[i].Split(new char[] { ',' });
                    if (strOnePropety.Length < 8)
                    {
                        continue;
                    }
                    string strDataPropertyName = strOnePropety[0];        //来源数据库中列名
                    string strName = strOnePropety[1];                    //绑定对象列名
                    string strHeaderText = strOnePropety[2];              //显示列名
                    string strDataGridViewColumnType = strOnePropety[3];  //列样式
                    string strVisible = strOnePropety[4];                 //是否可见
                    string strWidth = strOnePropety[5];                   //列宽
                    string strStyle = strOnePropety[6];                   //对齐方式
                    string strCanEdit = strOnePropety[7];       //可编辑
                    bool isCanEdit = Convert.ToBoolean(strCanEdit);
                    if (isCanEdit)
                    {
                        strHeaderText += strEditColunmEndString; //可编辑列的标题增加一个+号
                    }

                    if (i == 0)
                    {
                        isFirstColumnVisable = Convert.ToBoolean(strVisible);
                    }

                    #region 设置列内容对齐方式
                    DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                    switch (strStyle)
                    {
                        case "TL":
                            dgvcs.Alignment = DataGridViewContentAlignment.TopLeft;
                            break;
                        case "TC":
                            dgvcs.Alignment = DataGridViewContentAlignment.TopCenter;
                            break;
                        case "TR":
                            dgvcs.Alignment = DataGridViewContentAlignment.TopRight;
                            break;
                        case "ML":
                            dgvcs.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        case "MC":
                            dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "MR":
                            dgvcs.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "BL":
                            dgvcs.Alignment = DataGridViewContentAlignment.BottomLeft;
                            break;
                        case "BC":
                            dgvcs.Alignment = DataGridViewContentAlignment.BottomCenter;
                            break;
                        case "BR":
                            dgvcs.Alignment = DataGridViewContentAlignment.BottomRight;
                            break;
                        case "NS":
                            dgvcs.Alignment = DataGridViewContentAlignment.NotSet;
                            break;
                        default:
                            dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                    }
                    #endregion

                    #region 设置网格样式
                    DataGridViewColumn dgvc;
                    switch (strDataGridViewColumnType)
                    {
                        case "T"://文本框列
                            dgvc = new DataGridViewTextBoxColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            
                            if (strOnePropety.Length >= 9 && isCanEdit)
                            {
                                ((DataGridViewTextBoxColumn)dgvc).MaxInputLength = int.Parse(strOnePropety[8]); //对可编辑列设置最大输入长度
                            }
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            //当是绑定序号列，设置值类型为整型
                            if (strName.Equals("ROWNO", StringComparison.OrdinalIgnoreCase))
                            {
                                dgvc.ValueType = typeof(int);
                            }
                            break;
                        case "B"://按钮列
                            dgvc = new DataGridViewButtonColumn();
                            //dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            ((DataGridViewButtonColumn)dgvc).DefaultCellStyle.NullValue = strDataPropertyName;
                            ((DataGridViewButtonColumn)dgvc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            dgvc.ReadOnly = !isCanEdit;
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            break;
                        case "C"://下拉框列
                            dgvc = new DataGridViewComboBoxColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            break;
                        case "K"://复选框列
                            dgvc = new DataGridViewCheckBoxColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            ((DataGridViewCheckBoxColumn)dgvc).TrueValue = "1";// bool.TrueString;
                            ((DataGridViewCheckBoxColumn)dgvc).FalseValue = "0";// bool.FalseString;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            break;
                        case "L"://链接列
                            dgvc = new DataGridViewLinkColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            break;
                        case "I"://图像列
                            dgvc = new DataGridViewImageColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            break;
                        default: //默认为文本框
                            dgvc = new DataGridViewTextBoxColumn();
                            dgvc.DataPropertyName = strDataPropertyName;
                            dgvc.Name = strName;
                            dgvc.DisplayIndex = i + 1;
                            dgvc.HeaderText = strHeaderText;
                            dgvc.Visible = Convert.ToBoolean(strVisible);
                            dgvc.Width = Convert.ToInt32(strWidth);
                            dgvc.DefaultCellStyle = dgvcs;
                            dgvc.ReadOnly = !isCanEdit;
                            if (strOnePropety.Length >= 9 && isCanEdit)
                            {
                                ((DataGridViewTextBoxColumn)dgvc).MaxInputLength = int.Parse(strOnePropety[8]); //对可编辑列设置最大输入长度
                            }
                            if (isCanEdit)
                            {
                                SetEditGridColumnStyle(dgvc);
                            }
                            //当是绑定序号列，设置值类型为整型
                            if (strName.Equals("ROWNO", StringComparison.OrdinalIgnoreCase))
                            {
                                dgvc.ValueType = typeof(int);
                            }
                            break;
                    }
                    #endregion
                    //数据浏览控制增加列
                    dgv.Columns.Add(dgvc);
                }

                //设置行的交替色
                SetRowColor(dgv);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 判断网格列是否存在私有方法
        [System.Diagnostics.Conditional("DEBUG")]
        private static void CheckGridColNotExists(List<FlexGridColumn> cols, DataTable dt, DataGridView flexGrid)
        {
            if (dt == null)
            {
                throw new Exception(string.Format("网格{0}数据源不能为null！", flexGrid.Name));
            }

            List<string> lst = new List<string>();
            foreach (FlexGridColumn col in cols)
            {
                if (col.IsButtonColumn == false && !dt.Columns.Contains(col.ColumnName))
                {
                    lst.Add(col.ColumnName);
                }
            }

            if (lst.Count > 0)
            {
                throw new Exception(string.Format("网格{0}绑定失败！Tag中以下列[{1}]未在数据源中！", flexGrid.Name, string.Join(StaticConstant.FRA_GRID_COLUMN_SPLIT_STR, lst)));
            }
        }
        #endregion

        #region 生成空表私有方法
        private static DataTable GenerateEmptyTable(IEnumerable<FlexGridColumn> gridColums)
        {
            DataTable dt = new DataTable();
            foreach (FlexGridColumn gridColumn in gridColums)
            {
                DataColumn dc;
                //行号列，设置为自动增长
                if ("ROWNO".Equals(gridColumn.ColumnName, StringComparison.OrdinalIgnoreCase) || gridColumn.IsRowNumColumn)
                {
                    dc = dt.Columns.Add(gridColumn.ColumnName, typeof(int));
                    //设置为自增长：但操作过程中删除行后，行号还是会自动增加
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                }
                else
                {
                    dc = dt.Columns.Add(gridColumn.ColumnName);
                    if (gridColumn.ColumnDisplayType== DataGridViewColumnTypeEnum.CheckBox)
                    {
                        dc.DefaultValue = "1"; //设置默认值
                    }
                }
                dc.Caption = gridColumn.ColumnCaption;
            }

            if (!dt.Columns.Contains("ROWNO"))
            {
                dt.Columns.Add("ROWNO",typeof(int));
            }

            return dt;
        }
        #endregion

        #region 设置编辑网格列的样式
        private static void SetEditGridColumnStyle(DataGridViewColumn dgvc)
        {
            Color cEditColunmHead = Color.LightGreen;
            //设置可编辑列的颜色
            dgvc.DefaultCellStyle.BackColor = cEditColunmHead;
        }
        #endregion

        #endregion

        #region 获取网格后台绑定的表
        /// <summary>
        /// 获取网格后台绑定的表
        /// </summary>
        /// <param name="RelList">列名与控件关系类集合</param>
        /// <returns></returns>
        public static DataTable GetBindingTable(this DataGridView dgv)
        {
            if (dgv.DataSource is BindingSource)
            {
                BindingSource bs = dgv.DataSource as BindingSource;
                if (bs.DataSource is DataTable)
                {
                    return bs.DataSource as DataTable;
                }
                else
                {
                    return null;
                }
            }
            else if (dgv.DataSource is DataTable)
            {
                return dgv.DataSource as DataTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取网格后台绑定的表
        /// <summary>
        /// 获取网格后台绑定的表
        /// </summary>
        /// <param name="RelList">列名与控件关系类集合</param>
        /// <returns></returns>
        public static DataRow GetCurrentRow(this DataGridView dgv)
        {
            try
            {
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource bs = dgv.DataSource as BindingSource;
                    if (bs.DataSource is DataTable)
                    {
                        if (bs.Current == null)
                        {
                            throw new Exception("请选择一条记录！");
                        }
                        return ((DataRowView)bs.Current).Row;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (dgv.DataSource is DataTable)
                {
                    if (dgv.CurrentRow == null)
                    {
                        throw new Exception("请选择一条记录！");
                    }
                    return (dgv.CurrentRow.DataBoundItem as DataRowView).Row;
                }
                else
                {
                    throw new Exception("请保证网格绑定的数据源为DataTable或BindingSource！");
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 是否点击指定的列
        public static bool IsClickColumn(this DataGridView dgv, string strColunmName)
        {
            if (dgv.CurrentCell.ColumnIndex == dgv.Columns[strColunmName].Index)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 查找并定位到指定单元格
        public static bool FindPlaceCell(this DataGridView dgv, string strColunmName, string strFindValue)
        {
            BindingSource bs = dgv.DataSource as BindingSource;
            int iFind = bs.Find(strColunmName, strFindValue);
            if (iFind > -1)
            {
                bs.Position = iFind;
                dgv.CurrentCell = dgv.Rows[iFind].Cells[strColunmName];
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 得到网格样式路径
        public static string GetStylePathString(this DataGridView dgv)
        {
            string fileFullPath = string.Empty;
            string strFormName = string.Empty;
            List<string> formList = new List<string>();

            Control form = dgv.FindForm();

            while (form != null && form is Form && !form.Name.Equals(StaticConstant.FRA_MDIFormName, StringComparison.OrdinalIgnoreCase))
            {
                formList.Add(form.Text);
                strFormName = form.Name;//最后一层的功能名称
                form = form.Parent;
            }

            if (formList.Count > 0)
            {
                formList.Reverse();
                //格式为：功能名+ 窗体名 + 网格名
                string fileName = string.Join(".", formList) + "." + strFormName + "." + dgv.Name + ".style";
                fileFullPath = Path.Combine(WinFormContext.Instance.DataGridTagHistoryPath, fileName);
            }
            else //针对桌面上的网格
            {
                string fileName = "桌面." + dgv.Name + ".style";
                fileFullPath = Path.Combine(WinFormContext.Instance.DataGridTagHistoryPath, fileName);
            }
            return fileFullPath;
        }
        #endregion

        /// <summary>
        /// 全选或全不选某列
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="strColunmName"></param>
        /// <param name="isSelect"></param>
        /// <param name="sSelectColumnName">双击后先中的列</param>
        public static void AllChecked(this DataGridView dgv, string strColunmName, ref bool isSelect)
        {
            foreach (DataGridViewRow item in dgv.Rows)
            {
                string sCelValue = item.Cells[strColunmName].Value.ToString();
                if ("1".Equals(sCelValue) || "0".Equals(sCelValue))
                {
                    item.Cells[strColunmName].Value = isSelect ? "1" : "0";
                }
                else
                {
                    item.Cells[strColunmName].Value = isSelect ? "True" : "False";
                }

            }
            isSelect = !isSelect;
            ChangeCurrentCell(dgv, strColunmName);
        }

        /// <summary>
        /// 改变当前所在网格
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="strColunmName"></param>
        public static void ChangeCurrentCell(this DataGridView dgv, string strColunmName)
        {
            //解决当开始是全部选中，双击后全部取消选 中，但因为焦点没有离开选择列，显示还是选中状态的问题
            if (dgv.CurrentCell.ColumnIndex == dgv.Columns[strColunmName].Index)
            {
                int iNewAdd = dgv.CurrentCell.ColumnIndex + 1;
                int iNewDown = dgv.CurrentCell.ColumnIndex - 1;
                if (dgv.Columns.Count >= iNewAdd)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[iNewAdd];
                    dgv.EndEdit();
                }
                else
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[iNewDown];
                    dgv.EndEdit();
                }
            }
        }

        /// <summary>
        /// 改变当前所在网格
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="iColunmIndex"></param>
        public static void ChangeCurrentCell(this DataGridView dgv, int iColunmIndex)
        {
            //解决当开始是全部选中，双击后全部取消选 中，但因为焦点没有离开选择列，显示还是选中状态的问题
            if (dgv.CurrentCell.ColumnIndex == iColunmIndex)
            {
                int iNewAdd = dgv.CurrentCell.ColumnIndex + 1;
                int iNewDown = dgv.CurrentCell.ColumnIndex - 1;
                if (dgv.Columns.Count >= iNewAdd)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[iNewAdd];
                    dgv.EndEdit();
                }
                else
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[iNewDown];
                    dgv.EndEdit();
                }
            }
        }

        /// <summary>
        /// 查找文本
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sText"></param>
        /// <param name="listColumn"></param>
        /// <param name="isForwardFind">是否身前查找：true是，false否</param>
        public static void SeachText(this DataGridView dgv, string sText, ref DataGridViewFindText dgvText, List<string> listColumn = null, bool isForwardFind = true)
        {
            bool isFind = false;
            if (dgvText == null)
            {
                dgvText = new DataGridViewFindText();
            }
            if (isForwardFind)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    //向前查找
                    if (row.Index >= dgvText.RowIndex)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (listColumn != null && listColumn.Count > 0 && !listColumn.Contains(dgv.Columns[cell.ColumnIndex].Name))
                            {
                                continue;
                            }

                            if (!cell.Visible) continue;

                            if (row.Index == dgvText.RowIndex)
                            {
                                if (cell.ColumnIndex > dgvText.ColumnIndex)
                                {
                                    if (cell.Value != null && cell.Value.ToString().Contains(sText))
                                    {
                                        isFind = SetFindEntityInfo(dgv, dgvText, cell, isForwardFind);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (cell.Value != null && cell.Value.ToString().Contains(sText))
                                {
                                    isFind = SetFindEntityInfo(dgv, dgvText, cell, isForwardFind);
                                    return;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                //向后查找
                if (dgvText.ColumnIndex == -1 || dgvText.RowIndex == -1)
                {
                    dgvText.ColumnIndex = dgv.Columns.Count - 1;
                    dgvText.RowIndex = dgv.Rows.Count - 1;
                }

                for (int i = dgv.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgv.Rows[i];
                    if (row.Index <= dgvText.RowIndex)
                    {
                        for (int j = dgv.Columns.Count - 1; j >= 0; j--)
                        {
                            DataGridViewCell cell = row.Cells[j];

                            if (listColumn != null && listColumn.Count > 0 && !listColumn.Contains(dgv.Columns[cell.ColumnIndex].Name))
                            {
                                continue;
                            }

                            if (!cell.Visible) continue;

                            //如果是当前选中行 只查找前面的列
                            if (dgvText.RowIndex == row.Index)
                            {
                                if (cell.ColumnIndex < dgvText.ColumnIndex)
                                {
                                    if (cell.Value != null && cell.Value.ToString().Contains(sText))
                                    {
                                        isFind = SetFindEntityInfo(dgv, dgvText, cell, isForwardFind);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (cell.Value != null && cell.Value.ToString().Contains(sText))
                                {
                                    isFind = SetFindEntityInfo(dgv, dgvText, cell, isForwardFind);
                                    return;
                                }
                            }

                        }
                    }
                }
            }

            //没找到，重置索引
            if (!isFind)
            {
                dgvText.IsFindEnd = true;
                dgvText.ColumnIndex = -1;
                dgvText.RowIndex = -1;
                dgvText.CurrentIndex = 0;
                dgvText.CurrentMsg = isForwardFind ? "已查找到最后，下次将从头开始查找..." : "已查找到开头，下次将从尾部开始查找...";
            }
        }

        private static bool SetFindEntityInfo(DataGridView dgv, DataGridViewFindText dgvText, DataGridViewCell cell, bool isNext)
        {
            bool isFind;
            cell.Selected = true;
            dgv.CurrentCell = cell;

            dgvText.ColumnIndex = cell.ColumnIndex;
            dgvText.RowIndex = cell.RowIndex;
            if (isNext)
            {
                dgvText.CurrentIndex++;
            }
            else
            {
                dgvText.CurrentIndex--;
            }
            dgvText.CurrentMsg = string.Format("已找到第{0}个", dgvText.CurrentIndex);
            isFind = true;
            return isFind;
        }
    }
}
