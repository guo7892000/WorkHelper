 /*É¾³ýSQL ServerµÄJOBÓï¾ä*/
DECLARE @JobID VARBINARY(100)
IF  EXISTS (SELECT job_id FROM msdb.dbo.sysjobs_view WHERE name = N'P_AJUST_PA_MONTH_TOTAL')
BEGIN
	SELECT @JobID=job_id FROM msdb.dbo.sysjobs_view WHERE name = N'P_AJUST_PA_MONTH_TOTAL'
	EXEC msdb.dbo.sp_delete_job @job_id=@JobID, @delete_unused_schedule=1
END

