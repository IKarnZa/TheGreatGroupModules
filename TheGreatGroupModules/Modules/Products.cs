﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Modules
{
    public class Products
    {
        public int No { get; set; }
        public int ProductID { get; set; }
        public string ProductDetail { get; set; }
        public string ProductName { get; set; }
        public string ProductGroupName { get; set; }
        public decimal UnitAmount { get; set; }
        public string UnitAmount_Text
        {
            get
            {
                return UnitAmount.ToString();
        } }
        public string UnitName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductPrice_Text
        {
            get
            {
                return ProductPrice.ToString("#,##0.00");

            }
        }
        public static IList<Products> ToObjectList(DataTable dt)
        {
            return dt.AsEnumerable().Select(dr => new Products()
            {
                ProductID = dr.Field<int>("ProductID"),
                UnitAmount = dr.Field<decimal>("UnitAmount"),
                ProductDetail = dr.Field<string>("ProductGroupName")+" "+dr.Field<string>("ProductName")+
                " (" + dr.Field<decimal>("UnitAmount").ToString() + " " + dr.Field<string>("UnitName") + ")",
                ProductPrice = dr.Field<decimal>("ProductPrice"),
         
            }).ToList();
        }
    }

    public class ProductSelect
    {
      //  public int No { get; set; }
        public int ProductID { get; set; }
       // public string ProductDetail { get; set; }
        public string ProductName { get; set; }
        public int Unit{ get; set; }
        public double ProductPrice { get; set; } // ราคาต่อหน่วย
        public string ProductPrice_Text
        {
            get
            {
                return ProductPrice.ToString("#,##0.00");

            }
        }
        public string TotalPrice { get {
            return (Unit * ProductPrice).ToString("#,#00.00");
        }
        }
        public int ProductGroupID { get; set; }
        public decimal PriceGold { get; set; }
        
    }
}