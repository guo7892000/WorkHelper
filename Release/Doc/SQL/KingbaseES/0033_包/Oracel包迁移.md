## Oracel包迁移
在Oracle兼容模式下，金仓数据库（KingbaseES）支持创建和使用包；并且在迁移Oracle包时，通常从Oracle中复制包的声明和包体，
然后在KingbaseES中创建同名包并粘贴内容即可。以下是迁移步骤和注意事项：
### 游标更新修改为MERGE INTO
```
--更新行号（旧）
Declare
    Cursor c_Expand_Update(i_ConversationID In Varchar2) Is
    Select Row_number() over(Order By LEVEL_PATH) RN
        From mbom_t_expand_tmp P
        Where P.CONVERSATIONID = I_CONVERSATIONID
        For Update Of CONVERSATIONID;
Begin
    Open c_Expand_Update(i_ConversationID => i_ConversationID);
    Loop
    Fetch c_Expand_Update
        Into var_Num;
    Exit When c_Expand_Update%Notfound;
      
    Update mbom_t_expand_tmp Set NUM = var_Num Where Current Of c_Expand_Update;
    End Loop;
    Close c_expand_update;
End;

--更新行号（新）
MERGE INTO mbom_t_expand_tmp AS T
	   USING (
	      SELECT ROWID, 
	             ROW_NUMBER() OVER (ORDER BY LEVEL_PATH) AS rn
	      FROM mbom_t_expand_tmp
	      WHERE CONVERSATIONID = i_ConversationID
	   ) AS S
	   ON (T.ROWID = S.ROWID)
	   WHEN MATCHED THEN
	      UPDATE SET T.NUM = S.rn;

```
### Merge Into中的新增，Insert Values要使用具体列名和值，不能使用行变量
```
--旧
Merge Into ebom_t_prdmodel_relation t
Using (Select * From dual) s
On (t.PM_ID_FROM = lv_baseID And t.PM_ID_TO = lv_BaseIns)
When Not Matched Then
    Insert Values var_prdmodel_relation;
--新
Merge Into ebom_t_prdmodel_relation t
Using (Select * From dual) s
On (t.PM_ID_FROM = lv_baseID And t.PM_ID_TO = lv_BaseIns)
When Not Matched Then
Insert (relation_id, pm_id_from, ver_num_from, prdmodel_type_from,
        prdmodel_code_from, pm_id_to, ver_num_to, prdmodel_type_to,
        prdmodel_code_to, cnc_bgn, date_bgn, cnc_end, date_end)
Values (sys_guid, lv_baseID, '-', '-', lv_baseID, lv_BaseIns, '-',
        Case When lv_basetype = 'dt_itemtype_02' Then 'dt_itemtype_40'
                When lv_basetype = 'dt_itemtype_30' Then 'dt_itemtype_32'
        End, lv_BaseIns, i_cnc, '', '', '');
```
### 系统自带的sys.odcivarchar2list，替换为新增的类型varchar2list，并且初始化时去掉New
```
--旧
var_MODEL_CODE_LIST sys.odcivarchar2list;
var_MODEL_CODE_LIST := New sys.odcivarchar2list();
--新
CREATE TYPE "fawbom"."varchar2list" AS VARRAY (32767) OF varchar;
var_MODEL_CODE_LIST varchar2list;
var_MODEL_CODE_LIST := varchar2list();
```
### 需要针对ROWID转换为varchar，才能取最大值
```
--旧
Select p.oper_id, p.prd_id, p.prd_pid, max(ROWID) As row_id
 From MBOM_T_PRODUCTLINE_TMP P
Where OPER_ID = 'var_unique_operId'
  Group By p.oper_id, p.prd_id, p.prd_pid
 Having Count(1) > 1
--新
Select p.oper_id, p.prd_id, p.prd_pid, max(ROWID::varchar) As row_id
 From MBOM_T_PRODUCTLINE_TMP P
Where OPER_ID = 'var_unique_operId'
  Group By p.oper_id, p.prd_id, p.prd_pid
 Having Count(1) > 1
```
### 模板
```
--旧

--新

```