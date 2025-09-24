## 本项目-LINQ

### 将所有列值拼接起来得到重复行或唯一行数据
```
// 生成拼接字段字符串作为分组依据
var separator = string.Empty; // 选择一个不易出现的分隔符
var query = from data in dtMain.AsEnumerable()
            group data by dic.GetLinqDynamicTableColumnString(data, true,ref separator, stringCovert) into gData
            where isRepeat ? gData.Count() > 1 : gData.Count() == 1
            select new { g = gData, c = gData.Count() };
var rs = query.ToList();
foreach (var item in rs)
{
    Regex regex = new Regex(separator, RegexOptions.IgnoreCase);
    MatchCollection mc = regex.Matches(item.g.Key);
    DataRow drNew = dtNew.NewRow();
    int i = 0;
    int iIdx = 0;
    foreach (Match mat in mc)
    {
        drNew[i] = item.g.Key.substring(iIdx, mat.Index);
        iIdx = mat.Index + mat.Value.Length;
        i++;
    }
    drNew[i] = item.g.Key.substring(iIdx); // 最后一个元素
    drNew[i+1] = item.c; // 重复数
    dtNew.Rows.Add(drNew);
}
```
### 