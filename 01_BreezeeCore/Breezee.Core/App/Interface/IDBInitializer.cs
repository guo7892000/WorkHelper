using Breezee.AutoSQLExecutor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************************		
 * 对象名称：数据库初始化接口
 * 对象类别：接口		
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
    /// 数据库初始化接口
    /// </summary>
    public interface IDBInitializer
    {
        IDataAccess DataAccess { get; }
        bool IsNeedInit();
        void InitDataBase();
        void DropObject();
        void InitTableStruct();
        void InitTableData();
        void InitView();
        void InitProduce();
        void InitFunction();
    }
}
