﻿using Breezee.Framework.BaseUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Breezee.Framework.Tool;
using System.IO;
using Breezee.WorkHelper.DBTool.Entity;

namespace Breezee.WorkHelper.DBTool.UI.StringBuild
{
    /// <summary>
    /// 点击复制字符或打开目录
    /// </summary>
    public partial class FrmDBTClickCopyStringAuto : BaseForm
    {
        public FrmDBTClickCopyStringAuto()
        {
            InitializeComponent();
        }

        private void FrmDBTClickCopyStringAuto_Load(object sender, EventArgs e)
        {
            txbXmlPath.Text = Path.Combine(DBTGlobalValue.AppPath,DBTGlobalValue.StringBuild.Xml_CopyString);
            GenerateControls();
        }

        private void GenerateControls()
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
            iDefaultMax = int.Parse(root.GetOrDefaultAttrValue(CopyStringPropertyName.GroupMax, iDefaultMax.ToString()));
            XmlNodeList groups = doc.SelectNodes("strings/group");

            int iNewRow = 4;
            int iGroup = 0;
            GroupBox gb;
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                XmlNode gpNode = groups[i];
                if (gpNode.ChildNodes.Count == 0) continue;
                XmlNodeList itemList = gpNode.SelectNodes("string");
                if (itemList.Count == 0) continue;

                gb = new GroupBox();
                if (gpNode.TryGetAttrValue(CopyStringPropertyName.GroupText, out sText))
                {
                    gb.Text = sText;
                    gb.ForeColor = Color.Red;
                }

                iNewRow = int.Parse(gpNode.GetOrDefaultAttrValue(CopyStringPropertyName.GroupMax, iDefaultMax.ToString()));

                TableLayoutPanel tlp = new TableLayoutPanel();
                double d = itemList.Count < iNewRow ? 1.0 : itemList.Count * 1.0 / iNewRow;
                tlp.RowCount = int.Parse(Math.Ceiling(d).ToString());
                tlp.ColumnCount = 3 * iNewRow + 1;
                tlp.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.AutoSize, 20f));

                tlp.Height = int.Parse(Math.Ceiling(32f * tlp.RowCount).ToString());

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

                    lb.ForeColor = Color.Black;

                    CopyString cs = getCopyString(item);
                    if (cs == null) continue;
                    if (cs.Ctrol.EqualsIgnorEmptyCase("RichTextBox"))
                    {
                        tb = new RichTextBox();
                        if (cs.Type.EqualsIgnorEmptyCase("file"))
                        {
                            if (!string.IsNullOrWhiteSpace(cs.PathRel))
                            {
                                tb.AppendText(File.ReadAllText(Path.Combine(DBTGlobalValue.AppPath, cs.PathRel)));
                                cs.Tip = string.Format("文本框是相对路径【{0}】文件的内容", cs.PathRel);
                            }
                            if (!string.IsNullOrWhiteSpace(cs.PathAbs))
                            {
                                tb.AppendText(File.ReadAllText(cs.PathAbs));
                                cs.Tip = string.Format("文本框是绝对路径【{0}】文件的内容", cs.PathAbs);
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
                    bt.Width = 20;
                    bt.Height = 23;
                    bt.Anchor = AnchorStyles.Left;
                    bt.Tag = cs;
                    bt.Text = ".";

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

                iGroup++;
            }
            pnlAll.Controls.Add(gbGlobal);
            pnlAll.AutoScroll = true;
            gbGlobal.Dock = DockStyle.Top;
        }

        void bt_Click(object sender, EventArgs e)
        {
            CopyString cs = (sender as Button).Tag as CopyString;
            string sText = (cs.tbb as TextBoxBase).Text;
            Clipboard.SetText(sText);
            if (cs.Type.EqualsIgnorEmptyCase("path") && ckbOpenPath.Checked)
            {
                if (Directory.Exists(sText))
                {
                    System.Diagnostics.Process.Start("explorer.exe", sText);
                }
            }
        }

        /// <summary>
        /// 点击获取毫秒数示例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetMillisecond_Click(object sender, EventArgs e)
        {
            CopyString cs = (sender as Button).Tag as CopyString;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string sText = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            (cs.tbb as TextBoxBase).Text = sText;
            Clipboard.SetText(sText);
        }

        private void TsbDownLoad_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.StringBuild.Xml_CopyString, "点击拷贝字符模板", true);
        }

        private CopyString getCopyString(XmlNode xn)
        {
            CopyString cs = null;
            string sText = "";
            if(xn.TryGetAttrValue(CopyStringPropertyName.StringType,out sText))
            {
                cs = new CopyString();
                cs.Type = sText;
                if(xn.TryGetAttrValue(CopyStringPropertyName.StringCtrol,out sText))
                {
                    cs.Ctrol = sText;
                }
                else
                {
                    return null;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringLable, out sText))
                {
                    cs.Lable = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringPathAbs, out sText))
                {
                    cs.PathAbs = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringPathRel, out sText))
                {
                    cs.PathRel = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringPwdchar, out sText))
                {
                    cs.Pwdchar = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringText, out sText))
                {
                    cs.Text = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringTip, out sText))
                {
                    cs.Tip = sText;
                }
                if (xn.TryGetAttrValue(CopyStringPropertyName.StringMethod, out sText))
                {
                    cs.Method = sText;
                }
            }
            return cs;
        }

        private void BtnSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "(*.xml)|*.xml";
            dia.Multiselect = false;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                txbXmlPath.Text = dia.FileName;
                GenerateControls();
            }
        }

        private void TsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    class CopyStringPropertyName
    {
        public static string GroupText="text";
        public static string GroupMax = "max";
        public static string StringType = "type";
        public static string StringCtrol = "ctrol";
        public static string StringLable = "label";
        public static string StringTip = "tip";
        public static string StringText = "text";
        public static string StringPwdchar = "pwdchar";
        public static string StringPathAbs = "pathAbs";
        public static string StringPathRel = "pathRel";
        public static string StringMethod = "method";
    }

    class CopyString
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
        public TextBoxBase tbb;
    }

}
