using Breezee.AutoSQLExecutor.Common;
using Breezee.AutoSQLExecutor.Core;
using Breezee.Core.Interface;
using Breezee.Core.IOC;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using Breezee.WorkHelper.DBTool.IBLL;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDictionaryExtension = Breezee.Core.Interface.IDictionaryExtension;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 自动参数化SQL验证（MyPeach.Net项目的算法验证）
    /// </summary>
    public partial class FrmDBTAutoSqlVerify : BaseForm
    {
        private IDBConfigSet _IDBConfigSet;
        private DbServerInfo _dbServer;
        private IDataAccess _dataAccess;
        private DataTable dtInputType;
        public FrmDBTAutoSqlVerify()
        {
            InitializeComponent();
        }

        private void FrmDBTAutoSqlVerify_Load(object sender, EventArgs e)
        {
            #region 设置数据库连接控件
            _IDBConfigSet = ContainerContext.Container.Resolve<IDBConfigSet>();
            _dicQuery[DT_DBT_BD_DB_CONFIG.SqlString.IS_ENABLED] = "1";
            DataTable dtConn = _IDBConfigSet.QueryDbConfig(_dicQuery).SafeGetDictionaryTable();
            uC_DbConnection1.SetDbConnComboBoxSource(dtConn);
            uC_DbConnection1.IsDbNameNotNull = true;
            //uC_DbConnection1.DBType_SelectedIndexChanged += cbbDatabaseType_SelectedIndexChanged;//数据库类型下拉框变化事件
            uC_DbConnection1.ShowGlobalMsg += ShowGlobalMsg_Click;
            #endregion

            _dicString.Add("String", "String");
            _dicString.Add("DateTime", "DateTime");
            _dicString.Add("Int", "Int");
            dtInputType = _dicString.GetTextValueTable(false);
            _dicString.Clear();
            _dicString.Add("1", "select");
            _dicString.Add("2", "withSelect");
            _dicString.Add("3", "selectUnion");
            _dicString.Add("20", "insertValues");
            _dicString.Add("21", "insertSelect");
            _dicString.Add("22", "insertWithSelect");
            _dicString.Add("23", "withInsertSelect");
            _dicString.Add("30", "update");
            _dicString.Add("40", "delete");
            _dicString.Add("50", "mergeInto");
            _dicString.Add("60", "动态条件拼接SQL段");
            _dicString.Add("70", "IN清单分拆");
            cbbExampleType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), true, true);
            SetTag();

            _dicString.Clear();
            _dicString.Add("1", "命名参数");
            _dicString.Add("2", "位置参数");
            _dicString.Add("3", "值替换");
            cbbParamType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            //加载用户偏好值
            rtbSqlInput.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SQLAutoParamVerify_BeforeSql, "").Value;
            lblBefore.Text = "条件格式：#键名:M:R:LS:D-now()-r-n:N#，其中M表示非空，R表示值替换，LS表示字符列表，LI为整型列表，即IN括号里的部分字符。N不加引号；D默认值配置，其第二个参数为默认值。";
            lblFuncInfo.Text = "针对自动参数化SQL的个人项目（Java版和C#版）：MyPeach、MyPeach.Net的有效性验证！";
            rtbSqlOutput.ReadOnly= true;
        }

        #region 显示全局提示信息事件
        private void ShowGlobalMsg_Click(object sender, string msg)
        {
            ShowDestopTipMsg(msg);
        }
        #endregion

        private void SetTag()
        {
            //条件网格
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name("IN_KEY").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("IN_VALUE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(true).Visible().Build(),
                new FlexGridColumn.Builder().Name("IN_TYPE").Type(DataGridViewColumnTypeEnum.ComboBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(0).Edit(true).Visible(false).Build()
            );
            dgvConditionInput.Tag = fdc.GetGridTagString();
            dgvConditionInput.BindDataGridView(fdc.GetNullTable()); 
            //网格中的条件类型下拉框：隐藏，没使用。因为SQL配置可以指定，加引号就是是字符，不加就是整型。
            DataGridViewComboBoxColumn cmbWarehouse = dgvConditionInput.Columns["IN_TYPE"] as DataGridViewComboBoxColumn;
            cmbWarehouse.Name = IDictionaryExtension.Dictionary_Key;
            cmbWarehouse.HeaderText = "类型";
            cmbWarehouse.DataPropertyName = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.ValueMember = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.DisplayMember = IDictionaryExtension.Dictionary_Value;
            cmbWarehouse.DataSource = dtInputType;

            fdc = new FlexGridColumnDefinition();
            fdc.AddColumn(
                FlexGridColumn.NewRowNoCol(),
                new FlexGridColumn.Builder().Name("OUT_KEY").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("OUT_VALUE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(150).Edit(false).Visible().Build(),
                new FlexGridColumn.Builder().Name("OUT_TYPE").Type(DataGridViewColumnTypeEnum.TextBox).Align(DataGridViewContentAlignment.MiddleLeft).Width(0).Edit(false).Visible(false).Build()
            );
            dgvConditionOutput.Tag = fdc.GetGridTagString();
            dgvConditionOutput.BindDataGridView(fdc.GetNullTable());
        }

        /// <summary>
        /// 获取条件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGetCondition_Click(object sender, EventArgs e)
        {
            string sSqlBefore = rtbSqlInput.Text.Trim();
            if (string.IsNullOrEmpty(sSqlBefore))
            {
                ShowErr("请输入要参数化的SQL！");
                return;
            }
            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                ShowErr("请选择一个连接！");
                return;
            }
            else
            {
                _dataAccess = AutoSQLExecutors.Connect(_dbServer); //每次都要获取一个连接，因为可能会连接不同数据库
            }
            IDictionary<string, SqlKeyValueEntity> dicPreCondition = _dataAccess.SqlParsers.PreGetParam(sSqlBefore,_dicObject);
            DataTable dt = dgvConditionInput.GetBindingTable();
            dt.Clear();
            foreach (var item in dicPreCondition.Keys)
            {
                DataRow drNew = dt.NewRow();
                drNew["IN_KEY"] = item;
                drNew["IN_VALUE"] = dicPreCondition[item].KeyValue;
                dt.Rows.Add(drNew);
            }
            dgvConditionInput.ShowRowNum(true);
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {
            btnGetCondition.PerformClick();
        }

        private async void tsbConvert_Click(object sender, EventArgs e)
        {
            string sSql = rtbSqlInput.Text.Trim();
            if (string.IsNullOrEmpty(sSql))
            {
                ShowErr("请输入要参数化的SQL！");
                return;
            }

            _dbServer = await uC_DbConnection1.GetDbServerInfo();
            if (_dbServer == null)
            {
                ShowErr("请选择一个连接！");
                return;
            }
            else
            {
                _dataAccess = AutoSQLExecutors.Connect(_dbServer);
            }

            DataTable dt = dgvConditionInput.GetBindingTable();
            IDictionary<string,object> data = new Dictionary<string,object>();
            foreach (DataRow dr in dt.Rows)
            {
                string sKey = dr["IN_KEY"].ToString();
                string sValue = dr["IN_VALUE"].ToString().trim();
                bool isString = sValue.IndexOf("'") > -1;
                if (!string.IsNullOrEmpty(sValue))
                {
                    string[] lstArr = sValue.Split(new char[] { ',', '，' });
                    if (lstArr.Length == 1)
                    {
                        if (!data.ContainsKey(sKey))
                        {
                            data.Add(sKey, sValue.replace("'",""));
                        }
                    }
                    else
                    {
                        List<string> lstString = new List<string>();
                        List<int> lstInt = new List<int>();
                        if (isString)
                        {
                            foreach (string item in lstArr)
                            {
                                lstString.Add(item);
                            }
                            data[sKey] = lstString;
                        }
                        else
                        {
                            foreach (string item in lstArr)
                            {
                                int iResult;
                                if(int.TryParse(item, out iResult))
                                {
                                    lstInt.Add(int.Parse(item));
                                }
                                else
                                {
                                    ShowInfo("IN清单的【"+ item + "】转换为整型失败，请保证不加引号的IN项为整型！");
                                    return;
                                }
                            }
                            data[sKey] = lstInt;
                        }
                    }
                }
            }
            TargetSqlParamTypeEnum sqlParamTypeEnum = TargetSqlParamTypeEnum.NameParam;
            string sParamType = cbbParamType.SelectedValue.ToString();
            if("2".Equals(sParamType, StringComparison.OrdinalIgnoreCase))
            {
                sqlParamTypeEnum = TargetSqlParamTypeEnum.PositionParam;
            }
            else if ("3".Equals(sParamType, StringComparison.OrdinalIgnoreCase))
            {
                sqlParamTypeEnum = TargetSqlParamTypeEnum.DIRECT_RUN;
            }
            //参数化转化
            ParserResult result = _dataAccess.SqlParsers.parse(sSql, data, sqlParamTypeEnum);
            if ("0".equals(result.Code))
            {
                rtbSqlOutput.Text = result.Sql;
                dt = dgvConditionOutput.GetBindingTable();
                dt.Clear();
                if (sqlParamTypeEnum == TargetSqlParamTypeEnum.NameParam)
                {
                    // 命名参数
                    foreach (var item in result.entityQuery)
                    {
                        DataRow drNew = dt.NewRow();
                        drNew["OUT_KEY"] = item.Key;
                        drNew["OUT_VALUE"] = item.Value.KeyValue;
                        dt.Rows.Add(drNew);
                    }
                    dgvConditionOutput.Columns["OUT_KEY"].Visible = true;
                }
                else if (sqlParamTypeEnum == TargetSqlParamTypeEnum.PositionParam)
                {
                    // 位置参数
                    foreach (var item in result.PositionCondition)
                    {
                        DataRow drNew = dt.NewRow();
                        drNew["OUT_VALUE"] = item.ToString();
                        dt.Rows.Add(drNew);
                    }
                    dgvConditionOutput.Columns["OUT_KEY"].Visible= false;
                }
                else
                {
                    // 值替换

                }

                dgvConditionOutput.ShowRowNum(true); //显示行号
                _dicObject = result.ObjectQuery;
                //tabControl1.SelectedTab = tpAutoAfter;
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SQLAutoParamVerify_BeforeSql, rtbSqlInput.Text, "SQL自动参数化验证_参数化前SQL");
                WinFormContext.UserLoveSettings.Save();
                ShowInfo("SQL参数化转换成功！");
            }
            else
            {
                ShowErr(result.Message);
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 参数化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            tsbConvert.PerformClick();
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            string sSqlOut = rtbSqlOutput.Text.Trim();
            if (string.IsNullOrEmpty(sSqlOut))
            {
                ShowErr("请先转换包含#参数#的SQL，得到参数化后的SQL，才能执行！");
                return;
            }
            try
            {
                DataTable dtResult = _dataAccess.QueryHadParamSqlData(sSqlOut, _dicObject);
                dgvQuery.BindAutoColumn(dtResult);
                dgvQuery.ShowRowNum(true); //显示行号
            }
            catch(Exception ex) 
            { 
                ShowErr(ex.Message);
            }
        }

        private void cbbExampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbExampleType.SelectedValue == null) return;
            string sType = cbbExampleType.SelectedValue.ToString();
            switch (sType)
            {
                case "1":
                    rtbSqlInput.Text = @"SELECT DISTINCT A.[PROVINCE_ID] AS PROVINCE_ID
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME]
 ,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#' AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB AS A
LEFT  OUTER JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
FULL JOIN BC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
RIGHT  OUTER  JOIN ( /*我是多行注---
换行的释释*/
    SELECT ID,PROVINCE_ID,UPDATE_CONTROL_ID,ID
    FROM TAB_C
    WHERE PROVINCE_ID = '#PROVINCE_ID#'
       AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
) D on D.ID = B.ID AND D.TNAME ='#TNAME#'
WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
    AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
    AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd') #我是注释#号呀
	AND MODIFIER IN ('#MDLIST:LS#') --我是注释--啊
    AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#BF#' )
GROUP BY 	PROVINCE_ID
HAVING A.SORT_ID > #SORT_ID:M:R#
ORDER BY A.[PROVINCE_CODE],B.[CITY_NAME]
LIMIT 100,#PAGE_SIZE:M#";
                    break;
                case "2":
                    rtbSqlInput.Text = @"WITH SE1 AS (
   SELECT 1 FROM TB_AA L
   JOIN TB_BB M ON L.ID = M.AID AND L.TD = '#TD#'
   WHERE G.FF = '#GF#'
    AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')),
SE2 AS (
     SELECT 1,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#') AS ID
     FROM TB_AA L
     JOIN TB_BB M ON L.ID = M.AID AND L.TD = '#TD3#'
     WHERE G.FFF = '#GFF#'
      AND ( ( CREATOR = '#CREATOR1#' OR CREATOR_ID = #CREATOR_ID2# ) AND TFLG = '#TFLG3#')
)
SELECT A.[PROVINCE_ID],A.[PROVINCE_CODE],
 A.[PROVINCE_NAME]
 ,A.[SORT_ID]
 ,B.[CITY_ID]
 ,B.CITY_CODE
 ,B.[CITY_NAME]
 ,((SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#')) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB A
LEFT JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
LEFT JOIN BC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
 WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
     AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )
 AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd')
	AND MODIFIER='#MODIFIER#'
AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#BF#' )
GROUP BY 	PROVINCE_ID
HAVING A.SORT_ID > 1
ORDER BY MODIFIER DESC";
                    break;
                case "3":
                    rtbSqlInput.Text = @"SELECT DISTINCT A.[PROVINCE_ID]
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME]
 ,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#' AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB A
LEFT JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
LEFT JOIN (
    SELECT ID,PROVINCE_ID,UPDATE_CONTROL_ID,ID
    FROM TAB_C
    WHERE PROVINCE_ID = '#PROVINCE_ID#'
       AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
) C on C.ID = B.ID AND C.TNAME ='#TNAME#'
 WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
 AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd')
	AND MODIFIER IN ('#MDLIST:LS#')
AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#BF#' )
ORDER BY A.[PROVINCE_CODE],B.[CITY_NAME]
LIMIT 100,10
UNION    ALL
SELECT DISTINCT A.[PROVINCE_ID]
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME]
 ,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#' AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB A
LEFT JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
LEFT JOIN BC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
 WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	AND TFLG IS NOT NULL
	AND SORT_ID not between 1 and 4
UNION
SELECT DISTINCT A.[PROVINCE_ID]
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME]
 ,(SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#NAME#' AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )) AS ID
  ,A.[UPDATE_CONTROL_ID]
FROM TAB A
LEFT JOIN CBC B on A.ID = B.ID AND A.NAME ='#NAME#' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#DATE#'
LEFT JOIN CDC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
 WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	AND TFLG IS NOT NULL
GROUP BY 	PROVINCE_ID
HAVING A.SORT_ID > 1";
                    break;
                case "20":
                    rtbSqlInput.Text = @"INSERT INTO [dbo].[BAS_PROVINCE] --test
(
  [PROVINCE_ID],[PROVINCE_CODE] --test
    ,[PROVINCE_NAME]
    ,[SORT_ID]
 ,[TFLAG],CDATE
)
VALUES (
 '#PROVINCE_ID#' /*22*/
    ,'#PROVINCE_CODE#' --test
 ,#PROVINCE_NAME#',
 #SORT_ID#
   ,'#TFLAG#',
   TO_DATE('#CDATE#','YYYY-MM-DD')
)";
                    break;
                case "21":
                    rtbSqlInput.Text = @"INSERT INTO [dbo].[BAS_PROVINCE] --test
  ([PROVINCE_ID],[PROVINCE_CODE] --test
    ,[PROVINCE_NAME]
    ,[SORT_ID]
 ,[TFLAG])
 SELECT PROVINCE_ID /*22*/
    ,TO_DATE(A.CREATE_TIME,'yyyy-MM-dd') --test
 ,A.PROVINCE_NAME,A.SORT_ID
   ,'#TFLAG#'
FROM BAS_CITY A
WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
     AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )
 AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd')
	AND MODIFIER='#MODIFIER#'
AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#BF#' )";
                    break;
                case "22":
                    rtbSqlInput.Text = @"/*MySql、Oracle:必须是INSERT INTO在with之前。Oracle查空表必须加FROM DUAL，MySql可有可无。*/
INSERT INTO TEST_TABLE(ID,CNAME)
with TMP_A AS(select #SORT_ID# as id,'#TFLAG#' as name FROM DUAL)
select * from TMP_A

/* 注释222 */
/*PostgreSQL、SQLite:表名和列名要加双引号。:必须是INSERT INTO在with之前。SQLite不能加FROM DUAL。
INSERT INTO ""TEST_TABLE""(""ID"",""CNAME"")
with TMP_A AS(select #SORT_ID# as id,'#TFLAG#' as name FROM DUAL)
select * from TMP_A
*/";
                    break;
                case "23":
                    rtbSqlInput.Text = @"/*SqlServer:必须是with在INSERT INTO之前*/
with TMP_A AS(select #SORT_ID# as id,'#TFLAG#' as name)
INSERT INTO TEST_TABLE(ID,CNAME)
select * from TMP_A";
                    break;
                case "30":
                    rtbSqlInput.Text = @"UPDATE --2232
 [dbo].[BAS_PROVINCE] /***8888***/
   SET [PROVINCE_CODE] = '#PROVINCE_CODE#',[PROVINCE_NAME] = '#PROVINCE_NAME#'
      ,[SORT_ID] = SORT_ID
      ,[REMARK] = '#REMARK#'
      ,[CREATE_TIME] = TO_DATE(#CREATE_TIME#,'yyyy-MM-dd')
      ,[TFLAG] = TFLAG
FROM TAB A
LEFT JOIN BAB B on A.ID = B.ID AND A.NAME ='#NAME#'
LEFT JOIN BC C on C.ID = B.ID AND C.TNAME ='#TNAME#'
 WHERE PROVINCE_ID = '#PROVINCE_ID#'
	AND UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
	OR REMARK LIKE '%#REMARK#'
	AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) OR TFLG = '#TFLG#')
     AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )
 AND TO_CHAR(TFLG,'yyyy') = '#TFLG2#'
AND TFLG =  TO_DATE('#TFLG#','yyyy-MM-dd')
AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND MODIFIER='#MODIFIER#')";
                    break;
                case "40":
                    rtbSqlInput.Text = @"DELETE FROM [dbo].[BAS_PROVINCE]
