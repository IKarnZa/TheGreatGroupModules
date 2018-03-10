
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

        // GET: /Products/GetListProductCart?CustomerID:id
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
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }



         
        }

        //TODO :: AddProductData
        public JsonResult PostAddProduct(List<ProductSelect> products,
            int CustomerID, int ContractID)
        {
            try
            {
                ProductData pd = new ProductData();

                pd.AddProductSelect(products, CustomerID, ContractID);
         
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
