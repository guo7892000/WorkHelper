using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：样式文件保存路径：C:\ProgramData\Peach\Config\FormStyles		
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
    public class FlexGridColumnDefinition
    {
        #region 变量
        private List<FlexGridColumn> mColuns = new List<FlexGridColumn>();
 
        private int mFrozenCols = 0;
        private int mFrozenRows = 0;
        private int mFixedCols = 0;
        #endregion

        #region 属性
        /// <summary>
        /// 冻结的列数
        /// </summary>
        public int FrozenCols
        {
            get { return mFrozenCols; }
            set
            {
                if (value < 0)
                {
                    mFrozenCols = 0;
                }
                else
                {
                    mFrozenCols = value;
                }
            }
        }

        // <summary>
        /// 冻结的行数
        /// </summary>
        public int FrozenRows
        {
            get { return mFrozenRows; }
            set
            {
                if (value < 0)
                {
                    mFrozenRows = 0;
                }
                else
                {
                    mFrozenRows = value;
                }
            }
        }
        #endregion

        #region 获取网格列设置
        /// <summary>
        /// 获取网格列设置
        /// </summary>
        public List<FlexGridColumn> Columns
        {
            get { return mColuns; }
        } 
        #endregion

        public DataTable GetNullTable()
        {
            DataTable dtNull = new DataTable();
            foreach (var item in mColuns)
            {
                dtNull.Columns.Add(item.DBColumnName);
            }
            return dtNull;
        }

        #region 添加列
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="name">列名</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">长度</param>
        /// <param name="allowEditing">能否编辑，默认不能</param>
        /// <param name="visible">是否可见，默认可见</param>
        /// <param name="cellFormat">网格格式，默认无</param>
        /// <returns></returns>
        public FlexGridColumnDefinition AddColumn(string DbColunmName, string name, string caption, DataGridViewColumnTypeEnum cte, bool visible, int width, DataGridViewContentAlignment alignment, bool allowEditing, int iMaxLength)
        {
            //0数据库列名,1列名,2显示文本,3列显示样式,4是否可见，5列宽度,6对齐方式，7可编辑,8可编辑列最大长度（可省略）
            #region 旧方式（已取消）
            //FlexGridColumn column = new FlexGridColumn
            //{
            //    DBColumnName = DbColunmName,
            //    ColumnName = name,
            //    ColumnCaption = caption,
            //    ColumnDisplayType = cte,
            //    Alignment = alignment,
            //    ColumnWidth = width,
            //    AllowEditing = allowEditing,
            //    ColumnVisible = visible,
            //    MaxLength = iMaxLength,
            //    IsButtonColumn = cte == DataGridViewColumnTypeEnum.Button ? true : false
            //}; 
            #endregion

            FlexGridColumn column = new FlexGridColumn.Builder().DBName(DbColunmName).Name(name).Caption(caption).Type(cte).
                Align(alignment).Width(width).Edit(allowEditing).Visible(visible).MaxLen(iMaxLength)
                .Build();

            this.Columns.Add(column);
            return this;
        }

        /// <summary>
        /// 增加网格列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public FlexGridColumnDefinition AddColumn(params FlexGridColumn[] column)
        {
            this.Columns.AddRange(column);
            return this;
        }

        /// <summary>
        /// 重构函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caption"></param>
        /// <param name="cte"></param>
        /// <param name="visible"></param>
        /// <param name="width"></param>
        /// <param name="alignment"></param>
        /// <param name="allowEditing"></param>
        /// <param name="iMaxLength"></param>
        /// <returns></returns>
        public FlexGridColumnDefinition AddColumn(string name, string caption, DataGridViewColumnTypeEnum cte = DataGridViewColumnTypeEnum.TextBox, bool visible = true, int width = 100, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft, bool allowEditing = false, int iMaxLength = 8000)
        {
            return AddColumn(name, name, caption, cte, visible, width, alignment, allowEditing, iMaxLength);
        }

        public void AddColumn(object cUST_NAME_S, string v1, DataGridViewColumnTypeEnum textBox, bool v2, int v3, DataGridViewContentAlignment middleLeft, bool v4, int v5)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 将网格Tag值保存到文件
        public void SaveToFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    string dir = Path.GetDirectoryName(path);
                    Directory.CreateDirectory(dir);
                }
                else
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }

                List<string> lst = new List<string>();
                lst.Add(string.Format("{0}-{1}", this.FrozenCols, this.FrozenRows));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    builder.Append(this.Columns[i].ToString());
                    if (i != this.Columns.Count - 1)
                    {
                        builder.Append(StaticConstant.FRA_GRID_COLUMN_SPLIT_STR);//分号
                    }
                }

                lst.Add(builder.ToString());
                File.WriteAllLines(path, lst, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region 得到网格Tag字符串
        public string GetGridTagString()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    builder.Append(this.Columns[i].ToString());
                    if (i != this.Columns.Count - 1)
                    {
                        builder.Append(StaticConstant.FRA_GRID_COLUMN_SPLIT_STR);//分号
                    }
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断两个网格列定义是否相同
        /// <summary>
        /// 判断两个网格列定义是否相同.
        /// </summary>
        /// <param name="other">另一个网格列定义</param>
        /// <returns>如果相同则返回true，否则false</returns>
        /// <remarks>
        /// 比较规则：
        /// <para>1.如果如果列的个数不一致则false</para>
        /// <para>2.列数个数一致，但有列名不同则false</para>
        /// <para>3.列数个数一致，但有标题名不同则false</para>
        /// </remarks>
        public bool IsSame(FlexGridColumnDefinition other)
        {
            if (other == null)
                return false;

            //1.比较长度是否一致
            if (this.Columns.Count != other.Columns.Count)
            {
                return false;
            }

            //2.比较列名是否一致
            if (!this.Columns.TrueForAll(x => other.Columns.Exists(
                y => y.ColumnName.ToUpper() == x.ColumnName.ToUpper() && y.ColumnVisible == x.ColumnVisible && y.AllowEditing == x.AllowEditing
                )))
            {
                return false;
            }

            //3.比较标题是否一致
            if (!this.Columns.TrueForAll(x => other.Columns.Exists(
                y => y.ColumnCaption == x.ColumnCaption && y.ColumnVisible == x.ColumnVisible && y.AllowEditing == x.AllowEditing
                )))
            {
                return false;
            }

            //4.比较类型是否一致
            if (!this.Columns.TrueForAll(x => other.Columns.Exists(
                y => y.ColumnName.ToUpper() == x.ColumnName.ToUpper() && y.ColumnDisplayType == x.ColumnDisplayType
                )))
            {
                return false;
            }

            return true;
        } 
        #endregion

        #region 将网格转换为网格定义列
        public static FlexGridColumnDefinition GetDefinitionFromGrid(DataGridView flexGrid)
        {
            FlexGridColumnDefinition definition = new FlexGridColumnDefinition();

            for (int i = 0; i < flexGrid.Rows.Count; i++)
            {
                var row = flexGrid.Rows[i];
                if (row.Frozen)
                {
                    definition.FrozenRows = row.Index; //确定冻结行
                }
            }

            for (int i = 0; i < flexGrid.Columns.Count; i++)
            {
                var column = flexGrid.Columns[i];
                if (column.Frozen)
                {
                    definition.FrozenCols = column.DisplayIndex; //确定冻结列
                }

                string caption = column.HeaderText;
                int iMaxInputLengh = 8000;//最大输入长度
                DataGridViewColumnTypeEnum colunmType = DataGridViewColumnTypeEnum.TextBox; //列类型
                if (!column.ReadOnly && caption.Length > 0 && caption.LastIndexOf("+") == caption.Length - 1)
                {
                    caption = caption.Remove(caption.LastIndexOf("+"));//去掉加号
                }
                if (column is DataGridViewTextBoxColumn)
                {
                    iMaxInputLengh = ((DataGridViewTextBoxColumn)column).MaxInputLength;
                    colunmType = DataGridViewColumnTypeEnum.TextBox;
                }
                else if (column is DataGridViewButtonColumn)
                {
                    colunmType = DataGridViewColumnTypeEnum.Button;
                }
                else if (column is DataGridViewComboBoxColumn)
                {
                    colunmType = DataGridViewColumnTypeEnum.ComboBox;
                }
                else if (column is DataGridViewCheckBoxColumn)
                {
                    colunmType = DataGridViewColumnTypeEnum.CheckBox;
                }
                else if (column is DataGridViewLinkColumn)
                {
                    colunmType = DataGridViewColumnTypeEnum.Link;
                }
                else if (column is DataGridViewImageColumn)
                {
                    colunmType = DataGridViewColumnTypeEnum.Image;
                }
                else
                {
                    throw new Exception("未定义的类型：" + column.CellType);
                }
                //增加自定义列
                definition.AddColumn(column.Name, caption, colunmType, column.Visible,column.Width, column.DefaultCellStyle.Alignment, !column.ReadOnly, iMaxInputLengh);
            }

            return definition;
        } 
        #endregion

        #region 将字符串转换为网格定义列
        public static FlexGridColumnDefinition GetDefinitionFromTagString(string tagString)
        {
            if (string.IsNullOrEmpty(tagString))
            {
                throw new Exception("Tag中没有包含网格列定义");
            }

            string[] lstColumns = tagString.Split(new string[] { StaticConstant.FRA_GRID_COLUMN_SPLIT_STR }, StringSplitOptions.RemoveEmptyEntries);
            if (lstColumns.Length == 0)
            {
                throw new Exception("Tag中没有包含网格列定义");
            }

            FlexGridColumnDefinition definition = new FlexGridColumnDefinition();
            foreach (string colStr in lstColumns)
            {
                if (colStr.Trim().Length == 0)
                {
                    continue;
                }

                FlexGridColumn column = FlexGridColumn.FromString(colStr);
                definition.Columns.Add(column);
            }

            return definition;
        } 
        #endregion

        #region 将文件转换为网格定义列
        public static FlexGridColumnDefinition GetDefinitionFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                if (lines.Length < 2)
                {
                    return null;
                }

                FlexGridColumnDefinition definition = new FlexGridColumnDefinition();
                string[] listA = lines[0].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                definition.FrozenCols = int.Parse(listA[0]);
                definition.FrozenRows = int.Parse(listA[1]);

                string[] listB = lines[1].Split(new string[] { StaticConstant.FRA_GRID_COLUMN_SPLIT_STR }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in listB)
                {
                    FlexGridColumn col = FlexGridColumn.FromString(str);
                    definition.Columns.Add(col);
                }

                return definition;
            }
            catch
            {
                return null;
            }
        } 
        #endregion

        #region 将网格列的文本对齐方式枚举转换为文本
        public static string ConvertDataGridViewTextAlignToString(DataGridViewContentAlignment textAlign)
        {
            string strReturn = "ML";
            switch (textAlign)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    strReturn = "BC";
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    strReturn = "BL";
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    strReturn = "BR";
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    strReturn = "MC";
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    strReturn = "ML";
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    strReturn = "MR";
                    break;
                case DataGridViewContentAlignment.NotSet:
                    strReturn = "NS";
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    strReturn = "TC";
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    strReturn = "TL";
                    break;
                case DataGridViewContentAlignment.TopRight:
                    strReturn = "TR";
                    break;
                default:
                    strReturn = "ML";
                    break;
            }

            return strReturn;
        } 
        #endregion

        #region 将文本转换为网格列的文本对齐方式枚举
        public static DataGridViewContentAlignment ConvertToDataGridViewTextAlignFromString(string alignment)
        {
            DataGridViewContentAlignment textAlign = DataGridViewContentAlignment.MiddleLeft;
            switch (alignment)
            {
                case "BC":
                    textAlign = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case "BL":
                    textAlign = DataGridViewContentAlignment.BottomLeft;
                    break;
                case "BR":
                    textAlign = DataGridViewContentAlignment.BottomRight;
                    break;
                case "MC":
                    textAlign = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "ML":
                    textAlign = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case "MR":
                    textAlign = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "NS":
                    textAlign = DataGridViewContentAlignment.NotSet;
                    break;
                case "TC":
                    textAlign = DataGridViewContentAlignment.TopCenter;
                    break;
                case "TL":
                    textAlign = DataGridViewContentAlignment.TopLeft;
                    break;
                case "TR":
                    textAlign = DataGridViewContentAlignment.TopRight;
                    break;
                default:
                    textAlign = DataGridViewContentAlignment.MiddleLeft;
                    break;
            }
            return textAlign;
        }
        #endregion

        #region 将网格列类型转换为文本
        public static string ConvertDataGridColumnTypeToString(DataGridViewColumn dgvc)
        {
            string strReturn = "T";
            if (dgvc is DataGridViewTextBoxColumn)
            {
                strReturn = "T";
            }
            else if (dgvc is DataGridViewButtonColumn)
            {
                strReturn = "B";
            }
            else if (dgvc is DataGridViewComboBoxColumn)
            {
                strReturn = "C";
            }
            else if (dgvc is DataGridViewCheckBoxColumn)
            {
                strReturn = "K";
            }
            else if (dgvc is DataGridViewLinkColumn)
            {
                strReturn = "L";
            }
            else if (dgvc is DataGridViewImageColumn)
            {
                strReturn = "I";
            }
            return strReturn;
        }
        #endregion

        #region 将文本转换为网格列类型枚举
        public static DataGridViewColumnTypeEnum ConvertToDataGridColumnTypeEnumFromString(string dgvc)
        {
            DataGridViewColumnTypeEnum strReturn = DataGridViewColumnTypeEnum.TextBox;
            if (dgvc == "T")
            {
                strReturn = DataGridViewColumnTypeEnum.TextBox;
            }
            else if (dgvc == "B")
            {
                strReturn = DataGridViewColumnTypeEnum.Button;
            }
            else if (dgvc == "C")
            {
                strReturn = DataGridViewColumnTypeEnum.ComboBox;
            }
            else if (dgvc == "K")
            {
                strReturn = DataGridViewColumnTypeEnum.CheckBox;
            }
            else if (dgvc == "L")
            {
                strReturn = DataGridViewColumnTypeEnum.Link;
            }
            else if (dgvc == "I")
            {
                strReturn = DataGridViewColumnTypeEnum.Image;
            }
            return strReturn;
        }
        #endregion

        #region 将网格列类型枚举转换为文本
        public static string DataGridColumnTypeEnumToString(DataGridViewColumnTypeEnum alignment)
        {
            string strReturn = "T";
            switch (alignment)
            {
                case DataGridViewColumnTypeEnum.TextBox:
                    strReturn = "T";
                    break;
                case DataGridViewColumnTypeEnum.Button:
                    strReturn = "B";
                    break;
                case DataGridViewColumnTypeEnum.ComboBox:
                    strReturn = "C";
                    break;
                case DataGridViewColumnTypeEnum.CheckBox:
                    strReturn = "K";
                    break;
                case DataGridViewColumnTypeEnum.Link:
                    strReturn = "L";
                    break;
                case DataGridViewColumnTypeEnum.Image:
                    strReturn = "I";
                    break;
                default:
                    strReturn = "T";
                    break;
            }
            return strReturn;
        }
        #endregion

    }
}
