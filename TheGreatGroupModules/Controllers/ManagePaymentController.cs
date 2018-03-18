using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class ManagePaymentController : Controller
    {
        //
        // GET: /ManagePayment/

        public ActionResult Installment()
        {
            return View();
        }

        public ActionResult CustomerHistory()
        {
            return View();
        }

        public JsonResult PaymentCallData(string CustomerID)
        {
            // รับค่าราคา


            return Json(new
            {
                data = "",
                success = true
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: /ManagePayment/DailyReceiptsReport?staffId=1
        public JsonResult GetDailyReceiptsReport(int staffId, string dateAsOf)
        {

            //DateTime datetime
            try
            {
                List<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();
                ReportData data = new ReportData();
                listData = data.GetDailyReceiptsReport(staffId, dateAsOf);



                return Json(new
                {
                    data = listData,
                    SumData = (listData.Sum(c => c.PriceReceipts)).ToString("#,##0.00"),
                    countData= listData.Count,
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


    
        public JsonResult SaveActivateDailyReceipts(int staffId, string dateAsOf)
        {
            try
            {
               
                ReportData data = new ReportData();
                 data.SaveActivateDailyReceipts(staffId, dateAsOf);



                return Json(new
                {
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
        // GET: /ManagePayment/GetCustomerDetailOnCard?staffId=1?CustomerID=1?ContractID=1
        public JsonResult GetCustomerDetailOnCard(string staffId, string CustomerID, string ContractID)
        {

            try
            {
                IList<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();
                ReportData data = new ReportData();
                listData = data.GetCustomerDetailOnCard(staffId, CustomerID, ContractID);

                IList<LastTransaction> listData1 = new List<LastTransaction>();
                 listData1=   data.GetTransaction(staffId, CustomerID, ContractID);
            listData1= listData1.OrderByDescending(c => c.DateAsOf).ToArray();
                return Json(new
                {
                    data = listData,
                    latest_transaction = listData1,
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
        // GET: /ManagePayment/PostPaymentDailyReceipts
        [HttpPost]
        public JsonResult PostPaymentDailyReceipts(DailyReceiptsReport item)
        {
            try
            {
                List<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();
                CustomersData data = new CustomersData();
                data.PaymentDailyReceipts(item);


                return Json(new
                {
                    data = "บันทึกรายการสำเร็จ",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    data = ex.Message
                }, JsonRequestBehavior.AllowGet);

            }
             
             }



    }
}
