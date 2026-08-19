### DBeaver
#### 新增多个生成UUID函数	
在Dbeaver中执行多个创建UUID函数，注要选择：执行脚本，不是执行语句！！
```
CREATE OR REPLACE FUNCTION gen_uuid()
RETURNS varchar
LANGUAGE sql
AS $$
SELECT replace(md5(random()::text || clock_timestamp()::text)::text,'-','');
$$;

CREATE OR REPLACE FUNCTION gen_uuid_36
RETURNS uuid LANGUAGE sql AS $$
SELECT uuid(md5(random()::text || clock_timestamp()::text));
$$;

```

