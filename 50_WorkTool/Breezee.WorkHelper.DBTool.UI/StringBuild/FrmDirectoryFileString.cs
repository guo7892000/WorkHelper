using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;
using System.Text;
using AppSet = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using Breezee.Core.Interface;
using System.IO;
using System;
using System.Windows.Forms;
using Breezee.WorkHelper.DBTool.Entity;

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
            strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.DirStringLastSelectedPath, "").Value;

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
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get("LastSelectedPath", "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbSelectPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.DirStringLastSelectedPath, dialog.SelectedPath, "【目录字符生成】最后选择的目录");
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
            StringBuilder sb = new StringBuilder();
            DirectoryInfo rootDirectory = new DirectoryInfo(sPath);
            //
            GetDirectoryFile(sb, rootDirectory, strOutType, strPathType, isSearchChild, iDept);
            rtbString.AppendText(sb.ToString());
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

            string sSelectPath = txbSelectPath.Text.Trim();
            string sRelatePath = sSelectPath.EndsWith("\\") ? sSelectPath : sSelectPath + "\\";

            if (sOutType == "1" || sOutType == "3")//仅文件、目录和文件 
            {
                foreach (var file in rootDirectory.GetFiles()) //文件的处理
                {
                    if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
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
                        sb.Append(DealString(sDirFileName.Replace(sRelatePath, "")) + "\n");
                    }
                }
            }

            if (sOutType == "2" || sOutType == "3") //仅目录、目录和文件 
            {
                foreach (var path in rootDirectory.GetDirectories())//目录的处理
                {
                    if (sPathType == "1")//全路径
                    {
                        sb.Append(DealString(path.FullName) + "\n");
                    }
                    else if (sPathType == "2")//仅文件名
                    {
                        sb.Append(path.FullName.Replace(sRelatePath, "") + "\n");
                    }
                    else//相对路径
                    {
                        sb.Append(path.FullName.Replace(sRelatePath, "") + "\n");
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
