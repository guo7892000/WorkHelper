using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// GroupBox的扩展类
    /// </summary>
    public static class GroupBoxExtend
    {
        /// <summary>
        /// 增加右键折叠菜单
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static void AddFoldRightMenu(this GroupBox cb, GroupBoxFoldInParam foldParam = null)
        {
            if(cb.Tag == null)
            {
                GroupBoxFoldParam groupBoxFoldParam = new GroupBoxFoldParam();
                groupBoxFoldParam.SourceHeight = cb.Height; //记录原高度
                if (foldParam != null )
                {
                    groupBoxFoldParam.InParam = foldParam;
                }
                else
                {
                    groupBoxFoldParam.InParam = new GroupBoxFoldInParam();
                }
                cb.Tag = groupBoxFoldParam;
                groupBoxFoldParam.SourceGroupBox = cb;
                groupBoxFoldParam.SourceParentHeight = cb.Parent.Height;
                //右键菜单处理
                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem tmsi = new ToolStripMenuItem();
                tmsi.Text = "折叠";
                tmsi.Tag = groupBoxFoldParam;
                tmsi.Click += OnFoldExpand_Click;
                cms.Items.Add(tmsi);
                
                cb.ContextMenuStrip = cms;
            }
        }

        public static void AddFoldRightMenu(this GroupBox cb, bool isParentFoldToo)
        {
            if (isParentFoldToo)
            {
                GroupBoxFoldInParam param = new GroupBoxFoldInParam();
                param.IsParentFold = true;
                cb.AddFoldRightMenu(param); //增加右键折叠功能
            }
            else
            {
                AddFoldRightMenu(cb);
            }
        }

        private static void OnFoldExpand_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tmsi = sender as ToolStripMenuItem;
            if (tmsi == null)
            {
                return;
            }

            GroupBoxFoldParam foldParam = tmsi.Tag as GroupBoxFoldParam;
            GroupBox cb = foldParam.SourceGroupBox;
            if (foldParam.IsFold)
            {
                //本次为展开
                cb.Height = foldParam.SourceHeight;
                tmsi.Text = foldParam.InParam.FoldText; //点击后为折叠
                if (foldParam.InParam.IsParentFold)
                {
                    cb.Parent.Height = foldParam.SourceParentHeight;
                }
            }
            else
            {
                //本次为折叠
                cb.Height = foldParam.InParam.FoldHeight;
                tmsi.Text = foldParam.InParam.ExpandText; //点击后为展开
                if (foldParam.InParam.IsParentFold)
                {
                    cb.Parent.Height = foldParam.InParam.ParentHeight;
                }
            }
            foldParam.IsFold = !foldParam.IsFold;
        }

        public class GroupBoxFoldInParam
        {
            public int FoldHeight = 15;  //折叠时GroupBox的高度，默认为15，可自行设置。
            public string FoldText = "折叠";
            public string ExpandText = "展开";
            public bool IsParentFold = false;
            public int ParentHeight = 16;  //折叠时GroupBox父类对象的高度，默认为15，可自行设置。
        }
        private class GroupBoxFoldParam
        {
            public GroupBoxFoldInParam InParam;
            public int SourceHeight; //未折叠时GroupBox的高度
            public bool IsFold = false; //是否折叠：默认为否，即显示
            public GroupBox SourceGroupBox; //所属GroupBox
            public int SourceParentHeight; //未折叠时GroupBox父类对象的高度
            //public Control SourceParent;
        }
    }
}
