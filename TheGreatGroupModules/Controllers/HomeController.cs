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
            return View();
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

    
        
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }

     

          
    }
}
