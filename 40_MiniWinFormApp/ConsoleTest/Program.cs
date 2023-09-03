using Breezee.Core.Interface;
using Breezee.Core.Tool.Helper;
using MyPeachNetTest;
using System;
using System.Collections.Generic;
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
            string sResult = FileDirHelper.ReadWebText("https://gitee.com/breezee2000/WorkHelper/blob/master/README.md");

            await FileDirHelper.DownloadWebZipAndUnZipAsync(@"https://gitee.com/breezee2000/WorkHelper/releases/download/1.2.24/WorkHelper1.2.24.rar", "E:\\mypeach");

        }
    }
}
