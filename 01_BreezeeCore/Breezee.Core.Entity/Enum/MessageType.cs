using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***************************************************************************
 * 对象名称：消息提醒/消息返回类
 * 对象类别：返回类
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：主要提供表名的获取
 * 注意点：这里的键都加上FRA前缀，表示框架使用的键，防止跟用户自定义的冲突。
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************************************************/
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 消息提醒/消息返回类
    /// </summary>
    public class MessageType
    {
        static string strSaveSuccess = "保存成功！";
        static string strSaveFaile = "保存失败！";

        /// <summary>
        /// 保存成功
        /// </summary>
        public static string SaveSuccess
        {
            get { return strSaveSuccess; }
        }

        /// <summary>
        /// 保存失败
        /// </summary>
        public static string SaveFailure
        {
            get { return strSaveFaile; }
        }

        /// <summary>
        /// 保存失败
        /// </summary>
        public static string SaveFailureMsg(string str)
        {
            return strSaveFaile + str;
        }

        /// <summary>
        /// 保存成功
        /// </summary>
        public static string SaveSuccessMsg(string str)
        {
            return str + strSaveSuccess;
        }

        /// <summary>
        /// 保存成功
        /// </summary>
        public static IDictionary<string, string> SaveSuccessMsgDictionary(string str)
        {
            IDictionary<string, string> iRes = new Dictionary<string, string>();
            iRes[StaticConstant.FRA_USER_MSG] = str + strSaveSuccess;
            return iRes;
        }

        /// <summary>
        /// 保存失败
        /// </summary>
        public static IDictionary<string, string> SaveFailureMsgDictionary(string str)
        {
            IDictionary<string, string> iRes = new Dictionary<string, string>();
            iRes[StaticConstant.FRA_USER_MSG] = strSaveFaile + str;
            iRes[StaticConstant.FRA_EXCEPTION] = strSaveFaile;
            return iRes;
        }

        /// <summary>
        /// 非空数据验证提示
        /// </summary>
        public static string ValidateNullMsg(string str)
        {
            return str + " 不能为空！";
        }

        /// <summary>
        /// 查询成功
        /// </summary>
        public static string QuerySuccess
        {
            get { return "查询成功！"; }
        }

        /// <summary>
        /// 删除成功
        /// </summary>
        public static string DeleteSuccess
        {
            get { return "删除成功！"; }
        }

    }
}
