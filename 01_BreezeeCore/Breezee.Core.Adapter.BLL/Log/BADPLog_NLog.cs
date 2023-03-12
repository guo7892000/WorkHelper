using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Core.Adapter.IBLL;
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
namespace Breezee.Core.Adapter.BLL
{
    /// <summary>
    /// 使用NLog日志的实现
    /// </summary>
    public class BADPLog_NLog : IADPLog
    {
        #region 使用NLog日志
        public void LogError(string strInfo)
        {
            LogManager.GetCurrentClassLogger().Error(strInfo);
        }

        public void LogInfo(string strInfo)
        {
            LogManager.GetCurrentClassLogger().Info(strInfo);
        }

        public void LogWarn(string strInfo)
        {
            LogManager.GetCurrentClassLogger().Warn(strInfo);
        }

        public void LogTrace(string strInfo)
        {
            LogManager.GetCurrentClassLogger().Trace(strInfo);
        }

        public void LogDebug(string strInfo)
        {
            LogManager.GetCurrentClassLogger().Debug(strInfo);
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
        public void WriteDBLog(IADPLogLevel levle, string appName, string moduleName, string procName, string logLevel, string logTitle, string logMessage, string strREMARK)
        {
            LogLevel nLogLevel = LogLevel.Info;
            switch (levle)
            {
                case IADPLogLevel.Debug:
                    nLogLevel = LogLevel.Debug;
                    break;
                case IADPLogLevel.Info:
                    nLogLevel = LogLevel.Info;
                    break;
                case IADPLogLevel.Warn:
                    nLogLevel = LogLevel.Warn;
                    break;
                case IADPLogLevel.Error:
                    nLogLevel = LogLevel.Error;
                    break;
                case IADPLogLevel.Fatal:
                    nLogLevel = LogLevel.Fatal;
                    break;
                case IADPLogLevel.Off:
                    nLogLevel = LogLevel.Off;
                    break;
                default:
                    nLogLevel = LogLevel.Info;
                    break;
            }
            LogEventInfo ei = new LogEventInfo(nLogLevel, "测试日志", "测试内容（...）");
            ei.Properties["appName"] = appName;
            ei.Properties["moduleName"] = moduleName;
            ei.Properties["procName"] = procName;
            ei.Properties["logLevel"] = logLevel.ToUpper();
            ei.Properties["logTitle"] = logTitle;
            ei.Properties["logMessage"] = logMessage;
            ei.Properties["remark"] = strREMARK;
            LogManager.GetCurrentClassLogger().Log(ei);
        }
        #endregion
    }
}
