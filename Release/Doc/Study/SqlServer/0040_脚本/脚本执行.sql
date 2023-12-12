/*动态执行SQL函数（sp_executesql）示例*/
EXECUTE sp_executesql 
          N'SELECT * FROM pa_part_list 
          WHERE partno = @level',
          N'@level varchar(50)',
          @level = '0009141430-A034';

