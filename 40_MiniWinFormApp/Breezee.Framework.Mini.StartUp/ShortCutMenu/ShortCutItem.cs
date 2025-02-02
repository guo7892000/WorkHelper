using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Breezee.Framework.Mini.Entity;
using Breezee.Core.Interface;

namespace Breezee.Framework.Mini.StartUp
{
    /// <summary>
    /// 快捷菜单项
    /// </summary>
    public partial class ShortCutItem : UserControl
    {
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemClick;
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemCancel;
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemoMoveFirst;
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemoMoveLast;
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemoMoveBefore;
        public EventHandler<ShortCutItemClickEventArgs> ShortCutItemoMoveBack;

        string strRootPath = AppDomain.CurrentDomain.BaseDirectory;
        string strInFilePah;
        string strOutFilePah;
        MenuEntity _Menu;

        public MenuEntity Menu
        {
            get { return _Menu; }
        }

        public ShortCutItem(MenuEntity dMenu)
        {
            InitializeComponent();
            _Menu = dMenu;

            lblMenuName.Text = dMenu.Name;
        }

        private void ShortCutItem_Load(object sender, EventArgs e)
        {
            strInFilePah = Path.Combine(strRootPath, "Image", "ShortCut_In.png");
            strOutFilePah = Path.Combine(strRootPath, "Image", "ShortCut_Out.png");
            this.BackgroundImage = Image.FromFile(strOutFilePah);
            this.BackgroundImageLayout = ImageLayout.Zoom;

            this.Click += Item_Click;
            lblMenuName.Click += Item_Click;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemClick!=null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemClick(this,arg);
            }
        }

        private void ShortCutItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(strInFilePah);
            this.Cursor = Cursors.Hand;
        }

        private void ShortCutItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(strOutFilePah);
        }

        private void tsmiCancelShortCutMenu_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemCancel!=null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemCancel(this, arg);
            }
        }

        private void tsmiMoveFirst_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemoMoveFirst != null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemoMoveFirst(this, arg);
            }
        }

        private void tsmiMoveLast_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemoMoveLast != null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemoMoveLast(this, arg);
            }
        }

        private void tsmiMoveBefore_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemoMoveBefore != null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemoMoveBefore(this, arg);
            }
        }

        private void tsmiMoveBack_Click(object sender, EventArgs e)
        {
            if (_Menu != null && ShortCutItemoMoveBack != null)
            {
                ShortCutItemClickEventArgs arg = new ShortCutItemClickEventArgs();
                arg.Menu = _Menu;
                ShortCutItemoMoveBack(this, arg);
            }
        }
    }

    public class ShortCutItemClickEventArgs : EventArgs
    {
        public MenuEntity Menu;
    }
}
