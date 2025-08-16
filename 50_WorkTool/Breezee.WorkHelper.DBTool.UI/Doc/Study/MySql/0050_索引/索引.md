/*【Mysql的一般索引】：命令规则以IDX_开头，第一列加列数。*/
alter table T_TABLE add  key IDX_DLR3 (DLR_ID,CREATED_DATE,ACCOUNT_INFO_CODE);

/*【Mysql的唯一索引】：命令规则以UK_开头，第一列加列数。*/
alter table T_TABLE add unique key UK_ACCOUNT_INFO_CODE2 (ACCOUNT_INFO_CODE,MODULE_CODE);

