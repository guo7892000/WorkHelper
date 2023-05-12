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
using System.Collections;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并脚本
    /// </summary>
    public partial class FrmDBTScriptMerge : BaseForm
    {
        private string sConfigPath;
        public FrmDBTScriptMerge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDBTScriptMerge_Load(object sender, EventArgs e)
        {
            ckbAutoOpen.Checked = true;
            lblMergeInfo.Text= "请保证所有要合并的文件在配置文件所在目录（或子目录）下，并且文件的格式为UTF-8格式（可通过另存为UTF-8保证），否则合并后可能会有乱码";
            //加载用户偏好值
            txbSelectPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.MergeScriptPath, Path.Combine(DBTGlobalValue.AppPath, DBTGlobalValue.StringBuild.Xml_MergeScript)).Value;
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
            string sDirSource = Path.GetDirectoryName(sConfigPath);
            string sDirTarget = sDirSource;
            XmlNodeList rootList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.NodeString.Root); //configuration
            if (rootList.Count == 0) return;

            //获取根配置信息
            string sDB = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.DBType); //DB
            string sSourcePath = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.SourcePath);
            if(!string.IsNullOrEmpty(sSourcePath) && Directory.Exists(sSourcePath))
            {
                sDirSource = sSourcePath;
            }
            string sTargetPath = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.TargetPath);
            if (!string.IsNullOrEmpty(sTargetPath))
            {
                sDirTarget = sTargetPath;
                if (!Directory.Exists(sTargetPath))
                {
                    Directory.CreateDirectory(sDirTarget);
                }
            }
            else
            {
                sDirTarget = Path.Combine(sDirTarget, "900_FinalScript");
            }

            //获取所有分类
            XmlNodeList xmlList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.NodeString.ClassPath);
            string[] sqlFiles;

            foreach (XmlNode cla in xmlList)
            {
                //得到每个分类的属性
                string sType = cla.GetAttributeValue(ScriptMergeString.ClassProp.ObjectType);
                string sOutFileName = cla.GetAttributeValue(ScriptMergeString.ClassProp.FinalName);
                string sSourcePathRel = cla.GetAttributeValue(ScriptMergeString.ClassProp.SourcePathRel);
                string sSourcePathAbs = cla.GetAttributeValue(ScriptMergeString.ClassProp.SourcePathAbs);
                string sCharSet = cla.GetAttributeValue(ScriptMergeString.ClassProp.CharSet);
                string sFileExt = cla.GetAttributeValue(ScriptMergeString.ClassProp.FileExt);
                string sFinalPath = Path.Combine(sDirTarget, sOutFileName);
                Encoding useEnc = Encoding.UTF8;
                if (!string.IsNullOrEmpty(sCharSet))
                {
                    if ("utf16".Equals(sCharSet, StringComparison.OrdinalIgnoreCase))
                    {
                        useEnc = Encoding.Unicode;
                    }
                    else if ("utf32".Equals(sCharSet, StringComparison.OrdinalIgnoreCase))
                    {
                        useEnc = Encoding.UTF32;
                    }
                }
                //得到目录文件清单
                if(string.IsNullOrEmpty(sSourcePathRel) && string.IsNullOrEmpty(sSourcePathAbs))
                {
                    sqlFiles = Directory.GetFiles(sDirSource, "*.*", SearchOption.AllDirectories);
                    if (sqlFiles.Length == 0) continue;
                }
                else if (!string.IsNullOrEmpty(sSourcePathAbs))
                {
                    sqlFiles = Directory.GetFiles(sSourcePathAbs, "*.*", SearchOption.AllDirectories);
                }
                else
                {
                    if (sSourcePathRel.StartsWith(@"\") || sSourcePathRel.StartsWith(@"/"))
                    {
                        sSourcePathRel = sSourcePathRel.Substring(1); //去掉前面的斜杆，让后面的Path.Combine能正常合并路径；否则得到的路径是错的
                    }
                    sqlFiles = Directory.GetFiles(Path.Combine(sDirSource, sSourcePathRel), "*.*", SearchOption.AllDirectories);
                }
               
                IList<string> fileList = new List<string>();
                bool isHasChildItem = false;
                //如配置有具体的子节点，那么根据子节点查找文件，存在就加入清单
                foreach (XmlNode ch in cla.ChildNodes)
                {
                    string sFilePath = ch.InnerText.Trim();//2021-11-04文件名不区分大小写
                    if (string.IsNullOrEmpty(sFilePath)) continue;
                    IEnumerable<string> exist = sqlFiles.ToList().Where(t => t.Equals(sFilePath, StringComparison.OrdinalIgnoreCase));
                    if (exist.Count() == 0) continue;
                    fileList.Add(exist.First());
                    isHasChildItem = true;
                }
                //如配置有文件扩展名，那么根据扩展名查找文件，找到的文件加入清单
                if (!string.IsNullOrEmpty(sFileExt))
                {
                    foreach (string ext in sFileExt.Split(new char[] { ',','，', '|' }))
                    {
                        IEnumerable<string> exist = sqlFiles.ToList().Where(t => t.ToLower().EndsWith("."+ ext.ToLower()));
                        if (exist.Count() == 0) continue;
                        foreach (var item in exist)
                        {
                            fileList.Add(item);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(sSourcePathRel) || !string.IsNullOrEmpty(sSourcePathAbs))
                {
                    //没有扩展名，但有配置相对路径名或绝对路径名，且没有子项设置时，才增加目录下所有文件
                    if (!isHasChildItem)
                    {
                        foreach (var item in sqlFiles)
                        {
                            fileList.Add(item);
                        }
                    }
                }

                if (fileList.Count == 0) continue;

                using (StreamWriter writer = new StreamWriter(sFinalPath,false, useEnc))
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
            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.MergeScriptPath, txbSelectPath.Text, "【合并脚本】选择路径");
            WinFormContext.UserLoveSettings.Save();
            if (ckbAutoOpen.Checked)
            {
                System.Diagnostics.Process.Start("explorer.exe", sDirTarget);//打开文件夹
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
