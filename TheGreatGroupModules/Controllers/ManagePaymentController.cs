using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGreatGroupModules.Controllers
{
    public class ManagePaymentController : Controller
    {
        //
        // GET: /ManagePayment/

        public ActionResult Installment()
        {
            return View();
        }

        public ActionResult CustomerHistory()
        {
            return View();
        }
        
    }
}
