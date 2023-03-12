using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：数据库初始化抽象类
 * 对象类别：抽象类		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/12	
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/12 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core
{
    /// <summary>
    /// 数据库初始化抽象类
    /// </summary>
    public abstract class DBInitializer : IDBInitializer
    {
        public abstract IDataAccess DataAccess { get; }
        public abstract void DropObject();
        public abstract void InitDataBase();
        public abstract void InitFunction();
        public abstract void InitProduce();
        public abstract void InitTableData();
        public abstract void InitTableStruct();
        public abstract void InitView();
        public abstract bool IsNeedInit();
    }
}
