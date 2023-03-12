using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
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
    /// 实体基类
    /// </summary>
    public abstract class DbEntity: IDbEntity
    {
        #region 数据库相关
        /// <summary>
        /// 实体类型
        /// </summary>
        public abstract DbObjectType EntType { get; }
        /// <summary>
        /// 数据库表名
        /// </summary>
        public abstract string DBTableName { get; }

        /// <summary>
        /// 表中文名
        /// </summary>
        public virtual string DBTableNameCN { get; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string DBTableComment { get; }

        /// <summary>
        /// 数据库列清单
        /// </summary>
        public abstract List<DbField> DbColumnList { get; }

        public abstract List<string> ColumnStringList { get; }
        #endregion

        #region 使用相关
        /// <summary>
        /// 并发更新ID值：当需要使用并发更新字段检查并发时，并给该字段赋值
        /// </summary>
        public string UpdateControlIdValue { get; set; }

        /// <summary>
        /// 编辑类型
        /// </summary>
        public DbEditType EditType { get; set; }
        public string TableCode { get => "TableCode"; }

        public string Id => "DbID";

        public string Code => "DbCode";

        public string Name => "DbName";

        public string Description => "DbDescription";

        public IDictionary<string, object> DicObjects => throw new NotImplementedException();

        public List<DbField> GetUpdateColumnList()
        {
            if(EntType== DbObjectType.Table && EditType== DbEditType.Update)
            {
                return DbColumnList.Where(r => r.IsUpdate = true).ToList();
            }
            return new List<DbField>();
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <returns></returns>
        public virtual string Validate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
