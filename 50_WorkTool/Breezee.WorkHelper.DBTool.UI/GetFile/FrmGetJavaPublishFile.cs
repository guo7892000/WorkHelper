using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Tool.Helper;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.Entity.ExcelTableSQL;
using LibGit2Sharp;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：获取Java发布文件
    /// 创建作者：黄国辉
    /// 创建日期：2025-9-4
    /// 功能说明：获取Java发布文件
    /// </summary>
    public partial class FrmGetJavaPublishFile : BaseForm
    {
        #region 变量
        private readonly string _sGridColumnSelect = "IsSelect";
        private bool _allSelectFile = false;//默认全选，这里取反
        private bool _allSelectCodeClass = false;//默认全选，这里取反
        int iFileNum = 0;
        //分隔的字符数组
        char[] splitCharArr = new char[] { ',', '，', '：', ';', '；', '|' };

        List<string> _listFilePath;
        string sKeyId;
        JavaPublishFileConfig javaPublishFileConfig;
        #endregion

        #region 构造函数
        public FrmGetJavaPublishFile()
        {
            InitializeComponent();
        }

        #endregion

        #region 加载事件
        private void FrmDirectoryFileString_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now.AddHours(-10);
            dtpEnd.Value = DateTime.Now;
            //绑定覆盖类型下拉框
            _dicString.Clear();
            _dicString["1"] = "覆盖";
            _dicString["2"] = "覆盖当天";
            _dicString["3"] = "每次新增";
            cbbCopyType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            toolTip1.SetToolTip(label3, "覆盖：复制的文件放根目录，并覆盖其下所有文件；\r\n覆盖当天：当天目录，不存在则新增目录，存在则覆盖其下所有文件；\r\n每次新增：每次复制都新增年月日时分秒的目标，然后文件放入其中。");

            javaPublishFileConfig = new JavaPublishFileConfig("JavaPublishFileConfig.xml");
            sKeyId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            cbbTemplateType.BindDropDownList(javaPublishFileConfig.MoreXmlConfig.KeyData, sKeyId, JavaPublishFileConfig.KeyString.Name, true, true);

            // 绑定网格
            //新旧字符网格
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("file").Caption("代码相对文件路径").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(800).Edit(true).Visible().Build()
                );
            dgvInput.Tag = fdc.GetGridTagString();
            dgvInput.BindDataGridView(null, false);
            dgvInput.AllowUserToAddRows = true;

            // 代码和class目录对照
            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name(_sGridColumnSelect).Caption("选择").Type(DataGridViewColumnTypeEnum.CheckBox).Align(DataGridViewContentAlignment.MiddleCenter).Width(50).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(JavaPublishFileConfig.ValueString.RelCodeDir).Caption("代码目录").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name(JavaPublishFileConfig.ValueString.RelClassDir).Caption("class目录").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(300).Edit(true).Visible().Build()
                );
            dgvCodeClassRelConfig.Tag = fdc.GetGridTagString();
            dgvCodeClassRelConfig.BindDataGridView(null, false);
            dgvCodeClassRelConfig.AllowUserToAddRows = true;
            // 设置提示信息
            toolTip1.SetToolTip(label3, "src的父目录");
            toolTip1.SetToolTip(txbCodePath, "src的父目录");
            toolTip1.SetToolTip(label6, "相对于【代码目录】的相对路径，且会自动过滤以.开头的目录；\r\n支持逗号（中英文）、分号（中英文）、冒号（中文）、竖线（英文）分隔的多个排除项配置。");
            toolTip1.SetToolTip(label8, "相对于【代码目录】的相对路径，且会自动过滤以.开头的文件；\r\n支持逗号（中英文）、分号（中英文）、冒号（中文）、竖线（英文）分隔的多个排除项配置。");
        }
        #endregion

        #region 生成SQL按钮事件
        private async void tsbAutoSQL_Click(object sender, EventArgs e)
        {
            try
            {
                label21.Focus();
                string sCodePath = txbCodePath.Text.Trim();
                string sSelectCfgId = cbbTemplateType.SelectedValue == null ? "" : cbbTemplateType.SelectedValue.ToString();
                CopyCoverTypeEnum copyCoverType = (CopyCoverTypeEnum)int.Parse(cbbCopyType.SelectedValue.ToString());
                if (string.IsNullOrEmpty(sCodePath))
                {
                    ShowErr("请选择【代码目录】！");
                    return;
                }
                else
                {
                    if (!Directory.Exists(sCodePath))
                    {
                        ShowErr("【代码目录】不存在，请重新选择或录入！");
                        return;
                    }
                }

                string sClassPath = txbClassPath.Text.Trim();
                if (string.IsNullOrEmpty(sClassPath))
                {
                    ShowErr("请选择【class目录】！");
                    return;
                }
                else
                {
                    if (!Directory.Exists(sClassPath))
                    {
                        ShowErr("【class目录】不存在，请重新选择或录入！");
                        return;
                    }
                }

                string sCopyToPath = txbCopyToPath.Text.Trim();
                if (string.IsNullOrEmpty(sCopyToPath))
                {
                    ShowErr("请选择【复制到目录】！");
                    return;
                }
                else
                {
                    if (!Directory.Exists(sCopyToPath))
                    {
                        ShowErr("【复制到目录】不存在，请重新选择或录入！");
                        return;
                    }
                }

                // 变化文件录入
                DataTable dtInputFiles = dgvInput.GetBindingTable();
                DataTable dtSelectInput = dtInputFiles.Clone();
                string sFilter = string.Format("{0}='1' or {0}='True'", _sGridColumnSelect); //筛选选中表的所有列
                foreach (DataRow dr in dtInputFiles.Select(sFilter))
                {
                    dtSelectInput.ImportRow(dr);
                }
                if (dtInputFiles.Rows.Count == 0)
                {
                    ShowErr("请在【变化文件录入】网格中输入并勾选要获取的文件清单！");
                    return;
                }

                // 代码与class目录对照关系配置
                DataTable dtRelCodeClass = dgvCodeClassRelConfig.GetBindingTable();
                DataTable dtRelCfgSelet = dtRelCodeClass.Clone();
                sFilter = string.Format("{0}='1' or {0}='True'", _sGridColumnSelect); //筛选选中表的所有列
                foreach (DataRow dr in dtRelCodeClass.Select(sFilter))
                {
                    dtRelCfgSelet.ImportRow(dr);
                }
                if ( dtRelCfgSelet.Rows.Count == 0)
                {
                    ShowErr("【代码与class目录对照关系配置】不能为空，至少输入并选择一项！");
                    return;
                }

                rtbString.Clear();
                _listFilePath = new List<string>();
                StringBuilder sb = new StringBuilder();
                DirectoryInfo codeDirectory = new DirectoryInfo(sCodePath);
                GetJavaClassEntity getJavaClassEntity = new GetJavaClassEntity();
                getJavaClassEntity.ClassPath = sClassPath.Replace("/","\\").Trim('\\'); //将路径中的分隔符修改为符合Windows目录的
                getJavaClassEntity.CodePath = sCodePath.Replace("/", "\\").Trim('\\'); //将路径中的分隔符修改为符合Windows目录的
                getJavaClassEntity.CopyToPath = sCopyToPath.Replace("/", "\\").Trim('\\'); //将路径中的分隔符修改为符合Windows目录的
                getJavaClassEntity.CopyCoverType = copyCoverType;
                getJavaClassEntity.ChangList = dtSelectInput;
                getJavaClassEntity.RelCodeClassList = dtRelCfgSelet;
                switch (copyCoverType)
                {
                    case CopyCoverTypeEnum.Cover:
                        break;
                    case CopyCoverTypeEnum.CoverNow:
                        getJavaClassEntity.CopyToPath = Path.Combine(getJavaClassEntity.CopyToPath,DateTime.Now.ToString("yyyy-MM-dd"));
                        break;
                    case CopyCoverTypeEnum.AwaysNew:
                        getJavaClassEntity.CopyToPath = Path.Combine(getJavaClassEntity.CopyToPath, DateTime.Now.ToString("yyyyMMdd-HHmmss"));
                        break;
                    default:
                        break;
                }
                // 创建生成目录
                if (Directory.Exists(getJavaClassEntity.CopyToPath))
                {
                    Directory.CreateDirectory(getJavaClassEntity.CopyToPath);
                }
                //查找并输出文件
                iFileNum = 0;
                tsbAutoSQL.Enabled = false;
                ShowDestopTipMsg("正在异步获取文件清单，请稍等一会...");
                //异步获取文件
                await Task.Run(() => GetFixedFile(sb, getJavaClassEntity));

                tsbAutoSQL.Enabled = true; //重置按钮为有效
                rtbString.AppendText(string.Format("复制成功 {0} 个文件！", _listFilePath.Count) + Environment.NewLine);
                rtbString.AppendText(sb.ToString());

                // 保存
                SaveCfg(false);
                if (string.IsNullOrEmpty(sSelectCfgId))
                {
                    cbbTemplateType.SelectedValue = sSelectCfgId;
                }
                
                if (iFileNum <= 0)
                {
                    ShowInfo("异步获取文件完成，没有修改的文件！");
                }
                else
                {
                    ShowInfo("异步获取文件完成，修改的文件数为：" + iFileNum.ToString());
                }
                tabControl1.SelectedTab = tpResult;
            }
            catch(Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 获取指定文件方法
        /// <summary>
        /// 获取指定文件方法
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="rootDirectory"></param>
        /// <param name="getJavaClassEntity"></param>
        private async void GetFixedFile(StringBuilder sb, GetJavaClassEntity getJavaClassEntity)
        {
            foreach (DataRow dr in getJavaClassEntity.ChangList.Rows)
            {
                // 查找源码文件是否存在
                string sRelFilePath = dr["file"].ToString().Replace("/","\\").Trim('\\');
                string sFullFilePath = Path.Combine(getJavaClassEntity.CodePath, sRelFilePath);
                if (!File.Exists(sFullFilePath))
                {
                    sb.AppendLine(sFullFilePath+"：源码文件不存在！");
                    continue;
                }

                string sClassFullPath = string.Empty;
                string sCoptyToFullPath = string.Empty;
                // 查找class文件是否存在
                foreach (DataRow drCfg in getJavaClassEntity.RelCodeClassList.Rows)
                {
                    string sCfgCodePath = drCfg[JavaPublishFileConfig.ValueString.RelCodeDir].ToString().Replace("/", "\\").Trim('\\');
                    string sCfgClassPath = drCfg[JavaPublishFileConfig.ValueString.RelClassDir].ToString().Replace("/", "\\").Trim('\\');
                    string sJavaClassFileName = string.Empty;
                    if (sRelFilePath.StartsWith(sCfgCodePath))
                    {
                        string sCfgClassEndPath = sRelFilePath.Replace(sCfgCodePath, "").Trim('\\');
                        if (sRelFilePath.ToLower().endsWith(".java"))
                        {
                            sCfgClassEndPath = sCfgClassEndPath.Replace(".java", ".class").Trim('\\');
                            sJavaClassFileName = sCfgClassEndPath.substring(sCfgClassEndPath.LastIndexOf("\\"), sCfgClassEndPath.LastIndexOf(".")).Trim('\\');
                        }
                        sClassFullPath = getJavaClassEntity.ClassPath + "\\" + sCfgClassPath + "\\" + sCfgClassEndPath;
                        sCoptyToFullPath = getJavaClassEntity.CopyToPath + "\\" + sCfgClassPath + "\\" + sCfgClassEndPath;
                        if (!File.Exists(sClassFullPath))
                        {
                            sb.AppendLine(sClassFullPath + "文件不存在！");
                            break;
                        }

                        string sCopyToDirParent = sCoptyToFullPath.Substring(0, sCoptyToFullPath.LastIndexOf("\\"));
                        if (!Directory.Exists(sCopyToDirParent))
                        {
                            Directory.CreateDirectory(sCopyToDirParent);
                        }

                        File.Copy(sClassFullPath, sCoptyToFullPath, true);
                        sb.AppendLine(sCoptyToFullPath + "：复制成功！");
                        iFileNum++;
                        _listFilePath.Add(sClassFullPath);

                        // 针对文件名$开头的文件也要复制
                        if (!string.IsNullOrEmpty(sJavaClassFileName))
                        {
                            string sClassParentPath = sClassFullPath.Substring(0, sClassFullPath.LastIndexOf("\\"));
                            string sNewClassPath = sCoptyToFullPath.Substring(0, sCoptyToFullPath.LastIndexOf("\\")).Trim('\\');
                            DirectoryInfo classDirectory = new DirectoryInfo(sClassParentPath);
                            //文件处理
                            foreach (FileInfo file in classDirectory.GetFiles())
                            {
                                if(file.Name.StartsWith(sJavaClassFileName+ "$"))
                                {
                                    string sNewPath = Path.Combine(sNewClassPath, file.Name);
                                    File.Copy(file.FullName, sNewPath, true);
                                    sb.AppendLine(sNewPath + "：复制成功！");
                                    iFileNum++;
                                    _listFilePath.Add(sNewPath);
                                }
                            }
                        }
                        
                        break;
                    }
                }
                if (string.IsNullOrEmpty(sClassFullPath))
                {
                    sb.AppendLine(sFullFilePath + "：对应的clss文件不存在！");
                    continue;
                }
            }            
        }
        #endregion

        /// <summary>
        /// 获取指定日期内代码变化的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetChangeFile_Click(object sender, EventArgs e)
        {
            if (dtpBegin.Value.CompareTo(dtpEnd.Value) > 0)
            {
                ShowErr("修改的开始时间不能大于结束时间！");
                return;
            }
            string sCodePath = txbCodePath.Text.Trim().Trim('\\');
            string sSelectCfgId = cbbTemplateType.SelectedValue == null ? "" : cbbTemplateType.SelectedValue.ToString();
            CopyCoverTypeEnum copyCoverType = (CopyCoverTypeEnum)int.Parse(cbbCopyType.SelectedValue.ToString());
            if (string.IsNullOrEmpty(sCodePath))
            {
                ShowErr("请选择【代码目录】！");
                return;
            }
            else
            {
                if (!Directory.Exists(sCodePath))
                {
                    ShowErr("【代码目录】不存在，请重新选择或录入！");
                    return;
                }
            }

            //获取文件
            DataTable dtInput = dgvInput.GetBindingTable();
            if (dtInput != null && dtInput.Rows.Count>0)
            {
                dtInput.Clear();
            }
            DirectoryInfo codeDirectory = new DirectoryInfo(sCodePath);
            string[] sExcludeFullDir = rtbExcludeRelateDir.Text.Trim().Replace("/", "\\").ToLower().Split(splitCharArr,StringSplitOptions.RemoveEmptyEntries); //得到排除的相对目录
            string[] sExcludeFullFile = rtbExcludeRelateFile.Text.Trim().Replace("/", "\\").ToLower().Split(splitCharArr, StringSplitOptions.RemoveEmptyEntries); //得到排除的相对文件
            GetDirectoryFile(codeDirectory, sExcludeFullDir, sCodePath, sExcludeFullFile);
        }

        #region 获取目录文件方法
        /// <summary>
        /// 获取目录文件方法
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="rootDirectory"></param>
        /// <param name="getJavaClassEntity"></param>
        private async void GetDirectoryFile(DirectoryInfo rootDirectory, string[] sExcludeFullDir, string sCodePath, string[] sExcludeFullFile)
        {
            //文件处理
            foreach (FileInfo file in rootDirectory.GetFiles()) 
            {
                getChangCodeFileList(file, sCodePath, sExcludeFullFile);
            }
            //普通目录的处理
            foreach (DirectoryInfo path in rootDirectory.GetDirectories())
            {
                if (path.Name.StartsWith("."))
                {
                    continue; //跳过点开头的系统目录
                }
                bool isSkip = false;
                foreach(string sRelDir in sExcludeFullDir)
                {
                    if(path.FullName.StartsWith(Path.Combine(sCodePath, sRelDir.Trim().Trim('\\'))))
                    {
                        isSkip = true;
                        break;
                    }
                }
                //跳过忽略的文件名、绝对路径文件名
                if (isSkip)
                {
                    continue;
                }
                // 递归查询其下目录
                GetDirectoryFile(path, sExcludeFullDir, sCodePath, sExcludeFullFile);
            }
        }
        #endregion

        /// <summary>
        /// 获取代码变化的列表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sb"></param>
        private void getChangCodeFileList(FileInfo file, string sCodePath, string[] sExcludeFullFile)
        {
            if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
            {
                return; //跳过系统文件
            }
            if (file.LastWriteTime < dtpBegin.Value || file.LastWriteTime > dtpEnd.Value)
            {
                return;  //不在修改时间范围内的文件跳过
            }
            if (file.Name.startsWith("."))
            {
                return;  //跳过.开头的文件
            }
            bool isSkip = false;
            foreach (string sRelDir in sExcludeFullFile)
            {
                if (file.FullName.Equals(Path.Combine(sCodePath, sRelDir.Trim().Trim('\\')),StringComparison.OrdinalIgnoreCase))
                {
                    isSkip = true;
                    break;
                }
            }
            //跳过忽略的文件名、绝对路径文件名
            if (isSkip)
            {
                return;
            }
            //文件加入到网格中
            DataTable dtInput = dgvInput.GetBindingTable();
            DataRow drNew = dtInput.NewRow();
            drNew["file"] = file.FullName.Replace(sCodePath,"").Trim('\\');
            drNew["ROWNO"] = dtInput.Rows.Count + 1;
            dtInput.Rows.Add(drNew);
        }

        #region 配置相关
        /// <summary>
        /// 配置选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbTemplateType.Text.Trim()))
            {
                txbCodePath.Text = string.Empty;
                txbCopyToPath.Text = string.Empty;
                txbClassPath.Text = string.Empty;
                rtbExcludeRelateDir.Clear();
                rtbExcludeRelateFile.Clear();
                DataTable dtCfg = dgvCodeClassRelConfig.GetBindingTable();
                if (dtCfg != null && dtCfg.Rows.Count > 0)

                {
                    dtCfg.Clear();
                }
                tabControl1.SelectedTab = tpSource;
                return;
            }

            if (cbbTemplateType.SelectedValue == null) return;
            string sTempType = cbbTemplateType.SelectedValue.ToString();
            txbReplaceTemplateName.Text = cbbTemplateType.Text;
            DataRow[] drArr = javaPublishFileConfig.MoreXmlConfig.KeyData.Select(sKeyId + "='" + sTempType + "'");
            if (drArr.Length > 0)
            {
                //代码目录
                txbCodePath.Text = drArr[0][JavaPublishFileConfig.KeyString.CodeDir].ToString();
                //class目录
                txbClassPath.Text = drArr[0][JavaPublishFileConfig.KeyString.ClassDir].ToString();
                //生成目录
                txbCopyToPath.Text = drArr[0][JavaPublishFileConfig.KeyString.CopyToDir].ToString();
                cbbCopyType.SelectedValue = drArr[0][JavaPublishFileConfig.KeyString.CopyCoverType].ToString();
                // 排除目录
                rtbExcludeRelateDir.Text = drArr[0][JavaPublishFileConfig.KeyString.ExcludeRelateDir].ToString();
                rtbExcludeRelateFile.Text = drArr[0][JavaPublishFileConfig.KeyString.ExcludeRelateFile].ToString();

                // 查询明细
                string sKeyId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
                DataRow[] drArrConfg = javaPublishFileConfig.MoreXmlConfig.ValData.Select(sKeyId + "='" + sTempType + "'");
                DataTable dtConfig = dgvCodeClassRelConfig.GetBindingTable().Clone();
                int i = 1;
                foreach (DataRow dr in drArrConfg)
                {
                    DataRow drNew = dtConfig.NewRow();
                    drNew[JavaPublishFileConfig.ValueString.RelCodeDir] = dr[JavaPublishFileConfig.ValueString.RelCodeDir];
                    drNew[JavaPublishFileConfig.ValueString.RelClassDir] = dr[JavaPublishFileConfig.ValueString.RelClassDir];
                    drNew["ROWNO"] = i;
                    dtConfig.Rows.Add(drNew);
                    i++;
                }
                dgvCodeClassRelConfig.BindDataGridView(dtConfig);
                dgvCodeClassRelConfig.AllowUserToAddRows = true;
                tabControl1.SelectedTab = tpSource;
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveReplaceTemplate_Click(object sender, EventArgs e)
        {
            SaveCfg();
        }

        private bool SaveCfg(bool isShowConfirm = true)
        {
            string sTempName = txbReplaceTemplateName.Text.Trim();

            if (string.IsNullOrEmpty(sTempName))
            {
                ShowInfo("配置名称不能为空！");
                return false;
            }
            DataTable dtCodeClass = dgvCodeClassRelConfig.GetBindingTable();
            dtCodeClass.DeleteNullRow();
            if (dtCodeClass.Rows.Count == 0)
            {
                ShowInfo("请录入变化的代码文件相对【读取目录】的相对路径！");
                return false;
            }

            if (isShowConfirm)
            {
                if (ShowOkCancel("确定要保存配置？") == DialogResult.Cancel) return false;
            }

            string sKeyId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = javaPublishFileConfig.MoreXmlConfig.KeyData;
            DataTable dtValConfig = javaPublishFileConfig.MoreXmlConfig.ValData;

            string sKeyIdNew = string.Empty;
            DataRow dr;
            bool isAdd = string.IsNullOrEmpty(cbbTemplateType.Text.Trim()) ? true : false;
            if (isAdd)
            {
                //新增
                sKeyIdNew = Guid.NewGuid().ToString();
                dr = dtKeyConfig.NewRow();
                dr[sKeyId] = sKeyIdNew;
                
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
                    dr = dtKeyConfig.NewRow();
                    dr[sKeyId] = sKeyIdNew;
                    dtKeyConfig.Rows.Add(dr);
                }
                else
                {
                    dr = drArrKey[0];
                }
                // 统一赋值
                dr[JavaPublishFileConfig.KeyString.Name] = sTempName;
                dr[JavaPublishFileConfig.KeyString.CodeDir] = txbCodePath.Text.Trim();
                dr[JavaPublishFileConfig.KeyString.ClassDir] = txbClassPath.Text.Trim();
                dr[JavaPublishFileConfig.KeyString.CopyToDir] = txbCopyToPath.Text.Trim();
                dr[JavaPublishFileConfig.KeyString.CopyCoverType] = cbbCopyType.SelectedValue.ToString();
                dr[JavaPublishFileConfig.KeyString.ExcludeRelateDir] = rtbExcludeRelateDir.Text.Trim().Replace("/","\\");
                dr[JavaPublishFileConfig.KeyString.ExcludeRelateFile] = rtbExcludeRelateFile.Text.Trim().Replace("/", "\\");

                if (drArrVal.Length > 0)
                {
                    foreach (DataRow drOld in drArrVal)
                    {
                        dtValConfig.Rows.Remove(drOld);
                    }
                    dtValConfig.AcceptChanges();
                }
            }

            foreach (DataRow drCode in dtCodeClass.Rows)
            {
                DataRow drNew = dtValConfig.NewRow();
                drNew[sValId] = Guid.NewGuid().ToString();
                drNew[sKeyId] = sKeyIdNew;
                drNew[JavaPublishFileConfig.ValueString.RelCodeDir] = drCode[JavaPublishFileConfig.ValueString.RelCodeDir].ToString();
                drNew[JavaPublishFileConfig.ValueString.RelClassDir] = drCode[JavaPublishFileConfig.ValueString.RelClassDir].ToString();
                dtValConfig.Rows.Add(drNew);
            }
            javaPublishFileConfig.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(javaPublishFileConfig.MoreXmlConfig.KeyData, sKeyId, JavaPublishFileConfig.KeyString.Name, true, true);
            if (isShowConfirm)
            {
                ShowInfo("配置保存成功！"); 
            }
            return true;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (cbbTemplateType.SelectedValue == null)
            {
                ShowInfo("请选择一个配置！");
                return;
            }
            string sKeyIDValue = cbbTemplateType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue) || string.IsNullOrEmpty(cbbTemplateType.Text.Trim()))
            {
                ShowInfo("请选择一个配置！");
                return;
            }

            if (ShowOkCancel("确定要删除该配置？") == DialogResult.Cancel) return;

            string sKeyId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.KeyIdPropName;
            string sValId = javaPublishFileConfig.MoreXmlConfig.MoreKeyValue.ValIdPropName;
            DataTable dtKeyConfig = javaPublishFileConfig.MoreXmlConfig.KeyData;
            DataTable dtValConfig = javaPublishFileConfig.MoreXmlConfig.ValData;
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
            javaPublishFileConfig.MoreXmlConfig.Save();
            //重新绑定下拉框
            cbbTemplateType.BindDropDownList(javaPublishFileConfig.MoreXmlConfig.KeyData, sKeyId, JavaPublishFileConfig.KeyString.Name, true, true);
            ShowInfo("配置删除成功！");
        } 
        #endregion

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            tsbAutoSQL.PerformClick();
        }
        
        #region 路径按钮事件
        private void SelectFilePath(TextBox tb)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tb.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 读取路径按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadPath_Click(object sender, EventArgs e)
        {
            SelectFilePath(txbCodePath);
        }

        /// <summary>
        /// class路径按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClassPath_Click(object sender, EventArgs e)
        {
            SelectFilePath(txbClassPath);
        }

        /// <summary>
        /// 复制到路径按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTargetPath_Click(object sender, EventArgs e)
        {
            SelectFilePath(txbCopyToPath);
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 网格头双击事件
        private void dgvInput_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvInput, ref _allSelectFile, e);
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

        private void dgvCodeClassRelConfig_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAllOrCancel(dgvCodeClassRelConfig, ref _allSelectCodeClass, e);
        }

        private void dgvInput_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvInput.Columns[e.ColumnIndex].Name == _sGridColumnSelect)
            {
                return;
            }
        }
        private void dgvCodeClassRelConfig_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.dgvCodeClassRelConfig.Columns[e.ColumnIndex].Name == _sGridColumnSelect)
            {
                return;
            }
        } 
        #endregion

        private void dgvInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                PasteTextFromClipse();
            }
        }

        private void PasteTextFromClipse()
        {
            try
            {
                string pasteText = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                {
                    return;
                }

                DataTable dtMain = dgvInput.GetBindingTable();
                if (!ckbIsPasteAppend.Checked && dtMain.Rows.Count > 0)
                {
                    dtMain.Clear();
                }
                foreach (DataRow dr in dtMain.Select("file is null or file=''"))
                {
                    dtMain.Rows.Remove(dr);
                }

                DataTable dtNew = dtMain.Copy();
                pasteText.GetFirstColumnTable(dtNew, true, false, false,true,"ROWNO", "file",true);
                dgvInput.BindDataGridView(dtNew, true);
                dgvInput.ShowRowNum(true); //显示行号
                dgvInput.AllowUserToAddRows = true;
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            dgvInput.GetBindingTable().Clear();
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            PasteTextFromClipse();
        }

        private void tsmiCfgClear_Click(object sender, EventArgs e)
        {
            dgvCodeClassRelConfig.GetBindingTable().Clear();
        }

        private void dgvCodeClassRelConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                PasteCfgTextFromClipse();
            }
        }

        private void PasteCfgTextFromClipse()
        {
            try
            {
                string pasteText = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(pasteText))//包括IN的为生成的SQL，不用粘贴
                {
                    return;
                }

                DataTable dtMain = dgvCodeClassRelConfig.GetBindingTable();
                if (!ckbIsPasteAppend.Checked && dtMain.Rows.Count > 0)
                {
                    dtMain.Clear();
                }
                string sFilter = string.Format("{0} is null or {0}='' or {1} is null or {1}=''",JavaPublishFileConfig.ValueString.RelCodeDir,JavaPublishFileConfig.ValueString.RelClassDir);
                foreach (DataRow dr in dtMain.Select(sFilter))
                {
                    dtMain.Rows.Remove(dr);
                }

                DataTable dtNew = dtMain.Copy();
                // 粘贴字符
                pasteText.GetStringTable(dtNew,new string[] { JavaPublishFileConfig.ValueString.RelCodeDir, JavaPublishFileConfig.ValueString.RelClassDir },false,true);
                dgvCodeClassRelConfig.BindDataGridView(dtNew, true);
                dgvCodeClassRelConfig.ShowRowNum(true); //显示行号
                dgvCodeClassRelConfig.AllowUserToAddRows = true;
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }

        private void tsmiCfgPaste_Click(object sender, EventArgs e)
        {
            PasteCfgTextFromClipse();
        }

        
    }
}
