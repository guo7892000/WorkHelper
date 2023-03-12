using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Breezee.Core.Tool;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 附件控件
    /// </summary>
    public partial class AttachmentItem : UserControl
    {
        #region 变量属性
        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        bool _isShowDelete = true;
        public bool IsShowDelete
        {
            get { return _isShowDelete; }
            set { _isShowDelete = value; }
        }

        /// <summary>
        /// 是否显示下载按钮
        /// </summary>
        bool _isShowDownload = true;
        public bool IsShowDownload
        {
            get { return _isShowDownload; }
            set { _isShowDownload = value; }
        }

        /// <summary>
        /// 上传的ID号
        /// </summary>
        private string _UploadID;

        public string UploadID
        {
            get { return _UploadID; }
            set { _UploadID = value; }
        }

        /// <summary>
        /// 上传后文件名
        /// </summary>
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        private string _fileSize;

        public string FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        ///// <summary>
        ///// 文件完整路径
        ///// </summary>
        //private string _fileUploadFullPath;

        //public string FileUploadFullPath
        //{
        //    get { return _fileUploadFullPath; }
        //    set { _fileUploadFullPath = value; }
        //}

        /// <summary>
        /// 文件相对路径
        /// </summary>
        private string _fileUploadPath;

        public string FileUploadPath
        {
            get { return _fileUploadPath; }
            set { _fileUploadPath = value; }
        }

        /// <summary>
        /// 服务器配置名称
        /// </summary>
        private string _ftpServerConfigName;

        public string FtpServerConfigName
        {
            get { return _ftpServerConfigName; }
            set { _ftpServerConfigName = value; }
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        private string _fileType;

        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        #endregion

        #region 构造函数
        public AttachmentItem(string filename, string filesize, string ftpServerConfigName,bool isShowDelete,bool isShowDownload)
        {
            InitializeComponent();
            _fileName = filename;
            _fileSize = filesize;
            _ftpServerConfigName = ftpServerConfigName;
            _isShowDelete = isShowDelete;
            _isShowDownload = isShowDownload;
        } 
        #endregion

        #region 加载事件
        private void AttachmentItem_Load(object sender, EventArgs e)
        {
            this.Width = 150;
            this.Height = 32;
            this.Cursor = Cursors.Hand;

            pic.Width = pic.Height = 32;

            lblFileName.Top = 3;
            lblFileName.Left = 35;
            lblFileName.Text = _fileName;
            lblFileName.AutoSize = true;
            lblFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            lblFileSize.Text = _fileSize;
            lblFileSize.Left = 35;
            lblFileSize.Top = 18;
            lblFileSize.AutoSize = true;
            lblFileSize.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            #region 删除和下载链接是否显示
            if (_isShowDelete)
            {
                lblDelete.Visible = true;
            }
            else
            {
                lblDelete.Visible = false;
            }

            if (_isShowDownload)
            {
                lblDownload.Visible = true;
            }
            else
            {
                lblDownload.Visible = false;
            } 
            #endregion

            //设置鼠标进入事件
            this.MouseEnter += highlightWhenEnter;
            pic.MouseEnter += highlightWhenEnter;
            lblFileName.MouseEnter += highlightWhenEnter;
            lblFileSize.MouseEnter += highlightWhenEnter;
            //设置鼠标离开事件
            this.MouseLeave += recoveryWhenLeave;
            pic.MouseLeave += recoveryWhenLeave;
            lblFileName.MouseLeave += recoveryWhenLeave;
            lblFileSize.MouseLeave += recoveryWhenLeave;
            //设置鼠标停留在控件上的事件
            this.MouseHover += highlightWhenHover;
            pic.MouseHover += highlightWhenHover;
            lblFileName.MouseHover += highlightWhenHover;
            lblFileSize.MouseHover += highlightWhenHover;            
        } 
        #endregion

        #region 鼠标离开后还原
        private void recoveryWhenLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            lblFileName.ForeColor = lblFileSize.ForeColor = SystemColors.ControlText;
        } 
        #endregion

        #region 鼠标经过时高亮
        private void highlightWhenEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 160, 230);
            lblFileName.ForeColor = lblFileSize.ForeColor = Color.White;
        } 
        #endregion

        #region 鼠标经过时高亮
        private void highlightWhenHover(object sender, EventArgs e)
        {
            //设置显示文件全名
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this, _fileName);
        }
        #endregion

        #region 删除按钮事件
        private void lblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                #region 删除文件
                if (this.Parent.Parent is AttachmentList)
                {
                    AttachmentList attachList = (AttachmentList)this.Parent.Parent;
                    attachList.AttachList.Remove(this);//集合移除附件
                }
                this.Parent.Controls.Remove(this);//控件移除附件
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No such file"))
                {
                    MessageBox.Show("文件不存在！", "删除失败");
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "删除失败");
                }
            }
        } 
        #endregion

        #region 下载链接事件
        private void llbDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            #region 下载文件
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择文件保存的位置";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string directory = dlg.SelectedPath.TrimEnd('\\') + "\\";//保存目录
            if (System.IO.File.Exists(directory.TrimEnd('\\') + @"\" + _fileName))
            {
                var result = MessageBox.Show(string.Format("文件 {0} 在本地已经存在，是否覆盖？", _fileName), "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            try
            {
                FtpServerHelper ftpHelper = new FtpServerHelper(_ftpServerConfigName);
                bool bSave = ftpHelper.Download(directory, _fileName, _fileName);
                if (bSave)
                {
                    MessageBox.Show("下载完成", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No such file"))
                {
                    MessageBox.Show("文件不存在！", "下载失败");
                    return;
                }
            }
            
            #endregion
        } 
        #endregion
    }
}
