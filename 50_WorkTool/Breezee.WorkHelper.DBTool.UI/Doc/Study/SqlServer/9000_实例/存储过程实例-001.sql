/*创建存储过程例子*/
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
*	V1.7.06: 由CLR存储过程转换为SQL存储过程(CLR布署太麻烦了) hgh 2012-12-05 
***********************************************************************************/ 
BEGIN
	DECLARE @curTemp CURSOR
	SET @curTemp = Cursor For SELECT In_Id 
		FROM  AA		  
	OPEN @curTemp
	FETCH NEXT FROM @curTemp INTO @In_Id 		  
	WHILE @@fetch_status = 0 
		BEGIN		
					  
			FETCH NEXT FROM @curTemp INTO @In_Id
		END 	
	close @curTemp
	deallocate @curTemp 	
END


