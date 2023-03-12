using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using Breezee.Core.Tool;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.WinFormUI;
using Breezee.AutoSQLExecutor.Common;
using Breezee.WorkHelper.DBTool.UI.StringBuild;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据库连接用户控件
    /// </summary>
    public partial class UC_DbConnection : UserControl
    {
        #region 变量
        public bool IsDbNameNotNull = true;//是否数据库名非空
        public bool IsFilterDbExtnedFile = true;//是否过滤db后缀名的数据库文件
        public EventHandler<DBTypeSelectedChangeEventArgs> DBType_SelectedIndexChanged;
        public EventHandler<EventArgs> DBConnName_SelectedIndexChanged;
        private IDictionary<string, DataTable> _dicConnUserTalbeList = new Dictionary<string, DataTable>();
        
        public DataTable UserTableList;
        public IDataAccess _dataAccess;

        public string DbConnName
        {
            get { return cbbDbConnName.Text.Trim(); }
        }
        #endregion

        #region 构造函数
        public UC_DbConnection()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void UC_DbConnection_Load(object sender, EventArgs e)
        {
            #region 绑定下拉框
            //数据库类型
            DataTable dtDbType = DBToolUIHelper.GetBaseDataTypeTable();
            cbbDatabaseType.BindTypeValueDropDownList(dtDbType, false, true);
            
            #endregion
            //数据库类型为只读
            cbbDatabaseType.Enabled = false;

            this.cbbDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cbbDatabaseType_SelectedIndexChanged);
            this.cbbDbConnName.SelectedIndexChanged += new System.EventHandler(this.cbbDbConnName_SelectedIndexChanged);

            cbbDatabaseType.SelectedIndex = 0;

        }
        #endregion

        #region 数据库连接名下拉框选择变化
        private void cbbDbConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDbConnName.SelectedValue == null || string.IsNullOrEmpty(cbbDbConnName.SelectedValue.ToString()))
            {
                cbbDatabaseType.SelectedIndex = 0;
            }
            else
            {
                DataRow dr = (cbbDbConnName.SelectedItem as DataRowView).Row;
                cbbDatabaseType.SelectedValue = dr["DB_TYPE"].ToString();//记得这个放第一位
                txbSchemaName.Text = dr["SCHEMA_NAME"].ToString();
                txbServerIP.Text = dr["SERVER_IP"].ToString();
                txbPortNO.Text = dr["PORT_NO"].ToString();
                txbDbName.Text = dr["DB_NAME"].ToString();
                txbUserName.Text = dr["USER_NAME"].ToString();
                txbPassword.Text = dr["USER_PASSWORD"].ToString();
                
            }
            //调用代理
            if (DBConnName_SelectedIndexChanged != null)
            {
                DBConnName_SelectedIndexChanged(this, new EventArgs() { });
            }
        }
        #endregion

        #region 数据库类型选择变化事件
        private void cbbDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //重置控件
            UIHelper.ResetControl(txbServerIP, txbUserName, txbPassword, txbDbName, txbPortNO, txbSchemaName);
            //默认显示端口号
            lblPortNO.Visible = true;
            txbPortNO.Visible = true;
            //显示数据库
            lblDbName.Visible = true;
            txbDbName.Visible = true;
            //
            lblServerAddr.Text = "服务器地址：";
            btnSelectDbFile.Visible = false;
            ckbUseConString.Checked = false;

            int iDbType = int.Parse(cbbDatabaseType.SelectedValue.ToString());
            DataBaseType selectDBType = (DataBaseType)iDbType;


            switch (selectDBType)
            {
                case DataBaseType.SqlServer:
                    //
                    txbServerIP.Text = "localhost";
                    break;
                case DataBaseType.Oracle:
                    lblServerAddr.Text = "TNS名称：";
                    //不显示端口号
                    lblPortNO.Visible = false;
                    txbPortNO.Visible = false;
                    //不显示数据库
                    lblDbName.Visible = false;
                    txbDbName.Visible = false;
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.SQLite:
                    lblServerAddr.Text = "数据库文件路径：";
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
                    break;
                default:
                    throw new Exception("暂不支持该数据库类型！");
                    //break;
            }
            //调用代理
            if (DBType_SelectedIndexChanged != null)
            {
                DBType_SelectedIndexChanged(this, new DBTypeSelectedChangeEventArgs() { SelectDBType = selectDBType });
            }
        }
        #endregion

        #region 设置数据库连接数据源
        public void SetDbConnComboBoxSource(DataTable dtDbConn)
        {
            //数据库连接配置
            dtDbConn.Rows.InsertAt(dtDbConn.NewRow(), 0);//新增一个空行
            cbbDbConnName.BindDropDownList(dtDbConn,DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_ID, DT_DBT_BD_DB_CONFIG.SqlString.DB_CONFIG_CODE, true);
        }
        #endregion

        #region 判断验证是否通过
        public DbServerInfo GetDbServerInfo()
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
                UseConnString = ckbUseConString.Checked,
                ConnString = txbDBConString.Text.Trim(),
            };

            int iDbType = int.Parse(cbbDatabaseType.SelectedValue.ToString());
            DataBaseType selectDBType = (DataBaseType)iDbType;

            if (!ckbUseConString.Checked)
            {
                if (selectDBType != DataBaseType.Oracle && selectDBType != DataBaseType.SQLite)
                {
                    if (IsDbNameNotNull && string.IsNullOrEmpty(DbServer.Database))
                    {
                        MsgHelper.ShowErr("数据库名称不能为空！");
                        return null;
                    }
                }

                switch (selectDBType)
                {
                    case DataBaseType.SqlServer:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return null;
                        }
                        break;
                    case DataBaseType.Oracle:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("TNS名称不能为空！");
                            return null;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return null;
                        }
                        break;
                    case DataBaseType.MySql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return null;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return null;
                        }
                        break;
                    case DataBaseType.SQLite:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("数据库文件路径不能为空！");
                            return null;
                        }
                        break;
                    case DataBaseType.PostgreSql:
                        if (string.IsNullOrEmpty(DbServer.ServerName))
                        {
                            MsgHelper.ShowErr("服务器地址不能为空！");
                            return null;
                        }
                        if (string.IsNullOrEmpty(DbServer.UserName) || string.IsNullOrEmpty(DbServer.Password))
                        {
                            MsgHelper.ShowErr("用户名和密码都不能为空！");
                            return null;
                        }
                        break;
                    default:
                        throw new Exception("暂不支持该数据库类型！");
                        //break;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(DbServer.ConnString))
                {
                    MsgHelper.ShowErr("连接字符串不能为空！");
                    return null;
                }
            }
            //得到数据库访问对象
            _dataAccess = AutoSQLExecutors.Connect(DbServer);
            //所有用户表：GetSqlSchemaTables 和 GetSqlSchemaTables
            UserTableList = _dataAccess.GetSchemaTables();
            //返回
            return DbServer;
        }
        #endregion

        #region 选择数据文件
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

        #region 设置固定数据库类型
        public void SetFixDataBaseType(DataBaseType dbtFix)
        {
            cbbDatabaseType.SelectedValue = ((int)dbtFix).ToString();
            cbbDatabaseType.Enabled = false;
        }
        #endregion

        private void ckbUseConString_CheckedChanged(object sender, EventArgs e)
        {
            txbDBConString.ReadOnly = ckbUseConString.Checked == false ? true : false;
        }
    }

    public class DBTypeSelectedChangeEventArgs: EventArgs
    {
        public DataBaseType SelectDBType;
    }
}
