/*创建存储过程例子(含写日志)*/
/*删除存储过程*/
IF ( OBJECT_ID('IFR_UC_T_CAR_SERIESProcedure') IS NOT NULL ) 
	DROP PROCEDURE IFR_UC_T_CAR_SERIESProcedure
GO	
/*创建存储过程*/
CREATE PROCEDURE IFR_UC_T_CAR_SERIESProcedure 
AS
/***********************************************************************************
* 存储过程名称: 二手车车系接收
* 创建作者: 黄国辉
* 创建日期: 2012-12-05   
* 使用系统: DMS
* 使用模块: 二手车管理
* 说明：二手车车系接收
* 修改历史说明：
*	2012-12-05: 由CLR存储过程转换为SQL存储过程(CLR布署太麻烦了) hgh  
***********************************************************************************/ 
BEGIN
	/*变量声明*/
	DECLARE @NETCODE VARCHAR(10),
			@FUN_NAME VARCHAR(200) ,
			@IF_NO VARCHAR(50) ,    
			@IF_NAME VARCHAR(200),  
			@IF_TYPE VARCHAR(1),    
			@MESSAGE VARCHAR(MAX),
			@curTemp CURSOR
    
	/*接口信息*/  
	SELECT  @FUN_NAME = '服务管理', 
			@IF_NO = 'DMS_SE_E3SS_006',
			@IF_NAME = '有偿单台KPI数据上传',
			@IF_TYPE = 'N',
			@MESSAGE = ''
    /*取一网编码*/	
	SELECT @NETCODE=NETCODE FROM dbo.BD_SYSINFO WHERE DLR_TYPE='1'
	/*设置游标*/ 
	SET @curTemp = Cursor For SELECT In_Id FROM  AA
	OPEN @curTemp
	FETCH NEXT FROM @curTemp INTO @In_Id 		  
	WHILE @@fetch_status = 0 
		BEGIN
			BEGIN TRY		
				/*游标内逻辑处理:出错可以用:GOTO Final_Deal*/
				/**此处略******/
				
				/*写入处理成功信息*/
				SELECT  @MESSAGE = 'E3S出库单' +@E3S_OUT_CODE+ '接收成功！',@IF_TYPE = 'N'
			END TRY
			BEGIN CATCH 
				IF @@trancount > 0 
					BEGIN
						ROLLBACK TRAN
					END
					SELECT  @MESSAGE = ERROR_MESSAGE()+'. Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10)),@IF_TYPE = 'E'
					SET @MESSAGE = 'E3S出库单: '+ CAST(@E3S_OUT_CODE AS VARCHAR(50)) + '接收失败！'+ @MESSAGE
			END CATCH
			/*定义最终处理标签*/
			Final_Deal:
			/*更新接口表记录为已处理状态1*/        
			UPDATE dbo.IFR_PA_E3S_OUTSTOCK SET MQSENDSTATUS='1'
			WHERE NETCODE=@NETCODE AND OUTSTORAGENO=@E3S_OUT_CODE
			/*提交事务*/
			IF @@trancount > 0 
			BEGIN
				COMMIT TRAN
			END      
			--写日志    
			EXEC PROC_LOG_MESSAGE @FUN_NAME_ = @FUN_NAME,@IF_NO_ = @IF_NO, @IF_NAME_ = @IF_NAME, @SCENS_ = '',
				   @DIRECT_ = 'SR', @IF_TYPE_ = @IF_TYPE,@REMARK_ = @MESSAGE		  
			/*处理下一条游标记录*/
			FETCH NEXT FROM @curTemp INTO @In_Id
		END	
	--关闭游标	        	
	close @curTemp
	deallocate @curTemp 	
END