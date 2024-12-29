using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;
using System.Text;
using AppSet = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using Breezee.Core.Interface;
using System.IO;
using System;
using System.Windows.Forms;
using Breezee.WorkHelper.DBTool.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Breezee.Core.Tool.Helper;
using LibGit2Sharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：获取修改过的文件清单
    /// 创建作者：黄国辉
    /// 创建日期：2023-6-9
    /// 功能说明：获取修改过的文件清单
    /// </summary>
    public partial class FrmGetUpdateDirectoryFile : BaseForm
    {
        #region 变量
        string strLastSelectedPath;
        int iFileNum = 0;
        string sDirType;
        //分隔的字符数组
        char[] splitCharArr = new char[] { ',', '，', '：', ';', '；','|' };
        List<string> _listFilePath;
        string[] sExcludeDirName; //排除的目录名
        string[] sExcludeFullDir;
        string[] sExcludeFileName; //排除的文件名
        string[] sExcludeFullFileName;
        #endregion

        #region 构造函数
        public FrmGetUpdateDirectoryFile()
        {
            InitializeComponent();
        }

        #endregion

        #region 加载事件
        private void FrmDirectoryFileString_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now.AddHours(-10);
            dtpEnd.Value = DateTime.Now;
            lblExcludeTip.Text = "支持逗号（中英文）、分号（中英文）、冒号（中文）、竖线（英文）分隔的多个排除项配置！另外：会过滤以.开头的文件夹";
            //绑定下拉框
            _dicString["1"]= "含git源码的目录";
            _dicString["9"] = "普通目录";
            cbbDirType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false),false,true);
            toolTip1.SetToolTip(cbbDirType, "【含git源码的目录】：针对有变化的新增和修改的文件，git拉取下来的文件不含在内，但【普通目录】会包括！");

            //加载用户偏好值
            //读取目录
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ReadPath, "").Value;

            if(!string.IsNullOrEmpty(strLastSelectedPath))
            {
                txbReadPath.Text = strLastSelectedPath;
            }
            //生成目录
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_TargetPath, "").Value;
            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                txbTargetPath.Text = strLastSelectedPath;
            }
            ckbDateDir.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_IsGenerateDateTimeDir, "0").Value) ? true : false;
            cbbDirType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_DirType, "1").Value;
            //排除项
            txbExcludeEndprx.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ExcludeEndprx, "").Value;
            txbExcludeDirName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ExcludeDirName, "").Value;
            txbExcludeFullDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ExcludeFullDir, "").Value;
            txbExcludeFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ExcludeFileName, "").Value;
            txbExcludeFullFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ExcludeFullFileName, "").Value;
            //包含已提交
            ckbNowModify.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_IsIncludeModify, "1").Value) ? true : false;
            ckbNowAdd.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_IsIncludeAdd, "1").Value) ? true : false;
            ckbIncludeCommit.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_IsIncludeCommit, "1").Value) ? true : false;
            txbEmail.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_Email, "").Value;
            txbUserName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_UserName, "").Value;

            groupBox2.AddFoldRightMenu();
            grbExclude.AddFoldRightMenu();
        } 
        #endregion

        #region 读取路径按钮事件
        private void btnReadPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_ReadPath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbReadPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ReadPath, dialog.SelectedPath, "【获取修改过的文件】最后选择的读取目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        #endregion

        #region 目标路径按钮事件
        private void btnTargetPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_TargetPath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbTargetPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_TargetPath, dialog.SelectedPath, "【获取修改过的文件】最后选择的生成目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        #endregion

        #region 生成SQL按钮事件
        private async void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                string sPath = txbReadPath.Text.Trim();
                if (string.IsNullOrEmpty(sPath))
                {
                    ShowErr("请选择读取目录！");
                    return;
                }

                string sTargePath = txbTargetPath.Text.Trim();
                if (string.IsNullOrEmpty(sTargePath))
                {
                    ShowErr("请选择生成目录！");
                    return;
                }

                if (ckbEndToNow.Checked)
                {
                    dtpEnd.Value = DateTime.Now;
                }
                else
                {
                    if (dtpBegin.Value.CompareTo(dtpEnd.Value) > 0)
                    {
                        ShowErr("修改的开始时间不能大于结束时间！");
                        return;
                    }
                }

                rtbString.Clear();
                _listFilePath = new List<string>();
                StringBuilder sb = new StringBuilder();
                DirectoryInfo rootDirectory = new DirectoryInfo(sPath);
                //查找并输出文件
                iFileNum = 0;
                sDirType = cbbDirType.SelectedValue.ToString();
                if ("1".Equals(sDirType))
                {
                    if (!ckbNowAdd.Checked && !ckbNowModify.Checked && !ckbIncludeCommit.Checked)
                    {
                        ShowInfo("含正在修改、含将新增、含之前提交，这三个复选框至少选择一个！");
                        return;
                    }
                }
                else
                {

                }
                tsbAutoSQL.Enabled = false;
                ShowDestopTipMsg("正在异步获取文件清单，请稍等一会...");
                //异步获取文件
                await Task.Run(() => GetDirectoryFile(sb, rootDirectory));
                tsbAutoSQL.Enabled = true; //重置按钮为有效
                rtbString.AppendText(sb.ToString());

                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ReadPath, sPath, "【获取修改过的文件】最后选择的读取目录");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_TargetPath, sTargePath, "【获取修改过的文件】最后选择的生成目录");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ExcludeEndprx, txbExcludeEndprx.Text.Trim(), "【获取修改过的文件】的排除扩展名列表");

                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ExcludeDirName, txbExcludeDirName.Text.Trim(), "【获取修改过的文件】的排除目录名列表");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ExcludeFullDir, txbExcludeFullDir.Text.Trim(), "【获取修改过的文件】的排除全路径目录列表");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ExcludeFileName, txbExcludeFile.Text.Trim(), "【获取修改过的文件】的排除文件名列表");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_ExcludeFullFileName, txbExcludeFullFile.Text.Trim(), "【获取修改过的文件】的排除全路径文件名列表");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_IsGenerateDateTimeDir, ckbDateDir.Checked ? "1" : "0", "【获取修改过的文件】的是否生成日期目录");

                if (ckbSaveEndTime.Checked)
                {
                    WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_LastSaveEndDateTime, dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"), "【获取修改过的文件】的最后修改时间");
                }

                //包含已提交
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_IsIncludeAdd, ckbNowAdd.Checked ? "1" : "0", "【获取修改过的文件】的是否包含将增加");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_IsIncludeModify, ckbNowModify.Checked ? "1" : "0", "【获取修改过的文件】的是否包含正在修改");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_IsIncludeCommit, ckbIncludeCommit.Checked ? "1" : "0", "【获取修改过的文件】的是否包含之前提交");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_Email, txbEmail.Text.Trim(), "【获取修改过的文件】的Email");
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFile_UserName, txbUserName.Text.Trim(), "【获取修改过的文件】的用户名");

                WinFormContext.UserLoveSettings.Save();
                if (iFileNum <= 0)
                {
                    ShowInfo("异步获取文件清单完成，没有修改的文件！");
                }
                else
                {
                    ShowInfo("异步获取文件清单完成，修改的文件数为：" + iFileNum.ToString());
                }
            }
            catch(Exception ex)
            {
                ShowErr(ex.Message);
            }
            
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
        private async void GetDirectoryFile(StringBuilder sb, DirectoryInfo rootDirectory)
        {
            
            //得到排除项
            sExcludeDirName = txbExcludeDirName.Text.Trim().ToLower().Split(splitCharArr); //得到排除的目录名
            sExcludeFullDir = txbExcludeFullDir.Text.Trim().ToLower().Split(splitCharArr); //得到排除的绝对目录
            sExcludeFileName = txbExcludeFile.Text.Trim().ToLower().Split(splitCharArr); //得到排除的文件名
            sExcludeFullFileName = txbExcludeFullFile.Text.Trim().ToLower().Split(splitCharArr); //得到排除的绝对路径文件名

            if ("1".Equals(sDirType))
            {
                //1：git源码管理目录处理
                bool isGitDir = false;
                DirectoryInfo[] dirArr = rootDirectory.GetDirectories();//迭代子目录
                var gitDir = dirArr.AsQueryable().Where(t => t.Name.Equals(".git", StringComparison.OrdinalIgnoreCase));
                //判断是否
                if (gitDir.Count() > 0)
                {
                    //
                    isGitDir = true;
                    string sDirName = gitDir.First().FullName;
                    string sParentDirName = gitDir.First().Parent.FullName;
                    string sEmail= txbEmail.Text.Trim();
                    string sUserName = txbUserName.Text.Trim();

                    using (var repo = new Repository(sDirName))
                    {
                        //查找变化的文件：包括未跟踪的
                        using (var changes = repo.Diff.Compare<TreeChanges>(null, true))
                        {
                            if (ckbNowAdd.Checked)
                            {
                                //新增的
                                foreach (var item in changes.Added)
                                {
                                    FileInfo file = new FileInfo(Path.Combine(sParentDirName, item.Path)); 
                                    if (file.Exists)
                                    {
                                        CopyFileBackup(file, sb);
                                    }
                                }
                            }
                            if (ckbNowModify.Checked)
                            {
                                //修改的
                                foreach (var item in changes.Modified)
                                {
                                    FileInfo file = new FileInfo(Path.Combine(sParentDirName, item.Path));
                                    if (file.Exists)
                                    {
                                        CopyFileBackup(file, sb);
                                    }
                                }
                            }
                        }
                        //查找已提交的
                        if (ckbIncludeCommit.Checked)
                        {
                            ///get commits from all branches, not just master
                            var commits = repo.Commits.QueryBy(new CommitFilter());
                            foreach (var commit in commits)
                            {
                                //取指定人提交的文件
                                if (!string.IsNullOrEmpty(sEmail) && !sEmail.Equals(commit.Author.Email)) continue; //如邮件不为空，且不相等，则跳过
                                if (!string.IsNullOrEmpty(sUserName) && !sUserName.Equals(commit.Author.Name)) continue;//如用户名不为空，且不相等，则跳过
                                //不在选择的时间范围内
                                if (commit.Committer.When< dtpBegin.Value || commit.Committer.When > dtpEnd.Value)
                                {
                                    continue;
                                }
                                foreach (var parent in commit.Parents)
                                {
                                    //要排除冲突时帮其他人提交的文件：不知道为什么还是包括不是指定人改的文件？？？
                                    if (!string.IsNullOrEmpty(sEmail) && !sEmail.Equals(parent.Author.Email)) continue; //如邮件不为空，且不相等，则跳过
                                    if (!string.IsNullOrEmpty(sUserName) && !sUserName.Equals(parent.Author.Name)) continue;//如用户名不为空，且不相等，则跳过
                                    foreach (TreeEntryChanges change in repo.Diff.Compare<TreeChanges>(parent.Tree, commit.Tree))
                                    {
                                        FileInfo file = new FileInfo(Path.Combine(sParentDirName, change.Path));
                                        if (file.Exists)
                                        {
                                            CopyFileBackup(file, sb,true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //当前目录不包括.git目录，那么继续查找子目录
                if (!isGitDir)
                {
                    foreach (DirectoryInfo path in rootDirectory.GetDirectories())
                    {
                        if (path.Name.StartsWith("."))
                        {
                            continue; //跳过点开头的系统目录
                        }
                        //目录名、绝对目录
                        if (sExcludeDirName.Contains(path.Name.ToLower()) || sExcludeFullDir.Contains(path.FullName.ToLower()))
                        {
                            continue;
                        }
                        GetDirectoryFile(sb, path);
                    }
                }
            }
            else
            {
                //9：普通目录的处理
                foreach (FileInfo file in rootDirectory.GetFiles()) //文件的处理
                {
                    CopyFileBackup(file,sb);
                }

                foreach (DirectoryInfo path in rootDirectory.GetDirectories())
                {
                    if (path.Name.StartsWith("."))
                    {
                        continue; //跳过点开头的系统目录
                    }
                    //目录名、绝对目录
                    if (sExcludeDirName.Contains(path.Name.ToLower()) || sExcludeFullDir.Contains(path.FullName.ToLower()))
                    {
                        continue;
                    }
                    GetDirectoryFile(sb, path);
                }
            }
        }
        #endregion

        /// <summary>
        /// 复制文件备份
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sb"></param>
        private void CopyFileBackup(FileInfo file, StringBuilder sb,bool isCommitFile=false)
        {
            string sReadPath = txbReadPath.Text.Trim();
            string sTargetPath = txbTargetPath.Text.Trim();
            if (ckbDateDir.Checked)
            {
                sTargetPath = Path.Combine(txbTargetPath.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
            }
            string[] sExcludeEndprx = txbExcludeEndprx.Text.Trim().ToLower().Split(splitCharArr);//得到排除的后缀

            if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
            {
                return;
            }
            //这里如果是包括已提交，则直接跳过
            if (!isCommitFile)
            {
                if(file.LastWriteTime < dtpBegin.Value || file.LastWriteTime > dtpEnd.Value)
                {
                    return;  //不在修改时间范围内的文件跳过
                }
            }
            //排除后缀
            if (!string.IsNullOrEmpty(file.Extension) && (sExcludeEndprx.Contains(file.Extension.Substring(1))))
            {
                return;
            }

            //跳过忽略的文件名、绝对路径文件名
            if (sExcludeFileName.Contains(file.Name.ToLower()) || sExcludeFullFileName.Contains(file.FullName.ToLower()))
            {
                return;
            }

            //跳过忽略的绝对目录
            if (sExcludeFullDir.Contains(file.DirectoryName))
            {
                return;
            }

            //跳过忽略的目录名
            string[] sFileDirs = file.DirectoryName.ToLower().Split(new char[] { '\\', '/' });
            foreach (var sDir in sFileDirs)
            {
                if (sExcludeDirName.Contains(sDir))
                {
                    return;
                }
            }

            //跳过已包含的文件
            if (_listFilePath.Contains(file.FullName))
            {
                return;
            }
            _listFilePath.Add(file.FullName);
            //生成目录
            string sFinalDir = file.DirectoryName.Replace(sReadPath, sTargetPath);
            //复制文件
            FileDirHelper.CopyFilesToDirKeepSrcDirName(file.FullName, sFinalDir);
            sb.Append(Path.Combine(sFinalDir, file.Name) + "\n");
            iFileNum++;
        }

        private void ckbSetBeginAsLastSaveEnd_CheckedChanged(object sender, EventArgs e)
        {
            string lastEndDateTim = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFile_LastSaveEndDateTime, "").Value;
            dtpBegin.Enabled = true;
            if (ckbSetBeginAsLastSaveEnd.Checked)
            {
                if (!string.IsNullOrEmpty(lastEndDateTim))
                {
                    dtpBegin.Value = DateTime.Parse(lastEndDateTim);
                    dtpBegin.Enabled = false;
                }
            }
            
        }

        /// <summary>
        /// 结束时间为当前时间复选框选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbEndToNow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEndToNow.Checked)
            {
                dtpEnd.Value = DateTime.Now;
                dtpEnd.Enabled = false;
            }
            else
            {
                dtpEnd.Enabled = true;
            }
        }

        private void ckbIncludeCommit_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbIncludeCommit.Checked)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
        }

        private void cbbDirType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDirType.SelectedValue == null) return;
            if ("1".Equals(cbbDirType.SelectedValue.ToString()))
            {
                panel1.Visible = true;
                panel2.Visible = true;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = false;
            }
        }
    }
}
