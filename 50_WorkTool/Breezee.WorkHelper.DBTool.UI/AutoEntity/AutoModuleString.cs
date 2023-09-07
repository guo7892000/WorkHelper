using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breezee.WorkHelper.DBTool.UI
{
    public class AutoImportModuleString
    {
        /// <summary>
        /// 导入模板的Sheet名称
        /// </summary>
        public class SheetName
        {
            public static readonly string CodeModule = "代码模板";           
            public static readonly string MyParam = "自定义变量";
            public static readonly string DbTypeConvert = "类型转换";
            public static readonly string SysParam = "系统变量";
            //自动实体使用
            public static readonly string EntityModule = "实体模板";
        }
        /// <summary>
        /// 导入模板的中【模板】Sheet中的列名
        /// </summary>
        public class ColumnNameModule
        {
            public static readonly string ModuleCode = "模板编码";
            public static readonly string ModuleContent = "模板内容";
            public static readonly string ModuleFileSuffix = "文件后缀";
        }

        /// <summary>
        /// 导入模板的中【类文件】Sheet中的列名
        /// </summary>
        public class ColumnNameClass
        {
            public static readonly string SortNum = "序号";
            public static readonly string Path = "路径";
            public static readonly string PackName = "包名";
            public static readonly string PackNameKey = "包名键";
            public static readonly string BeginString = "前缀";
            public static readonly string EndString = "后缀";
            public static readonly string FileContent = "文件内容";
            //不在导入模板中，是替换变量后的实际值
            public static readonly string FinalPath = "最终路径";
            public static readonly string FinalPackName = "最终包名";
        }
        /// <summary>
        /// 导入模板的中【自定义变量】Sheet中的列名
        /// </summary>
        public class ColumnNameMyParam
        {
            public static readonly string ChangeType = "变化类型";
            public static readonly string ParamName = "变量名";
            public static readonly string ConcatString = "连接符";
            public static readonly string ParamContent = "变量内容";
            public static readonly string ParamValueInfo = "变量值说明";
        }
        /// <summary>
        /// 导入模板的中【系统变量】Sheet中的列名
        /// </summary>
        public class ColumnNameSysParam
        {
            public static readonly string ChangeType = "变化类型";
            public static readonly string ParamName = "变量名";
            public static readonly string ParamContent = "变量内容";
            public static readonly string ParamContentWhere = "变量内容条件";
            public static readonly string ParamValueInfo = "变量值说明";
            public static readonly string Example = "示例";
        }
        /// <summary>
        /// 导入模板的中【类型转换】Sheet中的列名
        /// </summary>
        public class ColumnNameTypeConvert
        {
            public static readonly string DbType = "数据库类型";
            public static readonly string DevLangType = "开发语言类型";
        }
        /// <summary>
        /// 导入模板的中【系统变量】Sheet中所有定义的系统变量键
        /// </summary>
        public class AutoFileSysParam
        {
            public static readonly string DateNow = "#DATE_NOW#";
            public static readonly string PackPath = "#PACK_PATH#";

            public static readonly string TableDbName = "#TABLE_DB_NAME#";

            public static readonly string EntName = "#ENT_NAME#";
            public static readonly string EntNameClass = "#ENT_NAME_CLASS#";
            public static readonly string EntNameLcc = "#ENT_NAME_LCC#";
            public static readonly string EntNameCn = "#ENT_NAME_CN#";
            public static readonly string EntExt = "#ENT_EXT#";//实体扩展信息

            public static readonly string ColName = "#COL_NAME#";
            public static readonly string ColNameLcc = "#COL_NAME_LCC#";
            public static readonly string ColNameCn = "#COL_NAME_CN#";

            public static readonly string ColDbName = "#COL_DB_NAME#";
            public static readonly string ColDbLength = "#COL_DB_LEN#";
            public static readonly string ColDbType = "#COL_DB_TYPE#";
            public static readonly string ColDbTypeLength = "#COL_DB_TYPE_LEN#";
            public static readonly string ColDbDecimalBegin = "#COL_DB_DEC_BEG#";
            public static readonly string ColDbDecimalEnd = "#COL_DB_DEC_END#";
            public static readonly string ColDbKey = "#COL_DB_KEY#";
            public static readonly string ColDbNotNull = "#COL_DB_NOT_NULL#";
            public static readonly string ColDbDefaultValue = "#COL_DB_DEF_VAL#";
            public static readonly string ColDbRemark = "#COL_DB_EXT#";//列扩展信息

            //生成实体文件相关
            public static readonly string SetEntColDbName = "#SET_ENT_COL_NAME#";
            public static readonly string SetEntColDbType= "#SET_ENT_COL_TYPE#";
            public static readonly string SetEntColDbLength = "#SET_ENT_COL_LEN#";
            public static readonly string SetEntColDbDecimalBegin = "#SET_ENT_COL_DEC_BEG#";
            public static readonly string SetEntColDbDecimalEnd= "#SET_ENT_COL_DEC_END#";
            public static readonly string SetEntColDbTypeLength = "#SET_ENT_COL_TYPE_LEN#";
            public static readonly string SetEntColDbDefaultValue = "#SET_ENT_COL_DEF_VAL#";
            public static readonly string SetEntColDbNotNull = "#SET_ENT_COL_NOT_NULL#";
            public static readonly string SetEntColDbKey = "#SET_ENT_COL_KEY_PK#";
            public static readonly string SetEntColDbNameCn = "#SET_ENT_COL_CN#";
            public static readonly string SetEntColDbRemark = "#SET_ENT_COL_EXT#";//列扩展信息


            //public static readonly string ColDbNameAll = "#COL_DB_NAME_ALL#";//逗号分隔所有字符

            //生成代码中的实体相关
            public static readonly string ColEntType = "#COL_ENT_TYPE#";
            public static readonly string ColEntNote = "#COL_ENT_NOTE#";
            //生成MAP配置相关
            public static readonly string ColMapNode = "#COL_MAP_NODE#";
            //生成查询和保存入出参
            public static readonly string ColQueryIn = "#COL_QUERY_IN#";
            public static readonly string ColQueryOut = "#COL_QUERY_OUT#";
            public static readonly string ColSaveIn = "#COL_SAVE_IN#";


        }
    }
}
