using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Entity;
using Breezee.Core.Interface;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// 实体辅助类(无用)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityHelper<T> where T : DbEntity
    {
        /// <summary>
        /// 设置实体默认值方法
        /// </summary>
        /// <param name="dn">IBaseEntity接口对象</param>
        /// <param name="su">用户对象</param>
        /// <returns></returns>
        public static T SetDefalutValue(T dn, SYS_USER su)
        {
            if (dn == null)
            {
                throw new Exception("传入实体不能为空！");
            }
            //IFrameCommon obj = ContainerContext.Container.Resolve<IFrameCommon>();
            //DateTime dtNowTime = obj.GetServerDateTime();
            //dn.CREATE_TIME = dtNowTime;
            //dn.CREATOR_ID = su.USER_ID;
            //dn.CREATOR = su.USER_NAME;
            //dn.IS_ENABLED = "1";
            //dn.IS_SYSTEM = "0";
            //dn.LAST_UPDATED_TIME = dtNowTime;
            //dn.MODIFIER = su.USER_NAME;
            //dn.MODIFIER_ID = su.USER_ID;
            //dn.ORG_ID = su.ORG_ID;
            //dn.SORT_ID = 1;
            //dn.TFLAG = "0";
            //dn.UPDATE_CONTROL_ID = Guid.NewGuid().ToString();
            return dn;
        }
    }
}
