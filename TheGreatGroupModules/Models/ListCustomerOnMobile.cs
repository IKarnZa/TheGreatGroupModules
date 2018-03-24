using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Models
{
    public class ListCustomerOnMobile
    {
        public int ContractID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ContractExpDate { get; set; }
        public string ContractExpDate_Text { get { return ContractExpDate.ToString(@"dd\/MM\/yyyy"); } }
        public decimal ContractAmount { get; set; }
        public decimal ContractPayment { get; set; }
        public decimal TotalPay { get; set; }
        public string CustomerNumber { get; set; }
        public string ContractAmount_Text { get { return ContractAmount.ToString("#,##0.00"); } }
        public string ContractPayment_Text { get { return ContractPayment.ToString("#,##0.00"); } }
        public string TotalPay_Text { get { return TotalPay.ToString("#,##0.00"); } }
        public int StatusPay { get; set; }
          public static List<ListCustomerOnMobile> ToObjectList(DataTable dt)
        {

            List<ListCustomerOnMobile> list = new List<ListCustomerOnMobile>();
            ListCustomerOnMobile obj = new ListCustomerOnMobile();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                obj = new ListCustomerOnMobile();
                obj.ContractID = Convert.ToInt32(dt.Rows[i]["ContractID"].ToString());
                obj.CustomerID = Convert.ToInt32(dt.Rows[i]["CustomerId"].ToString());
                obj.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                obj.CustomerNumber = dt.Rows[i]["ContractNumber"].ToString(); 
                obj.ContractExpDate = Convert.ToDateTime(dt.Rows[i]["ContractExpDate"].ToString());
                obj.ContractAmount = Convert.ToDecimal(dt.Rows[i]["ContractAmount"].ToString());
                obj.ContractPayment = Convert.ToDecimal(dt.Rows[i]["ContractPayment"].ToString());
                if (dt.Rows[i]["TotalPay"] != DBNull.Value)
                    obj.TotalPay = Convert.ToDecimal(dt.Rows[i]["TotalPay"].ToString());
                else
                    obj.TotalPay = 0;
                obj.StatusPay = Convert.ToInt32(dt.Rows[i]["StatusPay"].ToString());
                list.Add(obj);
            }
            return list;
          }

    }


}