using Breezee.Core.Interface;
using Breezee.Core.Tool.Helper;
using MyPeachNetTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int i = 3*26*26+25;
            //Console.WriteLine(i.ToExcelColumnWord());
            //Console.WriteLine(i.ToUpperWord());
            //Console.ReadKey();

            CommonTest commonTest = new CommonTest();
            //commonTest.MergeTest();

            SelectTest selectTest = new SelectTest();
            //selectTest.Select();
            //selectTest.WithSelect();
            //selectTest.UnionSelect();
            //selectTest.InList();

            Select2Test select2Test = new Select2Test();
            //select2Test.Select();
            //select2Test.Select2();
            //select2Test.SelectDynamic();

            InsertTest insertTest = new InsertTest();
            //insertTest.Insert();
            //insertTest.InsertSelect();
            //insertTest.WithInsertSelect();
            //insertTest.InsertWithSelect();

            UpdateTest updateTest = new UpdateTest();
            //updateTest.Update();

            DeleteTest deleteTest = new DeleteTest();
            //deleteTest.Delete();

            downLoadTet();

            Console.ReadKey();
        }

        private static async void downLoadTet()
        {
            //string sResult = AppUpgradeTool.ReadWebText("https://gitee.com/breezee2000/WorkHelper/blob/master/README.md");
            //注：gitee上发布包占用的空间是有限的。如果超了，发布包发布不上去的，删除之前的一些发布包，再重新发布即可。
            //await AppUpgradeTool.DownloadWebZipAndUnZipAsync(@"https://gitee.com/breezee2000/WorkHelper/releases/download/1.2.24/WorkHelper1.2.24.rar", "E:\\mypeach");
            await AppUpgradeTool.DownloadWebZipAndUnZipAsync("https://github.com/guo7892000/WorkHelper/releases/download/1.2.36/WorkHelper1.2.36.rar", "E:\\mypeach");
            //await AppUpgradeTool.DownloadWebZipAndUnZipAsync("https://gitlab.com/guo7892000/WorkHelper/uploads/1c41da296dca4d999cf8bc08fc06d00e/WorkHelper1.2.36.rar", "E:\\mypeach");
        }
    }
}
