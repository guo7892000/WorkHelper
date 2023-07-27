using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 全局上下文
    /// </summary>
    public class GlobalContext
    {
        #region 只读配置路径
        /// <summary>
        /// 入口路径(不可变)
        /// </summary>
        public static readonly string AppEntryAssemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        /// <summary>
        /// 基目录(不可变)
        /// </summary>
        public static readonly string AppBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 启动配置路径(不可变)
        /// </summary>
        public static readonly string AppStartConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalDir.App);
        #endregion

        #region 可变配置路径
        /// <summary>
        /// 配置根路径(可变)
        /// 默认为用户数据目录/应用名
        /// </summary>
        public static string AppRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalDir.App);

        /// <summary>
        /// 修改配置文件根目录
        /// </summary>
        /// <returns></returns>
        public static void PathConfigRoot(string path)
        {
            AppRootPath = path;
        }

        /// <summary>
        /// 获取临时文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathConfig()
        {
            return Path.Combine(AppRootPath, GlobalDir.Config);
        }

        /// <summary>
        /// 获取数据文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathData()
        {
            return Path.Combine(AppRootPath, GlobalDir.Data);
        }

        /// <summary>
        /// 获取临时文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathTemp()
        {
            return Path.Combine(PathConfig(), GlobalDir.Temp);
        }

        /// <summary>
        /// 获取数据库配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathDb()
        {
            return Path.Combine(PathConfig(), GlobalDir.Db);
        }

        /// <summary>
        /// 获取网格样式配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathGridStyle()
        {
            return Path.Combine(PathTemp(), GlobalDir.GridStyle);
        }

        #endregion

    }
}
