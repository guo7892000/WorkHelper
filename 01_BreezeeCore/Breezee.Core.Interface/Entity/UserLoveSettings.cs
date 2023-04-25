using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

/*********************************************************************		
 * 对象名称：用户喜爱设定		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2023/04/24 22:29:28		
 * 对象说明：针对某些功能中界面的选择或输入值，在下次打开功能时自动加载值。		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 用户喜爱设定
    /// </summary>
    public class UserLoveSettings : ConfigEntity
    {
        public MiniXmlConfig XmlConfig { get; protected set; }
        public List<UserLoveSet> UserLoveSets { get => _userLoveSets;protected set => _userLoveSets = value; }

        private List<UserLoveSet> _userLoveSets= new List<UserLoveSet>();
        private DataTable _dtUserSet;
        public UserLoveSettings(string sPath,string sConfigName, XmlConfigSaveType saveType)
        {
            Dir = sPath;
            FileName = sConfigName;
            XmlConfig = new MiniXmlConfig(sPath, sConfigName, SetString.GetList(), SetNodeString.Pk, SetNodeString.Root, SetNodeString.Node, saveType);

            _dtUserSet = XmlConfig.Load();
            foreach (DataRow dr in _dtUserSet.Rows)
            {
                _userLoveSets.Add(new UserLoveSet(dr[SetString.Key].ToString(), dr[SetString.Value].ToString(), dr[SetString.Remark].ToString()));
            }
        }

        public void Set(string sKey,string sValue, string sRemark)
        {
            IEnumerable<UserLoveSet> userSet = _userLoveSets.Where(t => t.Key.Equals(sKey, StringComparison.OrdinalIgnoreCase));
            if (userSet == null || userSet.Count() == 0)
            {
                _userLoveSets.Add(new UserLoveSet(sKey, sValue, sRemark));
            }
            else
            {
                UserLoveSet set = userSet.First();
                set.Value = sValue;
                set.Remark= sRemark;
            }
        }

        public UserLoveSet Get(string sKey,string sDefault)
        {
            IEnumerable<UserLoveSet> userSet = _userLoveSets.Where(t => t.Key.Equals(sKey, StringComparison.OrdinalIgnoreCase));
            if (userSet == null || userSet.Count() == 0)
            {
                UserLoveSet set = new UserLoveSet(sKey, sDefault, string.Empty);
                _userLoveSets.Add(set);
                return set;
            }
            else
            {
                return userSet.First();
            }
        }

        public bool Save()
        {
            foreach (UserLoveSet set in _userLoveSets)
            {
                DataRow[] drArr = _dtUserSet.Select(SetString.Key + "='" + set.Key + "'");
                if (drArr.Length == 0)
                {
                    DataRow drNew = _dtUserSet.NewRow();
                    drNew[SetString.Key] = set.Key;
                    drNew[SetString.Value] = set.Value;
                    drNew[SetString.Remark] = set.Remark;
                    _dtUserSet.Rows.Add(drNew);
                }
                else
                {
                    DataRow drNew = drArr[0];
                    drNew[SetString.Key] = set.Key;
                    drNew[SetString.Value] = set.Value;
                    drNew[SetString.Remark] = set.Remark;
                }
            }
            XmlConfig.Save(_dtUserSet);
            return true;
        }

        public static class SetNodeString
        {
            public static readonly string Pk = "key";
            public static readonly string Root = "configs";
            public static readonly string Node = "config";
        }
        public static class SetString
        {
            public static readonly string Key = "key";
            public static readonly string Value = "value";
            public static readonly string Remark = "remark";

            public static List<string> GetList()
            {
                return new List<string>
                {
                    Key, Value, Remark
                };
            }
        }

        public class UserLoveSet
        {
            public string Key;
            public string Value;
            public string Remark;

            public UserLoveSet(string key, string value, string remark)
            {
                Key = key;
                Value = value;
                Remark = remark;
            }
        }

    }
}
