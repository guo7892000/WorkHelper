using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Breezee.Core.Tool
{
    /// <summary>
    /// 实体辅助类(无用)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReflectHelper
    {
        public static MethodInfo GetMethod<T>(string sMethodName) where T:class
        {
            Type typeInfo = typeof(T);
            MethodInfo methodInfo = typeInfo.GetMethod(sMethodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            return methodInfo;
        }

        public static EventInfo GetEvent<T>(string sEventName) where T : class
        {
            Type typeInfo = typeof(T);
            EventInfo EventInfo = typeInfo.GetEvent(sEventName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            return EventInfo;
        }
    }
}
