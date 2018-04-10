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
    public class SettingController : Controller
    {

        public ActionResult ManageStaffZone(int zoneId)
        {
            if (Session["iuser"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Session หมดอายุ , กรูณาเข้าสู่ระบบใหม่อีกครั้ง";
                return RedirectToAction("Login", "Home");
            }

        } 

        public ActionResult ListZone()
        {
            if (Session["iuser"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Session หมดอายุ , กรูณาเข้าสู่ระบบใหม่อีกครั้ง";
                return RedirectToAction("Login", "Home");
            }
          
        }

        // GET: /Setting/GetLocation/
        public JsonResult GetLocation()
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
            return Json(new
            {
                dataProvince = listData,
                dataDistrict = listData1,
                dataSubDistrict = listData2,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: /Setting/GetProvince/
        public JsonResult GetProvince()
        {

            try
            {

                List<Province> listData = new List<Province>();
                SettingData data = new SettingData();
                listData = data.GetProvince();

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


        // GET: /Setting/GetDistrict/:ids
        public JsonResult GetDistrict(int id)
        {

            try
            {
                List<District> listData = new List<District>();
                SettingData data = new SettingData();
                 listData = data.GetDistrict(id);

           

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
        // GET: /Setting/GetDistrict/:ids
        public JsonResult GetSubDistrict(int id)
        {

            try
            {

                List<SubDistrict> listData = new List<SubDistrict>();
                SettingData data = new SettingData();
                listData = data.GetSubDistrict(id);
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


        // GET: /Setting/GetDistrict/:ids
        public JsonResult GetZipCode(int id)
        {

            try
            {

                List<SubDistrict> listData = new List<SubDistrict>();
                SettingData data = new SettingData();
                listData = data.GetZipCode(id);
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

        #region :: Manage Zone ::


        // GET: /Setting/GetZone
        public JsonResult GetZone()
        {

            try
            {
                // รับค่าราคา
                StaffData st = new StaffData();
                DataTable dt = new DataTable();
                List<ListItems> item = new List<ListItems>();
                dt = st.GetZoneName();
                if (dt.Rows.Count > 0)
                {
                    item = dt.AsEnumerable().Select(dr => new ListItems()
                    {
                        ID = dr.Field<int>("zoneid"),
                        Code = dr.Field<string>("zonecode"),
                        Value = dr.Field<string>("zonename"),

                    }).ToList();

                }
                return Json(new
                {
                    data = item,
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




        // POST: /Setting/GetEditZone
        [HttpPost]
        public JsonResult GetEditZone(Zone zone)
        {

            try
            {
                // รับค่าราคา
                SettingData st = new SettingData();

                st.EditZone(zone);
               
                return Json(new
                {
                    data = " บันทึกการแก้ไขข้อมูลสำเร็จ ",
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


        // POST: /Setting/GetAddZone
        [HttpPost]
        public JsonResult GetAddZone(Zone zone)
        {
            try
            {
                // รับค่าราคา
                SettingData st = new SettingData();

                st.AddZone(zone);

                return Json(new
                {
                    data = " บันทึกการเพิ่มข้อมูลสำเร็จ ",
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



        // POST: /Setting/GetAddStaffZone
        [HttpPost]
        public JsonResult GetAddStaffZone(StaffZone staffzone)
        {
            try
            {
                // รับค่าราคา
                SettingData st = new SettingData();

                st.AddStaffZone(staffzone);

                return Json(new
                {
                    data = " บันทึกการเพิ่มข้อมูลสำเร็จ ",
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

        // POST: /Setting/GetDeleteStaffZone
        //   {StaffID:0,ZoneID:2}
        [HttpPost]
        public JsonResult GetDeleteStaffZone(StaffZone staffzone)
        {
            try
            {
                // รับค่าราคา
                SettingData st = new SettingData();

                st.DeleteStaffZone(staffzone);

                return Json(new
                {
                    data = " บันทึกการเพิ่มข้อมูลสำเร็จ ",
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
        #endregion :: Manage Zone ::
    }
}
