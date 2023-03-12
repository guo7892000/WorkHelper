using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Entity;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Collections;

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
    /// List<string>扩展
    /// </summary>
    public static class TreeViewExtension
    {
        #region 根据文本查找树节点
        /// <summary>
        /// 根据文本查找树节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="sSeachText"></param>
        /// <param name="iLevel">搜索层级：当为-1时表示所有层级都搜索。如果要搜索时，其值必须大小0</param>
        /// <returns></returns>
        public static TreeNode FindNodeByText(this TreeView tv, string sSeachText, int iLevel = -1)
        {
            TreeNode tnFind = null;
            foreach (TreeNode xn in tv.Nodes)
            {
                tnFind = FindNodeByTextInner(xn, sSeachText, iLevel);
                if (tnFind != null)
                {
                    break;
                }
            }
            return tnFind;
        }

        private static TreeNode FindNodeByTextInner(TreeNode tnParent, string strText, int iLevel)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strText) return tnParent;

            if (iLevel > 0)
            {
                if (iLevel == 1)
                {
                    return null;
                }
                iLevel--;
            }
            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNodeByTextInner(tn, strText, iLevel);
                if (tnRet != null) break;
            }
            return tnRet;
        }
        #endregion

        #region 查找所有叶节点
        public static IList<TreeNode> GetAllLastNode(this TreeView tv)
        {
            IList<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode xn in tv.Nodes)
            {
                GetAllLastNodeInner(xn, list);
            }
            return list;
        }

        private static void GetAllLastNodeInner(TreeNode tnParent, IList<TreeNode> list)
        {
            if (tnParent.Nodes == null || tnParent.Nodes.Count == 0)
            {
                list.Add(tnParent);
            }

            foreach (TreeNode xn in tnParent.Nodes)
            {
                GetAllLastNodeInner(xn, list);
            }
        }
        #endregion

        #region 生成树公用方法
        /// <summary>
        /// 名称：生成树公用方法
        /// 作者：黄国辉
        /// 日期：2013-11-23
        /// </summary>
        /// <param name="tv">TreeView对象</param>
        /// <param name="dtTreeTable">生成树的数据表</param>
        /// <param name="strIDColumnName">ID列名</param>
        /// <param name="strParentIDColumnName">父节点列名</param>
        /// <param name="strDisplayColumnName">显示节点名称列名</param>
        /// <param name="strSortIDColumnName">正序排列的节点列名</param>
        /// <param name="strCheckedColumnName">选中列名</param>
        /// <param name="strParentFlitSql">根节点筛选SQL</param>
        public static void CreateTree(this TreeView tv, DataTable dtTreeTable, string strIDColumnName, string strParentIDColumnName, string strDisplayColumnName, string strSortIDColumnName, string strCheckedColumnName, string strRootFilterSql)
        {
            //先清空
            tv.Nodes.Clear();
            //过滤和排序字段
            string filter;
            string sort;
            //第一个节点处理：根节点其父节点（strParentIDColumnName）必须为空或-1
            DataView firstTreeNode = dtTreeTable.DefaultView;
            if (string.IsNullOrEmpty(strRootFilterSql))
            {
                filter = strParentIDColumnName + " is null or " + strParentIDColumnName + " ='-1'";
            }
            else
            {
                filter = strRootFilterSql;
            }
            firstTreeNode.RowFilter = filter;
            sort = strSortIDColumnName + " ASC";//按排序号排序
            firstTreeNode.Sort = sort;
            //针对第一个节点循环
            foreach (DataRowView firstDataRow in firstTreeNode)
            {
                //系统节点
                TreeNode trFirst = new TreeNode();
                string strDisplayName = firstDataRow[strDisplayColumnName].ToString();
                string strKeyID = firstDataRow[strIDColumnName].ToString();
                trFirst.Text = strDisplayName;
                trFirst.ToolTipText = strDisplayName;
                trFirst.Name = strKeyID;//表示一级节点：系统
                trFirst.Tag = firstDataRow.Row; //Tag为行数据
                if (!string.IsNullOrEmpty(strCheckedColumnName))
                {
                    //设置为选中状态
                    trFirst.Checked = firstDataRow[strCheckedColumnName].ToString() == "1" ? true : false;
                }
                //第二个节点
                DataView twoDataView = dtTreeTable.DefaultView;
                filter = strParentIDColumnName + "='" + strKeyID + "'";//筛选第二个节点
                firstTreeNode.RowFilter = filter;
                twoDataView.RowFilter = filter;
                sort = strSortIDColumnName + " ASC";//按排序号排序
                twoDataView.Sort = sort;
                foreach (DataRowView twoDataRow in twoDataView)
                {
                    TreeNode tnTwo = new TreeNode();
                    string strTowName = twoDataRow[strDisplayColumnName].ToString();
                    string strTowID = twoDataRow[strIDColumnName].ToString();
                    tnTwo.Text = strTowName;
                    tnTwo.Tag = twoDataRow.Row;
                    tnTwo.ToolTipText = strTowName;
                    tnTwo.Name = strTowID;

                    DataView dvChild = dtTreeTable.DefaultView;
                    //调用增加菜单树方法
                    AddChirldTree(tnTwo, dvChild, strTowID, strIDColumnName, strParentIDColumnName, strDisplayColumnName, strSortIDColumnName, strCheckedColumnName);
                    if (!string.IsNullOrEmpty(strCheckedColumnName))
                    {
                        //设置为选中状态
                        tnTwo.Checked = twoDataRow[strCheckedColumnName].ToString() == "1" ? true : false;
                    }
                    //增加一级菜单
                    trFirst.Nodes.Add(tnTwo);
                }
                //根节点增加系统
                tv.Nodes.Add(trFirst);
            }
        }

        public static void CreateTree(this TreeView tv, DataTable dtTreeTable, string strIDColumnName, string strParentIDColumnName, string strDisplayColumnName, string strSortIDColumnName)
        {
            CreateTree(tv, dtTreeTable, strIDColumnName, strParentIDColumnName, strDisplayColumnName, strSortIDColumnName, null, null);
        }

        public static void CreateTree(this TreeView tv, DataTable dtTreeTable, string strIDColumnName, string strParentIDColumnName, string strDisplayColumnName, string strSortIDColumnName, string strCheckedColumnName)
        {
            CreateTree(tv, dtTreeTable, strIDColumnName, strParentIDColumnName, strDisplayColumnName, strSortIDColumnName, strCheckedColumnName, null);
        }
        #endregion

        #region 遍历增加子树节点方法
        /// <summary>
        /// 名称：遍历增加子树节点方法
        /// 作者：黄国辉
        /// 日期：2013-11-23
        /// </summary>
        /// <param name="tnParent">TreeNode父对象</param>
        /// <param name="dvChild">字节点的来源表，注要包括递归下的所有节点</param>
        /// <param name="strParentID">父节点值</param>
        /// <param name="strIDColumnName">ID列名</param>
        /// <param name="strParentIDColumnName">父节点列名</param>
        /// <param name="strDisplayColumnName">显示节点名称列名</param>
        /// <param name="strSortIDColumnName">正序排列的节点列名</param>
        private static void AddChirldTree(TreeNode tnParent, DataView dvChild, string strParentID, string strIDColumnName,
            string strParentIDColumnName, string strDisplayColumnName, string strSortIDColumnName, string strCheckedColumnName = null)
        {
            string filter = strParentIDColumnName + "='" + strParentID + "'";
            dvChild.RowFilter = filter;
            string sort = strSortIDColumnName + " ASC";//按排序号排序
            dvChild.Sort = sort;
            foreach (DataRowView childDataRow in dvChild)
            {
                TreeNode trChild = new TreeNode();
                string strChildName = childDataRow[strDisplayColumnName].ToString();
                string strChildID = childDataRow[strIDColumnName].ToString();
                trChild.Text = strChildName;
                trChild.Tag = childDataRow.Row;
                trChild.ToolTipText = strChildName;
                trChild.Name = strChildID;
                if (!string.IsNullOrEmpty(strCheckedColumnName))
                {
                    //设置为选中状态
                    trChild.Checked = childDataRow[strCheckedColumnName].ToString() == "1" ? true : false;
                }
                DataView formChirldChirld = dvChild;
                filter = strParentIDColumnName + "='" + strParentID + "'";
                formChirldChirld.RowFilter = filter;
                sort = strSortIDColumnName + " ASC";//按排序号排序
                formChirldChirld.Sort = sort;
                if (formChirldChirld.Count > 0)
                {
                    //递归调用增加子菜单
                    AddChirldTree(trChild, dvChild, strChildID, strIDColumnName, strParentIDColumnName, strDisplayColumnName,
                        strSortIDColumnName, strCheckedColumnName);
                }
                tnParent.Nodes.Add(trChild);
            }
        }
        #endregion

        
    }
}
