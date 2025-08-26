## LINQ
### ʾ��1
```
//��ѯ
var query = from f in dtMain.AsEnumerable()
            join s in dtSec.AsEnumerable()
            on dic.GetLinqDynamicTableColumnObj(f, true)
            equals dic.GetLinqDynamicTableColumnObj(s, false)
            where ckbNullNotEquals.Checked ? GetLinqDynamicWhere(dic, f, s) : true
            select new { F1 = f, S1 = s };

//��ѯ��������
var joinList = query.ToList();
var restult = joinList.Select(t => t.F1.ItemArray.Concat(t.S1.ItemArray).ToArray()); //����������Ҫ����ToArray
foreach (var item in restult)
{
    dtResult.Rows.Add(item); //���������ݣ�����̫��ʱ������ᱨ�ڴ����System.OutOfMemoryException����
}

public static object GetLinqDynamicTableColumnObj(this IDictionary<string, string> dic, DataRow dr, bool isKey)
{
    int iCondCount = dic.Count();
    object result = null;

    if (iCondCount == 1)
    {
        result = isKey ? new 
        { 
            c1 = dr.Field<string>(dic.ElementAt(0).Key) 
        } : 
        new 
        { 
            c1 = dr.Field<string>(dic.ElementAt(0).Value) 
        };
    }
    else if (iCondCount == 2)
    {
        result = isKey ? new
        {
            c1 = dr.Field<string>(dic.ElementAt(0).Key),
            c2 = dr.Field<string>(dic.ElementAt(1).Key)
        } :
        new
        {
            c1 = dr.Field<string>(dic.ElementAt(0).Value),
            c2 = dr.Field<string>(dic.ElementAt(1).Value)
        };
    }
    else 
    {
        // ...
    }
}
```
### ʾ��2
```
DataTable dSplitChars = dgvSplitChar.GetBindingTable();
var splitListFixErr = from f in dSplitChars.AsEnumerable()
                    where int.TryParse(f.Field<string>("A"), out int iRigth) == false
                    select f.Field<string>("A");
sSplitCharArr = splitListFixErr.ToArray();
```
