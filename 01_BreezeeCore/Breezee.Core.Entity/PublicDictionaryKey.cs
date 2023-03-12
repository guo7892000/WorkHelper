using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.Core.Entity
{
    /// <summary>
    /// 公共的字典键
    /// </summary>
    public class PublicDictionaryKey
    {
        static List<string> _pubDicKey = new List<string>();

        public static List<string> PublicDicKey
        {
            get
            {
                if (_pubDicKey.Count == 0)
                {
                    //_pubDicKey.Add(DT_SYS_USER.ORG_ID);
                    //_pubDicKey.Add(DT_SYS_USER.EMP_ID);
                    //_pubDicKey.Add(DT_ORG_EMPLOYEE.EMP_NAME);
                    //_pubDicKey.Add(DT_SYS_USER.USER_ID);
                    //_pubDicKey.Add(DT_SYS_USER.USER_NAME);
                    //_pubDicKey.Add(DT_SYS_USER.USER_TYPE);
                }
                return PublicDictionaryKey._pubDicKey; 
            }
        }
        
    }
}
