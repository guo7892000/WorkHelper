/*游标嵌套*/
DECLARE @curTemp CURSOR
	SET @curTemp = Cursor For SELECT In_Id 
		FROM  AA		  
	OPEN @curTemp
	FETCH NEXT FROM @curTemp INTO @In_Id 		  
	WHILE @@fetch_status = 0 
		BEGIN		
			DECLARE @curButton CURSOR
			SET @curButton	= Cursor For SELECT BUTTON_NAME 
				FROM BBB
				WHERE In_Id=@In_Id
			
			 /*按钮游标*/
			 OPEN @curButton
				FETCH NEXT FROM @curButton INTO @BUTTON_NAME  
				WHILE @@fetch_status = 0 
					BEGIN   
						/*增加按钮表记录*/
						
				FETCH NEXT FROM @curButton INTO @BUTTON_NAME
				
			END 
			close @curButton
			deallocate @curButton 			  
	FETCH NEXT FROM @curTemp INTO @In_Id
END 	
close @curTemp
deallocate @curTemp 


