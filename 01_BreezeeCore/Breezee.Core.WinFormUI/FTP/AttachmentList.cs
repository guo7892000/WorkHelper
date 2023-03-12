using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.Core.Interface;

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
    /// 附件清单
    /// </summary>
    public partial class AttachmentList : UserControl
    {
        #region 变量属性
        //一行显示的附件数量
        int _PerRowNum = 3;

        public int PerRowNum
        {
            get { return _PerRowNum; }
            set { _PerRowNum = value; }
        }

        //上传文件
        FtpServerHelper _FtpServer = new FtpServerHelper();

        public FtpServerHelper FtpServer
        {
            get { return _FtpServer; }
            set { _FtpServer = value; }
        }

        SystemModelEnum _SystemModel = SystemModelEnum.NULL;

        public SystemModelEnum SystemModel
        {
            get { return _SystemModel; }
            set { _SystemModel = value; }
        }

        IList<AttachmentItem> _AttachList = new List<AttachmentItem>();

        public IList<AttachmentItem> AttachList
        {
            get { return _AttachList; }
        }
        #endregion

        #region 默认构造函数
        public AttachmentList()
        {
            InitializeComponent();
        } 
        #endregion

        #region 增加附件方法
        /// <summary>
        /// 增加附件方法
        /// </summary>
        /// <param name="strFileFullPath"></param>
        /// <param name="name"></param>
        /// <param name="fileSize"></param>
        /// <param name="fizeExtension"></param>
        /// <param name="IsUpload">是否上传</param>
        /// <param name="isShowDelete">是否显示删除链接</param>
        /// <param name="isShowDownload">是否显示下载链接</param>
        /// <returns></returns>
        public AttachmentItem AddAttach(string strFileFullPath, string name, string fileSize, string fizeExtension, string ftpServerConfigName,bool IsUpload, bool isShowDelete, bool isShowDownload)
        {
            AttachmentItem Attach = new AttachmentItem(name, fileSize,ftpServerConfigName, isShowDelete, isShowDownload);
            if (IsUpload)
            {
                //上传文件
                string UpURL = _FtpServer.Upload(strFileFullPath, _SystemModel);
                //相对路径为去掉服务器IP
                Attach.FileUploadPath = UpURL.Replace(_FtpServer.FtpServer.ftpURI,"");
            }
            
            Attach.UploadID = Guid.NewGuid().ToString();
            Attach.FileName = name;
            //Attach.FileUploadFullPath = _FtpServer.FtpServer.ftpURI + name;
            
            Attach.FileType = fizeExtension;
            Attach.FileSize = fileSize;
            pnlAttachList.Controls.Add(Attach);
            //附件清单集合
            _AttachList.Add(Attach);
            return Attach;
        } 
        #endregion

        #region 重新设置附件布局方法
        public void ResetAttachLayout()
        {
            //控件宽度
            int control_width = (pnlAttachList.Width - _PerRowNum * 15) / _PerRowNum;
            int i = 0;
            foreach (Control ctr in pnlAttachList.Controls)
            {
                ctr.Width = control_width;
                ctr.Left = (i % _PerRowNum) * control_width + (i % _PerRowNum + 1) * 15;
                ctr.Top = (i / _PerRowNum) * 40 + 5;
                i++;
            }
            
        } 
        #endregion

        #region 清除所有附件方法
        public void CleaAllAttach()
        {
            pnlAttachList.Controls.Clear();
            if (_AttachList.Count > 0)
            {
                _AttachList.Clear();
            }
        }
        #endregion

        #region 清除所有附件方法
        public ControlCollection GetAllAttach()
        {
            return pnlAttachList.Controls;
        }
        #endregion

        #region 加载事件
        private void AttachmentList_Load(object sender, EventArgs e)
        {
            pnlAttachList.AutoScroll = true;//自动增加滚动条
        } 
        #endregion
 
    }
}
