using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 自动生成文件的工具类
    /// </summary>
    public  class AutoFileUtil
    {
        /// <summary>
        /// 确定系统动态变量
        /// </summary>
        /// <param name="dr">列定义行</param>
        /// <param name="dtSysDefine">系统变量表</param>
        /// <param name="_dicQuery"></param>
        private static void FixSysDynamicParam(DataRow dr, DataTable dtSysDefine, IDictionary<string, string> _dicQuery)
        {
            DBColumnEntity ent = DBColumnEntity.GetEntity(dr);
            //得到列相关变量
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColName] = ent.Name.FirstLetterUpper();
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColNameLcc] = ent.Name.FirstLetterUpper(false);
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColNameCn] = ent.NameCN;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbName] = ent.Name;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbType] = ent.DataType;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbLength] = ent.DataLength;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbTypeLength] = ent.DataTypeFull;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbDecimalBegin] = ent.DataPrecision;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbDecimalEnd] = ent.DataScale;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbKey] = ent.KeyType == DBColumnKeyType.PK ? "PK" : "";
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbNotNull] = ent.NotNull;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbDefaultValue] = ent.Default;
            _dicQuery[AutoImportModuleString.AutoFileSysParam.ColDbRemark] = ent.Comments;
            //替换参数值
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbName,dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbLength, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbDecimalBegin, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbDecimalEnd, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbTypeLength, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbDefaultValue, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbNotNull, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbKey, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbNameCn, dtSysDefine, _dicQuery);
            SetEntityColumnNotNullValue(AutoImportModuleString.AutoFileSysParam.SetEntColDbRemark, dtSysDefine, _dicQuery);
        }

        private static void SetEntityColumnNotNullValue(string sKey,DataTable dtSysDefine, IDictionary<string, string> _dicQuery)
        {
            DataRow[] drArr = dtSysDefine.Select(AutoImportModuleString.ColumnNameSysParam.ParamName + "='" + sKey + "'");
            if (drArr.Length > 0)
            {
                //取出变量内容
                string sContne = drArr[0][AutoImportModuleString.ColumnNameMyParam.ParamContent].ToString();
                Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                MatchCollection mc = regex.Matches(sContne);
                //得到##匹配值
                foreach (Match item in mc)
                {
                    //如果包含全局公共值，先替换
                    if (_dicQuery.ContainsKey(item.Value))
                    {
                        sContne = sContne.Replace(item.Value, _dicQuery[item.Value].ToString());
                    }
                }
                _dicQuery[sKey] = sContne; //得到最终替换值
            }
        }

        public static string GetFinalString(string sIn, DataRow dr,DataTable dtSysParam,IDictionary<string, string> _dicQuery, DataTable dtConvert)
        {
            FixSysDynamicParam(dr, dtSysParam, _dicQuery);
            string sColApi = sIn.Trim();
            foreach (string key in _dicQuery.Keys)
            {
                sColApi = sColApi.Replace(key, _dicQuery[key]);
            }

            if (dtConvert != null)
            {
                DataRow[] drArr = dtConvert.Select(AutoImportModuleString.ColumnNameTypeConvert.DbType + "='" + dr[DBColumnEntity.SqlString.DataType].ToString() + "'");
                if (drArr.Length > 0 && !string.IsNullOrEmpty(drArr[0][AutoImportModuleString.ColumnNameTypeConvert.DevLangType].ToString()))
                {
                    sColApi = sColApi.Replace(AutoImportModuleString.AutoFileSysParam.ColEntType, drArr[0][AutoImportModuleString.ColumnNameTypeConvert.DevLangType].ToString());
                }
                else
                {
                    sColApi = sColApi.Replace(AutoImportModuleString.AutoFileSysParam.ColEntType, "String");
                }
            }

            return sColApi;
        }

        /// <summary>
        /// 确定自定义变量中的固定值
        /// </summary>
        /// <param name="_dicString"></param>
        /// <param name="dtMyDefine"></param>
        public static void DealMyFixParam(IDictionary<string,string> _dicString,DataTable dtMyDefine)
        {
            if (dtMyDefine != null && dtMyDefine.Rows.Count > 0)
            {
                DataRow[] drArrMy = dtMyDefine.Select(AutoImportModuleString.ColumnNameMyParam.ChangeType + "='1'");

                foreach (DataRow dr in drArrMy)
                {
                    string sContne = dr[AutoImportModuleString.ColumnNameMyParam.ParamContent].ToString();
                    Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                    MatchCollection mc = regex.Matches(sContne);
                    foreach (Match item in mc)
                    {
                        if (_dicString.ContainsKey(item.Value))
                        {
                            sContne = sContne.Replace(item.Value, _dicString[item.Value]);
                        }
                    }
                    _dicString[dr[AutoImportModuleString.ColumnNameMyParam.ParamName].ToString()] = sContne;
                }

            }
        }

        /// <summary>
        /// 确定自定义的动态变量值
        /// </summary>
        /// <param name="_dicString"></param>
        /// <param name="dtMyDefineDynamic">自定义动态变量表</param>
        /// <param name="dtColumnList">列清单表</param>
        public static void FixMyDynamicParam(IDictionary<string, string> _dicString, DataTable dtMyDefineDynamic, DataTable dtColumnList, DataTable dtSysParam, bool isNeedReset)
        {
            if (dtMyDefineDynamic != null && dtMyDefineDynamic.Rows.Count > 0)
            {
                DataRow[] drArrMy = dtMyDefineDynamic.Select(AutoImportModuleString.ColumnNameMyParam.ChangeType + "='2'");
                StringBuilder sbMy;
                //循环自定义的动态值
                foreach (DataRow drDynamic in drArrMy)
                {
                    sbMy = new StringBuilder();
                    //取出变量内容
                    string sContne = drDynamic[AutoImportModuleString.ColumnNameMyParam.ParamContent].ToString();
                    string sConnString = drDynamic[AutoImportModuleString.ColumnNameMyParam.ConcatString].ToString();
                    Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                    MatchCollection mc = regex.Matches(sContne);

                    if (dtColumnList == null)
                    {
                        //得到##匹配值
                        foreach (Match item in mc)
                        {
                            //如果包含全局公共值，先替换
                            if (_dicString.ContainsKey(item.Value))
                            {
                                sContne = sContne.Replace(item.Value, _dicString[item.Value].ToString());
                            }
                        }
                        //得到最终动态值
                        _dicString[drDynamic[AutoImportModuleString.ColumnNameMyParam.ParamName].ToString()] = sContne;
                        return;
                    }

                    //循环列清单
                    int i = 0;
                    foreach (DataRow dr in dtColumnList.Rows)
                    {
                        //得到列所有转换数据
                        FixSysDynamicParam(dr, dtSysParam, _dicString);
                        //得到##匹配值
                        foreach (Match item in mc)
                        {
                            //如果包含全局公共值，先替换
                            if (_dicString.ContainsKey(item.Value))
                            {
                                sContne = sContne.Replace(item.Value, _dicString[item.Value].ToString());
                            }
                        }

                        if (i > 0)
                        {
                            sContne = sConnString + sContne;
                        }
                        i++;
                        //还原为初始值
                        if (string.IsNullOrEmpty(sConnString))
                        {
                            sbMy.AppendLine(sContne);
                        }
                        else
                        {
                            sbMy.Append(sContne);
                        }
                        sContne = drDynamic[AutoImportModuleString.ColumnNameMyParam.ParamContent].ToString();
                    }
                    //得到最终动态值
                    _dicString[drDynamic[AutoImportModuleString.ColumnNameMyParam.ParamName].ToString()] = sbMy.ToString();

                }

            }
        }
    }
}
