using Breezee.Core.WinFormUI;
using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.Entity;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.AutoSQLExecutor.Core;
using Breezee.AutoSQLExecutor.Common;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据库配置维护
    /// </summary>
    public partial class FrmDBConfigSet_D : BaseForm
    {
        #region 变量
        private DataRow _drEdit;
        public bool IsDbNameNotNull = true;//是否数据库名非空
        public bool IsFilterDbExtnedFile = true;//是否过滤db后缀名的数据库文件
        //控件集合字典
        List<DBColumnControlRelation> _listSupply = new List<DBColumnControlRelation>();
        //
        private IDBConfigSet _IDBConfigSet;
        #endregion

        #region 构造函数
        public FrmDBConfigSet_D()
        {
            InitializeComponent();
        }

        public FrmDBConfigSet_D(DataRow drEdit)
        {
            InitializeComponent();
            _drEdit = drEdit;

        }
        #endregion

        #region 窗体加载事件
        private void FrmDBConfigSet_D_Load(object sender, EventArgs e)
        {
            //接口对象
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();

            #region 绑定下拉框
            //数据库类型
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbDatabaseType.BindTypeValueDropDownList(dtDbType, false, true);
            #endregion

            //设置控件关系
            SetControlColumnRelation();

            if (_drEdit == null)//新增
            {
                tsbCopyAdd.Visible = false;
            }
            else //修改
            {
                //解密密码
                string sEncPwd = _drEdit[DT_DBT_BD_DB_CONFIG.SqlString.USER_PASSWORD].ToString();
                if (!string.IsNullOrEmpty(sEncPwd))
                {
                    try
                    {
                        _drEdit[DT_DBT_BD_DB_CONFIG.SqlString.USER_PASSWORD] = EncryptHelper.AESDecrypt(sEncPwd, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector);
                    }
                    catch
                    {
                        //报错时啥都不用做
                    }
                }
                _listSupply.SetControlValue(_drEdit);
                tsbCopyAdd.Visible = true;
            }
        }
        #endregion

        #region 设置列名与控件关系
        private void SetControlColumnRelation()
        {
            //配置表
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_ID, txbID));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.UPDATE_CONTROL_ID, txbUPDATE_CONTROL_ID));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.DB_TYPE, cbbDatabaseType, "数据库类型"));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_CODE, txbDBConfigCode, "配置编码"));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_NAME, txbDBConfigName));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.SERVER_IP, txbServerIP, "服务器IP"));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.PORT_NO, txbPortNO));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.DB_NAME, txbDbName));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.SCHEMA_NAME, txbSchemaName));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.USER_NAME, txbUserName));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.USER_PASSWORD, txbPassword));
            _listSupply.Add(new DBColumnControlRelation(DT_DBT_BD_DB_CONFIG.SqlString.REMARK, txbRemark));
        }
        #endregion

        #region 保存按钮事件
        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 保存前判断
                string strInfo = _listSupply.JudgeNotNull(true);
                if (!string.IsNullOrEmpty(strInfo))
                {
                    ShowInfo("保存失败！\n" + strInfo);
                    return;
                }
                #endregion

                _dicObject = CreateObjectDictionary(true);
                DataTable dtSave;

                #region 供应商表
                bool isAdd = txbID.Text.Length == 0;

                List<string> coloumns = isAdd ? null : _listSupply.GetSaveColumnNameList();
                dtSave = DBToolHelper.Instance.DataAccess.GetTableConstruct(DT_DBT_BD_DB_CONFIG.TName, coloumns);
                dtSave.DefaultValue(_loginUser);//设置登录用户的默认值
                _listSupply.GetControlValue(dtSave, isAdd);
                //加密密码
                string sEncPwd = dtSave.Rows[0][DT_DBT_BD_DB_CONFIG.SqlString.USER_PASSWORD].ToString();
                if (!string.IsNullOrEmpty(sEncPwd))
                {
                    dtSave.Rows[0][DT_DBT_BD_DB_CONFIG.SqlString.USER_PASSWORD] = EncryptHelper.AESEncrypt(sEncPwd, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector);
                }
                if (isAdd)
                {
                    dtSave.Rows[0][DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_ID] = StringHelper.GetGUID();
                    dtSave.Columns[DT_DBT_BD_DB_CONFIG.SqlString.CREATE_TIME].ExtProp(DbDefaultValueType.DateTime);
                    dtSave.Columns[DT_DBT_BD_DB_CONFIG.SqlString.LAST_UPDATED_TIME].ExtProp(DbDefaultValueType.DateTime);
                }
                #endregion
                //保存传入参数处理
                _dicObject[DT_SYS_USER.ORG_ID] = _loginUser.ORG_ID;
                _dicObject[DT_SYS_USER.USER_ID] = _loginUser.USER_ID;
                _dicObject[DT_ORG_EMPLOYEE.EMP_ID] = _loginUser.EMP_ID;
                _dicObject[DT_ORG_EMPLOYEE.EMP_NAME] = _loginUser.EMP_NAME;
                _dicObject[IDBConfigSet.SaveDbConfig_InDicKey.DT_TABLE] = dtSave;
                //保存维修单
                _IDBConfigSet.SaveDbConfig(_dicObject).SafeGetDictionary();
                ShowInfo("保存成功！");
                DialogResult = DialogResult.OK;
                Close();
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

                int iDbType = int.Parse(cbbDatabaseType.SelectedValue.ToString());
                DataBaseType selectDBType = (DataBaseType)iDbType;

                if (selectDBType != DataBaseType.Oracle && selectDBType != DataBaseType.SQLite)
                {
                    if (IsDbNameNotNull && string.IsNullOrEmpty(DbServer.Database))
                    {
                        MsgHelper.ShowErr("数据库名称不能为空！");
                        return;
                    }
                }

                switch (selectDBType)
                {
                    case DataBaseType.SqlServer:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return;
                        }
                        break;
                    case DataBaseType.Oracle:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("TNS名称不能为空！");
                            return;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return;
                        }
                        break;
                    case DataBaseType.MySql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return;
                        }
                        break;
                    case DataBaseType.SQLite:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("数据库文件路径不能为空！");
                            return;
                        }
                        break;
                    case DataBaseType.PostgreSql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return;
                        }
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                        //break;
                }
                //得到数据库访问对象
                IDataAccess _dataAccess = AutoSQLExecutors.Connect(DbServer);
                DataTable UserTableList = _dataAccess.GetSchemaTables();
                MsgBox.Show("连接成功！");
            }
            catch(Exception ex)
            {
                MsgHelper.ShowErr("连接失败，请检查！具体错误："+ex.Message);
            }
        }

        /// <summary>
        /// 复制新增按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCopyAdd_Click(object sender, EventArgs e)
        {
            txbID.Text = string.Empty;
            txbUPDATE_CONTROL_ID.Text = string.Empty;
            txbDBConfigCode.Text = txbDBConfigCode.Text.Trim() + "Copy";
        }
    }
}
