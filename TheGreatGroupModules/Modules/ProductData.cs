using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheGreatGroupModules.Models;

namespace TheGreatGroupModules.Modules
{
    public class ProductData
    {

        private string errMsg = "";
        public IList<Products> GetListProduct()
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" SELECT pg.ProductGroupName,p.* FROM products p 
                                LEFT JOIN product_group pg ON p.ProductGroupID=pg.ProductGroupID
                                WHERE p.Activated=1 AND p.Deleted=0
                                ";
                StrSql += @" Order by pg.ProductGroupName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                IList<Products> listData = new List<Products>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listData = Products.ToObjectList(dt);
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

        public void  GetProductPrice(ref ProductSelect item)
        {

            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);

            try
            {

                string StrSql = @" SELECT pg.ProductGroupName,p.* FROM products p 
                                LEFT JOIN product_group pg ON p.ProductGroupID=pg.ProductGroupID
                                WHERE p.Activated=1 AND p.Deleted=0
                                ";
                

                if(item.ProductID>0){
                
                  StrSql += @" and p.ProductID="+item.ProductID;
                }

                StrSql += @" Order by pg.ProductGroupName ASC";
                DataTable dt = DBHelper.List(StrSql, ObjConn);
                int productgroupId=0;
                SettingData data = new SettingData();
                List<GoldData> datanew = new List<GoldData>();
                datanew = data.GetPriceGold();
                if (dt.Rows.Count > 0) {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        productgroupId=Convert.ToInt32(dt.Rows[i]["ProductGroupID"].ToString());
                        item.ProductGroupID = productgroupId;
                        if(productgroupId==1){
                            item.ProductPrice = (Convert.ToDouble(datanew[0].ask) / 15.16) * Convert.ToDouble(dt.Rows[i]["UnitAmount"].ToString());
                            item.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " " + dt.Rows[i]["ProductName"].ToString()+
                                 " (" +  dt.Rows[i]["UnitAmount"].ToString() + " " +  dt.Rows[i]["UnitName"].ToString() + ")";
                            item.PriceGold=Convert.ToDecimal(datanew[0].ask);
                        }
                        else if (productgroupId == 2)
                        {
                            item.ProductPrice = (Convert.ToDouble(datanew[0].ask) / 15.16) * Convert.ToDouble(dt.Rows[i]["UnitAmount"].ToString());
                            item.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " " + dt.Rows[i]["ProductName"].ToString() +
                                                           " (" + dt.Rows[i]["UnitAmount"].ToString() + " " + dt.Rows[i]["UnitName"].ToString() + ")";
                            item.PriceGold = Convert.ToDecimal(datanew[1].ask);
                        }
                        else {
                            item.ProductName = dt.Rows[i]["ProductName"].ToString();
                            item.ProductPrice = Convert.ToDouble(dt.Rows[i]["ProductPrice"].ToString());
                            item.PriceGold = 0;
                        }
                        
                       
                        
                    }
                
                }

                
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




        public void AddProductSelect(List<ProductSelect> products,     
            int CustomerID,int ContractID) {
            ProductSelect product = new ProductSelect();
            if (products.Count > 0)
            {

                for (int i = 0; i < products.Count ; i++)
                {
                    product = new ProductSelect();
                    product = products[i];
                    GetProductPrice(ref product);

                    // AddData
                    AddCustomerProduct(product, CustomerID, ContractID);
                }
                
            }


       
        }



        public void AddCustomerProduct(
            ProductSelect products,
            int CustomerID,int ContractID)
        {
           
            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string StrSql = @"INSERT INTO product_customer
            (CustomoerID,
             ContractID,
             ProductID,
             ProductGroupID,
             PriceGold,
             Unit,
             ProductPrice,
             deleted,
             IsCal)
VALUES ({0}, {1}, {2}, {3}, {4},{5},{6}, {7}, {8});";

                String.Format(StrSql
                    ,CustomerID
                    ,ContractID
                     , products.ProductID
                      , products.ProductGroupID
                       , products.PriceGold
                    , products.Unit
                    , products.ProductPrice
                    ,0
                    ,0
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

    }
}
