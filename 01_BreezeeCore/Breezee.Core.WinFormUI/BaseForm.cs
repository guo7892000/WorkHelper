using Breezee.Core.Entity;
using Breezee.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
    /// 类名：窗体基类
    /// 功能说明：为了以后统一窗体风格等
    /// 注：因为该类为其他所有窗体的基类，所以这里的加载方法不能出错或者有对象为空。
    ///     BaseForm的全局变量中不能有IOC对象，那样会导致子窗体显示错误。
    /// </summary>
    public partial class BaseForm : GlobalForm
    {
        //查询字典
        public IDictionary<string, string> _dicQuery = new Dictionary<string, string>();
        //字符字典
        public IDictionary<string, string> _dicString = new Dictionary<string, string>();
        public IDictionary<string, string> _dicQueryCondition = new Dictionary<string, string>();
        //对象字典
        public IDictionary<string, object> _dicObject = new Dictionary<string, object>();
        //保存字典
        public IDictionary<string, object> _dicSave = new Dictionary<string, object>();
        //控件与数据库列关系的集合
        public List<DBColumnControlRelation> _listControlColumn = new List<DBColumnControlRelation>();

        //登录用户
        public static LoginUserInfo _loginUser;//在登录时会赋值
        //显示提示全局信息事件
        public EventHandler<ShowGlobalMsgEventArgs> ShowGlobalMsg;
        public MainFormModelEnum MainFormMode = MainFormModelEnum.FullFunction;
        public string MenuName;
        //最后的提示信息
        public string LastestTipMsg;
        //弹出窗体是否可以最大最小及调整大小
        public bool ShowPopFormMaxBox = false;
        public bool ShowPopFormMinBox = false;
        public FormBorderStyle ShowPopFormBorderStyle = FormBorderStyle.FixedSingle;//边框样式，如为FormBorderStyle.Sizable则可以调整大小。
        //默认子窗体样式类型与样式值
        public static string ChildFormStyleType = "0";//默认颜色
        public static string ChildFormStyleValue = "216,246,255"; //浅蓝
        private WinFormConfig _WinFormConfig;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
            if (Parent == null)
            {
                StartPosition = FormStartPosition.CenterScreen;//不起作用的，必须在子窗体的构造函数中加上这一段才有效。
            }
        }
        #endregion

        #region 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        ///  注：当继承该类的子窗体显示不了，一般都是该加载事件的问题。
        ///  所以对该方法修改后，要试着打开任何一个子类看看是否能正常显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                ControlBox = true;
                MaximizeBox = ShowPopFormMaxBox;
                MinimizeBox = ShowPopFormMinBox;
                FormBorderStyle = ShowPopFormBorderStyle;
            }
            _WinFormConfig = WinFormContext.Instance.WinFormConfig;
            //使用辅助缓冲区重汇窗体
            DoubleBuffered = true;

            #region 如果窗体的可视化设计模式打不开，请将以下代码注释掉
            if (!DesignMode)
            {
                if (WinFormContext.DtUserEnviromentConfig != null) //这里如不加非空判断，会导致继承子窗体的设计窗体显示报错
                {
                    this.SetFormBackGroupStyle(WinFormContext.DtUserEnviromentConfig, "ChildFormSkin");
                }
                else
                {
                    this.SetFormBackGroupStyle(_WinFormConfig.Get(GlobalKey.CommonSkinType, ChildFormStyleType), _WinFormConfig.Get(GlobalKey.CommonSkinValue, ChildFormStyleValue));//设置子窗体样式
                }
            }
            #endregion
        }
        #endregion

        #region 窗体关闭前事件
        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (Control ctr in Controls)
                {
                    ctr.SaveDataGridViewColunmStyle();
                }
            }
            catch(Exception ex)
            {
                string strErr = ex.Message;
            }
        }
        #endregion

        #region 是否系统管理员
        public bool IsSystemAdminAccount()
        {
            if (_loginUser.USER_CODE.ToLower() != StaticConstant.FRA_SysteAdminUserName.ToLower())
            {
                return false;
            }
            return true;
        } 
        #endregion

        #region 构造字典

        #region 构造一个string,object类型的Dictionary
        /// <summary>
        /// 构造一个string,object类型的Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> CreateObjectDictionary(bool IsEnptyDic = false)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!IsEnptyDic)
            {
                SetPublicInfo(dic);
            }
            return dic;
        }
        #endregion

        #region 构造一个string,string类型的Dictionary
        /// <summary>
        /// 构造一个string,string类型的Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CreateStringDictionary(bool IsEnptyDic = false)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!IsEnptyDic)
            {
                SetPublicInfo(dic);
            }
            return dic;
        }
        #endregion

        #region 设置公共参数到Dictionary参数里
        /// <summary>
        /// 设置公共参数到Dictionary参数里
        /// </summary>
        /// <param name="dicSource"></param>
        /// <returns></returns>
        public Dictionary<string, string> SetPublicInfo(Dictionary<string, string> dicSource)
        {
            var _loginUser = WinFormContext.Instance.LoginUser;  //当前登录用户
            if (_loginUser.USER_TYPE != "1") //非全局管理员才加上组织ID
            {
                dicSource[DT_SYS_USER.ORG_ID] = _loginUser.ORG_ID;
            }
            dicSource[DT_SYS_USER.EMP_ID] = _loginUser.EMP_ID;
            dicSource[DT_ORG_EMPLOYEE.EMP_NAME] = _loginUser.EMP_NAME;
            dicSource[DT_SYS_USER.USER_ID] = _loginUser.USER_ID;
            dicSource[DT_SYS_USER.USER_NAME] = _loginUser.USER_NAME;
            dicSource[DT_SYS_USER.USER_TYPE] = _loginUser.USER_TYPE;
            return dicSource;
        }

        public Dictionary<string, object> SetPublicInfo(Dictionary<string, object> dicSource)
        {
            var _loginUser = WinFormContext.Instance.LoginUser;  //当前登录用户
            if (_loginUser.USER_TYPE != "1") //非全局管理员才加上组织ID
            {
                dicSource[DT_SYS_USER.ORG_ID] = _loginUser.ORG_ID;
            }
            dicSource[DT_SYS_USER.EMP_ID] = _loginUser.EMP_ID;
            dicSource[DT_ORG_EMPLOYEE.EMP_NAME] = _loginUser.EMP_NAME;
            dicSource[DT_SYS_USER.USER_ID] = _loginUser.USER_ID;
            dicSource[DT_SYS_USER.USER_NAME] = _loginUser.USER_NAME;
            dicSource[DT_SYS_USER.USER_TYPE] = _loginUser.USER_TYPE;
            return dicSource;
        }
        #endregion

        #endregion

        #region 显示消息
        public void ShowInfo(string Msg)
        {
            if(!IsGlobalShowMsg(Msg))
            {
                MsgHelper.ShowInfo(Msg);
            }
        }

        private bool IsGlobalShowMsg(string Msg)
        {
            if(ShowGlobalMsg == null)
            {
                return false;
            }
            if ((MainFormMode == MainFormModelEnum.FullFunction && WinFormContext.UserEnvConfig.SaveMsgPrompt == SaveMsgPromptTypeEnum.OnlyPromptNotPopup) 
                || (MainFormMode == MainFormModelEnum.Mini && "2".Equals(_WinFormConfig.Get(GlobalKey.SavePromptType, "2"))))
            {
                if(!string.IsNullOrEmpty(MenuName))
                {
                    Msg = string.Format("【{0}】功能提示：{1}", MenuName, Msg);
                }
                LastestTipMsg = Msg;
                ShowGlobalMsgEventArgs arg = new ShowGlobalMsgEventArgs(Msg);
                ShowGlobalMsg(this, arg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 显示桌面提示信息
        /// </summary>
        public void ShowDestopTipMsg(string Msg)
        {
            if (!string.IsNullOrEmpty(MenuName))
            {
                Msg = string.Format("【{0}】{1}", MenuName, Msg);
            }
            LastestTipMsg = Msg;
            ShowGlobalMsgEventArgs arg = new ShowGlobalMsgEventArgs(Msg);
            ShowGlobalMsg(this, arg);
        }

        public void ShowInfo(string Msg,string title)
        {
            if (!IsGlobalShowMsg(Msg))
            {
                MsgHelper.ShowInfo(Msg, title);
            }
        }

        public void ShowInfo(IDictionary<string, string> idic, string title = "信息提示", string longMsg = "")
        {
            if (!IsGlobalShowMsg(longMsg))
            {
                MsgHelper.ShowInfo(idic, title, longMsg);
            }
        }

        public void ShowInfo(string Msg, string title = "信息提示", string longMsg = "")
        {
            if (!IsGlobalShowMsg(longMsg))
            {
                MsgHelper.ShowInfo(Msg, title, longMsg);
            }
        }
        #endregion

        #region 显示错误
        public void ShowErr(Exception ex)
        {
            MsgHelper.ShowErr(ex);
        }

        public void ShowErr(string message, Exception ex)
        {
            MsgHelper.ShowErr(message, ex);
        }

        public void ShowErr(string Msg)
        {
            if (!IsGlobalShowMsg(Msg))
            {
                MsgHelper.ShowErr(Msg);
            }
        }

        public void ShowErr(string Msg, string title)
        {
            if (!IsGlobalShowMsg(Msg))
            {
                MsgHelper.ShowErr(Msg, title);
            }
        }
        #endregion

        #region 显示选择对话框
        public DialogResult ShowOkCancel(string Msg)
        {
            return MsgHelper.ShowOkCancel(Msg);
        }

        public DialogResult ShowYesNo(string Msg)
        {
            return MsgHelper.ShowYesNo(Msg);
        }

        public DialogResult ShowQuestion(string Msg, MyButtons btn)
        {
            return MsgHelper.ShowQuestion(Msg,btn); 
        }

        public DialogResult ShowQuestion(string Msg, string title, MyButtons btn)
        {
            return MsgHelper.ShowQuestion(Msg, title, btn);
        }
        #endregion

        #region 获取程序绝对路径方法
        /// <summary>
        /// 获取程序绝对路径方法
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public string GetSystemFullPath(string strPath)
        {
            return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, strPath);
        }
        #endregion 

        #region 重置控件值
        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="controls">可以是一个或者多个，还可以是容器控件</param>
        public void ResetControl(params Control[] controls)
        {
            foreach (var item in controls)
            {
                item.ResetControl();
            }
        }

        /// <summary>
        /// 重置控件的值
        /// </summary>
        /// <param name="controls">可以是一个或者多个，还可以是容器控件</param>
        public void SetControlsReadOnly(bool IsOnlyRead = true, params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.SetControlReadOnly(IsOnlyRead);
            }
        }
        #endregion

        #region 下载文件
        public void DownloadModel(string fileDirectory, string fileName, bool askOpen = true, SaveFileDialog saveDialog = null)
        {
            ExportHelper.DownloadModel(GetSystemFullPath(fileDirectory), fileName, askOpen, saveDialog);
        } 
        #endregion

        public static void ShowHtmlHelpPage(string sMenuName,string sHelpPath)
        {
            if (string.IsNullOrEmpty(sHelpPath)) return;
            FrmMenuHtmlHelp frm = new FrmMenuHtmlHelp();
            frm.SelectHelpPath = sHelpPath;
            frm.MenuName = sMenuName;
            frm.ShowDialog();
        }
    }



}
