/*增加列*/
  ALTER TABLE emp01 ADD eno NUMBER(4);

/*修改列定义*/
  ALTER TABLE emp01 MODIFY job VARCHAR2(15)  DEFAULT 'CLERK';

/*删除列*/
  ALTER TABLE emp01 DROP COLUMN dno;

/*修改列名*/
  ALTER TABLE emp01 RENAME COLUMN eno TO empno;

/*修改表名*/
  RENAME emp01 TO employee;

/*先删除后增加主键*/
ALTER TABLE MQ_PS_PREPARE DROP CONSTRAINT PK_MQ_PS_PREPARE;
ALTER TABLE MQ_PS_PREPARE ADD CONSTRAINT PK_MQ_PS_PREPARE PRIMARY KEY (OID);
/*操作表常用SQL*/
COMMENT ON TABLE TEST1 IS '测试表';--给表填加注释
COMMENT ON COLUMN T_AI_BU_INSURE_ORDER.LINK_NO IS '合同单号';--给列加注释
ALTER TABLE TEST1 MODIFY PLANMONTH NUMBER;
ALTER TABLE TEST1 ADD LONG1 LONG;
ALTER TABLE TEST1 ADD DFD NUMBER(14,2) DEFAULT 0;
ALTER TABLE TEST1 ADD FDFED NUMBER DEFAULT 1;
ALTER TABLE TEST1 ADD CONSTRAINT PK_ID PRIMARY KEY (ID);
ALTER TABLE TEST1 ADD CONSTRAINT FK_D_ID FOREIGN KEY (D_ID) REFERENCES TEST2 (RID);
ALTER TABLE TEST1 ADD CONSTRAINT U_DFD UNIQUE (PLANMONTH);
ALTER TABLE TEST1 DROP COLUMN PLANMONTH;

-- Create table
create table T_PA_BU_DLR_OUT_STORE
(
  out_store_id               VARCHAR2(36) not null,
  out_store_code             VARCHAR2(50) not null,
  bill_type                  VARCHAR2(10) not null,
  relate_order_id            VARCHAR2(36),
  relate_order_code          VARCHAR2(50),
  cust_id                    VARCHAR2(50),
  cust_name                  VARCHAR2(200),
  vin                        VARCHAR2(17),
  out_store_date             DATE,
  picking_person_name        VARCHAR2(100),
  balance_month              VARCHAR2(6),
  out_store_status           VARCHAR2(10) default '0',
  applier_name               VARCHAR2(100),
  remark                     VARCHAR2(200),
  balance_status             VARCHAR2(2),
  balance_date               DATE,
  car_brand_code             VARCHAR2(50),
  car_license                VARCHAR2(50),
  relate_out_store_code      VARCHAR2(50),
  relate_out_store_link_code VARCHAR2(50),
  dlr_id                     VARCHAR2(36) not null,
  creator                    VARCHAR2(50) not null,
  created_date               TIMESTAMP(6) default systimestamp not null,
  modifier                   VARCHAR2(50) not null,
  last_updated_date          TIMESTAMP(6) default systimestamp not null,
  is_enable                  VARCHAR2(50) default '1' not null,
  sdp_user_id                VARCHAR2(36) default nvl(SYS_CONTEXT('SDP_CONTEXT','userid'),'88888') not null,
  sdp_org_id                 VARCHAR2(36) default nvl(SYS_CONTEXT('SDP_CONTEXT','orgid'),'2') not null,
  update_control_id          VARCHAR2(36) default sys_guid() not null,
  picking_person_id          VARCHAR2(100),
  relate_out_store_id        VARCHAR2(36),
  relate_out_store_link_id   VARCHAR2(36),
  bill_big_type              VARCHAR2(10),
  applier_id                 VARCHAR2(36),
  tax_rate                   NUMBER(14,4),
  is_new_car_acce_out        VARCHAR2(2) default 0,
  sale_order_code            VARCHAR2(50),
  car_type_code              VARCHAR2(50),
  use_dept_id                VARCHAR2(36),
  draw_type_code             VARCHAR2(10),
  is_account_check           VARCHAR2(2) default 0 not null,
  car_series_code            VARCHAR2(50),
  transfer_in_dlr_id         VARCHAR2(36),
  audit_flag                 VARCHAR2(2),
  is_archived                VARCHAR2(2),
  interface_update_date      TIMESTAMP(6)
);
-- Add comments to the table 
comment on table T_PA_BU_DLR_OUT_STORE
  is '专营店备件出退库单(DLR)';
