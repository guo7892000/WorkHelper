using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Dynamic;
using System.Reflection.Emit;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

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
namespace Breezee.Core.Interface
{
    /// <summary>
    /// IDictionary<TKey, TValue>扩展方法
    /// </summary>
    public static class IDictionaryExtension
    {
        public static readonly string Dictionary_Key = "VALUE_CODE";
        public static readonly string Dictionary_Value = "VALUE_NAME";

        #region 转换为字符字典
        /// <summary>
        /// 转换为字符字典
        /// </summary>
        /// <param name="dict">传入的字典</param>
        /// <returns></returns>
        /// <example>
        /// IDictionary&lt;string, object&gt; dict = new Dictionary&lt;string, object&gt;();
        /// dict.ToStringDict();
        /// </example>
        public static IDictionary<string, string> ToStringDict(this IDictionary<string, object> dict)
        {
            IDictionary<string, string> dicRet = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> item in dict)
            {
                if (item.Value is DataTable || item.Value is Array)
                {
                    continue;
                }

                dicRet[item.Key] = item.Value.IsNullOrEmpty() ? string.Empty : item.Value.ToString();
            }

            return dicRet;
        }
        #endregion

        #region 转换为对象字典
        /// <summary>
        /// 转换为对象字典
        /// </summary>
        /// <param name="dict">传入的字典</param>
        /// <returns></returns>
        /// <example>
        /// IDictionary&lt;string, string&gt; dict = new Dictionary&lt;string, string&gt;();
        /// dict.ToObjectDict();
        /// </example>
        public static IDictionary<string, object> ToObjectDict(this IDictionary<string, string> dict)
        {
            IDictionary<string, object> dicRet = new Dictionary<string, object>();
            foreach (KeyValuePair<string, string> item in dict)
            {
                dicRet[item.Key] = item.Value;
            }

            return dicRet;
        }
        #endregion

        #region 将第二个字典合并到第一字典
        /// <summary>
        /// 将第二个字典合并到第一字典（注意：将会覆盖第一个字典存在的键值对）
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IDictionary<string, string> Merge(this IDictionary<string, string> first, IDictionary<string, string> second)
        {
            foreach (KeyValuePair<string, string> item in second)
            {
                first[item.Key] = item.Value;
            }

            return first;
        }
        #endregion

        #region 将第二个字典合并到第一字典
        /// <summary>
        /// 将第二个字典合并到第一字典（注意：将会覆盖第一个字典存在的键值对）
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IDictionary<string, string> Merge(this IDictionary<string, string> first, IDictionary<string, object> second)
        {
            foreach (KeyValuePair<string, object> item in second)
            {
                if (item.Value is DataTable || item.Value is Array)
                {
                    continue;
                }

                first[item.Key] = item.Value == null ? string.Empty : item.Value.ToString();
            }

            return first;
        }
        #endregion

        #region 移除字典中值为空的项
        /// <summary>
        /// 移除字典中值为空的项
        /// </summary>
        /// <param name="dic">字典</param>
        /// <returns>原字典，仅仅为了链式操作</returns>
        public static IDictionary<string, string> RemoveNullItem(this IDictionary<string, string> dic)
        {
            string[] keys = new string[dic.Keys.Count];
            dic.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
            {
                if (string.IsNullOrWhiteSpace(dic[key]))
                {
                    dic.Remove(key);
                }
            }
            return dic;
        }
        #endregion

        #region 安全方式根据键获取字典中的值
        /// <summary>
        /// 安全方式根据键获取字典中的值。当键不存在时返回string.Empty, 不会抛出键不存在的异常。
        /// </summary>
        /// <param name="dict">传入的字典</param>
        /// <param name="key">键</param>
        /// <returns>键存在则返回键所对应的值，否则空</returns>
        public static string SafeGet(this IDictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : string.Empty;
        }
        #endregion

