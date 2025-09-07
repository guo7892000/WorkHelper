using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 功能名称：软件开发总结
    /// 使用场景：
    /// 最后更新日期：2025-08-15
    /// 修改人员：黄国辉
    /// </summary>
    public partial class FrmDBTCodeDevelop : BaseForm
    {
        private string sRootDir;
        public FrmDBTCodeDevelop()
        {
            InitializeComponent();
        }

        private void FrmDBTExchangeStringPlace_Load(object sender, EventArgs e)
        {
            //SQL学习文本根路径
            sRootDir = Path.Combine(GlobalContext.AppBaseDirectory, "Doc", "Develop");

            DataTable dtEncode = BaseFileEncoding.GetEncodingTable(false);
            cbbCharSetEncode.BindTypeValueDropDownList(dtEncode, false, true);

            _dicString.Add("1", "MD格式");
            _dicString.Add("2", "文本格式");
            cbbShowType.BindTypeValueDropDownList(_dicString.GetTextValueTable(false), false, true);
            richTextBox1.Visible = false;

            toolTip1.SetToolTip(cbbCharSetEncode, "如文件出现乱码，需要修改文件字符集！");;
            //加载配置
            cbbCharSetEncode.SelectedValue = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.SQLStudy_FileCharsetEncoding, BaseFileEncoding.FileEncodingString.GB2312).Value;
            //加载树数据
            LoadTreeViewData();
        }

        private void LoadTreeViewData()
        {
            if (!Directory.Exists(sRootDir)) return;
            string sEncode = string.Empty;
            if (cbbCharSetEncode.SelectedValue == null || string.IsNullOrEmpty(cbbCharSetEncode.SelectedValue.ToString()))
            {
                sEncode = BaseFileEncoding.FileEncodingString.GB2312;
            }
            else
            {
                sEncode = cbbCharSetEncode.SelectedValue.ToString();
            }

            DirectoryInfo dirRoot = new DirectoryInfo(sRootDir);
            tvList.Nodes.Clear();
            TreeNode tnRoot = tvList.Nodes.Add("软件开发总结");
            GetDirectoryFile(tnRoot, dirRoot, BaseFileEncoding.GetEncodingByKey(sEncode));
            tvList.Nodes[0].Expand();
            //保存配置
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.SQLStudy_FileCharsetEncoding, cbbCharSetEncode.SelectedValue.ToString(), "【SQL总结】文件的字符集类型");
            WinFormContext.UserLoveSettings.Save();
        }

        #region 获取目录文件方法
        /// <summary>
        /// 获取目录文件方法
        /// </summary>
        /// <param name="node">上级节点</param>
        /// <param name="rootDirectory"></param>
        private void GetDirectoryFile(TreeNode node, DirectoryInfo rootDirectory, Encoding encoding)
        {
            foreach (var file in rootDirectory.GetFiles()) //文件的处理
            {
                if (file.Attributes == FileAttributes.System || file.Attributes == FileAttributes.Temporary || file.Attributes == FileAttributes.Hidden)
                {
                    continue;
                }

                TreeNode nodeFile = new TreeNode();
                nodeFile.Text = file.Name.Replace(file.Extension,"");
                nodeFile.Name = file.FullName;
                nodeFile.ToolTipText = "文件";
                nodeFile.Tag = File.ReadAllText(file.FullName, encoding);
                node.Nodes.Add(nodeFile);
            }

            //迭代子目录
            foreach (var path in rootDirectory.GetDirectories())
            {
                TreeNode nodeDir = new TreeNode();
                nodeDir.Text = path.Name;
                nodeDir.Name = path.FullName;
                nodeDir.ToolTipText = "目录";
                node.Nodes.Add(nodeDir);
                //递归获取文件下所有目录和文件
                GetDirectoryFile(nodeDir, path, encoding);
            }
        }
        #endregion

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowFileContent();
        }

        private void tsbReload_Click(object sender, EventArgs e)
        {
            //加载树数据
            LoadTreeViewData();
        }

        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            TreeNode trSelect = tvList.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            trSelect.ExpandAll();
        }

        private void tsmiCloseAll_Click(object sender, EventArgs e)
        {
            TreeNode trSelect = tvList.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            trSelect.Collapse(false);
        }

        /// <summary>
        /// 编码下拉框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbCharSetEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowFileContent();
        }

        private void ShowFileContent()
        {
            TreeNode trSelect = tvList.SelectedNode;
            if (trSelect == null)
            {
                return;
            }
            string sEncode = string.Empty;
            if (cbbCharSetEncode.SelectedValue == null || string.IsNullOrEmpty(cbbCharSetEncode.SelectedValue.ToString()))
            {
                sEncode = BaseFileEncoding.FileEncodingString.GB2312;
            }
            else
            {
                sEncode = cbbCharSetEncode.SelectedValue.ToString();
            }

            if ("文件".Equals(trSelect.ToolTipText))
            {
                var mdContent = File.ReadAllText(trSelect.Name, BaseFileEncoding.GetEncodingByKey(sEncode));
                if ("1".Equals(cbbShowType.SelectedValue.ToString()))
                {
                    richTextBox1.Visible = false;
                    webBrowser1.Visible = true;
                    webBrowser1.Dock = DockStyle.Fill;
                    var html = CommonMark.CommonMarkConverter.Convert(mdContent);
                    webBrowser1.DocumentText = html;
                }
                else
                {
                    webBrowser1.Visible = false;
                    richTextBox1.Visible = true;
                    richTextBox1.Dock = DockStyle.Fill;
                    richTextBox1.Clear();
                    richTextBox1.AppendText(mdContent);
                }
            }
            else
            {
                webBrowser1.DocumentText = "";
            }
        }

        private void cbbShowType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowFileContent();
        }
    }
}
