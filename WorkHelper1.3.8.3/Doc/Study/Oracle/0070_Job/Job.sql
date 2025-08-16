--查看任务：
select * from user_jobs;
select * from all_jobs;
--查看正在运行的任务（不推荐使用，速度慢）：
select * from dba_jobs_running;

/*添加JOB*/
DECLARE JOBID NUMBER;
BEGIN
  SELECT MAX(JOB)+1 INTO JOBID FROM ALL_JOBS;
  DBMS_JOB.SUBMIT(JOBID, 'P_PA_DELETE_OVERDUE_LACK_AUTO;', SYSDATE,'TRUNC(SYSDATE+1)');
  COMMIT;
END;

/*修改JOB*/
declare jobID number;
begin
  select JOB into jobID from all_jobs where what='P_SE_IR_DAY_ONCE;';
  dbms_job.change(jobID ,'P_SE_IR_DAY_ONCE;', sysdate+1,'sysdate+1/24' );
  commit;
end;

/*JOB的其他命令*/
begin
 dbms_job.remove(41); --删除JO
 dbms_job.broken(25,true); --停止job
 dbms_job.run(25); --运行job
 dbms_job.what(v_job,'sp_fact_charge_code;'); --修改What内容
 dbms_job.next_date(v_job,sysdate); --修改某个job名 修改下一次运行时间
end;

