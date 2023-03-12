using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.IBLL
{
    public abstract class IDBDefaultValue : IDBToolModule
    {
        #region 查询数据库配置设置
        /// <summary>
        /// 查询数据库配置设置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public abstract IDictionary<string, object> QueryDefaultValue(IDictionary<string, string> dicQuery);
        #endregion

        #region 保存数据库配置设置
        /// <summary>
        /// 保存数据库配置设置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public abstract IDictionary<string, object> SaveDefaultValue(IDictionary<string, object> dicQuery);
        #endregion

        public static class QueryDefaultValue_InDicKey
        {
            public static readonly string COL_DEFAULT_ID = "COL_DEFAULT_ID";
            public static readonly string COLUMN_NAME = "COLUMN_NAME";
        }

        public static class SaveDefaultValue_InDicKey
        {
            public static readonly string DT_TABLE = "DT_TABLE_COLUMN_DEFAULT";
        }
    }

    
}
