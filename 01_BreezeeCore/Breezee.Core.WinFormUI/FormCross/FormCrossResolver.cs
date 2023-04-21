using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

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
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// UI窗体交叉引用处理类，UI层的调用示例：
    /// var frm = FormCrossResolver.CreateCrossFrom<IMainCommonFormCross>("Breezee.Framework.Mini.StartUp.FrmDBConfig", new object[] { '参数1','参数2' });
    /// frm.ShowDialog();
    /// </summary>
    public static class FormCrossResolver
    {
        //全局变量
        static Dictionary<string, Assembly> asmCache = new Dictionary<string, Assembly>();

        #region 创建交叉窗体实例
        /// <summary>
        /// 创建交叉窗体实例
        /// </summary>
        /// <typeparam name="T">窗体接口（须继承IFormCross接口，且设置了DllNameAttribute特性）</typeparam>
        /// <param name="formfullType">窗体类型(全类名)，须实现接口T</param>
        /// <returns>返回窗体接口类</returns>
        public static T CreateCrossFrom<T>(string formfullType) where T : IFormCross
        {
            return CreateCrossFrom<T>(formfullType, new object[0]);
        } 
        #endregion

        #region 创建交叉窗体实例
        /// <summary>
        /// 创建交叉窗体实例
        /// </summary>
        /// <typeparam name="T">窗体接口（须继承IFormCross接口，且设置了DllNameAttribute特性）</typeparam>
        /// <param name="formfullType">窗体类型，须实现T</param>
        /// <param name="args">窗体构造函数参数数组</param>
        /// <returns></returns>
        public static T CreateCrossFrom<T>(string formfullType, object[] args) where T : IFormCross
        {
            DllNameAttribute attr = DllNameAttribute.Find<T>();
            if (attr == null)
            {
                throw new Exception("请设置窗体接口T的DllNameAttribute特性");
            }

            return (T)CreateCrossFormCore(attr.DllName, formfullType, args);
        } 
        #endregion

        #region 创建交叉窗体实例
        /// <summary>
        /// 创建交叉窗体实例
        /// </summary>
        /// <typeparam name="T">窗体接口（须继承IFormCross接口）</typeparam>
        /// <param name="assemblyFile">窗体所在程序集文件名，如LY.MDS.UI.SE.Common.dll</param>
        /// <param name="formfullType">窗体类型，须实现T</param>
        /// <param name="args">窗体构造函数参数数组</param>
        /// <returns></returns>
        public static T CreateCrossFrom<T>(string assemblyFile, string formfullType, object[] args = null) where T : IFormCross
        {
            return (T)CreateCrossFormCore(assemblyFile, formfullType, args);
        } 
        #endregion

        #region 创建交叉窗体实例私有方法
        /// <summary>
        /// 创建交叉窗体实例私有方法
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="formfullType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static object CreateCrossFormCore(string assemblyFile, string formfullType, object[] args)
        {
            if (string.IsNullOrEmpty(assemblyFile))
            {
                throw new ArgumentNullException("assemblyFile");
            }

            if (string.IsNullOrEmpty(formfullType))
            {
                throw new ArgumentNullException("formfullType");
            }
            object obj = null;

            Assembly assm = LoadAssembly(assemblyFile);
            obj = assm.CreateInstance(formfullType, false, BindingFlags.CreateInstance,
                Type.DefaultBinder, args, System.Globalization.CultureInfo.InvariantCulture, null);

            if (!typeof(Form).IsAssignableFrom(obj.GetType()))
            {
                throw new ArgumentException(string.Format("{0}不是有效的窗体类型!", formfullType));
            }

            return obj;
        } 
        #endregion

        #region 加裁DLL
        /// <summary>
        /// 加裁DLL
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <returns></returns>
        private static Assembly LoadAssembly(string assemblyFile)
        {
            if (!asmCache.ContainsKey(assemblyFile))
            {
                Assembly assm = Assembly.LoadFrom(assemblyFile);
                asmCache[assemblyFile] = assm;
            }

            return asmCache[assemblyFile];
        } 
        #endregion
    }
}
