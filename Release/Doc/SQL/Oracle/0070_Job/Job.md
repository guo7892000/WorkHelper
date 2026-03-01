### JOB
Oracle 10g引入的DBMS_SCHEDULER提供了更强大的调度能力，支持日历表达式、任务依赖等高级功能。  
```
--创建带参数的任务
BEGIN
  DBMS_SCHEDULER.CREATE_JOB(
    job_name => 'PARAM_JOB',
    job_type => 'STORED_PROCEDURE',
    job_action => 'PROCESS_DATA',
    number_of_arguments => 1,
    start_date => SYSTIMESTAMP,
    repeat_interval => 'FREQ=DAILY',
    enabled => FALSE
  );
  
  -- 设置参数
  DBMS_SCHEDULER.SET_JOB_ARGUMENT_VALUE(
    job_name => 'PARAM_JOB',
    argument_position => 1,
    argument_value => 'INPUT_DATA'
  );
  
  -- 启用任务
  DBMS_SCHEDULER.ENABLE('PARAM_JOB');
END;
/
```
### 示例-固定10、12、15、17、23时8分执行
```
begin
 sys.dbms_scheduler.create_job(job_name            => 'PA.JOB_GET_TEST',
                               job_type            => 'STORED_PROCEDURE',
                               job_action          => 'PKG_IF_TEST.P_TEST_01',
                               start_date          => to_date('10-01-2026 14:16:14', 'dd-mm-yyyy hh24:mi:ss'),
                               repeat_interval     => 'Freq=Hourly;ByHour=10,12,15,17,23;ByMinute=8',
                               end_date            => to_date(null),
                               job_class           => 'DEFAULT_JOB_CLASS',
                               enabled             => true,
                               auto_drop           => false,
                               comments            => '增量获取数据');
end;
/
```
### 示例-每小时执行语句块
```
BEGIN
   DBMS_SCHEDULER.CREATE_JOB (
       job_name        => 'my_job',
       job_type        => 'PLSQL_BLOCK',
       job_action      => 'BEGIN proc1; proc2; END;',
       start_date      => SYSTIMESTAMP,
       repeat_interval => 'FREQ=HOURLY; BYHOUR=1-24',  -- 每天每小时运行一次
       enabled         => TRUE,
       comments        => 'Runs proc1 and proc2 every hour'
   );
END;
/
```
