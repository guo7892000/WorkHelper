using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// Java发布文件配置
    /// </summary>
    public class GetJavaClassEntity
    {
        public string CodePath {  get; set; }
        public string ClassPath { get; set; }
        public string CopyToPath { get; set; }
        public DateTime DateTimeBeg { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public CopyCoverTypeEnum CopyCoverType { get; set; }
        public DataTable ChangList { get; set; }
        public DataTable RelCodeClassList { get; set; }
    }

    public enum CopyCoverTypeEnum
    {
        Cover = 1,
        CoverNow = 2,
        AwaysNew = 3
    }

}
