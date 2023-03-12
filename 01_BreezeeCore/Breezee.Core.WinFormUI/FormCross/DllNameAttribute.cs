using System;
using System.Collections.Generic;
using System.Linq;
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
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// DLL属性类：指定窗体的Dll文件名
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class DllNameAttribute : Attribute
    {
        static Dictionary<Type, DllNameAttribute> dllNameCache = new Dictionary<Type, DllNameAttribute>();

        /// <summary>
        /// 用指定的Dll文件名创建DllNameAttribute的实例
        /// </summary>
        /// <param name="dllName">Dll文件名，如：LY.MDS.UI.SE.Common.dll</param>
        public DllNameAttribute(string dllName)
        {
            if (string.IsNullOrEmpty(dllName))
            {
                throw new ArgumentNullException("dllName");
            }

            this.DllName = dllName;
        }

        /// <summary>
        /// 返回Dll文件名
        /// </summary>
        public string DllName { get; private set; }

        /// <summary>
        /// 查找指定类型接口的DllName特性
        /// </summary>
        /// <typeparam name="T">接口</typeparam>
        /// <returns></returns>
        public static DllNameAttribute Find<T>()
        {
            Type t = typeof(T);
            if (!dllNameCache.ContainsKey(t))
            {
                object[] result = t.GetCustomAttributes(typeof(DllNameAttribute), false);
                if (result != null && result.Length > 0)
                {
                    dllNameCache[t] = (DllNameAttribute)result[0];
                }
                else
                {
                    dllNameCache[t] = null;
                }
            }

            return dllNameCache[t];
        }
    }
}
