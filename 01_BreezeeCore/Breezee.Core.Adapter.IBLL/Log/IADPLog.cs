using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
namespace Breezee.Core.Adapter.IBLL
{
    /// <summary>
    /// 日志适配接口
    /// </summary>
    public interface IADPLog: IBaseRefOuter
    {
        #region 使用NLog日志相关
        void LogError(string strInfo);

        void LogInfo(string strInfo);

        void LogWarn(string strInfo);

        void LogTrace(string strInfo);

        void LogDebug(string strInfo);

        void WriteDBLog(IADPLogLevel levle, string appName, string moduleName, string procName, string logLevel, string logTitle, string logMessage, string strREMARK);
        #endregion
    }

    /// <summary>
    /// IADPLogLevel
    /// </summary>
    public enum IADPLogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        Off
    }
}
