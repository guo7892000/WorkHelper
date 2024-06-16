using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*********************************************************************		
 * 对象名称：键值对配置(只有Key、Value、Remark三种属性)	
 * 对象类别：类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：使用属性方式，针对某一种功能的单个文件配置，例如：是否显示在桌面上的菜单配置。
 * 示例：key为菜单ID，value为是否显示：0-否，1-是
 * <config>
 * <data key="A464F839-715A-4AD1-9DD0-F40BC001F911" value="0" remark="" />
 * <data key="59D1EA9B-E422-4F22-88D8-A0509B14EC0F" value="0" remark="" />
 * </config>
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class KeyValuePairConfig : IXmlConfigPair
    {
        private XDocument Doc;

        private MiniXmlConfig MiniXmlConfig { get; }
        public List<KeyValuePairEntity> ListEntity { get; }

        public override IXmlConfigPair XmlConfig => this;

        public KeyValuePairConfig(string sPath, string sConfigName, XmlConfigSaveType saveType)
        {
            MiniXmlConfig = new MiniXmlConfig(sPath, sConfigName, KeyValuePairString.getList(), KeyValuePairString.PK,KeyValuePairString.Root, KeyValuePairString.Item, saveType);
            MiniXmlConfig.Load();

            ListEntity = new List<KeyValuePairEntity>();
            foreach (DataRow dr in MiniXmlConfig.Data.Rows)
            {
                ListEntity.Add(new KeyValuePairEntity(dr[KeyValuePairString.Columns.Key].ToString(), dr[KeyValuePairString.Columns.Value].ToString(), dr[KeyValuePairString.Columns.Remark].ToString()));
            }
        }

        public override void Set(string sKey, string sValue, string sRemark)
        {
            IEnumerable<KeyValuePairEntity> userSet = ListEntity.Where(t => t.Key.Equals(sKey, StringComparison.OrdinalIgnoreCase));
            if (userSet == null || userSet.Count() == 0)
            {
                ListEntity.Add(new KeyValuePairEntity(sKey, sValue, sRemark));
            }
            else
            {
                KeyValuePairEntity set = userSet.First();
                set.Value = sValue;
                set.Comment = sRemark;
            }
        }

        public override KeyValuePairEntity GetEntity(string sKey, string sDefault)
        {
            IEnumerable<KeyValuePairEntity> userSet = ListEntity.Where(t => t.Key.Equals(sKey, StringComparison.OrdinalIgnoreCase));
            if (userSet == null || userSet.Count() == 0)
            {
                KeyValuePairEntity set = new KeyValuePairEntity(sKey, sDefault, string.Empty);
                ListEntity.Add(set);
                return set;
            }
            else
            {
                return userSet.First();
            }
        }
        public override string Get(string sKey, string sDefault)
        {
            IEnumerable<KeyValuePairEntity> userSet = ListEntity.Where(t => t.Key.Equals(sKey, StringComparison.OrdinalIgnoreCase));
            if (userSet == null || userSet.Count() == 0)
            {
                KeyValuePairEntity set = new KeyValuePairEntity(sKey, sDefault, string.Empty);
                ListEntity.Add(set);
                return sDefault;
            }
            else
            {
                return userSet.First().Value;
            }
        }

        public override bool Save()
        {
            MiniXmlConfig.Data.Clear();
            foreach (KeyValuePairEntity item in ListEntity)
            {
                DataRow drNew = MiniXmlConfig.Data.NewRow();
                drNew[KeyValuePairString.Columns.Key] = item.Key;
                drNew[KeyValuePairString.Columns.Value] = item.Value;
                drNew[KeyValuePairString.Columns.Remark] = item.Comment;
                MiniXmlConfig.Data.Rows.Add(drNew);
            }
            MiniXmlConfig.Save();
            return true;
        }
    }
}
