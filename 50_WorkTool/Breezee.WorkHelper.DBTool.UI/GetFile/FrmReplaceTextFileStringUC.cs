using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Breezee.Core;
using Breezee.Core.Tool.Helper;
using System.Collections;
using org.breezee.MyPeachNet;
using FluentFTP.Rules;
using Renci.SshNet;
using FluentFTP.Helpers;
using StaticConstant = Breezee.Core.Entity.StaticConstant;
using LibGit2Sharp;
using Renci.SshNet.Sftp;
using static System.Collections.Specialized.BitVector32;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 替换文本文件字符：将文本文件内容进行替换
    /// @history:
    ///   2023-10-15 huangguohui 新增    
    /// </summary>
    public partial class FrmReplaceTextFileStringUC : BaseForm
    {
        private readonly string _sGridColumnSelect = "IsSelect";
        private bool _allSelectFtp = false;//默认全选，这里取反
        private bool _allSelectWait = false;//默认全选，这里取反
        private bool _allSelectOldNewChar = false;//默认全选，这里取反
        private bool _allSelectResult = false;//默认全选，这里取反
        DataGridViewFindText dgvFindText;
        ReplaceStringXmlConfig replaceStringData;//替换字符模板XML配置
        private delegate void BindGrid(DataTable dt); //绑定网格代理
        public FrmReplaceTextFileStringUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmReplaceTextFileString_Load(object sender, EventArgs e)
        {
            //自定义FTP控件的事件
            uC_FtpDownUpload1.ShowGlobalMsg += ShowGlobalMsg_Click;
            uC_FtpDownUpload1.AfterDownAllFile += UC_FtpDownUpload1_AfterDownAllFile;
            //uC_FtpDownUpload1.SetFileEncoding(cbbFileSource.SelectedValue.ToString(), cbbFileSource.Text, true);
            uC_FtpDownUpload1.SetUploadLocalDir(txbSavePath.Text, true);
            uC_FtpDownUpload1.BeforeUploadSelectedFile += UC_FtpDownUpload1_BeforeUploadSelectedFile;
            //绑定下拉框
            BindingDropDownList();
            //设置Tag
            SetTag();
            //初始化配置
            LoadFuncConfig();

            //其他
            toolTip1.SetToolTip(btnReplaceString, "直接使用【最终生成路径】中的文件，进行文本替换！");
            toolTip1.SetToolTip(btnCopyFileReplace, "复制【待复制的本地源目录或源文件】到【最终生成路径】中，并对【最终生成路径】中所有文件，进行文本替换！");
            toolTip1.SetToolTip(btnFinalResultCopy, "【复制】时，会先删除原目录，再创建目录并增加文件。"); 

            lblInfo2.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：所有行都为数据！";
            lblReplaceInfo.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：所有行都为数据！";
            
        }

        private void UC_FtpDownUpload1_BeforeUploadSelectedFile(object sender, EventArgs e)
        {
            DataTable dtResult = dgvResult.GetBindingTable();
            DataRow[] drWillArr = dtResult.Select(_sGridColumnSelect + "= '1'");
            if (drWillArr.Length == 0)
            {
                ShowErr("没有要上传的文件！");
                return;
            }
            uC_FtpDownUpload1.SetSelectRows(drWillArr);
        }

        #region 下载所有文件后绑定代处理网格
        /// <summary>
        /// FTP下载所有文件后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_FtpDownUpload1_AfterDownAllFile(object sender, AfterDownFileEventArgs e)
        {
            DataTable dtWaitList = dgvFileListWaitFor.GetBindingTable().Clone();
            BindGrid bindGrid = new BindGrid(BindWaitForDataGridView); //实例化代理对象，参数为要调用的操作UI元素方法
            foreach (DownFileInfo item in e.DownFile)
            {
                dtWaitList.Rows.Add(item.Idx, item.IsSelected, item.FileType, item.FullPath, item.RelPath); //添加到待处理网格中
            }
            if (dgvFileListWaitFor.InvokeRequired)
            {
                dgvFileListWaitFor.Invoke(bindGrid, dtWaitList);
            }
            else
            {
                bindGrid(dtWaitList);
            }
        }

        private void BindWaitForDataGridView(DataTable dtWaitList)
        {
            dgvFileListWaitFor.BindDataGridView(dtWaitList, false);
        } 
        #endregion

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

        private void BindingDropDownList()
        {
            _dicString["1"] = "本地文件";
            _dicString["2"] = "FTP远程文件";
            cbbFileSource.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            _dicString.Clear();
            _dicString["1"] = "先删后增";
            _dicString["2"] = "覆盖";
            _dicString["3"] = "仅新增";
            cbbLocalCopyType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbLocalCopyType, "本地文件复制方式");

            cbbFileContentCharSetEncode.BindTypeValueDropDownList(BaseFileEncoding.GetEncodingTable(false), false, true);
            toolTip1.SetToolTip(cbbFileContentCharSetEncode, "如文件出现乱码，需要修改文件字符集！");

            //加载模板数据
            replaceStringData = new ReplaceStringXmlConfig(DBTGlobalValue.ReplaceTextFileString.Xml_FileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, ReplaceStringXmlConfig.KeyString.Name, true, true);
        }

        private void SetTag()
        {
            //本地目录或文件
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("TYPE").Caption("类型").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("FILE_PATH").Caption("路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("COPY_PATH").Caption("复制相对路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build()
                );
            dgvFileListWaitFor.Tag = fdc.GetGridTagString();
            dgvFileListWaitFor.BindDataGridView(null, false);
            //新旧字符网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("OLD").Caption("旧字符").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("NEW").Caption("新字符").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(100).Edit(true).Visible().Build()
                );
            dgvOldNewChar.Tag = fdc.GetGridTagString();
            dgvOldNewChar.BindDataGridView(null, false);
            dgvOldNewChar.AllowUserToAddRows = true;
            //结果网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("FILE_PATH").Caption("文件全路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(500).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("COPY_PATH").Caption("复制相对路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("COPY_FILE").Caption("复制文件").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible(false).Build()
                );
            dgvResult.Tag = fdc.GetGridTagString();
            dgvResult.BindDataGridView(null, false);

            //用于筛选结果网格的文件名网格：
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                //new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("FILE_NAME").Caption("文件名").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(200).Edit(true).Visible().Build()
                );
            dgvResultFilter.Tag = fdc.GetGridTagString();
            dgvResultFilter.BindDataGridView(null, false);
            dgvResultFilter.AllowUserToAddRows= true;
        }

        /// <summary>
        /// 生成文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            btnCopyFileReplace.PerformClick();
        }


        /// <summary>
        /// 替换文件文本
        /// </summary>
        /// <param name="sSavePath"></param>
        private bool ReplaceFileTextJude(string sSavePath,bool isCopyFile)
        {
            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
            if (drReplace.Length == 0)
            {
                ShowErr("没有需要替换的字符！");
                return false;
            }

            if (ckbLoadFinalSaveDirFile.Checked)
            {
                DataTable dtSource = dgvFileListWaitFor.GetBindingTable();
                DataRow[] drWillArr = dtSource.Select(_sGridColumnSelect + "= '1'");
                if (drWillArr.Length == 0)
                {
                    ShowErr("没有要处理的文件，请先选择！");
                    return false;
                }
            }
            else
            {
                //目录不存在则创建目录
                if (!Directory.Exists(sSavePath))
                {
                    Directory.CreateDirectory(sSavePath);
                }
                if (!isCopyFile)
                {
                    //读取保存目录文件进行
                    FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                    if (arrFiles.Length == 0)
                    {
                        ShowErr("【最终生成目录】下没有文件，请先复制文件！");
                        return false;
                    }
                }
            }
            return true;       
        }

        private async Task ReplaceFileTextAsync(string sSavePath, DataTable dtResult, string sFileEncoding, DataRow[] drReplace)
        {
            if (ckbLoadFinalSaveDirFile.Checked)
            {
                DataTable dtSource = dgvFileListWaitFor.GetBindingTable();
                DataRow[] drWillArr = dtSource.Select(_sGridColumnSelect + "= '1'");
                int iIdx = 1;
                //得到文件清单
                var files = drWillArr.AsEnumerable().Select(t => t["FILE_PATH"].ToString()).ToList();
                Encoding encoding = BaseFileEncoding.GetEncodingByKey(sFileEncoding);//字符集
                foreach (string file in files)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(File.ReadAllText(file, encoding));
                    foreach (DataRow dr in drReplace)
                    {
                        sb.Replace(dr["OLD"].ToString().Trim(), dr["NEW"].ToString().Trim());
                    }
                    // 处理每个文件
                    File.WriteAllText(file, sb.ToString(), encoding);
                    string sRelateDirFile = file.Replace(sSavePath, "");
                    string sRelateDir = sRelateDirFile.Substring(0, sRelateDirFile.LastIndexOf(@"\"));
                    string sRelateDirRealFile = sRelateDirFile.Substring(sRelateDirFile.LastIndexOf(@"\")).Replace("\\", "");
                    //结果添加文件
                    dtResult.Rows.Add(iIdx, "1", file, sRelateDir, sRelateDirRealFile);
                    iIdx++;
                }
            }
            else
            {
                FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                int iIdx = 1;
                //字符集
                Encoding encoding = BaseFileEncoding.GetEncodingByKey(sFileEncoding); 

                foreach (FileInfo file in arrFiles)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(File.ReadAllText(file.FullName, encoding));
                    foreach (DataRow dr in drReplace)
                    {
                        sb.Replace(dr["OLD"].ToString().Trim(), dr["NEW"].ToString().Trim());
                    }
                    // 处理每个文件
                    File.WriteAllText(file.FullName, sb.ToString(), encoding);
                    string sRelateDirFile = file.FullName.Replace(sSavePath, "");
                    string sRelateDir = sRelateDirFile.Substring(0, sRelateDirFile.LastIndexOf(@"\"));
                    string sRelateDirRealFile = sRelateDirFile.Substring(sRelateDirFile.LastIndexOf(@"\")).Replace("\\", "");
                    //结果添加文件
                    dtResult.Rows.Add(iIdx, "1", file.FullName, sRelateDir, sRelateDirRealFile);
                    iIdx++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbLoadFinalSaveDirFile_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLoadFinalSaveDirFile.Checked)
            {
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    ShowErr("【最终生成目录】不能为空！");
                    return;
                }
                //目录不存在则创建目录
                if (!Directory.Exists(sSavePath))
                {
                    Directory.CreateDirectory(sSavePath);
                }

                //读取保存目录文件进行
                FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                if (arrFiles.Length == 0)
                {
                    ShowErr("最终保存路径下没有文件，请先复制文件！");
                    return;
                }
                //将文件清单加载到网格中
                DataTable dtWaitList = dgvFileListWaitFor.GetBindingTable();
                dtWaitList.Rows.Clear();
                int iIndex = 1;
                foreach (FileInfo file in arrFiles)
                {
                    string sRelateDirFile = file.FullName.Replace(sSavePath, "");
                    string sRelateDir = sRelateDirFile.Substring(0, sRelateDirFile.LastIndexOf(@"\"));
                    string sRelateDirRealFile = sRelateDirFile.Substring(sRelateDirFile.LastIndexOf(@"\")).Replace("\\", "");
                    //结果添加文件
                    dtWaitList.Rows.Add(iIndex, "1", "文件", file.FullName, sRelateDir);
                    iIndex++;
                }

                btnCopyFileReplace.Enabled = false;
            }
            else
            {
                DataTable dtWaitList = dgvFileListWaitFor.GetBindingTable();
                dtWaitList.Rows.Clear();
                btnCopyFileReplace.Enabled = true;

            }
        }
        private async void btnCopyFileReplace_Click(object sender, EventArgs e)
        {
            try
            {
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    ShowErr("【最终生成目录】不能为空！");
                    return;
                }

                bool isNeedDeletSaveDir = false;
                bool isOverWrite = true;
                string sGeneratType = cbbLocalCopyType.SelectedValue.ToString();
                if (!Directory.Exists(sSavePath))
                {
                    Directory.CreateDirectory(sSavePath);
                }
                else
                {
                    if ("1".Equals(sGeneratType))
                    {
                        isNeedDeletSaveDir = true;
                    }
                }
                if ("1".Equals(sGeneratType))
                {
                    isOverWrite = false;
                }
                else
                {
                    isOverWrite = true;
                }

                DataTable dtWillDeal = dgvFileListWaitFor.GetBindingTable();
                DataRow[] drWill = dtWillDeal.Select(_sGridColumnSelect + "=  '1'");

                if (dtWillDeal.Rows.Count > 0 && drWill.Length == 0)
                {
                    ShowErr("请选择要复制的文件夹或文件！");
                    return;
                }

                if (ckbLoadFinalSaveDirFile.Checked && drWill.Length == 0)
                {
                    ShowErr("请选择要复制的文件！");
                    return;
                }

                string sFileSource = cbbFileSource.SelectedValue.ToString();
                bool isFtp = "2".Equals(sFileSource) ? true : false;

                string sFtpDownloadPath = string.Empty;
                if (isFtp)
                {
                    uC_FtpDownUpload1.SaveFtpConifg(false);
                    sFtpDownloadPath = uC_FtpDownUpload1.FtpServerInfo.DownloadLocalDir;
                }
                    
                #region 处理前的判断
                if (dtWillDeal.Rows.Count == 0)
                {
                    //网格没有行时
                    //非FTP或原FTP下载目录为空时
                    if (!isFtp || string.IsNullOrEmpty(sFtpDownloadPath))
                    {
                        ShowErr("没有要生成的文件！");
                        return;
                    }
                    else
                    {
                        //FTP且原FTP下载目录非空时
                        //读取原FTP下载目录进行复制
                        FileInfo[] arrFiles = new DirectoryInfo(sFtpDownloadPath).GetFiles("*.*", SearchOption.AllDirectories);
                        if (arrFiles.Length == 0)
                        {
                            ShowErr("FTP下载的【本地保存路径】下没有文件，请先从FTP服务器下载文件！");
                            return;
                        }
                    }
                }
                else
                {
                    //待处理网格选中数据时：复制文件到保存目录
                    foreach (DataRow dr in drWill)
                    {
                        string sFileType = dr["TYPE"].ToString().Trim();
                        string sFileName = dr["FILE_PATH"].ToString().Trim();
                        string sCopyPath = dr["COPY_PATH"].ToString().Trim();
                        //复制文件到【最终生成目录】时，拼接上相对路径
                        string sTargetPath = sSavePath + sCopyPath.Replace(@"/", @"\");
                        if ("目录".Equals(sFileType))
                        {
                            if (!Directory.Exists(sFileName))
                            {
                                ShowErr(sFileName + "目录不存在！");
                                return;
                            }
                        }
                        else
                        {
                            FileInfo file = new FileInfo(sFileName);
                            if (!File.Exists(file.FullName))
                            {
                                ShowErr(sFileName + "文件不存在！");
                                return;
                            }
                        }
                    }
                }
                #endregion

                DataTable dtResult = dgvResult.GetBindingTable().Clone();
                string sFileEncoding = cbbFileContentCharSetEncode.SelectedValue.ToString();
                //替换字符的判断
                if(!ReplaceFileTextJude(sSavePath,true))
                {
                    return;
                }
                if (isNeedDeletSaveDir)
                {
                    Directory.Delete(sSavePath, true);
                }
                DataTable dtReplace = dgvOldNewChar.GetBindingTable();
                DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");

                ShowInfo("已启动异步复制并替换，请稍等...");
                btnCopyFileReplace.Enabled = false;
                await Task.Run(() => CopyLocalFileAsync(sSavePath, dtWillDeal, drWill, isOverWrite, sFtpDownloadPath,dtResult, drReplace, sFileEncoding));
                btnCopyFileReplace.Enabled = true;

                SaveFuncConfig();//保存用户偏好值
                dgvResult.BindDataGridView(dtResult, false);
                dgvResult.ShowRowNum();
                tabControl1.SelectedTab = tpResult;
                ShowInfo("异步复制并替换完成！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnCopyFileReplace.Enabled = true;
            }
        }

        /// <summary>
        /// 异步复制本地文件方法
        /// </summary>
        /// <param name="sSavePath"></param>
        /// <param name="dtWillDeal"></param>
        /// <param name="drWill"></param>
        /// <param name="isOverWrite"></param>
        /// <param name="sFtpDownloadPath"></param>
        private async void CopyLocalFileAsync(string sSavePath, DataTable dtWillDeal, DataRow[] drWill, bool isOverWrite, string sFtpDownloadPath, DataTable dtResult, DataRow[] drReplace,string sFileEncoding)
        {
            if (dtWillDeal.Rows.Count == 0)
            {
                //网格没有行时
                //FTP且原FTP下载目录非空时
                //读取原FTP下载目录进行复制
                FileInfo[] arrFiles = new DirectoryInfo(sFtpDownloadPath).GetFiles("*.*", SearchOption.AllDirectories);

                //针对排除文件和目录过滤
                string sExcludeFileName = txbCopyExcludeFile.Text.Trim();
                string sExcludeDirName = txbCopyExcludeDir.Text.Trim();
                string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                string[] sFilterDir = sExcludeDirName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                var lsFiles = from f in arrFiles.AsEnumerable()
                              where GetDirCanCopyDynamicWhere(sFilter, sFilterDir, f)
                              select f;
                //字符集
                //Encoding encoding = GetEncoding(false);
                foreach (FileInfo file in lsFiles.ToArray())
                {
                    //目标路径要加上目录
                    string sRelateDirFile = file.FullName.Replace(sFtpDownloadPath, "");
                    string sRelateDir = sRelateDirFile.Substring(0, sRelateDirFile.LastIndexOf(@"\"));
                    //string sRelateDirRealFile = sRelateDirFile.Substring(sRelateDirFile.LastIndexOf(@"\")).Replace("\\", "");
                    //复制文件到最终保存路径
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(file.FullName, sSavePath + sRelateDir, false, isOverWrite);
                }
            }
            else
            {
                //待处理网格选中数据时：复制文件到保存目录
                foreach (DataRow dr in drWill)
                {
                    string sFileType = dr["TYPE"].ToString().Trim();
                    string sFileName = dr["FILE_PATH"].ToString().Trim();
                    string sCopyPath = dr["COPY_PATH"].ToString().Trim();
                    //复制文件到【最终生成目录】时，拼接上相对路径
                    string sTargetPath = sSavePath + sCopyPath.Replace(@"/", @"\");
                    if ("目录".Equals(sFileType))
                    {
                        FileDirHelper.CopyFilesToDirKeepSrcDirName(sFileName, sTargetPath, false, isOverWrite);
                    }
                    else
                    {
                        FileInfo file = new FileInfo(sFileName);
                        FileDirHelper.CopyFilesToDir(sFileName, sTargetPath, false, isOverWrite);
                    }
                }
            }
            //替换文件文本
            ReplaceFileTextAsync(sSavePath, dtResult, sFileEncoding, drReplace);
        }

        /// <summary>
        /// 是否目录或文件可复制的动态条件
        /// </summary>
        /// <param name="filterArr"></param>
        /// <param name="filterDirArr"></param>
        /// <param name="file"></param>
        /// <returns>true：可复制，false：不可复制</returns>
        private static bool GetDirCanCopyDynamicWhere(string[] filterArr, string[] filterDirArr, FileInfo file)
        {
            foreach (var item in filterArr)
            {
                if (file.Name.Contains(item))
                {
                    return false;
                }
            }
                
            foreach (var item in filterDirArr)
            {
                string[] sFilePath = file.DirectoryName.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (sFilePath.Contains(item))
                {
                    return false;
                }
            }
            //可复制
            return true;
        }

        private async void btnReplaceString_Click(object sender, EventArgs e)
        {
            try
            {
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    ShowErr("最终生成目录不能为空！");
                    return;
                }

                string sFileSource = cbbFileSource.SelectedValue.ToString();
                if ("2".Equals(sFileSource))
                {
                    uC_FtpDownUpload1.SaveFtpConifg(false);
                }

                DataTable dtResult = dgvResult.GetBindingTable().Clone();
                string sFileEncoding = cbbFileContentCharSetEncode.SelectedValue.ToString();
                //替换字符的判断
                if (!ReplaceFileTextJude(sSavePath,false))
                {
                    return;
                }
                DataTable dtReplace = dgvOldNewChar.GetBindingTable();
                DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
                //替换文件文本
                ShowInfo("已启动异步文件文本替换，请稍等...");
                btnReplaceString.Enabled = false;
                await Task.Run(() => ReplaceFileTextAsync(sSavePath, dtResult, sFileEncoding, drReplace));
                btnReplaceString.Enabled = true;
  
                SaveFuncConfig();//保存用户偏好值
                dgvResult.BindDataGridView(dtResult, false);
                dgvResult.ShowRowNum();
                //DataTable dtResultFiter = dgvResultFilter.GetBindingTable();
                //dgvResultFilter.ClearDataGridViewData();
                tabControl1.SelectedTab = tpResult;

                ShowInfo("文件替换完成！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnReplaceString.Enabled = true;
            }
        }

        #region 目录或文件选择
        /// <summary>
        /// 右键选择目录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddDir_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_RightAddDir, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = dgvFileListWaitFor.GetBindingTable();
                dt.Rows.Add(1, "1","目录", dialog.SelectedPath,""); //添加到待处理网格中
                dgvFileListWaitFor.ShowRowNum(true);
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_RightAddDir, dialog.SelectedPath, "【文本文件字符替换】最后添加的目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        /// <summary>
        /// 右键选择文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_RightAddFile, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.InitialDirectory = strLastSelectedPath;
            }
            dialog.Title = "请选择文件";
            dialog.Multiselect = true; //可以多选
            string sLastDir = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in dialog.FileNames)
                {
                    DataTable dt = dgvFileListWaitFor.GetBindingTable();
                    dt.Rows.Add(1, "1", "文件", item, ""); //添加到待处理网格中
                    dgvFileListWaitFor.ShowRowNum(true);
                    sLastDir = Path.GetFullPath(item);
                }
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_RightAddFile, sLastDir, "【文本文件字符替换】最后添加的文件所在目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }

        /// <summary>
        /// 最终生成目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_SavePath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbSavePath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_SavePath, dialog.SelectedPath, "【文本文件字符替换】最终生成目录");
                WinFormContext.UserLoveSettings.Save();
            }
        }


        /// <summary>
        /// 替换结果的二次筛选保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinalResultSelectDir_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_ReplaceResultFilterSavePath, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbFinalResultSavePath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_ReplaceResultFilterSavePath, dialog.SelectedPath, "【文本文件字符替换】替换结果的二次筛选保存路径");
                WinFormContext.UserLoveSettings.Save();
            }
        }
        #endregion

        #region 右键菜单
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            DataRow dataRow = dgvSelect.GetCurrentRow();
            if (dataRow == null || dataRow.RowState == DataRowState.Detached) return;
            dt.Rows.Remove(dataRow);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            DataTable dt = dgvSelect.GetBindingTable();
            dt.Rows.Clear();
        }

        /// <summary>
        /// 右键菜单打开中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip cu = sender as ContextMenuStrip;
            DataGridView dgvSelect = cu.SourceControl as DataGridView;
            if (dgvSelect == dgvFileListWaitFor && !ckbLoadFinalSaveDirFile.Checked)
            {
                //非加载最终生成目录文件时
                cu.Items[0].Visible = true;
                cu.Items[1].Visible = true;
            }
            else
            {
                //加载最终生成目录文件时
                cu.Items[0].Visible = false;
                cu.Items[1].Visible = false;
            }
        } 
        #endregion
       
        #region 复制文件的排除
        private void btnCopyExclude_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbCopyExcludeFile.Text.Trim();
            string sExcludeDirName = txbCopyExcludeDir.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName) && string.IsNullOrEmpty(sExcludeDirName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            string[] sFilterDir = sExcludeDirName.Split(new char[] { ',', '，'}, StringSplitOptions.RemoveEmptyEntries);

            DataTable dtFtpFile = dgvFileListWaitFor.GetBindingTable();
            if (dtFtpFile.Rows.Count == 0) return;

            var query = from f in dtFtpFile.AsEnumerable()
                        where GetCopyLinqDynamicWhere(sFilter, sFilterDir, f)
                        select f;
            foreach (var item in query.ToList())
            {
                item[_sGridColumnSelect] = "0"; //设置为不选中
            }

            SaveFuncConfig();
        }

        private static bool GetCopyLinqDynamicWhere(string[] filterArr, string[] filterDirArr, DataRow drF)
        {
            string sFilePath = drF.Field<string>("FILE_PATH").Replace("/", @"\");
            string sFileName = sFilePath.Substring(sFilePath.LastIndexOf(@"\")).Replace(@"\", "");
            string sDirPath = sFilePath.Substring(0, sFilePath.LastIndexOf(@"\"));
            string[] arrSplit = sDirPath.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in filterArr)
            {
                if (sFilePath.Contains(item))
                {
                    return true;
                }
            }
            foreach (var item in filterDirArr)
            {           
                if (arrSplit.Contains(item))
                {
                    return true;
                }
            }
            return false;
        } 
        #endregion


        #region 配置相关
        private void LoadFuncConfig()
        {
            //加载设置值
            cbbFileSource.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_FileSource, "1").Value;
            txbSavePath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_SavePath, "").Value;

            cbbFileContentCharSetEncode.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_CharsetEncoding, BaseFileEncoding.FileEncodingString.UTF8).Value;
            cbbTemplateType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_TemplateType, "1").Value;
            cbbLocalCopyType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_GenerateType, "1").Value;

            txbCopyExcludeFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_CopyExcludeFileName, "").Value;
            txbCopyExcludeDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_CopyExcludeDirName, "").Value;

            txbFinalResultSavePath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_ReplaceResultFilterSavePath, "").Value;
            txbResultFilterExcludeFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_ResultCopyExcludeFileName, "").Value;//替换结果复制时的排除文件名
            txbFileFileEndfix.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_ResultCopyFileEndfix, "").Value;
            ckbIsUseEndfix.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace2_ResultCopyFileIsUseEndfix, "1").Value) ? true : false;

        }

        /// <summary>
        /// 保存功能配置
        /// </summary>
        private void SaveFuncConfig()
        {
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_FileSource, cbbFileSource.SelectedValue.ToString(), "【文本文件字符替换】文件来源");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_SavePath, txbSavePath.Text.Trim(), "【文本文件字符替换】最终生成目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_CharsetEncoding, cbbFileContentCharSetEncode.SelectedValue.ToString(), "【文本文件字符替换】文本的字符集类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_ReplaceResultFilterSavePath, txbFinalResultSavePath.Text.Trim(), "【文本文件字符替换】替换结果的二次筛选保存路径");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_CopyExcludeFileName, txbCopyExcludeFile.Text.Trim(), "【文本文件字符替换】复制到本地时排除包含的文件名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_CopyExcludeDirName, txbCopyExcludeDir.Text.Trim(), "【文本文件字符替换】复制到本地时排除包含的目录名");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_GenerateType, cbbLocalCopyType.SelectedValue.ToString(), "【文本文件字符替换】从已下载目录复制文件的方式");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_TemplateType, cbbTemplateType.SelectedValue.ToString(), "【文本文件字符替换】模板类型");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_ResultCopyExcludeFileName, txbResultFilterExcludeFile.Text.Trim(), "【文本文件字符替换】替换结果复制时的排除文件名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_ResultCopyFileEndfix, txbFileFileEndfix.Text.Trim(), "【文本文件字符替换】替换结果复制时指定的文件后缀");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace2_ResultCopyFileIsUseEndfix, ckbIsUseEndfix.Checked ? "1" : "0", "【文本文件字符替换】替换结果复制时是否使用指定的文件后缀");
            
            WinFormContext.UserLoveSettings.Save();
        } 
        #endregion

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region 网格头双击全选
        private void SelectAllOrCancel(DataGridView dgv, ref bool isSelect, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns[_sGridColumnSelect].Index)
            {
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Cells[_sGridColumnSelect].Value = isSelect ? "1" : "0";
                }
                isSelect = !isSelect;
            }
        }

        private void dgvFileListWaitFor_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvFileListWaitFor, ref _allSelectWait, e);
        }

        private void dgvOldNewChar_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvOldNewChar, ref _allSelectOldNewChar, e);
        }

        private void dgvResult_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvResult, ref _allSelectResult, e);
        }
        #endregion

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            FindGridText(true);
        }

        private void btnFindFront_Click(object sender, EventArgs e)
        {
            FindGridText(false);
        }

        private void FindGridText(bool isNext)
        {
            string sSearch = txbSearchColumn.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) return;
            dgvResult.SeachText(sSearch, ref dgvFindText, null, isNext);
            lblFind.Text = dgvFindText.CurrentMsg;
            //选择列为选中状态
            DataRow drCur = dgvResult.GetCurrentRow();
            if (drCur != null)
            {
                drCur[_sGridColumnSelect] = "1";
            }
        }

        /// <summary>
        /// 结果过滤按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultFiter_Click(object sender, EventArgs e)
        {
            DataTable dtResultFilter = dgvResultFilter.GetBindingTable();
            if (dtResultFilter.Rows.Count == 0) return;

            string[] sFilterExcludeFile = txbResultFilterExcludeFile.Text.Trim().Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            string[] sFilterIncludeEndfix = null;
            if (ckbIsUseEndfix.Checked)
            {
                sFilterIncludeEndfix = txbFileFileEndfix.Text.Trim().Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            }
            //如选中仅选中查找到的项，先反选所有列
            DataTable dtResult = dgvResult.GetBindingTable();
            if (dtResult.Rows.Count == 0) return;
            if (ckbOnlySelectFound.Checked)
            {
                foreach (DataRow item in dtResult.Rows)
                {
                    item[_sGridColumnSelect] = "0";
                }
            }

            var leftJoin = from f in dtResult.AsEnumerable()
                           where GetResultLinqDynamicWhere(f, dtResultFilter, sFilterExcludeFile, sFilterIncludeEndfix)
                           select f;
            var LeftJoinRes = leftJoin.ToList();

            foreach (DataRow item in LeftJoinRes)
            {
                item[_sGridColumnSelect] = "1";
            }
            SaveFuncConfig();//保存配置
            //按选中状态反选
            dgvResult.Sort(dgvResult.Columns[_sGridColumnSelect], ListSortDirection.Descending);
            //移动到第一个列
            dgvResult.CurrentCell = dgvResult.Rows[0].Cells[0];
        }

        private static bool GetResultLinqDynamicWhere(DataRow drF, DataTable dtFilter, string[] excludeFileArr, string[] sFilterIncludeEndfix)
        {
            //文件名过滤网格的是否符合判断
            string sFilePath = drF["FILE_PATH"].ToString();
            //string sDirName = sFilePath.Substring(0,sFilePath.LastIndexOf("\\"));
            string sFileName = sFilePath.Substring(sFilePath.LastIndexOf("\\") + 1);
            string sFileNamePre = sFileName.substring(0,sFileName.LastIndexOf("."));
            string sFileNameEndfix = sFileName.substring(sFileName.LastIndexOf(".")+1);

            var fitlerPre = from f in dtFilter.AsEnumerable()
                         where f["FILE_NAME"].ToString().Equals(sFileNamePre, StringComparison.OrdinalIgnoreCase)
                         select f;
            if(fitlerPre.ToArray().Length == 0)
            {
                return false; //文件名不符合
            }

            //文件后缀是否符合的判断
            if (sFilterIncludeEndfix != null)
            {
                var fitlerEndFix = from e in sFilterIncludeEndfix.AsEnumerable()
                                   where e.Equals(sFileNameEndfix, StringComparison.OrdinalIgnoreCase)
                                   select e;
                if(fitlerEndFix.ToArray().Length == 0)
                {
                    return false; //文件后缀不符合
                }
            }
            
            //排除文件名的判断
            var fitlerEx = from d in excludeFileArr.AsEnumerable()
                           where sFileName.Contains(d)
                           select d;
            if(fitlerEx.ToArray().Length > 0)
            {
                return false; //在排除文件中，也不符合
            }
            //前面都没问题，则最终返回符合
            return true;
        }

        private async void btnFinalResultCopy_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultFilterPath = txbFinalResultSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sResultFilterPath))
                {
                    ShowErr("生成结果的筛选保存中的【保存路径】不能为空！");
                    txbFinalResultSavePath.Focus();
                    return;
                }

                ShowInfo("已启动异步复制已替换的文件，请稍等...");
                btnFinalResultCopy.Enabled = false;
                await Task.Run(() => CopyResultFileAsync(sResultFilterPath));
                btnFinalResultCopy.Enabled = true;

                SaveFuncConfig();
                ShowInfo("已替换文件复制成功！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFinalResultCopy.Enabled = true;
            }
        }

        private async void CopyResultFileAsync(string sResultFilterPath)
        {
            if (Directory.Exists(sResultFilterPath))
            {
                Directory.Delete(sResultFilterPath, true);
                Directory.CreateDirectory(sResultFilterPath);
            }
            else
            {
                Directory.CreateDirectory(sResultFilterPath);
            }

            DataTable dtResult = dgvResult.GetBindingTable();
            if (ckbOnlySaleSelected.Checked)
            {
                DataRow[] arrDataRow = dtResult.Select(_sGridColumnSelect + "='1'");
                foreach (DataRow item in arrDataRow)
                {
                    string sTargetDir = sResultFilterPath;
                    string sRelDir = item["COPY_PATH"].ToString();
                    if (!string.IsNullOrEmpty(sRelDir)) sTargetDir = sTargetDir + sRelDir;
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(item["FILE_PATH"].ToString(), sTargetDir);
                }
            }
            else
            {
                foreach (DataRow item in dtResult.Rows)
                {
                    string sTargetDir = sResultFilterPath;
                    string sRelDir = item["COPY_PATH"].ToString();
                    if (!string.IsNullOrEmpty(sRelDir)) sTargetDir = sTargetDir + sRelDir;
                    FileDirHelper.CopyFilesToDirKeepSrcDirName(item["FILE_PATH"].ToString(), sTargetDir);
                }
            }
        }

        private void dgvResultFilter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                {
                    string pasteText = Clipboard.GetText().Trim();
                    if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                    {
                        return;
                    }

                    
                    int iRow = 0;
                    int iColumn = 0;
                    Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                    if(iRow == 0)
                    {
                        return;
                    }

                    DataTable dtMain = dgvResultFilter.GetBindingTable();
                    dtMain.Rows.Clear();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        if (string.IsNullOrEmpty(strData))
                        {
                            continue;
                        }

                        if (dtMain.Select("FILE_NAME='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.Rows.Count+1,strData);
                        }
                    }
                    //dgvResultFilter.ShowRowNum(true); //显示行号
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        /// <summary>
        /// 按选择列排序右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSortBySelectItem_Click(object sender, EventArgs e)
        {
            //按选中状态反选
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            dgvSelect.Sort(dgvSelect.Columns[_sGridColumnSelect], ListSortDirection.Descending);
        }

        private void ckbIsUseEndfix_CheckedChanged(object sender, EventArgs e)
        {
            txbFileFileEndfix.Enabled = ckbIsUseEndfix.Checked;
        }

        private void dgvOldNewChar_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvOldNewChar.Columns[e.ColumnIndex].Name == _sGridColumnSelect)
            {
                return;
            }
        }

        private void tsmiRowNoReset_Click(object sender, EventArgs e)
        {
            DataGridView dgvSelect = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView;
            dgvSelect.ShowRowNum(true);
        }

        private void cbbFileSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFileSource.SelectedValue == null) return;
            if ("2".Equals(cbbFileSource.SelectedValue.ToString()))
            {
                uC_FtpDownUpload1.Visible = true;
            }
            else
            {
                uC_FtpDownUpload1.Visible = false;
            }
        }

        private void txbSavePath_TextChanged(object sender, EventArgs e)
        {
            uC_FtpDownUpload1.SetUploadLocalDir(txbSavePath.Text, true);
        }

        private void cbbCharSetEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFileContentCharSetEncode.SelectedValue != null)
            {
                uC_FtpDownUpload1.SetFileEncoding(cbbFileContentCharSetEncode.SelectedValue.ToString(), true);
            }
        }

        private void dgvOldNewChar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                {
                    string pasteText = Clipboard.GetText().Trim();
                    if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                    {
                        return;
                    }

                    int iRow = 0;
                    int iColumn = 0;
                    Object[,] data = StringHelper.GetStringArray(ref pasteText, ref iRow, ref iColumn);
                    if (iRow == 0 || iColumn < 2)
                    {
                        return;
                    }

                    DataTable dtMain = dgvOldNewChar.GetBindingTable();
                    dtMain.Rows.Clear();
                    //获取获取当前选中单元格所在的行序号
                    for (int j = 0; j < iRow; j++)
                    {
                        string strData = data[j, 0].ToString().Trim();
                        string strData2 = data[j, 1].ToString().Trim();
                        if (string.IsNullOrEmpty(strData) || string.IsNullOrEmpty(strData2))
                        {
                            continue;
                        }

                        if (dtMain.Select("OLD='" + data[j, 0] + "'").Length == 0)
                        {
                            dtMain.Rows.Add(dtMain.Rows.Count + 1, "1", strData, strData2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void cbbTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null) return;
            string sTempType = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sTempType))
            {
                //txbReplaceTemplateName.ReadOnly = false;
                txbReplaceTemplateName.Text = string.Empty;
                return;
            }

            txbReplaceTemplateName.Text = cbbTemplateType.Text;
            //txbReplaceTemplateName.ReadOnly = true;
            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            DataRow[] drArr = replaceStringData.MoreXmlConfig.ValData.Select(sKeyId + "='" + sTempType + "'");

            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            if (drArr.Length > 0)
            {
                dtReplace.Rows.Clear();
                foreach (DataRow dr in drArr)
                {
                    dtReplace.Rows.Add(dtReplace.Rows.Count + 1, dr[ReplaceStringXmlConfig.ValueString.IsSelected].ToString(), dr[ReplaceStringXmlConfig.ValueString.OldString].ToString(), dr[ReplaceStringXmlConfig.ValueString.NewString].ToString());
                }
            }
            else if (dtReplace != null)
            {
                dtReplace.Clear();
            }
        }
        private void btnSaveReplaceTemplate_Click(object sender, EventArgs e)
        {
            string sTempName = txbReplaceTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("模板名称不能为空！");
                return;
            }
            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            dtReplace.DeleteNullRow();
            if (dtReplace.Rows.Count == 0)
            {
                ShowInfo("请录入要替换的新旧字符！");
                return;
            }

            if (ShowOkCancel("确定要保存模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceStringData.MoreXmlConfig.ValData;

            string sKeyIdNew = string.Empty;
            bool isAdd = string.IsNullOrEmpty(cbbTemplateType.Text.Trim()) ? true : false;
            if (isAdd)
            {
                //新增
                sKeyIdNew = Guid.NewGuid().ToString();
                DataRow dr = dtKeyConfig.NewRow();
                dr[sKeyId] = sKeyIdNew;
                dr[ReplaceStringXmlConfig.KeyString.Name] = sTempName;
                dtKeyConfig.Rows.Add(dr);
            }
            else
            {
                //修改
                string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
                sKeyIdNew = sKeyIDValue;
                DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
                if (drArrKey.Length == 0)
                {
                    DataRow dr = dtKeyConfig.NewRow();
                    dr[sKeyId] = sKeyIdNew;
                    dr[ReplaceStringXmlConfig.KeyString.Name] = sTempName;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    drArrKey[0][ReplaceStringXmlConfig.KeyString.Name] = sTempName;//修改名称
                }
                if (drArrVal.Length > 0)
                {
                    foreach (DataRow dr in drArrVal)
                    {
                        dtValConfig.Rows.Remove(dr);
                    }
                    dtValConfig.AcceptChanges();
                }
            }

            foreach (DataRow dr in dtReplace.Rows)
            {
                DataRow drNew = dtValConfig.NewRow();
                drNew[sValId] = Guid.NewGuid().ToString();
                drNew[sKeyId] = sKeyIdNew;
                drNew[ReplaceStringXmlConfig.ValueString.IsSelected] = dr[ReplaceStringXmlConfig.ValueString.IsSelected].ToString();
                drNew[ReplaceStringXmlConfig.ValueString.OldString] = dr[ReplaceStringXmlConfig.ValueString.OldString].ToString();
                drNew[ReplaceStringXmlConfig.ValueString.NewString] = dr[ReplaceStringXmlConfig.ValueString.NewString].ToString();
                dtValConfig.Rows.Add(drNew);
            }
            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, ReplaceStringXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板保存成功！");
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null)
            {
                ShowInfo("请选择一个模板！");
                return;
            }
            string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue))
            {
                ShowInfo("请选择一个模板！");
                return;
            }

            if (ShowOkCancel("确定要删除该模板？") == DialogResult.Cancel) return;

            string sKeyId = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = replaceStringData.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = replaceStringData.MoreXmlConfig.KeyData;
            DataTable dtValConfig = replaceStringData.MoreXmlConfig.ValData;
            DataRow[] drArrKey = dtKeyConfig.Select(sKeyId + "='" + sKeyIDValue + "'");
            DataRow[] drArrVal = dtValConfig.Select(sKeyId + "='" + sKeyIDValue + "'");

            if (drArrVal.Length > 0)
            {
                foreach (DataRow dr in drArrVal)
                {
                    dtValConfig.Rows.Remove(dr);
                }
                dtValConfig.AcceptChanges();
            }

            if (drArrKey.Length > 0)
            {
                foreach (DataRow dr in drArrKey)
                {
                    dtKeyConfig.Rows.Remove(dr);
                }
                dtKeyConfig.AcceptChanges();
            }
            replaceStringData.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, ReplaceStringXmlConfig.KeyString.Name, true, true);
            ShowInfo("模板删除成功！");
        }
    }
}
