using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Entity;
using Breezee.Core.Tool;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    public partial class FrmMenuHtmlHelp : BaseForm
    {
        public string SelectHelpPath;
        public FrmMenuHtmlHelp()
        {
            InitializeComponent();
            ShowPopFormMaxBox = true;
            ShowPopFormMinBox = true;
            ShowPopFormBorderStyle = FormBorderStyle.Sizable;//可以调整大小
            Width = 1024;
            Height = 600;
        }

        private void FrmMenuHtmlHelp_Load(object sender, EventArgs e)
        {
            InitMenuTree();//初始化树
            if(!string.IsNullOrEmpty(SelectHelpPath))//查找路径
            {
                foreach (var xn in tvLeftMenu.GetAllLastNode())
                {
                    if (xn.Tag == null) continue;
                    EntMenuHelp entMenuHelp = xn.Tag as EntMenuHelp;
                    if (entMenuHelp.HelpPath.Equals(SelectHelpPath))
                    {
                        tvLeftMenu.SelectedNode = xn;
                        break;
                    }
                }
            }
            tvLeftMenu.ExpandAll();
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitMenuTree()
        {
            if (WinFormContext.Instance.MenuHelpList == null || WinFormContext.Instance.MenuHelpList.Count == 0)
            {
                return;
            }
            
            foreach (var mh in WinFormContext.Instance.MenuHelpList)
            {
                string[] arrMenu = mh.MenuFullPath.Split(new string[] { StaticConstant.FRA_FULL_MENU_PATH_SPLIT_CHAR, ">" }, StringSplitOptions.RemoveEmptyEntries);
                TreeNode[] arrNodes = new TreeNode[arrMenu.Length];

                TreeNodeCollection tnc = tvLeftMenu.Nodes;
                for (int i = 0; i < arrMenu.Length; i++)
                {
                    foreach (TreeNode nd in tnc)
                    {
                        if(nd.Text.Equals(arrMenu[i]))
                        {
                            arrNodes[i] = nd; //有菜单
                            tnc = nd.Nodes; //确定下一次搜索的集合
                            break;
                        }
                    }

                    if(arrNodes[i] == null)//没找到菜单
                    {
                        if(i==0)//一级菜单
                        {
                            arrNodes[i] = tvLeftMenu.Nodes.Add(arrMenu[i]);
                        }
                        else//非一级菜单
                        {
                            arrNodes[i] = arrNodes[i-1].Nodes.Add(arrMenu[i]);
                        }
                        tnc = arrNodes[i].Nodes; //确定下一次搜索的集合
                    }

                    if (i == arrMenu.Length - 1)//最后一个菜单
                    {
                        arrNodes[i].Tag = mh;
                    }
                }
            }
        }

        private void TvLeftMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag!=null)
            {
                EntMenuHelp entMenuHelp = e.Node.Tag as EntMenuHelp;
                Text = string.Format("【{0}】帮助说明", entMenuHelp.MenuName);
                webBrowser1.Navigate("file:///" + Environment.CurrentDirectory + entMenuHelp.HelpPath);
            }
        }
    }
}