-- Add comments to the columns 
comment on column T_PA_BU_DLR_OUT_STORE.out_store_id
  is '专营店备件出库单ID';
comment on column T_PA_BU_DLR_OUT_STORE.out_store_code
  is '出库单编号';
comment on column T_PA_BU_DLR_OUT_STORE.bill_type
  is '单据类型  出库类型：1  维修出库 2  售后精品销售出库 3  网点调拨出库 4  内部领用出库 5  辅料领用出库 6  备件报废出库 7  网点内部调拨出库 8  备件盘亏出库 9  新车精品销售出库；10  服务二网销售出库；11  锦上阳光销售出库；12  供应商发货出库；13 报修出库  出库退库类型：101 维修出库退货、102 售后精品销售出库退货、103 网点调拨出库退货、104 内部领用出库退货、105 辅料领用出库退货、109 新车精品销售出库退货、110  服务二网销售出库退货、111 锦上阳光销售退库';
comment on column T_PA_BU_DLR_OUT_STORE.relate_order_id
  is '关联单据ID  派工单 售后精品销售单 网点调拔单 网点内部调拔单 库存盘点单 新车精品销售单';
comment on column T_PA_BU_DLR_OUT_STORE.relate_order_code
  is '关联单据编码  派工单 售后精品销售单 网点调拔单 网点内部调拔单 库存盘点单 新车精品销售单';
comment on column T_PA_BU_DLR_OUT_STORE.cust_id
  is '客户ID';
comment on column T_PA_BU_DLR_OUT_STORE.cust_name
  is '客户名称  维修出库：派工的客户姓名 售后精品销售出库：精品销售单的客户姓名 网点调拔出库：目标网点 辅料领用出库:维修技师 内部领用出库:使用人员 报废出库：当前系统登录人 网点内部调拔出库：对方网点 盘亏出库：当前系统登录人';
comment on column T_PA_BU_DLR_OUT_STORE.vin
  is 'VIN码';
comment on column T_PA_BU_DLR_OUT_STORE.out_store_date
  is '出库日期';
comment on column T_PA_BU_DLR_OUT_STORE.picking_person_name
  is '领料人员  维修出库，售后精品销售出库，新车精品销售出库，必填，其它出库可不填';
comment on column T_PA_BU_DLR_OUT_STORE.balance_month
  is '月结月份';
comment on column T_PA_BU_DLR_OUT_STORE.out_store_status
  is '出库单状态  0表示未入库 1表示部分入库 2表示完全入库';
comment on column T_PA_BU_DLR_OUT_STORE.applier_name
  is '申请人姓名';
comment on column T_PA_BU_DLR_OUT_STORE.remark
  is '备注';
comment on column T_PA_BU_DLR_OUT_STORE.balance_status
  is '维修派工结算后回写结算标志。';
comment on column T_PA_BU_DLR_OUT_STORE.balance_date
  is '结算时间';
comment on column T_PA_BU_DLR_OUT_STORE.car_brand_code
  is '车辆品牌';
comment on column T_PA_BU_DLR_OUT_STORE.car_license
  is '车牌号';
comment on column T_PA_BU_DLR_OUT_STORE.relate_out_store_code
  is '关联出库单号：针对出库退货，保存原出库单号';
comment on column T_PA_BU_DLR_OUT_STORE.relate_out_store_link_code
  is '关联出库源单号：针对出库退货，保存工单号、售后精品单号、新车精品单号';
