using Breezee.Core.WinFormUI;
using Breezee.Core.IOC;
using Breezee.WorkHelper.DBTool.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    public partial class FrmDBTSystemSet : BaseForm
    {
        IDBTSystemSet _IDBTSystemSet;

        public FrmDBTSystemSet()
        {
            InitializeComponent();
        }

        private void FrmDBTSystemSet_Load(object sender, EventArgs e)
        {
            _IDBTSystemSet = ContainerContext.Container.Resolve<IDBTSystemSet>();
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {

        }

        private void TsbDBInit_Click(object sender, EventArgs e)
        {
            if (ShowOkCancel("数据库（DBTool）将还原到首次登录时状态，请自行做好数据备份。确定继续？") != DialogResult.OK) return;
            _IDBTSystemSet.InitDB(_dicQuery);
            ShowInfo("初始化成功！！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
