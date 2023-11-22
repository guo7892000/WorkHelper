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
    /// 1、发布路径-Gitee：包含版本号的压缩包
    /// 2、发布路径-Github：包含版本号的压缩包
    /// 3、发布路径-Gitlab：包含版本号的压缩包
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
        /// 发布路径-Gitee
        /// </summary>
        public string downUrlPublishGitee;
        /// <summary>
        /// 发布路径-Github
        /// </summary>
        public string downUrlPublishGithub;
        /// <summary>
        /// 发布路径-Gitlab
        /// </summary>
        public string downUrlPublishGitlab;
    }
}
