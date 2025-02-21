using Breezee.Core.Tool;
using Breezee.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Interface;

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
    /// IDictionary扩展类
    /// </summary>
    public static class IDictionaryExtension
    {
        #region 安全获取字典集合
        public static IDictionary<string, object> SafeGetDictionary(this IDictionary<string, object> dicObj, bool IsSendMsg = false)
        {
            if (dicObj.ContainsKey(StaticConstant.FRA_RETURN_FLAG))
            {
                if (dicObj[StaticConstant.FRA_RETURN_FLAG].ToString() == "0")
                {
                    throw new Exception(dicObj[StaticConstant.FRA_USER_MSG].ToString());
                }
                else
                {
                    if (IsSendMsg)
                    {
                        MsgHelper.ShowInfo(dicObj[StaticConstant.FRA_USER_MSG].ToString());
                    }
                }
            }
            return dicObj;
        }
        #endregion

        #region 安全获取字段中的结果表
        /// <summary>
        /// 安全获取字段中的结果表
        /// </summary>
        /// <param name="dicObj"></param>
        /// <param name="IsThrowErr"></param>
        /// <returns></returns>
        public static DataTable SafeGetDictionaryTable(this IDictionary<string, object> dicObj, bool IsSendMsg = false)
        {
            DataTable dtReturn = null;
            if (dicObj.ContainsKey(StaticConstant.FRA_RETURN_FLAG))
            {
                if (dicObj[StaticConstant.FRA_RETURN_FLAG].ToString() == "0")
                {
                    throw new Exception(dicObj[StaticConstant.FRA_USER_MSG].ToString());
                }
                else
                {
                    dtReturn = dicObj[StaticConstant.FRA_QUERY_RESULT] as DataTable;
                    if (IsSendMsg)
                    {
                        MsgHelper.ShowInfo(dicObj[StaticConstant.FRA_USER_MSG].ToString());
                    }
                }
            }
            return dtReturn;
        }
        #endregion

        #region 重置控件值
        /// <summary>
        /// 重置控件值
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">键为列名，值为控件名</param>
        public static void CleanControlValue(this IDictionary<string, Control> dicCtrl)
        {
            IEnumerator itor = dicCtrl.GetEnumerator();
            while (itor.MoveNext())
            {
                string sKey = ((KeyValuePair<string, Control>)itor.Current).Key;
                Control ctrl = dicCtrl[sKey];
                ctrl.ResetControl();
            }
        }

        /// <summary>
        /// 重置控件值
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">键为列名，值为控件名</param>
        public static void CleanControlValue(this List<DBColumnControlRelation> dicCtrl)
        {
            foreach (DBColumnControlRelation col in dicCtrl)
            {
                if (col.ReadSaveEnum != DBColumnControlReadSaveEnum.ReadOnly && !string.IsNullOrEmpty(col.SaveColumnName))
                {
                    col.ControlName.ResetControl();
                }
            }
        }

        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="ctrl">需要重置的控件</param>
        public static void ResetControl(this Control ctrl)
        {
            if (ctrl == null)
            {
                return;
            }
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).Text = "";
            }
            else if (ctrl is ComboBox)
            {
                ComboBox cbb = (ComboBox)ctrl;
                if (cbb.Items.Count == 0)
                {
                    cbb.SelectedIndex = -1;
                }
                else
                {
                    cbb.SelectedIndex = 0;
                }
            }
            else if (ctrl is DateTimePicker)
            {
                ((DateTimePicker)ctrl).Value = DateTime.Now;
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Checked = false;
            }
            else if (ctrl is RichTextBox)
            {
                ((RichTextBox)ctrl).Text = "";
            }
            if (ctrl.HasChildren)
            {
                ResetControls(ctrl.Controls);
            }
        }

        private static void ResetControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.ResetControl();
            }
        }
        #endregion

        #region 设置控件只读
        /// <summary>
        /// 设置控件只读
        /// </summary>
        /// <param name="ctrl">设置控件只读</param>
        public static void SetControlReadOnly(this Control ctrl, bool IsOnlyRead = true)
        {
            if (ctrl == null)
            {
                return;
            }
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).ReadOnly = IsOnlyRead;
            }
            else if (ctrl is RichTextBox)
            {
                ((RichTextBox)ctrl).ReadOnly = IsOnlyRead;
            }
            else if (ctrl is ComboBox)
            {
                ((ComboBox)ctrl).Enabled = !IsOnlyRead;
            }
            else if (ctrl is DateTimePicker)
            {
                ((DateTimePicker)ctrl).Enabled = !IsOnlyRead;
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Enabled = !IsOnlyRead;
            }

            else if (ctrl is Button)
            {
                ((Button)ctrl).Enabled = !IsOnlyRead;
            }
            else if (ctrl is DataGridView)
            {
                ((DataGridView)ctrl).ReadOnly = IsOnlyRead;
            }
            else if (ctrl is ToolStrip)
            {
                ctrl.Enabled = !IsOnlyRead;
            }
            //如有子控件，则继续
            if (ctrl.HasChildren)
            {
                SetControlsReadOnly(ctrl.Controls, IsOnlyRead);
            }
        }

        private static void SetControlsReadOnly(Control.ControlCollection controls, bool IsOnlyRead = true)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.SetControlReadOnly(IsOnlyRead);
            }
        }

        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="controls">可以是一个或者多个，还可以是容器控件</param>
        public static void SetControlsReadOnly(bool IsOnlyRead, params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.SetControlReadOnly(IsOnlyRead);
            }
        }
        #endregion

        #region 将表的第一行数据赋给控件
        /// <summary>
        /// 将表的第一行数据赋给控件
        /// </summary>
        /// <param name="dicCtrl">列名与控件关系表</param>
        /// <param name="dtOneRow">来源表</param>
        public static void SetControlValue(this IDictionary<string, Control> dicCtrl, DataTable dtOneRow)
        {
            dicCtrl.SetControlValue(dtOneRow.Rows[0]);
        }
        #endregion

        #region 将网格行数据赋给控件
        /// <summary>
        /// 将网格行数据赋给控件
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">列名与控件关系表</param>
        /// <param name="dtOneRow">来源表</param>
        public static void SetControlValue(this IDictionary<string, Control> dicCtrl, DataGridViewRow dtOneRow)
        {
            try
            {
                IEnumerator itor = dicCtrl.GetEnumerator();
                if (dtOneRow.Cells.Count == 0)
                {
                    throw new Exception("传入行没有数据！");
                }
                while (itor.MoveNext())
                {
                    string sKey = ((KeyValuePair<string, Control>)itor.Current).Key;
                    Control ctrl = dicCtrl[sKey];
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = dtOneRow.Cells[sKey].Value.ToString();
                    }
                    else if (ctrl is ComboBox)
                    {
                        ((ComboBox)ctrl).SelectedValue = dtOneRow.Cells[sKey].Value.ToString();
                    }
                    else if (ctrl is DateTimePicker)
                    {
                        ((DateTimePicker)ctrl).Value = Convert.ToDateTime(dtOneRow.Cells[sKey].Value.ToString());
                    }
                    else if (ctrl is CheckBox)
                    {
                        ((CheckBox)ctrl).Checked = Convert.ToBoolean(dtOneRow.Cells[sKey].Value.ToString());
                    }
                    else if (ctrl is RichTextBox)
                    {
                        ((RichTextBox)ctrl).Text = dtOneRow.Cells[sKey].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //ShowInfo(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region 将表行数据赋给控件
        /// <summary>
        /// 将表行数据赋给控件
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">列名与控件关系表</param>
        /// <param name="dtOneRow">数据行</param>
        /// <param name="IsIgnoreNotExistsColumn">是否忽略列不存在错误</param>
        public static void SetControlValue(this IDictionary<string, Control> dicCtrl, DataRow dtOneRow, bool IsIgnoreNotExistsColumn = false)
        {
            try
            {
                IEnumerator itor = dicCtrl.GetEnumerator();
                while (itor.MoveNext())
                {
                    string sKey = ((KeyValuePair<string, Control>)itor.Current).Key;
                    Control ctrl = dicCtrl[sKey];
                    if (IsIgnoreNotExistsColumn && !dtOneRow.ContainsColumn(sKey))
                    {
                        continue;//继续下一个赋值
                    }
                    string sValue = dtOneRow[sKey].ToString();
                    if (ctrl is TextBox) //文本框
                    {
                        ((TextBox)ctrl).Text = sValue;
                    }
                    else if (ctrl is ComboBox) //下拉框
                    {
                        if (!string.IsNullOrEmpty(sValue))
                        {
                            ((ComboBox)ctrl).SelectedValue = sValue;
                        }
                    }
                    else if (ctrl is DateTimePicker) //时间控件
                    {
                        if (string.IsNullOrEmpty(sValue)) //时间为空时
                        {
                            ((DateTimePicker)ctrl).ShowCheckBox = true; //显示复选框
                            ((DateTimePicker)ctrl).Checked = false; //复选框为不选中状态
                        }
                        else
                        {
                            ((DateTimePicker)ctrl).Value = Convert.ToDateTime(sValue);
                        }
                    }
                    else if (ctrl is CheckBox)//复选框
                    {
                        if (sValue == "1")
                        {
                            ((CheckBox)ctrl).Checked = true;
                        }
                        else if (sValue == "0" || string.IsNullOrEmpty(sValue))
                        {
                            ((CheckBox)ctrl).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)ctrl).Checked = Convert.ToBoolean(sValue);
                        }
                    }
                    else if (ctrl is RichTextBox) //多文本框
                    {
                        ((RichTextBox)ctrl).Text = sValue;
                    }
                    else
                    {
                        throw new Exception("错误，暂未增加该类型[" + ctrl.GetType() + "]控件的赋值处理。请联系系统管理员！");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将表行数据赋给控件
        /// <summary>
        /// 将表行数据赋给控件
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">列名与控件关系表</param>
        /// <param name="dtOneRow">数据行</param>
        /// <param name="IsIgnoreNotExistsColumn">是否忽略列不存在错误</param>
        public static void SetControlValue(this List<DBColumnControlRelation> listDBColumnControlRelation, DataRow dtOneRow, bool IsIgnoreNotExistsColumn = false)
        {
            try
            {
                foreach (DBColumnControlRelation kv in listDBColumnControlRelation)
                {
                    string sKey = kv.ReadColumnName;
                    int iDecimalDigits = kv.DecimalDigits;//小数位数
                    Control ctrl = kv.ControlName;
                    if (IsIgnoreNotExistsColumn && !dtOneRow.ContainsColumn(sKey) || kv.ReadSaveEnum == DBColumnControlReadSaveEnum.SaveOnly)
                    {
                        continue;//继续下一个赋值
                    }
                    string sValue = dtOneRow[sKey].ToString();
                    if (ctrl is TextBox) //文本框
                    {
                        if (iDecimalDigits > -1)
                        {
                            decimal dNew;
                            if (decimal.TryParse(sValue.ToString(), out dNew))
                            {
                                //转换为指定位数的小数
                                ((TextBox)ctrl).Text = Math.Round(dNew, iDecimalDigits).ToString();
                            }
                            else
                            {
                                ((TextBox)ctrl).Text = sValue.ToString();
                            }
                        }
                        else
                        {
                            ((TextBox)ctrl).Text = sValue;
                        }
                    }
                    else if (ctrl is ComboBox) //下拉框
                    {
                        if (!string.IsNullOrEmpty(sValue))
                        {
                            ((ComboBox)ctrl).SelectedValue = sValue;
                        }
                    }
                    else if (ctrl is DateTimePicker) //时间控件
                    {
                        if (string.IsNullOrEmpty(sValue)) //时间为空时
                        {
                            ((DateTimePicker)ctrl).ShowCheckBox = true; //显示复选框
                            ((DateTimePicker)ctrl).Checked = false; //复选框为不选中状态
                        }
                        else
                        {
                            ((DateTimePicker)ctrl).Value = Convert.ToDateTime(sValue);
                        }
                    }
                    else if (ctrl is CheckBox)//复选框
                    {
                        if (sValue == "1")
                        {
                            ((CheckBox)ctrl).Checked = true;
                        }
                        else if (sValue == "0" || string.IsNullOrEmpty(sValue))
                        {
                            ((CheckBox)ctrl).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)ctrl).Checked = Convert.ToBoolean(sValue);
                        }
                    }
                    else if (ctrl is RichTextBox) //多文本框
                    {
                        ((RichTextBox)ctrl).Text = sValue;
                    }
                    else if (ctrl is NumericUpDown) //多文本框
                    {
                        ((NumericUpDown)ctrl).Value = string.IsNullOrEmpty(sValue) ? 0 : decimal.Parse(sValue);
                    }
                    else
                    {
                        throw new Exception("错误，暂未增加该类型[" + ctrl.GetType() + "]控件的赋值处理。请联系系统管理员！");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将控件值赋给表的第一行数据
        /// <summary>
        /// 将控件值赋给表的第一行数据
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">列名与控件关系表</param>
        /// <param name="dtOneRow">写入值的表</param>
        /// <param name="isAdd">true新增，false修改</param>
        public static void GetControlValue(this IDictionary<string, Control> dicCtrl, DataTable dtOneRow, bool isAdd)
        {
            try
            {
                if (dtOneRow.Rows.Count == 0)
                {
                    dtOneRow.Rows.Add(dtOneRow.NewRow());
                }
                foreach (KeyValuePair<string, Control> kv in dicCtrl)
                {
                    string sKey = kv.Key;
                    Control ctrl = kv.Value;
                    if (ctrl is TextBox)
                    {
                        string strValue = ((TextBox)ctrl).Text.Trim();
                        if (string.IsNullOrEmpty(strValue))
                        {
                            dtOneRow.Rows[0][sKey] = DBNull.Value;
                        }
                        else
                        {
                            dtOneRow.Rows[0][sKey] = strValue;
                        }
                    }
                    else if (ctrl is ComboBox)
                    {
                        ComboBox cbb = (ComboBox)ctrl;
                        if (cbb.SelectedValue == null || string.IsNullOrEmpty(cbb.SelectedValue.ToString()))
                        {
                            dtOneRow.Rows[0][sKey] = DBNull.Value;
                        }
                        else
                        {
                            dtOneRow.Rows[0][sKey] = cbb.SelectedValue.ToString();
                        }
                    }
                    else if (ctrl is DateTimePicker)
                    {
                        if (((DateTimePicker)ctrl).ShowCheckBox == true && ((DateTimePicker)ctrl).Checked == false)
                        {
                            dtOneRow.Rows[0][sKey] = DBNull.Value;
                        }
                        else
                        {
                            dtOneRow.Rows[0][sKey] = ((DateTimePicker)ctrl).Value.ToString();
                        }
                    }
                    else if (ctrl is CheckBox)
                    {
                        dtOneRow.Rows[0][sKey] = ((CheckBox)ctrl).Checked == true ? "1" : "0";
                    }
                    else if (ctrl is RichTextBox)
                    {
                        string strValue = ((RichTextBox)ctrl).Text;
                        if (string.IsNullOrEmpty(strValue))
                        {
                            dtOneRow.Rows[0][sKey] = DBNull.Value;
                        }
                        else
                        {
                            dtOneRow.Rows[0][sKey] = strValue;
                        }
                    }
                }
                dtOneRow.Rows[0].AcceptChanges();
                if (isAdd)
                {
                    //设置行状态为新增
                    dtOneRow.Rows[0].SetAdded();
                }
                else
                {
                    //设置行状态为修改
                    dtOneRow.Rows[0].SetModified();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将控件值赋给表的第一行数据
        /// <summary>
        /// 将控件值赋给表的第一行数据
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox
        /// </summary>
        /// <param name="listDBColumnControlRelation">列名与控件关系集合</param>
        /// <param name="dtOneRow">写入值的表</param>
        /// <param name="isAdd">true新增，false修改</param>
        public static void GetControlValue(this List<DBColumnControlRelation> listDBColumnControlRelation, DataTable dtOneRow, bool isAdd)
        {
            try
            {
                DataRow drEdit;
                if (dtOneRow.Rows.Count == 0)
                {
                    drEdit = dtOneRow.NewRow();
                    dtOneRow.Rows.Add(drEdit);
                }
                else
                {
                    drEdit = dtOneRow.Rows[0];
                }
                //调用赋值行方法
                GetControlValue(listDBColumnControlRelation, drEdit, isAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将控件值赋给行数据
        /// <summary>
        /// 将控件值赋给行数据
        /// 说明：目前只支持文本框、下拉框、时间、复选框、RichTextBox、NumericUpDown
        /// 注：针对新增，会自动将drEdit加入表中
        /// </summary>
        /// <param name="listDBColumnControlRelation">列名与控件关系集合</param>
        /// <param name="dtOneRow">写入值的表</param>
        /// <param name="isAdd">true新增，false修改</param>
        public static void GetControlValue(this List<DBColumnControlRelation> listDBColumnControlRelation, DataRow drEdit, bool isAdd)
        {
            try
            {
                foreach (DBColumnControlRelation kv in listDBColumnControlRelation)
                {
                    if (kv.ReadSaveEnum == DBColumnControlReadSaveEnum.ReadOnly) //只读的
                    {
                        continue;//继续下一条记录
                    }
                    string strSaveColumnName = kv.SaveColumnName;
                    Control ctrl = kv.ControlName;

                    if (ctrl is TextBox)
                    {
                        string strValue = ((TextBox)ctrl).Text.Trim();
                        if (string.IsNullOrEmpty(strValue))
                        {
                            drEdit[strSaveColumnName] = DBNull.Value;
                        }
                        else
                        {
                            drEdit[strSaveColumnName] = strValue;
                        }
                    }
                    else if (ctrl is ComboBox)
                    {
                        #region 下拉框类型的处理
                        ComboBox cbb = (ComboBox)ctrl;
                        if (kv.ComboBoxSaveEnum == DBColumnComboBoxSaveEnum.BindingSourceTableColumnName)//取后台绑定的指定列名值
                        {
                            if (string.IsNullOrEmpty(kv.ComboBoxSaveSourceTableColumnName))
                            {
                                throw new Exception("获取下拉框后台绑定值错误，未传入后台绑定的列名！");
                            }
                            if (cbb.DataSource is BindingSource)
                            {
                                DataRow dr = (DataRow)cbb.SelectedValue;
                                drEdit[strSaveColumnName] = dr[kv.ComboBoxSaveSourceTableColumnName];
                            }
                            if (cbb.DataSource is DataTable)
                            {
                                DataRow dr = (DataRow)cbb.SelectedValue;
                                drEdit[strSaveColumnName] = dr[kv.ComboBoxSaveSourceTableColumnName];
                            }
                        }
                        else if (kv.ComboBoxSaveEnum == DBColumnComboBoxSaveEnum.Text)//取文本
                        {
                            drEdit[strSaveColumnName] = cbb.Text.Trim();
                        }
                        else //取后台绑定值
                        {
                            if (cbb.SelectedValue == null || string.IsNullOrEmpty(cbb.SelectedValue.ToString()))
                            {
                                drEdit[strSaveColumnName] = DBNull.Value;
                            }
                            else
                            {
                                drEdit[strSaveColumnName] = cbb.SelectedValue.ToString();
                            }
                        }
                        #endregion
                    }
                    else if (ctrl is DateTimePicker)
                    {
                        drEdit[strSaveColumnName] = ((DateTimePicker)ctrl).Value.ToString();
                    }
                    else if (ctrl is CheckBox)
                    {
                        drEdit[strSaveColumnName] = ((CheckBox)ctrl).Checked == true ? "1" : "0";
                    }
                    else if (ctrl is RichTextBox)
                    {
                        string strValue = ((RichTextBox)ctrl).Text;
                        if (string.IsNullOrEmpty(strValue))
                        {
                            drEdit[strSaveColumnName] = DBNull.Value;
                        }
                        else
                        {
                            drEdit[strSaveColumnName] = strValue;
                        }
                    }
                    else if (ctrl is NumericUpDown)
                    {
                        drEdit[strSaveColumnName] = ((NumericUpDown)ctrl).Value.ToString();
                    }
                    else
                    {
                        throw new Exception("错误，暂未增加该类型[" + ctrl.GetType() + "]控件的取值处理。请联系系统管理员！");
                    }
                }

                if (isAdd)
                {
                    if (drEdit.Table.Rows.Count == 0)
                    {
                        drEdit.Table.Rows.Add(drEdit); // 只有源表为0时，才加入
                    }
                    drEdit.AcceptChanges();
                    //设置行状态为新增
                    drEdit.SetAdded();
                }
                else
                {
                    drEdit.AcceptChanges();
                    //设置行状态为修改
                    drEdit.SetModified();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 控件非空判断
        /// <summary>
        /// 控件非空判断
        /// 说明：目前只支持文本框、下拉框、时间、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">键为UI文本名称，值为控件名</param>
        /// <param name="isClewAll">是否提示所有：true是，false否</param>
        public static string JudgeNotNull(this IDictionary<string, Control> dicCtrl, bool isClewAll)
        {
            StringBuilder sbAll = new StringBuilder();
            StringBuilder sbOne = new StringBuilder();
            sbAll.Append("以下控件不能为空：\n");
            bool iHaveEnptyItem = false;
            #region 控件空判断与返回值构造
            IEnumerator itor = dicCtrl.GetEnumerator();
            int i = 1;
            while (itor.MoveNext())
            {
                string sKey = ((KeyValuePair<string, Control>)itor.Current).Key;
                Control ctrl = dicCtrl[sKey];
                if (string.IsNullOrEmpty(sKey))
                {
                    continue;
                }
                bool IsBreak = JudgeControlNotNull(isClewAll, sbAll, sbOne, ref iHaveEnptyItem, ref i, sKey, ctrl);
                if (IsBreak)
                {
                    break;
                }
            }
            #endregion

            #region 返回值处理
            if (iHaveEnptyItem)
            {
                if (!isClewAll)
                {
                    return sbOne.ToString();
                }
                else
                {
                    return sbAll.ToString();
                }
            }
            return null;
            #endregion
        }
        #endregion

        #region 控件非空判断
        /// <summary>
        /// 控件非空判断
        /// 说明：目前只支持文本框、下拉框、时间、RichTextBox
        /// </summary>
        /// <param name="dicCtrl">键为UI文本名称，值为控件名</param>
        /// <param name="isClewAll">是否提示所有：true是，false否</param>
        public static string JudgeNotNull(this List<DBColumnControlRelation> listDBColumnControlRelation, bool isClewAll)
        {
            StringBuilder sbAll = new StringBuilder();
            StringBuilder sbOne = new StringBuilder();
            sbAll.Append("以下控件不能为空：\n");
            bool iHaveEnptyItem = false;
            #region 控件空判断与返回值构造
            int i = 1;
            foreach (DBColumnControlRelation db in listDBColumnControlRelation)
            {
                string sKey = db.NotNullJudgeTipName;
                Control ctrl = db.ControlName;
                if (string.IsNullOrEmpty(sKey) || !db.IsMust)
                {
                    continue;
                }
                bool IsBreak = JudgeControlNotNull(isClewAll, sbAll, sbOne, ref iHaveEnptyItem, ref i, sKey, ctrl);
                if (IsBreak)
                {
                    break;
                }
            }
            #endregion
            #region 返回值处理
            if (iHaveEnptyItem)
            {
                if (!isClewAll)
                {
                    return sbOne.ToString();
                }
                else
                {
                    return sbAll.ToString();
                }
            }
            return null;
            #endregion
        }
        #endregion

        #region 非空判断私有方法
        private static bool JudgeControlNotNull(bool isClewAll, StringBuilder sbAll, StringBuilder sbOne, ref bool iHaveEnptyItem, ref int i, string sKey, Control ctrl)
        {
            if (ctrl is TextBox)
            {
                if (string.IsNullOrEmpty(((TextBox)ctrl).Text.Trim()))
                {
                    sbAll.Append(i + "、【" + sKey + "】不能为空！\n");
                    sbOne.Append("【" + sKey + "】不能为空！");
                    iHaveEnptyItem = true;
                    i++;
                    if (!isClewAll)
                    {
                        return true;
                    }
                }
            }
            else if (ctrl is ComboBox)
            {
                ComboBox cbb = (ComboBox)ctrl;
                if (cbb.SelectedValue == null || string.IsNullOrEmpty(cbb.SelectedValue.ToString().Trim()))
                {
                    sbAll.Append(i + "、【" + sKey + "】不能为空！\n");
                    sbOne.Append("【" + sKey + "】不能为空！");
                    iHaveEnptyItem = true;
                    i++;
                    if (!isClewAll)
                    {
                        return true;
                    }
                }
            }
            else if (ctrl is DateTimePicker)
            {
                //显示复选框且没选中时为空
                if (((DateTimePicker)ctrl).ShowCheckBox == true && ((DateTimePicker)ctrl).Checked == false || string.IsNullOrEmpty(((DateTimePicker)ctrl).Value.ToString().Trim()))
                {
                    sbAll.Append(i + "、【" + sKey + "】不能为空！\n");
                    sbOne.Append("【" + sKey + "】不能为空！");
                    iHaveEnptyItem = true;
                    i++;
                    if (!isClewAll)
                    {
                        return true;
                    }
                }
            }
            else if (ctrl is RichTextBox)
            {
                if (string.IsNullOrEmpty(((RichTextBox)ctrl).Text.ToString().Trim()))
                {
                    sbAll.Append(i + "、【" + sKey + "】不能为空！");
                    sbOne.Append("【" + sKey + "】不能为空！");
                    iHaveEnptyItem = true;
                    i++;
                    if (!isClewAll)
                    {
                        return true;
                    }
                }
            }
            else if (ctrl is CheckBox)
            {

            }
            else if (ctrl is NumericUpDown)
            {
                if(string.IsNullOrEmpty(((NumericUpDown)ctrl).Value.ToString().Trim()))
                {
                    sbAll.Append(i + "、【" + sKey + "】不能为空！");
                    sbOne.Append("【" + sKey + "】不能为空！");
                    iHaveEnptyItem = true;
                    i++;
                    if (!isClewAll)
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new Exception("未定义的判断非空类型：" + ctrl.GetType()+ "，请联系系统管理员！");
            }
            return false;
        }
        #endregion
    }
}
