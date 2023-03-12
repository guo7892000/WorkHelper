using Breezee.Core.WinFormUI;
using Breezee.Framework.Mini.IBLL;
using Breezee.Core.IOC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Breezee.Framework.Mini.StartUp
{
    public partial class FrmMiniSystemSet : BaseForm
    {
        IMiniSystem _IMiniSystem;
        public FrmMiniSystemSet()
        {
            InitializeComponent();
        }

        private void FrmMiniSystemSet_Load(object sender, EventArgs e)
        {
            _IMiniSystem = ContainerContext.Container.Resolve<IMiniSystem>();
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {

        }

        private void TsbDBInit_Click(object sender, EventArgs e)
        {
            if (ShowOkCancel("数据库（Mini）将还原到首次登录时状态，请自行做好数据备份。确定继续？") != DialogResult.OK) return;
            _IMiniSystem.InitDB(_dicQuery).SafeGetDictionary();
            ShowInfo("初始化成功！！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
