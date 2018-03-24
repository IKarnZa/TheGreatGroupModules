using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class SettingData
    {

        private string errMsg = "";

        public List<GoldData> GetPriceGold()
        {


            string jsonString = new WebClient().DownloadString(@"http://www.thaigold.info/RealTimeDataV2/gtdata_.txt");

            var list = JsonConvert.DeserializeObject<List<GoldData>>(jsonString);
            string GoldBuy_Text = list[4].bid;
            string GoldSale_Text = list[4].ask;

            List<GoldData> datanew = new List<GoldData>();
            GoldData datagold = new GoldData();
            datagold.name = "T1"; // ทองคำแท่ง 
            datagold.bid = Convert.ToDouble(GoldBuy_Text).ToString("#,##0.00"); //
            datagold.ask = Convert.ToDouble(GoldSale_Text).ToString("#,##0.00");
            datanew.Add(datagold);

            datagold = new GoldData();
            datagold.name = "T2"; // ทองคำรูปพรรณ 
            datagold.bid = (Math.Round((Convert.ToDouble(GoldBuy_Text) - (Convert.ToDouble(GoldBuy_Text) * 0.018)) / 15.16, 0) * 15.16).ToString("#,##0.00");
            datagold.ask = (Convert.ToDouble(GoldSale_Text) + 500).ToString("#,##0.00");
            datanew.Add(datagold);
            return datanew;
        }
        public List<Province> GetProvince()
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
                List<Province> listData = new List<Province>();
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

        public List<District> GetDistrict(int id)
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

                List<District> listData = new List<District>();
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

        public List<SubDistrict> GetSubDistrict(int id)
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
                List<SubDistrict> listData = new List<SubDistrict>();
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

        public List<SubDistrict> GetZipCode(int id)
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
                List<SubDistrict> listData = new List<SubDistrict>();
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