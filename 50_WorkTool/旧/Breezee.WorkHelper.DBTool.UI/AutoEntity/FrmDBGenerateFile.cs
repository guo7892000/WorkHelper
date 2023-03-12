using Breezee.WorkHelper.DBTool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Breezee.Core.Tool;
using Breezee.Core.WinFormUI;

namespace Breezee.WorkHelper.DBTool.UI
{
    /// <summary>
    /// 生成程序文件
    /// 测试结果：通过
    /// </summary>
    public partial class FrmDBGenerateFile : BaseForm
    {
        #region 变量
        string _strFileList = "类清单";
        DataSet dsExcel;
        List<EntityInfo> list = new List<EntityInfo>();
        BindingSource bsLayerName = new BindingSource();
        BindingSource bsFileList = new BindingSource();

        //路径
        string IFilePath;
        string BFilePath;
        string DFilePath;
        string IDFilePath;
        string UIFilePath;
        //空间名
        string ISpaceName;
        string BSpaceName;
        string DSpaceName;
        string IDSpaceName;
        string UISpaceName;
        //类名
        string IClassName;
        string BClassName;
        string DClassName;
        string IDClassName;
        string UIClassName;

        //工程名后缀
        public string sIBLL_ProjectEndString
        {
            get
            {
                return txbIBLLProjectEndString.Text.Trim();
            }
        }
        public string sBLL_ProjectEndString
        {
            get
            {
                return txbBLLProjectEndString.Text.Trim();
            }
        }
        public string sIDAL_ProjectEndString
        {
            get
            {
                return txbIDALProjectEndString.Text.Trim();
            }
        }
        public string sDAL_ProjectEndString
        {
            get
            {
                return txbDALProjectEndString.Text.Trim();
            }
        }
        public string sUI_ProjectEndString
        {
            get
            {
                return txbUIProjectEndString.Text.Trim();
            }
        }
        //类文件名前缀
        public string sIBLL_ClassPreString
        {
            get
            {
                return txbIBLLPreStr.Text.Trim();
            }
        }
        public string sBLL_ClassPreString
        {
            get
            {
                return txbBLLPreStr.Text.Trim();
            }
        }
        public string sIDAL_ClassPreString
        {
            get
            {
                return txbIDALPreStr.Text.Trim();
            }
        }
        public string sDAL_ClassPreString
        {
            get
            {
                return txbDALPreStr.Text.Trim();
            }
        }
        public string sUI_ClassPreString
        {
            get
            { return txbUIPreStr.Text.Trim(); }
        }
        #endregion

