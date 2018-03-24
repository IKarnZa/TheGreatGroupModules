﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class ContractData
    {
        private string errMsg = "";


        public IList<Contract> GetContract(int CustomerID, int ContractID)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" select * FROM Contract 
                                    where 0=0
                                    and Activated=1
                                   
                                ";
                if (CustomerID > 0) {

                    StrSql += "  and ContractCustomerID=" + CustomerID;
                }
                if (ContractID > 0)
                {

                    StrSql += "  and ContractID=" + ContractID;
                }

                StrSql += @" Order by ContractCreateDate ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<Contract> listData = new List<Contract>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    Contract con=new Contract();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        con=new Contract();

                        if (dt.Rows[i]["ContractID"]!=DBNull.Value)
                        con.ContractID =  Convert.ToInt32(dt.Rows[i]["ContractID"].ToString());
                        if (dt.Rows[i]["ContractCustomerID"] != DBNull.Value)
                        con.ContractCustomerID =  Convert.ToInt32(dt.Rows[i]["ContractCustomerID"].ToString());
                        if (dt.Rows[i]["ContractCreateDate"] != DBNull.Value)
                          con.ContractCreateDate = Convert.ToDateTime(dt.Rows[i]["ContractCreateDate"].ToString());
                        if (dt.Rows[i]["ContractStartDate"] != DBNull.Value)
                          con.ContractStartDate = Convert.ToDateTime(dt.Rows[i]["ContractStartDate"].ToString());
                        if (dt.Rows[i]["ContractExpDate"] != DBNull.Value)
                          con.ContractExpDate = Convert.ToDateTime(dt.Rows[i]["ContractExpDate"].ToString());
                        if (dt.Rows[i]["ContractNumber"] != DBNull.Value)
                            con.ContractNumber = dt.Rows[i]["ContractNumber"].ToString();
                        if (dt.Rows[i]["ContractReward"] != DBNull.Value)
                            con.ContractReward = Convert.ToDecimal(dt.Rows[i]["ContractReward"].ToString());
                        if (dt.Rows[i]["ContractInterest"] != DBNull.Value)
                          con.ContractInterest =Convert.ToDecimal(dt.Rows[i]["ContractInterest"].ToString());
                        if (dt.Rows[i]["ContractAmount"] != DBNull.Value)
                          con.ContractAmount =Convert.ToDecimal(dt.Rows[i]["ContractAmount"].ToString());
                        if (dt.Rows[i]["ContractAmountLast"] != DBNull.Value)
                          con.ContractAmountLast =Convert.ToDecimal(dt.Rows[i]["ContractAmountLast"].ToString());
                        if (dt.Rows[i]["ContractPeriod"] != DBNull.Value)
                            con.ContractPeriod = Convert.ToInt32(dt.Rows[i]["ContractPeriod"].ToString());
                        if (dt.Rows[i]["ContractPayment"] != DBNull.Value)
                          con.ContractPayment =Convert.ToDecimal(dt.Rows[i]["ContractPayment"].ToString());
                        if (dt.Rows[i]["ContractRefNumber"] != DBNull.Value)
                          con.ContractRefNumber=  dt.Rows[i]["ContractRefNumber"].ToString();
                        if (dt.Rows[i]["ContractInsertBy"] != DBNull.Value)
                        con.ContractInsertBy=  Convert.ToInt32(dt.Rows[i]["ContractInsertBy"].ToString());
                        if (dt.Rows[i]["ContractInsertDate"] != DBNull.Value)
                        con.ContractInsertDate=  Convert.ToDateTime(dt.Rows[i]["ContractInsertDate"].ToString());

                        if (dt.Rows[i]["ContractType"] != DBNull.Value) 
                            con.ContractType = dt.Rows[i]["ContractType"].ToString();
                        if (dt.Rows[i]["ContractStatus"] != DBNull.Value) 
                            con.ContractStatus = Convert.ToInt32(dt.Rows[i]["ContractStatus"].ToString());
                        if (dt.Rows[i]["ContractRemark"] != DBNull.Value) 
                            con.ContractRemark = dt.Rows[i]["ContractRemark"].ToString();
                        if (dt.Rows[i]["Activated"] != DBNull.Value)
                          con.Activated=  Convert.ToInt32(dt.Rows[i]["Activated"].ToString());
                        if (dt.Rows[i]["Deleted"] != DBNull.Value)
                          con.Deleted=  Convert.ToInt32(dt.Rows[i]["Deleted"].ToString());

                           if (dt.Rows[i]["ContractPayEveryDay"] != DBNull.Value)
                               con.ContractPayEveryDay = Convert.ToInt32(dt.Rows[i]["ContractPayEveryDay"].ToString());
                           if (dt.Rows[i]["CustomerSpecialholiday"] != DBNull.Value)
                               con.CustomerSpecialholiday = Convert.ToBoolean(dt.Rows[i]["CustomerSpecialholiday"].ToString());
                           if (dt.Rows[i]["ContractSuretyID1"] != DBNull.Value)
                               con.CustomerSurety1 = Convert.ToInt32(dt.Rows[i]["ContractSuretyID1"].ToString());
                           if (dt.Rows[i]["ContractSuretyID2"] != DBNull.Value)
                               con.CustomerSurety2 = Convert.ToInt32(dt.Rows[i]["ContractSuretyID2"].ToString());
                        
                        listData.Add(con);
                    }



                  
                  
                }

                //หาส่วนของผู้ค้ำประกัน
                if (listData.Count > 0)
                {
                    CustomerSurety Surety = new CustomerSurety();
                    for (int i = 0; i < listData.Count; i++)
                    {

                        #region ::  ผู้ค้ำคนที่ 1  ::
                        if (listData[i].CustomerSurety1 > 0)
                        {

                            StrSql = @"select * FROM customer_surety where CustomerSuretyID=" + listData[i].CustomerSurety1;
                            DataTable dt1 = DBHelper.List(StrSql, ObjConn);
                            var dataSurety = new CustomerSurety();

                            if (dt1.Rows.Count > 0)
                            {

                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    dataSurety = new CustomerSurety();
                                    dataSurety.CustomerSuretyID = Convert.ToInt32(dt1.Rows[j]["CustomerSuretyID"].ToString());
                                    dataSurety.CustomerSuretyTitle = dt1.Rows[j]["CustomerSuretyTitle"].ToString();
                                    dataSurety.CustomerSuretyFirstName = dt1.Rows[j]["CustomerSuretyFirstName"].ToString();
                                    dataSurety.CustomerSuretyLastName = dt1.Rows[j]["CustomerSuretyLastName"].ToString();
                                    dataSurety.CustomerSuretyAddress = dt1.Rows[j]["CustomerSuretyAddress"].ToString();
                                    dataSurety.CustomerSuretySubDistrictId = Convert.ToInt32(dt1.Rows[j]["CustomerSuretySubDistrict"].ToString());
                                    dataSurety.CustomerSuretyDistrictId = Convert.ToInt32(dt1.Rows[j]["CustomerSuretyDistrict"].ToString());
                                    dataSurety.CustomerSuretyProvinceId = Convert.ToInt32(dt1.Rows[j]["CustomerSuretyProvince"].ToString());
                                    dataSurety.CustomerSuretyZipCode = dt1.Rows[j]["CustomerSuretyZipCode"].ToString();
                                    dataSurety.CustomerSuretyIdCard = dt1.Rows[j]["CustomerSuretyIdCard"].ToString();
                                    dataSurety.CustomerSuretyMobile = dt1.Rows[j]["CustomerSuretyMobile"].ToString();
                                    dataSurety.CustomerSuretyTelephone = dt1.Rows[j]["CustomerSuretyTelephone"].ToString();
                                    listData[i].CustomerSuretyData1 = dataSurety;
                                }
                            }
                        }

                        #endregion ::  ผู้ค้ำคนที่ 1  ::

                        #region ::  ผู้ค้ำคนที่ 2  ::
                        if (listData[i].CustomerSurety2 > 0)
                        {

                            StrSql = @"select * FROM customer_surety where CustomerSuretyID=" + listData[i].CustomerSurety2;
                            DataTable dt2 = DBHelper.List(StrSql, ObjConn);
                            var dataSurety2 = new CustomerSurety();

                            if (dt2.Rows.Count > 0)
                            {

                                for (int k = 0; k < dt2.Rows.Count; k++)
                                {
                                    dataSurety2 = new CustomerSurety();
                                    dataSurety2.CustomerSuretyID = Convert.ToInt32(dt2.Rows[k]["CustomerSuretyID"].ToString());
                                    dataSurety2.CustomerSuretyTitle = dt2.Rows[k]["CustomerSuretyTitle"].ToString();
                                    dataSurety2.CustomerSuretyFirstName = dt2.Rows[k]["CustomerSuretyFirstName"].ToString();
                                    dataSurety2.CustomerSuretyLastName = dt2.Rows[k]["CustomerSuretyLastName"].ToString();
                                    dataSurety2.CustomerSuretyAddress = dt2.Rows[k]["CustomerSuretyAddress"].ToString();
                                    dataSurety2.CustomerSuretySubDistrictId = Convert.ToInt32(dt2.Rows[k]["CustomerSuretySubDistrict"].ToString());
                                    dataSurety2.CustomerSuretyDistrictId = Convert.ToInt32(dt2.Rows[k]["CustomerSuretyDistrict"].ToString());
                                    dataSurety2.CustomerSuretyProvinceId = Convert.ToInt32(dt2.Rows[k]["CustomerSuretyProvince"].ToString());
                                    dataSurety2.CustomerSuretyZipCode = dt2.Rows[k]["CustomerSuretyZipCode"].ToString();
                                    dataSurety2.CustomerSuretyIdCard = dt2.Rows[k]["CustomerSuretyIdCard"].ToString();
                                    dataSurety2.CustomerSuretyMobile = dt2.Rows[k]["CustomerSuretyMobile"].ToString();
                                    dataSurety2.CustomerSuretyTelephone = dt2.Rows[k]["CustomerSuretyTelephone"].ToString();
                                    listData[i].CustomerSuretyData1 = dataSurety2;
                                }
                            }
                        }
                        #endregion ::  ผู้ค้ำคนที่ 2  ::


                        #region ::  ข้อมูลผู้ซื้อร่วม   ::
                        if (listData[i].CustomerPartner> 0)
                        {

                            StrSql = @"select * FROM customer_partner where CustomerPartnerID=" + listData[i].CustomerPartner;
                            DataTable dt3 = DBHelper.List(StrSql, ObjConn);
                            var dataPartner = new CustomerPartner();

                            if (dt3.Rows.Count > 0)
                            {

                                for (int k = 0; k < dt3.Rows.Count; k++)
                                {
                                    dataPartner = new CustomerPartner();
                                    dataPartner.CustomerPartnerID = Convert.ToInt32(dt3.Rows[k]["CustomerPartnerID"].ToString());
                                    dataPartner.CustomerPartnerTitle = dt3.Rows[k]["CustomerPartnerTitle"].ToString();
                                    dataPartner.CustomerPartnerFirstName = dt3.Rows[k]["CustomerPartnerFirstName"].ToString();
                                    dataPartner.CustomerPartnerLastName = dt3.Rows[k]["CustomerPartnerLastName"].ToString();
                                    dataPartner.CustomerPartnerAddress = dt3.Rows[k]["CustomerPartnerAddress"].ToString();
                                    dataPartner.CustomerPartnerSubDistrictId = Convert.ToInt32(dt3.Rows[k]["CustomerPartnerSubDistrict"].ToString());
                                    dataPartner.CustomerPartnerDistrictId = Convert.ToInt32(dt3.Rows[k]["CustomerPartnerDistrict"].ToString());
                                    dataPartner.CustomerPartnerProvinceId = Convert.ToInt32(dt3.Rows[k]["CustomerPartnerProvince"].ToString());
                                    dataPartner.CustomerPartnerZipCode = dt3.Rows[k]["CustomerPartnerZipCode"].ToString();
                                    dataPartner.CustomerPartnerIdCard = dt3.Rows[k]["CustomerPartnerIdCard"].ToString();
                                    dataPartner.CustomerPartnerMobile = dt3.Rows[k]["CustomerPartnerMobile"].ToString();
                                    dataPartner.CustomerPartnerTelephone = dt3.Rows[k]["CustomerPartnerTelephone"].ToString();
                                    listData[i].CustomerPartnerData = dataPartner;
                                }
                            }
                        }
                        #endregion ::   ข้อมูลผู้ซื้อร่วม   ::
                    }

                }
                else { 
                   Contract con=new Contract();
                   var dataSurety = new CustomerSurety();
                   dataSurety.CustomerSuretyID = con.CustomerSurety1 ;
                   con.CustomerSuretyData1 = dataSurety;
                   dataSurety = new CustomerSurety();
                   dataSurety.CustomerSuretyID = con.CustomerSurety2;
                   con.CustomerSuretyData2 = dataSurety;
                   var  dataPartner1 = new CustomerPartner();
                   con.CustomerPartner =con.CustomerPartner;
                   con.CustomerPartnerData = dataPartner1;

                
                 listData.Add(con);
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

           public IList<Contract> GetListContract(int CustomerID, int ContractID)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" select * FROM Contract 
                                    where 0=0
                                    and Activated=1
                                   
                                ";
                if (CustomerID > 0) {

                    StrSql += "  and ContractCustomerID=" + CustomerID;
                }
                if (ContractID > 0)
                {

                    StrSql += "  and ContractID=" + ContractID;
                }

                StrSql += @" Order by ContractCreateDate ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<Contract> listData = new List<Contract>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    Contract con=new Contract();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        con=new Contract();

                        if (dt.Rows[i]["ContractID"]!=DBNull.Value)
                        con.ContractID =  Convert.ToInt32(dt.Rows[i]["ContractID"].ToString());
                        if (dt.Rows[i]["ContractCustomerID"] != DBNull.Value)
                        con.ContractCustomerID =  Convert.ToInt32(dt.Rows[i]["ContractCustomerID"].ToString());
                        if (dt.Rows[i]["ContractCreateDate"] != DBNull.Value)
                          con.ContractCreateDate = Convert.ToDateTime(dt.Rows[i]["ContractCreateDate"].ToString());
                        if (dt.Rows[i]["ContractStartDate"] != DBNull.Value)
                          con.ContractStartDate = Convert.ToDateTime(dt.Rows[i]["ContractStartDate"].ToString());
                        if (dt.Rows[i]["ContractExpDate"] != DBNull.Value)
                          con.ContractExpDate = Convert.ToDateTime(dt.Rows[i]["ContractExpDate"].ToString());
                        if (dt.Rows[i]["ContractNumber"] != DBNull.Value)
                            con.ContractNumber = dt.Rows[i]["ContractNumber"].ToString();
                        if (dt.Rows[i]["ContractReward"] != DBNull.Value)
                            con.ContractReward = Convert.ToDecimal(dt.Rows[i]["ContractReward"].ToString());
                        if (dt.Rows[i]["ContractInterest"] != DBNull.Value)
                          con.ContractInterest =Convert.ToDecimal(dt.Rows[i]["ContractInterest"].ToString());
                        if (dt.Rows[i]["ContractAmount"] != DBNull.Value)
                          con.ContractAmount =Convert.ToDecimal(dt.Rows[i]["ContractAmount"].ToString());
                        if (dt.Rows[i]["ContractAmountLast"] != DBNull.Value)
                          con.ContractAmountLast =Convert.ToDecimal(dt.Rows[i]["ContractAmountLast"].ToString());
                        if (dt.Rows[i]["ContractPeriod"] != DBNull.Value)
                            con.ContractPeriod = Convert.ToInt32(dt.Rows[i]["ContractPeriod"].ToString());
                        if (dt.Rows[i]["ContractPayment"] != DBNull.Value)
                          con.ContractPayment =Convert.ToDecimal(dt.Rows[i]["ContractPayment"].ToString());
                        if (dt.Rows[i]["ContractRefNumber"] != DBNull.Value)
                          con.ContractRefNumber=  dt.Rows[i]["ContractRefNumber"].ToString();
                        if (dt.Rows[i]["ContractInsertBy"] != DBNull.Value)
                        con.ContractInsertBy=  Convert.ToInt32(dt.Rows[i]["ContractInsertBy"].ToString());
                        if (dt.Rows[i]["ContractInsertDate"] != DBNull.Value)
                        con.ContractInsertDate=  Convert.ToDateTime(dt.Rows[i]["ContractInsertDate"].ToString());

                        if (dt.Rows[i]["ContractType"] != DBNull.Value) 
                            con.ContractType = dt.Rows[i]["ContractType"].ToString();
                        if (dt.Rows[i]["ContractStatus"] != DBNull.Value) 
                            con.ContractStatus = Convert.ToInt32(dt.Rows[i]["ContractStatus"].ToString());
                        if (dt.Rows[i]["ContractRemark"] != DBNull.Value) 
                            con.ContractRemark = dt.Rows[i]["ContractRemark"].ToString();
                        if (dt.Rows[i]["Activated"] != DBNull.Value)
                          con.Activated=  Convert.ToInt32(dt.Rows[i]["Activated"].ToString());
                        if (dt.Rows[i]["Deleted"] != DBNull.Value)
                          con.Deleted=  Convert.ToInt32(dt.Rows[i]["Deleted"].ToString());

                           if (dt.Rows[i]["ContractPayEveryDay"] != DBNull.Value)
                               con.ContractPayEveryDay = Convert.ToInt32(dt.Rows[i]["ContractPayEveryDay"].ToString());
                           if (dt.Rows[i]["CustomerSpecialholiday"] != DBNull.Value)
                               con.CustomerSpecialholiday = Convert.ToBoolean(dt.Rows[i]["CustomerSpecialholiday"].ToString());
                           if (dt.Rows[i]["ContractSuretyID1"] != DBNull.Value)
                               con.CustomerSurety1 = Convert.ToInt32(dt.Rows[i]["ContractSuretyID1"].ToString());
                           if (dt.Rows[i]["ContractSuretyID2"] != DBNull.Value)
                               con.CustomerSurety2 = Convert.ToInt32(dt.Rows[i]["ContractSuretyID2"].ToString());
                        
                        listData.Add(con);
                    }

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

                  
                  
          

        public int Add_NewContract(Contract item) {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            int ConT_ID = 0;
            try
            {
                item.ContractID = Utility.GetMaxID("contract", "ContractID");
                string StrSql = @" INSERT INTO  contract
            (
             ContractID,
             ContractCustomerID,
             ContractCreateDate,
             ContractStartDate,
             ContractExpDate,
             ContractNumber,
             ContractReward,
             ContractInterest,
             ContractAmount,
             ContractAmountLast,
             ContractPeriod,
             ContractPayment,
             ContractRefNumber,
             ContractInsertBy,
             ContractInsertDate,
             ContractType,
             ContractStatus,
             ContractPayEveryDay,
             CustomerSpecialholiday,
             ContractRemark,
             Activated,
             Deleted)
VALUES ({0},{1},{2}, {3}, {4},{5}, {6},{7}, {8}, {9},{10},{11},{12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21});";


                StrSql = String.Format(StrSql,
                    item.ContractID,
                     item.ContractCustomerID,
                   Utility.FormateDate(item.ContractCreateDate),
                       Utility.FormateDate(item.ContractStartDate),
                       Utility.FormateDate(item.ContractExpDate),
                      Utility.ReplaceString(item.ContractNumber),
                      item.ContractReward,
                     item.ContractInterest,
                      item.ContractAmount,
                      item.ContractAmountLast,
                      item.ContractPeriod,
                      item.ContractPayment,
                      Utility.ReplaceString(item.ContractRefNumber),
                      item.ContractInsertBy,
                       Utility.FormateDate(DateTime.Now),
                       Utility.ReplaceString( item.ContractType),
                      1,
                      item.ContractPayEveryDay,
                      item.CustomerSpecialholiday,
                      Utility.ReplaceString(item.ContractRemark),
                      1,
                      0
                         );
                
                DBHelper.Execute(StrSql,ObjConn);
                ConT_ID = item.ContractID;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally {


                ObjConn.Close();
             
            }
            return ConT_ID;
          
        }

        public void Edit_NewContract(Contract item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
           
            try
            {
               
                string StrSql = @" Update contract Set
             ContractCustomerID={1},
             ContractCreateDate={2},
             ContractStartDate={3},
             ContractExpDate={4},
             ContractNumber={5},
             ContractReward={6},
             ContractInterest={7},
             ContractAmount={8},
             ContractAmountLast={9},
             ContractPeriod={10},
             ContractPayment={11},
             ContractRefNumber={12},
             ContractInsertBy={13},
             ContractInsertDate={14},
             ContractType={15},
             ContractStatus={16},
             ContractPayEveryDay={17},
             CustomerSpecialholiday={18},
             ContractRemark={19},
             Activated={20},
             Deleted={21}
            Where ContractID={0}";


                StrSql = String.Format(StrSql,
                      item.ContractID,
                     item.ContractCustomerID,
                   Utility.FormateDate(item.ContractCreateDate),
                       Utility.FormateDate(item.ContractStartDate),
                       Utility.FormateDate(item.ContractExpDate),
                      Utility.ReplaceString(item.ContractNumber),
                      item.ContractReward,
                     item.ContractInterest,
                      item.ContractAmount,
                      item.ContractAmountLast,
                      item.ContractPeriod,
                      item.ContractPayment,
                      Utility.ReplaceString(item.ContractRefNumber),
                      item.ContractUpdateBy,
                        Utility.FormateDate(DateTime.Now),
                       Utility.ReplaceString(item.ContractType),
                     item.ContractStatus,
                      item.ContractPayEveryDay,
                      item.CustomerSpecialholiday,
                      Utility.ReplaceString(item.ContractRemark),
                      1,
                      0
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


        public int Add_Surety(CustomerSurety item)
        {
            
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            item.CustomerSuretyID = Utility.GetMaxID("customer_surety", "CustomerSuretyID");
            try
            {
              
                string StrSql = @" INSERT INTO  customer_surety
            ( CustomerSuretyID,
             CustomerSuretyTitle,
             CustomerSuretyFirstName,
             CustomerSuretyLastName,
             CustomerSuretyAddress,
             CustomerSuretySubDistrict,
             CustomerSuretyDistrict,
             CustomerSuretyProvince,
             CustomerSuretyZipCode,
             CustomerSuretyIdCard,
             CustomerSuretyMobile,
             CustomerSuretyTelephone)
VALUES ({0},{1},{2}, {3}, {4},{5}, {6},{7}, {8}, {9},{10},{11});";
           

                StrSql = String.Format(StrSql,
                        item.CustomerSuretyID,
               Utility.ReplaceString(item.CustomerSuretyTitle),
               Utility.ReplaceString(item.CustomerSuretyFirstName),
               Utility.ReplaceString(item.CustomerSuretyLastName),
               Utility.ReplaceString(item.CustomerSuretyAddress),
             item.CustomerSuretySubDistrictId,
             item.CustomerSuretyDistrictId,
             item.CustomerSuretyProvinceId,
               Utility.ReplaceString(item.CustomerSuretyZipCode),
               Utility.ReplaceString(item.CustomerSuretyIdCard),
               Utility.ReplaceString(item.CustomerSuretyMobile),
               Utility.ReplaceString(item.CustomerSuretyTelephone)
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
            return item.CustomerSuretyID;

        }

        public int Update_Surety(CustomerSurety item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string StrSql = @" Update customer_surety set
             CustomerSuretyTitle,
             CustomerSuretyFirstName,
             CustomerSuretyLastName,
             CustomerSuretyAddress,
             CustomerSuretySubDistrict,
             CustomerSuretyDistrict,
             CustomerSuretyProvince,
             CustomerSuretyZipCode,
             CustomerSuretyIdCard,
             CustomerSuretyMobile,
             CustomerSuretyTelephone
where CustomerSuretyID= {0};";


                StrSql = String.Format(StrSql,
                        item.CustomerSuretyID,
               Utility.ReplaceString(item.CustomerSuretyTitle),
               Utility.ReplaceString(item.CustomerSuretyFirstName),
               Utility.ReplaceString(item.CustomerSuretyLastName),
               Utility.ReplaceString(item.CustomerSuretyAddress),
             item.CustomerSuretySubDistrictId,
             item.CustomerSuretyDistrictId,
             item.CustomerSuretyProvinceId,
               Utility.ReplaceString(item.CustomerSuretyZipCode),
               Utility.ReplaceString(item.CustomerSuretyIdCard),
               Utility.ReplaceString(item.CustomerSuretyMobile),
               Utility.ReplaceString(item.CustomerSuretyTelephone)
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
            return item.CustomerSuretyID;

        }
        public int Add_Partner(CustomerPartner item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            item.CustomerPartnerID = Utility.GetMaxID("customer_partner", "CustomerPartnerID");
            try
            {

                string StrSql = @" INSERT INTO  customer_Partner
            ( CustomerPartnerID,
             CustomerPartnerTitle,
             CustomerPartnerFirstName,
             CustomerPartnerLastName,
             CustomerPartnerAddress,
             CustomerPartnerSubDistrictId,
             CustomerPartnerDistrictId,
             CustomerPartnerProvinceId,
             CustomerPartnerZipCode,
             CustomerPartnerIdCard,
             CustomerPartnerMobile,
             CustomerPartnerTelephone)
VALUES ({0},{1},{2}, {3}, {4},{5}, {6},{7}, {8}, {9},{10},{11});";


                StrSql = String.Format(StrSql,
                        item.CustomerPartnerID,
               Utility.ReplaceString(item.CustomerPartnerTitle),
               Utility.ReplaceString(item.CustomerPartnerFirstName),
               Utility.ReplaceString(item.CustomerPartnerLastName),
               Utility.ReplaceString(item.CustomerPartnerAddress),
             item.CustomerPartnerSubDistrictId,
             item.CustomerPartnerDistrictId,
             item.CustomerPartnerProvinceId,
               Utility.ReplaceString(item.CustomerPartnerZipCode),
               Utility.ReplaceString(item.CustomerPartnerIdCard),
               Utility.ReplaceString(item.CustomerPartnerMobile),
               Utility.ReplaceString(item.CustomerPartnerTelephone)
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
            return item.CustomerPartnerID;

        }
        public int Update_Partner(CustomerPartner item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string StrSql = @" Update Set customer_Partner
             CustomerPartnerTitle={1},
             CustomerPartnerFirstName={2},
             CustomerPartnerLastName={3},
             CustomerPartnerAddress={4},
             CustomerPartnerSubDistrictId={5},
             CustomerPartnerDistrictId={6},
             CustomerPartnerProvinceId={7},
             CustomerPartnerZipCode={8},
             CustomerPartnerIdCard={9},
             CustomerPartnerMobile={10},
             CustomerPartnerTelephone={11}
            Where   CustomerPartnerID={12}";


                StrSql = String.Format(StrSql,
                        item.CustomerPartnerID,
               Utility.ReplaceString(item.CustomerPartnerTitle),
               Utility.ReplaceString(item.CustomerPartnerFirstName),
               Utility.ReplaceString(item.CustomerPartnerLastName),
               Utility.ReplaceString(item.CustomerPartnerAddress),
             item.CustomerPartnerSubDistrictId,
             item.CustomerPartnerDistrictId,
             item.CustomerPartnerProvinceId,
               Utility.ReplaceString(item.CustomerPartnerZipCode),
               Utility.ReplaceString(item.CustomerPartnerIdCard),
               Utility.ReplaceString(item.CustomerPartnerMobile),
               Utility.ReplaceString(item.CustomerPartnerTelephone)
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
            return item.CustomerPartnerID;

        }
              public void UpdateSurety_In_Contract(Contract item)
        {
            
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string StrSql = "";
            try
            {
                StrSql = @" Update contract set 
                ContractSuretyID1={0}
                ,ContractSuretyID2={1}
                ,CustomerPartner={2}
                ,ContractPayment={3}
                Where ContractID={4}";

                StrSql = String.Format(StrSql, item.CustomerSurety1, item.CustomerSurety2, item.CustomerPartner, item.ContractPayment, item.ContractID);

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


              public void Update_Product_customer(Contract item)
              {

                  MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
                  string StrSql = "";
                  try
                  {
                      StrSql = @" Update product_customer set 
                        ContractID={0}
                        Where CustomerID={1} And  ContractID=0";

                      StrSql = String.Format(StrSql, item.ContractID, item.ContractCustomerID);

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

        

    }
}