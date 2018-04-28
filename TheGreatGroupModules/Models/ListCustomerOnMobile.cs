using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGreatGroupModules.Models
{
    public class ListCustomerOnMobile
    {
        public int ContractID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ContractExpDate { get; set; }
        public string ContractExpDate_Text { get { return ContractExpDate.ToString(@"dd\/MM\/yyyy"); } }
        public DateTime LastDate { get; set; }
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
                if (dt.Rows[i]["ContractID"] != DBNull.Value)
                obj.ContractID = Convert.ToInt32(dt.Rows[i]["ContractID"].ToString());
                if (dt.Rows[i]["CustomerId"] != DBNull.Value)
                obj.CustomerID = Convert.ToInt32(dt.Rows[i]["CustomerId"].ToString());
                if (dt.Rows[i]["CustomerName"] != DBNull.Value)
                obj.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                if (dt.Rows[i]["ContractNumber"] != DBNull.Value)
                obj.CustomerNumber = dt.Rows[i]["ContractNumber"].ToString();
                if (dt.Rows[i]["ContractExpDate"] != DBNull.Value)
                obj.ContractExpDate = Convert.ToDateTime(dt.Rows[i]["ContractExpDate"].ToString());
                if (dt.Rows[i]["ContractAmount"] != DBNull.Value)
                obj.ContractAmount = Convert.ToDecimal(dt.Rows[i]["ContractAmount"].ToString());
                if (dt.Rows[i]["ContractPayment"] != DBNull.Value)
                obj.ContractPayment = Convert.ToDecimal(dt.Rows[i]["ContractPayment"].ToString());

                if (dt.Rows[i]["TotalPay"] != DBNull.Value)
                    obj.TotalPay = Convert.ToDecimal(dt.Rows[i]["TotalPay"].ToString());
                else
                    obj.TotalPay = 0;

                if (dt.Rows[i]["lastDate"]!=DBNull.Value)
                obj.StatusPay = DiffLastTransaction(Convert.ToDateTime(dt.Rows[i]["lastDate"].ToString()));

                list.Add(obj);
            }
            return list;
        }

        public static int DiffLastTransaction(DateTime dateTransaction)
        {

            DateTime dateNow = DateTime.Now.Date;
            double result = 0;
            result = (dateNow - dateTransaction).TotalDays;
            int totaldate = Convert.ToInt32(Math.Floor(result));

            if (dateTransaction != DateTime.MinValue)
            {

                if (dateNow > dateTransaction)
                {
                    if (totaldate == 1)// วันปัจจุบันยังไม่จ่าย
                    {
                        totaldate = -1;
                        return totaldate;
                    }
                    else
                    {// ไม่ได้จ่ายแล้วหลายวัน
                        return totaldate;
                    }
                }
                else // จ่ายแล้ว
                {
                    return totaldate;

                }
               

            }
            else {
                totaldate = -1;
                return totaldate;
            
            }

            
            

        }



    }


}