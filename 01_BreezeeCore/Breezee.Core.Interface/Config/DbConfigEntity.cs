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
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 数据库配置实体类
    /// </summary>
    public class DbConfigEntity: ConfigEntity
    {
        public virtual IDictionary<string,DbServerInfo> DbServers { get; protected set; }

        public virtual DbServerInfo MainDb { get; protected set; }

        public MiniXmlConfig XmlConfig { get; protected set; }

        public DbConfigEntity(string sPath,string sConfigName, XmlConfigSaveType saveType)
        {
            Dir = sPath;
            FileName = sConfigName;
            XmlConfig = new MiniXmlConfig(sPath, sConfigName, DbServerInfo.XmlAttrString.getList(), DbServerInfo.XmlNodeString.pk, DbServerInfo.XmlNodeString.root,DbServerInfo.XmlNodeString.node, saveType);
            if (DbServers == null) {
                DbServers = new Dictionary<string, DbServerInfo>();
            }
            DataTable dt = XmlConfig.Load();
            foreach (DataRow dr in dt.Rows)
            {
                DbServerInfo dbServer = DbServerInfo.GetDbServer(dr);
                if (!DbServers.ContainsKey(dbServer.Key))
                {
                    DbServers[dbServer.Key] = dbServer;
                }
                if ("1".Equals(dbServer.IsMain,StringComparison.OrdinalIgnoreCase))
                {
                    MainDb = dbServer;
                }
            }
        }

        public bool SetMain(string sKey)
        {
            if(DbServers.ContainsKey(sKey))
            {
                MainDb = DbServers[sKey];
                return true;
            }
            return false;
        }

    }
}
