/*执行动态SQL并返回值*/
DECLARE @IntVariable int;
DECLARE @SQLString nvarchar(500);
DECLARE @ParmDefinition nvarchar(500);
DECLARE @max_title varchar(30);

SET @IntVariable = 197;
SET @SQLString = N'SELECT @max_titleOUT = max(Title) 
   FROM AdventureWorks.HumanResources.Employee
   WHERE ManagerID = @level';
SET @ParmDefinition = N'@level tinyint, @max_titleOUT varchar(30) OUTPUT';

EXECUTE sp_executesql @SQLString, @ParmDefinition, @level = @IntVariable, @max_titleOUT=@max_title OUTPUT;
SELECT @max_title;
 

