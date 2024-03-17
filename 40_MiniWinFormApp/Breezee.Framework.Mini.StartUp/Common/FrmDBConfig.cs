using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Framework.Mini.Entity;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 数据库配置：保存在配置文件中
    /// </summary>
    public partial class FrmDBConfig : BaseForm,IMainCommonFormCross
    {
        #region 变量
        private DataRow _drEdit;
        public bool IsDbNameNotNull = true;//是否数据库名非空
        public bool IsFilterDbExtnedFile = true;//是否过滤db后缀名的数据库文件
        //控件集合字典
        List<DBColumnControlRelation> _listSupply = new List<DBColumnControlRelation>();
        //
        string _sKey;
        string _strConfigFilePath;
        string _sConfigFileName;
        string _sTitle;

        MiniXmlConfig _xmlCommon;
        DataTable dtXml;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sKey">数据库访问对象IDataAccess获取实例的键，这个由系统提定，不能修改</param>
        /// <param name="strConfigFilePath"></param>
        /// <param name="sConfigFileName"></param>
        public FrmDBConfig(string sKey,string strConfigFilePath, string sConfigFileName, string sTitle)
        {
            InitializeComponent();
            _sKey = sKey;
            _strConfigFilePath = strConfigFilePath;
            _sConfigFileName = sConfigFileName;
            _sTitle = sTitle;
        }
        #endregion

        #region 窗体加载事件
        private void FrmDBConfigSet_D_Load(object sender, EventArgs e)
        {
            Text = _sTitle;
            _xmlCommon = new MiniXmlConfig(_strConfigFilePath, _sConfigFileName, DbServerInfo.XmlAttrString.getList(), DbServerInfo.XmlAttrString.key, "xml", "dbConfig", XmlConfigSaveType.Element);
            //接口对象

            #region 绑定下拉框
            _dicQuery.Clear();
            string[] enumKey = Enum.GetNames(typeof(DataBaseType));
            int[] enumValue = new int[enumKey.Length];
            Enum.GetValues(typeof(DataBaseType)).CopyTo(enumValue, 0);
            for (int i = 0; i < enumKey.Length; i++)
            {
                if (!"NONE".Equals(enumKey[i], StringComparison.OrdinalIgnoreCase))
                {
                    _dicQuery.Add(enumValue[i].ToString(), enumKey[i]);
                }
            }

            //数据库类型
            DataTable dtDbType = _dicQuery.GetTextValueTable(false);
            cbbDatabaseType.BindTypeValueDropDownList(dtDbType, false, true);
            #endregion

            //设置控件关系
            SetControlColumnRelation();
            dtXml = _xmlCommon.Load();
            if (dtXml.Rows.Count > 0)
            {
                //解密密码
                string sEncPwd = dtXml.Rows[0][DbServerInfo.XmlAttrString.password].ToString();
                if (!string.IsNullOrEmpty(sEncPwd))
                {
                    try
                    {
                        dtXml.Rows[0][DbServerInfo.XmlAttrString.password] = EncryptHelper.AESDecrypt(sEncPwd, MiniGlobalValue.MiniDesEncryKey, MiniGlobalValue.MiniDesEncryVector);
                    }
                    catch
                    {
                        //报错时啥都不用做
                    }
                }
                //控件赋值
                _listSupply.SetControlValue(dtXml.Rows[0]);
                cbbDatabaseType.SetControlReadOnly(true);//为简单起见，这里只能使用SQLite数据库
            }
            else
            {
                cbbDatabaseType.SelectedValue = ((int)DataBaseType.SQLite).ToString();
                cbbDatabaseType.SetControlReadOnly(true);//为简单起见，这里只能使用SQLite数据库
            }
            txbDBConfigCode.Text = _sKey;
            txbDBConfigCode.ReadOnly = true;
        }
        #endregion

        #region 设置列名与控件关系
        private void SetControlColumnRelation()
        {
            //配置表
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.key, txbDBConfigCode, "key"));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.dbType, cbbDatabaseType, "数据库类型"));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.serverName, txbServerIP, "服务器IP"));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.portNo, txbPortNO));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.dataBase, txbDbName));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.schemaName, txbSchemaName));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.userName, txbUserName));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.password, txbPassword));
            _listSupply.Add(new DBColumnControlRelation(DbServerInfo.XmlAttrString.otherString, txbRemark));
        }
        #endregion

        #region 保存按钮事件
        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sErr;
                if(TestConnect(out sErr))
                {
                    string strInfo = _listSupply.JudgeNotNull(true);
                    if (!string.IsNullOrEmpty(strInfo))
                    {
                        ShowInfo("保存失败！\n" + strInfo);
                        return;
                    }
                    _listSupply.GetControlValue(dtXml, false);
                    //加密密码
                    string sEncPwd = dtXml.Rows[0][DbServerInfo.XmlAttrString.password].ToString();
                    if (!string.IsNullOrEmpty(sEncPwd))
                    {
                        dtXml.Rows[0][DbServerInfo.XmlAttrString.password] = EncryptHelper.AESEncrypt(sEncPwd, MiniGlobalValue.MiniDesEncryKey, MiniGlobalValue.MiniDesEncryVector);
                    }
                    _xmlCommon.Save(dtXml);
                    ShowInfo("保存成功，但需要重新登录才能生效！");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MsgBox.Show(sErr);
                }
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 退出按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 选择变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //重置控件
            ResetControl(txbServerIP, txbUserName, txbPassword, txbDbName, txbPortNO, txbSchemaName);
            //默认显示端口号
            lblPortNO.Visible = true;
            txbPortNO.Visible = true;
            //显示数据库
            lblDbName.Visible = true;
            txbDbName.Visible = true;
            //
            lblServerAddr.Text = "服务器地址：";
            toolTip1.SetToolTip(txbServerIP, "服务器地址");
            btnSelectDbFile.Visible = false;

            if (cbbDatabaseType.SelectedValue == null)
            {
                return;
            }

            int iDbType = int.Parse(cbbDatabaseType.SelectedValue.ToString());
            DataBaseType selectDBType = (DataBaseType)iDbType;

            switch (selectDBType)
            {
                case DataBaseType.SqlServer:
                    //显示登录类型
                    //lblLoginType.Visible = true;
                    //cbbLoginType.Visible = true;
                    //
                    txbServerIP.Text = "localhost";
                    //txbPortNO.Text = "1433";
                    break;
                case DataBaseType.Oracle:
                    lblServerAddr.Text = "数据源名称：";
                    toolTip1.SetToolTip(txbServerIP, "可以是TNS名称；\r\n或类似【//localhost:1521/orcl】；\r\n或TNS实际配置内容（不换行），如【(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))】");
                    //不显示端口号
                    lblPortNO.Visible = false;
                    txbPortNO.Visible = false;
                    //不显示数据库
                    lblDbName.Visible = false;
                    txbDbName.Visible = false;
                    break;
                case DataBaseType.MySql:
                    txbPortNO.Text = "3306";
                    break;
                case DataBaseType.SQLite:
                    lblServerAddr.Text = "数据库文件路径：";
                    toolTip1.SetToolTip(txbServerIP, "请选择SQLite文件");
                    btnSelectDbFile.Visible = true;
                    //不显示端口号
                    lblPortNO.Visible = false;
                    txbPortNO.Visible = false;
                    //不显示数据库
                    lblDbName.Visible = false;
                    txbDbName.Visible = false;

                    break;
                case DataBaseType.PostgreSql:
                    lblPortNO.Visible = true;
                    txbPortNO.Visible = true;
                    txbPortNO.Text = "5432";
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
                    //break;
            }
        }
        #endregion

        #region 选择数据库文件
        private void btnSelectDbFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (IsFilterDbExtnedFile)
            {
                ofd.Filter = "所有db文件|*.db";
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txbServerIP.Text = ofd.FileName;
                //txbServerIP.Text = ofd.SafeFileName;
            }
        }
        #endregion

        private void tsbConnetTest_Click(object sender, EventArgs e)
        {
            string sErr;
            TestConnect(out sErr);
            MsgBox.Show(sErr);
        }

        private bool TestConnect(out string msg)
        {
            try
            {
                var DbServer = new DbServerInfo()
                {
                    Database = txbDbName.Text.Trim(),
                    DatabaseType = (DataBaseType)int.Parse(cbbDatabaseType.SelectedValue.ToString()),
                    Password = txbPassword.Text.Trim(),
                    PortNo = txbPortNO.Text.Trim(),
                    SchemaName = txbSchemaName.Text.Trim(),
                    ServerName = txbServerIP.Text.Trim(),
                    UserName = txbUserName.Text.Trim(),
                    UseConnString = false,
                    ConnString = "",
                };
                DbServerInfo.ResetConnKey(DbServer);

                int iDbType = int.Parse(cbbDatabaseType.SelectedValue.ToString());
                DataBaseType selectDBType = (DataBaseType)iDbType;

                if (selectDBType != DataBaseType.Oracle && selectDBType != DataBaseType.SQLite)
                {
                    if (IsDbNameNotNull && string.IsNullOrEmpty(DbServer.Database))
                    {
                        msg = "数据库名称不能为空！";
                        return false;
                    }
                }

                switch (selectDBType)
                {
                    case DataBaseType.SqlServer:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            msg = "服务器地址不能为空！";
                            return false;
                        }
                        break;
                    case DataBaseType.Oracle:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            msg = "TNS名称不能为空！";
                            return false;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            msg = "用户名和密码都不能为空！";
                            return false;
                        }
                        break;
                    case DataBaseType.MySql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            msg = "服务器地址不能为空！";
                            return false;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            msg = "用户名和密码都不能为空！";
                            return false;
                        }
                        break;
                    case DataBaseType.SQLite:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            msg = "数据库文件路径不能为空！";
                            return false;
                        }
                        break;
                    case DataBaseType.PostgreSql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            msg = "服务器地址不能为空！";
                            return false;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            msg = "用户名和密码都不能为空！";
                            return false;
                        }
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                        //break;
                }
                //得到数据库访问对象
                IDataAccess _dataAccess = AutoSQLExecutors.Connect(DbServer);
                DataTable UserTableList = _dataAccess.GetSchemaTables();
                msg = "连接成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = "连接失败，请检查！具体错误：" + ex.Message;
                return false;
            }
        }
    }
}
