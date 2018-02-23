using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class SettingData
    {

        private string errMsg = "";
        public IList<Province> GetProvince()
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" SELECT
                                      ProvinceId,
                                      ProvinceName
                                   FROM province where 0=0";
                StrSql += @" Order by ProvinceName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<Province> listData = new List<Province>();
                if (dt != null && dt.Rows.Count > 0)
                {
                   listData = Province.ToObjectList(dt);
                }

                return listData;
     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public IList<District> GetDistrict(int id)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT
                                      DistrictId,
                                      DistrictName,
                                      ProvinceId
                                    FROM district where 0=0";
                if (id > 0)
                {

                    StrSql += @" and ProvinceId=" + id;
                }

                StrSql += @" Order by DistrictName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                IList<District> listData = new List<District>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = District.ToObjectList(dt);
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public IList<SubDistrict> GetSubDistrict(int id)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT
                                      SubDistrictId,
                                      SubDistrictName,
                                      SubDistrictZipCode,
                                      DistrictId
                                    FROM subdistrict where 0=0 ";
                if (id > 0)
                {

                    StrSql += @" and DistrictId=" + id;
                }
                StrSql += @" Order by SubDistrictName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<SubDistrict> listData = new List<SubDistrict>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = SubDistrict.ToObjectList(dt);
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }

        public IList<SubDistrict> GetZipCode(int id)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT
                                      SubDistrictId,
                                      SubDistrictName,
                                      SubDistrictZipCode,
                                      DistrictId
                                    FROM subdistrict where 0=0 ";
                if (id > 0)
                {

                    StrSql += @" and SubDistrictId=" + id;
                }
                StrSql += @" Order by SubDistrictName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<SubDistrict> listData = new List<SubDistrict>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = SubDistrict.ToObjectList(dt);
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjConn.Close();
            }
        }
    }
}