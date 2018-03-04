using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Models
{
    public class Customers
    {
       
        public int CustomerID { get; set; }
        public string CustomerTitleName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerNickName { get; set; }
        public string CustomerName {
            get{
                return CustomerTitleName + CustomerFirstName + " " + CustomerLastName;
            }
        }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerSubDistrict { get; set; }
        public string CustomerDistrict { get; set; }
        public string CustomerProvince { get; set; }
        public string CustomerZipCode { get; set; }
        public int CustomerSubDistrictId { get; set; }
        public int CustomerDistrictId { get; set; }
        public int CustomerProvinceId { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerIdCard { get; set; }
        public string CustomerCareer { get; set; }
           public string   CustomerStatus{ get; set; }
          public string     CustomerPartner{ get; set; }
            public string   CustomerJob{ get; set; }
            public string   CustomerJobYear{ get; set; }
          public string     CustomerSalary{ get; set; }
           public string    CustomerJobAddress{ get; set; }
           public int    CustomerJobSubDistrictId{ get; set; }
           public int CustomerJobDistrictId { get; set; }
           public int CustomerJobProvinceId { get; set; }
          public string     CustomerJobZipCode{ get; set; }
          public string     CustomerSpouseTitle{ get; set; }
           public string    CustomerSpouseFirstName{ get; set; }
           public string    CustomerSpouseLastName{ get; set; }
           public string    CustomerSpouseNickName{ get; set; }
           public string    CustomerSpouseAddress{ get; set; }
           public int CustomerSpouseSubDistrictId { get; set; }
           public int CustomerSpouseDistrictId { get; set; }
           public int CustomerSpouseProvinceId { get; set; }
            public string   CustomerSpouseZipCode{ get; set; }
            public string   CustomerSpouseMobile{ get; set; }
            public string   CustomerSpouseTelephone{ get; set; }
           public int    SaleID{ get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }

        public static IList<Customers> ToObjectList(DataTable dt)
        {
            return dt.AsEnumerable().Select(dr => new Customers()
            {
                CustomerID = dr.Field<int>("CustomerId"),
                CustomerTitleName = dr.Field<string>("CustomerTitleName"),
                CustomerCode = dr.Field<string>("CustomerCode"),
                CustomerNickName = dr.Field<string>("CustomerNickName"),
                CustomerFirstName = dr.Field<string>("CustomerFirstname"),
                CustomerLastName = dr.Field<string>("CustomerLastname"),
                CustomerAddress1 = dr.Field<string>("CustomerAddress1"),
                CustomerAddress2 = dr.Field<string>("CustomerAddress2"),
                CustomerSubDistrict = dr.Field<string>("CustomerSubDistrict"),
                CustomerDistrict = dr.Field<string>("CustomerDistrict"),
                CustomerProvince = dr.Field<string>("CustomerProvince"),
                CustomerZipCode = dr.Field<string>("CustomerZipCode"),
                CustomerEmail = dr.Field<string>("CustomerEmail"),
                CustomerMobile = dr.Field<string>("CustomerMobile"),
                CustomerCareer = dr.Field<string>("CustomerCareer"),
            }).ToList();
        }

        public static IList<Customers> ToObjectList2(DataTable dt)
        {
            return dt.AsEnumerable().Select(dr => new Customers()
            {
                CustomerID = dr.Field<int>("CustomerId"),
                CustomerTitleName = dr.Field<string>("CustomerTitleName"),
                CustomerCode = dr.Field<string>("CustomerCode"),
                CustomerNickName = dr.Field<string>("CustomerNickName"),
                CustomerFirstName = dr.Field<string>("CustomerFirstname"),
                CustomerLastName = dr.Field<string>("CustomerLastname"),
                CustomerAddress1 = dr.Field<string>("CustomerAddress1") 
                 +" ตำบล/แขวง"  + dr.Field<string>("CustomerSubDistrict")
                 + " อำเภอ/เขต" + dr.Field<string>("CustomerDistrict")
                 + " จังหวัด" + dr.Field<string>("CustomerProvince")
                 +" " + dr.Field<string>("CustomerZipCode"),
                CustomerEmail = dr.Field<string>("CustomerEmail"),
                CustomerMobile = dr.Field<string>("CustomerMobile"),
                CustomerIdCard = dr.Field<string>("CustomerIdCard"),
                CustomerCareer = dr.Field<string>("CustomerCareer"),
            }).ToList();
        }
    }
}