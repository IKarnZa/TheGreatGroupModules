using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Models;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Session["iuser"] != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Login");
            }
          
        }
        public ActionResult LogIn(StaffLogin login)
        {
            if (!String.IsNullOrEmpty(login.StaffCode) || !String.IsNullOrEmpty(login.StaffPassword) )
            {

                if (Session["iuser"] == null)
                {
                    try
                    {
                        CustomersData data = new CustomersData();
                        login = data.GetStaffLogin(login);
                        Session["iuser"] = login.StaffID;
                        Session["iusername"] = login.StaffName;
                        Session["istaffrole"] = login.StaffRoleID;
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = ex.Message;
                        return View();
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else {
                if (String.IsNullOrEmpty(login.StaffCode) || String.IsNullOrEmpty(login.StaffPassword))
                {
                    TempData["error"] = "กรุณากรอกรหัสพนักงานและรหัสผ่าน";
                    return View();
                }
                else if (String.IsNullOrEmpty(login.StaffCode))
                {
                    TempData["error"] = "กรุณากรอกรหัสพนักงาน";
                    return View();
                }
                else if (String.IsNullOrEmpty(login.StaffPassword))
                {
                    TempData["error"] = "กรุณากรอกรหัสผ่าน";
                    return View();
                }
                else
                {
                    TempData["error"] = "ไม่มีข้อมูลพนักงาน";
                    return View();
                }


            }
           



         

        }

        public ActionResult LogOut()
        {
                Session.Clear();
                return RedirectToAction("Login");
            
          
        }
        
        // GET: /Home/CallGold
        public JsonResult CallGold()
        {
            try
            {
                SettingData data = new SettingData();
                List<GoldData> datanew = new List<GoldData>();
                datanew = data.GetPriceGold();
                return Json(new
                {
                    data = datanew,
                    NewCustomer=3,
                    PriceSaleOfWeek=2,
                    CustomerReceiptCount=20,
                    Closejob = 20,
                    success = true
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex) {
                return Json(new
                {
                    data = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);
            
            }
         
        }



     
        public ActionResult Demo()
        {
            return View();
        }

     

          
    }
}
