using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Breezee.Core.Entity;

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
    /// 网格定义列
    /// </summary>
    public class FlexGridColumn
    {
        /***********************************************************************************************
        设置网格的Tag值样式：0数据库列名,1列名,2显示文本,3列显示样式,4是否可见，5列宽度,6对齐方式，7可编辑,8可编辑列最大长度（可省略）
        其中:
             一、列样式(DataGridViewColumn)：
                T表示DataGridViewTextBoxColumn
                B表示DataGridViewButtonColumn
                C表示DataGridViewComboBoxColumn
                K表示DataGridViewCheckBoxColumn
                L表示DataGridViewLinkColumn
                I表示DataGridViewImageColumn
        ***********************************************************************************************/

        #region 属性
        /// <summary>
        /// 数据库中的列名
        /// </summary>
        public string DBColumnName { get; private set; }

        /// <summary>
        /// 绑定对象列名（一般跟数据库中的列名一样）
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// 获取或者设置标题
        /// </summary>
        public string ColumnCaption { get; private set; }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool AllowEditing { get; private set; } = false;

        /// <summary>
        /// 获取或者设置文本对齐方式，默认为左对齐
        /// </summary>
        public DataGridViewContentAlignment Alignment { get; private set; } = DataGridViewContentAlignment.MiddleLeft;

        /// <summary>
        /// 获取或者设置列宽，默认为100
        /// </summary>
        public int ColumnWidth { get; private set; } = 100;

        /// <summary>
        /// 获取或者设置是否显示
        /// </summary>
        public bool ColumnVisible { get; private set; } = true;

        /// <summary>
        /// 获取或者设置网格列的显示样式
        /// </summary>
        public DataGridViewColumnTypeEnum ColumnDisplayType { get; private set; } = DataGridViewColumnTypeEnum.TextBox;

        /// <summary>
        /// 获取或者设置网格列的最大可输入长度
        /// </summary>
        public int MaxLength { get; private set; } = 8000;

        #region 是否按钮属性
        /// <summary>
        /// 判断是否为按钮，如果Name值为按钮则是
        /// </summary>
        public bool IsButtonColumn { get; private set; }
        #endregion

        #endregion

        #region 转换为字符方法
        public override string ToString()
        {
            //0数据库列名,1列名,2显示文本,3列显示样式,4是否可见，5列宽度,6对齐方式，7可编辑,8可编辑列最大长度（可省略）
            List<string> lst = new List<string>();
            lst.Add(DBColumnName);//0数据库列名
            lst.Add(ColumnName);//1列名
            lst.Add(ColumnCaption);//2显示文本
            lst.Add(FlexGridColumnDefinition.DataGridColumnTypeEnumToString(ColumnDisplayType));//3列显示样式
            lst.Add(ColumnVisible.ToString());//4是否可见
            lst.Add(ColumnWidth.ToString());//5列宽度
            lst.Add(FlexGridColumnDefinition.ConvertDataGridViewTextAlignToString(Alignment));//6对齐方式
            lst.Add(AllowEditing.ToString());//7可编辑
            lst.Add(MaxLength.ToString());//8可编辑列最大长度（可省略）

            return string.Join(StaticConstant.FRA_GRID_COLUMN_SPLIT_PROPERT_STR, lst);
        } 
        #endregion

        #region 将文本转换为网格列定义
        /// <summary>
        /// 将文本转换为网格列定义
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static FlexGridColumn FromString(string text)
        {
            //0数据库列名,1列名,2显示文本,3列显示样式,4是否可见，5列宽度,6对齐方式，7可编辑,8可编辑列最大长度（可省略）
            string[] segments = text.Split(new char[] { StaticConstant.FRA_GRID_COLUMN_SPLIT_PROPERT_CHAR, ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length < 9)
            {
                throw new ArgumentException("不是有效的网格列字符串");
            }
            int width = 0;
            int.TryParse(segments[5], out width);
            int maxLength = 0;
            int.TryParse(segments[8], out maxLength);
            FlexGridColumn column = new FlexGridColumn.Builder().DBName(segments[0]).Name(segments[1]).Caption(segments[2]).Type(FlexGridColumnDefinition.ConvertToDataGridColumnTypeEnumFromString(segments[3]))
                .Visible(segments[4].ToUpper() == "TRUE").Width(width).Align(FlexGridColumnDefinition.ConvertToDataGridViewTextAlignFromString(segments[6]))
                .Edit(segments[7].ToUpper() == "TRUE").MaxLen(maxLength).Build();
            return column;
        }
        #endregion

        #region 特殊列方法
        /// <summary>
        /// 新序号列方法
        /// </summary>
        /// <returns></returns>
        public static FlexGridColumn NewRowNoCol()
        {
            FlexGridColumn column = new FlexGridColumn.Builder().Name(StaticConstant.FRA_GRID_ROWNO_STR).Caption("序号").Type(DataGridViewColumnTypeEnum.TextBox)
                .Visible(true).Width(40).Align(DataGridViewContentAlignment.MiddleRight).Edit(false).Build();
            return column;
        }

        /// <summary>
        /// 新隐藏列
        /// </summary>
        /// <returns></returns>
        public static FlexGridColumn NewHideCol(string colName)
        {
            FlexGridColumn column = new FlexGridColumn.Builder().Name(colName).Caption(colName).Visible(false).Width(40).Edit(false).Build();
            return column;
        }
        #endregion

        /// <summary>
        /// 构建者类
        /// </summary>
        public sealed class Builder
        {
            private string DBColumnName;
            private string ColumnName;
            private string ColumnCaption;
            private bool AllowEditing;
            private DataGridViewContentAlignment Alignment;
            private int ColumnWidth;
            private bool ColumnVisible;
            private DataGridViewColumnTypeEnum ColumnDisplayType;
            private int MaxLength;
            private bool IsButtonColumn;

            public Builder DBName(string sValue)
            {
                this.DBColumnName = sValue;
                return this;
            }
            public Builder Name(string sValue)
            {
                this.ColumnName = sValue;
                if (string.IsNullOrEmpty(DBColumnName))
                {
                    this.DBColumnName = sValue;
                }
                return this;
            }
            public Builder Caption(string sValue)
            {
                this.ColumnCaption = sValue;
                return this;
            }
            public Builder Edit(bool sValue = true)
            {
                this.AllowEditing = sValue;
                return this;
            }
            public Builder Align(DataGridViewContentAlignment sValue= DataGridViewContentAlignment.MiddleLeft)
            {
                this.Alignment = sValue;
                return this;
            }
            public Builder Width(int sValue)
            {
                ColumnWidth = sValue < 0 ? 0 : sValue;  
                return this;
            }
            public Builder Visible(bool sValue=true)
            {
                this.ColumnVisible = sValue;
                return this;
            }
            public Builder Type(DataGridViewColumnTypeEnum sValue = DataGridViewColumnTypeEnum.TextBox)
            {
                this.ColumnDisplayType = sValue;
                if (sValue == DataGridViewColumnTypeEnum.Button) this.IsButtonColumn = true;
                return this;
            }
            public Builder MaxLen(int sValue)
            {
                this.MaxLength = sValue;
                return this;
            }
            public Builder Button(bool sValue=true)
            {
                this.IsButtonColumn = sValue;
                this.ColumnDisplayType = DataGridViewColumnTypeEnum.Button;//设置是按钮时，列类型也修改
                return this;
            }

            public FlexGridColumn Build()
            {
                FlexGridColumn column = new FlexGridColumn();
                column.DBColumnName = this.DBColumnName;
                column.ColumnName = this.ColumnName;
                column.ColumnCaption = string.IsNullOrEmpty(ColumnCaption) ? ColumnName : ColumnCaption;
                column.AllowEditing = this.AllowEditing;
                column.Alignment = this.Alignment;
                column.ColumnWidth = this.ColumnWidth;
                column.ColumnVisible = this.ColumnVisible;
                column.ColumnDisplayType = this.ColumnDisplayType;
                column.MaxLength = this.MaxLength;
                column.IsButtonColumn = this.IsButtonColumn;

                return column;
            }

        }
    }
}
