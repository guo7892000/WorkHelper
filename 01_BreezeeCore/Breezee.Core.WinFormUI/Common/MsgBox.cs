using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Timer = System.Windows.Forms.Timer;

/***************************************
 * 对象名称：弹出对话框窗体
 * 对象类别：窗体层
 * 创建作者：黄国辉
 * 创建日期：2014-7-25
 * 对象说明：主要提供弹出对话框窗体
 * 修改历史：
 *      V1.0 新建 hgh 2014-7-25
 * ************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 弹出对话框窗体
    /// </summary>
    public partial class MsgBox : Form
    {
        #region 变量
        //按钮出现的三个位置
        private static int[] A_loc = { 122, 4 };
        private static int[] B_loc = { 186, 4 };
        private static int[] C_loc = { 250, 4 };
        
        //窗体出现效果
        private const int AW_HOR_POSITIVE = 0x00000001;    //自左向右显示窗体
        private const int AW_HOR_NEGATIVE = 0x00000002;    //自右向左显示窗体
        private const int AW_VER_POSITIVE = 0x00000004;    //自上而下显示窗体
        private const int AW_VER_NEGATIVE = 0x00000008;    //自下而上显示窗体
        private const int AW_CENTER = 0x00000010;          //窗体向外扩展
        private const int AW_HIDE = 0x00010000;            //隐藏窗体
        private const int AW_ACTIVATE = 0x00020000;        //激活窗体
        private const int AW_SLIDE = 0x00040000;           //使用滚动动画类型
        private const int AW_BLEND = 0x00080000;           //使用淡入效果
        //声明AnimateWindow函数
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private string sShowType = "淡入窗体动画效果";
        #endregion

        #region 构造函数
        public MsgBox()
        {
            InitializeComponent();

            setSkin();
        }

        /// <summary>
        /// 设置背景
        /// </summary>
        private void setSkin()
        {
            //if (LocationPoint.skinIndex == 1)
            //{
            //    ImageFormClass.SetPanelBackImage(this.panelTop, Properties.Resources.skinG1);
            //}
            //else if (LocationPoint.skinIndex == 2)
            //{
            //    ImageFormClass.SetPanelBackImage(this.panelTop, Properties.Resources.skinG2);
            //}
            //else if (LocationPoint.skinIndex == 3)
            //{
            //    ImageFormClass.SetPanelBackImage(this.panelTop, Properties.Resources.skinG3);
            //}
        }
        #endregion

        #region 设置消息
        private string Message;
        private string error = string.Empty;
        private const int _maxWidth = 40;//每行最多字符设置
        private const int _row = 5;//最多行数

        private void SetLocation(Control ctl,int[] sizes)
        {
            ctl.Location = new Point(sizes[0],sizes[1]);
        }
        protected void SetTimeOutInfo(int msgtimeout)
        {
            timeout = msgtimeout;
        }
       
        /// <summary>
        /// 内容信息过长，自动换行,屏蔽，采用label自动换行。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string getMessage(string message)
        {
            Char[] newC = message.ToCharArray();
            int wid = 1;
            int row = 1;
            string result = string.Empty;
            for (int i = 0; i < newC.Length; i++)
            {
                string sNow = newC[i].ToString();

                #region 针对字符中已包括换行的处理
                if (sNow.Equals("\n"))
                {
                    wid = 1;
                    row += 1;
                    result += "\r\n";
                    continue;
                }
                else if (sNow.Equals("\r"))
                {
                    continue;
                } 
                #endregion

                if (row >= _row && wid >= _maxWidth - 6)
                {
                    result += "...";
                    IsMore = true;
                    return result;
                }
                if (wid >= _maxWidth)
                {
                    wid = 1;
                    row += 1;
                    result += "\r\n";
                }
                result += sNow;
                if (newC[i] > 0x255)
                {
                    wid += 2;
                }
                else
                {
                    wid++;
                }
            }
            IsMore = false;
            return result;
        }

        public void SetInformation(string message, string title = "")
        {
            this.Text = title;
            if (string.IsNullOrEmpty(title))
                this.Text = "信息提示";
            this.lblTitle.Text = title;
            Message = message;
            this.lblMessage.Text = getMessage(message);
            this.btnAbort.Visible = false;
            this.btnCancel.Visible = false;
            this.btnConfirm.Visible = false;
            this.btnIgnore.Visible = false;
            this.btnNo.Visible = false;
            this.btnRetry.Visible = false;
            this.btnYes.Visible = false;
            this.btnErrorInfo.Visible = false;
        }

        protected void SetInformation(string message, string title, string error)
        {
            this.Text = title;
            if (string.IsNullOrEmpty(title))
                this.Text = "信息提示";
            this.Message = message;
            this.lblTitle.Text = title;
            this.btnAbort.Visible = false;
            this.btnCancel.Visible = false;
            this.btnConfirm.Visible = false;
            this.btnIgnore.Visible = false;
            this.btnNo.Visible = false;
            this.btnRetry.Visible = false;
            this.btnYes.Visible = false;
            this.btnErrorInfo.Visible = false;
            this.lblMessage.Text = getMessage(message);
            this.error = error;
            Txt_ErrorInfo.Text = error;
        }

        protected void SetBtn(MyButtons myButtons)
        {
            switch (myButtons)
            { 
                case MyButtons.OK:
                    this.btnConfirm.Visible = true;
                    SetLocation(this.btnConfirm,C_loc);
                    break;
                case MyButtons.OKMore:
                    this.btnConfirm.Visible = true;
                    this.btnErrorInfo.Visible = true;
                    SetLocation(this.btnConfirm, B_loc);
                    SetLocation(this.btnErrorInfo, C_loc);
                    break;
                case MyButtons.OKError:
                    this.btnConfirm.Visible = true;
                    this.btnErrorInfo.Visible = true;
                    SetLocation(this.btnConfirm, B_loc);
                    SetLocation(this.btnErrorInfo, C_loc);
                    break;
                case MyButtons.OKCancel:
                    this.btnConfirm.Visible = true;
                    this.btnCancel.Visible = true;
                    SetLocation(this.btnConfirm, B_loc);
                    SetLocation(this.btnCancel, C_loc);
                    break;
                case MyButtons.OKCancelMore:
                    this.btnConfirm.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnErrorInfo.Visible = true;
                    SetLocation(this.btnConfirm, A_loc);
                    SetLocation(this.btnCancel, B_loc);
                    SetLocation(this.btnErrorInfo, C_loc);
                    break;
                case MyButtons.YesNo:
                    this.btnYes.Visible = true;
                    this.btnNo.Visible = true;
                    SetLocation(this.btnYes, B_loc);
                    SetLocation(this.btnNo, C_loc);
                    break;
                case MyButtons.YesNoCancel:
                    this.btnYes.Visible = true;
                    this.btnNo.Visible = true;
                    this.btnRetry.Visible = true;
                    SetLocation(this.btnYes, A_loc);
                    SetLocation(this.btnNo, B_loc);
                    SetLocation(this.btnRetry, C_loc);
                    break;
                case MyButtons.RetryCancel:
                    this.btnCancel.Visible = true;
                    this.btnRetry.Visible = true;
                    SetLocation(this.btnRetry, B_loc);
                    SetLocation(this.btnCancel, C_loc);
                    break;
                case MyButtons.AbortRetryIgnore:
                    this.btnAbort.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnRetry.Visible = true;
                    SetLocation(this.btnRetry, A_loc);
                    SetLocation(this.btnAbort, B_loc);
                    SetLocation(this.btnCancel, C_loc);
                    break;
            }
        }

        protected void setPicIcon(MyIcon myIcon)
        {
            switch (myIcon)
            {
                case MyIcon.Error:
                    this.picIcon.Image = Properties.Resources.MsgBoxError;
                    break;
                case MyIcon.Information:
                    this.picIcon.Image = Properties.Resources.MsgBoxInformation;
                    break;
                case MyIcon.Question:
                    this.picIcon.Image = Properties.Resources.MsgBoxQuestion;
                    break;
                case MyIcon.None:
                    this.picIcon.Image = null;
                    break;
            }
            this.picIcon.SizeMode = PictureBoxSizeMode.CenterImage;
        }
        #endregion

        #region 按钮单击事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void btnErrorInfo_Click(object sender, EventArgs e)
        {
            if (panelErrorInfo.Visible)
            {
                panelErrorInfo.Visible = false;
                this.Height = 125;
            }
            else
            {
                panelErrorInfo.Visible = true;
                if (string.IsNullOrEmpty(this.error))
                {
                    Txt_ErrorInfo.Text = Message;
                }
                else
                {
                    Txt_ErrorInfo.Text = this.error;
                }
                this.Height = 250;
            }
        }
        #endregion

        #region 加载事件
        private void MDS_FrmMsgBox_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(ScreenWidth / 2 - this.Width / 2, ScreenHeight / 2 - 60);
            //
            lblMessage.MouseEnter += lblMessage_MouseEnter;
            btnMore.Click += btnMore_Click;

            panelErrorInfo.Visible = false;
            //设置对话框高度：NET 4.8中设置为125，NET 6以上中设置为172
            this.Height = "4".Equals(WinFormContext.Instance.NetVersion) ? 125 : 172;

            #region 设置效果
            switch (sShowType)
            {
                case "自左向右滚动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_HOR_POSITIVE);
                    break;
                case "自左向右滑动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_SLIDE + AW_HOR_POSITIVE);
                    break;
                case "自右向左滚动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_HOR_NEGATIVE);
                    break;
                case "自右向左滑动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_SLIDE + AW_HOR_NEGATIVE);
                    break;
                case "自上向下滚动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_VER_POSITIVE);
                    break;
                case "自上向下滑动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_SLIDE + AW_VER_POSITIVE);
                    break;
                case "自下向上滚动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_VER_NEGATIVE);
                    break;
                case "自下向上滑动窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_SLIDE + AW_VER_NEGATIVE);
                    break;
                case "向外扩展窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_SLIDE + AW_CENTER);
                    break;
                case "淡入窗体动画效果":
                    AnimateWindow(this.Handle, 500, AW_BLEND);
                    break;
            } 
            #endregion

            if (timeout > 0)
            {
                StartTimer(timeout);
                labTimer.Visible = true;
                labTimer.Text = (timeout / 1000).ToString() + "秒";
            }
        }

        private void MDS_FrmMsgBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 500, AW_CENTER | AW_HIDE | AW_BLEND);
        }

        private Point mPoint = new Point();
        private void MDS_FrmMsgBox_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void MDS_FrmMsgBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }
        #endregion

        #region 记时器
        private int timeout = 0;

        private void StartTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeout <= 0)
            {
                //停止计时器
                ((Timer)sender).Enabled = false;
                this.Close();
            }
            else
            {
                timeout = timeout - ((Timer)sender).Interval;
                labTimer.Text = (timeout / 1000).ToString() + "秒";
            }
        }
        #endregion

        #region 写日志
        private static void WriteExceptLog(string msg)
        {
            try
            {
                string pathName = Application.StartupPath + @"\log\Exception\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (File.Exists(pathName))
                {
                }
                else
                {
                    File.Create(pathName);//创建该文件
                }
                StreamWriter sw = new StreamWriter(pathName, true);
                sw.WriteLine("时间" + "：" + DateTime.Now.ToString() + "     " + "异常信息：" + msg);
                sw.WriteLine("");
                sw.Close();
                return;
            }
            catch
            { }
        }
        #endregion

        #region 更多信息按钮事件
        private bool IsMore = false;
        private void lblMessage_MouseEnter(object sender, EventArgs e)
        {
            if (IsMore)
            {
                this.btnMore.Visible = true;
            }
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if (panelErrorInfo.Visible)
            {
                panelErrorInfo.Visible = false;
                this.Height = 125;
            }
            else
            {
                panelErrorInfo.Visible = true;
                Txt_ErrorInfo.Text = Message;
                this.Height = 250;
            }
        }
        #endregion

        #region 显示信息静态公共方法
        public static DialogResult ShowException(Exception e)
        {
            string msg = GetMsgFormException(e);
            return ShowException(msg);
        }

        public static DialogResult ShowException(string strException)
        {
            return ShowExceptionInternal("系统出现异常，请联系管理员！" + Environment.NewLine, strException);
        }

        public static DialogResult ShowException(string message, Exception ex)
        {
            string strException = GetMsgFormException(ex);
            return ShowExceptionInternal(message, strException);
        }

        private static DialogResult ShowExceptionInternal(string message, string strException)
        {
            MsgBox mmbox = new MsgBox();
            mmbox.error = strException;
            mmbox.SetInformation(message, "信息提示", strException);
            mmbox.SetBtn(MyButtons.OKError);
            mmbox.setPicIcon(MyIcon.Error);
            //写日志
            //LogHelper.Error(message + "。\n"+strException);
            //显示对话框
            mmbox.ShowDialog();
            return mmbox.DialogResult;
        }

        private static string GetMsgFormException(Exception ex)
        {
            string msg = "";
            StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            if (trace != null && trace.FrameCount > 0)
            {
                msg = "文件位置:" + trace.GetFrame(0).GetFileName() + ";\r\n行号:" + trace.GetFrame(0).GetFileLineNumber().ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + ";\r\n类名:" + trace.GetFrame(0).GetMethod().DeclaringType.FullName + ";\r\n方法名：" + trace.GetFrame(0).GetMethod().Name + ";\r\n";
            }
            msg += ex.Message;
            return msg;
        }

        public static DialogResult Show(IDictionary<string, string> idic, string title = "温馨提示", string longMsg = "")
        {
            MsgBox mmbox = new MsgBox();
            if (idic.ContainsKey(StaticConstant.FRA_EXCEPTION))
            {
                mmbox.SetInformation(idic[StaticConstant.FRA_USER_MSG], title, idic[StaticConstant.FRA_EXCEPTION]);
                mmbox.SetBtn(MyButtons.OKError);
                mmbox.setPicIcon(MyIcon.Error);
                WriteExceptLog(idic[StaticConstant.FRA_EXCEPTION]);
            }
            else
            {
                mmbox.SetInformation(idic[StaticConstant.FRA_USER_MSG], title);
                mmbox.SetBtn(MyButtons.OK);
                mmbox.setPicIcon(MyIcon.Information);
            }
            if (longMsg != "")
            {
                mmbox.Message = longMsg;
                mmbox.SetBtn(MyButtons.OKMore);
            }
            mmbox.ShowDialog();
            return mmbox.DialogResult;
        }

        public static DialogResult Show(string message, string title = "温馨提示",string longMsg="")
        {
            return Show(message, title, MyButtons.OK, MyIcon.Information, longMsg);
        }

        public static DialogResult Show(int msg_timeout,string message, string title = "信息提示",string longMsg="")
        {
            return Show(message, title, MyButtons.OK, MyIcon.Information, longMsg, msg_timeout);
        }

        public static DialogResult Show(string message, string title, MyButtons myButtons,string longMsg="")
        {
            return Show(message, title, myButtons, MyIcon.Information, longMsg);
        }

        public static DialogResult Show(string message, string title, MyButtons myButtons, MyIcon myIcon, string longMsg = "", int msg_timeout = 0)
        {
            MsgBox mmbox = new MsgBox();
            mmbox.SetInformation(message, title);
            mmbox.SetBtn(myButtons);
            mmbox.setPicIcon(myIcon);
            if (msg_timeout > 0)
            {
                mmbox.SetTimeOutInfo(msg_timeout);
            }

            if (longMsg != "")
            {
                mmbox.Message = longMsg;
                mmbox.SetBtn(MyButtons.OKMore);
            }
            mmbox.ShowDialog();
            return mmbox.DialogResult;
        }
        #endregion

        #region 复制右键菜单事件
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Message);
        } 
        #endregion
    }

    #region 枚举
    public enum MyButtons
    {
        OK,
        OKMore,
        OKCancelMore,
        OKError,
        OKCancel,
        YesNo,
        YesNoCancel,
        AbortRetryIgnore,
        RetryCancel
    }
    //需要在资源中添加三张图片，以显示不同对话框的提示图片
    public enum MyIcon
    {
        Error,
        Information,
        Question,
        None
    }
    #endregion

    #region 图像窗体类
    public static class ImageFormClass
    { /// <summary>
        /// 背景赋值
        /// </summary>
        /// <param name="torl"></param>
        /// <param name="bmap"></param>
        /// <param name="sizes"></param>
        public static void SetPanelBackImage(Control torl, Bitmap bmap)
        {
            torl.BackgroundImage = bmap;
            torl.BackgroundImageLayout = ImageLayout.Stretch;
        }
    } 
    #endregion
}