        #region 安全方式根据键获取字典中的值
        /// <summary>
        /// 安全方式根据键获取字典中的值。当键不存在时返回null, 不会抛出键不存在的异常。
        /// </summary>
        /// <param name="dict">传入的字典</param>
        /// <param name="key">键</param>
        /// <returns>键存在则返回键所对应的值，否则null</returns>
        public static object SafeGet(this IDictionary<string, object> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : null;
        }
        #endregion

        #region 将行转为字典
        /// <summary>
        /// 将DataRow的转换为IDictionary&lt;string, string&gt;型的字典（dic["#Key#"] = Value）并传递系统监控全局参数给生成的字典
        /// </summary>
        /// <param name="row">要转换的数据行</param>
        /// <param name="dicPara">能传递监控全局参数的对象</param>
        /// <returns></returns>
        public static IDictionary<string, string> ToDict(this DataRow row, IDictionary<string, object> dicPara)
        {
            DataTable dt = row.Table;
            IDictionary<string, string> dic = ToStringDict(dicPara);
            string columnName = string.Empty;

            //变量赋值
            foreach (DataColumn column in dt.Columns)
            {
                columnName = column.ColumnName;
                dic["#" + columnName + "#"] = row[columnName].ToString();
            }
            return dic;
        }
        #endregion

        #region 根据字典来构造自定义表方法
        /// <summary>
        /// 根据字典来构造自定义表（列先后包括ID和Text）
        /// </summary>
        /// <param name="dic">字典集合，字典键是值，字典值显示文本</param>
        /// <param name="bIncludeNull">是否包括空白行</param>
        /// <returns></returns>
        public static DataTable GetTextValueTable(this IDictionary<string, string> dic, bool bIncludeNull)
        {
            DataTable dtSource = new DataTable();
            dtSource.Columns.Add(Dictionary_Key);
            dtSource.Columns.Add(Dictionary_Value);
            IEnumerator itor = dic.GetEnumerator();
            if (bIncludeNull)
            {
                dtSource.Rows.Add(new object[] { null, "" });
            }
            foreach (string strKey in dic.Keys)
            {
                dtSource.Rows.Add(new object[] { strKey, dic[strKey] });
            }
            return dtSource;
        }
        #endregion

        #region 根据字典来构造自定义表方法
        /// <summary>
        /// 根据字典来构造自定义表（列先后包括ID和Text）
        /// </summary>
        /// <param name="dic">字典集合，字典键是值，字典值显示文本</param>
        /// <param name="bIncludeNull">是否包括空白行</param>
        /// <returns></returns>
        public static DataTable GetTable(this IDictionary<string, string> dic)
        {
            DataTable dtSource = new DataTable();
            foreach (string strKey in dic.Keys)
            {
                dtSource.Columns.Add(strKey);
            }
            return dtSource;
        }
        #endregion

        #region 判断集合中是否包括所有【需验证的键清单】的非空值
        /// <summary>
        /// 判断集合中是否包括所有【需验证的键清单】的非空值
        /// </summary>
        /// <param name="dicSource">待验证的集合</param>
        /// <param name="strKeyList">需验证的键清单</param>
        /// <returns>非空：不符合的键描述；空值：验证通过</returns>
        public static string GetNotNullValue(this IDictionary<string, object> dicSource, string[] strKeyList)
        {
            return JudgeNotValue(dicSource.ToStringDict(), strKeyList);
        }

        public static string GetNotNullValue(this IDictionary<string, object> dicSource, List<string> List)
        {
            return JudgeNotValue(dicSource.ToStringDict(), List.ToArray());
        }
        #endregion

        #region 判断集合中是否包括所有【需验证的键清单】的非空值
        /// <summary>
        /// 判断集合中是否包括所有【需验证的键清单】的非空值
        /// </summary>
        /// <param name="dicSource">待验证的集合</param>
        /// <param name="strKeyList">需验证的键清单</param>
        /// <returns>非空：不符合的键描述；空值：验证通过</returns>
        public static string GetNotNullValue(this IDictionary<string, string> dicSource, string[] strKeyList)
        {
            return JudgeNotValue(dicSource, strKeyList);
        }

