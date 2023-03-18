using Breezee.Core.WinFormUI;
using Breezee.Core.Tool;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Breezee.Core.Interface;

namespace Breezee.WorkHelper.DBTool.UI
{
    public partial class FrmDBTScriptMerge : BaseForm
    {
        private string sConfigPath;
        public FrmDBTScriptMerge()
        {
            InitializeComponent();
        }

        private void FrmDBTScriptMerge_Load(object sender, EventArgs e)
        {
            ckbAutoOpen.Checked = true;
            lblMergeInfo.Text= "请保证所有要合并的文件在配置文件所在目录（或子目录）下，并且文件的格式为UTF-8格式（可通过另存为UTF-8保证），否则合并后可能会有乱码";
        }
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "(*.xml)|*.xml";
            dia.Multiselect = false;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                sConfigPath = dia.FileName;
                txbSelectPath.Text = sConfigPath;
            }
        }
        private void TsbAutoSQL_Click(object sender, EventArgs e)
        {
            sConfigPath = txbSelectPath.Text.Trim();
            if (string.IsNullOrEmpty(sConfigPath))
            {
                ShowErr("请先选择配置文件！");
                return;
            }

            if(!File.Exists(sConfigPath))
            {
                ShowErr("输入的配置文件不存在，请重新输入或选择！");
                return;
            }

            //bool isGenDropSql = false;
            rtbString.Clear();
            StringBuilder sbDrop = new StringBuilder();
            string sDir = Path.GetDirectoryName(sConfigPath);
            XmlNodeList rootList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.rootPath);
            if (rootList.Count == 0) return;

            string sDB = rootList[0].GetAttributeValue(ScriptMergeString.DB);
            string sDrop = rootList[0].GetAttributeValue(ScriptMergeString.Drop);
            //if (sDB.Equals("SqlServer") && sDrop.Equals("1")) isGenDropSql = true;

            XmlNodeList xmlList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.DBOClass.ClassPath);
            string[] sqlFiles = Directory.GetFiles(sDir, "*.*", SearchOption.AllDirectories);

            foreach (XmlNode cla in xmlList)
            {
                string sType = cla.GetAttributeValue(ScriptMergeString.DBOClass.Type);
                string sOutFileName = cla.GetAttributeValue(ScriptMergeString.DBOClass.Name);
                string sFinalPath = Path.Combine(sDir, "900_FinalScript", sOutFileName);

                if (sqlFiles.Length == 0) continue;
                IList<string> fileList = new List<string>();

                //得到有效的节点
                foreach (XmlNode ch in cla.ChildNodes)
                {
                    string sFilePath = ch.InnerText.Trim().ToLower();//2021-11-04文件名不区分大小写
                    if (string.IsNullOrEmpty(sFilePath)) continue;
                    IEnumerable<string> exist = sqlFiles.ToList().Where(t => t.ToLower().EndsWith(sFilePath));
                    if (exist.Count() == 0) continue;
                    fileList.Add(exist.First());
                }

                if (fileList.Count == 0) continue;

                using (StreamWriter writer = new StreamWriter(sFinalPath,false, Encoding.UTF8))
                {
                    foreach (string file in fileList)
                    {
                        using (StreamReader reader = File.OpenText(file))
                        {
                            writer.Write(reader.ReadToEnd());
                            writer.WriteLine();
                        }
                    }
                }
                rtbString.AppendText(sFinalPath + "\n");
            }
            if (ckbAutoOpen.Checked)
            {
                System.Diagnostics.Process.Start(Path.Combine(sDir, "900_FinalScript"));
            }
        }
        private void TsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.StringBuild.Xml_MergeScript, "合并脚本配置模板", true);
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
