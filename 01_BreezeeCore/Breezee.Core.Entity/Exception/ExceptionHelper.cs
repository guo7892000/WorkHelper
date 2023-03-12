using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Breezee.Core.Entity;

/***************************************
 * 对象名称：异常处理辅助类
 * 对象类别：异常类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：异常处理辅助类
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public class ExceptionHelper 
    {
        #region 对发生的异常进行友好提示转换
        /// <summary>
        /// 对发生的异常进行友好提示转换
        /// </summary>
        /// <author>hgh</author>
        /// <param name="ex">异常信息</param>
        /// <param name="iRes">直接添加结果信息</param>
        /// <returns></returns>
        public static IDictionary<string, object> ExceptioinManage(Exception ex, IDictionary<string, object> iRes)
        {
            IDictionary<string, string> idic = ExceptioinManage(ex);
            foreach (KeyValuePair<string, string> ikey in idic)
            {
                iRes[ikey.Key] = ikey.Value;
            }
            return iRes;
        } 
        #endregion

        #region 对发生的异常进行友好提示转换
        /// <summary>
        /// 对发生的异常进行友好提示转换
        /// </summary>
        /// <author>hgh</author>
        /// <param name="ex">异常信息</param>
        /// <param name="iRes">直接添加结果信息</param>
        /// <returns></returns>
        public static IDictionary<string, string> ExceptioinManage(Exception ex, IDictionary<string, string> iRes)
        {
            IDictionary<string, string> idic = ExceptioinManage(ex);
            foreach (KeyValuePair<string, string> ikey in idic)
            {
                iRes[ikey.Key] = ikey.Value;
            }
            return iRes;
        } 
        #endregion

        #region 对发生的异常进行友好提示转换
        /// <summary>
        /// 对发生的异常进行友好提示转换
        /// </summary>
        /// <author>hgh</author>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static IDictionary<string, string> ExceptioinManage(Exception ex)
        {
            IDictionary<string, string> idic = new Dictionary<string, string>();
            idic[StaticConstant.FRA_RETURN_FLAG] = "0";
            idic[StaticConstant.FRA_EXCEPTION] = ex.Message;

            //if (ex.GetType() == typeof(System.Data.OracleClient.OracleException))
            //{
            //    idic[StaticConstant.FRA_ERR_TYPE] = "ORACLE数据库异常";
            //    idic[StaticConstant.FRA_ERR_MSG] = ResolveOracleException(ex);
            //}
            //else 
            if (ex.GetType() == typeof(ArgumentException))
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "应用程序异常";
                if (!string.IsNullOrWhiteSpace(ex.Message))
                {
                    idic[StaticConstant.FRA_ERR_MSG] = ex.Message;
                }
                else
                {
                    idic[StaticConstant.FRA_ERR_MSG] = "方法的参数是非法的";
                }

            }
            else if (ex.GetType() == typeof(ArgumentNullException))
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "应用程序异常";
                idic[StaticConstant.FRA_ERR_MSG] = "一个空参数传递给方法，该方法不能接受该参数";
            }
            else if (ex.GetType() == typeof(ArgumentOutOfRangeException))
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "应用程序异常";
                idic[StaticConstant.FRA_ERR_MSG] = "参数值超出范围";
            }
            else if (ex.GetType() == typeof(ArithmeticException))
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "应用程序异常";
                idic[StaticConstant.FRA_ERR_MSG] = "出现算术上溢或者下溢";
            }
            else if (ex is ConcurrencyException)
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "数据并发异常";
                idic[StaticConstant.FRA_ERR_MSG] = "该数据记录已被修改，请重新加载后再进行修改";
            }
            else if (ex is BusinessException)
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "业务逻辑层异常";
                idic[StaticConstant.FRA_ERR_MSG] = ex.Message;
                
            }
            else if (ex is DataAccessException)
            {
                idic[StaticConstant.FRA_ERR_TYPE] = "数据访问层异常";
                idic[StaticConstant.FRA_ERR_MSG] = ex.Message;
            }
            else
            {
                idic[StaticConstant.FRA_EXCEPTION] = "未做定义的异常";
                idic[StaticConstant.FRA_ERR_MSG] = ex.Message;
            }

            #region 错误说明
            //SystemException 其他用户可处理的异常的基本类 
            //ArgumentException 方法的参数是非法的 
            //ArgumentNullException 一个空参数传递给方法，该方法不能接受该参数 
            //ArgumentOutOfRangeException 参数值超出范围 
            //ArithmeticException 出现算术上溢或者下溢 
            //ArrayTypeMismatchException 试图在数组中存储错误类型的对象 
            //BadImageFormatException 图形的格式错误 
            //DivideByZeroException 除零异常 
            //DllNotFoundException 找不到引用的DLL 
            //FormatException 参数格式错误 
            //IndexOutOfRangeException 数组索引超出范围 
            //InvalidcastException 使用无效的类 
            //InvalidOperationException 方法的调用时间错误 
            //MethodAccessException 试图访问思友或者受保护的方法 
            //MissingMemberException 访问一个无效版本的DLL 
            //NotFiniteNumberException 对象不是一个有效的成员 
            //NotSupportedException 调用的方法在类中没有实现 
            //NullReferenceException 试图使用一个未分配的引用 
            //OutOfMemoryException 内存空间不够 
            //PlatformNotSupportedException 平台不支持某个特定属性时抛出该错误 
            //StackOverflowException 堆栈溢出  
            #endregion

            //错误信息赋值
            idic[StaticConstant.FRA_USER_MSG] = "系统出现异常，请联系管理员！异常信息：" + Environment.NewLine + idic[StaticConstant.FRA_ERR_MSG];
            //写日志
            //LogHelper.Error(ex.Message);
            //返回
            return idic;
        } 
        #endregion

        #region 直接返回异常友好提示信息
        /// <summary>
        /// 直接返回异常友好提示信息
        /// </summary>
        /// <author>hgh</author>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static string ExceptioinMessage(Exception ex)
        {
            IDictionary<string, string> idic = ExceptioinManage(ex);
            return idic[StaticConstant.FRA_ERR_MSG];
        } 
        #endregion

        #region 解析ORACLE数据返回的异常，进行友好转换
        /// <summary>
        /// 解析ORACLE数据返回的异常，进行友好转换
        /// </summary>
        /// <author>hgh</author>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        public static string ResolveOracleException(Exception ex)
        {
            string reStr = "";

            string checkmsg = ex.Message.ToUpper().Substring(0, ex.Message.ToUpper().IndexOf(":"));
            switch (checkmsg)
            {
                case "ORA-00001":
                    reStr = "数据库字段存储重复，主键唯一值冲突";
                    break;
                case "ORA-00017":
                    reStr = "请求会话以设置跟踪事件";
                    break;
                case "ORA-00018":
                    reStr = "超出最大会话数";
                    break;
                case "ORA-00019":
                    reStr = "超出最大会话许可数";
                    break;
                case "ORA-00020":
                    reStr = "超出最大进程数";
                    break;
                case "ORA-00021":
                    reStr = "会话附属于其它某些进程,无法转换会话";
                    break;
                case "ORA-00022":
                    reStr = "无效的会话 ID,访问被拒绝";
                    break;
                case "ORA-00051":
                    reStr = "等待资源超时";
                    break;
                case "ORA-00052":
                    reStr = "超出最大入队资源数";
                    break;
                case "ORA-00053":
                    reStr = "超出最大入队数";
                    break;
                case "ORA-00904":
                    reStr = "invalid column name 无效列名";
                    break;
                case "ORA-00942":
                    reStr = "table or view does not exist 表或者视图不存在";
                    break;
                case "ORA-01400":
                    reStr = "cannot insert NULL into () 不能将空值插入";
                    break;
                case "ORA-00936":
                    reStr = "缺少表达式";
                    break;
                case "ORA-00933":
                    reStr = "SQL 命令未正确结束";
                    break;
                case "ORA-01722":
                    reStr = "无法将字符串转化成有效数字";
                    break;
                case "ORA-06530":
                    reStr = "企图将值写入未初化对象的属性";
                    break;
                case "ORA-06592":
                    reStr = "case语句格式有误，没有分支语句";
                    break;
                case "ORA-06531":
                    reStr = "企图将集合填入未初始化的嵌套表中";
                    break;
                case "ORA-06511":
                    reStr = "企图打开已经打开的指针．指针已经打开，要再次打开必须先关闭";
                    break;
                case "ORA-01001":
                    reStr = "非法指针操作，例如关闭未打开的指针";
                    break;
                case "ORA-01017":
                    reStr = "企图用无效的用户名或密码登录oracle";
                    break;
                case "ORA-01403":
                    reStr = "NO_DATA_FOUND 无数据发现";
                    break;
                case "ORA-01012":
                    reStr = "程序发送数据库命令，但未与oracle建立连接";
                    break;
                case "ORA-06501":
                    reStr = "pl/sql系统问题";
                    break;
                case "ORA-06504":
                    reStr = "ROWTYPE_MISMATCH 行类型不匹配";
                    break;
                case "ORA-06500":
                    reStr = "PL/SQL运行内存溢出或内存冲突";
                    break;
                case "ORA-06533":
                    reStr = "子句超出数量";
                    break;
                case "ORA-06532":
                    reStr = "子句非法数量";
                    break;
                case "ORA-01410":
                    reStr = "SYS_INVALID_ROWID 无效的字段名";
                    break;
                case "ORA-01422":
                    reStr = "TOO_MANY_ROWS 返回超过一行";
                    break;
                case "ORA-06502":
                    reStr = "VALUE_ERROR 值错误";
                    break;
                case "ORA-01476":
                    reStr = "ZERO_DIVIDE 除0错误";
                    break;
                default:
                    reStr = "未定义的异常" + ex.Message;
                    break;
            }

            return reStr;
        } 
        #endregion
    }
}
