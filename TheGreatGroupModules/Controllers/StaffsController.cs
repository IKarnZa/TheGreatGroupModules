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
                return RedirectToAction("Login", "Home");
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
                return RedirectToAction("Login", "Home");
            }
          
        }
        public ActionResult AddStaff(int staffID)
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

        public ActionResult EditStaff(int staffID)
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

        public ActionResult ListStaffRole()
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
        public ActionResult ListStaffBranch()
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

        public ActionResult SettingPermission(int staffID)
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
        public ActionResult StaffLocation()
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


        #region ::  Manage Staff ::

        public JsonResult GetListStaffs(int staffID)
        {

            try
            {
                StaffData st = new StaffData();
                List<Staffs> item = new List<Staffs>();
                item = st.GetStaff(staffID);

                List<Province> listData4 = new List<Province>();
                SettingData data = new SettingData();
                listData4 = data.GetProvince();


                List<District> listData1 = new List<District>();
                SettingData data1 = new SettingData();
                listData1 = data.GetDistrict(0);


                List<SubDistrict> listData2 = new List<SubDistrict>();
                SettingData data2 = new SettingData();
                listData2 = data.GetSubDistrict(0);

                List<StaffRole> item2 = new List<StaffRole>();
                item2 = st.GetListStaffRole(0);


                return Json(new
                {
                    data = item,
                    dataStaffRole=item2,
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

        [HttpPost]
        public JsonResult AddStaffs(Staffs staff)
        {


            StaffData data = new StaffData();

            try
            {
                if (Session["iuser"] == null)
                    throw new Exception(" Session หมดอายุ , กรุณาเข้าสู่ระบบใหม่อีกครั้ง !! ");

                staff.InsertBy = (Int32)Session["iuser"];

                data.AddStaff(staff);

                return Json(new
                {
                    data = "บันทึกข้อมูลสำเร็จ",
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



        [HttpPost]
        public JsonResult EditStaffs(Staffs staff)
        {


            StaffData data = new StaffData();

            try
            {
                if (Session["iuser"] == null)
                    throw new Exception(" Session หมดอายุ , กรุณาเข้าสู่ระบบใหม่อีกครั้ง !! ");

                staff.UpdateBy = (Int32)Session["iuser"];

                data.EditStaff(staff);

                return Json(new
                {
                    data = "บันทึกข้อมูลสำเร็จ",
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

        #endregion  ::  Manage Staff ::

        #region  :: Manage StaffRole  ::
        // POST:  /Staffs/AddStaffRole
         //  {StaffRoleName:""}
        [HttpPost]
        public JsonResult AddStaffRole(StaffRole role) {


            StaffData data = new StaffData();

            try
            {

                data.AddStaffRole(role);

                  return Json(new
                {
                    data = "บันทึกข้อมูลสำเร็จ",
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

        // POST:  /Staffs/EditStaffRole
        //  {StaffRoleID:"",
        // StaffRoleName:""}
        [HttpPost]
        public JsonResult EditStaffRole(StaffRole role)
        {


            StaffData data = new StaffData();

            try
            {

                data.EditStaffRole(role);

                return Json(new
                {
                    data = "บันทึกข้อมูลสำเร็จ",
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


        // POST:  /Staffs/DeleteStaffRole
        //  {StaffRoleID:""}
        [HttpPost]
        public JsonResult DeleteStaffRole(StaffRole role)
        {


            StaffData data = new StaffData();

            try
            {

                data.DeletedStaffRole(role);

                return Json(new
                {
                    data = "บันทึกข้อมูลสำเร็จ",
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

        // GET: /Staffs/GetListStaffRole?staffroleID=0
        public JsonResult GetListStaffRole(int staffroleID)
        {

            try
            {

                StaffData st = new StaffData();
                List<StaffRole> item = new List<StaffRole>();
                item = st.GetListStaffRole(staffroleID);

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
        #endregion  :: Manage StaffRole  ::





        //api/staffs/GetStaffPermission?staffroleID=1
        public JsonResult GetStaffPermission(int staffroleID)
        {

            try
            {
               
                StaffData st = new StaffData();
                 List<StaffPermission> item = new  List<StaffPermission>();
                 List<StaffRole> item1 = new List<StaffRole>();

                
                item = st.GetStaffPermission();
                item1 =  st.GetListStaffRole(staffroleID);

                return Json(new
                {
                    data = item,
                    dataStaffRole = item1,
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