comment on column T_PA_BU_DLR_OUT_STORE.dlr_id
  is '专营店ID';
comment on column T_PA_BU_DLR_OUT_STORE.creator
  is '创建人';
comment on column T_PA_BU_DLR_OUT_STORE.created_date
  is '创建时间';
comment on column T_PA_BU_DLR_OUT_STORE.modifier
  is '最后更新人员';
comment on column T_PA_BU_DLR_OUT_STORE.last_updated_date
  is '最后更新时间';
comment on column T_PA_BU_DLR_OUT_STORE.is_enable
  is '是否可用';
comment on column T_PA_BU_DLR_OUT_STORE.sdp_user_id
  is 'SDP用户ID';
comment on column T_PA_BU_DLR_OUT_STORE.sdp_org_id
  is 'SDP组织ID';
comment on column T_PA_BU_DLR_OUT_STORE.update_control_id
  is '并发控制字段';
comment on column T_PA_BU_DLR_OUT_STORE.picking_person_id
  is '领料人ID';
comment on column T_PA_BU_DLR_OUT_STORE.relate_out_store_id
  is '关联出库ID 针对出库退货，保存原出库主单ID';
comment on column T_PA_BU_DLR_OUT_STORE.relate_out_store_link_id
  is '关联出库源单ID 针对出库退货，保存原出库来源的主单ID。例如维修工单ID、售后精品销售单ID、新车精品销售单ID';
comment on column T_PA_BU_DLR_OUT_STORE.bill_big_type
  is '单据大类 3：出库 4：出库退货';
comment on column T_PA_BU_DLR_OUT_STORE.applier_id
  is '申请人ID';
comment on column T_PA_BU_DLR_OUT_STORE.tax_rate
  is '税率';
comment on column T_PA_BU_DLR_OUT_STORE.is_new_car_acce_out
  is '是否新车精品出库  0否，1是';
comment on column T_PA_BU_DLR_OUT_STORE.sale_order_code
  is '销售订单号';
comment on column T_PA_BU_DLR_OUT_STORE.car_type_code
  is '车型编码';
comment on column T_PA_BU_DLR_OUT_STORE.use_dept_id
  is '使用部门ID';
comment on column T_PA_BU_DLR_OUT_STORE.draw_type_code
  is '内部领用类型  内部领用类型:办公用品、市场礼品、工具件...';
comment on column T_PA_BU_DLR_OUT_STORE.is_account_check
  is '是否已对账  0未对账  1已对账 2对账中';
comment on column T_PA_BU_DLR_OUT_STORE.car_series_code
  is '车系编码';
comment on column T_PA_BU_DLR_OUT_STORE.transfer_in_dlr_id
  is '调入专营店ID';
comment on column T_PA_BU_DLR_OUT_STORE.audit_flag
  is '审核标志';
comment on column T_PA_BU_DLR_OUT_STORE.is_archived
  is '归档标志';
-- Create/Recreate indexes 
create index IDX01_T_PA_BU_DLR_OUT_STORE on T_PA_BU_DLR_OUT_STORE (OUT_STORE_CODE);
create index IDX9_T_PA_BU_DLR_OUT_STORE on T_PA_BU_DLR_OUT_STORE (DLR_ID, RELATE_ORDER_CODE)
  nologging  local;
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_PA_BU_DLR_OUT_STORE
  add constraint PK_T_PA_BU_DLR_OUT_STORE1 primary key (OUT_STORE_ID)
;
-- Grant/Revoke object privileges 
grant select on T_PA_BU_DLR_OUT_STORE to DEVELOPERS_FOR_E3S;
grant select, insert, update, delete on T_PA_BU_DLR_OUT_STORE to MDS;
grant select, insert, update, delete on T_PA_BU_DLR_OUT_STORE to FI with grant option;
grant select, insert, update, delete, references, alter, index, debug on T_PA_BU_DLR_OUT_STORE to IFR;
