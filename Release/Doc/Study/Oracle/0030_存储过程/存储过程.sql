/*ִ�д洢����*/
begin
   p_se_if_to_other_system('SE_TOOL_MAINTAIN_002','D2305','MD23051207001',null,null,null,null);
end;

/*�����б���*/
Row_config  ebom_t_item_config%Rowtype;
Begin
Row_config := Null;
-- ��ֵ
Row_config.con_id      := sys_guid();
Row_config.item_id     := i_itemid;
Row_config.creater     := i_operid;
Row_config.create_time := Sysdate;
Insert Into ebom_t_item_config Values Row_config;
End;

/*�Զ����¼����*/
Type CommonReturn Is Record(
   o_Return_Code   Varchar2(20) Not Null := 'ERROR', --���ش���
   o_Error_Message Varchar2(200), --ҵ�������Ϣ
   o_Sqlerrm       Varchar2(2000) --�洢�����쳣
   );
 var_CommonReturn CommonReturn;



