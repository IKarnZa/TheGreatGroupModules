
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class CustomersData
    {

        private string errMsg = "";
        public IList<Customers> Get(int id)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" Select * From Customer where Deleted=0 ";

                if (id > 0) { 
                
                   StrSql +=  @" and CustomerID="+id ;
                }
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                IList<Customers> listData = new List<Customers>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = Customers.ToObjectList(dt);
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

        public IList<Customers> Get(Customers item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {
                string StrSql = @"  SELECT c.* ,
                                   s.SubDistrictName as CustomerSubDistrict,
                                   d.DistrictName as CustomerDistrict,
                                   p.ProvinceName as CustomerProvince,
                                   z.ZipCodeName as CustomerZipCode
                                   FROM customer c 
                                  LEFT OUTER JOIN province p ON c.CustomerProvinceId = p.ProvinceId
                                  LEFT OUTER JOIN district d ON c.CustomerDistrictId = d.DistrictId
                                  LEFT OUTER JOIN subDistrict s ON c.CustomerSubDistrictId = s.SubDistrictId
                                  LEFT OUTER JOIN zipcode z ON z.ZipcodeId = c.CustomerZipCodeId
                                where Deleted=0 ";

                if (!String.IsNullOrEmpty(item.CustomerFirstName))
                {

                    StrSql += @" and c.CustomerFirstname like '%" + item.CustomerFirstName + "%' ";
                }

                if (!String.IsNullOrEmpty(item.CustomerLastName))
                {

                    StrSql += @" and c.CustomerLastname like '%" + item.CustomerLastName + "%' ";
                }

                if (!String.IsNullOrEmpty(item.CustomerMobile))
                {

                    StrSql += @" and c.CustomerMobile like '%" + item.CustomerMobile + "%' ";
                }
                if (!String.IsNullOrEmpty(item.CustomerIdCard))
                {

                    StrSql += @" and c.CustomerIdCard like '%" + item.CustomerIdCard + "%' ";
                }
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                IList<Customers> listData = new List<Customers>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = Customers.ToObjectList(dt);
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

        public void AddCustomer(Customers item)
        {
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            
            try
            {
                item.CustomerID = Utility.GetMaxID("Customer", "CustomerID");
                item.CustomerCode = "MB" + item.CustomerID.ToString("000000000");
                string StrSql = @" INSERT INTO db_greatgroup_dev.customer
            (CustomerId,
             CustomerCode,
             CustomerTitleName,
             CustomerFirstName,
             CustomerLastName,
             CustomerNickName,
             CustomerIdCard,
             CustomerAddress1,
             CustomerAddress2,
             CustomerSubDistrictId,
             CustomerDistrictId,
             CustomerProvinceId,
             CustomerZipCodeId,
             CustomerMobile,
             CustomerEmail,
             Activated,
             Deleted)values("
            
             +item.CustomerID+ ","
             + Utility.ReplaceString(item.CustomerCode)+ ","
             + Utility.ReplaceString(item.CustomerTitleName)+ ","
             +Utility.ReplaceString(item.CustomerFirstName)+ ","
             +Utility.ReplaceString(item.CustomerLastName)+ ","
             +Utility.ReplaceString(item.CustomerNickName)+ ","
             +Utility.ReplaceString(item.CustomerIdCard)+ ","
             +Utility.ReplaceString(item.CustomerAddress1)+ ","
             +Utility.ReplaceString(item.CustomerAddress2)+ ","
             + item.CustomerSubDistrictId+ ","
             +  item.CustomerDistrictId+ ","
             +  item.CustomerProvinceId+ ","
             +  item.CustomerZipCode+ ","
             +Utility.ReplaceString(  item.CustomerMobile)+ ","
           + Utility.ReplaceString(item.CustomerEmail) + ","
           + 1 + ","
           +  0+ ")";

                DBHelper.Execute(StrSql, ObjConn);
             
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