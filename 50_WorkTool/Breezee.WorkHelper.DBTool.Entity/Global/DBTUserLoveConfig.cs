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
        public static readonly string ExcelCopy_DataConnect = "ExcelCopy_DataConnect";
        public static readonly string ExcelCopy_SqlType = "ExcelCopy_SqlType";
        public static readonly string ExcelCopy_DbType = "ExcelCopy_DbType";
        public static readonly string ExcelCopy_WordConvert = "ExcelCopy_WordConvert";
        public static readonly string ExcelCopy_IsAutoWord = "ExcelCopy_IsAutoWord";
        //列转行
        public static readonly string ExcelCol2Row_FixRowCount = "ExcelCol2Row_FixRowCount";
        public static readonly string ExcelCol2Row_EachDataRowCount = "ExcelCol2Row_EachDataRowCount";
        #region 数据字典
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
        public static readonly string ColumnDic_QueryConditionParamNameModule = "ColumnDic_QueryConditionParamNameModule";//模板参数名
        public static readonly string ColumnDic_QueryConditionParamColumnModule = "ColumnDic_QueryConditionParamColumnModule";//模板参数列名
        public static readonly string ColumnDic_QueryConditionParamNameLatest = "ColumnDic_QueryConditionParamNameLatest";//最后设置的参数名
        public static readonly string ColumnDic_QueryConditionParamColumnLatest = "ColumnDic_QueryConditionParamColumnLatest";//最后设置的参数列 
        #endregion
        //点击复制
        public static readonly string ClickCopy_Path = "ClickCopy_Path";
        public static readonly string DirString_LastSelectedPath = "DirString_LastSelectedPath";
        //  获取SQL
        public static readonly string DbGetSql_ParamType = "DbGetSql_ParamType";
        public static readonly string DbGetSql_FirstWordType = "DbGetSql_FirstWordType";
        // 自动实体
        public static readonly string AutoEntity_Path = "AutoEntity_Path";
        public static readonly string AutoCode_Path = "AutoCode_Path";
        // 脚本合并
        public static readonly string MergeScript_Path = "MergeScript_Path";
        #region 获取修改文件
        //获取修改文件
        public static readonly string GetFile_ReadPath = "GetFile_ReadPath";
        public static readonly string GetFile_TargetPath = "GetFile_TargetPath";
        public static readonly string GetFile_ExcludeEndprx = "GetFile_ExcludeEndprx";
        public static readonly string GetFile_ExcludeDirName = "GetFile_ExcludeDirName";
        public static readonly string GetFile_ExcludeFullDir = "GetFile_ExcludeFullDir";
        public static readonly string GetFile_ExcludeFileName = "GetFile_ExcludeFileName";
        public static readonly string GetFile_ExcludeFullFileName = "GetFile_ExcludeFullFileName";
        public static readonly string GetFile_IsGenerateDateTimeDir = "GetFile_IsGenerateDateTimeDir";
        public static readonly string GetFile_LastSaveEndDateTime = "GetFile_SaveEndDateTime";
        public static readonly string GetFile_DirType = "GetFile_DirType";
        public static readonly string GetFile_IsIncludeModify = "GetFile_IsIncludeModify";
        public static readonly string GetFile_IsIncludeAdd = "GetFile_IsIncludeAdd";
        public static readonly string GetFile_IsIncludeCommit = "GetFile_IsIncludeCommit";
        public static readonly string GetFile_Email = "GetFile_Email";
        public static readonly string GetFile_UserName = "GetFile_UserName"; 
        #endregion
        //Excel公式
        public static readonly string ExcelFomulate_Type = "ExcelFomulate_Type";
        public static readonly string ExcelFomulate_ConnStringType = "ExcelFomulate_ConnStringType";
        public static readonly string ExcelFomulate_TableName = "ExcelFomulate_TableName";
        public static readonly string ExcelFomulate_ColumnNum = "ExcelFomulate_ColumnNum";
        public static readonly string ExcelFomulate_EmptyToNull = "ExcelFomulate_EmptyToNull";
        
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
        #region 生成表SQL
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
        #endregion
        //DB间SQL转换
        public static readonly string DbSqlConvert_IsAutoExcludeTableSource = "DbSqlConvert_IsAutoExcludeTableSource";
        public static readonly string DbSqlConvert_ExcludeTableListSource = "DbSqlConvert_ExcludeTableListSource";
        public static readonly string DbSqlConvert_IsAutoExcludeTableTarget = "DbSqlConvert_IsAutoExcludeTableTarget";
        public static readonly string DbSqlConvert_ExcludeTableListTarget = "DbSqlConvert_ExcludeTableListTarget";
        public static readonly string DbSqlConvert_NewOldColumnSourceType = "DbSqlConvert_NewOldColumnSourceType";//新旧列关系来源类型
        public static readonly string DbSqlConvert_SourceDbType = "DbSqlConvert_SourceDbType";//源数据库类型
        public static readonly string DbSqlConvert_TargetDbType = "DbSqlConvert_TargetDbType";//目标数据库类型
        public static readonly string DbSqlConvert_LatestSql = "DbSqlConvert_LatestSql";
        public static readonly string DbSqlConvert_IsParaToHash = "DbSqlConvert_IsParaToHash"; //是否@参数转换为#参数#
        // 分隔拼接字符
        public static readonly string SplitConnString_SplitType = "SplitConnString_SplitType"; //分隔类型
        public static readonly string SplitConnString_SplitModel = "SplitConnString_SplitModel"; //分隔模式
        public static readonly string SplitConnString_IsIgnoreEmptyData = "SplitConnString_IsIgnoreEmptyData"; //是否忽略分隔后的空数据
        public static readonly string SplitConnString_IsTrimData = "SplitConnString_IsTrimData"; //是否每项剔除前后空白字符
        public static readonly string SplitConnString_IsFixNewLine = "SplitConnString_IsFixNewLine"; //是否指定换行符
        public static readonly string SplitConnString_NewLineString = "SplitConnString_NewLineString"; //换行符
        public static readonly string SplitConnString_SplitList = "SplitConnString_SplitList"; //分隔符列表
        public static readonly string SplitConnString_SplitListSplitByChar = "SplitConnString_SplitListSplitByChar"; //分隔符列表的分隔符
        public static readonly string SplitConnString_LastInputSplitString= "SplitConnString_LastInputSplitString"; //最后输入的要分隔的字符串
        // MyBatis参数化（占位符）的SQL转换实际可执行的SQL
        public static readonly string MyBatisSqlConvert_SqlAndParam = "MyBatisSqlConvert_SqlAndParam"; // SQL和参数
        public static readonly string MyBatisSqlConvert_Param = "MyBatisSqlConvert_Param"; // 参数
    }
}
