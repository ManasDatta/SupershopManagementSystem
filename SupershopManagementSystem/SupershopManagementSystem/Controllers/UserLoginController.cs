using SMSDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupershopManagementSystem.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (SMSDBContext db = new SMSDBContext())
            {
                var usr = db.Users.SingleOrDefault(u => u.UserId == user.UserId && u.UserPassword == user.UserPassword);
                if (usr != null)
                {
                    var id = user.UserId.ToString();
                    Session["USERID"] = id;
                    Session["USER"] = "user";
                    return RedirectToAction("Home", "Users");
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