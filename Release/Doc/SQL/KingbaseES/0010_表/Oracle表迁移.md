## Oracle表结构迁移
以Oracle中导出的表DDL为例。   
```
create table BOM.DP_SYS_T_RESOURCEITEM
(
  ritem_id              VARCHAR2(48) not null,
  ritem_code            VARCHAR2(75) not null,
  ritem_name            VARCHAR2(75),
  creater               VARCHAR2(48),
  creat_time            TIMESTAMP(6),
  modifyer              VARCHAR2(48),
  modify_time           TIMESTAMP(6)
)
tablespace COMMON_UTIL_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  );
comment on table FAWBOM.DP_SYS_T_RESOURCEITEM
  is '资源元素表';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.ritem_id
  is '内置主健';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.ritem_code
  is '元素编号';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.ritem_name
  is '元素名称';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.creater
  is '创建者';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.creat_time
  is '创建时间';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.modifyer
  is '修改者';
comment on column FAWBOM.DP_SYS_T_RESOURCEITEM.modify_time
  is '修改时间';
alter table FAWBOM.DP_SYS_T_RESOURCEITEM
  add constraint DP_SYS_T_RESOURCEITEM_PK primary key (RITEM_ID)
  using index
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
```
在金仓数据库中执行报错：SQL 错误 [42601]: ERROR: syntax error at or near "pctfree"  
  Position: 611 At Line: 18, Line Position: 3  
错误原因分析		
报错 syntax error at or near "pctfree" 的直接原因是：金仓数据库的 CREATE TABLE 语法不支持 Oracle 中用于定义物理存储属性的 pctfree、storage 等子句。		
尽管金仓数据库有 Oracle 兼容模式，但在物理存储层面，它不提供与 Oracle 完全相同的参数。直接执行包含这些子句的 Oracle 建表语句就会报错。		
解决方案：修改建表语句		
要解决此问题，需要将原语句中 Oracle 特有的物理存储子句全部移除，只保留金仓数据库兼容的核心表结构定义。		
		
关键修改说明		
• 移除 tablespace 子句：原语句中表级和索引级的 tablespace 定义均被移除。如需指定表空间，可在 CREATE TABLE 语句末尾使用 TABLESPACE tablespace_name 子句-		
• 移除 storage 子句：原语句中所有 storage 相关的物理存储参数（如 pctfree, initrans, maxtrans, initial, next 等）均被移除。		
• 移除 pctfree 等参数：pctfree 及其后的数值被完全移除。		
• 主键约束独立：将原 alter table 中的主键约束语句保留并独立执行。金仓数据库支持此语法。		
• 注释语句保留：comment on 语句在金仓数据库中完全兼容，无需修改。		
关于存储参数		
金仓数据库使用不同的方式管理存储：		
表空间：使用 TABLESPACE 子句指定。		
填充因子：对于 PCTFREE 的概念，金仓数据库提供了 fillfactor 存储参数作为替代方案。可以在 CREATE TABLE 语句的 WITH ( ... ) 子句中设置。		
其他参数：initrans、maxtrans、storage 等 Oracle 特有参数在金仓数据库中通常没有直接对应项，或由系统自动管理。		

最终修改原Oracel的SQL，将以下字符去掉。注：因为部分替换内容存在包含关系，所以替换得从没有被其他字符包括的开始替换！！	
替换前字符（去掉前后双引号）
```
"using index
  tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"using index
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"using index
  tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"using index
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"using index
  tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"using index
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255"
"tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255"
"pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  )"
"pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
"pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 16K
    next 8K
    minextents 1
    maxextents unlimited
  )"
```
### Oracle表数据迁移
Oralce查询，并导出为csv文件。注：时间戳列字段需要查询时转换为字符，再导出为csv文件。  
```				
select ritem_id,
       ritem_code,
       ritem_name,
       creater,
      TO_CHAR(creat_time,'yyyy-MM-dd HH24:mi:ss') as creat_time,
      modifyer,
      TO_CHAR(modify_time,'yyyy-MM-dd HH24:mi:ss') modify_time
  from dp_sys_t_resourceitem				
```	