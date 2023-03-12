using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

/*****************************************************************
 * 对象名称：级联菜单帮助类
 * 对象类别：辅助类
 * 创建作者：黄国辉
 * 创建日期：2016-1-5
 * 对象说明：提供网格下拉框联动菜单的绑定。其中网格列名为DataGridViewComboBoxColumn.Name对应的值。
 *  示例：RelateComboBoxHelper.BuildComboBox(DataGridViewName, "级联父列名-如部门", "级联子列名-如部门成员",
 *  所有部门成员数据的DataTable(需要包含[DeptNo列]),"子列名根据父列名筛选内容的筛选字串DeptNo='{0}'");
 * 修改历史：
 *      V1.0 根据他人博客代码来新建 hgh 2016-1-5
 * **************************************************************/

namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// Author:武广敬
    /// Blog:tonyepaper.cnblogs.com
    /// 级联菜单帮助类
    /// </summary>
    public class FlexGridRelateComboBox
    {
        #region 变量
        //未筛选的数据源
        private static BindingSource[] _unFilterBS = new BindingSource[10];
        //筛选后的数据源
        private static BindingSource[] _filterBS = new BindingSource[10];
        //级联主选单
        private static string[] _sourceComboBoxName;
        //级联选单
        private static string[] _filterComboBoxName;
        //Ex:"GoodsNo='{0}'"
        private static string[] _filterExpression; 
        #endregion

        #region 绑定下拉框方法
        /// <summary>
        /// 处理有级联的DataGridViewComboBoxColumn,使从属DataGridViewComboBoxColumn
        /// 可以根据父DataGridViewComboBoxColumn的值改变自身的数据源
        /// </summary>
        /// <param name="dataGridView">需要处理级联的DataGridView</param>
        /// <param name="sourceComboBoxName">级联父DataGridViewComboBoxColumn：即网格中父列名（DataGridViewComboBoxColumn.Name，非DataGridViewComboBoxColumn.DataPropertyName）</param>
        /// <param name="filterComboBoxName">级联子DataGridViewComboBoxColumn：DataGridViewComboBoxColumn.Name</param>
        /// <param name="dataSource">级联子DataGridViewComboBoxColumn的数据源DataTable</param>
        /// <param name="filterExpression">级联子DataGridViewComboBoxColumn根据父DataGridViewComboBoxColumn改变的筛选表达式 Ex:"GoodsNo='{0}'"</param>
        public static void BuildComboBox(DataGridView dataGridView, string sourceComboBoxName, string filterComboBoxName, DataTable dataSource, string filterExpression)
        {
            BuildComboBox(dataGridView, new string[] { sourceComboBoxName }, new string[] { filterComboBoxName }, new DataTable[] { dataSource }, new string[] { filterExpression });
        }
        /// <summary>
        /// 处理有级联的DataGridViewComboBoxColumn,使从属DataGridViewComboBoxColumn(使用下拉选单本身的数据源)
        /// 可以根据父DataGridViewComboBoxColumn的值改变自身的数据源
        /// </summary>
        /// <param name="dataGridView">需要处理级联的DataGridView</param>
        /// <param name="sourceComboBoxName">级联父DataGridViewComboBoxColumn</param>
        /// <param name="filterComboBoxName">级联子DataGridViewComboBoxColumn</param>
        /// <param name="filterExpression">级联子DataGridViewComboBoxColumn根据父DataGridViewComboBoxColumn改变的筛选表达式 Ex:"GoodsNo='{0}'"</param>
        public static void BuildComboBox(DataGridView dataGridView, string sourceComboBoxName, string filterComboBoxName, string filterExpression)
        {
            BuildComboBox(dataGridView, new string[] { sourceComboBoxName }, new string[] { filterComboBoxName }, new DataTable[] { (dataGridView.Columns[filterComboBoxName] as DataGridViewComboBoxColumn).DataSource as DataTable }, new string[] { filterExpression });
        }
        /// <summary>
        /// 处理多个级联的下拉选单
        /// </summary>
        /// <param name="dataGridView">需要处理级联的DataGridView</param>
        /// <param name="sourceComboBoxName">级联父DataGridViewComboBoxColumn</param>
        /// <param name="filterComboBoxName">级联子DataGridViewComboBoxColumn</param>
        /// <param name="dataSource">级联子DataGridViewComboBoxColumn的数据源DataTable</param>
        /// <param name="filterExpression">级联子DataGridViewComboBoxColumn根据父DataGridViewComboBoxColumn改变的筛选表达式 Ex:"GoodsNo='{0}'"</param>
        public static void BuildComboBox(DataGridView dataGridView, string[] sourceComboBoxName, string[] filterComboBoxName, DataTable[] dataSource, string[] filterExpression)
        {
            try
            {
                //单元格开始编辑事件
                dataGridView.CellBeginEdit += new DataGridViewCellCancelEventHandler(dataGridView_CellBeginEdit);
                //单元格结束编辑事件
                dataGridView.CellEndEdit += new DataGridViewCellEventHandler(dataGridView_CellEndEdit);

                for (int i = 0; i < sourceComboBoxName.Length; i++)
                {
                    _unFilterBS[i] = new BindingSource();
                    _unFilterBS[i].DataSource = dataSource[i].Copy(); //注：这里一定要用复制出来的数据源
                    _filterBS[i] = new BindingSource();
                    _filterBS[i].DataSource = dataSource[i].Copy(); //注：这里一定要用复制出来的数据源
                    DataGridViewComboBoxColumn dgvComboxChile = dataGridView.Columns[filterComboBoxName[i]] as DataGridViewComboBoxColumn;
                    dgvComboxChile.DataSource = _unFilterBS[i];
                    //因为联动导致了子下拉框没有选择已修改的值，所以这里增加赋值 hgh 2016-4-21
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        DataTable dtSource = (DataTable)((BindingSource)dataGridView.DataSource).DataSource;
                        string strSourceColumnName = dgvComboxChile.DataPropertyName + "1";
                        if (dtSource.Columns.Contains(strSourceColumnName))
                        {
                            dataGridView.Rows[j].Cells[filterComboBoxName[i]].Value = dtSource.Rows[j][strSourceColumnName].ToString();
                        }
                        
                    }
                }
                _filterComboBoxName = filterComboBoxName;
                _sourceComboBoxName = sourceComboBoxName;
                _filterExpression = filterExpression;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 处理多个级联的下拉选单(使用下拉选单本身的数据源)
        /// </summary>
        /// <param name="dataGridView">需要处理级联的DataGridView</param>
        /// <param name="sourceComboBoxName">级联父DataGridViewComboBoxColumn</param>
        /// <param name="filterComboBoxName">级联子DataGridViewComboBoxColumn</param>
        /// <param name="filterExpression">级联子DataGridViewComboBoxColumn根据父DataGridViewComboBoxColumn改变的筛选表达式 Ex:"GoodsNo='{0}'"</param>
        public static void BuildComboBox(DataGridView dataGridView, string[] sourceComboBoxName, string[] filterComboBoxName, string[] filterExpression)
        {
            DataTable[] datasource = new DataTable[filterComboBoxName.Length];
            for (int i = 0; i < sourceComboBoxName.Length; i++)
            {
                datasource[i] = (dataGridView.Columns[filterComboBoxName[i]] as DataGridViewComboBoxColumn).DataSource as DataTable;
            }
            BuildComboBox(dataGridView, sourceComboBoxName, filterComboBoxName, datasource, filterExpression);
        } 
        #endregion

        #region 网格开始编辑事件
        static void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView datagridView = sender as DataGridView;

            try
            {
                for (int i = 0; i < _sourceComboBoxName.Length; i++)
                {
                    if (e.ColumnIndex == datagridView.Columns[_filterComboBoxName[i]].Index) //选择的是要过滤的列
                    {
                        DataGridViewComboBoxCell filterComboBox = datagridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                        filterComboBox.DataSource = _filterBS[i];
                        DataGridViewComboBoxCell sourceComboBox = datagridView.Rows[e.RowIndex].Cells[_sourceComboBoxName[i]] as DataGridViewComboBoxCell;
                        _filterBS[i].Filter = string.Format(_filterExpression[i], sourceComboBox.FormattedValue);
                    }
                    else if (e.ColumnIndex == datagridView.Columns[_sourceComboBoxName[i]].Index) //选择的是要源列
                    {
                        //清空过滤列的当前值
                        datagridView.Rows[e.RowIndex].Cells[_filterComboBoxName[i]].Value = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 网格结束编辑事件
        static void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView datagridView = sender as DataGridView;
            try
            {
                for (int i = 0; i < _sourceComboBoxName.Length; i++)
                {
                    if (e.ColumnIndex == datagridView.Columns[_filterComboBoxName[i]].Index)
                    {
                        DataGridViewComboBoxCell filterComboBox = datagridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                        filterComboBox.DataSource = _unFilterBS[i];
                        _filterBS[i].RemoveFilter();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

    }
}