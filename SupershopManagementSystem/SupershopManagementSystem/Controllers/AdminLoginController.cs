using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSDataLayer;

namespace SupershopManagementSystem.Controllers
{
    public class AdminLoginController : Controller
    {

        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            using (SMSDBContext db = new SMSDBContext())
            {
                Admin usr = db.Admins.SingleOrDefault(u => u.AdminId == admin.AdminId && u.AdminPassword == admin.AdminPassword);
                if (usr != null)
                {
                    var id = admin.AdminId.ToString();
                    Session["ADMINID"] = id;
                    Session["ADMIN"] = "admin";
                    return RedirectToAction("Home", "Admins");
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