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
    /// 1、发布路径：包含版本号的压缩包
    /// 2、发布备份路径：包含版本号的压缩包
    /// 3、开发生成的目录：是目录名，没有版本号。本地需要创建目录名，并下载该目录所有文件
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
        /// 发布路径
        /// </summary>
        public string downUrlPublish;
        /// <summary>
        /// 发布备份路径
        /// </summary>
        public string downUrlPublishBak;
        /// <summary>
        /// 开发生成的目录
        /// </summary>
        public string downUrlRelease;
    }
}
