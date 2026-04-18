using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Ookii.Dialogs.WinForms;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AppSet = Breezee.WorkHelper.DBTool.UI.Properties.Settings;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：获取目录字符
    /// 最后更新日期：2021-08-17
    /// 修改人：黄国辉
    /// </summary>
    public partial class FrmDirectoryFileString : BaseForm
    {
        #region 变量
        string strLastSelectedPath;
        //分隔的字符数组
        char[] splitCharArr = new char[] { ',', '，', '：', ';', '；', '|' };
        string[] sExcludeFullDir; //得到排除的相对目录
        string[] sExcludeFullFile; //得到排除的相对文件
        string[] sExcludeExt; //得到排除的后缀
        string sCurRelateDir; //当前相对目录名
        #endregion

        #region 构造函数
        public FrmDirectoryFileString()
        {
            InitializeComponent();
        }

        #endregion

        #region 加载事件
        private void FrmDirectoryFileString_Load(object sender, EventArgs e)
        {
            //加载用户偏好值
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirString_LastSelectedPath, "").Value;
            ckbMulDir.Checked = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirString_MulDir, "0").Value == "1" ? true : false;
            rtbExcludeRelateDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirString_ExcludeDir, "").Value; //排除目录
            rtbExcludeRelateFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirString_ExcludeFile, "").Value; //排除文件
            txbExcludeEndprx.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirString_ExcludeExt, "").Value; //排除后缀

            _dicString.Add("1", "仅文件");
            _dicString.Add("2", "仅目录");
            _dicString.Add("3", "目录和文件");
            cbbOutType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //
            _dicString.Clear();
            _dicString.Add("1", "全路径");
            _dicString.Add("2", "仅文件名");
            _dicString.Add("3", "相对路径");
            cbbPathType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            if(!string.IsNullOrEmpty(strLastSelectedPath))
            {
                txbSelectPath.Text = strLastSelectedPath;
            }

            nudDept.Minimum = 1;
            groupBox1.AddFoldRightMenu();
        } 
        #endregion

        #region 选择路径按钮事件
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            #region 取消使用自带的FolderBrowserDialog
            //var dialog = new FolderBrowserDialog();
            //var strLastSelectedPath = WinFormContext.UserLoveSettings.Get("LastSelectedPath", "").Value;

            //if (!string.IsNullOrEmpty(strLastSelectedPath))
            //{
            //    dialog.SelectedPath = strLastSelectedPath;
            //}
            //dialog.Description = "请选择文件路径";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    txbSelectPath.Text = dialog.SelectedPath;
            //    //保存用户偏好值
            //    WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_LastSelectedPath, dialog.SelectedPath, "【目录字符生成】最后选择的目录");
            //    WinFormContext.UserLoveSettings.Save();
            //} 
            #endregion

            //这里不使用自带的FolderBrowserDialog，那样选择目录很不方便。这个第3方库Ookii Dialogs，显示的界面更好用！！
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.Description = "请选择一个目录";
            folderBrowserDialog.ShowNewFolderButton = true;
            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                folderBrowserDialog.SelectedPath = strLastSelectedPath;
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (!ckbMulDir.Checked)
                {
                    //单目录
                    txbSelectPath.Text = folderBrowserDialog.SelectedPath; 
                }
                else
                {
                    //多目录
                    string sPath = txbSelectPath.Text.Trim();
                    if (string.IsNullOrEmpty(sPath))
                    {
                        txbSelectPath.Text = folderBrowserDialog.SelectedPath + ";";
                    }
                    else
                    {
                        if (!sPath.EndsWith(";"))
                        {
                            sPath = sPath + ";";
                        }
                        txbSelectPath.Text = sPath + folderBrowserDialog.SelectedPath + ";";
                    }
                }
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_LastSelectedPath, folderBrowserDialog.SelectedPath, "【目录字符生成】最后选择的目录");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_MulDir, ckbMulDir.Checked ? "1" : "0", "【目录字符生成】是否多目录");
                WinFormContext.UserLoveSettings.Save();
            }
        } 
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            string sPath = txbSelectPath.Text.Trim();
            if (string.IsNullOrEmpty(sPath))
            {
                ShowErr("请选择路径！");
            }
            rtbString.Clear();
            var strOutType = cbbOutType.SelectedValue.ToString();
            var strPathType = cbbPathType.SelectedValue.ToString();
            var isSearchChild = ckbSetDirectoryDept.Checked;
            int iDept = int.Parse(nudDept.Value.ToString());

            sExcludeFullDir = rtbExcludeRelateDir.Text.Trim().GetLinuxPath().ToLower().Split(splitCharArr, StringSplitOptions.RemoveEmptyEntries); //得到排除的相对目录
            sExcludeFullFile = rtbExcludeRelateFile.Text.Trim().GetLinuxPath().ToLower().Split(splitCharArr, StringSplitOptions.RemoveEmptyEntries); //得到排除的相对文件
            sExcludeExt = txbExcludeEndprx.Text.Trim().ToLower().Split(splitCharArr, StringSplitOptions.RemoveEmptyEntries); //得到排除后缀

            StringBuilder sb = new StringBuilder();
            string[] dirs = sPath.Split(';');
            int iDirCount = 0;
            foreach (string dir in dirs)
            {
                if (string.IsNullOrEmpty(dir)) 
                {  
                    continue; 
                }
                sCurRelateDir = dir;
                DirectoryInfo rootDirectory = new DirectoryInfo(dir);
                GetDirectoryFile(sb, rootDirectory, strOutType, strPathType, isSearchChild, iDept);
                iDirCount++;
            }
            rtbString.AppendText(sb.ToString());
            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_LastSelectedPath, sPath, "【目录字符生成】最后选择的目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_MulDir, iDirCount > 1 ? "1" : "0", "【目录字符生成】是否多目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_ExcludeDir, rtbExcludeRelateDir.Text.Trim(), "【目录字符生成】排除目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_ExcludeFile, rtbExcludeRelateFile.Text.Trim(), "【目录字符生成】排除文件");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirString_ExcludeExt, txbExcludeEndprx.Text.Trim(), "【目录字符生成】排除后缀");
            WinFormContext.UserLoveSettings.Save();
        } 
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 获取目录文件方法
        /// <summary>
        /// 获取目录文件方法
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="rootDirectory"></param>
        /// <param name="sOutType">输出类型：1仅文件，2仅目录，3目录和文件</param>
        /// <param name="sPathType">路径类型：1全路径，2仅文件名，3相对路径</param>
        /// <param name="IsSearchDept"></param>
        /// <param name="iDeep"></param>
        private void GetDirectoryFile(StringBuilder sb, DirectoryInfo rootDirectory, string sOutType, string sPathType, bool IsSearchDept,int iDeep)
        {
            if (IsSearchDept)//指定目录深度
            {
                if (iDeep > 0)
                {
                    iDeep--;
                }
                else
                {
                    return;
                }
            }

            if (sOutType == "1" || sOutType == "3")//仅文件、目录和文件 
            {
                foreach (var file in rootDirectory.GetFiles()) //文件的处理
                {
                    if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
                    {
                        continue;
                    }
                    bool isContinue = false;

                    //跳过忽略的相对目录
                    foreach (string sDir in sExcludeFullDir)
                    {
                        string sDirNew = sDir.Replace("\\", "").Replace("/", "");
                        if (file.FullName.StartsWith(sDirNew, StringComparison.OrdinalIgnoreCase))
                        {
                            isContinue = true;
                            break;
                        }
                        if (file.FullName.Contains("/" + sDirNew + "/") || file.FullName.Contains("\\" + sDirNew + "\\"))
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (isContinue)
                    {
                        continue;
                    }

                    //跳过忽略的后缀
                    foreach (string sExt in sExcludeExt)
                    {
                        string sExtNew = sExt.StartsWith(".") ? sExt : "." + sExt;
                        if (file.Extension.Equals(sExtNew, StringComparison.OrdinalIgnoreCase))
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (isContinue)
                    {
                        continue;
                    }

                    //跳过忽略的相对文件名
                    foreach (string sFile in sExcludeFullFile)
                    {
                        if (file.Name.Equals(sFile, StringComparison.OrdinalIgnoreCase))
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (isContinue)
                    {
                        continue;
                    }

                    string sDirFileName;
                    if (sPathType == "1")//全路径
                    {
                        sDirFileName = ckbShowFileDir.Checked ? file.DirectoryName : file.FullName;//【仅显示文件目录】复选框
                        sb.Append(DealString(sDirFileName) + "\n");
                    }
                    else if (sPathType == "2")//仅文件名
                    {
                        sb.Append(DealString(file.Name) + "\n");
                    }
                    else//相对路径
                    {
                        sDirFileName = ckbShowFileDir.Checked ? file.DirectoryName + "\\" : file.FullName;//【仅显示文件目录】复选框
                        sb.Append(DealString(sDirFileName.Replace(sCurRelateDir, "")) + "\n");
                    }
                }
            }

            if (sOutType == "2" || sOutType == "3") //仅目录、目录和文件 
            {
                foreach (var path in rootDirectory.GetDirectories())//目录的处理
                {
                    bool isContinue = false;
                    //跳过忽略的相对目录
                    foreach (string sDir in sExcludeFullDir)
                    {
                        string sDirNew = sDir.Replace("\\", "").Replace("/", "");
                        if (path.FullName.StartsWith(sDirNew, StringComparison.OrdinalIgnoreCase))
                        {
                            isContinue = true;
                            break;
                        }
                        if (path.FullName.Contains("/"+ sDirNew + "/") || path.FullName.Contains("\\" + sDirNew + "\\"))
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (isContinue)
                    {
                        continue;
                    }
                    if (sPathType == "1")//全路径
                    {
                        sb.Append(DealString(path.FullName) + "\n");
                    }
                    else if (sPathType == "2")//仅文件名
                    {
                        sb.Append(path.FullName.Replace(sCurRelateDir, "") + "\n");
                    }
                    else//相对路径
                    {
                        sb.Append(path.FullName.Replace(sCurRelateDir, "") + "\n");
                    }
                }
            }

            //迭代子目录
            foreach (var path in rootDirectory.GetDirectories())
            {
                GetDirectoryFile(sb, path, sOutType, sPathType, IsSearchDept, iDeep);
            }

        } 
        #endregion

        private string DealString(string str)
        {
            StringBuilder sb = new StringBuilder();
            string sPre = txbPre.Text;
            string sEnd = txbEnd.Text;
            string sOld = txbOld.Text.Trim();
            string sNew = txbNew.Text.Trim();

            if(!string.IsNullOrEmpty(sOld))
            {
                sb.Append(str.Replace(sOld, sNew));
            }
            else
            {
                sb.Append(str);
            }

            if (!string.IsNullOrEmpty(sPre))
            {
                sb.Insert(0, sPre);
            }
            if (!string.IsNullOrEmpty(sEnd))
            {
                sb.Append(sEnd);
            }
            return sb.ToString();
        }

    }
}
