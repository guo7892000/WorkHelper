### DBMS_JOB
DBMS_JOB是Oracle早期版本提供的定时任务工具，适用于11g及更早版本。其核心流程包括存储过程创建、任务提交和生命周期管理三个阶段。 
```
--查看任务：
select * from user_jobs;
select * from all_jobs;
--查看正在运行的任务（不推荐使用，速度慢）：
select * from dba_jobs_running;

/*添加JOB*/
DECLARE JOBID NUMBER;
BEGIN
  SELECT MAX(JOB)+1 INTO JOBID FROM ALL_JOBS;
  DBMS_JOB.SUBMIT(JOBID, 'P_GD_DELETE_OVERDUE_LACK_AUTO;', SYSDATE,'TRUNC(SYSDATE+1)');
  COMMIT;
END;

/*修改JOB*/
declare jobID number;
begin
  select JOB into jobID from all_jobs where what='P_GD_IR_DAY_ONCE;';
  dbms_job.change(jobID ,'P_GD_IR_DAY_ONCE;', sysdate+1,'sysdate+1/24' );
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

```
### 创建有参数的JOB
注：:job表示在执行时，需要输入JOB的ID。但实际生成的ID不是使用输入值。
```
begin
 sys.dbms_job.submit(job => :job,
                     what => 'declare
 V_RETURN_CODE varchar2(100);
 V_ERROR_MESSAGE varchar2(4000);
 V_SQLERRM varchar2(4000);
begin
 PKG_IF_SAP.P_REC_ITEM_PRD_QTY_REDO(O_RETURN_CODE => V_RETURN_CODE,
                                    O_ERROR_MESSAGE => V_ERROR_MESSAGE,
                                    O_SQLERRM => V_SQLERRM);
end;',
                     next_date => to_date('22-12-2025', 'dd-mm-yyyy'),
                     interval => 'sysdate+2/24');
 commit;
end;
/
```