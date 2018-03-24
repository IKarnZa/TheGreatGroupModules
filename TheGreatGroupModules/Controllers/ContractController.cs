using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Models;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class ContractController : Controller
    {
        //
        // GET: /Contract/

        public ActionResult Index()
        {
            if (Session["iuser"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Session หมดอายุ , กรูณาเข้าสู่ระบบใหม่อีกครั้ง";
                return RedirectToAction("Login");
            }
          
        }



        // GET: /Customers/GetListContract/:id
        public JsonResult GetListContract(int CustomerID)
        {

            try
            {

                IList<Contract> listContract = new List<Contract>();
                ContractData cd = new ContractData();
                listContract = cd.GetListContract(CustomerID, 0);
                return Json(new
                {
                    data = listContract,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    data = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /Customers/GetContract?CustomerID=id&ContractID=id
        public JsonResult GetContract(int CustomerID, int ContractID)
        {
            try
            {
                List<Province> listData4 = new List<Province>();
                SettingData data = new SettingData();
                listData4 = data.GetProvince();


                List<District> listData1 = new List<District>();
                SettingData data1 = new SettingData();
                listData1 = data.GetDistrict(0);


                List<SubDistrict> listData2 = new List<SubDistrict>();
                SettingData data2 = new SettingData();
                listData2 = data.GetSubDistrict(0);

                IList<Contract> listContract = new List<Contract>();
                ContractData cd = new ContractData();
                listContract = cd.GetContract(CustomerID, ContractID);

                IList<Customers> listData = new List<Customers>();
                CustomersData dataCus = new CustomersData();
                listData = dataCus.Get(CustomerID);

                ProductData dataPro = new ProductData();
                IList<ProductSelect> listProductsSelect = new List<ProductSelect>();


                if (ContractID > 0)
                    listProductsSelect = dataPro.GetProductCustomer(CustomerID, ContractID);
                List<ProductSelect> listProductsSelect1 = new List<ProductSelect>();
                listProductsSelect1 = dataPro.ProductContractSummary(listProductsSelect);


                return Json(new
                {
                    data = listContract,
                    dataCustomers = listData,
                    dataProductSelect = listProductsSelect1,
                    dataProvince = listData4,
                    dataDistrict = listData1,
                    dataSubDistrict = listData2,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    data = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // เพิ่มข้อมูลสัญญา
        // POST: /Customers/PostAdd_NewContract
        [HttpPost]
        public JsonResult PostAdd_NewContract(Contract item)
        {

            ContractData CD = new ContractData();
            int Surety1 = 0;
            int Surety2 = 0;
            int ContractID = 0;
            int parner = 0;
            try
            {

                ContractID = CD.Add_NewContract(item);


                if (item.CustomerSuretyData1 != null)
                {
                    Surety1 = CD.Add_Surety(item.CustomerSuretyData1);
                }

                if (item.CustomerSuretyData2 != null)
                {
                    Surety2 = CD.Add_Surety(item.CustomerSuretyData2);
                }

                if (item.CustomerPartnerData != null)
                {
                    parner = CD.Add_Partner(item.CustomerPartnerData);
                }

                item.ContractID = ContractID;
                item.CustomerSurety1 = Surety1;
                item.CustomerSurety2 = Surety2;
                item.CustomerPartner = parner;

                //Update Product this Contract
                CD.Update_Product_customer(item);
                // getProduct By Contract
                ProductData dataPro = new ProductData();
                IList<ProductSelect> listProductsSelect = new List<ProductSelect>();
                listProductsSelect = dataPro.GetProductCustomer(item.ContractCustomerID, ContractID);
                item.ContractPayment = Convert.ToDecimal(listProductsSelect.Sum(c => c.ProductPrice));
                // Update Contract Surety
                CD.UpdateSurety_In_Contract(item);

                

            


                return Json(new
                {
                    ContractID = ContractID,
                    data = "บันทึกการทำสัญญาสำเร็จ",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ContractID = 0,
                    data = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);

            }

        }


        // แก้ไขข้อมูลสัญญา
        // POST: /Customers/PostEdit_Contract
        [HttpPost]
        public JsonResult PostEdit_Contract(Contract item)
        {
            int Surety1 = 0;
            int Surety2 = 0;
            int parner = 0;
            try
            {
                ContractData CD = new ContractData();
                CD.Edit_NewContract(item);

                //check insert Surety1

                if (item.CustomerSurety1 == 0)
                {
                    Surety1 = CD.Add_Surety(item.CustomerSuretyData1);
                }
                else {
               //     Surety1 = CD.Edit_Surety(item.CustomerSuretyData1);
                }

                
                //check insert Partner
                  if (item.CustomerPartner == 0)
                {
                    parner = CD.Add_Partner(item.CustomerPartnerData);
                }
                else {
                    parner = CD.Update_Partner(item.CustomerPartnerData);
                }


                //check insert Surety1


                //check insert Surety2


                return Json(new
                {
                    data = "บันทึกการแก้ไขสัญญาสำเร็จ",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    data = ex.Message,
                    success = false
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


                ProductData dataPro = new ProductData();
                IList<Products> listProduct = new List<Products>();
                listProduct = dataPro.GetListProduct();

                IList<ProductSelect> listProductsSelect = new List<ProductSelect>();
                listProductsSelect = dataPro.GetProductCustomer(id, 0);

                return Json(new
                {
                    data = listData,
                    dataProduct = listProduct,
                    dataProductSelect = listProductsSelect,
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
