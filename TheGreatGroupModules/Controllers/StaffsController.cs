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

        public ActionResult ListStaff()
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
        public ActionResult AddStaff()
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

        public ActionResult ListStaffRole()
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
        public ActionResult ListStaffBranch()
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

        public ActionResult SettingPermission()
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
        public ActionResult StaffLocation()
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
                            Code =dr.Field<string>("zonecode"),
                            Value = dr.Field<string>("zonename"),

                        }).ToList();

               }
            return Json(new
            {
                data = item,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
       
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

        // GET: /Staffs/GetStaffData?staffID=0&staffroleId=0
        public JsonResult GetStaffData(int staffID,int staffroleId)
        {
            // รับค่าราคา
            StaffData st = new StaffData();
            DataTable dt = new DataTable();
            List<Staffs> staffList=new List<Staffs>();

            try
            {
                dt = st.GetStaffRole(staffID, staffroleId);
                if (dt.Rows.Count > 0)
                {
                    staffList = dt.AsEnumerable().Select(dr => new Staffs()
                    {
                        StaffID = dr.Field<int>("StaffID"),
                        StaffCode = dr.Field<string>("StaffCode"),
                        StaffTitleName = dr.Field<string>("StaffTitleName"),
                        StaffFirstName = dr.Field<string>("StaffFirstName"),
                        StaffLastName = dr.Field<string>("StaffLastName"),
                        StaffRoleID = dr.Field<int>("StaffRoleID"),
                        StaffRoleName = dr.Field<string>("StaffRoleName"),
                        StaffName = dr.Field<string>("StaffTitleName") + dr.Field<string>("StaffFirstName") + " "
                        + dr.Field<string>("StaffLastName"),

                    }).ToList();

                }


                return Json(new
                {
                    data = staffList,
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
    }
}
