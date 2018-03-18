
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

                string StrSql = @"  SELECT c.* ,
                                   s.SubDistrictName as CustomerSubDistrict,
                                   d.DistrictName as CustomerDistrict,
                                   p.ProvinceName as CustomerProvince
                                   FROM customer c 
                                  LEFT OUTER JOIN province p ON c.CustomerProvinceId = p.ProvinceId
                                  LEFT OUTER JOIN district d ON c.CustomerDistrictId = d.DistrictId
                                  LEFT OUTER JOIN subDistrict s ON c.CustomerSubDistrictId = s.SubDistrictId
                                where Deleted=0 ";

                if (id > 0) { 
                
                   StrSql +=  @" and c.CustomerID="+id ;
                }
                DataTable dt = DBHelper.List(StrSql, ObjConn);

                IList<Customers> listData = new List<Customers>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = Customers.ToObjectList2(dt);
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
                                   p.ProvinceName as CustomerProvince
                                   FROM customer c 
                                  LEFT OUTER JOIN province p ON c.CustomerProvinceId = p.ProvinceId
                                  LEFT OUTER JOIN district d ON c.CustomerDistrictId = d.DistrictId
                                  LEFT OUTER JOIN subDistrict s ON c.CustomerSubDistrictId = s.SubDistrictId
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
                    listData = Customers.ToObjectList2(dt);
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

        public void AddCustomer(ref Customers item)
        {
            item.CustomerID = Utility.GetMaxID("Customer", "CustomerID");
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {
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
             CustomerSubDistrictId,
             CustomerDistrictId,
             CustomerProvinceId,
             CustomerZipCode,
             CustomerMobile, 
             CustomerTelephone,
             CustomerStatus,
             CustomerEmail,
             CustomerCareer,
             CustomerJob,
             CustomerJobYear,
             CustomerSalary,
             CustomerJobAddress,
             CustomerJobSubDistrictId,
             CustomerJobDistrictId,
             CustomerJobProvinceId,
             CustomerJobZipCode,
             CustomerSpouseTitle,
             CustomerSpouseFirstName,
             CustomerSpouseLastName,
             CustomerSpouseNickName,
             CustomerSpouseAddress,
             CustomerSpouseSubDistrictId,
             CustomerSpouseDistrictId,
             CustomerSpouseProvinceId,
             CustomerSpouseZipCode,
             CustomerSpouseMobile,
             CustomerSpouseTelephone,
            CustomerEmergencyTitle,
            CustomerEmergencyFirstName,
            CustomerEmergencyLastName,
            CustomerEmergencyRelation,
            CustomerEmergencyMobile,
            CustomerEmergencyTelephone,
             SaleID,
            CustomerPartner,
             Activated,
             Deleted)values("
             + item.CustomerID+ ","
             + Utility.ReplaceString(item.CustomerCode)+ ","
             + Utility.ReplaceString(item.CustomerTitleName)+ ","
             +Utility.ReplaceString(item.CustomerFirstName)+ ","
             +Utility.ReplaceString(item.CustomerLastName)+ ","
             +Utility.ReplaceString(item.CustomerNickName)+ ","
             +Utility.ReplaceString(item.CustomerIdCard)+ ","
             +Utility.ReplaceString(item.CustomerAddress1)+ ","
             + item.CustomerSubDistrictId+ ","
             +  item.CustomerDistrictId+ ","
             +  item.CustomerProvinceId+ ","
             + Utility.ReplaceString(item.CustomerZipCode) + ","
             +Utility.ReplaceString(item.CustomerMobile)+ ","
              +Utility.ReplaceString(item.CustomerTelephone)+ ","
              +Utility.ReplaceString(item.CustomerStatus)+ ","
           + Utility.ReplaceString(item.CustomerEmail) + ","
           + Utility.ReplaceString(item.CustomerCareer) + ","
           + Utility.ReplaceString(item.CustomerJob) + ","
            + Utility.ReplaceString(item.CustomerJobYear) + ","
           + Utility.ReplaceString(item.CustomerSalary) + ","
           + Utility.ReplaceString(item.CustomerJobAddress) + ","
           + item.CustomerJobSubDistrictId + ","
           + item.CustomerJobDistrictId + ","
           + item.CustomerJobProvinceId + ","
           + Utility.ReplaceString(item.CustomerJobZipCode) + ","
            + Utility.ReplaceString(item.CustomerSpouseTitle) + ","
           + Utility.ReplaceString(item.CustomerSpouseFirstName) + ","
           + Utility.ReplaceString(item.CustomerSpouseLastName) + ","
           + Utility.ReplaceString(item.CustomerSpouseNickName) + ","
           + Utility.ReplaceString(item.CustomerSpouseAddress) + ","
           + item.CustomerSpouseSubDistrictId + ","
           + item.CustomerSpouseDistrictId + ","
           + item.CustomerSpouseProvinceId + ","
           + Utility.ReplaceString(item.CustomerSpouseZipCode) + ","
           + Utility.ReplaceString(item.CustomerSpouseMobile) + ","
           + Utility.ReplaceString(item.CustomerSpouseTelephone) + ","
           + Utility.ReplaceString(item.CustomerEmergencyTitle) + ","
           + Utility.ReplaceString(item.CustomerEmergencyFirstName) + ","
           + Utility.ReplaceString(item.CustomerEmergencyLastName) + ","
           + Utility.ReplaceString(item.CustomerEmergencyRelation) + ","
           + Utility.ReplaceString(item.CustomerEmergencyMobile) + ","
           + Utility.ReplaceString(item.CustomerEmergencyTelephone) + ","
           + item.SaleID + ","
           + item.CustomerPartner + ","
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

        public void EditCustomer(ref Customers item)
        {
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {
               
                string StrSql = @" Update customer Set
             CustomerTitleName={1},
             CustomerFirstName={2},
             CustomerLastName={3},
             CustomerNickName={4},
             CustomerIdCard={5},
             CustomerAddress1={6},
             CustomerSubDistrictId={7},
             CustomerDistrictId={8},
             CustomerProvinceId={9},
             CustomerZipCode={10},
             CustomerMobile={11}, 
             CustomerTelephone={12},
             CustomerStatus={13},
             CustomerEmail={14},
             CustomerCareer={15},
             CustomerJob={16},
             CustomerJobYear={17},
             CustomerSalary={18},
             CustomerJobAddress={19},
             CustomerJobSubDistrictId={20},
             CustomerJobDistrictId={21},
             CustomerJobProvinceId={22},
             CustomerJobZipCode={23},
             CustomerSpouseTitle={24},
             CustomerSpouseFirstName={25},
             CustomerSpouseLastName={26},
             CustomerSpouseNickName={27},
             CustomerSpouseAddress={28},
             CustomerSpouseSubDistrictId={29},
             CustomerSpouseDistrictId={30},
             CustomerSpouseProvinceId={31},
             CustomerSpouseZipCode={32},
             CustomerSpouseMobile={33},
             CustomerSpouseTelephone={34},
            CustomerEmergencyTitle={35},
            CustomerEmergencyFirstName={36},
            CustomerEmergencyLastName={37},
            CustomerEmergencyRelation={38},
            CustomerEmergencyMobile={39},
            CustomerEmergencyTelephone={40},
            SaleID={41}
            where  CustomerId={0} ";
              StrSql= String.Format(StrSql,
              item.CustomerID  ,
              Utility.ReplaceString(item.CustomerTitleName)  ,
              Utility.ReplaceString(item.CustomerFirstName)  ,
              Utility.ReplaceString(item.CustomerLastName)  ,
              Utility.ReplaceString(item.CustomerNickName)  ,
              Utility.ReplaceString(item.CustomerIdCard)  ,
              Utility.ReplaceString(item.CustomerAddress1)  ,
              item.CustomerSubDistrictId  ,
              item.CustomerDistrictId  ,
              item.CustomerProvinceId  ,//10
              Utility.ReplaceString(item.CustomerZipCode)  ,
              Utility.ReplaceString(item.CustomerMobile)  ,
               Utility.ReplaceString(item.CustomerTelephone)  ,
               Utility.ReplaceString(item.CustomerStatus)  ,
            Utility.ReplaceString(item.CustomerEmail),
            Utility.ReplaceString(item.CustomerCareer),
            Utility.ReplaceString(item.CustomerJob),
             Utility.ReplaceString(item.CustomerJobYear),
            Utility.ReplaceString(item.CustomerSalary),
            Utility.ReplaceString(item.CustomerJobAddress),//20
            item.CustomerJobSubDistrictId,
            item.CustomerJobDistrictId,
            item.CustomerJobProvinceId,
            Utility.ReplaceString(item.CustomerJobZipCode),
             Utility.ReplaceString(item.CustomerSpouseTitle),
            Utility.ReplaceString(item.CustomerSpouseFirstName),
            Utility.ReplaceString(item.CustomerSpouseLastName),
            Utility.ReplaceString(item.CustomerSpouseNickName),
            Utility.ReplaceString(item.CustomerSpouseAddress),
            item.CustomerSpouseSubDistrictId,//30
            item.CustomerSpouseDistrictId,
            item.CustomerSpouseProvinceId,
            Utility.ReplaceString(item.CustomerSpouseZipCode),
            Utility.ReplaceString(item.CustomerSpouseMobile),
            Utility.ReplaceString(item.CustomerSpouseTelephone),
            Utility.ReplaceString(item.CustomerEmergencyTitle),
            Utility.ReplaceString(item.CustomerEmergencyFirstName),
            Utility.ReplaceString(item.CustomerEmergencyLastName),
            Utility.ReplaceString(item.CustomerEmergencyRelation),
            Utility.ReplaceString(item.CustomerEmergencyMobile),//40
            Utility.ReplaceString(item.CustomerEmergencyTelephone),
            item.SaleID
         );

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
        public void PaymentDailyReceipts(DailyReceiptsReport item)
        {
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            if (item.PriceReceipts<=0) {

                throw new Exception("จำนวนเงินต้องมากว่า 0 บาท");
            }
            try
            {
             
                decimal totalsales = 0; // ยอดเงินทั้งหมด
                decimal rate = 0; //ดอกเบี้ย
           
                // get เงนิ
                GetContractID(
                   out   totalsales, // ยอดเงินทั้งหมด
                   out rate, //ดอกเบี้ย
                   item.CustomerID, item.ContractID
                  );

                decimal interest = item.PriceReceipts * (rate / 100); //ดอกเบี้ย
                decimal Priciple = item.PriceReceipts - interest; // เงินต้น



                // StaffID ,CustomerID ,ContractID,PriceReceipts
              


                item.ID = Utility.GetMaxID("daily_receipts", "ID");
                string StrSql = @" INSERT INTO daily_receipts(ID,CustomerID,ContractID,DateAsOf,
            TotalSales,PriceReceipts,Principle,Interest,StaffID,Activated,Deleted)
            VALUES("

             + item.ID + ","
             + item.CustomerID + ","
             + item.ContractID+ ","
             + Utility.FormateDateTime(DateTime.Now)+ ","
             + totalsales + ","
             + item.PriceReceipts + ","
             + Priciple + ","
             + interest + ","
             + item.StaffID+ ","
             + 0 + ","
             + 0 + ")";

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

        public void GetContractID(  
              out  decimal totalsales, // ยอดเงินทั้งหมด
              out  decimal rate , //ดอกเบี้ย
               int CustomerID ,
               int ContractID
            ) {

                     totalsales = 0;
                    rate = 0;
                MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
                string StrSql = @" Select * From contract where Deleted=0 and Activated=1 ";

                if (CustomerID > 0)
                {

                    StrSql += @" and ContractCustomerID=" + CustomerID;
                }

                if (ContractID > 0)
                {

                    StrSql += @" and ContractID=" + ContractID;
                }
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                if (dt.Rows.Count>0) {

                    totalsales = (decimal)dt.Rows[0]["ContractPayment"];
                    rate = (decimal)dt.Rows[0]["ContractInterest"];
                }

        }


        public Customers GetCustomerInfo_ByID(int id)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  SELECT c.* ,
                                   s.SubDistrictName as CustomerSubDistrict,
                                   d.DistrictName as CustomerDistrict,
                                   p.ProvinceName as CustomerProvince
                                   FROM customer c 
                                  LEFT OUTER JOIN province p ON c.CustomerProvinceId = p.ProvinceId
                                  LEFT OUTER JOIN district d ON c.CustomerDistrictId = d.DistrictId
                                  LEFT OUTER JOIN subDistrict s ON c.CustomerSubDistrictId = s.SubDistrictId
                                where Deleted=0 ";

                if (id >= 0)
                {

                    StrSql += @" and c.CustomerID=" + id;
                }
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                Customers cust=new Customers();
                IList<Customers> listData = new List<Customers>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = Customers.ToObjectList(dt);
                    cust=listData[0];
                   
                }


                return cust;
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

        public void GetChangeMobilePhone(Customers item) {
               MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
               try
               {
                   string sqlStr = @"update customer set 
                    CustomerMobile={0},
                    CustomerTelephone={1}
                    where CustomerID={2}";

                   sqlStr = String.Format(sqlStr,
                       Utility.ReplaceString(item.CustomerMobile),
                       Utility.ReplaceString(item.CustomerTelephone),
                      item.CustomerID);


                   DBHelper.Execute(sqlStr, ObjConn);

               }
               catch (Exception ex )
               {

                   throw new Exception(ex.Message);
               }
               finally {
                   ObjConn.Close();
               }
        }

    }
}