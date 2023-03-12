using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 窗体名称：公共打印预览界面
    /// 创建作者：pansq
    /// 创建日期：2014-10-23
    /// </summary>
    public partial class FrmPrintView : BaseForm
    {
        #region 变量
        private List<ReportParameter> rptParameters = new List<ReportParameter>();
        private List<DataTable> dtList = new List<DataTable>();
        private string rptPath = string.Empty;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPrintView()
        {
            InitializeComponent();
        } 
        #endregion

        #region 属性
        /// <summary>
        /// 返回预览界面的本地报表
        /// </summary>
        public LocalReport Report
        {
            get { return reportViewer1.LocalReport; }
        }


        private DataTable currentOrgData;
        /// <summary>
        /// 当前网点数据
        /// </summary>
        public DataTable CurrentOrgData
        {
            get
            {
                return currentOrgData;
            }
            set
            {
                currentOrgData = value;
            }
        }

        private DataTable parentOrgData;
        /// <summary>
        /// 一级网点数据
        /// </summary>
        public DataTable ParentOrgData
        {
            get
            {
                return parentOrgData;
            }
            set
            {
                parentOrgData = value;
            }
        } 
        #endregion

        #region 窗体加载事件
        private void FrmPrintView_Load(object sender, EventArgs e)
        {
            try
            {
                //1.设置报表文件
                if (!File.Exists(this.rptPath))
                {
                    ShowInfo("请使用SetReportPath方法设置报表文件！");
                    return;
                }

                this.Report.ReportPath = this.rptPath;

                //2.允许外部图片
                this.Report.EnableExternalImages = true;

                //3.添加参数
                if (rptParameters.Count > 0)
                {
                    this.Report.SetParameters(rptParameters);
                }

                //4.设置数据源
                this.Report.DataSources.Clear();
                foreach (DataTable dt in dtList)
                {
                    ReportDataSource dataSource = new ReportDataSource();
                    dataSource.Name = dt.TableName;
                    dataSource.Value = dt;

                    this.Report.DataSources.Add(dataSource);
                }

                //5.刷新报表
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException != null)
                {
                    ShowInfo(ex.InnerException.InnerException.Message);
                }
                else
                {
                    ShowInfo(ex.InnerException.Message);
                }
            }
        } 
        #endregion

        #region 添加报表参数
        /// <summary>
        /// 添加报表参数
        /// </summary>
        /// <param name="name">参数名字，需要与报表文件(rdlc)里的一致</param>
        /// <param name="value">参数值</param>
        public FrmPrintView AddParameter(string name, string value)
        {
            this.rptParameters.Add(new ReportParameter(name, value));
            return this;
        } 
        #endregion

        #region 添加报表参数
        /// <summary>
        /// 添加报表参数
        /// </summary>
        /// <param name="parameter"></param>
        public FrmPrintView AddParameter(ReportParameter parameter)
        {
            this.rptParameters.Add(parameter);
            return this;
        } 
        #endregion

        #region 添加数据集到报表
        /// <summary>
        /// 添加数据集到报表
        /// </summary>
        /// <param name="ds">数据集，请确保每个DataTable的TableName与报表文件里的一致</param>
        /// <returns></returns>
        public FrmPrintView AddDataSource(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                AddDataSource(dt);
            }

            return this;
        } 
        #endregion

        #region 添加数据到报表
        /// <summary>
        /// 添加数据到报表
        /// </summary>
        /// <param name="dt">请确保TableName与报表文件里的一致</param>
        /// <returns></returns>
        public FrmPrintView AddDataSource(DataTable dt)
        {
            if (string.IsNullOrEmpty(dt.TableName))
            {
                throw new Exception("DataTable的TableName不能为空");
            }

            this.dtList.Add(dt);
            return this;
        } 
        #endregion

        #region 设置报表文件路径
        /// <summary>
        /// 设置报表文件路径
        /// </summary>
        /// <param name="moduelName">模块名字，如SE、PA、VE等. 可以为空</param>
        /// <param name="fileName">报表文件名字如：估价单.rdlc</param>
        /// <returns></returns>
        public FrmPrintView SetReportPath(string moduelName, string fileName)
        {
            if (string.IsNullOrEmpty(moduelName))
            {
                this.rptPath = PrintHelper.GetRDLCFullPath(fileName);
            }
            else
            {
                this.rptPath = PrintHelper.GetRDLCFullPath(moduelName + "\\" + fileName);
            }

            return this;
        } 
        #endregion

        #region 设置报表的显示名字
        /// <summary>
        /// 设置报表的显示名字
        /// </summary>
        /// <param name="rptName">报表名字</param>
        public FrmPrintView SetReportName(string rptName)
        {
            this.Report.DisplayName = rptName;
            return this;
        } 
        #endregion

        #region 获取当前专营店品牌的logo参数
        /// <summary>
        /// 获取当前专营店品牌的logo参数
        /// 参数的名字为 &quot;Logo&quot;，请确保报表文件里面的参数名字与此一致。
        /// </summary>
        /// <returns>返回Logo的报表参数</returns>
        public ReportParameter GetDLRLogo()
        {
            return PrintHelper.GetDLRLogo();
        } 
        #endregion

        #region 页面的边距
        /// <summary>
        /// 页面的边距，by X.t@LuniaYS
        /// </summary>
        public class PrintMargins
        {
            /// <summary>
            /// 左边距
            /// </summary>
            public int Left { get; set; }
            /// <summary>
            /// 右边据
            /// </summary>
            public int Right { get; set; }
            /// <summary>
            /// 上边距
            /// </summary>
            public int Top { get; set; }
            /// <summary>
            /// 下边距
            /// </summary>
            public int Bottom { get; set; }
        } 
        #endregion

        #region 打印纸张属性设置
        /// <summary>
        /// 打印纸张属性设置, by X.t@LuniaYS
        /// </summary>
        public class PageSizeSetting
        {
            /// <summary>
            /// 纸张高度 单位：百分之一英寸
            /// </summary>
            public int Height { get; set; }
            /// <summary>
            /// 纸张宽度 单位：百分之一英寸
            /// </summary>
            public int Width { get; set; }
            /// <summary>
            /// 打印的纸张名称
            /// </summary>
            public string PaperName { get; set; }
            /// <summary>
            /// 绑定当前设置到报表控件中
            /// </summary>
            /// <param name="viewer">使用的报表控件</param>
            public void BindPrintViewer(ReportViewer viewer)
            {
                System.Drawing.Printing.PageSettings setting = viewer.GetPageSettings();
                setting.PaperSize.Height = Height;
                setting.PaperSize.Width = Width;
                setting.PaperSize.PaperName = PaperName;
                viewer.SetPageSettings(setting);
            }
        } 
        #endregion

        #region 设置打印的页面设置
        /// <summary>
        /// 设置打印的页面设置
        /// </summary>
        /// <param name="IsLandscape">是否横向打印 true:是 false:否</param>
        /// <param name="IsColorPrint">是否彩色打印 true:是 false:否</param>
        /// <param name="Margins">打印边距设置，可为null(采用默认设置)</param>
        /// <param name="PageSize">打印纸张大小设置，可为null(采用默认设置)</param>
        public void SetPrintPageSettings(bool IsLandscape, bool IsColorPrint, PrintMargins Margins, PageSizeSetting PageSize)
        {
            try
            {
                System.Drawing.Printing.PageSettings setting = this.reportViewer1.GetPageSettings();
                if (setting == null)
                    setting = new System.Drawing.Printing.PageSettings();
                if (Margins != null)
                {
                    System.Drawing.Printing.Margins mar = new System.Drawing.Printing.Margins(Margins.Left, Margins.Right, Margins.Top, Margins.Bottom);
                }
                if (PageSize != null)
                {
                    PageSize.BindPrintViewer(this.reportViewer1);
                }
                setting.Landscape = IsLandscape;
                setting.Color = IsColorPrint;
                this.reportViewer1.SetPageSettings(setting);
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message);
            }
        } 
        #endregion

        #region 获取logo参数
        /// <summary>
        /// 获取logo参数
        /// 参数的名字为 &quot;Logo&quot;，请确保报表文件里面的参数名字与此一致。
        /// </summary>
        /// <param name="carBrandCode">品牌编码</param>
        /// <returns></returns>
        public ReportParameter GetLogo(string carBrandCode)
        {
            return PrintHelper.GetLogo(carBrandCode);
        } 
        #endregion

        #region 生成条形码参数
        /// <summary>
        /// 生成条形码参数
        /// </summary>
        /// <param name="barcodeText">条形码文字</param>
        /// <param name="paraName">参数名字，必须和报表文件中的一致</param>
        /// <returns>返回参数</returns>
        public ReportParameter GetBarcode(string barcodeText, string paraName)
        {
            return PrintHelper.GetBarcode(barcodeText, paraName);
        } 
        #endregion

        #region 生成图片路径参数
        /// <summary>
        /// 生成图片路径参数
        /// </summary>
        /// <param name="filePath">文件路径，必须是绝对路径</param>
        /// <param name="name">参数名字，必须和报表文件中的一致</param>
        /// <returns>返回参数</returns>
        public ReportParameter GetImage(string filePath, string name)
        {
            return PrintHelper.GetImage(filePath, name);
        } 
        #endregion

        #region 关闭页面时触发
        /// <summary>
        /// 关闭页面时触发，这里主要是释放报表对象所用到的资源，在自定义界面一定要释放。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            this.Report.Dispose();
            this.reportViewer1.Dispose();
            base.OnClosed(e);
        } 
        #endregion
    }
}