WHERE PROVINCE_ID = '#PROVINCE_ID:N#'
 and UPDATE_CONTROL_ID= '#UPDATE_CONTROL_ID#'
AND REMARK like '%#REMARK:R#'
AND ( TFLG = '#TFLG#' OR ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) )
AND ( ( CREATOR = '#CREATOR#' OR CREATOR_ID = #CREATOR_ID# ) AND TFLG = '#TFLG#')
AND MODIFIER='#MODIFIER#'";
                    break;
                case "50":
                    rtbSqlInput.Text = @"MERGE INTO credit.record_rule_data AS a
USING tem1 AS b 
ON a.rule_id =b.ruleName AND a.user_id ='#USER_ID#' AND a.auth_type =4
WHEN MATCHED THEN UPDATE SET a.rule_value =b.ruleValue ,update_time=GETDATE()
WHEN NOT MATCHED THEN
INSERT
(gid, create_time, update_time, data_version, user_id, auth_type, rule_id, rule_value)
VALUES
(NEWID(),GETDATE(),GETDATE(),'11','39917B36-9663-42E5-A9E8-7CEB875EDF5F','#AUTH_TYPE#',b.ruleName,b.ruleValue)";
                    break;
                case "60":
                    rtbSqlInput.Text = @"SELECT A.PROVINCE_ID,A.PROVINCE_CODE
 ,A.PROVINCE_NAME
 ,A.SORT_ID
 ,B.CITY_ID
 ,B.CITY_CODE
 ,B.CITY_NAME
 ,((SELECT TOP 1 ID FROM SUB T WHERE T.RID = A.RID AND A.NAME ='#{NAME}')) AS ID
  ,A.UPDATE_CONTROL_ID,
  /***@MP&DYN {[id=1]}& {[A.ID,B.ID]}  @MP&DYN****/
 /* @MP&DYN{[id=2]} &{[A.ID]}  @MP&DYN**/
/* @MP&DYN {[id>=3]}& {[B.ID]} @MP&DYN  */
FROM TAB A
LEFT JOIN BAB B on A.ID = B.ID AND A.NAME ='#{NAME:D&getdate()-R-n}' AND TO_CAHR(A.CDATE,'yyyy-MM-dd') ='#{DATE}'
LEFT JOIN BC C on C.ID = B.ID AND C.TNAME ='#{TNAME}'
 WHERE PROVINCE_ID = '#{PROVINCE_ID}'
	AND UPDATE_CONTROL_ID= '#{UPDATE_CONTROL_ID}'
	OR REMARK LIKE '%#{REMARK}'
	AND ( ( CREATOR = '#{CREATOR}' OR CREATOR_ID = #{CREATOR_ID} ) AND TFLG = '#{TFLG}')
     AND ( TFLG = '#{TFLG}' OR ( CREATOR = '#{CREATOR}' OR CREATOR_ID = #{CREATOR_ID} ) )
 AND TO_CHAR(TFLG,'yyyy') = '#{TFLG2}'
AND TFLG =  TO_DATE('#{TFLG}','yyyy-MM-dd')
	AND MODIFIER='#{MODIFIER}'
AND EXISTS(SELECT 1 FROM TBF G WHERE G.ID = A.ID AND G.BF = '#{BF}' )
GROUPY BY 
  /**  *@MP&DYN {[id=1]}&{[A.ID,B.ID]}  @MP&DYN  ****/
 /* @MP&DYN{[id=2]} &{[A.ID]}  @MP&DYN**/
/* @MP&DYN {[id>=4]} &{[B.ID]} @MP&DYN  */";
                    break;
                case "70":
                    rtbSqlInput.Text = @"SELECT DISTINCT A.[PROVINCE_ID] AS PROVINCE_ID
  ,A.[PROVINCE_CODE]
 ,B.[CITY_NAME],B.CITY_ID,B.CITY_CODE,B.CITY_NAME
FROM BAS_PROVINCE AS A
JOIN BAS_CITY B on A.PROVINCE_ID = B.PROVINCE_ID 
WHERE A.ORG_ID = '#ORG_ID#'
	AND (A.PROVINCE_ID IN ('#PROVINCE_ID_LIST:LS-4#') AND ( A.CREATOR = '#CREATOR#' OR A.CREATOR_ID = #CREATOR_ID# ) AND B.CITY_ID IN ('#CITY_ID_LIST:LS-4#'))
	AND A.PROVINCE_CODE IN ('#PROVINCE_CODE_LIST:LS-2#')
 AND TO_CHAR(A.TFLG,'yyyy') = '#TFLG2#'";
                    break;
                default:
                    break;
            }
        }
    }
}
