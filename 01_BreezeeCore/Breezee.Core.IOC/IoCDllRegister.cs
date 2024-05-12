using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.IOC
{
    /// <summary>
    /// IOC的DLL程序集注册类
    /// </summary>
    public class IoCDllRegister
    {
        private static List<ImplementDllInfo> implementDlls = new List<ImplementDllInfo>();

        public static List<ImplementDllInfo> ImplementDlls => implementDlls;

        public static List<ImplementDllInfo> Reg(ImplementDllInfo info)
        {
            implementDlls.Add(info);
            return implementDlls;
        }

        public static List<ImplementDllInfo> Reg(string[] sDllNameArr)
        {
            foreach (string s in sDllNameArr)
            {
                ImplementDllInfo info = new ImplementDllInfo();
                info.AssemblyName = s;
                implementDlls.Add(info);
            }
            return implementDlls;
        }

        public static List<ImplementDllInfo> Reg(List<string> listDllName)
        {
            return Reg(listDllName.ToArray());
        }
    }
}
