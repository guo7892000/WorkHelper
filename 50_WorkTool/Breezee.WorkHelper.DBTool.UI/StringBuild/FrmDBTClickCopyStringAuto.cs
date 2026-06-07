using Breezee.Core.Interface;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;
using Breezee.WorkHelper.DBTool.Entity;
using org.breezee.MyPeachNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Setting = Breezee.WorkHelper.DBTool.UI.Properties.Settings;

namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    /// <summary>
    /// 点击复制字符
    /// </summary>
    public partial class FrmDBTClickCopyStringAuto : BaseForm
    {
        private List<GroupBox> listGroupBox = new List<GroupBox>();
        private List<FlowLayoutPanel> listFlowLayoutPanel = new List<FlowLayoutPanel>();
        private System.Drawing.Color _fileTextAreaColor = Color.OldLace;//读取文件的文本框背景色
        private Color _pathDirColor = Color.OldLace; //目录的按钮背景色
        private Color _fileNotExistsTextAreaColor = Color.Yellow;//读取文件不存在时的文本框背景色
        ClickCopyConfigFile _dataCfg; //点击复制配置文件
        DataTable _dtConfigFile;
        Panel _pnlAll; //面板
        TabPage _TabPageMain; //针对旧格式的放这个页签
        bool _IsRemeMainTap = false;
        IDictionary<string, string> _dicGlobalParam = new Dictionary<string, string>(); //全局参数
        string _sJHChar = "{{@JH@}}"; //为防止跟参数#冲突，string子节点中的#需要写为：{{@JH@}}，以在最后替换为#。
        bool _isNeedSaveXml = false; //是否需要保存Xml
        Panel _globalPnl; //全局局面板
        public FrmDBTClickCopyStringAuto()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDBTClickCopyStringAuto_Load(object sender, EventArgs e)
        {
            _dataCfg = new ClickCopyConfigFile();
            _dtConfigFile = _dataCfg.XmlConfig.Load();
            cbbCfgFile.BindDropDownList(_dtConfigFile, ClickCopyConfigFileStr.Id, ClickCopyConfigFileStr.Name, true,true);//绑定下拉框

            //加载用户偏好值
            txbXmlPath.Text = WinFormContext.UserLoveSettings.Get(DBTUserLoveConfig.ClickCopy_Path, Path.Combine(DBTGlobalValue.AppPath, DBTGlobalValue.StringBuild.Xml_CopyString)).Value;
            ckbFlowDesign.Checked = true;
            toolTip1.SetToolTip(ckbFlowDesign, "选中时是自动生成组件布局；不选中时是根据配置中指定的每行几项来布局！");
            //调用生成控件
            GenerateControls();
        }

        /// <summary>
        /// 生成控件
        /// </summary>
        private void GenerateControls()
        {
            _isNeedSaveXml = false;
            string sXmlPath = txbXmlPath.Text.Trim();
            if (!File.Exists(sXmlPath))
            {
                return;
            }

            ckbOpenPath.Checked = true;
            //gbGlobal.Parent = null;
            int iDefaultMax = 5;
            //读取配置文件
            XmlDocument doc = new XmlDocument();
            doc.Load(sXmlPath);

            XmlNode root = doc.SelectSingleNode("strings");
            iDefaultMax = int.Parse(root.GetOrDefaultAttrValue(GroupPropertyName.Max, iDefaultMax.ToString())); //最外层strings根节点的max属性
            //公共时间参数：全部转换为大写
            _dicGlobalParam["YYYYMMDD"] = DateTime.Now.ToString("yyyyMMdd");
            _dicGlobalParam["YYYY-MM-DD"] = DateTime.Now.ToString("yyyy-MM-dd");
            _dicGlobalParam["YYYY-MM"] = DateTime.Now.ToString("yyyy-MM");
            _dicGlobalParam["YYYYMM"] = DateTime.Now.ToString("yyyyMM");
            _dicGlobalParam["YYYY"] = DateTime.Now.ToString("yyyy");
            _dicGlobalParam["YYYYMMDDHHMI"] = DateTime.Now.ToString("yyyyMMddHHmm");
            _dicGlobalParam["YYYYMMDDHHMISS"] = DateTime.Now.ToString("yyyyMMddHHmmss");
            //全局参数配置
            XmlNodeList para = doc.SelectNodes("strings/params/param");
            foreach (XmlNode paraNode in para)
            {
                ParamEntity paramEntity = ClickCopyEntityConvert.getParamEnity(paraNode);
                if (!string.IsNullOrEmpty(paramEntity.Key))
                {
                    _dicGlobalParam[paramEntity.Key] = paramEntity.Value;
                }
            }
            //全局组的处理
            XmlNodeList globalGroups = doc.SelectNodes("strings/global/group");
            if(globalGroups.Count > 0)
            {
                if (ckbFlowDesign.Checked)
                {
                    pnlGlobal.Visible = false;
                    pnlGlobalFlow.Visible = true;
                    pnlGlobalFlow.AutoSize = true;
                    pnlGlobalFlow.Controls.Clear();
                    _globalPnl = pnlGlobalFlow; //针对流式布局，使用流式的Panel
                    AddFlowTapControl(sXmlPath, iDefaultMax, globalGroups, _globalPnl); //增加Tab页控件
                }
                else
                {
                    pnlGlobalFlow.Visible = false;
                    pnlGlobal.Visible = true;
                    pnlGlobal.AutoSize = true;
                    pnlGlobal.Controls.Clear();
                    _globalPnl = pnlGlobal; //针对一般的组布局，使用一般的Panel
                    AddGroupTapControl(sXmlPath, iDefaultMax, globalGroups, _globalPnl); //增加Tab页控件
                }
            }
            else
            {
                pnlGlobalFlow.Visible = false;
                pnlGlobal.Visible = false;
            }

            //旧版本配置文件，没有加Tap的，作为Main主页签
            XmlNodeList groups = doc.SelectNodes("strings/group");
            TabPage tabPage;
            foreach (TabPage tp in tapAll.TabPages)
            {
                if (tp.Text == "Main")
                {
                    _TabPageMain = tp;
                }
                else
                {
                    tapAll.TabPages.Remove(tp);
                }
            }
            if(groups.Count > 0 && !tapAll.TabPages.Contains(_TabPageMain))
            {
                tapAll.TabPages.Add(_TabPageMain);
            }
            //旧版本配置文件处理
            if (groups.Count > 0)
            {
                _IsRemeMainTap = false;
                _pnlAll = new Panel();
                foreach (Control grp in _TabPageMain.Controls)
                {
                    _TabPageMain.Controls.Remove(grp);
                }
                _TabPageMain.Controls.Add(_pnlAll);
                _pnlAll.AutoScroll = true;
                _pnlAll.Dock = DockStyle.Fill;
                if (ckbFlowDesign.Checked)
                {
                    AddFlowTapControl(sXmlPath, iDefaultMax, groups, _pnlAll); //增加Tab页控件
                }
                else
                {
                    AddGroupTapControl(sXmlPath, iDefaultMax, groups, _pnlAll); //增加Tab页控件
                }
            }
            else
            {
                _IsRemeMainTap = true;
            }

            //针对组的页签配置处理
            XmlNodeList taps = doc.SelectNodes("strings/tap");
            for (int i = 0; i < taps.Count; i++)
            {
                TapEntity tapEntity = ClickCopyEntityConvert.getTapEnity(taps[i]);
                tabPage = new TabPage(tapEntity.Name);
                _pnlAll = new Panel();
                tabPage.Controls.Add(_pnlAll);
                tapAll.Controls.Add(tabPage);
                _pnlAll.AutoScroll = true;
                _pnlAll.Dock = DockStyle.Fill;
                groups = taps[i].SelectNodes("group");
                if (ckbFlowDesign.Checked)
                {
                    AddFlowTapControl(sXmlPath, iDefaultMax, groups, _pnlAll); //增加Tab页控件
                }
                else
                {
                    AddGroupTapControl(sXmlPath, iDefaultMax, groups, _pnlAll); //增加Tab页控件
                }
            }
            if (_IsRemeMainTap)
            {
                tapAll.TabPages.Remove(_TabPageMain); 
            }
            if (_isNeedSaveXml)
            {
                doc.Save(sXmlPath);
            }
        }

        /// <summary>
        /// 针对组布局的页签处理
        /// </summary>
        /// <param name="sXmlPath"></param>
        /// <param name="iDefaultMax"></param>
        /// <param name="groups"></param>
        private void AddGroupTapControl(string sXmlPath, int iDefaultMax, XmlNodeList groups, Panel panel)
        {
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
                GroupEntity groupEntity = ClickCopyEntityConvert.getGroupEntity(gpNode);
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
                //循环点击复制项
                foreach (XmlNode item in itemList)
                {
                    lb = new Label();
                    tb = new TextBox();
                    bt = new Button();
                    //获取复制项
                    CopyItemEntity cs = ClickCopyEntityConvert.getCopyItemEntity(item);
                    if (cs.IsChange)
                    {
                        _isNeedSaveXml = true;
                    }
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
                panel.Controls.Add(gb);
                listGroupBox.Add(gb);//增加到集合中
                iGroup++;
            }
        }

        /// <summary>
        /// 针对流式布局的页签处理
        /// </summary>
        /// <param name="sXmlPath"></param>
        /// <param name="iDefaultMax"></param>
        /// <param name="groups"></param>
        private void AddFlowTapControl(string sXmlPath, int iDefaultMax, XmlNodeList groups, Panel panel)
        {
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
                gbChildPanl.Dock = DockStyle.Fill;
                //获取组项
                GroupEntity groupEntity = ClickCopyEntityConvert.getGroupEntity(gpNode);
                toolTip1.SetToolTip(gbChildPanl, groupEntity.Text);
                iNewRow = iDefaultMax;
                if (groupEntity.Max > 0)
                {
                    iNewRow = groupEntity.Max;
                }

                int iItmeIdx = 0;
                foreach (XmlNode item in itemList)
                {
                    //Group中的子项
                    FlowLayoutPanel tlpPanl = new FlowLayoutPanel();
                    tlpPanl.FlowDirection = FlowDirection.LeftToRight;
                    tlpPanl.BorderStyle = BorderStyle.FixedSingle;
                    tlpPanl.Dock = DockStyle.Fill;
                    
                    if (iItmeIdx == 0)
                    {
                        //增加一个按钮显示分类
                        TableLayoutPanel tlpTip = new TableLayoutPanel();
                        tlpTip.RowCount = 1;
                        tlpTip.ColumnCount = 1;
                        tlpTip.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.AutoSize, 20f));
                        tlpTip.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        tlpTip.Height = 20;
                        tlpTip.AutoSize = true;
                        //按钮处理
                        Button btTip = new Button();
                        btTip.Width = 20;
                        btTip.Height = 25;
                        btTip.Anchor = AnchorStyles.Left;
                        string sPaht = GetSystemFullPath(@"Image\fei.png");
                        btTip.BackgroundImage = Image.FromFile(sPaht);
                        btTip.BackgroundImageLayout = ImageLayout.Stretch;
                        toolTip1.SetToolTip(btTip, groupEntity.Text);
                        tlpTip.Controls.Add(btTip, 0, 0);
                        tlpPanl.Controls.Add(tlpTip);
                    }
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
                    CopyItemEntity cs = ClickCopyEntityConvert.getCopyItemEntity(item);
                    if (cs.IsChange)
                    {
                        _isNeedSaveXml = true;
                    }

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
                    iItmeIdx++;
                }
                gbPanl.Controls.Add(gbChildPanl);
                gbChildPanl.Dock = DockStyle.Fill;
                gbChildPanl.AutoSize = true;
                gbPanl.Dock = DockStyle.Top;
                gbPanl.AutoSize = true;

                panel.Controls.Add(gbPanl);
                listFlowLayoutPanel.Add(gbPanl);//增加到集合中
                iGroup++;
            }
        }

        /// <summary>
        /// 点击复制按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bt_Click(object sender, EventArgs e)
        {
            try
            {
                CopyItemEntity cs = (sender as Button).Tag as CopyItemEntity;
                string sText = (cs.tbb as TextBoxBase).Text;
                if(cs.NeedEncrypt && cs.HadEncrypt)
                {
                    sText = cs.TextFact;//取明文
                }
                // 参数动态替换
                Regex regex = new Regex(@"#\w+#", RegexOptions.IgnoreCase);
                MatchCollection mc = regex.Matches(sText);
                foreach (Match m in mc)
                {
                    string sKey = m.Value.Replace("#", "").ToUpper();
                    if (_dicGlobalParam.ContainsKey(sKey))
                    {
                        sText = sText.Replace(m.Value, _dicGlobalParam[sKey]);
                    }
                }
                sText = sText.Replace(_sJHChar, "#"); //将特殊的#替换回来
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

        /// <summary>
        /// 重新加载文件
        /// </summary>
        private void ReloadFile()
        {
            string sXmlPath = txbXmlPath.Text.Trim();
            if (string.IsNullOrEmpty(sXmlPath))
            {
                return;
            }

            if (!File.Exists(sXmlPath))
            {
                ShowErr("文件：" + sXmlPath + "不存在！");
                return;
            }
            foreach (GroupBox gb in listGroupBox)
            {
                _pnlAll.Controls.Remove(gb);
            }
            foreach (FlowLayoutPanel gb in listFlowLayoutPanel)
            {
                _pnlAll.Controls.Remove(gb);
            }
            GenerateControls();
            ShowInfo("文件加载成功！");
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 流式布局复选框选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbFlowDesign_CheckedChanged(object sender, EventArgs e)
        {
            ReloadFile();
        }

        /// <summary>
        /// 保存配置按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sCfgName = txbCfgName.Text.Trim();
            string sCfgPaht = txbXmlPath.Text.Trim();
            if (string.IsNullOrEmpty(sCfgName))
            {
                ShowErr("请输入配置文件名称！");
                return;
            }
            if (string.IsNullOrEmpty(sCfgPaht))
            {
                ShowErr("请输入配置文件路径！");
                return;
            }

            if (MsgHelper.ShowYesNo("确定要保存？") != DialogResult.Yes)
            {
                return;
            }

            string sKeyIdNew;
            bool isAdd = string.IsNullOrEmpty(cbbCfgFile.Text.Trim()) ? true : false;
            DataRow dr;
            if (isAdd)
            {
                //新增
                sKeyIdNew = Guid.NewGuid().ToString();
                dr = _dtConfigFile.NewRow();
                dr[ClickCopyConfigFileStr.Id] = sKeyIdNew;
                _dtConfigFile.Rows.Add(dr);
            }
            else
            {
                //修改
                sKeyIdNew = cbbCfgFile.SelectedValue.ToString();
                DataRow[] drArrKey = _dtConfigFile.Select(ClickCopyConfigFileStr.Id + "='" + sKeyIdNew + "'");
                if (drArrKey.Length == 0)
                {
                    //新增
                    sKeyIdNew = Guid.NewGuid().ToString();
                    dr = _dtConfigFile.NewRow();
                    dr[ClickCopyConfigFileStr.Id] = sKeyIdNew;
                    _dtConfigFile.Rows.Add(dr);
                }
                else
                {
                    dr = drArrKey[0];
                }

            }

            dr[ClickCopyConfigFileStr.Name] = sCfgName;
            dr[ClickCopyConfigFileStr.FilePath] = sCfgPaht;
            dr[ClickCopyConfigFileStr.IsOpenDir] = ckbOpenPath.Checked ? "1" : "0";
            dr[ClickCopyConfigFileStr.IsFlowShow] = ckbFlowDesign.Checked ? "1" : "0";
            _dataCfg.XmlConfig.Save(_dtConfigFile);
            //重新绑定下拉框
            cbbCfgFile.BindDropDownList(_dtConfigFile, ClickCopyConfigFileStr.Id, ClickCopyConfigFileStr.Name, true, true);//绑定下拉框
            ShowInfo("保存成功！");
        }

        /// <summary>
        /// 删除配置按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbbCfgFile.SelectedValue == null)
            {
                ShowInfo("请选择一个配置！");
                return;
            }
            string sKeyIDValue = cbbCfgFile.SelectedValue.ToString();
            if (string.IsNullOrEmpty(sKeyIDValue))
            {
                ShowInfo("请选择一个配置！");
                return;
            }

            if (ShowOkCancel("确定要删除该配置？") == DialogResult.Cancel) return;

            DataRow[] drArrKey = _dtConfigFile.Select(ClickCopyConfigFileStr.Id + "='" + sKeyIDValue + "'");
            if (drArrKey.Length > 0)
            {
                foreach (DataRow dr in drArrKey)
                {
                    _dtConfigFile.Rows.Remove(dr);
                }
                _dtConfigFile.AcceptChanges();
            }
            _dataCfg.XmlConfig.Save();
            //重新绑定下拉框
            cbbCfgFile.BindDropDownList(_dtConfigFile, ClickCopyConfigFileStr.Id, ClickCopyConfigFileStr.Name, true, true);//绑定下拉框
            ShowInfo("删除配置成功！");
        }

        /// <summary>
        /// 配置文件选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbCfgFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCfgFile.SelectedValue == null) return;
            string sCfgId = cbbCfgFile.SelectedValue.ToString();
            DataRow[] drArrKey = _dtConfigFile.Select(ClickCopyConfigFileStr.Id + "='" + sCfgId + "'");
            if (drArrKey.Length == 0)
            {
                return;
            }

            txbCfgName.Text = drArrKey[0][ClickCopyConfigFileStr.Name].ToString();
            txbXmlPath.Text = drArrKey[0][ClickCopyConfigFileStr.FilePath].ToString();
            ckbOpenPath.Checked = "1".Equals(drArrKey[0][ClickCopyConfigFileStr.IsOpenDir].ToString()) ? true : false;
            ckbFlowDesign.Checked = "1".Equals(drArrKey[0][ClickCopyConfigFileStr.IsFlowShow].ToString()) ? true : false;
            btnReloadFile.PerformClick(); //重新加载文件
        }
    }
}
