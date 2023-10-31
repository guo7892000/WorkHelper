using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 文件文本替换界面中的控件值
    /// 解决跨线程调用问题
    /// </summary>
    internal class ReplaceTesfFileFormControleValue
    {
        public string EncodingConnect { get; set; }
        public string EncodingFile { get; set; }
        public string DownLocalPath { get; set; }
        public string ReadPath { get; set; }
        public bool IsDownPathExcludeFtpReadPath { get; set; }
        public string FtpUploadPath { get; set; }
        public string BackupDirType { get; internal set; }
    }
}
