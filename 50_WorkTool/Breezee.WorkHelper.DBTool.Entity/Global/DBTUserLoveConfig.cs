using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.Entity
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class DBTUserLoveConfig
    {
        public static readonly string ExcelCopyDataConnect = "ExcelCopyDataConnect";
        public static readonly string ExcelCol2Row_FixRowCount = "ExcelCol2Row_FixRowCount";
        public static readonly string ExcelCol2Row_EachDataRowCount = "ExcelCol2Row_EachDataRowCount";
        public static readonly string ColumnDicConfirmColumnType = "ColumnDicConfirmColumnType";
        public static readonly string ClickCopyPath = "ClickCopyPath";
        public static readonly string DirStringLastSelectedPath = "DirStringLastSelectedPath";
        public static readonly string DbGetSql_ParamType = "DbGetSql_ParamType";
        public static readonly string DbGetSql_FirstWordType = "DbGetSql_FirstWordType";
        public static readonly string AutoEntity_Path = "AutoEntity_Path";
        public static readonly string AutoCode_Path = "AutoCode_Path";
        public static readonly string MergeScriptPath = "MergeScriptPath";
        //获取修改文件
        public static readonly string GetFileReadPath = "GetFile_ReadPath";
        public static readonly string GetFileTargetPath = "GetFile_TargetPath";
        public static readonly string GetFileExcludeEndprx = "GetFile_ExcludeEndprx";
        public static readonly string GetFileExcludeDirName = "GetFile_ExcludeDirName";
        public static readonly string GetFileExcludeFullDir = "GetFile_ExcludeFullDir";
        public static readonly string GetFileExcludeFileName = "GetFile_ExcludeFileName";
        public static readonly string GetFileExcludeFullFileName = "GetFile_ExcludeFullFileName";
        public static readonly string GetFileIsGenerateDateTimeDir = "GetFile_IsGenerateDateTimeDir";
        public static readonly string GetFileLastSaveEndDateTime = "GetFile_SaveEndDateTime";
        public static readonly string GetFileDirType = "GetFile_DirType";
        public static readonly string GetFileIsIncludeModify = "GetFile_IsIncludeModify";
        public static readonly string GetFileIsIncludeAdd = "GetFile_IsIncludeAdd";
        public static readonly string GetFileIsIncludeCommit = "GetFile_IsIncludeCommit";
        public static readonly string GetFileEmail = "GetFile_Email";
        public static readonly string GetFileUserName = "GetFile_UserName";
        //Excel公式
        public static readonly string ExcelFomulate_Type = "ExcelFomulate_Type";
        public static readonly string ExcelFomulate_TableName = "ExcelFomulate_TableName";
        public static readonly string ExcelFomulate_ColumnNum = "ExcelFomulate_ColumnNum";
        //SQL自动参数化验证
        public static readonly string SQLAutoParamVerify_BeforeSql = "SQLAutoParamVerify_BeforeSql";
    }
}
