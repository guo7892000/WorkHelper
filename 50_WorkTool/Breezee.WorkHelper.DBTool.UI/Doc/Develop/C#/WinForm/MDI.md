## WinForm-MDI
### C#限制MDI子窗体重复打开——C#判断窗体是否已经打开
```
private bool HaveOpened(Form 父窗体, string 子窗体Name)					
{					
    //查看窗口是否已经被打开					
    bool bReturn = true;					
    for (int i = 0; i < 父窗体.MdiChildren.Length; i++)					
    {					
        if (父窗体.MdiChildren[i].Name == 子窗体Name)					
        {					
            父窗体.MdiChildren[i].BringToFront();					
            bReturn = false;					
            break;					
        }					
    }					
    return bReturn;					
}					
					
打开窗体时调用上述方法：					
Form1 f1=new Form();					
if (HaveOpened(父窗体, "子窗体Name"))					
{					
    f1.MdiParent = 父窗体;					
    f1.Show();					
}					
					
通过窗口名判断是否已经打开窗口,如此做的好处就是一个窗体通过不同的Name打开多个这个窗口					
比如说不同的模块打开不同的Mdi父窗体.但相同的Name只能打开一个					
/// <summary>					
/// 判断是否打开Mdi的窗口,如打开则得到窗口					
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
当 MDI 子窗体具有一个 MainMenu 组件（通常带有菜单项的菜单结构），而且它在有 MainMenu 组件（通常带有菜单项的菜单结构）的 MDI 父窗体中打开时，
如果设置了 MergeType 属性（或 MergeOrder 属性），这些菜单项就会自动合并。 将两个 MainMenu 组件的 MergeType 属性和子窗体的所有菜单项设置为 MergeItems。
此外，设置 MergeOrder 属性，以便这两个菜单的菜单项按所需顺序显示。 此外，请记住，关闭 MDI 父窗体时，每个 MDI 子窗体先引发一个 Closing 事件，
再引发 MDI 父窗体的 Closing 事件。 取消 MDI 子窗体的 Closing 事件将不会阻止引发 MDI 父窗体的 Closing 事件；但是，
MDI 父窗体的 Closing事件的 CancelEventArgs 参数将设置为 true。 通过将 CancelEventArgs 参数设置为 false 可以强制 MDI 父窗体和所有 MDI 子窗体关闭。



