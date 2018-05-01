using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Models
{
    public class ListCustomerOnMobile
    {
        public int ContractID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractExpDate { get; set; }
        public string ContractExpDate_Text { get { return ContractExpDate.ToString(@"dd\/MM\/yyyy"); } }
        public DateTime LastDate { get; set; }
        public int ContractPayEveryDay { get; set; }
        public bool ContractSpecialholiday { get; set; }
        public int ContractPeriod { get; set; }
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
            DateTime[] HolidaysArr = Utility.Holidays(1);
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
                if (dt.Rows[i]["ContractPeriod"] != DBNull.Value)
                    obj.ContractPeriod = Convert.ToInt32(dt.Rows[i]["ContractPeriod"].ToString());
                if (dt.Rows[i]["ContractStartDate"] != DBNull.Value)
                    obj.ContractStartDate = Convert.ToDateTime(dt.Rows[i]["ContractStartDate"].ToString());
                if (dt.Rows[i]["ContractExpDate"] != DBNull.Value)
                obj.ContractExpDate = Convert.ToDateTime(dt.Rows[i]["ContractExpDate"].ToString());
                if (dt.Rows[i]["ContractAmount"] != DBNull.Value)
                obj.ContractAmount = Convert.ToDecimal(dt.Rows[i]["ContractAmount"].ToString());
                if (dt.Rows[i]["ContractPayment"] != DBNull.Value)
                obj.ContractPayment = Convert.ToDecimal(dt.Rows[i]["ContractPayment"].ToString());

                // ทุกวัน =2 / จ-ศ =1
                if (dt.Rows[i]["ContractPayEveryDay"] != DBNull.Value)
                    obj.ContractPayEveryDay = Convert.ToInt32(dt.Rows[i]["ContractPayEveryDay"].ToString());

                // 1 เว้นวันหยุด  / 0 ไม่เว้นไม่หยุด
                if (dt.Rows[i]["ContractSpecialholiday"] != DBNull.Value)
                    obj.ContractSpecialholiday = Convert.ToBoolean(dt.Rows[i]["ContractSpecialholiday"].ToString());


                if (dt.Rows[i]["TotalPay"] != DBNull.Value)
                    obj.TotalPay = Convert.ToDecimal(dt.Rows[i]["TotalPay"].ToString());
                else
                    obj.TotalPay = 0;

                if (dt.Rows[i]["lastDate"]!=DBNull.Value)
                    obj.StatusPay = DiffLastTransaction(Convert.ToDateTime(dt.Rows[i]["lastDate"].ToString()), obj.ContractPayEveryDay, obj.ContractSpecialholiday, HolidaysArr);

                list.Add(obj);
            }
            return list;
        }

        public static int DiffLastTransaction(DateTime dateTransaction, int ContractPayEveryDay, bool ContractSpecialholiday, DateTime[] HolidaysArr)
        {

            DateTime dateNow = DateTime.Now.Date;
            double result = 0;
            result = (dateNow - dateTransaction).TotalDays;
            int diffdate = Convert.ToInt32(Math.Floor(result));                                                               
            int totaldate = 0;

            DateTime startDate = dateTransaction;
            DateTime EndDate = dateNow;
            totaldate = diffdate;
            // เว้นวันหยุด
            if (ContractSpecialholiday)
            {
               
                if (ContractPayEveryDay == 1) // ทุกวัน ยกเว้นวันหยุดนขตฤกษ์
                {
                    while (diffdate > 0)
                    {

                        EndDate = EndDate.AddDays(-1);
                        if (Utility.IsHolidays(EndDate, HolidaysArr))
                            totaldate = totaldate- 1;

                        diffdate--;
                    }

                }
                else if (ContractPayEveryDay == 2) 
                {// จัน-ศุก ยกเว้นวันหยุดนขตฤกษ์
                    while (diffdate > 0)
                    {

                        EndDate = EndDate.AddDays(-1);
                        if (EndDate.DayOfWeek == DayOfWeek.Saturday || EndDate.DayOfWeek == DayOfWeek.Sunday || Utility.IsHolidays(EndDate, HolidaysArr))
                            totaldate = totaldate - 1;

                        diffdate--;
                    }
                }
            }
            else  // จ-ศ =1
            {
                   if (ContractPayEveryDay == 1) // ทุกวัน ไม่เว้นวันหยุดนขตฤกษ์
                {
                    totaldate = diffdate;
                }
                   else if (ContractPayEveryDay == 2) 
                   {


                       while (diffdate > 0)
                       {

                           EndDate = EndDate.AddDays(-1);
                           if (EndDate.DayOfWeek == DayOfWeek.Saturday || EndDate.DayOfWeek == DayOfWeek.Sunday)
                               totaldate = totaldate - 1;

                           diffdate--;
                       }
                   
                   }
            }

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