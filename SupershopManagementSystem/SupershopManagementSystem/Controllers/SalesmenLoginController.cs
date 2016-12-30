using SMSDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupershopManagementSystem.Controllers
{
    public class SalesmenLoginController : Controller
    {
        // GET: SalesmenLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Salesman salesman)
        {
            using (SMSDBContext db = new SMSDBContext())
            {
                var usr = db.Salesmans.SingleOrDefault(u => u.SalesmanId == salesman.SalesmanId && u.SalesmanPassword == salesman.SalesmanPassword);
                if (usr != null)
                {
                    var id = salesman.SalesmanId.ToString();
                    Session["SALESMANID"] = id;
                    Session["SALESMAN"] = "salesman";
                    return RedirectToAction("Home", "Salesmen");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid!");
                }
            }
            return View();
        }
    }
}