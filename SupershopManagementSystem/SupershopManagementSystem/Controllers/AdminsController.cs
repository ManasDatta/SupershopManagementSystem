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
    public class AdminsController : Controller
    {
        private SMSDBContext db = new SMSDBContext();
       
        public ActionResult Home()
        {
            var x = Convert.ToInt32(Session["ADMINID"]);
            Admin admin = db.Admins.Find(x);
            //Response.Write("<script>alert('" + admin.AdminEmail + "')</script>");
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View();
        }

        public ActionResult Record(string searchBy, string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (searchBy == "Date")
                {
                    return View(db.Records.Include(p => p.Salesman).Where(x => x.InvoiceDate.StartsWith(search) || search == null).ToList());
                }
                else
                {
                    return View(db.Records.Include(p => p.Salesman).Where(x => x.Salesman.SalesmanName.ToString().StartsWith(search) || search == null).ToList());
                }
                //var records = db.Records.Include(r => r.Salesman);
                //return View(records.ToList());
            }
        }

        // GET: Admins
        public ActionResult Index(string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View(db.Admins.Where(x => x.AdminName.StartsWith(search) || search == null).ToList());
                //return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                var x = Convert.ToInt32(Session["ADMINID"]);
                id = x;
                if (id == null)
                    
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,AdminName,AdminEmail,AdminPhone,AdminAddress,AdminDOB,AdminPassword")] Admin admin)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    var x = admin.AdminId;
                    Response.Write("<script>alert('Login id is : "+x+"')</script>");
                    //return RedirectToAction("Index");
                }

                return View(admin);
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,AdminName,AdminEmail,AdminPhone,AdminAddress,AdminDOB,AdminPassword")] Admin admin)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Logout");
                }
                return View(admin);
            }
        }

        // GET: Admins/Delete/5
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
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                Admin admin = db.Admins.Find(id);
                db.Admins.Remove(admin);
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
            return RedirectToAction("Login","AdminLogin");
        }
    }
}
