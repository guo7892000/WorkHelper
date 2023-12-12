/*解决新增或修改视图报授权选项问题*/
--查询表权限
SELECT * FROM USER_TAB_PRIVS T 
WHERE T.GRANTOR='MDM' AND T.grantee='UC'
ORDER BY T.TABLE_NAME;
select * from T_MDM_COMP_BRAND;
--在UC用户下执行视图V_UC_BASE_ALLCARBRAND变更时报错。提示：ORA-01720：不存在“****.****"授权选项。
--执行以下语句解决
GRANT SELECT ON MDM.T_MDM_COMP_BRAND TO UC WITH GRANT OPTION;

