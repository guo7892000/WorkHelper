/*执行存储过程*/
begin
   p_se_if_to_other_system('SE_TOOL_MAINTAIN_002','D2305','MD23051207001',null,null,null,null);
end;

/*定义行变量*/
Row_config  ebom_t_item_config%Rowtype;
Begin
Row_config := Null;
-- 赋值
Row_config.con_id      := sys_guid();
Row_config.item_id     := i_itemid;
Row_config.creater     := i_operid;
Row_config.create_time := Sysdate;
Insert Into ebom_t_item_config Values Row_config;
End;

/*自定义记录类型*/
Type CommonReturn Is Record(
   o_Return_Code   Varchar2(20) Not Null := 'ERROR', --返回代码
   o_Error_Message Varchar2(200), --业务错误信息
   o_Sqlerrm       Varchar2(2000) --存储过程异常
   );
 var_CommonReturn CommonReturn;



