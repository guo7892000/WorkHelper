using System;
using System.IO;
using System.Drawing;
using Microsoft.Reporting.WinForms;
using System.Data;
using Breezee.Core.Entity;
using ZXing;
using System.Collections.Generic;


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
    /// 打印辅助类
    /// </summary>
    public class PrintHelper
    {
        #region 变量
        private static string Current_DLR_ID;
        private static string Parent_DLR_ID;
        private static DataTable mCurrentDLRData;
        private static DataTable mFirstDLRData;
        public static LoginUserInfo _LoginUser = WinFormContext.Instance.LoginUser;  //当前登录用户
        #endregion

        #region 构造函数
        static PrintHelper()
        {
            Current_DLR_ID = _LoginUser.ORG_ID;
            Parent_DLR_ID = _LoginUser.BELONG_ORG_ID;
        }
        #endregion

        #region 获取应用程序的根目录
        /// <summary>
        /// 获取应用程序的根目录
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        } 
        #endregion

        #region 获取本地报表文件的绝对路径
        /// <summary>
        /// 获取本地报表文件的绝对路径。
        /// </summary>
        /// <param name="fileName">报表文件的名字</param>
        /// <returns>返回报表文件的绝对路径</returns>
        /// <example>  
        /// <code>
        ///     this.reportViewer1.LocalReport.ReportPath = PrintHelper.GetRDLCFullPath("Demo.rdlc");
        /// </code>
        /// </example>
        /// <exception cref="FileNotFoundException">
        /// 如果报表文件没有找到，将抛出FileNotFoundException的异常
        /// </exception>
        public static string GetRDLCFullPath(string fileName)
        {

            string fullPath = PrintHelper.GetFullPath("RDLC\\" + fileName);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("报表文件没有找到", fileName);
            }

            return fullPath;
        } 
        #endregion

        #region 获取基于应用程序根目录的相对路径的绝对路径
        /// <summary>
        /// 获取基于应用程序根目录的相对路径的绝对路径
        /// </summary>
        /// <param name="path">相对于应用程序根目录的文件或者文件夹</param>
        /// <returns>返回绝对路径</returns>
        private static string GetFullPath(string path)
        {
            return Path.Combine(PrintHelper.ApplicationPath, path);
        } 
        #endregion

        #region 生成当前组织的logo参数
        /// <summary>
        /// 生成当前组织的logo参数
        /// 参数的名字为 &quot;Logo&quot;，请确保报表文件里面的参数名字与此一致。
        /// </summary>
        /// <returns>返回Logo的报表参数</returns>
        public static ReportParameter GetDLRLogo()
        {
            return GetLogo("1");
        } 
        #endregion

        #region 生成当前组织的logo参数
        /// <summary>
        /// 生成当前组织的logo参数
        /// 参数的名字为 &quot;Logo&quot;，请确保报表文件里面的参数名字与此一致。
        /// </summary>
        /// <param name="LogoImageFileNum">Logo图片文件号</param>
        /// <returns></returns>
        public static ReportParameter GetLogo(string LogoImageFileNum)
        {
            if (string.IsNullOrEmpty(LogoImageFileNum))
            {
                throw new ArgumentNullException("LogoImageFileNum");
            }

            string logoImage = string.Format("{0}.jpg", LogoImageFileNum);
            string filePath = PrintHelper.GetFullPath(string.Format("Resources\\Logo\\PrintLogo\\{0}", logoImage));

            return GetImage(filePath, "Logo");
        } 
        #endregion

        #region 生成条形码参数
        /// <summary>
        /// 生成条形码参数
        /// </summary>
        /// <param name="barcodeText">条形码文字</param>
        /// <param name="ReportParameterName">参数名字，必须和报表文件中的一致</param>
        /// <returns>返回参数</returns>
        public static ReportParameter GetBarcode(string barcodeText, string ReportParameterName)
        {
            string appDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.DoNotVerify);
            string filePath = Path.Combine(appDir, string.Format("BarCode_{0}.jpg", _LoginUser.USER_ID ?? "barcode"));

            //BarcodeWriter writer = new BarcodeWriter();
            BarcodeWriterSvg writer = new BarcodeWriterSvg()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 150,
                    Width = 150
                }
            };
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options.PureBarcode = false;
            writer.Options.Margin = 1;
            writer.Options.Width = 160;
            writer.Options.Height = 80;
            writer.Renderer = new ZXing.Rendering.SvgRenderer();
            ZXing.Rendering.SvgRenderer.SvgImage img = writer.Write(barcodeText);
            //img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //img.Dispose();

            return GetImage(filePath, ReportParameterName);
        } 
        #endregion

        #region 生成二维码参数
        /// <summary>
        /// 生成二维码参数
        /// </summary>
        /// <param name="codeText">二维码文字</param>
        /// <param name="ReportParameterName">参数名字，必须和报表文件中的一致</param>
        /// <returns>返回参数</returns>
        public static ReportParameter GetQRcode(string codeText, string ReportParameterName)
        {
            string appDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.DoNotVerify);
            string filePath = Path.Combine(appDir, string.Format("QRCode_{0}.jpg", _LoginUser.USER_ID ?? "QRCode"));
            //BarcodeWriter writer = new BarcodeWriter();
            BarcodeWriterSvg writer = new BarcodeWriterSvg();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.PureBarcode = true;
            writer.Options.Margin = 1;
            writer.Options.Width = 150;
            writer.Options.Height = 150;
            writer.Renderer = new ZXing.Rendering.SvgRenderer();
            ZXing.Rendering.SvgRenderer.SvgImage img = writer.Write(codeText);
            //img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //img.Dispose();
            return GetImage(filePath, ReportParameterName);
        } 
        #endregion

        #region 生成图片路径参数
        /// <summary>
        /// 生成图片路径参数
        /// </summary>
        /// <param name="filePath">文件路径，必须是绝对路径</param>
        /// <param name="ReportParameterName">参数名字，必须和报表文件中的一致</param>
        /// <returns>返回参数</returns>
        public static ReportParameter GetImage(string filePath, string ReportParameterName)
        {
            string value = "file:" + filePath;
            ReportParameter parameter = new ReportParameter(ReportParameterName, value);
            return parameter;
        }
        #endregion
    }
}
