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
namespace Breezee.Core.Adapter.IBLL
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public abstract class IADPCacheManage : IBaseRefOuter
    {
        /*使用示例
         var cache = CacheFactory.Build("getStartedCache", settings =>
         {
             settings.WithSystemRuntimeCacheHandle("handleName");
         });
 
         cache.Add("keyA", "valueA");
         cache.Put("keyB", 23);
         cache.Update("keyB", v => 42);
         Console.WriteLine("KeyA is " + cache.Get("keyA"));      // should be valueA
         Console.WriteLine("KeyB is " + cache.Get("keyB"));      // should be 42
         cache.Remove("keyA");
         Console.WriteLine("KeyA removed? " + (cache.Get("keyA") == null).ToString());
         Console.WriteLine("We are done...");
         Console.ReadKey();
        */
        public abstract IADPCacheManage CacheManage { get; }

        public IADPCacheManage()
        {
            Init();
        }
        protected abstract IADPCacheManage Init();

        public abstract bool Exists(string sKey);
        public abstract bool Exists(string sKey, string region);

        public abstract object Get(string sKey);
        public abstract object Get(string sKey, string region);

        public abstract object GetOrAdd(string sKey, object value);

        public abstract object GetOrAdd(string sKey, string region, object value);
        public abstract void Put(string sKey, object value);

        public abstract void Put(string sKey, object value, string region);

        public abstract object Update(string sKey, Func<object, object> updateValue);

        public abstract object Update(string sKey, string region, Func<object, object> updateValue);

        public abstract object AddOrUpdate(string sKey, object addValue, Func<object, object> updateValue);
        public abstract object AddOrUpdate(string sKey, string region, object addValue, Func<object, object> updateValue);

        public abstract bool Remove(string sKey);
        public abstract bool Remove(string sKey, string region);

        public abstract bool TryGetOrAdd(string sKey, Func<string, object> addValue, out object value);

        public abstract bool TryUpdate(string sKey, Func<object, object> updateValue, out object value);


    }
}
