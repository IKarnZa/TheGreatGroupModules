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
                StrSql += @" order by  p.ProductGroupID Asc ,p.UnitAmount DESC";
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
                            item.ProductPrice_Text = item.ProductPrice.ToString("#,###.00");
                            item.TotalPrice_Text = item.ProductPrice.ToString("#,###.00");
                            item.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " " + dt.Rows[i]["ProductName"].ToString()+
                                 " (" +  dt.Rows[i]["UnitAmount"].ToString() + " " +  dt.Rows[i]["UnitName"].ToString() + ")";
                            item.PriceGold = Convert.ToDouble(datanew[0].ask);
                            item.PriceGoldReceipt = Convert.ToDouble(datanew[0].bid);
                        }
                        else if (productgroupId == 2)
                        {
                            item.ProductPrice = (Convert.ToDouble(datanew[0].ask) / 15.16) * Convert.ToDouble(dt.Rows[i]["UnitAmount"].ToString());
                            item.ProductPrice_Text = item.ProductPrice.ToString("#,###.00");
                            item.TotalPrice_Text = item.ProductPrice.ToString("#,###.00");
                            item.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " " + dt.Rows[i]["ProductName"].ToString() +
                                                           " (" + dt.Rows[i]["UnitAmount"].ToString() + " " + dt.Rows[i]["UnitName"].ToString() + ")";
                            item.PriceGold = Convert.ToDouble(datanew[0].ask);
                            item.PriceGoldReceipt = Convert.ToDouble(datanew[0].bid);
                        }
                        else {
                            item.ProductName = dt.Rows[i]["ProductName"].ToString();
                            item.ProductPrice = Convert.ToDouble(dt.Rows[i]["ProductPrice"].ToString());
                            item.PriceGold = 0;
                            item.PriceGoldReceipt = 0;
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
            (CustomerID,
             ContractID,
             ProductID,
             ProductGroupID,
             PriceGold,
             PriceGoldReceipt,
             Unit,
             ProductPrice,
             deleted,
             IsCal)
            VALUES ({0}, {1}, {2}, {3}, {4},{5},{6}, {7}, {8}, {9});";

           StrSql=     String.Format(StrSql
                    ,CustomerID
                    ,ContractID
                    , products.ProductID
                    , products.ProductGroupID
                    , products.PriceGold
                    , products.PriceGoldReceipt
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



        public List<ProductSelect> GetProductCustomer(int CustomerID, int ContractID)
        {


            MySqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            try
            {

                string StrSql = @"SELECT pg.ProductGroupName,p.ProductName,p.UnitAmount,p.UnitName,
            pc.*,c.ContractReward ,c.ContractInterest
                FROM product_customer pc 
                LEFT JOIN products  p  ON pc.ProductID=p.ProductID
                LEFT JOIN product_group  pg  ON p.ProductGroupID=pg.ProductGroupID
                 LEFT JOIN contract  c  ON pc.ContractID=c.ContractID
                WHERE 0=0 ";
                if (CustomerID > 0)
                    StrSql += " AND pc.CustomerID=" + CustomerID;

             //   if (ContractID > 0)
                    StrSql += " AND pc.ContractID=" + ContractID;

                DataTable dt=DBHelper.List(StrSql, ObjConn);


                List<ProductSelect> product =new List<ProductSelect>();

                ProductSelect data = new ProductSelect();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        data.ProductGroupID = Convert.ToInt32(dt.Rows[i]["ProductGroupID"]);
                        if (data.ProductGroupID == 1 || data.ProductGroupID == 2)
                        {
                            data.ContractInterest = Convert.ToDouble(dt.Rows[i]["ContractInterest"].ToString());
                            data.PriceGoldReceipt = Convert.ToDouble(dt.Rows[i]["PriceGoldReceipt"].ToString());
                            data.PriceGold = Convert.ToDouble(dt.Rows[i]["PriceGold"].ToString());
                            data.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                            data.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " "
                                + dt.Rows[i]["ProductName"].ToString() + "(" + dt.Rows[i]["UnitAmount"].ToString()
                                + dt.Rows[i]["UnitName"] + ")";
                            data.Unit = Convert.ToInt32(dt.Rows[i]["Unit"]);
                            data.ProductPrice = (Convert.ToDouble(dt.Rows[i]["PriceGold"].ToString())
                                / 15.16) * Convert.ToDouble(dt.Rows[i]["UnitAmount"].ToString());
                            data.ContractReward = Convert.ToDouble(dt.Rows[i]["ContractReward"].ToString());
                          data.UnitAmount=Convert.ToDouble(dt.Rows[i]["UnitAmount"].ToString());
                        }
                        else {

                            data.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                            data.ProductName = dt.Rows[i]["ProductGroupName"].ToString() + " "
                                + dt.Rows[i]["ProductName"].ToString() + "(" + dt.Rows[i]["UnitAmount"].ToString()
                                + dt.Rows[i]["UnitName"] + ")";

                            data.Unit = Convert.ToInt32(dt.Rows[i]["Unit"]);
                            data.ProductPrice = Convert.ToDouble(dt.Rows[i]["ProductPrice"].ToString());
                            data.ContractReward = 0;
                            data.PriceGoldReceipt = 0;
                            data.PriceGold = 0;
                            data.UnitAmount=0;
                        }
                       
                        product.Add(data);
                    }
               
                }

                return product;
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


        public List<ProductSelect> ProductContractSummary(IList<ProductSelect>  products)
        {
             List<ProductSelect>  listproduct=new List<ProductSelect>();
            try
            {
                ProductSelect product = new ProductSelect();
               //double
                int no=0;
               double ProductPrice = 0; // ราคาสินค้า
               double Vat = 7 / 100 ; // ภาษี 
               double Reward = 0; // ค่ากำเหน็จ
               double PriceGoldReceipt = 0; // ราคารับซื้อทอง
             //  double TotalPriceip = 0; // ราคารวม
               double interest = 0; // ดอกเบี้ย

                if (products.Count > 0) {

                     
                     for (int i = 0; i < products.Count; i++)
                     {
                         product = new ProductSelect();
                         product = products[i];
                         Reward=  products[i].ContractReward;
                         PriceGoldReceipt = products[i].PriceGoldReceipt;
                         interest = products[i].ContractInterest / 100;
                         no = (i + 1);

                         //ราคาทอง
                         ProductPrice += products[i].ProductPrice + Reward+ (products[i].ProductPrice * interest);

                         if ( products[i].ProductGroupID == 2)
                         {
                               //สินค้าทองรูปพรรณ
                               product = SetProductPrice.ProductNormal(no.ToString(), (ProductPrice - Reward), products[i].Unit);
                               product.ProductName = products[i].ProductName;
                               listproduct.Add(product);

                             //ค่ากำเหน็จ
                              product = new ProductSelect();
                              product = SetProductPrice.ProductNormal("", Reward, 0);
                              product.ProductName = "ค่ากำเหน็จ";
                              listproduct.Add(product);

                              // หักประกาศรับซื้อทองประจำวัน 
                              product = new ProductSelect();
                              product = SetProductPrice.Product_ReceiptGoldDay(PriceGoldReceipt, products[i].UnitAmount);
                              listproduct.Add(product);

                          // ส่วนต่างที่คิดภาษีมูลค่าเพิ่ม
                
                              product = new ProductSelect();
                              product = SetProductPrice.Product_VatDiff(ProductPrice,PriceGoldReceipt, products[i].UnitAmount);
                              listproduct.Add(product);
                     

                         }
                         else if(products[i].ProductGroupID == 1 )
                         {
                             product.No = no.ToString();
                             product = SetProductPrice.ProductNormal(no.ToString(), ProductPrice, products[i].Unit);
                             product.ProductName = products[i].ProductName;
                             listproduct.Add(product);
                        

                         }
                        
                     }
                
                }
                // รวมเงิน
                product = new ProductSelect();
                product = SetProductPrice.Product_TotalPrice(ProductPrice);
                listproduct.Add(product);


                // ภาษีมูลค่าเพิ่ม
                product = new ProductSelect();
                listproduct.Add(product);

                //// รวมเงินทั้งสิ้น
                //product = new ProductSelect();
                //product = SetProductPrice.Product_TotalSale(ProductPrice, PriceGoldReceipt, products[i].UnitAmount,Vat);
                //listproduct.Add(product);
           

                return listproduct;
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
                
            }
        
        }
    }
}