        #region 构造函数
        public FrmDBGenerateFile()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void FrmDBGenerateFile_Load(object sender, EventArgs e)
        {
            txbIBLLPreStr.Text = "I";
            txbIBLLProjectEndString.Text = "IBLL";

            txbBLLPreStr.Text = "B";
            txbBLLProjectEndString.Text = "BLL";

            txbIDALPreStr.Text = "ID";
            txbIDALProjectEndString.Text = "IDAL";

            txbDALPreStr.Text = "D";
            txbDALProjectEndString.Text = "DAL";

            txbUIPreStr.Text = "Frm";
            txbUIProjectEndString.Text = "UI";

            tsbGenIBDFile.Enabled = false;
            //读取代码文件模板
            txbIBLLFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.I));
            txbBLLFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.B));
            txbIDALFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.ID));
            txbDALFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.D));
            txbUIFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.Frm));
            txbUIDesignFileContent.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.Frm_Designer));
            //参数说明
            rtbPara.Text = File.ReadAllText(GetSystemFullPath(DBTGlobalValue.AutoFile.Para));
            rtbPara.ReadOnly = true;
            //文件生成路径默认为桌面
            txbIBDMainPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        } 
        #endregion

        #region 导入生成IBD文件模板文件
        private void btnImportAutoFile_Click(object sender, EventArgs e)
        {
            dsExcel = ExportHelper.GetExcelDataSet();//导入模板
            if (dsExcel != null)
            {
                bsFileList.DataSource = dsExcel.Tables[0];
                dgvIBDFileList.DataSource = bsFileList;

                lblInfo.Text = "导入成功！";
                tsbGenIBDFile.Enabled = true;
            }
        }
        #endregion

        #region 生成IBD文件
        private void tsbGenIBDFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsExcel == null)
                {
                    ShowInfo("请先导入！");
                    return;
                }

                string strMainPath = txbIBDMainPath.Text.Trim();
                if (string.IsNullOrEmpty(strMainPath))
                {
                    ShowInfo("请选择“生成的根目录”！");
                    return;
                }
                
                DataTable dtClass = (DataTable)(dgvIBDFileList.DataSource as BindingSource).DataSource;
                
                list = new List<EntityInfo>();
                strMainPath = strMainPath + @"\IBD自动生成文件夹\";
               
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    //取出Excel数据
                    string strPath = dtClass.Rows[i]["DIR_PATH"].ToString().Trim().Replace("/", "\\");
                    string sModelName = dtClass.Rows[i]["MODEL_NAME"].ToString().Trim();
                    string sProjectPre = dtClass.Rows[i]["PROJECT_PRE"].ToString().Trim();
                    string sClassName = dtClass.Rows[i]["CLASS_NAME"].ToString().Trim();

                    string sProjectPreString = sProjectPre + ".";
                    //文件路径
                    IFilePath = Path.Combine(strMainPath, sProjectPreString + sIBLL_ProjectEndString, strPath);
                    BFilePath = Path.Combine(strMainPath, sProjectPreString + sBLL_ProjectEndString, strPath);
                    IDFilePath = Path.Combine(strMainPath, sProjectPreString + sIDAL_ProjectEndString, strPath);
                    DFilePath = Path.Combine(strMainPath, sProjectPreString + sDAL_ProjectEndString, strPath);
                    UIFilePath = Path.Combine(strMainPath, sProjectPreString + sUI_ProjectEndString, strPath);
                    //类名
                    IClassName = sIBLL_ClassPreString + sModelName + sClassName;
                    BClassName = sBLL_ClassPreString + sModelName + sClassName;
                    IDClassName = sIDAL_ClassPreString + sModelName + sClassName;
                    DClassName = sDAL_ClassPreString + sModelName + sClassName;
                    UIClassName = sUI_ClassPreString + sModelName + sClassName;
                    //空间名
                    if (ckbNameSpaceAddDirName.Checked)
                    {
                        #region 空间名包括路径
                        string sEndSpaceString = "." + strPath.Replace("\\", ".");
                        ISpaceName = sProjectPreString + sIBLL_ProjectEndString + sEndSpaceString;
                        BSpaceName = sProjectPreString + sBLL_ProjectEndString + sEndSpaceString;
                        IDSpaceName = sProjectPreString + sIDAL_ProjectEndString + sEndSpaceString;
                        DSpaceName = sProjectPreString + sDAL_ProjectEndString + sEndSpaceString;
                        UISpaceName = sProjectPreString + sUI_ProjectEndString + sEndSpaceString;
                        #endregion
                    }
                    else
                    {
                        #region 空间名只使用项目名称：这样方便引用
                        ISpaceName = sProjectPreString + sIBLL_ProjectEndString;
                        BSpaceName = sProjectPreString + sBLL_ProjectEndString;
                        IDSpaceName = sProjectPreString + sIDAL_ProjectEndString;
                        DSpaceName = sProjectPreString + sDAL_ProjectEndString;
                        UISpaceName = sProjectPreString + sUI_ProjectEndString; 
                        #endregion
                    }

                    #region 创建目录
                    if (!Directory.Exists(IFilePath))
                    {
                        Directory.CreateDirectory(IFilePath);
                    }
                    if (!Directory.Exists(BFilePath))
                    {
                        Directory.CreateDirectory(BFilePath);
                    }
                    if (!Directory.Exists(IDFilePath))
                    {
                        Directory.CreateDirectory(IDFilePath);
                    }
                    if (!Directory.Exists(DFilePath))
                    {
                        Directory.CreateDirectory(DFilePath);
                    }
                    if (!Directory.Exists(UIFilePath))
                    {
                        Directory.CreateDirectory(UIFilePath);
                    }
                    #endregion

                    #region 生成程序文件
                    
                    #region 旧方式已取消
                    //GenerateIFile();//I
                    //GenerateBFile();//B
                    //GenerateIDFile();//ID
                    //GenerateDFile();//D
                    //GenerateUIFile();//UI
                    //GenerateUIDesignerFile();//UI_Des 
                    #endregion

                    GenerateFile(IFilePath, txbIBLLFileContent, ISpaceName, IClassName);
                    GenerateFile(BFilePath, txbBLLFileContent, BSpaceName, BClassName);
                    GenerateFile(IDFilePath, txbIDALFileContent, IDSpaceName, IDClassName);
                    GenerateFile(DFilePath, txbDALFileContent, DSpaceName, DClassName);
                    GenerateFile(UIFilePath, txbUIFileContent, UISpaceName, UIClassName);
                    GenerateFile(UIFilePath, txbUIDesignFileContent, UISpaceName, UIClassName, ".Designer.cs"); 
                    #endregion
                }
                //提示成功
                ShowInfo("生成成功！");
                lblInfo.Text = "生成成功！";
            }
            catch (Exception ex)
            {
                ShowErr(ex.Message);
            }
        }
        #endregion

        #region 生成文件方法
        private void GenerateFile(string sFilePath, RichTextBox rtbCode, string sSpaceName, string sClassName, string sFileExtend = ".cs")
        {
            using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(sFilePath, sClassName + sFileExtend), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
            {
                foreach (string sRow in rtbCode.Lines)
                {
                    string sNew = sRow.Replace("#NAME_SPACE_I#", ISpaceName)
                                .Replace("#NAME_SPACE_B#", BSpaceName)
                                .Replace("#NAME_SPACE_ID#", IDSpaceName)
                                .Replace("#NAME_SPACE_D#", DSpaceName)
                                .Replace("#NAME_SPACE_UI#", UISpaceName)
                                //类名替换
                                .Replace("#CLASS_NAME_I#", IClassName)
                                .Replace("#CLASS_NAME_B#", BClassName)
                                .Replace("#CLASS_NAME_ID#", IDClassName)
                                .Replace("#CLASS_NAME_D#", DClassName)
                                .Replace("#CLASS_NAME_UI#", UIClassName);
                    writer.WriteLine(sNew);
                }
                
                list.Add(new EntityInfo(sSpaceName, "public", sClassName, Path.Combine(sFilePath, sClassName + sFileExtend)));
            }
        } 
        #endregion

        #region 生成文件方法(已取消旧方式：文件内容在代码里)
        //private void GenerateIFile()
        //{
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(IFilePath,IClassName + ".cs") , FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using Breezee.AutoSQLExecutor.Core;");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace " + IDSpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpublic class " + IClassName + " : BaseBUS");//继承一个基本的BUS抽象类，方便扩展功能
        //        writer.WriteLine("\t{");
        //        writer.WriteLine("\t");
        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(IDSpaceName, "public", IClassName, Path.Combine(IFilePath, IClassName + ".cs")));
        //    }
        //}

        //private void GenerateBFile()
        //{
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(BFilePath,BClassName + ".cs") , FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {

        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using Breezee.AutoSQLExecutor.Core;");//公共数据访问类
        //        writer.WriteLine("using Breezee.Core.Tool;");//工具类
        //        writer.WriteLine("using Breezee.Core.IOC;");//IOC容器类
        //        writer.WriteLine("using Breezee.Core.Entity;");//公共实体类
        //        writer.WriteLine("using " + ISpaceName + ";");
        //        writer.WriteLine("using " + IDSpaceName + ";");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace " + BSpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpublic class " + BClassName + " : " + IClassName);//继承一个实现了BUS接口的基本类，方便扩展功能
        //        writer.WriteLine("\t{");
        //        writer.WriteLine("\t");
        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(BSpaceName, "public", BClassName, Path.Combine(BFilePath, BClassName + ".cs")));
        //    }
        //}

        //private void GenerateDFile()
        //{
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(DFilePath, DClassName + ".cs"), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using System.Linq;");
        //        writer.WriteLine("using System.Data;");
        //        writer.WriteLine("using Breezee.AutoSQLExecutor.Core;");//公共数据访问类
        //        writer.WriteLine("using Breezee.Core.Tool;");//工具类
        //        writer.WriteLine("using Breezee.Core.IOC;");//IOC容器类
        //        writer.WriteLine("using Breezee.Core.Entity;");//公共实体类
        //        writer.WriteLine();
        //        writer.WriteLine("namespace " + DSpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpublic class " + DClassName+ " : "+ IDClassName);
        //        writer.WriteLine("\t{");
        //        writer.WriteLine("\t");
        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(DSpaceName, "public", DClassName, Path.Combine(DFilePath , DClassName + ".cs")));
        //    }
        //}

        //private void GenerateIDFile()
        //{
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(IDFilePath , IDClassName + ".cs"), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using System.Linq;");
        //        writer.WriteLine("using System.Data;");
        //        writer.WriteLine("using Breezee.AutoSQLExecutor.Core;");//公共数据访问类
        //        writer.WriteLine();
        //        writer.WriteLine("namespace " + IDSpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpublic class " + DClassName + " : IBaseDAC");
        //        writer.WriteLine("\t{");
        //        writer.WriteLine("\t");
        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(IDSpaceName, "public", DClassName, Path.Combine(IDFilePath, IDClassName + ".cs")));
        //    }
        //}

        //private void GenerateUIFile()
        //{
        //    //生成FrmXX.cs文件
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(UIFilePath, UIClassName + ".cs"), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using System.ComponentModel;");
        //        writer.WriteLine("using System.Data;");
        //        writer.WriteLine("using System.Drawing;");
        //        writer.WriteLine("using System.Linq;");
        //        writer.WriteLine("using System.Windows.Forms;");
        //        //以下为非系统的空间
        //        writer.WriteLine("using Breezee.Core.WinFormUI;");//UI基类
        //        writer.WriteLine("using Breezee.AutoSQLExecutor.Core;");//公共数据访问类
        //        writer.WriteLine("using Breezee.Core.Tool;");//工具类
        //        writer.WriteLine("using Breezee.Core.IOC;");//IOC容器类
        //        writer.WriteLine("using Breezee.Core.Entity;");//公共实体类
        //        //动态空间名
        //        writer.WriteLine("using " + ISpaceName + ";");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace " + UISpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpublic partial class " + UIClassName + " : BaseForm");
        //        writer.WriteLine("\t{");
        //        writer.WriteLine("\t\tpublic " + UIClassName + "()");
        //        writer.WriteLine("\t\t{");
        //        writer.WriteLine("\t\t\tInitializeComponent();");
        //        writer.WriteLine("\t\t}");
        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(UISpaceName, "public", UIClassName, Path.Combine(UIFilePath, UIClassName + ".cs")));
        //    }
        //}

        //private void GenerateUIDesignerFile()
        //{
        //    //生成FrmXX.Designer.cs文件
        //    using (TextWriter writer = new StreamWriter(new BufferedStream(new FileStream(Path.Combine(UIFilePath, UIClassName + ".Designer.cs"), FileMode.Create, FileAccess.Write)), Encoding.GetEncoding("gbk")))
        //    {
        //        writer.WriteLine("namespace " + UISpaceName);
        //        writer.WriteLine("{");
        //        writer.WriteLine("\tpartial class " + UIClassName);
        //        writer.WriteLine("\t{");

        //        writer.WriteLine("\t\t/// <summary>");
        //        writer.WriteLine("\t\t/// Required designer variable.");
        //        writer.WriteLine("\t\t/// </summary>");
        //        writer.WriteLine("\t\tprivate System.ComponentModel.IContainer components = null;");
        //        writer.WriteLine("");

        //        writer.WriteLine("\t\t/// <summary>");
        //        writer.WriteLine("\t\t/// Clean up any resources being used.");
        //        writer.WriteLine("\t\t/// </summary>");
        //        writer.WriteLine("\t\t/// <param name=\"disposing\">true if managed resources should be disposed; otherwise, false.</param>");
        //        writer.WriteLine("\t\tprotected override void Dispose(bool disposing)");
        //        writer.WriteLine("\t\t{");
        //        writer.WriteLine("\t\t\tif (disposing && (components != null))");
        //        writer.WriteLine("\t\t\t{");
        //        writer.WriteLine("\t\t\t\tcomponents.Dispose();");
        //        writer.WriteLine("\t\t\t}");
        //        writer.WriteLine("\t\t\tbase.Dispose(disposing);");
        //        writer.WriteLine("\t\t}");
        //        writer.WriteLine("");

        //        writer.WriteLine("\t\t#region Windows Form Designer generated code");
        //        writer.WriteLine("\t\t/// <summary>");
        //        writer.WriteLine("\t\t/// Required method for Designer support - do not modify");
        //        writer.WriteLine("\t\t/// the contents of this method with the code editor.");
        //        writer.WriteLine("\t\t/// </summary>");
        //        writer.WriteLine("\t\tprivate void InitializeComponent()");
        //        writer.WriteLine("\t\t{");
        //        writer.WriteLine("\t\t\tthis.components = new System.ComponentModel.Container();");
        //        writer.WriteLine("\t\t\tthis.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;");
        //        writer.WriteLine("\t\t\tthis.Text = \"" + UIClassName + "\";");
        //        writer.WriteLine("\t\t}");
        //        writer.WriteLine("\t\t#endregion");
        //        writer.WriteLine("");

        //        writer.WriteLine("\t}");
        //        writer.WriteLine("}");
        //        list.Add(new EntityInfo(UISpaceName, "public", UIClassName, Path.Combine(UIFilePath, UIClassName + ".Designer.cs")));
        //    }
        //}
        #endregion

        #region 选择生成IBD文件目录
        private void btnIBDSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txbIBDMainPath.Text = dialog.SelectedPath;
            }
        }
        #endregion

        #region 模板下载事件
        private void tsbDownloadModel_Click(object sender, EventArgs e)
        {
            DBToolUIHelper.DownloadFile(DBTGlobalValue.AutoFile.Excel_IBD, "模板_生成IBD文件", true);
        }
        #endregion

        #region 退出按钮事件
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        
    }
}
