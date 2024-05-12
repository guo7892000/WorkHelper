using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Entity
{
    public class DictionaryHelper
    {
        #region 成功、失败提示
        private static Dictionary<string, object> New(bool isSuccess, string Message)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic[StaticConstant.FRA_RETURN_FLAG] = isSuccess ? 1 : 0;
            dic[StaticConstant.FRA_USER_MSG] = Message;
            return dic;
        }

        public static IDictionary<string, object> Success()
        {
            return New(true, MessageType.SaveSuccess);
        }

        public static IDictionary<string, object> Success(string msg)
        {
            return New(true, msg);
        }

        public static IDictionary<string, object> QuerySuccess()
        {
            return New(true, MessageType.QuerySuccess);
        }

        public static IDictionary<string, object> DeleteSuccess()
        {
            return New(false, MessageType.DeleteSuccess);
        }

        public static IDictionary<string, object> Fail(string msg)
        {
            return New(false, msg);
        }

        public static IDictionary<string, object> FailDefault()
        {
            return New(false, MessageType.SaveFailure);
        }
        public static IDictionary<string, object> FailException(Exception ex)
        {
            return New(false, ex.Message);
        }
        #endregion
    }
}
