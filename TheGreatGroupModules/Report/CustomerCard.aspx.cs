using MessagingToolkit.QRCode.Codec;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Report
{
    public partial class CustomerCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = "1:1";
            QRCodeEncoder enc = new QRCodeEncoder();
            enc.QRCodeScale = 5;
            Bitmap qrcode = enc.Encode(code);
            ContractData cd = new ContractData();
            int ContractID = Convert.ToInt32(Request.QueryString["ContractID"]);
            List<ReportCustomerOnCard> dt = cd.GetPayment_OnCard(ContractID);

            using (MemoryStream ms = new MemoryStream())
            {
                qrcode.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                qrcode.Save(Server.MapPath("~/Content/Barcode1.jpg"), System.Drawing.Imaging.ImageFormat.Png);
            }


            string url=new Uri(Server.MapPath("~/Content/Barcode1.jpg")).AbsoluteUri;

            ReportParameter param = new ReportParameter("Path", url);


            string sourceViewReport = @"\Report\CustomerCard.rdlc";
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~/")) + sourceViewReport;
            //ReportDataSource rpt = new ReportDataSource("ClosedAccReport", listData);
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rpt);
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.Refresh();
            string fileType = ".pdf";
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string PDF_FOLDER_FILE = "../PDF/";
            string PDF_FILE_NAME = "CustomerCard";

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            string PDF_File = Convert.ToString((Convert.ToString(PDF_FOLDER_FILE + Convert.ToString("/"))
                + PDF_FILE_NAME) + "_") + fileType;

            //########################### Check Folder ######################################

            string folder = Server.MapPath(PDF_FOLDER_FILE);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            //########################### Check Folder ######################################

            int len = 0;
            using (Stream f = File.Open(Server.MapPath(PDF_File), FileMode.Create))
            {
                if (bytes != null)
                {
                    len = bytes.Length;
                    f.Write(bytes, 0, bytes.Length);
                }
                f.Close();
            }

            Response.Redirect((PDF_File + Convert.ToString("?")) + System.DateTime.Now.ToString());
        }
    }
}