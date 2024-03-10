using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 最新版本信息
    /// 下载的优先级：
    /// 1、最新版发布路径：包含版本号的压缩包，目前也是放在Gitee
    /// 2、发布路径-Gitee：包含版本号的压缩包
    /// 3、发布路径-Github：包含版本号的压缩包
    /// </summary>
    [Serializable]
    public class LatestVerion
    {
        /// <summary>
        /// 最新版本
        /// </summary>
        public string version;
        /// <summary>
        /// 发布日期
        /// </summary>
        public string date;
        /// <summary>
        /// 最新版发布路径
        /// </summary>
        public string downUrlPublishLatest;
        /// <summary>
        /// 发布路径-Gitee
        /// </summary>
        public string downUrlPublishGitee;
        /// <summary>
        /// 发布路径-Github
        /// </summary>
        public string downUrlPublishGithub;

        /// <summary>
        /// 稳定版-版本号
        /// </summary>
        public string stableVersion;
        /// <summary>
        /// 稳定版-发布日期
        /// </summary>
        public string stableDate;
        /// <summary>
        /// 稳定版-发布路径
        /// </summary>
        public string stableDownUrlPublish;

    }
}
