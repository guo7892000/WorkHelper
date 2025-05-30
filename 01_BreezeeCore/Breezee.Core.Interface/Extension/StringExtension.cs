﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;


/************************************************************************************
 * 对象名称：String扩展类
 * 对象类别：对象类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：对象扩展类，使对象本身具有的方法，扩展现有对象（包括微软类库中的类）。
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * *********************************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static class StringExtension
    {
        #region 获取字符串的字节长度
        /// <summary>
        /// 获取字符串的字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetByteLength(this string str)
        {
            //空值处理
            if (string.IsNullOrEmpty(str)) return 0;

            //获取字节数
            byte[] obj = System.Text.Encoding.Default.GetBytes(str);
            return obj.Length;
        }
        #endregion

        #region 移除最后一个字符
        /// <summary>
        /// 移除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveLastChar(this string str)
        {
            //空值处理
            if (string.IsNullOrEmpty(str)) return "";

            //获取字节数
            return str.Remove(str.Length - 1);
        }
        #endregion

        #region 转换对象为decimal
        /// <summary>
        /// 转换对象为decimal，如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="strValue">需要转换的对象</param>
        /// <returns>返回decimal</returns>
        public static decimal ToDecimal(this string strValue, string strDefaultValue = "0.00")
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return decimal.Parse(strDefaultValue);
            }

            decimal result;
            if (decimal.TryParse(strValue.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return decimal.Parse(strDefaultValue);
            }
        }
        #endregion

        #region 转换对象为decimal
        /// <summary>
        /// 如果对象为null、DBNull或者空则返回0
        /// </summary>
        /// <param name="strValue">需要转换的对象</param>
        /// <param name="iNum">小数位数</param>
        /// <returns>返回decimal</returns>
        public static decimal ToDecimal(this string strValue, int iNum, string strDefaultValue = "0.00")
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return decimal.Round(decimal.Parse("0.00"), iNum);
            }

            decimal result;
            if (decimal.TryParse(strValue.ToString().Trim(), out result))
            {
                return decimal.Round(result, iNum);
            }
            else //转换出错
            {
                if (decimal.TryParse(strDefaultValue.ToString().Trim(), out result))
                {
                    return decimal.Round(result, iNum);
                }
                else
                {
                    return decimal.Round(decimal.Parse("0.00"), iNum);
                }
            }
        }
        #endregion

        #region 将txt文件内容转化成DataTable
        /// <summary>
        /// 将txt文件内容转化成DataTable
        /// 2014-06-25
        /// 将txt文件流转成datatable
        /// 1:若传入的datatable为空，则创建datatable,创建的datatable列为"col"+数字,类型为string
        /// 2:传入的datatable列必须与txt流的列一 一对应
        /// 3:通过colMappingArr数组可建立起DataTable列和txt流的列对应关系,colMappingArr内容必须为Datatable的列名
        /// </summary>
        /// <param name="inputPath">txt文件的路径</param>
        /// <param name="outDataTable">输出的DataTable,需要给出DataTable列,若列表不存在,则可以根据txt流的列自动生成列</param>
        /// <param name="splitFlag">文件分割符 默认为','</param>
        /// <param name="colMappingArr">列映射 默认为null</param>
        /// <param name="IsNeedHeader">是否需要传入表头 默认为false</param>
        public static void TxtConvertDataTable(this string inputPath, ref DataTable outDataTable, char splitFlag = ',', string[] colMappingArr = null, bool IsNeedHeader = false)
        {
            try
            {
                StreamReader streamReader = new StreamReader(inputPath, Encoding.Default);
                string rowData = string.Empty;
                rowData = streamReader.ReadLine();

                if (rowData == null)
                {
                    throw new Exception("数据不能为空!");
                }
                string[] rows = rowData.Split(splitFlag);
                int recordNum = rows.Length;
                if (recordNum > 0)
                {
                    if (outDataTable == null)
                    {//创建表格,指定表格列数
                        outDataTable = new DataTable();
                        for (int i = 0; i < recordNum; i++)
                        {
                            outDataTable.Columns.Add(rows[i], typeof(string));
                        }
                        outDataTable.AcceptChanges();
                    }
                    else if (outDataTable.Columns.Count < recordNum)
                    {
                        throw new Exception("传入的表格列数不能小于导入的流列数!");
                    }
                }
                int temp = 0;
                while (rowData != null)
                {
                    if (IsNeedHeader == true && temp == 0)
                    {
                        temp++;
                        rowData = streamReader.ReadLine();
                        continue;
                    }
                    rows = rowData.Split(splitFlag);
                    if (!string.IsNullOrEmpty(rows[0]))
                    {
                        DataRow dr = outDataTable.NewRow();
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (colMappingArr != null && colMappingArr.Length > 0)
                            {
                                if (dr[colMappingArr[i]].GetType() == typeof(decimal))
                                {
                                    dr[colMappingArr[i]] = Convert.ToDecimal(rows[i]);
                                }
                                else
                                {
                                    dr[colMappingArr[i]] = rows[i];
                                }
                            }
                            else
                            {
                                if (dr[i].GetType() == typeof(decimal))
                                {
                                    dr[i] = Convert.ToDecimal(rows[i]);
                                }
                                else
                                {
                                    dr[i] = rows[i];
                                }
                            }

                        }
                        outDataTable.Rows.Add(dr);
                    }
                    rowData = streamReader.ReadLine();
                }
                outDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断一个对象是否为空
        /// <summary>
        /// 判断一个对象是否为空，true表示空
        /// </summary>
        /// <param name="val">传入的对象</param>
        /// <returns>true为空</returns>
        public static bool IsNullOrEmpty(this object val)
        {
            if (val == null || val == DBNull.Value)
            {
                return true;
            }

            string s = val.ToString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return true;
            }

            return false;
        }
        #endregion

        public static bool EqualsIgnorEmptyCase(this string sSource, string sTarget)
        {
            return sSource.Replace(" ", "").Equals(sTarget.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// 获取粘贴板第一列的表
        /// </summary>
        /// <param name="pasteText"></param>
        /// <param name="dt"></param>
        /// <param name="isTrimData"></param>
        /// <param name="AutoColumnName"></param>
        /// <param name="isFirstColumnName"></param>
        /// <param name="isAddRowNum"></param>
        /// <param name="sRowNumColumnName"></param>
        /// <returns></returns>
        public static DataTable GetFirstColumnTable(this string pasteText, DataTable dt = null, bool isTrimData = false, bool AutoColumnName=false,bool isFirstColumnName = false,  bool isAddRowNum = true, string sRowNumColumnName = "ROWNO")
        {
            string sDataColumnName = string.Empty;

            if (dt == null)
            {
                dt = new DataTable();
            }
            else if (dt.Columns.Count > 0)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (!sRowNumColumnName.Equals(dc.ColumnName))
                    {
                        sDataColumnName = dc.ColumnName;
                        break;
                    }
                }
            }

            if (!dt.Columns.Contains(sRowNumColumnName) && isAddRowNum)
            {
                dt.Columns.Add(sRowNumColumnName, typeof(int)); //设置序号为整型
            }
            HashSet<string> doubleCol = new HashSet<string>();
            string[] rows = pasteText.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);//分割的行数数组
            string[] colNames = rows[0].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);//列头数组

            
            for (int i = 0; i < rows.Length; i++)//行
            {
                if (i == 0 && isFirstColumnName)//列名处理
                {
                    if (AutoColumnName)
                    {
                        string sColName = 0.ToUpperWord();
                        if (!dt.Columns.Contains(sColName))
                        {
                            dt.Columns.Add(sColName, typeof(string));
                        }
                    }
                    else
                    {
                        if (!dt.Columns.Contains(colNames[0].Trim()))
                        {
                            dt.Columns.Add(colNames[0].Trim(), typeof(string));
                        }
                        else
                        {
                            doubleCol.Add(colNames[0]);
                        }
                    }
                }
                else
                {
                    // 数据处理
                    DataRow dr = dt.NewRow();
                    string[] cols = isTrimData ? rows[i].Trim().Split(new string[] { "\t" }, StringSplitOptions.None) : rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);//注：这里不要去掉空白
                    dr[sRowNumColumnName] = i+1; //行号
                    dr[sDataColumnName] = isTrimData ? cols[0].Trim('"').Trim() : cols[0].Trim('"'); //第一列为序号，需要跳过
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取字符表
        /// </summary>
        /// <param name="pasteText">粘贴的文本</param>
        /// <param name="AutoColumnName">是否自动列名</param>
        /// <param name="dt">在哪个表上累加数据</param>
        /// <param name="autoColumnEndString">自动列名的生缀</param>
        /// <param name="isTrimData">是否去掉数据前后空格</param>
        /// <param name="isAddRowNum">是否增加序号列</param>
        /// <returns></returns>
        public static DataTable GetStringTable(this string pasteText, bool AutoColumnName, DataTable dt = null, string autoColumnEndString = "", bool isTrimData = false, bool isAddRowNum = true, string sRowNumColumnName = "ROWNO")
        {
            if (dt == null)
            {
                dt = new DataTable();
            }
            bool addNumRow = false;
            int iRowNum = 0;
            if (!dt.Columns.Contains(sRowNumColumnName) && isAddRowNum)
            {
                dt.Columns.Add(sRowNumColumnName, typeof(int)); ////设置序号为整型
                addNumRow = true;
                iRowNum = 1;
            }

            HashSet<string> doubleCol = new HashSet<string>();
            string[] rows = pasteText.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);//分割的行数数组
            string[] colNames = rows[0].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);//列头数组
            for (int i = 0; i < rows.Length; i++)//行
            {
                if (i == 0)//列名处理
                {
                    if (AutoColumnName)
                    {
                        for (int j = 0; j < colNames.Length; j++)
                        {
                            string sColName = j.ToUpperWord() + autoColumnEndString;
                            if (!dt.Columns.Contains(sColName))
                            {
                                dt.Columns.Add(sColName, typeof(string));
                            }
                        }
                    }
                    else
                    {
                        foreach (string s in colNames)
                        {
                            if (!dt.Columns.Contains(s.Trim()))
                            {
                                dt.Columns.Add(s.Trim(), typeof(string));
                            }
                            else
                            {
                                doubleCol.Add(s);
                            }
                        }
                        if (doubleCol.Count > 0)
                        {
                            throw new Exception("粘贴的Excel存在重复的列名，请修改后重新粘贴！包括：" + string.Join(",", doubleCol));
                        }
                    }
                }
                else
                {
                    // 数据处理
                    DataRow dr = dt.NewRow();
                    string[] cols = isTrimData ? rows[i].Trim().Split(new string[] { "\t" }, StringSplitOptions.None) : rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);//注：这里不要去掉空白
                    //增加数据列数与表列数的大小比较，防止访问表列的数组越界而报错。注：这里要去掉序号列
                    if (cols.Length > (dt.Columns.Count - iRowNum))
                    {
                        //数据列数大于表列数
                        if (addNumRow)
                        {
                            // 有序号列
                            dr[sRowNumColumnName] = i; //行号
                            int okIndex = 0;
                            for (int j = 0; j < cols.Length; j++)
                            {
                                if ("\"".Equals(cols[j]))
                                {
                                    continue;
                                }
                                else
                                {
                                    // 注：因为Excel中针对部分包含特殊字符的文本会在前后加上引号，所以后面会有去掉前后引号的处理。数据例如："	2023款 经典 2.0L CVT XV+领先版 国6"
                                    dr[okIndex + 1] = isTrimData ? cols[j].Trim('"').Trim() : cols[j].Trim('"'); //第一列为序号，需要跳过
                                    okIndex++;
                                    if (okIndex >= dt.Columns.Count - 1)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 无序号列
                            int okIndex = 0;
                            for (int j = 0; j < cols.Length; j++)
                            {
                                if ("\"".Equals(cols[j]))
                                {
                                    continue;
                                }
                                else
                                {
                                    // 注：因为Excel中针对部分包含特殊字符的文本会在前后加上引号，所以后面会有去掉前后引号的处理。数据例如："	2023款 经典 2.0L CVT XV+领先版 国6"
                                    dr[okIndex] = isTrimData ? cols[j].Trim('"').Trim() : cols[j].Trim('"'); //第一列为实际数据
                                    okIndex++;
                                    if (okIndex >= dt.Columns.Count)
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        //数据列数小于等于表列数
                        if (addNumRow)
                        {
                            // 有序号列
                            dr[sRowNumColumnName] = i; //行号
                            int okIndex = 0;
                            for (int j = 0; j < cols.Length; j++)
                            {
                                if ("\"".Equals(cols[j]))
                                {
                                    continue;
                                }
                                else
                                {
                                    // 注：因为Excel中针对部分包含特殊字符的文本会在前后加上引号，所以后面会有去掉前后引号的处理。数据例如："	2023款 经典 2.0L CVT XV+领先版 国6"
                                    dr[okIndex + 1] = isTrimData ? cols[j].Trim('"').Trim() : cols[j].Trim('"'); //第一列为序号，需要跳过
                                    okIndex++;
                                    if (okIndex >= dt.Columns.Count - 1)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 无序号列
                            int okIndex = 0;
                            for (int j = 0; j < cols.Length; j++)
                            {
                                if ("\"".Equals(cols[j]))
                                {
                                    continue;
                                }
                                else
                                {
                                    // 注：因为Excel中针对部分包含特殊字符的文本会在前后加上引号，所以后面会有去掉前后引号的处理。数据例如："	2023款 经典 2.0L CVT XV+领先版 国6"
                                    dr[okIndex] = isTrimData ? cols[j].Trim('"').Trim() : cols[j].Trim('"'); //第一列为实际数据
                                    okIndex++;
                                    if (okIndex >= dt.Columns.Count)
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 下横线转驼峰
        /// </summary>
        /// <param name="strColCode">要转换的字符</param>
        /// <param name="isFirstWorldUpper">是：大驼峰，否：小驼峰</param>
        /// <returns></returns>
        public static string FirstLetterUpper(this string strColCode, bool isFirstWorldUpper = true)
        {
            strColCode = strColCode.ToLower();
            string[] firstUpper = strColCode.Split('_');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var s in firstUpper)
            {
                if (i == 0 && !isFirstWorldUpper)
                {
                    sb.Append(s);
                }
                else
                {
                    sb.Append(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s));
                }
                i++;
            }
            strColCode = sb.ToString();
            return strColCode;
        }

        /// <summary>
        /// 驼峰转下横线
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnderscoreCase(this string str, bool isUpper = true)
        {
            string sUnderLine = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
            return isUpper ? sUnderLine.ToUpper() : sUnderLine.ToLower();
        }

        /// <summary>
        /// 表列中的类型不需要长度的处理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TableColTypeNotNeedLenDeal(this string str)
        {
            return str.Replace("datetime(7)", "datetime").Replace("date(7)", "date").Replace("decimal(22,0)", "int").Replace("decimal(22)", "int");
        }
    }
}
