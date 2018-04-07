using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OpenCloseAccountReport()
        {
            return View();
        }


        // GET: /Report/GetOpenAccountReport?zoneId=1&datefrom=2018-04-01&dateto=2018-04-30
        public JsonResult GetOpenAccountReport(int zoneId, string datefrom, string dateto)
        {

            //DateTime datetime
            try
            {
                List<OpenAccountReport> listData = new List<OpenAccountReport>();
                ReportData data = new ReportData();
                listData = data.OpenAccountReports(zoneId, datefrom, dateto);



                return Json(new
                {
                    data = listData,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    errMsg = ex.Message
                }, JsonRequestBehavior.AllowGet);

            }

        }

    }
}
