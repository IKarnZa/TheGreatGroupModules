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
    }
}
