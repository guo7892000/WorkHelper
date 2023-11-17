using Breezee.Core.Adapter.IBLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

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
namespace Breezee.Core.Adapter.BLL
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public class BADPJson_Newtonsoft: IADPJson
    {
        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch { }
            return default(T);
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        public T Deserialize<T>(string json, out string errMsg) where T : class
        {
            errMsg = string.Empty;
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return default(T);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON数据</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON数据</param>
        /// <param name="type">类型</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        public object Deserialize(string json, Type type, out string errMsg)
        {
            try
            {
                errMsg = string.Empty;
                return JsonConvert.DeserializeObject(json, type);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return null;
        }

        /// <summary>
        /// 序列化到json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Serialize<T>(T entity) where T : class
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(entity, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 将datatable转换为json  
        /// </summary>
        /// <param name="dt">Dt</param>
        /// <returns>JSON字符串</returns>
        public string DataTableToJson(DataTable dt)
        {
            string sJson = JsonConvert.SerializeObject(dt);
            return sJson;
        }

        /// <summary>    
        /// 将获取的Json数据转换为DataTable    
        /// </summary>    
        /// <param name="strJson">Json字符串</param>   
        /// <returns></returns>    
        public DataTable JsonToDataTable(string json)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            return dt;
        }

        /// <summary>
        /// 反序列化为对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<T> CollectDeserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
