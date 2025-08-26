## WinForm-MDI
### C#����MDI�Ӵ����ظ��򿪡���C#�жϴ����Ƿ��Ѿ���
```
private bool HaveOpened(Form ������, string �Ӵ���Name)					
{					
    //�鿴�����Ƿ��Ѿ�����					
    bool bReturn = true;					
    for (int i = 0; i < ������.MdiChildren.Length; i++)					
    {					
        if (������.MdiChildren[i].Name == �Ӵ���Name)					
        {					
            ������.MdiChildren[i].BringToFront();					
            bReturn = false;					
            break;					
        }					
    }					
    return bReturn;					
}					
					
�򿪴���ʱ��������������					
Form1 f1=new Form();					
if (HaveOpened(������, "�Ӵ���Name"))					
{					
    f1.MdiParent = ������;					
    f1.Show();					
}					
					
ͨ���������ж��Ƿ��Ѿ��򿪴���,������ĺô�����һ������ͨ����ͬ��Name�򿪶���������					
����˵��ͬ��ģ��򿪲�ͬ��Mdi������.����ͬ��Nameֻ�ܴ�һ��					
/// <summary>					
/// �ж��Ƿ��Mdi�Ĵ���,�����õ�����					
/// </summary>					
/// <param name="asFormName"></param>					
/// <returns></returns>					
public Form CheckMdiFormIsOpen(string asFormName)					
{					
    Form form = null;					
    foreach (Form frm in Application.OpenForms)					
    {					
        if (frm.Name == asFormName)					
        {					
            form = frm;					
            break;					
        }					
    }					
    return form;					
}					
					
					
private bool ContainMDIChild(string childTypeString)					
{					
    Form form = null;					
    foreach (Form form2 in base.MdiChildren)					
    {					
        if (form2.GetType().ToString() == childTypeString)					
        {					
            form = form2;					
            break;					
        }					
    }					
    if (form != null)					
    {					
        //form.TopMost = true;					
        form.Show();					
        form.Focus();					
        return true;					
    }					
    return false;					
}					
					
private bool ContainChild(string childTypeString)					
{					
    Form form = null;					
    foreach (Form form2 in Application.OpenForms)					
    {					
        if (form2.GetType().ToString() == childTypeString)					
        {					
            form = form2;					
            break;					
        }					
    }					
    if (form != null)					
    {					
        //form.TopMost = true;					
        form.Show();					
        form.Focus();					
        return true;					
    }					
    return false;					
}					
```					
�� MDI �Ӵ������һ�� MainMenu �����ͨ�����в˵���Ĳ˵��ṹ�������������� MainMenu �����ͨ�����в˵���Ĳ˵��ṹ���� MDI �������д�ʱ��
��������� MergeType ���ԣ��� MergeOrder ���ԣ�����Щ�˵���ͻ��Զ��ϲ��� ������ MainMenu ����� MergeType ���Ժ��Ӵ�������в˵�������Ϊ MergeItems��
���⣬���� MergeOrder ���ԣ��Ա��������˵��Ĳ˵������˳����ʾ�� ���⣬���ס���ر� MDI ������ʱ��ÿ�� MDI �Ӵ���������һ�� Closing �¼���
������ MDI ������� Closing �¼��� ȡ�� MDI �Ӵ���� Closing �¼���������ֹ���� MDI ������� Closing �¼������ǣ�
MDI ������� Closing�¼��� CancelEventArgs ����������Ϊ true�� ͨ���� CancelEventArgs ��������Ϊ false ����ǿ�� MDI ����������� MDI �Ӵ���رա�



