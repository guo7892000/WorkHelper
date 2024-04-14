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
        public static readonly string AppEntryAssemblyPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        /// <summary>
        /// 基目录(不可变)
        /// </summary>
        public static readonly string AppBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 启动配置路径(不可变)
        /// </summary>
        public static readonly string AppStartConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalDir.App);
        #endregion

        #region 根路径(可变)
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
        #endregion

        #region 根目录下的第一层子目录
        /// <summary>
        /// 获取数据文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathData()
        {
            return Path.Combine(AppRootPath, GlobalDir.Data);
        }

        /// <summary>
        /// 获取日志文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathLog()
        {
            return Path.Combine(AppRootPath, GlobalDir.Log);
        }

        /// <summary>
        /// 获取临时文件配置目录
        /// </summary>
        /// <returns></returns>
        public static string PathTemp()
        {
            return Path.Combine(AppRootPath, GlobalDir.Temp);
        }
        #endregion

        #region 相对子目录
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

        /// <summary>
        /// 运行目录下的配置文件目录（Mini）
        /// </summary>
        /// <returns></returns>
        public static string RunPathMiniData()
        {
            return Path.Combine(AppBaseDirectory, GlobalDir.Config, GlobalDir.Mini, GlobalDir.Data);
        } 
        #endregion
    }
}
