using Breezee.Core.WinFormUI;
using System.Xml;
using Breezee.Core.Interface;
using Breezee.WorkHelper.DBTool.Entity;
using Setting = Breezee.WorkHelper.DBTool.UI.Properties.Settings;
using Breezee.Core.Tool;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    /// <summary>
    /// 点击复制字符或打开目录
    /// </summary>
    public partial class FrmDBTClickCopyStringAuto : BaseForm
    {
        private List<GroupBox> listGroupBox = new List<GroupBox>();
        private List<FlowLayoutPanel> listFlowLayoutPanel = new List<FlowLayoutPanel>();
        private Color _fileTextAreaColor = Color.OldLace;//读取文件的文本框背景色
        private Color _pathDirColor = Color.OldLace; //目录的按钮背景色
        private Color _fileNotExistsTextAreaColor = Color.Yellow;//读取文件不存在时的文本框背景色

        public FrmDBTClickCopyStringAuto()
        {
            InitializeComponent();
        }

        private void FrmDBTClickCopyStringAuto_Load(object sender, EventArgs e)
        {
            //加载用户偏好值
            txbXmlPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ClickCopy_Path, Path.Combine(DBTGlobalValue.AppPath, DBTGlobalValue.StringBuild.Xml_CopyString)).Value;
            ckbFlowDesign.Checked = true;
            toolTip1.SetToolTip(ckbFlowDesign, "选中时是自动生成组件布局；不选中时是根据配置中指定的每行几项来布局！");
            GeneratePanelControls();
        }

        /// <summary>
        /// 生成分局布局控件
        /// </summary>
        private void GenerateGroupControls()
        {
            string sXmlPath = txbXmlPath.Text.Trim();
            if (!File.Exists(sXmlPath))
            {
                return;
            }

            ckbOpenPath.Checked = true;
            gbGlobal.Parent = null;
            int iDefaultMax = 5;

            XmlDocument doc = new XmlDocument();
            doc.Load(sXmlPath); 

            XmlNode root = doc.SelectSingleNode("strings");
            iDefaultMax = int.Parse(root.GetOrDefaultAttrValue(GroupPropertyName.Max, iDefaultMax.ToString())); //最外层strings根节点的max属性
            XmlNodeList groups = doc.SelectNodes("strings/group");

            int iNewRow = 4;
            int iGroup = 0;
            GroupBox gb;
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                XmlNode gpNode = groups[i];
                if (gpNode.ChildNodes.Count == 0)
                {
                    continue;
                }
                XmlNodeList itemList = gpNode.SelectNodes("string");
                if (itemList.Count == 0)
                {
                    continue;
                }

                gb = new GroupBox();
                //获取组项
                GroupEntity groupEntity = getGroupEntity(gpNode);
                gb.Text = groupEntity.Text;
                gb.ForeColor = groupEntity.FontColor;
                iNewRow = iDefaultMax;
                if (groupEntity.Max > 0)
                {
                    iNewRow = groupEntity.Max;
                }

                TableLayoutPanel tlp = new TableLayoutPanel();
                double d = itemList.Count < iNewRow ? 1.0 : itemList.Count * 1.0 / iNewRow;
                tlp.RowCount = int.Parse(Math.Ceiling(d).ToString());
                tlp.ColumnCount = 3 * iNewRow + 1;
                tlp.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.AutoSize, 20f));

                tlp.Height = int.Parse(Math.Ceiling(28f * tlp.RowCount).ToString());

                int iItem = 0;
                int iRowIndex = 0;
                int iColumnIndex = 0;
                Label lb;
                TextBoxBase tb;
                Button bt;
                foreach (XmlNode item in itemList)
                {
                    lb = new Label();
                    tb = new TextBox();
                    bt = new Button();
                    //获取复制项
                    CopyItemEntity cs = getCopyItemEntity(item);
                    //标签颜色配置
                    Color colorLable = cs.FontColor.SafeParseColor();
                    if (colorLable == Color.Empty)
                    {
                        colorLable = groupEntity.ItemFontColor; //当前配置项没有颜色，取组的配置颜色
                    }
                    if (colorLable == Color.Empty)
                    {
                        colorLable = Color.Black;  //当前配置项没有颜色，取黑色
                    }
                    lb.ForeColor = colorLable;

                    if (cs == null) continue;
                    if (cs.Ctrol.EqualsIgnorEmptyCase("RichTextBox"))
                    {
                        tb = new RichTextBox();
                        if (cs.Type.EqualsIgnorEmptyCase("file"))
                        {
                            if (!string.IsNullOrWhiteSpace(cs.PathRel))
                            {
                                if (cs.PathRel.StartsWith(@"\") || cs.PathRel.StartsWith(@"/"))
                                {
                                    cs.PathRel = cs.PathRel.Substring(1); //去掉前面的斜杆，让后面的Path.Combine能正常合并路径；否则得到的路径是错的
                                }
                                string sPath = Path.Combine(Path.GetDirectoryName(sXmlPath), cs.PathRel);
                                if (File.Exists(sPath))
                                {
                                    //相对配置文件所在目录
                                    tb.AppendText(File.ReadAllText(sPath));
                                    cs.Tip = string.Format("文本框是相对路径【{0}】文件的内容", cs.PathRel);
                                    tb.BackColor = _fileTextAreaColor;
                                }
                                else
                                {
                                    tb.BackColor = _fileNotExistsTextAreaColor;
                                    tb.AppendText("文件不存在！");
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(cs.PathAbs))
                            {
                                //绝对路径
                                if (File.Exists(cs.PathAbs))
                                {
                                    tb.AppendText(File.ReadAllText(cs.PathAbs));
                                    cs.Tip = string.Format("文本框是绝对路径【{0}】文件的内容", cs.PathAbs);
                                    tb.BackColor = _fileTextAreaColor;
                                }
                                else
                                {
                                    tb.BackColor = _fileNotExistsTextAreaColor;
                                    tb.AppendText("文件不存在！");
                                }
                            }
                        }
                        else
                        {
                            tb.AppendText(cs.Text);
                        }
                    }
                    else
                    {
                        tb.Text = cs.Text;
                        if (!string.IsNullOrWhiteSpace(cs.Pwdchar))
                        {
                            (tb as TextBox).PasswordChar = cs.Pwdchar[0];
                        }
                    }

                    cs.tbb = tb;
                    lb.Text = cs.Lable;
                    if (!string.IsNullOrWhiteSpace(cs.Tip))
                    {
                        toolTip1.SetToolTip(bt, cs.Tip);
                    }

                    lb.AutoSize = true;
                    lb.Anchor = AnchorStyles.Right;
                    tb.Width = 120;
                    tb.Height = 20;
                    tb.Anchor = AnchorStyles.Left;
                    tb.ReadOnly = true;
                    bt.Width = 20;
                    bt.Height = 23;
                    bt.Anchor = AnchorStyles.Left;
                    bt.Tag = cs;
                    bt.Text = ".";
                    if (cs.Type.EqualsIgnorEmptyCase("path"))
                    {
                        bt.BackColor = _pathDirColor; //针对目录，按钮显示为黄色
                    }

                    if (!string.IsNullOrEmpty(cs.Method))
                    {
                        var click = bt.GetType().GetEvents().FirstOrDefault(ei => ei.Name.ToLower() == "click");
                        var method = ReflectHelper.GetMethod<FrmDBTClickCopyStringAuto>(cs.Method);
                        if (click != null && method != null)
                        {
                            var handler = Delegate.CreateDelegate(click.EventHandlerType, this, method);
                            click.AddEventHandler(bt, handler);
                        }
                    }
                    else
                    {
                        bt.Click += bt_Click;
                    }

                    if (iItem % iNewRow == 0)
                    {
                        iRowIndex++;
                        iColumnIndex = 0;
                    }
                    else
                    {
                        iColumnIndex += 3;
                    }

                    tlp.Controls.Add(lb, iColumnIndex, iRowIndex);
                    tlp.Controls.Add(tb, iColumnIndex + 1, iRowIndex);
                    tlp.Controls.Add(bt, iColumnIndex + 2, iRowIndex);

                    iItem++;
                }

                gb.Controls.Add(tlp);
                gb.Height = tlp.Height + 10;
                gb.Dock = DockStyle.Top;
                gb.AutoSize = true;
                tlp.Dock = DockStyle.Top;
                pnlAll.Controls.Add(gb);
                listGroupBox.Add(gb);//增加到集合中
                iGroup++;
            }
            pnlAll.Controls.Add(gbGlobal);
            pnlAll.AutoScroll = true;
            gbGlobal.Dock = DockStyle.Top;
        }

        /// <summary>
        /// 生成流式布局控件
        /// </summary>
        private void GeneratePanelControls()
        {
            string sXmlPath = txbXmlPath.Text.Trim();
            if (!File.Exists(sXmlPath))
            {
                return;
            }

            ckbOpenPath.Checked = true;
            gbGlobal.Parent = null;
            string sText = "";
            int iDefaultMax = 5;

            XmlDocument doc = new XmlDocument();
            doc.Load(sXmlPath);

            XmlNode root = doc.SelectSingleNode("strings");
            iDefaultMax = int.Parse(root.GetOrDefaultAttrValue(GroupPropertyName.Max, iDefaultMax.ToString()));
            XmlNodeList groups = doc.SelectNodes("strings/group");

            int iNewRow = 4;
            int iGroup = 0;
            FlowLayoutPanel gbPanl = new FlowLayoutPanel();
            for (int i = 0; i < groups.Count; i++)
            {
                //单个Group
                XmlNode gpNode = groups[i];
                if (gpNode.ChildNodes.Count == 0) continue;
                XmlNodeList itemList = gpNode.SelectNodes("string");
                if (itemList.Count == 0) continue;

                FlowLayoutPanel gbChildPanl = new FlowLayoutPanel();
                gbChildPanl.FlowDirection = FlowDirection.LeftToRight;
                gbChildPanl.BorderStyle = BorderStyle.FixedSingle;
                //获取组项
                GroupEntity groupEntity = getGroupEntity(gpNode);
                toolTip1.SetToolTip(gbChildPanl, groupEntity.Text);
                iNewRow = iDefaultMax;
                if (groupEntity.Max > 0)
                {
                    iNewRow = groupEntity.Max;
                }

                foreach (XmlNode item in itemList)
                {
                    //Group中的子项
                    Panel tlpPanl = new Panel();
                    TableLayoutPanel tlp = new TableLayoutPanel();

                    tlp.RowCount = 2;
                    tlp.ColumnCount = 4;
                    tlp.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.AutoSize, 20f));
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    tlp.Height = 20;

                    Label lb = new Label();
                    TextBoxBase tb = new TextBox();
                    Button bt = new Button();
                    //获取复制项
                    CopyItemEntity cs = getCopyItemEntity(item);
                    
                    if (cs == null) continue;
                    if (cs.Ctrol.EqualsIgnorEmptyCase("RichTextBox"))
                    {
                        tb = new RichTextBox();
                        if (cs.Type.EqualsIgnorEmptyCase("file"))
                        {
                            if (!string.IsNullOrWhiteSpace(cs.PathRel))
                            {
                                if (cs.PathRel.StartsWith(@"\") || cs.PathRel.StartsWith(@"/"))
                                {
                                    cs.PathRel = cs.PathRel.Substring(1); //去掉前面的斜杆，让后面的Path.Combine能正常合并路径；否则得到的路径是错的
                                }
                                string sPath = Path.Combine(Path.GetDirectoryName(sXmlPath), cs.PathRel);
                                if (File.Exists(sPath))
                                {
                                    //相对配置文件所在目录
                                    tb.AppendText(File.ReadAllText(sPath));
                                    cs.Tip = string.Format("文本框是相对路径【{0}】文件的内容", cs.PathRel);
                                    tb.BackColor = _fileTextAreaColor;
                                }
                                else
                                {
                                    tb.BackColor = _fileNotExistsTextAreaColor;
                                    tb.AppendText("文件不存在！");
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(cs.PathAbs))
                            {
                                //绝对路径
                                if (File.Exists(cs.PathAbs))
                                {
                                    tb.AppendText(File.ReadAllText(cs.PathAbs));
                                    cs.Tip = string.Format("文本框是绝对路径【{0}】文件的内容", cs.PathAbs);
                                    tb.BackColor = _fileTextAreaColor;
                                }
                                else
                                {
                                    tb.BackColor = _fileNotExistsTextAreaColor;
                                    tb.AppendText("文件不存在！");
                                }
                            }
                        }
                        else
                        {
                            tb.AppendText(cs.Text);
                        }
                    }
                    else
                    {
                        tb.Text = cs.Text;

                        if (!string.IsNullOrWhiteSpace(cs.Pwdchar))
                        {
                            (tb as TextBox).PasswordChar = cs.Pwdchar[0];
                        }
                    }

                    cs.tbb = tb;
                    lb.Text = cs.Lable;
                    //标签颜色配置
                    Color colorLable = cs.FontColor.SafeParseColor();
                    if (colorLable == Color.Empty)
                    {
                        colorLable = groupEntity.ItemFontColor; //当前配置项没有颜色，取组的配置颜色
                    }
                    if (colorLable == Color.Empty)
                    {
                        colorLable = Color.Black;  //当前配置项没有颜色，取黑色
                    }
                    lb.ForeColor = colorLable;
                    if (!string.IsNullOrWhiteSpace(cs.Tip))
                    {
                        toolTip1.SetToolTip(bt, cs.Tip);
                    }

                    lb.AutoSize = true;
                    lb.Anchor = AnchorStyles.Right;
                    tb.Width = 120;
                    tb.Height = 20;
                    tb.Anchor = AnchorStyles.Left;
                    tb.ReadOnly = true;
                    bt.Width = 20;
                    bt.Height = 23;
                    bt.Anchor = AnchorStyles.Left;
                    bt.Tag = cs;
                    bt.Text = ".";
                    if (cs.Type.EqualsIgnorEmptyCase("path"))
                    {
                        bt.BackColor = _pathDirColor; //针对目录，按钮显示为黄色
                    }
                    if (!string.IsNullOrEmpty(cs.Method))
                    {
                        var click = bt.GetType().GetEvents().FirstOrDefault(ei => ei.Name.ToLower() == "click");
                        var method = ReflectHelper.GetMethod<FrmDBTClickCopyStringAuto>(cs.Method);
                        if (click != null && method != null)
                        {
                            var handler = Delegate.CreateDelegate(click.EventHandlerType, this, method);
                            click.AddEventHandler(bt, handler);
                        }
                    }
                    else
                    {
                        bt.Click += bt_Click;
                    }
                    tlp.Controls.Add(lb, 0, 0);
                    tlp.Controls.Add(tb, 1, 0);
                    tlp.Controls.Add(bt, 2, 0);

                    tlp.AutoSize = true;
                    tlpPanl.Controls.Add(tlp);
                    tlpPanl.AutoSize = true;
                    gbChildPanl.Controls.Add(tlpPanl);
                }
                gbPanl.Controls.Add(gbChildPanl);
                gbChildPanl.Dock = DockStyle.Fill;
                gbChildPanl.AutoSize = true;
                gbPanl.Dock = DockStyle.Top;
                gbPanl.AutoSize = true;
                
                pnlAll.Controls.Add(gbPanl);
                listFlowLayoutPanel.Add(gbPanl);//增加到集合中
                iGroup++;
            }
            pnlAll.Controls.Add(gbGlobal);
            pnlAll.AutoScroll = true;
            gbGlobal.Dock = DockStyle.Top;
        }

        void bt_Click(object sender, EventArgs e)
        {
            try
            {
                CopyItemEntity cs = (sender as Button).Tag as CopyItemEntity;
                string sText = (cs.tbb as TextBoxBase).Text;
                if ("1".Equals(cs.ParamRep))
                {
                    // 针对动态替换，将时间格式替换为当前日期
                    sText = sText.ReplaceText("#yyyyMMdd#", DateTime.Now.ToString("yyyyMMdd"))
                        .ReplaceText("#yyyy-MM-dd#", DateTime.Now.ToString("yyyy-MM-dd"))
                        .ReplaceText("#yyyy-MM#", DateTime.Now.ToString("yyyy-MM"))
                        .ReplaceText("#yyyyMM#", DateTime.Now.ToString("yyyyMM"))
                        .ReplaceText("#yyyy#", DateTime.Now.ToString("yyyy"))
                        .ReplaceText("#yyyyMMddHHmi#", DateTime.Now.ToString("yyyyMMddHHmm"))
                        .ReplaceText("#yyyyMMddHHmiss#", DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                Clipboard.SetText(sText);
                if (cs.Type.EqualsIgnorEmptyCase("path") && ckbOpenPath.Checked)
                {
                    if (Directory.Exists(sText))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", sText);
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 点击获取毫秒数示例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetMillisecond_Click(object sender, EventArgs e)
        {
            CopyItemEntity cs = (sender as Button).Tag as CopyItemEntity;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string sText = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            (cs.tbb as TextBoxBase).Text = sText;
            Clipboard.SetText(sText);
        }

        private void TsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.StringBuild.Xml_CopyString, "点击拷贝字符模板", true);
        }

        /// <summary>
        /// 获取复制项实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        private CopyItemEntity getCopyItemEntity(XmlNode xn)
        {
            CopyItemEntity cs = null;
            string sText = "";
            if(xn.TryGetAttrValue(CopyItemPropertyName.Type,out sText))
            {
                //能正常获取type属性
                cs = new CopyItemEntity();
                cs.Type = sText;
                if(xn.TryGetAttrValue(CopyItemPropertyName.CtrolType,out sText))
                {
                    cs.Ctrol = sText; //控件类型
                }
                else
                {
                    return null;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Lable, out sText))
                {
                    cs.Lable = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.PathAbs, out sText))
                {
                    cs.PathAbs = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.PathRel, out sText))
                {
                    cs.PathRel = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Pwdchar, out sText))
                {
                    cs.Pwdchar = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Text, out sText))
                {
                    cs.Text = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Tip, out sText))
                {
                    cs.Tip = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.Method, out sText))
                {
                    cs.Method = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.ParamReplace, out sText))
                {
                    cs.ParamRep = sText;
                }
                if (xn.TryGetAttrValue(CopyItemPropertyName.FontColor, out sText))
                {
                    cs.FontColor = sText;
                }
            }
            return cs;
        }

        /// <summary>
        /// 获取组实体
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        private GroupEntity getGroupEntity(XmlNode xn)
        {
            GroupEntity cs = new GroupEntity();
            string sText = "";
            cs = new GroupEntity();
            if (xn.TryGetAttrValue(GroupPropertyName.Text, out sText))
            {
                cs.Text = sText;
            }
            if (xn.TryGetAttrValue(GroupPropertyName.Max, out sText))
            {
                cs.Max = int.Parse(sText);
            }
            if (xn.TryGetAttrValue(GroupPropertyName.FontColor, out sText))
            {
                cs.FontColor = sText.SafeParseColor();
            }
            if (xn.TryGetAttrValue(GroupPropertyName.ItemFontColor, out sText))
            {
                cs.ItemFontColor = sText.SafeParseColor();
            }
            return cs;
        }


        /// <summary>
        /// 选择配置文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "(*.xml)|*.xml";
            dia.Multiselect = false;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                txbXmlPath.Text = dia.FileName;
                ReloadFile(); //重新加载文件
                //保存用户偏好值
                WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ClickCopy_Path, txbXmlPath.Text, "【点击复制】选择配置文件");
                WinFormContext.UserLoveSettings.Save();
            }
        }

        /// <summary>
        /// 重新加载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReloadFile_Click(object sender, EventArgs e)
        {
            ReloadFile(); //重新加载文件
            //保存用户偏好值
            WinFormContext.UserLoveSettings.Set(DBTUserLoveConfig.ClickCopy_Path, txbXmlPath.Text, "【点击复制】选择路径");
            WinFormContext.UserLoveSettings.Save();
        }

        private void ReloadFile()
        {
            string sXmlPath = txbXmlPath.Text.Trim();
            if (!File.Exists(sXmlPath))
            {
                ShowErr("文件：" + sXmlPath + "不存在！");
                return;
            }
            foreach (GroupBox gb in listGroupBox)
            {
                pnlAll.Controls.Remove(gb);
            }
            foreach (FlowLayoutPanel gb in listFlowLayoutPanel)
            {
                pnlAll.Controls.Remove(gb);
            }
            if(ckbFlowDesign.Checked)
            {
                GeneratePanelControls();
            }
            else
            {
                GenerateGroupControls();
            }
            ShowInfo("文件加载成功！");
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ckbFlowDesign_CheckedChanged(object sender, EventArgs e)
        {
            ReloadFile();
        }

    }

    class GroupPropertyName
    {
        public static string Text = "text";
        public static string Max = "max";
        public static string FontColor = "fontColor";
        public static string ItemFontColor = "itemFontColor";
    }

    class CopyItemPropertyName
    {
        public static string Type = "type";
        public static string CtrolType = "ctrol";
        public static string Lable = "label";
        public static string Tip = "tip";
        public static string Text = "text";
        public static string Pwdchar = "pwdchar";
        public static string PathAbs = "pathAbs";
        public static string PathRel = "pathRel";
        public static string Method = "method";
        public static string ParamReplace = "paramRep";
        public static string FontColor = "fontColor";
    }

    class GroupEntity
    {
        public string Text;
        public int Max;
        public Color FontColor;
        public Color ItemFontColor;
    }

    class CopyItemEntity
    {
        public string Type;
        public string Ctrol;
        public string Lable;
        public string Tip;
        public string Text;
        public string Pwdchar;
        public string PathAbs;
        public string PathRel;
        public string Method;
        public string ParamRep;
        public TextBoxBase tbb;
        public string FontColor;
    }

}
