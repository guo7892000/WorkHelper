using Breezee.Core.Entity;
using Breezee.Core.Interface;
using System.Data;
using System.Windows.Forms;
using static Breezee.Core.Interface.KeyValueGroupConfig;

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
namespace Breezee.Core.Tool
{
    public static class ComboBoxExtension
    {
        #region 绑定下拉框类
        /// <summary>
        /// 绑定下拉框类
        /// </summary>
        /// <param name="cbbControl">下拉框控件</param>
        /// <param name="dtBind">绑定的表</param>
        /// <param name="strKey">后台绑定值列名</param>
        /// <param name="strText">前台显示列名</param>
        /// <param name="isSelectOnly">是否只能选择</param>
        public static void BindDropDownList(this ComboBox cbbControl, DataTable dtReturn, string strKey, string strText, bool haveEnptyAll, bool isSelectOnly)
        {
            DataTable dtBind = dtReturn.Copy();
            if (haveEnptyAll)
            {
                dtBind.Rows.InsertAt(dtBind.NewRow(), 0);
            }
            cbbControl.ValueMember = strKey;
            cbbControl.DisplayMember = strText;
            cbbControl.DataSource = dtBind;
            if (isSelectOnly)
            {
                cbbControl.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        #endregion

        #region 绑定下拉框类
        /// <summary>
        /// 绑定下拉框类
        /// </summary>
        /// <param name="cbbControl">下拉框控件</param>
        /// <param name="dtBind">绑定的表</param>
        /// <param name="strKey">后台绑定值列名</param>
        /// <param name="strText">前台显示列名</param>
        /// <param name="isSelectOnly">是否只能选择</param>
        public static void BindDropDownList(this ComboBox cbbControl, DataTable dtBind, string strKey, string strText, bool isSelectOnly)
        {
            BindDropDownList(cbbControl, dtBind, strKey, strText, false, isSelectOnly);
        }
        #endregion

        #region 绑定值列表下拉框类
        /// <summary>
        /// 绑定下拉框类
        /// </summary>
        /// <param name="cbbControl">下拉框控件</param>
        /// <param name="strTypeCode">值列表类型编码</param>
        /// <param name="haveEnptyAll">是否含空白的所有</param>
        /// <param name="isSelectOnly">是否只能选择</param>
        public static void BindTypeValueDropDownList(this ComboBox cbbControl, DataTable dtBind, bool haveEnptyAll, bool isSelectOnly)
        {
            BindDropDownList(cbbControl, dtBind, DT_BAS_VALUE.VALUE_CODE, DT_BAS_VALUE.VALUE_NAME, haveEnptyAll, isSelectOnly);
        }
        #endregion

        #region 获取下拉框的当前选择行
        /// <summary>
        /// 获取下拉框的当前选择行
        /// </summary>
        /// <param name="cbbControl">下拉框控件</param>
        public static DataRow GetComboBoxSelectedRow(this ComboBox cbbControl)
        {
            return (cbbControl.SelectedItem as DataRowView).Row;
        }
        #endregion

        #region 获取下拉框选择值
        public static string GetComboBoxSelectedValue(this ComboBox cb)
        {
            if (cb.SelectedValue == null)
            {
                return "";
            }
            else
            {
                return cb.SelectedValue.ToString();
            }
        }
        #endregion

        #region 选择父下拉框变化事件
        public static void SelectParentComboBoxIndexChange(this ComboBox cbbParent, ComboBox cbbChild, DataTable dtFilterTable, string strFliterString, string strValueID, string strText, bool IsHaveEmpty = true, bool IsSelectOnly = true)
        {
            if (cbbParent.SelectedValue != null && !string.IsNullOrEmpty(cbbParent.SelectedValue.ToString()))
            {
                if (dtFilterTable.Select(strFliterString).Length > 0)
                {
                    DataTable dtPlace = dtFilterTable.Select(strFliterString).CopyToDataTable();
                    BindDropDownList(cbbChild, dtPlace, strValueID, strText, IsHaveEmpty, IsSelectOnly);
                }
                else
                {
                    BindDropDownList(cbbChild, dtFilterTable.Clone(), strValueID, strText, IsHaveEmpty, IsSelectOnly);
                }
            }
        }
        #endregion

        #region 选择父下拉框变化事件
        public static void SelectParentTypeValueComboBoxIndexChange(this ComboBox cbbParent, ComboBox cbbChild, DataTable dtFilterTable, string strFliterString, bool IsHaveEmpty = true, bool IsSelectOnly = true)
        {
            SelectParentComboBoxIndexChange(cbbParent, cbbChild, dtFilterTable, strFliterString, DT_BAS_VALUE.VALUE_CODE, DT_BAS_VALUE.VALUE_NAME, IsHaveEmpty, IsSelectOnly);
        }
        #endregion

        public static void BindXmlTypeValueDropDownList(this ComboBox cbbControl, DataTable dtReturn, bool haveEnptyAll, bool isSelectOnly)
        {
            DataTable dtBind = dtReturn.Copy();
            if (haveEnptyAll)
            {
                dtBind.Rows.InsertAt(dtBind.NewRow(), 0);
            }
            cbbControl.ValueMember = KeyValueGroupString.ValueProp.Vid;
            cbbControl.DisplayMember = KeyValueGroupString.ValueProp.Name;
            cbbControl.DataSource = dtBind;
            if (isSelectOnly)
            {
                cbbControl.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
    }
}