        public static string GetNotNullValue(this IDictionary<string, string> dicSource, List<string> List)
        {
            return JudgeNotValue(dicSource, List.ToArray());
        }
        #endregion

        #region 非空判断私有方法
        private static string JudgeNotValue(IDictionary<string, string> dicSource, string[] strKeyList)
        {
            string strNotRightList = "";
            foreach (string strItem in strKeyList)
            {
                //包含键且键值不为空
                if (dicSource.ContainsKey(strItem) && !string.IsNullOrEmpty(dicSource[strItem].ToString()))
                {
                    continue;
                }
                string strItemJH = "#" + strItem + "#";
                if (dicSource.ContainsKey(strItemJH) && !string.IsNullOrEmpty(dicSource[strItemJH].ToString()))
                {
                    continue;
                }
                strNotRightList = strNotRightList + strItem + "、";

            }
            if (!string.IsNullOrEmpty(strNotRightList))
            {
                return "传入的字典集合中没有包括以下非空值的键：" + strNotRightList.Substring(0, strNotRightList.Length - 1);
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 获取LINQ动态表列的对象：用于JOIN条件或分组
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="dr"></param>
        /// <param name="isKey"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object GetLinqDynamicTableColumnObj(this IDictionary<string, string> dic, DataRow dr, bool isKey)
        {
            int iCondCount = dic.Count();
            object result = null;

            #region 1-10
            if (iCondCount == 1)
            {
                result = isKey ? new 
                { 
                    c1 = dr.Field<string>(dic.ElementAt(0).Key) 
                } : 
                new 
                { 
                    c1 = dr.Field<string>(dic.ElementAt(0).Value) 
                };
            }
            else if (iCondCount == 2)
            {
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
                result = isKey ? new
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
            #endregion

            #region 11-15
            else if (iCondCount == 11)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key)
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value)
                };
            }
            else if (iCondCount == 12)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                };
            }
            else if (iCondCount == 13)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                };
            }
            else if (iCondCount == 14)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                };
            }
            else if (iCondCount == 15)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                };
            }
            #endregion

            #region 16-20
            else if (iCondCount == 16)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                };
            }
            else if (iCondCount == 17)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                };
            }
            else if (iCondCount == 18)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                };
            }
            else if (iCondCount == 19)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                };
            }
            else if (iCondCount == 20)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                };
            }
            #endregion

            #region 21-25
            else if (iCondCount == 21)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                };
            }
            else if (iCondCount == 22)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                };
            }
            else if (iCondCount == 23)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                };
            }
            else if (iCondCount == 24)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                };
            }
            else if (iCondCount == 25)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                };
            }
            #endregion

            #region 26-30
            else if (iCondCount == 26)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                };
            }
            else if (iCondCount == 27)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                };
            }
            else if (iCondCount == 28)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                };
            }
            else if (iCondCount == 29)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                };
            }
            else if (iCondCount == 30)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                };
            }
            #endregion

            #region 31-35
            else if (iCondCount == 31)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                };
            }
            else if (iCondCount == 32)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                };
            }
            else if (iCondCount == 33)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                };
            }
            else if (iCondCount == 34)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                };
            }
            else if (iCondCount == 35)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                };
            }
            #endregion

            #region 36-40
            else if (iCondCount == 36)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                };
            }
            else if (iCondCount == 37)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                };
            }
            else if (iCondCount == 38)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                };
            }
            else if (iCondCount == 39)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                };
            }
            else if (iCondCount == 40)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                };
            }
            #endregion

            #region 41-45
            else if (iCondCount == 41)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                };
            }
            else if (iCondCount == 42)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                };
            }
            else if (iCondCount == 43)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                };
            }
            else if (iCondCount == 44)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                };
            }
            else if (iCondCount == 45)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                };
            }
            #endregion

            #region 46-50
            else if (iCondCount == 46)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                };
            }
            else if (iCondCount == 47)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                };
            }
            else if (iCondCount == 48)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                };
            }
            else if (iCondCount == 49)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                };
            }
            else if (iCondCount == 50)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                };
            }
            #endregion

            #region 51-55
            else if (iCondCount == 51)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                };
            }
            else if (iCondCount == 52)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                };
            }
            else if (iCondCount == 53)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                };
            }
            else if (iCondCount == 54)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                };
            }
            else if (iCondCount == 55)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                };
            }
            #endregion

            #region 56-60
            else if (iCondCount == 56)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
                    c56 = dr.Field<string>(dic.ElementAt(55).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                    c56 = dr.Field<string>(dic.ElementAt(55).Value),
                };
            }
            else if (iCondCount == 57)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
                    c56 = dr.Field<string>(dic.ElementAt(55).Key),
                    c57 = dr.Field<string>(dic.ElementAt(56).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                    c56 = dr.Field<string>(dic.ElementAt(55).Value),
                    c57 = dr.Field<string>(dic.ElementAt(56).Value),
                };
            }
            else if (iCondCount == 58)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
                    c56 = dr.Field<string>(dic.ElementAt(55).Key),
                    c57 = dr.Field<string>(dic.ElementAt(56).Key),
                    c58 = dr.Field<string>(dic.ElementAt(57).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                    c56 = dr.Field<string>(dic.ElementAt(55).Value),
                    c57 = dr.Field<string>(dic.ElementAt(56).Value),
                    c58 = dr.Field<string>(dic.ElementAt(57).Value),
                };
            }
            else if (iCondCount == 59)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
                    c56 = dr.Field<string>(dic.ElementAt(55).Key),
                    c57 = dr.Field<string>(dic.ElementAt(56).Key),
                    c58 = dr.Field<string>(dic.ElementAt(57).Key),
                    c59 = dr.Field<string>(dic.ElementAt(58).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                    c56 = dr.Field<string>(dic.ElementAt(55).Value),
                    c57 = dr.Field<string>(dic.ElementAt(56).Value),
                    c58 = dr.Field<string>(dic.ElementAt(57).Value),
                    c59 = dr.Field<string>(dic.ElementAt(58).Value),
                };
            }
            else if (iCondCount == 60)
            {
                result = isKey ? new
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Key),
                    c11 = dr.Field<string>(dic.ElementAt(10).Key),
                    c12 = dr.Field<string>(dic.ElementAt(11).Key),
                    c13 = dr.Field<string>(dic.ElementAt(12).Key),
                    c14 = dr.Field<string>(dic.ElementAt(13).Key),
                    c15 = dr.Field<string>(dic.ElementAt(14).Key),
                    c16 = dr.Field<string>(dic.ElementAt(15).Key),
                    c17 = dr.Field<string>(dic.ElementAt(16).Key),
                    c18 = dr.Field<string>(dic.ElementAt(17).Key),
                    c19 = dr.Field<string>(dic.ElementAt(18).Key),
                    c20 = dr.Field<string>(dic.ElementAt(19).Key),
                    c21 = dr.Field<string>(dic.ElementAt(20).Key),
                    c22 = dr.Field<string>(dic.ElementAt(21).Key),
                    c23 = dr.Field<string>(dic.ElementAt(22).Key),
                    c24 = dr.Field<string>(dic.ElementAt(23).Key),
                    c25 = dr.Field<string>(dic.ElementAt(24).Key),
                    c26 = dr.Field<string>(dic.ElementAt(25).Key),
                    c27 = dr.Field<string>(dic.ElementAt(26).Key),
                    c28 = dr.Field<string>(dic.ElementAt(27).Key),
                    c29 = dr.Field<string>(dic.ElementAt(28).Key),
                    c30 = dr.Field<string>(dic.ElementAt(29).Key),
                    c31 = dr.Field<string>(dic.ElementAt(30).Key),
                    c32 = dr.Field<string>(dic.ElementAt(31).Key),
                    c33 = dr.Field<string>(dic.ElementAt(32).Key),
                    c34 = dr.Field<string>(dic.ElementAt(33).Key),
                    c35 = dr.Field<string>(dic.ElementAt(34).Key),
                    c36 = dr.Field<string>(dic.ElementAt(35).Key),
                    c37 = dr.Field<string>(dic.ElementAt(36).Key),
                    c38 = dr.Field<string>(dic.ElementAt(37).Key),
                    c39 = dr.Field<string>(dic.ElementAt(38).Key),
                    c40 = dr.Field<string>(dic.ElementAt(39).Key),
                    c41 = dr.Field<string>(dic.ElementAt(40).Key),
                    c42 = dr.Field<string>(dic.ElementAt(41).Key),
                    c43 = dr.Field<string>(dic.ElementAt(42).Key),
                    c44 = dr.Field<string>(dic.ElementAt(43).Key),
                    c45 = dr.Field<string>(dic.ElementAt(44).Key),
                    c46 = dr.Field<string>(dic.ElementAt(45).Key),
                    c47 = dr.Field<string>(dic.ElementAt(46).Key),
                    c48 = dr.Field<string>(dic.ElementAt(47).Key),
                    c49 = dr.Field<string>(dic.ElementAt(48).Key),
                    c50 = dr.Field<string>(dic.ElementAt(49).Key),
                    c51 = dr.Field<string>(dic.ElementAt(50).Key),
                    c52 = dr.Field<string>(dic.ElementAt(51).Key),
                    c53 = dr.Field<string>(dic.ElementAt(52).Key),
                    c54 = dr.Field<string>(dic.ElementAt(53).Key),
                    c55 = dr.Field<string>(dic.ElementAt(54).Key),
                    c56 = dr.Field<string>(dic.ElementAt(55).Key),
                    c57 = dr.Field<string>(dic.ElementAt(56).Key),
                    c58 = dr.Field<string>(dic.ElementAt(57).Key),
                    c59 = dr.Field<string>(dic.ElementAt(58).Key),
                    c60 = dr.Field<string>(dic.ElementAt(59).Key),
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
                    c10 = dr.Field<string>(dic.ElementAt(9).Value),
                    c11 = dr.Field<string>(dic.ElementAt(10).Value),
                    c12 = dr.Field<string>(dic.ElementAt(11).Value),
                    c13 = dr.Field<string>(dic.ElementAt(12).Value),
                    c14 = dr.Field<string>(dic.ElementAt(13).Value),
                    c15 = dr.Field<string>(dic.ElementAt(14).Value),
                    c16 = dr.Field<string>(dic.ElementAt(15).Value),
                    c17 = dr.Field<string>(dic.ElementAt(16).Value),
                    c18 = dr.Field<string>(dic.ElementAt(17).Value),
                    c19 = dr.Field<string>(dic.ElementAt(18).Value),
                    c20 = dr.Field<string>(dic.ElementAt(19).Value),
                    c21 = dr.Field<string>(dic.ElementAt(20).Value),
                    c22 = dr.Field<string>(dic.ElementAt(21).Value),
                    c23 = dr.Field<string>(dic.ElementAt(22).Value),
                    c24 = dr.Field<string>(dic.ElementAt(23).Value),
                    c25 = dr.Field<string>(dic.ElementAt(24).Value),
                    c26 = dr.Field<string>(dic.ElementAt(25).Value),
                    c27 = dr.Field<string>(dic.ElementAt(26).Value),
                    c28 = dr.Field<string>(dic.ElementAt(27).Value),
                    c29 = dr.Field<string>(dic.ElementAt(28).Value),
                    c30 = dr.Field<string>(dic.ElementAt(29).Value),
                    c31 = dr.Field<string>(dic.ElementAt(30).Value),
                    c32 = dr.Field<string>(dic.ElementAt(31).Value),
                    c33 = dr.Field<string>(dic.ElementAt(32).Value),
                    c34 = dr.Field<string>(dic.ElementAt(33).Value),
                    c35 = dr.Field<string>(dic.ElementAt(34).Value),
                    c36 = dr.Field<string>(dic.ElementAt(35).Value),
                    c37 = dr.Field<string>(dic.ElementAt(36).Value),
                    c38 = dr.Field<string>(dic.ElementAt(37).Value),
                    c39 = dr.Field<string>(dic.ElementAt(38).Value),
                    c40 = dr.Field<string>(dic.ElementAt(39).Value),
                    c41 = dr.Field<string>(dic.ElementAt(40).Value),
                    c42 = dr.Field<string>(dic.ElementAt(41).Value),
                    c43 = dr.Field<string>(dic.ElementAt(42).Value),
                    c44 = dr.Field<string>(dic.ElementAt(43).Value),
                    c45 = dr.Field<string>(dic.ElementAt(44).Value),
                    c46 = dr.Field<string>(dic.ElementAt(45).Value),
                    c47 = dr.Field<string>(dic.ElementAt(46).Value),
                    c48 = dr.Field<string>(dic.ElementAt(47).Value),
                    c49 = dr.Field<string>(dic.ElementAt(48).Value),
                    c50 = dr.Field<string>(dic.ElementAt(49).Value),
                    c51 = dr.Field<string>(dic.ElementAt(50).Value),
                    c52 = dr.Field<string>(dic.ElementAt(51).Value),
                    c53 = dr.Field<string>(dic.ElementAt(52).Value),
                    c54 = dr.Field<string>(dic.ElementAt(53).Value),
                    c55 = dr.Field<string>(dic.ElementAt(54).Value),
                    c56 = dr.Field<string>(dic.ElementAt(55).Value),
                    c57 = dr.Field<string>(dic.ElementAt(56).Value),
                    c58 = dr.Field<string>(dic.ElementAt(57).Value),
                    c59 = dr.Field<string>(dic.ElementAt(58).Value),
                    c60 = dr.Field<string>(dic.ElementAt(59).Value),
                };
            } 
            #endregion
            
            else
            {
                throw new Exception("程序出错了，最多只能输入60个条件！");
            }
            return result;
        }

        /// <summary>
        /// 测试不成功
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="dr"></param>
        /// <param name="isKey"></param>
        /// <returns></returns>
        public static object GetLinqDynamicTableColumnObjEasy(this IDictionary<string, string> dic, DataRow dr, bool isKey)
        {
            dynamic result = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)result;

            int index = 1;
            foreach (var kvp in dic)
            {
                string columnName = isKey ? kvp.Key : kvp.Value;
                dictionary[$"c{index}"] = dr.Field<string>(columnName);
                index++;
            }

            return result;
        }

        /// <summary>
        /// 获取LINQ动态表列的拼接字符串：用于分组，测试通过
        /// </summary>
        /// <param name="dic">字段字典</param>
        /// <param name="dr">数据行</param>
        /// <param name="isKey">是否使用字典的键</param>
        /// <param name="separator">分隔符：可传空，也可自定义</param>
        /// <returns>拼接的字段值字符串</returns>
        public static string GetLinqDynamicTableColumnString(this IDictionary<string, string> dic,DataRow dr, bool isKey,ref string separator)
        {
            if (string.IsNullOrEmpty(separator))
            {
                separator = @"###___@___####____@@____#####____@@@____"; // 选择一个不易出现的分隔符
            }
            var builder = new StringBuilder(); // 使用StringBuilder拼接字符串，提高效率
            foreach (var kvp in dic)
            {
                string columnName = isKey ? kvp.Key : kvp.Value;
                string value = dr[columnName].ToString();
                builder.Append(value).Append(separator);
            }
            return builder.ToString().TrimEnd(separator.ToCharArray());
        }
    }
}
