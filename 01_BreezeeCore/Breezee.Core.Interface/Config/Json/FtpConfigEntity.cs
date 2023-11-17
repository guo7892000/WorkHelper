using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

/*********************************************************************		
 * 对象名称：数据库配置		
 * 对象类别：类		
 * 创建作者：黄国辉		
 * 创建日期：2023/11/08 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2023/11/08 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// FTP配置实体类
    /// </summary>
    public class FtpConfigEntity
    {
        public IJsonConfig JsonConfig { get; }

        public DataTable Data { get { return JsonConfig.MiniJsonConfig.Data; } }

        public FtpConfigEntity(string sPath,string sConfigName,IList<string> moreConfig=null,string sPK="ID")
        {
            IList<string> allConfig = FtpServerInfo.getPropList();
            if (moreConfig!= null)
            {
                foreach (var item in moreConfig)
                {
                    if (!allConfig.Contains(item))
                    {
                        allConfig.Add(item);
                    }
                }
            }
            JsonConfig = new BJsonConfig(sPath, sConfigName, allConfig, sPK);
        }

        public bool Save()
        {
            return JsonConfig.Save();
        }

        public void Set(string sKey, IDictionary<string, string> dicConfig)
        {
            JsonConfig.Set(sKey, dicConfig);
        }

        public DataRow[] Get(string sKey)
        {
            return JsonConfig.Get(sKey);
        }
    }
}
