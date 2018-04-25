
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


        public IList<Customers> GetCustomerByZone(int zoneId)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @"  	SELECT c.*  ,sz.ZoneID,
				s.SubDistrictName AS CustomerSubDistrict,
                                   d.DistrictName AS CustomerDistrict,
                                   p.ProvinceName AS CustomerProvince
                                   FROM customer c 
                                  LEFT OUTER JOIN province p ON c.CustomerProvinceId = p.ProvinceId
                                  LEFT OUTER JOIN district d ON c.CustomerDistrictId = d.DistrictId
                                  LEFT OUTER JOIN subDistrict s ON c.CustomerSubDistrictId = s.SubDistrictId
				LEFT JOIN staff_zone  sz ON  c.SaleID=sz.StaffID
				WHERE 0=0 ";

                if (zoneId > 0)
                {

                    StrSql += @" AND sz.ZoneID=" + zoneId;
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
                string StrSql = @" INSERT INTO customer
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

        public void DeleteCustomer(int CustomerID)
        {
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {
               
                string StrSql = @" Update customer Set
                Deleted=1
                where  CustomerId={0} ";

                  StrSql= String.Format(StrSql, CustomerID   );

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
           
                // get เงิน
                GetContractID(
                   out   totalsales, // ยอดเงินทั้งหมด
                   out rate, //ดอกเบี้ย
                   item.CustomerID, item.ContractID, ObjConn
                  );


                decimal interest = item.PriceReceipts * (rate / 100); //ดอกเบี้ย
                decimal Priciple = item.PriceReceipts - interest; // เงินต้น

              

                // StaffID ,CustomerID ,ContractID,PriceReceipts
              
                item.ID = Utility.GetMaxID("daily_receipts", "ID");
                string StrSql = @" INSERT INTO daily_receipts(ID,CustomerID,ContractID,DateAsOf,
            TotalSales,PriceReceipts,Principle,Interest,StaffID,Latitude,Longitude,Activated,Deleted)
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
             + item.Latitude + ","
             + item.Longitude + ","
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


        public void AddDailyReceipts(DailyReceiptsReport item) { 
        
        
        
        }
        public void GetContractID(  
              out  decimal totalsales, // ยอดเงินทั้งหมด
              out  decimal rate , //ดอกเบี้ย
               int CustomerID ,
               int ContractID,
             MySqlConnection ObjConn
            ) {

                     totalsales = 0;
                    rate = 0;
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

        public List<ListCustomerOnMobile> GetListCustomerOnMobile(int StaffID)
        {
            List<ListCustomerOnMobile> list = new List<ListCustomerOnMobile>(); 
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {
                string sqlStr = @"SELECT  c.CustomerId,CONCAT(c.CustomerTitleName,c.CustomerFirstName,' ',c.CustomerLastName) AS CustomerName
                ,ct.ContractID,ct.ContractNumber,ct.ContractExpDate,ct.ContractAmount,ct.ContractPayment , (ct.ContractPayment-d.TotalPay ) AS TotalPay,
                CASE WHEN  PriceReceipts IS NOT NULL THEN 1 ELSE 0 END AS StatusPay
                 FROM customer c 
                LEFT JOIN contract ct ON c.CustomerId=ct.contractCustomerID
                 LEFT JOIN (SELECT SUM(PriceReceipts)AS TotalPay , ContractID ,CustomerID  FROM  daily_receipts
                 Where   Deleted=0
                GROUP BY   ContractID ,CustomerID )d ON 
                d.ContractID=ct.ContractID AND d.CustomerID=ct.ContractCustomerID 
                LEFT JOIN (
                SELECT  ContractID ,CustomerID,DateAsOf,PriceReceipts  
                FROM  daily_receipts 
                WHERE DATE(DateAsOf)={0}
                GROUP BY   ContractID ,CustomerID) st ON
                st.ContractID=ct.ContractID AND st.CustomerID=ct.ContractCustomerID 
                WHERE c.SaleID={1} AND ct.ContractID IS NOT NULL
                 AND ct.contractstatus=1
                ";
                sqlStr = String.Format(sqlStr,
                       Utility.FormateDate(DateTime.Now),
                       StaffID);

              DataTable dt=   DBHelper.List(sqlStr, ObjConn);
              if (dt != null && dt.Rows.Count > 0)
              {
                  list= ListCustomerOnMobile.ToObjectList(dt);
              }

              return list;

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

        public StaffLogin GetStaffLogin(StaffLogin login)
        {
             MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string strSQL = "select * FROM staff Where Staffcode={0} and staffpassword={1} and Deleted=0 and Activated=1 ";
                strSQL = string.Format(strSQL, Utility.ReplaceString(login.StaffCode), Utility.ReplaceString(Utility.HashPassword(login.StaffPassword)));
                DataTable dt = DBHelper.List(strSQL, ObjConn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    login.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
                    login.StaffName = dt.Rows[0]["StaffFirstName"].ToString();
                    login.ImageUrl = dt.Rows[0]["StaffImagePath"].ToString();
                    login.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
                }
                else {
                    throw new Exception("รหัสพนักงาน หรือ รหัสผ่านไม่ถูกต้อง,กรุณาตรวจสอบ");
                }
                return login;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public StaffLogin GetStaffLoginOnMobile(StaffLogin login)
        {
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string strSQL = "select * FROM staff Where Staffcode={0} and staffpassword={1} and Deleted=0 and Activated=1 ";
                strSQL = string.Format(strSQL, Utility.ReplaceString(login.StaffCode), Utility.ReplaceString(Utility.HashPassword(login.StaffPassword)));
                DataTable dt = DBHelper.List(strSQL, ObjConn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    login.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
                    login.StaffName = dt.Rows[0]["StaffFirstName"].ToString();
                    login.ImageUrl = dt.Rows[0]["StaffImagePath"].ToString();
                    login.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
                }
                else
                {
                    throw new Exception("รหัสพนักงาน หรือ รหัสผ่านไม่ถูกต้อง,กรุณาตรวจสอบ");
                }
                return login;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}