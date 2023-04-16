using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Tool;
using System.Data;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Core;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据库工具的UI辅助类
    /// </summary>
    public class DBToolUIHelper
    {
        #region 下载文件
        public static void DownloadFile(string sSourceFilePath, string sSaveFileName, bool isOverWrite)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                var fileInfo = new FileInfo(sSourceFilePath);
                string sEnd = fileInfo.Extension.ToLower().Replace(".","");
                saveDialog.DefaultExt = sEnd;
                if (sEnd.Equals("xls"))
                {
                    saveDialog.Filter = "Excel文件(*.xls)|*.xls";
                }
                else if (sEnd.Equals("xlsx"))
                {
                    saveDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
                }
                else if (sEnd.Equals("xml"))
                {
                    saveDialog.Filter = "配置文件(*.xml)|*.xml";
                }
                else if (sEnd.Equals("txt"))
                {
                    saveDialog.Filter = "文本文件(*.txt)|*.txt";
                }
                else
                {
                    
                }

                saveDialog.FileName = sSaveFileName + "_" + DateTime.Now.ToyyyyMMddHHmmss();
                saveDialog.FilterIndex = 1;
                //saveDialog.OverwritePrompt = true;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, sSourceFilePath);
                    string destFileName = saveDialog.FileName;

                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Copy(fileName, destFileName, isOverWrite);
                        MsgHelper.ShowInfo(string.Format("下载成功，文件保存在 {0}", destFileName));
                    }
                    else
                    {
                        MsgHelper.ShowErr("模板文件不存在，下载失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgHelper.ShowErr(ex.Message);
            }
        }
        #endregion

        #region 获取数据库类型表
        public static DataTable GetBaseDataTypeTable(bool hasNone = false)
        {
            IDictionary<string, string> dic_List = new Dictionary<string, string>();
            //生成数据库类型
            if (hasNone)
            {
                dic_List.Add(((int)DataBaseType.None).ToString(), "");
            }
            dic_List.Add(((int)DataBaseType.SqlServer).ToString(), "SqlServer");
            dic_List.Add(((int)DataBaseType.Oracle).ToString(), "Oracle");
            dic_List.Add(((int)DataBaseType.MySql).ToString(), "MySql");
            dic_List.Add(((int)DataBaseType.SQLite).ToString(), "SQLite");
            dic_List.Add(((int)DataBaseType.PostgreSql).ToString(), "PostgreSql");
            return dic_List.GetTextValueTable(false);
        } 
        #endregion
    }

    public enum HelpType
    {
        TableStruct = 1,
        DataSql = 2,
    }
}
