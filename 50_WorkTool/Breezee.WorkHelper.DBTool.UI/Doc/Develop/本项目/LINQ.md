## ����Ŀ-LINQ

### ��������ֵƴ�������õ��ظ��л�Ψһ������
```
// ����ƴ���ֶ��ַ�����Ϊ��������
var separator = string.Empty; // ѡ��һ�����׳��ֵķָ���
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
    drNew[i] = item.g.Key.substring(iIdx); // ���һ��Ԫ��
    drNew[i+1] = item.c; // �ظ���
    dtNew.Rows.Add(drNew);
}
```
### 