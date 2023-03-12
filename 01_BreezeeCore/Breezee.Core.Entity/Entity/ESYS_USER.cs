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
namespace Breezee.Core.Entity
{
    /// <summary>
    /// 使用实体基类的测试
    /// </summary>
    public class ESYS_USER : DbEntity
    {
        public override DbObjectType EntType { get { return DbObjectType.Table; } }
        public override string DBTableName { get { return "SYS_USER"; } }

        public DbField USER_ID = new DbField().DBColName("USER_ID").DBColType("VARCHAR").DBTypeSize("VARCHAR(36)").DBScale(-1).Nullable(false).PK(true).DBLen(36).FieldName("用户ID").FieldType(EntityFieldType.DbColumn).UpdateControl(false);

        public override List<DbField> DbColumnList
        {
            get
            {
                var DbColumn = new List<DbField>(); 
                DbColumn.AddRange(new DbField[] { USER_ID, USER_ID });
                return DbColumn;
            }
        }

        public override List<string> ColumnStringList
        {
            get
            {
                var DbColumn = new List<string>();
                DbColumn.AddRange(new string[] { "USER_ID", "USER_ID" });
                return DbColumn;
            }
        }

    }
}
