using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Models;
using CrystalDecisions.Shared;
using TheGreatGroupModules.Modules;
using System.Web.Routing;
namespace TheGreatGroupModules.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PurchaseOrder(int CustomerID)
        {
            return View();
        }

        public ActionResult EditCustomer(int CustomerID)
        {
            return View();
        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        public ActionResult Contract()
        {
            return View();
        }
        public ActionResult Discount()
        {
          
            return View();
        }
        public ActionResult CustomerProduct(int CustomerID)
        {
            ViewBag.CustomerID = CustomerID;

            return View();
        }
        public ActionResult ExportExcel()
        {

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            var cellStyleHeader = workbook.CreateCellStyle();
            cellStyleHeader.Alignment = HorizontalAlignment.Center;
            cellStyleHeader.VerticalAlignment = VerticalAlignment.Center;

            //cellStyleHeader.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            //cellStyleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            //cellStyleHeader.FillPattern = FillPattern.SolidForeground;
            //cellStyleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleHeader.WrapText = true;

            var cellStyleRowRight = workbook.CreateCellStyle();
            cellStyleRowRight.Alignment = HorizontalAlignment.Right;
            cellStyleRowRight.VerticalAlignment = VerticalAlignment.Center;
            cellStyleRowRight.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowRight.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowRight.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowRight.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleRowRight.WrapText = true;

            var cellStyleRowLeft = workbook.CreateCellStyle();
            cellStyleRowLeft.Alignment = HorizontalAlignment.Left;
            cellStyleRowLeft.VerticalAlignment = VerticalAlignment.Center;
            cellStyleRowLeft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowLeft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowLeft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowLeft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleRowLeft.WrapText = true;

            var cellStyleRowCenter = workbook.CreateCellStyle();
            cellStyleRowCenter.Alignment = HorizontalAlignment.Center;
            cellStyleRowCenter.VerticalAlignment = VerticalAlignment.Center;
            cellStyleRowCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleRowCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyleRowCenter.WrapText = true;


            var cellStyleNumber4 = workbook.CreateCellStyle();
            cellStyleNumber4.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0000");
            cellStyleNumber4.Alignment = HorizontalAlignment.Right;
            cellStyleNumber4.VerticalAlignment = VerticalAlignment.Center;
            cellStyleNumber4.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber4.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber4.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber4.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

            var cellStyleNumber6 = workbook.CreateCellStyle();
            cellStyleNumber6.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.000000");
            cellStyleNumber6.Alignment = HorizontalAlignment.Right;
            cellStyleNumber6.VerticalAlignment = VerticalAlignment.Center;
            cellStyleNumber6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleNumber6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

            int _start = 0;
            IRow row1 = sheet1.CreateRow(_start);
            row1.Height = 400;
            IRow row2 = sheet1.CreateRow(_start + 1);
            row2.Height = 400;


            row1.CreateCell(0).SetCellValue("No.");
            row1.Cells[0].CellStyle = cellStyleHeader;
            row2.CreateCell(0).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 0, 0));

            row1.CreateCell(1).SetCellValue("Date As Of");
            row1.Cells[1].CellStyle = cellStyleHeader;
            row2.CreateCell(1).CellStyle = cellStyleHeader;

            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 1, 1));

            row1.CreateCell(2).SetCellValue("Security Code");
            row1.Cells[2].CellStyle = cellStyleHeader;
            row2.CreateCell(2).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 2, 2));

            row1.CreateCell(3).SetCellValue("Portfolio");
            row1.Cells[3].CellStyle = cellStyleHeader;
            row2.CreateCell(3).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 3, 3));

            row1.CreateCell(4).SetCellValue("Fund Name");
            row1.Cells[4].CellStyle = cellStyleHeader;
            row2.CreateCell(4).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 4, 4));

            row1.CreateCell(5).SetCellValue("Fund Code");
            row1.Cells[5].CellStyle = cellStyleHeader;
            row2.CreateCell(5).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 5, 5));

            row1.CreateCell(6).SetCellValue("Securities Type");
            row1.Cells[6].CellStyle = cellStyleHeader;
            row2.CreateCell(6).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 6, 6));

            row1.CreateCell(7).SetCellValue("Unit Amount");
            row1.Cells[7].CellStyle = cellStyleHeader;
            row2.CreateCell(7).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 7, 7));

            row1.CreateCell(8).SetCellValue("Face Amount");
            row1.Cells[8].CellStyle = cellStyleHeader;
            row2.CreateCell(8).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 8, 8));

            row1.CreateCell(9).SetCellValue("MTM");
            row1.Cells[9].CellStyle = cellStyleHeader;
            row2.CreateCell(9).CellStyle = cellStyleHeader;
            sheet1.AddMergedRegion(new CellRangeAddress(_start, _start + 1, 9, 9));

          
            //for (int i = 0; i < datareport.Count; i++)
            //{
            //    IRow row = sheet1.CreateRow((i + 1) + (_start + 1));
            //    row.Height = 310;

            //    row.CreateCell(0).SetCellValue((i + 1).ToString("N0"));
            //    row.Cells[0].CellStyle = cellStyleRowCenter;

            //    row.CreateCell(1).SetCellValue(datareport[i].DateAsOf);
            //    row.Cells[1].CellStyle = cellStyleRowLeft;

            //    row.CreateCell(2).SetCellValue(datareport[i].SECURITYCODE);
            //    row.Cells[2].CellStyle = cellStyleRowLeft;

            //    row.CreateCell(3).SetCellValue(datareport[i].PORTFOLIOCODE);
            //    row.Cells[3].CellStyle = cellStyleRowLeft;

            //    row.CreateCell(4).SetCellValue(datareport[i].PORTFOLIONAME);
            //    row.Cells[4].CellStyle = cellStyleRowLeft;

            //    row.CreateCell(5).SetCellValue(datareport[i].FUNDCODE);
            //    row.Cells[5].CellStyle = cellStyleRowLeft;

            //    row.CreateCell(6).SetCellValue(datareport[i].SECURITYTYPE);
            //    row.Cells[6].CellStyle = cellStyleRowLeft;


            //    row.CreateCell(7).SetCellValue(Convert.ToDouble(datareport[i].UNIT.ToString()));
            //    row.Cells[7].CellStyle = cellStyleNumber4;


            //    row.CreateCell(8).SetCellValue(Convert.ToDouble(datareport[i].FACEAMOUNT.ToString()));
            //    row.Cells[8].CellStyle = cellStyleNumber4;


            //    row.CreateCell(9).SetCellValue((Convert.ToDouble(datareport[i].PRICE.ToString())));
            //    row.Cells[9].CellStyle = cellStyleNumber6;

            //    if (i == datareport.Count - 1)
            //    {
            //        row = sheet1.CreateRow((i + 2) + (_start + 1));
            //        row.Height = 310;
            //        row.CreateCell(0).SetCellValue("");
            //        row.Cells[0].CellStyle = cellStyleRowRight;
            //        row.CreateCell(1).SetCellValue("");
            //        row.Cells[1].CellStyle = cellStyleRowRight;
            //        row.CreateCell(2).SetCellValue("");
            //        row.Cells[2].CellStyle = cellStyleRowRight;
            //        row.CreateCell(3).SetCellValue("");
            //        row.Cells[3].CellStyle = cellStyleRowRight;
            //        row.CreateCell(4).SetCellValue("");
            //        row.Cells[4].CellStyle = cellStyleRowRight;
            //        row.CreateCell(5).SetCellValue("");
            //        row.Cells[5].CellStyle = cellStyleRowRight;

            //        row.CreateCell(6).SetCellValue("Total");
            //        row.Cells[6].CellStyle = cellStyleRowCenter;

            //        row.CreateCell(7).SetCellValue(Convert.ToDouble(datareport.Sum(s => s.UNIT).ToString()));
            //        row.Cells[7].CellStyle = cellStyleNumber4;


            //        row.CreateCell(8).SetCellValue(Convert.ToDouble(datareport.Sum(s => s.FACEAMOUNT).ToString()));
            //        row.Cells[8].CellStyle = cellStyleNumber4;

            //        row.CreateCell(9).SetCellValue("");
            //        row.Cells[9].CellStyle = cellStyleRowRight;
            //    }
            //}
            //sheet1.SetColumnWidth(0, 1400);
            //sheet1.SetColumnWidth(1, 4000);
            //sheet1.SetColumnWidth(2, 4000);
            //sheet1.SetColumnWidth(3, 4000);
            //sheet1.SetColumnWidth(4, 4200);
            //sheet1.SetColumnWidth(5, 4200);
            //sheet1.SetColumnWidth(6, 4200);
            //sheet1.SetColumnWidth(7, 4500);
            //sheet1.SetColumnWidth(8, 5200);
            //sheet1.SetColumnWidth(9, 3300);


            using (var export = new MemoryStream())
            {
                System.Web.HttpContext.Current.Response.Clear();
                workbook.Write(export);
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "HoldingCrossBySecuritiesReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls"));
                System.Web.HttpContext.Current.Response.BinaryWrite(export.GetBuffer());
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
            }

            return View();
        }


        public ActionResult ExportPDF(Customers item)
        {
            IList<Customers> listData = new List<Customers>();
            CustomersData data = new CustomersData();
            listData = data.Get(item);

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("pCondition", listData[0].CustomerFirstName + " " + listData[0].CustomerLastName);

                param.Add("pAddress", "  100 ต.ควนมะพร้าว อ.เมืองพัทลุง จ.พัทลุง 10000 เบอร์โทร 081-476-2091");
            Utility.ExportPDF(
                "RedemptionReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                "~/Report/CustomerReport.rpt",
                 listData,
                param
            );
            return View();
        }
        // GET: /Customers/GetCustomers/:id
        public JsonResult GetCustomers(int id)
        {


            try
            {
                IList<Customers> listData = new List<Customers>();
                CustomersData data = new CustomersData();
                listData = data.Get(id);

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
        // GET: /Customers/GetDataPurchaseData/:id
        public JsonResult GetDataPurchaseData(int id)
        {


            try
            {
                IList<Customers> listData = new List<Customers>();
                CustomersData data = new CustomersData();
                listData = data.Get(id);

                IList<Products> listProduct = new List<Products>();
                ProductData dataPro = new ProductData();
                listProduct = dataPro.GetListProduct();


                //IList<ProductsSelect> listProductsSelect = new List<ProductsSelect>();
                return Json(new
                {
                    data = listData,
                    dataProduct= listProduct,
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

        [HttpPost]
        public JsonResult GetCustomers(Customers item)
        {


            try
            {
                IList<Customers> listData = new List<Customers>();
                CustomersData data = new CustomersData();
                listData = data.Get(item);

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
           [HttpPost]
        public ActionResult AddCustomers(Customers item)
        {
            try
            {
                new CustomersData().AddCustomer(ref item );

                return RedirectToAction("CustomerProduct", new RouteValueDictionary(
    new { controller = "Customers", action = "PurchaseOrder", CustomerID = item.CustomerID }));
           
            }
            catch (Exception ex)
            {
                
                return RedirectToAction("AddCustomer");
            }

           
       
        }


           // GET: /Customers/GetCustomerID/:id

           public JsonResult GetCustomerID(int id)
           {
               List<Province> listData = new List<Province>();
               SettingData data = new SettingData();
               listData = data.GetProvince();


               List<District> listData1 = new List<District>();
               SettingData data1 = new SettingData();
               listData1 = data.GetDistrict(0);


               List<SubDistrict> listData2 = new List<SubDistrict>();
               SettingData data2 = new SettingData();
               listData2 = data.GetSubDistrict(0);

               Customers cus_info= new Customers();
               CustomersData data3=new CustomersData();
               cus_info = data3.GetCustomerInfo_ByID(id);

               return Json(new
               {
                   dataProvince = listData,
                   dataDistrict = listData1,
                   dataSubDistrict = listData2,
                   dataCustomer=cus_info,
                   success = true
               }, JsonRequestBehavior.AllowGet);
           }
        
    }
}
