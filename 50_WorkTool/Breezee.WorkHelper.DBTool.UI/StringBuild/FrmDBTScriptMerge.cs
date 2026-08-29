using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Ude;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 合并脚本
    /// </summary>
    public partial class FrmDBTScriptMerge : BaseForm
    {
        private string sConfigPath;
        string sTarLastDir = "900_FinalScript";
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
            lblMergeInfo.Text= "源目录会优先从配置文件中读取，如果目录不存在，会找该配置文件所在的目录！另外，为防止误删除文件，生成目录如不是以" + sTarLastDir + "结尾，会自动创建该子目录用于存放生成文件！";

            DataTable dtEncode = BaseFileEncoding.GetEncodingTable(false);
            cbbCharSetEncode.BindTypeValueDropDownList(dtEncode, false, true);
            toolTip1.SetToolTip(cbbCharSetEncode, "生成文件的字符集！");
            //加载用户偏好值
            txbSelectPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.MergeScript_Path, Path.Combine(DBTGlobalValue.AppPath, DBTGlobalValue.StringBuild.Xml_MergeScript)).Value;
            ckbDelDirBfGen.Checked = true;
        }
        
        /// <summary>
        /// 选择文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 合并脚本按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            
            rtbString.Clear();
            StringBuilder sbDrop = new StringBuilder();
            string sDirSource = Path.GetDirectoryName(sConfigPath);
            string sDirTarget = sDirSource;
            XmlNodeList rootList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.NodeString.Root); //configuration
            if (rootList.Count == 0) return;

            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.MergeScript_Path, txbSelectPath.Text, "【合并脚本】选择路径");
            WinFormContext.UserLoveSettings.Save();
            //获取根配置信息
            string sDB = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.DBType); //DB
            string sSourcePath = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.SourcePath);
            if(!string.IsNullOrEmpty(sSourcePath) && Directory.Exists(sSourcePath))
            {
                sDirSource = sSourcePath;
            }
            lblRealReadDir.Text = "实际读取的目录:" + sDirSource;
            
            //获取生成目录
            string sTargetPath = rootList[0].GetAttributeValue(ScriptMergeString.RootProp.TargetPath);
            if (!string.IsNullOrEmpty(sTargetPath))
            {
                sDirTarget = sTargetPath;
                if(!sDirTarget.EndsWith(sTarLastDir))
                {
                    sDirTarget = Path.Combine(sDirTarget, sTarLastDir);
                }
                if (!Directory.Exists(sDirTarget))
                {
                    Directory.CreateDirectory(sDirTarget);
                }
                else
                {
                    if (ckbDelDirBfGen.Checked)
                    {
                        Directory.Delete(sDirTarget, true);
                        Directory.CreateDirectory(sDirTarget);
                    }
                }
            }
            else
            {
                sDirTarget = Path.Combine(sDirTarget, sTarLastDir);
                
            }

            //获取所有分类
            XmlNodeList xmlList = XmlHelper.GetXmlNodeListByXpath(sConfigPath, ScriptMergeString.NodeString.ClassPath);
            string[] sqlFiles = null;

            foreach (XmlNode cla in xmlList)
            {
                //得到每个分类的属性
                string sOutFileName = cla.GetAttributeValue(ScriptMergeString.ClassProp.FinalName);
                string sSourcePathRel = cla.GetAttributeValue(ScriptMergeString.ClassProp.SourcePathRel);
                string sSourcePathAbs = cla.GetAttributeValue(ScriptMergeString.ClassProp.SourcePathAbs);
                string sFileExt = cla.GetAttributeValue(ScriptMergeString.ClassProp.FileExt);
                string sRemak = cla.GetAttributeValue(ScriptMergeString.ClassProp.Remark);

                string sFinalPath = Path.Combine(sDirTarget, sOutFileName);
                Encoding useEnc = Encoding.UTF8; //默认为UTF-8
                if (cbbCharSetEncode.SelectedValue != null && !string.IsNullOrEmpty(cbbCharSetEncode.SelectedValue.ToString()))
                {
                    useEnc = BaseFileEncoding.GetEncodingByKey(cbbCharSetEncode.SelectedValue.ToString());
                }
                //得到目录文件清单
                if (string.IsNullOrEmpty(sSourcePathRel) && string.IsNullOrEmpty(sSourcePathAbs))
                {
                    //没有配置相对目录和绝对目录时,读取源目录下的所有文件
                    sqlFiles = Directory.GetFiles(sDirSource, "*.*", SearchOption.AllDirectories);
                    if (sqlFiles.Length == 0)
                    {
                        continue;
                    }
                }
                else
                {
                    //相对目录和绝对目录至少有一个不为空
                    if (!string.IsNullOrEmpty(sSourcePathAbs))
                    {
                        //绝对目录不为空时，获取其下所有文件
                        sqlFiles = Directory.GetFiles(sSourcePathAbs, "*.*", SearchOption.AllDirectories);
                    }
                    if (!string.IsNullOrEmpty(sSourcePathRel))
                    {
                        //相对目录不为空时
                        if (sSourcePathRel.StartsWith(@"\") || sSourcePathRel.StartsWith(@"/"))
                        {
                            sSourcePathRel = sSourcePathRel.Substring(1); //去掉前面的斜杆，让后面的Path.Combine能正常合并路径；否则得到的路径是错的
                        }
                        string sFilePath = Path.Combine(sDirSource, sSourcePathRel);
                        if (!Directory.Exists(sFilePath))
                        {
                            rtbString.AppendText(sFilePath + "文件夹不存在！" + Environment.NewLine);
                            continue;
                        }
                        string[]  sqlFilesRel = Directory.GetFiles(sFilePath, "*.*", SearchOption.AllDirectories);
                        //按名称排序
                        Array.Sort(sqlFilesRel, StringComparer.OrdinalIgnoreCase);
                        if (sqlFiles == null)
                        {
                            sqlFiles = sqlFilesRel; //原SQL数组为空时，直接取相对目录文件数组
                        }
                        else if(sqlFilesRel.Length>0)
                        {
                            foreach (string item in sqlFilesRel)
                            {
                                sqlFiles.Append(item); //将相对目录文件数组添加到原SQL数组中
                            }
                        }
                    }
                    //没有文件时继续下一个分类
                    if (sqlFiles == null || sqlFiles.Length == 0)
                    {
                        continue;
                    }
                }
               
                IList<string> fileList = new List<string>();
                bool isHasChildItem = false;
                //如配置有具体的子节点，那么根据子节点查找文件，存在就加入清单
                foreach (XmlNode ch in cla.ChildNodes)
                {
                    string sFilePath = ch.InnerText.Trim();//2021-11-04文件名不区分大小写
                    if (string.IsNullOrEmpty(sFilePath)) continue;
                    IEnumerable<string> exist = sqlFiles.ToList().Where(t => t.Equals(sFilePath, StringComparison.OrdinalIgnoreCase));
                    if (exist.Count() > 0) continue;

                    string sFileFullPath = Path.Combine(sDirSource, sSourcePathRel, sFilePath);
                    if (!string.IsNullOrEmpty(sSourcePathAbs))
                    {
                        sFileFullPath = Path.Combine(sSourcePathAbs, sFilePath);
                    }
                    fileList.Add(sFileFullPath);
                    isHasChildItem = true;
                }
                //如配置有文件扩展名，那么根据扩展名查找文件，找到的文件加入清单
                if (!string.IsNullOrEmpty(sFileExt))
                {
                    foreach (string ext in sFileExt.Split(new char[] { ',','，', ';', '；', '|' }))
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

                // 这里如果文件存在，那么后面的文件会追加到这个文件中，所以如果你希望每次运行时都覆盖旧的文件，可以在写入前删除旧文件
                using (StreamWriter writer = new StreamWriter(sFinalPath, true, useEnc))
                {
                    foreach (string filePath in fileList)
                    {
                        var detectedEncoding = DetectEncoding(filePath);

                        // 自动检测文件编码（优先尝试 GBK，再尝试 GB2312）
                        Encoding fileEncoding = DetectEncoding(filePath) ?? Encoding.Default;
                        string content = File.ReadAllText(filePath, fileEncoding);
                        writer.Write(content);
                    }
                }

                rtbString.AppendText(sFinalPath + "\n");
                sqlFiles = null;
            }
            
            if (ckbAutoOpen.Checked)
            {
                System.Diagnostics.Process.Start("explorer.exe", sDirTarget);//打开文件夹
            }
        }

        private static Encoding DetectEncoding(string filePath)
        {
            return DetectEncoding(filePath, File.OpenRead(filePath));
        }

        private static Encoding DetectEncoding(string filePath, FileStream fs)
        {
            var detector = new CharsetDetector();
            detector.Feed(fs);
            detector.DataEnd();

            if (detector.IsDone() && detector.Charset != null)
            {
                try
                {
                    return Encoding.GetEncoding(detector.Charset);
                }
                catch
                {
                    // 如果无法获取编码（如 detector 返回 "windows-949" 但 .NET 不支持名称），返回 null
                }
            }

            // 如果 UDE 无法识别，回退到检查 BOM
            return GetEncodingFromBom(filePath) ?? null;
        }

        private static Encoding GetEncodingFromBom(string filePath)
        {
            var bom = new byte[4];
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            if (bom[0] == 0xFF && bom[1] == 0xFE) return Encoding.Unicode;      // UTF-16 LE
            if (bom[0] == 0xFE && bom[1] == 0xFF) return Encoding.BigEndianUnicode; // UTF-16 BE
            if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) return Encoding.UTF8;
            if (bom[0] == 0x2B && bom[1] == 0x2F && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32;

            return null; // 无 BOM
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
