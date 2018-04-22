using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Modules
{
    

    /*  บันทึกค่างวดรายวัน */
    public class DailyReceiptsReport 
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNickName { get; set; }
        public DateTime ContractCreateDate { get; set; }
        public DateTime ContractExpDate  { get; set; }
        public decimal ContractAmountLast { get; set; }
        public decimal ContractAmount { get; set; }
        
        public decimal PriceReceipts { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalSales { get; set; }
        public string ContractCreateDate_Text { get { return ContractCreateDate.ToString("dd/MM/yyyy"); } }
        public string ContractExpDate_Text { get { return ContractExpDate.ToString("dd/MM/yyyy"); } }
        public string ContractAmount_Text { get { return ContractAmount.ToString("#,##0.00"); } }
        public string PriceReceipts_Text { get { return PriceReceipts.ToString("#,##0.00"); } }
        public string Balance_Text { get { return Balance.ToString("#,##0.00"); } }
        public string TotalSales_Text { get { return TotalSales.ToString("#,##0.00"); } }
        public int StaffID { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public decimal TotalBalance { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        
        
        public static List<DailyReceiptsReport> ToObjectList(DataTable dt)
        {
            return dt.AsEnumerable().Select(dr => new DailyReceiptsReport()
            {
                CustomerID = dr.Field<int>("CustomerID"),
                ContractID = dr.Field<int>("ContractID"),
                ContractNumber = dr.Field<string>("ContractNumber"),
                CustomerName = dr.Field<string>("CustomerName"),
                CustomerNickName = dr.Field<string>("CustomerNickName"),
                ContractCreateDate = dr.Field<DateTime>("ContractCreateDate"),
                ContractExpDate = dr.Field<DateTime>("ContractExpDate"),
                ContractAmount = dr.Field<decimal>("ContractAmount"),
                PriceReceipts = dr.Field<decimal>("PriceReceipts"),
                Balance = dr.Field<decimal>("Balance"),
                TotalBalance = dr.Field<decimal>("ContractPayment"),
                ContractAmountLast = dr.Field<decimal>("ContractAmountLast"),
                PhoneNumber = dr.Field<string>("CustomerTelephone"),
                MobileNumber = dr.Field<string>("CustomerMobile"),
                StaffID = dr.Field<int>("StaffID"),
            }).ToList();
        }
    }

    public class Transaction
    {
    
        public int CustomerID { get; set; }
        public int ContractID { get; set; }
        public DateTime DateAsOf { get; set; }
        public string DateAsOf_Text { get { return DateAsOf.ToString("dd/MM/yyyy"); } }
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; set; } // ยอดเงินคงเหลือ
        public decimal ContractAmount { get; set; } // งวดละ
        public decimal ContractPayment { get; set; } //  จำนวนเงินทั้งหมด
        public string Balance_Text { get; set; } // ยอดเงินคงเหลือ
        public string ContractAmount_Text { get; set; } // งวดละ
        public string ContractPayment_Text { get; set; } // จำนวนเงินทั้งหมด
    }

     public class LastTransaction
    {
        public DateTime DateAsOf { get; set; }
        public string Date { get { return DateAsOf.ToString("dd/MM/yyyy"); } }
        public decimal Amount { get; set; } // งวดละ
    }

     public class OpenAccountReport
     {

         public int CustomerID { get; set; }
         public int ContractID { get; set; }
         public string ContractNumber { get; set; }
         public string CustomerName { get; set; }
         public DateTime ContractCreateDate { get; set; }
         public string ContractCreateDate_Text { get { return ContractCreateDate.ToString("dd/MM/yyyy"); } }
         public DateTime ContractExpDate { get; set; }
         public string ContractExpDate_Text { get { return ContractExpDate.ToString("dd/MM/yyyy"); } }
       
         public decimal TotalPayment { get; set; } // ยอดสินเชื่อ
         public decimal CostAmount { get; set; } // ราคาทุน
         public string TotalPayment_Text { get { return TotalPayment.ToString("#,##0.00"); } } // ยอดสินเชื่อ
         public string CostAmount_Text { get { return CostAmount.ToString("#,##0.00"); } } // ราคาทุน


         public static List<OpenAccountReport> ToObjectList(DataTable dt)
         {
             return dt.AsEnumerable().Select(dr => new OpenAccountReport()
             {
                 CustomerID = dr.Field<int>("ContractCustomerID"),
                 ContractID = dr.Field<int>("ContractID"),
                 ContractNumber = dr.Field<string>("ContractNumber"),
                 CustomerName = dr.Field<string>("CustomerName") +
                                dr.Field<string>("CustomerMobile") +  
                                " ที่อยู่ "+  dr.Field<string>("CustomerAddress"),
                 ContractCreateDate = dr.Field<DateTime>("ContractCreateDate"),
                 ContractExpDate = dr.Field<DateTime>("ContractExpDate"),
                 TotalPayment = dr.Field<decimal>("ContractPayment"),
                 CostAmount = dr.Field<decimal>("PriceCost"),
              
             }).ToList();
         }

     }

}