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
namespace Breezee.Core.Interface
{
    public static class IntExtension
    {
        /// <summary>
        /// 数字字母数组
        /// 包括0至9、大写A至Z共36个字符
        /// </summary>
        public static string[] NumberLetter = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public static string[] Letter = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <summary>
        /// 数值转换为大写字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToUpperWord(this int num)
        {
            if (num < 0)
            {
                num = 0;
            }
            int times = num / 26;
            return times > 0 ? Letter[num % 26] + times.ToString() : Letter[num % 26];
        }

        /// <summary>
        /// 数值转换为Excel列字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToExcelColumnWord(this int num)
        {
            if (num < 0)
            {
                num = 0;
            }

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Insert(0, Letter[num % 26]);//得到余数，作为最后的数字

                num = num / 26;
                if (num == 0) break;
                if (num <= 26)
                {
                    sb.Insert(0, Letter[num-1]);
                    break;
                }
            }
            while (num >= 26);

            return sb.ToString();
        }

        /// <summary>
        /// 随机产生指定位数的字符串
        /// </summary>
        /// <param name="num">字符串的位数</param>
        /// <returns></returns>
        public static string RandomNumberString(this int num)
        {
            string code = "";

            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += NumberLetter[rd.Next(0, NumberLetter.Length)];
            }
            return code;
        }

        /// <summary>
        /// 转换为数字字母流水号
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToNumberLetterSequenceNo(this int num)
        {
            if (num < 0)
            {
                num = 0;
            }


            int times = num / 36;
            int idx = num % 36;

            string code = NumberLetter[idx];

            while(times > 36)
            {

            }
            code = NumberLetter[times] + code;
            return code;
        }

        private static string GetSequenceNo(int num,string sOut)
        {
            string sReturn = sOut;

            return sReturn;
        }

    }
}
