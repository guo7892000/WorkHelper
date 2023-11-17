using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;

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
    }
}
