using Breezee.Core.Entity;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.Core.WinFormUI.Common;
using Breezee.Framework.Mini.Entity;
using Breezee.WorkHelper.DBTool.Entity;
using FluentFTP;
using FluentFTP.Rules;
using org.breezee.MyPeachNet;
using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Breezee.Core.Interface.FtpServerInfo;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// FTP上传下载控件
    /// </summary>
    public partial class UC_FtpDownUpload : BaseUserControl
    {
        private IDictionary<string,string> _dicString = new Dictionary<string,string>();
        private readonly string _sGridColumnSelect = "IsSelect";
        private bool _allSelectFtp = false;//默认全选，这里取反
        public FtpServerInfo _ftpServerInfo;

        public event EventHandler<AfterDownFileEventArgs> AfterDownAllFile; //下载所有文件后事件
        public event EventHandler<EventArgs> BeforeUploadSelectedFile; //上传指定文件前
        private DataRow[] willUploadDataRows; //将上传的数据行
        public FtpConfigEntity ftpConfig;
        //控件集合字典
        List<DBColumnControlRelation> _listControlRel = new List<DBColumnControlRelation>();

        public FtpServerInfo FtpServerInfo { get { return _ftpServerInfo; } }


        public UC_FtpDownUpload()
        {
            InitializeComponent();
        }

        private void UC_FtpDownUpload_Load(object sender, EventArgs e)
        {
            //_ftpServerInfo = new FtpServerInfo();
            ftpConfig = new FtpConfigEntity(GlobalContext.PathData(), MiniStaticString.FtpServerConfig);

            _dicString.Clear();
            _dicString[((int)FtpUploadStyle.DeleteAdd).ToString()] = "先删后增"; 
            _dicString[((int)FtpUploadStyle.OverWrite).ToString()] = "直接覆盖";
            _dicString[((int)FtpUploadStyle.Backup).ToString()] = "备份后覆盖";
            cbbUploadReplaceType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbUploadReplaceType, "先删后增：会把FTP上的目录删除后，再新增；\r\n直接覆盖：使用原目录，文件存在会覆盖。\r\n备份后覆盖：会将原目录复制到备份目录，并加上日期重命名；再覆盖文件。");

            _dicString.Clear();
            _dicString[((int)FtpProtocolType.FTP).ToString()] = "FTP";
            _dicString[((int)FtpProtocolType.SFTP).ToString()] = "SFTP";
            _dicString[((int)FtpProtocolType.FTPS).ToString()] = "FTPS";
            cbbFtpProtocol.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);

            _dicString.Clear();
            _dicString[((int)UploadBackupDirStyle.BackupUploadDir).ToString()] = "备份上传目录";
            _dicString[((int)UploadBackupDirStyle.BackupDownloadDir).ToString()] = "备份读取目录";
            cbbBackupDirType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(cbbBackupDirType, "FTP服务器端哪个目录的文件需要备份！");

            DataTable dtEncoding = BaseFileEncoding.GetEncodingTable(false);
            cbbFileCharSetEncode.BindTypeValueDropDownList(dtEncoding, false, true);
            toolTip1.SetToolTip(cbbFileCharSetEncode, "如文件出现乱码，需要修改文件字符集！");
            //连接字符集：跟文件数据集一样的内容
            cbbConnCharset.BindTypeValueDropDownList(dtEncoding.Copy(), false, true);
            toolTip1.SetToolTip(cbbConnCharset, "如目录出现乱码，需要修改连接字符集！");
            //本地目录或文件
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("FILE_PATH").Caption("文件路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(600).Edit(true).Visible().Build()
                );
            dgvFtpFileList.Tag = fdc.GetGridTagString();
            dgvFtpFileList.BindDataGridView(null, false);

            //设置组件控件关系
            SetControlColumnRelation();
            //绑定下拉框
            cbbConfigCode.BindDropDownList(ftpConfig.Data, FtpServerInfo.PropString.ID, FtpServerInfo.PropString.ConfigCode,true,true);
        }

        #region 设置列名与控件关系
        private void SetControlColumnRelation()
        {
            //配置表
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ID, txbId)); 
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ConfigCode, txbConfigCode, "FTP配置编码"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ConfigName, txbFtpConfigName, "FTP配置名称"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.IPAddr, txbIPAddr,"FTP服务器IP"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.PortNum, txbPort));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UserID, txbUserName, "用户名"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.Password, txbPassword, "密码"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.InitDir, txbFtpInitDir));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.Protocol, cbbFtpProtocol));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.FileEncodingString, cbbFileCharSetEncode));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ConnEncodingString, cbbConnCharset));
            //下载项
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ReadServerDir, txbFtpReadPath, "读取目录"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.DownloadLocalDir, txbFtpDownloadLocalPath, "本地保存路径"));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.DownloadIsDeleteDir, ckbClearLocalSaveDir));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.DownloadIsExcludeReadDir, ckbDownPathExcludeFtpReadPath));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.DownloadIsAutoExclude, ckbFtpExcludeDefault));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ExcludeDirList, txbExcludeDir));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.ExcludeFileList, txbExcludeFileName));
            //保存项
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UploadServerDir,txbFtpUploadPath));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UploadLocalDir, txbSavePath));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UploadBackupDir, txbFtpUploadBackupDir));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UploadBackupDirStyle, cbbBackupDirType));
            _listControlRel.Add(new DBColumnControlRelation(FtpServerInfo.PropString.UploadStyle, cbbUploadReplaceType));
        }
        #endregion

        public string SaveFtpConifg(bool isUplpad)
        {
            //保存前判断
            string strInfo = _listControlRel.JudgeNotNull(true);
            if (!string.IsNullOrEmpty(strInfo))
            {
                return strInfo;
            }

            if (isUplpad)
            {
                if (string.IsNullOrEmpty(txbFtpUploadPath.Text.Trim()))
                {
                    return "【上传目录】不能为空！";
                }
                if (string.IsNullOrEmpty(txbSavePath.Text.Trim()))
                {
                    return "【上传的本地路径】不能为空！";
                }
            }

            DataTable dtSave = FtpServerInfo.getPropList().GetTable();
            bool isAdd = string.IsNullOrEmpty(cbbConfigCode.Text);
            if (isAdd)
            {
                string sCode = txbConfigCode.Text;
                if (!string.IsNullOrEmpty(sCode))
                {
                    DataRow[] drArr = ftpConfig.JsonConfig.Get(sCode);
                    if (drArr.Count() > 0)
                    {
                        return sCode + "已存在，不能新增！";
                    }
                }
                txbId.Text = Guid.NewGuid().ToString(); //新增时赋值GUID
                _listControlRel.GetControlValue(dtSave, isAdd);
                _ftpServerInfo = FtpServerInfo.getServerInfo(dtSave.Rows[0]);
                ftpConfig.Data.ImportRow(dtSave.Rows[0]);
                ftpConfig.Save();
                //重新绑定下拉框
                cbbConfigCode.BindDropDownList(ftpConfig.Data, FtpServerInfo.PropString.ID, FtpServerInfo.PropString.ConfigCode, true, true);
            }
            else
            {
                DataRow[] drArr = ftpConfig.Get(txbId.Text);
                if (drArr.Count() > 0)
                {
                    _listControlRel.GetControlValue(drArr[0], isAdd);
                    _ftpServerInfo = FtpServerInfo.getServerInfo(drArr[0]);
                    ftpConfig.Save();
                }
            }
            return string.Empty;
        }
        private async void btnFtpDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr = SaveFtpConifg(false);
                if (!string.IsNullOrEmpty(sErr))
                {
                    MsgHelper.ShowErr(sErr);
                    return;
                }

                string sDownLocalPath = txbFtpDownloadLocalPath.Text.Trim();
                string sReadPath = txbFtpReadPath.Text.Trim();
                if (string.IsNullOrEmpty(sDownLocalPath))
                {
                    MsgHelper.ShowErr("本地保存路径不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(sReadPath))
                {
                    MsgHelper.ShowErr("读取目录不能为空！");
                    return;
                }

                if (MsgHelper.ShowOkCancel("确定要下载所有文件？") == DialogResult.Cancel)
                    return;

                //DataTable dtList = dgvFileListWaitFor.GetBindingTable().Clone();
                List<DownFileInfo> downFiles = new List<DownFileInfo>();
                //异步下载FTP所有文件
                ShowGlobalMsg(this,"已启动异步下载所有文件，请稍等...");
                btnFtpDownLoad.Enabled = false;
                await Task.Run(() => DowloadAllFtpFiles(_ftpServerInfo, sDownLocalPath, sReadPath, downFiles));
                btnFtpDownLoad.Enabled = true;

                if (AfterDownAllFile != null)
                {
                    AfterDownFileEventArgs args = new AfterDownFileEventArgs(downFiles);
                    AfterDownAllFile(this, args);
                }
                ShowGlobalMsg(this,"所有文件下载成功！");
            }
            catch (Exception ex)
            {
                MsgHelper.ShowErr(ex.Message);
            }
            finally
            {
                btnFtpDownLoad.Enabled = true;
            }
        }

        private async void btnFtpDownSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr = SaveFtpConifg(false);
                if (!string.IsNullOrEmpty(sErr))
                {
                    MsgHelper.ShowErr(sErr);
                    return;
                }

                DataTable dtList = dgvFtpFileList.GetBindingTable();
                DataRow[] drWillArr = dtList.Select(_sGridColumnSelect + "= '1'");
                if (drWillArr.Length == 0)
                {
                    MsgHelper.ShowErr("没有要下载的文件，请先选择！");
                    return;
                }
                string sDownPath = txbFtpDownloadLocalPath.Text.Trim();
                if (string.IsNullOrEmpty(sDownPath))
                {
                    MsgHelper.ShowErr("本地保存路径不能为空！");
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

                //DataTable dtWaitList = dgvFileListWaitFor.GetBindingTable().Clone();
                List<DownFileInfo> downFiles = new List<DownFileInfo>();
                ShowGlobalMsg(this,"已启动异步下载所选择的文件，请稍等...");
                btnFtpDownSelectFile.Enabled = false;
                await Task.Run(() => DownloadSelectedFileAsync(_ftpServerInfo, drWillArr, sDownPath, downFiles));
                btnFtpDownSelectFile.Enabled = true;

                if (AfterDownAllFile != null)
                {
                    AfterDownFileEventArgs args = new AfterDownFileEventArgs(downFiles);
                    AfterDownAllFile(this, args);
                }
                ShowGlobalMsg(this,"选择部分文件已下载成功！");
            }
            catch (Exception ex)
            {
                MsgHelper.ShowErr(ex.Message);
            }
            finally
            {
                btnFtpDownSelectFile.Enabled = true;
            }
        }

        private async void btnFtpShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr = SaveFtpConifg(false);
                if (!string.IsNullOrEmpty(sErr))
                {
                    MsgHelper.ShowErr(sErr);
                    return;
                }

                if (_ftpServerInfo == null) return;

                DataTable dtList = dgvFtpFileList.GetBindingTable().Clone();

                ShowGlobalMsg(this,"已启动异步显示所有文件，请稍等...");
                btnFtpShowAll.Enabled = false;
                await Task.Run(() => ShowFtpFileAsync(_ftpServerInfo, dtList));
                btnFtpShowAll.Enabled = true;
                ShowGlobalMsg(this, "异步显示所有文件完毕！");

                dgvFtpFileList.BindDataGridView(dtList, false);
                //如果选中默认排除筛选框，那么调用点击排除按钮
                if (ckbFtpExcludeDefault.Checked)
                {
                    btnExcludeSelect.PerformClick();
                }
            }
            catch (Exception ex)
            {
                ShowGlobalMsg(this, ex.Message);
            }
            finally
            {
                btnFtpShowAll.Enabled = true;
            }
        }

        private async void btnExcludeSelect_Click(object sender, EventArgs e)
        {
            string sExcludeFileName = txbExcludeFileName.Text.Trim();
            string sExcludeDirName = txbExcludeDir.Text.Trim();
            if (string.IsNullOrEmpty(sExcludeFileName) && string.IsNullOrEmpty(sExcludeDirName))
            {
                return;
            }

            string[] sFilter = sExcludeFileName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
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

            //SaveFuncConfig();
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

        private async void btnFtpUploadDir_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr = SaveFtpConifg(true);
                if (!string.IsNullOrEmpty(sErr))
                {
                    MsgHelper.ShowErr(sErr);
                    return;
                }

                if (_ftpServerInfo == null) return;

                string sUploadPath = txbFtpUploadPath.Text.Trim();
                if (string.IsNullOrEmpty(sUploadPath))
                {
                    MsgHelper.ShowErr("【上传目录】不能为空！");
                    return;
                }
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    MsgHelper.ShowErr("【最终生成目录】不能为空！");
                    return;
                }

                //读取保存目录文件进行
                FileInfo[] arrFiles = new DirectoryInfo(sSavePath).GetFiles("*.*", SearchOption.AllDirectories);
                if (arrFiles.Length == 0)
                {
                    MsgHelper.ShowErr("【最终保存路径】目录下没有要上传的文件！");
                    return;
                }

                //1先删后增、2覆盖、3备份
                string sUploadType = cbbUploadReplaceType.SelectedValue.ToString();
                string sFtpBackupDir = txbFtpUploadBackupDir.Text.Trim();
                if ("3".Equals(sUploadType) && string.IsNullOrEmpty(sFtpBackupDir))
                {
                    MsgHelper.ShowErr("当【上传方式】为【备份】时，【备份目录】不能为空！");
                    txbFtpUploadBackupDir.Focus();
                    return;
                }

                if (MsgHelper.ShowOkCancel("确定要将【最终生成目录】文件上传到FTP服务上？") == DialogResult.Cancel)
                    return;

                bool isSuccess = false;
                //异步下载FTP所有文件
                ShowGlobalMsg(this, "已启动异步目录文件上传到FTP服务器，请稍等...");
                btnFtpUploadDir.Enabled = false;
                isSuccess = await Task.Run(() => UploadDirAsync(_ftpServerInfo, sUploadPath, sSavePath, arrFiles, sUploadType, sFtpBackupDir, isSuccess));
                btnFtpUploadDir.Enabled = true;

                //SaveFuncConfig();//保存用户偏好值
                if (isSuccess)
                {
                    ShowGlobalMsg(this, "目录文件已成功上传到FTP服务器！");
                }
                else
                {
                    MsgHelper.ShowErr("目录文件上传到FTP服务器失败！");
                }
            }
            catch (Exception ex)
            {
                MsgHelper.ShowErr(ex.Message);
            }
            finally
            {
                btnFtpUploadDir.Enabled = true;
            }
        }

        private async void btnFtpUpLoadSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr = SaveFtpConifg(true);
                if (!string.IsNullOrEmpty(sErr))
                {
                    MsgHelper.ShowErr(sErr);
                    return;
                }
                if (_ftpServerInfo == null) return;

                string sUploadPath = txbFtpUploadPath.Text.Trim();
                if (string.IsNullOrEmpty(sUploadPath))
                {
                    MsgHelper.ShowErr("【上传目录】不能为空！");
                    return;
                }
                string sSavePath = txbSavePath.Text.Trim();
                if (string.IsNullOrEmpty(sSavePath))
                {
                    MsgHelper.ShowErr("【最终生成目录】不能为空！");
                    return;
                }

                if (BeforeUploadSelectedFile != null)
                {
                    BeforeUploadSelectedFile(this, new EventArgs());
                }

                if (willUploadDataRows==null || willUploadDataRows.Length == 0)
                {
                    MsgHelper.ShowErr("没有要上传的文件！");
                    return;
                }

                //1先删后增、2覆盖、3备份
                string sUploadType = cbbUploadReplaceType.SelectedValue.ToString();
                string sFtpBackupDir = txbFtpUploadBackupDir.Text.Trim();
                if ("3".Equals(sUploadType) && string.IsNullOrEmpty(sFtpBackupDir))
                {
                    MsgHelper.ShowErr("当【上传方式】为【备份】时，【备份目录】不能为空！");
                    txbFtpUploadBackupDir.Focus();
                    return;
                }

                if (MsgHelper.ShowOkCancel("确定要将【所选文件】文件上传到FTP服务上？") == DialogResult.Cancel)
                    return;

                //异步下载FTP所有文件
                ShowGlobalMsg(this, "已启动异步文件上传到SFTP服务器，请稍等...");
                btnFtpUpLoadSelectFile.Enabled = false;
                await Task.Run(() => UploadSelectedFileAsync(_ftpServerInfo, sUploadPath, sSavePath, willUploadDataRows, sUploadType, sFtpBackupDir));
                btnFtpUpLoadSelectFile.Enabled = true;

                ShowGlobalMsg(this, "所选文件已全部成功上传到SFTP服务器！");
            }
            catch (Exception ex)
            {
                MsgHelper.ShowErr(ex.Message);
            }
            finally
            {
                btnFtpUpLoadSelectFile.Enabled = true;
            }
        }

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
        private async Task DowloadAllFtpFiles(FtpServerInfo ftpServer, string sDownPath, string sReadPath, List<DownFileInfo> downFiles)
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
                myConnectionInfo.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);
                int iIndex = 1;

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录
                    SftpTool.DownSFTPDirectory(client, ftpServer.ReadServerDir, sDownPath, ftpServer,ref iIndex, downFiles); //递归下载文件
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
                    conn.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString); //
                    await conn.Connect(token);

                    //更新模式：FtpFolderSyncMode.Update，源方式（删除多余文件）：FtpFolderSyncMode.Mirror
                    var result = await conn.DownloadDirectory(sDownPath, sReadPath, FtpFolderSyncMode.Mirror, token: token);

                    int iIndex = 1;
                    foreach (var item in result)
                    {
                        if (item.Type == FtpObjectType.File)
                        {
                            string sRelDir = item.RemotePath.Substring(0, item.RemotePath.LastIndexOf("/"));
                            if (ftpServer.DownloadIsExcludeReadDir)
                            {
                                sRelDir = sRelDir.Replace(ftpServer.ReadServerDir, "");//排除读取路径
                            }

                            DownFileInfo downFile = new DownFileInfo(iIndex, "1", "文件", item.LocalPath, sRelDir);
                            downFiles.Add(downFile);
                            //dtWaitList.Rows.Add(iIndex, "1", "文件", item.LocalPath, sRelDir); //添加到待处理网格中
                            iIndex++;
                        }
                    }
                }
                #endregion
            }
            if (AfterDownAllFile != null)
            {
                AfterDownFileEventArgs args = new AfterDownFileEventArgs(downFiles);
                AfterDownAllFile(this, args);
            }
        }

        /// <summary>
        /// 异步下载所选文件方法
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="drWillArr"></param>
        /// <param name="sDownPath"></param>
        /// <returns></returns>
        private async Task DownloadSelectedFileAsync(FtpServerInfo ftpServer, DataRow[] drWillArr, string sDownPath, List<DownFileInfo> downFiles)
        {
            int iIndex = 1;
            //得到文件清单
            var files = drWillArr.AsEnumerable().Select(t => t["FILE_PATH"].ToString()).ToList();

            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录
                    
                    foreach (string item in files)
                    {
                        string sServerRelDir = ftpServer.DownloadIsExcludeReadDir ? item.Replace(ftpServer.ReadServerDir, "") : item;
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
                        string sRelDir = item.Substring(0, item.LastIndexOf("/")).Replace(ftpServer.ReadServerDir, "");
                        DownFileInfo downFile = new DownFileInfo(iIndex, "1", "文件", sLocal, sRelDir);
                        downFiles.Add(downFile);
                        //dtWaitList.Rows.Add(iIndex, "1", "文件", sLocal, sRelDir); //添加到待处理网格中
                        iIndex++;
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
                    conn.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);
                    await conn.Connect(token);

                    foreach (string item in files)
                    {
                        string sServerRelDir = ftpServer.DownloadIsExcludeReadDir ? item.Replace(ftpServer.ReadServerDir, "") : item;
                        string sLocal = sDownPath + sServerRelDir.Replace(@"/", @"\");
                        string sDir = sLocal.substring(0, sLocal.LastIndexOf(@"\"));
                        //如果本地目录不存在，则创建
                        if (!Directory.Exists(sDir))
                        {
                            Directory.CreateDirectory(sDir);
                        }
                        //下载并覆盖
                        var result = await conn.DownloadFile(sLocal, item, FtpLocalExists.Overwrite, FtpVerify.None, token: token);
                        DownFileInfo downFile = new DownFileInfo(iIndex, "1", "文件", sLocal, sServerRelDir.Substring(0, sServerRelDir.LastIndexOf("/")));
                        downFiles.Add(downFile);
                        //dtWaitList.Rows.Add(iIndex, "1", "文件", sLocal, sServerRelDir.Substring(0, sServerRelDir.LastIndexOf("/"))); //添加到待处理网格中
                        iIndex++;
                    }
                }
            }
        }

        private async Task ShowFtpFileAsync(FtpServerInfo ftpServer, DataTable dtList)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);

                using (var client = new SftpClient(myConnectionInfo))
                {
                    //client.
                    await client.ConnectAsync(token); //连接
                    client.ChangeDirectory(ftpServer.ReadServerDir); //改变目录
                    SftpTool.ListDirectory(client, ".", dtList); //递归查找文件
                }
            }
            else
            {
                var token = new CancellationToken();
                using (var conn = new AsyncFtpClient(ftpServer.IPAddr, ftpServer.UserID, ftpServer.Password, ftpServer.PortNum))
                {
                    conn.Config.EncryptionMode = FtpEncryptionMode.None;
                    conn.Config.ValidateAnyCertificate = true;
                    conn.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);
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

        private async Task<bool> UploadDirAsync(FtpServerInfo ftpServer, string sUploadPath, string sSavePath, FileInfo[] arrFiles, string sUploadType, string sFtpBackupDir, bool isSuccess)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                #region SFTP
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);

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
                            SftpTool.DeleteDirectoryAndFile(client, sUploadPath);
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

                        string sBackupSourceDir = ftpServer.UploadBackupDirType== FtpServerInfo.UploadBackupDirStyle.BackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        client.CreateDirectory(sNewPath); //创建一个子目录
                        //递归备份文件
                        client.ChangeDirectory(sNewPath);//改变目录为上传目录
                        ShowGlobalMsg(this, "正在备份目录“" + sBackupSourceDir + "”，请稍等...");
                        SftpTool.BackupFilesSFTP(client, sBackupSourceDir, client.WorkingDirectory, ftpServer);
                        ShowGlobalMsg(this, "备份已完成，正在上传文件，请稍等...");
                    }
                    else
                    {
                        //覆盖方式
                        if (!isExistDir)
                        {
                            client.CreateDirectory(sUploadPath);
                        }
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
                            //上传文件
                            string sFilePath = ftpServer.UploadServerDir + Path.Combine(sDir, file.Name).Replace("\\", "/");
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
                    conn.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString); //
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
                        string sBackupSourceDir = ftpServer.UploadBackupDirType == FtpServerInfo.UploadBackupDirStyle.BackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sFtpBackupDir + "/" + sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        if (!isExistDir)
                        {
                            await conn.CreateDirectory(sFtpBackupDir);
                        }
                        else
                        {
                            //目录存在，则判断其下有没文件，有才备份
                            FtpListItem[] listArr = await conn.GetListing(sBackupSourceDir, token: token);
                            if (listArr.Length > 0)
                            {
                                await conn.CreateDirectory(sNewPath); //创建一个子目录
                            }
                            //以下方式会移除原目录，不能使用
                            ShowGlobalMsg(this, "正在备份目录“" + sBackupSourceDir + "”，请稍等...");
                            //await conn.MoveDirectory(sBackupSourceDir, sNewPath, FtpRemoteExists.Overwrite, token: token);
                            await BackFtpsFile(conn, sNewPath, listArr, token); //递归备份服务文件
                            ShowGlobalMsg(this, "备份已完成，正在上传文件，请稍等...");
                        }
                    }
                    else
                    {
                        //覆盖方式
                        if (!isExistDir)
                        {
                            await conn.CreateDirectory(sUploadPath);
                        }
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
        /// FTPS的备份文件
        /// 使用流方式，有点慢
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sNewPath"></param>
        /// <param name="listArr"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static async Task BackFtpsFile(AsyncFtpClient conn, string sNewPath, FtpListItem[] listArr, CancellationToken token)
        {
            foreach (FtpListItem item in listArr)
            {
                if (item.Type == FtpObjectType.Directory)
                {
                    FtpListItem[] listArr2 = await conn.GetListing(item.FullName);
                    string sNewPath2 = sNewPath + "/" + item.Name;
                    if (!conn.DirectoryExists(sNewPath2).GetAwaiter().GetResult())
                    {
                        await conn.CreateDirectory(sNewPath2);
                    }
                    await BackFtpsFile(conn, sNewPath2, listArr2, token);
                }
                else if(item.Type == FtpObjectType.File)
                {
                    await conn.UploadBytes(await conn.DownloadBytes(item.FullName, token: token), sNewPath + "/" + item.Name, FtpRemoteExists.Overwrite, true, token: token);

                }
            }
        }

        private async Task UploadSelectedFileAsync(FtpServerInfo ftpServer, string sUploadPath, string sSavePath, DataRow[] drWillArr, string sUploadType, string sFtpBackupDir)
        {
            if (ftpServer.Protocol == FtpProtocolType.SFTP)
            {
                #region SFTP
                var token = new CancellationToken();
                var meth = new PasswordAuthenticationMethod(ftpServer.UserID, ftpServer.Password);
                ConnectionInfo myConnectionInfo = new ConnectionInfo(ftpServer.IPAddr, ftpServer.PortNum, ftpServer.UserID, meth);
                myConnectionInfo.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString);

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
                            SftpTool.DeleteDirectoryAndFile(client, sUploadPath);
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

                        string sBackupSourceDir = ftpServer.UploadBackupDirType== FtpServerInfo.UploadBackupDirStyle.BackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        client.CreateDirectory(sNewPath); //创建一个子目录

                        //递归备份文件
                        client.ChangeDirectory(sNewPath);//改变目录
                        ShowGlobalMsg(this, "正在备份目录“" + sBackupSourceDir + "”，请稍等...");
                        SftpTool.BackupFilesSFTP(client, sBackupSourceDir, client.WorkingDirectory, ftpServer);
                        ShowGlobalMsg(this, "备份已完成，正在上传文件，请稍等...");
                    }
                    else
                    {
                        //覆盖方式
                        if (!isExistDir)
                        {
                            client.CreateDirectory(sUploadPath);
                        }
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
                    conn.Encoding = BaseFileEncoding.GetEncodingByKey(ftpServer.ConnEncodingString); //
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
                        string sBackupSourceDir = ftpServer.UploadBackupDirType == FtpServerInfo.UploadBackupDirStyle.BackupUploadDir ? ftpServer.UploadServerDir : ftpServer.ReadServerDir;
                        string sNewPath = sFtpBackupDir + "/" + sBackupSourceDir.Substring(1) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        if (!isExistDir)
                        {
                            //目录不存在，则创建目录
                            await conn.CreateDirectory(sFtpBackupDir);
                        }
                        else
                        {
                            //目录存在，则判断其下有没文件，有才备份
                            FtpListItem[] listArr = await conn.GetListing(sBackupSourceDir, token: token);
                            if (listArr.Length > 0)
                            {
                                await conn.CreateDirectory(sNewPath); //创建一个子目录
                            }
                            //以下方式会移除原目录，不能使用
                            ShowGlobalMsg(this, "正在备份目录“" + sBackupSourceDir + "”，请稍等...");
                            //await conn.MoveDirectory(sBackupSourceDir, sNewPath, FtpRemoteExists.Overwrite, token: token);
                            await BackFtpsFile(conn, sNewPath, listArr, token); //递归备份服务文件
                            ShowGlobalMsg(this, "备份已完成，正在上传文件，请稍等...");
                        }
                    }
                    else
                    {
                        //覆盖方式
                        if (!isExistDir)
                        {
                            await conn.CreateDirectory(sUploadPath);
                        }
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


        #region 网格双击全选事件
        private void dgvFtpFileList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvFtpFileList, ref _allSelectFtp, e);
        }
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
        #endregion

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string sErr = SaveFtpConifg(true);
            if (!string.IsNullOrEmpty(sErr))
            {
                MsgHelper.ShowErr(sErr);
                return;
            }
           ShowGlobalMsg(this,"配置保存成功！");
        }

        /// <summary>
        /// 删除配置按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveConfig_Click(object sender, EventArgs e)
        {
            //保存前判断
            string sId = txbId.Text;
            if (string.IsNullOrEmpty(sId))
            {
                return;
            }

            DataRow[] drArr = ftpConfig.Get(sId);
            if (drArr.Count() > 0)
            {
                ftpConfig.Data.Rows.Remove(drArr[0]);
                ftpConfig.Save();
                cbbConfigCode.BindDropDownList(ftpConfig.Data, FtpServerInfo.PropString.ID, FtpServerInfo.PropString.ConfigCode, true, true);//重新绑定下拉框
                ShowGlobalMsg(this,"成功删除配置！");
            }
        }

        /// <summary>
        /// 复制新增按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            cbbConfigCode.SelectedIndexChanged-= cbbConfigCode_SelectedIndexChanged;
            cbbConfigCode.SelectedIndex= 0;
            txbConfigCode.Text= txbConfigCode.Text+"_COPY";
            txbConfigCode.ReadOnly = false;
            cbbConfigCode.SelectedIndexChanged += cbbConfigCode_SelectedIndexChanged;
        }

        /// <summary>
        /// 配置选择下拉框选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbConfigCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbbConfigCode.Text))
            {
                string sID = cbbConfigCode.SelectedValue.ToString();
                DataRow[] drArr = ftpConfig.JsonConfig.Get(sID);
                if (drArr.Count() > 0)
                {
                    _listControlRel.SetControlValue(drArr[0]);
                }
                txbConfigCode.ReadOnly = true;
            }
            else
            {
                _listControlRel.CleanControlValue(); //清空控件值
                cbbConnCharset.SelectedValue = BaseFileEncoding.FileEncodingString.UTF8;
                cbbFileCharSetEncode.SelectedValue = BaseFileEncoding.FileEncodingString.UTF8;
            }
        }

        private void btnSelectUploadLocalDir_Click(object sender, EventArgs e)
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
            }
        }

        public void SetFileEncoding(string sValue, bool isReadonly = false)
        {
            cbbFileCharSetEncode.SelectedValue = sValue;
            cbbFileCharSetEncode.SetControlReadOnly(isReadonly);
        }
        public void SetUploadLocalDir(string sValue, bool isReadonly = false)
        {
            txbSavePath.Text = sValue;
            txbSavePath.SetControlReadOnly(isReadonly);
            btnSelectUploadLocalDir.SetControlReadOnly(isReadonly);
        }

        public void SetSelectRows(DataRow[] drArr)
        {
            if(drArr == null || drArr.Length == 0)
            {
                ShowGlobalMsg(this, "请传入要上传的行数组！");
                return;
            }
            if (!drArr[0].ContainsColumn("FILE_PATH") || !drArr[0].ContainsColumn("COPY_PATH") || !drArr[0].ContainsColumn("COPY_FILE"))
            {
                ShowGlobalMsg(this, "请保证上传的行包括以下列：FILE_PATH、COPY_PATH、COPY_FILE");
                return;
            }
            willUploadDataRows = drArr;
        }

        private void cbbUploadReplaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbUploadReplaceType.SelectedValue == null) return;
            int iDbType = int.Parse(cbbUploadReplaceType.SelectedValue.ToString());
            FtpUploadStyle ftpUploadStyle = (FtpUploadStyle)iDbType;
            if (ftpUploadStyle.Equals(FtpUploadStyle.Backup))
            {
                lblBackupType.Visible = true;
                cbbBackupDirType.Visible = true;
            }
            else
            {
                lblBackupType.Visible = false;
                cbbBackupDirType.Visible = false;
            }
        }
    }

    public class AfterDownFileEventArgs : EventArgs
    {
        private List<DownFileInfo> _downFileInfoList;

        public AfterDownFileEventArgs(List<DownFileInfo> downFileInfoList)
        {
            _downFileInfoList = downFileInfoList;
        }

        public List<DownFileInfo> DownFile { get => _downFileInfoList; set => _downFileInfoList = value; }
    }

    public class DownFileInfo
    {
        private int _idx;
        private string _isSelected;
        private string _fileType;
        private string _fullPath;
        private string _RelPath;

        public DownFileInfo(int idx, string isSelected, string fileType, string fullPath, string relPath)
        {
            _RelPath = relPath;
            _fullPath = fullPath;
            _idx = idx;
            IsSelected = isSelected;
            FileType = fileType;
        }

        public int Idx { get => _idx; set => _idx = value; }
        public string FullPath { get => _fullPath; set => _fullPath = value; }
        public string RelPath { get => _RelPath; set => _RelPath = value; }
        public string IsSelected { get => _isSelected; set => _isSelected = value; }
        public string FileType { get => _fileType; set => _fileType = value; }
    }
}
