using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Tool
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {
        #region 使用NLog日志相关
        public static Logger NLog = LogManager.GetCurrentClassLogger();

        public static void NLogError(string strInfo)
        {
            NLog.Error(strInfo);
        }

        public static void NLogInfo(string strInfo)
        {
            NLog.Info(strInfo);
        }

        public static void NLogWarn(string strInfo)
        {
            NLog.Warn(strInfo);
        }

        public static void NLogTrace(string strInfo)
        {
            NLog.Trace(strInfo);
        }

        public static void NLogDebug(string strInfo)
        {
            NLog.Debug(strInfo);
        }

        /// <summary>
        /// 写入数据库（有自定义变量）
        /// </summary>
        /// <param name="levle"></param>
        /// <param name="appName"></param>
        /// <param name="moduleName"></param>
        /// <param name="procName"></param>
        /// <param name="logLevel"></param>
        /// <param name="logTitle"></param>
        /// <param name="logMessage"></param>
        /// <param name="strREMARK"></param>
        public static void WriteNLogByDataBase(LogLevel levle, string appName, string moduleName, string procName, string logLevel, string logTitle, string logMessage, string strREMARK)
        {
            LogEventInfo ei = new LogEventInfo(levle, "测试日志", "测试内容（...）");
            ei.Properties["appName"] = appName;
            ei.Properties["moduleName"] = moduleName;
            ei.Properties["procName"] = procName;
            ei.Properties["logLevel"] = logLevel.ToUpper();
            ei.Properties["logTitle"] = logTitle;
            ei.Properties["logMessage"] = logMessage;
            ei.Properties["remark"] = strREMARK;
            NLog.Log(ei);
        }
        #endregion
    }
}
