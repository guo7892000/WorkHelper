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
        //分隔的字符数组
        char[] splitCharArr = new char[] { ',', '，', '：', ';', '；','|' };
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
            dtpEnd.Value = DateTime.Now.AddDays(1);
            lblExcludeTip.Text = "支持逗号（中英文）、分号（中英文）、冒号（中文）、竖线（英文）分隔的多个排除项配置！";
            //加载用户偏好值
            //读取目录
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileReadPath, "").Value;

            if(!string.IsNullOrEmpty(strLastSelectedPath))
            {
                txbReadPath.Text = strLastSelectedPath;
            }
            //生成目录
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileTargetPath, "").Value;
            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                txbTargetPath.Text = strLastSelectedPath;
            }
            ckbDateDir.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileIsGenerateDateTimeDir, "0").Value) ? true : false;
            //排除项
            txbExcludeEndprx.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileExcludeEndprx, "").Value; 
            txbExcludeDirName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileExcludeDirName, "").Value;
            txbExcludeFullDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileExcludeFullDir, "").Value;
        } 
        #endregion

        #region 读取路径按钮事件
        private void btnReadPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileReadPath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbReadPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileReadPath, dialog.SelectedPath, "【获取修改过的文件】最后选择的读取目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        #endregion

        #region 目标路径按钮事件
        private void btnTargetPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.GetFileTargetPath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbTargetPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileTargetPath, dialog.SelectedPath, "【获取修改过的文件】最后选择的生成目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        #endregion

        #region 生成SQL按钮事件
        private void tsbAutoSQL_Click(object sender, EventArgs e)
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

            if (dtpBegin.Value.CompareTo(dtpEnd.Value)>0)
            {
                ShowErr("修改的开始时间不能大于结束时间！");
                return;
            }

            rtbString.Clear();

            StringBuilder sb = new StringBuilder();
            DirectoryInfo rootDirectory = new DirectoryInfo(sPath);
            //查找并输出文件
            iFileNum = 0;
            GetDirectoryFile(sb, rootDirectory);
            rtbString.AppendText(sb.ToString());

            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileReadPath, sPath, "【获取修改过的文件】最后选择的读取目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileTargetPath, sTargePath, "【获取修改过的文件】最后选择的生成目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileExcludeEndprx, txbExcludeEndprx.Text.Trim(), "【获取修改过的文件】的排除扩展名列表");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileExcludeDirName, txbExcludeDirName.Text.Trim(), "【获取修改过的文件】的排除目录名列表");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileExcludeFullDir, txbExcludeFullDir.Text.Trim(), "【获取修改过的文件】的排除全路径目录列表");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.GetFileIsGenerateDateTimeDir, ckbDateDir.Checked ? "1" : "0", "【获取修改过的文件】的是否生成日期目录");
            WinFormContext.UserLoveSettings.Save();
            if (iFileNum <= 0)
            {
                ShowInfo("没有修改的文件！");
            }
            else
            {
                ShowInfo("修改的文件数为："+iFileNum.ToString());
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
        private void GetDirectoryFile(StringBuilder sb, DirectoryInfo rootDirectory)
        {
            string sReadPath = txbReadPath.Text.Trim();
            string sTargetPath = txbTargetPath.Text.Trim();
            if (ckbDateDir.Checked)
            {
                sTargetPath = Path.Combine(txbTargetPath.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
            }
            
            //得到排除项
            string[] sExcludeEndprx = txbExcludeEndprx.Text.Trim().ToLower().Split(splitCharArr);//得到排除的后缀
            string[] sExcludeDirName = txbExcludeDirName.Text.Trim().ToLower().Split(splitCharArr);//得到排除的文件名
            string[] sExcludeFullDir = txbExcludeFullDir.Text.Trim().ToLower().Split(splitCharArr);//得到排除的绝对目录

            foreach (FileInfo file in rootDirectory.GetFiles()) //文件的处理
            {
                if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
                {
                    continue;
                }
                //不在修改时间范围内的文件跳过
                if (file.LastWriteTime< dtpBegin.Value || file.LastWriteTime > dtpEnd.Value)
                {
                    continue;
                }
                //排除后缀
                if (sExcludeEndprx.Contains(file.Extension.Substring(1)) )
                {
                    continue;
                }

                //生成目录
                string sFinalDir = file.DirectoryName.Replace(sReadPath, sTargetPath);
                //复制文件
                FileDirHelper.CopyFilesToDirKeepSrcDirName(file.FullName, sFinalDir);
                sb.Append(Path.Combine(sFinalDir, file.Name) + "\n");
                iFileNum++;
            }
            
            //迭代子目录
            foreach (DirectoryInfo path in rootDirectory.GetDirectories())
            {
                //排除后缀、目录名、绝对目录
                if (sExcludeDirName.Contains(path.Name.ToLower()) || sExcludeFullDir.Contains(path.FullName.ToLower()))
                {
                    continue;
                }
                GetDirectoryFile(sb, path);
            }

        } 
        #endregion


    }
}
