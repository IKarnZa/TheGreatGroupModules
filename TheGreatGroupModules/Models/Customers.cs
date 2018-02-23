﻿using System;
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
                CustomerIdCard = dr.Field<string>("CustomerIdCard"),
            }).ToList();
        }
    }
}