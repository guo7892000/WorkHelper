### JOB
Oracle 10g引入的DBMS_SCHEDULER提供了更强大的调度能力，支持日历表达式、任务依赖等高级功能。  
```
-- 创建带参数的任务
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
