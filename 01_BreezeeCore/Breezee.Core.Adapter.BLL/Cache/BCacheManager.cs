using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Core;
using Breezee.Core.Adapter.IBLL;

namespace Breezee.Core.Adapter.BLL
{
    public class BADPCacheManager : IADPCacheManage
    {
        #region 变量
        private ICacheManager<object> manager = null;
        public override IADPCacheManage CacheManage { get { return this; } }
        #endregion

        protected override IADPCacheManage Init()
        {
            manager = CacheFactory.Build("getStartedCache", settings =>
                {
                    object value = settings.WithSystemRuntimeCacheHandle("handleName")

                    .And
                    .WithRedisConfiguration("redis", config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            .WithEndpoint("localhost", 6379);
                    })
                    .WithMaxRetries(100)
                    .WithRetryTimeout(50)
                    .WithRedisBackplane("redis")
                    .WithRedisCacheHandle("redis", true)
                    ;
                });
            return this;
        }

        public override bool Exists(string sKey)
        {
            return manager.Exists(sKey);
        }

        public override bool Exists(string sKey, string region)
        {
            return manager.Exists(sKey, region);
        }

        public override object Get(string sKey)
        {
            return manager.Get(sKey);
        }

        public override object Get(string sKey, string region)
        {
            return manager.Get(sKey, region);
        }

        public override object GetOrAdd(string sKey, object value)
        {
            return manager.GetOrAdd(sKey, value);
        }

        public override object GetOrAdd(string sKey, string region, object value)
        {
            return manager.GetOrAdd(sKey, region, value);
        }

        public override void Put(string sKey, object value)
        {
            manager.Put(sKey, value);
        }

        public override void Put(string sKey, object value, string region)
        {
            manager.Put(sKey, value, region); 
        }

        public override object Update(string sKey, Func<object, object> updateValue)
        {
            return manager.Update(sKey, updateValue);
        }

        public override object Update(string sKey, string region, Func<object, object> updateValue)
        {
            return manager.Update(sKey, region,updateValue);
        }

        public override object AddOrUpdate(string sKey, object addValue, Func<object, object> updateValue)
        {
            return manager.AddOrUpdate(sKey, addValue, updateValue);
        }
        public override object AddOrUpdate(string sKey, string region, object addValue, Func<object, object> updateValue)
        {
            return manager.AddOrUpdate(sKey, region, addValue,updateValue);
        }

        public override bool Remove(string sKey)
        {
            return manager.Remove(sKey);
        }
        public override bool Remove(string sKey, string region)
        {
            return manager.Remove(sKey, region);
        }

        public override bool TryGetOrAdd(string sKey, Func<string, object> addValue,out object value)
        {
            return manager.TryGetOrAdd(sKey, addValue,out value);
        }

        public override bool TryUpdate(string sKey, Func<object, object> updateValue, out object value)
        {
            return manager.TryUpdate(sKey, updateValue, out value);
        }

    }


}
