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
    public class SalesmenController : Controller
    {
        private SMSDBContext db = new SMSDBContext();
        
        public ActionResult Home()
        {
            var x = Convert.ToInt32(Session["SALESMANID"]);
            Salesman salesman = db.Salesmans.Find(x);
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
                return View();
        }

        public ActionResult Product(string searchBy, string search)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
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
                //return View(db.Products.Include(p => p.Category).Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
                //var products = db.Products.Include(p => p.Category);
                //return View(products.ToList());
            }
        }

        // GET: Salesmen
        public ActionResult Index(string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View(db.Salesmans.Where(x => x.SalesmanName.StartsWith(search) || search == null).ToList());
        }

        // GET: Salesmen/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                var x = Convert.ToInt32(Session["SALESMANID"]);
                id = x;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salesman salesman = db.Salesmans.Find(id);
                if (salesman == null)
                {
                    return HttpNotFound();
                }
                return View(salesman);
            }
        }

        // GET: Salesmen/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View();
        }

        // POST: Salesmen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesmanId,SalesmanName,SalesmanEmail,SalesmanPhone,SalesmanAddress,SalesmanDOB,SalesmanJoiningDate,SalesmanSalary,SalesmanPassword")] Salesman salesman)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Salesmans.Add(salesman);
                    db.SaveChanges();
                    var x = salesman.SalesmanId;
                    Response.Write("<script>alert('Login id is : " + x + "')</script>");
                    //return RedirectToAction("Index");
                }

                return View(salesman);
            }
        }

        // GET: Salesmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salesman salesman = db.Salesmans.Find(id);
                if (salesman == null)
                {
                    return HttpNotFound();
                }
                return View(salesman);
            }
        }

        // POST: Salesmen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesmanId,SalesmanName,SalesmanEmail,SalesmanPhone,SalesmanAddress,SalesmanDOB,SalesmanJoiningDate,SalesmanSalary,SalesmanPassword")] Salesman salesman)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(salesman).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Logout");
                }
                return View(salesman);
            }
        }

        // GET: Salesmen/Delete/5
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
                Salesman salesman = db.Salesmans.Find(id);
                if (salesman == null)
                {
                    return HttpNotFound();
                }
                return View(salesman);
            }
        }

        // POST: Salesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                Salesman salesman = db.Salesmans.Find(id);
                db.Salesmans.Remove(salesman);
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
            return RedirectToAction("Login", "SalesmenLogin");
        }
    }
}
