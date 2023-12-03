using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.Core.Interface
{
    /// <summary>
    /// 文件编码类
    /// </summary>
    public class BaseFileEncoding
    {
        /// <summary>
        /// 获取编码字典
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetEncodingDictionary()
        {
            return FileEncodingString.GetDictionary();
        }

        /// <summary>
        /// 获取编码表
        /// </summary>
        /// <param name="isHasEmpty"></param>
        /// <returns></returns>
        public static DataTable GetEncodingTable(bool isHasEmpty)
        {
            return GetEncodingDictionary().GetTextValueTable(isHasEmpty);
        }

        /// <summary>
        /// 根据键获取编码
        /// </summary>
        /// <param name="sEncodingKey"></param>
        /// <returns></returns>
        public static Encoding GetEncodingByKey(string sEncodingKey)
        {
            Encoding encoding;
            //支持中间加-或不加-
            if (FileEncodingString.UTF8Bom.Equals(sEncodingKey, StringComparison.OrdinalIgnoreCase)
                || FileEncodingString.UTF8Bom.Replace("-","").Equals(sEncodingKey, StringComparison.OrdinalIgnoreCase))
            {
                encoding = new UTF8Encoding(true);
            }
            else if (FileEncodingString.UTF8.Equals(sEncodingKey, StringComparison.OrdinalIgnoreCase)
                || FileEncodingString.UTF8.Replace("-", "").Equals(sEncodingKey, StringComparison.OrdinalIgnoreCase))
            {
                encoding = new UTF8Encoding(false);
            }
            else
            {
                //NetCore 6以后，System.Text命名空间中不再包含 “gb2312”字符编码。这时需要手动引入“System.Text.Encoding.CodePages”扩展包，并且给Encoding注册字符编码
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//注册简体中文的支持：注NET 4不需要，NetCore 6才需要
                encoding = Encoding.GetEncoding(sEncodingKey);
            }

            return encoding;
        }
        public static class FileEncodingString
        {
            public static readonly string UTF8 = "utf-8";
            public static readonly string UTF8Bom = "utf-8-Bom";
            public static readonly string UTF16 = "utf-16";
            public static readonly string UTF32 = "utf-32";
            public static readonly string GB2312 = "gb2312";
            public static readonly string GB18030 = "GB18030";
            public static readonly string BIG5 = "big5";
            public static readonly string ISO88591 = "ISO-8859-1";

            public static string[] GetKeyList()
            {
                return new string[]
                {
                    UTF8,UTF8Bom,UTF16,UTF32,GB2312,GB18030,BIG5,ISO88591
                };
            }

            public static IDictionary<string, string> GetDictionary()
            {
                IDictionary<string, string> _dicString = new Dictionary<string, string>();
                _dicString[UTF8] = "utf-8";
                _dicString[UTF8Bom] = "带有BOM的utf-8";
                _dicString[UTF16] = "unicode";
                _dicString[UTF32] = "utf-32";
                _dicString[GB2312] = "gb2312";
                _dicString[GB18030] = "GB18030";
                _dicString[BIG5] = "big5";
                _dicString[ISO88591] = "ISO-8859-1";
                return _dicString;
            }
                
        }
    }
}
