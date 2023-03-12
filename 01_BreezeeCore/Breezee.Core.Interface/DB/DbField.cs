using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/*********************************************************************		
 * 对象名称：实体字段基类
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：对应于表或视图中的一个列
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.Interface
{
    /// <summary>
    /// 实体字段基类
    /// 对应于表或视图中的一个列。
    /// </summary>
    public class DbField
    {
        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <returns></returns>
        public static DbField New()
        {
            return new DbField();
        }

        /// <summary>
        /// 字段类型：数据库列（默认）和其他
        /// </summary>
        public EntityFieldType Field_Type { get; private set; } = EntityFieldType.DbColumn;
        public DbField FieldType(EntityFieldType sValue= EntityFieldType.DbColumn)
        {
            Field_Type = sValue;
            return this;
        }

        #region 数据库相关
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string DbColumnName { get; private set; }
        public DbField DBColName(string sValue)
        {
            DbColumnName = sValue;
            return this;
        }

        /// <summary>
        /// 数据库列类型字符
        /// </summary>
        public string DbColumnType { get; private set; }
        public DbField DBColType(string sValue)
        {
            DbColumnType = sValue;
            return this;
        }

        /// <summary>
        /// 类型和大小
        /// </summary>
        public string DbColumnTypeSize { get; private set; }
        public DbField DBTypeSize(string sValue)
        {
            DbColumnTypeSize = sValue;
            return this;
        }

        /// <summary>
        /// 对应.Net的数据类型
        /// </summary>
        public DbType DbType { get; private set; }
        public DbField NetType(DbType sValue)
        {
            DbType = sValue;
            return this;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int DbLength { get; private set; }
        public DbField DBLen(int sValue)
        {
            DbLength = sValue;
            return this;
        }

        /// <summary>
        /// 精度：默认为-1
        /// </summary>
        public int DBNumberPrecision { get; private set; } = -1;
        public DbField DBPrecision(int sValue)
        {
            DBNumberPrecision = sValue;
            return this;
        }

        /// <summary>
        /// 小数位数：默认为-1
        /// </summary>
        public int DBNumberScale { get; private set; } = -1;
        public DbField DBScale(int sValue)
        {
            DBNumberScale = sValue;
            return this;
        }

        /// <summary>
        /// 是否可空（默认true可空）
        /// </summary>
        public bool DbIsNullable { get; private set; } = true;
        public DbField Nullable(bool sValue= true)
        {
            DbIsNullable = sValue;
            DbIsNotNull = !sValue;
            return this;
        }

        /// <summary>
        /// 是否非空（默认false可空）
        /// </summary>
        public bool DbIsNotNull { get; private set; } = false;
        public DbField NotNull(bool sValue = true)
        {
            DbIsNotNull = sValue;
            DbIsNullable = !sValue;
            return this;
        }


        /// <summary>
        /// 是否主键（默认否）
        /// </summary>
        public bool DbIsPK { get; private set; } = false;
        public DbField PK(bool sValue=true)
        {
            DbIsPK = sValue;
            return this;
        }

        /// <summary>
        /// 是否并发更新ID字段（默认否）
        /// </summary>
        public bool IsUpdateControlColumn { get; private set; } = false;
        public DbField UpdateControl(bool sValue=true)
        {
            IsUpdateControlColumn = sValue;
            return this;
        }

        /// <summary>
        /// 数据库中的备注信息
        /// </summary>
        public string DbComment { get; protected set; }
        public DbField DBComment(string sValue)
        {
            DbComment = sValue;
            return this;
        }

        /// <summary>
        /// 数据库中的默认值字符
        /// </summary>
        public string DbDefault { get; protected set; }
        public DbField DBDefault(string sValue)
        {
            DbDefault = sValue;
            return this;
        }
        #endregion

        #region 验证相关
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Field_Name { get; protected set; }
        public DbField FieldName(string sValue)
        {
            Field_Name = sValue;
            return this;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region 使用相关
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 默认值类型
        /// </summary>
        public DbDefaultValueType DefaultValueType { get; set; }
        public DbField DefaultType(DbDefaultValueType sValue)
        {
            DefaultValueType = sValue;
            return this;
        }

        /// <summary>
        /// 对象值
        /// </summary>
        public object ObjValue { get; set; }


        public IDictionary<string,Object> DicObject { get; set; }
        #endregion
    }

    



}
