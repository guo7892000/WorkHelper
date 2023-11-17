using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static Breezee.Core.Interface.UserLoveSettings;

/*********************************************************************		
 * 对象名称：JSON配置实现类	
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：使用属性方式		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// JSON配置实现类
    /// </summary>
    public class BJsonConfig : IJsonConfig
    {
        public override MiniJsonConfig MiniJsonConfig { get; }
        public BJsonConfig(string sPath, string sConfigName,IList<string> listProp,string sPK)
        {
            MiniJsonConfig = new MiniJsonConfig(sPath, sConfigName, listProp, sPK);
            MiniJsonConfig.Load();
        }

        public override void Set(string sKey, IDictionary<string,string> dicConfig)
        {
            var list = from t in MiniJsonConfig.Data.AsEnumerable()
                       where t[MiniJsonConfig.PK].Equals(sKey)
                       select t;
            DataRow[] drArr = list.ToArray();
            if (drArr.Count() == 0)
            {
                DataRow drNew = MiniJsonConfig.Data.NewRow();
                drNew[MiniJsonConfig.PK] = sKey;
                foreach (string col in dicConfig.Keys)
                {
                    if(drNew.ContainsColumn(col))
                    drNew[col] = dicConfig[col];
                }
                MiniJsonConfig.Data.Rows.Add(drNew);
            }
            else
            {
                DataRow drNew = drArr[0];
                drNew[MiniJsonConfig.PK] = sKey;
                foreach (string col in dicConfig.Keys)
                {
                    if (drNew.ContainsColumn(col))
                        drNew[col] = dicConfig[col];
                }
            }
        }

        public override DataRow[] Get(string sValue)
        {
            var list = from t in MiniJsonConfig.Data.AsEnumerable()
                       where t[MiniJsonConfig.PK].Equals(sValue)
                       select t;
            return list.ToArray();
        }

        public override bool Save()
        {
            MiniJsonConfig.Save(MiniJsonConfig.Data);
            return true;
        }

        public override DataRow[] Get(string sKey, string sValue)
        {
            var list = from t in MiniJsonConfig.Data.AsEnumerable()
                       where  t.ContainsColumn(sKey) && t[sKey].Equals(sValue)
                       select t;
            return list.ToArray();
        }
    }
}
