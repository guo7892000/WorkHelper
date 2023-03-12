using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
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
    public static class DataColumnExtension
    {
        /// <summary>
        /// 设置表中列的扩展属性
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="defaultType"></param>
        /// <returns></returns>
        public static DataColumn ExtProp(this DataColumn dc, DbDefaultValueType defaultType)
        {
            dc.ExtendedProperties[StaticConstant.FRA_TABLE_EXTEND_PROPERTY_COLUMNS_FIX_VALUE] = defaultType;
            return dc;
        }
    }
}
