using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Models;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class StaffsController : Controller
    {
        //
        // GET: /Staffs/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Staffs/GetZone
        public JsonResult GetZone()
        {
            // รับค่าราคา
            StaffData st = new StaffData();
            DataTable dt = new DataTable();
            List<ListItems> item = new List<ListItems>();
               dt= st.GetZone();
               if (dt.Rows.Count > 0) { 
                        item = dt.AsEnumerable().Select(dr => new ListItems()
                        {
                            ID = dr.Field<int>("zoneid"),
                            Value = dr.Field<string>("zonename"),

                        }).ToList();

               }
            return Json(new
            {
                data = item,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
        // GET: /Staffs/GetZone
        public JsonResult GetStaffs(int staffroleId,int zoneId)
        {
            // รับค่าราคา
            StaffData st = new StaffData();
            DataTable dt = new DataTable();
            List<ListItems> item = new List<ListItems>();
            dt = st.GetStaff(staffroleId, zoneId);
            if (dt.Rows.Count > 0)
            {
                item = dt.AsEnumerable().Select(dr => new ListItems()
                {
                    ID = dr.Field<int>("StaffID"),
                    Value = dr.Field<string>("StaffTitleName") + dr.Field<string>("StaffFirstName") +" "
                    + dr.Field<string>("StaffLastName"),

                }).ToList();

            }
            return Json(new
            {
                data = item,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
