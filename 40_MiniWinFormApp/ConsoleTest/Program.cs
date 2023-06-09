using Breezee.Core.Interface;
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
            int i = 3*26*26+25;
            Console.WriteLine(i.ToExcelColumnWord());
            Console.WriteLine(i.ToUpperWord());
            Console.ReadKey();
        }
    }
}
