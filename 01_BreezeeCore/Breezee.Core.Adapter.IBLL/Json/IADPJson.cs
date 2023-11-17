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
namespace Breezee.Core.Adapter.IBLL
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public interface IADPJson
    {
        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T Deserialize<T>(string json) where T : class;

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        T Deserialize<T>(string json, out string errMsg) where T : class;

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON数据</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        object Deserialize(string json, Type type);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">JSON数据</param>
        /// <param name="type">类型</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        object Deserialize(string json, Type type, out string errMsg);

        /// <summary>
        /// 序列化到json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        string Serialize<T>(T entity) where T : class;

        /// <summary>
        /// 将datatable转换为json  
        /// </summary>
        /// <param name="dt">Dt</param>
        /// <returns>JSON字符串</returns>
        string DataTableToJson(DataTable dt);

        /// <summary>    
        /// 将获取的Json数据转换为DataTable    
        /// </summary>    
        /// <param name="strJson">Json字符串</param>   
        /// <returns></returns>    
        DataTable JsonToDataTable(string json);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        List<T> CollectDeserialize<T>(string json) where T : class;
    }
}
