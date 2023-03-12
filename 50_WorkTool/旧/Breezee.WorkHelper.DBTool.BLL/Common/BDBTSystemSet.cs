using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.IBLL;

namespace Breezee.WorkHelper.DBTool.BLL
{
    public class BDBTSystemSet : IDBTSystemSet
    {
        #region 初始化数据库配置设置
        /// <summary>
        /// 查询数据库配置设置
        /// </summary>
        /// <param name="dicQuery"></param>
        /// <returns></returns>
        public override IDictionary<string, object> InitDB(IDictionary<string, string> dicQuery)
        {
            IDictionary<string, object> dicRet = new Dictionary<string, object>();
            try
            {
                //var dal = ContainerContext.Container.Resolve<IDBTDBInitializer>();
                dicRet = ExecuteResultHelper.QuerySuccess();
                DBInitializer.InitDataBase();
            }
            catch (Exception ex)
            {
                dicRet = ExecuteResultHelper.FailException(ex);
            }
            return dicRet;
        }
        #endregion
    }
}
