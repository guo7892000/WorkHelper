using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 全局键
    /// </summary>
    public class GlobalKey
    {
        //窗体样式配置
        public static string MainSkinType = "MainSkinType";
        public static string MainSkinValue = "MainSkinValue";
        public static string MainSkinColorName = "MainSkinColorName";
        public static string CommonSkinType = "CommonSkinType";
        public static string CommonSkinValue = "CommonSkinValue";
        public static string CommonSkinColorName = "CommonSkinColorName";
        //网格样式
        public static string IsUsedMyDefineGridHeaderStyle = "IsUsedMyDefineGridHeaderStyle";//是否使用自定义网格头样式
        public static string GridHeaderHeight = "GridHeaderHeight";//网格头高度
        public static string GridHeaderBackColor = "GridHeaderBackColor";//网格头背景色
        public static string GridOddRowBackColor = "GridOddRowBackColor";//奇数行背景色
        //保存提示类型
        public static string SavePromptType = "SavePromptType";
        //配置目录配置
        public static string ConfigPathKey = "ConfigPath";
        //升级配置
        public static string Upgrade_IsAutoCheckVersion = "Upgrade_IsAutoCheckVersion"; //是否自动检测新版本
        public static string Upgrade_IsDeleteOldVersion = "Upgrade_IsDeleteOldVersion"; //是否升级新版本成功后删除旧版本
        public static string Upgrade_IsDeleteOldVersionNeedConfirm = "Upgrade_IsDeleteOldVersionNeedConfirm"; //是否升级新版本成功后删除旧版本需要确认？
        public static string Upgrade_IsDeleteNewVerZipFile = "Upgrade_IsDeleteNewVerZipFile"; //是否升级新版本成功后删除新版本的压缩包
        public static string Upgrade_TempPath = "Upgrade_TempPath"; //升级临时文件路径
        public static string Upgrade_PreVersionPath = "Upgrade_PreVersionPath"; //上个版本的路径
        public static string Upgrade_LatestVersionRootDir = "Upgrade_LatestVersionRootDir"; //最新版的根目录
        public static string Upgrade_IsAutoCorrectDesktopQuickLink = "Upgrade_IsAutoCorrectDesktopQuickLink"; //是否自动修正桌面快捷方式路径
        //行号
        public static string RowNum = "ROWNO";
        public static string MaxOpenFormNum = "MaxOpenFormNum";
        //日志配置
        public static string GlobalLog_IsEnableLog= "GlobalLog_IsEnableLog"; //是否启用日志：1是，0否
        public static string GlobalLog_LogPath = "GlobalLog_LogPath"; //日志路径
        public static string GlobalLog_KeepDays = "GlobalLog_KeepDays"; //日志保留天数
        public static string GlobalLog_AppendType = "GlobalLog_AppendType"; //追加方式：1插入到开始位置，2追加到末层
        //正常SQL日志配置
        public static string OkSqlLog_IsEnableLog = "OkSqlLog_IsEnableLog"; //是否启用日志：1是，0否
        public static string OkSqlLog_LogPath = "OkSqlLog_LogPath"; //日志路径
        public static string OkSqlLog_KeepDays = "OkSqlLog_KeepDays"; //日志保留天数
        public static string OkSqlLog_AppendType = "OkSqlLog_AppendType"; //追加方式：1插入到开始位置，2追加到末层
        //异常SQL日志配置
        public static string ErrSqlLog_IsEnableLog = "ErrSqlLog_IsEnableLog"; //是否启用日志：1是，0否
        public static string ErrSqlLog_LogPath = "ErrSqlLog_LogPath"; //日志路径
        public static string ErrSqlLog_KeepDays = "ErrSqlLog_KeepDays"; //日志保留天数
        public static string ErrSqlLog_AppendType = "ErrSqlLog_AppendType"; //追加方式：1插入到开始位置，2追加到末层
    }
}
