using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// 运行时的相关配置
    /// 注：跟用户无关
    /// </summary>
    public class RunningConfig
    {
        /// <summary>
        /// 是否使用验证码
        /// </summary>
        public bool IsUseVerificationCode;

        /// <summary>
        /// 数据库环境类型
        /// </summary>
        public string DbEnvType;

        /// <summary>
        /// 是否菜单使用统计
        /// </summary>
        public bool IsMenuUseCount;

        /// <summary>
        /// 是否SQL计时统计
        /// </summary>
        public bool IsSqlTimeCount;
    }
}
