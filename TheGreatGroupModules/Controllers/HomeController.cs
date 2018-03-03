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
                string jsonString = new WebClient().DownloadString(@"http://www.thaigold.info/RealTimeDataV2/gtdata_.txt");

                var list = JsonConvert.DeserializeObject<List<GoldData>>(jsonString);
                string GoldBuy_Text = list[4].bid;
                string GoldSale_Text = list[4].ask;

                List<GoldData> datanew = new List<GoldData>();
                GoldData datagold = new GoldData();
                datagold.name = "T1"; // ทองคำแท่ง 
                datagold.bid = Convert.ToDouble(GoldBuy_Text).ToString("#,##0.00");
                datagold.ask = Convert.ToDouble(GoldSale_Text).ToString("#,##0.00");
                datanew.Add(datagold);

                datagold = new GoldData();
                datagold.name = "T2"; // ทองคำรูปพรรณ 
                datagold.bid = (Math.Round((Convert.ToDouble(GoldBuy_Text) - (Convert.ToDouble(GoldBuy_Text) * 0.018)) / 15.16, 0) * 15.16).ToString("#,##0.00");
                datagold.ask = (Convert.ToDouble(GoldSale_Text) + 500).ToString("#,##0.00");
                datanew.Add(datagold);

                return Json(new
                {
                    data = datanew,
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
