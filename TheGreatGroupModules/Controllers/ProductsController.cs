
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGreatGroupModules.Models;
using TheGreatGroupModules.Modules;

namespace TheGreatGroupModules.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListProductCart(int CustomerID, int productID,int unit)
        {

            List<ProductSelect> ProductData = new List<ProductSelect>();

            try
            { 
                ProductData product = new ProductData();
                ProductSelect selectItem = new ProductSelect();
                selectItem.ProductID = productID;
                selectItem.Unit = unit;
                product.GetProductPrice(ref selectItem);
                ProductData.Add(selectItem);

                   return Json(new
                {
                    data = ProductData,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                   return Json(new
                {
                    data = ProductData,
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }



         
        }



        // GET: /Products/PostAddProduct?CustomerID=1&ContractID=1
        [HttpPost]
        public JsonResult PostAddProduct(ProductSelect product,
            int CustomerID, int ContractID)
        {
            try
            {
                List<ProductSelect> products = new List<ProductSelect>();
                products.Add(product);


                ProductData pd = new ProductData();
                pd.AddProductSelect(products, CustomerID, ContractID);

                // getProduct By Contract
                ProductData dataPro = new ProductData();
                IList<ProductSelect> listProductsSelect = new List<ProductSelect>();
                listProductsSelect = dataPro.GetProductCustomer(CustomerID, ContractID);
                decimal ContractPayment = Convert.ToDecimal(listProductsSelect.Sum(c => c.ProductPrice));
                ContractData cd = new ContractData();
                cd.UpdateContractPayment(ContractID, CustomerID, ContractPayment);

                return Json(new
                {
                    data = "",
                    success = true
                }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    data = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        
        }
    }
}
