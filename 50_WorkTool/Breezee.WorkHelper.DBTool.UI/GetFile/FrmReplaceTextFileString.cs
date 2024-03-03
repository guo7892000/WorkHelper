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

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 替换文本文件字符：将文本文件内容进行替换
    /// @history:
    ///   2023-10-15 huangguohui 新增    
    /// </summary>
    public partial class FrmReplaceTextFileString : BaseForm
    {
        private readonly string _sGridColumnSelect = "IsSelect";
        private bool _allSelectFtp = false;//默认全选，这里取反
        private bool _allSelectWait = false;//默认全选，这里取反
        private bool _allSelectOldNewChar = false;//默认全选，这里取反
        private bool _allSelectResult = false;//默认全选，这里取反
        DataGridViewFindText dgvFindText;
        ReplaceStringXmlConfig replaceStringData;//替换字符模板XML配置
        public FrmReplaceTextFileString()
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
            //绑定下拉框
            BindingDropDownList();
            //设置Tag
            SetTag();
            //初始化配置
            LoadFuncConfig();

            //其他
            toolTip1.SetToolTip(txbExcludeFileName, "排除包含指定字符的文件名，以逗号分隔！");
            toolTip1.SetToolTip(ckbClearLocalSaveDir, "选中时，下载前会先把【本地保存路径】目录清空！");
            toolTip1.SetToolTip(btnReplaceString, "直接使用【最终生成路径】中的文件，进行文本替换！");
            toolTip1.SetToolTip(btnCopyFileReplace, "复制【待复制的本地源目录或源文件】到【最终生成路径】中，并对【最终生成路径】中所有文件，进行文本替换！");
            toolTip1.SetToolTip(txbFtpDownloadLocalPath, "【本地保存路径】会包含【读取目录】名称。");
            toolTip1.SetToolTip(btnFinalResultCopy, "【复制】时，会先删除原目录，再创建目录并增加文件。"); 

            lblInfo2.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：所有行都为数据！";
            lblReplaceInfo.Text = "可在Excel中复制数据后，点击网格后按ctrl + v粘贴即可。注：所有行都为数据！";
        }

        private void BindingDropDownList()
        {
            _dicString["1"] = "本地文件";
            _dicString["2"] = "FTP远程文件";
            cbbFileSource.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            _dicString.Clear();
            _dicString["1"] = "先删后增";
            _dicString["2"] = "覆盖";
            _dicString["3"] = "备份";
            cbbUploadReplaceType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbUploadReplaceType, "先删后增:会把FTP上的目录删除后，再新增；\r\n覆盖：使用原目录，文件存在会覆盖。\r\n备份：会将原目录移入备份目录，并加上日期重命名。");

            _dicString.Clear();
            _dicString["1"] = "FTP";
            _dicString["2"] = "SFTP";
            _dicString["3"] = "FTPS";
            cbbFtpProtocol.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            _dicString.Clear();
            _dicString["1"] = "先删后增";
            _dicString["2"] = "覆盖";
            _dicString["3"] = "仅新增";
            cbbLocalCopyType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbLocalCopyType, "本地文件复制方式");

            _dicString.Clear();
            _dicString["1"] = "备份上传目录";
            _dicString["2"] = "备份读取目录";
            cbbBackupDirType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbBackupDirType, "FTP服务器端哪个目录的文件需要备份！");

            //加载模板数据
            replaceStringData = new ReplaceStringXmlConfig(DBTGlobalValue.ReplaceTextFileString.Xml_FileName);
            string sColName = replaceStringData.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sColName, ReplaceStringXmlConfig.KeyString.Name, true, true);

            DataTable dtEncode = BaseFileEncoding.GetEncodingTable(false);
            cbbCharSetEncode.BindTypeValueDropDownList(dtEncode, false, true);
            toolTip1.SetToolTip(cbbCharSetEncode, "如文件出现乱码，需要修改文件字符集！");
            cbbConnCharset.BindTypeValueDropDownList(dtEncode, false, true);//连接字符集：跟文件数据集一样的内容
            toolTip1.SetToolTip(cbbConnCharset, "如目录出现乱码，需要修改连接字符集！");
            
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
            //FTP清单网格
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("FILE_PATH").Caption("文件路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(600).Edit(true).Visible().Build()
                );
            dgvFtpFileList.Tag = fdc.GetGridTagString();
            dgvFtpFileList.BindDataGridView(null, false);
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
        /// 已下载文件加入待处理清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHadDownAddToWait_Click(object sender, EventArgs e)
        {
            string sSavePath = txbSavePath.Text.Trim();
            string sFtpDownloadPath = txbFtpDownloadLocalPath.Text.Trim();
            if (string.IsNullOrEmpty(sSavePath))
            {
                ShowErr("【最终生成目录】不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(sFtpDownloadPath))
            {
                ShowErr("FTP下载的【本地保存路径】不能为空！");
                return;
            }
            //读取原FTP下载目录进行复制
            FileInfo[] arrFiles = new DirectoryInfo(sFtpDownloadPath).GetFiles("*.*", SearchOption.AllDirectories);
            if (arrFiles.Length == 0)
            {
                ShowErr("FTP下载的【本地保存路径】下没有文件，请先从FTP服务器下载文件！");
                return;
            }
;
            DataTable dtList = dgvFileListWaitFor.GetBindingTable();
            dtList.Rows.Clear();
            int iIndex = 1;
            foreach (FileInfo file in arrFiles)
            {
                string sRelateDir = file.FullName.Replace(sFtpDownloadPath, "");
                sRelateDir = sRelateDir.Substring(0, sRelateDir.LastIndexOf(@"\"));
                dtList.Rows.Add(iIndex, "1", "文件", file.FullName, sRelateDir); //添加到待处理网格中
                iIndex++;
            }
        }

        /// <summary>
        /// 替换文件文本
        /// </summary>
        /// <param name="sSavePath"></param>
        private async void ReplaceFileText(string sSavePath, ReplaceTesfFileFormControleValue fromValue, DataTable dtResult)
        {
            DataTable dtReplace = dgvOldNewChar.GetBindingTable();
            DataRow[] drReplace = dtReplace.Select(_sGridColumnSelect + "= '1'");
            if (drReplace.Length == 0)
            {
                ShowErr("没有需要替换的字符！");
                return;
            }

            #region 处理前判断
            if (ckbLoadFinalSaveDirFile.Checked)
            {
                DataTable dtSource = dgvFileListWaitFor.GetBindingTable();
                DataRow[] drWillArr = dtSource.Select(_sGridColumnSelect + "= '1'");
                if (drWillArr.Length == 0)
                {
                    ShowErr("没有要处理的文件，请先选择！");
                    return;
                }
            }
            else
            {
                //目录不存在则创建目录
                if (!Directory.Exists(sSavePath))
                {
                    Directory.CreateDirectory(sSavePath);
                    ShowErr("【最终生成目录】下没有文件，请先复制文件！");
                    return;
                }
                //读取保存目录文件进行
                FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                if (arrFiles.Length == 0)
                {
                    ShowErr("【最终生成目录】下没有文件，请先复制文件！");
                    return;
                }
            }
            #endregion

            //调用异步替换文件方法
            await ReplaceFileTextAsync(sSavePath, drReplace, fromValue, dtResult);           
        }

        private async Task ReplaceFileTextAsync(string sSavePath, DataRow[] drReplace, ReplaceTesfFileFormControleValue fromValue, DataTable dtResult)
        {
            if (ckbLoadFinalSaveDirFile.Checked)
            {
                DataTable dtSource = dgvFileListWaitFor.GetBindingTable();
                DataRow[] drWillArr = dtSource.Select(_sGridColumnSelect + "= '1'");
                int iIdx = 1;
                //得到文件清单
                var files = drWillArr.AsEnumerable().Select(t => t["FILE_PATH"].ToString()).ToList();
                Encoding encoding = GetEncoding(false, fromValue);//字符集
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
                Encoding encoding = GetEncoding(false, fromValue);

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
                if (Directory.Exists(sSavePath))
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
        private Encoding GetEncoding(bool isConnect, ReplaceTesfFileFormControleValue fromValue)
        {
            Encoding encoding;
            string sEncode = fromValue.EncodingFile;
            if (isConnect)
            {
                sEncode = fromValue.EncodingConnect; //针对FTP连接，取连接下拉框的字符集
            }
            if ("utf-8-Bom".Equals(sEncode, StringComparison.OrdinalIgnoreCase))
            {
                encoding = new UTF8Encoding(true);
            }
            else if ("utf-8".Equals(sEncode, StringComparison.OrdinalIgnoreCase))
            {
                encoding = new UTF8Encoding(false);
            }
            else
            {
                encoding = Encoding.GetEncoding(sEncode);
            }

            return encoding;
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

                if (!Directory.Exists(sSavePath))
                {
                    Directory.CreateDirectory(sSavePath);
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

                string sGeneratType = cbbLocalCopyType.SelectedValue.ToString();
                bool isOverWrite = true;
                if ("1".Equals(sGeneratType))
                {
                    if (Directory.Exists(sSavePath))
                    {
                        Directory.Delete(sSavePath, true);
                        Directory.CreateDirectory(sSavePath);
                    }
                    else
                    {
                        Directory.CreateDirectory(sSavePath);
                    }
                }
                else if ("2".Equals(sGeneratType))
                {
                    isOverWrite = true;
                }
                else
                {
                    isOverWrite = false;
                }

                string sFileSource = cbbFileSource.SelectedValue.ToString();
                bool isFtp = "2".Equals(sFileSource) ? true : false;

                string sFtpDownloadPath = txbFtpDownloadLocalPath.Text.Trim();

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
                ReplaceTesfFileFormControleValue fromValue = getFormValue();
                ShowInfo("已启动异步复制并替换，请稍等...");
                btnCopyFileReplace.Enabled = false;
                await Task.Run(() => CopyLocalFileAsync(sSavePath, dtWillDeal, drWill, isOverWrite, sFtpDownloadPath, fromValue, dtResult));
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
        private async void CopyLocalFileAsync(string sSavePath, DataTable dtWillDeal, DataRow[] drWill, bool isOverWrite, string sFtpDownloadPath, ReplaceTesfFileFormControleValue fromValue, DataTable dtResult)
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
            ReplaceFileText(sSavePath, fromValue, dtResult);
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
                ReplaceTesfFileFormControleValue fromValue = getFormValue();
                DataTable dtResult = dgvResult.GetBindingTable().Clone();
                //替换文件文本
                ShowInfo("已启动异步文件文本替换，请稍等...");
                btnReplaceString.Enabled = false;
                await Task.Run(() => ReplaceFileText(sSavePath, fromValue, dtResult));
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

        /// <summary>
        /// 显示所有FTP文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFtpShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServerInfo ftpServer = CheckFtpServerInfo();
                if (ftpServer == null) return;

                DataTable dtList = dgvFtpFileList.GetBindingTable().Clone();

                ReplaceTesfFileFormControleValue fromValue = getFormValue();

                ShowInfo("已启动异步显示所有文件，请稍等...");
                btnFtpShowAll.Enabled = false;
                await Task.Run(() => ShowFtpFileAsync(ftpServer, dtList, fromValue));
                btnFtpShowAll.Enabled = true;
                ShowInfo("异步显示所有文件完毕！");

                dgvFtpFileList.BindDataGridView(dtList, false);
                //如果选中默认排除筛选框，那么调用点击排除按钮
                if (ckbFtpExcludeDefault.Checked)
                {
                    btnExcludeSelect.PerformClick();
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFtpShowAll.Enabled = true;
            }
        }

        private async Task ShowFtpFileAsync(FtpServerInfo ftpServer, DataTable dtList, ReplaceTesfFileFormControleValue fromValue)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = GetEncoding(true, fromValue);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    //client.
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录
                    ListDirectory(client, ".", dtList); //递归查找文件
                }
            }
            else
            {
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    conn.Encoding = GetEncoding(true, fromValue);
                    await conn.Connect(token); //连接
                    //await conn.SetWorkingDirectory(ftpServer.ReadDir);//改变目录

                    int iIndex = 1;
                    foreach (var item in await conn.GetListing(ftpServer.ReadServerDir, FtpListOption.Recursive, token))
                    {
                        switch (item.Type)
                        {
                            case FtpObjectType.File:
                                dtList.Rows.Add(iIndex, "1", item.FullName); //文件清单加入到
                                iIndex++;
                                break;
                            case FtpObjectType.Directory:
                                break;
                            case FtpObjectType.Link:
                                break;
                        }
                    }

                }
            }
        }

        #region SFTP相关的递归方法
        /// <summary>
        /// 递归显示SFTP服务器文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="dt"></param>
        private void ListDirectory(SftpClient client, string dirName, DataTable dt)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.endsWith("."))
                    {
                        ListDirectory(client, entry.FullName, dt);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    dt.Rows.Add(dt.Rows.Count + 1, "1", entry.FullName);
                }
            }
        }

        /// <summary>
        /// 递归删除SFTP服务器目录或文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        private void DeleteDirectoryAndFile(SftpClient client, string dirName)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.endsWith("."))
                    {
                        DeleteDirectoryAndFile(client, entry.FullName);
                        client.Delete(entry.FullName);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    client.Delete(entry.FullName);
                }
            }
        }

        /// <summary>
        /// 递归下载SFTP服务器目录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="localPath"></param>
        private void DownSFTPDirectory(SftpClient client, string dirName, string localPath, ReplaceTesfFileFormControleValue formValue)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.endsWith("."))
                    {
                        //如果本地目录不存在，则创建
                        string sFullDir = localPath + entry.FullName;
                        if (formValue.IsDownPathExcludeFtpReadPath)
                        {
                            sFullDir = localPath + entry.FullName.Replace(formValue.ReadPath, "");//排除读取路径
                        }
                        if (!Directory.Exists(sFullDir))
                        {
                            Directory.CreateDirectory(sFullDir);
                        }
                        //递归下载文件
                        DownSFTPDirectory(client, entry.FullName, localPath, formValue);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    string sFullDir = localPath + entry.FullName;
                    if (formValue.IsDownPathExcludeFtpReadPath)
                    {
                        sFullDir = localPath + entry.FullName.Replace(formValue.ReadPath, "");//排除读取路径
                    }

                    var byt = client.ReadAllBytes(entry.FullName);
                    if (byt.Length > 0)
                    {
                        File.WriteAllBytes(sFullDir, byt);
                    }
                }
            }
        }

        /// <summary>
        /// 递归备份SFTP服务器文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dirName"></param>
        /// <param name="TargetPath"></param>
        private void BackupFilesSFTP(SftpClient client, string dirName, string TargetPath, ReplaceTesfFileFormControleValue formValue)
        {
            foreach (var entry in client.ListDirectory(dirName))
            {
                if (entry.IsDirectory)
                {
                    //注：清单里会出现/.和/..的目录，这里一定要排除它们，不然会出现死循环
                    if (!entry.FullName.endsWith("."))
                    {
                        //如果本地目录不存在，则创建
                        string sFullDir = "";
                        if ("1".Equals(formValue.BackupDirType))
                        {
                            sFullDir = TargetPath + entry.FullName.Replace(formValue.FtpUploadPath, "");
                        }
                        else
                        {
                            sFullDir = TargetPath + entry.FullName.Replace(formValue.ReadPath, "");
                        }

                        if (!client.Exists(sFullDir))
                        {
                            client.CreateDirectory(sFullDir);
                            client.ChangeDirectory(sFullDir);
                        }
                        else
                        {
                            client.ChangeDirectory(sFullDir);
                        }
                        //递归调用备份目录
                        BackupFilesSFTP(client, entry.FullName, TargetPath, formValue);
                    }
                }
                else if (entry.IsRegularFile)
                {
                    //注：这里使用先读取原文件，再写到新位置实现复制
                    string sNewFilePath = "";
                    if ("1".Equals(formValue.BackupDirType))
                    {
                        //备份上传目录
                        sNewFilePath = entry.FullName.Replace(formValue.FtpUploadPath, TargetPath);
                    }
                    else
                    {
                        //备份读取目录
                        sNewFilePath = entry.FullName.Replace(formValue.ReadPath, TargetPath);
                    }
                    string sRead = client.ReadAllText(entry.FullName, GetEncoding(false, formValue));
                    client.WriteAllText(sNewFilePath, sRead);
                    //client.RenameFile(entry.FullName, sNewFilePath); //这个方式相当于剪切文件。注：目录还存在
                }
            }
        } 
        #endregion

        /// <summary>
        /// 获取界面控件值
        /// </summary>
        /// <returns></returns>
        private ReplaceTesfFileFormControleValue getFormValue()
        {
            ReplaceTesfFileFormControleValue fv = new ReplaceTesfFileFormControleValue();
            fv.EncodingConnect = cbbConnCharset.SelectedValue.ToString();
            fv.EncodingFile = cbbCharSetEncode.SelectedValue.ToString();
            fv.DownLocalPath = txbFtpDownloadLocalPath.Text.Trim();
            fv.ReadPath = txbFtpReadPath.Text.Trim();
            fv.IsDownPathExcludeFtpReadPath = ckbDownPathExcludeFtpReadPath.Checked;
            fv.FtpUploadPath = txbFtpUploadPath.Text.trim();
            fv.BackupDirType = cbbBackupDirType.SelectedValue.ToString();

            return fv;
        }

        /// <summary>
        /// 下载所有文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFtpDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServerInfo ftpServer = CheckFtpServerInfo();
                if (ftpServer == null) return;

                string sDownLocalPath = txbFtpDownloadLocalPath.Text.Trim();
                string sReadPath = txbFtpReadPath.Text.Trim();
                if (string.IsNullOrEmpty(sDownLocalPath))
                {
                    ShowErr("本地保存路径不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(sReadPath))
                {
                    ShowErr("读取目录不能为空！");
                    return;
                }

                if (ShowOkCancel("确定要下载所有文件？") == DialogResult.Cancel)
                    return;

                DataTable dtList = dgvFileListWaitFor.GetBindingTable().Clone();
                ReplaceTesfFileFormControleValue fromValue = getFormValue();
                //异步下载FTP所有文件
                ShowInfo("已启动异步下载所有文件，请稍等...");
                btnFtpDownLoad.Enabled = false;
                await Task.Run(() => DowloadAllFtpFiles(ftpServer, sDownLocalPath, sReadPath, dtList, fromValue));
                btnFtpDownLoad.Enabled = true;

                dgvFileListWaitFor.BindDataGridView(dtList);
                SaveFuncConfig();//保存用户偏好值
                ShowInfo("所有文件下载成功！");
            }
            catch(Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFtpDownLoad.Enabled = true;
            }
        }

        /// <summary>
        /// 异步下载所有文件方法
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="sDownPath"></param>
        /// <param name="sReadPath"></param>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        private async Task DowloadAllFtpFiles(FtpServerInfo ftpServer, string sDownPath, string sReadPath, DataTable dtList,ReplaceTesfFileFormControleValue fromValue)
        {
            if (Directory.Exists(sDownPath))
            {
                //存在，且下载前清除目录选中时
                if (ckbClearLocalSaveDir.Checked)
                {
                    Directory.Delete(sDownPath, true);
                    Directory.CreateDirectory(sDownPath);
                }
            }
            else
            {
                //不存在就创建
                Directory.CreateDirectory(sDownPath);
            }

            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                #region SFTP
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = GetEncoding(true, fromValue);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录
                    DownSFTPDirectory(client, ftpServer.ReadServerDir, sDownPath, fromValue); //递归下载文件
                }
                #endregion
            }
            else
            {
                #region FTPS
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    conn.Encoding = GetEncoding(true, fromValue); //
                    await conn.Connect(token);

                    //更新模式：FtpFolderSyncMode.Update，源方式（删除多余文件）：FtpFolderSyncMode.Mirror
                    var result = await conn.DownloadDirectory(sDownPath, sReadPath, FtpFolderSyncMode.Mirror, token: token);

                    if (ckbDownLoadAddList.Checked)
                    {
                        int iIndex = 1;
                        foreach (var item in result)
                        {
                            if (item.Type == FtpObjectType.File)
                            {
                                string sRelDir = item.RemotePath.Substring(0, item.RemotePath.LastIndexOf("/"));
                                if (fromValue.IsDownPathExcludeFtpReadPath)
                                {
                                    sRelDir = sRelDir.Replace(fromValue.ReadPath, "");//排除读取路径
                                }

                                dtList.Rows.Add(iIndex, "1", "文件", item.LocalPath, sRelDir); //添加到待处理网格中
                                iIndex++;
                            }
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 下载FTP上部分选择的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFtpDownSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServerInfo ftpServer = CheckFtpServerInfo();
                if (ftpServer == null) return;

                DataTable dtList = dgvFtpFileList.GetBindingTable();
                DataRow[] drWillArr = dtList.Select(_sGridColumnSelect + "= '1'");
                if (drWillArr.Length == 0)
                {
                    ShowErr("没有要下载的文件，请先选择！");
                    return;
                }
                string sDownPath = txbFtpDownloadLocalPath.Text.Trim();
                if (string.IsNullOrEmpty(sDownPath))
                {
                    ShowErr("本地保存路径不能为空！");
                    return;
                }

                if (Directory.Exists(sDownPath))
                {
                    //存在，且下载前清除目录选中时
                    if (ckbClearLocalSaveDir.Checked)
                    {
                        Directory.Delete(sDownPath, true);
                        Directory.CreateDirectory(sDownPath);
                    }
                }
                else
                {
                    //不存在就创建
                    Directory.CreateDirectory(sDownPath);
                }

                DataTable dtWaitList = dgvFileListWaitFor.GetBindingTable().Clone();
                ReplaceTesfFileFormControleValue fromValue = getFormValue();
                ShowInfo("已启动异步下载所选择的文件，请稍等...");
                btnFtpDownSelectFile.Enabled = false;
                await Task.Run(() => DownloadSelectedFileAsync(ftpServer, drWillArr, sDownPath, dtWaitList, fromValue));
                btnFtpDownSelectFile.Enabled = true;

                SaveFuncConfig();//保存用户偏好值
                dgvFileListWaitFor.BindDataGridView(dtWaitList, false);
                ShowInfo("选择部分文件已下载成功！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFtpDownSelectFile.Enabled = true;
            }
        }

        /// <summary>
        /// 异步下载所选文件方法
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="drWillArr"></param>
        /// <param name="sDownPath"></param>
        /// <returns></returns>
        private async Task DownloadSelectedFileAsync(FtpServerInfo ftpServer, DataRow[] drWillArr, string sDownPath, DataTable dtWaitList,ReplaceTesfFileFormControleValue fromValue)
        {
            int iIndex = 1;
            //得到文件清单
            var files = drWillArr.AsEnumerable().Select(t => t["FILE_PATH"].ToString()).ToList();

            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = GetEncoding(true, fromValue);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录

                    foreach (string item in files)
                    {
                        string sServerRelDir = fromValue.IsDownPathExcludeFtpReadPath ? item.Replace(fromValue.ReadPath, "") : item;
                        string sLocal = sDownPath + sServerRelDir.Replace(@"/", @"\");
                        string sDir = sLocal.substring(0, sLocal.LastIndexOf(@"\"));
                        //如果本地目录不存在，则创建
                        if (!Directory.Exists(sDir))
                        {
                            Directory.CreateDirectory(sDir);
                        }

                        var byt = client.ReadAllBytes(item);
                        if (byt.Length > 0)
                        {
                            File.WriteAllBytes(sLocal, byt);
                        }
                        //下载并覆盖
                        if (ckbDownLoadAddList.Checked)
                        {
                            string sRelDir = item.Substring(0, item.LastIndexOf("/")).Replace(fromValue.ReadPath, "");
                            dtWaitList.Rows.Add(iIndex, "1", "文件", sLocal, sRelDir); //添加到待处理网格中
                            iIndex++;
                        }
                    }
                }
            }
            else
            {
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    conn.Encoding = GetEncoding(true, fromValue);
                    await conn.Connect(token);

                    foreach (string item in files)
                    {
                        string sServerRelDir = fromValue.IsDownPathExcludeFtpReadPath ? item.Replace(fromValue.ReadPath, "") : item;
                        string sLocal = sDownPath + sServerRelDir.Replace(@"/", @"\");
                        string sDir = sLocal.substring(0, sLocal.LastIndexOf(@"\"));
                        //如果本地目录不存在，则创建
                        if (!Directory.Exists(sDir))
                        {
                            Directory.CreateDirectory(sDir);
                        }
                        //下载并覆盖
                        var result = await conn.DownloadFile(sLocal, item, FtpLocalExists.Overwrite, FtpVerify.None, token: token);
                        if (ckbDownLoadAddList.Checked)
                        {
                            dtWaitList.Rows.Add(iIndex, "1", "文件", sLocal, sServerRelDir.Substring(0, sServerRelDir.LastIndexOf("/"))); //添加到待处理网格中
                            iIndex++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 上传整个目录到FTP服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFtpUploadDir_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServerInfo ftpServer = CheckFtpServerInfo();
                if (ftpServer == null) return;

                string sUploadPath = txbFtpUploadPath.Text.Trim();
                if (string.IsNullOrEmpty(sUploadPath))
                {
                    ShowErr("【上传目录】不能为空！");
                    return;
                }
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    ShowErr("【最终生成目录】不能为空！");
                    return;
                }

                //读取保存目录文件进行
                FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                if (arrFiles.Length == 0)
                {
                    ShowErr("【最终保存路径】目录下没有要上传的文件！");
                    return;
                }

                //1先删后增、2覆盖、3备份
                string sUploadType = cbbUploadReplaceType.SelectedValue.ToString();
                string sFtpBackupDir = txbFtpUploadBackupDir.Text.Trim();
                if ("3".Equals(sUploadType) && string.IsNullOrEmpty(sFtpBackupDir))
                {
                    ShowErr("当【上传方式】为【备份】时，【备份目录】不能为空！");
                    txbFtpUploadBackupDir.Focus();
                    return;
                }

                if (ShowOkCancel("确定要将【最终生成目录】文件上传到FTP服务上？") == DialogResult.Cancel)
                    return;

                bool isSuccess = false;
                ReplaceTesfFileFormControleValue fromValue = getFormValue();

                //异步下载FTP所有文件
                ShowInfo("已启动异步目录文件上传到FTP服务器，请稍等...");
                btnFtpUploadDir.Enabled = false;
                isSuccess = await Task.Run(() => UploadDirAsync(ftpServer, sUploadPath, sSavePath, arrFiles, sUploadType, sFtpBackupDir, isSuccess, fromValue));
                btnFtpUploadDir.Enabled = true;

                SaveFuncConfig();//保存用户偏好值
                if (isSuccess)
                {
                    ShowInfo("目录文件已成功上传到FTP服务器！");
                }
                else
                {
                    ShowErr("目录文件上传到FTP服务器失败！");
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFtpUploadDir.Enabled = true;
            }
        }

        private async Task<bool> UploadDirAsync(FtpServerInfo ftpServer, string sUploadPath, string sSavePath, FileInfo[] arrFiles, string sUploadType, string sFtpBackupDir, bool isSuccess, ReplaceTesfFileFormControleValue fromValue)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                #region SFTP
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = GetEncoding(true, fromValue);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                    //client.ChangeDirectory(ftpServer.UploadDir); //改变目录

                    bool isExistDir = client.Exists(sUploadPath);
                    if ("1".Equals(sUploadType))
                    {
                        //先删除后增加
                        if (isExistDir)
                        {
                            //递归删除其下的目录和文件（SFTP只支持删除单个文件或空目录，所以才使用递归方式）
                            DeleteDirectoryAndFile(client, sUploadPath);
                        }
                        else
                        {
                            client.CreateDirectory(sUploadPath);
                        }
                    }
                    else if ("3".Equals(sUploadType))
                    {
                        //备份
                        isExistDir = client.Exists(sFtpBackupDir);
                        if (!isExistDir)
                        {
                            client.CreateDirectory(sFtpBackupDir);
                            client.ChangeDirectory(sFtpBackupDir); //改变目录
                        }
                        else
                        {
                            client.ChangeDirectory(sFtpBackupDir); //改变目录
                        }

                        bool isBackupUploadDir = "1".Equals(fromValue.BackupDirType);
                        string sBackupSourceDir = isBackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        client.CreateDirectory(sNewPath); //创建一个子目录
                        //递归备份文件
                        client.ChangeDirectory(sNewPath);//改变目录为上传目录
                        BackupFilesSFTP(client, sBackupSourceDir, client.WorkingDirectory, fromValue);
                    }
                    else
                    {
                        //覆盖方式
                    }

                    //改变目录为上传目录
                    client.ChangeDirectory(ftpServer.UploadServerDir);
                    //上传文件
                    foreach (FileInfo file in arrFiles)
                    {
                        using (FileStream fs = File.OpenRead(file.FullName))
                        {
                            //这里要循环创建目录，不能一次性把全目录路径来创建
                            string sDir = file.DirectoryName.Replace(sSavePath, "");
                            string[] arrDir = sDir.Split(new char[] { '/', '\\' },StringSplitOptions.RemoveEmptyEntries);
                            foreach (string item in arrDir)
                            {
                                if (!client.Exists(item))
                                {
                                    client.CreateDirectory(item);
                                    client.ChangeDirectory(item);
                                }
                                else
                                {
                                    client.ChangeDirectory(item);
                                }
                            }
                            //上传文件
                            string sFilePath = ftpServer.UploadServerDir + Path.Combine(sDir,file.Name).Replace("\\","/");
                            if (client.Exists(sFilePath))
                            {
                                client.Delete(sFilePath);//注：针对备份或覆盖文件方式，也是采用先删除后增加。否则会报错，且错误信息为乱码。
                            }
                            client.UploadFile(fs, sFilePath); 
                        }
                        //回到根目录
                        client.ChangeDirectory(ftpServer.UploadServerDir);
                    }
                    isSuccess = true;
                }
                #endregion
            }
            else
            {
                #region FTPS
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    conn.Encoding = GetEncoding(true, fromValue); //
                    await conn.Connect(token);

                    bool isExistDir = await conn.DirectoryExists(sUploadPath);
                    if ("1".Equals(sUploadType))
                    {
                        //先删除后增加
                        if (isExistDir)
                        {
                            await conn.DeleteDirectory(sUploadPath);
                            await conn.CreateDirectory(sUploadPath);
                        }
                        else
                        {
                            await conn.CreateDirectory(sUploadPath);
                        }
                    }
                    else if ("3".Equals(sUploadType))
                    {
                        //备份
                        isExistDir = await conn.DirectoryExists(sFtpBackupDir);
                        string sNewPath = Path.Combine(sFtpBackupDir, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        if (!isExistDir)
                        {
                            await conn.CreateDirectory(sFtpBackupDir);

                        }
                        await conn.CreateDirectory(sNewPath); //创建一个子目录
                        await conn.MoveDirectory(sUploadPath, sNewPath, FtpRemoteExists.Overwrite, token: token);
                    }
                    else
                    {
                        //覆盖方式

                    }
                    //上传整个目录：Update方式为存在即更新；
                    var result = await conn.UploadDirectory(sSavePath, sUploadPath, FtpFolderSyncMode.Update, token: token);
                    isSuccess = result.Count > 0 ? true : false;
                }
                #endregion
            }

            return isSuccess;
        }

        /// <summary>
        /// 选择指定文件上传到FTP服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFtpUpLoadSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                FtpServerInfo ftpServer = CheckFtpServerInfo();
                if (ftpServer == null) return;

                string sUploadPath = txbFtpUploadPath.Text.Trim();
                if (string.IsNullOrEmpty(sUploadPath))
                {
                    ShowErr("【上传目录】不能为空！");
                    return;
                }
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    ShowErr("【最终生成目录】不能为空！");
                    return;
                }

                DataTable dtResult = dgvResult.GetBindingTable();
                DataRow[] drWillArr = dtResult.Select(_sGridColumnSelect + "= '1'");
                if (drWillArr.Length == 0)
                {
                    ShowErr("没有要上传的文件！");
                    return;
                }

                //1先删后增、2覆盖、3备份
                string sUploadType = cbbUploadReplaceType.SelectedValue.ToString();
                string sFtpBackupDir = txbFtpUploadBackupDir.Text.Trim();
                if ("3".Equals(sUploadType) && string.IsNullOrEmpty(sFtpBackupDir))
                {
                    ShowErr("当【上传方式】为【备份】时，【备份目录】不能为空！");
                    txbFtpUploadBackupDir.Focus();
                    return;
                }

                if (ShowOkCancel("确定要将【所选文件】文件上传到FTP服务上？") == DialogResult.Cancel)
                    return;

                ReplaceTesfFileFormControleValue fromValue = getFormValue();

                //异步下载FTP所有文件
                ShowInfo("已启动异步文件上传到SFTP服务器，请稍等...");
                btnFtpUpLoadSelectFile.Enabled = false;
                await Task.Run(() => UploadSelectedFileAsync(ftpServer, sUploadPath, sSavePath, drWillArr, sUploadType, sFtpBackupDir, fromValue));
                btnFtpUpLoadSelectFile.Enabled = true;

                SaveFuncConfig();//保存用户偏好值
                ShowInfo("所选文件已全部成功上传到SFTP服务器！");
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
            finally
            {
                btnFtpUpLoadSelectFile.Enabled = true;
            }
        }

        private async Task UploadSelectedFileAsync(FtpServerInfo ftpServer, string sUploadPath, string sSavePath, DataRow[] drWillArr, string sUploadType, string sFtpBackupDir, ReplaceTesfFileFormControleValue fromValue)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                #region SFTP
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = GetEncoding(true, fromValue);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                                                      //client.ChangeDirectory(ftpServer.UploadDir); //改变目录
                    bool isExistDir = client.Exists(sUploadPath);

                    if ("1".Equals(sUploadType))
                    {
                        //先删除后增加
                        if (isExistDir)
                        {
                            //递归删除其下的目录和文件（SFTP只支持删除单个文件或空目录，所以才使用递归方式）
                            DeleteDirectoryAndFile(client, sUploadPath);
                        }
                        else
                        {
                            client.CreateDirectory(sUploadPath);
                        }
                    }
                    else if ("3".Equals(sUploadType))
                    {
                        //备份
                        isExistDir = client.Exists(sFtpBackupDir);
                        if (!isExistDir)
                        {
                            client.CreateDirectory(sFtpBackupDir);
                            client.ChangeDirectory(sFtpBackupDir); //改变目录
                        }
                        else
                        {
                            client.ChangeDirectory(sFtpBackupDir); //改变目录
                        }

                        bool isBackupUploadDir = "1".Equals(fromValue.BackupDirType);
                        string sBackupSourceDir = isBackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        client.CreateDirectory(sNewPath); //创建一个子目录

                        //递归备份文件
                        client.ChangeDirectory(sNewPath);//改变目录
                        BackupFilesSFTP(client, sBackupSourceDir, client.WorkingDirectory, fromValue);
                    }
                    else
                    {
                        //覆盖方式
                    }

                    //改变目录为上传目录
                    client.ChangeDirectory(ftpServer.UploadServerDir);
                    //上传文件
                    foreach (DataRow dr in drWillArr)
                    {
                        using (FileStream fs = File.OpenRead(dr["FILE_PATH"].ToString()))
                        {
                            //这里要循环创建目录，不能一次性把全目录路径来创建
                            string sDir = Path.Combine(sUploadPath, dr["COPY_PATH"].ToString());
                            string[] arrDir = sDir.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string item in arrDir)
                            {
                                if (!client.Exists(item))
                                {
                                    client.CreateDirectory(item);
                                    client.ChangeDirectory(item);
                                }
                                else
                                {
                                    client.ChangeDirectory(item);
                                }
                            }
                            string sFile = dr["COPY_FILE"].ToString();
                            if (client.Exists(sFile))
                            {
                                client.Delete(sFile); //注：针对备份或覆盖文件方式，也是采用先删除后增加。否则会报错，且错误信息为乱码。
                            }
                            client.UploadFile(fs, sFile);
                        }
                        //回到根目录
                        client.ChangeDirectory(ftpServer.UploadServerDir);
                    }
                }
                #endregion
            }
            else
            {
                #region FTPS
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    //conn.Config.SslProtocols = System.Security.Authentication.SslProtocols.None;
                    conn.Encoding = GetEncoding(true, fromValue); //
                    await conn.Connect(token);


                    bool isExistDir = await conn.DirectoryExists(sUploadPath, token: token);
                    if ("1".Equals(sUploadType))
                    {
                        //先删除后增加
                        if (isExistDir)
                        {
                            await conn.DeleteDirectory(sUploadPath, token: token);
                            await conn.CreateDirectory(sUploadPath, token: token);
                        }
                        else
                        {
                            await conn.CreateDirectory(sUploadPath, token: token);
                        }
                    }
                    else if ("3".Equals(sUploadType))
                    {
                        //备份
                        isExistDir = await conn.DirectoryExists(sFtpBackupDir);
                        string sNewPath = Path.Combine(sFtpBackupDir, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        if (!isExistDir)
                        {
                            //目录不存在，则创建目录
                            await conn.CreateDirectory(sFtpBackupDir);
                        }
                        else
                        {
                            //目录存在，则判断其下有没文件，有才备份
                            FtpListItem[] listArr = await conn.GetListing(sUploadPath, token: token);
                            if (listArr.Length > 0)
                            {
                                await conn.CreateDirectory(sNewPath); //创建一个子目录
                                await conn.MoveDirectory(sUploadPath, sNewPath, FtpRemoteExists.Overwrite, token: token);
                            }
                        }
                    }
                    else
                    {
                        //覆盖方式
                    }

                    #region 已取消
                    //方式一：逐个文件上传：如不创建子目录，则文件都会放根目录；但如创建子目录，放文件时会报拒绝访问错误，这个未解决！2023-10-22
                    //var files = drWillArr.AsEnumerable().Select(t => new { path = t["FILE_PATH"].ToString(), absDir = t["COPY_PATH"].ToString() }).ToList(); //得到文件清单
                    //foreach (var item in files)
                    //{
                    //    string sServerPath = sUploadPath + item.absDir.Replace("\\", "/");
                    //    isExistDir = await conn.DirectoryExists(sServerPath);
                    //    //上传
                    //    if (!isExistDir)
                    //    {
                    //        await conn.CreateDirectory(sServerPath, token: token);
                    //    }
                    //    //以下代码报错：FtpCommandException: Code: 550 Message: Access is denied. 
                    //    var result = await conn.UploadFile(item.path, sServerPath, FtpRemoteExists.Overwrite, true, token: token);
                    //} 
                    #endregion

                    //方法二：上传目录：使用白名单
                    var files = drWillArr.AsEnumerable().Select(t => t["COPY_FILE"].ToString()).ToList();//得到文件清单
                    List<FtpRule> ftpRule = new List<FtpRule>();
                    ftpRule.Add(new FtpFileNameRule(true, files));
                    //上传整个目录：Update方式为存在即更新；
                    var result = await conn.UploadDirectory(sSavePath, sUploadPath, FtpFolderSyncMode.Update, FtpRemoteExists.Overwrite, FtpVerify.None, ftpRule, token: token);

                }
                #endregion
            }
        }

        private void cbbFileSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFileSource.SelectedValue == null) return;
            if ("2".Equals(cbbFileSource.SelectedValue.ToString()))
            {
                pnlFtp.Visible = true;
                splitContainer2.Panel1Collapsed = false; //设计左方非折叠
            }
            else
            {
                pnlFtp.Visible = false;
                splitContainer2.Panel1Collapsed = true; //设计左方折叠
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
        /// FTP下载保存到的本地路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLocalPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var strLastSelectedPath = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_DownloadLocalDir, "").Value;

            if (!string.IsNullOrEmpty(strLastSelectedPath))
            {
                dialog.SelectedPath = strLastSelectedPath;
            }
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txbFtpDownloadLocalPath.Text = dialog.SelectedPath;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_DownloadLocalDir, dialog.SelectedPath, "【文本文件字符替换】下载到本机的目录");
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

        private FtpServerInfo CheckFtpServerInfo()
        {
            FtpServerInfo ftpServer = new FtpServerInfo();
            string sFtpIP = txbIPAddr.Text.Trim();
            string sFtpUserName = txbUserName.Text.Trim();
            string sFtpPassword = txbPassword.Text.Trim();
            string sFtpInitPath = txbFtpInitDir.Text.Trim();
            string sFtpReadDir = txbFtpReadPath.Text.Trim();
            string sFtpUploadDir = txbFtpUploadPath.Text.Trim();
            string sFtpPort = txbPort.Text.Trim();
            int iPort = 21;
            if (string.IsNullOrEmpty(sFtpIP))
            {
                ShowErr("FTP服务器地址不能为空！");
                return null;
            }
            if (!string.IsNullOrEmpty(sFtpPort))
            {
                if (!int.TryParse(sFtpPort, out iPort))
                {
                    ShowErr("FTP服务器端口必须为整数！");
                    return null;
                }
            }
            if (string.IsNullOrEmpty(sFtpUserName))
            {
                ShowErr("FTP用户名不能为空！");
                return null;
            }
            if (string.IsNullOrEmpty(sFtpPassword))
            {
                ShowErr("FTP用户密码不能为空！");
                return null;
            }

            if (cbbConnCharset.SelectedValue == null || string.IsNullOrEmpty(cbbConnCharset.SelectedValue.ToString()))
            {
                ShowErr("请选择连接字符集！");
                return null;
            }

            if (cbbCharSetEncode.SelectedValue == null || string.IsNullOrEmpty(cbbCharSetEncode.SelectedValue.ToString()))
            {
                ShowErr("请选择文件字符集！");
                return null;
            }

            ftpServer.ConnEncodingString = cbbConnCharset.SelectedValue.ToString();
            ftpServer.ConnEncoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);
            ftpServer.FileEncodingString = cbbCharSetEncode.SelectedValue.ToString();
            ftpServer.FileEncoding = BaseFileEncoding.GetEncodingByKey(ftpServer.FileEncodingString);

            string sProtocol = cbbFtpProtocol.SelectedValue.ToString();
            FtpProtocolType protocolType = FtpProtocolType.FTP;
            if ("1".Equals(sProtocol))
            {
                protocolType = FtpProtocolType.FTP;
                if (string.IsNullOrEmpty(sFtpPort)) iPort = 21;
            }
            else if ("2".Equals(sProtocol))
            {
                protocolType = FtpProtocolType.SFTP;
                if (string.IsNullOrEmpty(sFtpPort)) iPort = 22;
            }
            else
            {
                protocolType = FtpProtocolType.FTPS;
                if (string.IsNullOrEmpty(sFtpPort)) iPort = 21;
            }

            //确定读取目录
            string sCon = "/"; //连接字符
            if (string.IsNullOrEmpty(sFtpInitPath))
            {
                sFtpInitPath = "/"; //为空时取根目录
                sCon = "";
            }

            if (string.IsNullOrEmpty(sFtpReadDir))
            {
                sFtpReadDir = sFtpInitPath; //为空时取根目录
            }
            else
            {
                if(sFtpReadDir.startsWith("/") || sFtpReadDir.startsWith(@"\"))
                {
                    sFtpReadDir = sFtpInitPath + sCon + sFtpReadDir.Substring(1); //为空时取根目录
                }
                else
                {
                    sFtpReadDir = sFtpInitPath + sCon + sFtpReadDir; //为空时取根目录
                }
            }

            //确定上传目录
            if (string.IsNullOrEmpty(sFtpUploadDir))
            {
                sFtpUploadDir = sFtpInitPath; //为空时取根目录
            }
            else
            {
                if (sFtpUploadDir.startsWith("/") || sFtpUploadDir.startsWith(@"\"))
                {
                    sFtpUploadDir = sFtpInitPath + sCon + sFtpUploadDir.Substring(1); //为空时取根目录
                }
                else
                {
                    sFtpUploadDir = sFtpInitPath + sCon + sFtpUploadDir; //为空时取根目录
                }
            }
            //
            ftpServer.IPAddr = sFtpIP;
            ftpServer.PortNum = iPort;
            ftpServer.UserID = sFtpUserName;
            ftpServer.Password = sFtpPassword;
            ftpServer.Protocol = protocolType;
            ftpServer.InitDir = sFtpInitPath;
            ftpServer.ReadServerDir = sFtpReadDir;
            ftpServer.UploadServerDir = sFtpUploadDir;

            SaveFuncConfig();//保存用户偏好值
            return ftpServer;
        }

        #region FTP的排除
        /// <summary>
        /// 排除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcludeSelect_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbExcludeFileName.Text.Trim();
            string sExcludeDirName = txbExcludeDir.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName) && string.IsNullOrEmpty(sExcludeDirName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' },StringSplitOptions.RemoveEmptyEntries);
            string[] sFilterDir = sExcludeDirName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

            DataTable dtFtpFile = dgvFtpFileList.GetBindingTable();
            if (dtFtpFile.Rows.Count == 0) return;

            var query = from f in dtFtpFile.AsEnumerable()
                        where GetLinqDynamicWhere(sFilter, sFilterDir, f)
                        select f;
            foreach (var item in query.ToList())
            {
                item[_sGridColumnSelect] = "0"; //设置为不选中
            }

            SaveFuncConfig();
        }

        private static bool GetLinqDynamicWhere(string[] filterArr, string[] filterDirArr, DataRow drF)
        {
            foreach (var item in filterArr)
            {
                string sFilePath = drF.Field<string>("FILE_PATH");
                sFilePath = sFilePath.Substring(sFilePath.LastIndexOf("/")).Replace("/", "");
                if (sFilePath.Contains(item))
                {
                    return true;
                }
            }
            foreach (var item in filterDirArr)
            {
                string sFilePath = drF.Field<string>("FILE_PATH");
                sFilePath = sFilePath.Substring(0, sFilePath.LastIndexOf("/"));
                string[] arrSplit = sFilePath.Split(new char[] { '/', '\\' },StringSplitOptions.RemoveEmptyEntries);
                if (arrSplit.Contains(item))
                {
                    return true;
                }
            }
            return false;
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
            string[] sFilterDir = sExcludeDirName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

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
            cbbFileSource.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FileSource, "1").Value;
            txbSavePath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_SavePath, "").Value;
            
            txbIPAddr.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_IPAddr, "").Value;
            txbPort.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_PortNum, "").Value;
            txbUserName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_UserName, "").Value;
            txbPassword.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_Pwd, "").Value;
            txbFtpInitDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_InitDir, "").Value;
            cbbFtpProtocol.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_Protocol, "2").Value;
            cbbConnCharset.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_CharsetEncodingConnection, "utf-8").Value;

            txbFtpDownloadLocalPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_DownloadLocalDir, "").Value;
            txbFtpReadPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_ReadDir, "").Value;
            ckbDownLoadAddList.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_DownLoadIsAddList, "1").Value) ? true : false;
            ckbClearLocalSaveDir.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_DownloadBeforeIsClearLocalDir, "1").Value) ? true : false;
            ckbFtpExcludeDefault.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_IsDefaultExclude, "1").Value) ? true : false;
            ckbDownPathExcludeFtpReadPath.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_DownPathExcludeFtpReadPath, "1").Value) ? true : false;

            txbFtpUploadPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_UploadDir, "").Value;
            cbbUploadReplaceType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_UploadReplaceType, "1").Value;
            txbFtpUploadBackupDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_UploadBackupDir, "").Value;

            cbbCharSetEncode.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_CharsetEncoding, "utf-8").Value;
            cbbTemplateType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_TemplateType, "1").Value;
            cbbLocalCopyType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_GenerateType, "1").Value;
            cbbBackupDirType.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_UploadBackupDirType, "1").Value;

            txbExcludeFileName.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_ExcludeFileName, "").Value;
            txbExcludeDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_FTP_ExcludeDirName, "").Value;
            txbCopyExcludeFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_CopyExcludeFileName, "").Value;
            txbCopyExcludeDir.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_CopyExcludeDirName, "").Value;

            txbFinalResultSavePath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_ReplaceResultFilterSavePath, "").Value;
            txbResultFilterExcludeFile.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_ResultCopyExcludeFileName, "").Value;//替换结果复制时的排除文件名
            txbFileFileEndfix.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_ResultCopyFileEndfix, "").Value;
            ckbIsUseEndfix.Checked = "1".Equals(WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.TextFileReplace_ResultCopyFileIsUseEndfix, "1").Value) ? true : false;

        }

        /// <summary>
        /// 保存功能配置
        /// </summary>
        private void SaveFuncConfig()
        {
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FileSource, cbbFileSource.SelectedValue.ToString(), "【文本文件字符替换】文件来源");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_SavePath, txbSavePath.Text.Trim(), "【文本文件字符替换】最终生成目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_CharsetEncoding, cbbCharSetEncode.SelectedValue.ToString(), "【文本文件字符替换】文本的字符集类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_ReplaceResultFilterSavePath, txbFinalResultSavePath.Text.Trim(), "【文本文件字符替换】替换结果的二次筛选保存路径");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_ExcludeFileName, txbExcludeFileName.Text.Trim(), "【文本文件字符替换】FTP下载时排除包含的文件名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_ExcludeDirName, txbExcludeDir.Text.Trim(), "【文本文件字符替换】FTP下载时排除包含的目录名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_CopyExcludeFileName, txbCopyExcludeFile.Text.Trim(), "【文本文件字符替换】复制到本地时排除包含的文件名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_CopyExcludeDirName, txbCopyExcludeDir.Text.Trim(), "【文本文件字符替换】复制到本地时排除包含的目录名");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_GenerateType, cbbLocalCopyType.SelectedValue.ToString(), "【文本文件字符替换】从已下载目录复制文件的方式");
            string sTempType = cbbTemplateType.SelectedValue == null ? "" : cbbTemplateType.SelectedValue.ToString();
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_TemplateType, sTempType, "【文本文件字符替换】模板类型");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_IPAddr, txbIPAddr.Text.Trim(), "【文本文件字符替换】Ftp服务器的IP");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_PortNum, txbPort.Text.Trim(), "【文本文件字符替换】Ftp服务器的端口号");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_UserName, txbUserName.Text.Trim(), "【文本文件字符替换】Ftp服务器的用户名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_Pwd, txbPassword.Text.Trim(), "【文本文件字符替换】Ftp服务器的密码");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_InitDir, txbFtpInitDir.Text.Trim(), "【文本文件字符替换】Ftp服务器的初始目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_Protocol, cbbFtpProtocol.SelectedValue.ToString(), "【文本文件字符替换】Ftp服务器的协议");
            sTempType = cbbConnCharset.SelectedValue == null ? "" : cbbConnCharset.SelectedValue.ToString();
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_CharsetEncodingConnection, sTempType, "【文本文件字符替换】FTP连接的字符集类型"); 

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_DownloadLocalDir, txbFtpDownloadLocalPath.Text.Trim(), "【文本文件字符替换】保存的本地目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_ReadDir, txbFtpReadPath.Text.Trim(), "【文本文件字符替换】Ftp读取目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_DownLoadIsAddList, ckbDownLoadAddList.Checked ? "1" : "0", "【文本文件字符替换】是否下载加入清单");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_DownloadBeforeIsClearLocalDir, ckbClearLocalSaveDir.Checked ? "1" : "0", "【文本文件字符替换】是否下载前清除本地目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_IsDefaultExclude, ckbFtpExcludeDefault.Checked ? "1" : "0", "【文本文件字符替换】是否FTP默认排除");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_DownPathExcludeFtpReadPath, ckbDownPathExcludeFtpReadPath.Checked ? "1" : "0", "【文本文件字符替换】下载路径不含读取路径");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_UploadDir, txbFtpUploadPath.Text.Trim(), "【文本文件字符替换】Ftp上传目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_UploadBackupDir, txbFtpUploadBackupDir.Text.Trim(), "【文本文件字符替换】Ftp上传备份目录");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_UploadReplaceType, cbbUploadReplaceType.SelectedValue.ToString(), "【文本文件字符替换】Ftp上传的替换类型");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_FTP_UploadBackupDirType, cbbBackupDirType.SelectedValue.ToString(), "【文本文件字符替换】Ftp上传的备份目录类型");

            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_ResultCopyExcludeFileName, txbResultFilterExcludeFile.Text.Trim(), "【文本文件字符替换】替换结果复制时的排除文件名");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_ResultCopyFileEndfix, txbFileFileEndfix.Text.Trim(), "【文本文件字符替换】替换结果复制时指定的文件后缀");
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.TextFileReplace_ResultCopyFileIsUseEndfix, ckbIsUseEndfix.Checked ? "1" : "0", "【文本文件字符替换】替换结果复制时是否使用指定的文件后缀");
            
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
        private void dgvFtpFileList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvFtpFileList, ref _allSelectFtp, e);
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
                    if (iRow == 0)
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
                    if(iRow == 0 || iColumn < 2)
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
                            dtMain.Rows.Add(dtMain.Rows.Count + 1, "1",strData, strData2);
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
            else if(dtReplace != null)
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
            if(isAdd)
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
            cbbTemplateType.BindDropDownList(replaceStringData.MoreXmlConfig.KeyData, sKeyId, ReplaceStringXmlConfig.KeyString.Name, true,true);
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
