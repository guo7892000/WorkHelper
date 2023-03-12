using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Breezee.Core.Tool;
using System.IO;
using Breezee.Core.WinFormUI;

namespace Breezee.Framework.Mini.StartUp
{
    public partial class FrmDllManage : BaseForm
    {
        DLLSet _dllSet;
        public FrmDllManage()
        {
            InitializeComponent();
        }

        private void FrmDllManage_Load(object sender, EventArgs e)
        {
            _dllSet = new DLLSet();
            SetTag();
            
            DataTable dtDll = _dllSet.LoadXMLFile();
            dgvQuery.BindDataGridView(dtDll, true);
            dgvQuery.AllowUserToAddRows = true;//允许新增行
        }

        #region 设置Tag方法
        private void SetTag()
        {
            //设置Tag
            FlexGridColumnDefinition fdc = new FlexGridColumnDefinition();
            fdc.AddColumn("Code", "DLL名称", DataGridViewColumnTypeEnum.TextBox, true, 400, DataGridViewContentAlignment.MiddleLeft, true, 800);
            fdc.AddColumn("Name", "备注", DataGridViewColumnTypeEnum.TextBox, true, 400, DataGridViewContentAlignment.MiddleLeft, true, 800);
            dgvQuery.Tag = fdc.GetGridTagString();
            dgvQuery.BindDataGridView(null, true);
        }
        #endregion

        private void tsbSave_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();//一定要离开网格焦点，新增的行才生效
            DataTable dtSource = dgvQuery.GetBindingTable();
            
            if (dtSource==null || dtSource.Rows.Count==0)
            {
                ShowErr("没有要保存的数据！");
                return;
            }

            foreach (DataRow dr in dtSource.Rows)
            {
                string sPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dr["Code"].ToString());
                if(!File.Exists(sPath))
                {
                    ShowErr("所输入的dll或exe文件在程序运行目录中不存在，请修正！详细路径为："+ sPath);
                    return;
                }
            }


            _dllSet.SaveXMLFile(dtSource, _dllSet.GetFileName());
            ShowInfo("保存成功！");
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            dgvQuery.Rows.Remove(dgvQuery.CurrentRow);
        }
    }
}
