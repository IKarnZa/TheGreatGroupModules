using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Modules
{
    public class ReportData
    {

        private string errMsg = "";

        public void SaveActivateDailyReceipts(int staffId, string date_str)
        {
        
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {
                DateTime dateAsOf = DateTime.ParseExact(date_str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string StrSql = @"   UPDATE daily_receipts SET Activated=1
                                     WHERE  Deleted=0 AND DATE(DateAsOf)='" + dateAsOf.ToString("yyyy-MM-dd") + "'";


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
        public List<DailyReceiptsReport> GetDailyReceiptsReport(int staffId, string date_str)
        {
            DateTime dateAsOf = DateTime.ParseExact(date_str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"    SELECT c.CustomerID,c.CustomerMobile, ct.ContractID ,CONCAT(c.CustomerTitleName,c.CustomerFirstName, '  ', c.CustomerLastName)AS  CustomerName ,c.CustomerNickName,                                  
                                     ct.ContractNumber,ct.ContractCreateDate,ct.ContractExpDate ,ct.ContractAmount,
                                     ct.ContractPayment,SUM( a.PriceReceipts ) AS PriceReceipts,ct.ContractAmountLast,
                                    Case When a.Activated>0 then 'ตรวจสอบแล้ว' else 'รอการตรวจสอบ' end as Status, 
                                    Case When a.Remark  IS NULL then '' else a.Remark end as Remark, 
                                    ( SELECT  ct.ContractPayment- SUM(d.PriceReceipts)  FROM  daily_receipts d
                                    WHERE  d.Deleted=0 AND d.ContractID=ct.ContractID
                                    AND DATE(d.DateAsOf)<='" + dateAsOf.ToString("yyyy-MM-dd") + "')AS Balance "+
                                   @" FROM daily_receipts a
                                    LEFT JOIN Customer c ON  a.CustomerID= c.CustomerId
                                    LEFT JOIN contract ct ON  a.ContractID= ct.ContractID
                                    WHERE 0=0   
                                    AND a.Deleted=0
                                      AND DATE(a.DateAsOf)='" + dateAsOf.ToString("yyyy-MM-dd") + "' " +
                                   "  AND  a.StaffID=" + staffId +
                                   "  GROUP BY  a.CustomerID ORDER BY a.DateAsOf ";
                

                DataTable dt = DBHelper.List(StrSql, ObjConn);
                List<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData=  dt.AsEnumerable().Select(dr => new DailyReceiptsReport()
                    {
                        CustomerID = dr.Field<int>("CustomerID"),
                        ContractID = dr.Field<int>("ContractID"),
                        ContractNumber = dr.Field<string>("ContractNumber"),
                        CustomerName = dr.Field<string>("CustomerName"),
                        ContractCreateDate = dr.Field<DateTime>("ContractCreateDate"),
                        ContractExpDate = dr.Field<DateTime>("ContractExpDate"),
                        ContractAmount = dr.Field<decimal>("ContractAmount"),
                        PriceReceipts = dr.Field<decimal>("PriceReceipts"),
                        Balance = dr.Field<decimal>("Balance"),
                        ContractAmountLast = dr.Field<decimal>("ContractAmountLast"),
                        Status = dr.Field<string>("Status"),
                        Remark = dr.Field<string>("Remark"),
                    }).ToList();
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

        public List<DailyReceiptsReport> GetCustomerDetailOnCard(string staffId, string CustomerID, string ContractID)
        {
              MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {
            List<DailyReceiptsReport> listData = new List<DailyReceiptsReport>();

            string StrSql = @"    SELECT  c.CustomerID, ct.ContractID ,c.CustomerMobile, ct.ContractPayment,
                                       CONCAT(c.CustomerTitleName,c.CustomerFirstName, '  ', c.CustomerLastName)AS  CustomerName 
                                    ,c.CustomerNickName,c.SaleID As StaffID   ,                         
                                     ct.ContractNumber,ct.ContractCreateDate,ct.ContractExpDate ,ct.ContractAmount,
                                             SUM( a.PriceReceipts ) AS PriceReceipts,ct.ContractAmountLast,
                                    ( SELECT  ct.ContractPayment- IFNULL( SUM(d.PriceReceipts),0 )
                                    FROM  daily_receipts d
                                    WHERE  d.Deleted=0 AND d.ContractID=ct.ContractID  )AS Balance,
                                      ( SELECT  ct.ContractAmountLast- IFNULL(SUM(d.Diff) ,0)
                                    FROM  daily_receipts d
                                    WHERE  d.Deleted=0 AND d.ContractID=ct.ContractID  )AS Diff
                                    FROM daily_receipts a
                                    LEFT JOIN Customer c ON  a.CustomerID= c.CustomerId
                                    LEFT JOIN contract ct ON  a.ContractID= ct.ContractID
                                    WHERE 0=0    
                                    AND a.Activated=0
                                    AND a.Deleted=0
                                   AND  c.SaleID=" + staffId +
                                 "  AND a.ContractID=" + ContractID +
                                 "  AND a.CustomerID=" + CustomerID +
                                 "  GROUP BY  a.CustomerID ORDER BY a.DateAsOf ";

            DataTable dt = DBHelper.List(StrSql, ObjConn);
            if (dt != null && dt.Rows.Count > 0)
            {
                listData = DailyReceiptsReport.ToObjectList(dt);
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

        public IList<LastTransaction> GetTransaction(string staffId, string CustomerID, string ContractID)
        {

            IList<LastTransaction> listData = new List<LastTransaction>();
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {
                string StrSql = @" 
                    SELECT tb.* FROM (
                    SELECT DATE(dr.DateAsOf)  AS DateAsOf ,
                    SUM(dr.PriceReceipts) AS Amount 
                    FROM daily_receipts dr
                    LEFT JOIN contract c ON dr.ContractID=c.ContractID
                    WHERE dr.Deleted=0 AND
                    dr.customerID=1 AND 
                    dr.ContractID=1
                    GROUP BY dr.customerID,dr.ContractID,DATE(dr.DateAsOf)
                    ) tb ";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = dt.AsEnumerable().Select(dr => new LastTransaction()
                    {
                        DateAsOf = dr.Field<DateTime>("DateAsOf"),
                        Amount = dr.Field<decimal>("Amount"),
                     
                    }).ToList();
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