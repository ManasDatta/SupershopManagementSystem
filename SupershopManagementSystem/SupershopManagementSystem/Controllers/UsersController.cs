using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMSDataLayer;

namespace SupershopManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private SMSDBContext db = new SMSDBContext();
        public ActionResult Home()
        {
            var x = Convert.ToInt32(Session["USERID"]);
            User user = db.Users.Find(x);
            if (Session["USER"] == null || Session["USER"].ToString() != "user")
                return RedirectToAction("Login", "UserLogin");

            else
            return View();
        }

        // GET: Users
        public ActionResult Index(string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View(db.Users.Where(x => x.UserName.StartsWith(search) || search == null).ToList());
                //return View(db.Users.ToList());
        }

        public ActionResult Product(string searchBy, string search)
        {
            if (Session["USER"] == null || Session["USER"].ToString() != "user")
                return RedirectToAction("Login", "UserLogin");
            else
            {
                if (searchBy == "Pname")
                {
                    return View(db.Products.Include(p => p.Category).Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
                }
                else
                {
                    return View(db.Products.Include(p => p.Category).Where(x => x.Category.CategoryName.ToString().StartsWith(search) || search == null).ToList());
                }
                //var products = db.Products.Include(p => p.Category);
                //return View(products.ToList());

                //return View(db.Products.Include(p => p.Category).Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["USER"] == null || Session["USER"].ToString() != "user")
                return RedirectToAction("Login", "UserLogin");
            else
            {
                var x = Convert.ToInt32(Session["USERID"]);
                id = x;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
           
                return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserPhone,UserPassword")] User user)
        {
           
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var x = user.UserId;
                    Response.Write("<script>alert('Registration Successfull! Your Login id is : " + x + "')</script>");
                    //return RedirectToAction("Login","UserLogin");
                }

                return View(user);
            
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["USER"] == null || Session["USER"].ToString() != "user")
                return RedirectToAction("Login", "UserLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserPhone,UserPassword")] User user)
        {
            if (Session["USER"] == null || Session["USER"].ToString() != "user")
                return RedirectToAction("Login", "UserLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Logout");
                }
                return View(user);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "UserLogin");
        }
    }
}
