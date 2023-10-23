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
        //文本文件字符替换
        public static readonly string TextFileReplace_FileSource = "TextFileReplace_FileSource"; //文件来源
        public static readonly string TextFileReplace_SavePath = "TextFileReplace_SavePath";
        public static readonly string TextFileReplace_CharsetEncoding = "TextFileReplace_CharsetEncoding";
        public static readonly string TextFileReplace_GenerateType = "TextFileReplace_GenerateType";
        public static readonly string TextFileReplace_TemplateType = "TextFileReplace_ExampleType";

        public static readonly string TextFileReplace_RightAddDir = "TextFileReplace_RightAddDir"; //右键增加目录最后选择的目录
        public static readonly string TextFileReplace_RightAddFile = "TextFileReplace_RightAddFile"; //右键增加文件时最后选择的文件

        public static readonly string TextFileReplace_FTP_IPAddr = "TextFileReplace_FTP_IPAddr";
        public static readonly string TextFileReplace_FTP_PortNum = "TextFileReplace_FTP_PortNum";
        public static readonly string TextFileReplace_FTP_UserName = "TextFileReplace_FTP_UserName";
        public static readonly string TextFileReplace_FTP_Pwd = "TextFileReplace_FTP_Pwd";
        public static readonly string TextFileReplace_FTP_Protocol = "TextFileReplace_FTP_Protocol";
        public static readonly string TextFileReplace_FTP_InitDir = "TextFileReplace_FTP_InitDir";//初始目录

        public static readonly string TextFileReplace_FTP_ReadDir = "TextFileReplace_FTP_ReadDir";
        public static readonly string TextFileReplace_FTP_DownLoadIsAddList = "TextFileReplace_FTP_UploadDirIsAddList";
        public static readonly string TextFileReplace_FTP_DownloadLocalDir = "TextFileReplace_FTP_DownloadLocalDir";
        public static readonly string TextFileReplace_FTP_DownloadBeforeIsClearLocalDir = "TextFileReplace_FTP_DownloadBeforeIsClearLocalDir"; //下载前是否清空目录

        public static readonly string TextFileReplace_FTP_UploadDir = "TextFileReplace_FTP_UploadDir"; //上传目录
        public static readonly string TextFileReplace_FTP_UploadBackupDir = "TextFileReplace_FTP_UploadBackupDir"; //上传备份目录
        public static readonly string TextFileReplace_FTP_UploadReplaceType = "TextFileReplace_FTP_UploadReplaceType";

        
        public static readonly string TextFileReplace_FTP_ExcludeFileName = "TextFileReplace_FTP_ExcludeFileName"; //排除文件名
        public static readonly string TextFileReplace_FTP_ExcludeDirName = "TextFileReplace_FTP_ExcludeDirName"; //排除目录名

    }
}
