/*修改表增加列*/
if Not Exists(Select 1 From sysobjects a,syscolumns b 
Where a.id = b.id And a.name = 'PA_STORAGE' And b.name = 'RDCQTY' And a.type = 'U')
ALTER TABLE PA_STORAGE ADD RDCQTY DECIMAL(14,2) /*RDC库数量*/
;

/*增加指定的自增长列值记录*/
Set   IDENTITY_INSERT   Temp_Test   ON   
INSERT   INTO   Temp_Test(CHEKCID,CHEKCLISTNO,CheckModel )   
SELECT   CHEKCID,CHEKCLISTNO,CheckModel    
FROM dbo.PA_STORAGE_CHECK_M WHERE CHEKCLISTNO='PC100903'
Set   IDENTITY_INSERT   Temp_Test   Off

/*创建新表*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OP_PLAN_Audit]') AND type in (N'U'))
CREATE TABLE [dbo].[OP_PLAN_Audit] (
[ID]  [BIGINT ] IDENTITY(1,1) NOT NULL,
[YEAR]  [int ] NULL,
[MONTH]  [int ] NULL,
[ISENABLE]  [VARCHAR ](1) NULL,
[ISSYSTEM]  [VARCHAR ](1) NULL,
[NETCODE]  [VARCHAR ](10) NULL,
CONSTRAINT [ OP_Audit_ID] PRIMARY KEY CLUSTERED  ( [ID] ASC ))  
GO

---创建统计用到的表PA_TEMP_IN_OUT_TOTAL，如存在，则删除后重建
  IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PA_TEMP_IN_OUT_TOTAL]') AND type in (N'U'))
    TRUNCATE TABLE PA_TEMP_IN_OUT_TOTAL;
 CREATE TABLE PA_TEMP_IN_OUT_TOTAL
 (
      PARTNO VARCHAR(50),/*备件编号*/
      PARTNAME VARCHAR(50),/*备件名称*/
      UNIT VARCHAR(50),/*单位*/
      JPXSLR DECIMAL(14, 2)/*精品销售利润*/
 )

 /*判断临时表是否存在*/
 if object_id('tempdb..#TEMP_TABLE') is not null 
 print '临时表#TEMP_TABLE已存在!' 

 /*增加启辰品牌：如有ID为2的记录先删除再新增*/
IF EXISTS(SELECT * FROM SE_CARBRAND WHERE CARBRAND_ID=2)
DELETE FROM SE_CARBRAND WHERE CARBRAND_ID=2
GO
SET IDENTITY_INSERT SE_CARBRAND ON
INSERT INTO SE_CARBRAND(CARBRAND_ID ,
        CARBRAND_CODE ,
        BRANDNAME ,
        REMARK ,
        ORDERNO ,
        IS_HD ,
        CREATETIME ,
        CREATOR ,
        LAST_UPDATED_DATE ,
        MODIFIER ,
        ISENABLE ,
        ISSYSTEM ,
        NETCODE)
SELECT 2,--CARBRAND_ID ,
       2,--CARBRAND_CODE ,
        '启辰',--BRANDNAME ,
        '',--REMARK ,
        '2',--ORDERNO ,
        '1',--IS_HD ,
        GETDATE(),--CREATETIME ,
        'Sys',--CREATOR ,
        GETDATE(),--LAST_UPDATED_DATE ,
        'Sys',--MODIFIER ,
        '1',--ISENABLE ,
        '0',--ISSYSTEM ,
        NETCODE 
FROM dbo.BD_SYSINFO WHERE DLR_TYPE=1
SET IDENTITY_INSERT SE_CARBRAND OFF
GO

/*修改表或列说明*/
IF EXISTS(SELECT * FROM sys.extended_properties A
JOIN sys.objects B ON a.major_id=b.object_id
LEFT JOIN sys.all_columns C ON A.minor_id=C.column_id AND B.object_id = C.object_id
WHERE B.name='IFS_BAS_USER' AND (C.name IS NULL OR C.name='MQSENDSTATUS'))
BEGIN
	EXEC sys.sp_dropextendedproperty @name = N'MS_Description',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'IFS_BAS_USER',  
    @level2type = N'COLUMN', @level2name = N'MQSENDSTATUS' 
END
EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'传输标志：0未上传(默认)，1已上传',
    @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'IFS_BAS_USER',  
    @level2type = N'COLUMN', @level2name = N'MQSENDSTATUS'  

