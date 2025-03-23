using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Tool
{
    /// <summary>
    /// 最新版本配置信息
    /// 下载的优先级：
    /// 1、最新版发布路径：包含版本号的压缩包，目前也是放在Gitee
    /// 2、发布路径-Gitee：包含版本号的压缩包
    /// 3、发布路径-Github：包含版本号的压缩包
    /// </summary>
    [Serializable]
    public class LatestVerionConfig
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
        /// 应用名
        /// </summary>
        public string appName;
        /// <summary>
        /// 发布包后缀格式字符：支持逗号、分号、竖杠|、冒号分割
        /// </summary>
        public string zipFormats;
        /// <summary>
        /// 发布说明
        /// </summary>
        public string publishRemark;
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
        /// 稳定版-发布说明
        /// </summary>
        public string stableRemark;
        /// <summary>
        /// 稳定版-发布路径
        /// </summary>
        public string stableDownUrlPublish;

    }
}
