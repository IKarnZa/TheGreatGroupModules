using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGreatGroupModules.Models;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Report
{
    public partial class ReportPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             int staffID = Convert.ToInt32((Request.Params["staffID"]==null ? Request.Params["staffID"] :"0"));
             string date= Request.Params["date"];
              if (Request.Params["date"] == null)
                date = "04/03/2018";


            IList<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();
            ReportData data = new ReportData();
          
          
             staffID = Convert.ToInt32(Request.Params["staffID"].ToString());
           
            listData = data.GetDailyReceiptsReport(staffID, date);

            string sourceViewReport = @"\Report\Report1.rdlc";
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~/")) + sourceViewReport;
            ReportDataSource rpt = new ReportDataSource("ClosedAccReport", listData);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpt);

          //  ReportViewer1.LocalReport.SetParameters(param);
            string fileType = ".pdf";
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string PDF_FOLDER_FILE = "../PDF/";
            string PDF_FILE_NAME = "DailyReport";

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            string PDF_File = Convert.ToString((Convert.ToString(PDF_FOLDER_FILE + Convert.ToString("/"))
                + PDF_FILE_NAME) +  "_" ) + fileType;

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