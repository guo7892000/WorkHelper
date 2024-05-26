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
        //Excel复制拼接
        public static readonly string ExcelCopyDataConnect = "ExcelCopyDataConnect";
        //列转行
        public static readonly string ExcelCol2Row_FixRowCount = "ExcelCol2Row_FixRowCount";
        public static readonly string ExcelCol2Row_EachDataRowCount = "ExcelCol2Row_EachDataRowCount";
        //数据字典
        public static readonly string ColumnDic_ConfirmColumnType = "ColumnDic_ConfirmColumnType";
        public static readonly string ColumnDic_TemplateType = "ColumnDic_TemplateType";
        public static readonly string ColumnDic_IsPage = "ColumnDic_IsPage";
        public static readonly string ColumnDic_IsAutoParam = "ColumnDic_IsAutoParam";
        public static readonly string ColumnDic_IsAutoExcludeTable = "ColumnDic_IsAutoExcludeTable";
        public static readonly string ColumnDic_ExcludeTableList = "ColumnDic_ExcludeTableList";
        public static readonly string ColumnDic_IsOnlyMatchSqlTable = "ColumnDic_IsOnlyMatchSqlTable";
        public static readonly string ColumnDic_QueryResultType = "ColumnDic_QueryResultType";
        public static readonly string ColumnDic_IsColumnMust = "ColumnDic_IsColumnMust";
        //点击复制
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

        #region 文本文件字符替换
        public static readonly string TextFileReplace_FileSource = "TextFileReplace_FileSource"; //文件来源
        public static readonly string TextFileReplace_SavePath = "TextFileReplace_SavePath";//替换结果保存路径
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
        public static readonly string TextFileReplace_CharsetEncodingConnection = "TextFileReplace_CharsetEncodingConnection"; //连接字符集

        public static readonly string TextFileReplace_FTP_ReadDir = "TextFileReplace_FTP_ReadDir";
        public static readonly string TextFileReplace_FTP_DownLoadIsAddList = "TextFileReplace_FTP_UploadDirIsAddList";
        public static readonly string TextFileReplace_FTP_DownloadLocalDir = "TextFileReplace_FTP_DownloadLocalDir";
        public static readonly string TextFileReplace_FTP_DownloadBeforeIsClearLocalDir = "TextFileReplace_FTP_DownloadBeforeIsClearLocalDir"; //下载前是否清空目录
        public static readonly string TextFileReplace_FTP_IsDefaultExclude = "TextFileReplace_FTP_IsDefaultExclude"; //是否FTP默认排除
        public static readonly string TextFileReplace_FTP_DownPathExcludeFtpReadPath = "TextFileReplace_FTP_DownPathExcludeFtpReadPath"; //下载路径不包含读取目录

        public static readonly string TextFileReplace_FTP_UploadDir = "TextFileReplace_FTP_UploadDir"; //上传目录
        public static readonly string TextFileReplace_FTP_UploadBackupDir = "TextFileReplace_FTP_UploadBackupDir"; //上传备份目录
        public static readonly string TextFileReplace_FTP_UploadReplaceType = "TextFileReplace_FTP_UploadReplaceType"; //上传替换类型
        public static readonly string TextFileReplace_FTP_UploadBackupDirType = "TextFileReplace_FTP_UploadBackupDirType"; //上传备份目录类型

        public static readonly string TextFileReplace_FTP_ExcludeFileName = "TextFileReplace_FTP_ExcludeFileName"; //排除文件名
        public static readonly string TextFileReplace_FTP_ExcludeDirName = "TextFileReplace_FTP_ExcludeDirName"; //排除目录名
        public static readonly string TextFileReplace_CopyExcludeFileName = "TextFileReplace_FTP_CopyExcludeFileName"; //排除文件名
        public static readonly string TextFileReplace_CopyExcludeDirName = "TextFileReplace_FTP_CopyExcludeDirName"; //排除目录名
        //复制结果相关
        public static readonly string TextFileReplace_ReplaceResultFilterSavePath = "TextFileReplace_ReplaceResultFilterSavePath"; //替换结果后的二次筛选保存路径
        public static readonly string TextFileReplace_ResultCopyExcludeFileName = "TextFileReplace_ResultCopyExcludeFileName"; //替换结果复制时的排除文件名
        public static readonly string TextFileReplace_ResultCopyFileEndfix = "TextFileReplace_ResultCopyFileEndfix"; //替换结果复制时文件后缀
        public static readonly string TextFileReplace_ResultCopyFileIsUseEndfix = "TextFileReplace_ResultCopyFileIsUseEndfix"; //替换结果复制时是否指定文件后缀 
        #endregion

        #region 文本文件字符替换2
        public static readonly string TextFileReplace2_FileSource = "TextFileReplace2_FileSource"; //文件来源
        public static readonly string TextFileReplace2_SavePath = "TextFileReplace2_SavePath";//替换结果保存路径
        public static readonly string TextFileReplace2_CharsetEncoding = "TextFileReplace2_CharsetEncoding";
        public static readonly string TextFileReplace2_GenerateType = "TextFileReplace2_GenerateType";
        public static readonly string TextFileReplace2_TemplateType = "TextFileReplace2_TemplateType";

        public static readonly string TextFileReplace2_RightAddDir = "TextFileReplace2_RightAddDir"; //右键增加目录最后选择的目录
        public static readonly string TextFileReplace2_RightAddFile = "TextFileReplace2_RightAddFile"; //右键增加文件时最后选择的文件

        public static readonly string TextFileReplace2_CopyExcludeFileName = "TextFileReplace2_CopyExcludeFileName"; //排除文件名
        public static readonly string TextFileReplace2_CopyExcludeDirName = "TextFileReplace2_CopyExcludeDirName"; //排除目录名
        //复制结果相关
        public static readonly string TextFileReplace2_ReplaceResultFilterSavePath = "TextFileReplace2_ReplaceResultFilterSavePath"; //替换结果后的二次筛选保存路径
        public static readonly string TextFileReplace2_ResultCopyExcludeFileName = "TextFileReplace2_ResultCopyExcludeFileName"; //替换结果复制时的排除文件名
        public static readonly string TextFileReplace2_ResultCopyFileEndfix = "TextFileReplace2_ResultCopyFileEndfix"; //替换结果复制时文件后缀
        public static readonly string TextFileReplace2_ResultCopyFileIsUseEndfix = "TextFileReplace2_ResultCopyFileIsUseEndfix"; //替换结果复制时是否指定文件后缀 
        #endregion

        //SQL总结
        public static readonly string SQLStudy_FileCharsetEncoding = "SQLStudy_FileCharsetEncoding";
        //生成表SQL
        public static readonly string GenerateTableSQL_InputType = "GenerateTableSQL_InputType";//录入类型
        public static readonly string GenerateTableSQL_TargetDbType = "GenerateTableSQL_TargetDbType";//目标数据库类型
        public static readonly string GenerateTableSQL_IsExcludeColumn = "GenerateTableSQL_IsExcludeColumn";//是否排除文件名
        public static readonly string GenerateTableSQL_ExcludeColumnList = "GenerateTableSQL_ExcludeColumnList";//是否排除文件列表
        public static readonly string GenerateTableSQL_QueryColumnRealTime = "GenerateTableSQL_QueryColumnRealTime";//是否实时查询列信息
        public static readonly string GenerateTableSQL_IsFullType = "GenerateTableSQL_IsFullType";//是否全类型
        public static readonly string GenerateTableSQL_IsLYTemplate = "GenerateTableSQL_IsLYTemplate";//是否LY模板
        public static readonly string GenerateTableSQL_IsOnlyRemark = "GenerateTableSQL_IsOnlyRemark";//是否仅使用备注作为表或列的注释
        public static readonly string GenerateTableSQL_IsDefaultPK = "GenerateTableSQL_IsDefaultPK";//是否默认主键：当没有主键时，默认以第一行的列为主键列。
        public static readonly string GenerateTableSQL_IsDefaultColNameCN = "GenerateTableSQL_IsDefaultColNameCN";//是否使用默认列中文名
        public static readonly string GenerateTableSQL_IsPkRemoveDefault = "GenerateTableSQL_IsPkRemoveDefault";//是否主键剔除默认值
        public static readonly string GenerateTableSQL_IsColumnHeadMerge = "GenerateTableSQL_IsColumnHeadMerge"; //是否列头合并
        public static readonly string GenerateTableSQL_IsAutoFillColNameCn = "GenerateTableSQL_IsAutoFillColNameCn"; //是否自动修改列名称
        public static readonly string GenerateTableSQL_DefaultColNameCN = "GenerateTableSQL_DefaultColNameCN";//列中文名为空时使用的列名
        //DB间SQL转换
        public static readonly string DbSqlConvert_IsAutoExcludeTableSource = "DbSqlConvert_IsAutoExcludeTableSource";
        public static readonly string DbSqlConvert_ExcludeTableListSource = "DbSqlConvert_ExcludeTableListSource";
        public static readonly string DbSqlConvert_IsAutoExcludeTableTarget = "DbSqlConvert_IsAutoExcludeTableTarget";
        public static readonly string DbSqlConvert_ExcludeTableListTarget = "DbSqlConvert_ExcludeTableListTarget";
        public static readonly string DbSqlConvert_NewOldColumnSourceType = "DbSqlConvert_NewOldColumnSourceType";//新旧列关系来源类型
        public static readonly string DbSqlConvert_SourceDbType = "DbSqlConvert_SourceDbType";//源数据库类型
        public static readonly string DbSqlConvert_TargetDbType = "DbSqlConvert_TargetDbType";//目标数据库类型
        public static readonly string DbSqlConvert_LatestSql = "DbSqlConvert_LatestSql";
    }
}
