using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class MiniKeyValue
    {
        
        public static KeyValueListConfig KeyValue = new KeyValueListConfig(System.IO.Path.Combine(MiniGlobalValue.AppPath + MiniGlobalValue.Config.KeyValueCofig_Path));

        public static DataTable GetValue(MiniKeyEnum miniKeyEnum)
        {
            DataTable dtR;
            switch (miniKeyEnum)
            {
                case MiniKeyEnum.RBG_VALUE:
                    dtR = KeyValue.GetValues("RBG_VALUE");
                    break;
                case MiniKeyEnum.RBG_NAME:
                    dtR = KeyValue.GetValues("RBG_NAME");
                    break;
                case MiniKeyEnum.FORM_SKIN_TYPE:
                    dtR = KeyValue.GetValues("FORM_SKIN_TYPE");
                    break;
                case MiniKeyEnum.SAVE_TIP:
                    dtR = KeyValue.GetValues("SAVE_TIP");
                    break;
                default:
                    dtR = null;
                    break;
            }
            return dtR;
        }
    }

    public enum MiniKeyEnum
    {
        RBG_VALUE,
        RBG_NAME,
        FORM_SKIN_TYPE,
        SAVE_TIP,

    }
}
