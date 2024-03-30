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
using System.Threading.Tasks;
using Breezee.Core.WinFormUI.Common;
using Breezee.Core;
using System.Linq;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 数据库连接用户控件
    /// </summary>
    public partial class UC_DbConnection : BaseUserControl
    {
        #region 变量
        public bool IsDbNameNotNull = true;//是否数据库名非空
        public bool IsFilterDbExtnedFile = true;//是否过滤db后缀名的数据库文件
        public EventHandler<DBTypeSelectedChangeEventArgs> DBType_SelectedIndexChanged;
        public EventHandler<EventArgs> DBConnName_SelectedIndexChanged;
        public EventHandler<EventArgs> ColumnDefaultValue_LoadCompleted;//列默认值加载完成
        private IDictionary<string, DataTable> _dicConnUserTalbeList = new Dictionary<string, DataTable>();
        //每个功能实例内共用，不作为类静态变量
        public IDictionary<string, DataTable> userTableDic = new Dictionary<string, DataTable>(); //用户所有表清单
        public IDictionary<string, DataTable> userColumnDic = new Dictionary<string, DataTable>(); //用户所有表的所有列：只根据schema来过滤
        //用户所有表所有列的默认值清单：因为查询比较慢，所以作为类静态变量，所有功能共用
        public static IDictionary<string, DataTable> defaultValueDic = new Dictionary<string, DataTable>(); 
        public IDataAccess _dataAccess;
        public DbServerInfo LatestDbServerInfo;//最后一次连接的服务器信息

        public bool IsConnChange { get; private set; }
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
            cbbDatabaseType.BindTypeValueDropDownList(dtDbType, true, true);
            
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
                //解密密码
                string sEncPwd = dr["USER_PASSWORD"].ToString();
                if (!string.IsNullOrEmpty(sEncPwd))
                {
                    try
                    {
                        dr["USER_PASSWORD"] = EncryptHelper.AESDecrypt(sEncPwd, DBTGlobalValue.DBTDesEncryKey, DBTGlobalValue.DBTDesEncryVector);
                    }
                    catch
                    {
                        //报错时啥都不用做
                    }
                }
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

        #region 获取数据服务器信息
        /// <summary>
        /// 获取数据服务器信息
        /// </summary>
        /// <param name="isQueryTableColumnRealTime">是否实时查询数据库表列信息：默认否</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DbServerInfo> GetDbServerInfo(bool isQueryTableColumnRealTime = false)
        {
            DataBaseType curDatabaseType = cbbDatabaseType.SelectedValue == null || string.IsNullOrEmpty(cbbDatabaseType.SelectedValue.ToString()) ? DataBaseType.None : (DataBaseType)int.Parse(cbbDatabaseType.SelectedValue.ToString());

            var DbServer = new DbServerInfo()
            {
                Database = txbDbName.Text.Trim(),
                DatabaseType = curDatabaseType,
                Password = txbPassword.Text.Trim(),
                PortNo = txbPortNO.Text.Trim(),
                SchemaName = txbSchemaName.Text.Trim(),
                ServerName = txbServerIP.Text.Trim(),
                UserName = txbUserName.Text.Trim(),
                UseConnString = ckbUseConString.Checked,
                ConnString = txbDBConString.Text.Trim(),
                DbConnConfigName = cbbDbConnName.Text.Trim(),
            };
            DbServerInfo.ResetConnKey(DbServer);

            DataBaseType selectDBType = curDatabaseType;

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
                        DbServer.SchemaName = DbServer.UserName.ToUpper();//Oracle需要用户名作为Schema
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
                        DbServer.SchemaName = DbServer.Database;//Oracle需要数据库作为Schema
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

            //最后的服务器：为空或与当前获取的服务器不一样，就重新连接
            bool isSameServer = DbServerInfo.IsSameServer(DbServer, LatestDbServerInfo);
            if (LatestDbServerInfo == null || !isSameServer)
            {
                LatestDbServerInfo = DbServer;
                ShowGlobalMsg(this, "正在异步获取数据库的表列信息，请稍等...");
                await QueryTableColumns(DbServer,isQueryTableColumnRealTime, isSameServer);
                ShowGlobalMsg(this, "异步获取数据库的表列信息完成！");
                IsConnChange = true;
            }
            else
            {
                IsConnChange = false;
            }
            //返回
            return DbServer;
        }

        /// <summary>
        /// 查询表列信息
        /// </summary>
        /// <param name="isQueryTableColumnRealTime">是否实时查询数据库表列信息</param>
        /// <param name="DbServer"></param>
        /// <returns></returns>
        private async Task QueryTableColumns(DbServerInfo DbServer, bool isQueryTableColumnRealTime,bool isSameServer)
        {
            //得到数据库访问对象
            _dataAccess = AutoSQLExecutors.Connect(DbServer);
            //判断是否实时查询
            if (isQueryTableColumnRealTime)
            {
                //所有用户表：GetSqlSchemaTables 和 GetSqlSchemaTables
                userTableDic[DbServer.DbConnKey] = _dataAccess.GetSqlSchemaTables();
                //所有用户表的所有列
                userColumnDic[DbServer.DbConnKey] = _dataAccess.GetSqlSchemaTableColumns(string.Empty, DbServer.SchemaName);
            }
            else
            {
                if (!isSameServer || userTableDic.ContainsKey(DbServer.DbConnKey) || userTableDic[DbServer.DbConnKey].Rows.Count == 0)
                {
                    //所有用户表：GetSqlSchemaTables 和 GetSqlSchemaTables
                    userTableDic[DbServer.DbConnKey] = _dataAccess.GetSqlSchemaTables();
                }
                if (!isSameServer || userColumnDic.ContainsKey(DbServer.DbConnKey) || userColumnDic[DbServer.DbConnKey].Rows.Count == 0)
                {
                    //所有用户表的所有列
                    userColumnDic[DbServer.DbConnKey] = _dataAccess.GetSqlSchemaTableColumns(string.Empty, DbServer.SchemaName);
                }
            }
            //默认值处理
            if (!defaultValueDic.ContainsKey(DbServer.DbConnKey))
            {
                //异步查询默认值
                Task.Run(() => QueryColumnsDefaultValue(DbServer));
            }
            else
            {
                ReplaceColumnListDefault(DbServer.DbConnKey); //设置列清单的默认值
            }
        }

        /// <summary>
        /// 设置列清单的默认值
        /// </summary>
        /// <param name="sKey"></param>
        private void ReplaceColumnListDefault(string sKey)
        {
            //使用LINQ：速度比较快
            var query = from f in defaultValueDic[sKey].AsEnumerable()
                        join s in userColumnDic[sKey].AsEnumerable()
                        on new
                        {
                            c1 = f.Field<string>(DBColumnEntity.SqlString.TableName),
                            c2 = f.Field<string>(DBColumnEntity.SqlString.Name)
                        }
                        equals new
                        {
                            c1 = s.Field<string>(DBColumnEntity.SqlString.TableName),
                            c2 = s.Field<string>(DBColumnEntity.SqlString.Name)
                        }
                        select new { F1 = f, S1 = s };
            var joinList = query.ToList();
            //用当前默认值来覆盖列清单
            foreach (var item in joinList)
            {
                item.S1[DBColumnEntity.SqlString.Default] = item.F1[DBColumnEntity.SqlString.Default].ToString();
            }
        }

        /// <summary>
        /// 查询表列的默认值信息
        /// </summary>
        /// <param name="DbServer"></param>
        /// <returns></returns>
        public async Task QueryColumnsDefaultValue(DbServerInfo DbServer)
        {
            if (string.IsNullOrEmpty(DbServer.DbConnConfigName))
            {
                return;
            }
            //得到数据库访问对象
            _dataAccess = AutoSQLExecutors.Connect(DbServer);
            if(DbServer.DatabaseType== DataBaseType.Oracle)
            {
                ShowGlobalMsg(this, "准备异步查询列的默认值信息，过程会有点慢，请耐心等待...");
            }
            //这里查询所有表的默认值：解决不了查询慢的问题
            defaultValueDic[DbServer.DbConnKey] = _dataAccess.GetSqlTableColumnsDefaultValue(new List<string>());
            //设置列清单的默认值
            ReplaceColumnListDefault(DbServer.DbConnKey);
            //调用绑定的代理方法
            if (ColumnDefaultValue_LoadCompleted != null)
            {
                ColumnDefaultValue_LoadCompleted(this, new EventArgs() { });
            }
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

        public string getSelectedDatabaseType()
        {
            if(cbbDatabaseType == null)
            {
                return string.Empty;
            }
            return cbbDatabaseType.SelectedValue.ToString();
        }

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
